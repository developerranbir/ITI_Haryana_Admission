using HigherEducation.BusinessLayer;
using HigherEducation.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HigherEducation.Controllers
{
    public class SubjectConfigurationController : Controller
    {
        // GET: SubjectConfiguration
        CourseCombinationDB courseCombination = new CourseCombinationDB();
        CourseDB courseDB = new CourseDB();
        public ActionResult SubjectConfiguration()
        {
            var obj = courseCombination.GetSessionView();
            ViewBag.Sessionobj = obj;
            return View();
        }
        [HttpGet]
        public JsonResult GetCourseName(string Sessionid)
        {
            DataSet Course;
            try
            {
                if (Session["LoginName"] == null)
                {
                    Course = courseCombination.GetCollegeCourse(Sessionid);
                    if (Course.Tables.Count > 0)
                    {
                        var tableJson = GetData(Course.Tables[0]);
                        return Json(tableJson, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                courseDB.SaveError(ex);
                return Json(HttpStatusCode.InternalServerError, "exception", JsonRequestBehavior.AllowGet);

            }
        }

        [HttpGet]
        public JsonResult GetSection(string Courseid, string Sessionid)
        {
            DataSet Course;
            try
            {
                if (Session["LoginName"] == null)
                {
                    Course = courseCombination.GetSection(Courseid, Sessionid);
                    if (Course.Tables.Count > 0)
                    {
                        var tableJson = GetData(Course.Tables[0]);
                        return Json(tableJson, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                courseDB.SaveError(ex);
                return Json(HttpStatusCode.InternalServerError, "exception", JsonRequestBehavior.AllowGet);

            }
        }



        [HttpGet]
        public JsonResult GetSubjectFeeDetails(string Courseid, string Sessionid, string Sectionid)
        {
            DataSet Course;
            try
            {
                if (Session["LoginName"] == null)
                {
                    Course = courseCombination.GetSubjectFeeDetails(Courseid, Sessionid, Sectionid);
                    if (Course.Tables.Count > 0)
                    {
                        var tableJson = GetData(Course.Tables[0]);
                        return Json(tableJson, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                courseDB.SaveError(ex);
                return Json(HttpStatusCode.InternalServerError, "exception", JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public JsonResult SaveSubjectFee(List<SubjectFee> jdata)
        {
            DataSet Course = new DataSet();
            try
            {
                if (Session["LoginName"] == null)
                {

                    Course = courseCombination.SaveSubjectFee(jdata);
                    if (Course.Tables.Count > 0)
                    {
                        var tableJson = GetData(Course.Tables[0]);
                        return Json(tableJson, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                courseDB.SaveError(ex);
                return Json(HttpStatusCode.InternalServerError, "exception", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult GetSeats(string Courseid, string Sessionid, string SectionId)
        {
            DataSet Course;
            try
            {
                if (Session["LoginName"] == null)
                {
                    Course = courseCombination.GetSeats(Courseid, Sessionid, SectionId);
                    if (Course.Tables.Count > 0)
                    {
                        var tableJson = GetData(Course.Tables[0]);
                        return Json(tableJson, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                courseDB.SaveError(ex);
                return Json(HttpStatusCode.InternalServerError, "exception", JsonRequestBehavior.AllowGet);

            }
        }

        public static List<Dictionary<string, string>> GetData(DataTable dt)   // convert datatable to Dictionary
        {
            return dt.AsEnumerable().Select(
                row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column].ToString()
                )).ToList();
        }
    }

    public class SubjectFee
    {
        public string Subject_id { get; set; }
        public string Subject_name { get; set; }
        public string Fee { get; set; }
        public string Seats { get; set; }
        public string IsOptional { get; set; }
        public string Sessionid { get; set; }
        public string Courseid { get; set; }
        public string Sectionid { get; set; }
    }

}