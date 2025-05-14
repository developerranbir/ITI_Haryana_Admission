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
    public partial class frmViewCollegeSeatMatrix : System.Web.UI.Page
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
                    ddlCollege.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select ITI--", "0"));
                    ddlCollege.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }
      
        protected void btSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
          
            if (ddlCollege.SelectedValue != "0")
            {
                dvFullForm.Style.Add("display", "Inline-block");
                clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();
                CS.Collegeid = ddlCollege.SelectedValue;
             
                    dt = CS.GetCollegeSeatMatrix();
              
                if (dt.Rows.Count > 0)
                {
                    GridView3.DataSource = dt;
                    GridView3.DataBind();
                    //Calculate Sum and display in Footer Row


                    Int64 totalseats = dt.AsEnumerable().Sum(row => row.Field<Int64>("totalSeats"));
                    Int64 totalvacantseats = dt.AsEnumerable().Sum(row => row.Field<Int64>("totalvacantseats"));
                    Int64 OPENGEN = dt.AsEnumerable().Sum(row => row.Field<Int64>("OPENGEN"));
                    Int64 OPENGENESM = dt.AsEnumerable().Sum(row => row.Field<Int64>("OPENGENESM"));
                    Int64 BC = dt.AsEnumerable().Sum(row => row.Field<Int64>("BC"));
                    Int64 ESMBC = dt.AsEnumerable().Sum(row => row.Field<Int64>("ESMBC"));
                    Int64 SC = dt.AsEnumerable().Sum(row => row.Field<Int64>("SC"));
                    Int64 ESMSC = dt.AsEnumerable().Sum(row => row.Field<Int64>("ESMSC"));
                    Int64 EWS = dt.AsEnumerable().Sum(row => row.Field<Int64>("EWS"));
                    Int64 PH = dt.AsEnumerable().Sum(row => row.Field<Int64>("PH"));


                    GridView3.FooterRow.Cells[1].Text = "Total";
                    GridView3.FooterRow.Cells[1].ForeColor = Color.Navy;
                    GridView3.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    GridView3.FooterRow.Cells[2].Text = totalseats.ToString();
                    GridView3.FooterRow.Cells[3].Text = totalvacantseats.ToString();
                    GridView3.FooterRow.Cells[4].Text = OPENGEN.ToString();
                    GridView3.FooterRow.Cells[5].Text = OPENGENESM.ToString();
                    GridView3.FooterRow.Cells[6].Text = BC.ToString();
                    GridView3.FooterRow.Cells[7].Text = ESMBC.ToString();
                    GridView3.FooterRow.Cells[8].Text = SC.ToString();
                    GridView3.FooterRow.Cells[9].Text = ESMSC.ToString();
                    GridView3.FooterRow.Cells[10].Text = EWS.ToString();
                    GridView3.FooterRow.Cells[11].Text = PH.ToString();
                }
                else
                {
                    clsAlert.AlertMsg(this, "Seats not found.");
                    GridView3.DataSource = null;
                    GridView3.DataBind();
                }
            }
            else
            {
                clsAlert.AlertMsg(this, "Please Select ITI");
                GridView3.DataSource = null;
                GridView3.DataBind();
            }
        }

       
    }
}
    