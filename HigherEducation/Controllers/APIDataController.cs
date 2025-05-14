using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HigherEducation.BusinessLayer;
using HigherEducation.DataAccess;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace HigherEducation.Controllers
{
    public class APIDataController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        APIDataContext apiObj = new APIDataContext();

        private void ClearSessionAndCookies()
        {
            GenerateHashKeyForStore();
            Response.Cookies["yoyo"].Value = "";
            //Request.Cookies["yoyo"].Secure = true;
            Session.Remove("yoyo");
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        private void GenerateHashKeyForStore()
        {
            StringBuilder myStr = new StringBuilder();
            myStr.Append(Request.Browser.Browser);
            myStr.Append(Request.Browser.Platform);
            myStr.Append(Request.Browser.MajorVersion);
            myStr.Append(Request.Browser.MinorVersion);
            myStr.Append(Request.LogonUserIdentity.User.Value);
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashdata = sha.ComputeHash(Encoding.UTF8.GetBytes(myStr.ToString()));
            Session["BrowserId"] = Convert.ToBase64String(hashdata);
        }
        // GET: APIData

        public ActionResult viewCandidateRecords()
        {
            string Usertype = "0";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(Usertype);
            if (Session["collegeId"] != null)
            {
                //Security Check
                eSessionMgmt.AntiFixationInit();
                eSessionMgmt.AntiHijackInit();
                //Security Check
                return View();
            }
            else
            {
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });

            }
            
            //return View();
            
        }

        [HttpGet]
        public JsonResult d_getCollegeType()
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getCollegeType();
                }
                else
                {
                    TempData.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.APIDataController.[HttpPost] d_getCollegeType()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpGet]
        public JsonResult getCollegeListAsPerCollegeType(String collegeType)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {

                dt = apiObj.getCollegeListAsPerCollegeType(collegeType);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] getCollegeListAsPerCollegeType(" + collegeType + ")");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCourseListByCollege(String collegeId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = apiObj.GetCourseListByCollege(collegeId);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.APIDataController.[HttpGet] GetCourseListByCollege()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public JsonResult d_getStudentDetails(String collegeType,String collegeId, String courseId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getStudentDetailsList(collegeType,collegeId, courseId);
                }
                else
                {
                    TempData.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    //ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.APIDataController.[HttpPost] d_getStudentDetails()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult viewCandidateDetails(string RegID)
        {
            DataTable dt = new DataTable();
            var collegeid = "";
            if (Session["CollegeId"] != null && (Session["CollegeId"].ToString() != ""))
            {
                collegeid = Session["CollegeId"].ToString();
                if ((RegID == null || RegID == "") && collegeid != "")
                {
                    return RedirectToAction("../APIData/viewCandidateRecords");
                }
                ViewBag.regID = RegID;
                return View();
                
            }
            else
            {
                Response.Redirect("/dhe/frmlogin.aspx");
            }
            return View();
        }

        public JsonResult d_getCandidateDetails(String SRegid)
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.getCandidateRecord(SRegid);
                }
                else
                {
                    TempData.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    //ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.APIDataController.[HttpPost] d_getCandidateDetails()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public String d_updateDataOfStudent(String DATA)
        {
            DataTable dt = new DataTable();
            string mobile="", email="", shift="", unit="",regID="",catName="",pwdCat="",tName="",fName="",mName="";
            var result = "0";
            var uData = JsonConvert.DeserializeObject<Dictionary<string, string>>(DATA);
            foreach (var kv in uData)
            {
                if(kv.Key == "mobile")
                    mobile = kv.Value;
                if (kv.Key == "email")
                    email = kv.Value; 
                if (kv.Key == "shift")
                    shift = kv.Value;
                if (kv.Key == "unit")
                    unit = kv.Value;
                if (kv.Key == "category")
                    catName = kv.Value;
                if (kv.Key == "pwdCat")
                    pwdCat = kv.Value;
                if (kv.Key == "reg_ID")
                    regID = kv.Value;
                if (kv.Key == "traineeName")
                    tName = kv.Value;
                if (kv.Key == "fName")
                    fName = kv.Value;
                if (kv.Key == "mName")
                    mName = kv.Value;
            }

            try
            {
                if ((Session["UserID"] != null) && (Session["UserID"].ToString() != ""))
                {
                    dt = apiObj.UpdateDataforAPI(mobile,email,shift,unit,regID,catName,pwdCat,tName,fName,mName);
                    if (dt.Rows.Count > 0)
                    {
                        result="1";
                    }
                }
                else
                {
                    TempData.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpPost] d_updateDataOfStudent()");
            }
            return result;
        }

        [HttpPost]
        public JsonResult sendDataToDGT()
        {
            ITI objITI = new ITI();
            DataTable dt = new DataTable();
            dt = objITI.GetITIDataForAPI();
            int max = dt.Rows.Count;
            foreach (DataRow row in dt.Rows)
            {
                APIRequestData objAPIRequest = new APIRequestData();
                List<APIRequestData> objAPIRequestList = new List<APIRequestData>();
                objAPIRequest.StateRegNumber = Convert.ToString(row["StateRegNumber"]);
                objAPIRequest.TraineeName = Convert.ToString(row["TraineeName"]);
                objAPIRequest.UIDNumber = Convert.ToString(row["UIDNumber"]);
                objAPIRequest.DateOfBirth = Convert.ToString(row["DateOfBirth"]);
                objAPIRequest.Gender = Convert.ToString(row["Gender"]);
                objAPIRequest.Category = Convert.ToString(row["Category"]);
                objAPIRequest.FatherGuardianName = Convert.ToString(row["FatherGuardianName"]);
                objAPIRequest.MotherName = Convert.ToString(row["MotherName"]);
                objAPIRequest.MobileNumber = Convert.ToString(row["MobileNumber"]);
                objAPIRequest.EmailID = Convert.ToString(row["EmailID"]);
                objAPIRequest.Session = Convert.ToString(row["Session"]);
                objAPIRequest.AdmissionDate = Convert.ToString(row["AdmissionDate"]);
                objAPIRequest.HighestQualification = Convert.ToString(row["HighestQualification"]);
                objAPIRequest.Trade = Convert.ToString(row["Trade"]);
                objAPIRequest.Shift = Convert.ToString(row["Shift"]);
                objAPIRequest.Unit = Convert.ToString(row["Unit"]);
                objAPIRequest.IsTraineeDualMode = Convert.ToString(row["IsTraineeDualMode"]);
                objAPIRequest.MISITICode = Convert.ToString(row["MISITICode"]);
                objAPIRequest.PersonwithDisability = Convert.ToString(row["PersonwithDisability"]);
                objAPIRequest.PWDcategory = Convert.ToString(row["PWDcategory"]);
                objAPIRequest.EconomicWeakerSection = Convert.ToString(row["EconomicWeakerSection"]);
                objAPIRequest.TraineeType = Convert.ToString(row["TraineeType"]);
                objAPIRequest.TraineePhoto = Convert.ToString(row["TraineePhoto"]);
                objAPIRequestList.Add(objAPIRequest);
                string Result = objITI.CallAPIForSend(objAPIRequestList);
                if (!string.IsNullOrEmpty(Result))
                {
                    string json = Result;
                    var data = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
                    string sucessRec = data["sucessRec"].Value<string>();
                    objITI.SaveAPIResponse(Convert.ToString(row["SrNo"]), Convert.ToString(row["MISITICode"]), Convert.ToString(row["TraineeName"]), Convert.ToString(row["StateRegNumber"]), Result, sucessRec);
                }
            }
            if(max>0)
            {
                return Json("1", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult viewResponse()
        {
            if (Session["CollegeId"] != null && (Session["CollegeId"].ToString() != ""))
            {
                return View();
            }
            else
            {
                Response.Redirect("/dhe/frmlogin.aspx");
            }
            return View();
        }

        public JsonResult d_getErrorResponse(String collegeType, String collegeId, String courseId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.getErrorResponse(collegeType,collegeId, courseId);
                }
                else
                {
                    TempData.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    //ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.APIDataController.[HttpPost] d_getErrorResponse()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        #region Data for new Portal SID
        public ActionResult sendDataToSID()
        {
            if (Session["CollegeId"] != null && (Session["CollegeId"].ToString() != ""))
            {
                return View();
            }
            else
            {
                Response.Redirect("/dhe/frmlogin.aspx");
            }
            return View();
        }

        public ActionResult viewStudentRecordSID()
        {
            if (Session["CollegeId"] != null && (Session["CollegeId"].ToString() != ""))
            {
                return View();
            }
            else
            {
                Response.Redirect("/dhe/frmlogin.aspx");
            }
            return View();
        }
        public ActionResult viewSIDResponse()
        {
            if (Session["CollegeId"] != null && (Session["CollegeId"].ToString() != ""))
            {
                return View();
            }
            else
            {
                Response.Redirect("/dhe/frmlogin.aspx");
            }
            return View();
        }

        public ActionResult updateCredentials()
        {
            if (Session["CollegeId"] != null && (Session["CollegeId"].ToString() != ""))
            {
                return View();
            }
            else
            {
                Response.Redirect("/dhe/frmlogin.aspx");
            }
            return View();
        }

        [HttpGet]
        public JsonResult getUserData()
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.getStateUserData();
                }
                else
                {
                    TempData.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    //ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.APIDataController.[HttpGet] getUserData()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public String updateUserData(String Id, String Mobile, String Pass,String Type)
        {
            DataTable dt = new DataTable();
            String result = "0";
            try
            {
                if (Session["collegeId"] != null)
                {
                    String base64String = ConvertBase64Encode("{\"mobileNumber\":\""+ Mobile+"\",\"pin\": \""+Pass+"\",\"userType\":\""+Type+"\"}");
                    dt = apiObj.updateUserData(Id, Mobile, Pass,Type, base64String);
                    if (dt.Rows.Count > 0)
                    {
                        result = "1";
                    }
                }
                else
                {
                    TempData.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    //ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    return result;
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.APIDataController.[HttpPost] updateUserData()");
            }
            return result;
        }

        public string ConvertBase64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }

        public string getBase64UserData()
        {
            string result="";
            DataTable dt = new DataTable();
            dt = apiObj.getBase64Data();
            foreach (DataRow row in dt.Rows)
            {
                result = row["Body"].ToString();
            }
            return result;
        }
        
        [HttpPost]
        public JsonResult sendDataToSIDPortal()
        {
            DataTable dt = new DataTable();
            AccessData accData = null;
            TokenData t1 = new TokenData();
            t1.Body = getBase64UserData();
            ITI objITI = new ITI();
            accData = objITI.generateAccessToken(t1);
            dt = objITI.GetITIDataForAPI();
            int max = dt.Rows.Count;
            if (max > 0)
            {
                int r = objITI.CallAPIForSendDataSID(dt, accData.accessToken, accData.sessionId,max);
                if(r==1)
                    return Json("1", JsonRequestBehavior.AllowGet);
                else
                    return Json("2", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult getAPIResponseSID()
        {
            DataTable dt = new DataTable();
            AccessData accData = null;
            TokenData t1 = new TokenData();
            LogHistoryBody logHistory = new LogHistoryBody();
            ITI objITI = new ITI();
            t1.Body = getBase64UserData();
            accData = objITI.generateAccessToken(t1);
            logHistory.LogId = objITI.getLogID();
            logHistory.PageSize = "100000";
            logHistory.PageNumber = "1";
            
            int r = objITI.getAPIResponseSID(logHistory,accData.accessToken, accData.sessionId);
            if (r == 1)
                return Json("1", JsonRequestBehavior.AllowGet);
            else if (r == 2)
                return Json("2", JsonRequestBehavior.AllowGet);
            else
                return Json("0", JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}