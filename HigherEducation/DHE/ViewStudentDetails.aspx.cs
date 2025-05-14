using HigherEducation.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.DHE
{
    public partial class ViewStudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string UserType = "1";
            //eDISHAutil eSessionMgmt = new eDISHAutil();
            //eSessionMgmt.CheckSession(UserType);
            //if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
            //{
            //    Response.Redirect("~/DHE/frmlogin.aspx", true);
            //}
            //eSessionMgmt.SetCookie();

           
        }
        

       
        //protected void txtRagiste_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //  txtRemarks.Text = "";


        //        CandidateDetails CSR = new CandidateDetails();
        //        DataTable dt = new DataTable();
        //        CSR.RegId = txtRagiste.Text.Trim();
        //        //CSR.Collegeid = Convert.ToString(Session["CollegeId"]);
        //        dt = CSR.GetCandidateInfo();
        //        if (dt.Rows.Count > 0)
        //        {

        //            txtCandidateName.Text = dt.Rows[0]["CandidateName"].ToString();
        //            txtFName.Text = dt.Rows[0]["FHName"].ToString();
        //            txtmother.Text = dt.Rows[0]["MotherName"].ToString();
        //            txtmobile.Text = dt.Rows[0]["MobileNo"].ToString();
        //            txtaddress.Text = dt.Rows[0]["Street_Address_1"].ToString();
        //            //string AadhaarNo;
        //            //string MaskedAadhaarNo;
        //            //if (string.IsNullOrEmpty(dt.Rows[0]["AadhaarNo"].ToString()))

        //            //{
        //            //    AadhaarNo = "";
        //            //    MaskedAadhaarNo = "";
        //            //}
        //            //else
        //            //{
        //            //    AadhaarNo = dt.Rows[0]["AadhaarNo"].ToString();
        //            //    MaskedAadhaarNo = "XXXXXXXX" + AadhaarNo.Substring(AadhaarNo.Length - 4);
        //            //}
        //            //lblAadhaarNo.Text = MaskedAadhaarNo;
        //            //lblReservCatg.Text = dt.Rows[0]["reservationname"].ToString();
        //            //lblCasteCatg.Text = dt.Rows[0]["CategoryDesc"].ToString();
        //            //if (string.IsNullOrEmpty(dt.Rows[0]["castedesc"].ToString()))
        //            //{
        //            //    lblCaste.Text = "NA";
        //            //}
        //            //else
        //            //{
        //            //    lblCaste.Text = dt.Rows[0]["castedesc"].ToString();
        //            //}
        //            //lblExamPassed.Text = dt.Rows[0]["exampassed"].ToString() + " in (" + dt.Rows[0]["PassingYear"].ToString() + ")";
        //            //lblStream.Text = dt.Rows[0]["streamName"].ToString();
        //            //lblBoard.Text = dt.Rows[0]["Board"].ToString();
        //            //lblPassingYear.Text = dt.Rows[0]["PassingYear"].ToString();
        //            //lblTopFivePercentage.Text = dt.Rows[0]["topfivepercentage"].ToString();
        //            //lblFamilyId.Text = dt.Rows[0]["familyidfromppp"].ToString();
        //            //lblTweleveHaryana.Text = dt.Rows[0]["TwelveHarana"].ToString();
        //            //lblDomicile.Text = dt.Rows[0]["Has_Domicile"].ToString();
        //            //int weightage = 0;

        //            //if (Convert.ToInt32(dt.Rows[0]["WeightagePercentage"]) > 10)//Maximum 10
        //            //{
        //            //    weightage = 10;
        //            //}
        //            //else
        //            //{
        //            //    weightage = Convert.ToInt32(dt.Rows[0]["WeightagePercentage"]);
        //            //}

        //            //lblWeightage.Text = weightage.ToString();
        //            //Decimal topfivepercentage;
        //            //if (string.IsNullOrEmpty(dt.Rows[0]["topfivepercentage"].ToString()))
        //            //{
        //            //    topfivepercentage = 0;
        //            //}
        //            //else
        //            //{
        //            //    topfivepercentage = Convert.ToDecimal(lblTopFivePercentage.Text);
        //            //}
        //            //lblTotalPercentage.Text = Convert.ToString(weightage + topfivepercentage);
        //            //hdBoard_Code.Value = dt.Rows[0]["Board_Code"].ToString();
        //            //hdqualification.Value = dt.Rows[0]["exampassed"].ToString();
        //            //hdGenderid.Value = dt.Rows[0]["Genderid"].ToString();
        //            //hdRollNo.Value = dt.Rows[0]["RollNo"].ToString();
        //            //hdCollegeid.Value = Convert.ToString(Session["CollegeId"]);
        //            //hdCollegeName.Value = dt.Rows[0]["CollegeName"].ToString();
        //            //hdReservationcategoryid.Value = dt.Rows[0]["Reservationcategoryid"].ToString();
        //            //hdcategoryid.Value = dt.Rows[0]["categoryid"].ToString();
        //            //hdcasteid.Value = dt.Rows[0]["Casteid"].ToString();
        //            //hdmobileno.Value = dt.Rows[0]["MobileNo"].ToString();
        //            //hdemailid.Value = dt.Rows[0]["EmailId"].ToString();
        //            //Session["RegId"] = txtRegId.Text;
        //            //enable();

        //            //if (Session["CollegeType"].ToString() == "1")//Govt College
        //            //{
        //            //    dvPMSSCScholarShip.Style.Add("display", "none");
        //            //}
        //            //else
        //            //{
        //            //    dvPMSSCScholarShip.Style.Add("display", "inline-block");
        //            //}

        //            //GetAdmissionStatus();
        //            //GetSeatallotment_open1();


        //        }
        //        else
        //        {
        //            //dvSection.Attributes.Remove("class");
        //            //clear();
        //            clsAlert.AlertMsg(this, "Student registration not found.");
        //            return;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsLogger.ExceptionError = ex.Message;
        //        clsLogger.ExceptionPage = "DHE/ViewStudentDetails";
        //        clsLogger.ExceptionMsg = "btnGo_Click";
        //        clsLogger.SaveException();

        //    }
        //}

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Page.IsValid)
        //        {
        //            string confirmValue = Request.Form["confirm_value"];
        //            if (txtRagiste.Text == "")
        //            {
        //                clsAlert.AlertMsg(this, "Please Enter Registration No");
        //                return;
        //            }
        //            if (txtCandidateName.Text == "")
        //            {
        //                clsAlert.AlertMsg(this, "Please Enter Candidate Name");
        //                return;
        //            }
        //            if (txtFName.Text == "")
        //            {
        //                clsAlert.AlertMsg(this, "Please Select Father Name");
        //                return;
        //            }
        //            if (txtmother.Text == "")
        //            {
        //                clsAlert.AlertMsg(this, "Please Select Mobile");
        //                return;
        //            }
                    



        //            //CandidateDetails cg = new CandidateDetails();
        //            //cg.RegId = txtRagiste.Text.Trim();
        //            //cg.CandidateName = txtCandidateName.Text.Trim();
        //            //cg.FatherName = txtFName.Text.Trim();
        //            //cg.motherName = txtmother.Text.Trim();
        //            //cg.MobileNo = txtmobile.Text.Trim();
                    
        //            if (btnSubmit.Text == "Save")
        //            {



        //            }
        //            else if (btnSubmit.Text == "Update")
        //            {
        //                //string s = cg.UpdateCandidate();
        //                //if (s == "1")
        //                //{

        //                //    clsAlert.AlertMsg(this, "Candidate Updated Successfully.");
        //                //    return;
        //                //}
        //                //else
        //                //{
        //                //    clsAlert.AlertMsg(this, "Candidate not Updated.");
        //                //    return;
        //                //}


        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsLogger.ExceptionError = ex.Message;
        //        clsLogger.ExceptionPage = "HigherEducation/ViewStudentDetails";
        //        clsLogger.ExceptionMsg = "btnSubmit_Click";
        //        clsLogger.SaveException();
        //    }
        //}

        protected void btnsa_Click(object sender, EventArgs e)
        {

        }

        //protected void btnimg_Click(object sender, EventArgs e)
        //{
        //    double currentFileSize = 0;
        //    string imagename = "";
        //    lblimgerror.Text = "";
        //    // if (FileUpload1.HasFile)
        //    if (FileUpload1.HasFile)
        //    {
        //        currentFileSize = Math.Round(FileUpload1.PostedFile.ContentLength / (1024.00 * 1024.00), 1);
        //        ViewState["currentFileSize"] = currentFileSize;
        //        if (currentFileSize > 5)
        //        {
        //            lblimgerror.ForeColor = System.Drawing.Color.Red;
        //            lblimgerror.Text = "Can't Upload more than 5MB file";
        //            return;
        //        }
        //        string path = Server.MapPath("..\\MemberImage");
        //        DirectoryInfo di = new DirectoryInfo(path);
        //        if (!di.Exists)
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        string s = Path.GetFileName(FileUpload1.FileName);
        //        string[] splits = s.Split('.');
        //        imagename = "img" + Session["MaxReg"].ToString() + "." + splits[1];
        //        ViewState["imagename"] = imagename;
        //        FileUpload1.SaveAs(Path.Combine(path, imagename).ToString());
        //        //img1.ImageUrl = "..\\MemberImage\\" + imagename + "";
        //        int changeimg = objDUT.ExecuteSql("update member_master set IDNo='" + imagename + "' where regno=" + Session["MaxReg"].ToString() + "");
        //        if (changeimg > 0)
        //        {
        //            lblimgerror.Text = "Image Uploaded...";
        //            lblimgerror.ForeColor = System.Drawing.Color.Green;
        //            btnimg.Enabled = false;
        //        }


        //    }
        //    else
        //    {
        //        lblimgerror.ForeColor = System.Drawing.Color.Red;
        //        lblimgerror.Text = "Please  Upload Photo - jpg,png,jpeg - format";
        //        return;
        //    }
        //}

        //protected void EduRagistration_TextChanged(object sender, EventArgs e)
        //{
        //    CandidateDetails CS = new CandidateDetails();

        //    Qualification();

        //}
        //public void Qualification()
        //{
        //    try
        //    {
        //        CandidateDetails CS = new CandidateDetails();
        //        CS.quaRegId = EduRagistration.Text.Trim();
        //        DataTable dt = new DataTable();
        //        dt = CS.BindQualification();
        //        if (dt.Rows.Count > 0)
        //        {
        //            grdCollege.DataSource = dt;
        //            grdCollege.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsLogger.ExceptionError = ex.Message;
        //        clsLogger.ExceptionPage = "HigherEducation/ViewStudentDetails";
        //        clsLogger.ExceptionMsg = "BindQualification";
        //        clsLogger.SaveException();
        //    }
        //}
        //protected void grdCollege_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "SEL")
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsLogger.ExceptionError = ex.Message;
        //        clsLogger.ExceptionPage = "HigherEducation/frmCollegeMaster";
        //        clsLogger.ExceptionMsg = "grdCollege_RowCommand";
        //        clsLogger.SaveException();
        //    }
        //}
        //protected void grdCollege_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grdCollege.PageIndex = e.NewPageIndex;
        //    Qualification();
        //}
    }
}