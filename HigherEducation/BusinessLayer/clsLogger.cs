using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using HigherEducation.HigherEducation;
using HigherEducation.Models;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for clsLogger
/// </summary>
namespace HigherEducation.BusinessLayer
{
    public static class clsLogger
    {
        private static string _ExceptionError;
        public static string ExceptionError
        {
            get { return _ExceptionError; }
            set { _ExceptionError = value; }
        }
        private static string _ExceptionMsg;
        public static string ExceptionMsg
        {
            get { return _ExceptionMsg; }
            set { _ExceptionMsg = value; }
        }
        private static string _ExceptionPage;
        public static string ExceptionPage
        {
            get { return _ExceptionPage; }
            set { _ExceptionPage = value; }
        }
        private static string _ExceptionDetail;
        public static string ExceptionDetail
        {
            get { return _ExceptionDetail; }
            set { _ExceptionDetail = value; }
        }
        private static DateTime? _ExceptionDate;
        public static DateTime? ExceptionDate
        {
            get { return _ExceptionDate; }
            set { _ExceptionDate = value; }
        }
        private static string _ExceptionDuration;
        public static string ExceptionDuration
        {
            get { return _ExceptionDuration; }
            set { _ExceptionDuration = value; }
        }

        static string vconnHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        static string vconnHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        static clsLogger()
        {
            _ExceptionError = null;
            _ExceptionPage = null;
            _ExceptionMsg = null;
            _ExceptionDetail = null;
        }
        private static void ResetException()
        {
            clsLogger.ExceptionError = null;
            clsLogger.ExceptionPage = null;
            clsLogger.ExceptionMsg = null;
            clsLogger.ExceptionDetail = null;
        }
        public static void SaveException()
        {
            MySqlConnection con= new MySqlConnection(vconnHE);
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (_ExceptionError != "Thread was being aborted.")
                {
                    //using (con = new MySqlConnection(vconnHE))
                   // {
                        using (MySqlCommand cmd = new MySqlCommand("RS_SaveTraceError", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            con.Open();
                            cmd.Parameters.AddWithValue("@p_ExceptionError", _ExceptionError);
                            cmd.Parameters.AddWithValue("@p_ExceptionPage", _ExceptionPage);
                            cmd.Parameters.AddWithValue("@p_ExceptionMsg", _ExceptionMsg);
                            cmd.Parameters.AddWithValue("@p_ExceptionDetail", _ExceptionDetail);
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                       // }
                    }

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    
                }
                ResetException();
            }
            catch (Exception ex)
            {
                string ex_str = string.Empty;
                ex_str = ex.Message;
            }
        }
        public static DataSet GetAllError()
        {
            DataSet dt = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@ExceptionPage", _ExceptionPage);
                p[1] = new SqlParameter("@ExceptionMsg", _ExceptionMsg);
                p[2] = new SqlParameter("@ExceptionDate", _ExceptionDate);
                p[3] = new SqlParameter("@ExceptionDuration", _ExceptionDuration);
                // dt = SQLHelper_SP.SQLHelper_SP.ExecuteDataset(HE_constring, CommandType.StoredProcedure, "RS_GetAllError", p);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        public static DataSet DeleteError()
        {
            DataSet dt = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@ExceptionPage", _ExceptionPage);
                p[1] = new SqlParameter("@ExceptionMsg", _ExceptionMsg);
                p[2] = new SqlParameter("@ExceptionDate", _ExceptionDate);
                p[3] = new SqlParameter("@ExceptionDuration", _ExceptionDuration);
                // SQLHelper_SP.SQLHelper_SP.ExecuteNonQuery(HE_constring, CommandType.StoredProcedure, "RS_DeleteError", p);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        public static DataSet GetErrorMsg()
        {
            DataSet dt = new DataSet();
            try
            {
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@ExceptionPage", _ExceptionPage);
                //  dt = SQLHelper_SP.SQLHelper_SP.ExecuteDataset(VC_ReadOnly, CommandType.StoredProcedure, "RS_GetAllErrorMsg", p);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public static DataSet GetErrorPage()
        {
            DataSet dt = new DataSet();
            try
            {
                // dt = SQLHelper_SP.SQLHelper_SP.ExecuteDataset(VC_ReadOnly, CommandType.StoredProcedure, "RS_GetAllErrorPage");
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
    }

}