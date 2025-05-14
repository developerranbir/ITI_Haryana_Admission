using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class RankCard
    {
        public string Reg_id { get; set; }
        public string CandidateName { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ApplicantDOB { get; set; }
        public string Photo { get; set; }
        public string CounsellingRound { get; set; }
        public string DateFromTo { get; set; }
        public string Category { get; set; }
        public string HighestQualification { get; set; }
        
        public List<AcademicDetail> academicDetails { get; set; }
        public List<MarksandWeightage> marksandWeightages { get; set; }
        public string GramPanchayatweightage { get; set; }

    }
    public class AcademicDetail
    {
        public string ExamPassed { get; set; }
    }
    public class MarksandWeightage
    {
        public string Sr { get; set; }
        public string Qualification { get; set; }
        public string MarksWeightage { get; set; }
        public string EducationWeightage { get; set; }
        public string WidowWeightage { get; set; }
        public string TotalMarks { get; set; }
        public string PanchayatWeightage { get; set; }

    }
}