using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;

namespace HigherEducation.HigherEducations
{
    public partial class feedetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ///eDISHAutil eSessionMgmt = new eDISHAutil();
            //Security Check
            // eSessionMgmt.AntiFixationInit();
            // eSessionMgmt.AntiHijackInit();
            //Security Check
            //string UserType = "2";
            string UserType = "0";
            if (Convert.ToString(Session["UserType"])=="1" || Convert.ToString(Session["UserType"])=="2")
            {
                UserType = Convert.ToString(Session["UserType"]);
            }
           

            
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
            {
                Response.Redirect("~/DHE/frmlogin.aspx", true);
            }
            eSessionMgmt.SetCookie();

        }
    }
}