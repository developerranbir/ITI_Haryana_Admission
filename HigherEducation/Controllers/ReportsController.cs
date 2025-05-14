using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HigherEducation.DataAccess;
using HigherEducation.BusinessLayer;
using HigherEducation.Models;


namespace HigherEducation.Controllers
{
    public class ReportsController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        ReportsContext rptObj = new ReportsContext();

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
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CollegeCourseWiseAppRec()
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

        }

        public ActionResult GetSummaryRep()
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

        public ActionResult GetCourseWiseRep()
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

        public ActionResult CourseWiseStudentDetail()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetCourseCollegeWiseReport(string collegeId, string courseId, String UGPG)
        {

            //string regId = "";

            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.GetCourseCollegeWiseApplications(collegeId, courseId, UGPG);//collegeid and courseId
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json(new { });
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] GetCandidateVerifyObjection()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PGGetCourseCollegeWiseReport(string collegeId, string courseId)
        {

            //string regId = "";

            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.PGGetCourseCollegeWiseApplications(collegeId, courseId);//collegeid and courseId
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json(new { });
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] GetCandidateVerifyObjection()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ObjectionsReport()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getObjectionsReport(String collegeId)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                //if (Session["RegId"] != null)
                //{
                //    regId = Session["RegId"].ToString();
                //}
                //else
                //{
                //    TempData.Clear();
                //    System.Web.Security.FormsAuthentication.SignOut();
                //    Session.RemoveAll();
                //    Session.Clear();
                //    Session.Abandon();
                //    Response.Cookies.Clear();
                //    ClearSessionAndCookies();
                //    TempData["SessionExpired"] = 1;
                //    return Json(new
                //    {
                //        redirectUrl = Url.Action("Login", "Account"),
                //        isRedirect = true
                //    });
                //}

                dt = rptObj.ObjectionsReport(collegeId);//collegeid and courseId
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] GetCandidateVerifyObjection()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCollegeList()
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                //if (Session["RegId"] != null)
                //{
                //    regId = Session["RegId"].ToString();
                //}
                //else
                //{
                //    TempData.Clear();
                //    System.Web.Security.FormsAuthentication.SignOut();
                //    Session.RemoveAll();
                //    Session.Clear();
                //    Session.Abandon();
                //    Response.Cookies.Clear();
                //    ClearSessionAndCookies();
                //    TempData["SessionExpired"] = 1;
                //    return Json(new
                //    {
                //        redirectUrl = Url.Action("Login", "Account"),
                //        isRedirect = true
                //    });
                //}

                dt = rptObj.d_getCollegeNames();//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeNames()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCourseList(String collegeId)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                //if (Session["RegId"] != null)
                //{
                //    regId = Session["RegId"].ToString();
                //}
                //else
                //{
                //    TempData.Clear();
                //    System.Web.Security.FormsAuthentication.SignOut();
                //    Session.RemoveAll();
                //    Session.Clear();
                //    Session.Abandon();
                //    Response.Cookies.Clear();
                //    ClearSessionAndCookies();
                //    TempData["SessionExpired"] = 1;
                //    return Json(new
                //    {
                //        redirectUrl = Url.Action("Login", "Account"),
                //        isRedirect = true
                //    });
                //}

                dt = rptObj.d_getCourseNameAsPerCollege(collegeId);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeNames()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult d_GetHeaderStateData()
        {
            //string regId = "";
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_GetHeaderStateData();//collegeid and courseId
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
                    return Json(new
                    {
                        redirectUrl = Url.Action("Login", "Account"),
                        isRedirect = true
                    });
                }



            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeNames()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        #region DashboardReports
        public ActionResult ShowDash()
        {

            return View();
            //string Usertype = "0";
            //eDISHAutil eSessionMgmt = new eDISHAutil();
            //eSessionMgmt.CheckSession(Usertype);
            //if (Session["collegeId"] != null)
            //{
            //    //Security Check
            //    eSessionMgmt.AntiFixationInit();
            //    eSessionMgmt.AntiHijackInit();
            //    //Security Check
            //    return View();
            //}
            //else
            //{
            //    Response.Redirect("~/dhe/frmlogin.aspx");
            //    return Json(new { });

            //}
        }

        [HttpGet]
        public JsonResult d_GetDateWiseRegistrations()
        {
            //string regId = "";
            DataSet dt = new DataSet();
            try
            {
                dt = rptObj.d_GetDateWiseRegistrations();//collegeid and courseId
                /*if (Session["collegeId"] != null)
                {
                   
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
                    return Json(new
                    {
                        redirectUrl = Url.Action("Login", "Account"),
                        isRedirect = true
                    });
                }*/



            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_GetDateWiseRegistrations()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowDashPG()
        {
            return View();

        }
        [HttpGet]
        public JsonResult dPG_GetDateWiseRegistrations()
        {
            //string regId = "";
            DataSet dt = new DataSet();
            try
            {
                dt = rptObj.dPG_GetDateWiseRegistrations();//collegeid and courseId
                /*if (Session["collegeId"] != null)
                {
                   
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
                    return Json(new
                    {
                        redirectUrl = Url.Action("Login", "Account"),
                        isRedirect = true
                    });
                }*/



            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_GetDateWiseRegistrations()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult SectionWiseAppRec()
        {
            return View();
        }
        public JsonResult GetSectionWiseReport(string collegeId, string courseId, String UGPG)
        {

            //string regId = "";

            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.GetSectionWiseCandidate(collegeId, courseId, UGPG);//collegeid and courseId
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json(new { });
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] GetCandidateVerifyObjection()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        #region MeritListCutoffList
        public ActionResult MeritList()
        {
            //Session["collegeId"] = "0";
            // Session["UserId"] = "";
            //  Session["collegeId"] = "";
            return View();
        }
        public ActionResult MeritList2()
        {
            if (Session["collegeId"] == null || Convert.ToString(Session["collegeId"]) == "")
            {
                TempData.Clear();
                System.Web.Security.FormsAuthentication.SignOut();
                Session.RemoveAll();
                Session.Clear();
                Session.Abandon();
                Response.Cookies.Clear();
                ClearSessionAndCookies();
                TempData["SessionExpired"] = 1;
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });
            }

            return View();
        }
        public ActionResult FeeReceiptList()
        {

            return View();
        }
        public ActionResult CutOffList()
        {
            return View();
        }
        [HttpPost]
        public JsonResult d_getMeritList(String UGPG, String CollegeId, String CourseId, String SectionId, String MeritId)
        {
            DataSet dt = new DataSet();

            dt = rptObj.d_getMeritList(UGPG, CollegeId, CourseId, SectionId, MeritId);//collegeid and courseId

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }
        [HttpPost]
        public JsonResult d_getMeritList2(String UGPG, String CollegeId, String CourseId, String SectionId, String MeritId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getMeritList2(UGPG, CollegeId, CourseId, SectionId, MeritId);//collegeid and courseId
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] d_getMeritList2(" + CollegeId + CourseId + SectionId + ")");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            //var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
        }
        [HttpPost]
        public JsonResult GetSectionList(String CollegeId, String CourseId)
        {
            DataSet dt = new DataSet();

            dt = rptObj.D_GetSectionList(CollegeId, CourseId);//collegeid and courseId

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult d_getMeritFeeList(String CollegeId, String CourseId, String SectionId, String CollegeType, String ShowDuplicates)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getMeritFeeList(CollegeId, CourseId, SectionId, CollegeType, ShowDuplicates);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getMeritFeeList(" + CollegeId + CourseId + SectionId + ")");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult FeeReceiptPartial(string regid = "", string TxnId = "")
        {

            if (Session["collegeId"] == null || Session["collegeId"].ToString() == "")
            {
                return RedirectToAction("LogOut");
            }
            else
            {
                Session["regid"] = regid;

            }
            FeeModule ObjfeeModule1 = new FeeModule();

            try
            {
                List<FeeModule> ObjfeeModule = new List<FeeModule>();
                ObjfeeModule1 = rptObj.GetCandidatePaymentSuccesDetail(regid, TxnId);
            }
            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] FeeReceipt(),'" + regid + "'");
            }
            return PartialView(ObjfeeModule1);
        }

        public ActionResult FeeReceiptPartial2(string TxnId = "")
        {


            FeeModule ObjfeeModule1 = new FeeModule();

            try
            {
                List<FeeModule> ObjfeeModule = new List<FeeModule>();
                String RegId = rptObj.d_GetRegIDForTxnNo(TxnId);
                Session["regid"] = RegId;
                ObjfeeModule1 = rptObj.GetCandidatePaymentSuccesDetail_abhi(RegId, TxnId);
            }
            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] FeeReceipt(),'" + TxnId + "'");
            }
            return PartialView(ObjfeeModule1);
        }
        #endregion
        public ActionResult CancellationReport()
        {

            return View();
        }
        [HttpPost]
        public JsonResult d_getCancellationReport(String CollegeId, String UGPG)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getCancellationReport(CollegeId, UGPG);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getCancellationReport(" + CollegeId + ")");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpPost]
        public JsonResult d_getCancelAppDoc(String RegId, String UGPG)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getCancelAppDoc(RegId, UGPG);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getCancelAppDoc(" + RegId + ")");
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
                    dt = rptObj.d_getCollegeType();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getCollegeType()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        // For District List
        [HttpGet]
        public JsonResult getDistrictList()
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getDistrictNameforReport();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] getDistrictList()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        // for District wise ITI List
        [HttpGet]
        public JsonResult getCollegeListAsPerCollegeDistrict(String collegeType, String distName)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {

                dt = rptObj.getCollegebyCollegeTypeAndDistrict(collegeType, distName);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] getCollegeListAsPerCollegeDistrict()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getITIAdmissionStatus(String District, String CollegeType, String collegeId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.getITIAdmissionStatusRpt(District, CollegeType, collegeId);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] getITIAdmissionStatus()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        [HttpGet]
        public JsonResult d_getCollegeListAsPerCollegeType(String collegeType)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                //if (Session["RegId"] != null)
                //{
                //    regId = Session["RegId"].ToString();
                //}
                //else
                //{
                //    TempData.Clear();
                //    System.Web.Security.FormsAuthentication.SignOut();
                //    Session.RemoveAll();
                //    Session.Clear();
                //    Session.Abandon();
                //    Response.Cookies.Clear();
                //    ClearSessionAndCookies();
                //    TempData["SessionExpired"] = 1;
                //    return Json(new
                //    {
                //        redirectUrl = Url.Action("Login", "Account"),
                //        isRedirect = true
                //    });
                //}

                dt = rptObj.d_getCollegeNamesFromCollegeType(collegeType);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeListAsPerCollegeType()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public ActionResult VacantSeats()
        {
            return View();
        }
        [HttpPost]
        public JsonResult d_getVacancyReport(String collegeId, String courseId, String sectionId, String MeritId, String UGPG)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getVacancyReport(collegeId, courseId, sectionId, MeritId, UGPG);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getVacancyReport()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpPost]
        public JsonResult d_getStudentDetailRollNo(String collegeId, String courseId, String UGPG)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getStudentDetailRollNo(collegeId, courseId, UGPG);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getStudentDetailRollNo()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult GetFeeReceiptList()
        {
            return View();
        }
        [HttpPost]
        public JsonResult d_getFeeReceiptList(String collegeId, String courseId, String sectionId, String UGPG)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getFeeReceiptList(collegeId, courseId, sectionId, UGPG);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getFeeReceiptList()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult FeeCollectionReport()
        {
            return View();
        }
        [HttpPost]
        public JsonResult d_getFeeCollectionReport(String CollegeType, String collegeId, String courseId = "0", String sectionId = "0")
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getFeeCollectionReport(CollegeType, collegeId, courseId, sectionId);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getFeeCollectionReport()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult REPORT()
        {
            return View();
        }
        [System.Web.Http.HttpGet]
        public HttpResponse d_getStudentDetailRollNo_Download(String collegeId, String courseId, String UGPG)
        {
            try
            {
                // SaralSashanDB db = new SaralSashanDB();
                DataTable vdt = new DataTable();
                vdt = rptObj.d_getStudentDetailRollNo_Download(collegeId, courseId, UGPG);

                HttpResponse Response = System.Web.HttpContext.Current.Response;

                string attachment = "attachment; filename=Inward.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in vdt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in vdt.Rows)
                {
                    tab = "";
                    for (i = 0; i < vdt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

                return Response; //CreateResponse(HttpStatusCode.OK, vdt);
            }
            catch (Exception ex)
            {
                //SaveError(ex);
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Write("Error failed to load file");
                return response;
            }

        }
        [System.Web.Http.HttpGet]
        public HttpResponse DownloadRR()
        {
            try
            {
                // SaralSashanDB db = new SaralSashanDB();
                DataTable vdt = new DataTable();
                vdt = rptObj.d_Download_RR();


                HttpResponse Response = System.Web.HttpContext.Current.Response;
                string attachment = "attachment; filename=CollegeRR.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in vdt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in vdt.Rows)
                {
                    tab = "";
                    for (i = 0; i < vdt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

                return Response; //CreateResponse(HttpStatusCode.OK, vdt);
            }
            catch (Exception ex)
            {
                //SaveError(ex);
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Write("Error failed to load file");
                return response;
            }

        }
        public ActionResult TrackStudent()
        {
            return View();
        }
        [HttpGet]
        public JsonResult DPG_getCollegeNames(String UGPG)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                //if (Session["RegId"] != null)
                //{
                //    regId = Session["RegId"].ToString();
                //}
                //else
                //{
                //    TempData.Clear();
                //    System.Web.Security.FormsAuthentication.SignOut();
                //    Session.RemoveAll();
                //    Session.Clear();
                //    Session.Abandon();
                //    Response.Cookies.Clear();
                //    ClearSessionAndCookies();
                //    TempData["SessionExpired"] = 1;
                //    return Json(new
                //    {
                //        redirectUrl = Url.Action("Login", "Account"),
                //        isRedirect = true
                //    });
                //}

                dt = rptObj.DPG_getCollegeNames(UGPG);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");

                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] dPG_getCollegeNames(" + UGPG == null ? "NULL" : UGPG + ")");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        //Get UG PG CHANGES
        [HttpGet]
        public JsonResult GetCollegeListUGPG(String UGPG)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {

                dt = rptObj.d_getCollegeNamesUGPG(UGPG);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeNames()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetCourseListUGPG(String collegeId, String UGPG)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                //if (Session["RegId"] != null)
                //{
                //    regId = Session["RegId"].ToString();
                //}
                //else
                //{
                //    TempData.Clear();
                //    System.Web.Security.FormsAuthentication.SignOut();
                //    Session.RemoveAll();
                //    Session.Clear();
                //    Session.Abandon();
                //    Response.Cookies.Clear();
                //    ClearSessionAndCookies();
                //    TempData["SessionExpired"] = 1;
                //    return Json(new
                //    {
                //        redirectUrl = Url.Action("Login", "Account"),
                //        isRedirect = true
                //    });
                //}

                dt = rptObj.d_getCourseNameAsPerCollegeUGPG(collegeId, UGPG);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeNames()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSectionListUGPG(String CollegeId, String CourseId, String UGPG)
        {
            DataSet dt = new DataSet();

            dt = rptObj.D_GetSectionListUGPG(CollegeId, CourseId, UGPG);//collegeid and courseId

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult d_getCollegeListAsPerCollegeTypeUGPG(String collegeType, String UGPG)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {

                dt = rptObj.d_getCollegeNamesFromCollegeTypeUGPG(collegeType, UGPG);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeListAsPerCollegeTypeUGPG(" + collegeType + "," + UGPG + ")");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpGet]
        public HttpResponse DownloadRRPG()
        {
            try
            {
                // SaralSashanDB db = new SaralSashanDB();
                DataTable vdt = new DataTable();
                vdt = rptObj.d_Download_RRPG();


                HttpResponse Response = System.Web.HttpContext.Current.Response;
                string attachment = "attachment; filename=CollegeRR.xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                string tab = "";
                foreach (DataColumn dc in vdt.Columns)
                {
                    Response.Write(tab + dc.ColumnName);
                    tab = "\t";
                }
                Response.Write("\n");
                int i;
                foreach (DataRow dr in vdt.Rows)
                {
                    tab = "";
                    for (i = 0; i < vdt.Columns.Count; i++)
                    {
                        Response.Write(tab + dr[i].ToString());
                        tab = "\t";
                    }
                    Response.Write("\n");
                }
                Response.End();

                return Response; //CreateResponse(HttpStatusCode.OK, vdt);
            }
            catch (Exception ex)
            {
                //SaveError(ex);
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Write("Error failed to load file");
                return response;
            }

        }
        [HttpPost]
        public JsonResult d_getFeeCollectionReportUGPG(String CollegeType, String collegeId, String courseId = "0", String sectionId = "0", String UGPG = "UG")
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getFeeCollectionReportUGPG(CollegeType, collegeId, courseId, sectionId, UGPG);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getFeeCollectionReport()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        //END UG PG CHANGES
        public ActionResult SubHeadWiseCollectionReport()
        {
            return View();
        }
        [HttpPost]
        public JsonResult d_getSubHeadWiseReportUGPG(String collegeId, String courseId = "0", String sectionId = "0", String UGPG = "UG")
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getSubHeadWiseReportUGPG(collegeId, courseId, sectionId, UGPG);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getSubHeadWiseReportUGPG()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public ActionResult SearchCollegeIndex()
        {
            return View();
        }

        //[HttpGet]
        //public JsonResult d_GetDistrictName()
        //{
        //    //string regId = "";
        //    DataTable dt = new DataTable();
        //    try
        //    {

        //        dt = rptObj.d_getDistrictName();//collegeid and courseId

        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] d_GetDistrictName() ");
        //    }
        //    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public JsonResult d_getCollegeFilter(int? p_college_id, int? p_course_id,
                                                                int? p_section_id, int? p_district_id,
                                                                int? p_collegeType_id, int? p_iswomen_id,
                                                                string p_UGPG)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {

                dt = rptObj.d_getCollegeFilter(p_college_id, p_course_id, p_section_id, p_district_id, p_collegeType_id, p_iswomen_id, p_UGPG);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, String.Format("Error occured in HigherEducation.AccountContoller.[HttpGet] d_getCollegeListAsPerCollegeTypeUGPG(p_college_id:{0},p_course_id:{1},p_section_id:{2},p_district_id:{3},p_collegeType_id:{4},p_iswomen_id:{5},p_UGPG:{6})", p_college_id, p_course_id, p_section_id, p_district_id, p_collegeType_id, p_iswomen_id, p_UGPG));
            }
            //return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }
        [HttpGet]
        public JsonResult f_getFilterMenu()//not required anymore
        {
            //string regId = "";
            DataSet ds = new DataSet();
            try
            {

                ds = rptObj.f_getFilterMenu();//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] f_getFilterMenu()");
            }
            //return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(ds), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        [HttpGet]
        public JsonResult f_getCollegeDetails(string p_college_id, string p_ugpg)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            clsCollegeSearch cs = new clsCollegeSearch();
            try
            {
                cs.collegeid = p_college_id;
                if (p_ugpg == "UG") { dt = cs.GetCollegeInfo(); } else { dt = cs.GetCollegeInfo_PG(); }
                //dt = cs.GetCollegeInfo_pg();
                //dt = cs.GetCollegeInfo();//collegeid and courseId
                //int i = 0;
                //foreach (DataRow row in dt.Rows)
                //{
                //    if (i == 0)
                //    {
                //        i++;
                //    }
                //    else
                //    {
                //        row["docs"] = "";
                //    }

                //}
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] f_collegeDetails()");
            }
            //return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpGet]
        public ActionResult Logger(string login, string password)
        {
            if (login.ToString() == "logger" && password.ToString() == "Logger@123")
            {
                return View();
            }
            return Json(new { });

        }
        [HttpGet]
        public JsonResult d_getCollegeCourseFees(string p_college_id, string p_ugpg)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            ReportsContext cs = new ReportsContext();
            try
            {
                dt = cs.d_getCollegeCourseFee(p_college_id, p_ugpg);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] f_collegeDetails()");
            }
            //return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        public ActionResult GetCollegeCourseFeesReport()
        {
            return View();
        }


        public ActionResult VerificationReport()
        {

            return View();
        }
        public ActionResult d_VerificationReport(String collegeId, String v_status)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            ReportsContext cs = new ReportsContext();



            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = cs.d_VerificationReport(collegeId, v_status);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] d_VerificationReport()");
            }
            //return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
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

                dt = rptObj.getCollegeListAsPerCollegeType(collegeType);//collegeid and courseId

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] getCollegeListAsPerCollegeType(" + collegeType + ")");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult d_getStudentDetails(String collegeId, String courseId, String sectionId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getStudentDetailsList(collegeId, courseId, sectionId);
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
        public JsonResult getCollegeName(String collegeId)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.getCollegeName(collegeId);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getFeeReceiptList()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        public JsonResult getSeatAllotment(String CollegeType, String collegeId, string counselling)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.getSeatAllotment(CollegeType, collegeId, counselling);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] getSeatAllotment()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }
        [HttpGet]
        public ActionResult SeatAllotmentRep()
        {
            return View();
        }
        public JsonResult getCondidateDetail(String RegNO, string UserStatus)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.getCondidateDetail(RegNO, UserStatus);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getFeeReceiptList()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult IDCardReport()
        {
            return View();
        }
        public ActionResult d_getIdCard(string RegId)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            ReportsContext cs = new ReportsContext();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = cs.d_getIdentityCard(RegId);
                    //IDCardData dataforPartialView = new IDCardData();
                    if (dt.Rows.Count > 0)
                    {
                        ViewBag.applicant_name = dt.Rows[0]["applicant_name"].ToString();
                        ViewBag.trade = dt.Rows[0]["trade"].ToString();
                        ViewBag.registrationID = dt.Rows[0]["registrationID"].ToString();
                        ViewBag.applicant_dob = dt.Rows[0]["applicant_dob"].ToString();
                        ViewBag.issue_date = dt.Rows[0]["issue_date"].ToString();
                        ViewBag.valid_upto = dt.Rows[0]["valid_upto"].ToString();
                        ViewBag.session = dt.Rows[0]["session"].ToString();
                        ViewBag.residential_address = dt.Rows[0]["residential_address"].ToString();
                        ViewBag.father_name = dt.Rows[0]["father_name"].ToString();
                        ViewBag.mother_name = dt.Rows[0]["mother_name"].ToString();
                        ViewBag.MobileNo = dt.Rows[0]["MobileNo"].ToString();
                        ViewBag.EmailID = dt.Rows[0]["EmailID"].ToString();
                        ViewBag.collegename = dt.Rows[0]["collegename"].ToString().ToUpper();
                        ViewBag.college_address = dt.Rows[0]["college_address"].ToString();
                        ViewBag.phoneno = dt.Rows[0]["phoneno"].ToString();
                        ViewBag.website = dt.Rows[0]["website"].ToString();
                        ViewBag.photo = dt.Rows[0]["photo"].ToString();
                        ViewBag.Principal_Name = dt.Rows[0]["Principal_Name"].ToString();
                        ViewBag.coursesession = dt.Rows[0]["coursesession"].ToString();
                        ViewBag.QrCodeImgBase64 = cs.GenerateQrCode(ViewBag.registrationID, ViewBag.applicant_name, ViewBag.trade, ViewBag.issue_date, ViewBag.valid_upto, ViewBag.collegename, ViewBag.father_name, ViewBag.mother_name, ViewBag.MobileNo, ViewBag.coursesession);
                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] d_getIdCard()");
            }

            return PartialView();
        }


        /// <summary>
        /// Code for Quarterly Fee report
        /// </summary>
        /// <returns></returns>

        public ActionResult GetFeeReceiptQuarterly()
        {
            return View();
        }

        [HttpPost]
        public JsonResult d_getFeeReceiptListQuarterly(String collegeId, String courseId, String sectionId, String UGPG, String Qtr)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getFeeReceiptListQuarterly(collegeId, courseId, sectionId, UGPG, Qtr);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getFeeReceiptListQuarterly()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        /// <summary>
        /// Code for ERP Data Module
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDataForERP()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getDataForERP()
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.getDataForERP();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] getDataForERP()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }



        #region added by ranbir singh on 17-May-2023
        public ActionResult GetseatallotmentReport()
        {
            if (Session["collegeId"] != null && Session["UserType"].ToString() == "1")
            {
                return View();
            }
            else
            {
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });

            }
        }

        [HttpGet]
        public JsonResult d_getCounselling()
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getCounselling();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getCounselling()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult GetSeatAllotmentcandidate(String counselling)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null && Session["UserType"].ToString() == "1")
                {
                    dt = rptObj.GetSeatAllotmentCandidate(counselling);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] GetSeatAllotment()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult GetRankingReport()
        {
            if (Session["collegeId"] != null && Session["UserType"].ToString() == "1")
            {
                return View();
            }
            else
            {
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });

            }
        }

        public JsonResult d_getCounsellingforRank()
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getCounsellingforRank();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getCounsellingforRank()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult GetRanking(String classType, String counselling)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null && Session["UserType"].ToString() == "1")
                {
                    dt = rptObj.GetRankingDetails(classType, counselling);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] GetSeatAllotment()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
        #endregion

        public ActionResult GetFeeReceiptQuarterly22_24()
        {
            return View();
        }

        [HttpPost]
        public JsonResult d_getFeeReceiptListQuarterly22_24(String collegeId, String courseId, String sectionId, String UGPG, String Qtr)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getFeeReceiptListQuarterly22_24(collegeId, courseId, sectionId, UGPG, Qtr);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getFeeReceiptListQuarterly22_24()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        #region for candidate list with Panelty and differ cases
        public ActionResult GetCandidateList()
        {
            if (Session["collegeId"] != null && Session["UserType"].ToString() == "1")
            {
                return View();
            }
            else
            {
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });

            }
        }

        public JsonResult d_getChallanStatus()
        {
            DataTable dt = new DataTable();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.d_getChallanStatus();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] d_getChallanStatus()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public JsonResult GetCandidateListStatus(String cStatus, String counselling, String isPenalty)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null && Session["UserType"].ToString() == "1")
                {
                    dt = rptObj.GetCandidateDetails(cStatus, counselling, isPenalty);
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] GetCandidateListStatus()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        #endregion


        public ActionResult GetDataAdmissionReport()
        {
            return View();
        }

        //ranbir Singh
        [HttpPost]
        public JsonResult getDataAdmissionReport()
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.getDataForAdmssionReport();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpPost] getDataAdmissionReport()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        public ActionResult CancelledRegistrationReport()
        {
            if (Session["collegeId"] != null)
            {
                return View();
            }
            else
            {
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });

            }
        }
        [HttpGet]
        public JsonResult GetCancelledRegistrationReport()
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.GetCancelledRegistration();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] GetCancelledRegistrationReport()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public ActionResult CancelledAdmissionReport()
        {
            if (Session["collegeId"] != null)
            {
                return View();
            }
            else
            {
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });

            }
        }

        [HttpGet]
        public JsonResult GetCancelledAdmissionReport()
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.GetCancelledAdmission();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] GetCancelledAdmissionReport()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult RestoreAdmissionReport()
        {
            if (Session["collegeId"] != null)
            {
                return View();
            }
            else
            {
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });

            }
        }

        [HttpGet]
        public JsonResult GetRestoredAdmissionReport()
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["collegeId"] != null)
                {
                    dt = rptObj.GetRestoredAdmission();
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
                logger.Error(ex, "Error occured in HigherEducation.ReportsController.[HttpGet] GetRestoredAdmissionReport()");
            }
            // return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            var jsonResult = Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

    }
}
