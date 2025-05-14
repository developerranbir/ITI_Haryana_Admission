using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.HigherEducations
{
    public partial class frmDistrictWiseCollege : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            if (!Page.IsPostBack)
            {
                BindDistrict();
                BindCollegeType();

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }
        public void BindDistrict()
        {
            try
            {
                clsCollegeSearch CS = new clsCollegeSearch();

                DataTable dt = new DataTable();
                dt = CS.BindDistrict();
                if (dt.Rows.Count > 0)
                {
                    ddlDistrict.DataSource = dt;
                    ddlDistrict.DataTextField = "Text";
                    ddlDistrict.DataValueField = "Value";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Please Select District--", "0"));
                    ddlDistrict.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void BindCollegeType()
        {
            try
            {
                clsCollegeSearch CS = new clsCollegeSearch();

                DataTable dt = new DataTable();
                dt = CS.BindCollegeType();
                if (dt.Rows.Count > 0)
                {
                    ddlCollegeType.DataSource = dt;
                    ddlCollegeType.DataTextField = "Text";
                    ddlCollegeType.DataValueField = "Value";
                    ddlCollegeType.DataBind();

                    ddlCollegeType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "A"));
                    //ddlCollegeType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select CollegeType--", "0"));
                    ddlCollegeType.Focus();

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (ddlDistrict.SelectedValue != "0")
            {

                clsCollegeSearch CS = new clsCollegeSearch();
                CS.distcode = ddlDistrict.SelectedValue;
                CS.collegetype = ddlCollegeType.SelectedValue;
                
                    dt = CS.GetCollegebyDistandType();
              
                   

                GridView1.DataSource = dt;
                GridView1.DataBind();
                // head.InnerText = ddlCollegeType.SelectedValue;
            }
            else
            {
                clsAlert.AlertMsg(this, "Please Select District");
            }
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    //Determine the RowIndex of the Row whose LinkButton was clicked.
                    int rowIndex = Convert.ToInt32(e.CommandArgument);

                    //Reference the GridView Row.
                    GridViewRow row = GridView1.Rows[rowIndex];

                    //Fetch value of Name.


                    string collegeid = (row.FindControl("lblCollegeid1") as Label).Text;
                    string collegeName = (row.FindControl("lblCollegeName") as Label).Text;
                    string collegeType = (row.FindControl("lblCollegeType") as Label).Text;

                    //Session["Collegeid"] = collegeid;

                    DataTable dt = new DataTable();
                    clsCollegeSearch cs = new clsCollegeSearch();
                    cs.collegeid = collegeid;

                   
                        dt = cs.GetCollegeInfo();
                        //liNoteUG.Visible = true;
                 
                  
                    if (dt.Rows.Count > 0)
                    {
                       
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                            GridView4.DataSource = null;
                            GridView4.DataBind();
                       
                        tdCName.InnerText = collegeName;
                        tdCType.InnerText = collegeType;
                        tdDist.InnerText = dt.Rows[0]["districtName"].ToString();
                        if (string.IsNullOrEmpty(dt.Rows[0]["isppp"].ToString()))
                        {
                            tdPPP.InnerText = "--";
                        }
                        else
                        {
                            tdPPP.InnerText = dt.Rows[0]["isppp"].ToString();
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["isExserviceMan"].ToString()))
                        {
                            tdExServiceman.InnerText = "--";
                        }
                        else
                        {
                            tdExServiceman.InnerText = dt.Rows[0]["isExserviceMan"].ToString();
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["isDumbAndDeaf"].ToString()))
                        {
                            tdDeafDumb.InnerText = "--";
                        }
                        else
                        {
                            tdDeafDumb.InnerText = dt.Rows[0]["isDumbAndDeaf"].ToString();
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["address"].ToString()))
                        {
                            tdAddress.InnerText = "-";
                        }
                        else
                        {
                            tdAddress.InnerText = dt.Rows[0]["address"].ToString();
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["EduMode"].ToString()))
                        {
                            tdEduMode.InnerText = "-";
                        }
                        else
                        {
                            tdEduMode.InnerText = dt.Rows[0]["EduMode"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["phoneno"].ToString()))
                        {
                            tdContact.InnerText = "-";
                        }
                        else
                        {
                            tdContact.InnerText = dt.Rows[0]["phoneno"].ToString();
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["emailid"].ToString()))
                        {
                            tdEmail.InnerText = "-";
                        }
                        else
                        {
                            tdEmail.InnerText = dt.Rows[0]["emailid"].ToString();
                        }
                        if (string.IsNullOrEmpty(dt.Rows[0]["website"].ToString()))
                        {
                            tdWebsite.InnerText = "-";
                        }
                        else
                        {
                            tdWebsite.InnerText = dt.Rows[0]["website"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["Principal_Name"].ToString()))
                        {
                            tdPrincipalName.InnerText = "-";
                        }
                        else
                        {
                            tdPrincipalName.InnerText = dt.Rows[0]["Principal_Name"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["Principal_PhoneNo"].ToString()))
                        {
                            tdPrincipalPhoneNo.InnerText = "-";
                        }
                        else
                        {
                            tdPrincipalPhoneNo.InnerText = dt.Rows[0]["Principal_PhoneNo"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["NodalAdmissions"].ToString()))
                        {
                            tdNodalOfficer.InnerText = "-";
                        }
                        else
                        {
                            tdNodalOfficer.InnerText = dt.Rows[0]["NodalAdmissions"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["NodalAdm_PhoneNo"].ToString()))
                        {
                            tdNodalPhone.InnerText = "-";
                        }
                        else
                        {
                            tdNodalPhone.InnerText = dt.Rows[0]["NodalAdm_PhoneNo"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordArts"].ToString()))
                        {
                            tdArts.InnerText = "-";
                        }
                        else
                        {
                            tdArts.InnerText = dt.Rows[0]["CordArts"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordArts_PhoneNo"].ToString()))
                        {
                            tdArtsPhone.InnerText = "-";
                        }
                        else
                        {
                            tdArtsPhone.InnerText = dt.Rows[0]["CordArts_PhoneNo"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordComm"].ToString()))
                        {
                            tdComm.InnerText = "-";
                        }
                        else
                        {
                            tdComm.InnerText = dt.Rows[0]["CordComm"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordComm_PhoneNo"].ToString()))
                        {
                            tdCommPhone.InnerText = "-";
                        }
                        else
                        {
                            tdCommPhone.InnerText = dt.Rows[0]["CordComm_PhoneNo"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordSc"].ToString()))
                        {
                            tdSc.InnerText = "-";
                        }
                        else
                        {
                            tdSc.InnerText = dt.Rows[0]["CordSc"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordSC_PhoneNo"].ToString()))
                        {
                            tdScPhone.InnerText = "-";
                        }
                        else
                        {
                            tdScPhone.InnerText = dt.Rows[0]["CordSC_PhoneNo"].ToString();
                        }


                        if (string.IsNullOrEmpty(dt.Rows[0]["CordJob"].ToString()))
                        {
                            tdJobOrd.InnerText = "-";
                        }
                        else
                        {
                            tdJobOrd.InnerText = dt.Rows[0]["CordJob"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordJob_PhoneNo"].ToString()))
                        {
                            tdJobOrdPhone.InnerText = "-";
                        }
                        else
                        {
                            tdJobOrdPhone.InnerText = dt.Rows[0]["CordJob_PhoneNo"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordFee"].ToString()))
                        {
                            tdFeeStruct.InnerText = "-";
                        }
                        else
                        {
                            tdFeeStruct.InnerText = dt.Rows[0]["CordFee"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["CordFee_PhoneNo"].ToString()))
                        {
                            tdFeeStructPhone.InnerText = "-";
                        }
                        else
                        {
                            tdFeeStructPhone.InnerText = dt.Rows[0]["CordFee_PhoneNo"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["MainAttract"].ToString()))
                        {
                            tdMainAttract.InnerText = "-";
                        }
                        else
                        {
                            tdMainAttract.InnerText = dt.Rows[0]["MainAttract"].ToString();
                        }

                        if (string.IsNullOrEmpty(dt.Rows[0]["docs"].ToString()))
                        {
                            hdPros.Value = "";
                            dvPros.Visible = false;
                        }
                        else
                        {
                            hdPros.Value = dt.Rows[0]["docs"].ToString();
                            dvPros.Visible = true;
                        }

                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showCollegeInfo();", true);
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Records not found");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmDistrictWiseCollege";
                clsLogger.ExceptionMsg = "GridView1_RowCommand";
                clsLogger.SaveException();
            }
           
        }

       

        //protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView2, "Select$" + e.Row.RowIndex);
        //        e.Row.ToolTip = "Click to select this row.";
                
        //    }
        //}

        //protected void OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        foreach (GridViewRow row in GridView2.Rows)
        //        {

        //            if (row.RowIndex == GridView2.SelectedIndex)
        //            {
        //                row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
        //                row.ToolTip = string.Empty;
        //                string coursesectionId = (row.FindControl("lblcoursesectionId") as Label).Text;
        //                string courseid = (row.FindControl("lblcourseId") as Label).Text;
        //                string collegeName = tdCName.InnerText;
        //                string courseName = (row.FindControl("lblCourseName") as Label).Text;
        //                tdG3CN.InnerText = collegeName;
        //                tdG3Course.InnerText = courseName;
        //                DataTable dt = new DataTable();
        //                clsCollegeSearch CS = new clsCollegeSearch();
        //                CS.collegeid = Convert.ToString(Session["Collegeid"]);
        //                CS.Courseid = courseid;
        //                CS.Sectionid = coursesectionId;
        //                dt = CS.GetSearchCollegeSeatMatrix();
        //                GridView3.DataSource = dt;
        //                GridView3.DataBind();

        //                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "showSubjectComb();", true);

        //            }
        //            else
        //            {
        //                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
        //                row.ToolTip = "Click to select this row.";

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsLogger.ExceptionError = ex.Message;
        //        clsLogger.ExceptionPage = "DHE/frmDistrictWiseCollege";
        //        clsLogger.ExceptionMsg = "OnSelectedIndexChanged";
        //        clsLogger.SaveException();
        //    }
          

            

        //}

        //protected void btnPros_Click(object sender, EventArgs e)
        //{

        //        string filename = "Prospectus";
        //        byte[] pdfBytes =Convert.FromBase64String(hdPros.Value); 
        //        MemoryStream ms = new MemoryStream(pdfBytes);
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "inline;filename=" + filename + ".pdf");//attachment for download ; inline for show
        //        Response.Buffer = true;
        //        ms.WriteTo(Response.OutputStream);
        //        Response.Flush();
        //        Response.SuppressContent = true;

        //}
    }
}
    