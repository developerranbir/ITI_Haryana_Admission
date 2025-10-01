using HigherEducation.BusinessLayer;
using HigherEducation.Controllers;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.DHE
{
    public partial class UpdateStudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = Convert.ToString(Session["UserType"]);
            eDISHAutil eSessionMgmt = new eDISHAutil();

            clsLoginUser.CheckSession(UserType);

            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
                return;
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
            if (!Page.IsPostBack) { clear(); }

            if (Session["UserType"].ToString() == "2") { divstate.Visible = false; } else { divstate.Visible = true; }

        }

        public void disable()
        {
            dvSection.Style.Add("display", "none");
            dvSave.Style.Add("display", "none");
            dvCaptcha.Style.Add("display", "none");
        }

        public void enable()
        {
            dvSection.Style.Add("display", "block");
            dvSave.Style.Add("display", "inline-block");
            dvCaptcha.Style.Add("display", "block");
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

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                //  txtRemarks.Text = "";

                clsChangeCandidateMobile CSR = new clsChangeCandidateMobile();
                DataTable dt = new DataTable();
                CSR.RegId = txtRegId.Text.Trim();
                CSR.Collegeid = Convert.ToString(Session["CollegeId"]);
                dt = CSR.GetAdmissionDetailInfo();
                if (dt.Rows.Count > 0)
                {
                    dvSection.Attributes.Add("class", "cus-middle-section");
                    lblRegId.Text = dt.Rows[0]["Reg_Id"].ToString();
                    lblStudentName.Text = dt.Rows[0]["StudentName"].ToString();

                    lblStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    lblStuMotherName.Text = dt.Rows[0]["MotherName"].ToString();
                    lblGender.Text = dt.Rows[0]["GenderName"].ToString();
                     lblDOB.Text = (Convert.ToDateTime(dt.Rows[0]["BirthDate"].ToString())).ToString("yyyy-MM-dd");
                    lblCollegeName.Text = dt.Rows[0]["collegename"].ToString();
                    lblSectionName.Text = dt.Rows[0]["SectionName"].ToString();
                    lblAdmissionStatus.Text = dt.Rows[0]["Challan_status"].ToString();
                    string mobilno = dt.Rows[0]["MobileNo"].ToString();
                    string MaskedMobileNo = mobilno.Substring(0, 2) + "XXXX" + mobilno.Substring(mobilno.Length - 4);
                    //lblMobileNo.Text = MaskedMobileNo;


                    txtStudentName.Text = dt.Rows[0]["StudentName"].ToString();
                    txtStuFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                    txtStuMotherName.Text = dt.Rows[0]["MotherName"].ToString();

                    lblmobileno.Text = dt.Rows[0]["mobileno"].ToString();
                    lblEmailid.Text = dt.Rows[0]["EmailID"].ToString();

                    txtMobile.Text = dt.Rows[0]["mobileno"].ToString();
                    txtEmail.Text = dt.Rows[0]["EmailID"].ToString();
                      txtDOB.Text = (Convert.ToDateTime(dt.Rows[0]["BirthDate"].ToString())).ToString("yyyy-MM-dd");
                    enable();
                }
                else
                {
                    dvSection.Attributes.Remove("class");
                    clear();
                    clsAlert.AlertMsg(this, "Student not found. Please check Registration Number");
                    return;
                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/UpdateStudentDetails";
                clsLogger.ExceptionMsg = "btnGo_Click";
                clsLogger.SaveException();

            }

        }

       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (lblEmailid.ToString() != txtEmail.Text) 
                //{ }
                //if (lblmobileno.ToString() != txtMobile.Text) 
                //{ }


                if (txtDOB.Text == "" ||
                    txtStudentName.Text == "" ||
                    txtStuFatherName.Text == "" ||
                    txtStuMotherName.Text == "" ||
                    txtEmail.Text == "" ||
                    txtMobile.Text == "") 


                {
                    
                    clsAlert.AlertMsg(this, "Please Enter Required Details.");
                    return;
                }

                else 
                {
                    if (Session["UserType"].ToString() != "1") 
                    {
                        if (threeCharacterVal(lblStudentName.Text, txtStudentName.Text) > 3)
                        {
                            clsAlert.AlertMsg(this, "Only Three Character Changes Allowed in Name.");
                            return;
                        }

                        if (threeCharacterVal(lblStuFatherName.Text, txtStuFatherName.Text) > 3)
                        {
                            clsAlert.AlertMsg(this, "Only Three Character Changes Allowed in Father Name.");
                            return;
                        }

                        if (threeCharacterVal(lblStuMotherName.Text, txtStuMotherName.Text) > 3)
                        {
                            clsAlert.AlertMsg(this, "Only Three Character Changes Allowed in Mother Name.");
                            return;
                        }
                    }


                    bool isValid = IsValidDate(txtDOB.Text);

                    if (!isValid)
                    {
                        clsAlert.AlertMsg(this, "Incorrect Date Format or age criteria not met.");
                        return;
                    }
                    

                    clsChangeCandidateMobile CSR = new clsChangeCandidateMobile();
                    CSR.RegId = lblRegId.Text.Trim();
                    CSR.UserId = Convert.ToString(Session["UserId"]);
                    CSR.IPAddress = GetIPAddress();
                    CSR.StudentName = txtStudentName.Text.Trim();   
                    CSR.FatherName=txtStuFatherName.Text.Trim();
                    CSR.MotherName=txtStuMotherName.Text.Trim();
                    CSR.MobileNo = txtMobile.Text.Trim();
                    CSR.EmailID = txtEmail.Text.Trim();
              
                    CSR.DOB = (Convert.ToDateTime(txtDOB.Text.Trim()).ToString("yyyy-MM-dd"));

                    DataTable dt = new DataTable();
                    dt = CSR.UpdateStudentDetails();

                    int result = 5;
                    if (dt.Rows.Count > 0)
                    {
                        result = Convert.ToInt32(dt.Rows[0]["Result"].ToString());
                    }
                    if (result == 1)
                    {
                        clsAlert.AlertMsg(this, "Student Details updated successfully.");
                        clear();
                        return;
                    }
                    else if (result == 0)
                    {
                        clsAlert.AlertMsg(this, "Already Updated Once");
                        clear();
                        return;
                    }
                    else if (result == 6)
                    {
                        clsAlert.AlertMsg(this, "MobileNo Already Exists");
                        clear();
                        return;
                    }
                    else if (result == 7)
                    {
                        clsAlert.AlertMsg(this, "Email ID Already Exists");
                        clear();
                        return;
                    }
                    else
                    {
                        clsAlert.AlertMsg(this, "Student Details not changed.... try again later.");
                        clear();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/UpdateStudentDetails";
                clsLogger.ExceptionMsg = "btnSave_Click";
                clsLogger.SaveException();
            }

        }

        public static bool IsValidDate(string inputDate)
        {
            if (string.IsNullOrWhiteSpace(inputDate))
                return false;

            // Attempt to parse the input date
            if (DateTime.TryParse(inputDate, out DateTime parsedDate))
            {
                // Check if the parsed date is within a reasonable range (optional, based on your use case)
                DateTime maxDate = new DateTime(2011, 9, 01);  // i september 2011
                if (parsedDate <= maxDate)
                {
                    return true;
                }
            }

            return false;
        }
        private int threeCharacterVal(string word1, string word2)
        {
            if (word1 == null || word2 == null)
            {
                throw new ArgumentNullException("Input words cannot be null.");
            }

            int minLength = Math.Min(word1.Length, word2.Length);
            int differences = Math.Abs(word1.Length - word2.Length);
            if (differences < 3)
            {
                for (int i = 0; i < minLength; i++)
                {
                  
                 if (word1[i] != word2[i])
                    {
                        differences++;
                    }
                }
            }
            else
            {
                Console.WriteLine("Cannot update trainee details as changes are more then 3 character. Please contact your Department.");
            }

            return differences;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            disable();
            //ddlCatgAllotment.SelectedIndex = -1;
            // rdbtnPMS_SC.SelectedValue = "0";
            txtRegId.Text = "";
            lblRegId.Text = "";
            lblStudentName.Text = "";
            lblStuFatherName.Text = "";
            lblGender.Text = "";
            lblDOB.Text = "";
            lblCollegeName.Text = "";
            lblSectionName.Text = "";
            lblAdmissionStatus.Text = "";

            txtDOB.Text = string.Empty;
            txtStudentName.Text = string.Empty;
            txtStuFatherName.Text = string.Empty;
            txtStuMotherName.Text = string.Empty;

            chkDOB.Checked = false; 
            chkfname.Checked = false; 
            chkMother.Checked = false;
            ckhstdname.Checked = false;

            txtDOB.Visible = false;
            txtStudentName.Visible = false;
            txtStuFatherName.Visible = false;
            txtStuMotherName.Visible = false;


            lblDOB.Visible = true;
            lblStudentName.Visible = true;
            lblStuFatherName.Visible = true;
            lblStuMotherName.Visible = true;




        }

        protected void ckhstdname_CheckedChanged(object sender, EventArgs e)
        {
            if (ckhstdname.Checked)
            {
                txtStudentName.Visible = true;
                lblStudentName.Visible = false;
            }
            else
            {
                txtStudentName.Visible = false;
                lblStudentName.Visible = true;
            }

        }

        protected void chkfname_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfname.Checked)
            {
                txtStuFatherName.Visible = true;
                lblStuFatherName.Visible = false;
            }
            else
            {
                txtStuFatherName.Visible = false;
                lblStuFatherName.Visible = true;
            }
        }

        protected void chkDOB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDOB.Checked)
            {
                txtDOB.Visible = true;
                lblDOB.Visible = false;
            }
            else
            {
                txtDOB.Visible = false;
                lblDOB.Visible = true;
            }

        }

        protected void chkMother_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMother.Checked)
            {
                txtStuMotherName.Visible = true;
                lblStuMotherName.Visible = false;
            }
            else
            {
                txtStuMotherName.Visible = false;
                lblStuMotherName.Visible = true;
            }

        }

        protected void chkEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEmail.Checked)
            {
                txtEmail.Visible = true;
                lblEmailid.Visible = false;
            }
            else
            {
                txtEmail.Visible = false;
                lblEmailid.Visible = true;
            }


        }

        protected void chkMobile_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMobile.Checked)
            {
                txtMobile.Visible = true;
                lblmobileno.Visible = false;
            }
            else
            {
                txtMobile.Visible = false;
                lblmobileno.Visible = true;
            }
        }
    }
}