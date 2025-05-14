using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class Course
    {
    }

    public class FillDropdown
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class SaveCourse
    {
        public string Session { get; set; }
        [Required(ErrorMessage = "Session Rquired")]
        public string Course { get; set; }
        public string Section { get; set; }
        public string TotalSeat { get; set; }
        public string CourseType { get; set; }
        public string Collegecourseid { get; set; }
        public string SportSeat { get; set; }
        
    }
    public class DeleteCourse
    {
        public string Collegecourseid { get; set; }
    }
    public class FreezeCourse
    {
        public string Sessionid { get; set; }
    }
    public class FreezeUnFreezeData
    {
        public string SessionId { get; set; }
        [Required(ErrorMessage = "Session Rquired")]
        public string CollegeId { get; set; }
        [Required(ErrorMessage = "Session Rquired")]
        public string LastStatus { get; set; }
        public string NewStatus { get; set; }
    }
}