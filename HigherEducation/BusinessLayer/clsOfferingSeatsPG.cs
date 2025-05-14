using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;

namespace HigherEducation.BusinessLayer
{

    public class clsOfferingSeatsPG
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        public string RollNo { get; set; }
        public string RegId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public DateTime ? BirthDate { get; set; }
        public string Collegeid { get; set; }
        public string FamilyId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string docs { get; set; }
        public string FatherName {get; set;}
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


        //******************************************************clsOfferingSeatsPG PG Start************************************************
        //Get GetAdmissionStatus
        public DataSet GetAdmissionStatus_PG()
        {

            DataSet vds = new DataSet();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateAlloCollegeList_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", RegId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
                if (vds.Tables.Count > 0)
                {
                    return vds;
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsOfferingSeatsPG";
                clsLogger.ExceptionMsg = "GetAdmissionStatus_PG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        


        //Get OfferingSeatsInfo
        public DataTable GetOfferingStudentInfoPG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetOfferingStudentInfo_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsOfferingSeatsPG";
                clsLogger.ExceptionMsg = "GetOfferingStudentInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        //checkseatfreeze
        public DataTable checkseatfreezePG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("checkseatfreeze_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
               
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsOfferingSeatsPG";
                clsLogger.ExceptionMsg = "checkseatfreeze_pg";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        //Get GetCourseSectionDetails
        public DataTable GetCourseSectionDetailsPG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCourseSectionDetails_Open1_pg", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsOfferingSeatsPG";
                clsLogger.ExceptionMsg = "GetCourseSectionDetails";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Get GetCategoryWiseVacantSeat
        public DataTable GetCategoryWiseVacantSeatPG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCategoryWiseVacantSeat_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Courseid", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Sectionid", Sectionid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsOfferingSeatsPG";
                clsLogger.ExceptionMsg = "GetCategoryWiseVacantSeat_PG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }


       
        //GetGetSeatallotment_open1
        public DataTable GetSeatallotment_open1_PG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSeatallotment_open1_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", Collegeid);
              
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsOfferingSeatsPG";
                clsLogger.ExceptionMsg = "GetSeatallotment_open1_PG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Write Connection String here
        public string SaveOfferSeatsPG()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveOfferSeats_PG", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_FatherName", FatherName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MotherName", MotherName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Gender", Gender);
                vadap.SelectCommand.Parameters.AddWithValue("@p_DOB", BirthDate);
                vadap.SelectCommand.Parameters.AddWithValue("@p_AadhaarNo", AadhaarNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ReservCatgid", ReservCatgid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ReservCatgName", ReservCatgName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Catgid", Categoryid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Category", Category);
                vadap.SelectCommand.Parameters.AddWithValue("@p_casteid", Casteid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_caste", Caste);
                vadap.SelectCommand.Parameters.AddWithValue("@p_BoardName", BoardName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RollNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CollegeName", CollegeName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Courseid", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CourseName", CourseName);  
                vadap.SelectCommand.Parameters.AddWithValue("@p_Sectionid", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SectionName", SectionName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SubCombid", 0);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SubCombName", SubCombName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_PMS_SC", PMS_SC);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SeatAllocationCategory", SeatAllocationCategory);
                vadap.SelectCommand.Parameters.AddWithValue("@p_exampassed", exampassed);
                vadap.SelectCommand.Parameters.AddWithValue("@p_TotalPercentage", TotalPercentage);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Weightage", Weightage);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MobileNo", MobileNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_EmailId", Email);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Domicile", Domicile);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_flgDelete", flgDelete);
                vadap.SelectCommand.Parameters.AddWithValue("@p_TwelveHaryana", TwelveHaryana);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                { 
                 
                    result = vds.Tables[0].Rows[0]["Result"].ToString();
                    if (result == "0")
                    {
                        clsLogger.ExceptionError = vds.Tables[0].Rows[0]["errorMsg"].ToString();
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsOfferingSeatsPG";
                        clsLogger.ExceptionMsg = "SaveOfferSeats_PG";
                        clsLogger.SaveException();
                    }
                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsOfferingSeats";
                clsLogger.ExceptionMsg = "SaveOfferSeats";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }


        //******************************************************PG End**************************************************
    }

}