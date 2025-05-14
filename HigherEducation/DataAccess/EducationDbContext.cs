using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using HigherEducation.Models;
using System.Data;
using NLog;
using System.Globalization;
using HigherEducation.BusinessLayer;
using HigherEducation.Controllers;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using PayUIntegration;
using System.Web.Mvc;
using HigherEducation.BAL;

namespace HigherEducation.DataAccess
{
    public class EducationDbContext
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        CandidateDetail objDetails = new CandidateDetail();

        #region ConnectionString
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);

        static readonly string HigherEducationR = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        readonly MySqlConnection connection_readonly = new MySqlConnection(HigherEducationR);

        // string IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        string IPAddress = GetIPAddress();
        string UserId = Convert.ToString(HttpContext.Current.Session["UserId"]);
        #endregion;

        public static string GetIPAddress()
        {
            string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }
        #region INSERT DATA SECTION
        public int Register(RegistrationViewModel objCandidateDetail)
        {
            var result = 0;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("sp_EducationRegistartion", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                DateTime? DateOutput;
                CultureInfo engb = new CultureInfo("en-GB");
                DateOutput = Convert.ToDateTime(objCandidateDetail.Dob.ToString(), engb);

                cmd.Parameters.AddWithValue("P_Reg_Id", objCandidateDetail.RegID);
                cmd.Parameters.AddWithValue("P_TwelveRollNo", objCandidateDetail.TwelveRollNo);
                cmd.Parameters.AddWithValue("P_BoardName", objCandidateDetail.BoardCode);
                cmd.Parameters.AddWithValue("P_PassingYear", objCandidateDetail.PassingOfYear);
                cmd.Parameters.AddWithValue("P_CandidateName", objCandidateDetail.CandidateName);
                cmd.Parameters.AddWithValue("P_Sex", objCandidateDetail.Sex);
                cmd.Parameters.AddWithValue("P_FatherName", objCandidateDetail.FatherHusbandName);
                cmd.Parameters.AddWithValue("P_MotherName", objCandidateDetail.MotherName);
                cmd.Parameters.AddWithValue("P_MobileNo", objCandidateDetail.MobileNo);
                cmd.Parameters.AddWithValue("P_Email", objCandidateDetail.Email);
                cmd.Parameters.AddWithValue("P_BirthDate", DateOutput);
                cmd.Parameters.AddWithValue("P_Password", objCandidateDetail.Password);
                cmd.Parameters.AddWithValue("P_Status", true);
                cmd.Parameters.AddWithValue("P_Ipaddress", objCandidateDetail.IPAddress);
                cmd.Parameters.AddWithValue("P_BrowserName", objCandidateDetail.BrowserName);
                cmd.Parameters.AddWithValue("P_ChecKApi", objCandidateDetail.CheckAPIStatus);
                cmd.Parameters.AddWithValue("P_CreateUser", objCandidateDetail.MobileNo);
                cmd.Parameters.AddWithValue("p_boardcode", objCandidateDetail.Board_code);
                cmd.Parameters.AddWithValue("p_Aadhaar", objCandidateDetail.Aadhaar);
                cmd.Parameters.AddWithValue("p_QualificationCode", objCandidateDetail.QualificationCode);

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                result = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Register()" + objCandidateDetail.RegID);
            }
            connection.Close();
            return result;
        }
        #endregion

        #region update candidate tab data
        public DataTable UpdateCandidateDetailTabData(CandidateDetail candidateDetail)
        {
            DataTable ds = new DataTable();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("sp_Personaldetails_insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                DateTime? DateOutput;
                CultureInfo engb = new CultureInfo("en-GB");
                DateOutput = Convert.ToDateTime(candidateDetail.Dob.ToString(), engb);
                DateTime? DOB_PPP_OutPut = null;
                if (candidateDetail.DOB_PPP.ToString() != "" && candidateDetail.DOB_PPP.ToString() != null)
                {
                    DOB_PPP_OutPut = Convert.ToDateTime(candidateDetail.DOB_PPP.ToString(), engb);
                }
                //CultureInfo provider = CultureInfo.InvariantCulture;
                //DateTime dateTime10; // 1/1/0001 12:00:00 AM  
                //bool isSuccess1 = DateTime.TryParseExact(candidateDetail.BirthDate, "dd/MM/yyyy", provider, DateTimeStyles.None, out dateTime10);
                //DateTime dateTime11 = Convert.ToDateTime(dateTime10);

                cmd.Parameters.AddWithValue("P_P_Id", candidateDetail.PId);
                cmd.Parameters.AddWithValue("P_RegistrationId", candidateDetail.RegID);
                cmd.Parameters.AddWithValue("P_CandidateName", candidateDetail.CandidateName);
                cmd.Parameters.AddWithValue("P_FHName", candidateDetail.FatherHusbandName);
                cmd.Parameters.AddWithValue("P_MotherName", candidateDetail.MotherName);
                cmd.Parameters.AddWithValue("P_BirthDate", DateOutput);
                cmd.Parameters.AddWithValue("P_Sex", candidateDetail.Sex);
                cmd.Parameters.AddWithValue("P_Email", candidateDetail.Email);
                cmd.Parameters.AddWithValue("P_AadhaarNo", candidateDetail.AadharNo);
                cmd.Parameters.AddWithValue("P_MobileNo", candidateDetail.MobileNo);
                cmd.Parameters.AddWithValue("P_Marital_Status", candidateDetail.Marital_Status);
                cmd.Parameters.AddWithValue("P_Father_Occupation", candidateDetail.Father_Occupation);
                cmd.Parameters.AddWithValue("P_Mother_Occupation", candidateDetail.Mother_Occupation);
                cmd.Parameters.AddWithValue("P_Guardian_Name", candidateDetail.Guardian_Name);
                cmd.Parameters.AddWithValue("P_TelephoneNo", candidateDetail.TelephoneNo);
                cmd.Parameters.AddWithValue("P_Guardian_Mobile_No", candidateDetail.GuardianMobileNo);
                cmd.Parameters.AddWithValue("P_Guardian_EmailID", candidateDetail.GuradianEmail);
                cmd.Parameters.AddWithValue("P_Blood_Group", candidateDetail.BloodGroup);
                cmd.Parameters.AddWithValue("P_Religion", candidateDetail.Religion);
                cmd.Parameters.AddWithValue("P_Parental_Income", candidateDetail.ParentalIncome);
                cmd.Parameters.AddWithValue("P_Urban_Rural", candidateDetail.RuralUrban);
                cmd.Parameters.AddWithValue("P_Country_Code", candidateDetail.Country_Code);
                cmd.Parameters.AddWithValue("P_State_Code", candidateDetail.State_Code);
                cmd.Parameters.AddWithValue("P_District_Rural", candidateDetail.District_Code_Rural);
                cmd.Parameters.AddWithValue("P_District_Urban", candidateDetail.District_Code_Urban);
                cmd.Parameters.AddWithValue("P_Municiplity", candidateDetail.Municiplity);
                cmd.Parameters.AddWithValue("P_Block", candidateDetail.Block);
                cmd.Parameters.AddWithValue("P_Village", candidateDetail.CityTownVillage);
                cmd.Parameters.AddWithValue("P_Street_Address_1", candidateDetail.StreetAddress1);
                cmd.Parameters.AddWithValue("P_Street_Address_2", candidateDetail.StreetAddress2);
                cmd.Parameters.AddWithValue("P_Pin_Code", candidateDetail.Pincode);
                cmd.Parameters.AddWithValue("P_Is_Correspondence", candidateDetail.Is_Correspondence);
                cmd.Parameters.AddWithValue("P_C_Urban_Rural", candidateDetail.C_RuralUrban);
                cmd.Parameters.AddWithValue("P_C_Country_Code", candidateDetail.C_Country_Code);
                cmd.Parameters.AddWithValue("P_C_State_Code", candidateDetail.C_State_Code);
                cmd.Parameters.AddWithValue("P_C_District_Rural", candidateDetail.C_District_Code_Rural);
                cmd.Parameters.AddWithValue("P_C_District_Urban", candidateDetail.C_District_Code_Urban);
                cmd.Parameters.AddWithValue("P_C_Municiplity", candidateDetail.C_Municiplity);
                cmd.Parameters.AddWithValue("P_C_Block", candidateDetail.C_Block);
                cmd.Parameters.AddWithValue("P_C_Village", candidateDetail.C_CityTownVillage);
                cmd.Parameters.AddWithValue("P_C_Street_Address_1", candidateDetail.C_StreetAddress1);
                cmd.Parameters.AddWithValue("P_C_Street_Address_2", candidateDetail.C_StreetAddress2);
                cmd.Parameters.AddWithValue("P_C_Pin_Code", candidateDetail.C_Pincode);
                cmd.Parameters.AddWithValue("P_BPL_Card_No", candidateDetail.BPLCardNo);
                cmd.Parameters.AddWithValue("P_Mode_Of_Transport", candidateDetail.ModeOfTransport);
                cmd.Parameters.AddWithValue("P_Hostel", candidateDetail.Hostel);
                cmd.Parameters.AddWithValue("P_Status", candidateDetail.Status);
                cmd.Parameters.AddWithValue("P_ipAddress", candidateDetail.IPAddress);
                cmd.Parameters.AddWithValue("P_BrowserName", candidateDetail.BrowserName);
                cmd.Parameters.AddWithValue("P_Passport", candidateDetail.PassportNo);
                cmd.Parameters.AddWithValue("P_PassportNo", candidateDetail.PassportText);
                cmd.Parameters.AddWithValue("P_Licence", candidateDetail.DrivingLicenceNo);
                cmd.Parameters.AddWithValue("P_LicenceNo", candidateDetail.DrivingLicenceText);
                cmd.Parameters.AddWithValue("P_BPlCategory", candidateDetail.BPLCategory);
                cmd.Parameters.AddWithValue("P_max_page", candidateDetail.Max_page == null ? 1 : candidateDetail.Max_page);
                cmd.Parameters.AddWithValue("P_current_page", candidateDetail.Current_page == null ? 1 : candidateDetail.Current_page);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                cmd.Parameters.AddWithValue("P_NationalyType", candidateDetail.NationalyType);
                cmd.Parameters.AddWithValue("P_N_State_Code", candidateDetail.N_State_Code);
                cmd.Parameters.AddWithValue("P_N_Country_Code", candidateDetail.N_Country_Code);
                cmd.Parameters.AddWithValue("P_HasDomicile", candidateDetail.HaryanaDomicile);
                cmd.Parameters.AddWithValue("P_FamilyID", candidateDetail.FamilyID);
                cmd.Parameters.AddWithValue("P_MemberId", candidateDetail.MemberId);
                cmd.Parameters.AddWithValue("P_ReservationCategory", candidateDetail.ReservationCategory);
                cmd.Parameters.AddWithValue("P_CasteCategory", candidateDetail.CasteCategory);
                cmd.Parameters.AddWithValue("P_Caste", candidateDetail.Caste);
                cmd.Parameters.AddWithValue("P_TwelveHarana", candidateDetail.TwelveHarana);
                cmd.Parameters.AddWithValue("P_IsKashmiriMigrant", candidateDetail.KashmirMigrant);
                cmd.Parameters.AddWithValue("P_IsMinority", candidateDetail.Minority);
                cmd.Parameters.AddWithValue("P_Minority", candidateDetail.MinorityData);
                cmd.Parameters.AddWithValue("P_IsVoterId", candidateDetail.VoterIdCard);
                cmd.Parameters.AddWithValue("P_VoterCardNumber", candidateDetail.VoterCardText);
                cmd.Parameters.AddWithValue("P_IsCasteVerified", candidateDetail.isCasteVerified);
                cmd.Parameters.AddWithValue("P_IsHryResidentVerified", candidateDetail.isResidenceVerified);
                cmd.Parameters.AddWithValue("P_IsPhyHandicapVerified", candidateDetail.isDivyangVerified);
                cmd.Parameters.AddWithValue("P_IsIncomeVerified", candidateDetail.isIncomeVerified);
                cmd.Parameters.AddWithValue("P_Name_PPP", candidateDetail.Name_PPP);
                cmd.Parameters.AddWithValue("P_FHName_PPP", candidateDetail.FHName_PPP);
                cmd.Parameters.AddWithValue("P_DOB_PPP", DOB_PPP_OutPut);
                cmd.Parameters.AddWithValue("P_isNameVerified", candidateDetail.isNameVerified);
                cmd.Parameters.AddWithValue("P_isFnameVerified", candidateDetail.isFnameVerified);
                cmd.Parameters.AddWithValue("P_isDOBVerified", candidateDetail.isDOBVerified);
                cmd.Parameters.AddWithValue("P_DOBVerifiedFrom", candidateDetail.DOBVerifiedFrom);
                cmd.Parameters.AddWithValue("P_CheckAPIStatus", candidateDetail.CheckAPIStatus);
                cmd.Parameters.AddWithValue("P_CasteCategory_PPP", candidateDetail.CasteCategory_PPP);
                cmd.Parameters.AddWithValue("P_subCaste_name_PPP", candidateDetail.subCaste_name_PPP);
                cmd.Parameters.AddWithValue("P_subCaste_code_PPP", candidateDetail.subCaste_code_PPP);
                cmd.Parameters.AddWithValue("P_casteDescription_PPP", candidateDetail.casteDescription_PPP);
                cmd.Parameters.AddWithValue("P_IsITICompleted", candidateDetail.IsITICompleted);
                cmd.Parameters.AddWithValue("P_ITICompletedYear", candidateDetail.ITICompletedYear);
                cmd.Parameters.AddWithValue("P_ITICompletedState", candidateDetail.ITICompletedState);
                cmd.Parameters.AddWithValue("P_ITICompletedName", candidateDetail.ITICompletedName);
                cmd.Parameters.AddWithValue("P_ITICompletedTrade", candidateDetail.ITICompletedTrade);
                cmd.Parameters.AddWithValue("P_ITICompletedRollNo", candidateDetail.ITICompletedRollNo);
                cmd.Parameters.AddWithValue("P_DisableCategory", candidateDetail.DisableCategory);
                cmd.Parameters.AddWithValue("P_Gender_PPP", candidateDetail.Gender_PPP);
                cmd.Parameters.AddWithValue("P_Json_PPP", candidateDetail.Json_PPP);
                //int result = cmd.ExecuteNonQuery();


                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(ds);
                //result = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());

                //if (result > 0)
                //{
                //    return 1;
                //}
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.UpdateCandidateDetailTabData()");
            }
            finally
            {
                connection.Close();
            }

            return ds;
        }



        #endregion


        #region Get RegistrationID
        public LoginTrackModels GetRegistartionId()
        {
            LoginTrackModels objCandidate = new LoginTrackModels();
            try
            {
                string query = "Select Regi_No,PWD from candidate_password order by id desc";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.CommandTimeout = 600;
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    objCandidate.RegID = Convert.ToString(rdr["Regi_No"]);
                    objCandidate.Password = Convert.ToString(rdr["PWD"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetRegistartionId()");
            }
            connection.Close();
            return objCandidate;
        }
        #endregion

        #region get candidate details
        public CandidateDetail GetCandidateDataByRegistrationIdId(string registrationId)
        {
            CandidateDetail objCandidate = new CandidateDetail();
            try
            {
                string query = "GetCandidateRegDetails";
                MySqlCommand cmd = new MySqlCommand(query, connection_readonly);
                cmd.Parameters.AddWithValue("p_RegId", registrationId);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 600;
                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }

                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    objCandidate.PId = Convert.ToInt32(rdr["P_Id"]);
                    objCandidate.TwelveRollNo = Convert.ToString(rdr["Xii_Rollno"]);
                    objCandidate.CandidateName = Convert.ToString(rdr["Candidate_FullName"]);
                    objCandidate.FatherHusbandName = Convert.ToString(rdr["FatherName"]);
                    objCandidate.MotherName = Convert.ToString(rdr["MotherName"]);
                    objCandidate.BirthDate = rdr["BirthDate"] != System.DBNull.Value ? Convert.ToDateTime(rdr["BirthDate"]).ToString("dd/MM/yyyy") : "NA";
                    objCandidate.Sex = Convert.ToInt32(rdr["Sex"]);
                    objCandidate.Email = Convert.ToString(rdr["Email"]);
                    objCandidate.MobileNo = Convert.ToString(rdr["MobileNo"]);
                    objCandidate.BoardCode = Convert.ToString(rdr["Board_Name"]);
                    objCandidate.CheckAPIStatus = Convert.ToString(rdr["check_api_status"]);
                    objCandidate.PassingOfYear = Convert.ToInt32(rdr["Passing_Year"]);
                    objCandidate.Board_code = Convert.ToString(rdr["Board_Code"]);
                    objCandidate.AdhaarNo = Convert.ToString(rdr["AdhaarNo"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateDataByRegistrationIdId()");
            }
            connection_readonly.Close();
            return objCandidate;
        }
        #endregion
        #region get candidate submitted details
        public CandidateDetail GetCandidateDataById(string registrationId)
        {

            CandidateDetail objCandidate = new CandidateDetail();
            try
            {
                string query = "Select * from Candidate where RegistrationId='" + registrationId + "'LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, connection_readonly);
                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                MySqlDataReader rdr = cmd.ExecuteReader();
                cmd.CommandTimeout = 600;
                while (rdr.Read())
                {
                    objCandidate.PId = Convert.ToInt32(rdr["P_Id"]);
                    //objCandidate.CourseId = Convert.ToString(rdr["CourseId"]);              
                    objCandidate.CandidateName = Convert.ToString(rdr["CandidateName"]);
                    objCandidate.FatherHusbandName = Convert.ToString(rdr["FHName"]);
                    objCandidate.MotherName = Convert.ToString(rdr["MotherName"]);
                    objCandidate.BirthDate = rdr["BirthDate"] != System.DBNull.Value ? Convert.ToDateTime(rdr["BirthDate"]).ToString("dd/MM/yyyy") : "NA";
                    // objCandidate.BirthDate = Convert.ToString(rdr["BirthDate"]);
                    objCandidate.Sex = Convert.ToInt32(rdr["Sex"]);
                    objCandidate.Email = Convert.ToString(rdr["Email"]);
                    objCandidate.AadharNo = Convert.ToString(rdr["AadhaarNo"]);
                    objCandidate.MobileNo = Convert.ToString(rdr["MobileNo"]);
                    objCandidate.Marital_Status = Convert.ToInt32(rdr["Marital_Status"]);
                    objCandidate.Father_Occupation = Convert.ToInt32(rdr["Father_Occupation"]);
                    objCandidate.Mother_Occupation = Convert.ToInt32(rdr["Mother_Occupation"]);
                    objCandidate.Guardian_Name = Convert.ToString(rdr["Guardian_Name"]);
                    objCandidate.TelephoneNo = Convert.ToString(rdr["TelephoneNo"]);
                    objCandidate.GuardianMobileNo = Convert.ToString(rdr["Guardian_Mobile_No"]);
                    objCandidate.GuradianEmail = Convert.ToString(rdr["Guardian_EmailID"]);
                    objCandidate.BloodGroup = Convert.ToInt32(rdr["Blood_Group"]);
                    objCandidate.Religion = Convert.ToInt32(rdr["Religion"]);
                    objCandidate.ParentalIncome = Convert.ToInt32(rdr["Parental_Income"]);
                    objCandidate.RuralUrban = rdr["urbanrural"].ToString();
                    objCandidate.State_Code = Convert.ToInt32(rdr["State_Code"]);
                    objCandidate.District_Code_Urban = Convert.ToInt32(rdr["district_urban"]);
                    objCandidate.District_Code_Rural = Convert.ToInt32(rdr["district_rural"]);
                    objCandidate.IsITICompleted = Convert.ToString(rdr["IsITICompleted"]);
                    objCandidate.ITICompletedYear = Convert.ToString(rdr["ITICompletedYear"]);
                    objCandidate.ITICompletedState = Convert.ToString(rdr["ITICompletedState"]);
                    objCandidate.ITICompletedName = Convert.ToString(rdr["ITICompletedName"]);
                    objCandidate.ITICompletedTrade = Convert.ToString(rdr["ITICompletedTrade"]);
                    objCandidate.ITICompletedRollNo = Convert.ToString(rdr["ITICompletedRollNo"]);
                    if (rdr["DisableCategory"] == System.DBNull.Value)
                        objCandidate.DisableCategory = null;
                    else
                        objCandidate.DisableCategory = Convert.ToInt32(rdr["DisableCategory"]);

                    if (rdr["municiplity"] == System.DBNull.Value)
                        objCandidate.Municiplity = 0;
                    else
                        objCandidate.Municiplity = Convert.ToInt32(rdr["municiplity"]);
                    if (rdr["block"] == System.DBNull.Value)
                        objCandidate.Block = 0;
                    else
                        objCandidate.Block = Convert.ToInt32(rdr["block"]);
                    if (rdr["village"] == System.DBNull.Value)
                        objCandidate.CityTownVillage = 0;
                    else
                        objCandidate.CityTownVillage = Convert.ToInt32(rdr["village"]);
                    objCandidate.StreetAddress1 = Convert.ToString(rdr["Street_Address_1"]);
                    objCandidate.StreetAddress2 = Convert.ToString(rdr["Street_Address_2"]);
                    objCandidate.Pincode = Convert.ToString(rdr["Pin_Code"]);
                    objCandidate.Is_Correspondence = Convert.ToBoolean(rdr["Is_Correspondence"]);
                    objCandidate.C_RuralUrban = rdr["c_urbanrural"].ToString();
                    objCandidate.C_State_Code = Convert.ToInt32(rdr["C_State_Code"]);
                    objCandidate.C_District_Code_Urban = Convert.ToInt32(rdr["c_district_urban"]);
                    objCandidate.C_District_Code_Rural = Convert.ToInt32(rdr["c_district_rural"]);
                    if (rdr["c_municiplity"] == System.DBNull.Value)
                        objCandidate.C_Municiplity = 0;
                    else
                        objCandidate.C_Municiplity = Convert.ToInt32(rdr["c_municiplity"]);
                    if (rdr["c_block"] == System.DBNull.Value)
                        objCandidate.C_Block = 0;
                    else
                        objCandidate.C_Block = Convert.ToInt32(rdr["c_block"]);
                    if (rdr["c_village"] == System.DBNull.Value)
                        objCandidate.C_CityTownVillage = 0;
                    else
                        objCandidate.C_CityTownVillage = Convert.ToInt32(rdr["c_village"]);
                    objCandidate.C_StreetAddress1 = Convert.ToString(rdr["C_Street_Address_1"]);
                    objCandidate.C_StreetAddress2 = Convert.ToString(rdr["C_Street_Address_2"]);
                    objCandidate.C_Pincode = Convert.ToString(rdr["C_Pin_Code"]);
                    // objCandidate.Gap_Year = Convert.ToInt32(rdr["Gap_Year"]);
                    objCandidate.Hostel = Convert.ToString(rdr["Hostel"]);
                    objCandidate.ModeOfTransport = Convert.ToString(rdr["Mode_Of_Transport"]);
                    objCandidate.BPLCategory = Convert.ToString(rdr["bpl_category"]);
                    objCandidate.BPLCardNo = Convert.ToString(rdr["BPL_Card_No"]);
                    objCandidate.DrivingLicenceNo = Convert.ToString(rdr["licence"]);
                    objCandidate.DrivingLicenceText = Convert.ToString(rdr["licenceno"]);
                    objCandidate.PassportNo = Convert.ToString(rdr["passport"]);
                    objCandidate.PassportText = Convert.ToString(rdr["passportno"]);
                    objCandidate.IsApplyEducationLoan = Convert.ToString(rdr["is_applyloan"]);
                    objCandidate.IsParticipateActivites = Convert.ToString(rdr["is_participateactivites"]);
                    objCandidate.IsMatricScholarship = Convert.ToString(rdr["is_matricscholorship"]);
                    //change here
                    objCandidate.NationalyType = Convert.ToString(rdr["Nationality"]);
                    objCandidate.N_State_Code = Convert.ToInt32(rdr["N_State_Code"]);
                    objCandidate.N_Country_Code = Convert.ToInt32(rdr["other_countrycode"]);
                    objCandidate.VoterIdCard = Convert.ToString(rdr["Is_VoterId"]);
                    objCandidate.VoterCardText = Convert.ToString(rdr["VoterCardNumber"]);
                    objCandidate.KashmirMigrant = Convert.ToString(rdr["Is_KashmirI_Migrant"]);
                    objCandidate.Minority = Convert.ToString(rdr["Is_Minority"]);
                    objCandidate.MinorityData = Convert.ToInt32(rdr["minority"]);
                    objCandidate.ReservationCategory = Convert.ToInt32(rdr["Reservation_Cat"]);
                    objCandidate.CasteCategory = Convert.ToInt32(rdr["castecategory"]);
                    objCandidate.Caste = Convert.ToInt32(rdr["caste"]);
                    objCandidate.HaryanaDomicile = Convert.ToString(rdr["Has_Domicile"]);
                    objCandidate.FamilyID = rdr["familyidfromppp"] != System.DBNull.Value ? Convert.ToString(rdr["familyidfromppp"]) : "";
                    objCandidate.MemberId = rdr["memberidfromppp"] != System.DBNull.Value ? Convert.ToString(rdr["memberidfromppp"]) : "";
                    objCandidate.TwelveHarana = Convert.ToString(rdr["TwelveHarana"]);
                    objCandidate.isCasteVerified = Convert.ToString(rdr["IsCasteVerified_FromPPP"]);
                    objCandidate.isResidenceVerified = Convert.ToString(rdr["IsHryResidentVerified_FromPPP"]);
                    objCandidate.isDivyangVerified = Convert.ToString(rdr["IsPhyHandicapVerified_FromPPP"]);
                    objCandidate.isIncomeVerified = Convert.ToString(rdr["IsIncomeVerified_FromPPP"]);
                    objCandidate.DOB_PPP = rdr["DOB_PPP"] != System.DBNull.Value ? Convert.ToDateTime(rdr["DOB_PPP"]).ToString("dd/MM/yyyy") : "NA";//Convert.ToString(rdr["DOB_PPP"]);
                    objCandidate.DOBVerifiedFrom = rdr["DOBVerifiedFrom"] != System.DBNull.Value ? Convert.ToString(rdr["DOBVerifiedFrom"]) : "";
                    objCandidate.isCasteCatgMatch_WithPPP = rdr["isCasteCatgMatch_WithPPP"] != System.DBNull.Value ? Convert.ToString(rdr["isCasteCatgMatch_WithPPP"]) : "";
                    objCandidate.CasteCategory_PPP = Convert.ToInt32(rdr["CasteCategory_PPP"]);

                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateDataById()");
            }
            connection_readonly.Close();
            return objCandidate;

        }
        #endregion

        public DataSet GetBlockName(string district_id)
        {
            DataSet ds = new DataSet();
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetBlock";
                cmd.Connection = connection_readonly;
                cmd.Parameters.AddWithValue("P_DistrictCode", district_id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetBlockName()");
            }
            finally
            {
                connection_readonly.Close();
            }
            return ds;
        }
        public DataSet GetVillageCity(string state_id, string district_id, string block_id)
        {
            DataSet ds = new DataSet();
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetViilage";
                cmd.Connection = connection_readonly;
                cmd.Parameters.AddWithValue("P_statecode", state_id);
                cmd.Parameters.AddWithValue("P_districtcode", district_id);
                cmd.Parameters.AddWithValue("P_BlockCode", block_id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetVillageCity()");
            }
            finally
            {
                connection_readonly.Close();
            }
            return ds;
        }
        public DataSet GetMuniciplity(string district_id)
        {
            DataSet ds = new DataSet();
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetMuniciplity";
                cmd.Connection = connection_readonly;
                cmd.Parameters.AddWithValue("P_DistrictCode", district_id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetMuniciplity()");
            }
            finally
            {
                connection_readonly.Close();
            }
            return ds;
        }
        public DataSet CasteCategory_Bind(string reservationCategory_id)
        {
            DataSet ds = new DataSet();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ssp_GetCasteCategory";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("P_ReservationCategoryId", reservationCategory_id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CasteCategory_Bind()");
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        public DataSet CasteCategory_Bind_PPP(string CasteCategoryId)
        {
            DataSet ds = new DataSet();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ssp_GetCasteCategory_PPP";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("P_CasteCategoryId", CasteCategoryId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CasteCategory_Bind_PPP()");
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }
        public DataSet Caste_Bind(string casteCategory_Id)
        {
            DataSet ds = new DataSet();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ssp_GetCaste";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("P_CasteCategoryId", casteCategory_Id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Caste_Bind()");
            }
            finally
            {
                connection.Close();
            }
            return ds;
        }

        public DataSet Tehsil_Bind(string Sub_District_Code)
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand com = new MySqlCommand("Select vtc_name,vtc_id from erp_master_vtc where vtc_status='ACTIVE' and Sub_Districtid=" + Sub_District_Code, connection_readonly);
                com.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(com);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Tehsil_Bind()");
            }
            connection_readonly.Close();
            return ds;
        }
        public DataSet SubDistrict_Bind(string district_id)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand com = new MySqlCommand("Select sub_district_name,sub_district_id from erp_master_sub_district where sub_district_status='ACTIVE' and District_id=" + district_id, connection);
                com.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(com);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SubDistrict_Bind()");
            }
            return ds;
        }


        public DataSet CityTownVillage_Bind(string Tehsil_Code)
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand com = new MySqlCommand("Select VIL_NAME,VIL_CODE from VIL_MAS where TEH_CODE=" + Tehsil_Code, connection_readonly);
                com.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(com);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CityTownVillage_Bind()");
            }
            connection_readonly.Close();
            return ds;
        }
        public int SaveWeightage(WeightageViewModel objWeightage)
        {
            var details = 0;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlDataAdapter sdr = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("SaveWeightage", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            try
            {
                cmd.Parameters.AddWithValue("P_Widow_Ward", objWeightage.Widow_Ward);
                cmd.Parameters.AddWithValue("P_Orphan", objWeightage.Orphan);
                cmd.Parameters.AddWithValue("P_Panch_Weightage", objWeightage.Panch_Weightage);
                cmd.Parameters.AddWithValue("P_Panch_Weightage_Village", objWeightage.Panch_Weightage_Village);
                cmd.Parameters.AddWithValue("P_Reg_Id", objWeightage.Reg_id);
                cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);

                sdr.SelectCommand = cmd;
                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    details = Convert.ToInt32(dt.Rows[0]["result"]);
                }
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveWeightage()");
            }

            if (details >= 1)
            {
                connection.Close();
                return 1;
            }
            connection.Close();
            return 0;
        }

        public int SaveWeightageUpdate(CandidateDetail objWeightage)
        {
            var details = 0;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlDataAdapter sdr = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand("SaveWeightageUpdate", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            try
            {
                cmd.Parameters.AddWithValue("P_IsNationalAward", objWeightage.IsNationalAward);
                cmd.Parameters.AddWithValue("P_IsNccCadet", objWeightage.IsNccCadet);
                cmd.Parameters.AddWithValue("P_IsRuralArea", objWeightage.IsRuralArea);
                cmd.Parameters.AddWithValue("P_Reg_Id", objWeightage.RegID);
                sdr.SelectCommand = cmd;
                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    details = Convert.ToInt32(dt.Rows[0]["result"]);
                }
                // details = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveWeightage()");
            }

            if (details >= 1)
            {
                connection.Close();
                return 1;
            }
            connection.Close();
            return 0;
        }
        //public int SaveEduQualification(AllSubject objAll, CandidateAuditTrail objAudit)
        //{
        //    var details = 0;
        //    connection.Open();
        //    MySqlCommand cmd = new MySqlCommand("SaveEduQualification", connection)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("P_ExamPassed", objAll.ExamPassed);
        //        cmd.Parameters.AddWithValue("P_Optional", objAll.Optional);
        //        cmd.Parameters.AddWithValue("P_UnivBoard", objAll.UnivBoard);
        //        cmd.Parameters.AddWithValue("P_SchoolCollege", objAll.SchoolCollege);
        //        cmd.Parameters.AddWithValue("P_RegistrationRollNo", objAll.RegistrationRollNo);
        //        cmd.Parameters.AddWithValue("P_ResultStatus", objAll.ResultStatus);
        //        cmd.Parameters.AddWithValue("P_PassingYear", objAll.PassingYear);
        //        cmd.Parameters.AddWithValue("P_CGPA", objAll.CGPA);
        //        cmd.Parameters.AddWithValue("P_MarksObt", objAll.MarksObt);
        //        cmd.Parameters.AddWithValue("P_MaxMarks", objAll.MaxMarks);
        //        cmd.Parameters.AddWithValue("P_Subject", objAll.Subject);
        //        cmd.Parameters.AddWithValue("P_Percentage", objAll.Percentage);
        //        cmd.Parameters.AddWithValue("P_Reg_Id", objAll.Reg_Id);
        //        cmd.Parameters.AddWithValue("P_Status", objAudit.Status);
        //        cmd.Parameters.AddWithValue("P_auditStatus", objAudit.Status);
        //        cmd.Parameters.AddWithValue("P_ipaddress", objAudit.ipaddress);
        //        cmd.Parameters.AddWithValue("P_errormsg", objAudit.errormsg);
        //        cmd.Parameters.AddWithValue("P_actiontype", objAudit.actiontype);
        //        cmd.Parameters.AddWithValue("P_FromType", objAudit.FromType);
        //        cmd.Parameters.AddWithValue("P_SrNo", objAudit.SrNo);
        //        details = cmd.ExecuteNonQuery();
        //    }

        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //    }


        //    if (details >= 1)
        //    {
        //        connection.Close();
        //        return 1;
        //    }
        //    connection.Close();
        //    return 0;
        //}
        //public int SaveSubjectDetail(AllSubject objAll, CandidateAuditTrail objAudit)
        //{
        //    var details = 0;
        //    connection.Open();
        //    MySqlCommand cmd = new MySqlCommand("SaveSubjectDetail", connection)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    };
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("P_B_SubjectId", objAll.B_SubjectId);
        //        cmd.Parameters.AddWithValue("P_B_MarksObtained", objAll.B_MarksObtained);
        //        cmd.Parameters.AddWithValue("P_B_MaxMarks", objAll.B_MaxMarks);
        //        cmd.Parameters.AddWithValue("P_CourseType", objAll.CourseType);
        //        cmd.Parameters.AddWithValue("P_Reg_Id", objAll.Reg_Id);
        //        cmd.Parameters.AddWithValue("P_Status", objAudit.Status);
        //        cmd.Parameters.AddWithValue("P_auditStatus", objAudit.Status);
        //        cmd.Parameters.AddWithValue("P_ipaddress", objAudit.ipaddress);
        //        cmd.Parameters.AddWithValue("P_errormsg", objAudit.errormsg);
        //        cmd.Parameters.AddWithValue("P_actiontype", objAudit.actiontype);
        //        cmd.Parameters.AddWithValue("P_FromType", objAudit.FromType);
        //        cmd.Parameters.AddWithValue("P_SrNo", objAudit.SrNo);
        //        details = cmd.ExecuteNonQuery();
        //    }

        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //    }


        //    if (details >= 1)
        //    {
        //        connection.Close();
        //        return 1;
        //    }
        //    connection.Close();
        //    return 0;
        //}
        public FileViewModel GetCandidateDataByRegId(string registrationId)
        {
            FileViewModel objfileUpload = new FileViewModel();
            List<DocumentList> objDocumentlist = new List<DocumentList>();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetdetailsofCandidateRegid", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_RegId", registrationId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < vds.Tables[0].Rows.Count; i++)
                        {
                            objfileUpload.Caste = Convert.ToInt32(vds.Tables[0].Rows[i]["caste"]);
                            objfileUpload.Applicant_Name = Convert.ToString(vds.Tables[0].Rows[i]["Candidate_FullName"]);
                            objfileUpload.Father_Name = Convert.ToString(vds.Tables[0].Rows[i]["FatherName"]);
                            objfileUpload.Mother_Name = Convert.ToString(vds.Tables[0].Rows[i]["MotherName"]);
                            objfileUpload.IsDocExists = Convert.ToInt32(vds.Tables[0].Rows[i]["docid"]);
                        }
                    }
                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        objDocumentlist = (from DataRow row in vds.Tables[1].Rows
                                           select new DocumentList
                                           {
                                               DocumentId = (Convert.ToInt32(row["docid"])),
                                               DocumentName = Convert.ToString(row["DocName"]),
                                               EdishaServiceId = Convert.ToString(row["eDishaServiceCd"]),
                                               DocumentNo = Convert.ToString(row["DocumentNo"]),
                                               IsDocVerify = Convert.ToString(row["IsDocVerify"]),
                                           }).ToList();
                    }
                }
                objfileUpload.documentLists = objDocumentlist;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateDataByRegId()");
            }
            connection.Close();
            return objfileUpload;
        }
        public DataSet District_Bind()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand com = new MySqlCommand("select districtcode,districtname from districtmaster  where statecode=6 ORDER BY districtname", connection_readonly);
                com.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(com);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.District_Bind()");
            }
            connection_readonly.Close();
            return ds;
        }

        public int SaveCourseSection(ChoiceofCourseViewModel objCandiDetails)
        {
            var details = 0;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand("spp_SaveCourseSection", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            try
            {
                cmd.Parameters.AddWithValue("P_Reg_Id", objCandiDetails.RegID);
                cmd.Parameters.AddWithValue("P_State", "6");
                cmd.Parameters.AddWithValue("P_DistrictName", objCandiDetails.Course_District);
                cmd.Parameters.AddWithValue("P_CollegeName", objCandiDetails.Course_College);
                cmd.Parameters.AddWithValue("P_CourseName", objCandiDetails.Course_Course);
                cmd.Parameters.AddWithValue("P_CourseSection", objCandiDetails.Course_CourseSection);
                cmd.Parameters.AddWithValue("P_SubjectName", 0);
                cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);

                cmd.Parameters.Add("P_ColId", MySqlDbType.Int32);
                cmd.Parameters["P_ColId"].Direction = ParameterDirection.Output;

                //  details = cmd.ExecuteNonQuery();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                details = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveCourseSection()");
            }

            if (details == 3)
            {
                connection.Close();
                return 3;

            }
            else if (details == 4)
            {
                connection.Close();
                return 4;

            }
            else if (details == 6)
            {
                connection.Close();
                return 6;

            }
            else if (details >= 1)
            {
                connection.Close();
                return 1;
            }
            connection.Close();
            return 0;
        }

        public DataSet College_Bind(string courseDistrictId, string passStatus, string regid, int collagetype)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand com = new MySqlCommand();
                com.CommandTimeout = 600;
                com.CommandText = "getDistrictwisecollege";
                com.Parameters.AddWithValue("p_Districtid", courseDistrictId);
                com.Parameters.AddWithValue("Pass_status", passStatus);
                com.Parameters.AddWithValue("P_collagetype", collagetype);
                com.Parameters.AddWithValue("Preg_id", regid);
                com.CommandType = CommandType.StoredProcedure;
                if (connection_readonly.State == ConnectionState.Closed)
                    connection_readonly.Open();
                com.Connection = connection_readonly;
                MySqlDataAdapter da = new MySqlDataAdapter(com);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.College_Bind()");
            }
            connection_readonly.Close();
            return ds;
        }
        public DataSet Course_Bind(string coursecollegeid, string qualificationid, string percentage, string physicalid, string da, string age, string mf)
        {
            DataSet ds = new DataSet();
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "getcollegewise";
                cmd.Connection = connection_readonly;
                cmd.Parameters.AddWithValue("p_collageId", coursecollegeid);
                cmd.Parameters.AddWithValue("P_qualificationid", qualificationid);
                cmd.Parameters.AddWithValue("P_PERCENTAGE", percentage);
                cmd.Parameters.AddWithValue("P_physicalid", physicalid);
                cmd.Parameters.AddWithValue("P_Age", age);
                cmd.Parameters.AddWithValue("P_MF", mf);
                cmd.Parameters.AddWithValue("P_da", da);

                MySqlDataAdapter _da = new MySqlDataAdapter(cmd);
                _da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Course_Bind()");
            }
            finally
            {
                connection_readonly.Close();
            }
            return ds;
        }

        public DataSet CourseSection_Bind(string coursecourseid, string collegeid)
        {
            //MySqlCommand com = new MySqlCommand("Select coursesectionid, sectionname from dhe_legacy_coursesection where dhe_legacy_coursesection_status='ACTIVE' and coursesectionid=" + coursecourseid, connection);
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            DataSet ds = new DataSet();
            try
            {
                //MySqlCommand com = new MySqlCommand("Select  coursesectionid, concat(sectionname,' (',concat(concat('Course Fee: &#8377 ',ifnull(c.coursefee,0)) ,', ',concat('Practical Fee: &#8377 ',ifnull(c.practical_fee,0))),')') sectionname  from dhe_legacy_coursesection  cs inner join dhe_legacy_collegecourse c on c.sectionid = cs.coursesectionid and c.courseid = cs.courseid where dhe_legacy_coursesection_status = 'ACTIVE' and  cs.courseid=" + coursecourseid + " and collegeid= " + collegeid, connection);
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CourseSection_Bind";
                cmd.Connection = connection_readonly;
                cmd.Parameters.AddWithValue("P_courseid", coursecourseid);
                cmd.Parameters.AddWithValue("P_collegeid", collegeid);
                cmd.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CourseSection_Bind()");
            }
            connection_readonly.Close();
            return ds;
        }
        public DataSet Subject_Bind(string sectionid, string courseid)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            DataSet ds = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ssp_SubjectCombination";
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("P_SectionID", Convert.ToInt32(sectionid));
                cmd.Parameters.AddWithValue("P_collageid", Convert.ToInt32(courseid));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CourseSection_Bind()");
            }
            connection.Close();
            return ds;
        }

        public List<CandidateDetail> GetCourseSectionDetails(string RegID)
        {
            List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand cmd = new MySqlCommand("GetCourseSectionDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_RegId", RegID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(
                        new CandidateDetail
                        {
                            PId = Convert.ToInt32(dr["id"]),
                            RegID = Convert.ToString(dr["reg_id"]),
                            Course_State = Convert.ToString(dr["statename"]),
                            Course_District = Convert.ToString(dr["districtname"]),
                            Course_College = Convert.ToString(dr["collegename"]),
                            Course_Course = Convert.ToString(dr["name"]),
                            Course_CourseSection = Convert.ToString(dr["sectionname"]),
                            Course_SubjectCombination = Convert.ToString(dr["SubjectCombination"]),
                            TotalFee = Convert.ToString(dr["TotalFee"])

                        }
                        );
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCourseSectionDetails()");
            }
            connection.Close();
            return lst;

        }

        public int DeleteCourseDetails(int id)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("DeleteCourseDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_ID", id);
                var details = cmd.ExecuteNonQuery();
                if (details >= 1)
                {
                    connection.Close();
                    return 1;
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.DeleteCourseDetails()");
            }
            connection.Close();
            return 0;
        }
        #region AbishekCode Merge
        public DataTable getData(string RegID)
        {
            DataTable dt = new DataTable();
            List<CandidateDetail> lst = new List<CandidateDetail>();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetCourseSectionDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_RegId", RegID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.getData()");
            }
            connection.Close();
            return dt;

        }
        public DataTable SavePrefData(string RegID, List<postPerfData> objpostPref)
        {
            DataTable dt = new DataTable();

            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                for (int i = 0; i < objpostPref.Count; i++)
                {
                    MySqlCommand cmd = new MySqlCommand("Save_preferences", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.CommandTimeout = 600;
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("P_courseId", objpostPref[i].pref_course);
                    cmd.Parameters.AddWithValue("P_pref_no", objpostPref[i].pref_no);
                    cmd.Parameters.AddWithValue("P_Reg_Id", RegID);
                    cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                    cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                    da.Fill(dt);
                }

                return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SavePrefData()" + RegID);
            }
            connection.Close();
            return dt;

        }
        #endregion

        #region vishal code merge

        public int Save08THEduQualification(EducationViewModel objAll)
        {
            var details = 0;
            DataTable dt = new DataTable();


            MySqlCommand cmd = new MySqlCommand("SaveEduQualification", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            try
            {
                cmd.Parameters.AddWithValue("P_ExamPassed", objAll.ExamPassed_8th);
                cmd.Parameters.AddWithValue("P_UnivBoard", objAll.Uniboard_8th);
                cmd.Parameters.AddWithValue("P_SchoolCollege", objAll.School_8th);
                cmd.Parameters.AddWithValue("P_RegistrationRollNo", objAll.Rollno_8th);
                cmd.Parameters.AddWithValue("P_ResultStatus", objAll.Result_8th);
                cmd.Parameters.AddWithValue("P_PassingYear", objAll.PassingYear_8th);
                cmd.Parameters.AddWithValue("P_CGPA", objAll.CGPA_8th);
                cmd.Parameters.AddWithValue("P_MarksObt", objAll.MarksObtain_8th);
                cmd.Parameters.AddWithValue("P_MaxMarks", objAll.MaxMarks_8th);
                cmd.Parameters.AddWithValue("P_Percentage", objAll.Percentage_8th);
                cmd.Parameters.AddWithValue("P_Reg_Id", objAll.Reg_Id);
                cmd.Parameters.AddWithValue("P_StreamId", objAll.SelectedStream);
                cmd.Parameters.AddWithValue("P_Gapyear", objAll.Gapyear);
                cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                cmd.Parameters.AddWithValue("P_BestFive_Percentage", null);
                cmd.Parameters.AddWithValue("P_StreamChanged", "N");

                adp.Fill(dt);

                connection.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "1")
                    {
                        return 1;
                    }
                }
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Save08THEduQualification() " + objAll.Reg_Id + "");
            }


            if (details >= 1)
            {
                // connection.Close();
                return 1;
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }
        public int Save10THEduQualification(EducationViewModel objAll)
        {
            var details = 0;
            DataTable dt = new DataTable();


            MySqlCommand cmd = new MySqlCommand("SaveEduQualification", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            try
            {
                cmd.Parameters.AddWithValue("P_ExamPassed", objAll.ExamPassed_10th);
                cmd.Parameters.AddWithValue("P_UnivBoard", objAll.Uniboard_10th);
                cmd.Parameters.AddWithValue("P_SchoolCollege", objAll.School_10th);
                cmd.Parameters.AddWithValue("P_RegistrationRollNo", objAll.Rollno_10th);
                cmd.Parameters.AddWithValue("P_ResultStatus", objAll.Result_10th);
                cmd.Parameters.AddWithValue("P_PassingYear", objAll.PassingYear_10th);
                cmd.Parameters.AddWithValue("P_CGPA", objAll.CGPA_10th);
                cmd.Parameters.AddWithValue("P_MarksObt", objAll.MarksObtain_10th);
                cmd.Parameters.AddWithValue("P_MaxMarks", objAll.MaxMarks_10th);
                cmd.Parameters.AddWithValue("P_Percentage", objAll.Percentage_10th);
                cmd.Parameters.AddWithValue("P_Reg_Id", objAll.Reg_Id);
                cmd.Parameters.AddWithValue("P_StreamId", objAll.SelectedStream);
                cmd.Parameters.AddWithValue("P_Gapyear", objAll.Gapyear);
                cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                cmd.Parameters.AddWithValue("P_BestFive_Percentage", null);
                cmd.Parameters.AddWithValue("P_StreamChanged", "N");

                adp.Fill(dt);

                connection.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "1")
                    {
                        return 1;
                    }
                }
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Save10THEduQualification() " + objAll.Reg_Id + "");
            }


            if (details >= 1)
            {
                return 1;
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }
        public int Save12THEduQualification(EducationViewModel objAll)
        {
            var details = 0;
            DataTable dt = new DataTable();

            MySqlCommand cmd = new MySqlCommand("SaveEduQualification", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            try
            {
                cmd.Parameters.AddWithValue("P_ExamPassed", objAll.ExamPassed_12th);
                cmd.Parameters.AddWithValue("P_UnivBoard", objAll.Uniboard_12th);
                cmd.Parameters.AddWithValue("P_SchoolCollege", objAll.School_12th);
                cmd.Parameters.AddWithValue("P_RegistrationRollNo", objAll.Rollno_12th);
                cmd.Parameters.AddWithValue("P_ResultStatus", objAll.Result_12th);
                cmd.Parameters.AddWithValue("P_PassingYear", objAll.PassingYear_12th);
                cmd.Parameters.AddWithValue("P_CGPA", objAll.CGPA_12th);
                cmd.Parameters.AddWithValue("P_MarksObt", objAll.MarksObtain_12th);
                cmd.Parameters.AddWithValue("P_MaxMarks", objAll.MaxMarks_12th);
                cmd.Parameters.AddWithValue("P_Percentage", objAll.Percentage_12th);
                cmd.Parameters.AddWithValue("P_Reg_Id", objAll.Reg_Id);
                cmd.Parameters.AddWithValue("P_StreamId", objAll.SelectedStream);
                cmd.Parameters.AddWithValue("P_Gapyear", objAll.Gapyear);
                cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                cmd.Parameters.AddWithValue("P_BestFive_Percentage", objAll.BestFive_Percentage);
                cmd.Parameters.AddWithValue("P_StreamChanged", "N");
                adp.Fill(dt);
                connection.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "1")
                    {
                        return 1;
                    }
                }
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Save12THEduQualification() " + objAll.Reg_Id + "");
            }


            if (details >= 1)
            {
                //connection.Close();
                return 1;
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }
        public int SaveDiplomaEduQualification(EducationViewModel objAll)
        {
            var details = 0;
            DataTable dt = new DataTable();

            MySqlCommand cmd = new MySqlCommand("SaveEduQualification", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            try
            {
                cmd.Parameters.AddWithValue("P_ExamPassed", objAll.ExamPassed_Diploma);
                cmd.Parameters.AddWithValue("P_UnivBoard", objAll.Uniboard_Diploma);
                cmd.Parameters.AddWithValue("P_SchoolCollege", objAll.School_Diploma);
                cmd.Parameters.AddWithValue("P_RegistrationRollNo", objAll.Rollno_Diploma);
                cmd.Parameters.AddWithValue("P_ResultStatus", objAll.Result_Diploma);
                cmd.Parameters.AddWithValue("P_PassingYear", objAll.PassingYear_Diploma);
                cmd.Parameters.AddWithValue("P_CGPA", objAll.CGPA_Diploma);
                cmd.Parameters.AddWithValue("P_MarksObt", objAll.MarksObtain_Diploma);
                cmd.Parameters.AddWithValue("P_MaxMarks", objAll.MaxMarks_Diploma);
                cmd.Parameters.AddWithValue("P_Percentage", objAll.Percentage_Diploma);
                cmd.Parameters.AddWithValue("P_Reg_Id", objAll.Reg_Id);
                cmd.Parameters.AddWithValue("P_StreamId", null);
                cmd.Parameters.AddWithValue("P_Gapyear", null);
                cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                cmd.Parameters.AddWithValue("P_BestFive_Percentage", null);
                cmd.Parameters.AddWithValue("P_StreamChanged", "N");
                adp.Fill(dt);
                connection.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "1")
                    {
                        return 1;
                    }
                }
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveDiplomaEduQualification() " + objAll.Reg_Id + "");
            }


            if (details >= 1)
            {
                //connection.Close();
                return 1;
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }
        public int SaveEduQualification12_school(EducationViewModel objAll)
        {
            var details = 0;
            DataTable dt = new DataTable();

            MySqlCommand cmd = new MySqlCommand("SaveEduQualificationschool", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            try
            {
                cmd.Parameters.AddWithValue("P_SchoolCollege", objAll.School_12th);
                cmd.Parameters.AddWithValue("P_Reg_Id", objAll.Reg_Id);
                cmd.Parameters.AddWithValue("P_StreamId", objAll.SelectedStream);
                cmd.Parameters.AddWithValue("P_Gapyear", objAll.Gapyear);
                cmd.Parameters.AddWithValue("P_BestFive_Percentage", objAll.BestFive_Percentage);
                cmd.Parameters.AddWithValue("P_StreamChanged", "N");
                cmd.Parameters.AddWithValue("P_Result", objAll.Result_12th);

                //if (objAll.SelectedStream != objAll.OldSelectedStream && objAll.OldSelectedStream != null)
                //{
                //    cmd.Parameters.AddWithValue("P_StreamChanged", "Y");

                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("P_StreamChanged", "N");
                //}
                //if (connection.State == ConnectionState.Closed)
                //{
                //    connection.Open();
                //}
                adp.Fill(dt);
                connection.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Result"].ToString() == "1")
                    {
                        return 1;
                    }
                }
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveEduQualification() " + objAll.Reg_Id + "");
            }


            if (details >= 1)
            {
                /// connection.Close();
                return 1;
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return 0;
        }

        public int SaveSubjectDetail(EducationViewModel education)
        {
            var details = 0;
            DataTable dt = new DataTable();
            int i = 1;

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                foreach (var objAll in education.subjectDetails)
                {
                    MySqlCommand cmd = new MySqlCommand("SaveSubjectDetail", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.CommandTimeout = 600;
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);

                    cmd.Parameters.AddWithValue("P_Ids", objAll.P_id);
                    cmd.Parameters.AddWithValue("P_B_SubjectId", objAll.SelectedSubjectId);
                    cmd.Parameters.AddWithValue("P_B_MarksObtained", objAll.MarksObtain);
                    cmd.Parameters.AddWithValue("P_B_MaxMarks", objAll.MaxMarks);
                    cmd.Parameters.AddWithValue("P_Reg_Id", education.Reg_Id);
                    cmd.Parameters.AddWithValue("P_B_SubjectNo", i);
                    cmd.Parameters.AddWithValue("P_twelveAPI", education.Rollno_10th);
                    cmd.Parameters.AddWithValue("P_BoardCode", education.Uniboard_10th);
                    cmd.Parameters.AddWithValue("P_schoolName", education.School_10th);
                    cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                    cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                    cmd.Parameters.AddWithValue("P_Year", education.PassingYear_10th);
                    cmd.Parameters.AddWithValue("P_QualificationCode", "10");

                    adp.Fill(dt);
                    i++;
                }
            }

            catch (Exception ex)
            {
                i = 0;
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveSubjectDetail()" + education.Reg_Id);
            }

            if (details >= 1)
            {
                connection.Close();
                return 1;
            }
            connection.Close();
            return i;
        }
        public EducationViewModel GetEduData(string registrationId)
        {
            EducationViewModel objCandidate = new EducationViewModel();
            List<SubjectDetail> ObjsubjectDetails = new List<SubjectDetail>();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetEducationdetails", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("p_reg_id", registrationId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < vds.Tables[0].Rows.Count; i++)
                        {
                            if (vds.Tables[0].Rows[i]["ExamPassed"].ToString().ToLower() == "8th")
                            {

                                objCandidate.ExamPassed_8th = vds.Tables[0].Rows[i]["ExamPassed"].ToString();
                                objCandidate.Uniboard_8th = (vds.Tables[0].Rows[i]["UnivBoard"].ToString());
                                objCandidate.School_8th = vds.Tables[0].Rows[i]["SchoolCollege"].ToString();
                                objCandidate.Rollno_8th = vds.Tables[0].Rows[i]["RegistrationRollNo"].ToString();
                                objCandidate.Result_8th = vds.Tables[0].Rows[i]["ResultStatus"].ToString();
                                objCandidate.PassingYear_8th = vds.Tables[0].Rows[i]["PassingYear"].ToString();
                                objCandidate.CGPA_8th = Convert.ToBoolean(vds.Tables[0].Rows[i]["CGPA"].ToString());
                                objCandidate.MarksObtain_8th = Convert.ToDecimal(vds.Tables[0].Rows[i]["MarksObt"].ToString());
                                objCandidate.MaxMarks_8th = Convert.ToInt32(vds.Tables[0].Rows[i]["MaxMarks"].ToString());
                                objCandidate.Percentage_8th = Convert.ToDecimal(vds.Tables[0].Rows[i]["Percentage"].ToString());
                                objCandidate.Gapyear = Convert.ToString(vds.Tables[0].Rows[i]["GapYear"].ToString());
                                objCandidate.SelectedStream = (vds.Tables[0].Rows[i]["Stream"].ToString());
                                objCandidate.OldSelectedStream = (vds.Tables[0].Rows[i]["Stream"].ToString());
                                objCandidate.IsFromApi_8th = (vds.Tables[0].Rows[i]["IsFromApi"].ToString());

                            }
                            if (vds.Tables[0].Rows[i]["ExamPassed"].ToString().ToLower() == "10th")
                            {

                                objCandidate.ExamPassed_10th = vds.Tables[0].Rows[i]["ExamPassed"].ToString();
                                objCandidate.Uniboard_10th = (vds.Tables[0].Rows[i]["UnivBoard"].ToString());
                                objCandidate.School_10th = vds.Tables[0].Rows[i]["SchoolCollege"].ToString();
                                objCandidate.Rollno_10th = vds.Tables[0].Rows[i]["RegistrationRollNo"].ToString();
                                objCandidate.Result_10th = vds.Tables[0].Rows[i]["ResultStatus"].ToString();
                                objCandidate.PassingYear_10th = vds.Tables[0].Rows[i]["PassingYear"].ToString();
                                objCandidate.CGPA_10th = Convert.ToBoolean(vds.Tables[0].Rows[i]["CGPA"].ToString());
                                objCandidate.MarksObtain_10th = Convert.ToDecimal(vds.Tables[0].Rows[i]["MarksObt"].ToString());
                                objCandidate.MaxMarks_10th = Convert.ToInt32(vds.Tables[0].Rows[i]["MaxMarks"].ToString());
                                objCandidate.Percentage_10th = Convert.ToDecimal(vds.Tables[0].Rows[i]["Percentage"].ToString());
                                objCandidate.Gapyear = Convert.ToString(vds.Tables[0].Rows[i]["GapYear"].ToString());
                                objCandidate.SelectedStream = (vds.Tables[0].Rows[i]["Stream"].ToString());
                                objCandidate.OldSelectedStream = (vds.Tables[0].Rows[i]["Stream"].ToString());
                                objCandidate.IsFromApi_10th = (vds.Tables[0].Rows[i]["IsFromApi"].ToString());

                            }
                            else if (vds.Tables[0].Rows[i]["ExamPassed"].ToString().ToLower() == "12th")
                            {
                                objCandidate.ExamPassed_12th = vds.Tables[0].Rows[i]["ExamPassed"].ToString();
                                objCandidate.Uniboard_12th = (vds.Tables[0].Rows[i]["UnivBoard"].ToString());
                                objCandidate.School_12th = vds.Tables[0].Rows[i]["SchoolCollege"].ToString();
                                objCandidate.Rollno_12th = vds.Tables[0].Rows[i]["RegistrationRollNo"].ToString();
                                objCandidate.Result_12th = vds.Tables[0].Rows[i]["ResultStatus"].ToString();
                                objCandidate.PassingYear_12th = vds.Tables[0].Rows[i]["PassingYear"].ToString();
                                objCandidate.CGPA_12th = Convert.ToBoolean(vds.Tables[0].Rows[i]["CGPA"].ToString());
                                objCandidate.MarksObtain_12th = Convert.ToInt32(vds.Tables[0].Rows[i]["MarksObt"].ToString());
                                objCandidate.MaxMarks_12th = Convert.ToInt32(vds.Tables[0].Rows[i]["MaxMarks"].ToString());
                                objCandidate.Percentage_12th = Convert.ToDecimal(vds.Tables[0].Rows[i]["Percentage"].ToString());
                                objCandidate.Gapyear = Convert.ToString(vds.Tables[0].Rows[i]["GapYear"].ToString());
                                objCandidate.SelectedStream = (vds.Tables[0].Rows[i]["Stream"].ToString());
                                objCandidate.OldSelectedStream = (vds.Tables[0].Rows[i]["Stream"].ToString());
                                objCandidate.IsFromApi_12th = (vds.Tables[0].Rows[i]["IsFromApi"].ToString());
                            }
                            else if (vds.Tables[0].Rows[i]["ExamPassed"].ToString().ToLower() == "graduation")
                            {
                                objCandidate.ExamPassed_Diploma = vds.Tables[0].Rows[i]["ExamPassed"].ToString();
                                objCandidate.Uniboard_Diploma = (vds.Tables[0].Rows[i]["UnivBoard"].ToString());
                                objCandidate.School_Diploma = vds.Tables[0].Rows[i]["SchoolCollege"].ToString();
                                objCandidate.Rollno_Diploma = vds.Tables[0].Rows[i]["RegistrationRollNo"].ToString();
                                objCandidate.Result_Diploma = vds.Tables[0].Rows[i]["ResultStatus"].ToString();
                                objCandidate.PassingYear_Diploma = vds.Tables[0].Rows[i]["PassingYear"].ToString();
                                objCandidate.CGPA_Diploma = Convert.ToBoolean(vds.Tables[0].Rows[i]["CGPA"].ToString());
                                objCandidate.MarksObtain_Diploma = Convert.ToInt32(vds.Tables[0].Rows[i]["MarksObt"].ToString());
                                objCandidate.MaxMarks_Diploma = Convert.ToInt32(vds.Tables[0].Rows[i]["MaxMarks"].ToString());
                                objCandidate.Percentage_Diploma = Convert.ToDecimal(vds.Tables[0].Rows[i]["Percentage"].ToString());

                            }
                        }

                    }

                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        ObjsubjectDetails = (from DataRow row in vds.Tables[1].Rows
                                             select new SubjectDetail
                                             {
                                                 SubjectList = GetAllSubject(Convert.ToInt32(row["SubjectId"]), Convert.ToInt32(vds.Tables[2].Rows[0]["Board_code"])),
                                                 P_id = Convert.ToInt32(row["P_Id"]),
                                                 SelectedSubjectId = Convert.ToInt32(row["SubjectId"]),
                                                 MarksObtain = Convert.ToString(row["MarksObt"]),
                                                 MaxMarks = Convert.ToString(row["MaxMarks"])
                                             }).ToList();

                        //decimal MarksObtain = ObjsubjectDetails.Sum(item => Convert.ToInt32(item.MarksObtain));
                        //decimal MaxMarks = ObjsubjectDetails.Sum(item => Convert.ToInt32(item.MaxMarks));
                        //decimal Best5Pec = Math.Round((MarksObtain / MaxMarks) * 100, 2);
                        //ObjsubjectDetails.ToList().ForEach(s => s.Best5Percentage = Best5Pec);    // update value of existing item in list
                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            ObjsubjectDetails.Add(new SubjectDetail
                            {
                                SubjectList = GetAllSubject(0, Convert.ToInt32(vds.Tables[2].Rows[0]["Board_code"])),
                                P_id = 0,
                                SelectedSubjectId = 0,
                                MarksObtain = "",
                                MaxMarks = ""
                            });
                        }
                    }

                    if (vds.Tables[1].Rows.Count < 3 && vds.Tables[1].Rows.Count > 0)
                    {
                        var looplength = 3 - vds.Tables[1].Rows.Count;
                        for (int i = 0; i < looplength; i++)
                        {
                            ObjsubjectDetails.Add(new SubjectDetail
                            {
                                SubjectList = GetAllSubject(0, Convert.ToInt32(vds.Tables[2].Rows[0]["Board_code"])),
                                P_id = 0,
                                SelectedSubjectId = 0,
                                MarksObtain = "",
                                MaxMarks = ""
                            });
                        }
                    }


                    if (vds.Tables[2].Rows.Count > 0)
                    {
                        objCandidate.DOB_Year = Convert.ToInt32(vds.Tables[2].Rows[0]["BirthYear"].ToString());
                        objCandidate.QualificationCode = Convert.ToString(vds.Tables[2].Rows[0]["QualificationCode"].ToString());
                        objCandidate.SelectedBoard = Convert.ToString(vds.Tables[2].Rows[0]["Board_code"].ToString());


                    }
                }

                objCandidate.subjectDetails = ObjsubjectDetails;

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetEduData()" + registrationId);
            }
            connection.Close();
            return objCandidate;
        }
        public List<SelectListItem> GetAllSubject(int selectedid, int boardcode)
        {
            GetDataInfo objDetails = new GetDataInfo();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objDetails.GetAllSubject(boardcode))
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = item.Id == selectedid ? true : false
                });
            }
            return items;
        }
        public AllSubject GetSubjectDataByAPI(string registrationId, int subjectno, int boardno, Int64 rollno)
        {
            AllSubject objCandidate = new AllSubject();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                string query = "Select distinct * from reg_subjectdetail where RollNumber='" + rollno + "' and board='" + boardno + "' and SubjectNo='" + subjectno + "' and Reg_Id='" + registrationId + "' and subjectid<>0  LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.CommandTimeout = 600;
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    objCandidate.B_P_Id = Convert.ToInt32(rdr["P_Id"].ToString());
                    objCandidate.B_SubjectId = Convert.ToInt32(rdr["SubjectId"].ToString());
                    objCandidate.B_MarksObtained = Convert.ToInt32(rdr["MarksObt"].ToString());
                    objCandidate.B_MaxMarks = Convert.ToInt32(rdr["MaxMarks"].ToString());
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetSubjectDataByAPI() " + registrationId + "");
            }
            connection.Close();
            return objCandidate;
        }
        public List<DeclarationTab> GetDeclarationData(string registrationId)
        {
            List<DeclarationTab> objdeclare = new List<DeclarationTab>();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetDeclarationData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@P_RegId", registrationId);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    objdeclare.Add(new DeclarationTab
                    {
                        PId = Convert.ToInt32(rdr["P_Id"].ToString()),
                        CandidateName = Convert.ToString(rdr["CandidateName"]),
                        FatherHusbandName = Convert.ToString(rdr["FHName"]),
                        MotherName = Convert.ToString(rdr["MotherName"]),
                        BirthDate = rdr["BirthDate"] != System.DBNull.Value ? Convert.ToDateTime(rdr["BirthDate"]).ToString("dd/MM/yyyy") : "NA",
                        Sex = Convert.ToString(rdr["Sex"]),
                        Email = Convert.ToString(rdr["Email"]),
                        AadharNo = Convert.ToString(rdr["AadhaarNo"]),
                        MobileNo = Convert.ToString(rdr["MobileNo"]),
                        MaritalStatus = Convert.ToString(rdr["Marital_Status"]),
                        FatherOccupation = Convert.ToString(rdr["Father_Occupation"]),
                        MotherOccupation = Convert.ToString(rdr["Mother_Occupation"]),
                        Guardian_Name = rdr["Guardian_Name"] != System.DBNull.Value ? Convert.ToString(rdr["Guardian_Name"]) : "NA",
                        TelephoneNo = Convert.ToString(rdr["TelephoneNo"]),
                        GuardianMobileNo = Convert.ToString(rdr["Guardian_Mobile_No"]),
                        GuradianEmail = Convert.ToString(rdr["Guardian_EmailID"]),
                        BloodGroup = Convert.ToString(rdr["blood_group_name"]),
                        Religion = Convert.ToString(rdr["religion_name"]),
                        ParentalIncome = Convert.ToString(rdr["Parental_Income"]),
                        //base64Image = rdr["canimage"].ToString(),
                        //base64Sign = rdr["cansign"].ToString(),
                        //UserImage = Convert.ToString(rdr["canimage"]),
                        //UserSign = Convert.ToString(rdr["cansign"]),
                        StreetAddress2 = Convert.ToString(rdr["Street_Address_2"]) + " " + Convert.ToString(rdr["Street_Address_1"]),
                        Pincode = Convert.ToString(rdr["Pin_Code"]),
                        Country_Code = Convert.ToString(rdr["country_name"]),
                        state = Convert.ToString(rdr["statename"]),
                        district_rural = Convert.ToString(rdr["district_rural"]),
                        district_urban = Convert.ToString(rdr["district_urban"]),
                        CityTownVillage = Convert.ToString(rdr["villagename"]),
                        ModeOfTransport = Convert.ToString(rdr["Mode_Of_Transport"]),
                        BPLCardNo = Convert.ToString(rdr["BPL_Card_No"]),
                        Gap_Year = Convert.ToInt32(rdr["Gap_Year"]),
                        Hostel = Convert.ToString(rdr["Hostel"]),
                        Caste = Convert.ToString(rdr["Caste"]),
                        NationalyType = Convert.ToString(rdr["Nationality"]),
                        ReservationCategory = Convert.ToString(rdr["reservationname"]),
                        HaryanaDomicile = Convert.ToString(rdr["Has_Domicile"]),
                        RegID = Convert.ToString(rdr["Reg_Id"]),

                    });
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetDeclarationData()");
            }
            connection.Close();
            return objdeclare;
        }
        public List<UploadDocs> GetUploadData(string registrationId)
        {
            List<UploadDocs> objdeclare = new List<UploadDocs>();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetUploadData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@P_RegId", registrationId);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    objdeclare.Add(new UploadDocs
                    {
                        PId = Convert.ToInt32(rdr["P_Id"].ToString()),
                        DocsName = Convert.ToString(rdr["DocsName"]),
                        Docs = Convert.ToString(rdr["Docs"]),
                        FormId = Convert.ToInt32(rdr["DocId"]),
                    });
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetUploadData()");
            }
            connection.Close();
            return objdeclare;
        }
        public List<CandidateDetail> GetWeightage(string registrationId)
        {
            List<CandidateDetail> objdeclare = new List<CandidateDetail>();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetWeightage", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@P_RegId", registrationId);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    objdeclare.Add(new CandidateDetail
                    {
                        PId = Convert.ToInt32(rdr["P_Id"].ToString()),
                        IsNationalAward = Convert.ToString(rdr["IsNationalAward"]),
                        IsNccCadet = Convert.ToString(rdr["IsNccCadet"]),
                        IsRuralArea = Convert.ToString(rdr["IsRuralArea"]),
                    });
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetWeightage()");
            }
            connection.Close();
            return objdeclare;
        }
        public int UpdateRegistrationId(string registrationId, Int64 twelverollno)
        {
            var details = 0;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand("UpdateRegistration", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            try
            {
                cmd.Parameters.AddWithValue("P_RegId", registrationId);
                cmd.Parameters.AddWithValue("P_Rollno", twelverollno);
                cmd.Parameters.AddWithValue("P_IPAddress", IPAddress);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                details = cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.UpdateRegistrationId()");
            }
            if (details >= 1)
            {
                connection.Close();
                return 1;
            }
            connection.Close();
            return 0;
        }
        public string GetApiValue(string registrationId)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            string Api = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetApiValue", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@P_RegId", registrationId);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Api = Convert.ToString(rdr["check_api_status"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetApiValue()");
            }
            connection.Close();
            return Api;
        }
        public eduData GetAPiData(string registrationId, Int64 rollNo)
        {
            eduData objedu = new eduData();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetApiData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@P_RegId", registrationId);
                cmd.Parameters.AddWithValue("@P_RollNumber", rollNo);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    objedu.Boardno = Convert.ToInt32(rdr["Board"].ToString());
                    objedu.Rollno = Convert.ToInt64(rdr["RollNumber"].ToString());
                    objedu.schoolName = Convert.ToString(rdr["SchoolName"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetAPiData()");
            }
            connection.Close();
            return objedu;
        }
        public string GetUnivName(int univId)
        {

            string univName = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetUniversityName", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@P_UnivId", univId);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    univName = Convert.ToString(rdr["university_name"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetUnivName()");
            }
            connection.Close();
            return univName;
        }
        #endregion
        #region
        public string SendOTP(string Mobileno)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                Random r = new Random();
                string rndnumber = r.Next(1111, 9999).ToString();
                string sms_details = "Your ITI application OTP Number is " + rndnumber;



                DateTime Currenttime = DateTime.Now;
                Currenttime = Currenttime.AddMinutes(5);
                HttpContext.Current.Session["OtpValidTime"] = Currenttime;

                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveOTPDetails", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@mobile_no", Mobileno);
                vadap.SelectCommand.Parameters.AddWithValue("@otp", rndnumber);
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@create_user", Mobileno);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                if (vds.Tables.Count > 0)
                {
                    AgriSMS.sendSingleSMS(Mobileno, sms_details, "1007030482147904866");
                    return "1";
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SendOTP()");
            }
            return "0";
        }
        public string SendRegistrationMessage(string Mobileno, string regID, string password, string Emailid)
        {

            try
            {
                string strIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                //string sms_details = "Registration Successful. Your registration id is  " + regID + "  and password is  " + password;
                string sms_details = "Registration Successful for admission in ITI. Your registration id is  " + regID + "  and password is  " + password + " Regards, SDIT Haryana";
                AgriSMS.sendSingleSMS(Mobileno, sms_details, "1007330480836285522");
                SMS.SendEmail(Emailid, "Registration Successful", sms_details);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SendRegistrationMessage()");
            }
            return "0";
        }
        public string CheckOTP(string Mobileno, string OTP)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                string result = "";
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("CheckOTPDetails", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@mobile_no", Mobileno);
                vadap.SelectCommand.Parameters.AddWithValue("@_otp", OTP);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();

                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["validotp"].ToString();
                }
                // connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CheckOTP()");
            }
            connection.Close();
            return "0";
        }
        public int SaveDocument(Document objAll)
        {
            var details = 0;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand("SaveDocument", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.CommandTimeout = 600;
            try
            {
                cmd.Parameters.AddWithValue("P_Docs", objAll.Docs);
                cmd.Parameters.AddWithValue("P_DocsName", objAll.Docid);
                cmd.Parameters.AddWithValue("P_Reg_Id", objAll.Reg_Id);
                cmd.Parameters.AddWithValue("P_ipaddress", IPAddress);
                cmd.Parameters.AddWithValue("P_docid", objAll.Docid);
                cmd.Parameters.AddWithValue("P_docno", objAll.DocNo);
                cmd.Parameters.AddWithValue("P_Isverify", objAll.Isverify);
                cmd.Parameters.AddWithValue("P_CreateUser", UserId);
                cmd.Parameters.AddWithValue("P_IsApi", objAll.IsApi);
                //details = cmd.ExecuteNonQuery();
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    details = Convert.ToInt32(ds.Rows[0][0].ToString());
                }
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveDocument()" + objAll.Reg_Id);
            }
            connection.Close();
            return details;
        }

        //public List<DocGet> GetUploadDoc(string registrationId)
        //{
        //    List<DocGet> objdeclare = new List<DocGet>();
        //    if (connection_readonly.State == ConnectionState.Closed)
        //    {
        //        connection_readonly.Open();
        //    }
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("getVerifieddoc", connection_readonly)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.CommandTimeout = 600;
        //        cmd.Parameters.AddWithValue("@p_reg_id", registrationId);
        //        MySqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            objdeclare.Add(new DocGet
        //            {
        //                IsVerify = Convert.ToString(rdr["IsDocVerify"]),
        //                DocumentNo = Convert.ToString(rdr["DocumentNo"]),
        //                Docid = Convert.ToString(rdr["DocId"]),
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetUploadDoc()");
        //    }
        //    connection_readonly.Close();
        //    return objdeclare;
        //}

        public DataSet DistrictP_Bind(string state_id)
        {
            DataSet ds = new DataSet();
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            try
            {
                MySqlCommand com = new MySqlCommand("select districtcode,districtname from districtmaster  where statecode= '" + state_id + "' ", connection_readonly);
                com.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(com);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.DistrictP_Bind()");
            }
            connection_readonly.Close();
            return ds;
        }

        #region get weightage details
        public WeightageViewModel GetWeightageDetail(string registrationId)
        {
            WeightageViewModel objweightage = new WeightageViewModel();
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("getcandidatedetailsforWeitage", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@p_regid", registrationId);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    objweightage.Widow_Ward = Convert.ToString(rdr["Widow_Ward"]);
                    objweightage.Orphan = Convert.ToString(rdr["Orphan"]);
                    objweightage.Panch_Weightage = Convert.ToString(rdr["Panch_Weightage"]);
                    objweightage.Panch_Weightage_Village = Convert.ToString(rdr["Panch_Weightage_Village"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetWeightageDetail()");
            }
            connection_readonly.Close();
            return objweightage;
        }
        #endregion
        #endregion
        public DataTable SaveUserPageDeclaration(string RegID, string Mobile, string Email)
        {
            DataTable dt = new DataTable();
            try
            {
                // string strIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand cmd1 = new MySqlCommand("saveuserFormprogress", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.CommandTimeout = 600;
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                cmd1.Parameters.AddWithValue("P_max_page", Convert.ToInt32(HttpContext.Current.Session["MaxPage"]));
                cmd1.Parameters.AddWithValue("P_current_page", Convert.ToInt32(HttpContext.Current.Session["currentPage"]));
                cmd1.Parameters.AddWithValue("P_Reg_Id", RegID);
                cmd1.Parameters.AddWithValue("P_IPAddress", IPAddress);
                cmd1.Parameters.AddWithValue("P_CreateUser", UserId);
                da1.Fill(dt);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["success"].ToString() == "1")
                    {
                        AgriSMS.sendSingleSMS(Mobile, "Your form vide Registration Id " + RegID + " has been submitted successfully! Regards, SDIT Haryana", "1007709846009482854");
                        SMS.SendEmail(Email, "Form submitted", "Your form vide Registration Id " + RegID + " has been submitted successfully!" + Environment.NewLine + "Regards");
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveUserPageDeclaration()");
            }
            return dt;

        }

        public string GetMobile(string Mobileno)
        {

            string mobileno = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("CheckMobileNumber", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_MobileNo", Mobileno);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    mobileno = Convert.ToString(rdr["MobileNo"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetMobile()");
            }
            connection.Close();
            return mobileno;
        }

        public string GetEmail(string Email)
        {

            string email = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("CheckEmail", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Email", Email);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    email = Convert.ToString(rdr["Email"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetEmail()");
            }
            connection.Close();
            return email;
        }
        public string GetRegId(string rollno, string passingyear)
        {

            string regid = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("CheckRegNumber", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_RollNo", rollno);
                cmd.Parameters.AddWithValue("P_Passingyear", passingyear);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    regid = Convert.ToString(rdr["Reg_Id"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetRegId()");
            }
            connection.Close();
            return regid;
        }

        public DataSet getDeclarationDataRAJ(string RegID, string UserID = null)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand cmd = new MySqlCommand("RSGetDecData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_Id", RegID);
                if (UserID == null)
                {
                    cmd.Parameters.AddWithValue("P_UserID", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("P_UserID", UserID);
                }
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.getDeclarationDataRAJ(),'" + RegID + "'");
            }
            connection.Close();
            return dt;

        }

        public DataTable CheckSubComMin(string RegID)
        {
            DataTable dt = new DataTable();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("CheckMinSubComb", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_RegId", RegID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CheckSubComMin()");
            }
            connection.Close();
            return dt;

        }

        public clsDGLocker GetEduSavedData(string RID, string EXAM)
        {
            clsDGLocker objedu = new clsDGLocker();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetEduSavedData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@P_Rid", RID);
                cmd.Parameters.AddWithValue("@P_Exampassed", EXAM);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    objedu.Board = Convert.ToString(rdr["UnivBoard"].ToString());
                    objedu.RollNo = Convert.ToString(rdr["RegistrationRollno"]);
                    objedu.Year = Convert.ToString(rdr["PassingYear"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Getmarks()");
            }
            connection.Close();
            return objedu;
        }
        public eduData Getmarks(Int64 twelverollno)
        {
            eduData objedu = new eduData();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetAPI12thTotalMarks", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@P_RollNumber", twelverollno);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    objedu.ApiMaxMarks = Convert.ToString(rdr["ApimaxMarks"].ToString());
                    objedu.APiMaxobt = Convert.ToString(rdr["ApiMarksObt"]);
                    objedu.ApiPassStatus = Convert.ToString(rdr["ApiPassStatus"]);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.Getmarks()");
            }
            connection.Close();
            return objedu;
        }

        public DataTable GetDOB(string regId)
        {

            DataTable vds = new DataTable();

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {

                MySqlDataAdapter vadap = new MySqlDataAdapter("GetDOB", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_regid", regId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetDOB()");
            }
            connection.Close();
            return vds;
        }

        public string SendOTPUnlock(string regId)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                Random r = new Random();
                string rndnumber = r.Next(1111, 9999).ToString();
                // string strIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                //string sms_details = "OTP to unlock register on Online Admission Portal is " + rndnumber;
                string sms_details = "OTP to unlock on Online Admission Portal for ITI is " + rndnumber + " Regards, SDIT Haryana";

                //DateTime Currenttime = DateTime.Now;
                //Currenttime = Currenttime.AddMinutes(5);
                //HttpContext.Current.Session["OtpValidTime"] = Currenttime;

                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveUnlockOTPDetails", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@otp", rndnumber);
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@create_user", regId);
                vadap.SelectCommand.Parameters.AddWithValue("@reg_id", regId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        result = vds.Tables[0].Rows[0]["issuccess"].ToString();
                        if (result == "1")
                        {
                            string mobileno = vds.Tables[0].Rows[0]["mobileno"].ToString();
                            string EmailId = vds.Tables[0].Rows[0]["emailid"].ToString();

                            AgriSMS.sendSingleSMS(mobileno, sms_details, "1007222238653939276");
                            SMS.SendEmail(EmailId, "OTP to Unlock Form", sms_details);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SendOTPUnlock()");
            }
            return result;
        }

        public string CheckOTPUnlock(string OTP, string regId)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("CheckOTPUnlockDetails", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@_regId", regId);
                vadap.SelectCommand.Parameters.AddWithValue("@_otp", OTP);
                vadap.SelectCommand.Parameters.AddWithValue("@P_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@P_CreateUser", UserId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();

                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["validotp"].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CheckOTPUnlock()");
            }
            connection.Close();
            return result;
        }

        public string CheckOTPUnlockFee(string OTP, string regId)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("CheckOTPUnlockDetailsFee", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@_regId", regId);
                vadap.SelectCommand.Parameters.AddWithValue("@_otp", OTP);
                vadap.SelectCommand.Parameters.AddWithValue("@P_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@P_CreateUser", UserId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();

                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["validotp"].ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CheckOTPUnlock()");
            }
            connection.Close();
            return result;
        }

        public DataTable GetCandidateVerifyObjection(string regId)
        {
            DataTable vds = new DataTable();


            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateVerifyObjRemarks", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@_regId", regId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateVerifyObjection()");
            }
            connection.Close();
            return vds;
        }
        //added 05/09/2020 begin
        public DataTable GetValidationResult(string RegID)
        {
            DataTable dt = new DataTable();
            List<CandidateDetail> lst = new List<CandidateDetail>();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("RSVaildateForVerification", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("PRegistrationId", RegID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetValidationResult()");
            }
            connection.Close();
            return dt;

        }
        //added on 05/09/2020

        public FeeModule GetCandidateFeeDetail(string Counselling, string registrationid, string collegeid)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateFeeDetail", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("P_counselling", Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", collegeid);



                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {

                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["registrationID"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.billing_address = Convert.ToString(vds.Tables[0].Rows[0]["section"]);
                        objFeepaid.billing_name = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.merchant_param2 = Convert.ToString(vds.Tables[0].Rows[0]["coursesession"]);
                        objFeepaid.merchant_param3 = Convert.ToString(vds.Tables[0].Rows[0]["collegeid"]);
                        objFeepaid.merchant_param4 = Convert.ToString(vds.Tables[0].Rows[0]["college_course_id"]);
                        objFeepaid.billing_tel = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.billing_email = Convert.ToString(vds.Tables[0].Rows[0]["emailid"]);

                        objCandidateFee = (from DataRow row in vds.Tables[0].Rows
                                           select new CandidateFee
                                           {
                                               FeeSubHeadName = Convert.ToString(row["fee_subhead_name"]),
                                               FeeAmountYearly = Convert.ToString(row["AdmissionFee"])
                                           }).ToList();
                        var totalcoursefee = objCandidateFee.Sum(item => Convert.ToInt32(item.FeeAmountYearly));

                        objFeepaid.TotalFee = (totalcoursefee);
                        objFeepaid.CandidateFee = objCandidateFee;
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateFeeDetail()" + registrationid);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }

        public FeeModule GetCandidateFeeDetailQ(string registrationid, string collegeid, string QNo)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateFeeDetailForQuarterlyFee", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", collegeid);



                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {

                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["registrationID"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.billing_address = Convert.ToString(vds.Tables[0].Rows[0]["section"]);
                        objFeepaid.billing_name = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.merchant_param2 = Convert.ToString(vds.Tables[0].Rows[0]["coursesession"]);
                        objFeepaid.merchant_param3 = Convert.ToString(vds.Tables[0].Rows[0]["collegeid"]);
                        objFeepaid.merchant_param4 = Convert.ToString(vds.Tables[0].Rows[0]["college_course_id"]);
                        objFeepaid.billing_tel = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.billing_email = Convert.ToString(vds.Tables[0].Rows[0]["emailid"]);
                        objFeepaid.QtrNo = Convert.ToString(QNo);

                        objCandidateFee = (from DataRow row in vds.Tables[0].Rows
                                           select new CandidateFee
                                           {
                                               FeeSubHeadName = Convert.ToString(row["fee_subhead_name"]),
                                               FeeAmountYearly = Convert.ToString(row["AdmissionFee"])
                                           }).ToList();
                        var totalcoursefee = objCandidateFee.Sum(item => Convert.ToInt32(item.FeeAmountYearly));

                        objFeepaid.TotalFee = (totalcoursefee);
                        objFeepaid.CandidateFee = objCandidateFee;
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateFeeDetailQ()" + registrationid);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }


        public FeeModule GetCandidatePaymentSuccesDetail(string registrationid, string paymentTransactionId)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetpaymentSuccessDetail", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_Regid", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_paymentTransactionId", paymentTransactionId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();


                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["Reg_id"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.FeePaid = Convert.ToString(vds.Tables[0].Rows[0]["Fee_paid"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.PaymentTransactionId = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
                        objFeepaid.PaymentGateway = Convert.ToString(vds.Tables[0].Rows[0]["Payment_gateway"]);

                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.FatherName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_father_name"]);


                        objFeepaid.PaymentTrackingID = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
                        objFeepaid.Transactiondate = Convert.ToString(vds.Tables[0].Rows[0]["Payment_Date"]);
                        objFeepaid.OrderStatus = Convert.ToString(vds.Tables[0].Rows[0]["order_status"]);
                        //objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.PaymentMode = Convert.ToString(vds.Tables[0].Rows[0]["payment_mode"]);
                        objFeepaid.billing_state = Convert.ToString(vds.Tables[0].Rows[0]["Installment_no"]);

                        objFeepaid.order_id = Convert.ToString(vds.Tables[0].Rows[0]["orderid"]);
                        objFeepaid.Bank_ref_no = Convert.ToString(vds.Tables[0].Rows[0]["bank_ref_no"]);
                        objFeepaid.CancelAdmission = Convert.ToString(vds.Tables[0].Rows[0]["cancelRemarks"]);
                        objFeepaid.Challan_status = Convert.ToString(vds.Tables[0].Rows[0]["Challan_status"]);
                        objFeepaid.TotalFee = Convert.ToInt32(vds.Tables[0].Rows[0]["TotalFee"]);
                        objFeepaid.Concession = Convert.ToInt32(vds.Tables[0].Rows[0]["Concession"]);
                        objFeepaid.PendingFee = Convert.ToInt32(vds.Tables[0].Rows[0]["pendingFee"]);
                        objFeepaid.CollegeType = Convert.ToString(vds.Tables[0].Rows[0]["collegetype"]);
                        objFeepaid.FeePaidNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.FeePaid));

                        objFeepaid.TotalFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.TotalFee));
                        objFeepaid.ConcessionNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.Concession));
                        objFeepaid.PendingFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.PendingFee));
                        objFeepaid.QtrNo = Convert.ToString(vds.Tables[0].Rows[0]["QTR_NO"]);
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidatePaymentSuccesDetail()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objFeepaid;
        }

        public string SaveFeeModule(FeeModule feeModule)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCandidatePaidFee", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeModule.RegistrationId);
                vadap.SelectCommand.Parameters.AddWithValue("P_feepaid", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_TotalFee", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", feeModule.PaymentTransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_Counselling", feeModule.Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("P_college_id", feeModule.merchant_param3);

                //vadap.SelectCommand.Parameters.AddWithValue("P_combinationid", feeModule.merchant_param5);
                //vadap.SelectCommand.Parameters.AddWithValue("P_mobile", feeModule.CandidateMobile);
                //vadap.SelectCommand.Parameters.AddWithValue("P_email", feeModule.CandidateEmailid);
                //vadap.SelectCommand.Parameters.AddWithValue("P_PendingAmount", feeModule.PendingFee);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveFeeModule()" + feeModule.RegistrationId);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SaveFeeModuleforQuarterFee(FeeModule feeModule)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCandidatePaidFeeForQuarterFee", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeModule.RegistrationId);
                vadap.SelectCommand.Parameters.AddWithValue("P_feepaid", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_TotalFee", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", feeModule.PaymentTransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_Counselling", feeModule.Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("P_college_id", feeModule.merchant_param3);
                vadap.SelectCommand.Parameters.AddWithValue("P_Qtr_No", feeModule.QtrNo);

                //vadap.SelectCommand.Parameters.AddWithValue("P_combinationid", feeModule.merchant_param5);
                //vadap.SelectCommand.Parameters.AddWithValue("P_mobile", feeModule.CandidateMobile);
                //vadap.SelectCommand.Parameters.AddWithValue("P_email", feeModule.CandidateEmailid);
                //vadap.SelectCommand.Parameters.AddWithValue("P_PendingAmount", feeModule.PendingFee);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveFeeModuleforQuarterFee()" + feeModule.RegistrationId);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public FeeViewModel GetCandidateAlloCollegeList(string registrationId, string Counselling)
        {
            List<CandidateAllocatedCollege> objCandidateAllocatedCollege = new List<CandidateAllocatedCollege>();
            List<CandidateAdmissionStatus> objCandidateAdmissionStatus = new List<CandidateAdmissionStatus>();
            List<CandidateSeatAllocated> objCandidateSeatAllocated = new List<CandidateSeatAllocated>();

            FeeViewModel objFeepaid = new FeeViewModel();
            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateAlloCollegeList", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", registrationId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_Counselling", Counselling);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objCandidateAllocatedCollege = (from DataRow row in vds.Tables[0].Rows
                                                        select new CandidateAllocatedCollege
                                                        {
                                                            A_Collegeid = Convert.ToString(row["collegeid"]),
                                                            A_CollegeName = Convert.ToString(row["collegename"]),
                                                            A_Section = Convert.ToString(row["SectionName"]),
                                                            A_RegistrationId = Convert.ToString(row["registrationID"]),
                                                        }).ToList();
                    }
                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        objCandidateAdmissionStatus = (from DataRow row in vds.Tables[1].Rows
                                                       select new CandidateAdmissionStatus
                                                       {
                                                           C_CollegeName = Convert.ToString(row["collegename"]),
                                                           C_SectionName = Convert.ToString(row["courseSectionName"]),
                                                           C_RegistrationId = Convert.ToString(row["reg_id"]),
                                                           C_AdmissionStatus = Convert.ToString(row["admissionstatus"]),
                                                           C_PaymentTransactionId = Convert.ToString(row["Payment_transactionId"]),

                                                       }).ToList();
                    }
                    if (vds.Tables[2].Rows.Count > 0)
                    {
                        objCandidateSeatAllocated = (from DataRow row in vds.Tables[2].Rows
                                                     select new CandidateSeatAllocated
                                                     {
                                                         Al_Collegeid = Convert.ToString(row["collegeid"]),
                                                         Al_CollegeName = Convert.ToString(row["collegename"]),
                                                         Al_Section = Convert.ToString(row["SectionName"]),
                                                         Al_Counselling = Convert.ToString(row["counselling"]),
                                                         Al_VerificationStatus = Convert.ToString(row["verificationstatus"]),
                                                         Al_RegistrationId = Convert.ToString(row["registrationID"]),
                                                         A1_meritid = Convert.ToString(row["merit_id"])

                                                     }).ToList();
                    }
                    objFeepaid.CandidateAllocatedCollege = objCandidateAllocatedCollege;
                    objFeepaid.candidateAdmissionStatuses = objCandidateAdmissionStatus;
                    objFeepaid.candidateSeatAllocated = objCandidateSeatAllocated;

                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateAlloCollegeList()" + registrationId);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }

        public FeeViewModel GetCandidateForQuarterFee(string registrationId)
        {
            List<CandidateAllocatedCollege> objCandidateAllocatedCollege = new List<CandidateAllocatedCollege>();
            List<CandidateAdmissionStatus> objCandidateAdmissionStatus = new List<CandidateAdmissionStatus>();
            List<CandidateSeatAllocated> objCandidateSeatAllocated = new List<CandidateSeatAllocated>();

            FeeViewModel objFeepaid = new FeeViewModel();
            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateDetailsForQuarterlyFee", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", registrationId);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objCandidateAllocatedCollege = (from DataRow row in vds.Tables[0].Rows
                                                        select new CandidateAllocatedCollege
                                                        {
                                                            A_Collegeid = Convert.ToString(row["collegeid"]),
                                                            A_CollegeName = Convert.ToString(row["collegename"]),
                                                            A_Section = Convert.ToString(row["SectionName"]),
                                                            A_QtrNo = Convert.ToString(row["Q_No"]),
                                                            A_RegistrationId = Convert.ToString(row["registrationID"]),
                                                        }).ToList();
                    }
                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        objCandidateAdmissionStatus = (from DataRow row in vds.Tables[1].Rows
                                                       select new CandidateAdmissionStatus
                                                       {
                                                           C_CollegeName = Convert.ToString(row["collegename"]),
                                                           C_SectionName = Convert.ToString(row["courseSectionName"]),
                                                           C_RegistrationId = Convert.ToString(row["reg_id"]),
                                                           C_AdmissionStatus = Convert.ToString(row["admissionstatus"]),
                                                           C_PaymentTransactionId = Convert.ToString(row["Payment_transactionId"]),
                                                           C_QtrNo = Convert.ToString(row["QTR_NO"]),
                                                       }).ToList();
                    }
                    if (vds.Tables[2].Rows.Count > 0)
                    {
                        objCandidateSeatAllocated = (from DataRow row in vds.Tables[2].Rows
                                                     select new CandidateSeatAllocated
                                                     {
                                                         Al_Collegeid = Convert.ToString(row["collegeid"]),
                                                         Al_CollegeName = Convert.ToString(row["collegename"]),
                                                         Al_Section = Convert.ToString(row["SectionName"]),
                                                         Al_Counselling = Convert.ToString(row["counselling"]),
                                                         Al_VerificationStatus = Convert.ToString(row["verificationstatus"]),
                                                         Al_RegistrationId = Convert.ToString(row["registrationID"]),
                                                         A1_meritid = Convert.ToString(row["merit_id"])

                                                     }).ToList();
                    }
                    objFeepaid.CandidateAllocatedCollege = objCandidateAllocatedCollege;
                    objFeepaid.candidateAdmissionStatuses = objCandidateAdmissionStatus;
                    objFeepaid.candidateSeatAllocated = objCandidateSeatAllocated;

                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateForQuarterFee()" + registrationId);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }

        public DataTable GetPaymentStatus(string registrationId)
        {
            DataTable result = new DataTable();

            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                //DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidatePaymentStatus", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", registrationId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(result);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetPaymentStatus()" + registrationId);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public static string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        public string SaveIndusIndPaymentResponse_Failure(clsPaymentGateway feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseDataIndus_failure", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("Pinv_no", feeresponse.inv_no);
                vadap.SelectCommand.Parameters.AddWithValue("Ptrn_ref", feeresponse.trn_ref);
                vadap.SelectCommand.Parameters.AddWithValue("Pamount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("Prequest_id", feeresponse.request_id);
                vadap.SelectCommand.Parameters.AddWithValue("Pstatus", feeresponse.status);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveIndusIndPaymentResponse_Failure()" + feeresponse.inv_no);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        public string SaveIndusIndPaymentResponse_success(clsPaymentGateway feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseDataIndus_success", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("Pinv_no", feeresponse.inv_no);
                vadap.SelectCommand.Parameters.AddWithValue("Ptrn_ref", feeresponse.trn_ref);
                vadap.SelectCommand.Parameters.AddWithValue("Pamount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("Prequest_id", feeresponse.request_id);
                vadap.SelectCommand.Parameters.AddWithValue("Pstatus", feeresponse.status);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveIndusIndPaymentResponse_success()" + feeresponse.inv_no);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        // to check regid in meritlist table 

        public DataTable GetStudentMeritListStatus(string registrationId)
        {
            DataTable result = new DataTable();

            try
            {
                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                //DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetStudentMeritListStatus", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", registrationId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(result);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                //if (vds.Tables.Count > 0)
                //{
                //    //if (vds.Tables[0].Rows.Count > 0)
                //    //{
                //    //    result = vds.Tables[0].Rows[0]["registrationID"].ToString();
                //    //}
                //}

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetPaymentStatus()" + registrationId);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return result;
        }

        // save fee for SC candidate verfied income certificate
        public string SaveFeeofSCCandidate(string Regid, string CollegeId, string CombinationId, string TransactionId, string VerifyStatus, string PaymentTransactionId)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveSCCandidateFee_Verified", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_Regid", Regid);
                vadap.SelectCommand.Parameters.AddWithValue("P_CollegeId", CollegeId);
                vadap.SelectCommand.Parameters.AddWithValue("P_CombinationId", CombinationId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_EdishaTransId", TransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_VerifyStatus", VerifyStatus);
                vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", PaymentTransactionId);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveFeeofSCCandidate()" + Regid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SaveVerifiedDocument(string Regid, string docId, string TransactionId)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveVerifiedDocument", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_Reg_Id", Regid);
                vadap.SelectCommand.Parameters.AddWithValue("P_docid", docId);
                vadap.SelectCommand.Parameters.AddWithValue("P_docno", TransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveVerifiedDocument()" + Regid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }


        // get image 
        public DataTable GetImage(string docid, string Regid)
        {
            DataTable result = new DataTable();

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlDataAdapter vadap = new MySqlDataAdapter("getsaveddoc", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_Regid", Regid);
                vadap.SelectCommand.Parameters.AddWithValue("P_docid", docid);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(result);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetImage()" + Regid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        public string checkRegId(string PaymentTransId)
        {
            string result = "0";

            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateRegIdfor_paymentresponse", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_PaymentTransactionId", PaymentTransId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        result = vds.Tables[0].Rows[0]["Reg_id"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.checkRegId()" + PaymentTransId);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SaveResponsePayU(clsPayU feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseData", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.AppCode);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.MihpayID);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.Bank_Ref_No);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.Status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.Mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.Payment_Source);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", "");
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.UnMappedStatus);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.addedon);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", feeresponse.AppCode);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.EdishaXtnID);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", feeresponse.udf2_MCODE);
                vadap.SelectCommand.Parameters.AddWithValue("P_combinationid", feeresponse.udf5_ServiceName);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.Net_Amount_Debit);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveResponsePayU()");
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SaveResponsePayU_Cancle(clsPayU feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseData_cancel", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.AppCode);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.MihpayID);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.Bank_Ref_No);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.Status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.Mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.Payment_Source);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.Error);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.Error_Msg);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.addedon);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", feeresponse.AppCode);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.EdishaXtnID);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.Net_Amount_Debit);


                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveResponsePayU_Cancle()");
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }


        public string OpenCounsellingConsent(string regId, string Counselling, string CounsellingEdit)
        {
            string result = "0";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("OpenCounsellingUpdateCandidateFlag", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", regId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@P_CreateUser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_Counselling", Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("@P_CounsellingEdit", CounsellingEdit == null ? "P" : CounsellingEdit);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.OpenCounsellingConsent()");
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string ChangeChoiceofCourses(string regId, string EditChoice)
        {
            string result = "0";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("OpenCounsellingUpdateCandidateFlag_choice", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", regId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@P_CreateUser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_Counselling", EditChoice);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.ChangeChoiceofCourses()");
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SetApiStatusNo(string regId)
        {
            string result = "0";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("PSsetAPiStatusNo", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_Regid", regId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@P_CreateUser", UserId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SetApiStatusNo()");
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SaveFeePayLater(string Regid, string CollegeId, string CombinationId, string Concession)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCandidateFeePayLater", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_Regid", Regid);
                vadap.SelectCommand.Parameters.AddWithValue("P_CollegeId", CollegeId);
                vadap.SelectCommand.Parameters.AddWithValue("P_CombinationId", CombinationId);
                vadap.SelectCommand.Parameters.AddWithValue("P_Concession", Concession);
                //vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                //vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveFeeofSCCandidate()" + Regid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SendOTPConfirmSeat(string regId)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                Random r = new Random();
                string rndnumber = r.Next(1111, 9999).ToString();
                //string sms_details = "OTP to confirm seat on Online Admission Portal is " + rndnumber;
                string sms_details = "OTP to cancel admission on Online Admission Portal for ITI is " + rndnumber + " Regards, SDIT Haryana";

                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveUnlockOTPDetails", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@otp", rndnumber);
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@create_user", regId);
                vadap.SelectCommand.Parameters.AddWithValue("@reg_id", regId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        result = vds.Tables[0].Rows[0]["issuccess"].ToString();
                        if (result == "1")
                        {
                            string mobileno = vds.Tables[0].Rows[0]["mobileno"].ToString();
                            string EmailId = vds.Tables[0].Rows[0]["emailid"].ToString();
                            AgriSMS.sendSingleSMS(mobileno, sms_details, "1007222238653939276");
                            SMS.SendEmail(EmailId, "OTP to Confirm Seat", sms_details);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SendOTPUnlock()");
            }
            return result;
        }

        // Fee payment PG
        public List<ErrorLogViewModel> GetErrorLog()
        {
            List<ErrorLogViewModel> errorLogViewModel1 = new List<ErrorLogViewModel>();
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetLogErrors", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ErrorLogViewModel errorLogViewModel = new ErrorLogViewModel();

                    errorLogViewModel.Exc_Date = Convert.ToString(rdr["Exc_Date"]);
                    errorLogViewModel.Exc_Location = Convert.ToString(rdr["Exc_Location"]);
                    errorLogViewModel.Exc_Type = Convert.ToString(rdr["Exc_Type"]);
                    errorLogViewModel.Exc_Method = Convert.ToString(rdr["Exc_Method"]);
                    errorLogViewModel.Full_Exc = Convert.ToString(rdr["Full_Exc"]);
                    errorLogViewModel.Exc_Message = Convert.ToString(rdr["Exc_Message"]);
                    errorLogViewModel.Exc_Level = Convert.ToString(rdr["Exc_Level"]);
                    errorLogViewModel1.Add(errorLogViewModel);

                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetErrorLog()");
            }
            connection_readonly.Close();
            return errorLogViewModel1;
        }

        public string CancelAdmissionITI(string regId, string Reason)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("CancelAdmissionITI", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_ip_address", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_create_user", regId);
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", regId);
                vadap.SelectCommand.Parameters.AddWithValue("P_Reason", Reason);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        result = vds.Tables[0].Rows[0]["success"].ToString();
                        if (result == "1")
                        {
                            //string mobileno = vds.Tables[0].Rows[0]["mobileno"].ToString();
                            //string EmailId = vds.Tables[0].Rows[0]["emailid"].ToString();
                            //AgriSMS.sendSingleSMS(mobileno, sms_details, "1007222238653939276");
                            //SMS.SendEmail(EmailId, "OTP to Confirm Seat", sms_details);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CancelAdmissionITI()");
            }
            return result;
        }

        public DataTable getData10th_ITI()
        {
            DataTable dt = new DataTable();
            List<CandidateDetail> lst = new List<CandidateDetail>();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("Get10thDataDigi_ITI", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.getData()");
            }
            connection.Close();
            return dt;

        }
        public string EditApplication(string regid)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                string result = "";
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("EditApplication", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("p_regid", regid);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                connection.Close();

                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                // connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.EditApplication()");
            }
            connection.Close();
            return "0";
        }

        #region for second Year Fee
        public FeeModule GetCandidateDetailsSecondYear(string registrationid)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateInfoForQuarterlyFee_2021", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {

                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["registrationID"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.CandidateEmailid = Convert.ToString(vds.Tables[0].Rows[0]["emailid"]);
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateDetailsSecondYear()" + registrationid);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }

        public FeeViewModel GetCandidateForQuarterFeeSecondYear(string registrationId)
        {
            List<CandidateAllocatedCollege> objCandidateAllocatedCollege = new List<CandidateAllocatedCollege>();
            List<CandidateAdmissionStatus> objCandidateAdmissionStatus = new List<CandidateAdmissionStatus>();
            List<CandidateSeatAllocated> objCandidateSeatAllocated = new List<CandidateSeatAllocated>();
            List<CandidatePasswordStatus> objCandidatePass = new List<CandidatePasswordStatus>();
            FeeViewModel objFeepaid = new FeeViewModel();

            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateDetailsForQuarterlyFee_2021", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", registrationId);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objCandidateAllocatedCollege = (from DataRow row in vds.Tables[0].Rows
                                                        select new CandidateAllocatedCollege
                                                        {
                                                            A_Collegeid = Convert.ToString(row["collegeid"]),
                                                            A_CollegeName = Convert.ToString(row["collegename"]),
                                                            A_Section = Convert.ToString(row["SectionName"]),
                                                            A_QtrNo = Convert.ToString(row["Q_No"]),
                                                            A_RegistrationId = Convert.ToString(row["registrationID"]),
                                                        }).ToList();
                    }
                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        objCandidateAdmissionStatus = (from DataRow row in vds.Tables[1].Rows
                                                       select new CandidateAdmissionStatus
                                                       {
                                                           C_CollegeName = Convert.ToString(row["collegename"]),
                                                           C_SectionName = Convert.ToString(row["courseSectionName"]),
                                                           C_RegistrationId = Convert.ToString(row["reg_id"]),
                                                           C_AdmissionStatus = Convert.ToString(row["admissionstatus"]),
                                                           C_PaymentTransactionId = Convert.ToString(row["Payment_transactionId"]),
                                                           C_QtrNo = Convert.ToString(row["QTR_NO"]),
                                                       }).ToList();
                    }
                    if (vds.Tables[2].Rows.Count > 0)
                    {
                        objCandidateSeatAllocated = (from DataRow row in vds.Tables[2].Rows
                                                     select new CandidateSeatAllocated
                                                     {
                                                         Al_Collegeid = Convert.ToString(row["collegeid"]),
                                                         Al_CollegeName = Convert.ToString(row["collegename"]),
                                                         Al_Section = Convert.ToString(row["SectionName"]),
                                                         Al_Counselling = Convert.ToString(row["counselling"]),
                                                         Al_VerificationStatus = Convert.ToString(row["verificationstatus"]),
                                                         Al_RegistrationId = Convert.ToString(row["registrationID"]),
                                                         A1_meritid = Convert.ToString(row["merit_id"])

                                                     }).ToList();
                    }
                    if (vds.Tables[3].Rows.Count > 0)
                    {
                        objCandidatePass = (from DataRow row in vds.Tables[3].Rows
                                            select new CandidatePasswordStatus
                                            {
                                                Updated = Convert.ToString(row["P_Status"]),
                                            }).ToList();
                    }
                    objFeepaid.CandidateAllocatedCollege = objCandidateAllocatedCollege;
                    objFeepaid.candidateAdmissionStatuses = objCandidateAdmissionStatus;
                    objFeepaid.candidateSeatAllocated = objCandidateSeatAllocated;
                    objFeepaid.candidatePasswordStatus = objCandidatePass;

                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateForQuarterFeeSecondYear()" + registrationId);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }


        public FeeModule GetCandidateFeeDetailSQ(string registrationid, string collegeid, string QNo)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateFeeDetailQuarter_2021", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", collegeid);



                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {

                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["registrationID"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.billing_address = Convert.ToString(vds.Tables[0].Rows[0]["section"]);
                        objFeepaid.billing_name = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.merchant_param2 = Convert.ToString(vds.Tables[0].Rows[0]["coursesession"]);
                        objFeepaid.merchant_param3 = Convert.ToString(vds.Tables[0].Rows[0]["collegeid"]);
                        objFeepaid.merchant_param4 = Convert.ToString(vds.Tables[0].Rows[0]["college_course_id"]);
                        objFeepaid.billing_tel = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.billing_email = Convert.ToString(vds.Tables[0].Rows[0]["emailid"]);
                        objFeepaid.QtrNo = Convert.ToString(QNo);

                        objCandidateFee = (from DataRow row in vds.Tables[0].Rows
                                           select new CandidateFee
                                           {
                                               FeeSubHeadName = Convert.ToString(row["fee_subhead_name"]),
                                               FeeAmountYearly = Convert.ToString(row["AdmissionFee"])
                                           }).ToList();
                        var totalcoursefee = objCandidateFee.Sum(item => Convert.ToInt32(item.FeeAmountYearly));

                        objFeepaid.TotalFee = (totalcoursefee);
                        objFeepaid.CandidateFee = objCandidateFee;
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateFeeDetailSQ()" + registrationid);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }

        public string SaveFeeModuleforSecondYear(FeeModule feeModule)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCandidatePaidFeeForQuarterFee_2021", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeModule.RegistrationId);
                vadap.SelectCommand.Parameters.AddWithValue("P_feepaid", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_TotalFee", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", feeModule.PaymentTransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_Counselling", feeModule.Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("P_college_id", feeModule.merchant_param3);
                vadap.SelectCommand.Parameters.AddWithValue("P_Qtr_No", feeModule.QtrNo);

                //vadap.SelectCommand.Parameters.AddWithValue("P_combinationid", feeModule.merchant_param5);
                //vadap.SelectCommand.Parameters.AddWithValue("P_mobile", feeModule.CandidateMobile);
                //vadap.SelectCommand.Parameters.AddWithValue("P_email", feeModule.CandidateEmailid);
                //vadap.SelectCommand.Parameters.AddWithValue("P_PendingAmount", feeModule.PendingFee);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveFeeModuleforSecondYear()" + feeModule.RegistrationId);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public FeeModule GetCandidatePaymentSuccesDetailSecondYear(string registrationid, string paymentTransactionId)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetpaymentSuccessDetail_2021", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_Regid", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_paymentTransactionId", paymentTransactionId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();


                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["Reg_id"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.FeePaid = Convert.ToString(vds.Tables[0].Rows[0]["Fee_paid"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.PaymentTransactionId = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
                        objFeepaid.PaymentGateway = Convert.ToString(vds.Tables[0].Rows[0]["Payment_gateway"]);

                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.FatherName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_father_name"]);


                        objFeepaid.PaymentTrackingID = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
                        objFeepaid.Transactiondate = Convert.ToString(vds.Tables[0].Rows[0]["Payment_Date"]);
                        objFeepaid.OrderStatus = Convert.ToString(vds.Tables[0].Rows[0]["order_status"]);
                        //objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.PaymentMode = Convert.ToString(vds.Tables[0].Rows[0]["payment_mode"]);
                        objFeepaid.billing_state = Convert.ToString(vds.Tables[0].Rows[0]["Installment_no"]);

                        objFeepaid.order_id = Convert.ToString(vds.Tables[0].Rows[0]["orderid"]);
                        objFeepaid.Bank_ref_no = Convert.ToString(vds.Tables[0].Rows[0]["bank_ref_no"]);
                        objFeepaid.CancelAdmission = Convert.ToString(vds.Tables[0].Rows[0]["cancelRemarks"]);
                        objFeepaid.Challan_status = Convert.ToString(vds.Tables[0].Rows[0]["Challan_status"]);
                        objFeepaid.TotalFee = Convert.ToInt32(vds.Tables[0].Rows[0]["TotalFee"]);
                        objFeepaid.Concession = Convert.ToInt32(vds.Tables[0].Rows[0]["Concession"]);
                        objFeepaid.PendingFee = Convert.ToInt32(vds.Tables[0].Rows[0]["pendingFee"]);
                        objFeepaid.CollegeType = Convert.ToString(vds.Tables[0].Rows[0]["collegetype"]);
                        objFeepaid.FeePaidNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.FeePaid));

                        objFeepaid.TotalFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.TotalFee));
                        objFeepaid.ConcessionNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.Concession));
                        objFeepaid.PendingFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.PendingFee));
                        objFeepaid.QtrNo = Convert.ToString(vds.Tables[0].Rows[0]["QTR_NO"]);
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidatePaymentSuccesDetailSecondYear()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objFeepaid;
        }


        #endregion


        #region for Old Session Quarter Fee
        public FeeModule GetCandidateDetailsOldSession(string registrationid, string aSession)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateInfoForQuarterlyFee", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("P_aSession", aSession);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {

                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["registrationID"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.CandidateEmailid = Convert.ToString(vds.Tables[0].Rows[0]["emailid"]);
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateDetailsOldSession()" + registrationid);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }

        public FeeViewModel GetCandidateForQuarterFeeOldSession(string registrationId, string aSession)
        {
            List<CandidateAllocatedCollege> objCandidateAllocatedCollege = new List<CandidateAllocatedCollege>();
            List<CandidateAdmissionStatus> objCandidateAdmissionStatus = new List<CandidateAdmissionStatus>();
            List<CandidateSeatAllocated> objCandidateSeatAllocated = new List<CandidateSeatAllocated>();
            List<CandidatePasswordStatus> objCandidatePass = new List<CandidatePasswordStatus>();
            FeeViewModel objFeepaid = new FeeViewModel();

            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateDetailsForQuarterlyFee", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", registrationId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_aSession", aSession);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objCandidateAllocatedCollege = (from DataRow row in vds.Tables[0].Rows
                                                        select new CandidateAllocatedCollege
                                                        {
                                                            A_Collegeid = Convert.ToString(row["collegeid"]),
                                                            A_CollegeName = Convert.ToString(row["collegename"]),
                                                            A_Section = Convert.ToString(row["SectionName"]),
                                                            A_QtrNo = Convert.ToString(row["Q_No"]),
                                                            A_RegistrationId = Convert.ToString(row["registrationID"]),
                                                        }).ToList();
                    }
                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        objCandidateAdmissionStatus = (from DataRow row in vds.Tables[1].Rows
                                                       select new CandidateAdmissionStatus
                                                       {
                                                           C_CollegeName = Convert.ToString(row["collegename"]),
                                                           C_SectionName = Convert.ToString(row["courseSectionName"]),
                                                           C_RegistrationId = Convert.ToString(row["reg_id"]),
                                                           C_AdmissionStatus = Convert.ToString(row["admissionstatus"]),
                                                           C_PaymentTransactionId = Convert.ToString(row["Payment_transactionId"]),
                                                           C_QtrNo = Convert.ToString(row["QTR_NO"]),
                                                       }).ToList();
                    }
                    if (vds.Tables[2].Rows.Count > 0)
                    {
                        objCandidateSeatAllocated = (from DataRow row in vds.Tables[2].Rows
                                                     select new CandidateSeatAllocated
                                                     {
                                                         Al_Collegeid = Convert.ToString(row["collegeid"]),
                                                         Al_CollegeName = Convert.ToString(row["collegename"]),
                                                         Al_Section = Convert.ToString(row["SectionName"]),
                                                         Al_Counselling = Convert.ToString(row["counselling"]),
                                                         Al_VerificationStatus = Convert.ToString(row["verificationstatus"]),
                                                         Al_RegistrationId = Convert.ToString(row["registrationID"]),
                                                         A1_meritid = Convert.ToString(row["merit_id"])

                                                     }).ToList();
                    }
                    if (vds.Tables[3].Rows.Count > 0)
                    {
                        objCandidatePass = (from DataRow row in vds.Tables[3].Rows
                                            select new CandidatePasswordStatus
                                            {
                                                Updated = Convert.ToString(row["P_Status"]),
                                            }).ToList();
                    }
                    objFeepaid.CandidateAllocatedCollege = objCandidateAllocatedCollege;
                    objFeepaid.candidateAdmissionStatuses = objCandidateAdmissionStatus;
                    objFeepaid.candidateSeatAllocated = objCandidateSeatAllocated;
                    objFeepaid.candidatePasswordStatus = objCandidatePass;

                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateForQuarterFeeOldSession()" + registrationId);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }

        public FeeModule GetCandidateFeeDetailOQ(string registrationid, string collegeid, string QNo, string admSession)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateFeeDetailQuarter", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("P_qNo", QNo);
                vadap.SelectCommand.Parameters.AddWithValue("P_aSession", admSession);



                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {

                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["registrationID"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.billing_address = Convert.ToString(vds.Tables[0].Rows[0]["section"]);
                        objFeepaid.billing_name = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.merchant_param2 = Convert.ToString(vds.Tables[0].Rows[0]["coursesession"]);
                        objFeepaid.merchant_param3 = Convert.ToString(vds.Tables[0].Rows[0]["collegeid"]);
                        objFeepaid.merchant_param4 = Convert.ToString(vds.Tables[0].Rows[0]["college_course_id"]);
                        objFeepaid.billing_tel = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.billing_email = Convert.ToString(vds.Tables[0].Rows[0]["emailid"]);
                        objFeepaid.QtrNo = Convert.ToString(QNo);

                        objCandidateFee = (from DataRow row in vds.Tables[0].Rows
                                           select new CandidateFee
                                           {
                                               FeeSubHeadName = Convert.ToString(row["fee_subhead_name"]),
                                               FeeAmountYearly = Convert.ToString(row["AdmissionFee"])
                                           }).ToList();
                        var totalcoursefee = objCandidateFee.Sum(item => Convert.ToInt32(item.FeeAmountYearly));

                        objFeepaid.TotalFee = (totalcoursefee);
                        objFeepaid.CandidateFee = objCandidateFee;
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateFeeDetailOQ()" + registrationid);
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
            return objFeepaid;
        }

        public string SaveFeeModuleforOldSession(FeeModule feeModule, string admSession)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCandidatePaidFeeForQuarterFee", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeModule.RegistrationId);
                vadap.SelectCommand.Parameters.AddWithValue("P_feepaid", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_TotalFee", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", feeModule.PaymentTransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_Counselling", feeModule.Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("P_college_id", feeModule.merchant_param3);
                vadap.SelectCommand.Parameters.AddWithValue("P_Qtr_No", feeModule.QtrNo);
                vadap.SelectCommand.Parameters.AddWithValue("P_aSession", admSession);

                //vadap.SelectCommand.Parameters.AddWithValue("P_combinationid", feeModule.merchant_param5);
                //vadap.SelectCommand.Parameters.AddWithValue("P_mobile", feeModule.CandidateMobile);
                //vadap.SelectCommand.Parameters.AddWithValue("P_email", feeModule.CandidateEmailid);
                //vadap.SelectCommand.Parameters.AddWithValue("P_PendingAmount", feeModule.PendingFee);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveFeeModuleforOldSession()" + feeModule.RegistrationId);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public FeeModule GetCandidatePaymentSuccesDetailOldSession(string registrationid, string paymentTransactionId)
        {
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetpaymentSuccessDetail_QTR", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_Regid", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_paymentTransactionId", paymentTransactionId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();


                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["Reg_id"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.FeePaid = Convert.ToString(vds.Tables[0].Rows[0]["Fee_paid"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.PaymentTransactionId = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
                        objFeepaid.PaymentGateway = Convert.ToString(vds.Tables[0].Rows[0]["Payment_gateway"]);

                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.FatherName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_father_name"]);


                        objFeepaid.PaymentTrackingID = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
                        objFeepaid.Transactiondate = Convert.ToString(vds.Tables[0].Rows[0]["Payment_Date"]);
                        objFeepaid.OrderStatus = Convert.ToString(vds.Tables[0].Rows[0]["order_status"]);
                        //objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.PaymentMode = Convert.ToString(vds.Tables[0].Rows[0]["payment_mode"]);
                        objFeepaid.billing_state = Convert.ToString(vds.Tables[0].Rows[0]["Installment_no"]);

                        objFeepaid.order_id = Convert.ToString(vds.Tables[0].Rows[0]["orderid"]);
                        objFeepaid.Bank_ref_no = Convert.ToString(vds.Tables[0].Rows[0]["bank_ref_no"]);
                        objFeepaid.CancelAdmission = Convert.ToString(vds.Tables[0].Rows[0]["cancelRemarks"]);
                        objFeepaid.Challan_status = Convert.ToString(vds.Tables[0].Rows[0]["Challan_status"]);
                        objFeepaid.TotalFee = Convert.ToInt32(vds.Tables[0].Rows[0]["TotalFee"]);
                        objFeepaid.Concession = Convert.ToInt32(vds.Tables[0].Rows[0]["Concession"]);
                        objFeepaid.PendingFee = Convert.ToInt32(vds.Tables[0].Rows[0]["pendingFee"]);
                        objFeepaid.CollegeType = Convert.ToString(vds.Tables[0].Rows[0]["collegetype"]);
                        objFeepaid.FeePaidNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.FeePaid));

                        objFeepaid.TotalFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.TotalFee));
                        objFeepaid.ConcessionNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.Concession));
                        objFeepaid.PendingFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.PendingFee));
                        objFeepaid.QtrNo = Convert.ToString(vds.Tables[0].Rows[0]["QTR_NO"]);
                        objFeepaid.admYear = Convert.ToString(vds.Tables[0].Rows[0]["admYear"]);
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidatePaymentSuccesDetailOldSession()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objFeepaid;
        }
        //public FeeModule GetCandidateDetailsOldSession(string registrationid)
        //{
        //    List<FeeModule> objAddFee = new List<FeeModule>();
        //    List<CandidateFee> objCandidateFee = new List<CandidateFee>();
        //    FeeModule objFeepaid = new FeeModule();
        //    try
        //    {

        //        if (connection_readonly.State == ConnectionState.Closed)
        //        {
        //            connection_readonly.Open();
        //        }
        //        DataSet vds = new DataSet();
        //        MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateInfoForQuarterlyFee_2022", connection_readonly);
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);

        //        vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

        //        vadap.Fill(vds);
        //        if (connection_readonly.State == ConnectionState.Open)
        //            connection_readonly.Close();

        //        if (vds.Tables.Count > 0)
        //        {
        //            if (vds.Tables[0].Rows.Count > 0)
        //            {

        //                objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["registrationID"]);
        //                objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
        //                objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
        //                objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
        //                objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
        //                objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
        //                objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
        //                objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
        //                objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
        //                objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
        //                objFeepaid.CandidateEmailid = Convert.ToString(vds.Tables[0].Rows[0]["emailid"]);
        //            }
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateDetailsOldSession()" + registrationid);
        //    }
        //    if (connection_readonly.State == ConnectionState.Open)
        //    {
        //        connection_readonly.Close();
        //    }
        //    return objFeepaid;
        //}

        //public FeeViewModel GetCandidateForQuarterFeeOldSession(string registrationId)
        //{
        //    List<CandidateAllocatedCollege> objCandidateAllocatedCollege = new List<CandidateAllocatedCollege>();
        //    List<CandidateAdmissionStatus> objCandidateAdmissionStatus = new List<CandidateAdmissionStatus>();
        //    List<CandidateSeatAllocated> objCandidateSeatAllocated = new List<CandidateSeatAllocated>();
        //    FeeViewModel objFeepaid = new FeeViewModel();

        //    try
        //    {

        //        if (connection_readonly.State == ConnectionState.Closed)
        //        {
        //            connection_readonly.Open();
        //        }
        //        DataSet vds = new DataSet();
        //        MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateDetailsForQuarterlyFee_2022", connection_readonly);
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.SelectCommand.Parameters.AddWithValue("@P_reg_id", registrationId);

        //        vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

        //        vadap.Fill(vds);
        //        if (connection_readonly.State == ConnectionState.Open)
        //            connection_readonly.Close();

        //        if (vds.Tables.Count > 0)
        //        {
        //            if (vds.Tables[0].Rows.Count > 0)
        //            {
        //                objCandidateAllocatedCollege = (from DataRow row in vds.Tables[0].Rows
        //                                                select new CandidateAllocatedCollege
        //                                                {
        //                                                    A_Collegeid = Convert.ToString(row["collegeid"]),
        //                                                    A_CollegeName = Convert.ToString(row["collegename"]),
        //                                                    A_Section = Convert.ToString(row["SectionName"]),
        //                                                    A_QtrNo = Convert.ToString(row["Q_No"]),
        //                                                    A_RegistrationId = Convert.ToString(row["registrationID"]),
        //                                                }).ToList();
        //            }
        //            if (vds.Tables[1].Rows.Count > 0)
        //            {
        //                objCandidateAdmissionStatus = (from DataRow row in vds.Tables[1].Rows
        //                                               select new CandidateAdmissionStatus
        //                                               {
        //                                                   C_CollegeName = Convert.ToString(row["collegename"]),
        //                                                   C_SectionName = Convert.ToString(row["courseSectionName"]),
        //                                                   C_RegistrationId = Convert.ToString(row["reg_id"]),
        //                                                   C_AdmissionStatus = Convert.ToString(row["admissionstatus"]),
        //                                                   C_PaymentTransactionId = Convert.ToString(row["Payment_transactionId"]),
        //                                                   C_QtrNo = Convert.ToString(row["QTR_NO"]),
        //                                               }).ToList();
        //            }
        //            if (vds.Tables[2].Rows.Count > 0)
        //            {
        //                objCandidateSeatAllocated = (from DataRow row in vds.Tables[2].Rows
        //                                             select new CandidateSeatAllocated
        //                                             {
        //                                                 Al_Collegeid = Convert.ToString(row["collegeid"]),
        //                                                 Al_CollegeName = Convert.ToString(row["collegename"]),
        //                                                 Al_Section = Convert.ToString(row["SectionName"]),
        //                                                 Al_Counselling = Convert.ToString(row["counselling"]),
        //                                                 Al_VerificationStatus = Convert.ToString(row["verificationstatus"]),
        //                                                 Al_RegistrationId = Convert.ToString(row["registrationID"]),
        //                                                 A1_meritid = Convert.ToString(row["merit_id"])

        //                                             }).ToList();
        //            }

        //            objFeepaid.CandidateAllocatedCollege = objCandidateAllocatedCollege;
        //            objFeepaid.candidateAdmissionStatuses = objCandidateAdmissionStatus;
        //            objFeepaid.candidateSeatAllocated = objCandidateSeatAllocated;

        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateForQuarterFeeOldSession()" + registrationId);
        //    }
        //    if (connection_readonly.State == ConnectionState.Open)
        //    {
        //        connection_readonly.Close();
        //    }
        //    return objFeepaid;
        //}


        //public FeeModule GetCandidateFeeDetailOQ(string registrationid, string collegeid, string QNo)
        //{
        //    List<FeeModule> objAddFee = new List<FeeModule>();
        //    List<CandidateFee> objCandidateFee = new List<CandidateFee>();
        //    FeeModule objFeepaid = new FeeModule();
        //    try
        //    {

        //        if (connection_readonly.State == ConnectionState.Closed)
        //        {
        //            connection_readonly.Open();
        //        }
        //        DataSet vds = new DataSet();
        //        MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateFeeDetailQuarter_2022", connection_readonly);
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", collegeid);



        //        vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

        //        vadap.Fill(vds);
        //        if (connection_readonly.State == ConnectionState.Open)
        //            connection_readonly.Close();

        //        if (vds.Tables.Count > 0)
        //        {
        //            if (vds.Tables[0].Rows.Count > 0)
        //            {

        //                objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["registrationID"]);
        //                objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
        //                objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
        //                objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
        //                objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
        //                objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
        //                objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
        //                objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
        //                objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
        //                objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
        //                objFeepaid.billing_address = Convert.ToString(vds.Tables[0].Rows[0]["section"]);
        //                objFeepaid.billing_name = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
        //                objFeepaid.merchant_param2 = Convert.ToString(vds.Tables[0].Rows[0]["coursesession"]);
        //                objFeepaid.merchant_param3 = Convert.ToString(vds.Tables[0].Rows[0]["collegeid"]);
        //                objFeepaid.merchant_param4 = Convert.ToString(vds.Tables[0].Rows[0]["college_course_id"]);
        //                objFeepaid.billing_tel = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
        //                objFeepaid.billing_email = Convert.ToString(vds.Tables[0].Rows[0]["emailid"]);
        //                objFeepaid.QtrNo = Convert.ToString(QNo);

        //                objCandidateFee = (from DataRow row in vds.Tables[0].Rows
        //                                   select new CandidateFee
        //                                   {
        //                                       FeeSubHeadName = Convert.ToString(row["fee_subhead_name"]),
        //                                       FeeAmountYearly = Convert.ToString(row["AdmissionFee"])
        //                                   }).ToList();
        //                var totalcoursefee = objCandidateFee.Sum(item => Convert.ToInt32(item.FeeAmountYearly));

        //                objFeepaid.TotalFee = (totalcoursefee);
        //                objFeepaid.CandidateFee = objCandidateFee;
        //            }
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidateFeeDetailOQ()" + registrationid);
        //    }
        //    if (connection_readonly.State == ConnectionState.Open)
        //    {
        //        connection_readonly.Close();
        //    }
        //    return objFeepaid;
        //}

        //public string SaveFeeModuleforOldSession(FeeModule feeModule)
        //{
        //    string result = "0";

        //    if (connection.State == ConnectionState.Closed)
        //    {
        //        connection.Open();
        //    }
        //    try
        //    {
        //        DataTable vds = new DataTable();
        //        MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCandidatePaidFeeForQuarterFee_2022", connection);
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeModule.RegistrationId);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_feepaid", feeModule.TotalFee);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_TotalFee", feeModule.TotalFee);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", feeModule.PaymentTransactionId);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_Counselling", feeModule.Counselling);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_college_id", feeModule.merchant_param3);
        //        vadap.SelectCommand.Parameters.AddWithValue("P_Qtr_No", feeModule.QtrNo);

        //        //vadap.SelectCommand.Parameters.AddWithValue("P_combinationid", feeModule.merchant_param5);
        //        //vadap.SelectCommand.Parameters.AddWithValue("P_mobile", feeModule.CandidateMobile);
        //        //vadap.SelectCommand.Parameters.AddWithValue("P_email", feeModule.CandidateEmailid);
        //        //vadap.SelectCommand.Parameters.AddWithValue("P_PendingAmount", feeModule.PendingFee);

        //        vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.Fill(vds);
        //        if (vds.Rows.Count > 0)
        //        {
        //            result = vds.Rows[0]["success"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveFeeModuleforOldSession()" + feeModule.RegistrationId);
        //    }
        //    if (connection.State == ConnectionState.Open)
        //    {
        //        connection.Close();
        //    }
        //    return result;
        //}

        //public FeeModule GetCandidatePaymentSuccesDetailOldSession(string registrationid, string paymentTransactionId)
        //{
        //    List<FeeModule> objAddFee = new List<FeeModule>();
        //    List<CandidateFee> objCandidateFee = new List<CandidateFee>();
        //    FeeModule objFeepaid = new FeeModule();
        //    try
        //    {

        //        if (connection.State == ConnectionState.Closed)
        //        {
        //            connection.Open();
        //        }
        //        DataSet vds = new DataSet();
        //        MySqlDataAdapter vadap = new MySqlDataAdapter("GetpaymentSuccessDetail_2022", connection);
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.SelectCommand.Parameters.AddWithValue("@P_Regid", registrationid);
        //        vadap.SelectCommand.Parameters.AddWithValue("@P_paymentTransactionId", paymentTransactionId);
        //        vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

        //        vadap.Fill(vds);
        //        if (connection.State == ConnectionState.Open)
        //            connection.Close();


        //        if (vds.Tables.Count > 0)
        //        {
        //            if (vds.Tables[0].Rows.Count > 0)
        //            {
        //                objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["Reg_id"]);
        //                objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
        //                objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
        //                objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
        //                objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
        //                objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
        //                objFeepaid.FeePaid = Convert.ToString(vds.Tables[0].Rows[0]["Fee_paid"]);
        //                objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
        //                objFeepaid.PaymentTransactionId = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
        //                objFeepaid.PaymentGateway = Convert.ToString(vds.Tables[0].Rows[0]["Payment_gateway"]);

        //                objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
        //                objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
        //                objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
        //                objFeepaid.FatherName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_father_name"]);


        //                objFeepaid.PaymentTrackingID = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
        //                objFeepaid.Transactiondate = Convert.ToString(vds.Tables[0].Rows[0]["Payment_Date"]);
        //                objFeepaid.OrderStatus = Convert.ToString(vds.Tables[0].Rows[0]["order_status"]);
        //                //objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
        //                objFeepaid.PaymentMode = Convert.ToString(vds.Tables[0].Rows[0]["payment_mode"]);
        //                objFeepaid.billing_state = Convert.ToString(vds.Tables[0].Rows[0]["Installment_no"]);

        //                objFeepaid.order_id = Convert.ToString(vds.Tables[0].Rows[0]["orderid"]);
        //                objFeepaid.Bank_ref_no = Convert.ToString(vds.Tables[0].Rows[0]["bank_ref_no"]);
        //                objFeepaid.CancelAdmission = Convert.ToString(vds.Tables[0].Rows[0]["cancelRemarks"]);
        //                objFeepaid.Challan_status = Convert.ToString(vds.Tables[0].Rows[0]["Challan_status"]);
        //                objFeepaid.TotalFee = Convert.ToInt32(vds.Tables[0].Rows[0]["TotalFee"]);
        //                objFeepaid.Concession = Convert.ToInt32(vds.Tables[0].Rows[0]["Concession"]);
        //                objFeepaid.PendingFee = Convert.ToInt32(vds.Tables[0].Rows[0]["pendingFee"]);
        //                objFeepaid.CollegeType = Convert.ToString(vds.Tables[0].Rows[0]["collegetype"]);
        //                objFeepaid.FeePaidNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.FeePaid));

        //                objFeepaid.TotalFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.TotalFee));
        //                objFeepaid.ConcessionNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.Concession));
        //                objFeepaid.PendingFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.PendingFee));
        //                objFeepaid.QtrNo = Convert.ToString(vds.Tables[0].Rows[0]["QTR_NO"]);
        //            }
        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidatePaymentSuccesDetailOldSession()" + registrationid);
        //    }
        //    if (connection.State == ConnectionState.Open)
        //    {
        //        connection.Close();
        //    }
        //    return objFeepaid;
        //}

        public DataSet CheckRemainingRegFee(string RegID)
        {
            DataSet dt = new DataSet();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                MySqlCommand cmd = new MySqlCommand("GetRegistrationFeeITI", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("P_Reg_id", RegID);
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.CheckRemainingRegFee()" + RegID);
            }
            connection.Close();
            return dt;

        }



        public FeeViewModel GetAdmissionSession()
        {
            List<AdmissionSession> objAdmissionSession = new List<AdmissionSession>();
            FeeViewModel objAdmissionSessionViewModel = new FeeViewModel();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataTable dt = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("d_getSessionForQtrFee", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(dt);
                if (connection.State == ConnectionState.Open)
                    connection.Close();
                objAdmissionSession = (from DataRow row in dt.Rows
                                       select new AdmissionSession
                                       {
                                           Year = Convert.ToString(row["session_year"]),
                                           SessionID = Convert.ToString(row["session_id"]),
                                       }).ToList();

                objAdmissionSessionViewModel.candidateAdmissionSession = objAdmissionSession;
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetAdmissionSession()");
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objAdmissionSessionViewModel;
        }
        #endregion


    }


}