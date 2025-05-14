using HigherEducation.DataAccess;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HigherEducation.Controllers
{
    public class CandidateDetailsITIWiseController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        CandidateDetailsITIWiseContext objCandidateDetails = new CandidateDetailsITIWiseContext();
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
        // GET: CondidateDetailsITIWise
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CandidateDetailsITIWise()
        {
            return View();
        }
        [HttpGet]
        public JsonResult getCollegeListAsPerCollegeType(String collegeType)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {

                dt = objCandidateDetails.getCollegeListAsPerCollegeType(collegeType);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] getCollegeListAsPerCollegeType(" + collegeType + ")");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetTradeListByITI(String collegeId)
        {

            DataTable dt = new DataTable();
            try
            {
                dt = objCandidateDetails.GetTradeListByITI(collegeId);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeNames()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSectionList(String CollegeId, String CourseId)
        {
            DataSet dt = new DataSet();

            dt = objCandidateDetails.GetSectionList(CollegeId, CourseId);//collegeid and courseId

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getStudentDetails(String collegeType,String collegeId, String courseId, String sectionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = objCandidateDetails.getStudentDetailsList(collegeType,collegeId, courseId, sectionId);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] getCashBookList()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpPost]
        public JsonResult d_getStudentDetails(String collegeId, String courseId, String sectionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = objCandidateDetails.d_getStudentDetailsList(collegeId, courseId, sectionId);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] getCashBookList()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpPost]
        public JsonResult getStudentDetails_2022(String collegeId, String courseId, String sectionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = objCandidateDetails.getStudentDetailsList_2022(collegeId, courseId, sectionId);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] getStudentDetails_2022()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult CandidateDetailsITIWise_2022()
        {
            return View();
        }


    }
}