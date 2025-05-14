using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HigherEducation.BusinessLayer
{
    public class CandidateDetails
    {

        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        public string SrNo { get; set; }
        public string P_Id { get; set; }
        public string RollNo { get; set; }
        public string RegId { get; set; }
        public string CandidateName { get; set; }
        public string FatherName { get; set; }
        public string motherName { get; set; }
        public string MobileNo { get; set; }
        public string Qualification { get; set; }
        public string quaRegId { get; set; }

        public string ExamPassed { get; set; }
        public string RegistrationRollno { get; set; }
        public string MarksObt { get; set; }
        public string Percentage { get; set; }
        public string MaxMarks { get; set; }
        public string DOB { get; set; }
        public Int32 CountCount   { get; set; }

        public string Candidate { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string checkDOB { get; set; }
        public string LblName { get; set; }
        public string LblFName { get; set; }
        public string Lblmother { get; set; }
        public string LblDOB2 { get; set; }
        public string CollegeStatus { get; set; }
        
        public string NCVTMO { get; set; }
        public string NCVTEmail { get; set; }
       
        public DataTable GetCandidateInfo()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetDetails", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_RegNO", RegId);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/CandidateDetails";
                clsLogger.ExceptionMsg = "GetOfferingStudentInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //public string UpdateCandidate()
        //{
        //    string result = "";
        //    DataSet vds = new DataSet();
        //    try
        //    {
        //        if (vconnHE.State == ConnectionState.Open)
        //        {
        //            vconnHE.Close();
        //        }
        //        MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateCandidate", vconnHE);
        //        vconnHE.Open();
        //        vadap.SelectCommand.Parameters.AddWithValue("@P_RegNO", RegId);
        //        vadap.SelectCommand.Parameters.AddWithValue("@p_CandidateName", CandidateName);
        //        vadap.SelectCommand.Parameters.AddWithValue("@p_FatherName", FatherName);
        //        vadap.SelectCommand.Parameters.AddWithValue("@p_motherName", motherName);
        //        vadap.SelectCommand.Parameters.AddWithValue("@p_MobileNo", MobileNo);
        //        vadap.SelectCommand.Parameters.AddWithValue("@p_Address", address);

        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        vadap.Fill(vds);
        //        if (vconnHE.State == ConnectionState.Open)
        //            vconnHE.Close();
        //        if (vds.Tables.Count > 0)
        //        {
        //            result = vds.Tables[0].Rows[0]["success"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsLogger.ExceptionError = ex.Message;
        //        clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
        //        clsLogger.ExceptionMsg = "UpdateCollege";
        //        clsLogger.SaveException();
        //    }
        //    if (vconnHE.State == ConnectionState.Open)
        //        vconnHE.Close();
        //    return result;
        //}
        public DataTable BindQualification()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("CandidateQuali", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_RegNO", quaRegId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "BindCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
    }
    public class GetPhoto
    {
        public string image { get; set; }
        public HttpPostedFileBase files { get; set; }
        public string DocsName { get; set; }
        public string RegId { get; set; }
        public string docid { get; set; }
        
    }
    public class registration
    {

    }
}