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
    public class SummaryRepController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        ReportsContext rptObj = new ReportsContext();
        SummaryContext objSummary = new SummaryContext();
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
        // GET: SummaryRep
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult getSummaryList(String collegeType,String collegeId, String courseId, String sectionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = objSummary.getTotalSeatList(collegeType,collegeId, courseId, sectionId);
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
                logger.Error(ex, "Error occured in HigherEducation.SummaryRepController.[HttpPost] getSummaryList()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpGet]
        public JsonResult GetTradeListByITI(String collegeId)
        {
            
            DataTable dt = new DataTable();
            try
            {
                dt = objSummary.GetTradeListByITI(collegeId);//collegeid and courseId

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

            dt = objSummary.GetSectionList(CollegeId, CourseId);//collegeid and courseId

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getCollegeListAsPerCollegeType(String collegeType)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {

                dt = objSummary.getCollegeListAsPerCollegeType(collegeType);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] getCollegeListAsPerCollegeType(" + collegeType + ")");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        // for course wise admission List
        [HttpPost]
        public JsonResult getCourseWiseList(String collegeType, String collegeId, String courseId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = objSummary.getCourseWiseList(collegeType, collegeId, courseId);
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
    }
}