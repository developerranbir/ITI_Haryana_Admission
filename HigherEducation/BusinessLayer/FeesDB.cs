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
    public class FeesDB
    {
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        private Log objlog = new Log();
        Logger logger = LogManager.GetCurrentClassLogger();

        public List<FeeHeadMaster> GetFeeHeadMaster(string connection)
        {
            try
            {
                List<FeeHeadMaster> result = new List<FeeHeadMaster>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeHeadMaster", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FeeHeadMaster
                              {
                                  SrNo = row["SrNo"].ToString(),
                                  FeeHeadID = row["FeeHeadID"].ToString(),
                                  FeeHeadName = row["FeeHeadName"].ToString(),
                                  IsActive = row["IsActive"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetFeeHeadMaster()");
                return null;
            }
        }

        public string SaveFeeHeadMaster(string connection, FeeHeadM SaveFeeHead)
        {
            try
            {
                string result = "";
                Regex regexHeadName = new Regex(@"^[A-Za-z0-9.\s]{1,50}$");
                if (!regexHeadName.IsMatch(Convert.ToString(SaveFeeHead.FeeHeadName)))
                {
                    result = "Invalid Fee Head Name";
                    return result;
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveFeeHeadMaster", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@feehead_name", SaveFeeHead.FeeHeadName);
                vadap.SelectCommand.Parameters.AddWithValue("@isactive", SaveFeeHead.IsActive);
                vadap.SelectCommand.Parameters.AddWithValue("@feehead_id", SaveFeeHead.FeeHeadID);
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
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] SaveFeeHeadMaster()");
                return null;
            }
        }

        public string DeleteFeeHead(string connection, string FeeHeadID)
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("DeleteFeeHead", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@feehead_id", FeeHeadID);
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
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] DeleteFeeHead()");
                return null;
            }
        }

        public List<FillDropdowns> GetFeeHead(string connection)
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeHead", vconnHE);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetFeeHead()");
                return null;
            }
        }

        public List<FeeSubHeadMaster> GetFeeSubHeadMaster(string connection, string FeeHeadID)
        {
            try
            {
                List<FeeSubHeadMaster> result = new List<FeeSubHeadMaster>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeSubHeadMaster", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@feehead_id", FeeHeadID);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FeeSubHeadMaster
                              {
                                  SrNo = row["SrNo"].ToString(),
                                  FeeSubHeadID = row["FeeSubHeadID"].ToString(),
                                  FeeSubHeadName = row["FeeSubHeadName"].ToString(),
                                  FeeHeadID = row["FeeHeadID"].ToString(),
                                  FeeHeadName = row["FeeHeadName"].ToString(),
                                  IsWavier = row["IsWavier"].ToString(),
                                  IsActive = row["IsActive"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetFeeSubHeadMaster()");
                return null;
            }
        }

        public string SaveFeeSubHeadMaster(string connection, FeeSubHeadM SaveFeeSubHead)
        {
            try
            {
                string result = "";
                Regex regexSubHeadName = new Regex(@"^[A-Za-z0-9.\s]{1,50}$");
                if (!regexSubHeadName.IsMatch(Convert.ToString(SaveFeeSubHead.FeeSubHeadName)))
                {
                    result = "Invalid Fee Sub Head Name";
                    return result;
                }
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveFeeSubHeadMaster", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@feehead_id", SaveFeeSubHead.FeeHeadID);
                vadap.SelectCommand.Parameters.AddWithValue("@feesubhead_name", SaveFeeSubHead.FeeSubHeadName);
                vadap.SelectCommand.Parameters.AddWithValue("@iswavier", SaveFeeSubHead.IsWavier);
                vadap.SelectCommand.Parameters.AddWithValue("@isactive", SaveFeeSubHead.IsActive);
                vadap.SelectCommand.Parameters.AddWithValue("@feesubhead_id", SaveFeeSubHead.FeeSubHeadID);
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
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] SaveFeeSubHeadMaster()");
                return null;
            }
        }

        public string DeleteFeeSubHead(string connection, string FeeSubHeadID)
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("DeleteFeeSubHead", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@feesubhead_id", FeeSubHeadID);
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
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] DeleteFeeSubHead()");
                return null;
            }
        }

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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollege", vconnHE);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetCollege()");
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetSession()");
                return null;
            }
        }

        public List<FillDropdowns> GetCourse(string connection,string Collegeid,string Sessionid)
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCourse", vconnHE);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetCourse()");
                return null;
            }
        }

        public List<FillDropdowns> GetSection(string connection, string Courseid, string Sessionid, string collageidbystate)
        {
            try
            {
                string collageid;
                string usertype = HttpContext.Current.Session["UserType"].ToString();
                if (usertype == "2")
                {
                    collageid = HttpContext.Current.Session["CollegeId"].ToString();
                }
                else
                {
                    collageid = collageidbystate;
                }
                List<FillDropdowns> result = new List<FillDropdowns>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSection", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@Course_Id", Courseid);
                vadap.SelectCommand.Parameters.AddWithValue("@Session_Id", Sessionid);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", collageid);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetSection()");
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeDetail", vconnHE);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetFeeDetail()");
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveFeeDetail", vconnHE);
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
                        result ="Invalid yearly Amount";
                        return result;
                    }
                    if (FeeDetail[i].YearAmount == "")
                    {
                        yearamt = 0;
                    }
                    else if (FeeDetail[i].YearAmount != ""  && FeeDetail[i].CollegeID != "0" && FeeDetail[i].SessionID != "0" && FeeDetail[i].CourseID != "0" && FeeDetail[i].SectionID != "0"
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
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] SaveFeeDetail()");
                return null;
            }
        }

        public List<FeeDetailData> GetFeeDetailData(string connection,string Collegeid,string Sessionid)
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeDetailData", vconnHE);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetFeeDetailData()");
                return null;
            }
        }

        public string  GetUniversityID(string connection, string Collegeid)
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetUniversityID", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@College_id", Collegeid);
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
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetUniversityID()");
                return null;
            }
        }

        public string SaveFeeHead(string connection, FeeHead SaveFeeHead)
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveFeeHeadDetail", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", SaveFeeHead.CollegeID);
                vadap.SelectCommand.Parameters.AddWithValue("@feehead_id", SaveFeeHead.FeeHeadID);
                vadap.SelectCommand.Parameters.AddWithValue("@feehead_name", SaveFeeHead.FeeHeadName);
                vadap.SelectCommand.Parameters.AddWithValue("@session_id", SaveFeeHead.SessionID);
                vadap.SelectCommand.Parameters.AddWithValue("@waivable", SaveFeeHead.Waivable);
                vadap.SelectCommand.Parameters.AddWithValue("@subhead_id", SaveFeeHead.FeeSubHeadID);
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
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] SaveFeeHead()");
                return null;
            }
        }

        public List<FeeHeadData> GetFeeHeadData(string connection,string Collegeid)
        {
            try
            {
                List<FeeHeadData> result = new List<FeeHeadData>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFeeHeadData", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@College_id", Collegeid);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new FeeHeadData
                              {
                                  FeeSubHeadID = row["FeeSubHeadID"].ToString(),
                                  FeeHeadID = row["FeeHeadID"].ToString(),
                                  FeeHeadName = row["FeeHeadName"].ToString(),
                                  FeeSubHeadName = row["FeeSubHeadName"].ToString(),
                                  CollegeID = row["CollegeID"].ToString(),
                                  FeeSessionName=row["FeeSessionName"].ToString(),
                                  FeeSessionID = row["FeeSessionID"].ToString(),
                                  IsWavier = row["IsWavier"].ToString()
                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetFeeHeadData()");
                return null;
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
                    MySqlDataAdapter vadap = new MySqlDataAdapter("SaveFreezeData", vconnHE);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] SaveFreezeData()");
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
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetFreezeData", vconnHE);
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
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        result = vds.Tables[0].Rows[0][0].ToString();
                    }
                    else
                    {
                        result = "";
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetFreezeData()");
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
        public List<CandidateDetails> GetCollegeMaster(string connection, string RegNO)
        {
            try
            {
                List<CandidateDetails> result = new List<CandidateDetails>();
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateQualification", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@P_RegNO", RegNO);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = (from DataRow row in vds.Tables[0].Rows
                              select new CandidateDetails
                              {
                                  SrNo = row["SrNo"].ToString(),
                                  ExamPassed = row["ExamPassed"].ToString(),
                                  RegistrationRollno = row["RegistrationRollno"].ToString(),
                                  MarksObt = row["MarksObt"].ToString(),
                                  Percentage = row["Percentage"].ToString(),
                                  P_Id = row["P_Id"].ToString(),
                                  MaxMarks = row["MaxMarks"].ToString(),

                              }).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpGet] GetFeeSubHeadMaster()");
                return null;
            }
        }



        public string UpdateEducation(string connection, CandidateDetails objCandidateDetails)
        {
            try
            {
                string result = "";
                Regex regexSubHeadName = new Regex(@"^[A-Za-z0-9.\s]{1,50}$");
                //if (!regexSubHeadName.IsMatch(Convert.ToString(SaveFeeSubHead.FeeSubHeadName)))
                //{
                //    result = "Invalid Fee Sub Head Name";
                //    return result;
                //}
                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.ConnectionString = connection;
                    vconnHE.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateEduDetails", vconnHE);
                vadap.SelectCommand.Parameters.AddWithValue("@PId", objCandidateDetails.P_Id);
                vadap.SelectCommand.Parameters.AddWithValue("@p_RegistrationRollno", objCandidateDetails.RollNo);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MarksObt", objCandidateDetails.MarksObt);
                vadap.SelectCommand.Parameters.AddWithValue("@P_MaxMarks", objCandidateDetails.MaxMarks);
                vadap.SelectCommand.Parameters.AddWithValue("@p_Percentage", objCandidateDetails.Percentage);


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
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] UpdateEducation()");
                return null;
            }
        }
         
 
    public string Updatepersonals(string connection, CandidateDetails objCandidateDetails)
        {
            try
            {
                string result = "";
                if (objCandidateDetails.CollegeStatus == "2")
                {
                    if (objCandidateDetails.CountCount <= 2)
                    {
                        Regex regexSubHeadName = new Regex(@"^[A-Za-z0-9.\s]{1,50}$");
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
                        MySqlDataAdapter vadap = new MySqlDataAdapter("Updatepersonals", vconnHE);
                        vadap.SelectCommand.Parameters.AddWithValue("@P_RegNO", objCandidateDetails.RegId);
                        if(objCandidateDetails.Candidate== "true")
                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@p_CandidateName", objCandidateDetails.CandidateName);
                        }
                        else
                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@p_CandidateName",null);
                        }
                        if (objCandidateDetails.Father == "true")
                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@p_FatherName", objCandidateDetails.FatherName);
                        }
                        else
                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@p_FatherName", null);
                        }
                        if (objCandidateDetails.Mother == "true")
                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@p_motherName", objCandidateDetails.motherName);
                        }
                        else
                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@p_motherName", null);
                        }
                       
                        //vadap.SelectCommand.Parameters.AddWithValue("@p_MobileNo", objCandidateDetails.MobileNo);
                        if (objCandidateDetails.checkDOB == "true")
                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@P_DOB", objCandidateDetails.DOB);
                        }
                        else
                        {
                            vadap.SelectCommand.Parameters.AddWithValue("@P_DOB", null);
                        }
                        vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);
                        vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                        vadap.SelectCommand.Parameters.AddWithValue("@P_UserType", objCandidateDetails.CollegeStatus);
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
                    else
                    {
                        result = "00";
                        return result;

                    }
                }
                else
                {
                    Regex regexSubHeadName = new Regex(@"^[A-Za-z0-9.\s]{1,50}$");
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
                    MySqlDataAdapter vadap = new MySqlDataAdapter("Updatepersonals", vconnHE);
                    vadap.SelectCommand.Parameters.AddWithValue("@P_RegNO", objCandidateDetails.RegId);
                    vadap.SelectCommand.Parameters.AddWithValue("@p_CandidateName", objCandidateDetails.CandidateName);
                    vadap.SelectCommand.Parameters.AddWithValue("@p_FatherName", objCandidateDetails.FatherName);
                    vadap.SelectCommand.Parameters.AddWithValue("@p_motherName", objCandidateDetails.motherName);
                    //vadap.SelectCommand.Parameters.AddWithValue("@p_MobileNo", objCandidateDetails.MobileNo);
                    if (objCandidateDetails.checkDOB == "true")
                    {
                        vadap.SelectCommand.Parameters.AddWithValue("@P_DOB", objCandidateDetails.DOB);
                    }
                    else
                    {
                        vadap.SelectCommand.Parameters.AddWithValue("@P_DOB", objCandidateDetails.LblDOB2);
                    }
                    vadap.SelectCommand.Parameters.AddWithValue("@ip_address", ipAddress);
                    vadap.SelectCommand.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["UserId"]);
                    vadap.SelectCommand.Parameters.AddWithValue("@P_UserType", objCandidateDetails.CollegeStatus);
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

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FeesDB.[HttpPost] Updatepersonals()");
                return null;
            }
        }
		
	
    }
}