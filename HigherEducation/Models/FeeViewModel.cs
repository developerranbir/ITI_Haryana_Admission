using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class FeeViewModel
    {
        public List<CandidateAdmissionStatus> candidateAdmissionStatuses { get; set; }
        public List<CandidateAllocatedCollege> CandidateAllocatedCollege { get; set; }
        public List<CandidateSeatAllocated> candidateSeatAllocated { get; set; }

        public List<CandidatePasswordStatus> candidatePasswordStatus { get; set; }
        public List<AdmissionSession> candidateAdmissionSession { get; set; }

    }
    public class CandidateAllocatedCollege
    {
        public string A_Collegeid { get; set; }
        public string A_CollegeName { get; set; }
        public string A_Section { get; set; }
        public string A_RegistrationId { get; set; }
        public string A_Course_Preference { get; set; }

        // for Quarter Number
        public string A_QtrNo { get; set; }

    }
    public class CandidateSeatAllocated
    {
        public string Al_Collegeid { get; set; }
        public string Al_CollegeName { get; set; }
        public string Al_Section { get; set; }
        public string Al_RegistrationId { get; set; }
        public string Al_Course_Preference { get; set; }
        public string Al_Counselling { get; set; }
        public string Al_VerificationStatus { get; set; }
        public string A1_meritid { get; set; }





    }
    public class CandidateAdmissionStatus
    {
        public string C_CollegeName { get; set; }
        public string C_SectionName { get; set; }
        public string C_RegistrationId { get; set; }
        public string C_AdmissionStatus { get; set; }
        public string C_PaymentTransactionId { get; set; }
        // for Quarter Number
        public string C_QtrNo { get; set; }
    }
    public class CandidateFee
    {
        public string FeeSubHeadName { get; set; }
        public string FeeAmountYearly { get; set; }
        public int TotalFee { get; set; }
    }
    public class SeatAllotmentLetter
    {
        public string Reg_id { get; set; }
        public string CandidateName { get; set; }
        public string Gender { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Address { get; set; }
        public string ApplicantDOB { get; set; }
        public string Photo { get; set; }
        public string CounsellingRound { get; set; }
        public string DateFromTo { get; set; }

        public List<SeatAllotmentDetail> seatAllotmentDetails { get; set; }
    }

    public class SeatAllotmentDetail
    {
        public string ApplicationNo { get; set; }
        public string MeritNumber { get; set; }
        public string CollegeName { get; set; }
        public string Trade { get; set; }
        public string CandidateCategory { get; set; }
        public string SeatAllotedCategory { get; set; }
        public string Fee { get; set; }
        public string LastDateFee { get; set; }
        public string ModeOfPayment { get; set; }
    }
    public class FeeModule
    {
        public string RegistrationId { get; set; }
        public string Rollno { get; set; }
        public string CandidateName { get; set; }
        public string CollegeName { get; set; }
        public string CourseName { get; set; }
        public string CategoryName { get; set; }
        public List<CandidateFee> CandidateFee { get; set; }
        public int TotalFee { get; set; }
        public string FeePaid { get; set; }
        public string PaymentGateway { get; set; }
        public string Feetobepay { get; set; }
        public string PaymentTransactionId { get; set; }
        public string CollegeType { get; set; }
        public string SeatAllocationCategory { get; set; }
        public string SectionName { get; set; }
        public string Sessionid { get; set; }
        public string gender_name { get; set; }
        public string ApplicantDOB { get; set; }
        public string PaymentTrackingID { get; set; }
        public string Transactiondate { get; set; }
        public string OrderStatus { get; set; }
        public string CandidateMobile { get; set; }
        public string PaymentMode { get; set; }
        public string AnnualInstallment { get; set; }
        public string CandidateEmailid { get; set; }
        public string edishaid { get; set; }
        public string Counselling { get; set; }
        public string FatherName { get; set; }

        // for Quarter Number
        public string QtrNo { get; set; }
        public string admYear { get; set; }


        //to be send 
        public string tid { get; set; }
        public string merchant_id { get; set; }
        public string order_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string redirect_url { get; set; }
        public string cancel_url { get; set; }
        public string language { get; set; }
        //optional

        public string billing_name { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public string billing_state { get; set; }
        public string merchant_param1 { get; set; }
        public string merchant_param2 { get; set; }
        public string merchant_param3 { get; set; }
        public string merchant_param4 { get; set; }
        public string merchant_param5 { get; set; }
        public string Bank_ref_no { get; set; }
        public string billing_tel { get; set; }
        public string billing_email { get; set; }
        public string FeePaidNumber { get; set; }
        public string EdishaTransactionid { get; set; }
        public string CancelAdmission { get; set; }
        public string Challan_status { get; set; }
        public int Concession { get; set; }
        public int PendingFee { get; set; }
        public string TotalFeeNumber { get; set; }
        public string ConcessionNumber { get; set; }
        public string PendingFeeNumber { get; set; }
        public string TwelthBoard { get; set; }
        public string ParentIncome { get; set; }

    }

    // For check password change status
    public class CandidatePasswordStatus
    {
        public string Updated { get; set; }
    }


    // For Admission Session on Login page
    public class AdmissionSession
    {
        public string Year { get; set; }
        public string SessionID { get; set; }
    }
}