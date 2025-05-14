using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data;
using HigherEducation.BusinessLayer;

namespace HigherEducation.HigherEducations
{
    public partial class changepwd : System.Web.UI.Page
    {
        private Int32 rndno;
       
        private string TransactionType = "";
        protected void Button1_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid == true)
            {
                try
                {
                    Int32 IsOldPwdCorrect = 0;
                    Int32 IsNewPwdMatchWithOld = 0;
                    // Dim user As New User
                    Int32 rndno = Convert.ToInt32(Session["rndno1"]); ;
                    string UserSessionID = Convert.ToString(Session.SessionID);
                    // string strHash = Mid(txtoldpwd.Value, Convert.ToInt16(rndno.ToString().Length) + 1); // extract hashed password string from seeded hashed password
                    string strHash = txtoldpwd.Value.Substring(Convert.ToInt16(rndno.ToString().Length));
                    clsLoginUser objLogin = new clsLoginUser();
                    objLogin.flag = "Change_Pwd";
                    objLogin.UserID = txtuid.Value.Trim();
                    objLogin.Password = strHash;
                    objLogin.UserSessionID = UserSessionID;
                    IsOldPwdCorrect = objLogin.AuthenticateUser_WithoutSeed();
                    if ((IsOldPwdCorrect == 0))
                    {
                        TraceUserAction("EF");
                       clsAlert.AlertMsg(this, "Incorrect Old password");
                        return;
                    }
                    else
                    {
                        // If old pwd and newpwd are same then give error msg
                        string strHashnew = txtnewpwd1.Value;
                        //strHashnew = Mid(strHashnew, Convert.ToInt16(rndno.ToString.Length) + 1)
                        strHashnew = strHashnew.Substring(Convert.ToInt16(rndno.ToString().Length));
                        objLogin.Password = strHashnew;
                        IsNewPwdMatchWithOld = objLogin.AuthenticateUser_WithoutSeed();
                        if ((IsNewPwdMatchWithOld == 1))
                        {
                            TraceUserAction("EF");
                            clsAlert.AlertMsg(this, "New password cannot be same as last 5 old passwords..");
                        }
                        else if (string.Compare(txtnewpwd1.Value, txtnewpwd2.Value, false) == 0)
                        {
                            //string newpwd = Mid(txtnewpwd1.Value, Convert.ToInt16(rndno.ToString().Length) + 1);
                            string newpwd = txtnewpwd1.Value.Substring(Convert.ToInt16(rndno.ToString().Length));
                            DataSet dtResult = new DataSet();
                            objLogin.flag = "Update_Pwd";
                            objLogin.Password = newpwd;
                            dtResult = objLogin.TryToLoginUser();
                            if ((Convert.ToString(dtResult.Tables[0].Rows[0]["Result"]) == "1"))
                            {
                                Session["MsgToChangePwd"] = "n";
                                TraceUserAction("E");
                                clsLoginUser.ClearPreviousSession();
                                clsAlert.Alert(this, "Password Changed successfully. Kindly login again with New Password", "frmlogin.aspx");
                            }
                            else
                            {
                                TraceUserAction("EF");
                                clsAlert.AlertMsg(this, "ERROR : Password NOT Changed");
                            }
                        }
                        else
                        {
                            TraceUserAction("EF");
                            clsAlert.AlertMsg(this, "New Password AND Confirm New Password DoNot match");
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected void Page_Load(object sender, System.EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.checkreferer();
            if ((clsLoginUser.CheckChangePwdSession()))
            {
                if (Convert.ToString(Session["ForcePwdChange"]) == "n")
                    lblmsg.Visible = true;
                else
                    lblmsg.Visible = false;
                try
                {
                    Int32 rndno = 0;
                    if (!IsPostBack)
                    {
                        // Dim user As New User
                        txtuid.Value = Convert.ToString(Session["UserId"]);
                        lblunm.Value = Convert.ToString(Session["UserName"]);
                        Random randomclass = new Random();
                      Session["rndno1"] = randomclass.Next();
                    }
                    rndno = Convert.ToInt32(Session["rndno1"]);
                    Button1.Attributes.Add("OnClick", "javascript:return md5auth(" + rndno + ")");
                }
                catch (Exception ex)
                {
                }
            }
            //Security Check
           // clsLoginUser.SetCookie();
            //Security Check
        }
        protected void btncancel_Click(object sender, System.EventArgs e)
        {
            txtoldpwd.Value = "";
            txtnewpwd1.Value = "";
            txtnewpwd2.Value = "";
            Response.Redirect("frmlogin.aspx");
        }
        protected void Page_Error(object sender, System.EventArgs e)
        {
            
         //   Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/errorpage.aspx", false);
        }
        private void TraceUserAction(string TransactionType)
        {
            clsLoginUser.SavePwdChangeUserLog(TransactionType);
        }
    }
}



