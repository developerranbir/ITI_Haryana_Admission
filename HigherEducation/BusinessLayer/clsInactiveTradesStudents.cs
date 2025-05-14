using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;


namespace HigherEducation.BusinessLayer
{
    public class clsInactiveTradesStudents
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());

        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducationR"].ToString());


        public string pNew_collegeid { get; set; }
        //public string pNew_courseid { get; set; }
        public string pNew_coursesectionid { get; set; }
        public string pOld_collegeid { get; set; }
        //public string pOld_courseid { get; set; }
        public string pOld_coursesectionid { get; set; }
        public string p_IPAddress { get; set; }
        public string p_ChangeUser { get; set; }
        public DataTable getAllInactiveTradeStudentCountWithCollege()
        {
            DataTable dt = new DataTable();
            try
            {
           
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }


                MySqlDataAdapter vadap = new MySqlDataAdapter("GetInactiveClg", vconnHE_ReadOnly);
                
                vconnHE_ReadOnly.Open();
               

                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(dt);
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/InactiveTradesStudents";
                clsLogger.ExceptionMsg = "GetInactiveTradeStudents";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return dt;
        }

        public DataTable getAllInactiveTradeSection(int collegID)
        {
            DataTable dt = new DataTable();
            try
            {

                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }


                MySqlDataAdapter vadap = new MySqlDataAdapter("GetInactiveTradeSection", vconnHE_ReadOnly);
                vadap.SelectCommand.Parameters.AddWithValue("@P_collegeID", collegID);
                vconnHE_ReadOnly.Open();

                
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(dt);
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/InactiveTradesStudents";
                clsLogger.ExceptionMsg = "InactiveTradesStudents";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return dt;
        }
        public DataTable GetActiveTradeSection(int collegID)
        {
            DataTable dt = new DataTable();
            try
            {

                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }


                MySqlDataAdapter vadap = new MySqlDataAdapter("GetActiveTradeSection", vconnHE_ReadOnly);
                vadap.SelectCommand.Parameters.AddWithValue("@P_collegeID", collegID);
                vconnHE_ReadOnly.Open();

                
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(dt);
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/InactiveTradesStudents";
                clsLogger.ExceptionMsg = "GetActiveTradeSection";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return dt;
        }


        public DataTable getAllActiveCollege()
        {
            DataTable dt = new DataTable();
            try
            {

                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                {
                    vconnHE_ReadOnly.Close();
                }


                MySqlDataAdapter vadap = new MySqlDataAdapter("getAllActiveCollege", vconnHE_ReadOnly);

                vconnHE_ReadOnly.Open();


                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(dt);
                if (vconnHE_ReadOnly.State == ConnectionState.Open)
                    vconnHE_ReadOnly.Close();
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/InactiveTradesStudents";
                clsLogger.ExceptionMsg = "getAllActiveCollege";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return dt;
        }
      

        public DataTable UpdateStuentPreference()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();

                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpatePreference", vconnHE_ReadOnly);
                vconnHE.Open();
           
                vadap.SelectCommand.Parameters.AddWithValue("@pNew_collegeid", pNew_collegeid);
                //vadap.SelectCommand.Parameters.AddWithValue("@pNew_courseid", pNew_courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@pNew_coursesectionid", pNew_coursesectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@pOld_collegeid", pOld_collegeid);
                //vadap.SelectCommand.Parameters.AddWithValue("@pOld_courseid", pOld_courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@pOld_coursesectionid", pOld_coursesectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ChangeUser", p_ChangeUser);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", p_IPAddress);
              

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
                clsLogger.ExceptionPage = "DHE/BusinessLayer/InactiveTradesStudents";
                clsLogger.ExceptionMsg = "UpatePreference";
                clsLogger.SaveException();
            }
            if (vconnHE_ReadOnly.State == ConnectionState.Open)
                vconnHE_ReadOnly.Close();
            return vds;
        }
    }
}