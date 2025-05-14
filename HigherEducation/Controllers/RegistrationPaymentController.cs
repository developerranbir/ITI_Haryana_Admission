using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HigherEducation.Models;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Reflection;
using CCA.Util;
using HigherEducation.DataAccess;
using System.Collections.Specialized;
using HigherEducation.BusinessLayer;

namespace HigherEducation.Controllers
{
    public class RegistrationPaymentController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        CCACrypto ccaCrypto = new CCACrypto();
        RegistrationFeeDB RegistrationFeeDB = new RegistrationFeeDB();
        PaymentResponse objPayment = new PaymentResponse();
        GetDataInfo objInfo = new GetDataInfo();
        // GET: RegistrationPayment
        public ActionResult RegistrationFee()
        {
            CandidateDetail objDetail = new CandidateDetail();
            UserMaxCurrentPage objuserMaxCurrentPage = new UserMaxCurrentPage();
            objDetail.RegID = Session["RegId"].ToString();
            objDetail = objInfo.GetAPIStatus(objDetail.RegID);    // get API status 

            objuserMaxCurrentPage = objInfo.GetMax_Current_page(objDetail.RegID);
            objDetail.Current_page = objuserMaxCurrentPage.current_page;
            objDetail.Max_page = objuserMaxCurrentPage.max_page;
            objDetail.Verificationstatus = objuserMaxCurrentPage.Verificationstatus;
            objDetail.HasUnlocked = objuserMaxCurrentPage.HasUnlocked;
            Session["IsApiData"] = objDetail.CheckAPIStatus;
            Session["MaxPage"] = objDetail.Max_page;
            Session["currentPage"] = objDetail.Current_page;
            Session["Verificationstatus"] = objDetail.Verificationstatus == null ? "" : objDetail.Verificationstatus;
            if (objuserMaxCurrentPage.FormStatus != "Y" && Session["ChangeChoice"].ToString() == "Y")
            {
                Session["Verificationstatus"] = "";
            }
            Session["HasUnlocked"] = objDetail.HasUnlocked == null ? "" : objDetail.HasUnlocked;
            TempData["Verificationstatus"] = objDetail.Verificationstatus == null ? "" : objDetail.Verificationstatus;
            //Session["ChangeChoice"] = "N";
            //Session["IsPanelty"] = "N";


            string regId;
            if ((Session["RegId"] == null || Session["RegId"].ToString() == ""))
            {
                return RedirectToAction("LogOut");
            }
            else
            {
                regId = Session["RegId"].ToString();
            }
            _ = new RegistrationPGViewModel();
            RegistrationPGViewModel ObjfeeModule = RegistrationFeeDB.GetCandidateRegFee(regId);
            return View(ObjfeeModule);
        }
        [HttpPost]
        public ActionResult RegistrationFeeRequest(RegistrationPGViewModel feeModule)
        {
            try
            {
                string regId = "";
                if ((Session["RegId"] == null || Session["RegId"].ToString() == ""))
                {
                    return RedirectToAction("LogOut");
                }
                else
                {
                    regId = Session["RegId"].ToString();
                }

                feeModule.Reg_Id = regId;
                string PaymentTransactionId = "0";
                string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                string onlynumber_regId = Regex.Replace(feeModule.Reg_Id, "[^0-9]", "");
                //Generate Transaction Number
                PaymentTransactionId = onlynumber_regId + currentDateTime;
                if (PaymentTransactionId.Length > 30)
                {
                    PaymentTransactionId = PaymentTransactionId.Substring(PaymentTransactionId.Length - 30);
                }

                RegistrationPGViewModel ObjfeeModule1 = new RegistrationPGViewModel();
                ObjfeeModule1 = RegistrationFeeDB.GetCandidateRegFee(regId);
                ObjfeeModule1.Reg_Id = regId;
                ObjfeeModule1.amount = ObjfeeModule1.amount;   //M
                ObjfeeModule1.PaymentId = PaymentTransactionId;   // M

                ObjfeeModule1.merchant_id = WebConfigurationManager.AppSettings["strMerchantId_ITI"];  //M
                ObjfeeModule1.order_id = PaymentTransactionId;  //M
                ObjfeeModule1.currency = "INR";  //M

                ObjfeeModule1.redirect_url = WebConfigurationManager.AppSettings["redirect_url_ITI"];  //M
                ObjfeeModule1.cancel_url = WebConfigurationManager.AppSettings["cancel_url_ITI"];   // M
                ObjfeeModule1.merchant_param1 = ObjfeeModule1.Reg_Id;
                ObjfeeModule1.language = "EN";  // M
                ObjfeeModule1.FeeType = "R";
                ObjfeeModule1.merchant_param3 = "R";


                var result = RegistrationFeeDB.InitiateRegFee(ObjfeeModule1);

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
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] FeeRequest()" + feeModule.Reg_Id);
            }
            return View();
        }

        [HttpPost]
        public ActionResult PenaltyFeeRequest(RegistrationPGViewModel feeModule)
        {
            try
            {
                string regId = "";
                if ((Session["RegId"] == null || Session["RegId"].ToString() == ""))
                {
                    return RedirectToAction("LogOut");
                }
                else
                {
                    regId = Session["RegId"].ToString();
                }

                feeModule.Reg_Id = regId;
                string PaymentTransactionId = "0";
                string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                string onlynumber_regId = Regex.Replace(feeModule.Reg_Id, "[^0-9]", "");
                //Generate Transaction Number
                PaymentTransactionId = onlynumber_regId + currentDateTime;
                if (PaymentTransactionId.Length > 30)
                {
                    PaymentTransactionId = PaymentTransactionId.Substring(PaymentTransactionId.Length - 30);
                }

                RegistrationPGViewModel ObjfeeModule1 = new RegistrationPGViewModel();
                ObjfeeModule1 = RegistrationFeeDB.GetCandidatePenaltyFee(regId);
                ObjfeeModule1.Reg_Id = regId;
                ObjfeeModule1.amount = ObjfeeModule1.amount;   //M
                ObjfeeModule1.PaymentId = PaymentTransactionId;   // M

                ObjfeeModule1.merchant_id = WebConfigurationManager.AppSettings["strMerchantId_ITI"];  //M
                ObjfeeModule1.order_id = PaymentTransactionId;  //M
                ObjfeeModule1.currency = "INR";  //M

                ObjfeeModule1.redirect_url = WebConfigurationManager.AppSettings["redirect_url_ITI"];  //M
                ObjfeeModule1.cancel_url = WebConfigurationManager.AppSettings["cancel_url_ITI"];   // M
                ObjfeeModule1.merchant_param1 = ObjfeeModule1.Reg_Id;
                ObjfeeModule1.language = "EN";  // M
                ObjfeeModule1.FeeType = "P";
                ObjfeeModule1.merchant_param3 = "P";

                var result = RegistrationFeeDB.InitiateRegFee(ObjfeeModule1);

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
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] FeeRequest()" + feeModule.Reg_Id);
            }
            return View();
        }

        [HttpPost]
        public ActionResult RegFeeResponse(RegistrationPGViewModel feeModule)
        {
            try
            {
                string workingKey = WebConfigurationManager.AppSettings["workingKey_ITI"];//put in the 32bit alpha numeric key in the quotes provided here
                CCACrypto ccaCrypto = new CCACrypto();
                string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
                // save encResponse
                RegistrationFeeDB.SavePGResponse(encResponse, "", "CC Avenue");
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
                    Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
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
                    result = RegistrationFeeDB.SaveRegistrationFeeSuccess(objPayment);
                    if (result == "1")
                    {
                        Session["RegId"] = objPayment.merchant_param1;
                        Session["MaxPage"] = 7;
                        if (objPayment.merchant_param3 == "R")
                        {
                            Session["IsPanelty"] = "N";
                        }
                        else if (objPayment.merchant_param3 == "P")
                        {
                            Session["IsPanelty"] = "Y";
                            Session["ChangeChoice"] = "Y";
                        }
                    }
                    TempData["Paymentstatus"] = "S";
                    ViewBag.status_message = objPayment.order_status;
                }

                else
                {
                    string result;
                    result = RegistrationFeeDB.SaveRegistrationFeeFailure(objPayment);
                    ViewBag.status_message = objPayment.status_message;
                    Session["RegId"] = objPayment.merchant_param1;
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

            }
            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] RegFeeResponse()" + feeModule.Reg_Id);
            }
            if (objPayment.merchant_param3 == "R")
            {
                return RedirectToAction("RegistrationFee");
            }
            else
            {
                return RedirectToAction("PaneltyFee");
            }
        }


        public ActionResult PaneltyFee()
        {
            CandidateDetail objDetail = new CandidateDetail();
            UserMaxCurrentPage objuserMaxCurrentPage = new UserMaxCurrentPage();
            objDetail.RegID = Session["RegId"].ToString();
            objDetail = objInfo.GetAPIStatus(objDetail.RegID);    // get API status 

            objuserMaxCurrentPage = objInfo.GetMax_Current_page(objDetail.RegID);
            objDetail.Current_page = objuserMaxCurrentPage.current_page;
            objDetail.Max_page = objuserMaxCurrentPage.max_page;
            objDetail.Verificationstatus = objuserMaxCurrentPage.Verificationstatus;
            objDetail.HasUnlocked = objuserMaxCurrentPage.HasUnlocked;

            Session["IsApiData"] = objDetail.CheckAPIStatus;

            Session["MaxPage"] = objDetail.Max_page;
            Session["currentPage"] = objDetail.Current_page;
            Session["Verificationstatus"] = objDetail.Verificationstatus == null ? "" : objDetail.Verificationstatus;
            if (objuserMaxCurrentPage.FormStatus != "Y" && Session["ChangeChoice"].ToString() == "Y")
            {
                Session["Verificationstatus"] = "";
            }
            Session["HasUnlocked"] = objDetail.HasUnlocked == null ? "" : objDetail.HasUnlocked;
            TempData["Verificationstatus"] = objDetail.Verificationstatus == null ? "" : objDetail.Verificationstatus;
            string regId;
            if ((Session["RegId"] == null || Session["RegId"].ToString() == ""))
            {
                return RedirectToAction("LogOut");
            }
            else
            {
                regId = Session["RegId"].ToString();
            }
            _ = new RegistrationPGViewModel();
            RegistrationPGViewModel ObjfeeModule = RegistrationFeeDB.GetCandidatePenaltyFee(regId);
            return View(ObjfeeModule);
        }
    }
}