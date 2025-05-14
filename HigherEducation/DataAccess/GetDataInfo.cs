using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HigherEducation.Models;
using System.Web;
using MySql.Data.MySqlClient;
using NLog;
using System.Web.Security;

namespace HigherEducation.DataAccess
{
    public class GetDataInfo
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        #region ConnectionString
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);

        static readonly string HigherEducationR = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        readonly MySqlConnection connection_readonly = new MySqlConnection(HigherEducationR);
        #endregion
        #region Method
        public List<GetDropDown> GetBoard()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstBoard = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetBoardName", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Type = Convert.ToString(r["universityId"]),
                                Value = r["univeristyname"].ToString()
                            };
                            lstBoard.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetBoard()");
            }
            connection_readonly.Close();
            return lstBoard;
        }

        public List<GetDropDown> GetUniversityMaster()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstBoard = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetUniversityMaster", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Type = Convert.ToString(r["universityId"]),
                                Value = r["univeristyname"].ToString()
                            };
                            lstBoard.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetBoard()");
            }
            connection_readonly.Close();
            return lstBoard;
        }

        public List<GetDropDown> GetQualificationMaster()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstBoard = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetQualificationMaster", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Type = Convert.ToString(r["QualificationCode"]),
                                Value = r["Qualification"].ToString()
                            };
                            lstBoard.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetBoard()");
            }
            connection_readonly.Close();
            return lstBoard;
        }
        public List<GetDropDown> GetMinorityData()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstMinority = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetMinorityData", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["castecode"]),
                                Value = r["castedesc"].ToString()
                            };
                            lstMinority.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetMinorityData()");
            }
            connection_readonly.Close();
            return lstMinority;
        }

        public List<GetDropDown> GetPanchVillages(string regId)
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstMinority = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetPanchVillages", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("P_Reg_Id", regId);
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["villagecode"]),
                                Value = r["villagename"].ToString()
                            };
                            lstMinority.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetPanchVillages()");
            }
            connection_readonly.Close();
            return lstMinority;
        }
        public List<GetDropDown> GetReservationCategory()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstCategory = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetReservationCategory", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["reservationid"]),
                                Value = r["reservationName"].ToString()
                            };
                            lstCategory.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetReservationCategory()");
            }
            connection_readonly.Close();
            return lstCategory;
        }
        public List<GetDropDown> GetCaste()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstCaste = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Get_Caste", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["Caste_Id"]),
                                Value = r["Caste_Name"].ToString()
                            };
                            lstCaste.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetCaste()");
            }
            connection_readonly.Close();
            return lstCaste;
        }

        public List<GetDropDown> GetCasteCategory()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstCategory = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Get_Caste_Category", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["Caste_ID"]),
                                Value = r["Caste_Categoty_Name"].ToString()
                            };
                            lstCategory.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetCasteCategory()");
            }
            connection_readonly.Close();
            return lstCategory;
        }
        public List<GetDropDown> GetCountryName()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstCountry = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetCountry", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["country_id"]),
                                Value = r["country_name"].ToString()
                            };
                            lstCountry.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetCountryName()");
            }
            connection_readonly.Close();
            return lstCountry;
        }

        public List<GetDropDown> GetAllCollages()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstCountry = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getAllcollege", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["collegeid"]),
                                Value = r["collegename"].ToString()
                            };
                            lstCountry.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetAllCollages()");
            }
            connection_readonly.Close();
            return lstCountry;
        }


        public List<GetDropDown> GetcollgecourseAll(string collageid)
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstCountry = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getcollgecourseAll", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("p_collegeid", collageid);
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["coursesectionid"]),
                                Value = r["sectionname"].ToString()
                            };
                            lstCountry.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetcollgecourseAll()");
            }
            connection_readonly.Close();
            return lstCountry;
        }
        public List<GetDropDown> GetState()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstState = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetStateName", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["statecode"]),
                                Value = r["statename"].ToString()
                            };
                            lstState.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetState()");
            }
            connection_readonly.Close();
            return lstState;
        }
        public List<GetDropDown> GetDistrict()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstDistrict = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetDistrictName", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["districtcode"]),
                                Value = r["districtname"].ToString()
                            };
                            lstDistrict.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetDistrict()");
            }
            connection_readonly.Close();
            return lstDistrict;
        }

        public CandidateDetail GetAPIStatus(string RegId)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            CandidateDetail objCandidate = new CandidateDetail();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetAPIStatus", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_Id", RegId);

                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objCandidate.CheckAPIStatus = Convert.ToString(rdr["check_api_status"]);
                        objCandidate.CandidateName = Convert.ToString(rdr["Candidate_FullName"]);
                        objCandidate.QualificationCode = Convert.ToString(rdr["QualificationCode"]);
                        objCandidate.RegID = RegId;
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetCandidateName()");
            }
            connection.Close();
            return objCandidate;
        }

        public ChoiceCourse GetDetailsForChoiceofCourses(string RegId)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            ChoiceCourse objCandidate = new ChoiceCourse();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetDetailsforChoiceCourse", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_Id", RegId);

                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objCandidate.RegID = Convert.ToString(rdr["Reg_Id"]);
                        objCandidate.Stream = Convert.ToString(rdr["qualificationid"]);
                        objCandidate.PassingOfYear = Convert.ToString(rdr["Passing_Year"]);
                        objCandidate.DisableCategory = Convert.ToString(rdr["Physicalid"]);
                        objCandidate.Status = Convert.ToString(rdr["passstatus"]);
                        objCandidate.Percentage = Convert.ToString(rdr["Percentage"]);
                        objCandidate.DA = Convert.ToString(rdr["v_da"]);
                        objCandidate.Age = Convert.ToString(rdr["v_Age"]);
                        objCandidate.MF = Convert.ToString(rdr["v_Mf"]);

                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetDetailsForChoiceofCourses()");
            }
            connection.Close();
            return objCandidate;
        }


        public string updatePasswordOldSession(String reg_id, String hasPass, String aSession, string oldpass)
        {
            string result = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("updatePassword_QTR", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_id", reg_id);
                cmd.Parameters.AddWithValue("P_NewPass", hasPass);
                cmd.Parameters.AddWithValue("P_aSession", aSession);
                cmd.Parameters.AddWithValue("P_oldPass", oldpass);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["Result"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserSecondYear()");
            }
            connection.Close();
            return result;
        }
        public LoginUserDetails GetLoginUserDetails(string RegID)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            LoginUserDetails objLUD = new LoginUserDetails();
            try
            {
                MySqlCommand cmd = new MySqlCommand("LoginUserDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_RegID", RegID);

                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        objLUD.LastLogin = Convert.ToDateTime(rdr["LastLogin"]);
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetLoginUserDetails()");
            }
            connection.Close();
            return objLUD;
        }

        public UserMaxCurrentPage GetMax_Current_page(string RegID)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            UserMaxCurrentPage userMaxCurrentPage = new UserMaxCurrentPage();
            try
            {
                MySqlCommand cmd = new MySqlCommand("Getformprogress_value", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("p_reg_id", RegID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        userMaxCurrentPage.max_page = Convert.ToInt32(rdr["max_page"]);
                        userMaxCurrentPage.current_page = Convert.ToInt32(rdr["current_page"]);
                        userMaxCurrentPage.Verificationstatus = Convert.ToString(rdr["verificationstatus"]);
                        userMaxCurrentPage.HasUnlocked = Convert.ToString(rdr["HasUnlocked"]);
                        userMaxCurrentPage.FormStatus = Convert.ToString(rdr["FormStatus"]);
                        userMaxCurrentPage.IsPassWordChange = Convert.ToString(rdr["IsPasswordChange"]);

                        

                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetMax_Current_page()");
            }
            connection.Close();
            return userMaxCurrentPage;

        }
        public int SaveLoginAndTrack(LoginTrackModels objLTM)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("spp_InsertLoginDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_RegID", objLTM.RegID);
                cmd.Parameters.AddWithValue("P_BrowserName", objLTM.BrowserName);
                cmd.Parameters.AddWithValue("P_IPAddress", objLTM.IPAddress);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                var details = cmd.ExecuteNonQuery();
                if (details >= 1)
                {
                    connection.Close();
                    return 1;

                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.SaveLoginAndTrack()");
            }
            connection.Close();
            return 0;
        }
        public bool ValidateUser(CandidateDetail objL)
        {
            CandidateDetail objCandidate = new CandidateDetail();
            bool IsExits = false;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("ValidateUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_UserName", objL.RegID);
                cmd.Parameters.AddWithValue("P_Password", objL.Password);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dbpwd = dt.Rows[0]["PWD"].ToString();
                    var HashDBpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objL.rno + dbpwd, "MD5");
                    if (objL.Password.Trim().ToLower() == HashDBpassword.Trim().ToLower())
                    {
                        IsExits = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUser()");
            }
            connection.Close();
            return IsExits;
        }

        #endregion
        public List<GetDropDown> GetReligionName()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstreligion = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetReligionName", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["religion_id"]),
                                Value = r["religion_name"].ToString()
                            };
                            lstreligion.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetReligionName()");
            }
            connection_readonly.Close();
            return lstreligion;
        }
        public List<GetDropDown> GetBloodGroup()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstBoard = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetBloodGroup", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["Blood_Group_ID"]),
                                Value = r["Blood_Group_Name"].ToString()
                            };
                            lstBoard.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetBloodGroup()");
            }
            connection_readonly.Close();
            return lstBoard;
        }
        public List<GetDropDown> GetParentalIncome()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstParentalIncome = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetParentalIncome", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["id"]),
                                Value = r["parental_income"].ToString()
                            };
                            lstParentalIncome.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetParentalIncome()");
            }
            connection_readonly.Close();
            return lstParentalIncome;
        }

        public List<GetDropDown> GetMaritalStatus()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstMarital = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetMaritalStatus", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["id"]),
                                Value = r["marital_status"].ToString()
                            };
                            lstMarital.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetMaritalStatus()");
            }
            connection_readonly.Close();
            return lstMarital;
        }
        public List<GetDropDown> GetGender()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstGender = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetGender", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["gender_id"]),
                                Value = r["gender_name"].ToString()
                            };
                            lstGender.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetGender()");
            }
            connection_readonly.Close();
            return lstGender;
        }
        public List<GetDropDown> GetOccupation()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstOccupation = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetOccupation", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["id"]),
                                Value = r["OccupationName"].ToString()
                            };
                            lstOccupation.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetOccupation()");
            }
            connection_readonly.Close();
            return lstOccupation;
        }

        public List<GetDropDown> GetDisabledCategory()
        {
            if (connection_readonly.State == ConnectionState.Closed)
            {
                connection_readonly.Open();
            }
            List<GetDropDown> lstOccupation = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetDisabledCategory", connection_readonly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["ppp_id"]),
                                Value = r["disability"].ToString()
                            };
                            lstOccupation.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetOccupation()");
            }
            connection_readonly.Close();
            return lstOccupation;
        }
        public List<GetDropDown> GetCourseType()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            List<GetDropDown> lstBoard = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetCourseType", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["CourseId"]),
                                Value = r["CourseType"].ToString()
                            };
                            lstBoard.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetCourseType()");
            }
            connection.Close();
            return lstBoard;
        }
        public List<GetDropDown> GetStream(string Rid)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            List<GetDropDown> lstStream = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getStreamMaster", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("Rid", Rid);
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["streamId"]),
                                Value = r["streamName"].ToString()
                            };
                            lstStream.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetStream()");
            }
            connection.Close();
            return lstStream;
        }

        #region for second year login
        public bool ValidateUserSecondYear(CandidateDetail objL)
        {
            bool IsExits = false;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("ValidateUser_2021", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_UserName", objL.RegID);
                cmd.Parameters.AddWithValue("P_Password", objL.Password);
                cmd.Parameters.AddWithValue("P_StdName", objL.CandidateName);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dbpwd = dt.Rows[0]["PWD"].ToString();
                    var dbstatus = dt.Rows[0]["v_status"].ToString();

                    var HashDBpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objL.rno + dbpwd, "MD5");
                    if (objL.Password.Trim().ToLower() == HashDBpassword.Trim().ToLower())
                    {
                        if (dbstatus.Trim().ToLower() == "yes")
                        {
                            IsExits = true;
                        }
                        else
                        {
                            IsExits = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserSecondYear()");
            }
            connection.Close();
            return IsExits;
        }


        public string getRegIDSecondYear(CandidateDetail objL)
        {
            string result = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("getRegID_2021", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_UserName", objL.RegID);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["Regi_No"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserSecondYear()");
            }
            connection.Close();
            return result;
        }


        public string updatePasswordSecondYear(String reg_id, String hasPass)
        {
            string result = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("updatePassword_2021", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_id", reg_id);
                cmd.Parameters.AddWithValue("P_hasPass", hasPass);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["Result"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserSecondYear()");
            }
            connection.Close();
            return result;
        }

        public string updatePasswordSecondYearStu(String reg_id, String hasPass,string ipaddress)
        {
            string result = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("ChangePwdStudent", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_id", reg_id);
                cmd.Parameters.AddWithValue("P_hasPass", hasPass);
                cmd.Parameters.AddWithValue("P_ipaddress", ipaddress);

                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["Result"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.updatePasswordSecondYearStu()");
            }
            connection.Close();
            return result;
        }
        #endregion


        #region for old session Quarter Fee
        public bool ValidateUserOldSession(CandidateDetail objL, String aSession)
        {
            bool IsExits = false;
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("ValidateUser_QTR", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_UserName", objL.RegID);
                cmd.Parameters.AddWithValue("P_aSession", aSession);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dbpwd = dt.Rows[0]["PWD"].ToString();

                    var HashDBpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objL.rno + dbpwd, "MD5");
                    if (objL.Password.Trim().ToLower() == HashDBpassword.Trim().ToLower())
                    {
                        IsExits = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserOldSession()");
            }
            connection.Close();
            return IsExits;
        }

        public string updatePasswordOldSession(String reg_id, String hasPass, String aSession)
        {
            string result = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("updatePassword_QTR", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_id", reg_id);
                cmd.Parameters.AddWithValue("P_hasPass", hasPass);
                cmd.Parameters.AddWithValue("P_aSession", aSession);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["Result"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserSecondYear()");
            }
            connection.Close();
            return result;
        }
        //public bool ValidateUserOldSession(CandidateDetail objL)
        //{
        //    bool IsExits = false;
        //    if (connection.State == ConnectionState.Closed)
        //    {
        //        connection.Open();
        //    }
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("ValidateUser_2022", connection)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.CommandTimeout = 600;
        //        cmd.Parameters.AddWithValue("P_UserName", objL.RegID);
        //        MySqlDataReader rdr = cmd.ExecuteReader();
        //        DataTable dt = new DataTable();
        //        dt.Load(rdr);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            var dbpwd = dt.Rows[0]["PWD"].ToString();

        //            var HashDBpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(objL.rno + dbpwd, "MD5");
        //            if (objL.Password.Trim().ToLower() == HashDBpassword.Trim().ToLower())
        //            {
        //                IsExits = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserOldSession()");
        //    }
        //    connection.Close();
        //    return IsExits;
        //}

        public string updatePasswordOldQtr(String reg_id, String hasPass)
        {
            string result = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("updatePassword_2022", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_id", reg_id);
                cmd.Parameters.AddWithValue("P_hasPass", hasPass);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["Result"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserSecondYear()");
            }
            connection.Close();
            return result;
        }


        /*
        public string updatePasswordSecondYear(String reg_id, String hasPass)
        {
            string result = "";
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("updatePassword_2021", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_id", reg_id);
                cmd.Parameters.AddWithValue("P_hasPass", hasPass);
                MySqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["Result"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.ValidateUserSecondYear()");
            }
            connection.Close();
            return result;
        }
        */

        #endregion




        #region vishal merging code
        public List<GetDropDown> GetAllSubject(int boardcode)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            List<GetDropDown> lstBoard = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetAllSubject", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("P_boardcode", boardcode);
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["subjectid"]),
                                Value = r["subjectname"].ToString()
                            };
                            lstBoard.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetAllSubject()");
            }
            connection.Close();
            return lstBoard;
        }

        public List<GetDropDown> GetParticularSubject()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            List<GetDropDown> lstBoard = new List<GetDropDown>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("GetDefaultSubject", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                var dr = cmd.ExecuteReader();
                var dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            GetDropDown obj = new GetDropDown
                            {
                                Id = Convert.ToInt32(r["subjectid"]),
                                Value = r["subjectname"].ToString()
                            };
                            lstBoard.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.GetDataInfo.GetParticularSubject()");
            }
            connection.Close();
            return lstBoard;
        }
        #endregion
    }
}