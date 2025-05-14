using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Configuration;
using NLog;
using HigherEducation.Models;


namespace HigherEducation.DataAccess
{
    public class VerificationDbContext
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
                MySqlCommand cmd = new MySqlCommand("v_getStudentList", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext. getStudentList()");
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
                MySqlCommand cmd = new MySqlCommand("v_getPersonalDetails", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext. getPersonalData()");
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
        public DataSet SaveVerificationData(String Reg_Id, String Div_Id, String Remaks, String Verification_Status, String Acceptance_Status)
        {
            DataSet dt = new DataSet();

            try
            {
                MySqlCommand cmd = new MySqlCommand("RSUpdateCandidateVer", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("PRegistrationId", Reg_Id);
                cmd.Parameters.AddWithValue("PDivId", Div_Id);
                cmd.Parameters.AddWithValue("PVerificationStatus", Verification_Status);
                cmd.Parameters.AddWithValue("PWeightageStatusFromVerifier", Acceptance_Status);
                cmd.Parameters.AddWithValue("PVerifierRemarks", Remaks);
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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.[HttpPost] SaveVerificationData()");

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

        public DataSet getStudentObjList(string CollegeId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("v_getStudentObjList", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.getStudentObjList()");
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
                MySqlCommand cmd = new MySqlCommand("v_getStudentObjRemList", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.getStudentObjRemList()");
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
                MySqlCommand cmd = new MySqlCommand("v_getStudentVerifiedList", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.getStudentVerifiedList()");
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
        public DataSet getCourseDetails(string Reg_Id)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("v_getCourseDetails", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("P_Reg_Id", Reg_Id);
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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.getCourseDetails()");
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
        public DataSet SaveFinalVerificationData(String Reg_Id, String Verification_Status, String IPaddress)
        {
            DataSet dt = new DataSet();

            try
            {
                MySqlCommand cmd = new MySqlCommand("RSFinalUpdateCandidateVer", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("PRegistrationId", Reg_Id);
                cmd.Parameters.AddWithValue("PVerificationStatus", Verification_Status);
                cmd.Parameters.AddWithValue("P_Verifier", HttpContext.Current.Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("P_CollegeId", Convert.ToInt32(HttpContext.Current.Session["collegeId"]));
                cmd.Parameters.AddWithValue("P_IPAddress", IPaddress);

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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.SaveFinalVerificationData()");
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

        public DataSet GetSchoolList()
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("v_getSchoolListXII", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 120;

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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.GetSchoolList()");
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
                MySqlCommand cmd = new MySqlCommand("v_getActionHistory", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.GetActionHistory()");
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

        public DataTable v_GetDocs(String Reg_Id, String DocId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("select Docs from uploaddocument where Reg_Id = @RegId and DocId = @DocId", connection_ReadOnly)
                {

                    CommandType = CommandType.Text
                };
                cmd.Parameters.AddWithValue("@RegId", Reg_Id);
                cmd.Parameters.AddWithValue("@DocId", DocId);
                cmd.CommandTimeout = 120;

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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.v_GetDocs()");
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
        public DataTable v_getVerificationQuestions(String Reg_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("v_getVerificationQuestions", connection_ReadOnly)
                {

                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@RegId", Reg_Id);
                cmd.CommandTimeout = 120;

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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.v_GetDocs()");
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
        public DataTable v_showCandidateseats(String Reg_Id, string collegeid)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("v_showCandidateseats", connection_ReadOnly)
                {

                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@P_RegId", Reg_Id);
                cmd.Parameters.AddWithValue("@P_collegeId", collegeid);
                cmd.Parameters.AddWithValue("@P_counselling", ConfigurationManager.AppSettings["Counselling"].ToString());
                cmd.CommandTimeout = 120;

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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.v_showCandidateseats(String Reg_Id)");
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

        public DataTable v_submitVerification(VerificationModel vm1)
        {

            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("v_saveFinalVerificationITI", connection)
                {

                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@P_regid", vm1.reg_id);
                cmd.Parameters.AddWithValue("@P_pers_1status", vm1.personal_verified);
                cmd.Parameters.AddWithValue("@P_edu_2status", vm1.education_verified);
                cmd.Parameters.AddWithValue("@P_res_3status", vm1.reservation_verified);
                cmd.Parameters.AddWithValue("@P_cast_4status", vm1.caste_verified);
                cmd.Parameters.AddWithValue("@P_domi_5status", vm1.domicile_verified);
                cmd.Parameters.AddWithValue("@P_widoph_6status", vm1.widow_orphan_verified);
                cmd.Parameters.AddWithValue("@P_panch_7status", vm1.panchyat_verified);
                cmd.Parameters.AddWithValue("@P_grad_8status", vm1.higher_edu_grad12_verified);
                cmd.Parameters.AddWithValue("@P_pers_1remarks", vm1.personal_remarks);
                cmd.Parameters.AddWithValue("@P_edu_2remarks", vm1.education_remarks);
                cmd.Parameters.AddWithValue("@P_res_3remarks", vm1.reservation_remarks);
                cmd.Parameters.AddWithValue("@P_cast_4remarks", vm1.caste_remarks);
                cmd.Parameters.AddWithValue("@P_domi_5remarks", vm1.domicile_remarks);
                cmd.Parameters.AddWithValue("@P_widoph_6remarks", vm1.widow_orphan_remarks);
                cmd.Parameters.AddWithValue("@P_panch_7remarks", vm1.panchyat_remarks);
                cmd.Parameters.AddWithValue("@P_grad_8remarks", vm1.higher_edu_grad12_remarks);
                cmd.Parameters.AddWithValue("@P_FinalVerification", vm1.final_verification);
                cmd.Parameters.AddWithValue("@P_collegeid", vm1.collegeid);
                cmd.Parameters.AddWithValue("@P_userid", vm1.userid);
                cmd.Parameters.AddWithValue("@P_IP_address", vm1.ipaddress);
                
                
                cmd.Parameters.AddWithValue("@P_merit_id", vm1.OfferSeat.Split('|')[0]);
                cmd.Parameters.AddWithValue("@P_counselling", vm1.OfferSeat.Split('|')[1]);
                cmd.Parameters.AddWithValue("@P_Section", vm1.OfferSeat.Split('|')[2]);
                cmd.Parameters.AddWithValue("@P_EligibleForPMSBenefits", vm1.EligibleForPMSBenefits);

                cmd.CommandTimeout = 120;

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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.v_saveFinalVerificationITI()");
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

        public DataTable CheckVerifyOrNot(String Reg_Id, string collegeId)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("v_CheckIsVerified", connection_ReadOnly)
                {

                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("P_RegId", Reg_Id);
                cmd.Parameters.AddWithValue("P_collegeId", collegeId);
                cmd.Parameters.AddWithValue("P_Counselling", ConfigurationManager.AppSettings["Counselling"].ToString());
                //adding this line to ensure the correct Counselling is sent to back end from web.config

                cmd.CommandTimeout = 120;

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
                logger.Error(ex, "Error occured in HigherEducation.VerificationDBContext.CheckVerifyOrNot()");
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
        
    


        
    }

}