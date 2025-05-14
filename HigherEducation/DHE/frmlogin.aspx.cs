using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;

namespace HigherEducation.HigherEducation
{
    public partial class frmlogin : System.Web.UI.Page
    {
        private int Result;
        private int rno = 0;
        private string HashPwd = "";
        private string TransactionType = "";
        // private SQLHelper help = new SQLHelper();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Session["UserId"] = "";
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1.0);
            Response.Cache.SetNoServerCaching();
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
            Response.Expires = -1500;
            Response.Buffer = true;
            Response.Expires = 0;
            if (!IsPostBack)
            {
                int valueencrypt1 = 0;
                Random randomclassencypt1 = new Random();
                valueencrypt1 = randomclassencypt1.Next(1111, 8888);
                string viewstatevalue = Convert.ToString(valueencrypt1);
                ViewState.Add(viewstatevalue, 0);
                Session.Add("rno", 0);
                Random randomclass = new Random();
                Session["rno"] = randomclass.Next();
                rno = Convert.ToInt32(Session["rno"]);
                Session["Attempt"] = 0;
                txtturing.Attributes.Add("autocomplete", "off");
            }
            btnlogin.Attributes.Add("OnClick", "javascript:md5auth(" + Session["rno"] + ");");
            btnlogin1.Attributes.Add("OnClick", "javascript:md5auth(" + Session["rno"] + ");");
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (Page.IsValid == true)
                {
                    //if (Convert.ToInt32(Session["Attempt"]) >= 1)
                    //{
                    //    dvCaptcha.Style.Add("display", "Inline");
                    //}
                    //else
                    //{
                    //    dvCaptcha.Style.Add("display", "none");
                    //}
                    //#region captcha
                    //if (Convert.ToString(Session["randomStr"]) == "" || Convert.ToString(Session["randomStr"]) == null)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Message3", "alert('Please Try Again!!!');", true);
                    //    txtturing.Text = String.Empty;
                    //    return;
                    //}
                    //string RNDStr = Session["randomStr"].ToString();
                    //if (txtturing.Text == "" && Convert.ToInt32(Session["Attempt"]) > 1)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "Message2", "alert('Please enter captcha');", true);
                    //    return;
                    //}
                    //else
                    //{
                    //    if (txtturing.Text.Trim() != RNDStr.Trim() && Convert.ToInt32(Session["Attempt"]) > 1)
                    //    {
                    //        //alert("Please enter your code correctly!!!");
                    //        ScriptManager.RegisterStartupScript(this, GetType(), "Message1", "alert('Please enter captcha correctly!!!');", true);
                    //        txtturing.Text = string.Empty;
                    //        return;
                    //    }
                    //}
                    //#endregion
                    if (Session["UserId"] == null || Convert.ToString(Session["UserId"]) == "")
                    {
                        Session.Add("Welcome", "n");
                        Session.Add("MsgToChangePwd", "n");
                        Session.Add("LoginUserId", txtUserId.Text.Trim());
                        if ((!revUseralphanum.IsValid || !revpassword.IsValid))
                        {
                            TraceUserAction("LF");
                            clsAlert.AlertMsg(this, "Login Failed, Please Enter Valid UserID and/or Password...");
                            label3.Text = "Login Failed, Please Enter Valid UserID and/or Password...";
                            label3.Visible = true;
                            Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                            return;
                        }
                        else
                        {
                            if (Session["rno"] == null)
                            {
                                clsAlert.Alert(this, "Your session was idle for long time.. So Please close this window and open the another one!", "frmlogin.aspx");
                                return;
                            }
                            else
                            {
                                if (IsAccountLocked() == "No")
                                {
                                    AuthenticateUser();
                                }
                            }

                        }
                    }
                    // Session Userid null means not login
                    else
                    {
                        clsAlert.AlertMsg(this, "Multiple users can not login at same time.kindly logout the current user or use another browser");
                       
                       
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "Login Failed, Please Enter Valid UserID and/or Password...");
                    label3.Text = "Login Failed, Please Enter Valid UserID and/or Password...";
                    label3.Visible = true;
                    Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "frmlogin";
                clsLogger.ExceptionMsg = "btnlogin_Click";
                clsLogger.SaveException();
                clsAlert.AlertMsg(this, "Server busy.... Please try after some time.");
                label3.Text = "Server busy.... Please try after some time.";
                label3.Visible = true;
            }
        }

        private string IsAccountLocked()
        {
            string IsLocked = "Yes";

            DataSet dtAccStatus = new DataSet();
            clsLoginUser objLogin = new clsLoginUser();
            objLogin.flag = "CheckAccStatus";
            objLogin.UserID = txtUserId.Text.Trim();
            objLogin.UserSessionID = Convert.ToString(Session.SessionID);
            dtAccStatus = objLogin.TryToLoginUser();
            if (dtAccStatus.Tables[0].Rows.Count > 0)   //if there is such record
            {
                if (dtAccStatus.Tables[0].Rows[0]["IsAccFreeze"].ToString() == "Yes")
                {
                    TraceUserAction("LF");
                    clsAlert.Alert(this, "Your account is locked...Please try after 10 minutes...", "Errorpage_AccountFreeze.aspx");
                    Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                    return IsLocked;
                }
                else if (dtAccStatus.Tables[0].Rows[0]["IsLastSessionLogOut"].ToString().Trim() == "No")
                {
                    clsLoginUser.ClearPreviousSession();
                    Response.Redirect("frmlogin.aspx");
                    //clsAlert.Alert(this, "Your account is not properly logout previous time... So Please login again!!!", "frmlogin.aspx");
                    Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                    return IsLocked;
                }
                else if (dtAccStatus.Tables[0].Rows[0]["UserType"].ToString() == "A")
                {
                    TraceUserAction("LF");
                    clsAlert.Alert(this, "You are not authorised to login!!!", "frmlogin.aspx");
                    Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                    return IsLocked;
                }
                else
                {
                    IsLocked = "No";
                }
            }
            return IsLocked;
        }

        private void AuthenticateUser()
        {
           
            DataSet dt = new DataSet();
            DataTable dtUserDetail = new DataTable();
            clsLoginUser objLoginUser = new clsLoginUser();
            objLoginUser.UserID = txtUserId.Text.Trim();
            objLoginUser.Password = txtpassword.Text.Trim();
            objLoginUser.SeedNumber = Convert.ToString(Session["rno"]);
            objLoginUser.UserSessionID = Convert.ToString(Session.SessionID);
            dt = objLoginUser.AuthenticateUser();
            if (dt.Tables.Count > 0)
            {
                dtUserDetail = dt.Tables[0];
                if (dtUserDetail.Rows.Count > 0)
                {
                    Result = Convert.ToInt32(dtUserDetail.Rows[0]["Result"]);
                    if (Result == 0)
                    {
                        TraceUserAction("LF");
                        clsAlert.AlertMsg(this, "Login Failed..Either UserID or Password is not correct..");
                        label3.Text = "Login Failed..Either UserID or Password is not correct..";
                        Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                        label3.Visible = true;
                    }
                    else if (Result == 2)
                    {
                        TraceUserAction("LF");
                        clsAlert.AlertMsg(this, "Login Failed..Either UserID or Password is not correct..");
                        label3.Text = "Login Failed..Either UserID or Password is not correct..";
                        Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                        label3.Visible = true;
                    }
                    else if (Result == 3)
                    {
                        TraceUserAction("LF");
                        clsAlert.AlertMsg(this, "Login Failed..Your Account is NOT activated!");
                        label3.Text = "Login Failure.. Your Account is NOT activated!";
                        Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                        label3.Visible = true;
                    }
                    else
                    {
                        if (Convert.ToInt32(dtUserDetail.Rows[0]["IsLoginSuccessfull"]) == 1)
                        {
                            //Session.Add("Welcome", "y");
                            Session.Add("UserName", Convert.ToString(dtUserDetail.Rows[0]["UserName"]).Trim());
                            Session.Add("UserType", Convert.ToString(dtUserDetail.Rows[0]["UserType"]).Trim());
                            Session.Add("MsgToChangePwd", Convert.ToString(dtUserDetail.Rows[0]["MsgToChangePwd"]).Trim());
                            Session.Add("UserId", Convert.ToString(dtUserDetail.Rows[0]["UserID"]).Trim());
                            Session.Add("ForcePwdChange", Convert.ToString(dtUserDetail.Rows[0]["ForcePwdChange"]).Trim());
                            Session.Add("CollegeId", Convert.ToString(dtUserDetail.Rows[0]["CollegeId"]).Trim());
                            Session.Add("CollegeName", Convert.ToString(dtUserDetail.Rows[0]["CollegeName"]).Trim());
                            Session.Add("CollegeType", Convert.ToString(dtUserDetail.Rows[0]["CollegeType"]).Trim());
                            Session.Add("PUniversityId ", Convert.ToString(dtUserDetail.Rows[0]["PUniversityId"]).Trim());
                            TraceUserAction("L");
                            //if (dt.Tables.Count > 1)
                            //{
                            //    DataTable dtUserLocationDetail = new DataTable();
                            //    dtUserLocationDetail = dt.Tables[1];
                            //    if (dtUserLocationDetail.Rows.Count > 0)
                            //    {
                            //        Session.Add("UserDName", Convert.ToString(dtUserLocationDetail.Rows[0]["dname"]).Trim());
                            //        Session.Add("UserTName", Convert.ToString(dtUserLocationDetail.Rows[0]["tname"]).Trim());
                            //        Session.Add("UserTypeDesc", Convert.ToString(dtUserLocationDetail.Rows[0]["UserTypeDesc"]).Trim());
                            //    }
                            //}
                            
                            if (Convert.ToString(dtUserDetail.Rows[0]["ForcePwdChange"]).Trim() == "n")
                            {
                                clsLoginUser.SetCookie();
                                // Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/changepwd.aspx", false);
                                Response.Redirect("changepwd.aspx", false);
                            }
                            else
                            {
                                Transfer_User_To_Page(Convert.ToString(Session["UserType"]));
                            }
                        }
                    }
                }
                else
                {
                    TraceUserAction("LF");
                    clsAlert.AlertMsg(this, "Login Failed..Either UserID or Password is not correct..");
                    label3.Text = "Login Failed..Either UserID or Password is not correct..";
                    Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                    label3.Visible = true;
                }
            }
            else
            {
                TraceUserAction("LF");
                clsAlert.AlertMsg(this, "Login Failed..Either UserID or Password is not correct..");
                label3.Text = "Login Failed..Either UserID or Password is not correct..";
                Session["Attempt"] = Convert.ToInt32(Session["Attempt"]) + 1;
                label3.Visible = true;
            }
        }

        private void Transfer_User_To_Page(string UserType)
        {
            int usertype;
            clsLoginUser.SetCookie();
            int.TryParse(UserType, out usertype);
            // Int16 AppAdminUserTypeCdFrom = Convert.ToInt16(ConfigurationManager.AppSettings["ApplicationAdminUserTypeCdFrom"]);
            // Int16 AppAdminUserTypeCdTo = Convert.ToInt16(ConfigurationManager.AppSettings["ApplicationAdminUserTypeCdTo"]);
            if (UserType == "A")
            {
                Response.Redirect("HEMenu.aspx", false);
            }
            else
            {
                Response.Redirect("HEMenu.aspx", false);
            }
        }

        private void TraceUserAction(string TransactionType)
        {
            if (!string.IsNullOrEmpty(txtUserId.Text.Trim()))
            {
                clsLoginUser.SaveLogInUserLog(TransactionType);
            }
        }


        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}