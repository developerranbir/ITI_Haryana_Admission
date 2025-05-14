using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HigherEducation.DataAccess;
using HigherEducation.BusinessLayer;
using NLog;

namespace HigherEducation.Controllers
{
    public class GrantRevokeController : Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        // GET: GrantRevoke
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GrantRevokeModules()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetRoles()
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                dt = obj.BindRole();
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpGet] BindRole()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetUsers(String RoleID)
        {
            TempData["roleid"] = RoleID;
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                dt = obj.BindUsers(RoleID);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpGet] GetUsers()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult BindUserBasedModules(String UserID)
        {
            //TempData["userid"] = UserID;
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                dt = obj.BindUserBasedModules(UserID);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpGet] BindUserBasedModules()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BindRoleBasedModules(String UserTypeID, String CollegeTypeID)
        {

            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                dt = obj.BindRoleBasedModules(UserTypeID, CollegeTypeID);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpGet] BindRoleBasedModules()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGrantedUserModules(String UserID)
        {
            //TempData["userid"] = UserID;
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                dt = obj.GetGrantedUserModules(UserID);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpGet] GetGrantedUserModules()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetGrantedRoleModules(String UserTypeID, string CollegeTypeID)
        {
            //string regId = "";
            DataTable dt = new DataTable();
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                dt = obj.GetGrantedRoleModules(UserTypeID, CollegeTypeID);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpGet] GetGrantedRoleModules()");
            }
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(dt), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public String GrantUserModules(String UserID, string ModuleID)
        {   
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                int result = obj.UpdateGrantedUserModules(UserID, ModuleID);
                return (result > 0) ? "1" : "0";
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpPost] GrantUserModules()");
                return "0";
            }
        }
        [HttpPost]
        public string GrantRoleModules(String UserTypeID, String CollegeTypeID, string ModuleID)
        {
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                int result = obj.UpdateGrantedRoleModules(UserTypeID, CollegeTypeID, ModuleID);
                return (result > 0) ? "1" : "0";
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpPost] GrantRoleModules()");
                return "0";
            }
        }

        [HttpPost]
        public String RevokeUserModules(String UserID, string ModuleID)
        {
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                int result = obj.UpdateRevokedUserModules(UserID, ModuleID);
                return (result > 0) ? "1" : "0";
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpGet] RevokeUserModules()");
                return "0";
            }
        }
        [HttpPost]
        public string RevokeRoleModules(String UserTypeID, String CollegeTypeID, string ModuleID)
        {
            try
            {
                GrantRevokeContext obj = new GrantRevokeContext();
                int result = obj.UpdateRevokedRoleModules(UserTypeID, CollegeTypeID, ModuleID);
                return (result > 0) ? "1" : "0";
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.GrantRevoke.[HttpPost] RevokeRoleModules()");
                return "0";
            }
        }
    }
}