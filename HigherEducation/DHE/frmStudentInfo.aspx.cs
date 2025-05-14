using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmStudentInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = Convert.ToString(Session["UserType"]);
            eDISHAutil eSessionMgmt = new eDISHAutil();
            clsLoginUser.CheckSession(UserType);
            if (UserType != "1")// State Level User Only
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
            }

            if (!Page.IsPostBack)
            {

            }

            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }




        protected void btnGo_Click(object sender, EventArgs e)
        {

            if (txtRegId.Text != "")
            {
                hdRefId.Value = txtRegId.Text.Trim();
                if (hdRefId.Value != "")
                {
                    EncryptionDecryption enc = new EncryptionDecryption();

                    hdRefId.Value = HttpUtility.UrlEncode(enc.EncryptKey(txtRegId.Text, "dheticketabhi@mohi#2020"));
                    string RegId = hdRefId.Value;
                   hdRefId.Value = RegId.Replace(" ", "+");
                    string url = "https://admissions.itiharyana.gov.in/UG/Account/candidet?Regid=" + Uri.EscapeDataString(hdRefId.Value); //Prod
                    //string url = "https://admissions.itiharyana.gov.in/UG/Account/candidet?Regid=" + hdRefId.Value; //Prod
                    //Response.Redirect(url);
                    string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=1000,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
                    

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('Invalid Registration_Id...!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('Kindly enter Registration_Id...!');", true);
            }
        }
    }

}
