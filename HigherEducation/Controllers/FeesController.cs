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
    public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public MyHttpControllerHandler(RouteData routeData): base(routeData)
        {

        }
    }
    public class MyHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MyHttpControllerHandler(requestContext.RouteData);
        }
    }
       
    [EnableCors("*", "*", "*")]
    public class FeesController : ApiController
    {
        FeesDB FeesDB = new FeesDB();
        Log objlog = new Log();
        string connectionString_HE = ConfigurationManager.ConnectionStrings["HigherEducation"].ToString();
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetFeeHeadMaster()
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFeeHeadMaster(connectionString_HE));
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
        public HttpResponseMessage SaveFeeHeadMaster(FeeHeadM SaveFeeHead)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.SaveFeeHeadMaster(connectionString_HE, SaveFeeHead));
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
        public HttpResponseMessage DeleteFeeHead(string FeeHeadID)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.DeleteFeeHead(connectionString_HE, FeeHeadID));
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
        public HttpResponseMessage GetFeeHead()
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFeeHead(connectionString_HE));
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
        public HttpResponseMessage GetFeeSubHeadMaster(string FeeHeadID)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFeeSubHeadMaster(connectionString_HE, FeeHeadID));
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
        public HttpResponseMessage SaveFeeSubHeadMaster(FeeSubHeadM SaveFeeSubHead)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.SaveFeeSubHeadMaster(connectionString_HE, SaveFeeSubHead));
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
        public HttpResponseMessage DeleteFeeSubHead(string FeeSubHeadID)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.DeleteFeeSubHead(connectionString_HE, FeeSubHeadID));
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
        public HttpResponseMessage GetCourse(string Collegeid,string Sessionid)
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
        public HttpResponseMessage GetSection(string Courseid, string Sessionid, string collageIdystate)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetSection(connectionString_HE, Courseid, Sessionid, collageIdystate));
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
        public HttpResponseMessage GetFeeDetail(string CollegeID,string SessionID,string CourseID,string SectionID)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFeeDetail(connectionString_HE,CollegeID,SessionID,CourseID,SectionID));
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
        public HttpResponseMessage GetFeeDetailData(string Collegeid,string Sessionid)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFeeDetailData(connectionString_HE,Collegeid, Sessionid));
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
        public HttpResponseMessage GetUniversityID(string Collegeid)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetUniversityID(connectionString_HE, Collegeid));
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
        public HttpResponseMessage SaveFeeHead(FeeHead SaveFeeHead)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.SaveFeeHead(connectionString_HE, SaveFeeHead));
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
        public HttpResponseMessage GetFeeHeadData(string Collegeid)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetFeeHeadData(connectionString_HE, Collegeid));
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
        public HttpResponseMessage SaveFreezeData(FreezeFeeDetail FeeData)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.SaveFreezeData(connectionString_HE,FeeData));
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
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCollegeMaster1(string RegNO)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, FeesDB.GetCollegeMaster(connectionString_HE, RegNO));
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