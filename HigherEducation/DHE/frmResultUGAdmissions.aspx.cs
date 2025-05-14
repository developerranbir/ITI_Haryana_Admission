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
    public partial class frmResultUGAdmissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            if (!Page.IsPostBack)
            {
               
               
            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }



        protected void btnGo_Click(object sender, EventArgs e)
        {
           
                GetCandidateInfo();
         
        }

      

        public void GetCandidateInfo()
        {
            clsResultUGAdm RU = new clsResultUGAdm();
            string AllocationSeat;
            string flgAllocation="0";
            DataTable dt = new DataTable();
            DataTable dtFilter = new DataTable();
            RU.RegId = txtRegId.Text.Trim();
            RU.CandidateName = txtCandidateName.Text.Trim();
            RU.Counselling = ddlCounselling.SelectedValue;
            dt = RU.GetCandidateInfo();
         

            if (dt.Rows.Count > 0)
            {
                lblRegId.Text= dt.Rows[0]["registrationID"].ToString();
                lblCandidateName.Text = dt.Rows[0]["applicant_name"].ToString();
                lblFatherName.Text  = dt.Rows[0]["applicant_father_name"].ToString();
                string eligible = dt.Rows[0]["eligible"].ToString().ToLower();
                if (eligible != "pass")
                {
                    clsAlert.AlertMsg(this, "Sorry! you are not eligible.");
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                    dvNote.Style.Add("display", "none");
                    dvGrdResultInfo.Style.Add("display", "none");
                    return;

                }

                dvSection.Style.Add("display", "inline-block");
                dvNote.Style.Add("display", "inline-block");
                dvGrdResultInfo.Style.Add("display", "inline-block");
                AllocationSeat = dt.Rows[0]["AllocationSeatStatus"].ToString();

               for (int i = 0; i < dt.Rows.Count; i++)
                {
                    AllocationSeat = dt.Rows[i]["AllocationSeatStatus"].ToString();
                    if (AllocationSeat == "1")
                    {
                     dtFilter = dt.AsEnumerable().Where
                       (row => row.Field<string>("AllocationSeatStatus").ToString() == "1").CopyToDataTable();
                        GrdResultInfo.DataSource = dtFilter;
                        GrdResultInfo.DataBind();
                        flgAllocation = "1";
                    }
                }

                if (flgAllocation == "0")
                {
                    clsAlert.AlertMsg(this, "Sorry! No Seat has been allocated to you in " + ddlCounselling.SelectedItem.Text + "");
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                    dvNote.Style.Add("display", "none");
                    dvGrdResultInfo.Style.Add("display", "none");
                    return;
                }
               
            }
            else
            {
                DataTable dtStu = new DataTable();
               
                RU.RegId = txtRegId.Text.Trim();
                dtStu = RU.GetStudentInfo();
                if (dtStu.Rows.Count > 0)
                {
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                    if (dtStu.Rows[0]["StudentName"].ToString().ToLower().Trim() == txtCandidateName.Text.ToLower().Trim())
                    {
                        lblRegId.Text = dtStu.Rows[0]["Reg_Id"].ToString();
                        lblCandidateName.Text = dtStu.Rows[0]["StudentName"].ToString();
                        lblFatherName.Text = dtStu.Rows[0]["FatherName"].ToString();
                        dvSection.Style.Add("display", "inline-block");
                    }
                    else
                    {
                        dvSection.Style.Add("display", "none");
                        dvNote.Style.Add("display", "none");
                        dvGrdResultInfo.Style.Add("display", "none");
                        GrdResultInfo.DataSource = null;
                        GrdResultInfo.DataBind();
                        clsAlert.AlertMsg(this, "Candidate Record Not Found");
                        return;
                    }
                    dvNote.Style.Add("display", "none");
                    dvGrdResultInfo.Style.Add("display", "none");
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                    clsAlert.AlertMsg(this, "Sorry!! No Seat has been allocated to you in " + ddlCounselling.SelectedItem.Text + "");
                    return;
                }
                else
                {
                    dvSection.Style.Add("display", "none");
                    dvNote.Style.Add("display", "none");
                    dvGrdResultInfo.Style.Add("display", "none");
                    GrdResultInfo.DataSource = null;
                    GrdResultInfo.DataBind();
                    clsAlert.AlertMsg(this, "Record not found");
                    return;
                }
            }
        }


        //public void GetCandidateInfo_PG()
        //{
        //    clsResultUGAdm RU = new clsResultUGAdm();
        //    string AllocationSeat;
        //    string flgAllocation = "0";
        //    DataTable dt = new DataTable();
        //    DataTable dtFilter = new DataTable();
        //    RU.RegId = txtRegId.Text.Trim();
        //    RU.CandidateName = txtCandidateName.Text.Trim();
        //    //RU.Counselling = ddlCounselling.SelectedValue;
        //    dt = RU.GetCandidateInfo_PG();


        //    if (dt.Rows.Count > 0)
        //    {
        //        lblRegId.Text = dt.Rows[0]["registrationID"].ToString();
        //        lblCandidateName.Text = dt.Rows[0]["applicant_name"].ToString();
        //        lblFatherName.Text = dt.Rows[0]["applicant_father_name"].ToString();
        //        string eligible = dt.Rows[0]["eligible"].ToString().ToLower();
        //        if (eligible != "pass")
        //        {
        //            clsAlert.AlertMsg(this, "Sorry! you are not eligible.");
        //            GrdResultInfo.DataSource = null;
        //            GrdResultInfo.DataBind();
        //            dvNote.Style.Add("display", "none");
        //            dvGrdResultInfo.Style.Add("display", "none");
        //            return;

        //        }

        //        dvSection.Style.Add("display", "inline-block");
        //        dvNote.Style.Add("display", "none");//PG Case none
        //        dvGrdResultInfo.Style.Add("display", "inline-block");
        //        AllocationSeat = dt.Rows[0]["AllocationSeatStatus"].ToString();

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            AllocationSeat = dt.Rows[i]["AllocationSeatStatus"].ToString();
        //            if (AllocationSeat == "1")
        //            {
        //                dtFilter = dt.AsEnumerable().Where
        //                  (row => row.Field<string>("AllocationSeatStatus").ToString() == "1").CopyToDataTable();
        //                GrdResultInfo.DataSource = dtFilter;
        //                GrdResultInfo.DataBind();
        //                flgAllocation = "1";
        //            }
        //        }

        //        if (flgAllocation == "0")
        //        {
        //            clsAlert.AlertMsg(this, "Sorry! No Seat has been allocated to you.");
        //            GrdResultInfo.DataSource = null;
        //            GrdResultInfo.DataBind();
        //            dvNote.Style.Add("display", "none");
        //            dvGrdResultInfo.Style.Add("display", "none");
        //            return;
        //        }

        //    }
        //    else
        //    {
        //        DataTable dtStu = new DataTable();

        //        RU.RegId = txtRegId.Text.Trim();
        //        dtStu = RU.GetStudentInfo_PG();
        //        if (dtStu.Rows.Count > 0)
        //        {
        //            GrdResultInfo.DataSource = null;
        //            GrdResultInfo.DataBind();
        //            if (dtStu.Rows[0]["StudentName"].ToString().ToLower() == txtCandidateName.Text.ToLower())
        //            {
        //                lblRegId.Text = dtStu.Rows[0]["Reg_Id"].ToString();
        //                lblCandidateName.Text = dtStu.Rows[0]["StudentName"].ToString();
        //                lblFatherName.Text = dtStu.Rows[0]["FatherName"].ToString();
        //                dvSection.Style.Add("display", "inline-block");
        //            }
        //            else
        //            {
        //                dvSection.Style.Add("display", "none");
        //                dvNote.Style.Add("display", "none");
        //                dvGrdResultInfo.Style.Add("display", "none");
        //                GrdResultInfo.DataSource = null;
        //                GrdResultInfo.DataBind();
        //                clsAlert.AlertMsg(this, "Candidate Record Not Found");
        //                return;
        //            }
        //            dvNote.Style.Add("display", "none");
        //            dvGrdResultInfo.Style.Add("display", "none");
        //            GrdResultInfo.DataSource = null;
        //            GrdResultInfo.DataBind();
        //            clsAlert.AlertMsg(this, "Sorry!! No Seat has been allocated to you..");
        //            return;
        //        }
        //        else
        //        {
        //            dvSection.Style.Add("display", "none");
        //            dvNote.Style.Add("display", "none");
        //            dvGrdResultInfo.Style.Add("display", "none");
        //            GrdResultInfo.DataSource = null;
        //            GrdResultInfo.DataBind();
        //            clsAlert.AlertMsg(this, "Record not found");
        //            return;
        //        }

        //    }
        //}

        protected void btReset_Click(object sender, EventArgs e)
        {
            txtRegId.Text = string.Empty;
            txtCandidateName.Text = string.Empty;
            dvSection.Style.Add("display", "none");
            dvNote.Style.Add("display", "none");
            dvGrdResultInfo.Style.Add("display", "none");
            GrdResultInfo.DataSource = null;
            GrdResultInfo.DataBind();
          
        }

        //protected void ddlUGPG_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //        dvUGMeritList.Style.Add("display", "inline-block");
        //        RequiredFieldValidator6.Enabled = true;
           
        //}
    }
}