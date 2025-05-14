using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HigherEducation.BusinessLayer
{
    public class clsResultUGAdm
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());

        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducationR"].ToString());

        public string RegId { get; set; }
        public string CandidateName { get; set; }
       // public string Candidate_FatherName { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string Collegeid { get; set; }
        public string Counselling { get; set; }

        //***********************************************PG Start*************************************************
        public DataTable GetCandidateInfo_PG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateInfo_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CandidateName", CandidateName);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsResultUGAdm";
                clsLogger.ExceptionMsg = "GetCandidateInfo_PG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        public DataTable GetStudentInfo_PG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetStudentInfo_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RegId);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsResultUGAdm";
                clsLogger.ExceptionMsg = "GetStudentInfo_PG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Get Payment Status for frmViewPaymentStatus (PG)
        public DataTable GetPaymentStatus_PG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("RSGetPaymentStatus_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@PReg_ID", RegId);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsResultUGAdm";
                clsLogger.ExceptionMsg = "GetPaymentStatus_PG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //**********************************************PG END****************************************************

        public DataTable GetCandidateInfo()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCandidateInfo", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CandidateName", CandidateName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Counselling", Counselling);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsResultUGAdm";
                clsLogger.ExceptionMsg = "GetCandidateInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        public DataTable GetpaymentSuccessDetail()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetpaymentSuccessDetail", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
               
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsResultUGAdm";
                clsLogger.ExceptionMsg = "GetpaymentSuccessDetail";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        //Get Payment Status for frmViewPaymentStatus
        public DataTable GetPaymentStatus()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("RSGetPaymentStatus", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@PReg_ID", RegId);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsResultUGAdm";
                clsLogger.ExceptionMsg = "GetPaymentStatus";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        public DataTable GetStudentInfo()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetStudentInfo", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RegId);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsResultUGAdm";
                clsLogger.ExceptionMsg = "GetStudentInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

    }
}