using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace HigherEducation.BusinessLayer
{
    public class clsSecurity
    {
        private static void AntiFixationInit()
        {
            int RandomValue;
            Random objRandom = new Random();
            RandomValue = objRandom.Next();
            HttpContext.Current.Request.Cookies.Clear();
            HttpContext.Current.Response.Cookies.Clear();
            HttpCookie MyCookie = new HttpCookie("APSF");
            MyCookie.HttpOnly = true;
          //  MyCookie.Secure = true;
            MyCookie.Value = RandomValue.ToString();
            HttpContext.Current.Response.Cookies.Add(MyCookie);
            HttpContext.Current.Session["APSF"] = RandomValue.ToString();
        }

        private static void AntiHijackInit()
        {
            HttpCookie NewCookie = new HttpCookie("Ck1");
            NewCookie.HttpOnly = true;
           // NewCookie.Secure = true;
            // NewCookie.Value = Convert.ToString(SQLFunc.encryptVB(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]));
            HttpContext.Current.Response.Cookies.Add(NewCookie);
        }
        public static void SetCookie()
        {
            AntiFixationInit();
            AntiHijackInit();
        }

        public static bool CheckSession()
        {
            string userid = Convert.ToString(HttpContext.Current.Session["RegId"]);
            string NewTab = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            if (NewTab == "" | NewTab == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/Account/LogOut", true);
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
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/Account/LogOut", false);
                    return false;
                }
                else if (Convert.ToString(HttpContext.Current.Request.Cookies["APSF"].Value) != Convert.ToString(HttpContext.Current.Session["APSF"]))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/Account/LogOut", false);
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


        public static bool CheckSessionRegistration()
        {
           // string userid = Convert.ToString(HttpContext.Current.Session["RegId"]);
            string NewTab = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            if (NewTab == "" | NewTab == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/Account/LogOut", true);
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
  
                if (Convert.ToString(HttpContext.Current.Request.Cookies["APSF"].Value) != Convert.ToString(HttpContext.Current.Session["APSF"]))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/Account/LogOut", false);
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

    }
}