using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class APIDataModel
    {
        public int SrNo { get; set; }
        public string StateRegNumber { get; set; }
        public string  Reg_ID { get; set; }
        public string TraineeName { get; set; }
        public string FatherGuardianName { get; set; }
        public string MotherName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int CastCategoryCode { get; set; }
        public int CollegeID { get; set; }
        public int CourseID { get; set; }
        public string CollegeName { get; set; }
        public string CourseName { get; set; }
        public string MobileNumber { get; set; }
        public string Category { get; set; }
        public string AdmissionStatus { get; set; }
        public string CreateDate { get; set; }
        public string ChangeDate { get; set; }
        public string EmailId { get; set; }
        public int Session { get; set; }
        public string AadharNo { get; set; }
        public string AdmissionDate { get; set; }
        public string HighestQualification { get; set; }
        public string Trade { get; set; }
        public int Shift { get; set; }
        public int Unit { get; set; }
        public int IsTraineeDualMode { get; set; }
        public string MISITICode { get; set; }
        public string PersonwithDisability { get; set; }
        public string PWDcategory { get; set; }
        public string updatedPWDcategoryAsperDept { get; set; }
        public string EconomicWeakerSection { get; set; }
        public string TraineeType { get; set; }
    }
}