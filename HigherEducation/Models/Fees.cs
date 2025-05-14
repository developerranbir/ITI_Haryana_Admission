using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class Fees
    {
    }
    public class FeeHeadMaster
    {
        public string SrNo { get; set; }
        public string FeeHeadID { get; set; }
        public string FeeHeadName { get; set; }
        public string IsActive { get; set; }
    }
    public class FeeSubHeadMaster
    {
        public string SrNo { get; set; }
        public string FeeSubHeadID { get; set; }
        public string FeeSubHeadName { get; set; }
        public string FeeHeadID { get; set; }
        public string FeeHeadName { get; set; }
        public string IsWavier { get; set; }
        public string IsActive { get; set; }
    }
    public class FeeHeadM
    {
        public string FeeHeadName { get; set; }
        public string IsActive { get; set; }
        public string FeeHeadID { get; set; }
    }
    public class FeeSubHeadM
    {
        public string FeeHeadID { get; set; }
        public string FeeSubHeadName { get; set; }
        public string IsWavier { get; set; }
        public string IsActive { get; set; }
        public string FeeSubHeadID { get; set; }
    }
    public class FillDropdowns
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public class FeeDetailG
    {
        public string FeeDetailID { get; set; }
        public string FeeSubHeadID { get; set; }
        public string FeeHeadName { get; set; }
        public string FeeSubHeadName { get; set; }
        public string Iswaiver { get; set; }
        public string Yearly { get; set; }
        //public string Semester1 { get; set; }
        //public string Semester2 { get; set; }
        //public string FeeType { get; set; }
    }
    public class FeeDetailData
    {
        public string SrNo { get; set; }
        public string SessionName { get; set; }
        public string SectionName { get; set; }
        public string YrAmnt { get; set; }
        //public string Sem1Amnt { get; set; }
        //public string Sem2Amnt { get; set; }
    }
    public class FeeHead
    {
        public string CollegeID { get; set; }
        public string FeeHeadID { get; set; }
        public string FeeHeadName { get; set; }
        public string SessionID { get; set; }
        public string Waivable { get; set; }
        public string FeeSubHeadID { get; set; }
    }
    public class FeeDetail
    {
        public string FeeDetailID { get; set; }
        public string CollegeID { get; set; }
        public string CourseID { get; set; }
        public string SectionID { get; set; }
        public string SessionID { get; set; }     
        //public string FeeType { get; set; }
        public string FeeSubHeadID { get; set; }
        public string YearAmount { get; set; }
        //public string Semester1Amount { get; set; }
        //public string Semester2Amount { get; set; }
    }
    public class FeeHeadData
    {
        public string FeeSubHeadID { get; set; }
        public string FeeHeadID { get; set; }
        public string FeeHeadName { get; set; }
        public string FeeSubHeadName { get; set; }
        public string CollegeID { get; set; }
        public string FeeSessionID { get; set; }
        public string FeeSessionName { get; set; }
        public string IsWavier { get; set; }
    }
    public class FreezeFeeDetail
    {
        public string CollegeID { get; set; }
        public string SessionID { get; set; }
        public string CourseID { get; set; }
        public string SectionID { get; set; }
    }
}