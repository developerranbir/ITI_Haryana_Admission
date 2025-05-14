using HigherEducation.BAL;
using HigherEducation.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.DHE
{
    public partial class frmPvtITIFeePaid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            eDISHAutil eSessionMgmt = new eDISHAutil();
            clsLoginUser.CheckSession(Session["UserType"].ToString());

            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);

            }
            else
            {
                if (!Page.IsPostBack)
                {
                    hdCollegeid.Value = Convert.ToString(Session["CollegeId"]);
                    
                    BindCourse();
                    
                }
                txtCollegeName.Text = Convert.ToString(Session["CollegeName"]);
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }
        public void BindCourse()
        {
            try
            {

                clsPvtITIFeePaid cfp = new clsPvtITIFeePaid();
                cfp.Collegeid = hdCollegeid.Value;
                DataTable dt = new DataTable();
                dt = cfp.BindCourse();
                if (dt.Rows.Count > 0)
                {
                    ddlCouseName.DataSource = dt;
                    ddlCouseName.DataTextField = "Text";
                    ddlCouseName.DataValueField = "Value";
                    ddlCouseName.DataBind();
                    ddlCouseName.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Trade", "0"));
                    ddlCouseName.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void GetITIVerifyStudentInfo()
        {
            clsPvtITIFeePaid cfp = new clsPvtITIFeePaid();
            DataTable dt = new DataTable();

            cfp.Collegeid = hdCollegeid.Value;
            cfp.Courseid = ddlCouseName.SelectedValue;
            cfp.Counselling = WebConfigurationManager.AppSettings["Counselling"];
            dt = cfp.GetITIVerifyStudentInfo();
            if (dt.Rows.Count > 0)
            {
                dvPvtITIFeePaid.Style.Add("display", "inline-block");
                dvSubmit.Visible = true;
                GrdPvtITIFeePaid.DataSource = dt;
                GrdPvtITIFeePaid.DataBind();


            }
            else
            {
                dvPvtITIFeePaid.Style.Add("display", "none");
                dvSubmit.Visible = false;
                GrdPvtITIFeePaid.DataSource = dt;
                GrdPvtITIFeePaid.DataBind();
                clsAlert.AlertMsg(this, "No Record Found.");
                return;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                #region captcha
                if (Convert.ToString(Session["randomStr"]) == "" || Convert.ToString(Session["randomStr"]) == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Message3", "alert('Please Try Again!!!');", true);
                txtturing.Text = String.Empty;
                return;
            }
            string RNDStr = Session["randomStr"].ToString();
            if (txtturing.Text == "")
            {
               // ScriptManager.RegisterStartupScript(this, GetType(), "Message2", "alert('Please enter captcha');", true);
                    clsAlert.AlertMsg(this, "Please enter captcha");
                    return;
            }
            else
            {
                if (txtturing.Text.Trim() != RNDStr.Trim())
                {
                        //alert("Please enter your code correctly!!!");
                        ScriptManager.RegisterStartupScript(this, GetType(), "Message1", "alert('Please enter captcha correctly!!!');", true);
                       
                        txtturing.Text = string.Empty;
                    return;
                }
            }
            #endregion
            foreach (GridViewRow rowP in GrdPvtITIFeePaid.Rows)
            {
                if (rowP.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkFeePaid = rowP.FindControl("chkFeePaid") as CheckBox;
                    Label lblRegId = rowP.FindControl("lblRegId") as Label;
                    Label lblMobileNo= rowP.FindControl("lblMobileNo") as Label;
                        if (chkFeePaid.Checked == true)
                    {
                        clsPvtITIFeePaid cfp = new clsPvtITIFeePaid();
                        DataTable dt = new DataTable();
                        cfp.RegId = lblRegId.Text;
                        cfp.Collegeid = hdCollegeid.Value;
                        cfp.Counselling = WebConfigurationManager.AppSettings["Counselling"];
                        cfp.UserId = Convert.ToString(Session["UserId"]);
                        cfp.IPAddress = GetIPAddress();
                        string s = cfp.UpdatePvtITIFeePaid();
                        if (s == "1")
                        {
                                string mobileNo;
                                mobileNo = lblMobileNo.Text;
                                if (mobileNo != "")
                                {
                                    AgriSMS.sendSingleSMS(mobileNo, "Dear Student, Receipt of your payment towards " + lblRegId.Text + " for ITI admission is confirmed. To check, please login at https://itiharyanaadmissions.nic.in Regards, SDIT Haryana", "1007867539276723247");
                                }
                                clsAlert.AlertMsg(this, "Fee Updated Successfully.");
                            GetITIVerifyStudentInfo();
                        }
                        else
                        {
                            clsAlert.AlertMsg(this, "There is some problem..." + s);
                            return;
                        }
                    }
                       
                   
                }
            }
          }
            catch (Exception ex)
            {

                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/frmPvtITIFeePaid";
                clsLogger.ExceptionMsg = "btnSubmit_Click";
                clsLogger.SaveException();
                clsAlert.AlertMsg(this, "There is some problem... try later.");
                return;
            }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetITIVerifyStudentInfo();

        }
    }

}