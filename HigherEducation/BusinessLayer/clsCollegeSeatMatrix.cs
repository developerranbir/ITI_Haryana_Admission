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

    public class clsCollegeSeatMatrix
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducationR"].ToString());

        public string Collegeid { get; set; }
        public string Courseid { get; set; }
        public string Sectionid { get; set; }
        public string Sessionid { get; set; }
        public string UserId { get; set; }
        public string CourseCombid { get; set; }
        public string OpenSeats { get; set; }
        public string HryGen { get; set; }
        public string SC { get; set; }
        public string DSC { get; set; }
        public string BCA { get; set; }
        public string BCB { get; set; }
        public string EcoWeaker { get; set; }
        public string TotalDA { get; set; }
        public string DAG { get; set; }
        public string DASC { get; set; }
        public string DABC { get; set; }
        public string ESMG { get; set; }
        public string ESMBCA { get; set; }
        public string ESMBCB { get; set; }
        public string ESMSC { get; set; }
        public string ESMDSC { get; set; }
        public string TotalSC { get; set; }
        public string TotalBC { get; set; }
        public string TotalHOGCEWS { get; set; }
        public string TotalESM { get; set; }
        public string IPAddress { get; set; }

        public string SchemeId { get; set; }

        //*****************************************PG Start***********************************
        //GetCollegeSeatMatrix PG
        public DataTable GetCollegeSeatMatrix_PG()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollegeSeatMatrix_PG", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                //vadap.SelectCommand.Parameters.AddWithValue("@p_Courseid", Courseid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "GetCollegeSeatMatrix_PG";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public string FreezeCollegeSeatMatrix_PG()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("FreezeCollegeSeatMatrix_PG", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();

                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "FreezeCollegeSeatMatrix_PG";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }

        //GegtVacantSeats PG
        public DataSet GetVacantSeatsPG()
        {
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetVacantSeats_PG", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "GetVacantSeatsPG";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;
        }
        //************************************PG END**************************************************

        //BindCollege
        public DataTable BindCollege()
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "BindCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        //GetCollegeSeatMatrix UG
        public DataTable GetCollegeSeatMatrix()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollegeSeatMatrix", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                //vadap.SelectCommand.Parameters.AddWithValue("@p_Courseid", Courseid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "GetCollegeSeatMatrix";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        //GegtVacantSeats
        public DataSet GetVacantSeats()
        {
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetVacantSeats", vconnHE_ReadOnly);
                vconnHE_ReadOnly.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "GetVacantSeats";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;
        }
        public DataTable BindCourse()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollegeCourse", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", Collegeid);

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "BindCourse";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        public DataTable GetCourseSeat()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCourseSeat", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_Id", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@Section_id", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", Collegeid);

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
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "GetCourseSeat";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        public DataTable BindSection()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSectionSubject", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@Course_Id", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", Collegeid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "BindSection";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        public string FreezeCollegeSeatMatrix()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("FreezeCollegeSeatMatrix", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();

                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "FreezeCollegeSeatMatrix";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }

        public string UpdateCollegeSeatMatrix()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateCollegeSeatMatrix", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_Collegeid", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Courseid", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Sectionid", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CourseCombid", CourseCombid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_OpenSeats", OpenSeats);

                vadap.SelectCommand.Parameters.AddWithValue("@p_HryGen", HryGen);
                vadap.SelectCommand.Parameters.AddWithValue("@p_EcoWeaker", EcoWeaker);
                vadap.SelectCommand.Parameters.AddWithValue("@p_TotalHOGCEWS", TotalHOGCEWS);

                vadap.SelectCommand.Parameters.AddWithValue("@p_SC", SC);
                vadap.SelectCommand.Parameters.AddWithValue("@p_DSC", DSC);
                vadap.SelectCommand.Parameters.AddWithValue("@p_TotalSC", TotalSC);

                vadap.SelectCommand.Parameters.AddWithValue("@p_BCA", BCA);
                vadap.SelectCommand.Parameters.AddWithValue("@p_BCB", BCB);
                vadap.SelectCommand.Parameters.AddWithValue("@p_TotalBC", TotalBC);
                
                vadap.SelectCommand.Parameters.AddWithValue("@p_DAG", DAG);
                vadap.SelectCommand.Parameters.AddWithValue("@p_DASC", DASC);
                vadap.SelectCommand.Parameters.AddWithValue("@p_DABC", DABC);
                vadap.SelectCommand.Parameters.AddWithValue("@p_TotalDA", TotalDA);

                vadap.SelectCommand.Parameters.AddWithValue("@p_ESMG", ESMG);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ESMBCA", ESMBCA);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ESMBCB", ESMBCB);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ESMSC", ESMSC);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ESMDSC", ESMDSC);
                vadap.SelectCommand.Parameters.AddWithValue("@p_TotalESM", TotalESM);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();

                }



            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "UpdateCollegeSeatMatrix";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }
        
        /* 
         * Code for Seat Matrix View and Update 
         */
        //BindScheme
        public DataTable BindScheme()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("getCategorySeatMatrix", vconnHE);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "BindScheme";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }


        //GetCollegeSeatMatrixView for Display Seat Bifurcation 
        public DataTable GetCollegeSeatMatrixView()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("getCollegeSeatMatrixView", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_SchemeId", SchemeId);
                //vadap.SelectCommand.Parameters.AddWithValue("@p_Courseid", Courseid);
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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "GetCollegeSeatMatrix";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        public void UpdateSeatMatrix(String lblid, String txtSeatSize, String txtOpenM, String txtOpenF, String txtESMGen, String txtBCAM, 
            String txtBCAF, String txtBCBM, String txtBCBF, String txtESMBCA, String txtSCM, String txtDepSCM, String txtSCF, 
            String txtDepSCF, String txtSCESM, String txtEWSM, String txtEWSF, String txtPH, String txttotalConfirm)
        {
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                using(MySqlCommand cmd = new MySqlCommand("Update_Seat_Matrix",vconnHE))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@seatsize", txtSeatSize);
                    cmd.Parameters.AddWithValue("@Open_M", txtOpenM);
                    cmd.Parameters.AddWithValue("@OPEN_F", txtOpenF);
                    cmd.Parameters.AddWithValue("@ESM_Gen", txtESMGen);
                    cmd.Parameters.AddWithValue("@BC_A_m", txtBCAM);
                    cmd.Parameters.AddWithValue("@BC_A_f", txtBCAF);
                    cmd.Parameters.AddWithValue("@BC_B_m", txtBCBM);
                    cmd.Parameters.AddWithValue("@BC_B_f", txtBCBF);
                    cmd.Parameters.AddWithValue("@ESM_BC_a", txtESMBCA);
                    cmd.Parameters.AddWithValue("@SC_M", txtSCM);
                    cmd.Parameters.AddWithValue("@Dep_SC_M", txtDepSCM);
                    cmd.Parameters.AddWithValue("@SC_F", txtSCF);
                    cmd.Parameters.AddWithValue("@Dep_SC_F", txtDepSCF);
                    cmd.Parameters.AddWithValue("@SC_ESM", txtSCESM);
                    cmd.Parameters.AddWithValue("@EWS_M", txtEWSM);
                    cmd.Parameters.AddWithValue("@EWS_F", txtEWSF);
                    cmd.Parameters.AddWithValue("@PH", txtPH);
                    cmd.Parameters.AddWithValue("@totalConfirm", txttotalConfirm);
                    cmd.Parameters.AddWithValue("@P_id", lblid);
                    vconnHE.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegeSeatMatrix";
                clsLogger.ExceptionMsg = "UpdateSeatMatrix";
                clsLogger.SaveException();
            }
            
        }


    }

}