using HigherEducation.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HigherEducation.Controllers
{
    public class CourseCombinationController : Controller
    {
        // GET: CourseCombination
        CourseCombinationDB courseCombination = new CourseCombinationDB();
        CourseDB courseDB = new CourseDB();

        public ActionResult coursecombination()
        {
            string UserType = "2";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);
            var obj = courseCombination.GetSessionView();
            ViewBag.Sessionobj = obj;
            eSessionMgmt.SetCookie();
            return View();
        }
        [HttpGet]
        public JsonResult GetCourseName(string Sessionid)
        {
            DataSet Course;
            try
            {
                if (Session["UserId"].ToString() != "")
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
                if (Session["UserId"].ToString() != "")
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
        public JsonResult GetCombinationSubjects(string Courseid, string Sessionid, string Sectionid, string CombinationGroup)
        {
            DataSet Course;
            try
            {
                if (Session["UserId"].ToString() != "")
                {
                    Course = courseCombination.GetCombinationSubjects(Courseid, Sessionid, Sectionid, CombinationGroup);
                    if (Course.Tables.Count > 0)
                    {
                        var tableJson = GetData(Course.Tables[1]);
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
        public JsonResult GetCombinationReport(string Sessionid, string Sectionid,string CourseId)
        {
            DataSet Course;
            try
            {
                if (Session["UserId"].ToString() != "")
                {
                    Course = courseCombination.GetCombinationReport(Sessionid, Sectionid, CourseId);
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
        //[HttpGet]
        //public JsonResult GetCombinationGroup()
        //{
        //    DataSet Course;
        //    try
        //    {
        //        if (Session["UserId"].ToString() != "")
        //        {
        //            Course = courseCombination.GetCombinationGroup();
        //            if (Course.Tables.Count > 0)
        //            {
        //                // var tableJson = GetData(Course.Tables[1]);

        //                return Json(Course.Tables, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(HttpStatusCode.Unauthorized, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        courseDB.SaveError(ex);
        //        return Json(HttpStatusCode.InternalServerError, "exception", JsonRequestBehavior.AllowGet);

        //    }
        //}


        [HttpPost]
        public JsonResult SaveSubjectCombination(List<SubjectCombination> jdata)
        {
            DataSet Course = new DataSet();
            try
            {
                if (Session["UserId"].ToString() != "")
                {

                    Course = courseCombination.SaveSubjectCombination(jdata);
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
        public JsonResult DeactivateCourse(string coursecombinationid)
        {
            DataSet Course = new DataSet();
            try
            {
                if (Session["UserId"].ToString() != "")
                {

                    Course = courseCombination.DeactivateCourse(coursecombinationid);
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
        public JsonResult GetCourseType(string collegemasterid)
        {
            DataSet Course;
            try
            {
                if (Session["UserId"].ToString() != "")
                {
                    Course = courseCombination.GetCourseType();
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
        public JsonResult GetSubjects()
        {
            DataSet Course;
            try
            {
                if (Session["UserId"].ToString() != "")
                {
                    Course = courseCombination.GetSubjects();
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
        public JsonResult UpdateSubjectCombinationfees(string SubjectCombinationId, string TotalFees, string NoofSeats)
        {
            DataSet Course = new DataSet();
            try
            {
                string MsgText = "";
                bool ret = true;
                Int32 resultcount = 0;

                if (SubjectCombinationId == "" || SubjectCombinationId == null)//When Mobile empty
                {
                    resultcount += 1;
                    MsgText += "Something went wrong with Subject Combination id" + Environment.NewLine;
                }
                if (TotalFees == "" || TotalFees == null)//When Mobile empty
                {
                    resultcount += 1;
                    MsgText += "Fees required" + Environment.NewLine;
                }
                if (NoofSeats == "" || NoofSeats == null)//When Mobile empty
                {
                    resultcount += 1;
                    MsgText += "Seats required" + Environment.NewLine;
                }
                if (resultcount > 0)
                {
                    return Json(MsgText, JsonRequestBehavior.AllowGet);
                }

                if (Session["UserId"].ToString() != "")
                {

                    Course = courseCombination.UpdateSubjectCombinationfees(SubjectCombinationId, TotalFees, NoofSeats);
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
        public JsonResult FreezeCombination(string SessionId, string CourseId, string SectionId)
        {
            DataSet Course = new DataSet();
            try
            {
                if (Session["UserId"].ToString() != "")
                {

                    Course = courseCombination.FreezeCombination(SessionId, CourseId, SectionId);
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

        [HttpPost]
        public JsonResult SaveSubjectCombinationUG(List<SubjectCombinationUG> jdata)
        {
            DataSet Course = new DataSet();
            try
            {
                if (Session["UserId"].ToString() != "")
                {

                    Course = courseCombination.SaveSubjectCombinationUG(jdata);
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
        public JsonResult ActivateCourse(string coursecombinationid)
        {
            DataSet Course = new DataSet();
            try
            {
                if (Session["UserId"].ToString() != "")
                {

                    Course = courseCombination.ActivateCourse(coursecombinationid);
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
        public JsonResult GetIsCombination(string Courseid)
        {
            DataSet Course;
            try
            {
                if (Session["UserId"].ToString() != "")
                {
                    Course = courseCombination.GetIsCombination(Courseid);
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

    }

    public class SubjectCombination
    {
        public string Subject { get; set; }
        public string SubjectId { get; set; }
        public string Sessionid { get; set; }
        public string Courseid { get; set; }
        public string Sectionid { get; set; }
        public string Combinationid { get; set; }
        public string CourseType { get; set; }


    }
    public class SubjectCombinationUG
    {
        public string[] Subject { get; set; }
        public string Sessionid { get; set; }
        public string Courseid { get; set; }
        public string Sectionid { get; set; }
        public string CourseType { get; set; }
        public string noofseats { get; set; }
        public string fees { get; set; }
    }
}