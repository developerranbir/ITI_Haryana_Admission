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
    public partial class InactiveTradesStudents : System.Web.UI.Page
    {
        clsInactiveTradesStudents clsData = new clsInactiveTradesStudents();


        protected void Page_Load(object sender, EventArgs e)
        {

            string UserType = Convert.ToString(Session["UserType"]);//"2";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            clsLoginUser.CheckSession(UserType);
            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
                return;
            }
            if (!Page.IsPostBack)
            {

                BindInactiveCollege();
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
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

        public void BindInactiveCollege()
        {
            DataTable dt = new DataTable();
            dt = clsData.getAllInactiveTradeStudentCountWithCollege();

            if (dt.Rows.Count > 0)
            {
                ddlInatctiveTradeClg.DataTextField = "ITI Name";
                ddlInatctiveTradeClg.DataValueField = "collegeid";
                ddlInatctiveTradeClg.DataSource = dt;
                ddlInatctiveTradeClg.DataBind();
                ddlInatctiveTradeClg.Items.Insert(0, new ListItem("Select", ""));

                ddlActiveClg.DataTextField = "ITI Name";
                ddlActiveClg.DataValueField = "collegeid";
                ddlActiveClg.DataSource = dt;
                ddlActiveClg.DataBind();
                ddlActiveClg.Items.Insert(0, new ListItem("Select", ""));



                ddlInactiveTradeSection.ClearSelection();
                ddlActiveSection.ClearSelection();
                
            }
        }

        protected void ddlInatctiveTradeClg_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTrade = new DataTable();
            if (ddlInatctiveTradeClg.SelectedIndex == 0)
            {
                clsAlert.AlertMsg(this, "Please Select Institute!!!");
                return;

            }
            dtTrade = clsData.getAllInactiveTradeSection(Convert.ToInt32(ddlInatctiveTradeClg.SelectedItem.Value));






            ddlInactiveTradeSection.DataTextField = "newname";
            ddlInactiveTradeSection.DataValueField = "sectionid";
            ddlInactiveTradeSection.DataSource = dtTrade;
            ddlInactiveTradeSection.DataBind();
            ddlInactiveTradeSection.Items.Insert(0, new ListItem("Select", ""));


            ddlActiveClg.SelectedIndex = ddlInatctiveTradeClg.SelectedIndex;
            bindddlActivetrade();


        }



        private void bindddlActivetrade()
        {
            DataTable dtTrade = new DataTable();
            if (ddlActiveClg.SelectedIndex == 0)
            {
                clsAlert.AlertMsg(this, "Please Select Institute!!!");
                return;

            }
            dtTrade = clsData.GetActiveTradeSection(Convert.ToInt32(ddlActiveClg.SelectedItem.Value));


            ddlActiveSection.DataTextField = "sectionname";
            ddlActiveSection.DataValueField = "sectionid";
            ddlActiveSection.DataSource = dtTrade;
            ddlActiveSection.DataBind();
            ddlActiveSection.Items.Insert(0, new ListItem("Select", ""));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlInatctiveTradeClg.SelectedIndex == 0)
            {
                clsAlert.AlertMsg(this, "Please Select Inactive Institute!!!");
                return;

            }
            if (ddlActiveClg.SelectedIndex == 0)
            {
                clsAlert.AlertMsg(this, "Please Select Active Institute !!!");
                return;

            }
            if (ddlActiveSection.SelectedIndex == 0)
            {
                clsAlert.AlertMsg(this, "Please Select Active Trade Section!!!");
                return;

            }
            if (ddlInactiveTradeSection.SelectedIndex == 0)
            {
                clsAlert.AlertMsg(this, "Please Select Inactive Trade Section!!!");
                return;

            }



            DataTable dt = new DataTable();
            clsData.pNew_collegeid = ddlActiveClg.SelectedItem.Value;

            clsData.pNew_coursesectionid = ddlActiveSection.SelectedItem.Value;

            clsData.pOld_collegeid = ddlInatctiveTradeClg.SelectedItem.Value;

            clsData.pOld_coursesectionid = ddlInactiveTradeSection.SelectedItem.Value;


            clsData.p_IPAddress = GetIPAddress();
            clsData.p_ChangeUser = Convert.ToString(Session["UserId"]);

            dt = clsData.UpdateStuentPreference();
            if (dt.Rows.Count > 0)
            {

            }

            int result = 5;
            if (dt.Rows.Count > 0)
            {
                result = Convert.ToInt32(dt.Rows[0]["Result"].ToString());
            }
            if (result == 1)
            {
                clsAlert.AlertMsg(this, "Studens Shifted Successfully.");
                BindInactiveCollege();
            }

            else
            {
                clsAlert.AlertMsg(this, "Students not Shifted.... try again later.");
                BindInactiveCollege();
            }
        }
    }

}