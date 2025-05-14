using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HigherEducation.BusinessLayer;
using System.IO;
using System.Drawing;

namespace HigherEducation.HigherEducations
{
    public partial class frmCollegeSeatMatrix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.checkreferer();
            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/DHE/frmlogin.aspx", true);

            }
         
            if (!Page.IsPostBack)
            {
               BindCollegeSeatMatrix();
        
            }
         
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }
        //public void BindCollege()
        //{
        //    try
        //    {
        //        clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();

        //        DataTable dt = new DataTable();
        //        dt = CS.BindCollege();
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlCollege.DataSource = dt;
        //            ddlCollege.DataTextField = "Text";
        //            ddlCollege.DataValueField = "Value";
        //            ddlCollege.DataBind();
        //            ddlCollege.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select College--", "0"));
        //            ddlCollege.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
       
        //public void BindCourse()
        //{
        //    try
        //    {
        //        clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();
        //        CS.Collegeid = ddlCollege.SelectedValue;
        //        CS.Sessionid = "10";//2020-21
        //        DataTable dt = new DataTable();
              
        //        dt = CS.BindCourse();
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlCourse.DataSource = dt;
        //            ddlCourse.DataTextField = "Text";
        //            ddlCourse.DataValueField = "Value";
        //            ddlCourse.DataBind();

        //            ddlCourse.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Course--", "0"));
        //            ddlCourse.Focus();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //public void BindSection()
        //{
        //    try
        //    {
        //        clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();
        //        CS.Courseid = ddlCourse.SelectedValue;
        //        CS.Collegeid = ddlCollege.SelectedValue;
        //        CS.Sessionid = "10";//2020-21
        //        DataTable dt = new DataTable();
        //        dt = CS.BindSection();
        //        if (dt.Rows.Count > 0)
        //        {
        //            ddlSection.DataSource = dt;
        //            ddlSection.DataTextField = "Text";
        //            ddlSection.DataValueField = "Value";
        //            ddlSection.DataBind();
        //            ddlSection.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Section--", "0"));
        //            ddlSection.Focus();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkId.Checked == true)
                {
                    int i = CheckCalSeats();
                    if (i == 1)
                    {

                        return;
                    }
                    else
                    {
                        clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();

                        foreach (GridViewRow row in GridView1.Rows)
                        {

                            if (row.RowType == DataControlRowType.DataRow)
                            {


                                Label lblTotalSeats = (row.FindControl("lblTotalSeats") as Label);
                                string Collegeid = (row.FindControl("lblCollegeid") as Label).Text;
                                string Courseid = (row.FindControl("lblCourseId") as Label).Text;
                                string Sectionid = (row.FindControl("lblSectionId") as Label).Text;

                                string CourseCombid = (row.FindControl("lblCourseCombid") as Label).Text;
                                string OpenSeats = (row.FindControl("txtOpen") as TextBox).Text;

                                string haryanaGeneral = (row.FindControl("txtHryGen") as TextBox).Text;
                                string EcoWeaker = (row.FindControl("txtEcoWeaker") as TextBox).Text;
                               // string TotalHOGCEWS = (row.FindControl("lblTotalHOGCEWS") as TextBox).Text;
                               

                                string SC = (row.FindControl("txtSC") as TextBox).Text;
                                string DSC = (row.FindControl("txtDSC") as TextBox).Text;
                                string TotalSC = (row.FindControl("lblTotalSC") as Label).Text;

                                string BCA = (row.FindControl("txtBCA") as TextBox).Text;
                                string BCB = (row.FindControl("txtBCB") as TextBox).Text;
                                string TotalBC = (row.FindControl("lblTotalBC") as Label).Text;

                                
                                string DAG = (row.FindControl("txtDAG") as TextBox).Text;//Phgen
                                string DASC = (row.FindControl("txtDASC") as TextBox).Text;//PhSC
                                string DABC = (row.FindControl("txtDABC") as TextBox).Text;//PhBC
                                string TotalDA = (row.FindControl("lblTotalDA") as Label).Text;


                                string ESMG = (row.FindControl("txtESMG") as TextBox).Text;
                                string ESMBCA = (row.FindControl("txtESMBCA") as TextBox).Text;
                                string ESMBCB = (row.FindControl("txtESMBCB") as TextBox).Text;
                                string ESMSC = (row.FindControl("txtESMSC") as TextBox).Text;
                                string ESMDSC = (row.FindControl("txtESMDSC") as TextBox).Text;
                                string TotalHOGCEWS = (row.FindControl("lblTotalHOGCEWS") as Label).Text;
                                string TotalESM = "0";//(row.FindControl("lblTotalESM") as Label).Text;

                                CS.OpenSeats = OpenSeats;
                                CS.HryGen = haryanaGeneral;
                                CS.EcoWeaker = EcoWeaker;
                                CS.SC = SC;
                                CS.DSC = DSC;
                                CS.TotalSC = TotalSC;

                                CS.BCA = BCA;
                                CS.BCB = BCB;
                                CS.TotalBC = TotalBC;

                                CS.DAG = DAG;
                                CS.DASC = DASC;
                                CS.DABC = DABC;
                                CS.TotalDA = TotalDA;

                               
                                CS.ESMG = ESMG;
                                CS.ESMBCA = ESMBCA;
                                CS.ESMBCB = ESMBCB;
                                CS.ESMSC = ESMSC;
                                CS.ESMDSC = ESMDSC;

                                CS.Collegeid = Collegeid;
                                CS.Courseid = Courseid;
                                CS.Sectionid = Sectionid;
                                CS.CourseCombid = CourseCombid;

                                CS.TotalHOGCEWS = TotalHOGCEWS;
                                CS.TotalESM = TotalESM;
                                CS.UserId = Convert.ToString(Session["UserId"]);
                                string dtUpdate = CS.UpdateCollegeSeatMatrix();
                                if (dtUpdate == "1")
                                {
                                    clsAlert.AlertMsg(this, "Records Updated Successfully");
                                    
                                    
                                }
                                else
                                {
                                    clsAlert.AlertMsg(this, "There is some problem...");
                                    return;
                                }

                            }

                        }
                        BindCollegeSeatMatrix();
                    }
                }
                else
                {
                    clsAlert.AlertMsg(this, "Please Select Consent.");
                    //RetainTotalLabelValue();
                    chkId.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "btnUpdate_Click";
                clsLogger.SaveException();
            }

           
        }
        //public void GetCourseSeat()
        //{
        //    string Sectionid="0";
        //    string Courseid = "0";
        //    string Collegeid="0";
        //    clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();
        //   // CS.Collegeid = Convert.ToString(Session["CollegeId"]);//ddlCollege.SelectedValue;
        //    CS.Sessionid = "10";//2020-21
        //    CS.Courseid = "";// ddlCourse.SelectedValue;

        //    foreach (GridViewRow row in GridView1.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            Collegeid = (row.FindControl("lblCollegeid") as Label).Text;
        //            Sectionid = (row.FindControl("lblSectionId") as Label).Text;
        //            Courseid= (row.FindControl("lblCourseId") as Label).Text;
        //        }
        //    }
        //    CS.Collegeid = Collegeid;
        //    CS.Sectionid = Sectionid;
        //    CS.Courseid = Courseid;
        //    DataTable dt = new DataTable();

        //    dt = CS.GetCourseSeat();
        //    if (dt.Rows.Count > 0)
        //    {
        //        lblTotalCourseSeats.Text =  dt.Rows[0]["totalseats"].ToString();
        //    }
               
        //}
        //public void TotalCombSubjectSeats()
        //{
        //    foreach (GridViewRow row in GridView1.Rows)
        //    {

        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            Label lblTotalSeats = (row.FindControl("lblTotalSeats") as Label);
        //            Int32 TotalSeats = Convert.ToInt32(lblTotalSeats.Text) + Convert.ToInt32(lblTotalSeats.Text);
   
        //            lblTotalSubCombSeats.Text = TotalSeats.ToString();

        //        }
        //    }
        //}
        public int CheckCalSeats()
        {
            int sum = 0;
            string TotalHOGCEWS = "0";
            string TotalESM = "0";
            clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();

            foreach (GridViewRow row in GridView1.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {


                    Label lblTotalSeats = (row.FindControl("lblTotalSeats") as Label);
                    string Sectionid = (row.FindControl("lblSectionId") as Label).Text;
                    string CourseCombid = (row.FindControl("lblCourseCombid") as Label).Text;

                    string OpenSeats = (row.FindControl("txtOpen") as TextBox).Text;

                    string haryanaGeneral = (row.FindControl("txtHryGen") as TextBox).Text;
                    string EcoWeaker = (row.FindControl("txtEcoWeaker") as TextBox).Text;

                    TotalHOGCEWS = (row.FindControl("lblTotalHOGCEWS") as Label).Text;
                   // TotalESM = (row.FindControl("lblTotalESM") as Label).Text;

                    //string hdTotalHOGCEWS = TotalHOGCEWS;


                    string SC = (row.FindControl("txtSC") as TextBox).Text;
                    string DSC = (row.FindControl("txtDSC") as TextBox).Text;

                    string BCA = (row.FindControl("txtBCA") as TextBox).Text;
                    string BCB = (row.FindControl("txtBCB") as TextBox).Text;
                    
                   
                    //string DA = (row.FindControl("txtDA") as TextBox).Text;
                    string DAG = (row.FindControl("txtDAG") as TextBox).Text;//Phgen
                    string DASC = (row.FindControl("txtDASC") as TextBox).Text;//PhSC
                    string DABC = (row.FindControl("txtDABC") as TextBox).Text;//PhBC

                    string ESMG = (row.FindControl("txtESMG") as TextBox).Text;
                    string ESMBCA = (row.FindControl("txtESMBCA") as TextBox).Text;
                    string ESMBCB = (row.FindControl("txtESMBCB") as TextBox).Text;
                    string ESMSC = (row.FindControl("txtESMSC") as TextBox).Text;
                    string ESMDSC = (row.FindControl("txtESMDSC") as TextBox).Text;

                 

                  

                    int i_OpenSeats = Convert.ToInt32(OpenSeats);
                    int i_HryGen = Convert.ToInt32(haryanaGeneral);
                    int i_EcoWeaker = Convert.ToInt32(EcoWeaker);
                    int i_SC = Convert.ToInt32(SC);
                    int i_DSC =Convert.ToInt32( DSC);

                    int i_BCA =Convert.ToInt32( BCA);
                    int i_BCB = Convert.ToInt32(BCB);

                    int i_DAG = Convert.ToInt32(DAG);
                    int i_DASC = Convert.ToInt32(DASC); 
                    int i_DABC = Convert.ToInt32(DABC);

                    int i_ESMG= Convert.ToInt32(ESMG);
                    int i_ESMBCA = Convert.ToInt32(ESMBCA);
                    int i_ESMBCB = Convert.ToInt32(ESMBCB);
                    int i_ESMSC = Convert.ToInt32(ESMSC);
                    int i_ESMDSC = Convert.ToInt32(ESMDSC);

                   // TotalESM = Convert.ToString(i_ESMG + i_ESMBCA + i_ESMBCB + i_ESMSC + i_ESMDSC);
                    TotalHOGCEWS = Convert.ToString(i_OpenSeats + i_HryGen);

                  Int32 TotalSumCatag = i_OpenSeats + i_HryGen + i_EcoWeaker +  i_SC + i_DSC + i_BCA + i_BCB + i_DAG + i_DASC + i_DABC + i_ESMG + i_ESMBCA + i_ESMBCB + i_ESMSC + i_ESMDSC;
                   

                    if (TotalSumCatag > Convert.ToInt32(lblTotalSeats.Text) )
                    {
                        clsAlert.AlertMsg(this, "Sum of all Category Seats cannot exceed Max. no of seats " + lblTotalSeats.Text);
                        sum = 1;
                      //  RetainTotalLabelValue();
                        return sum;
                    }
                    else
                    {
                        sum = 0;
                       //return sum;
                    }
                  
                }
                
            }
            return sum;
        }
        public void RetainTotalLabelValue()
        {
            //foreach (GridViewRow row in GridView1.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        string HdTotalHOGCEWS = (row.FindControl("hdHOG") as HiddenField).Value;
            //        (row.FindControl("lblTotalHOGCEWS") as Label).Text = HdTotalHOGCEWS;
            //        string HdTotalSC = (row.FindControl("hdTotalSC") as HiddenField).Value;
            //        (row.FindControl("lblTotalSC") as Label).Text = HdTotalSC;
            //        string HdTotalBC = (row.FindControl("hdTotalBC") as HiddenField).Value;
            //        (row.FindControl("lblTotalBC") as Label).Text = HdTotalBC;
            //        string HdTotalDA = (row.FindControl("hdTotalDA") as HiddenField).Value;
            //        (row.FindControl("lblTotalDA") as Label).Text = HdTotalDA;
            //        string HdTotalESM = (row.FindControl("hdTotalESM") as HiddenField).Value;
            //        (row.FindControl("lblTotalESM") as Label).Text = HdTotalESM;
            //    }
            //}
        }
        public void BindCollegeSeatMatrix()
        {
            try
            {
                DataTable dt = new DataTable();
                clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();
                CS.Collegeid = Convert.ToString(Session["CollegeId"]);//ddlCollege.SelectedValue;
                CS.Courseid = "";
                                 
                dt = CS.GetCollegeSeatMatrix();
                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    //Calculate Sum and display in Footer Row
                    
                    Int64 totalseats = dt.AsEnumerable().Sum(row => row.Field<Int64>("totalSeats"));
                    Int64 AIOC = dt.AsEnumerable().Sum(row => row.Field<Int64>("openSeats"));
                    Int64 HOGC = dt.AsEnumerable().Sum(row => row.Field<Int64>("haryanaGeneral"));
                    Int64 EcoWeaker = dt.AsEnumerable().Sum(row => row.Field<Int64>("EcoWeaker"));
                    Int64 TotalHOGCEWS = dt.AsEnumerable().Sum(row => row.Field<Int64>("TotalHOGCEWS"));
                    Int64 SC = dt.AsEnumerable().Sum(row => row.Field<Int64>("SC"));
                    Int64 DSC = dt.AsEnumerable().Sum(row => row.Field<Int64>("DSC"));
                    Int64 SCTotal = dt.AsEnumerable().Sum(row => row.Field<Int64>("SCTotal"));
                    Int64 BCA = dt.AsEnumerable().Sum(row => row.Field<Int64>("BCA"));
                    Int64 BCB = dt.AsEnumerable().Sum(row => row.Field<Int64>("BCB"));
                    Int64 BCTotal = dt.AsEnumerable().Sum(row => row.Field<Int64>("BCTotal"));
                    Int64 PHGen = dt.AsEnumerable().Sum(row => row.Field<Int64>("PHGen"));//DAG
                    Int64 PHBC = dt.AsEnumerable().Sum(row => row.Field<Int64>("PHBC"));//DABC
                    Int64 PHSC = dt.AsEnumerable().Sum(row => row.Field<Int64>("PHSC"));//DASC
                    Int64 TotalDA = dt.AsEnumerable().Sum(row => row.Field<Int64>("DA"));//TotalDA
                    Int64 ESMGenCatWise = dt.AsEnumerable().Sum(row => row.Field<Int64>("ESMGenCatWise"));
                    Int64 ESMBCACatWise = dt.AsEnumerable().Sum(row => row.Field<Int64>("ESMBCACatWise"));
                    Int64 ESMBCBCatWise = dt.AsEnumerable().Sum(row => row.Field<Int64>("ESMBCBCatWise"));
                    Int64 ESMSCCatWise = dt.AsEnumerable().Sum(row => row.Field<Int64>("ESMSCCatWise"));
                    Int64 ESMSCDCatwise = dt.AsEnumerable().Sum(row => row.Field<Int64>("ESMSCDCatwise"));
                    // Int64 TotalESM = dt.AsEnumerable().Sum(row => row.Field<Int64>("TotalESM"));

                    GridView1.FooterRow.Cells[1].Text = "Total";
                    GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    GridView1.FooterRow.Cells[2].Text = totalseats.ToString();
                    GridView1.FooterRow.Cells[3].Text = AIOC.ToString();
                    GridView1.FooterRow.Cells[4].Text = HOGC.ToString();
                    GridView1.FooterRow.Cells[5].Text = EcoWeaker.ToString();
                    GridView1.FooterRow.Cells[6].Text = ESMGenCatWise.ToString();
                    GridView1.FooterRow.Cells[7].Text = TotalHOGCEWS.ToString();
                    GridView1.FooterRow.Cells[8].Text = SC.ToString();
                    GridView1.FooterRow.Cells[9].Text = DSC.ToString();
                    GridView1.FooterRow.Cells[10].Text = ESMSCCatWise.ToString();
                    GridView1.FooterRow.Cells[11].Text = ESMSCDCatwise.ToString();
                    GridView1.FooterRow.Cells[12].Text = SCTotal.ToString();
                    GridView1.FooterRow.Cells[13].Text = BCA.ToString();
                    GridView1.FooterRow.Cells[14].Text = BCB.ToString();
                    GridView1.FooterRow.Cells[15].Text = ESMBCACatWise.ToString();
                    GridView1.FooterRow.Cells[16].Text = ESMBCBCatWise.ToString();
                    GridView1.FooterRow.Cells[17].Text = BCTotal.ToString();
                    GridView1.FooterRow.Cells[18].Text = PHGen.ToString();
                    GridView1.FooterRow.Cells[19].Text = PHBC.ToString();
                    GridView1.FooterRow.Cells[20].Text = PHSC.ToString();
                    GridView1.FooterRow.Cells[21].Text = TotalDA.ToString();
                    // GridView1.FooterRow.Cells[22].Text = TotalESM.ToString();
                    CheckCalSeats();

                    // TotalCombSubjectSeats();
                    //GetCourseSeat();
                    dvlblTotal.Style.Add("display", "inline-block");
                    dvUpdate.Style.Add("display", "inline-block");
                    dvchkId.Style.Add("display", "inline-block");
                    // dvFullForm.Style.Add("display", "inline-block");

                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    clsAlert.AlertMsg(this, "Records Not Found");
                    dvlblTotal.Style.Add("display", "none");
                    dvUpdate.Style.Add("display", "none");
                    dvchkId.Style.Add("display", "none");
                    //dvFullForm.Style.Add("display", "inline-block");
                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "BindCollegeSeatMatrix";
                clsLogger.SaveException();
            }
            

        }
        //protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCourse.SelectedValue != "0")
        //    {
        //        //BindSection();
        //        DataTable dt = new DataTable();
        //        clsCollegeSeatMatrix CS = new clsCollegeSeatMatrix();
        //        CS.Collegeid = ddlCollege.SelectedValue;
        //        CS.Courseid = ddlCourse.SelectedValue;
        //       // CS.Sectionid = ddlSection.SelectedValue;
        //        dt = CS.GetCollegeSeatMatrix();
        //        if (dt.Rows.Count > 0)
        //        {
        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();

        //           // TotalCombSubjectSeats();
        //            GetCourseSeat();
        //            dvlblTotal.Style.Add("display", "inline-block");
        //            dvUpdate.Style.Add("display", "inline-block");
        //            dvchkId.Style.Add("display", "inline-block");
        //            dvFullForm.Style.Add("display", "inline-block");

        //        }
        //        else
        //        {
        //            GridView1.DataSource = null;
        //            GridView1.DataBind();
        //            clsAlert.AlertMsg(this, "Records Not Found");
        //            dvlblTotal.Style.Add("display", "none");
        //            dvUpdate.Style.Add("display", "none");
        //            dvchkId.Style.Add("display", "none");
        //            dvFullForm.Style.Add("display", "none");
        //        }
        //    }
        //}

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        //protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlSection.SelectedValue != "0")
        //    {
               
              
        //    }
        //}

        //protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCollege.SelectedValue != "0")
        //    {
        //        BindCourse();
        //    }
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
          // ddlCourse.SelectedIndex = -1;
            //ddlSection.SelectedIndex = - 1;
            dvchkId.Style.Add("display", "none");
        }

        //protected void btnExptoExcel_Click(object sender, EventArgs e)
        //{
        //    if (GridView1.Rows.Count == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Data Found');", true);
        //        return;
        //    }
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=SeatMatrix.xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);

        //        //To Export all pages
        //        GridView1.AllowPaging = false;
        //       // BindGrid();
        //        GridView1.HeaderRow.BackColor = Color.White;
        //        GridView1.HeaderRow.ForeColor = Color.White;
        //        foreach (TableCell cell in GridView1.HeaderRow.Cells)
        //        {
        //            cell.BackColor = GridView1.HeaderStyle.BackColor;
        //        }
        //        foreach (GridViewRow row in GridView1.Rows)
        //        {
        //            row.BackColor = Color.White;
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                if (row.RowIndex % 2 == 0)
        //                {
        //                    cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
        //                }
        //                else
        //                {
        //                    cell.BackColor = GridView1.RowStyle.BackColor;
        //                }
        //                cell.CssClass = "textmode";
        //            }
        //        }

        //        GridView1.RenderControl(hw);

        //        //style to format numbers to string
        //        string style = @"<style> .textmode { } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
        //       server control at run time. */
        //}
    }

}