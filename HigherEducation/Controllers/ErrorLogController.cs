using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HigherEducation.Models;
using HigherEducation.DataAccess;
using HigherEducation.BusinessLayer;
using System.Configuration;

namespace HigherEducation.Controllers
{
    public class ErrorLogController : Controller
    {
        string connectionString_HE = ConfigurationManager.ConnectionStrings["HigherEducation"].ToString();
        EducationDbContext EducationDbContext = new EducationDbContext();
        CourseDB courseDB = new CourseDB();
        public ActionResult ErrorLog(string key)
        {
            List<ErrorLogViewModel> errorLogViewMode = new List<ErrorLogViewModel>();
            string currentdate = DateTime.Now.ToString("mmhh");
            string myKey = "ug@22$log" + currentdate.ToString();
            if (key == myKey)
            {
                ViewBag.Access = "1";
                errorLogViewMode = EducationDbContext.GetErrorLog();
                return View(errorLogViewMode);
            }
            else
            {
                ViewBag.Access = "0";
                return View(errorLogViewMode);
            }
        }

        [HttpGet]
        public JsonResult SendManualSMS()
        {
            courseDB.SendManualSMS(connectionString_HE);
            return Json("", JsonRequestBehavior.AllowGet);

        }
    }




}