//using Bhardwaj;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using HigherEducation.HigherEducation;
using HigherEducation.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using CommonFunctions;
/// <summary>
/// Summary description for clsLoginUser
/// </summary>
namespace HigherEducation.BusinessLayer
{
    public class clsLoginUser
    {
        public clsLoginUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _UserSessionID;

        public string UserSessionID
        {
            get { return _UserSessionID; }
            set { _UserSessionID = value; }
        }

        private string _flag;

        public string flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        private string _IPAddress;

        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }

        private string _SessionId;

        public string SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

        private string _ApplicationCode;

        public string ApplicationCode
        {
            get { return _ApplicationCode; }
            set { _ApplicationCode = value; }
        }

        private string _ModuleCode;

        public string ModuleCode
        {
            get { return _ModuleCode; }
            set { _ModuleCode = value; }
        }

        private string _ReceiptNo;

        public string ReceiptNo
        {
            get { return _ReceiptNo; }
            set { _ReceiptNo = value; }
        }

        private string _TransactionType;

        public string TransactionType
        {
            get { return _TransactionType; }
            set { _TransactionType = value; }
        }

        private string _SeedNumber;

        public string SeedNumber
        {
            get { return _SeedNumber; }
            set { _SeedNumber = value; }
        }

        public string UserName { get; set; }
        public string ActivateAcc { get; set; }
        public string UserType { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string CreateUser { get; set; }
        public string Designation { get; set; }
        public string userchangepwd { get; set; }
        public string IsConfirmationReqd { get; set; }
        public string OfficerWorkingWith { get; set; }
        public string SortCode { get; set; }
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        //  MySqlConnection vconnHE = new MySqlConnection(constr);
        public DataSet GetLoginUserDetail()
        {
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetLoginUserDetail", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserID", _UserID);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "clsLoginUser";
                clsLogger.ExceptionMsg = "GetLoginUserDetail";
                clsLogger.ExceptionDetail = "UserID_" + _UserID;
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        public DataSet TryToLoginUser()
        {
            DataSet vds = new DataSet();
            //DataSet dt = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }

                MySqlDataAdapter vadap = new MySqlDataAdapter("TryToLoginUser", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Flag", _flag);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserID", _UserID);
                vadap.SelectCommand.Parameters.AddWithValue("@p_HashPwd", _Password);
                vadap.SelectCommand.Parameters.AddWithValue("@p_LastSessionID", _UserSessionID);


                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    return vds;
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "clsLoginUser";
                clsLogger.ExceptionMsg = "TryToLoginUser";
                clsLogger.ExceptionDetail = "UserID_" + _UserID + "_Flag_" + _flag;
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        public DataSet AuthenticateUser()
        {
            DataSet UserDetail = new DataSet();
            try
            {
                string dbpassword;
                string HashDBpassword;
                _flag = "Get_Saved_DBPwd";
                UserDetail = TryToLoginUser();
                if (Convert.ToString(UserDetail.Tables[0].Rows[0]["Result"]).Trim() == "0")       // Result 0 means UserID is correct
                {
                    dbpassword = Convert.ToString(UserDetail.Tables[0].Rows[0]["Password"]).Trim();
                    HashDBpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(_SeedNumber + dbpassword, "MD5");
                    if (_Password.Trim().ToLower() == HashDBpassword.Trim().ToLower())
                    {
                        _flag = "LoginSuccess";
                        TryToLoginUser();                          //Update Success login status to loginAttempts table
                        UserDetail = GetLoginUserDetail();
                    }
                    else
                    {
                        _flag = "LoginFailed";
                        TryToLoginUser();                         //Update Wrong Password status to loginAttempts table
                    }
                }
                else
                {
                    _flag = "LoginFailed";
                    TryToLoginUser();                         //Update Wrong UserID status to loginAttempts table
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "clsLoginUser";
                clsLogger.ExceptionMsg = "authenticateUser";
                clsLogger.SaveException();
            }
            return UserDetail;
        }

        public int AuthenticateUser_WithoutSeed()
        {
            DataSet UserDetail = new DataSet();
            try
            {
                UserDetail = TryToLoginUser();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "clsLoginUser";
                clsLogger.ExceptionMsg = "authenticateUser_withoutseed";
                clsLogger.SaveException();
            }
            return Convert.ToInt32(UserDetail.Tables[0].Rows[0]["Result"]);
        }
        public DataSet GetMenuItem()
        {
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetAllowedMenuItem", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserID", _UserID);
                vadap.SelectCommand.Parameters.AddWithValue("@p_AppCode", _ApplicationCode);

                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;

                clsLogger.ExceptionMsg = "GetMenuItem";
                clsLogger.ExceptionDetail = "UserID_" + _UserID + "_ApplicationCode_" + _ApplicationCode;
                clsLogger.ExceptionPage = "clsLoginUser/GetMenuItem";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        private static void AntiFixationInit()
        {
            int RandomValue;
            Random objRandom = new Random();
            RandomValue = objRandom.Next();
            HttpContext.Current.Request.Cookies.Clear();
            HttpContext.Current.Response.Cookies.Clear();
            HttpCookie MyCookie = new HttpCookie("APSF");
            MyCookie.HttpOnly = true;
         //   MyCookie.Secure = true;
            MyCookie.Value = RandomValue.ToString();
            HttpContext.Current.Response.Cookies.Add(MyCookie);
            HttpContext.Current.Session["APSF"] = RandomValue.ToString();
        }

        private static void AntiHijackInit()
        {
            HttpCookie NewCookie = new HttpCookie("Ck1");
            NewCookie.HttpOnly = true;
          //  NewCookie.Secure = true;
            
            // NewCookie.Value = Convert.ToString(SQLFunc.encryptVB(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]));
            HttpContext.Current.Response.Cookies.Add(NewCookie);
        }

        public static void SetCookie()
        {
            AntiFixationInit();
            AntiHijackInit();
        }

        public static bool CheckSession(string StruserType)
        {
            try
            {
                string UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
                string userid = Convert.ToString(HttpContext.Current.Session["UserId"]);
                string NewTab = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
                if (NewTab == "" | NewTab == null)
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/CustomErrPage.aspx", true);
                    return false;
                }
                else
                {
                    HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                    HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cache.SetNoServerCaching();
                    HttpContext.Current.Response.CacheControl = "no-cache";
                    HttpContext.Current.Response.Cache.SetNoStore();
                    HttpContext.Current.Response.Expires = -1500;
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.Expires = 0;
                    string browser = HttpContext.Current.Request.Browser.Browser;
                    if (browser == "IE")
                    {
                        HttpContext.Current.Response.CacheControl = "No-Cache";
                    }
                    if (string.IsNullOrEmpty(userid))
                    {
                        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", false);
                        return false;
                    }
                    if (StruserType != HttpContext.Current.Session["UserType"].ToString() && StruserType != "0")
                    {
                        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/DHE/CustomErrPage.aspx", false);
                        return false;
                    }
                    else if (Convert.ToString(HttpContext.Current.Session["ForcePwdChange"]) != "y")
                    {
                        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/changepwd.aspx", false);
                        return false;
                    }
                    else if (Convert.ToString(HttpContext.Current.Request.Cookies["APSF"].Value) != Convert.ToString(HttpContext.Current.Session["APSF"]))
                    {
                        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/CustomErrPage.aspx", false);
                        return false;
                    }
                    //else if ((Convert.ToString(HttpContext.Current.Request.Cookies["Ck1"].Value) != Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])))
                    //{
                    //    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", false);
                    //    return false;
                    //}
                    else
                    {
                        StateBag ViewState = new StateBag();
                        Random objRandom = new Random();
                        ViewState.Add(Convert.ToString(objRandom.Next(1111, 8888)), 0);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;

                clsLogger.ExceptionMsg = "checksession";
                clsLogger.ExceptionDetail = "checksesion";
                clsLogger.ExceptionPage = "clsLoginUser/checksession";
                clsLogger.SaveException();
                return false;
            }

        }

        public static bool CheckAdminSession()
        {
            string UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
            string userid = Convert.ToString(HttpContext.Current.Session["UserId"]);
            string NewTab = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            if (NewTab == "" | NewTab == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/CustomErrPage.aspx", true);
                return false;
            }
            else
            {
                HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cache.SetNoServerCaching();
                HttpContext.Current.Response.CacheControl = "no-cache";
                HttpContext.Current.Response.Cache.SetNoStore();
                HttpContext.Current.Response.Expires = -1500;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Expires = 0;
                string browser = HttpContext.Current.Request.Browser.Browser;
                if (browser == "IE")
                {
                    HttpContext.Current.Response.CacheControl = "No-Cache";
                }
                if (string.IsNullOrEmpty(userid))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", false);
                    return false;
                }
                else if (Convert.ToString(HttpContext.Current.Session["ForcePwdChange"]) != "y")
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/changepwd.aspx", false);
                    return false;
                }
                else if (Convert.ToString(HttpContext.Current.Request.Cookies["APSF"].Value) != Convert.ToString(HttpContext.Current.Session["APSF"]))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/CustomErrPage.aspx", false);
                    return false;
                }
                else if ((Convert.ToString(HttpContext.Current.Request.Cookies["Ck1"].Value) != Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", false);
                    return false;
                }
                else
                {
                    StateBag ViewState = new StateBag();
                    Random objRandom = new Random();
                    ViewState.Add(Convert.ToString(objRandom.Next(1111, 8888)), 0);
                    return true;
                }
            }
        }

        public static bool CheckChangePwdSession()
        {
            string UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
            string userid = Convert.ToString(HttpContext.Current.Session["UserId"]);
            string NewTab = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            if (NewTab == "" | NewTab == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/CustomErrPage.aspx", true);
                return false;
            }
            else
            {
                HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cache.SetNoServerCaching();
                HttpContext.Current.Response.CacheControl = "no-cache";
                HttpContext.Current.Response.Cache.SetNoStore();
                HttpContext.Current.Response.Expires = -1500;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Expires = 0;
                string browser = HttpContext.Current.Request.Browser.Browser;
                if (browser == "IE")
                {
                    HttpContext.Current.Response.CacheControl = "No-Cache";
                }
                if (string.IsNullOrEmpty(userid))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", false);
                    return false;
                }
                else if (Convert.ToString(HttpContext.Current.Request.Cookies["APSF"].Value) != Convert.ToString(HttpContext.Current.Session["APSF"]))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/CustomErrPage.aspx", false);
                    return false;
                }
                //shweta
                //else if ((Convert.ToString(HttpContext.Current.Request.Cookies["Ck1"].Value) != Convert.ToString(SQLFunc.encryptVB(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]))))
                //{
                //    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/login.aspx", false);
                //    return false;
                //}
                else
                {
                    StateBag ViewState = new StateBag();
                    Random objRandom = new Random();
                    ViewState.Add(Convert.ToString(objRandom.Next(1111, 8888)), 0);
                    return true;
                }
            }
        }

        public static void ClearPreviousSession()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session["UserId"] = "";
            HttpContext.Current.Session["UserType"] = "";
            if (HttpContext.Current.Request.Cookies["Id"] != null)
            {
                HttpContext.Current.Response.Cookies["Id"].Value = string.Empty;
                HttpContext.Current.Response.Cookies["Id"].Expires = DateTime.Now.AddMonths(-10);
            }
            if (HttpContext.Current.Request.Cookies["APSF"] != null)
            {
                HttpContext.Current.Response.Cookies["APSF"].Value = string.Empty;
                HttpContext.Current.Response.Cookies["APSF"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (HttpContext.Current.Request.Cookies["Ck1"] != null)
            {
                HttpContext.Current.Response.Cookies["Ck1"].Value = string.Empty;
                HttpContext.Current.Response.Cookies["Ck1"].Expires = DateTime.Now.AddMonths(-20);
            }
            SessionIDManager manager = new SessionIDManager();
            string NewSessionID = manager.CreateSessionID(HttpContext.Current);
            bool redirected = false;
            bool isAdded = false;
            manager.SaveSessionID(HttpContext.Current, NewSessionID, out redirected, out isAdded);
        }

        public void SaveUserLog()
        {
            DataSet dt = new DataSet();
            eDISHAutil util = new eDISHAutil();
            DataSet vds = new DataSet();
            MySqlConnection con = new MySqlConnection(ConStrHE);
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                _IPAddress = (string)util.encrypt(_IPAddress);
                _UserID = (string)util.encrypt(_UserID);
                _ApplicationCode = (string)util.encrypt(_ApplicationCode);
                using (MySqlCommand cmd = new MySqlCommand("SaveUserLog", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_IPAddress", _IPAddress);
                    cmd.Parameters.AddWithValue("@p_SessionId", _SessionId);
                    cmd.Parameters.AddWithValue("@p_userid", _UserID);
                    cmd.Parameters.AddWithValue("@p_Applicationcd", _ApplicationCode);
                    cmd.Parameters.AddWithValue("@p_Modulecd", _ModuleCode);
                    cmd.Parameters.AddWithValue("@p_TransactionType", _TransactionType);
                    cmd.Parameters.AddWithValue("@p_TransactionNo", _ReceiptNo);
                    cmd.Parameters.AddWithValue("@p_PortalCode", "HE");
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
            catch (Exception ex)
            {
            }
            if (con.State == ConnectionState.Open)
                con.Close();
        }

        public static void LogOutUser(string AppCode)
        {
            string Path = "";
            string UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
            string UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
            if (string.IsNullOrEmpty(UserID))
            {
                HttpContext.Current.Response.Redirect(Path, true);
            }
            else
            {
                if (UserType == "A")
                {
                    Path = "~/adminlogin.aspx";
                }
                else
                {
                    Path = "~/dhe/frmlogin.aspx";
                }
                SaveLogOutUserLog(AppCode);
                ClearPreviousSession();
                HttpContext.Current.Response.Redirect(Path, true);
            }
        }

        public static void SaveLogOutUserLog(string AppCode)
        {
            clsLoginUser objLoginUser = new clsLoginUser();
            if (HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] == HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])
            {
                objLoginUser.IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
            }
            else
            {
                objLoginUser.IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }
            objLoginUser.SessionId = (HttpContext.Current.Session.SessionID);
            objLoginUser.UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
            objLoginUser.ApplicationCode = AppCode;   //LO for Login section
            objLoginUser.TransactionType = "LO";
            objLoginUser.ModuleCode = "00"; //00 for Login
            objLoginUser.ReceiptNo = "0";
            objLoginUser.SaveUserLog();
        }

        public static void SaveLogInUserLog(string TransactionType)
        {
            clsLoginUser objLoginUser = new clsLoginUser();
            if (HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] == HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])
            {
                objLoginUser.IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
            }
            else
            {
                objLoginUser.IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }
            objLoginUser.SessionId = (HttpContext.Current.Session.SessionID);
            objLoginUser.UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
            objLoginUser.ApplicationCode = "LI";   //LI for Login section
            objLoginUser.TransactionType = TransactionType;
            objLoginUser.ModuleCode = "00"; //00 for Login
            objLoginUser.ReceiptNo = "0";
            objLoginUser.SaveUserLog();
        }

        public static void SavePwdChangeUserLog(string TransactionType)
        {
            clsLoginUser objLoginUser = new clsLoginUser();
            if (HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] == HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])
            {
                objLoginUser.IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
            }
            else
            {
                objLoginUser.IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }
            objLoginUser.SessionId = (HttpContext.Current.Session.SessionID);
            objLoginUser.UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
            objLoginUser.ApplicationCode = "PC";   //PC for Password Change
            objLoginUser.TransactionType = TransactionType;
            objLoginUser.ModuleCode = "00"; //00 for Login
            objLoginUser.ReceiptNo = "0";
            objLoginUser.SaveUserLog();
        }

        public static void ModifyAppUserLog(string AppCode, string TransType, string ModuleCode, string ReceiptNo)
        {
            clsLoginUser objLoginUser = new clsLoginUser();
            if (HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"] == HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])
            {
                objLoginUser.IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
            }
            else
            {
                objLoginUser.IPAddress = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }
            objLoginUser.SessionId = (HttpContext.Current.Session.SessionID);
            objLoginUser.UserID = Convert.ToString(HttpContext.Current.Session["UserID"]);
            objLoginUser.ApplicationCode = AppCode;
            objLoginUser.TransactionType = TransType;
            objLoginUser.ModuleCode = ModuleCode;
            objLoginUser.ReceiptNo = ReceiptNo;
            objLoginUser.SaveUserLog();
        }




    }

}