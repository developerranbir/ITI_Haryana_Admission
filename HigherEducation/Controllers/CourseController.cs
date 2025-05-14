using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
using HigherEducation.BusinessLayer;
using HigherEducation.Models;
using NLog;
using System.Collections.Generic;

namespace HigherEducation.Controllers
{
    public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public MyHttpControllerHandler(RouteData routeData) : base(routeData)
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
    public class CourseController : ApiController
    {
        CourseDB courseDB = new CourseDB();
        Log objlog = new Log();


        string connectionString_HE = ConfigurationManager.ConnectionStrings["HigherEducation"].ToString();

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCourse()
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCourse(connectionString_HE));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCoursePG()
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCoursePG(connectionString_HE));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }



        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCombinationSubjects()
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCombinationSubjects(connectionString_HE));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
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
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetSession(connectionString_HE));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCombinationGroup(string Courseid, string Sectionid, string Sessionid)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCombinationGroup(connectionString_HE, Courseid, Sectionid, Sessionid));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetSection(string Courseid)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetSection(connectionString_HE, Courseid));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCourseType()
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCourseType(connectionString_HE));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetCourseSubjectComb(DeleteCourse deleteCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCourseSubjectComb(connectionString_HE, deleteCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCourseTypePG()
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCourseTypePG(connectionString_HE));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveCourse(SaveCourse saveCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.SaveCourse(connectionString_HE, saveCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveCoursePG(SaveCourse saveCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.SaveCoursePG(connectionString_HE, saveCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage UpdateCourse(SaveCourse saveCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.UpdateCourse(connectionString_HE, saveCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage UpdateCoursePG(SaveCourse saveCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.UpdateCoursePG(connectionString_HE, saveCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteCourse(DeleteCourse deleteCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.DeleteCourse(connectionString_HE, deleteCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage DeleteCoursePG(DeleteCourse deleteCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.DeleteCoursePG(connectionString_HE, deleteCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage ActiveCourse(DeleteCourse deleteCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.ActiveCourse(connectionString_HE, deleteCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage ActiveCoursePG(DeleteCourse deleteCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.ActiveCoursePG(connectionString_HE, deleteCourse));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCourseDetail(string SessionId)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCourseDetail(connectionString_HE, SessionId));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCourseDetailPG(string SessionId)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetCourseDetailPG(connectionString_HE, SessionId));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage FreezeCourse(FreezeCourse freezeCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.FreezeCourse(connectionString_HE, freezeCourse));
                }
                else

                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage FreezeCourseUG(FreezeCourse freezeCourse)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.FreezeCourseUG(connectionString_HE, freezeCourse));
                }
                else

                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage SendManualSMS()
        {
            try
            {
                EncryptionDecryption enc = new EncryptionDecryption();
                //var a = enc.Encrypt("dhe@12");
                //var b = enc.Decrypt(key.Replace(" ", "+"));
                //if (b == "dhe@12")
                //{
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.SendManualSMS(connectionString_HE));
                //}
                //else
                //{
                //    return Request.CreateResponse(HttpStatusCode.OK, "You are not authorized to send detail to candidate");
                //}
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.OK, "You are not authorized to send detail to candidate");

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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetFreezeUnFreezeData(string SessionId)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"].ToString() != "")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.GetFreezeUnFreezeData(connectionString_HE, SessionId));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
                }
            }
            catch (Exception ex)
            {
                SaveError(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveFreezeUnFreezeData([FromBody]List<FreezeUnFreezeData> FreezeData)
        {
            try
            {
                if (HttpContext.Current.Session["LoginName"] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courseDB.SaveFreezeUnFreezeData(connectionString_HE, FreezeData));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new { Error = "Un-Authorized Access !!", Login = "https://itiharyanaadmissions.nic.in/dhe/frmlogin.aspx" });
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