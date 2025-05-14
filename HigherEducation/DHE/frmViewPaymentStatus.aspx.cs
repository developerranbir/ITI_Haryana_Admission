using HigherEducation.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.DHE
{
    public partial class frmViewPaymentStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
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
            GetPaymentStatus();
        }



        public void GetPaymentStatus()
        {
            clsResultUGAdm RU = new clsResultUGAdm();
            DataTable dt = new DataTable();
            DataTable dtFilter = new DataTable();
            RU.RegId = txtRegId.Text.Trim();
            if (ddlUGPG.SelectedValue=="PG")
            {
                dt = RU.GetPaymentStatus_PG();
            }
            else
            {
                dt = RU.GetPaymentStatus();
            }
           


            if (dt.Rows.Count > 0)
            {
                lblStudentName.Text= dt.Rows[0]["StudentName"].ToString();
                lblCollegeName.Text = dt.Rows[0]["CollegeName"].ToString();
                lblCourseName.Text = dt.Rows[0]["CourseName"].ToString();
                lblSectionName.Text = dt.Rows[0]["SectionName"].ToString();
                lblSubComb.Text = dt.Rows[0]["CombinationName"].ToString();
                dvSection.Style.Add("display", "inline-block");
                GrdPaymentStatus.DataSource = dt;
                GrdPaymentStatus.DataBind();
                dvPaymentStatus.Style.Add("display", "inline-block");

            }

            else
            {

                GrdPaymentStatus.DataSource = null;
                GrdPaymentStatus.DataBind();
                clsAlert.AlertMsg(this, "Record not found");
                dvSection.Style.Add("display", "none");
                dvPaymentStatus.Style.Add("display", "none");
                return;
            }

        }
    }

}