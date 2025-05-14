using HigherEducation.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.HigherEducations
{
    public partial class frmAddFeeSubHead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = "1";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            // eSessionMgmt.checkreferer();
            eSessionMgmt.CheckSession(UserType);

            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }
    }
}