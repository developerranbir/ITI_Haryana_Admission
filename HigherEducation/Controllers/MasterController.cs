using HigherEducation.BusinessLayer;
using HigherEducation.DataAccess;
using HigherEducation.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;

namespace HigherEducation.Controllers
{
    public class MasterController : Controller
    {

        MasterContext MasterContext = new MasterContext();

        // GET: Master
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AddITI() { 
            return View();
        }

        public ActionResult viewCandiateFullInfo()
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

        [HttpPost]
        public JsonResult getCandidateMaterData(string regID)
        {
            if (Session["collegeId"] != null && Session["UserType"].ToString() == "1")
            {
                DataSet dt = MasterContext.getCandidateMaterData(regID);
                return Json(JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
            }
            else
            {
                Response.Redirect("~/dhe/frmlogin.aspx");
                return Json(new { });
            }

        }

    }
}