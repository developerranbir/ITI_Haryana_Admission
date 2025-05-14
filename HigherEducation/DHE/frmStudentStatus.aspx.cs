using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmStudentStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
           //  eSessionMgmt.checkreferer();
          //  clsCancelStudentRegId.CheckSession();
            if (!Page.IsPostBack)
            {

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }


       

      
        public void disable()
        {
            dvSection.Style.Add("display", "none");
        
          
        }

        public void enable()
        {
            dvSection.Style.Add("display", "inline-block");
         
            
        }
        public string GetIPAddress()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
              
                clsCancelStudentRegId CSR = new clsCancelStudentRegId();
                DataTable dt = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                if (ddlUGPG.SelectedValue == "PG")
                {
                   // dt = CSR.GetStudentStatus_PG();
                }
                else
                {
                    dt = CSR.GetStudentStatus();
                }
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section col-lg-12");
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblStudentStatus.Text = dt.Rows[0]["StudentStatus"].ToString();
                    hdMaxPage.Value= dt.Rows[0]["max_page"].ToString();

                    enable();
                    
                }
                else
                {
                    dvSection.Attributes.Remove("class");
                    clear();
                    clsAlert.AlertMsg(this, "Student registration not found.");
                    return;
                }
                
                
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmStudentStatus";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }

         public void clear()
        {
            disable();
            lblStudentName.Text = "";
            lblRegId.Text = "";
            lblStudentStatus.Text = "";
            hdMaxPage.Value = "";
        }
        
     
    }
   
}
    