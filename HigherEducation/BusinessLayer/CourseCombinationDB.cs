using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HigherEducation.Controllers;
using MySql.Data.MySqlClient;
using NLog;

namespace HigherEducation.BusinessLayer
{
    public class CourseCombinationDB
    {
        string connectionString_HE = ConfigurationManager.ConnectionStrings["HigherEducation"].ToString();
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        Logger logger = LogManager.GetCurrentClassLogger();

        public List<SelectListItem> GetSessionView()
        {
            List<SelectListItem> res = new List<SelectListItem>();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
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
                    res = (from DataRow row in vds.Tables[0].Rows
                           select new SelectListItem
                           {
                               Text = row["Text"].ToString(),
                               Value = row["Value"].ToString()
                           }).ToList();
                }
                return res;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetSessionView()");
                return null;
            }
        }

        public DataSet GetCollegeCourse(string Sessionid)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollegeCourse", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", Sessionid);
                //vadap.SelectCommand.Parameters.AddWithValue("@Course_Type", CourseType);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetCollegeCourse()");
                return null;
            }
        }

        public DataSet GetSection(string Courseid, string Sessionid)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSectionSubject", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_Id", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetSection()");
                return null;
            }
        }

        public DataSet GetSubjectFeeDetails(string Courseid, string Sessionid, string Sectionid)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSubjectfeedetails", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@sessionid", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@courseid", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@sectionid", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@collegeid", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetSubjectFeeDetails()");
                return null;
            }
        }

        public DataSet SaveSubjectFee(List<SubjectFee> jdata)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter();
                for (int i = 0; i < jdata.Count; i++)
                {
                    vadap = new MySqlDataAdapter("SaveSubjectFeeConfiguration", vconnHE);
                    vadap.SelectCommand.Parameters.AddWithValue("@_subjectid", jdata[i].Subject_id);
                    vadap.SelectCommand.Parameters.AddWithValue("@_feesamount", jdata[i].Fee);
                    vadap.SelectCommand.Parameters.AddWithValue("@_subjectseats", jdata[i].Seats);
                    vadap.SelectCommand.Parameters.AddWithValue("@_isoptional", jdata[i].IsOptional);
                    vadap.SelectCommand.Parameters.AddWithValue("@_sessionid", jdata[i].Sessionid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_collegeid", HttpContext.Current.Session["CollegeId"]);
                    vadap.SelectCommand.Parameters.AddWithValue("@_courseid", jdata[i].Courseid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_sectionid", jdata[i].Sectionid);
                    vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                    vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                    vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                    vadap.Fill(vds);
                }
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpPost] SaveSubjectFee()");
                return null;
            }
        }

        public DataSet SaveSubjectCombination(List<SubjectCombination> jdata)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter();
                for (int i = 0; i < jdata.Count; i++)
                {
                    vadap = new MySqlDataAdapter("SaveSubjectCombination", vconnHE);
                    vadap.SelectCommand.Parameters.AddWithValue("@_subjectid", jdata[i].SubjectId);
                    vadap.SelectCommand.Parameters.AddWithValue("@_subjectname", jdata[i].Subject);
                    vadap.SelectCommand.Parameters.AddWithValue("@_sessionid", jdata[i].Sessionid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_collegeid", HttpContext.Current.Session["CollegeId"]);
                    vadap.SelectCommand.Parameters.AddWithValue("@_courseid", jdata[i].Courseid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_sectionid", jdata[i].Sectionid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_Combinationid", jdata[i].Combinationid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_CourseType", jdata[i].CourseType);
                    vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                    vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                    vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                    vadap.Fill(vds);
                }
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpPost] SaveSubjectCombination()");
                return null;
            }
        }

        public DataSet DeactivateCourse(string coursecombinationid)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter();
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                vadap = new MySqlDataAdapter("DeactivateCourseCombination", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@_coursecombinationid", coursecombinationid);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpPost] DeactivateCourse()");
                return null;
            }
        }

        public DataSet GetCombinationSubjects(string Courseid, string Sessionid, string Sectionid, string CombinationGroup)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCombinationSubjectMaster", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@_sessionid", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@_collegeid", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@_courseid", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@_sectionid", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@combination_group", CombinationGroup);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetCombinationSubjects()");
                return null;
            }
        }

        public DataSet GetCombinationReport(string Sessionid, string Sectionid, string CourseId)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCominationReport", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@section_id", Sectionid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CourseId", CourseId);
                vadap.SelectCommand.Parameters.AddWithValue("@_collegeid", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetCombinationReport()");
                return null;
            }
        }

        public DataSet GetSeats(string Courseid, string Sessionid, string SectionId)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCourseSeat", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_Id", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@Section_Id", SectionId);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetSeats()");
                return null;
            }
        }

        public DataSet GetCourseType()
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("Getcoursetypecombination", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@college_tye", HttpContext.Current.Session["CollegeType"]);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetCourseType()");
                return null;
            }
        }

        public DataSet GetSubjects()
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSubjects", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetSubjects()");
                return null;
            }
        }

        public DataSet UpdateSubjectCombinationfees(string SubjectCombinationId, string TotalFees, string NoofSeats)
        {
            DataSet result = new DataSet();
            try
            {
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter();
                vadap = new MySqlDataAdapter("UpdateSubjectCombinationfeeSeats", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@P_SubjectCombinationId", SubjectCombinationId);
                vadap.SelectCommand.Parameters.AddWithValue("@P_TotalFees", TotalFees);
                vadap.SelectCommand.Parameters.AddWithValue("@P_NoofSeats", NoofSeats);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", ipAddress);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpPost] UpdateSubjectCombinationfees()");
                return null;
            }
        }

        public DataSet FreezeCombination(string SessionId, string CourseId, string SectionId)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                //string strIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter();
                vadap = new MySqlDataAdapter("FreezeCourseCombination", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SessionId", SessionId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CourseId",  CourseId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_SectionId", SectionId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", HttpContext.Current.Session["CollegeId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@P_ipaddress", ipAddress);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpPost] FreezeCombination()");
                return null;
            }
        }

        //public DataSet GetCombinationGroup()
        //{
        //    DataSet result = new DataSet();
        //    try
        //    {
        //        if (vconnHE.State == ConnectionState.Closed)
        //        {
        //            vconnHE.ConnectionString = connectionString_HE;
        //            vconnHE.Open();
        //        }
        //        DataSet vds = new DataSet();
        //        MySqlDataAdapter vadap = new MySqlDataAdapter("GetSubjectGroup", vconnHE);
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        vadap.Fill(vds);
        //        if (vconnHE.State == ConnectionState.Open)
        //            vconnHE.Close();
        //        if (vds.Tables.Count > 0)
        //        {
        //            result = vds;
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetCombinationGroup()");
        //        return null;
        //    }
        //}

        public DataSet SaveSubjectCombinationUG(List<SubjectCombinationUG> jdata)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter();
                for (int i = 0; i < jdata.Count; i++)
                {
                    string subject = "";
                    string subjectsort = "";
                    string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (string.IsNullOrEmpty(ipAddress))
                    {
                        ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    vadap = new MySqlDataAdapter("SaveSubjectCombination_UG", vconnHE);
                    for (int k = 0; k < jdata[i].Subject.Length; k++)
                    {
                        subject += jdata[i].Subject[k] + ",";
                    }
                    subject = subject.TrimEnd(',');
                    Array.Sort(jdata[i].Subject);
                    foreach (string item in jdata[i].Subject)
                    {
                        subjectsort += item + ",";
                    }
                    subjectsort = subjectsort.TrimEnd(',');
                    vadap.SelectCommand.Parameters.AddWithValue("@_subjectname", subject);
                    vadap.SelectCommand.Parameters.AddWithValue("@_sessionid", jdata[i].Sessionid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_collegeid", HttpContext.Current.Session["CollegeId"]);
                    vadap.SelectCommand.Parameters.AddWithValue("@_courseid", jdata[i].Courseid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_sectionid", jdata[i].Sectionid);
                    vadap.SelectCommand.Parameters.AddWithValue("@_CourseType", jdata[i].CourseType);
                    vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                    vadap.SelectCommand.Parameters.AddWithValue("@_sortedSubjectCombination", subjectsort);
                    vadap.SelectCommand.Parameters.AddWithValue("@_noofseats", jdata[i].noofseats);
                    vadap.SelectCommand.Parameters.AddWithValue("@_fees", jdata[i].fees);
                    vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);
                    vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                    vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                    vadap.Fill(vds);
                }
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpPost] SaveSubjectCombinationUG()");
                return null;
            }
        }

        public DataSet ActivateCourse(string coursecombinationid)
        {
            DataSet result = new DataSet();
            try
            {
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter();
                vadap = new MySqlDataAdapter("ActivateCourseCombination", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@_coursecombinationid", coursecombinationid);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] ActivateCourse()");
                return null;
            }
        }

        public DataSet GetIsCombination(string Courseid)
        {
            DataSet result = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connectionString_HE;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetIsCombination", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@course_id", Courseid);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds;
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.CourseCombinationDB.[HttpGet] GetIsCombination()");
                return null;
            }
        }
    }
}