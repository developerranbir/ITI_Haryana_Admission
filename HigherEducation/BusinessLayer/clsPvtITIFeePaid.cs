using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HigherEducation.BusinessLayer
{
    public class clsPvtITIFeePaid
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
        public string Counselling { get; set; }


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
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", "11");
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
                clsLogger.ExceptionMsg = "BindCourse";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        //Get GetITIVerifyStudentInfo
        public DataTable GetITIVerifyStudentInfo()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetITIVerifyStudentInfo", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_Courseid", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_Counselling", Counselling);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsPvtITIFeePaid";
                clsLogger.ExceptionMsg = "GetITIVerifyStudentInfo";
                clsLogger.SaveException();
            }
            finally
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
            }
            return vds;

        }

        //Update Fee

        public string UpdatePvtITIFeePaid()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdatePvtITIFeePaid", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Counselling", Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("@P_CreateUser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
               
                    result = vds.Tables[0].Rows[0]["Result"].ToString();
                    if (result == "0")
                    {
                        clsLogger.ExceptionError = vds.Tables[0].Rows[0]["errorMsg"].ToString();
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsPvtITIFeePaid";
                        clsLogger.ExceptionMsg = "UpdatePvtITIFeePaid";
                        clsLogger.SaveException();
                    }
                

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsPvtITIFeePaid";
                clsLogger.ExceptionMsg = "UpdatePvtITIFeePaid";
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
