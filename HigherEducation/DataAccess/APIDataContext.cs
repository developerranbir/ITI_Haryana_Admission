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
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using MySqlX.XDevAPI.Common;
using System.Web.Helpers;

namespace HigherEducation.DataAccess
{
    public class APIDataContext
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        #region ConnectionString
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        static readonly string ROConStr = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);
        MySqlConnection connection_ReadOnly = new MySqlConnection(ROConStr);
        #endregion;


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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.d_getCollegeType()");
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

        public DataTable d_getCollegeNames()
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
                MySqlCommand cmd = new MySqlCommand("d_getCollegeNames", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_id", collegeId == null || collegeId == "" ? "0" : collegeId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.d_getCollegeNames()");
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

        public DataTable getCandidateRecord(String SRN)
        {
            DataTable dt = new DataTable();
            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_GetCandidateDetailsForAPICandidate", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_Id", SRN == null || SRN == "" ? "0" : SRN);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                // connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.getCandidateRecord()");
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

        public DataTable GetCourseListByCollege(String collegeId)
        {
            DataTable dt = new DataTable();

            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCourseListByCollegeId", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.GetCourseListByCollege()");
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

        public DataSet d_getStudentDetailsList(String CollegeType, String CollegeId, String CourseId)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_GetCandidateDetailsForAPI", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_CollegeType", Convert.ToInt32(CollegeType == null || CollegeType == "" ? "0" : CollegeType));
                cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.d_getStudentDetailsList()");
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

        public DataTable UpdateDataforAPI(String mobile, String email, String shift, String unit, String regID, String catName, String pwdCat, String tName, String fName, String mName)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("d_UpdateDataForAPI", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("p_MobileNumber", mobile);
                cmd.Parameters.AddWithValue("p_Email", email);
                cmd.Parameters.AddWithValue("p_Shift", shift);
                cmd.Parameters.AddWithValue("p_Unit", unit);
                cmd.Parameters.AddWithValue("p_modifiedBy", Convert.ToString(HttpContext.Current.Session["UserID"]));
                cmd.Parameters.AddWithValue("p_RegdID", regID);
                cmd.Parameters.AddWithValue("p_catName", catName);
                cmd.Parameters.AddWithValue("p_pwdCat", pwdCat);
                cmd.Parameters.AddWithValue("p_tName", tName);
                cmd.Parameters.AddWithValue("p_fName", fName);
                cmd.Parameters.AddWithValue("p_mName", mName);
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.UpdateDataforAPI()");
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

        public DataSet getErrorResponse(string CollegeType, string CollegeId, string CourseId)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SP_tblapiresponse", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_CollegeType", Convert.ToInt32(CollegeType == null || CollegeType == "" ? "0" : CollegeType));
                cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.getErrorResponse()");
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

        #region Data for new Portal SID
        public DataTable getStateUserData()
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getStateUserDetails", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.getStateUserData()");
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

        public DataTable updateUserData(String id, String mobile, String pass, String type, String base64String)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("updateStateUserDetails", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("p_Id", id);
                cmd.Parameters.AddWithValue("p_Mobile", mobile);
                cmd.Parameters.AddWithValue("p_Pass", pass);
                cmd.Parameters.AddWithValue("p_Type", type);
                cmd.Parameters.AddWithValue("p_baseEncoded", base64String);

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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.updateUserData()");
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

        public DataTable getBase64Data()
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("getBase64Data", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.getStateUserData()");
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
        #endregion
    }


    public class AuthResponse
    {
        public AccessData Data { get; set; }
    }

    public class AccessData
    {
        public string accessToken { get; set; }
        public string sessionId { get; set; }
        public string logId { get; set; }
    }
    class TokenData
    {
        public string Body { get; set; }
        
    }
    class LogHistoryBody
    {
        public string LogId { get; set; }
        public string PageSize { get; set; }
        public string PageNumber { get; set; }
    }
    class ITI
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        //string ConnectionString = "server=10.145.40.38;port=3306;database=skill_development;User Id=app1_api;password=passWord@2020; Pooling=false;SslMode=None;Keepalive=60";
        //string ConnectionString = "Data Source=10.88.229.244;port=3306;Initial Catalog=higher_edu1;User Id=root;password=admin@757";
        #region ConnectionString
        static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        MySqlConnection connection_ReadOnly = new MySqlConnection(ConnectionString);
        MySqlConnection connection = new MySqlConnection(ConnectionString);
        #endregion;

        #region for old NCVT MIS Portal
        // for DGT NCVT MIS Portal
        public DataTable SaveAPIResponse(string SrNo, string MISITICode, string TraineeName, string StateRegNumber, string APIResponse, string sucessRec)
        {
            DataTable dt = new DataTable();
            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlParameter[] p = new MySqlParameter[6];
                p[0] = new MySqlParameter("@PSrNo", SrNo);
                p[1] = new MySqlParameter("@PMISITICode", MISITICode);
                // p[2] = new MySqlParameter("@PCollegeName", CollegeName);

                // p[3] = new MySqlParameter("@PTrade", Trade);
                //      p[2] = new MySqlParameter("@PReg_ID", Reg_ID);
                p[2] = new MySqlParameter("@PTraineeName", TraineeName);
                p[3] = new MySqlParameter("@PStateRegNumber", StateRegNumber);
                p[4] = new MySqlParameter("@PAPIResponse", APIResponse);
                p[5] = new MySqlParameter("@PIssuccess", sucessRec);
                dt = MySqlConnect.DBActions.ExecuteDataTable(connection_ReadOnly, CommandType.StoredProcedure, "RS_ITI_SaveAPIResponseNew", p);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.SaveAPIResponseNew()");
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

        // for DGT NCVT MIS Portal
        public string CallAPIForSend(List<APIRequestData> objData)
        {
            string result = "";
            try
            {
                //HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create("http://164.100.68.244:8082/MIS/api/traineeupload/UploadTrainees/"));
                HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create("http://164.100.213.37/MIS/api/traineeupload/UploadTrainees/"));
                req.Method = "POST";
                req.ContentType = "application/json";
                req.ProtocolVersion = HttpVersion.Version11;
                req.Headers.Add("username", "NCVTHRMIS");
                //req.Headers.Add("password", "qcFT64c8fXTphj2ciMM1+A==");
                req.Headers.Add("password", "5ut+lIbwK1Ng+0PRbTpgAg==");
                string jsondata = JsonConvert.SerializeObject(objData);
                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(objData));
                req.ContentLength = data.Length;
                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var httpResponse = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.CallAPIForSendDataSID()");
            }
            //Console.WriteLine("API Result  ==  " + result);
            return result;
        }

        #endregion

        // Common for both Old and New Portal
        public DataTable GetITIDataForAPI()
        {
            DataTable dt = new DataTable();
            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                dt = MySqlConnect.DBActions.ExecuteDataTable(connection_ReadOnly, CommandType.StoredProcedure, "RS_ITI_GetDataForAPI");
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.GetITIDataForAPI()");
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


        #region Code for new SID Portal for NCVT Data
        // Generate Access Token
        public AccessData generateAccessToken(TokenData t1)
        {
            ITI objITI = new ITI();
            string accToken = "";
            AccessData accData = new AccessData();
            try { 
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                //HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create("https://uat-api-fe-sid.betalaunch.in/api/discovery-account/token")); // Staging
                HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create("https://api-fe.skillindiadigital.gov.in/api/discovery-account/token")); // Prod
                req.Method = "POST";
                req.ContentType = "application/json";
                req.ProtocolVersion = HttpVersion.Version11;
                string jsondata = JsonConvert.SerializeObject(t1);
                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(t1));
                req.ContentLength = data.Length;
                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var httpResponse = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    accToken = streamReader.ReadToEnd();
                }

                // Deserialize the JSON response into an AuthResponse object
                AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(accToken);

                

                // Access the accessToken property

                accData.accessToken = authResponse.Data.accessToken;
                accData.sessionId = authResponse.Data.sessionId;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.generateAccessToken()");
            }

            return accData;
        }

        // send data on SID portal
        public int CallAPIForSendDataSID(DataTable objData, String accessToken,String sessionId,int count)
        {
            string result = "";
            int r = 0;
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                //HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create("https://uat-api-fe-sid.betalaunch.in/api/iti/state/trainee/register"));  // Staging
                HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create("https://api-fe.skillindiadigital.gov.in/api/iti/state/trainee/register")); // Prod
                req.Method = "POST";
                req.ContentType = "application/json";
                req.ProtocolVersion = HttpVersion.Version11;
                req.Headers.Add("Authorization", "Bearer " + accessToken);
                req.Headers.Add("sessionId", sessionId);
                string jsondata = JsonConvert.SerializeObject(objData);
                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(objData));
                req.ContentLength = data.Length;
                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var httpResponse = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                // Deserialize the JSON response into an AuthResponse object
                AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(result);

                AccessData accData = new AccessData();

                accData.logId = authResponse.Data.logId;
                if(accData.logId == null || accData.logId == "")
                {
                    r = 0;
                }
                else
                {
                    r = 1;
                    saveLogID(accessToken, sessionId, accData.logId,count);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.CallAPIForSendDataSID()");
            }
            return r;
        }

        // History API
        public int getAPIResponseSID(LogHistoryBody objData, String accessToken, String sessionId)
        {
            string result = "";
            int r = 0;
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12
                       | SecurityProtocolType.Ssl3;
                //HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create("https://uat-api-fe-sid.betalaunch.in/api/iti/state/trainee/json-history"));  // Staging
                HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create("https://api-fe.skillindiadigital.gov.in/api/iti/state/trainee/json-history")); // Prod
                req.Method = "POST";
                req.ContentType = "application/json";
                req.ProtocolVersion = HttpVersion.Version11;
                req.Headers.Add("Authorization", "Bearer " + accessToken);
                req.Headers.Add("sessionId", sessionId);
                string jsondata = JsonConvert.SerializeObject(objData);
                var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(objData));
                req.ContentLength = data.Length;
                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var httpResponse = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                int waitForRes = 0;
                if(result.Contains("Data upload process is in progress, please wait for some time"))
                {
                    waitForRes = 1;
                }
                // Parse the JSON string
                JObject json = JObject.Parse(result);
                if (json["StatusCode"].ToString() == "200")
                {
                    if (waitForRes == 1)
                    {
                        r = 2;
                    }
                    else
                    {
                        r = 1;
                        // Accessing the 'Results' array from the JSON response
                        JArray resResults = (JArray)json["Data"]["Data"]["Results"];

                        // Loop through each result
                        foreach (JToken oneResult in resResults)
                        {
                            int s = 0;
                            if (oneResult["RecordStatus"].ToString() == "S")
                            {
                                s = 1;
                            }
                            string response = oneResult["MISITICode"].ToString() + "---" + oneResult["MobileNumber"].ToString() + "---" + oneResult["StateRegNumber"].ToString() + "---" + oneResult["TraineeName"].ToString() + "---" + oneResult["Trade"].ToString();
                            saveSIDResponse(oneResult["MISITICode"].ToString(), oneResult["MobileNumber"].ToString(), oneResult["StateRegNumber"].ToString(), response, s, oneResult["ErrorDescription"].ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.getAPIResponseSID()");
            }
            return r;
        }
        
        // save response in table
        public DataTable saveSIDResponse(String MISITICode, String mobileNo, String RegistrationNo,String APIResponse,int status, String err)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_sid_response", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("p_MISITICode", MISITICode);
                cmd.Parameters.AddWithValue("p_mobileNo", mobileNo);
                cmd.Parameters.AddWithValue("p_RegistrationNo", RegistrationNo);
                cmd.Parameters.AddWithValue("p_APIResponse", APIResponse);
                cmd.Parameters.AddWithValue("p_status", status);
                cmd.Parameters.AddWithValue("p_err", err);
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.saveSIDResponse()");
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
        
        // save log id for getting response later
        public DataTable saveLogID(String accessToken,String sessionId,String logID, int count)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("save_logID", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("p_accessToken", accessToken);
                cmd.Parameters.AddWithValue("p_sessionId", sessionId);
                cmd.Parameters.AddWithValue("p_log_ID", logID);
                cmd.Parameters.AddWithValue("p_count", count);
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.saveLogID()");
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

        // fetch log id for checking response
        public string getLogID()
        {
            DataTable dt = new DataTable();
            string result="";
            try
            {
                MySqlCommand cmd = new MySqlCommand("get_logID", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    result = row["logid"].ToString();
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.APIDataContext.getLogID()");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return result;
        }

        #endregion
    }

    class APIRequestData
    {
        public string StateRegNumber { get; set; }
        public string TraineeName { get; set; }
        public string UIDNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Category { get; set; }
        public string FatherGuardianName { get; set; }
        public string MotherName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public string Session { get; set; }
        public string AdmissionDate { get; set; }
        public string HighestQualification { get; set; }
        public string Trade { get; set; }
        public string Shift { get; set; }
        public string Unit { get; set; }
        public string IsTraineeDualMode { get; set; }
        public string MISITICode { get; set; }
        public string PersonwithDisability { get; set; }
        public string PWDcategory { get; set; }
        public string EconomicWeakerSection { get; set; }
        public string TraineeType { get; set; }
        public string TraineePhoto { get; set; }
    }
}