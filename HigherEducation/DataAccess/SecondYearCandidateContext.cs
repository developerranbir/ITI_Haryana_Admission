using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace HigherEducation.DataAccess
{
    public class SecondYearCandidateContext
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        #region ConnectionString
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        static readonly string ROConStr = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);
        MySqlConnection connection_ReadOnly = new MySqlConnection(ROConStr);
        #endregion;

        public DataTable d_getAdmissionSession()
        {
            DataTable dt = new DataTable();
            var collegeId = "";
            if (HttpContext.Current.Session["collegeId"] != null)
            {
                collegeId = HttpContext.Current.Session["collegeId"].ToString();
            }
            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getSession", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getAdmissionSession()");
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


        public DataTable d_getCollegeType()
        {
            DataTable dt = new DataTable();
            var collegeId = "";
            if (HttpContext.Current.Session["collegeId"] != null)
            {
                collegeId = HttpContext.Current.Session["collegeId"].ToString();
            }
            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCollegeType", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_type", collegeId == null || collegeId == "" ? "0" : collegeId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getCollegeType()");
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

        public DataTable getCollegeListAsPerCollegeType(String collegeType,String sessionId)
        {
            DataTable dt = new DataTable();
            var collegeId = "";
            if (HttpContext.Current.Session["collegeId"] != null)
            {
                collegeId = HttpContext.Current.Session["collegeId"].ToString();
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand("GetCollegeAllotment_QTR", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("P_college_id", collegeId == null || collegeId == "" ? "0" : collegeId);
                cmd.Parameters.AddWithValue("P_college_type", collegeType);
                cmd.Parameters.AddWithValue("P_Year", sessionId == null || sessionId == "" ? "0" : sessionId);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.getCollegeListAsPerCollegeType()");
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

        public DataTable d_getCollegeNames(String sessionId)
        {
            DataTable dt = new DataTable();
            var collegeId = "";
            if (HttpContext.Current.Session["collegeId"] != null)
            {
                collegeId = HttpContext.Current.Session["collegeId"].ToString();
            }
            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCollegeNames_QTR", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_id", collegeId == null || collegeId == "" ? "0" : collegeId);
                cmd.Parameters.AddWithValue("P_Year", sessionId == null || sessionId == "" ? "0" : sessionId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getCollegeNames()");
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

        
        public DataSet d_updateStudentStatus(String CollegeId, String CourseId, String SectionId, String checkID)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_updateStatusSecondYear", connection_ReadOnly)
                {
                    CommandTimeout = 300,
                    CommandType = CommandType.StoredProcedure,
                };
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("p_Section_id", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("p_user", Convert.ToString(HttpContext.Current.Session["UserID"]));
                cmd.Parameters.AddWithValue("p_checkID", checkID);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_updateStudentStatus()");
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

        public DataSet d_getStudentDetailsListNO(String CollegeId, String CourseId, String SectionId, String sessionId)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_GetSecondYearCandidateDetails", connection_ReadOnly)
                {
                    CommandTimeout = 300,
                    CommandType = CommandType.StoredProcedure,
                };
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("p_Section_id", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("P_Year", Convert.ToInt32(sessionId == null || sessionId == "" ? "0" : sessionId));
                cmd.Parameters.AddWithValue("p_status", "NO");
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getStudentDetailsListNO()");
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

        public DataSet d_getStudentDetailsListYES(String CollegeId, String CourseId, String SectionId, String sessionId)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_GetSecondYearCandidateDetails", connection_ReadOnly)
                {
                    CommandTimeout = 300,
                    CommandType = CommandType.StoredProcedure,
                };
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("p_Section_id", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("P_Year", Convert.ToInt32(sessionId == null || sessionId == "" ? "0" : sessionId));
                cmd.Parameters.AddWithValue("p_status", "YES");
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getStudentDetailsListYES()");
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

        public DataSet d_getStudentDetailsListALL(String CollegeId, String CourseId, String SectionId, String sessionId)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_GetSecondYearCandidateDetails", connection_ReadOnly)
                {
                    CommandTimeout = 300,
                    CommandType = CommandType.StoredProcedure,
                };
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("p_Section_id", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("P_Year", Convert.ToInt32(sessionId == null || sessionId == "" ? "0" : sessionId));
                cmd.Parameters.AddWithValue("p_status", "ALL");
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getStudentDetailsListALL()");
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

        public DataTable d_getCourseNameAsPerCollege(String collegeId,String sessionId)
        {
            DataTable dt = new DataTable();

            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetTradebyITI_QTR", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_id", Convert.ToInt32(collegeId == null || collegeId == "" ? "0" : collegeId));
                cmd.Parameters.AddWithValue("P_Year", Convert.ToInt32(sessionId == null || sessionId == "" ? "0" : sessionId));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                // connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getCourseNameAsPerCollege()");
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

        public DataTable D_GetSectionList(String CollegeId, String CourseId, String SessionId)
        {
            DataTable dt = new DataTable();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("D_GetSectionList_QTR", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("P_Year", Convert.ToInt32(SessionId == null || SessionId == "" ? "0" : SessionId));

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.D_GetSectionList()");
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

        public DataSet d_getStudentLoginDetails(String CollegeId, String CourseId, String SectionId)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_GetSecondYearCandidateLoginDetails", connection_ReadOnly)
                {
                    CommandTimeout = 300,
                    CommandType = CommandType.StoredProcedure,
                };
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("p_Section_id", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getStudentLoginDetails()");
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

        public DataSet d_getFeeReceiptListQuarterly(String collegeType,String CollegeId, String CourseId, String SectionId, String Qtr, String SessionId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getFeeReceiptList_Qtr", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeType", Convert.ToInt32(collegeType == null || collegeType == "" ? "0" : collegeType));
                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("PQtr", Convert.ToInt32(Qtr == null || Qtr == "" ? "0" : Qtr));
                cmd.Parameters.AddWithValue("P_Year", Convert.ToInt32(SessionId == null || SessionId == "" ? "0" : SessionId));


                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.SecondYearCandidateContext.d_getFeeReceiptListQuarterly()");
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