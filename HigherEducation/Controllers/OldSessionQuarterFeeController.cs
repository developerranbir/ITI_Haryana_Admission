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
using HigherEducation.BusinessLayer;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using CCA.Util;
using System.Web.Configuration;
using System.Reflection;
using System.Configuration;
using PayUIntegration;
using HigherEducation.BAL;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System.Web.Helpers;
using System.Web.UI.WebControls;
using System.Xml.Linq;
//using System.Web.UI.WebControls;
//using PayUIntegration;

namespace HigherEducation.Controllers
{
    public class OldSessionQuarterFeeController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        // GET: Account
        LoginTrackModels objLTM = new LoginTrackModels();
        EducationDbContext EducationContext = new EducationDbContext();
        RegistrationFeeDB RegistrationFeeDBContext = new RegistrationFeeDB();
        CandidateDetail objDetail = new CandidateDetail();
        RegistrationViewModel objDetailRegister = new RegistrationViewModel();
        GetDataInfo objInfo = new GetDataInfo();
        LoginUserDetails objLUD = new LoginUserDetails();
        CandidateAuditTrail objAudit = new CandidateAuditTrail();
        UserMaxCurrentPage objuserMaxCurrentPage = new UserMaxCurrentPage();
        EncryptionDecryption dec = new EncryptionDecryption();//ADD
        //clsPaymentGateway objPayment = new clsPaymentGateway();
        clsPayU objpay = new clsPayU();
        VerificationDbContext UGverifyObj = new VerificationDbContext();
        private int rno = 0;
        //Logger logger = LogManager.GetCurrentClassLogger();
        //EducationDbContext EducationContext = new EducationDbContext();
        PaymentResponse objPayment = new PaymentResponse();
        RegistrationFeeDB ObjregistrationFeeDB = new RegistrationFeeDB();


        //CCACrypto ccaCrypto = new CCACrypto();
        static string Counselling = ConfigurationManager.AppSettings["Counselling"];

        private static Random random = new Random();

        List<SelectListItem> lstBoard = new List<SelectListItem>();
        List<SelectListItem> Classes = new List<SelectListItem>();
        List<SelectListItem> lstYears = new List<SelectListItem>();
        List<SelectListItem> lstCategory = new List<SelectListItem>();
        List<SelectListItem> lstCasteCategory = new List<SelectListItem>();
        List<SelectListItem> lstState = new List<SelectListItem>();
        List<SelectListItem> lstDistrict = new List<SelectListItem>();
        List<SelectListItem> lstBloodGroup = new List<SelectListItem>();
        List<SelectListItem> lstReligion = new List<SelectListItem>();
        List<SelectListItem> lstParentalIncome = new List<SelectListItem>();
        List<SelectListItem> lstAllSubject = new List<SelectListItem>();
        List<SelectListItem> lstCourseType = new List<SelectListItem>();
        List<SelectListItem> lstSex = new List<SelectListItem>();
        List<SelectListItem> lstMinority = new List<SelectListItem>();
        List<SelectListItem> lstStream = new List<SelectListItem>();
        List<SelectListItem> lstDefaultSubject = new List<SelectListItem>();
        List<SelectListItem> lstcountry = new List<SelectListItem>();
        List<SelectListItem> lstOccupation = new List<SelectListItem>();
        List<SelectListItem> lstMarital = new List<SelectListItem>();
        List<SelectListItem> lstMemberId = new List<SelectListItem>();
        List<SelectListItem> lstcollages = new List<SelectListItem>();
        List<SelectListItem> lstdisablecat = new List<SelectListItem>();
        List<SelectListItem> lstcollagestrades = new List<SelectListItem>();
        string Fathername, motherName, gender, genderid, CandidateName, RollNumber, H_ResultStatus, C_ResultStatus, mobileno;
        string v_dob, v_aadharno, v_aadharnoMask;

        CCACrypto ccaCrypto = new CCACrypto();

        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Login()
        {
            string NewTab = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

            if ((NewTab == "" || NewTab == null))
                return RedirectToAction("Index", "Account", new { area = "" });
            Session.Add("rno", 0);
            Random randomclass = new Random();
            Session["rno"] = randomclass.Next();
            rno = Convert.ToInt32(Session["rno"]);
            Session["Attempt"] = 0;
            FeeViewModel admissionSession = new FeeViewModel();
            admissionSession = EducationContext.GetAdmissionSession();
            return View(admissionSession);
        }
        public ActionResult Login_Stop()
        {
            Session.Add("rno", 0);
            Random randomclass = new Random();
            Session["rno"] = randomclass.Next();
            rno = Convert.ToInt32(Session["rno"]);
            Session["Attempt"] = 0;

            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection formCollect)
        {
            FeeViewModel admissionSession = new FeeViewModel();
            admissionSession = EducationContext.GetAdmissionSession();
            try
            {
                if (this.IsCaptchaValid("Validate your captcha"))
                {
                    Int32 resultcount = 0;
                    string MsgText = "";
                    bool ret = true;
                    if (formCollect.Get("reg_Id") != "")
                    {
                        var r = @"^[a-zA-Z0-9\s]*$";

                        Regex regex = new Regex(r);
                        ret = regex.IsMatch(formCollect.Get("reg_Id"));
                        if (!ret)
                        {
                            resultcount += 1;
                            MsgText += "Invalid UserName" + Environment.NewLine;
                        }
                    }
                    if (formCollect.Get("password") != "")
                    {
                        var r = @"^[a-zA-Z0-9\s]*$";

                        Regex regex = new Regex(r);
                        ret = regex.IsMatch(formCollect.Get("password"));
                        if (!ret)
                        {
                            resultcount += 1;
                            MsgText += "Invalid PassWord" + Environment.NewLine;
                        }
                    }
                    if (formCollect.Get("aSession") == "")
                    {
                        resultcount += 1;
                        MsgText += "Please Select Admission Session" + Environment.NewLine;
                    }
                    if (resultcount > 0)
                    {
                        TempData["validationmsg"] = MsgText;
                        return View(admissionSession);
                    }
                    ViewBag.ErrMessage = "Validation Messgae";
                    objDetail.RegID = formCollect.Get("reg_Id");
                    Session["UserId"] = formCollect.Get("reg_Id");
                    objDetail.Password = formCollect.Get("password");
                    objDetail.aSession = formCollect.Get("aSession");
                    objDetail.rno = Convert.ToString(Session["rno"]);
                    Session["admSession"] = formCollect.Get("aSession");

                    bool IsUserExits = objInfo.ValidateUserOldSession(objDetail, Session["admSession"].ToString());

                    objLTM.BrowserName = GetWebBrowserName();
                    objLTM.IPAddress = GetIPAddress();
                    objLTM.RegID = objDetail.RegID;
                    int IsLogin = objInfo.SaveLoginAndTrack(objLTM);


                    if (Session["RegId"] == null)
                    {
                        if (IsUserExits & IsLogin > 0)
                        {
                            Session["RegId"] = objDetail.RegID;
                            clsSecurity.SetCookie();
                            return RedirectToAction("QuarterlyFee", "OldSessionQuarterFee");
                        }
                        ViewBag.Message = "Whether the Username and password is incorrect or Login is Unverified";
                        return View(admissionSession);
                        //return View();
                    }
                    else
                    {
                        ViewBag.Message = "Another User is already logged In, kindly close the current session or use another browser";
                    }


                }
                else
                {
                    ViewBag.Error = "Wrong Captcha.";
                    //return View();
                    return View(admissionSession);

                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.OldSessionQuarterFeeController.[HttpPost] Login()");
            }
            //return View();
            return View(admissionSession);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public int GenerateRandomNo()
        {
            int _min = 100000;
            int _max = 999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
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


        public ActionResult LogOut()
        {
            //RCSEntities.UserInfo.CitizenInfo.ResetValue();
            TempData.Clear();
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Clear();
            ClearSessionAndCookies();
            return RedirectToAction("Login", "OldSessionQuarterFee", new { area = "" });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOut(FormCollection fc)
        {
            //RCSEntities.UserInfo.CitizenInfo.ResetValue();
            TempData.Clear();
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Response.Cookies.Clear();
            ClearSessionAndCookies();
            return RedirectToAction("Login", "OldSessionQuarterFee", new { area = "" });
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
        #region GetBroswer&IPAddress
        private string GetWebBrowserName()
        {
            StringBuilder myStr = new StringBuilder();
            myStr.Append(Request.Browser.Browser);
            myStr.Append("-");
            myStr.Append(Request.Browser.Platform);
            myStr.Append("-");
            myStr.Append(Request.Browser.MajorVersion);
            myStr.Append("-");
            myStr.Append(Request.Browser.MinorVersion);
            myStr.Append(Request.LogonUserIdentity.User.Value);
            return myStr.ToString();
        }
        #endregion

        public FileResult GetCaptchaImage()
        {
            string text = Convert.ToString(Session["Captcha"]);// UserInfo.CitizenInfo.Captcha;

            //first, create a dummy bitmap just to get a graphics object
            System.Drawing.Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public string Encrypt(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = Encoding.ASCII.GetBytes(key);// GetFileBytes(key); //System.IO.File.ReadAllBytes(key);//GetFileBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            rijndaelCipher.Padding = PaddingMode.Zeros;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }
        public string Decrypt(string textToDecrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = Encoding.ASCII.GetBytes(key);//GetFileBytes(key); //System.IO.File.ReadAllBytes(key);//
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            rijndaelCipher.Padding = PaddingMode.Zeros;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }


        #region

        // GET: QuarterlyFee
        public ActionResult QuarterlyFee()
        {
            FeeViewModel ObjfeeModule = new FeeViewModel();
            string regId = "";
            string aSession = "";
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
                    aSession = Session["admSession"].ToString();
                }

                ObjfeeModule = EducationContext.GetCandidateForQuarterFeeOldSession(regId, aSession);

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.OldSessionQuarterFeeController.[HttpGet] QuarterlyFee(),'" + regId + "'");
            }
            clsSecurity.SetCookie();
            return View(ObjfeeModule);
        }

        public ActionResult PartialFeeView(string collegeid, string QNo)
        {
            clsSecurity.CheckSession();
            FeeModule ObjfeeModule = new FeeModule();
            string regId = "";
            string aSession = "";

            try
            {
                if (Session["RegId"] == null || Session["RegId"].ToString() == "")
                {
                    return RedirectToAction("LogOut");
                }
                else
                {
                    regId = Session["RegId"].ToString();
                    aSession = Session["admSession"].ToString();
                }
                ObjfeeModule = EducationContext.GetCandidateFeeDetailOQ(regId, collegeid, QNo, aSession);
                clsSecurity.SetCookie();
            }

            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.QuarterlyFeeContoller.[HttpGet] PartialFeeView(),'" + regId + "'");
            }
            return PartialView(ObjfeeModule);
        }

        [HttpPost]
        public ActionResult FeeRequest(FeeModule feeModule)
        {
            try
            {
                string regId = "";
                string aSession = "";
                string result;

                if ((Session["RegId"] == null || Session["RegId"].ToString() == ""))
                {
                    return RedirectToAction("LogOut");
                }
                else
                {
                    regId = Session["RegId"].ToString();
                    aSession = Session["admSession"].ToString();
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
                //ObjfeeModule1 = EducationContext.GetCandidateFeeDetail(Counselling, regId, feeModule.merchant_param3);
                ObjfeeModule1 = EducationContext.GetCandidateFeeDetailOQ(regId, feeModule.merchant_param3, feeModule.QtrNo, aSession);
                ObjfeeModule1.RegistrationId = regId;
                ObjfeeModule1.TotalFee = ObjfeeModule1.TotalFee;  //M
                ObjfeeModule1.amount = ObjfeeModule1.TotalFee;  //M
                ObjfeeModule1.PaymentTransactionId = PaymentTransactionId;   // M

                ObjfeeModule1.merchant_id = WebConfigurationManager.AppSettings["strMerchantId_ITI"];  //M
                ObjfeeModule1.order_id = PaymentTransactionId;  //M
                ObjfeeModule1.currency = "INR";  //M

                ObjfeeModule1.redirect_url = WebConfigurationManager.AppSettings["redirect_url_ITI_OQ"];  //M
                ObjfeeModule1.cancel_url = WebConfigurationManager.AppSettings["cancel_url_ITI_OQ"];   // M
                ObjfeeModule1.merchant_param1 = ObjfeeModule1.RegistrationId;
                ObjfeeModule1.language = "EN";  // M
                ObjfeeModule1.Counselling = Counselling;

                result = EducationContext.SaveFeeModuleforOldSession(ObjfeeModule1, aSession);

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
                logger.Error(ex, "Error occured in HigherEducation.OldSessionQuarterFeeContoller.[HttpPost] FeeRequest()" + feeModule.RegistrationId);
            }
            return View();
        }
        [HttpPost]
        public ActionResult FeeResponse(FeeModule feeModule)
        {
            try
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
                    //ObjregistrationFeeDB.SavePGResponse(encResponse, "", "CC Avenue Test, Reg Id: "+ objPayment.merchant_param1+", Payment Id: "+objPayment.order_id);
                    string result;
                    result = ObjregistrationFeeDB.OldSessionQuarterFeeSuccess(objPayment);
                    TempData["Paymentstatus"] = "S";
                    ViewBag.status_message = objPayment.order_status;
                    if (result == "1")
                    {
                        Session["regid"] = objPayment.merchant_param1;
                        Session["paymenttransactionId"] = objPayment.order_id;
                        //AgriSMS.sendSingleSMS(objPayment.billing_tel, "Dear Student, Receipt of your payment towards " + objPayment.merchant_param1 + " for ITI admission is confirmed. To check, please login at https://admissions.itiharyana.gov.in/ Regards, SDIT Haryana", "1007867539276723247");
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
                    result = ObjregistrationFeeDB.OldSessionQuarterFeeFailure(objPayment);
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


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.OldSessionQuarterFeeController.[HttpPost] FeeResponse()");
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
                ObjfeeModule1 = EducationContext.GetCandidatePaymentSuccesDetailOldSession(regid, paymentTransactionId);
            }
            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.OldSessionQuarterFeeContoller.[HttpGet] FeeReceipt(),'" + regid + "'");
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

        public ActionResult FeeResponse()
        {
            return View();
        }

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
                logger.Error(ex, "Error occured in HigherEducation.OldSessionQuarterFeeContoller.[HttpGet] ConfirmSeatofSCCandidate()" + item.RegistrationId);
                return Json("999", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        public ActionResult PartialCandidateView()
        {
            clsSecurity.CheckSession();
            FeeModule ObjfeeModule = new FeeModule();
            string regId = "";
            string aSession = "";

            try
            {
                if (Session["RegId"] == null || Session["RegId"].ToString() == "")
                {
                    return RedirectToAction("LogOut");
                }
                else
                {
                    regId = Session["RegId"].ToString();
                    aSession = Session["admSession"].ToString();
                }
                ObjfeeModule = EducationContext.GetCandidateDetailsOldSession(regId, aSession);
                clsSecurity.SetCookie();
            }

            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.QuarterlyFeeContoller.[HttpGet] PartialCandidateView(),'" + regId + "'");
            }
            return PartialView(ObjfeeModule);
        }

        public ActionResult changePassword()
        {
            try
            {
                clsSecurity.CheckSession();
                if (Session["RegId"] == null || Session["RegId"].ToString() == "")
                {
                    return RedirectToAction("LogOut");
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.OldSessionQuarterFeeController.[HttpGet] changePassword()");
            }
            return View();
        }


        [HttpPost]
        public JsonResult changeCandidatePassword(String DATA)
        {
            try
            {
                string oldpass = "";
                string pass = "";
                var result = "0";
                var uData = JsonConvert.DeserializeObject<Dictionary<string, string>>(DATA);
                foreach (var kv in uData)
                {
                    if (kv.Key == "oldpwd")
                        oldpass = kv.Value;

                    if (kv.Key == "pwd")
                        pass = kv.Value;
                }
                if (Session["regid"] == null || Session["regid"].ToString() == "")
                {
                    return Json(new
                    {
                        redirectUrl = Url.Action("LogOut", "SecondYearLogin"),
                        isRedirect = true
                    });
                }
                else
                {
                    MD5 md5 = MD5.Create();
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
                    byte[] hash = md5.ComputeHash(inputBytes);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        sb.Append(hash[i].ToString("x2"));
                    }

                    byte[] inputBytes1 = System.Text.Encoding.ASCII.GetBytes(oldpass);
                    byte[] hash1 = md5.ComputeHash(inputBytes1);
                    StringBuilder sboldpass = new StringBuilder();
                    for (int i = 0; i < hash1.Length; i++)
                    {
                        sboldpass.Append(hash1[i].ToString("x2"));
                    }

                    result = objInfo.updatePasswordOldSession(Session["regid"].ToString(), sb.ToString(), Session["admSession"].ToString(), sboldpass.ToString());
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.SecondYearLogin.[HttpGet] changeCandidatePassword()");
                return Json("999", JsonRequestBehavior.AllowGet);
            }
        }

    }


}