using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HigherEducation.Models;
using HigherEducation.DataAccess;
using System.Web.Security;
using NLog;
using System.Drawing;
using CaptchaMvc.HtmlHelpers;
using Newtonsoft.Json;
using HigherEducation.BusinessLayer;
using HigherEducation.BAL;

namespace HigherEducation.Controllers
{

    public class VerificationController : Controller
    {

        Logger logger = LogManager.GetCurrentClassLogger();
        VerificationDbContext verifyObj = new VerificationDbContext();
        VerificationDbContext UGverifyObj = new VerificationDbContext();
        EncryptionDecryption dec = new EncryptionDecryption();//ADD
        EducationDbContext EducationContext = new EducationDbContext();
        // GET: Verification
        public ActionResult Index()
        {
            string UserType = "2";// for debugging.
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);
            if (Session["collegeId"] != null)
            {
                ////Security Check
                eSessionMgmt.SetCookie();
                //Security Check

                return View();
            }
            else
            {
                //Response.Redirect("~/dhe/frmlogin.aspx");
                return RedirectToAction("Account", "Error");
                //return Json(new { });

            }

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

        private void ClearSessionAndCookies()
        {
            GenerateHashKeyForStore();
            Response.Cookies["yoyo"].Value = "";
            Request.Cookies["yoyo"].Secure = true;
            Session.Remove("yoyo");
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        //public ActionResult v_GetDocument(string DocId) //This Requires the CollegeID and Registration Id to be present in Session
        //{
        //    Int32 collegeId ;
        //    String RegId;

        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != "") && Session["RegId"].ToString() != "")
        //        {
        //            collegeId = Convert.ToInt32(Session["CollegeId"]);
        //            RegId = Session["RegId"].ToString();
        //            dt = verifyObj.v_GetDocs(RegId,DocId);
        //        }
        //        else
        //        {
        //            TempData.Clear();
        //            FormsAuthentication.SignOut();
        //            Session.RemoveAll();
        //            Session.Clear();
        //            Session.Abandon();
        //            Response.Cookies.Clear();
        //            ClearSessionAndCookies();
        //            TempData["SessionExpired"] = 1;
        //            //Response.Redirect("~/dhe/frmlogin.aspx");
        //            return Json("1", JsonRequestBehavior.AllowGet);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.VerificationController.getStudentList()");
        //    }


        //    return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult v_GetDocument(string DocId, string regid) //This Requires the CollegeID to be present in Session
        //{
        //    // Int32 collegeId;
        //    // String RegId;

        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != ""))
        //        {
        //            //collegeId = Convert.ToInt32(Session["CollegeId"]);
        //            //RegId = Session["RegId"].ToString();
        //            //dt = UGverifyObj.v_GetDocs(RegId, DocId);
        //            dt = UGverifyObj.v_GetDocs(regid, DocId);
        //        }
        //        else
        //        {
        //            TempData.Clear();
        //            FormsAuthentication.SignOut();
        //            Session.RemoveAll();
        //            Session.Clear();
        //            Session.Abandon();
        //            Response.Cookies.Clear();
        //            ClearSessionAndCookies();
        //            TempData["SessionExpired"] = 1;
        //            //Response.Redirect("~/dhe/frmlogin.aspx");
        //            return Json("1", JsonRequestBehavior.AllowGet);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.VerificationController.getStudentList()");
        //    }


        //    return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult getStudentList(String CollegeId)
        {
            Int32 collegeId;// 

            DataSet dt = new DataSet();

            try
            {
                if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != ""))
                {
                    collegeId = Convert.ToInt32(Session["CollegeId"]);
                    dt = verifyObj.getStudentList(collegeId);
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController.getStudentList()");
            }


            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getPersonalData(String Reg_Id)
        {

            Session["RegId"] = Reg_Id;
            var regId = "";
            DataSet dt = new DataSet();
            try
            {
                if ((Session["RegId"] != null) && (Session["RegId"].ToString() != ""))
                {
                    regId = Session["RegId"].ToString();
                    dt = verifyObj.getPersonalData(regId);
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1; ;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController.getPersonalData()");
            }
            JsonResult result = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            result.MaxJsonLength = int.MaxValue;
            return result;
            //return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveVerificationData(String Reg_Id, String Div_Id, String Remarks, String Verification_Status, String Acceptance_Status)
        {
            //Session["RegId"] = Reg_Id;
            //Session = College_Id

            var regId = "";
            DataSet dt = new DataSet();
            try
            {
                if ((Session["RegId"] != null) && (Session["RegId"].ToString() != ""))
                {
                    regId = Session["RegId"].ToString();
                    dt = verifyObj.SaveVerificationData(Reg_Id, Div_Id, Remarks, Verification_Status, Acceptance_Status);
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. SaveVerificationData()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult getStudentObjList(String CollegeId)
        {


            var collegeId = "";// 
            DataSet dt = new DataSet();
            try
            {
                if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != ""))
                {
                    collegeId = Convert.ToString(Session["CollegeId"]); dt = verifyObj.getStudentObjList(collegeId);
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. getStudentObjList()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getStudentObjRemList(String CollegeId)
        {


            var collegeId = "";// 
            DataSet dt = new DataSet();
            try
            {
                if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != ""))
                {
                    collegeId = Convert.ToString(Session["CollegeId"]); dt = verifyObj.getStudentObjRemList(collegeId);
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. getStudentObjRemList()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getStudentVerifiedList(String CollegeId)
        {


            var collegeId = "";// 
            DataSet dt = new DataSet();
            try
            {
                if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != ""))
                {
                    collegeId = Convert.ToString(Session["CollegeId"]); dt = verifyObj.getStudentVerifiedList(collegeId);
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. getStudentVerifiedList()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getCourseDetails(String Reg_Id)
        {



            DataSet dt = new DataSet();
            try
            {
                if ((Session["RegId"] != null) && (Session["RegId"].ToString() != ""))
                {
                    Reg_Id = Convert.ToString(Session["RegId"]); dt = verifyObj.getCourseDetails(Reg_Id);
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. getCourseDetails()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveFinalVerificationData(String Reg_Id, String Verification_Status, String MobileNo, String EmailId)
        {
            //Session["RegId"] = Reg_Id;
            //Session = College_Id

            var regId = "";
            DataSet dt = new DataSet();
            try
            {
                if ((Session["RegId"] != null) && (Session["RegId"].ToString() != ""))
                {
                    regId = Session["RegId"].ToString();
                    var ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    dt = verifyObj.SaveFinalVerificationData(regId, Verification_Status, ip ?? "");//Used value in session not one that was sent from the API
                    if (dt.Tables.Count > 0 && Verification_Status == "O")
                    {
                        // MobileNo = "7307039878";
                        //EmailId = "er.abhishektomar@gmail.com";
                        //SMS.SendSMS(MobileNo, "Dear candidate," + Environment.NewLine + "Objection(s) raised against your Registration Id " + Reg_Id + ". Kindly login on dheadmissions.nic.in to view/remove objection(s).");

                        AgriSMS.sendUnicodeSMS(MobileNo, "Dear Candidate, Objection(s) raised against your Registration Id " + Reg_Id + ". Kindly login on itiharyanaadmissions.nic.in to view/remove objection(s). Regards, SDIT Haryana", "1007782704764858056");

                        SMS.SendEmail(EmailId, "Objection raised on application with Registration ID : " +
                          Reg_Id, "<p>Dear candidate,</p><p> Objection(s) raised against your <b>Registration Id : " + Reg_Id + "</b>. Kindly, login on <a href=\"https://itiharyanaadmissions.nic.in\"> dheadmissions.nic.in</a> &nbsp; to view/ remove objection(s).</p>");
                    }
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. SaveFinalVerificationData()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SchoolList()
        {
            if (Session["collegeId"] != null)
                return View();
            else
            {
                //Response.Redirect("~/dhe/frmlogin.aspx");
                //return Json("1", JsonRequestBehavior.AllowGet);
                return RedirectToAction("Account", "Error");

            }
        }

        [HttpGet]
        public JsonResult GetSchoolList(String Reg_Id)
        {



            DataSet dt = new DataSet();
            try
            {
                if ((Session["RegId"] != null) && (Session["RegId"].ToString() != ""))
                {
                    Reg_Id = Convert.ToString(Session["RegId"]); dt = verifyObj.GetSchoolList();
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. GetSchoolList()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetActionHistory(String Reg_Id)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Session["CollegeId"] != null && Session["CollegeId"].ToString() != "")
                {
                    //Reg_Id = Convert.ToString(Session["RegId"]); 
                    if (Reg_Id != null && Reg_Id != "")
                    {
                        dt = verifyObj.GetActionHistory(Reg_Id);
                    }

                    else
                    {
                        TempData.Clear();
                        FormsAuthentication.SignOut();
                        Session.RemoveAll();
                        Session.Clear();
                        Session.Abandon();
                        Response.Cookies.Clear();
                        ClearSessionAndCookies();
                        TempData["SessionExpired"] = 1;
                        //Response.Redirect("~/dhe/frmlogin.aspx");
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController.GetActionHistory()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        public ActionResult StateVerifier()
        {
            string UserType = "0";// for debugging
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);
            if (Session["collegeId"] != null)
            {
                ////Security Check
                eSessionMgmt.SetCookie();
                //Security Check

                return View();
            }
            else
            {
                //Response.Redirect("~/dhe/frmlogin.aspx");
                //return Json(new { });
                return RedirectToAction("Account", "Error");
            }
        }

        public ActionResult VerifyStudent(string txtRollNo)
        {


            string UserType = "2";// for debugging
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);

            DataTable dt = new DataTable();
            //if (txtRollNo == "" || txtRollNo == null)
            //{
            //    ViewBag.IsVerifyStatus = "-2";
            //    return View();
            //}
            var collegeid = "";
            if (Session["CollegeId"] != null && (Session["CollegeId"].ToString() != ""))
            {
                ////Security Check
                eSessionMgmt.SetCookie();
                //Security Check
                collegeid = Session["CollegeId"].ToString();
                if (txtRollNo != "" && collegeid != "")
                    dt = verifyObj.CheckVerifyOrNot(txtRollNo, collegeid);
                else
                {
                    ViewBag.IsVerifyStatus = "-2";
                    return View();
                }


                if (dt.Rows.Count > 0)
                {
                    ViewBag.IsVerifyStatus = dt.Rows[0]["Isverified"].ToString();
                    return View("VerifyStudent");
                }
                var t = txtRollNo;
                if (txtRollNo != null && collegeid != "")
                {
                    return RedirectToAction("../Verification/CandiDet", new { RegId = txtRollNo }); ;
                }
            }
            //else
            //{
            //    Response.Redirect("/dhe/frmlogin.aspx");
            //}
            return View();
        }

        [HttpGet]
        public JsonResult getDeclarationData(string Regid)
        {
            //int collegeType = Convert.ToInt32(Session["CollegeType"]);
            //bool shouldHideDiv = collegeType != 3;
            //ViewBag.ShouldHideDiv = shouldHideDiv;

            //var decrypptedRegId = HttpUtility.UrlDecode(dec.DecryptKey(Regid, "dheticketabhi@mohi#2020"));
            if (Session["UserID"] != null && (Session["UserID"].ToString() != ""))
            {
                DataSet dt = EducationContext.getDeclarationDataRAJ(Regid, Convert.ToString(Session["UserID"]));
                return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.StatusCode = 500;
                return Json(new { ErrorMessage = "Session Expired", StatusCode = 500 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult GetEduData(string Regid)
        {

            //var decrypptedRegId = HttpUtility.UrlDecode(dec.DecryptKey(Regid, "dheticketabhi@mohi#2020"));
            DataTable dt = EducationContext.getData(Regid);
            //var string = IJsonConverter(dt,);

            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CandiDet(string RegId)
        {
            string NewTab = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];  /// due to last date
            if (NewTab == "" | NewTab == null)
            {
                Response.Redirect("/dhe/frmlogin.aspx");
            }
            /*string NewTab = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

            if ((NewTab == "" || NewTab == null))
                return RedirectToAction("LogOut", "Account", new { area = "" });
            else
            {

                string regId = "";
                if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
                {
                    regId = Session["RegId"].ToString();
                }
                objuserMaxCurrentPage = objInfo.GetMax_Current_page(regId);
                objDetail.Current_page = objuserMaxCurrentPage.current_page;
                objDetail.Max_page = objuserMaxCurrentPage.max_page;
                objDetail.Verificationstatus = objuserMaxCurrentPage.Verificationstatus;
                objDetail.HasUnlocked = objuserMaxCurrentPage.HasUnlocked;
                Session["Verificationstatus"] = objDetail.Verificationstatus == null ? "" : objDetail.Verificationstatus;
                Session["HasUnlocked"] = objDetail.HasUnlocked == null ? "" : objDetail.HasUnlocked;
                */
            //var encryptedRegId = HttpUtility.UrlEncode(dec.EncryptKey(RegId, "dheticketabhi@mohi#2020"));
            // ViewBag.Regid = encryptedRegId;
            ViewBag.URegId = RegId;
            return View();
            //}

        }

        public ActionResult v_GetDocument(string DocId, string regid) //This Requires the CollegeID to be present in Session
        {
            // Int32 collegeId;
            // String RegId;

            DataTable dt = new DataTable();

            try
            {
                if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != ""))
                {
                    //collegeId = Convert.ToInt32(Session["CollegeId"]);
                    //RegId = Session["RegId"].ToString();
                    //dt = UGverifyObj.v_GetDocs(RegId, DocId);
                    dt = UGverifyObj.v_GetDocs(regid, DocId);
                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController.getStudentList()");
            }


            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public ActionResult v_getVerificationQuestions(string RegId) //This Requires the CollegeID to be present in Session
        {
            // Int32 collegeId;
            // String RegId;

            DataTable dt = new DataTable();

            try
            {
                if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != ""))
                {
                    //collegeId = Convert.ToInt32(Session["CollegeId"]);
                    //RegId = Session["RegId"].ToString();
                    //dt = UGverifyObj.v_GetDocs(RegId, DocId);
                    dt = UGverifyObj.v_getVerificationQuestions(RegId);

                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController.getStudentList()");
            }


            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public string GetIPAddress()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }

        [HttpPost]
        public ActionResult v_submitVerification(String v_regid
            , String v_status1, String v_status2, String v_status3, String v_status4, String v_status5, String v_status6, String v_status7, String v_status8
            , String remarks1, String remarks2, String remarks3, String remarks4, String remarks5, String remarks6, String remarks7, String remarks8
            , String finalVerification, String OfferSeat, String EligibleForPMSBenefits)
        {

            try
            {
                VerificationModel vm1 = new VerificationModel();

                vm1.reg_id = v_regid;
                vm1.personal_verified = v_status1;
                vm1.education_verified = v_status2;
                vm1.reservation_verified = v_status3;
                vm1.caste_verified = v_status4;
                vm1.domicile_verified = v_status5;
                vm1.widow_orphan_verified = v_status6;
                vm1.panchyat_verified = v_status7;
                vm1.higher_edu_grad12_verified = v_status8;
                vm1.personal_remarks = remarks1;
                vm1.education_remarks = remarks2;
                vm1.reservation_remarks = remarks3;
                vm1.caste_remarks = remarks4;
                vm1.domicile_remarks = remarks5;
                vm1.widow_orphan_remarks = remarks6;
                vm1.panchyat_remarks = remarks7;
                vm1.higher_edu_grad12_remarks = remarks8;
                vm1.final_verification = finalVerification;
                vm1.verifiedby = Session["Userid"].ToString();
                vm1.ChangeUser = Session["Userid"].ToString();
                vm1.OfferSeat = OfferSeat.ToString();
                //In case of EligibleForPMSBenefits validation doesn't apply, this flag will come blank so 
                //by default it will go 'No' from here.
                vm1.EligibleForPMSBenefits = EligibleForPMSBenefits.Trim().Length == 0 ? "No" : EligibleForPMSBenefits;
                if (Session["CollegeId"] != null)
                {
                    vm1.collegeid = Session["CollegeId"].ToString();
                }
                else
                {
                    return Json(JsonConvert.SerializeObject("1"), JsonRequestBehavior.AllowGet);
                }

                vm1.userid = Session["Userid"].ToString();

                vm1.ipaddress = GetIPAddress();

                if (
                  vm1.reg_id == ""
              || vm1.personal_verified == ""
              || vm1.education_verified == ""
              || vm1.reservation_verified == ""
              || vm1.caste_verified == ""
              || vm1.domicile_verified == ""
              || vm1.widow_orphan_verified == ""
              || vm1.panchyat_verified == ""
              || vm1.higher_edu_grad12_verified == ""
              || vm1.final_verification == ""
              || vm1.verifiedby == ""
              || vm1.ChangeUser == ""
              || vm1.userid == ""
              || vm1.ipaddress == ""
                    )
                {
                    ViewBag.Returnstatus = "R";
                    return View("CandiDet");
                    //return Json(JsonConvert.SerializeObject("-1"), JsonRequestBehavior.AllowGet);
                }
                else if (
                             (vm1.personal_verified == "R" && vm1.personal_remarks == "")
                            || (vm1.education_verified == "R" && vm1.education_remarks == "")
                            || (vm1.reservation_verified == "R" && vm1.reservation_remarks == "")
                            || (vm1.caste_verified == "R" && vm1.caste_remarks == "")
                            || (vm1.domicile_verified == "R" && vm1.domicile_remarks == "")
                            || (vm1.widow_orphan_verified == "R" && vm1.widow_orphan_remarks == "")
                            || (vm1.panchyat_verified == "R" && vm1.panchyat_remarks == "")
                            || (vm1.higher_edu_grad12_verified == "R" && vm1.higher_edu_grad12_remarks == "")
                         )
                {
                    return Json(JsonConvert.SerializeObject("-2"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    DataTable dt = new DataTable();
                    dt=UGverifyObj.v_submitVerification(vm1);
                    return Json(JsonConvert.SerializeObject(dt.Rows[0]["Result"].ToString()), JsonRequestBehavior.AllowGet);
                }
                //return Json(JsonConvert.SerializeObject("0"), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController.getStudentList()");
                return Json(JsonConvert.SerializeObject("0"), JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult v_showCandidateseats(string RegId)
        {
            // Int32 collegeId;
            // String RegId;

            DataTable dt = new DataTable();

            try
            {
                if ((Session["CollegeId"] != null) && (Session["CollegeId"].ToString() != ""))
                {
                    //collegeId = Convert.ToInt32(Session["CollegeId"]);
                    //RegId = Session["RegId"].ToString();
                    //dt = UGverifyObj.v_GetDocs(RegId, DocId);
                    dt = UGverifyObj.v_showCandidateseats(RegId, Session["CollegeId"].ToString());

                }
                else
                {
                    TempData.Clear();
                    FormsAuthentication.SignOut();
                    Session.RemoveAll();
                    Session.Clear();
                    Session.Abandon();
                    Response.Cookies.Clear();
                    ClearSessionAndCookies();
                    TempData["SessionExpired"] = 1;
                    //Response.Redirect("~/dhe/frmlogin.aspx");
                    return Json("1", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController.getStudentList()");
            }


            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
    }
}