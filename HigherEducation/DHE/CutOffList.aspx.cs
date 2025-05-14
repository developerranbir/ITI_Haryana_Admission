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
    public partial class CutOffList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ddlSection.Visible = false;
            if(!IsPostBack)
            {
                BindCollege();
            }
        }

  private void BindCollege()
        {
            ddlCourse.Items.Clear();
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            dt = obj.D_CutOffDdl(null, null, null, null);
            obj.BindDDLCommon(dt, ddlCollege, "Value", "Text");
            ddlCollege.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindCourse()
        {
            ddlCourse.Items.Clear();
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            dt = obj.D_CutOffDdl(Convert.ToInt32(ddlCollege.SelectedValue),null,null,null);
            obj.BindDDLCommon(dt, ddlCourse, "Value", "Text");
            ddlCourse.Items.Insert(0, new ListItem("--Select--", "0"));
        }
      
        private void BindSection()
        {
            ddlSection.Items.Clear();
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            dt = obj.D_CutOffDdl(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue),null,null);
            obj.BindDDLCommon(dt, ddlSection, "Value", "Text");
            ddlSection.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        private void BindChoice()
        {
            ddlChoice.Items.Clear();
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            dt = obj.D_CutOffDdl(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), null);
            obj.BindDDLCommon(dt, ddlChoice, "Value", "Text");
            ddlChoice.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSection();
        }
        protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindChoice();
        }
        protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCourse();
        }
        protected void ddlChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCutOffList();
        }

        private void BindCutOffList()
        {
            DataSet dt = new DataSet();
            RSHigherEdu obj = new RSHigherEdu();
            dt = obj.D_CutOffDdl(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlCourse.SelectedValue), Convert.ToInt32(ddlSection.SelectedValue), Convert.ToInt32(ddlChoice.SelectedValue));
            if (dt.Tables.Count > 0)
            {
                if (dt.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = dt.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.Visible = true;
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.Visible = false;
                }
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                //GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.Visible = false;
            }
        }
    }
}