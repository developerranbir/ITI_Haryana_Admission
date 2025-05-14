using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HigherEducation.DataAccess
{
    public class CondidateDetailsITIWiseContext
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        #region ConnectionString
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        static readonly string ROConStr = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);
        MySqlConnection connection_ReadOnly = new MySqlConnection(ROConStr);
        #endregion;
        public DataTable getCollegeListAsPerCollegeType(String collegeType)
        {
            DataTable dt = new DataTable();
            var collegeId = "";
            if (HttpContext.Current.Session["collegeId"] != null)
            {
                collegeId = HttpContext.Current.Session["collegeId"].ToString();
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand("GetCollegeAllotment", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("P_college_id", collegeId == null || collegeId == "" ? "0" : collegeId);
                cmd.Parameters.AddWithValue("P_college_type", collegeType);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCollegeNamesFromCollegeType()");
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
        public DataTable GetTradeListByITI(String collegeId)
        {
            DataTable dt = new DataTable();

            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetTradebyITI", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_id", Convert.ToInt32(collegeId == null || collegeId == "" ? "0" : collegeId));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                // connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCourseNameAsPerCollegeUGPG()");
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
        public DataSet GetSectionList(String CollegeId, String CourseId)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetSectionListbyITI", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.D_GetSectionListUGPG()");
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
        public DataSet getStudentDetailsList(String CollegeId, String CourseId, String SectionId)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetCondidateDetalsITIWise", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("P_Section", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getFeeReceiptList()");
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