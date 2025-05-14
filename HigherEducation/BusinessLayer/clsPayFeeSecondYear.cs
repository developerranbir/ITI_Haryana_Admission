using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;
using static System.Net.Mime.MediaTypeNames;

namespace HigherEducation.BusinessLayer
{

    public class clsPayFeeSecondYear
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducationR"].ToString());

        public string RollNo { get; set; }
        public string RegId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Collegeid { get; set; }
        public string FamilyId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string docs { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Gender { get; set; }
        public string AadhaarNo { get; set; }
        public string ReservCatgid { get; set; }
        public string ReservCatgName { get; set; }
        public string Categoryid { get; set; }
        public string Category { get; set; }
        public string Casteid { get; set; }
        public string Caste { get; set; }
        public string BoardName { get; set; }
        public string CollegeName { get; set; }
        public string Courseid { get; set; }
        public string CourseName { get; set; }
        public string Sectionid { get; set; }
        public string SectionName { get; set; }
        public string SubCombid { get; set; }
        public string SubCombName { get; set; }
        public string PMS_SC { get; set; }
        public string SeatAllocationCategory { get; set; }
        public string exampassed { get; set; }
        public string TotalPercentage { get; set; }
        public string Weightage { get; set; }
        public string Domicile { get; set; }
        public string flgDelete { get; set; }
        public string TwelveHaryana { get; set; }

        public string Counselling { get; set; }


        //Get GetAdmissionDetailInfo
        public DataTable GetAdmissionDetailInfo()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetAdmissionInfoMobileChange", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_Regid", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_Collegeid", Collegeid);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsPayFeeSecondYear";
                clsLogger.ExceptionMsg = "GetAdmissionDetailInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        // Change Mobile Number of Student
        public DataTable changeMobile()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("changeMobileCandidate", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MobileNew", MobileNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsPayFeeSecondYear";
                clsLogger.ExceptionMsg = "GetAdmissionDetailInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;
        }

    }
    
}