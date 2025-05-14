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
//using PayUIntegration;

namespace HigherEducation.Controllers
{
    public class AccountController : Controller
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
        clsPaymentGateway objPayment = new clsPaymentGateway();
        clsPayU objpay = new clsPayU();
        VerificationDbContext UGverifyObj = new VerificationDbContext();
        private int rno = 0;

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

        public ActionResult Index()
        {
            //Session["Captcha"] = GetRandomText();
            //GetCaptchaImage();

            return View();

            // return Redirect("~/index.html");

        }
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult ChangePwdStudent()
        {
            ChangePwd changePwd = new ChangePwd();
            changePwd.Regid = Session["RegId"].ToString();
            Session.Add("rno1", 0);
            Random randomclass = new Random();
            Session["rno1"] = randomclass.Next();
            rno = Convert.ToInt32(Session["rno1"]);
            Session["Attempt"] = 0;
            return View("ChangePwdStudent", changePwd);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeCandidatePasswordStu(ChangePwd changePwd)
        {
            var result = "0";
            Int32 resultcount = 0;
            string MsgText = "";
            if (ModelState.IsValid)
            {

                Session["UserId"] = Session["RegId"].ToString();
                objDetail.Password = changePwd.Password;
                objDetail.rno = Convert.ToString(Session["rno1"]);
                objDetail.RegID = Session["RegId"].ToString();
                bool IsUserExits = objInfo.ValidateUser(objDetail);

                if (!IsUserExits)
                {
                    MsgText += "0";
                    resultcount = 1;
                }
                if (changePwd.NewPassword != changePwd.ConfirmPassword)
                {
                    MsgText += "2";
                    resultcount = 1;
                }
                if (resultcount > 0)
                {
                    TempData["ChangePwdMsg"] = MsgText;
                    return RedirectToAction("ChangePwdStudent");
                }
                else
                {
                    MD5 md5 = MD5.Create();
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(changePwd.NewPassword);
                    byte[] hash = md5.ComputeHash(inputBytes);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        sb.Append(hash[i].ToString("x2"));
                    }

                    result = objInfo.updatePasswordSecondYearStu(Session["regid"].ToString(), sb.ToString(), GetIPAddress());
                    if (result == "1")
                    {
                        TempData["ChangePwdMsg"] = "1";
                    }
                    else
                    {
                        TempData["ChangePwdMsg"] = "3";
                    }
                }

            }
            return RedirectToAction("ChangePwdStudent");

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

            return View("Login");
        }

        public ActionResult Registration()
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            string NewTab = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];  /// due to last date
            if (NewTab == "" | NewTab == null)
            {
                return RedirectToAction("Login");
            }
            lstBoard = GetBoardName();
            lstYears = GetYear();
            lstSex = GetGender();
            ViewBag.Year = GetYear();
            ViewBag.BoardCode = lstBoard;
            ViewBag.Sex = lstSex;
            ViewBag.CandidateName = TempData["CandidateName"] == null ? string.Empty : (string)TempData["CandidateName"];
            ViewBag.FatherHusbandName = (string)TempData["FatherHusbandName"];
            ViewBag.MotherName = (string)TempData["MotherName"];
            ViewBag.C_ResultStatus = (string)TempData["C_ResultStatus"];
            //ViewBag.H_ResultStatus = (string)TempData["H_ResultStatus"];
            ViewBag.TwelveRollNo = (string)TempData["TwelveRollNo"];
            ViewBag.PassingOfYearid = (string)TempData["PassingOfYear"];
            ViewBag.Value = (string)TempData["BoardCode"];
            ViewBag.Sexid = (string)TempData["Sex"];
            ViewBag.Isapi = (string)TempData["Isapi"];
            //New Code
            ViewBag.v_dob = (string)TempData["v_dob"];
            ViewBag.REGid = (string)TempData["REGid"];
            ViewBag.mobileno = (string)TempData["mobileno"];
            ViewBag.QualificationCode = (string)TempData["QualificationCode"];

            Classes = GetQualificationMaster();
            ViewBag.Classes = Classes;
            clsSecurity.SetCookie();

            return View("Registration", registrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection formCollect)
        {
            int payment_regid = 0;
            int IsPanelty = 0;
            int IsChangeChoice = 0;
            int IsRejected = 0;
            int Isdiffer = 0;
            int rankcard = 0;
            var IsChangedPWD = "";

            string Apply_for_Counselling = "0";
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
                    if (formCollect.Get("reg_Id") != "")
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
                    if (resultcount > 0)
                    {
                        TempData["validationmsg"] = MsgText;
                        return RedirectToAction("Login", "Account");
                    }
                    ViewBag.ErrMessage = "Validation Messgae";
                    objDetail.RegID = formCollect.Get("reg_Id");
                    Session["UserId"] = formCollect.Get("reg_Id");
                    objDetail.Password = formCollect.Get("password");
                    objDetail.rno = Convert.ToString(Session["rno"]);

                    bool IsUserExits = objInfo.ValidateUser(objDetail);

                    objLTM.BrowserName = GetWebBrowserName();
                    objLTM.IPAddress = GetIPAddress();
                    objLTM.RegID = objDetail.RegID;
                    int IsLogin = objInfo.SaveLoginAndTrack(objLTM);
                    //objDetail = objInfo.GetCandidateName(objDetail.RegID);    // comment on 16/07/2021 no need to get choice of courses on login 
                    objDetail = objInfo.GetAPIStatus(objDetail.RegID);    // get API status 

                    objuserMaxCurrentPage = objInfo.GetMax_Current_page(objDetail.RegID);
                    objDetail.Current_page = objuserMaxCurrentPage.current_page;
                    objDetail.Max_page = objuserMaxCurrentPage.max_page;
                    objDetail.Verificationstatus = objuserMaxCurrentPage.Verificationstatus;
                    objDetail.HasUnlocked = objuserMaxCurrentPage.HasUnlocked;
                    IsChangedPWD = objuserMaxCurrentPage.IsPassWordChange;


                    DataTable IsStudenteligible_payment = EducationContext.GetStudentMeritListStatus(objDetail.RegID);

                    if (IsStudenteligible_payment.Rows.Count > 0)
                    {
                        payment_regid = Convert.ToInt32(IsStudenteligible_payment.Rows[0]["show_payment_tab"]);
                        IsPanelty = Convert.ToInt32(IsStudenteligible_payment.Rows[0]["Panelty_lgegi"]);
                        IsChangeChoice = Convert.ToInt32(IsStudenteligible_payment.Rows[0]["chnagechoice"]);
                        IsRejected = Convert.ToInt32(IsStudenteligible_payment.Rows[0]["IsRejected"]);
                        Isdiffer = Convert.ToInt32(IsStudenteligible_payment.Rows[0]["Isdiffer"]);
                        rankcard = Convert.ToInt32(IsStudenteligible_payment.Rows[0]["rankcard"]);
                    }

                    if (Session["RegId"] == null)
                    {
                        if (IsUserExits & IsLogin > 0)
                        {

                            Session["RegId"] = objDetail.RegID;
                            Session["IsApiData"] = objDetail.CheckAPIStatus;
                            Session["QualificationCode"] = objDetail.QualificationCode;
                            Session["CandidateName"] = objDetail.CandidateName;
                            Session["MaxPage"] = objDetail.Max_page;
                            Session["currentPage"] = objDetail.Current_page;
                            Session["Verificationstatus"] = objDetail.Verificationstatus == null ? "" : objDetail.Verificationstatus;

                            Session["HasUnlocked"] = objDetail.HasUnlocked == null ? "" : objDetail.HasUnlocked;
                            TempData["Verificationstatus"] = objDetail.Verificationstatus == null ? "" : objDetail.Verificationstatus;
                            TempData["IsPanelty"] = IsPanelty;
                            TempData["Isdiffer"] = Isdiffer;
                            TempData["CandidatePayment"] = payment_regid > 0 ? "Y" : "N";
                            Session["AllowUnlock"] = payment_regid > 0 ? "Y" : "N";
                            Session["rankcard"] = rankcard;

                            if (IsChangedPWD == "N")
                            {
                                return RedirectToAction("ChangePwdStudent", "account");
                            }

                            if (payment_regid > 0 || rankcard > 0)
                            {
                                Session["CandidatePayment"] = "Y";
                            }
                            else
                            {
                                Session["CandidatePayment"] = "N";
                            }
                            if (IsPanelty > 0)
                            {
                                Session["IsPanelty"] = "Y";

                            }
                            else
                            {
                                Session["IsPanelty"] = "N";
                            }
                            if (IsChangeChoice > 0 || Isdiffer > 0 || IsRejected > 0)
                            {
                                Session["ChangeChoice"] = "Y";
                            }
                            else
                            {
                                Session["ChangeChoice"] = "N";
                            }
                            if ((Session["ChangeChoice"].ToString() == "Y") && objuserMaxCurrentPage.FormStatus != "Y")
                            {
                                Session["Verificationstatus"] = "";
                            }

                            /*
                             * Quarterly fee Session set
                             */
                            Session["Quarter"] = "Y";

                            clsSecurity.SetCookie();
                            return RedirectToAction("PersonalDetails", "account");
                        }
                        ViewBag.Message = "Username or password is incorrect";
                        //  ViewBag.Message = "You are not allowed to change in your form.";

                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Another User is already logged In, kindly close the current session or use another browser";
                    }


                }
                else
                {
                    ViewBag.Error = "Wrong Captcha.";
                    return View();

                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountController.[HttpPost] Login()");
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(FormCollection formCollect, RegistrationViewModel candidateDetail)
        {
            try
            {
                clsSecurity.CheckSessionRegistration();
                if (ModelState.IsValid)
                {
                    var board = "";
                    DateTime expirationDate = DateTime.Today; // current date
                    string lastTwoDigitsOfYear = expirationDate.ToString("yy");

                    objDetailRegister.TwelveRollNo = Session["TwelveRollNo"].ToString();
                    objDetailRegister.BoardCode = Session["BoardCode"].ToString();
                    objDetailRegister.Board_code = Session["BoardCode"].ToString();
                    objDetailRegister.PassingOfYear = (Session["PassingYear"]).ToString();
                    objDetailRegister.Sex = formCollect.Get("Sex").ToString(); ;
                    objDetailRegister.FatherHusbandName = formCollect.Get("FatherHusbandName");
                    objDetailRegister.MotherName = formCollect.Get("MotherName");
                    objDetailRegister.QualificationCode = Session["QualificationCode"].ToString();
                    objDetailRegister.CandidateName = Session["CandidateName"].ToString();
                    string strPassingYear = Convert.ToString(objDetailRegister.PassingOfYear);
                    string substrPassingYear = strPassingYear.Substring(strPassingYear.Length - 2);
                    if (objDetailRegister.BoardCode == "119")
                    {
                        objDetailRegister.BoardCode = "A";
                        board = objDetailRegister.TwelveRollNo;
                    }
                    else if (objDetailRegister.BoardCode == "68")
                    {
                        objDetailRegister.BoardCode = "B";
                        board = "000" + objDetailRegister.TwelveRollNo;
                    }
                    else
                    {
                        board = "000" + objDetailRegister.TwelveRollNo;
                        objDetailRegister.BoardCode = "O";
                    }
                    string _qcode = "";
                    if (objDetailRegister.QualificationCode == "08")
                    {
                        _qcode = "A";
                    }
                    else if (objDetailRegister.QualificationCode == "08")
                    {
                        _qcode = "B";
                    }
                    else
                    {
                        _qcode = "C";
                    }
                    // Registration Generation 
                    objDetailRegister.RegID = "I" + _qcode + Convert.ToString(objDetailRegister.BoardCode) + lastTwoDigitsOfYear + substrPassingYear.ToString() + board.ToString();
                    objDetailRegister.MobileNo = formCollect.Get("MobileNo");
                    objDetailRegister.Email = formCollect.Get("Email");
                    objDetailRegister.Dob = formCollect.Get("DOB");

                    objDetailRegister.Password = GetRandomText();
                    objDetailRegister.IPAddress = GetIPAddress();
                    objDetailRegister.BrowserName = GetWebBrowserName();

                    objDetailRegister.CheckAPIStatus = Session["CheckApi"].ToString();
                    objDetailRegister.Aadhaar = Convert.ToString(Session["v_aadharno"]);

                    Session["PWD"] = objDetailRegister.Password;
                    Session["RegSessionId"] = objDetailRegister.RegID;

                    int i = EducationContext.Register(objDetailRegister);
                    if (i > 0)
                    {
                        EducationContext.SendRegistrationMessage(objDetailRegister.MobileNo, Session["RegSessionId"].ToString(), Session["PWD"].ToString(), objDetailRegister.Email);
                        TempData["RegID"] = Session["RegSessionId"].ToString();
                        TempData["PWD"] = Session["PWD"].ToString();
                        TempData["SubmitSuccess"] = 1;
                        return Redirect("~/Account/Login");
                    }
                    else
                    {
                        TempData["ErrorLog"] = 1;
                        return RedirectToAction("Registration");
                    }
                }
                else
                {
                    lstBoard = GetBoardName();
                    lstYears = GetYear();
                    lstSex = GetGender();
                    ViewBag.Year = GetYear();
                    ViewBag.BoardCode = lstBoard;
                    ViewBag.Sex = lstSex;
                    ViewBag.CandidateName = (string)TempData["CandidateName"];
                    ViewBag.FatherHusbandName = (string)TempData["FatherHusbandName"];
                    ViewBag.MotherName = (string)TempData["MotherName"];
                    ViewBag.C_ResultStatus = (string)TempData["C_ResultStatus"];
                    //ViewBag.H_ResultStatus = (string)TempData["H_ResultStatus"];
                    ViewBag.TwelveRollNo = (string)TempData["TwelveRollNo"];
                    ViewBag.PassingOfYearid = (string)TempData["PassingOfYear"];
                    ViewBag.Value = (string)TempData["BoardCode"];
                    ViewBag.Sexid = (string)TempData["Sex"];
                    ViewBag.Isapi = (string)TempData["Isapi"];
                    //New Code
                    ViewBag.v_dob = (string)TempData["v_dob"];
                    ViewBag.REGid = (string)TempData["REGid"];

                    Classes = GetQualificationMaster();
                    ViewBag.Classes = Classes;
                    return View();
                }
            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] Registration()");
                return RedirectToAction("Registration");
            }

        }
        public ActionResult PersonalDetails()
        {
            clsSecurity.CheckSession();
            CandidateDetail objCandidateDetail = new CandidateDetail();
            try
            {

                lstcountry = GetCountryName();
                lstState = GetStateName();
                lstDistrict = GetDistrictName();
                lstCategory = GetReservationCategory();
                lstDistrict = GetDistrictName();
                lstBloodGroup = GetBloodGroup();
                lstReligion = GetReligion();
                lstParentalIncome = GetParentalIncome();
                lstSex = GetGender();
                lstOccupation = GetOccupation();
                lstMarital = GetMaritalStatus();
                //lstBoard = GetBoardName();  
                lstMinority = GetMinorityData();
                lstcollages = GetallCollages();
                lstdisablecat = GetDisabledCategory();

                ViewBag.CountryName = lstcountry;
                ViewBag.Minority = lstMinority;
                ViewBag.Year = GetYear();
                ViewBag.Sex = lstSex;
                ViewBag.State = lstState;
                ViewBag.Category = lstCategory;
                ViewBag.BloodGroup = lstBloodGroup;
                ViewBag.Religion = lstReligion;
                ViewBag.ParentalIncome = lstParentalIncome;
                ViewBag.Occupation = lstOccupation;
                ViewBag.MaritalStatus = lstMarital;
                ViewBag.DistrictMaster = lstDistrict;
                ViewBag.CollagesMaster = lstcollages;
                ViewBag.DisableCategories = lstdisablecat;


                string regId = "";
                if (Session["RegId"] != null)
                {
                    regId = Session["RegId"].ToString();
                }
                ViewBag.RegID = regId;
                //data from candidate_detail
                objCandidateDetail = EducationContext.GetCandidateDataByRegistrationIdId(regId);

                ViewBag.CandidateName = objCandidateDetail.CandidateName;
                ViewBag.FathreName = objCandidateDetail.FatherHusbandName;
                ViewBag.MotherName = objCandidateDetail.MotherName;
                ViewBag.DOB = objCandidateDetail.BirthDate;
                ViewBag.Email = objCandidateDetail.Email;
                ViewBag.Mobile = objCandidateDetail.MobileNo;
                ViewBag.CheckApi = objCandidateDetail.CheckAPIStatus;
                ViewBag.SexValue = objCandidateDetail.Sex;
                ViewBag.BoardCode = objCandidateDetail.Board_code;
                ViewBag.AdhaarNo = objCandidateDetail.AdhaarNo;

                ///data from candidate
                objCandidateDetail = EducationContext.GetCandidateDataById(regId);
                if (!string.IsNullOrEmpty(objCandidateDetail.CandidateName))
                {
                    ViewBag.CandidateName = objCandidateDetail.CandidateName;
                    ViewBag.FathreName = objCandidateDetail.FatherHusbandName;
                    ViewBag.MotherName = objCandidateDetail.MotherName;
                    ViewBag.DOB = objCandidateDetail.BirthDate;
                    ViewBag.Email = objCandidateDetail.Email;
                    ViewBag.Mobile = objCandidateDetail.MobileNo;
                    ViewBag.CheckApi = objCandidateDetail.CheckAPIStatus;
                    ViewBag.SexValue = objCandidateDetail.Sex;
                    ViewBag.BoardCode = objCandidateDetail.Board_code;
                    ViewBag.AdhaarNo = objCandidateDetail.AdhaarNo;
                    ViewBag.MaritalStatusCheck = objCandidateDetail.Marital_Status;
                    ViewBag.FHOccupationCheck = objCandidateDetail.Father_Occupation;
                    ViewBag.BloodGroupVal = objCandidateDetail.BloodGroup;
                    ViewBag.ReligionVal = objCandidateDetail.Religion;
                    ViewBag.ParentalIncomeVal = objCandidateDetail.ParentalIncome;

                    ViewBag.UrbanRuralCode = objCandidateDetail.RuralUrban;
                    ViewBag.StateValue = objCandidateDetail.State_Code;
                    ViewBag.DistrictUrbanValue = objCandidateDetail.District_Code_Urban;
                    ViewBag.DistrictRuralValue = objCandidateDetail.District_Code_Rural;
                    ViewBag.MuniciplityValue = objCandidateDetail.Municiplity;
                    ViewBag.BlockValue = objCandidateDetail.Block;
                    ViewBag.VillageValue = objCandidateDetail.CityTownVillage;

                    ViewBag.C_UrbanRuralCode = objCandidateDetail.C_RuralUrban;
                    ViewBag.C_StateValue = objCandidateDetail.C_State_Code;
                    ViewBag.C_DistrictUrbanValue = objCandidateDetail.C_District_Code_Urban;
                    ViewBag.C_DistrictRuralValue = objCandidateDetail.C_District_Code_Rural;
                    ViewBag.C_MuniciplityValue = objCandidateDetail.C_Municiplity;
                    ViewBag.C_BlockValue = objCandidateDetail.C_Block;
                    ViewBag.C_VillageValue = objCandidateDetail.C_CityTownVillage;
                    //ViewBag.BplCategoryValue = objCandidateDetail.BPLCategory;
                    ViewBag.PassportValue = objCandidateDetail.PassportNo;
                    ViewBag.LicenceValue = objCandidateDetail.DrivingLicenceNo;
                    ViewBag.IsCorr = objCandidateDetail.Is_Correspondence;

                    // ViewBag.GapYear = objCandidateDetail.Gap_Year;
                    ViewBag.StreetAddress1 = objCandidateDetail.StreetAddress1;
                    ViewBag.C_StreetAddress1 = objCandidateDetail.C_StreetAddress1;
                    ViewBag.PinCode = objCandidateDetail.Pincode;
                    ViewBag.C_PinCode = objCandidateDetail.C_Pincode;
                    ViewBag.TwelveHarana = objCandidateDetail.TwelveHarana;
                    ViewBag.HasDomicile = objCandidateDetail.HaryanaDomicile;
                    ViewBag.KashmiriMigrant = objCandidateDetail.KashmirMigrant;
                    ViewBag.AadharValue = objCandidateDetail.AadharNo;
                    ViewBag.VoterIdValue = objCandidateDetail.VoterIdCard;
                    ViewBag.MinorityValue = objCandidateDetail.Minority;
                    ViewBag.ReservationCategoryCheck = objCandidateDetail.ReservationCategory;
                    ViewBag.DisableCategory = objCandidateDetail.DisableCategory;
                    string casteCategory = Convert.ToString(objCandidateDetail.CasteCategory);
                    if (casteCategory.Length == 1)
                    {
                        casteCategory = '0' + casteCategory;
                    }
                    ViewBag.CasteCategory = casteCategory;

                    string Caste = Convert.ToString(objCandidateDetail.Caste);
                    if (Caste.Length == 1)
                    {
                        Caste = '0' + Caste;
                    }
                    ViewBag.Caste = Caste;
                    ViewBag.DOBVerifiedFrom = objCandidateDetail.DOBVerifiedFrom;
                    ViewBag.NationalyType = objCandidateDetail.NationalyType;
                    ViewBag.IsCasteVerified = objCandidateDetail.isCasteVerified;
                    ViewBag.isCasteCatgMatch_WithPPP = objCandidateDetail.isCasteCatgMatch_WithPPP;
                    ViewBag.CasteCategory_PPP = objCandidateDetail.CasteCategory_PPP;
                    ViewBag.FamilyID = objCandidateDetail.FamilyID;
                    ViewBag.MemberID = objCandidateDetail.MemberId;
                    ViewBag.SexValue = objCandidateDetail.Sex;

                    ViewBag.IsITICompleted = objCandidateDetail.IsITICompleted;
                    ViewBag.ITICompletedState = objCandidateDetail.ITICompletedState;
                    ViewBag.ITICompletedYear = objCandidateDetail.ITICompletedYear;
                    ViewBag.ITICompletedName = objCandidateDetail.ITICompletedName;
                    ViewBag.ITICompletedTrade = objCandidateDetail.ITICompletedTrade;
                    ViewBag.ITICompletedRollNo = objCandidateDetail.ITICompletedRollNo;
                }
                clsSecurity.SetCookie();
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] PersonalDetails()");
            }
            //objCandidateDetail.
            return View(objCandidateDetail);
            //return View();
        }

        [HttpPost]
        public ActionResult PersonalDetails(FormCollection formCollect, CandidateDetail candidateDetail)
        {
            try
            {
                ////server side validationa

                Int32 resultcount = 0;
                string MsgText = "";
                bool ret = true;
                //change here


                if (string.IsNullOrEmpty(formCollect.Get("ReservationCategory")) || formCollect.Get("ReservationCategory") == "0" || formCollect.Get("ReservationCategory") == "")
                {


                    resultcount += 1;
                    MsgText += "Reservation Category is required" + Environment.NewLine;
                }
                else
                {
                    if (formCollect.Get("ReservationCategory") == "4" && string.IsNullOrEmpty(formCollect.Get("DisableCategory")) || formCollect.Get("DisableCategory") == "0" || formCollect.Get("DisableCategory") == "")
                    {


                        resultcount += 1;
                        MsgText += "Disable Category is required" + Environment.NewLine;
                    }
                }
                if (string.IsNullOrEmpty(formCollect.Get("CasteCategory")) || formCollect.Get("CasteCategory") == "0" || formCollect.Get("CasteCategory") == "")
                {
                    resultcount += 1;
                    MsgText += "Caste Category is required" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(formCollect.Get("Caste")) || formCollect.Get("Caste") == "0" || formCollect.Get("Caste") == "")
                {


                    resultcount += 1;
                    MsgText += "Caste is required" + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(formCollect.Get("NationalyType")))
                {

                    resultcount += 1;
                    MsgText += "National Type is required" + Environment.NewLine;
                }
                if (formCollect.Get("NationalyType") == "I")
                {
                    if (string.IsNullOrEmpty(formCollect.Get("HaryanaDomicile")))
                    {
                        resultcount += 1;
                        MsgText += "Haryana Domicile is required" + Environment.NewLine;
                    }
                }
                if (string.IsNullOrEmpty(formCollect.Get("TwelveHarana")))
                {


                    resultcount += 1;
                    MsgText += "TwelveHarana required" + Environment.NewLine;

                }
                //if (string.IsNullOrEmpty(formCollect.Get("KashmirMigrant")))
                //{
                //    resultcount += 1;
                //    MsgText += "KashmirMigrant required" + Environment.NewLine;
                //}
                if (string.IsNullOrEmpty(formCollect.Get("Minority")))
                {
                    resultcount += 1;
                    MsgText += "IsMinority required" + Environment.NewLine;
                }
                if (formCollect.Get("minorityYes") == "Y")
                {
                    if (string.IsNullOrEmpty(formCollect.Get("MinorityData")))
                    {
                        resultcount += 1;
                        MsgText += "Minority required" + Environment.NewLine;
                    }
                }

                //if (string.IsNullOrEmpty(formCollect.Get("VoterIdCard")))
                //{

                //    resultcount += 1;
                //    MsgText += "IsVoterIdCard required" + Environment.NewLine;
                //}
                //if (formCollect.Get("VoterIdCard") == "Y")
                //{
                //    if (string.IsNullOrEmpty(formCollect.Get("VoterCardText")))
                //    {

                //        resultcount += 1;
                //        MsgText += "VoterCardText required" + Environment.NewLine;
                //    }
                //}


                //change here
                if (formCollect.Get("CandidateName") == null || formCollect.Get("CandidateName") == "" || formCollect.Get("CandidateName") == "0")
                {
                    resultcount += 1;
                }
                else//Regex  Check
                {
                    var r = @"^[a-zA-Z\.\s]*$";

                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("CandidateName"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Alphabets Allowed in Candidate Name" + Environment.NewLine;
                    }
                }


                if (formCollect.Get("FatherHusbandName") == null || formCollect.Get("FatherHusbandName") == "" || formCollect.Get("FatherHusbandName") == "0")
                {
                    resultcount += 1;
                }
                else//Regex  Check
                {
                    var r = @"^[a-zA-Z\.\s]*$";

                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("FatherHusbandName"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Alphabets Allowed in Father's Name" + Environment.NewLine;
                    }
                }
                if (formCollect.Get("MotherName") == null || formCollect.Get("MotherName") == "" || formCollect.Get("MotherName") == "0")
                {
                    resultcount += 1;
                }
                else//Regex  Check
                {
                    var r = @"^[a-zA-Z\.\/\s]*$";
                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("MotherName"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Alphabets Allowed in Mother Name" + Environment.NewLine;
                    }
                }
                if (formCollect.Get("BirthDate") == null || formCollect.Get("BirthDate") == "" || formCollect.Get("BirthDate") == "0")
                {
                    resultcount += 1;
                    MsgText += "BirthDate required" + Environment.NewLine;

                }
                if (formCollect.Get("Sex") == null || formCollect.Get("Sex") == "" || formCollect.Get("Sex") == "0")
                {
                    resultcount += 1;
                    MsgText += "Gender required" + Environment.NewLine;

                }
                if (formCollect.Get("Email") == null || formCollect.Get("Email") == "" || formCollect.Get("Email") == "0")
                {
                    resultcount += 1;
                    MsgText += "Email required" + Environment.NewLine;

                }
                else//Regex  Check
                {
                    var r = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("Email"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Invalid E-mail" + Environment.NewLine;
                    }
                }
                if (formCollect.Get("MobileNo") == null || formCollect.Get("MobileNo") == "" || formCollect.Get("MobileNo") == "0")
                {
                    resultcount += 1;
                    MsgText += "MobileNo required" + Environment.NewLine;

                }
                else//Regex  Check
                {
                    var r = @"^[0-9]{5,18}$";
                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("MobileNo"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Invalid Mobile No" + Environment.NewLine;
                    }
                }
                if (formCollect.Get("Marital_Status") == null || formCollect.Get("Marital_Status") == "" || formCollect.Get("Marital_Status") == "0")
                {
                    resultcount += 1;
                    MsgText += "Marital_Status required" + Environment.NewLine;

                }
                if (formCollect.Get("Father_Occupation") == null || formCollect.Get("Father_Occupation") == "" || formCollect.Get("Father_Occupation") == "0")
                {
                    resultcount += 1;
                    MsgText += "Father_Occupation required" + Environment.NewLine;

                }
                if (formCollect.Get("Mother_Occupation") == null || formCollect.Get("Mother_Occupation") == "" || formCollect.Get("Mother_Occupation") == "0")
                {
                    resultcount += 1;
                    MsgText += "Mother_Occupation required" + Environment.NewLine;

                }

                if (formCollect.Get("Guardian_Name") != "")
                {
                    var r = @"^[a-zA-Z\s]*$";

                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("Guardian_Name"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Alphabets Allowed in Guardian Name " + Environment.NewLine;
                    }
                }
                //if (formCollect.Get("TelephoneNo") != "")
                //{
                //    var r = @"^[0-9]*$";

                //    Regex regex = new Regex(r);
                //    ret = regex.IsMatch(formCollect.Get("TelephoneNo"));
                //    if (!ret)
                //    {
                //        resultcount += 1;
                //        MsgText += "Only Number Allowed Telephone No" + Environment.NewLine;
                //    }
                //}

                if (formCollect.Get("GuardianMobileNo") != "")//Regex  Check
                {
                    var r = @"^[0-9]{5,18}$";
                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("GuardianMobileNo"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Invalid Guardian Mobile No" + Environment.NewLine;
                    }
                }
                if (formCollect.Get("GuradianEmail") != "")//Regex  Check
                {
                    var r = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("GuradianEmail"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Invalid Guardian E-mail" + Environment.NewLine;
                    }
                }


                //if (formCollect.Get("BloodGroup") == null || formCollect.Get("BloodGroup") == "" || formCollect.Get("BloodGroup") == "0")
                //{
                //    resultcount += 1;
                //    MsgText += "BloodGroup required" + Environment.NewLine;

                //}
                if (formCollect.Get("Religion") == null || formCollect.Get("Religion") == "" || formCollect.Get("Religion") == "0")
                {
                    resultcount += 1;
                    MsgText += "Religion required" + Environment.NewLine;

                }
                if (formCollect.Get("ParentalIncome") == null || formCollect.Get("ParentalIncome") == "" || formCollect.Get("ParentalIncome") == "0")
                {
                    resultcount += 1;
                    MsgText += "ParentalIncome required" + Environment.NewLine;

                }
                if (formCollect.Get("StreetAddress1") == null || formCollect.Get("StreetAddress1") == "" || formCollect.Get("StreetAddress1") == "0")
                {
                    resultcount += 1;
                    MsgText += "Street Address 1 required" + Environment.NewLine;

                }
                else
                {
                    var r = @"^[-a-zA-Z0-9\.\,\s\/\#]*$";
                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("StreetAddress1"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Alphabet, space, comma, dot, slash(/), #,-(Hyphen) allowed in Street Address1" + Environment.NewLine;
                    }
                }
                if (formCollect.Get("StreetAddress2") != "")
                {
                    var r = @"^[-a-zA-Z0-9\.\,\s\/\#]*$";
                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("StreetAddress2"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Alphabet, space, comma, dot, slash(/), #,-(Hyphen) allowed in Street Address2" + Environment.NewLine;
                    }
                }

                if (formCollect.Get("Pincode") == null || formCollect.Get("Pincode") == "" || formCollect.Get("Pincode") == "0")
                {
                    resultcount += 1;
                    MsgText += "Pincode required" + Environment.NewLine;

                }
                else
                {
                    var r = @"^[0-9]*$";

                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("Pincode"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Number Allowed Pincode" + Environment.NewLine;
                    }
                }
                //if (formCollect.Get("Gap_Year") != "")
                //{
                //    var r = @"^[0-9]*$";

                //    Regex regex = new Regex(r);
                //    ret = regex.IsMatch(formCollect.Get("Gap_Year"));
                //    if (!ret)
                //    {
                //        resultcount += 1;
                //        MsgText += "Only Number Allowed Gap_Year" + Environment.NewLine;
                //    }
                //}
                //if (formCollect.Get("BPLCardNo") != "")
                //{
                //    var r = @"^[a-zA-Z0-9\s]*$";

                //    Regex regex = new Regex(r);
                //    ret = regex.IsMatch(formCollect.Get("BPLCardNo"));
                //    if (!ret)
                //    {
                //        resultcount += 1;
                //        MsgText += "Only Alphanumeric BPL No" + Environment.NewLine;
                //    }
                //}
                if (formCollect.Get("PassportText") != "" && formCollect.Get("PassportText") != null)
                {
                    var r = @"^[a-zA-Z0-9\s]*$";

                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("PassportText"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Alphanumeric PassportText" + Environment.NewLine;
                    }
                }
                if (formCollect.Get("DrivingLicenceText") != "" && formCollect.Get("DrivingLicenceText") != null)
                {
                    var r = @"^[a-zA-Z0-9\s]*$";

                    Regex regex = new Regex(r);
                    ret = regex.IsMatch(formCollect.Get("DrivingLicenceText"));
                    if (!ret)
                    {
                        resultcount += 1;
                        MsgText += "Only Alphanumeric Driving Licence" + Environment.NewLine;
                    }
                }
                //if (formCollect.Get("BPLCategory") == null || formCollect.Get("BPLCategory") == "" || formCollect.Get("BPLCategory") == "0")
                //{
                //    resultcount += 1;
                //}
                //if (formCollect.Get("PassportNo") == null || formCollect.Get("PassportNo") == "" || formCollect.Get("PassportNo") == "0")
                //{
                //    resultcount += 1;
                //}
                //if (formCollect.Get("DrivingLicenceNo") == null || formCollect.Get("DrivingLicenceNo") == "" || formCollect.Get("DrivingLicenceNo") == "0")
                //{
                //    resultcount += 1;
                //}
                //if (formCollect.Get("IsApplyEducationLoan") == null || formCollect.Get("IsApplyEducationLoan") == "" || formCollect.Get("IsApplyEducationLoan") == "0")
                //{
                //    resultcount += 1;
                //}
                //if (formCollect.Get("IsParticipateActivites") == null || formCollect.Get("IsParticipateActivites") == "" || formCollect.Get("IsParticipateActivites") == "0")
                //{
                //    resultcount += 1;
                //}
                //if (formCollect.Get("IsMatricScholarship") == null || formCollect.Get("IsMatricScholarship") == "" || formCollect.Get("IsMatricScholarship") == "0")
                //{
                //    resultcount += 1;
                //}
                if (formCollect.Get("StreetAddress1") == null || formCollect.Get("StreetAddress1") == "" || formCollect.Get("StreetAddress1") == "0")
                {
                    resultcount += 1;
                }
                if (formCollect.Get("Pincode") == null || formCollect.Get("Pincode") == "" || formCollect.Get("Pincode") == "0")
                {
                    resultcount += 1;
                }

                if (formCollect.Get("HaryanaDomicile") == "Y")
                {
                    if (formCollect.Get("FIDUID") == "F" && (formCollect.Get("MemberId") == null || formCollect.Get("MemberId") == "" || formCollect.Get("FamilyID") == null || formCollect.Get("FamilyID") == ""))
                    {
                        MsgText += "FamilyId/MemberId Required" + Environment.NewLine;
                        resultcount += 1;
                    }
                    if (formCollect.Get("FIDUID") == "U" && (formCollect.Get("hdMemberId") == null || formCollect.Get("hdMemberId") == "" || formCollect.Get("hdFamilyId") == null || formCollect.Get("hdFamilyId") == ""))
                    {
                        MsgText += "FamilyId/MemberId Required." + Environment.NewLine;
                        resultcount += 1;
                    }
                }
                if (formCollect.Get("IsITICompleted") == null || formCollect.Get("IsITICompleted") == "" || formCollect.Get("IsITICompleted") == "0")
                {
                    resultcount += 1;
                    MsgText += "Please Select ITI Completed Option " + Environment.NewLine;
                }
                else if (formCollect.Get("IsITICompleted") == "Yes")
                {

                    if (formCollect.Get("ITICompletedYear") == null || formCollect.Get("ITICompletedYear") == "" || formCollect.Get("ITICompletedYear") == "0")
                    {
                        resultcount += 1;
                        MsgText += "Please Select ITI Completed Year " + Environment.NewLine;
                    }
                    else if (formCollect.Get("ITICompletedState") == null || formCollect.Get("ITICompletedState") == "" || formCollect.Get("ITICompletedState") == "0")
                    {
                        resultcount += 1;
                        MsgText += "Please Select ITI Completed State" + Environment.NewLine;
                    }
                    else if (formCollect.Get("ITICompletedName") == null || formCollect.Get("ITICompletedName") == "" || formCollect.Get("ITICompletedName") == "0")
                    {
                        resultcount += 1;
                        MsgText += "Please Select ITI Completed Institue Name " + Environment.NewLine;
                    }
                    else if (formCollect.Get("ITICompletedTrade") == null || formCollect.Get("ITICompletedTrade") == "" || formCollect.Get("ITICompletedTrade") == "0")
                    {
                        resultcount += 1;
                        MsgText += "Please Select ITI Completed Trade Name " + Environment.NewLine;
                    }
                    else if (formCollect.Get("ITICompletedRollNo") == null || formCollect.Get("ITICompletedRollNo") == "" || formCollect.Get("ITICompletedRollNo") == "0")
                    {
                        resultcount += 1;
                        MsgText += "Please Select ITI Completed Registration Number " + Environment.NewLine;
                    }
                    else
                    {
                        var r = @"^[a-zA-Z\.\/\s]*$";
                        Regex regex = new Regex(r);
                        ret = regex.IsMatch(formCollect.Get("ITICompletedName"));
                        if (!ret)
                        {
                            resultcount += 1;
                            MsgText += "Only Alphabets Allowed in ITI Completed Name" + Environment.NewLine;
                        }
                        else
                        {
                            ret = regex.IsMatch(formCollect.Get("ITICompletedTrade"));
                            if (!ret)
                            {
                                resultcount += 1;
                                MsgText += "Only Alphabets Allowed in ITI Completed Trade Name" + Environment.NewLine;
                            }
                            else
                            {
                                var r1 = @"^[a-zA-Z0-9\s]*$";
                                Regex regex1 = new Regex(r1);
                                ret = regex1.IsMatch(formCollect.Get("ITICompletedRollNo"));
                                if (!ret)
                                {
                                    resultcount += 1;
                                    MsgText += "Only Alphanumeric in ITI Completed Registration Number" + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                if (resultcount > 0)
                {
                    TempData["validationmsg"] = MsgText;
                    return RedirectToAction("PersonalDetails", "Account");
                }
                //// end 


                string regId = "";
                if (Session["RegId"] != null)
                {
                    regId = Session["RegId"].ToString();
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
                    return RedirectToAction("Login", "Account");
                }

                //change here
                objDetail.NationalyType = formCollect.Get("NationalyType");
                string N_State_Code = formCollect.Get("N_State_Code").ToString();
                if (N_State_Code.ToString() == "Select" || N_State_Code.ToString() == "" || N_State_Code.ToString() == null)
                    objDetail.N_State_Code = 0;
                else
                    objDetail.N_State_Code = Convert.ToInt32(formCollect.Get("N_State_Code"));
                string N_countrycode = formCollect.Get("N_Country_Code").ToString();
                if (N_countrycode.ToString() == "Select" || N_countrycode.ToString() == "" || N_countrycode.ToString() == null)
                    objDetail.N_Country_Code = 0;
                else
                    objDetail.N_Country_Code = Convert.ToInt32(formCollect.Get("N_Country_Code"));
                objDetail.HaryanaDomicile = formCollect.Get("HaryanaDomicile");
                if (formCollect.Get("FIDUID") == "F")
                {
                    objDetail.FamilyID = formCollect.Get("FamilyID");
                }
                else
                {
                    objDetail.FamilyID = formCollect.Get("hdFamilyId");
                }
                if (formCollect.Get("FIDUID") == "F")//Family Id
                {
                    objDetail.MemberId = formCollect.Get("MemberId");
                }
                else// Aadhaar Id
                {
                    objDetail.MemberId = formCollect.Get("hdMemberId");
                }
                objDetail.ReservationCategory = Convert.ToInt32(formCollect.Get("ReservationCategory"));
                if (formCollect.Get("CasteCategory") == null || formCollect.Get("CasteCategory") == "")
                {
                    objDetail.CasteCategory = 0;
                }
                else
                {
                    objDetail.CasteCategory = Convert.ToInt32(formCollect.Get("CasteCategory"));
                }
                if (formCollect.Get("caste") == null || formCollect.Get("caste") == "")
                {
                    objDetail.Caste = 0;
                }
                else
                {
                    objDetail.Caste = Convert.ToInt32(formCollect.Get("caste"));
                }
                objDetail.TwelveHarana = formCollect.Get("TwelveHarana");
                objDetail.KashmirMigrant = formCollect.Get("KashmirMigrant");
                objDetail.Minority = formCollect.Get("Minority");
                if (formCollect.Get("MinorityData") == "" || formCollect.Get("MinorityData") == null)
                {
                    objDetail.MinorityData = 0;
                }
                else
                {
                    objDetail.MinorityData = Convert.ToInt32(formCollect.Get("MinorityData"));
                }
                objDetail.VoterIdCard = formCollect.Get("VoterIdCard");
                objDetail.VoterCardText = formCollect.Get("VoterCardText");
                objDetail.isCasteVerified = formCollect.Get("hdIsCasteVerified");
                objDetail.isResidenceVerified = formCollect.Get("hdIsHryResidentVerified");
                objDetail.isDivyangVerified = formCollect.Get("hdIsPhyHandicapVerified");
                objDetail.isIncomeVerified = formCollect.Get("hdIsIncomeVerified");
                objDetail.Name_PPP = formCollect.Get("hdNamePPP");
                objDetail.Gender_PPP = formCollect.Get("hdGenderPPP");
                objDetail.FHName_PPP = formCollect.Get("hdFHNamePPP");
                objDetail.DOB_PPP = formCollect.Get("hdDOBPPP");
                objDetail.isNameVerified = formCollect.Get("hdisNameVerified");
                objDetail.isFnameVerified = formCollect.Get("hdisFnameVerified");
                objDetail.isDOBVerified = formCollect.Get("hdisDOBVerified");
                objDetail.DOBVerifiedFrom = formCollect.Get("hdDOBVerifiedFrom");
                objDetail.CheckAPIStatus = Convert.ToString(Session["IsApiData"]);

                objDetail.IsITICompleted = formCollect.Get("IsITICompleted");
                objDetail.ITICompletedYear = formCollect.Get("ITICompletedYear");
                objDetail.ITICompletedState = formCollect.Get("ITICompletedState");
                objDetail.ITICompletedName = formCollect.Get("ITICompletedName");
                objDetail.ITICompletedTrade = formCollect.Get("ITICompletedTrade");
                objDetail.ITICompletedRollNo = formCollect.Get("ITICompletedRollNo");

                objDetail.DisableCategory = Convert.ToInt32(formCollect.Get("DisableCategory"));

                if (formCollect.Get("hdcasteCategoryPPP") == "" || formCollect.Get("hdcasteCategoryPPP") == null)
                {
                    objDetail.CasteCategory_PPP = 0;
                }
                else
                {
                    objDetail.CasteCategory_PPP = Convert.ToInt32(formCollect.Get("hdcasteCategoryPPP"));
                }

                objDetail.subCaste_name_PPP = formCollect.Get("hdsubCaste_name_PPP");
                if (formCollect.Get("hdsubCaste_code_PPP") == "" || formCollect.Get("hdsubCaste_code_PPP") == null)
                {
                    objDetail.subCaste_code_PPP = 0;
                }
                else
                {
                    objDetail.subCaste_code_PPP = Convert.ToInt32(formCollect.Get("hdsubCaste_code_PPP"));
                }
                objDetail.casteDescription_PPP = formCollect.Get("hdcasteDescription_PPP");
                //change here
                objDetail.RegID = regId;
                objDetail.CandidateName = Convert.ToString(formCollect.Get("CandidateName"));
                objDetail.FatherHusbandName = formCollect.Get("FatherHusbandName");
                objDetail.MotherName = formCollect.Get("MotherName");
                objDetail.Dob = formCollect.Get("BirthDate");
                objDetail.Sex = Convert.ToInt32(formCollect.Get("Sex"));
                objDetail.Email = formCollect.Get("Email");
                objDetail.AadharNo = formCollect.Get("AadharNo");
                objDetail.MobileNo = formCollect.Get("MobileNo");
                objDetail.Marital_Status = Convert.ToInt32(formCollect.Get("Marital_Status"));
                objDetail.Father_Occupation = Convert.ToInt32(formCollect.Get("Father_Occupation"));
                objDetail.Mother_Occupation = Convert.ToInt32(formCollect.Get("Mother_Occupation"));
                objDetail.Guardian_Name = formCollect.Get("Guardian_Name");
                objDetail.TelephoneNo = formCollect.Get("TelephoneNo");
                objDetail.GuardianMobileNo = formCollect.Get("GuardianMobileNo");
                objDetail.GuradianEmail = formCollect.Get("GuradianEmail");
                if (formCollect.Get("BloodGroup") == "" || formCollect.Get("BloodGroup") == "Select")
                    objDetail.BloodGroup = 0;
                else
                    objDetail.BloodGroup = Convert.ToInt32(formCollect.Get("BloodGroup"));
                objDetail.Religion = Convert.ToInt32(formCollect.Get("Religion"));
                objDetail.ParentalIncome = Convert.ToInt32(formCollect.Get("ParentalIncome"));
                objDetail.RuralUrban = Convert.ToString(formCollect.Get("RuralUrban"));
                objDetail.State_Code = Convert.ToInt32(formCollect.Get("State_Code"));
                if (formCollect.Get("District_Code_Rural") == "" || formCollect.Get("District_Code_Rural") == "Select")
                    objDetail.District_Code_Rural = 0;
                else
                    objDetail.District_Code_Rural = Convert.ToInt32(formCollect.Get("District_Code_Rural"));
                if (formCollect.Get("District_Code_Urban") == "" || formCollect.Get("District_Code_Urban") == "Select")
                    objDetail.District_Code_Urban = 0;
                else
                    objDetail.District_Code_Urban = Convert.ToInt32(formCollect.Get("District_Code_Urban"));


                if (formCollect.Get("Municiplity") == "" || formCollect.Get("Municiplity") == "Select")
                    objDetail.Municiplity = 0;
                else
                    objDetail.Municiplity = Convert.ToInt32(formCollect.Get("Municiplity"));
                if (formCollect.Get("Block") == "" || formCollect.Get("Block") == "Select" || formCollect.Get("Block") == null)
                    objDetail.Block = 0;
                else
                    objDetail.Block = Convert.ToInt32(formCollect.Get("Block"));
                if (formCollect.Get("CityTownVillage") == "" || formCollect.Get("CityTownVillage") == "Select" || formCollect.Get("CityTownVillage") == null)
                    objDetail.CityTownVillage = 0;
                else
                    objDetail.CityTownVillage = Convert.ToInt32(formCollect.Get("CityTownVillage"));
                objDetail.StreetAddress1 = formCollect.Get("StreetAddress1");
                objDetail.StreetAddress2 = formCollect.Get("StreetAddress2");
                objDetail.Pincode = Convert.ToString(formCollect.Get("Pincode"));
                if ((Convert.ToBoolean(formCollect.Get("Is_Correspondence").Split(',')[0])) == true)
                {
                    if (Convert.ToString(formCollect.Get("RuralUrban")) != null)
                        objDetail.Is_Correspondence = true;
                    else
                        objDetail.Is_Correspondence = false;
                    objDetail.C_RuralUrban = Convert.ToString("C_" + formCollect.Get("RuralUrban"));

                    string C_State_Code = formCollect.Get("State_Code") == null ? "Select" : formCollect.Get("State_Code").ToString();
                    if (C_State_Code.ToString() == "Select" || C_State_Code.ToString() == "" || C_State_Code.ToString() == null)
                        objDetail.C_State_Code = 0;
                    else
                        objDetail.C_State_Code = Convert.ToInt32(formCollect.Get("State_Code"));

                    string c_distrural = formCollect.Get("District_Code_Rural") == null ? "Select" : formCollect.Get("District_Code_Rural").ToString();
                    if (c_distrural.ToString() == "Select" || c_distrural.ToString() == "" || c_distrural.ToString() == null)
                        objDetail.C_District_Code_Rural = 0;
                    else
                        objDetail.C_District_Code_Rural = Convert.ToInt32(formCollect.Get("District_Code_Rural"));

                    string c_disturban = formCollect.Get("District_Code_Urban") == null ? "Select" : formCollect.Get("District_Code_Urban").ToString();
                    if (c_disturban.ToString() == "Select" || c_disturban.ToString() == "" || c_disturban.ToString() == null)
                        objDetail.C_District_Code_Urban = 0;
                    else
                        objDetail.C_District_Code_Urban = Convert.ToInt32(formCollect.Get("District_Code_Urban"));

                    string c_municiplity = formCollect.Get("Municiplity") == null ? "Select" : formCollect.Get("Municiplity").ToString();
                    if (c_municiplity.ToString() == "Select" || c_municiplity.ToString() == "" || c_municiplity.ToString() == null)
                        objDetail.C_Municiplity = 0;
                    else
                        objDetail.C_Municiplity = Convert.ToInt32(formCollect.Get("Municiplity"));

                    string c_block = formCollect.Get("Block") == null ? "Select" : formCollect.Get("Block").ToString();
                    if (c_block.ToString() == "Select" || c_block.ToString() == "" || c_block.ToString() == null)
                        objDetail.C_Block = 0;
                    else
                        objDetail.C_Block = Convert.ToInt32(formCollect.Get("Block"));
                    string C_CityTownVillage = formCollect.Get("CityTownVillage") == null ? "Select" : formCollect.Get("CityTownVillage").ToString();
                    if (C_CityTownVillage.ToString() == "Select" || C_CityTownVillage.ToString() == "" || C_CityTownVillage.ToString() == null)
                        objDetail.C_CityTownVillage = 0;
                    else
                        objDetail.C_CityTownVillage = Convert.ToInt32(formCollect.Get("CityTownVillage"));
                    objDetail.C_StreetAddress1 = formCollect.Get("StreetAddress1");
                    objDetail.C_StreetAddress2 = formCollect.Get("StreetAddress2");
                    string C_Pincode = formCollect.Get("Pincode") == null ? "Select" : formCollect.Get("Pincode").ToString();
                    if (C_Pincode.ToString() == "Select" || C_Pincode.ToString() == "" || C_Pincode.ToString() == null)
                        objDetail.C_Pincode = "";
                    else
                        objDetail.C_Pincode = Convert.ToString(formCollect.Get("Pincode") != null ? Convert.ToString(formCollect.Get("Pincode")) : "");
                }
                else
                {


                    objDetail.Is_Correspondence = false;
                    objDetail.C_RuralUrban = Convert.ToString(formCollect.Get("C_RuralUrban") != null ? Convert.ToString(formCollect.Get("C_RuralUrban")) : "");
                    objDetail.C_State_Code = Convert.ToInt32(formCollect.Get("C_State_Code"));

                    string C_District_Code_Rural = formCollect.Get("C_District_Code_Rural") == null ? "Select" : formCollect.Get("C_District_Code_Rural").ToString();
                    if (C_District_Code_Rural.ToString() == "Select" || C_District_Code_Rural.ToString() == "" || C_District_Code_Rural.ToString() == null)
                        objDetail.C_District_Code_Rural = 0;
                    else
                        objDetail.C_District_Code_Rural = Convert.ToInt32(formCollect.Get("C_District_Code_Rural"));
                    string C_District_Code_Urban = formCollect.Get("C_District_Code_Urban") == null ? "Select" : formCollect.Get("C_District_Code_Urban").ToString();
                    if (C_District_Code_Urban.ToString() == "Select" || C_District_Code_Urban.ToString() == "" || C_District_Code_Urban.ToString() == null)
                        objDetail.C_District_Code_Urban = 0;
                    else
                        objDetail.C_District_Code_Urban = Convert.ToInt32(formCollect.Get("C_District_Code_Urban") != null ? Convert.ToInt32(formCollect.Get("C_District_Code_Urban")) : 0);
                    string C_Municiplity = formCollect.Get("C_Municiplity") == null ? "Select" : formCollect.Get("C_Municiplity").ToString();
                    if (C_Municiplity.ToString() == "Select" || C_Municiplity.ToString() == "" || C_Municiplity.ToString() == null)
                        objDetail.C_Municiplity = 0;
                    else
                        objDetail.C_Municiplity = Convert.ToInt32(formCollect.Get("C_Municiplity") != null ? Convert.ToInt32(formCollect.Get("C_Municiplity")) : 0);
                    string C_Block = formCollect.Get("C_Block") == null ? "Select" : formCollect.Get("C_Block").ToString();
                    if (C_Block.ToString() == "Select" || C_Block.ToString() == "" || C_Block.ToString() == null)
                        objDetail.C_Block = 0;
                    else
                        objDetail.C_Block = Convert.ToInt32(formCollect.Get("C_Block") != null ? Convert.ToInt32(formCollect.Get("C_Block")) : 0);
                    string C_CityTownVillage = formCollect.Get("C_CityTownVillage") == null ? "Select" : formCollect.Get("C_CityTownVillage").ToString();
                    if (C_CityTownVillage.ToString() == "Select" || C_CityTownVillage.ToString() == "" || C_CityTownVillage.ToString() == null)
                        objDetail.C_Block = 0;
                    else
                        objDetail.C_CityTownVillage = Convert.ToInt32(formCollect.Get("C_CityTownVillage"));
                    objDetail.C_StreetAddress1 = formCollect.Get("C_StreetAddress1");
                    objDetail.C_StreetAddress2 = formCollect.Get("C_StreetAddress2");
                    objDetail.C_Pincode = Convert.ToString(formCollect.Get("C_Pincode") != null ? Convert.ToString(formCollect.Get("C_Pincode")) : "");
                }

               //string Hostel = formCollect.Get("Hostel") == null ? "Select" : formCollect.Get("Hostel").ToString();
               //if (Hostel.ToString() == "Select" || Hostel.ToString() == "" || Hostel.ToString() == null)
               //    objDetail.Hostel = "None";
               //else
               //    objDetail.Hostel = Convert.ToString(formCollect.Get("Hostel"));
               /* objDetail.BPLCardNo = formCollect.Get("BPLCardNo")*/;
                string ModeOfTransport = Convert.ToString(formCollect.Get("ModeOfTransport"));
                if (ModeOfTransport.ToString() == "Select" || ModeOfTransport.ToString() == "" || ModeOfTransport.ToString() == null)
                    objDetail.ModeOfTransport = "None";
                else
                    objDetail.ModeOfTransport = formCollect.Get("ModeOfTransport");
                objDetail.PassportNo = formCollect.Get("PassportNo");
                objDetail.PassportText = formCollect.Get("PassportText");
                objDetail.DrivingLicenceNo = formCollect.Get("DrivingLicenceNo");
                objDetail.DrivingLicenceText = formCollect.Get("DrivingLicenceText");
                //objDetail.BPLCategory = formCollect.Get("BPLCategory");
                objDetail.Status = 0;
                objDetail.IPAddress = GetIPAddress();
                objDetail.BrowserName = GetWebBrowserName();
                //objDetail.IsApplyEducationLoan = formCollect.Get("IsApplyEducationLoan");
                //objDetail.IsParticipateActivites = formCollect.Get("IsParticipateActivites");
                //objDetail.IsMatricScholarship = formCollect.Get("IsMatricScholarship");

                objDetail.Max_page = Convert.ToInt32(Session["MaxPage"]);
                objDetail.Current_page = Convert.ToInt32(Session["currentPage"]);

                objDetail.Json_PPP = formCollect.Get("hdJsonPPP");
                DataTable result = EducationContext.UpdateCandidateDetailTabData(objDetail);
                if (result.Rows[0]["success"].ToString() == "1")
                {
                    Session["Gender"] = Convert.ToInt32(formCollect.Get("Sex"));
                    TempData["CandidateInsertData"] = 1;
                    Session["MaxPage"] = "1";
                }
                else
                {
                    ViewBag.ErrorMessage = "Something went wrong. Please check your details";
                    return View();
                }



            }
            catch (Exception ex)
            {
                ViewBag.Message = "Please try again. Something went wrong";
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] PersonalDetails()");
                return View();
            }
            return RedirectToAction("EduQualification", "Account");
        }
        public ActionResult Weightage()
        {
            WeightageViewModel objDetailWeitage = new WeightageViewModel();
            try
            {
                clsSecurity.CheckSession();

                string regId = "";
                if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
                {
                    regId = Session["RegId"].ToString();
                }
                else
                {
                    return RedirectToAction("LogOut", "Account", new { area = "" });
                }
                lstMinority = GetPanchVillages(regId);
                ViewBag.Villages = lstMinority;
                objDetailWeitage = EducationContext.GetWeightageDetail(regId);
                clsSecurity.SetCookie();
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountController.[HttpGet] Weightage()");
            }
            return View(objDetailWeitage);
        }
        [HttpPost]
        public ActionResult Weightage(WeightageViewModel candidateDetail)
        {
            try
            {

                if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
                {
                    candidateDetail.Reg_id = Session["RegId"].ToString();
                }
                else
                {
                    return RedirectToAction("LogOut", "Account", new { area = "" });
                }
                //if (candidateDetail.IsRuralArea.ToLower() == "no")
                //{
                //    this.ModelState.Remove("HaryanaRuralAreaSchool");
                //}
                if (candidateDetail.Panch_Weightage != "Yes")
                {
                    ModelState.Remove("Panch_Weightage_Village");
                }
                if (ModelState.IsValid)
                {
                    int i = EducationContext.SaveWeightage(candidateDetail);
                    if (i > 0)
                    {
                        TempData["WeightageSave"] = 1;
                        Session["MaxPage"] = "3";
                        return RedirectToAction("FileUpload", "FileUpload");
                    }
                }
                else
                {
                    lstMinority = GetPanchVillages(candidateDetail.Reg_id);
                    ViewBag.Villages = lstMinority;
                    return View(candidateDetail);
                }

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Please try again. Something went wrong";
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] Weightage()");
                return View();
            }
            return View();
        }
        public ActionResult ChoiceofCourses()
        {
            string regId = "";
            clsSecurity.CheckSession();
            ChoiceCourse objDetail = new ChoiceCourse();

            if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }
            else
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }


            lstState = GetStateName();
            ViewBag.State = lstState;
            ViewBag.RegId = regId;
            objDetail = objInfo.GetDetailsForChoiceofCourses(regId);
            if (objDetail.Status == "1")
            {
                ViewBag.passstatus = "P";
            }
            else
            {
                ViewBag.passstatus = "F";
            }
            ViewBag.streamId = objDetail.Stream;

            ViewBag.percentage = objDetail.Percentage;

            ViewBag.physicalid = objDetail.DisableCategory;

            ViewBag.da = objDetail.DA;

            ViewBag.age = objDetail.Age;

            ViewBag.mf = objDetail.MF;


            clsSecurity.SetCookie();

            return View();
        }
        [HttpPost]
        public List<SelectListItem> GetCountryName()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetCountryName())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }

        [HttpPost]
        public List<SelectListItem> GetallCollages()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetAllCollages())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }

        [HttpGet]
        public JsonResult GetcollgecourseAll(string collageid)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetcollgecourseAll(collageid))
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        public List<SelectListItem> GetStateName()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetState())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetDistrictName()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetDistrict())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetBoardName()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetBoard())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Type)
                });
            }
            return items;
        }

        public List<SelectListItem> GetUniversityMaster()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetUniversityMaster())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Type)
                });
            }
            return items;
        }
        public List<SelectListItem> GetQualificationMaster()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetQualificationMaster())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Type)
                });
            }
            return items;
        }

        public List<SelectListItem> GetMinorityData()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetMinorityData())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }


        public List<SelectListItem> GetPanchVillages(string regId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetPanchVillages(regId))
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetGender()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetGender())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetMaritalStatus()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetMaritalStatus())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetOccupation()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetOccupation())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetDisabledCategory()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetDisabledCategory())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetReservationCategory()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetReservationCategory())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetCaste()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetCaste())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetCasteCategory()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetCasteCategory())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetYear()
        {
            List<SelectListItem> years = new List<SelectListItem>();
            int CurrentYear = DateTime.Now.Year;

            for (int i = CurrentYear; i >= 1970; i--)
            {
                years.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString(),
                    Selected = true
                });
            }

            //Default It will Select Current Year  
            return years;
        }
        public List<SelectListItem> GetBloodGroup()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetBloodGroup())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetReligion()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetReligionName())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetParentalIncome()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetParentalIncome())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        public List<SelectListItem> GetStream(string regId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetStream(regId))
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id)
                });
            }
            return items;
        }

        public JsonResult DistrictP_Bind(string state_id)
        {
            DataSet ds = EducationContext.DistrictP_Bind(state_id);
            List<SelectListItem> districtList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                districtList.Add(new SelectListItem { Text = dr["districtname"].ToString(), Value = dr["districtcode"].ToString() });
            }
            return Json(districtList, JsonRequestBehavior.AllowGet);
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

        public JsonResult ImageSave()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

#pragma warning disable
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        var filename = Path.GetFileNameWithoutExtension(file.FileName);
                        string extensionName = Path.GetExtension(fname);
                        var rn_name = RandomString(5).ToLower();
                        int rn_number = GenerateRandomNo();
                        filename = rn_name + rn_number;
                        var fullFile = filename + extensionName;
                        //var dbsavePath = "/UploadedImages/" + fullFile;
                        //var imagepath = "~/UploadedImages/" + fullFile;
                        //fullFile = Path.Combine(Server.MapPath("~/UploadedImages/"), fullFile);
                        //file.SaveAs(fullFile);
                        //int filesize = file.ContentLength;
                        if (extensionName.ToLower() == ".jpg" || extensionName.ToLower() == ".png" || extensionName.ToLower() == ".jpeg")
                        {
                            Stream stream = file.InputStream;
                            BinaryReader binaryreader = new BinaryReader(stream);
                            byte[] bytes = binaryreader.ReadBytes((int)stream.Length);
                            string base64String = Convert.ToBase64String(bytes);
                            Session["Image"] = bytes;
                            // Session["FilePath"] = fullFile;
                            return Json(base64String, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("File type is not supported.");
                        }
                    }
#pragma warning restore
                    return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        public JsonResult SignatureSave()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

#pragma warning disable
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        var filename = Path.GetFileNameWithoutExtension(file.FileName);
                        string extensionName = Path.GetExtension(fname);
                        var rn_name = RandomString(5).ToLower();
                        int rn_number = GenerateRandomNo();
                        filename = rn_name + rn_number;
                        var fullFile = filename + extensionName;
                        //var dbsavePath = "/UploadedImages/" + fullFile;
                        //var imagepath = "~/UploadedImages/" + fullFile;
                        //fullFile = Path.Combine(Server.MapPath("~/UploadedImages/"), fullFile);
                        //file.SaveAs(fullFile);
                        //int filesize = file.ContentLength;
                        if (extensionName.ToLower() == ".jpg" || extensionName.ToLower() == ".png" || extensionName.ToLower() == ".jpeg")
                        {
                            Stream stream = file.InputStream;
                            BinaryReader binaryreader = new BinaryReader(stream);
                            byte[] bytes = binaryreader.ReadBytes((int)stream.Length);
                            string base64String = Convert.ToBase64String(bytes);
                            Session["Signature"] = bytes;
                            //Session["SignaturePath"] = fullFile;
                            return Json(base64String, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("File type is not supported.");
                        }
                    }
#pragma warning restore
                    return Json(new EmptyResult(), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
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

        public JsonResult CasteCategory_Bind(string reservationCategory_id)
        {
            DataSet ds = EducationContext.CasteCategory_Bind("0" + reservationCategory_id);
            List<SelectListItem> casteCategoryList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                casteCategoryList.Add(new SelectListItem { Text = dr["categorydesc"].ToString(), Value = dr["categorycode"].ToString() });
            }
            return Json(casteCategoryList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CasteCategory_Bind_PPP(string CasteCategoryId)
        {
            DataSet ds = EducationContext.CasteCategory_Bind_PPP("0" + CasteCategoryId);
            //List<Object> bindCaseReservationthroughPPP = new List<Object>();
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //     bindCaseReservationthroughPPP.Add(new Object
            //    {   
            ////         ValueReservation = dr["resevationcode"].ToString(),
            //        TextReservation = dr["reservationname"].ToString(),
            //        ValueCategory = dr["categorycode"].ToString(),
            //        TextCategory = dr["categorydesc"].ToString(),
            //    });
            // }
            //return Json(ds.Tables[0], JsonRequestBehavior.AllowGet);
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(ds.Tables[0]), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Caste_Bind(string casteCategory_Id)
        {
            DataSet ds = EducationContext.Caste_Bind(casteCategory_Id);
            List<SelectListItem> casteList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                casteList.Add(new SelectListItem { Text = dr["castedesc"].ToString(), Value = dr["castecode"].ToString() });
            }
            return Json(casteList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Block__Bind(string district_id)
        {
            DataSet ds = EducationContext.GetBlockName(district_id);
            List<SelectListItem> blocklist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                blocklist.Add(new SelectListItem { Text = dr["blockname"].ToString(), Value = dr["blockcode"].ToString() });
            }
            return Json(blocklist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Municiplity_Bind(string district_id)
        {
            DataSet ds = EducationContext.GetMuniciplity(district_id);
            List<SelectListItem> municiplitylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                municiplitylist.Add(new SelectListItem { Text = dr["villagename"].ToString(), Value = dr["villagecode"].ToString() });
            }
            return Json(municiplitylist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VillageCity_Bind(string state_id, string district_id, string block_id)
        {
            DataSet ds = EducationContext.GetVillageCity(state_id, district_id, block_id);
            List<SelectListItem> citylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                citylist.Add(new SelectListItem { Text = dr["villagename"].ToString(), Value = dr["villagecode"].ToString() });
            }
            return Json(citylist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubDistrict_Bind(string district_id)
        {
            DataSet ds = EducationContext.SubDistrict_Bind(district_id);
            List<SelectListItem> subDistrict = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                subDistrict.Add(new SelectListItem { Text = dr["sub_district_name"].ToString(), Value = dr["sub_district_id"].ToString() });
            }
            return Json(subDistrict, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Tehsil_Bind(string Sub_District_Code)
        {
            DataSet ds = EducationContext.Tehsil_Bind(Sub_District_Code);
            List<SelectListItem> tehsilList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tehsilList.Add(new SelectListItem { Text = dr["vtc_name"].ToString(), Value = dr["vtc_id"].ToString() });
            }
            return Json(tehsilList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CityTownVillage_Bind(string Tehsil_Code)
        {
            DataSet ds = EducationContext.CityTownVillage_Bind(Tehsil_Code);
            List<SelectListItem> tehsilList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tehsilList.Add(new SelectListItem { Text = dr["VIL_NAME"].ToString(), Value = dr["VIL_CODE"].ToString() });
            }
            return Json(tehsilList, JsonRequestBehavior.AllowGet);
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
            return RedirectToAction("Login", "Account", new { area = "" });
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
            return RedirectToAction("Login", "Account", new { area = "" });
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
        public List<SelectListItem> GetCourseType()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetCourseType())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        [HttpPost]
        public ActionResult SaveDocument(IEnumerable<HttpPostedFileBase> files, FormCollection formcollect)
        {
            Document objBL = new Document();
            int formId = 1;
            foreach (var file in files)
            {
                if (file != null)
                {
                    AttachmentType aa = new AttachmentType();
                    Stream str = file.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                    string base64ImageRepresentation = Convert.ToBase64String(FileDet);
                    objBL.Docs = base64ImageRepresentation;
                    decimal size = Math.Round(((decimal)file.ContentLength / (decimal)1024), 2);
                    if (size < 10000)
                    {
                        objBL.DocsName = Path.GetFileNameWithoutExtension(file.FileName);
                        objBL.FormId = formId;
                        objAudit.SrNo = Convert.ToString(GenerateRandomNo());
                        int i = EducationContext.SaveDocument(objBL);
                        formId++;
                    }
                }
            }

            return RedirectToAction("CandidateDetails", "Account");
        }

        public JsonResult District_Bind()
        {
            DataSet ds = EducationContext.District_Bind();
            List<SelectListItem> tehsilList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                tehsilList.Add(new SelectListItem { Text = dr["districtname"].ToString(), Value = dr["districtcode"].ToString() });
            }
            return Json(tehsilList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveCourseSection(CourseModel CourseModel)
        {
            ChoiceofCourseViewModel choiceofCourseViewModel = new ChoiceofCourseViewModel();
            string regId = "";
            if ((Session["RegId"] != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
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
                return Json("Login", "Account");
            }

            string MsgText = "";
            Int32 resultcount = 0;

            string Course_District = CourseModel.Course_District;
            string Course_College = CourseModel.Course_College;
            string Course_CollegeCourse = CourseModel.Course_CollegeCourse;
            string Course_CourseSection = CourseModel.Course_CourseSection;
            string Course_SubjectCombination = CourseModel.Course_SubjectCombination;

            if (Course_District == "" || Course_District == null || Course_District == "0")
            {
                resultcount += 1;
                MsgText += "Please Select District." + Environment.NewLine;
            }
            if (Course_College == "" || Course_College == null || Course_College == "0")
            {
                resultcount += 1;
                MsgText += "Please Select College." + Environment.NewLine;
            }
            if (Course_CollegeCourse == "" || Course_CollegeCourse == null || Course_CollegeCourse == "0")
            {
                resultcount += 1;
                MsgText += "Please Select Course." + Environment.NewLine;
            }
            if (Course_CourseSection == "" || Course_CourseSection == null || Course_CourseSection == "0")
            {
                resultcount += 1;
                MsgText += "Please Select Section." + Environment.NewLine;
            }
            //if (Course_SubjectCombination == "" || Course_SubjectCombination == null || Course_SubjectCombination == "0")
            //{
            //    resultcount += 1;
            //    MsgText += "Please Select Combination." + Environment.NewLine;
            //}
            if (resultcount > 0)
            {
                return Json("9", JsonRequestBehavior.AllowGet);
            }
            choiceofCourseViewModel.RegID = regId;
            choiceofCourseViewModel.Course_State = "6";
            choiceofCourseViewModel.Course_District = CourseModel.Course_District;
            choiceofCourseViewModel.Course_College = CourseModel.Course_College;
            choiceofCourseViewModel.Course_Course = CourseModel.Course_CollegeCourse;
            choiceofCourseViewModel.Course_CourseSection = CourseModel.Course_CourseSection;
            choiceofCourseViewModel.Course_SubjectCombination = CourseModel.Course_SubjectCombination;

            int i = EducationContext.SaveCourseSection(choiceofCourseViewModel);
            if (i == 3)
            {
                return Json("3", JsonRequestBehavior.AllowGet);
            }
            else if (i == 4)
            {
                return Json("4", JsonRequestBehavior.AllowGet);
            }
            else if (i == 6)
            {
                return Json("6", JsonRequestBehavior.AllowGet);
            }
            else if (i == 1)
            {
                Session["MaxPage"] = 5;
                return Json("Success!", JsonRequestBehavior.AllowGet);
            }
            else if (i == 0)
            {
                return Json("You Course Combination  Selecion Limit Exceed!", JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json("Kindly try again, there is some error!!", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult College_Bind(string courseDistrictId, string passstatus, int collagetype)
        {
            string regId = "";
            if (Session["RegId"] != null)
            {
                regId = Session["RegId"].ToString();
            }
            DataSet ds = EducationContext.College_Bind(courseDistrictId, passstatus, regId, collagetype);
            List<SelectListItem> distList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                distList.Add(new SelectListItem { Text = dr["collegename"].ToString(), Value = dr["collegeid"].ToString() });
            }
            return Json(distList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Course_Bind(string coursecollegeid)
        {
            _ = new ChoiceCourse();

            string regId = "";
            if (Session["RegId"] != null)
            {
                regId = Session["RegId"].ToString();
            }
            ChoiceCourse objDetail = objInfo.GetDetailsForChoiceofCourses(regId);
            string passstatus;
            if (objDetail.Status == "1")
            {
                passstatus = "P";
            }
            else
            {
                passstatus = "F";
            }
            DataSet ds = EducationContext.Course_Bind(coursecollegeid, objDetail.Stream, passstatus, objDetail.DisableCategory, objDetail.DA, objDetail.Age, objDetail.MF);


            List<SelectListItem> courseList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                courseList.Add(new SelectListItem { Text = dr["name"].ToString(), Value = dr["courseid"].ToString() });
            }
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CourseSection_Bind(string coursecourseid, string collegeid)
        {
            DataSet ds = EducationContext.CourseSection_Bind(coursecourseid, collegeid);
            List<SelectListItem> coursecourseList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                coursecourseList.Add(new SelectListItem { Text = dr["sectionname"].ToString(), Value = dr["coursesectionid"].ToString() });
            }
            return Json(coursecourseList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveMaxPageOnly()
        {
            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }

            string result = RegistrationFeeDBContext.SaveMaxPageOnly(regId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveMaxPageOnlywithZerofee()
        {
            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }

            string result = RegistrationFeeDBContext.SaveMaxPageOnlywithZerofee(regId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Subject_Bind(string sectionid, string courseid)
        {
            DataSet ds = EducationContext.Subject_Bind(sectionid, courseid);
            List<SelectListItem> subjectList = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                subjectList.Add(new SelectListItem { Text = dr["SubjectCombination"].ToString(), Value = dr["SubjectCombinationID"].ToString() });
            }
            return Json(subjectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCourseSectionData()
        {
            List<CandidateDetail> lst = new List<CandidateDetail>();
            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
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
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }
            lst = EducationContext.GetCourseSectionDetails(regId);

            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteCourseDetails(int id)
        {
            int i = EducationContext.DeleteCourseDetails(id);
            if (i >= 1)
            {
                Session["MaxPage"] = 5;
                return Json("Record Deleted Successfully", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Kindly try again, there is some error!!", JsonRequestBehavior.AllowGet);
            }
        }
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "2345679ACEFGHKLMNPRSWXZabcdefghkhmnpqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }
        public FileResult GetCaptchaImage()
        {
            string text = Convert.ToString(Session["Captcha"]);// UserInfo.CitizenInfo.Captcha;

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
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

        public List<SelectListItem> GetParticularSubject()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in objInfo.GetParticularSubject())
            {
                items.Add(new SelectListItem
                {
                    Text = Convert.ToString(item.Value),
                    Value = Convert.ToString(item.Id),
                    Selected = true
                });
            }
            return items;
        }
        //public List<SelectListItem> GetAllSubject()
        //{
        //    GetDataInfo objDetails = new GetDataInfo();
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    foreach (var item in objDetails.GetAllSubject())
        //    {
        //        items.Add(new SelectListItem
        //        {
        //            Text = Convert.ToString(item.Value),
        //            Value = Convert.ToString(item.Id),
        //            Selected = false
        //        });
        //    }
        //    return items;
        //}

        [HttpPost]
        public ActionResult GetData()
        {

            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
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
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }
            DataTable dt = EducationContext.getData(regId);
            //var string = IJsonConverter(dt,);

            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        public ActionResult lockPref()
        {
            clsSecurity.CheckSession();
            clsSecurity.SetCookie();
            return View();
            //string NewTab = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

            //if ((NewTab == "" || NewTab == null))
            //{

            //    return RedirectToAction("LogOut", "Account", new { area = "" });
            //}

            //else
            //{
            //    return View();
            //}
        }
        [HttpPost]
        public JsonResult PostPref(List<postPerfData> objpostPref)
        {

            string regId = "";
            string result = "0";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
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
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }

            DataTable dt = EducationContext.SavePrefData(regId, objpostPref);

            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0]["Success"].ToString();
                Session["MaxPage"] = 6;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubjects(string boardcode)
        {
            var subjects = EducationContext.GetAllSubject(0, Convert.ToInt32(boardcode));
            return Json(subjects, JsonRequestBehavior.AllowGet);
        }
        #region vishal codemerged
        public ActionResult EduQualification()
        {
            clsSecurity.CheckSession();

            string regId;
            if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }
            else
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }
            EducationViewModel educationViewModel = new EducationViewModel();

            try
            {
                // 10th Data
                educationViewModel = EducationContext.GetEduData(regId);
                educationViewModel.Stream = GetStream(regId);
                educationViewModel.BoardList = GetBoardName();
                educationViewModel.UniversityList = GetUniversityMaster();
                if (Session["IsApiData"] != null)
                    educationViewModel.IsAPI = Session["IsApiData"].ToString();

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] EduQualification() " + regId + "");
            }
            clsSecurity.SetCookie();
            return View(educationViewModel);

        }
        [HttpPost]
        public ActionResult EduQualification(EducationViewModel educationViewModel)
        {

            if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
            {
                educationViewModel.Reg_Id = Session["RegId"].ToString();
            }
            else
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }
            try
            {
                int first = 0, second = 0, third = 0, b_secondtSub = 1;
                var MarksObtain = 0;
                var MaxMarks = 0;
                decimal per = 0;
                //if (educationViewModel.IsAPI.ToLower() == "y")
                //{

                //    this.ModelState.Remove("School_12th");
                //}

                if (educationViewModel.SelectedStream == "8")
                {
                    educationViewModel.ExamPassed_10th = "";
                    educationViewModel.ExamPassed_12th = "";
                    educationViewModel.ExamPassed_Diploma = "";
                    this.ModelState.Remove("Uniboard_10th");
                    this.ModelState.Remove("School_10th");
                    this.ModelState.Remove("Rollno_10th");
                    this.ModelState.Remove("Result_10th");
                    this.ModelState.Remove("PassingYear_10th");
                    this.ModelState.Remove("MaxMarks_10th");
                    this.ModelState.Remove("MarksObtain_10th");
                    this.ModelState.Remove("Percentage_10th");

                    this.ModelState.Remove("Uniboard_12th");
                    this.ModelState.Remove("School_12th");
                    this.ModelState.Remove("Rollno_12th");
                    this.ModelState.Remove("Result_12th");
                    this.ModelState.Remove("PassingYear_12th");
                    this.ModelState.Remove("MaxMarks_12th");
                    this.ModelState.Remove("MarksObtain_12th");
                    this.ModelState.Remove("Percentage_12th");

                    this.ModelState.Remove("Uniboard_Diploma");
                    this.ModelState.Remove("School_Diploma");
                    this.ModelState.Remove("Rollno_Diploma");
                    this.ModelState.Remove("Result_Diploma");
                    this.ModelState.Remove("PassingYear_Diploma");
                    this.ModelState.Remove("MaxMarks_Diploma");
                    this.ModelState.Remove("MarksObtain_Diploma");
                    this.ModelState.Remove("Percentage_Diploma");
                }


                if (educationViewModel.SelectedStream == "1" || educationViewModel.SelectedStream == "2" || educationViewModel.SelectedStream == "3")
                {
                    educationViewModel.ExamPassed_8th = "";

                    if (string.IsNullOrEmpty(educationViewModel.Rollno_12th) || educationViewModel.Rollno_12th == null)
                    {
                        educationViewModel.ExamPassed_12th = "";
                        this.ModelState.Remove("Uniboard_12th");
                        this.ModelState.Remove("School_12th");
                        this.ModelState.Remove("Rollno_12th");
                        this.ModelState.Remove("Result_12th");
                        this.ModelState.Remove("PassingYear_12th");
                        this.ModelState.Remove("MaxMarks_12th");
                        this.ModelState.Remove("MarksObtain_12th");
                        this.ModelState.Remove("Percentage_12th");
                    }

                    if (string.IsNullOrEmpty(educationViewModel.Rollno_Diploma) || educationViewModel.Rollno_Diploma == null)
                    {
                        educationViewModel.ExamPassed_Diploma = "";
                        this.ModelState.Remove("Uniboard_Diploma");
                        this.ModelState.Remove("School_Diploma");
                        this.ModelState.Remove("Rollno_Diploma");
                        this.ModelState.Remove("Result_Diploma");
                        this.ModelState.Remove("PassingYear_Diploma");
                        this.ModelState.Remove("MaxMarks_Diploma");
                        this.ModelState.Remove("MarksObtain_Diploma");
                        this.ModelState.Remove("Percentage_Diploma");
                    }
                    this.ModelState.Remove("Uniboard_8th");
                    this.ModelState.Remove("School_8th");
                    this.ModelState.Remove("Rollno_8th");
                    this.ModelState.Remove("Result_8th");
                    this.ModelState.Remove("PassingYear_8th");
                    this.ModelState.Remove("MaxMarks_8th");
                    this.ModelState.Remove("MarksObtain_8th");
                    this.ModelState.Remove("Percentage_8th");
                }

                if (educationViewModel.SelectedStream == "4" || educationViewModel.SelectedStream == "5" || educationViewModel.SelectedStream == "6" || educationViewModel.SelectedStream == "7")
                {
                    educationViewModel.ExamPassed_8th = "";

                    this.ModelState.Remove("Uniboard_8th");
                    this.ModelState.Remove("School_8th");
                    this.ModelState.Remove("Rollno_8th");
                    this.ModelState.Remove("Result_8th");
                    this.ModelState.Remove("PassingYear_8th");
                    this.ModelState.Remove("MaxMarks_8th");
                    this.ModelState.Remove("MarksObtain_8th");
                    this.ModelState.Remove("Percentage_8th");

                    if (string.IsNullOrEmpty(educationViewModel.Rollno_Diploma) || educationViewModel.Rollno_Diploma == null)
                    {
                        educationViewModel.ExamPassed_Diploma = "";
                        this.ModelState.Remove("Uniboard_Diploma");
                        this.ModelState.Remove("School_Diploma");
                        this.ModelState.Remove("Rollno_Diploma");
                        this.ModelState.Remove("Result_Diploma");
                        this.ModelState.Remove("PassingYear_Diploma");
                        this.ModelState.Remove("MaxMarks_Diploma");
                        this.ModelState.Remove("MarksObtain_Diploma");
                        this.ModelState.Remove("Percentage_Diploma");
                    }

                }

                if (educationViewModel.ExamPassed_8th == "8th")
                {
                    if (educationViewModel.CGPA_8th == true)
                    {
                        this.ModelState.Remove("MaxMarks_8th");
                        if (Convert.ToInt32(educationViewModel.MaxMarks_8th) > 10)
                        {
                            this.ModelState.AddModelError("MaxMarks_8th", "Max Marks can not greater than 10");
                        }
                        if (Convert.ToInt32(educationViewModel.MaxMarks_8th) < 1)
                        {
                            this.ModelState.AddModelError("MaxMarks_8th", "Should be greater than 1");
                        }

                    }
                    else
                    {
                        if (Convert.ToInt32(educationViewModel.MarksObtain_8th) > Convert.ToInt32(educationViewModel.MaxMarks_8th))
                        {
                            this.ModelState.AddModelError("MarksObtain_8th", "Marks obtain can not be greater than Maximum Marks");
                        }
                    }
                }
                if (educationViewModel.ExamPassed_10th == "10th")
                {
                    if (educationViewModel.CGPA_10th == true)
                    {
                        this.ModelState.Remove("MaxMarks_10th");
                        if (Convert.ToInt32(educationViewModel.MaxMarks_10th) > 10)
                        {
                            this.ModelState.AddModelError("MaxMarks_10th", "Max Marks can not greater than 10");
                        }
                        if (Convert.ToInt32(educationViewModel.MaxMarks_10th) < 1)
                        {
                            this.ModelState.AddModelError("MaxMarks_10th", "Should be greater than 1");
                        }

                    }
                    else
                    {
                        if (Convert.ToInt32(educationViewModel.MarksObtain_10th) > Convert.ToInt32(educationViewModel.MaxMarks_10th))
                        {
                            this.ModelState.AddModelError("MarksObtain_10th", "Marks obtain can not be greater than Maximum Marks");
                        }
                    }
                }

                if (educationViewModel.ExamPassed_12th == "12th")
                {
                    if (Convert.ToInt32(educationViewModel.MarksObtain_12th) > Convert.ToInt32(educationViewModel.MaxMarks_12th))
                    {
                        this.ModelState.AddModelError("MarksObtain_12th", "Marks obtain can not be greater than Maximum Marks");
                    }
                }
                if (educationViewModel.SelectedStream == "3" || educationViewModel.SelectedStream == "4")
                {

                    for (int i = 0; i < educationViewModel.subjectDetails.Count; i++)
                    {
                        // Check if MaxMarks does not have a value
                        if (educationViewModel.subjectDetails[i].MaxMarks == null || Convert.ToInt32(educationViewModel.subjectDetails[i].MaxMarks) == 0)
                        {
                            this.ModelState.Remove("subjectDetails[" + i + "].SelectedSubjectId");
                            this.ModelState.Remove("subjectDetails[" + i + "].MarksObtain");
                            this.ModelState.Remove("subjectDetails[" + i + "].MaxMarks");
                        }

                    }
                }
                if (educationViewModel.SelectedStream != "8" && educationViewModel.SelectedStream != "3")
                {
                    if (ModelState.IsValid)
                    {
                        for (int i = 0; i < educationViewModel.subjectDetails.Count; i++)
                        {
                            if (Convert.ToDecimal(educationViewModel.subjectDetails[i].MarksObtain) > Convert.ToDecimal(educationViewModel.subjectDetails[i].MaxMarks))
                            {
                                this.ModelState.AddModelError("subjectDetails[" + i + "].MarksObtain", "Marks obtain can not be greater than Maximum Marks");
                            }
                            else
                            {  // calculate top 5 marks percentage
                               //MarksObtain = MarksObtain + Convert.ToInt32(educationViewModel.subjectDetails[i].MarksObtain);
                               //MaxMarks = MaxMarks + Convert.ToInt32(educationViewModel.subjectDetails[i].MaxMarks);
                            }
                        }
                        //if (educationViewModel.subjectDetails.Count > 0)
                        //{
                        //    decimal _MarksObtain = educationViewModel.subjectDetails.Sum(item => Convert.ToInt32(item.MarksObtain));
                        //    decimal _MaxMarks = educationViewModel.subjectDetails.Sum(item => Convert.ToInt32(item.MaxMarks));
                        //    educationViewModel.BestFive_Percentage = Math.Round((_MarksObtain / _MaxMarks) * 100, 2);
                        //}

                    }

                }
                //if (educationViewModel.SelectedStream == "8")
                //{

                //    for (int i = 0; i < educationViewModel.subjectDetails.Count; i++)
                //    {
                //        this.ModelState.Remove("subjectDetails[" + i + "].SelectedSubjectId");
                //        this.ModelState.Remove("subjectDetails[" + i + "].MarksObtain");
                //        this.ModelState.Remove("subjectDetails[" + i + "].MaxMarks");
                //    }
                //}
                if (educationViewModel.ExamPassed_10th == "10th")
                {
                    if (MarksObtain > educationViewModel.MarksObtain_10th)
                    {
                        this.ModelState.AddModelError("MarksObtain_10th", "Subject wise marks can't greater than 10th marks obt.");
                    }
                }



                if (ModelState.IsValid)
                {
                    educationViewModel.Reg_Id = Session["RegId"].ToString();
                    if (educationViewModel.ExamPassed_8th == "8th")
                    {
                        first = EducationContext.Save08THEduQualification(educationViewModel);
                        if (first == 0)
                        {
                            TempData["EduqualificationSave"] = 99;
                            return RedirectToAction("EduQualification", "Account", new { area = "" });
                        }
                    }
                    if (educationViewModel.ExamPassed_10th == "10th")
                    {
                        first = EducationContext.Save10THEduQualification(educationViewModel);
                        if (first == 0)
                        {
                            TempData["EduqualificationSave"] = 99;
                            return RedirectToAction("EduQualification", "Account", new { area = "" });
                        }
                    }
                    if (educationViewModel.ExamPassed_12th == "12th")
                    {
                        first = EducationContext.Save12THEduQualification(educationViewModel);
                        if (first == 0)
                        {
                            TempData["EduqualificationSave"] = 99;
                            return RedirectToAction("EduQualification", "Account", new { area = "" });
                        }
                    }
                    if (educationViewModel.ExamPassed_Diploma.ToLower() == "graduation" && first == 1)
                    {
                        third = EducationContext.SaveDiplomaEduQualification(educationViewModel);
                    }

                    // save subject details
                    if (educationViewModel.SelectedStream != "8" && educationViewModel.SelectedStream != "3" && educationViewModel.IsAPI != "Y")
                    {
                        b_secondtSub = EducationContext.SaveSubjectDetail(educationViewModel);
                    }

                    if (educationViewModel.SelectedStream == "8" && first == 1)
                    {
                        ViewBag.Success = "Education Qualification Details has been successfully saved";
                        TempData["EduqualificationSave"] = 1;
                        Session["MaxPage"] = "2";
                        return RedirectToAction("Weightage", "Account");
                    }
                    else if (first == 1 && Convert.ToInt32(educationViewModel.SelectedStream) <= 7 && b_secondtSub > 0)
                    {
                        ViewBag.Success = "Education Qualification Details has been successfully saved";
                        TempData["EduqualificationSave"] = 1;
                        Session["MaxPage"] = "2";
                        return RedirectToAction("Weightage", "Account");
                    }
                    else
                    {
                        TempData["EduqualificationSave"] = 99;
                        return RedirectToAction("EduQualification", "Account", new { area = "" });
                    }
                }
                else
                {
                    educationViewModel.Reg_Id = Session["RegId"].ToString();
                    educationViewModel = EducationContext.GetEduData(educationViewModel.Reg_Id);
                    educationViewModel.IsAPI = Session["IsApiData"].ToString();
                    educationViewModel.Stream = GetStream(Convert.ToString(Session["RegId"]));
                    educationViewModel.BoardList = GetBoardName();
                    educationViewModel.UniversityList = GetUniversityMaster();
                    return View(educationViewModel);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] EduQualification()" + Session["RegId"].ToString());
            }
            return View();
        }
        public ActionResult Declaration()
        {
            //clsSecurity.CheckSession();
            DataSet ds = new DataSet();
            int RemainingRegFee = 0;
            //int PaidRegFee = 0;

            string regId = "";
            if (Session["RegId"] != null && Session["RegId"].ToString() != "")
            {
                regId = Session["RegId"].ToString();
            }
            else
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }
            Session["ValidationStatusCode"] = "0";
            Session["ValidationStatusMsg"] = "";
            objuserMaxCurrentPage = objInfo.GetMax_Current_page(regId);
            objDetail.Current_page = objuserMaxCurrentPage.current_page;
            objDetail.Max_page = objuserMaxCurrentPage.max_page;
            objDetail.Verificationstatus = objuserMaxCurrentPage.Verificationstatus;
            objDetail.HasUnlocked = objuserMaxCurrentPage.HasUnlocked;
            Session["Verificationstatus"] = objDetail.Verificationstatus == null ? "" : objDetail.Verificationstatus;
            if (objuserMaxCurrentPage.FormStatus != "Y" && Session["ChangeChoice"].ToString() == "Y")
            {
                Session["Verificationstatus"] = "";
            }
            Session["HasUnlocked"] = objDetail.HasUnlocked == null ? "" : objDetail.HasUnlocked;
            Session["MaxPage"] = objDetail.Max_page;
            Session["MaxPage2"] = objDetail.Max_page;


            //  CHECK REMAINING REG FEE
            ds = EducationContext.CheckRemainingRegFee(regId);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RemainingRegFee = Convert.ToInt32(ds.Tables[0].Rows[0]["FeeAmount"]);
                }
                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    PaidRegFee = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
                //}
                if (RemainingRegFee > 0)
                {
                    Session["RemainingRegFee"] = "Y";
                }
                else
                {
                    Session["RemainingRegFee"] = "N";
                }
            }


            //clsSecurity.SetCookie();
            return View();

        }
        #endregion
        #region Ashish Sir merged code
        [HttpPost]
        public ActionResult RegistrationValidation(FormCollection formCollect, RegistrationViewModel registrationViewModel)
        {

            try
            {
                objDetailRegister.TwelveRollNo = formCollect.Get("TwelveRollNo");
                objDetailRegister.PassingOfYear = (formCollect.Get("PassingOfYear"));
                objDetailRegister.CandidateName = formCollect.Get("CandidateName");
                objDetailRegister.BoardCode = formCollect.Get("BoardCode");
                objDetailRegister.QualificationCode = formCollect.Get("QualificationCode");
                clsDGLocker digiloker = new clsDGLocker();
                digiloker.FUllName = objDetailRegister.CandidateName;
                digiloker.Year = objDetailRegister.PassingOfYear.ToString();
                digiloker.RollNo = objDetailRegister.TwelveRollNo;
                digiloker.Board = objDetailRegister.BoardCode;
                DataSet checckStudent = new DataSet();
                checckStudent = clsDGLocker.GetHBSEDataRegistrationtable(digiloker);
                string REGID = "";
                if (checckStudent.Tables[0].Rows.Count > 0)
                {
                    REGID = checckStudent.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    if (objDetailRegister.QualificationCode == "08")
                    {
                        TempData["Isapi"] = "N";
                        Session["CheckApi"] = TempData["Isapi"];
                        CandidateName = formCollect.Get("CandidateName");
                    }
                    else
                    {
                        if (objDetailRegister.BoardCode == "119" && objDetailRegister.QualificationCode != "08")
                        {
                            DataSet ds = new DataSet();
                            if (objDetailRegister.QualificationCode == "12")
                            {
                                ds = clsDGLocker.Get12HBSEData(digiloker, objDetailRegister.QualificationCode);
                            }
                            else
                            {
                                ds = clsDGLocker.Get10HBSEData(digiloker, objDetailRegister.QualificationCode);
                            }
                            int tot = Convert.ToInt32(ds.Tables[0].Rows.Count);
                            if (tot > 0)
                            {
                                CandidateName = ds.Tables[0].Rows[0]["v_cname"].ToString();
                                Fathername = ds.Tables[0].Rows[0]["v_fname"].ToString();
                                motherName = ds.Tables[0].Rows[0]["v_mane"].ToString();
                                genderid = ds.Tables[0].Rows[0]["v_gender"].ToString();
                                v_dob = ds.Tables[0].Rows[0]["v_dob"].ToString();
                                v_aadharno = ds.Tables[0].Rows[0]["v_aadharno"].ToString();
                                C_ResultStatus = ds.Tables[0].Rows[0]["v_RESULT"].ToString();
                                mobileno = ds.Tables[0].Rows[0]["v_mobile"].ToString();

                                TempData["Isapi"] = "Y";
                                Session["CheckApi"] = TempData["Isapi"];
                            }
                            else
                            {
                                TempData["Isapi"] = "N";
                                Session["CheckApi"] = TempData["Isapi"];
                                CandidateName = formCollect.Get("CandidateName");
                            }
                        }

                        else if (objDetailRegister.BoardCode == "68" && objDetailRegister.QualificationCode != "08")
                        {
                            DataSet ds = new DataSet();
                            ds = clsDGLocker.GetDocumentNew(digiloker, objDetailRegister.QualificationCode);
                            if (ds.Tables.Count <= 2)
                            {
                                TempData["Isapi"] = "N";
                                Session["CheckApi"] = TempData["Isapi"];
                                CandidateName = formCollect.Get("CandidateName");
                            }
                            else
                            {
                                for (int count = 0; count < ds.Tables.Count; count++)
                                {
                                    DataTable table = ds.Tables[count];
                                    int i = table.Rows.Count;
                                    if (table.TableName == "Person")
                                    {
                                        CandidateName = table.Rows[0]["name"].ToString();
                                        Fathername = table.Rows[0]["swd"].ToString();
                                        motherName = table.Rows[0]["motherName"].ToString();
                                        gender = table.Rows[0]["gender"].ToString();
                                        v_dob = table.Rows[0]["dob"].ToString().Replace("--", "");
                                        TempData["Isapi"] = "Y";
                                        Session["CheckApi"] = TempData["Isapi"];
                                    }
                                    if (table.TableName == "Performance")
                                    {
                                        C_ResultStatus = table.Rows[0]["result"].ToString();
                                    }
                                    if (table.TableName == "Subject")
                                    {
                                        table.DefaultView.Sort = "marksTotal DESC";
                                        table = table.DefaultView.ToTable();
                                        int totalsubjet = table.Rows.Count;
                                        if (totalsubjet >= 3)
                                        {
                                        }
                                        else
                                        {
                                            Session["CheckApi"] = "N";
                                        }
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(gender))
                            {
                                if (gender == "Male" || gender == "M")
                                {
                                    genderid = "1";
                                }
                                else if (gender == "Female" || gender == "F")
                                {
                                    genderid = "2";
                                }
                                else
                                {
                                    genderid = "3";
                                }
                            }
                        }

                        else
                        {
                            TempData["Isapi"] = "N";
                            Session["CheckApi"] = TempData["Isapi"];
                            CandidateName = formCollect.Get("CandidateName");
                        }
                    }
                }

                RollNumber = formCollect.Get("TwelveRollNo");

                TempData["CandidateName"] = CandidateName;
                TempData["PassingOfYear"] = objDetailRegister.PassingOfYear.ToString();
                TempData["BoardCode"] = formCollect.Get("BoardCode");
                TempData["QualificationCode"] = formCollect.Get("QualificationCode");
                TempData["TwelveRollNo"] = formCollect.Get("TwelveRollNo");
                Session["BoardCode"] = formCollect.Get("BoardCode");
                Session["QualificationCode"] = formCollect.Get("QualificationCode");
                Session["TwelveRollNo"] = formCollect.Get("TwelveRollNo");
                Session["CandidateName"] = CandidateName;
                Session["PassingYear"] = formCollect.Get("PassingOfYear");
                TempData["FatherHusbandName"] = Fathername;
                TempData["MotherName"] = motherName;
                TempData["mobileno"] = mobileno;

                TempData["C_ResultStatus"] = C_ResultStatus;

                TempData["Sex"] = genderid;
                TempData["REGid"] = REGID;

                TempData["v_dob"] = v_dob;
                TempData["v_aadharno"] = v_aadharno;
                Session["v_aadharno"] = v_aadharno;
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Please try again. Something went wrong";
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] RegistrationValidation()");
                return RedirectToAction("Registration");
            }
            return RedirectToAction("Registration");
        }

        #endregion

        #region Prashant code merged
        [HttpPost]
        public JsonResult SendOTP(OTPModel oTPModel)
        {
            bool ret = true;
            string i = "";
            string mobile = EducationContext.GetMobile(oTPModel.Mobileno);
            string email = EducationContext.GetEmail(oTPModel.Email);
            string regid = EducationContext.GetRegId(Session["TwelveRollNo"].ToString(), Session["PassingYear"].ToString());
            string isapi = Session["CheckApi"].ToString();
            if (oTPModel.Mobileno == mobile)
            {
                return Json("MobileExist", JsonRequestBehavior.AllowGet);
            }
            else if (oTPModel.Email == email)
            {
                return Json("EmailExist", JsonRequestBehavior.AllowGet);
            }
            else if (regid != "")
            {
                return Json("RegExist", JsonRequestBehavior.AllowGet);
            }
            else
            {

                i = EducationContext.SendOTP(oTPModel.Mobileno);
                return Json(i, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult FetchMarks(FetchMarkModel fetchMarkModel)
        {
            string regId;
            string name;
            if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
                name = Session["CandidateName"].ToString();
            }
            else
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }

            clsDGLocker digiloker = new clsDGLocker();
            digiloker.RollNo = fetchMarkModel.RollNo;
            digiloker.Year = fetchMarkModel.Year;
            digiloker.FUllName = name;
            digiloker.Board = fetchMarkModel.Board;


            DataSet ds = new DataSet();
            ds = clsDGLocker.GetDocumentNew2(digiloker, fetchMarkModel.QualificationCode, regId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetMarkSheet(string Exampassed)
        {
            string regId;
            string name;
            if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
                name = Session["CandidateName"].ToString();
            }
            else
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }
            string QualificationCode = "";
            clsDGLocker digiloker = new clsDGLocker();
            digiloker = EducationContext.GetEduSavedData(regId, Exampassed);
            if (!string.IsNullOrEmpty(digiloker.RollNo))
            {
                digiloker.FUllName = name;
            }
            if (Exampassed == "10th")
            {
                QualificationCode = "10";
            }
            else
            {
                QualificationCode = "12";
            }

            string marksheet = clsDGLocker.GetMarkSheet(digiloker, QualificationCode, regId);
            if (!string.IsNullOrEmpty(marksheet))
            {

                Document objBL = new Document();
                objBL.Docs = marksheet;
                objBL.DocsName = "marksheet.pdf";
                objBL.Docid = QualificationCode == "10" ? "1" : "2";
                objBL.Reg_Id = regId;
                objBL.Isverify = "N";
                objBL.IsApi = "Y";
                int i = EducationContext.SaveDocument(objBL);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckOTP(string Mobileno, string OTP)
        {
            string i = "";
            i = EducationContext.CheckOTP(Mobileno, OTP);
            return Json(i, JsonRequestBehavior.AllowGet);

        }
        #endregion

        [HttpPost]
        public JsonResult SaveUserPageDeclaration(string Mobile, string Email)
        {
            //test
            string regId = "";
            if (Session["RegId"] != null && Session["RegId"].ToString() != "")
            {
                regId = Session["RegId"].ToString();
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
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }

            DataTable dt = EducationContext.SaveUserPageDeclaration(regId, Mobile, Email);
            objuserMaxCurrentPage = objInfo.GetMax_Current_page(regId);
            objDetail.Current_page = objuserMaxCurrentPage.current_page;
            objDetail.Max_page = objuserMaxCurrentPage.max_page;
            objDetail.Verificationstatus = objuserMaxCurrentPage.Verificationstatus;
            //Session["CandidatePayment"] = "Y";
            //Session["rankcard"] = 1;
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getDeclarationDataRAJ()
        {
            // string regId = "";
            if (Session["RegId"] != null && Session["RegId"].ToString() != "")
            {
                // regId = Session["RegId"].ToString();
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
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }
            DataSet dt = EducationContext.getDeclarationDataRAJ(Session["RegId"].ToString());
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckSubComMin()
        {
            DataTable dt = new DataTable();
            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
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
                return Json(new
                {
                    redirectUrl = Url.Action("Login", "Account"),
                    isRedirect = true
                });
            }
            dt = EducationContext.CheckSubComMin(regId);

            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckAadhar(string AadharNo)
        {
            string i = "";
            bool Aadhar = Verhoeff.validateVerhoeff(AadharNo);
            if ((!Aadhar && (AadharNo != "1")) || (AadharNo == "0"))
            {
                return Json("00", JsonRequestBehavior.AllowGet);
            }
            else
            {
                //i = EducationContext.SendOTP(Mobileno);
                return Json(i, JsonRequestBehavior.AllowGet);
            }
        }

        [ChildActionOnly]
        public ActionResult Header()
        {

            string NewTab = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];

            if ((NewTab == "" || NewTab == null))
                return RedirectToAction("LogOut", "Account", new { area = "" });
            else
                return View("~/Views/Shared/_Header.cshtml");
        }
        [HttpPost]
        public JsonResult SendOTPUnlock()
        {
            string i;
            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }
            i = EducationContext.SendOTPUnlock(regId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CheckOTPUnlock(string OTP)
        {
            string i = "";
            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }
            i = EducationContext.CheckOTPUnlock(OTP, regId);
            if (i == "1")
            {
                Session["Verificationstatus"] = "U";
                Session["HasUnlocked"] = "Y";
                Session["MaxPage"] = "1";
            }
            return Json(i, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult GetCandidateVerifyObjection()
        {

            string regId = "";

            DataTable dt = new DataTable();
            try
            {
                if (Session["RegId"] != null)
                {
                    regId = Session["RegId"].ToString();
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
                    return Json(new
                    {
                        redirectUrl = Url.Action("Login", "Account"),
                        isRedirect = true
                    });
                }

                dt = EducationContext.GetCandidateVerifyObjection(regId);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] GetCandidateVerifyObjection()");
            }
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        //public static string MD5Hash(string text)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();

        //    //compute hash from the bytes of text  
        //    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

        //    //get hash result after compute it  
        //    byte[] result = md5.Hash;

        //    StringBuilder strBuilder = new StringBuilder();
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        //change it into 2 hexadecimal digits  
        //        //for each byte  
        //        strBuilder.Append(result[i].ToString("x2"));
        //    }

        //    return strBuilder.ToString();
        //}

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

        private string SavePGDetail()
        {
            string Result = "Fail";
            try
            {
                DataTable dtPayDetail = new DataTable();
                objpay.MihpayID = PayUResponse.MihpayID;
                objpay.Mode = PayUResponse.Mode;
                objpay.Status = PayUResponse.Status;
                objpay.EdishaXtnID = PayUResponse.EdishXtnID;
                objpay.TotalAmount = Convert.ToDecimal(PayUResponse.TotalAmount);
                objpay.Net_Amount_Debit = Convert.ToDecimal(PayUResponse.Net_Amount_Debit);
                objpay.Error_Msg = PayUResponse.Error_Msg;
                objpay.AppCode = PayUResponse.AppCode;
                objpay.ApplicantName = PayUResponse.ApplicantName;
                objpay.Address1_PROPNO = PayUResponse.Address1;
                objpay.Address2_COLNAME = PayUResponse.Address2;
                objpay.City_MCNAME = PayUResponse.City;
                objpay.EmailId = PayUResponse.email;
                objpay.MobileNo = PayUResponse.MobileNo;
                objpay.BankCode = PayUResponse.BankCode;
                objpay.Error = PayUResponse.Error;
                objpay.Bank_Ref_No = PayUResponse.Bank_Ref_No;
                objpay.PG_Type = PayUResponse.PG_Type;
                objpay.Payment_Source = PayUResponse.Payment_Source;
                objpay.CardNo = PayUResponse.CardNo;
                objpay.UnMappedStatus = PayUResponse.UnMappedStatus;
                objpay.Issuing_Bank = PayUResponse.Issuing_Bank;
                objpay.Card_Type = PayUResponse.Card_Type;
                objpay.Name_On_Card = PayUResponse.Name_On_Card;
                objpay.addedon = PayUResponse.AddedOn;
                objpay.udf1_PID = PayUResponse.PID;
                objpay.udf2_MCODE = PayUResponse.MCode;
                objpay.udf3_YEAR = PayUResponse.FinYear;
                objpay.udf4_PTaxFlag = PayUResponse.PTax_Flag;
                objpay.udf5_ServiceName = PayUResponse.ServiceName;
                objpay.ResponseSource = PayUResponse.ResponseSource;
                dtPayDetail = objpay.SavePGDetail();
                Result = "Success";
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpPost] SavePGDetail()");
            }
            return Result;
        }
        [HttpPost]
        public JsonResult SetApiStatusNo()
        {
            string result;
            string regId;

            if (Session["RegId"] == null || Session["RegId"].ToString() == "")
            {
                return Json("00", JsonRequestBehavior.AllowGet);
            }
            else
            {
                regId = Session["RegId"].ToString();
            }

            result = EducationContext.SetApiStatusNo(regId);
            if (result == "1")
            {
                Session["IsApiData"] = "N";
                Session["CheckApi"] = "N";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Fee Payment College PG 

        //PPP From FamilyID
        public JsonResult GetMemberfromPPP(string FamilyID)
        {
            string FamilyIdRequestData = "";
            string FamilyIdResponseData = "";
            FamiyID objRequest = new FamiyID();

            objRequest.DeptCode = "ITI";
            objRequest.ServiceCode = "ADM";
            objRequest.DeptKey = "n8ik3rfi56";
            objRequest.UIDFID = FamilyID;

            FamilyIdRequestData = JsonConvert.SerializeObject(objRequest);
            RootObj obj = new RootObj();
            FamilyIdResponseData = CallFamilyIdAPI(FamilyIdRequestData, "F");

            dynamic innerlevel1 = JsonConvert.DeserializeObject(FamilyIdResponseData);
            string message = innerlevel1["message"].ToString();
            if (message.ToLower() == "please get your document signed at ppp portal.")
            {
                // string jsonData = "{\"status\":\"nosigned\",\"message\":\"please get your document signed at ppp portal.\"}";
                return Json("{nosigned}", JsonRequestBehavior.AllowGet);
            }
            obj = (RootObj)JsonConvert.DeserializeObject(FamilyIdResponseData, typeof(RootObj));

            lstMemberId = GetFamilyData(obj, FamilyIdResponseData);

            if (lstMemberId == null)
            {
                return Json("{}", JsonRequestBehavior.AllowGet);
            }

            return Json(lstMemberId, JsonRequestBehavior.AllowGet);

        }

        //PPP from Aadhaar
        public JsonResult GetMemberfromPPPViaAadhaar(string AadharNo)
        {
            string FamilyIdRequestData = "";
            string FamilyIdResponseData = "";
            FamiyID objRequest = new FamiyID();

            objRequest.DeptCode = "ITI";
            objRequest.ServiceCode = "ADM";
            objRequest.DeptKey = "n8ik3rfi56";
            objRequest.UIDFID = AadharNo;

            FamilyIdRequestData = JsonConvert.SerializeObject(objRequest);
            RootObj obj = new RootObj();
            FamilyIdResponseData = CallFamilyIdAPI(FamilyIdRequestData, "U");

            if (FamilyIdResponseData == null)
            {
                return Json("{}", JsonRequestBehavior.AllowGet);
            }

            return Json(FamilyIdResponseData, JsonRequestBehavior.AllowGet);

        }
        //Call FamilyID API (URL)
        public string CallFamilyIdAPI(string JsonData, string FIDUID)
        {
            string result = "";
            string uri = "";
            try
            {
                //prod
                if (FIDUID == "F")
                {
                    uri = WebConfigurationManager.AppSettings["APIUrl_GetMemberFromFamilyID"];

                }
                else if (FIDUID == "U")
                {
                    uri = WebConfigurationManager.AppSettings["APIUrl_GetMemberFromAadhaarID"];
                }

                string json = JsonData;
                var data = Encoding.UTF8.GetBytes(json.Replace("'", "\""));
                result = SendRequest(new Uri(uri), data, "application/json", "POST");

            }
            catch (WebException ex)
            {
                result = ex.Message;

            }
            return result;
        }


        public List<SelectListItem> GetFamilyData(RootObj obj, string result)
        {
            try
            {
                dynamic innerlevel1 = JsonConvert.DeserializeObject(result);
                string status = innerlevel1["status"].ToString();
                if (status.ToLower() == "failed")
                {

                    return null;
                }
                obj = JsonConvert.DeserializeObject<RootObj>(innerlevel1["result"].ToString());
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (var item in obj.dropdown)
                {
                    items.Add(new SelectListItem
                    {
                        Text = Convert.ToString(item.Text),
                        Value = Convert.ToString(item.Value),
                        Selected = false

                    });
                }
                if (result == "")
                {
                    return null;
                }
                return items;
            }

            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "AccountController/GetFamilyData";
                clsLogger.ExceptionMsg = "GetFamilyData";
                clsLogger.SaveException();
                return null;
            }

        }

        pgVerificationDBContext verifyObj = new pgVerificationDBContext();
        [HttpPost]
        public JsonResult SaveVerificationData(String Reg_Id, String Div_Id, String Remarks, String Verification_Status, String Remarks2)
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
                    dt =
                        verifyObj.SaveVerificationData(Reg_Id, Div_Id, Remarks, Verification_Status, Remarks2);
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
        //Send OTP for MemberID (URL)
        public JsonResult SendOTPforMEMID(string MemberId)
        {
            string returnOTPStatus = string.Empty;
            string RequestJsonData = "";
            string ResponseData = "";
            CandidateDetail objPDetail = new CandidateDetail();

            ViewBag.hdtxnId = "";
            ViewBag.hdStatusOTP = "";
            ViewBag.hdStatusMessageOTP = "";

            OTPRequestforMEMID objRequest = new OTPRequestforMEMID();
            OTPResponseMemID objResp = new OTPResponseMemID();
            objRequest.DeptCode = "ITI";
            objRequest.ServiceCode = "ADM";
            objRequest.DeptKey = "n8ik3rfi56";
            objRequest.MemberID = MemberId;
            RequestJsonData = JsonConvert.SerializeObject(objRequest);
            try
            {
                string uri = WebConfigurationManager.AppSettings["APIUrl_OTPRequestforMEMID"];

                string json = RequestJsonData;
                var data = Encoding.UTF8.GetBytes(json.Replace("'", "\""));

                ResponseData = SendRequest(new Uri(uri), data, "application/json", "POST");
                dynamic innerlevel1 = JsonConvert.DeserializeObject(ResponseData);


                objResp = JsonConvert.DeserializeObject<OTPResponseMemID>(innerlevel1["result"].ToString());

                ViewBag.hdStatusOTP = objResp.status;
                ViewBag.hdStatusMessageOTP = objResp.message;
                ViewBag.hdtxnId = objResp.txn;



                if (ViewBag.hdStatusOTP == "Failed")
                {
                    return Json("{'status':'Failed'}", JsonRequestBehavior.AllowGet);
                }
                else if (ViewBag.hdStatusOTP == "Successfull")
                {
                    // returnOTPStatus = "1";
                    return Json(ResponseData, JsonRequestBehavior.AllowGet);

                    // return Json(returnOTPStatus, JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "AccountController/SendOTPforMEMID";
                clsLogger.ExceptionMsg = "SendOTPforMEMID";
                clsLogger.ExceptionDetail = ResponseData;
                clsLogger.SaveException();
                return Json("{}", JsonRequestBehavior.AllowGet);
            }

            return Json(returnOTPStatus, JsonRequestBehavior.AllowGet);
        }

        private string SendRequest(Uri uri, byte[] jsonDataBytes, string contentType, string method)
        {
            string response;
            WebRequest request;
            request = WebRequest.Create(uri);
            request.ContentLength = jsonDataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(jsonDataBytes, 0, jsonDataBytes.Length);
                requestStream.Close();

                using (var responseStream = request.GetResponse().GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        response = reader.ReadToEnd();
                    }
                }
            }

            return response;
        }
        //Fetch Data from PPP
        public JsonResult VerifyOTP(string MemberId, string OTP, string TXN)
        {
            string MemIdOTPRequestData = "";
            string MemIdResponseData = "";
            VerifyOTPRequestforMEMID objRequest = new VerifyOTPRequestforMEMID();
            objRequest.DeptCode = "ITI";
            objRequest.ServiceCode = "ADM";
            objRequest.DeptKey = "n8ik3rfi56";
            objRequest.MemberID = MemberId;
            objRequest.OTP = OTP;
            objRequest.TXN = TXN;
            MemIdOTPRequestData = JsonConvert.SerializeObject(objRequest);

            MemIdResponseData = VerifyPPPOTP(MemIdOTPRequestData);


            if (MemIdResponseData == "")
            {
                string jsonData = "{\"status\":\"nodata\",\"message\":\"There is no data fetch from this memeber id\"}";
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                RootObj obj = new RootObj();
                dynamic innerlevel1 = JsonConvert.DeserializeObject(MemIdResponseData);
                string status = innerlevel1["status"].ToString();
                if (status.ToLower() == "failed")
                {
                    clsLogger.ExceptionError = status + "_Data not found_";
                    clsLogger.ExceptionPage = "AccountController/VerifyOTP";
                    clsLogger.ExceptionMsg = "VerifyOTP";
                    clsLogger.ExceptionDetail = "";
                    clsLogger.SaveException();
                    return Json(MemIdResponseData, JsonRequestBehavior.AllowGet);
                }
                if (status.ToLower() == "successfull")
                {
                    return Json(MemIdResponseData, JsonRequestBehavior.AllowGet);
                }
                return Json(MemIdResponseData, JsonRequestBehavior.AllowGet);
            }
        }

        //Verify PPP OTP (URL)
        public string VerifyPPPOTP(string JSONOTPData)
        {
            string Result = "";
            string Data_type = string.Empty;
            try
            {
                string uri = WebConfigurationManager.AppSettings["APIUrl_VerifyOTPRequestforMEMID"];
                string json = JSONOTPData;
                var data = Encoding.UTF8.GetBytes(json.Replace("'", "\""));
                Result = SendRequest(new Uri(uri), data, "application/json", "POST");
                if (Result.ToLower() == "unable to connect to the remote server")
                {
                    return Result;
                }
            }


            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "AccountController/VerifyPPPOTP";
                clsLogger.ExceptionMsg = "VerifyPPPOTP";
                clsLogger.SaveException();
                return Result;
            }
            return Result;
        }

        public JsonResult SSOLOGIN(string FamilyID)
        {
            CultureInfo us = new CultureInfo("en-US");
            CultureInfo enGB = new CultureInfo("en-GB");
            String now = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss tt", enGB);//my change
                                                                                // String now = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                                                                                //DateTime date3 = DateTime.ParseExact(now, @"dd/MM/yyyy HH:mm:ss", us);
                                                                                // String now1 = Convert.ToString(date3);
            string ReqString = FamilyID + "|" + now;
            ReqString = Encrypt(ReqString, "P845f6L#*J8DK20NLK83#56l#yf#");
            // string url = "http://164.100.137.245/mmpsyweb/SSO/SSOLogin?DeptCode=DOHE&servicecode=SCH&input=" + ReqString;
            string url = "https://meraparivar.haryana.gov.in/SSO/SSOLogin?DeptCode=DOHE&servicecode=SCH&input=" + ReqString;
            var anonym = new
            {
                URL = url
            };
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(anonym), JsonRequestBehavior.AllowGet);

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
        [HttpGet]
        public JsonResult getDeclarationDataRAJ1(string Regid)
        {

            var decrypptedRegId = HttpUtility.UrlDecode(dec.DecryptKey(Regid, "dheticketabhi@mohi#2020"));
            DataSet dt = EducationContext.getDeclarationDataRAJ(decrypptedRegId);
            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetData1(string Regid)
        {

            var decrypptedRegId = HttpUtility.UrlDecode(dec.DecryptKey(Regid, "dheticketabhi@mohi#2020"));
            DataTable dt = EducationContext.getData(decrypptedRegId);
            //var string = IJsonConverter(dt,);

            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CandiDet(string RegId)
        {
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
                Session["HasUnlocked"] = objDetail.HasUnlocked == null ? "" : objDetail.HasUnlocked;
                */

            var encryptedRegId = HttpUtility.UrlEncode(dec.DecryptKey(
                 HttpUtility.UrlDecode(RegId)
                , "dheticketabhi@mohi#2020"));
            ViewBag.Regid = RegId;
            ViewBag.URegId = encryptedRegId;
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

        public JsonResult CheckOTPFee(string OTP)
        {
            string i = "";
            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }
            i = EducationContext.CheckOTPUnlockFee(OTP, regId);
            return Json(i, JsonRequestBehavior.AllowGet);

        }
        public JsonResult SendOTPConfirmSeat()
        {
            string i;
            string regId = "";
            if ((Session["RegId"].ToString() != null) && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }
            i = EducationContext.SendOTPConfirmSeat(regId);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CancelAdmissionITI(string Reason)
        {
            try
            {
                string result = "0";
                string regId = "";

                if (Reason == "" || Reason == null)
                {
                    return Json("999", JsonRequestBehavior.AllowGet);
                }
                if ((Session["RegId"] != null) && (Session["RegId"].ToString() != ""))
                {
                    regId = Session["RegId"].ToString();
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
                    Response.Redirect("/Account/LogOut");
                }

                result = EducationContext.CancelAdmissionITI(regId, Reason);
                if (result == "1")
                {
                    Session["ChangeChoice"] = "Y";
                    Session["Verificationstatus"] = "";
                    //Session["MaxPage"] = "5";

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
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] CancelAdmissionITI()");
                return Json("999", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Get10thDATA()
        {
            DataSet ds = new DataSet();

            try
            {
                clsDGLocker digiloker = new clsDGLocker();

                DataTable dt = EducationContext.getData10th_ITI();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        digiloker.Board = "119";
                        digiloker.FUllName = dt.Rows[i]["CandidateName"].ToString();
                        digiloker.Year = dt.Rows[i]["PassingYear"].ToString();
                        digiloker.RollNo = dt.Rows[i]["RegistrationRollno"].ToString();
                        ds = clsDGLocker.GetDocumentNew(digiloker, "10");

                    }
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.AccountContoller.[HttpGet] CancelAdmissionITI()");
            }
            return View();
        }
        [HttpPost]
        public JsonResult EditApplication()
        {
            string i = "1";
            string regid = Session["RegId"].ToString();
            i = EducationContext.EditApplication(regid);
            if (i == "1")
            {
                Session["Verificationstatus"] = "";
            }
            return Json(i, JsonRequestBehavior.AllowGet);

        }
    }


}