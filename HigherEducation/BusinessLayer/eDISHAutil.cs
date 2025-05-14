using System;
using System.Web;
using System.Web.UI;
//using EgBL;
using System.Text;
//using EgBL;
//using CrystalReportsDataDefModelLib;
/// <summary>
/// Summary description for eDISHAutil
/// </summary>
namespace HigherEducation.BusinessLayer
{
    public class eDISHAutil
    {

        public eDISHAutil()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void checkreferer()
        {
            string NewTab = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

            if (((NewTab == "")
                        || (NewTab == null)))
            {
                HttpContext.Current.Response.Redirect(("~" + "/DHE/CustomErrPage.aspx"), true);
            }
        }

        public void AntiFixationInit()
        {
            Int32 value;
            Random randomclass = new Random();
            value = Convert.ToInt32(randomclass.Next().ToString());
            HttpContext.Current.Response.Cookies.Clear();
            HttpCookie MyCookie = new HttpCookie("APSF");
            MyCookie.HttpOnly = true;
          //  MyCookie.Secure = true;
            MyCookie.Value = value.ToString();
          

            HttpContext.Current.Response.Cookies.Add(MyCookie);
            HttpContext.Current.Session["APSF"] = value.ToString();
        }

        public void AntiHijackInit()
        {
            // eDISHAutil e2 = new eDISHAutil();
            // Dim e2 As New eDISHAutil()
            HttpCookie Ck1 = new HttpCookie("Ck1");
            Ck1.HttpOnly = true;
           /// Ck1.Secure = true;
            Ck1.Value = Convert.ToString(encrypt(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]));
            HttpContext.Current.Response.Cookies.Add(Ck1);
        }

        public void SetCookie()
        {
            AntiFixationInit();
            AntiHijackInit();
        }

        public bool CheckSession(string UserType)
        {

            string userid = Convert.ToString(HttpContext.Current.Session["UserID"]);
            string NewTab = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            if (NewTab == "" | NewTab == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/DHE/CustomErrPage.aspx", true);
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
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/DHE/CustomErrPage.aspx", false);
                    return false;
                }

                if (UserType != HttpContext.Current.Session["UserType"].ToString() && UserType != "0")
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/DHE/CustomErrPage.aspx", false);
                    return false;
                }

                else if (Convert.ToString(HttpContext.Current.Request.Cookies["APSF"].Value) != Convert.ToString(HttpContext.Current.Session["APSF"]))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/DHE/CustomErrPage.aspx", false);
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


        public object encrypt(string str)
        {

            dynamic ch = null;
            dynamic newstr = null;
            dynamic newasc = null;
            dynamic newch = null;
            dynamic i = null;
            newstr = "";
            for (i = 1; i <= str.Length; i++)
            {
                ch = str.Substring(i - 1, 1);
                //Strings.Mid(str(), i, 1);
                //String.Mid(str, i, 1);
                newasc = (Asc(ch) * 2) + 3;
                newch = Chr(newasc);
                newstr = newstr + newch;
            }
            return newstr;
        }

        //  THIS FUNCTION IS USED TO DECRYPT THE ENCRYPTED STRING AS PARAMETER
        public string Decrypt(string str)
        {

            dynamic ch = null;
            dynamic newstr = null;
            dynamic newasc = null;
            dynamic newch = null;
            dynamic i = null;
            newstr = "";
            for (i = 1; i <= str.Length; i++)
            {
                ch = str.Substring(i, 1);
                //String.Mid(str, i);
                newasc = (Asc(ch) - 3) / 2;
                newch = Chr(newasc);
                newstr = newstr + newch;
            }
            return newstr;
        }
        static short Asc(string String)
        {
            return Encoding.Default.GetBytes(String)[0];
        }
        static string Chr(int CharCode)
        {
            if (CharCode > 255)
                throw new ArgumentOutOfRangeException("CharCode", CharCode, "CharCode must be between 0 and 255.");
            return Encoding.Default.GetString(new[] { (byte)CharCode });
        }



    }

}