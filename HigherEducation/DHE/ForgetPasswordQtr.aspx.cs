using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class ForgetPasswordQtr : System.Web.UI.Page
    {
        public string flag;

        public string Password;
        public string UserID;


        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection con = new MySqlConnection(ConStrHE);
        MySqlCommand cmd;
        string urllogin;
       
        DataTable dtbl;
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            //eSessionMgmt.checkreferer();
            try
            {
                String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/UG/DHE"));

                urllogin = parentDirectory;
                lblError.Visible = false;
                lblError.Text = "";

                if (!IsPostBack)
                {
                   
                        lblError.Visible = false;
                        lblSMsg.Visible = false;
                        lblSMsg.Text = "";
                       
                        txtUserId.Visible = true;
                        RequiredFieldValidator1.Enabled = true;
                       
                        btnSubmit.Visible = true;
                        dvNote.Visible = true;
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
            //eSessionMgmt.AntiFixationInit();
           // eSessionMgmt.AntiHijackInit();
            //Security Check
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid == true)
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
                    string strQry = "get_candidate_password_qtr";
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    cmd = new MySqlCommand(strQry, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", txtUserId.Text);
                    int n = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    if (n >= 1)
                    {
                        lblError.Visible = false;
                        lblSMsg.Visible = false;
                        lblSMsg.Text = "";
                       
                        
                        MySqlDataAdapter dad;
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        dad = new MySqlDataAdapter("Select EmailID, MobileNo from seatallotment_qtr where registrationID=@UserID limit 1;", con);
                       
                        dad.SelectCommand.Parameters.AddWithValue("@UserID", txtUserId.Text);
                        // dad.SelectCommand.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = txtUserId.Text;
                        dtbl = new DataTable();
                        dad.Fill(dtbl);
                        con.Close();
                        if (dtbl.Rows.Count >= 1)
                        {
                            string mobileNo = Convert.ToString(dtbl.Rows[0]["MobileNo"]);
                            string emailId = Convert.ToString(dtbl.Rows[0]["EmailID"]);
                            if (string.IsNullOrEmpty(mobileNo) && string.IsNullOrEmpty(emailId))
                            {
                                lblError.Visible = true;
                                lblError.ForeColor = Color.White;
                                lblError.Text = "Email Id and Mobile Number is not registered for this Registration Id.<br/> Kindly get it registered";
                                lblSMsg.Visible = false;
                                lblSMsg.Text = "";
                            }
                            else if (string.IsNullOrEmpty(mobileNo))
                            {
                                lblError.Visible = true;
                                lblError.ForeColor = Color.White;
                                lblError.Text = "Mobile Number is not registered for this Registration Id.<br/> Kindly get it registered";
                                lblSMsg.Visible = false;
                                lblSMsg.Text = "";
                            }
                            else if (string.IsNullOrEmpty(emailId))
                            {
                                lblError.Visible = true;
                                lblError.ForeColor = Color.White;
                                lblError.Text = "Email Id is not registered for this  Registration Id.<br/> Kindly get it registered";
                                lblSMsg.Visible = false;
                                lblSMsg.Text = "";
                            }
                            else
                            {
                                string Password = GetRandomText();
                                string result;
                                result = ResetPassword(Password);
                                if (result == "1")
                                {


                                    AgriSMS.sendSingleSMS(mobileNo, "New Password for Registration ID " + txtUserId.Text + " is -  " + Password + " Regards, SDIT Haryana", "1007868213709844954");

                                    string urlSubject = "New Password for ITI Registration ID " + txtUserId.Text;
                                    string msg = string.Empty;


                                    msg = "New Password for Registration ID  " + txtUserId.Text + " is -  " + Password + "<br/> Regards, SDIT Haryana";

                                    SMS.SendEmail(emailId, urlSubject, msg.Trim());
                                    lblSMsg.Visible = true;
                                    lblSMsg.ForeColor = Color.White;
                                    // lblSMsg.Text = "Password reset link sent to registered Email Id/Mobile No.<br/> This link is valid for next 20 minutes";
                                    lblSMsg.Text = "Password reset successfully and sent to your registered Email Id/Mobile No.";
                                    TraceUserAction("E");
                                    clsLoginUser.ClearPreviousSession();

                                    clsAlert.Alert(this, "Password reset successfully and sent to your registered Email Id/Mobile No. Kindly login again with New Password", urllogin);
                                    return;
                                }
                              else if(result == "2")  //One Day One Reset Password allowed check Only 
                                {
                                    clsAlert.AlertMsg(this, "Reset Password Limit exceeded for today");
                                    return;
                                }
                                else
                                {
                                    TraceUserAction("EF");
                                    lblError.Visible = true;
                                    lblError.ForeColor = Color.White;
                                    lblError.Text = "There is some problem to reset password.. try again later.";
                                    lblSMsg.Visible = false;
                                    lblSMsg.Text = "";
                                }
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
        
        private void TraceUserAction(string TransactionType)
        {
            clsLoginUser.SavePwdChangeUserLog(TransactionType);
        }
      
        public string GetIPAddress()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
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
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "2345679ACEFGHKLMNPRSWXZabcdefghkhmnpqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }
        public string ResetPassword(string Password)
        {

            string resetpwd = "0";
            string IPAddress = GetIPAddress();
            //md5 pwd generation
            string md5pwd = MD5Hash(Password);
            DataSet vds = new DataSet();
            
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                MySqlDataAdapter vadap = new MySqlDataAdapter("ResetPassword", con);
                con.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Flag", "Update_Pwd_qtr");
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserID", txtUserId.Text.Trim());
                vadap.SelectCommand.Parameters.AddWithValue("@p_HashPwd", md5pwd);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);


                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (con.State == ConnectionState.Open)
                    con.Close();
                if (vds.Tables.Count > 0)
                {
                    return resetpwd=vds.Tables[0].Rows[0]["Result"].ToString();
                    
                }
               


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "ForgetPasswordQtr";
                clsLogger.ExceptionMsg = "ResetPassword";
                clsLogger.ExceptionDetail = "UserID_" + txtUserId.Text + "_Flag_" + flag;
                clsLogger.SaveException();
            }
            return resetpwd;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UG/OldSessionQuarterFee/Login");
        }
    }
  

}