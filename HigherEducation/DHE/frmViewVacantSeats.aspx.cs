using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmViewVacantSeats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            if (!Page.IsPostBack)
            {
                BindCollege();
               
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }
        public void BindCollege()
        {
            try
            {
                clsCollegeSearch CS = new clsCollegeSearch();

                DataTable dt = new DataTable();
                dt = CS.GetCollege();
                if (dt.Rows.Count > 0)
                {
                    ddlCollege.DataSource = dt;
                    ddlCollege.DataTextField = "Text";
                    ddlCollege.DataValueField = "Value";
                    ddlCollege.DataBind();
                    ddlCollege.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select College--", "0"));
                    ddlCollege.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

      

        protected void btSearch_Click(object sender, EventArgs e)
        {
            DataSet dt = new DataSet();
            if (ddlCollege.SelectedValue != "0")
            {
                // dvFullForm.Style.Add("display", "Inline-block");
                clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();
                CS.Collegeid = ddlCollege.SelectedValue;

                dt = CS.GetVacantSeats();
                if (dt.Tables.Count > 0)
                {
                    dvCoursewise.Style.Add("display", "inline-block");
                    dvSubComb.Style.Add("display", "inline-block");
                    GridView1.DataSource = dt.Tables[0];
                    GridView1.DataBind();
                    //Calculate Sum and display in Footer Row

                    Int64 totalseats = dt.Tables[0].AsEnumerable().Sum(row => row.Field<Int64>("totalseats"));
                    Int64 HOGC = dt.Tables[0].AsEnumerable().Sum(row => row.Field<Int64>("haryanaGeneral"));
                    Int64 SC = dt.Tables[0].AsEnumerable().Sum(row => row.Field<Int64>("SC"));

                    GridView1.FooterRow.Cells[1].Text = "Total";
                    GridView1.FooterRow.Cells[1].ForeColor = Color.Navy;
                    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[2].Text = totalseats.ToString();
                    GridView1.FooterRow.Cells[3].Text = HOGC.ToString();
                    GridView1.FooterRow.Cells[4].Text = SC.ToString();

                    //Second Grid Bind
                    GridView2.DataSource = dt.Tables[1];
                    GridView2.DataBind();
                    //Calculate Sum and display in Footer Row

                    Int64 noofseats = dt.Tables[1].AsEnumerable().Sum(row => row.Field<Int64>("noofseats"));
                    
                    GridView2.FooterRow.Cells[2].Text = "Total";
                    GridView2.FooterRow.Cells[2].ForeColor = Color.Navy;
                    GridView2.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    GridView2.FooterRow.Cells[3].Text = noofseats.ToString();
                
                }
                else
                {
                    dvCoursewise.Style.Add("display", "none");
                    dvSubComb.Style.Add("display", "none");
                    clsAlert.AlertMsg(this, "No Vacant Seats Found");
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }
            else
            {
                dvCoursewise.Style.Add("display", "none");
                dvSubComb.Style.Add("display", "none");
                clsAlert.AlertMsg(this, "Please Select College");
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
        }

    }
}
    