using HigherEducation.DataAccess;
using HigherEducation.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HigherEducation.BusinessLayer;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;

namespace Higher_Education.Controllers
{
    public class FileUploadController : Controller
    {
        static string Edisha_Prod = ConfigurationManager.AppSettings["Edisha_Prod"];
        EducationDbContext EducationContext = new EducationDbContext();
        Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult FileUpload()
        {
            string regId = "";
            string NewTab = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
            if (NewTab == "" | NewTab == null)
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }
            if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }
            else
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }
            FileViewModel fileViewModel = new FileViewModel();


            fileViewModel = EducationContext.GetCandidateDataByRegId(regId);
            return View(fileViewModel);
        }
        [HttpPost]
        public ActionResult FileUpload(FileViewModel fileViewModel)
        {

            int ii = 1;
            string regId = "";
            if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }
            else
            {
                return RedirectToAction("LogOut", "Account", new { area = "" });
            }

            //for (int i = 0; i < fileViewModel.documentLists.Count; i++)
            //{
            //    if (fileViewModel.documentLists[i].IsDocVerify == "Y")
            //    {
            //        this.ModelState.Remove("documentLists[" + i + "].files");
            //    }
            //    else
            //    {
            //       // this.ModelState.AddModelError("documentLists[" + i + "].files", "required");

            //    }
            //}

            try
            {

                Document objBL = new Document();
                if (fileViewModel.IsDocExists > 0)
                {
                    for (int i = 0; i < fileViewModel.documentLists.Count; i++)
                    {
                        if (fileViewModel.documentLists[i].IsDocVerify == "Y" || fileViewModel.documentLists[i].IsDocVerify == "N")
                        {
                            this.ModelState.Remove("documentLists[" + i + "].files");
                        }
                    }
                }
                if (ModelState.IsValid)
                {
                    foreach (var Objfile in fileViewModel.documentLists)
                    {
                        if (Objfile.files != null)
                        {
                            byte[] FileDet = null;
                            string extensionName = Path.GetExtension(Objfile.files.FileName);

                            AttachmentType aa = new AttachmentType();
                            Stream str = Objfile.files.InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            FileDet = Br.ReadBytes((Int32)str.Length);

                            if (extensionName == ".pdf")
                            {
                            }
                            else
                            {
                                decimal sizecheck = Math.Round(((decimal)FileDet.Length / (decimal)1024), 2);

                                if (sizecheck > 300 && Objfile.DocumentId != 8 && Objfile.DocumentId != 9)
                                {
                                    MemoryStream ms = new MemoryStream(FileDet);
                                    System.Drawing.Image cellValue = System.Drawing.Image.FromStream(ms);
                                    cellValue = Resize(cellValue, 1600, 1200, true);
                                    FileDet = imageToByteArray(cellValue);
                                }
                            }
                            string base64ImageRepresentation = Convert.ToBase64String(FileDet);
                            objBL.Docs = base64ImageRepresentation;
                            string substring = "";
                            string substring2 = "";
                            string substring3 = "";

                            if (Objfile.DocumentId == 8 || Objfile.DocumentId == 9)
                            {
                                substring2 = "/9j/4";
                                substring3 = "iVBOR";
                                substring = "iVBOR";
                            }
                            else
                            {
                                substring = "JVBER";
                                substring2 = "/9j/4";
                                substring3 = "iVBOR";
                            }

                            if (base64ImageRepresentation.Contains(substring) || base64ImageRepresentation.Contains(substring2) || base64ImageRepresentation.Contains(substring3))
                            {

                            }
                            else
                            {
                                this.ModelState.AddModelError("documentLists[" + ii + "].files", "Invalid file format");
                            }

                            decimal size = Math.Round(((decimal)FileDet.Length / (decimal)1024), 2);
                            if (ModelState.IsValid)
                            {
                                if ((size < 400) || (size > 10 && size < 100 && (Objfile.DocumentId == 8 || Objfile.DocumentId == 9)))
                                {
                                    objBL.DocsName = Path.GetFileNameWithoutExtension(Objfile.files.FileName);
                                    objBL.Docid = Convert.ToString(Objfile.DocumentId);
                                    objBL.DocNo = Objfile.DocumentNo;
                                    objBL.Reg_Id = regId;
                                    objBL.Isverify = "N";

                                    int i = EducationContext.SaveDocument(objBL);
                                    if (i == 0)
                                    {
                                        this.ModelState.AddModelError("documentLists[" + ii + "].files", "Something went wrong");
                                        fileViewModel = EducationContext.GetCandidateDataByRegId(regId);
                                        return View(fileViewModel);
                                    }
                                }
                                else
                                {
                                    this.ModelState.AddModelError("documentLists[" + ii + "].files", "File size is greater.");
                                    fileViewModel = EducationContext.GetCandidateDataByRegId(regId);
                                    return View(fileViewModel);
                                }
                            }
                            else
                            {
                                fileViewModel = EducationContext.GetCandidateDataByRegId(regId);
                                return View(fileViewModel);
                            }

                        }
                    }
                    ++ii;
                }
                else
                {
                    fileViewModel = EducationContext.GetCandidateDataByRegId(regId);
                    return View(fileViewModel);
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FileUploadController.[HttpPost] FileUpload()");
                return RedirectToAction("FileUpload", "FileUpload");

            }
            TempData["SuccessFileUpload"] = "1";
            Session["MaxPage"] = "4";
           // return RedirectToAction("RegistrationFee", "RegistrationPayment");
            return RedirectToAction("ChoiceofCourses", "Account");

        }

        [HttpPost]
        public JsonResult GetImage(string DocId)
        {
            string regId = "";
            if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
            {
                regId = Session["RegId"].ToString();
            }

            try
            {
                DataTable result = new DataTable();
                result = EducationContext.GetImage(DocId, regId);
                string JSONString = JsonConvert.SerializeObject(result);
                return Json(JSONString, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FileUploadController.[HttpPost] GetImage()");
                return Json("999", JsonRequestBehavior.AllowGet);
            }


        }

        [HttpPost]
        public JsonResult GetDocumentStatus(DocumentStatus item)
        {
            try
            {
                if (Session["RegId"] != null && (Session["RegId"].ToString() != ""))
                {

                }
                else
                {
                    return Json("444", JsonRequestBehavior.AllowGet);
                }
                DocumentStatus objdocument = new DocumentStatus();

                item.RegistrationId = Session["RegId"].ToString();
                string PaymentTransactionId = "0";
                string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");

                string onlynumber_regId = Regex.Replace(item.RegistrationId == null ? "0" : item.RegistrationId, "[^0-9]", "");

                //Generate Transaction Number
                PaymentTransactionId = onlynumber_regId + currentDateTime;
                if (PaymentTransactionId.Length > 30)
                {
                    PaymentTransactionId = PaymentTransactionId.Substring(PaymentTransactionId.Length - 30);
                }
                string result = "";
                string edishaidResult = "";
                objdocument.Name = item.Name;
                objdocument.ServiceCode = item.ServiceCode;
                objdocument.TransactionID = item.TransactionID;
                objdocument.Caste = item.Caste;
                objdocument.DocId = item.DocId;

                objdocument.RegistrationId = item.RegistrationId;
                objdocument.CollegeId = item.CollegeId;
                objdocument.CombinationId = item.CombinationId;
                result = CallEdishaAPI_ForSave(objdocument);

                dynamic innerdata = JsonConvert.DeserializeObject(result);
                string StatusID = (string)innerdata["STATUS_ID"];  // 000
                string STATUS_MSG = (string)innerdata["STATUS_MSG"];  //PartialyVerified
                string RESPONSE = (string)innerdata["RESPONSE"];  //PartialyVerified

                if (StatusID == "000" && objdocument.ServiceCode == "06")
                {
                    if (STATUS_MSG == "PartialyVerified" || RESPONSE == "PartialyVerified")
                    {
                        objdocument.VerifyStatus = "P";
                    }
                    else
                    {
                        objdocument.VerifyStatus = "A";
                    }
                    edishaidResult = EducationContext.SaveFeeofSCCandidate(objdocument.RegistrationId, objdocument.CollegeId, objdocument.CombinationId, objdocument.TransactionID, objdocument.VerifyStatus, PaymentTransactionId);
                    if (edishaidResult == "2")
                    {
                        StatusID = "001";
                    }
                }
                if (StatusID == "000" && objdocument.ServiceCode != "06")
                {
                    EducationContext.SaveVerifiedDocument(objdocument.RegistrationId, objdocument.DocId, objdocument.TransactionID);
                }
                return Json(StatusID, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.FileUploadController.[HttpPost] GetDocumentStatus()");
                return Json("999", JsonRequestBehavior.AllowGet);
            }

        }
        public static string CallEdishaAPI_ForSave(DocumentStatus document)
        {
            string result = "";
        RerunTheCode:
            try
            {
                HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create(Edisha_Prod));
                req.Method = "POST";
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                req.ContentType = "application/json";
                req.ProtocolVersion = HttpVersion.Version11;
                var data = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(document));
                req.ContentLength = data.Length;
                using (var stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var httpResponse = (HttpWebResponse)req.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }

            catch (WebException wex)
            {
                if (wex.Message == "The underlying connection was closed: An unexpected error occurred on a send.")
                {
                    goto RerunTheCode;
                }


            }
            catch (Exception)
            {

            }
            return result;
        }

        public static System.Drawing.Image Resize(System.Drawing.Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new System.Drawing.Bitmap(newWidth, newHeight);

            using (var graphic = System.Drawing.Graphics.FromImage(res))
            {
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;
                graphic.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return res;
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}

public class DocumentStatus
{
    public string TransactionID { get; set; }
    public string ServiceCode { get; set; }
    public string Name { get; set; }
    public string Caste { get; set; }
    public string RegistrationId { get; set; }
    public string CollegeId { get; set; }
    public string CombinationId { get; set; }
    public string VerifyStatus { get; set; }
    public string DocId { get; set; }
}
