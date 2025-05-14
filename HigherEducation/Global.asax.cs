using HigherEducation.App_Start;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace HigherEducation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        protected void Application_Error()
        {
            //Server.ClearError();
            //Response.Redirect("/UG/Account/Error");
        }

        //protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.Headers.Remove("X-Powered-By");
        //    HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
        //    HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
        //    HttpContext.Current.Response.Headers.Remove("Server");
        //    HttpContext.Current.Response.Headers.Remove("ETag");
        //    HttpContext.Current.Response.AddHeader("X-Frame-Options", "DENY");
        //}
        //public void Application_PostRequestHandlerExecute(Object sender, EventArgs e)
        //{
        //    Response.Headers.Remove("Server");
        //    Response.Headers.Remove("X-Powered-By");
        //    Response.Headers.Remove("X-AspNet-Version");
        //    Response.Headers.Remove("ETag");
        //    Response.Headers.Remove("X-AspNetMvc-Version");
        //    //if (!(Request.Url.Host == "localhost" || Request.Url.Host == "aasstg.saralharyana.nic.in"))
        //    //{
        //    //    // TODO : log error (I suggest to also log HttpContext.Current.Request.RawUrl) and throw new exception
        //    //    //throw new HttpException(405, "GET not allowed for this.");
        //    //    // Response.Redirect("/Home/Error");
        //    //}
        //    if (HttpContext.Current.Response.Cookies.AllKeys.Contains("ASP.NET_SessionId"))
        //    {
        //        HttpContext.Current.Response.Cookies.Remove("ASP.NET_SessionId");
        //    }
        //}
        //public void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.Response.Cookies.AllKeys.Contains("ASP.NET_SessionId"))
        //    {
        //        HttpContext.Current.Response.Cookies.Remove("ASP.NET_SessionId");
        //    }
        //}
        //public void Application_PostRequestHandlerExecute(Object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.Request.ServerVariables["HTTP_HOST"] != "10.88.250.71")
        //    {
        //        Response.Redirect("https://10.88.250.71/higher/Account/Error");
        //    }
        //}
    }
}
