using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.HigherEducations
{
    public partial class Errorpage_AccountFreeze : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
            Session["UserId"] = "";
            Session["UserName"] = "";
            Session["UserType"] = "";
            Session["CollegeId"] = "";
            Session["CollegeName"] = "";
            Session["CollegeType"] = "";
            Session.Clear();
            Session.Abandon();

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("DHE", ""));

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/DHE/frmlogin.aspx", true);
        }
    }
}