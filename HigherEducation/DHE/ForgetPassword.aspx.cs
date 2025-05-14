using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BAL;
using HigherEducation.BusinessLayer;
using MySql.Data.MySqlClient;

namespace HigherEducation.HigherEducations
{
    [Obsolete]
    public partial class ForgetPassword : System.Web.UI.Page
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection con = new MySqlConnection(ConStrHE);
        MySqlCommand cmd;
        string urllogin;
        DataTable dtbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
           // eSessionMgmt.checkreferer();
            try
            {
                String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
                urllogin = parentDirectory + "/frmlogin.aspx";
                lblError.Visible = false;
                if (!IsPostBack)
                {
                    if (Request.QueryString["resetpass"] != null)
                    {
                        EncryptionDecryption encrypt = new EncryptionDecryption();
                        string keyy = "iFOrgOTmYpA$$wOrDsOkIndlyGiVemeMyP@ss";
                        string username =Request.QueryString["resetpass"].ToString();
                        string decrypt1=username;// = username.Substring(0, username.IndexOf("$$$$$"));
                        string username123 = encrypt.DecryptString(decrypt1.Replace(" ", "+"), keyy); // username + time
                        string realusername = username123.Substring(0, username123.IndexOf("#####")); // username
                        string time = username123.Substring(username123.IndexOf("#####") + 5);
                       // string getmd5 = username.Substring(username.IndexOf("$$$$$") + 5);
                        int exp = 0;
                       // if (FormsAuthentication.HashPasswordForStoringInConfigFile(username123, "MD5") == getmd5) // check md5 to check invalid request
                      //{
                            DateTime dateTime;
                            if (DateTime.TryParse(time, out dateTime))
                            {
                                TimeSpan timeDiff = DateTime.Now - dateTime;
                                if (timeDiff.TotalMinutes <= 20)
                                {
                                    //lblError.Visible = true;
                                    //lblError.Text = realusername;
                                    ViewState["Name"] = realusername;
                                    lblSMsg.Visible = false;
                                    lblSMsg.Text = "";
                                    Random randomClass = new Random();
                                    int rndno = randomClass.Next();
                                    ViewState["rndNo"] = rndno;
                                    btnResetPassword.Attributes.Add("OnClick", "javascript:return md5auth(" + rndno + ")");
                                }
                                else
                                {
                                    exp = 1;
                                    lblError.Visible = true;
                                    lblError.ForeColor = Color.White;
                                    lblError.Text = "URL expired !! Kindly visit " + urllogin + " to login";
                                    lblSMsg.Visible = false;
                                    lblSMsg.Text = "";
                                }
                            }
                            else
                            {
                                lblError.Visible = true;
                                lblError.ForeColor = Color.White;
                                lblError.Text = "Invalid Request !! Kindly visit " + urllogin + " to login";
                                lblSMsg.Visible = false;
                                lblSMsg.Text = "";
                            }
                        //}
                        //else
                        //{
                        //    lblError.Visible = true;
                        //    lblError.ForeColor = Color.White;
                        //    lblError.Text = "Invalid Request !! Kindly visit " + urllogin + " to login";
                        //    lblSMsg.Visible = false;
                        //    lblSMsg.Text = "";
                        //}
                        if (exp == 1)
                        {
                            exp = 0;
                            txtCNewPassword.Visible = false;
                            txtNewPassword.Visible = false;
                            btnResetPassword.Visible = false;
                            dvNote.Visible = false;
                        }
                        else
                        {
                             txtNewPassword.Visible = true;
                            txtCNewPassword.Visible = true;
                            btnResetPassword.Visible = true;
                            dvNote.Visible = true;
                        }
                        
                        txtUserId.Visible = false;
                        RequiredFieldValidator1.Enabled = false;
                        btnSubmit.Visible = false;
                        dvNote.Visible = false;
                        
                    }
                    else
                    {
                        //btnSubmit.Attributes.Add("OnClick", "javascript:return checkUserName()");
                        lblError.Visible = false;
                        lblSMsg.Visible = false;
                        lblSMsg.Text = "";
                        txtNewPassword.Visible = false;
                        txtCNewPassword.Visible = false;
                        txtUserId.Visible = true;
                        RequiredFieldValidator1.Enabled = true;
                        btnResetPassword.Visible = false;
                        btnSubmit.Visible = true;
                        dvNote.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.ForeColor = Color.White;
                lblError.Text = ex.Message;
                lblSMsg.Visible = false;
                lblSMsg.Text = "";
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                #region captcha
                if (Convert.ToString(Session["randomStr"]) == "" || Convert.ToString(Session["randomStr"]) == null)
                {
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "Message3", "alert('Please Try Again!!!');", true);
                    clsAlert.AlertMsg(this, "Please Try Again!!!");
                    txtturing.Text = String.Empty;
                    return;
                }
                string RNDStr = Session["randomStr"].ToString();
                if (txtturing.Text == "")
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Message2", "alert('Please enter captcha');", true);
                    clsAlert.AlertMsg(this, "Please enter captcha!");
                    return;
                }
                else
                {
                    if (txtturing.Text.Trim() != RNDStr.Trim())
                    {
                        //alert("Please enter your code correctly!!!");
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Message1", "alert('Please enter captcha correctly!!!');", true);
                        clsAlert.AlertMsg(this, "Please enter captcha correctly!!!");
                        txtturing.Text = string.Empty;
                        return;
                    }
                }
                #endregion
                string strQry = "Select Count(1) from tbllogin where (UserID = @UserID or EmailId=@UserID or MobileNo=@UserID);";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd = new MySqlCommand(strQry, con);
                cmd.Parameters.AddWithValue("@UserID", txtUserId.Text);
                int n = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                if (n >= 1)
                {
                    lblError.Visible = false;
                    lblSMsg.Visible = false;
                    lblSMsg.Text = "";
                    string strQry1 = "Select EmailId, MobileNo from tbllogin where (UserID = @UserID or EmailId=@UserID or MobileNo=@UserID);";
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    MySqlDataAdapter dad;
                    dad = new MySqlDataAdapter(strQry1, con);
                    dad.SelectCommand.Parameters.AddWithValue("@UserID", txtUserId.Text);
                    // dad.SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = txtUserId.Text;
                    dtbl = new DataTable();
                    dad.Fill(dtbl);
                    con.Close();
                    if (dtbl.Rows.Count >= 1)
                    {
                        string mobileNo = Convert.ToString(dtbl.Rows[0]["MobileNo"]);
                        string emailId = Convert.ToString(dtbl.Rows[0]["EmailId"]);
                        if (string.IsNullOrEmpty(mobileNo) && string.IsNullOrEmpty(emailId))
                        {
                            lblError.Visible = true;
                            lblError.ForeColor = Color.White;
                            lblError.Text = "Email Id and Mobile Number is not registered for this UserId.<br/> Kindly get it registered";
                            lblSMsg.Visible = false;
                            lblSMsg.Text = "";
                        }
                        else if (string.IsNullOrEmpty(mobileNo))
                        {
                            lblError.Visible = true;
                            lblError.ForeColor = Color.White;
                            lblError.Text = "Mobile Number is not registered for this UserId.<br/> Kindly get it registered";
                            lblSMsg.Visible = false;
                            lblSMsg.Text = "";
                        }
                        else if (string.IsNullOrEmpty(emailId))
                        {
                            lblError.Visible = true;
                            lblError.ForeColor = Color.White;
                            lblError.Text = "Email Id is not registered for this UserId.<br/> Kindly get it registered";
                            lblSMsg.Visible = false;
                            lblSMsg.Text = "";
                        }
                        else
                        {
                            string keyy = "iFOrgOTmYpA$$wOrDsOkIndlyGiVemeMyP@ss";
                            string username = txtUserId.Text + "#####" + DateTime.Now.ToString();
                            EncryptionDecryption encrypt = new EncryptionDecryption();
                            username = encrypt.EncryptString(username, keyy); //+ "$$$$$" + FormsAuthentication.HashPasswordForStoringInConfigFile(username, "MD5");
                            //string authority = HttpContext.Current.Request.Url.Authority;
                            string authority = Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath;
                          
                            //string url = authority + "/DHE/ForgetPassword.aspx?resetpass=" + username;//Staging
                            string url = "https://admissions.itiharyana.gov.in/UG/DHE/ForgetPassword.aspx?resetpass=" + username;// PROD
                            string smsurl = HttpUtility.UrlEncode(url);
                            //string smscheck = "Password reset link for UserID " + txtUserId.Text + " is - " + smsurl + " Regards, DHE Haryana";
 
                             AgriSMS.sendUnicodeSMS(mobileNo, "Password reset link for UserID " + txtUserId.Text + " is - " + smsurl + " Regards, SDIT Haryana", "1007366557264468513");
                            string urlSubject = "Password Reset Link for URL - " + authority;
                            string msg = string.Empty;
                            //Click to Reset Password
                            msg = "Password reset link for UserID " + txtUserId.Text + " is -  <a href=\"" + url+"\">"+ url.ToString() + "</a> <br/> Regards, SDIT Haryana";
                            SMS.SendEmail(emailId, urlSubject, msg);
                            string maskemailId = string.Format("{0}****{1}", emailId[0],emailId.Substring(emailId.IndexOf('@') - 1));
                            string maskmobileNo= "XXXXXXXX" + mobileNo.Substring(mobileNo.Length - 4);
                            lblSMsg.Visible = true;
                            lblSMsg.ForeColor = Color.White;
                            lblSMsg.Text = "Password reset link sent to registered Email Id " + maskemailId + "<br/> and Mobile No. " + maskmobileNo + "<br/> This link is valid for next 20 minutes";
                            
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.ForeColor = Color.White;
                        lblError.Text = "Please Enter Valid UserId";
                        lblSMsg.Visible = false;
                        lblSMsg.Text = "";
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.ForeColor = Color.White;
                    lblError.Text = "Please Enter Valid UserId";
                    lblSMsg.Visible = false;
                    lblSMsg.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.ForeColor = Color.White;
                lblError.Text = ex.Message;
                lblSMsg.Visible = false;
                lblSMsg.Text = "";
            }
        }
        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            clsLoginUser objLogin = new clsLoginUser();
            int IsNewPwdMatchWithOld = 0;
            DataSet dtResult;
            try
            {
                #region captcha
                if (Convert.ToString(Session["randomStr"]) == "" || Convert.ToString(Session["randomStr"]) == null)
                {
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "Message3", "alert('Please Try Again!!!');", true);
                    clsAlert.AlertMsg(this, "Please Try Again!!!");
                    txtturing.Text = String.Empty;
                    return;
                }
                string RNDStr = Session["randomStr"].ToString();
                if (txtturing.Text == "")
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Message2", "alert('Please enter captcha');", true);
                    clsAlert.AlertMsg(this, "Please enter captcha!");
                    return;
                }
                else
                {
                    if (txtturing.Text.Trim() != RNDStr.Trim())
                    {
                        //alert("Please enter your code correctly!!!");
                        // ScriptManager.RegisterStartupScript(this, GetType(), "Message1", "alert('Please enter captcha correctly!!!');", true);
                        clsAlert.AlertMsg(this, "Please enter captcha correctly!!!");
                        txtturing.Text = string.Empty;
                        return;
                    }
                }
                #endregion
                if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()) || string.IsNullOrEmpty(txtCNewPassword.Text.Trim()))
                {
                    lblError.Visible = true;
                    lblError.ForeColor = Color.White;
                    lblError.Text = "Please enter New Password/Confirm New Password";
                    lblSMsg.Visible = false;
                    lblSMsg.Text = "";
                    RegularExpressionValidator1.Text = "";
                    RegularExpressionValidator2.Text = "";
                }
                else
                {
                    lblError.Visible = false;
                    lblSMsg.Visible = false;
                    lblSMsg.Text = "";
                    //int res1 = 0;
                    //string strHashnew = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text, "MD5");
                    string strHashnew = txtNewPassword.Text;
                     string str1 = Convert.ToString(ViewState["rndNo"]);
                     int strLen = str1.Length;
                     strHashnew = strHashnew.Substring(strLen);
                   // strHashnew = MD5Hash(txtNewPassword.Text);
                    //strHashnew = Mid(strHashnew, Convert.ToInt16(l) + 1);
                    string UserSessionID = Convert.ToString(Session.SessionID);
                    objLogin.flag = "Change_Pwd";
                    Session["UserID"] = objLogin.UserID = Convert.ToString(ViewState["Name"]);
                    objLogin.Password = strHashnew;
                    objLogin.UserSessionID = UserSessionID;
                    IsNewPwdMatchWithOld = objLogin.AuthenticateUser_WithoutSeed();
                    if (IsNewPwdMatchWithOld == 1)
                    {
                        TraceUserAction("EF");
                        lblError.Visible = true;
                        lblError.ForeColor = Color.White;
                        lblError.Text = "New password cannot be same as last 5 old passwords..";
                        lblSMsg.Visible = false;
                        lblSMsg.Text = "";
                    }
                    else
                    {
                        if (txtNewPassword.Text.Trim() == txtCNewPassword.Text.Trim())
                        {
                            UserSessionID = Convert.ToString(Session.SessionID);
                            objLogin.flag = "Update_Pwd";
                            objLogin.UserID = Convert.ToString(ViewState["Name"]);
                            objLogin.Password = strHashnew;
                            objLogin.UserSessionID = UserSessionID;
                            dtResult = new DataSet();
                            dtResult = objLogin.TryToLoginUser();
                            if (Convert.ToString(dtResult.Tables[0].Rows[0]["Result"]) == "1")
                            {
                                TraceUserAction("E");
                                clsLoginUser.ClearPreviousSession();
                                //lblSMsg.Visible = true;
                                //lblSMsg.ForeColor = Color.White;
                                //lblSMsg.Text = "Password reset successfully.";
                                //SQLHelper help = new SQLHelper();
                                clsAlert.Alert(this, "Password Changed successfully. Kindly login again with New Password", "frmlogin.aspx");
                            }
                            else
                            {
                                TraceUserAction("EF");
                                lblError.Visible = true;
                                lblError.ForeColor = Color.White;
                                lblError.Text = "ERROR : Password NOT Changed";
                                lblSMsg.Visible = false;
                                lblSMsg.Text = "";
                            }
                        }
                        else
                        {
                            TraceUserAction("EF");
                            lblError.Visible = true;
                            lblError.ForeColor = Color.White;
                            lblError.Text = "New Password and Confirm New Password do not match";
                            lblSMsg.Visible = false;
                            lblSMsg.Text = "";
                        }
                    }
                    RegularExpressionValidator1.Text = "";
                    RegularExpressionValidator2.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = false;
                lblError.ForeColor = Color.White;
                lblError.Text = ex.Message;
                lblSMsg.Visible = false;
                lblSMsg.Text = "";
            }
        }
        public static string MD5Hash(string input)
        {
            System.Text.StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        private void TraceUserAction(string TransactionType)
        {
            clsLoginUser.SavePwdChangeUserLog(TransactionType);
        }
    }
}