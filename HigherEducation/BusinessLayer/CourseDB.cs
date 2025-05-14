using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using HigherEducation.Models;
using MySql.Data.MySqlClient;
using HigherEducation.BusinessLayer;
using NLog;
using HigherEducation.BAL;

namespace HigherEducation.BusinessLayer
{
    public class CourseDB
    {
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        private LogP objlog = new LogP();
        Logger logger = LogManager.GetCurrentClassLogger();

        public List<FillDropdown> GetCourse(string connection)
        {
            try
            {
                List<FillDropdown> result = new List<FillDropdown>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCoursecollege", vconnHE);
                //vadap.SelectCommand.Parameters.AddWithValue("@CourseType", CourseType);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdown
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCourse()");
                return null;
            }
        }

        public List<FillDropdown> GetCoursePG(string connection)
        {
            try
            {
                List<FillDropdown> result = new List<FillDropdown>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCoursecollege_PG", vconnHE);
                //vadap.SelectCommand.Parameters.AddWithValue("@CourseType", CourseType);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdown
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCoursePG()");
                return null;
            }
        }
       
        public List<FillDropdown> GetSession(string connection)
        {
            try
            {
                List<FillDropdown> result = new List<FillDropdown>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSession", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdown
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetSession()");
                return null;
            }
        }

        public List<FillDropdown> GetCombinationSubjects(string connection)
        {
            try
            {
                List<FillDropdown> result = new List<FillDropdown>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCombinationSubjectMaster", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdown
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCombinationSubjects()");
                return null;
            }
        }

        public DataSet GetCombinationGroup(string connection, string Courseid, string Sectionid, string Sessionid)
        {
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSubjectGroup", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_Id", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@Section_Id", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@Session_id", Sessionid);
                //vadap.SelectCommand.Parameters.AddWithValue("@college_id", "114");
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@Puniversity_Id", HttpContext.Current.Session["PUniversityId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {

                }
                return vds;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCombinationGroup()");
                return null;
            }
        }

        public List<FillDropdown> GetSection(string connection, string Courseid)
        {
            try
            {
                List<FillDropdown> result = new List<FillDropdown>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSectionCollege", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_Id", Courseid);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdown
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetSection()");
                return null;
            }
        }

        public List<FillDropdown> GetCourseType(string connection)
        {
            try
            {
                List<FillDropdown> result = new List<FillDropdown>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCourseType", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegetype", HttpContext.Current.Session["CollegeType"]);//added by shweta
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdown
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCourseType()");
                return null;
            }
        }

        public List<FillDropdown> GetCourseTypePG(string connection)
        {
            try
            {
                List<FillDropdown> result = new List<FillDropdown>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCourseType_PG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegetype", HttpContext.Current.Session["CollegeType"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdown
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCourseTypePG()");
                return null;
            }
        }
       
        public DataSet GetCourseDetail(string connection, string SessionId)
        {
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCoursedata", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Session_Id", SessionId);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {

                }
                return vds;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCourseDetail()");
                return null;
            }
        }

        public DataSet GetCourseDetailPG(string connection, string SessionId)
        {
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCoursedata_PG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Session_Id", SessionId);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {

                }
                return vds;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCourseDetailPG()");
                return null;
            }
        }
      
        public string SaveCourse(string connection, SaveCourse saveCourse)
        {
            string MsgText = "";
            bool ret = true;
            Int32 resultcount = 0;
            string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            // declare rejex 
            Regex seatno = new Regex(@"^[0-9]{0,4}$");
            try
            {
                if (saveCourse.Session == "0" || saveCourse.Session == "" || saveCourse.Session == null)
                {
                    resultcount += 1;
                    MsgText += "Please Select Session." + Environment.NewLine;
                }
                if (saveCourse.CourseType == "0" || saveCourse.CourseType == "" || saveCourse.CourseType == null)
                {
                    resultcount += 1;
                    MsgText += "Please Select CourseType." + Environment.NewLine;
                }
                if (saveCourse.Course == "0" || saveCourse.Course == "" || saveCourse.Course == null)
                {
                    resultcount += 1;
                    MsgText += "Please Select Course." + Environment.NewLine;
                }
                if (saveCourse.Section == "0" || saveCourse.Section == "" || saveCourse.Section == null)
                {
                    resultcount += 1;
                    MsgText += "Please Select Section." + Environment.NewLine;
                }
                if (saveCourse.TotalSeat == "" || saveCourse.TotalSeat == null)
                {
                    resultcount += 1;
                    MsgText += "Please Enter Seat." + Environment.NewLine;
                }
                else//Regex Seat Check
                {
                    ret = seatno.IsMatch(saveCourse.TotalSeat);
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Invalid Seat Number." + Environment.NewLine;
                    }
                }
                if (resultcount > 0)
                {
                    return MsgText;
                }
                string result = "";
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCourseDetail", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_id", saveCourse.Course);
                vadap.SelectCommand.Parameters.AddWithValue("@section_name", saveCourse.Section);
                vadap.SelectCommand.Parameters.AddWithValue("@total_seat", saveCourse.TotalSeat);
                vadap.SelectCommand.Parameters.AddWithValue("@course_type", saveCourse.CourseType);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", saveCourse.Session);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SportSeat", saveCourse.SportSeat);//added by shweta
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);//added by shweta
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] SaveCourse()");
                return null;
            }
        }

        public string SaveCoursePG(string connection, SaveCourse saveCourse)
        {
            string MsgText = "";
            bool ret = true;
            Int32 resultcount = 0;
            // declare rejex 
            Regex seatno = new Regex(@"^[0-9]{0,4}$");
            try
            {
                if (saveCourse.Session == "0" || saveCourse.Session == "" || saveCourse.Session == null)
                {
                    resultcount += 1;
                    MsgText += "Please Select Session." + Environment.NewLine;
                }
                if (saveCourse.CourseType == "0" || saveCourse.CourseType == "" || saveCourse.CourseType == null)
                {
                    resultcount += 1;
                    MsgText += "Please Select CourseType." + Environment.NewLine;
                }
                if (saveCourse.Course == "0" || saveCourse.Course == "" || saveCourse.Course == null)
                {
                    resultcount += 1;
                    MsgText += "Please Select Course." + Environment.NewLine;
                }
                if (saveCourse.Section == "0" || saveCourse.Section == "" || saveCourse.Section == null)
                {
                    resultcount += 1;
                    MsgText += "Please Select Section." + Environment.NewLine;
                }
                if (saveCourse.TotalSeat == "" || saveCourse.TotalSeat == null)
                {
                    resultcount += 1;
                    MsgText += "Please Enter Seat." + Environment.NewLine;
                }
                else//Regex Seat Check
                {
                    ret = seatno.IsMatch(saveCourse.TotalSeat);
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Invalid Seat Number." + Environment.NewLine;
                    }
                }
                if (resultcount > 0)
                {
                    return MsgText;
                }
                string result = "";
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCourseDetail_PG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_id", saveCourse.Course);
                vadap.SelectCommand.Parameters.AddWithValue("@section_name", saveCourse.Section);
                vadap.SelectCommand.Parameters.AddWithValue("@total_seat", saveCourse.TotalSeat);
                vadap.SelectCommand.Parameters.AddWithValue("@course_type", saveCourse.CourseType);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", saveCourse.Session);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SportSeat", saveCourse.SportSeat);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] SaveCoursePG()");
                return null;
            }
        }
      
        public string UpdateCourse(string connection, SaveCourse saveCourse)
        {
            try
            {
                string result = "";
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("EditCourseDetail", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_id", saveCourse.Course);
                vadap.SelectCommand.Parameters.AddWithValue("@section_name", saveCourse.Section);
                vadap.SelectCommand.Parameters.AddWithValue("@total_seat", saveCourse.TotalSeat);
                vadap.SelectCommand.Parameters.AddWithValue("@course_type", saveCourse.CourseType);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", saveCourse.Session);
                vadap.SelectCommand.Parameters.AddWithValue("@Collegecourse_id", saveCourse.Collegecourseid);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SportSeat", saveCourse.SportSeat);//added by shweta
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);//added by shweta
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);//added by shweta
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] UpdateCourse()");
                return null;
            }
        }

        public string UpdateCoursePG(string connection, SaveCourse saveCourse)
        {
            try
            {
                string result = "";
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("EditCourseDetailPG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_id", saveCourse.Course);
                vadap.SelectCommand.Parameters.AddWithValue("@section_name", saveCourse.Section);
                vadap.SelectCommand.Parameters.AddWithValue("@total_seat", saveCourse.TotalSeat);
                vadap.SelectCommand.Parameters.AddWithValue("@course_type", saveCourse.CourseType);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", saveCourse.Session);
                vadap.SelectCommand.Parameters.AddWithValue("@Collegecourse_id", saveCourse.Collegecourseid);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SportSeat", saveCourse.SportSeat);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] UpdateCoursePG()");
                return null;
            }
        }
        
        public string DeleteCourse(string connection, DeleteCourse deleteCourse)
        {
            try
            {
                string result = "";
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("DeleteCourseDetail", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@collegecourse_id", deleteCourse.Collegecourseid);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);//added by shweta
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] DeleteCourse()");
                return null;
            }
        }

        public string DeleteCoursePG(string connection, DeleteCourse deleteCourse)
        {
            try
            {
                string result = "";
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("DeleteCourseDetail_PG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@collegecourse_id", deleteCourse.Collegecourseid);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] DeleteCoursePG()");
                return null;
            }
        }

        public string ActiveCourse(string connection, DeleteCourse deleteCourse)
        {
            try
            {
                string result = "";
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("ActiveCourseDetail", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@collegecourse_id", deleteCourse.Collegecourseid);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);//added by shweta
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] ActiveCourse()");
                return null;
            }
        }

        public string ActiveCoursePG(string connection, DeleteCourse deleteCourse)
        {
            try
            {
                string result = "";
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("ActiveCourseDetail_PG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@collegecourse_id", deleteCourse.Collegecourseid);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] ActiveCoursePG()");
                return null;
            }
        }

        public string FreezeCourse(string connection, FreezeCourse freezeCourse)
        {
            try
            {
                string result = "";
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("FreezeCourseDetail", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@P_sessionid", freezeCourse.Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@P_ipaddress", ipAddress);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] FreezeCourse()");
                return null;
            }
        }

        public string FreezeCourseUG(string connection, FreezeCourse freezeCourse)
        {
            try
            {
                string result = "";
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("FreezeCourseDetail_UG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@P_sessionid", freezeCourse.Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@P_ipaddress", ipAddress);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] FreezeCourseUG()");
                return null;
            }
        }

        public string GetCourseSubjectComb(string connection, DeleteCourse deleteCourse)
        {
            try
            {
                string result = "";
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCourseSubjectComb", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@collegecourse_id", deleteCourse.Collegecourseid);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0][0].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetCourseSubjectComb()");
                return null;
            }
        }

        public DataSet GetFreezeUnFreezeData(string connection, string SessionId)
        {
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFreezeUnFreezeData", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@p_sessionid", SessionId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {

                }
                return vds;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpGet] GetFreezeUnFreezeData()");
                return null;
            }
        }

        public string SaveFreezeUnFreezeData(string connection, List<FreezeUnFreezeData> FreezeData)
        {
            try
            {
                string result = "";
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveFeeUnFreezeData", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < FreezeData.Count; i++)
                {
                    string freezeval = "";
                    if (FreezeData[i].LastStatus != FreezeData[i].NewStatus)
                    {
                        freezeval = FreezeData[i].NewStatus;
                        vadap.SelectCommand.Parameters.Clear();
                        if (FreezeData[i].SessionId != "" && FreezeData[i].SessionId != "0" && FreezeData[i].SessionId != "null" && FreezeData[i].SessionId != null
                           && FreezeData[i].CollegeId != "" && FreezeData[i].CollegeId != "0" && FreezeData[i].CollegeId != "null" && FreezeData[i].CollegeId != null)

                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@P_sessionid", FreezeData[i].SessionId);
                            vadap.SelectCommand.Parameters.AddWithValue("@P_college_id", FreezeData[i].CollegeId);
                            vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                            vadap.SelectCommand.Parameters.AddWithValue("@P_ipaddress", ipAddress);
                            vadap.SelectCommand.Parameters.AddWithValue("@p_freezeval", freezeval);
                            vadap.Fill(vds);
                        }
                    }
                }
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
                else
                {
                    result = "College data already saved!";
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] SaveFreezeUnFreezeData()");
                return null;
            }
        }

        public string SendManualSMS(string connection)
        {
            string totalcount = "";
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                //MySqlDataAdapter vadap = new MySqlDataAdapter("sendManualSMS", vconnHE);
                MySqlDataAdapter vadap = new MySqlDataAdapter("SendbulksmstoSCcandidate", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    totalcount = "Total Message Sent :- " + vds.Tables[0].Rows.Count.ToString();
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < vds.Tables[0].Rows.Count; i++)
                        {
                            string mobile = vds.Tables[0].Rows[i]["MobileNo"].ToString();
                            string smstext = vds.Tables[0].Rows[i]["SmsText"].ToString().Trim();
                            AgriSMS.sendSingleSMS(mobile, smstext, vds.Tables[0].Rows[i]["templateid"].ToString().Trim());
                        }
                    }
                }
                return totalcount;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseDB.[HttpPost] SendManualSMS()");
                return Convert.ToString(ex.Message);
            }
        }

        public void SaveError(Exception ex)
        {
            try
            {
                objlog.LogWrite("Method Name - " + ex.StackTrace.ToString() + Environment.NewLine + "Target Website - " + ex.TargetSite.ToString() + Environment.NewLine + " Error - " + ex.Message.ToString() + Environment.NewLine + (ex.InnerException != null ? ("Inner Exception : " + ex.InnerException.ToString()) : ""));
            }
            catch
            {

            }
        }

        public void SaveErrorString(string ex)
        {
            try
            {
                objlog.LogWrite(ex);
            }
            catch
            {

            }
        }
    }
}