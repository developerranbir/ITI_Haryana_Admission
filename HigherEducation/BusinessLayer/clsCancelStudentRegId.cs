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

    public class clsCancelStudentRegId
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
        public DateTime ? BirthDate { get; set; }
        public string Collegeid { get; set; }
        public string FamilyId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string docs { get; set; }
        public string Sectionid { get; set; }
        public string PaymentTransactionID { get; set;}

        public string SLCBogus { get; set; }

        //PG**************************Start****************************************************************************
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
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RollNo);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetStudentInfo_PG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        //Cancel Student Registration ID for PG
        public string CancelStudentRegId_PG()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("CancelStudentRegId_PG", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
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
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                        clsLogger.ExceptionMsg = "CancelStudentRegId_PG";
                        clsLogger.SaveException();
                    }
                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "CancelStudentRegId_PG";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }
        //Used for Revoke Verification for PG
        public DataTable GetVerifierStudentInfo_PG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetVerifierStudentInfo_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RollNo);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetVerifierStudentInfo_PG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        public string VerificationRevoked_PG()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("VerificationRevoked_PG", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
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
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                        clsLogger.ExceptionMsg = "VerificationRevoked_PG";
                        clsLogger.SaveException();
                    }
                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "VerificationRevoked_PG";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }

        //Get Action History
        public DataTable GetActionHistory_PG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("vPG_getActionHistory", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@PRegistrationId", RollNo);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetActionHistoryPG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Get GetAdmissionDetailInfo PG
        public DataTable GetAdmissionDetailInfoPG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetAdmissionDetailInfo_PG", vconnHE_ReadOnly);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetAdmissionDetailInfoPG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Get Cancel Admissions PG
        public DataTable GetCancelAdmissionsPG()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCancelAdmissions_PG", vconnHE_ReadOnly);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetCancelAdmissionsPG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Cancel Student Admission PG
        public string CancelAdmissionsPG()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }

                MySqlDataAdapter vadap = new MySqlDataAdapter("CancelAdmissions_PG", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MobileNo", MobileNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_EmailId", Email);
                vadap.SelectCommand.Parameters.AddWithValue("@p_docs", docs);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Sectionid", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_PaymentTransactionID", PaymentTransactionID);
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
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                        clsLogger.ExceptionMsg = "CancelStudentAdmissionPG";
                        clsLogger.SaveException();
                    }
                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "CancelStudentAdmissionPG";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }



        //PG*********************END**********************************************************PG


        //UG***************************************START***************************************UG
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
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RollNo);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetStudentInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        public DataTable GetStudentCancelRecords()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetStudentCancelRecords", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RollNo);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetStudentCancelRecords";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Used For Student Detail Rollno,Name,DateOfBirth Wise
        public DataTable GetStudentDetail()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetStudentDetail", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RollNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_dob", BirthDate);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetStudentDetail";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        //Used for Revoke Verification
        public DataTable GetVerifierStudentInfo()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetVerifierStudentInfo", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RollNo", RollNo);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetVerifierStudentInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

        //Used for Update Family ID
        public DataTable GetStudentDetailByRegId()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetStudentDetailByRegId", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_dob", BirthDate);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetStudentDetailByRegId";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Used for Know Student Status
        public DataTable GetStudentStatus()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetStudentStatus", vconnHE_ReadOnly);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetStudentStatus";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Get Action History
        public DataTable GetActionHistory()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("v_getActionHistory", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@PRegistrationId", RollNo);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetActionHistory";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }

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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetAdmissionDetailInfo", vconnHE_ReadOnly);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetAdmissionDetailInfo";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "GetCancelAdmissions";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;

        }
        //Write Connection String here
        public string CancelStudentRegId()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("CancelStudentRegId", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
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
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                        clsLogger.ExceptionMsg = "CancelStudentRegId";
                        clsLogger.SaveException();
                    }
                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "CancelStudentRegId";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }

        //Verification Revoked By College Id
        public string VerificationRevoked()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("VerificationRevoked", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
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
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                        clsLogger.ExceptionMsg = "VerificationRevoked";
                        clsLogger.SaveException();
                    }
                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "VerificationRevoked";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }
        //Update Family Id
        public string UpdateFamilyId()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateFamilyId", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_FamilyId", FamilyId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {

                    result = vds.Tables[0].Rows[0]["Result"].ToString();
                    if (string .IsNullOrEmpty(result)|| result == "0")
                    {
                        clsLogger.ExceptionError = vds.Tables[0].Rows[0]["errorMsg"].ToString();
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                        clsLogger.ExceptionMsg = "UpdateFamilyId";
                        clsLogger.SaveException();
                    }
                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "UpdateFamilyId";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }

        //Cancel Student Admission
        public string CancelAdmissions()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }

                MySqlDataAdapter vadap = new MySqlDataAdapter("CancelAdmissions", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegId", RegId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Name", Name);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Remarks", Remarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MobileNo", MobileNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_EmailId", Email);
                vadap.SelectCommand.Parameters.AddWithValue("@p_docs", docs);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SLCBogus", SLCBogus);
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
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                        clsLogger.ExceptionMsg = "CancelStudentAdmission";
                        clsLogger.SaveException();
                    }
                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCancelStudentRegId";
                clsLogger.ExceptionMsg = "CancelStudentAdmission";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }


        //Session Check
        public static bool CheckSession()
        {
            string UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
            string userid = Convert.ToString(HttpContext.Current.Session["UserId"]);
            string NewTab = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            if (NewTab == "" | NewTab == null)
            {
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/CustomErrPage.aspx", true);
                return false;
            }
            else
            {
                HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cache.SetNoServerCaching();
                HttpContext.Current.Response.CacheControl = "no-cache";
                HttpContext.Current.Response.Cache.SetNoStore();
                HttpContext.Current.Response.Expires = -1500;
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Expires = 0;
                string browser = HttpContext.Current.Request.Browser.Browser;
                if (browser == "IE")
                {
                    HttpContext.Current.Response.CacheControl = "No-Cache";
                }
                if (string.IsNullOrEmpty(userid))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", false);
                    return false;
                }
                else if (Convert.ToString(HttpContext.Current.Session["ForcePwdChange"]) != "y")
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/changepwd.aspx", false);
                    return false;
                }
                else if (Convert.ToString(HttpContext.Current.Request.Cookies["APSF"].Value) != Convert.ToString(HttpContext.Current.Session["APSF"]))
                {
                    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/CustomErrPage.aspx", false);
                    return false;
                }
                //else if ((Convert.ToString(HttpContext.Current.Request.Cookies["Ck1"].Value) != Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"])))
                //{
                //    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", false);
                //    return false;
                //}
                else
                {
                    StateBag ViewState = new StateBag();
                    Random objRandom = new Random();
                    ViewState.Add(Convert.ToString(objRandom.Next(1111, 8888)), 0);
                    return true;
                }
            }
        }


    }
    
}