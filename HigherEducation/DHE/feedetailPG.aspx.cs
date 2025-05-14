using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.DHE
{
    public partial class feedetailPG : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ///eDISHAutil eSessionMgmt = new eDISHAutil();
            //Security Check
            // eSessionMgmt.AntiFixationInit();
            // eSessionMgmt.AntiHijackInit();
            //Security Check
            string UserType = "2";
            //eDISHAutil eSessionMgmt = new eDISHAutil();
            //eSessionMgmt.CheckSession(UserType);
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
            {
                Response.Redirect("~/DHE/frmlogin.aspx", true);
            }
            //eSessionMgmt.SetCookie();
        }
    }
}