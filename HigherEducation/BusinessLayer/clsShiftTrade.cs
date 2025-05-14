using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HigherEducation.BusinessLayer
{
    public class clsShiftTrade
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());

        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducationR"].ToString());

        public string RegId { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string Collegeid { get; set; }
        public string Courseid { get; set; }
        public string Sectionid { get; set; }

        public string CourseName { get; set; }
        public string SectionName { get; set; }
        public string Payment_transactionId {get; set;}
        public string ITIName { get; set; }
        public string Remarks { get; set; }
        public string Counselling { get; set; }

        public string UserType { get; set; }
        public string ShiftCollegeid { get; set; }
        public string ShiftCollegeName { get; set; }

        public DataTable BindCourse()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCourse", vconnHE_ReadOnly);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", "12");
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
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsShiftTrade";
                clsLogger.ExceptionMsg = "BindCourse";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        //Get GetAdmissionDetailForShift
        public DataTable GetAdmissionDetailForShift()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetAdmissionDetailForShift", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_UserType", UserType);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsShiftTrade";
                clsLogger.ExceptionMsg = "GetAdmissionDetailForShift";
                clsLogger.SaveException();
            }
            finally
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
            }
            return vds;

        }
        // CourseSection_Bind
        public DataTable CourseSection_Bind()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("CourseSection_Bind", vconnHE_ReadOnly);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_courseid", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_collegeid", Collegeid);
               
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
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsPvtITIFeePaid";
                clsLogger.ExceptionMsg = "CourseSection_Bind";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        //Get Cancel Admissions
        public DataTable GetCancelAdmissions()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCancelAdmissions", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Regid", RegId);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsShiftTrade";
                clsLogger.ExceptionMsg = "GetCancelAdmissions";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        //Update ShiftTrade

        public string ShiftTrade()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("ShiftTrade", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ITIName", ITIName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_courseid", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_sectionid", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_coursename", CourseName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_sectionname", SectionName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Counselling", Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Payment_transactionId", Payment_transactionId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_UserType", UserType);
                vadap.SelectCommand.Parameters.AddWithValue("@P_ShiftCollegeid", ShiftCollegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_ShiftCollegeName",ShiftCollegeName);
                

                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
               
                    result = vds.Tables[0].Rows[0]["Result"].ToString();
                    if (result == "0")
                    {
                        clsLogger.ExceptionError = vds.Tables[0].Rows[0]["errorMsg"].ToString();
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsShiftTrade";
                        clsLogger.ExceptionMsg = "ShiftTrade";
                        clsLogger.SaveException();
                    }
                

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsShiftTrade";
                clsLogger.ExceptionMsg = "ShiftTrade";
                clsLogger.SaveException();
            }
            finally     
            {
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
            }
            return result;
        }
    }
}
