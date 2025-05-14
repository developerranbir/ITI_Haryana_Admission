using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HigherEducation.Models;

namespace HigherEducation.DHE
{
    public partial class MeritList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCollage();
            }
        }


        private void BindCollage()
        {
            ddlCollege.Items.Clear();
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            dt = obj.GetCollageData();
            obj.BindDDLCommon(dt, ddlCollege, "Value", "Text");
            ddlCollege.Items.Insert(0, new ListItem("--Select--", "0"));
        }


        private void BindCourse()
        {
            ddlCourse.Items.Clear();
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            dt = obj.BindCourse(ddlCollege.SelectedValue.Trim(), "10");
            obj.BindDDLCommon(dt, ddlCourse, "Value", "Text");
            ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCourse();
            BindMaritList();
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindMaritList();
        }
        private void BindMaritList()
        {
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            dt = obj.GetMaritDataList(ddlCollege.SelectedValue.Trim(), ddlCourse.SelectedValue.Trim());
            if (dt.Tables.Count > 0)
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = dt.Tables[0];
                    GridView1.DataBind();
                    GridView1.Visible = true;
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    GridView1.Visible = false;
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.Visible = false;
            }
        }
    }
}