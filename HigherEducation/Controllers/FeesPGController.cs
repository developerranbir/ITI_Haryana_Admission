using HigherEducation.BusinessLayer;
using HigherEducation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using System.Web.Http.WebHost;
using System.Web.SessionState;

namespace HigherEducation
{ 
    [EnableCors("*", "*", "*")]
    public class FeesPGController : ApiController
    {
        FeesPGDB FeesDB = new FeesPGDB();
        Log objlog = new Log();
        string connectionString_HE = ConfigurationManager.ConnectionStrings["HigherEducation"].ToString();
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCollege()
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetCollege(connectionString_HE));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCourse(string Collegeid, string Sessionid)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetCourse(connectionString_HE, Collegeid, Sessionid));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetSection(string Courseid, string Sessionid)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetSection(connectionString_HE, Courseid, Sessionid));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetSession()
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetSession(connectionString_HE));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetFeeDetail(string CollegeID, string SessionID, string CourseID, string SectionID)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFeeDetail(connectionString_HE, CollegeID, SessionID, CourseID, SectionID));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveFeeDetail([FromBody]List<FeeDetail> FeeDetail)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.SaveFeeDetail(connectionString_HE, FeeDetail));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetFeeDetailData(string Collegeid, string Sessionid)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFeeDetailData(connectionString_HE, Collegeid, Sessionid));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.NonAction]
        public void SaveError(Exception ex, string ExtraMessage = "")
        {
            try
            {
                objlog.LogWrite(ExtraMessage + "Method Name - " + ex.StackTrace.ToString() + Environment.NewLine + "Target Website - " + ex.TargetSite.ToString() + Environment.NewLine + " Error - " + ex.Message.ToString());
            }
            catch
            {

            }
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveFreezeData(FreezeFeeDetail FeeData)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.SaveFreezeData(connectionString_HE, FeeData));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetFreezeData(string CollegeID, string SessionID, string CourseID, string SectionID)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFreezeData(connectionString_HE, CollegeID, SessionID, CourseID, SectionID));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "http://localhost:57994/" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
