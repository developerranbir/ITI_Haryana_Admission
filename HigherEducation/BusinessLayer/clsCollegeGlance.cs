using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;

namespace HigherEducation.BusinessLayer
{

    public class clsCollegeGlance
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());

        public string distcode { get; set; }
        public string associatedwith { get; set; }
        public string collegeid { get; set; }
        public string website { get; set; }
        public string emailid { get; set; }
        public string contactno { get; set; }
        public string principalname { get; set; }
        public string principal_phoneno { get; set; }
        public string address { get; set; }
        public string NodalAdmissions { get; set; }
        public string NodalAdmissions_PhoneNo { get; set; }
        public string CordArts { get; set; }
        public string CordArts_PhoneNo { get; set; }
        public string CordComm { get; set; }
        public string CordComm_PhoneNo { get; set; }
        public string CordSc { get; set; }
        public string CordSc_PhoneNo { get; set; }
        public string CordJob { get; set; }
        public string CordJob_PhoneNo { get; set; }
        public string CordFee { get; set; }
        public string CordFee_PhoneNo { get; set; }
        public string MainAttract { get; set; }
        public string Prospectus { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string CollegeType { get; set; }
        public string EduMode { get; set; }
        public string bank_id { get; set; }
        public string branchname { get; set; }
        public string ifsc_code { get; set; }
        public string bankaccount { get; set; }
        public string ExServiceman { get; set;}
        public string DeafDumb { get; set; }
        public string PPP { get; set; }

        //BindDistrict
        public DataTable BindDistrict()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("BindDistrict", vconnHE);
                vconnHE.Open();
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeGlance";
                clsLogger.ExceptionMsg = "BindDistrict";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        public DataTable BindCollegeType()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("BindCollegeType", vconnHE);
                vconnHE.Open();
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                clsLogger.ExceptionMsg = "BindCollegeType";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        //Bind College
        public DataTable GetCollege()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollege", vconnHE);
                vconnHE.Open();
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                clsLogger.ExceptionMsg = "GetCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        //Bind Bank
        public DataTable BindBank()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("BindBank", vconnHE);
                vconnHE.Open();
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                clsLogger.ExceptionMsg = "BindBank";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable GetCollegeProfile()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollegeProfile", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", collegeid);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                clsLogger.ExceptionMsg = "GetCollegeProfile";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;

        }


        public string UpdateCollegeGlance()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateCollegeGlance", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_distcode", distcode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_website", website);
                vadap.SelectCommand.Parameters.AddWithValue("@p_emailid", emailid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_contactno", contactno);
                vadap.SelectCommand.Parameters.AddWithValue("@p_principalname", principalname);
                vadap.SelectCommand.Parameters.AddWithValue("@p_principalname_phoneno", principal_phoneno);
                vadap.SelectCommand.Parameters.AddWithValue("@p_address", address);
                vadap.SelectCommand.Parameters.AddWithValue("@p_NodalAdmissions", NodalAdmissions);
                vadap.SelectCommand.Parameters.AddWithValue("@p_NodalAdmissions_PhoneNo", NodalAdmissions_PhoneNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordArts", CordArts);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordArts_PhoneNo", CordArts_PhoneNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordComm", CordComm);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordComm_PhoneNo", CordComm_PhoneNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordSc", CordSc);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordSc_PhoneNo", CordSc_PhoneNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordJob", CordJob);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordJob_PhoneNo", CordJob_PhoneNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordFee", CordFee);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CordFee_PhoneNo", CordFee_PhoneNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MainAttract", MainAttract);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CollegeType", CollegeType);
                vadap.SelectCommand.Parameters.AddWithValue("@p_EduMode", EduMode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ExServiceman", ExServiceman);
                vadap.SelectCommand.Parameters.AddWithValue("@p_DeafDumb", DeafDumb);
                vadap.SelectCommand.Parameters.AddWithValue("@p_PPP", PPP);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    // result = vds.Tables[0].Rows[0]["success"].ToString();
                    result = vds.Tables[0].Rows[0]["Result"].ToString();
                    if (result == "0")
                    {
                        clsLogger.ExceptionError = vds.Tables[0].Rows[0]["errorMsg"].ToString();
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                        clsLogger.ExceptionMsg = "UpdateCollegeGlance";
                        clsLogger.SaveException();
                    }
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                clsLogger.ExceptionMsg = "UpdateCollegeGlance";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }

        public string UploadProspectus()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UploadProspectus", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", collegeid);

                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);

                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Prospectus", Prospectus);

                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    // result = vds.Tables[0].Rows[0]["success"].ToString();
                    result = vds.Tables[0].Rows[0]["Result"].ToString();
                    if (result == "0")
                    {
                        clsLogger.ExceptionError = vds.Tables[0].Rows[0]["errorMsg"].ToString();
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                        clsLogger.ExceptionMsg = "UploadProspectus";
                        clsLogger.SaveException();
                    }
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                clsLogger.ExceptionMsg = "UploadProspectus";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }

    }
    
}