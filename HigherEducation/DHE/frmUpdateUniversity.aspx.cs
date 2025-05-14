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
    public partial class frmUpdateUniversity : System.Web.UI.Page
    {
        public void GetParticularCollegeUnivData()
        {
            clsCollege cg = new clsCollege();
            DataTable dt = new DataTable();
            dt = cg.GetParticularCollegeUnivData(Convert.ToInt32(Session["CollegeId"]));
            if (dt.Rows.Count > 0)
            {
                lblUniv.Text = dt.Rows[0]["univeristyname"].ToString();
                if (dt.Rows[0]["associatedUniversity"].ToString() != "")
                {
                    ddlAssociatedUniv.SelectedValue = dt.Rows[0]["associatedUniversity"].ToString();
                }
            }
        }
        public void BindAssociatedUniversity(DropDownList ddl)
        {
            try
            {
                clsCollege cg = new clsCollege();
                DataTable dt = new DataTable();
                dt = cg.BindAssociatedUniversity();
                if (dt.Rows.Count > 0)
                {
                    ddl.DataSource = dt;
                    ddl.DataTextField = "Text";
                    ddl.DataValueField = "Value";
                    ddl.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void GetCollegeUnivData()
        {
            clsCollege cg = new clsCollege();
            DataTable dt = new DataTable();
            dt = cg.GetCollegeUnivData();
            if (dt.Rows.Count > 0)
            {
                grdCollege.DataSource = dt;
                grdCollege.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = Convert.ToString(Session["UserType"]);
            if (UserType == "1" || UserType == "2")
            {
                eDISHAutil eSessionMgmt = new eDISHAutil();
                eSessionMgmt.CheckSession(UserType);
                if (!IsPostBack)
                {
                    if (UserType == "2")
                    {
                        dvCollege.Style.Add("display", "block");
                        dvState.Style.Add("display", "none");
                        BindAssociatedUniversity(ddlAssociatedUniv);
                        GetParticularCollegeUnivData();
                    }
                    else if (UserType == "1")
                    {
                        dvCollege.Style.Add("display", "none");
                        dvState.Style.Add("display", "block");
                        GetCollegeUnivData();
                    }
                }
                eSessionMgmt.SetCookie();
            }
            else
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    lblMsgCollege.Text = "";
                    clsCollege cg = new clsCollege();
                    if (ddlAssociatedUniv.SelectedValue == "0")
                    {
                        clsAlert.AlertMsg(this, "Please Select Associated University");
                        return;
                    }
                    string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    string result=cg.UpdateAssociatedUniversity(Convert.ToInt32(Session["CollegeId"]),Convert.ToInt32(ddlAssociatedUniv.SelectedValue), Convert.ToString(Session["UserId"]), ipAddress);
                    lblMsgCollege.Text = "Associated University Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmUpdateUniversity";
                clsLogger.ExceptionMsg = "btnUpdate_Click";
                clsLogger.SaveException();
            }          
        }
        protected void grdCollege_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList univ = e.Row.FindControl("ddlAssUniv") as DropDownList;
                BindAssociatedUniversity(univ);
                if (DataBinder.Eval(e.Row.DataItem, "associatedUniversity").ToString().Trim() != "")
                {
                    univ.SelectedValue = DataBinder.Eval(e.Row.DataItem, "associatedUniversity").ToString().Trim();
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                lblMsgState.Text = "";
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                string result = "";
                clsCollege cg = new clsCollege();
                foreach (GridViewRow row in grdCollege.Rows)
                {
                    Label collegeid = (Label)row.FindControl("lblCollegeId");
                    DropDownList ddl = (DropDownList)row.FindControl("ddlAssUniv");
                    result = cg.UpdateAssociatedUniversity(Convert.ToInt32(collegeid.Text), Convert.ToInt32(ddl.SelectedValue), Convert.ToString(Session["UserId"]), ipAddress);
                }
                lblMsgState.Text = "Associated University Saved Successfully";
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frmUpdateUniversity";
                clsLogger.ExceptionMsg = "btnSave_Click";
                clsLogger.SaveException();
            }
        }
    }
}