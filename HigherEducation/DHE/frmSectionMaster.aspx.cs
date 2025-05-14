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
    public partial class frmSectionMaster : System.Web.UI.Page
    {
        public void BindCollege()
        {
            try
            {
                clsSection CS = new clsSection();
                DataTable dt = new DataTable();
                dt = CS.BindCollege();
                if (dt.Rows.Count > 0)
                {
                    ddlCollege.DataSource = dt;
                    ddlCollege.DataTextField = "Text";
                    ddlCollege.DataValueField = "Value";
                    ddlCollege.DataBind();
                    ddlCollege.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                    ddlCollege.Focus();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmSectionMaster";
                clsLogger.ExceptionMsg = "BindCollege";
                clsLogger.SaveException();
            }
        }
        public void BindCourse()
        {
            try
            {
                clsSection CS = new clsSection();
                DataTable dt = new DataTable();
                dt = CS.BindCourse();
                if (dt.Rows.Count > 0)
                {
                    ddlCourse.DataSource = dt;
                    ddlCourse.DataTextField = "Text";
                    ddlCourse.DataValueField = "Value";
                    ddlCourse.DataBind();
                    ddlCourse.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
                    ddlCourse.Focus();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmSectionMaster";
                clsLogger.ExceptionMsg = "BindCourse";
                clsLogger.SaveException();
            }
        }
        public void BindSection()
        {
            try
            {
                clsSection CS = new clsSection();
                DataTable dt = new DataTable();
                dt = CS.BindSection();
                if (dt.Rows.Count > 0)
                {
                    grdSection.DataSource = dt;
                    grdSection.DataBind();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmSectionMaster";
                clsLogger.ExceptionMsg = "BindSection";
                clsLogger.SaveException();
            }
        }
        public void Clear()
        {
            txtSectionName.Text = "";
            ddlCollege.SelectedValue = "0";
            ddlCourse.SelectedValue = "0";
            rdbIsActive.SelectedValue = "ACTIVE";
            btnSubmit.Text = "Save";
            ViewState["SectionID"] = null;
        }
        public string CheckDuplicateSection(string SectionName)
        {
            string val = "";
            try
            {
                clsSection CS = new clsSection();
                DataTable dt = new DataTable();
                dt = CS.CheckDuplicateSection(SectionName);
                if (dt.Rows.Count > 0)
                {
                    val = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmSectionMaster";
                clsLogger.ExceptionMsg = "CheckDuplicateSection";
                clsLogger.SaveException();
            }
            return val;
        }
        public string CheckDuplicateSectionforUpdate(string SectionName, int SectionId)
        {
            string val = "";
            try
            {
                clsSection CS = new clsSection();
                DataTable dt = new DataTable();
                dt = CS.CheckDuplicateSectionforUpdate(SectionName, SectionId);
                if (dt.Rows.Count > 0)
                {
                    val = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmSectionMaster";
                clsLogger.ExceptionMsg = "CheckDuplicateSectionforUpdate";
                clsLogger.SaveException();
            }
            return val;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCollege();
                BindCourse();
                BindSection();
            }
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (ddlCollege.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select College");
                        return;
                    }
                    if (ddlCourse.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select Course");
                        return;
                    }
                    if (txtSectionName.Text == "")
                    {
                        clsAlert.AlertMsg(this, "Please Enter Section Name");
                        return;
                    }
                    clsSection cg = new clsSection();
                    cg.sectionname = txtSectionName.Text.Trim();
                    cg.collegeid = ddlCollege.SelectedValue;
                    cg.courseid = ddlCourse.SelectedValue;
                    cg.UserId = Convert.ToString(Session["UserId"]);
                    cg.isactive = rdbIsActive.SelectedValue;
                    cg.isactivePG = rdbIsActivePG.SelectedValue;
                    string IPAddress = GetIPAddress();
                    cg.IPAddress = IPAddress;
                    if (btnSubmit.Text == "Save")
                    {
                        string a = "";
                        a = CheckDuplicateSection(txtSectionName.Text.Trim());
                        if (a != "")
                        {
                            clsAlert.AlertMsg(this, "Section Name already Exists.");
                            return;
                        }
                        else if (a == "")
                        {
                            string s = cg.AddSection();
                            if (s == "1")
                            {
                                Clear();
                                BindSection();
                                clsAlert.AlertMsg(this, "Section Added Successfully.");
                                return;
                            }
                            else
                            {
                                clsAlert.AlertMsg(this, "Section not Added.");
                                return;
                            }
                        }
                    }
                    else if (btnSubmit.Text == "Update")
                    {
                        if (Convert.ToString(ViewState["SectionID"]) != "" && Convert.ToString(ViewState["SectionID"]) != null)
                        {
                            string b = "";
                            b = CheckDuplicateSectionforUpdate(txtSectionName.Text.Trim(), Convert.ToInt32(ViewState["SectionID"]));
                            if (b != "")
                            {
                                clsAlert.AlertMsg(this, "Section Name already Exists.");
                                return;
                            }
                            else if (b == "")
                            {
                                cg.sectionid = Convert.ToInt32(ViewState["SectionID"]);
                                string s = cg.UpdateSection();
                                if (s == "1")
                                {
                                    Clear();
                                    BindSection();
                                    clsAlert.AlertMsg(this, "Section Updated Successfully.");
                                    return;
                                }
                                else
                                {
                                    clsAlert.AlertMsg(this, "Section not Updated.");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            clsAlert.AlertMsg(this, "Section not Updated !!!");
                            return;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmSectionMaster";
                clsLogger.ExceptionMsg = "btnSubmit_Click";
                clsLogger.SaveException();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void grdSection_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row;
                row = (GridViewRow)((Button)e.CommandSource).Parent.Parent;
                btnSubmit.Text = "Update";
                string sectionid = grdSection.DataKeys[row.RowIndex].Values[0].ToString();
                ViewState["SectionID"] = sectionid;
                Label lbsection = (Label)row.FindControl("lblSectionName");
                txtSectionName.Text = lbsection.Text.Trim();
                Label lbcollege = (Label)row.FindControl("lblCollegeId");
                ddlCollege.SelectedValue = lbcollege.Text.Trim();
                Label lbcourse = (Label)row.FindControl("lblCourseId");
                ddlCourse.SelectedValue = lbcourse.Text.Trim();
                Label lbactive = (Label)row.FindControl("lblIsActive");
                rdbIsActive.SelectedValue = lbactive.Text.Trim();
                Label lbactivepg = (Label)row.FindControl("lblIsActivePG");
                rdbIsActivePG.SelectedValue = lbactivepg.Text.Trim();
            }
            catch(Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmSectionMaster";
                clsLogger.ExceptionMsg = "grdSection_RowCommand";
                clsLogger.SaveException();
            }
        }
        protected void grdSection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSection.PageIndex = e.NewPageIndex;
            BindSection();
        }
    }
}