using HigherEducation.Models;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
namespace HigherEducation.BusinessLayer
{
    public class FeesPGDB
    {
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        private Log objlog = new Log();
        Logger logger = LogManager.GetCurrentClassLogger();

        public List<FillDropdowns> GetCollege(string connection)
        {
            try
            {
                List<FillDropdowns> result = new List<FillDropdowns>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollegePG", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdowns
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return null;
            }
        }
        public List<FillDropdowns> GetSession(string connection)
        {
            try
            {
                List<FillDropdowns> result = new List<FillDropdowns>();
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
                              select new FillDropdowns
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return null;
            }
        }
        public List<FillDropdowns> GetCourse(string connection, string Collegeid, string Sessionid)
        {
            try
            {
                List<FillDropdowns> result = new List<FillDropdowns>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCoursePG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@College_id", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@Session_id", Sessionid);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdowns
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()

                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return null;
            }
        }
        public List<FillDropdowns> GetSection(string connection, string Courseid, string Sessionid)
        {
            try
            {
                List<FillDropdowns> result = new List<FillDropdowns>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSectionPG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_Id", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@Session_Id", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", HttpContext.Current.Session["CollegeId"]);

                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FillDropdowns
                              {
                                  Text = row["Text"].ToString(),
                                  Value = row["Value"].ToString()

                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return null;
            }
        }
        public List<FeeDetailG> GetFeeDetail(string connection, string CollegeID, string SessionID, string CourseID, string SectionID)
        {
            try
            {
                List<FeeDetailG> result = new List<FeeDetailG>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeDetailPG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", CollegeID);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", SessionID);
                vadap.SelectCommand.Parameters.AddWithValue("@course_id", CourseID);
                vadap.SelectCommand.Parameters.AddWithValue("@section_id", SectionID);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FeeDetailG
                              {
                                  FeeDetailID = row["FeeDetailID"].ToString(),
                                  FeeSubHeadID = row["FeeSubHeadID"].ToString(),
                                  FeeHeadName = row["FeeHeadName"].ToString(),
                                  FeeSubHeadName = row["FeeSubHeadName"].ToString(),
                                  Iswaiver = row["Iswaiver"].ToString(),
                                  Yearly = row["Yearly"].ToString()
                                  //Semester1 = row["Semester1"].ToString(),
                                  //Semester2 = row["Semester2"].ToString(),
                                  //FeeType=row["FeeType"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return null;
            }
        }
        public string SaveFeeDetail(string connection, List<FeeDetail> FeeDetail)
        {
            try
            {
                string result = "";
                int yearamt = 0;
                string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                Regex regexAmount = new Regex(@"^[0-9]{1,6}$");
                //decimal sem1amt = 0;
                //decimal sem2amt = 0;
                int detailid;
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveFeeDetailPG", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < FeeDetail.Count; i++)
                {
                    vadap.SelectCommand.Parameters.Clear();
                    if (FeeDetail[i].FeeDetailID == "")
                    {
                        detailid = 0;
                    }
                    else
                    {
                        detailid = Convert.ToInt32(FeeDetail[i].FeeDetailID);
                    }
                    if (!regexAmount.IsMatch(Convert.ToString(FeeDetail[i].YearAmount)))
                    {
                        result = "Invalid yearly Amount";
                        return result;
                    }
                    if (FeeDetail[i].YearAmount == "")
                    {
                        yearamt = 0;
                    }
                    else if (FeeDetail[i].YearAmount != "" && FeeDetail[i].CollegeID != "0" && FeeDetail[i].SessionID != "0" && FeeDetail[i].CourseID != "0" && FeeDetail[i].SectionID != "0"
                            && FeeDetail[i].CollegeID != "" && FeeDetail[i].SessionID != "" && FeeDetail[i].CourseID != "" && FeeDetail[i].SectionID != ""
                            && FeeDetail[i].CollegeID != "null" && FeeDetail[i].SessionID != "null" && FeeDetail[i].CourseID != "null" && FeeDetail[i].SectionID != "null"
                             && FeeDetail[i].CollegeID != null && FeeDetail[i].SessionID != null && FeeDetail[i].CourseID != null && FeeDetail[i].SectionID != null)

                    {
                        yearamt = Convert.ToInt32(FeeDetail[i].YearAmount);
                        vadap.SelectCommand.Parameters.AddWithValue("@college_id", FeeDetail[i].CollegeID);
                        vadap.SelectCommand.Parameters.AddWithValue("@session_id", FeeDetail[i].SessionID);
                        vadap.SelectCommand.Parameters.AddWithValue("@course_id", FeeDetail[i].CourseID);
                        vadap.SelectCommand.Parameters.AddWithValue("@section_id", FeeDetail[i].SectionID);
                        //vadap.SelectCommand.Parameters.AddWithValue("@feetype", FeeDetail[i].FeeType);
                        vadap.SelectCommand.Parameters.AddWithValue("@feesubhead_id", FeeDetail[i].FeeSubHeadID);
                        vadap.SelectCommand.Parameters.AddWithValue("@year_amt", yearamt);
                        //vadap.SelectCommand.Parameters.AddWithValue("@sem1_amt", sem1amt);
                        //vadap.SelectCommand.Parameters.AddWithValue("@sem2_amt", sem2amt);
                        vadap.SelectCommand.Parameters.AddWithValue("@feedetail_id", detailid);
                        vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                        vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);
                        vadap.Fill(vds);
                    }
                    //if (FeeDetail[i].Semester1Amount == "")
                    //{
                    //    sem1amt = 0;
                    //}
                    //else
                    //{
                    //    sem1amt = Convert.ToDecimal(FeeDetail[i].Semester1Amount);
                    //}
                    //if (FeeDetail[i].Semester2Amount == "")
                    //{
                    //    sem2amt = 0;
                    //}
                    //else
                    //{
                    //    sem2amt = Convert.ToDecimal(FeeDetail[i].Semester2Amount);
                    //}
                }
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
                logger.Error(ex, "Error occured in HigherEducation.FeesPGDB.[HttpPost] SaveFeeDetail()");
                return null;
            }
        }
        public List<FeeDetailData> GetFeeDetailData(string connection, string Collegeid, string Sessionid)
        {
            try
            {
                List<FeeDetailData> result = new List<FeeDetailData>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeDetailDataPG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@College_id", Collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@Session_id", Sessionid);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FeeDetailData
                              {
                                  SrNo = row["SrNo"].ToString(),
                                  SessionName = row["SessionName"].ToString(),
                                  SectionName = row["SectionName"].ToString(),
                                  YrAmnt = row["YrAmnt"].ToString()
                                  //Sem1Amnt = row["Sem1Amnt"].ToString(),
                                  //Sem2Amnt = row["Sem2Amnt"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return null;
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
        public string SaveFreezeData(string connection, FreezeFeeDetail FeeData)
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
                if (FeeData.CollegeID != "0" && FeeData.SessionID != "0" && FeeData.CourseID != "0" && FeeData.SectionID != "0" &&
                    FeeData.CollegeID != "" && FeeData.SessionID != "" && FeeData.CourseID != "" && FeeData.SectionID != "" &&
                    FeeData.CollegeID != "null" && FeeData.SessionID != "null" && FeeData.CourseID != "null" && FeeData.SectionID != "null" &&
                    FeeData.CollegeID != null && FeeData.SessionID != null && FeeData.CourseID != null && FeeData.SectionID != null)
                {
                    MySqlDataAdapter vadap = new MySqlDataAdapter("SaveFreezeDataPG", vconnHE);
                    vadap.SelectCommand.Parameters.AddWithValue("@college_id", FeeData.CollegeID);
                    vadap.SelectCommand.Parameters.AddWithValue("@session_id", FeeData.SessionID);
                    vadap.SelectCommand.Parameters.AddWithValue("@course_id", FeeData.CourseID);
                    vadap.SelectCommand.Parameters.AddWithValue("@section_id", FeeData.SectionID);
                    vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);
                    vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                    vadap.SelectCommand.Parameters.AddWithValue("@freeze_val", 'Y');
                    vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                    vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                    vadap.Fill(vds);
                }
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
                SaveError(ex);
                return null;
            }
        }
        public string GetFreezeData(string connection, string CollegeID, string SessionID, string CourseID, string SectionID)
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFreezeDataPG", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", CollegeID);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", SessionID);
                vadap.SelectCommand.Parameters.AddWithValue("@course_id", CourseID);
                vadap.SelectCommand.Parameters.AddWithValue("@section_id", SectionID);
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
                SaveError(ex);
                return null;
            }
        }
    }
}