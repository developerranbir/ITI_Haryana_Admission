using HigherEducation.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.HigherEducation
{
    public partial class collegecoursePG : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = "2";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);
            if (!IsPostBack)
            {
                //btnAddcourse.Visible = false;
            }
            eSessionMgmt.SetCookie();
        }
    }
}