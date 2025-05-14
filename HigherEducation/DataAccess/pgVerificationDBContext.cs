using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
using NLog;

namespace HigherEducation.DataAccess
{
    public class pgVerificationDBContext
    {
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        static readonly string ROConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;  // change as per requirement 
        MySqlConnection connection = new MySqlConnection(ConStr);
        readonly MySqlConnection connection_ReadOnly = new MySqlConnection(ROConStr);
        Logger logger = LogManager.GetCurrentClassLogger();

        // Sets the data in leftpane of Verification Module as per College ID
        public DataSet getStudentList(Int32 CollegeId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("vPG_getStudentList", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("P_College_Id", CollegeId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext. getPgStudentList()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }

            return dt;

        }

        public DataSet getPersonalData(string RegID)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("vPG_getPersonalDetails", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("P_Reg_Id", RegID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext. getPgPersonalData()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }

            }
            return dt;

        }
        public DataSet getStudentObjList(string CollegeId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("vPG_getStudentObjList", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("P_College_Id", CollegeId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.getPgStudentObjList()");
            }

            finally
            {

                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;

        }
        public DataSet getStudentObjRemList(string CollegeId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("vPG_getStudentObjRemList", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("P_College_Id", CollegeId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);


                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.getPgStudentObjRemList()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }

            }
            return dt;

        }
        public DataSet getStudentVerifiedList(string CollegeId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("vPG_getStudentVerifiedList", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("P_College_Id", CollegeId);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);


                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.getPgStudentVerifiedList()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }

            }
            return dt;

        }
        public DataSet GetActionHistory(String Reg_Id)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("vPG_getActionHistory", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,


                };

                cmd.Parameters.AddWithValue("PRegistrationId", Reg_Id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.GetPgActionHistory()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }

            return dt;

        }
        public DataSet SaveVerificationData(String Reg_Id, String Div_Id, String Remaks, String Verification_Status,String Remarks2)
        {
            DataSet dt = new DataSet();

            try
            {
                MySqlCommand cmd = new MySqlCommand("RSPGUpdateCandidateVer", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("PRegistrationId", Reg_Id);
                cmd.Parameters.AddWithValue("PDivId", Div_Id);
                cmd.Parameters.AddWithValue("PVerificationStatus", Verification_Status);
                cmd.Parameters.AddWithValue("PVerifierRemarks", Remaks);

                cmd.Parameters.AddWithValue("PVerifierRemarks2", Remarks2);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.[HttpPost] PGSaveVerificationData()");

            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dt;

        }
        public DataSet SaveFinalVerificationData(String Reg_Id, String Verification_Status, String IPaddress,String FinalWeightageMarkedByCollege)
        {
            DataSet dt = new DataSet();

            try
            {
                MySqlCommand cmd = new MySqlCommand("RSPGFinalUpdateCandidateVer", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("PRegistrationId", Reg_Id);
                cmd.Parameters.AddWithValue("PVerificationStatus", Verification_Status);
                cmd.Parameters.AddWithValue("P_Verifier", HttpContext.Current.Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("P_CollegeId", Convert.ToInt32(HttpContext.Current.Session["collegeId"]));
                cmd.Parameters.AddWithValue("P_IPAddress", IPaddress);
                cmd.Parameters.AddWithValue("P_FinalWeightageMarkedByCollege", Convert.ToInt32(FinalWeightageMarkedByCollege));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.PG_SaveFinalVerificationData()");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dt;

        }

    }
}