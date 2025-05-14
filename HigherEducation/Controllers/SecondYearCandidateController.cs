using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HigherEducation.BusinessLayer;
using HigherEducation.DataAccess;
using HigherEducation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace HigherEducation.Controllers
{
    public class SecondYearCandidateController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        SecondYearCandidateContext apiObj = new SecondYearCandidateContext();

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

        public ActionResult viewVerifiredCandidateRecords()
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

        public ActionResult viewAllCandidateRecords()
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

        public ActionResult viewCandidatePass()
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
        public JsonResult getAdmissionSession()
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getAdmissionSession();
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
                logger.Error(ex, "Error occured in HigherEducation.SecondYearCandidateController.[HttpGet] getAdmissionSession()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

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
                logger.Error(ex, "Error occured in HigherEducation.SecondYearCandidateController.[HttpPost] d_getCollegeType()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpGet]
        public JsonResult getCollegeListAsPerCollegeType(String collegeType, String sessionId)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {

                dt = apiObj.getCollegeListAsPerCollegeType(collegeType, sessionId);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] getCollegeListAsPerCollegeType(" + collegeType + ")");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetCollegeList(String sessionId)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getCollegeNames(sessionId);//collegeid and courseId
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
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] GetCollegeList()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSectionList(String CollegeId, String CourseId,String SessionId)
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.D_GetSectionList(CollegeId, CourseId, SessionId);//collegeid and courseId
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
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] GetSectionList()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCourseList(String collegeId, String sessionId)
        {

            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getCourseNameAsPerCollege(collegeId, sessionId);//collegeid and courseId
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
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] GetCourseList()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult d_getStudentDetailsNO(String collegeId, String courseId, String sectionId, String SessionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getStudentDetailsListNO(collegeId, courseId, sectionId, SessionId);
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
                logger.Error(ex, "Error occured in HigherEducation.SecondYearCandidateController.[HttpPost] d_getStudentDetailsNO()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult updateStudentStatus(String collegeId, String courseId, String sectionId, String checkID)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_updateStudentStatus(collegeId, courseId, sectionId, checkID);
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
                logger.Error(ex, "Error occured in HigherEducation.SecondYearCandidateController.[HttpPost] updateStudentStatus()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult d_getStudentDetailsYES(String collegeId, String courseId, String sectionId, String SessionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getStudentDetailsListYES(collegeId, courseId, sectionId, SessionId);
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
                logger.Error(ex, "Error occured in HigherEducation.SecondYearCandidateController.[HttpPost] d_getStudentDetailsYES()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        [HttpPost]
        public JsonResult d_getStudentDetailsALL(String collegeId, String courseId, String sectionId, String SessionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getStudentDetailsListALL(collegeId, courseId, sectionId, SessionId);
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
                logger.Error(ex, "Error occured in HigherEducation.SecondYearCandidateController.[HttpPost] d_getStudentDetailsALL()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult d_getStudentLoginDetails(String collegeId, String courseId, String sectionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getStudentLoginDetails(collegeId, courseId, sectionId);
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
                logger.Error(ex, "Error occured in HigherEducation.SecondYearCandidateController.[HttpPost] d_getStudentLoginDetails()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        // for fee quarterly fee details
        public ActionResult viewQuarterFeeDetails()
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

        [HttpPost]
        public JsonResult d_getFeeReceiptListQuarterly(String collegeType, String collegeId, String courseId, String sectionId, String Qtr, String SessionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = apiObj.d_getFeeReceiptListQuarterly(collegeType, collegeId, courseId, sectionId, Qtr, SessionId);
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
                logger.Error(ex, "Error occured in HigherEducation.SecondYearCandidateController.[HttpPost] d_getFeeReceiptListQuarterly()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


    }
}