using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HigherEducation.BusinessLayer
{
    public class clsFeeUpdate
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
        public string combinationid { get; set; }
        public string PaymentTransid { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        public string RFAF { get; set; }//Refund or Additional Fee

       

        //Get GetFeeAdmissionDetailInfo
        public DataTable GetFeeAdmissionDetailInfo()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeAdmissionDetailInfo", vconnHE_ReadOnly);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsFeeUpdate";
                clsLogger.ExceptionMsg = "GetFeeAdmissionDetailInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        //Update Fee

        public string UpdateFee()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateFee", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Regid", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_PaymentTransid", PaymentTransid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Amount", Amount);
                vadap.SelectCommand.Parameters.AddWithValue("@p_RFAF", RFAF);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["Result"].ToString();

                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsFeeUpdate";
                clsLogger.ExceptionMsg = "UpdateFee";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }
    }
}
