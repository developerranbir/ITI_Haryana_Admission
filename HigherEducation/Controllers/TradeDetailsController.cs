using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HigherEducation.Models;
using HigherEducation.BusinessLayer;
using HigherEducation.DataAccess;
using Newtonsoft.Json;
using System.Web.Security;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace HigherEducation.Controllers
{
    public class TradeDetailsController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        TradeDetailsContext objTradeDetails = new TradeDetailsContext();
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
        public ActionResult TradeDetails()
        {
            return View();
        }



        [HttpPost]
        public String SaveFinalTradeDetails(String TL)
        {
            DataTable dt = new DataTable();
            try
            {
                if ((Session["UserID"] != null) && (Session["UserID"].ToString() != ""))
                {
                    dt = objTradeDetails.SaveFinalTradeDetails(TL);
                    if (dt.Rows.Count > 0)
                    {
                        return "1";
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
                    return "0";
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] SaveFinalTradeDetails()");
            }
            return "1";
        }

        [HttpPost]
        public String ValidateFinalTradeDetailsViaCSV(HttpPostedFileBase file)
        {
            DataTable dt = new DataTable();
            try
            {
                string TL = ""; // Json To be generate 
                if (file != null && file.ContentLength > 0)
                {
                    using (var reader = new StreamReader(file.InputStream))

                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))//chatgpt
                    {

                        var csvDataList = csv.GetRecords<TradeDetailsCSV>().ToList();

                        // Convert the list to JSON using your preferred JSON serializer
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(csvDataList, Newtonsoft.Json.Formatting.Indented);

                        TL = json;
                        // Use the JSON object as needed
                        //System.Console.WriteLine(json);
                    }
                }
                if ((Session["UserID"] != null) && (Session["UserID"].ToString() != ""))
                {
                    dt = objTradeDetails.ValidateFinalTradeDetailsViaCSV(TL);
                    if (dt.Rows.Count > 0)
                    {
                        return JsonConvert.SerializeObject(new { status = 2, errorlist = dt });
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
                    return "0";
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] SaveFinalTradeDetails()");
                return JsonConvert.SerializeObject(new { status = 500, ImpactedInfo = TruncateLongString(ex.Message, 250) });
            }
            return JsonConvert.SerializeObject(new { status = 1, errorlist = "" });
        }
        [HttpPost]
        public String SaveFinalTradeDetailsViaCSV(HttpPostedFileBase file)
        {
            DataTable dt = new DataTable();
            try
            {
                string TL = ""; // Json To be generate 
                if (file != null && file.ContentLength > 0)
                {
                    using (var reader = new StreamReader(file.InputStream))
                    //// Read the CSV data from the file
                    //while (!reader.EndOfStream)
                    //{
                    //    var line = reader.ReadLine();
                    //    var values = line.Split(',');

                    //    // Process the CSV data as needed
                    //    // values[] contains the values from each column in the CSV line
                    //    // convert json from data
                    //}

                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))//chatgpt
                    {
                        // Configure CsvReader options if needed (e.g., delimiter, header reading, etc.)
                        // csv.Configuration.Delimiter = ",";
                        //csv.Configuration.HasHeaderRecord = true;

                        var csvDataList = csv.GetRecords<TradeDetailsCSV>().ToList();

                        // Convert the list to JSON using your preferred JSON serializer
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(csvDataList, Newtonsoft.Json.Formatting.Indented);

                        TL = json;
                        // Use the JSON object as needed
                        //System.Console.WriteLine(json);
                    }
                }



                if ((Session["UserID"] != null) && (Session["UserID"].ToString() != ""))
                {
                    dt = objTradeDetails.SaveFinalTradeDetailsViaCSV(TL);
                    if (dt.Rows.Count > 0)
                    {
                        //return "1";
                        return JsonConvert.SerializeObject(new { status = 1, ImpactedInfo = dt });
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
                    return "0";
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] SaveFinalTradeDetails()");
                return JsonConvert.SerializeObject(new { status = 500, ImpactedInfo = TruncateLongString(ex.Message, 250) });
            }
            return JsonConvert.SerializeObject(new { status = 1, ImpactedInfo = "" });
        }

        public string TruncateLongString(string str, int maxLength)
        {
            if (string.IsNullOrEmpty(str)) return str;

            return str.Substring(0, Math.Min(str.Length, maxLength));
        }


        [HttpGet]
        public JsonResult GetDistricts()
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                clsCollegeGlance cg = new clsCollegeGlance();
                dt = cg.BindDistrict();
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] GetDistricts()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetITIs(String DistrictID)
        {
            TempData["districtcode"] = DistrictID;
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                dt = objTradeDetails.GetITIs(DistrictID);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] GetITIs()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTrades(String ITI) //collegeid 
        {
            TempData["ITI"] = ITI;

            //string regId = "";
            DataSet ds = new DataSet();
            try
            {
                ds = objTradeDetails.GetTrades(ITI);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] GetTrades()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(ds), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUnitSize(String Trade) //collegeid 
        {
            TempData["Trade"] = Trade;
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                dt = objTradeDetails.GetUnitSize(Trade);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] GetUnitSize()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTypes(String ITI)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = objTradeDetails.getType(ITI);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] GetType()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetShift(String Type)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = objTradeDetails.getShift(Type);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] GetShift()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetUnit(String Type)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = objTradeDetails.getUnit(Type);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] GetUnit()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetScheme()
        {
            DataTable dt = new DataTable();
            try
            {

                dt = objTradeDetails.getScheme();

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails.[HttpGet] GetScheme()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getTradeDetails(String districtcode, String ITI, String Trade)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = objTradeDetails.getTradeDetails(districtcode, ITI, Trade);
                return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails. getFamilyMemberVerifiedDetails()");
            }

            return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public String UpdateTradeDetail(String guid, String Scheme)
        {
            DataSet ds = new DataSet();
            try
            {
                if ((Session["UserID"] != null) && (Session["UserID"].ToString() != ""))
                {
                    ds = objTradeDetails.UpdateTradeDetail(guid, Scheme);
                    var tableJson = Common.Common.ToDictionary(ds.Tables[0]);
                    return "1";
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
                    return "0";
                }


            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails. ActivateDeactivateVerifier()");
            }
            return "1";
        }
        [HttpPost]
        public JsonResult ActivateDeactivateTradeDetail(String guid, String Action, String collegeid, String courseid, String Type, String coursesectionid)
        {

            DataSet ds = new DataSet();
            try
            {
                if ((Session["UserID"] != null) && (Session["UserID"].ToString() != ""))
                {
                    ds = objTradeDetails.ActivateDeactivateTradeDetail(guid, Action, collegeid, courseid, Type, coursesectionid);
                    var tableJson = Common.Common.ToDictionary(ds.Tables[0]);
                    return Json(tableJson, JsonRequestBehavior.AllowGet);
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
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.TradeDetails. ActivateDeactivateVerifier()");
            }
            return Json(JsonConvert.SerializeObject(ds), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public void UnitShiftDownload()
        {
            try
            {
                //Peek is used so that value remains avaiable for next requests
                String districtcode = Convert.ToString(TempData.Peek("districtcode"));
                String ITI = Convert.ToString(TempData.Peek("ITI"));
                String Trade = Convert.ToString(TempData.Peek("Trade"));


                //default value for select district/select ITI/select Trade etc. is taken as zero is clashing with in case of SCVT where shift and unit was having 0 as value and text. 
                districtcode = districtcode.Equals("default") ? String.Empty : districtcode;
                ITI = ITI.Equals("default") ? String.Empty : ITI;
                Trade = Trade.Equals("default") ? String.Empty : Trade;

                DataTable dt = new DataTable();
                dt = objTradeDetails.UnitShiftDownload(districtcode, ITI, Trade);

                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                    "attachment;filename=UnitShiftSavedData.csv");

                Response.Charset = "";

                Response.ContentType = "application/text";

                StringBuilder sb = new StringBuilder();

                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Columns[k].ColumnName + ',');
                }

                //append new line

                sb.Append("\r\n");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                }
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                //SaveError(ex);
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Write("Error failed to load file");
                //return response;
            }

        }

        [HttpGet]
        public void UnitShiftExistingData()
        {
            try
            {

                DataTable dt = new DataTable();
                dt = objTradeDetails.UnitShiftExistingData();

                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                    "attachment;filename=UnitShiftExistingData.csv");

                Response.Charset = "";

                Response.ContentType = "application/text";

                StringBuilder sb = new StringBuilder();

                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Columns[k].ColumnName + ',');
                }

                //append new line

                sb.Append("\r\n");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                    }
                    //append new line
                    sb.Append("\r\n");
                }
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                //SaveError(ex);
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Write("Error failed to load file");
                //return response;
            }

        }

        [HttpGet]
        public void SampleFormatDownload()
        {
            try
            {

                Response.Clear();

                Response.Buffer = true;

                Response.AddHeader("content-disposition",

                    "attachment;filename=SampleFormatToSavedData.csv");

                Response.Charset = "";

                Response.ContentType = "application/text";

                string file = System.IO.File.ReadAllText(Server.MapPath("~/Content/Samples/CollegeCourse.csv"));

                Response.Output.Write(file);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                //SaveError(ex);
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Write("Error failed to load file");
                //return response;
            }

        }
    }
}