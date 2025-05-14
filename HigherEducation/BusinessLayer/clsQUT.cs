using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonFunctions;

/// <summary>
/// Summary description for clsQUT
/// </summary>
/// 
namespace HigherEducation.BusinessLayer
{

    public static class clsQUT
    {
        public static void qutLogout()
        {
            clsLoginUser.ClearPreviousSession();
            eDISHAutil util = new eDISHAutil();
            string IPAddress1, SessionId1, userid1, Applicationcd1, Modulecd1, TransactionType1, TransactionNo1;
            IPAddress1 = Convert.ToString(util.encrypt(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]));
            SessionId1 = HttpContext.Current.Session.SessionID;
            userid1 = Convert.ToString(util.encrypt(HttpContext.Current.Session["UserId"].ToString()));
            Applicationcd1 = "LO";
            Applicationcd1 = (util.encrypt(Applicationcd1)).ToString();
            Modulecd1 = "00";
            TransactionNo1 = "0";
            TransactionType1 = "LO";

          
            //objHlp.trackuser(IPAddress1, SessionId1, userid1, Applicationcd1, Modulecd1, TransactionType1, TransactionNo1);


            HttpContext.Current.Session["UserName"] = null;
            HttpContext.Current.Session["UserType"] = null;
            HttpContext.Current.Session["CollegeId"] = null;
            HttpContext.Current.Session["CollegeName"] = null;
            HttpContext.Current.Session["UserId"] = null;

            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("DHE", ""));

            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
        }
    }
}