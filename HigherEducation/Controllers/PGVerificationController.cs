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
    public class PGVerificationController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        pgVerificationDBContext verifyObj = new pgVerificationDBContext();


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

        // GET: PGVerification
        public ActionResult Index()
        {

            return View();
        }

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


            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. getPgStudentObjList()");
            }
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. getPgStudentObjRemList()");
            }
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. getPgStudentVerifiedList()");
            }
            var jsonResult = Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpGet]
        public JsonResult GetActionHistory(String Reg_Id)
        {
            DataSet dt = new DataSet();
            try
            {
                if ((Session["RegId"] != null) && (Session["RegId"].ToString() != ""))
                {
                    Reg_Id = Convert.ToString(Session["RegId"]); dt = verifyObj.GetActionHistory(Reg_Id);
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController.GetActionHistory()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveVerificationData(String Reg_Id, String Div_Id, String Remarks, String Verification_Status,String Remarks2)
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
                    dt = verifyObj.SaveVerificationData(Reg_Id, Div_Id, Remarks, Verification_Status,Remarks2);
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. v_PG_SaveVerificationData()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveFinalVerificationData(String Reg_Id, String Verification_Status, String MobileNo, String EmailId,String FinalWeightageMarkedByCollege)
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
                    dt = verifyObj.SaveFinalVerificationData(regId, Verification_Status, ip ?? "", FinalWeightageMarkedByCollege);//Used value in session not one that was sent from the API
                    if (dt.Tables.Count > 0 && Verification_Status == "O")
                    {

                        //SMS.SendSMS(MobileNo, "Dear candidate," + Environment.NewLine + "Objection(s) raised against your Registration Id " + Reg_Id + ". Kindly login on dheadmissions.nic.in to view/remove objection(s).");
                        AgriSMS.sendUnicodeSMS(MobileNo, "Dear Candidate, Objection(s) raised against your Registration Id " + Reg_Id + ". Kindly login on itiharyanaadmissions.nic.in to view/remove objection(s). Regards, SDIT Haryana", "1007782704764858056");
                        
                        SMS.SendEmail(EmailId, "Objection raised on application with Registration ID : " +
                          Reg_Id, "<p>Dear candidate,</p><p> Objection(s) raised against your <b>Registration Id : " + Reg_Id + "</b>. Kindly, login on <a href=\"https://itiharyanaadmissions.nic.in/\"> dheadmissions.nic.in</a> &nbsp; to view/ remove objection(s).</p>");
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
                    Response.Redirect("~/dhe/frmlogin.aspx");
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.VerificationController. v_PG_SaveFinalVerificationData()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public ActionResult StateVerifier()
        {
            return View();
        }
    }
}