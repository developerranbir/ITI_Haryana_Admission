using System;
using System.Collections.Generic;
using System.Data;
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
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using CCA.Util;
using System.Web.Configuration;
using System.Reflection;
using System.Configuration;
using PayUIntegration;
using System.Web.UI;
using System.Net;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using HigherEducation.BAL;


namespace HigherEducation.Controllers
{
    public class AdmissionFeeController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        EducationDbContext EducationContext = new EducationDbContext();
        PaymentResponse objPayment = new PaymentResponse();
        RegistrationFeeDB ObjregistrationFeeDB = new RegistrationFeeDB();


        CCACrypto ccaCrypto = new CCACrypto();
        static string Counselling = ConfigurationManager.AppSettings["Counselling"];
        // GET: AdmissionFee
        public ActionResult AdmissionFee()
        {
            FeeViewModel ObjfeeModule = new FeeViewModel();
            string regId = "";
            try
            {
                clsSecurity.CheckSession();
                if (Session["RegId"] == null || Session["RegId"].ToString() == "")
                {
                    return RedirectToAction("LogOut");
                }
                else
                {
                    regId = Session["RegId"].ToString();
                }

                ObjfeeModule = EducationContext.GetCandidateAlloCollegeList(regId, Counselling);
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AdmissionFeeController.[HttpGet] AdmissionFee(),'" + regId + "'");
            }
            clsSecurity.SetCookie();
            return View(ObjfeeModule);
        }

        public ActionResult PartialFeeView(string collegeid)
        {
            clsSecurity.CheckSession();
            FeeModule ObjfeeModule = new FeeModule();
            string regId = "";

            try
            {
                if (Session["RegId"] == null || Session["RegId"].ToString() == "")
                {
                    return RedirectToAction("LogOut");
                }
                else
                {
                    regId = Session["RegId"].ToString();
                }
                ObjfeeModule = EducationContext.GetCandidateFeeDetail(Counselling, regId, collegeid);
                clsSecurity.SetCookie();
            }

            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AdmissionFeeContoller.[HttpGet] PartialFeeView(),'" + regId + "'");
            }
            return PartialView(ObjfeeModule);
        }

        [HttpPost]
        public ActionResult FeeRequest(FeeModule feeModule)
        {
            try
            {
                string regId = "";
                string result;

                if ((Session["RegId"] == null || Session["RegId"].ToString() == ""))
                {
                    return RedirectToAction("LogOut");
                }
                else
                {
                    regId = Session["RegId"].ToString();
                }

                feeModule.RegistrationId = regId;
                string PaymentTransactionId = "0";
                string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                string onlynumber_regId = Regex.Replace(feeModule.RegistrationId, "[^0-9]", "");
                //Generate Transaction Number
                PaymentTransactionId = onlynumber_regId + currentDateTime;
                if (PaymentTransactionId.Length > 30)
                {
                    PaymentTransactionId = PaymentTransactionId.Substring(PaymentTransactionId.Length - 30);
                }

                FeeModule ObjfeeModule1 = new FeeModule();
                ObjfeeModule1 = EducationContext.GetCandidateFeeDetail(Counselling, regId, feeModule.merchant_param3);
                ObjfeeModule1.RegistrationId = regId;
                ObjfeeModule1.TotalFee = ObjfeeModule1.TotalFee;  //M
                ObjfeeModule1.amount = ObjfeeModule1.TotalFee;  //M
                ObjfeeModule1.PaymentTransactionId = PaymentTransactionId;   // M

                ObjfeeModule1.merchant_id = WebConfigurationManager.AppSettings["strMerchantId_ITI"];  //M
                ObjfeeModule1.order_id = PaymentTransactionId;  //M
                ObjfeeModule1.currency = "INR";  //M

                ObjfeeModule1.redirect_url = WebConfigurationManager.AppSettings["redirect_url_ITI_A"];  //M
                ObjfeeModule1.cancel_url = WebConfigurationManager.AppSettings["cancel_url_ITI_A"];   // M
                ObjfeeModule1.merchant_param1 = ObjfeeModule1.RegistrationId;
                ObjfeeModule1.language = "EN";  // M
                ObjfeeModule1.Counselling = Counselling;




                result = EducationContext.SaveFeeModule(ObjfeeModule1);

                if (result == "1")
                {
                    string ccaRequest = "";
                    string strMerchantId = WebConfigurationManager.AppSettings["strMerchantId_ITI"];
                    string strEncRequest = "";

                    PropertyInfo[] properties = ObjfeeModule1.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        var propName = property.Name;
                        var propValue = property.GetValue(ObjfeeModule1, null);
                        if (propValue != null)
                        {
                            if (!propName.StartsWith("_"))
                            {
                                ccaRequest = ccaRequest + propName + "=" + HttpUtility.UrlEncode(Convert.ToString(propValue)) + "&";
                            }
                        }
                    }
                    string workingKey = WebConfigurationManager.AppSettings["workingKey_ITI"];
                    string strAccessCode = WebConfigurationManager.AppSettings["strAccessCode_ITI"];

                    strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
                    strMerchantId = WebConfigurationManager.AppSettings["strMerchantId_ITI"];
                    ViewBag.strEncRequest = strEncRequest;
                    ViewBag.strAccessCode = strAccessCode;
                    ViewBag.PaymentGateway = "H";
                    return View(ObjfeeModule1);

                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] FeeRequest()" + feeModule.RegistrationId);
            }
            return View();
        }
        [HttpPost]
        public ActionResult FeeResponse(FeeModule feeModule)
        {
            string workingKey = WebConfigurationManager.AppSettings["workingKey_ITI"];//put in the 32bit alpha numeric key in the quotes provided here
            CCACrypto ccaCrypto = new CCACrypto();
            string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
            // save encResponse
            ObjregistrationFeeDB.SavePGResponse(encResponse, "", "CC Avenue");
            NameValueCollection Params = new NameValueCollection();
            string[] segments = encResponse.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }

            for (int i = 0; i < Params.Count; i++)
            {
                // Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
                switch (Params.Keys[i])
                {
                    case "order_id":
                        objPayment.order_id = Params[i];
                        break;
                    case "tracking_id":
                        objPayment.tracking_id = Params[i];
                        break;
                    case "bank_ref_no":
                        objPayment.bank_ref_no = Params[i];
                        break;
                    case "order_status":
                        objPayment.order_status = Params[i];
                        break;
                    case "failure_message":
                        objPayment.failure_message = Params[i];
                        break;
                    case "payment_mode":
                        objPayment.payment_mode = Params[i];
                        break;
                    case "card_name":
                        objPayment.card_name = Params[i];
                        break;
                    case "status_code":
                        objPayment.status_code = Params[i];
                        break;
                    case "status_message":
                        objPayment.status_message = Params[i];
                        break;
                    case "currency":
                        objPayment.currency = Params[i];
                        break;
                    case "amount":
                        objPayment.amount = Params[i];
                        break;
                    case "billing_name":
                        objPayment.billing_name = Params[i];
                        break;
                    case "billing_address":
                        objPayment.billing_address = Params[i];
                        break;
                    case "billing_city":
                        objPayment.billing_city = Params[i];
                        break;
                    case "billing_state":
                        objPayment.billing_state = Params[i];
                        break;
                    case "billing_tel":
                        objPayment.billing_tel = Params[i];
                        break;
                    case "billing_email":
                        objPayment.billing_email = Params[i];
                        break;
                    case "merchant_param1":
                        objPayment.merchant_param1 = Params[i];
                        break;
                    case "merchant_param2":
                        objPayment.merchant_param2 = Params[i];
                        break;
                    case "merchant_param3":
                        objPayment.merchant_param3 = Params[i];
                        break;
                    case "merchant_param4":
                        objPayment.merchant_param4 = Params[i];
                        break;
                    case "merchant_param5":
                        objPayment.merchant_param5 = Params[i];
                        break;
                    case "vault":
                        objPayment.vault = Params[i];
                        break;
                    case "offer_type":
                        objPayment.offer_type = Params[i];
                        break;
                    case "offer_code":
                        objPayment.offer_code = Params[i];
                        break;
                    case "discount_value":
                        objPayment.discount_value = Params[i];
                        break;
                    case "mer_amount":
                        objPayment.mer_amount = Params[i];
                        break;
                    case "eci_value":
                        objPayment.eci_value = Params[i];
                        break;
                    case "retry":
                        objPayment.retry = Params[i];
                        break;
                    case "response_code":
                        objPayment.response_code = Params[i];
                        break;
                    case "billing_notes":
                        objPayment.billing_notes = Params[i];
                        break;
                    case "trans_date":
                        objPayment.trans_date = Params[i];
                        break;
                }
            }
            if (objPayment.order_status.ToLower() == "success")
            {
                string result;
                result = ObjregistrationFeeDB.AdmissionFeeSuccess(objPayment);
                TempData["Paymentstatus"] = "S";
                ViewBag.status_message = objPayment.order_status;
                if (result == "1")
                {
                    Session["regid"] = objPayment.merchant_param1;
                    Session["paymenttransactionId"] = objPayment.order_id;
                    AgriSMS.sendSingleSMS(objPayment.billing_tel, "Dear Student, Receipt of your payment towards " + objPayment.merchant_param1 + " for ITI admission is confirmed. To check, please login at https://admissions.itiharyana.gov.in/ Regards, SDIT Haryana", "1007867539276723247");
                    return RedirectToAction("FeeReceipt");
                }
                else
                {
                    return View();
                }
            }

            else
            {
                string result;
                result = ObjregistrationFeeDB.AdmissionFeeFailure(objPayment);
                ViewBag.status_message = objPayment.status_message;
                if (objPayment.order_status.ToLower() == "aborted")
                {
                    TempData["Paymentstatus"] = "A";
                }
                if (objPayment.order_status.ToLower() == "failure")
                {
                    TempData["Paymentstatus"] = "F";
                }
                else
                {
                    TempData["Paymentstatus"] = "O";
                }
            }

            return View();
        }

        public ActionResult FeeReceipt(string paymentTransactionId = "")
        {
            string regid;


            if (Session["regid"] == null || Session["regid"].ToString() == "")
            {
                return RedirectToAction("LogOut");
            }
            else
            {
                regid = Session["regid"].ToString();
                if (paymentTransactionId == "")
                {
                    paymentTransactionId = Session["paymenttransactionId"].ToString();
                }

            }
            FeeModule ObjfeeModule1 = new FeeModule();

            try
            {
                List<FeeModule> ObjfeeModule = new List<FeeModule>();
                ObjfeeModule1 = EducationContext.GetCandidatePaymentSuccesDetail(regid, paymentTransactionId);
            }
            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] FeeReceipt(),'" + regid + "'");
            }
            return View(ObjfeeModule1);
        }
        //public ActionResult SeatAllotmentLetterPDF()
        //{

        //    return new ViewAsPdf("RankCard")
        //    {
        //        PageOrientation = Rotativa.Options.Orientation.Portrait,
        //        PageSize = Rotativa.Options.Size.A4,
        //        MinimumFontSize = 12,
        //        Password = "123",
        //        CustomSwitches = "--footer-center \" [page] Page of [toPage] Pages\" --footer-line --footer-font-size \"9\" --footer-spacing 2 --footer-font-name \"calibri light\"",
        //        FileName = "TestViewAsPDF.pdf"
        //    };
        //}

        public ActionResult SeatAllotmentLetter(string Meritid)
        {
            string regId = "";
            if (Session["RegId"] == null || Session["RegId"].ToString() == "")
            {
                return RedirectToAction("LogOut");
            }
            else
            {
                regId = Session["RegId"].ToString();
            }
            SeatAllotmentLetter ObjfeeModule = new SeatAllotmentLetter();
            ObjfeeModule = ObjregistrationFeeDB.GetSeatAllotmentLetter(Counselling, regId, Meritid);
            return PartialView(ObjfeeModule);
        }
        public ActionResult FeeResponse()
        {
            return View();
        }

        public ActionResult RankCard()
        {
            // string regId = "ICA21345";
            string regId;
            if (Session["RegId"] == null || Session["RegId"].ToString() == "")
            {
                //return RedirectToAction("LogOut");
                return View("LogOut", "Account");
            }
            else
            {
                regId = Session["RegId"].ToString();
            }
            _ = new RankCard();
            RankCard ObjfeeModule = ObjregistrationFeeDB.RankCard(regId);
            return View(ObjfeeModule);
        }
        //public ActionResult FeeModuleCollegePartial(string registrationid)
        //{

        //    FeeModule ObjfeeModule1 = new FeeModule();
        //    string regId = "";
        //    string collegeid = "";
        //    try
        //    {
        //        if (Session["CollegeId"] == null || Session["CollegeId"].ToString() == "")
        //        {
        //            //collegeid = "23";
        //            return View("LogOut", "Account");
        //        }
        //        else
        //        {
        //            regId = registrationid;
        //            collegeid = Session["CollegeId"].ToString();
        //        }
        //        List<FeeModule> ObjfeeModule = new List<FeeModule>();
        //        ObjfeeModule1 = EducationContext.GetCandidateFeeDetail(Counselling, registrationid);
        //    }


        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.AdmissionFeeController.[HttpGet] FeeModuleCollegePartial(),'" + regId + "'");
        //    }
        //    return View(ObjfeeModule1);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult FeeModuleCollegePartial(FeeModule feeModule)
        //{
        //    try
        //    {
        //        if (Session["CollegeId"] == null || Session["CollegeId"].ToString() == "")
        //        {
        //            Response.Redirect("/dhe/frmlogin.aspx");
        //        }

        //        string result = "0";
        //        string PaymentTransactionId = "0";
        //        string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
        //        string onlynumber_regId = Regex.Replace(feeModule.RegistrationId, "[^0-9]", "");
        //        PaymentTransactionId = onlynumber_regId + currentDateTime;
        //        if (PaymentTransactionId.Length > 30)
        //        {
        //            PaymentTransactionId = PaymentTransactionId.Substring(PaymentTransactionId.Length - 30);
        //        }
        //        //Calculate Fee

        //        FeeModule ObjfeeModule1 = new FeeModule();
        //        ObjfeeModule1 = EducationContext.GetCandidateFeeDetail(Counselling, feeModule.RegistrationId);

        //        ObjfeeModule1.RegistrationId = feeModule.RegistrationId;
        //        ObjfeeModule1.TotalFee = ObjfeeModule1.TotalFee;  //M
        //        ObjfeeModule1.amount = ObjfeeModule1.TotalFee;  //M
        //        ObjfeeModule1.PaymentTransactionId = PaymentTransactionId;   // M

        //        ObjfeeModule1.order_id = PaymentTransactionId;  //M
        //        ObjfeeModule1.currency = "INR";  //M
        //        ObjfeeModule1.merchant_param1 = ObjfeeModule1.RegistrationId;
        //        ObjfeeModule1.language = "EN";  // M
        //        ObjfeeModule1.Counselling = Counselling;

        //       result = ObjregistrationFeeDB.SaveFeeModuleCollege(ObjfeeModule1);

        //        if (result == "1")
        //        {
        //            Session["regid"] = ObjfeeModule1.RegistrationId;
        //            Session["paymenttransactionId"] = PaymentTransactionId;

        //            return RedirectToAction("FeeReceipt", new { PaymentTransactionId = PaymentTransactionId });
        //        }
        //        return View(ObjfeeModule1);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] FeeModuleCollege_PG()" + feeModule.RegistrationId);
        //    }

        //    return View();
        //}

        //public ActionResult ShiftAdmissionTrade()
        //{
        //    return View();
        //}
        //public JsonResult GetAdmissionDetailForShift(string RegId)
        //{
        //    DataSet dataTable = ObjregistrationFeeDB.GetAdmissionDetailForShift(RegId);
        //    return Json(JsonConvert.SerializeObject(dataTable), JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public JsonResult ConfirmSeatofSCCandidate(SCCandidateFee item)
        {
            try
            {
                if (Session["regid"] == null || Session["regid"].ToString() == "")
                {
                    return Json(new
                    {
                        redirectUrl = Url.Action("LogOut", "Account"),
                        isRedirect = true
                    });
                }
                SCCandidateFee objdocument = new SCCandidateFee();
                string result = "";

                string PaymentTransactionId = "0";
                string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                string onlynumber_regId = Regex.Replace(item.RegistrationId, "[^0-9]", "");

                //Generate Transaction Number
                PaymentTransactionId = onlynumber_regId + currentDateTime;
                if (PaymentTransactionId.Length > 30)
                {
                    PaymentTransactionId = PaymentTransactionId.Substring(PaymentTransactionId.Length - 30);
                }

                objdocument.RegistrationId = item.RegistrationId;
                objdocument.CollegeId = item.CollegeId;

                result = ObjregistrationFeeDB.SaveFeeofSCCandidate(objdocument.RegistrationId, objdocument.CollegeId, PaymentTransactionId);
                if (result == "1")
                {
                    Session["paymenttransactionId"] = PaymentTransactionId;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] ConfirmSeatofSCCandidate()" + item.RegistrationId);
                return Json("999", JsonRequestBehavior.AllowGet);
            }
        }

    }
}