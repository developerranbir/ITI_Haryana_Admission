using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmSeatMatrix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            string UserType = Convert.ToString(Session["UserType"]);

            eSessionMgmt.CheckSession(UserType);

            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);

            }
            else
            {
                if (!Page.IsPostBack)
                {
                    if (UserType == "1")// State Level User Only
                    {
                        BindScheme();
                        ddlScheme.Enabled = true;
                    }
                    /*
                    else // Rest Users
                    {
                        BindCollege();
                        ddlScheme.SelectedValue = Convert.ToString(Session["CollegeId"]);
                        ddlScheme.Enabled = false;
                    }
                    */
                }
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }
        public void BindScheme()
        {
            try
            {
                clsCollegeSeatMatrix CSM = new clsCollegeSeatMatrix();

                DataTable dt = new DataTable();
                dt = CSM.BindScheme();
                if (dt.Rows.Count > 0)
                {
                    ddlScheme.DataSource = dt;
                    ddlScheme.DataTextField = "matrixforScheme";
                    ddlScheme.DataValueField = "matrixforSchemeID";
                    ddlScheme.DataBind();
                    ddlScheme.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select Scheme--", "0"));
                    ddlScheme.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }
      
        protected void btSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
          
            if (ddlScheme.SelectedValue != "0")
            {
                dvFullForm.Style.Add("display", "Inline-block");
                clsCollegeSeatMatrix CSM = new clsCollegeSeatMatrix();
                CSM.SchemeId = ddlScheme.SelectedValue;
                dt = CSM.GetCollegeSeatMatrixView();
                if (dt.Rows.Count > 0)
                {
                    grdViewUpdate.DataSource = dt;
                    grdViewUpdate.DataBind();
                    //Calculate Sum and display in Footer Row

                }
                else
                {
                    clsAlert.AlertMsg(this, "Seats Matrix Not Found.");
                    grdViewUpdate.DataSource = null;
                    grdViewUpdate.DataBind();
                }
            }
            else
            {
                clsAlert.AlertMsg(this, "Please Select Scheme");
                grdViewUpdate.DataSource = null;
                grdViewUpdate.DataBind();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            String Result = "";
            int i = 0;
            clsCollegeSeatMatrix CSM = new clsCollegeSeatMatrix();
            foreach (GridViewRow row in grdViewUpdate.Rows)
            {
                int Sum = 0;
                i++;
                Label lblid = (Label)row.FindControl("lblid");


                TextBox txtSeatSize = (TextBox)row.FindControl("txtSeatSize");
                Result += checkData(txtSeatSize.Text, i, "Seat Size");
                
                TextBox txtOpenM = (TextBox)row.FindControl("txtOpenM");
                Result += checkData(txtOpenM.Text, i, "Open-M");
                Sum += Convert.ToInt32(txtOpenM.Text.ToString());

                TextBox txtOpenF = (TextBox)row.FindControl("txtOpenF");
                Result += checkData(txtOpenF.Text, i, "Open-F");
                Sum += Convert.ToInt32(txtOpenF.Text.ToString());

                TextBox txtESMGen = (TextBox)row.FindControl("txtESMGen");
                Result += checkData(txtESMGen.Text, i, "ESM Gen");
                Sum += Convert.ToInt32(txtESMGen.Text.ToString());

                TextBox txtBCAM = (TextBox)row.FindControl("txtBCAM");
                Result += checkData(txtBCAM.Text, i, "BCA-M");
                Sum += Convert.ToInt32(txtBCAM.Text.ToString());

                TextBox txtBCAF = (TextBox)row.FindControl("txtBCAF");
                Result += checkData(txtBCAF.Text, i, "BCA-F");
                Sum += Convert.ToInt32(txtBCAF.Text.ToString());

                TextBox txtBCBM = (TextBox)row.FindControl("txtBCBM");
                Result += checkData(txtBCBM.Text, i, "BCB-M");
                Sum += Convert.ToInt32(txtBCBM.Text.ToString());

                TextBox txtBCBF = (TextBox)row.FindControl("txtBCBF");
                Result += checkData(txtBCBF.Text, i, "BCB-F");
                Sum += Convert.ToInt32(txtBCBF.Text.ToString()); 
                
                TextBox txtESMBCA = (TextBox)row.FindControl("txtESMBCA");
                Result += checkData(txtESMBCA.Text, i, "ESM-BCA");
                Sum += Convert.ToInt32(txtESMBCA.Text.ToString()); 
                
                TextBox txtSCM = (TextBox)row.FindControl("txtSCM");
                Result += checkData(txtSCM.Text, i, "SC-M");
                Sum += Convert.ToInt32(txtSCM.Text.ToString()); 
                
                TextBox txtDepSCM = (TextBox)row.FindControl("txtDepSCM");
                Result += checkData(txtDepSCM.Text, i, "Dept. SC-M");
                Sum += Convert.ToInt32(txtDepSCM.Text.ToString()); 
                
                TextBox txtSCF = (TextBox)row.FindControl("txtSCF");
                Result += checkData(txtSCF.Text, i, "SC-F");
                Sum += Convert.ToInt32(txtSCF.Text.ToString()); 
                
                TextBox txtDepSCF = (TextBox)row.FindControl("txtDepSCF");
                Result += checkData(txtDepSCF.Text, i, "Dept. SC-F");
                Sum += Convert.ToInt32(txtDepSCF.Text.ToString()); 
                
                TextBox txtSCESM = (TextBox)row.FindControl("txtSCESM");
                Result += checkData(txtSCESM.Text, i, "SC-ESM");
                Sum += Convert.ToInt32(txtSCESM.Text.ToString()); 
                
                TextBox txtEWSM = (TextBox)row.FindControl("txtEWSM");
                Result += checkData(txtEWSM.Text, i, "EWS-M");
                Sum += Convert.ToInt32(txtEWSM.Text.ToString()); 
                
                TextBox txtEWSF = (TextBox)row.FindControl("txtEWSF");
                Result += checkData(txtEWSF.Text, i, "EWS-F");
                Sum += Convert.ToInt32(txtEWSF.Text.ToString()); 
                
                TextBox txtPH = (TextBox)row.FindControl("txtPH");
                Result += checkData(txtPH.Text, i, "PH");
                Sum += Convert.ToInt32(txtPH.Text.ToString()); 
                
                 
                if (Sum != Convert.ToInt32(txtSeatSize.Text.ToString()))
                {
                    Result += "Error in : Row Number " + i + " Seat Size not equal to Sum of All Columns, ";
                }
            }
            if (Result == "")
            {
                foreach (GridViewRow row in grdViewUpdate.Rows)
                {
                    Label lblid = (Label)row.FindControl("lblid");
                    TextBox txtSeatSize = (TextBox)row.FindControl("txtSeatSize");
                    TextBox txtOpenM = (TextBox)row.FindControl("txtOpenM");
                    TextBox txtOpenF = (TextBox)row.FindControl("txtOpenF");
                    TextBox txtESMGen = (TextBox)row.FindControl("txtESMGen");
                    TextBox txtBCAM = (TextBox)row.FindControl("txtBCAM");
                    TextBox txtBCAF = (TextBox)row.FindControl("txtBCAF");
                    TextBox txtBCBM = (TextBox)row.FindControl("txtBCBM");
                    TextBox txtBCBF = (TextBox)row.FindControl("txtBCBF");
                    TextBox txtESMBCA = (TextBox)row.FindControl("txtESMBCA");
                    TextBox txtSCM = (TextBox)row.FindControl("txtSCM");
                    TextBox txtDepSCM = (TextBox)row.FindControl("txtDepSCM");
                    TextBox txtSCF = (TextBox)row.FindControl("txtSCF");
                    TextBox txtDepSCF = (TextBox)row.FindControl("txtDepSCF");
                    TextBox txtSCESM = (TextBox)row.FindControl("txtSCESM");
                    TextBox txtEWSM = (TextBox)row.FindControl("txtEWSM");
                    TextBox txtEWSF = (TextBox)row.FindControl("txtEWSF");
                    TextBox txtPH = (TextBox)row.FindControl("txtPH");

                    CSM.UpdateSeatMatrix(lblid.Text, txtSeatSize.Text, txtOpenM.Text, txtOpenF.Text, txtESMGen.Text, txtBCAM.Text, txtBCAF.Text, txtBCBM.Text, txtBCBF.Text, txtESMBCA.Text, txtSCM.Text, txtDepSCM.Text, txtSCF.Text, txtDepSCF.Text, txtSCESM.Text, txtEWSM.Text, txtEWSF.Text, txtPH.Text, txtSeatSize.Text);

                }
                clsAlert.AlertMsg(this, "Updated Successfully!!!");
            }
            else
            {
                clsAlert.AlertMsg(this, Result);

            }
        }

        private String checkData(String d,int rowNum, String fName)
        {
            String r = "";
            if (d == null || d == "")
            {
                r = "Error in : Row Number " + rowNum+ " Not Empty in Column " + fName +" , ";
            }
            else if(Convert.ToInt32(d)<0)
            {
                r = "Error in : Row Number " + rowNum + " value not less then 0 in Column " + fName + " , ";
            }

            return r;
        }
    }
}
    