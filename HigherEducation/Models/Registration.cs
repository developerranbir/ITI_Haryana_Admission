using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class CandidateAuditTrail
    {
        public int Status { get; set; }
        public string auditStatus { get; set; }
        public string ipaddress { get; set; }
        public string errormsg { get; set; }
        public string actiontype { get; set; }
        public string FromType { get; set; }
        public string SrNo { get; set; }
    }
    public class Document
    {
        public string Docs { get; set; }
        public string DocsName { get; set; }
        public string Reg_Id { get; set; }
        public int FormId { get; set; }
        public string Docid { get; set; }
        public string DocNo { get; set; }
        public int? Max_page { get; set; }
        public int? Current_page { get; set; }
        public string Isverify { get; set; }
        public string IsApi { get; set; }
        

    }
    public class AttachmentType
    {
        public string MimeType { get; set; }
        public string FriendlyName { get; set; }
        public string Extension { get; set; }
    }
    public class EmployeeModelClass
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Contact { get; set; }

        public IEnumerable<AllSubject> DeptList { get; set; }
    }
    public class AllSubject
    {
        public int P_Id { get; set; }
        public int B_P_Id { get; set; }
        public int B_SubjectId { get; set; }
        public int B_MaxMarks { get; set; }
        public int B_MarksObtained { get; set; }
        public string ExamPassed { get; set; }
        public int StreamId { get; set; }
        public int UnivBoard { get; set; }
        public string SchoolCollege { get; set; }
        public string RegistrationRollNo { get; set; }
        public string ResultStatus { get; set; }
        public string PassingYear { get; set; }
        public bool IsCgpa { get; set; }
        public int CGPA { get; set; }
        public string MarksObt { get; set; }
        public int MaxMarks { get; set; }
        public string Subject { get; set; }
        public decimal Percentage { get; set; }
        public string Reg_Id { get; set; }
        public int B_SubjectNo { get; set; }
        public string schoolName { get; set; }
        public int BoardCode { get; set; }
        public Int64 twelveAPI { get; set; }

        public int? Max_page { get; set; }
        public int? Current_page { get; set; }
        public string Top5Percentage { get; set; }

    }
    //public class Registration
    //{

    //    public string RollNo { get; set; }
    //    public string CandidateName { get; set; }
    //    public string BirthDate { get; set; }
    //    public string Sex { get; set; }
    //    public string MotherName { get; set; }
    //    public string FatherHusbandName { get; set; }
    //    public string TelephoneNo { get; set; }
    //    public string MobileNo { get; set; }
    //    public string Email { get; set; }
    //    public string StreetAddress1 { get; set; }
    //    public string StreetAddress2 { get; set; }
    //    public int Country { get; set; }
    //    public int State { get; set; }
    //    public int District { get; set; }
    //    public int Tehsil { get; set; }
    //    public int CityTownVillage { get; set; }
    //    public bool Corespondance { get; set; }
    //    public string RegistrationDate { get; set; }
    //    public string Remarks { get; set; }
    //    public string BankCode { get; set; }
    //    public int QualificationCode { get; set; }
    //    public string ApperaredPassed { get; set; }
    //    public string PassingOfYear { get; set; }
    //    public string Area { get; set; }
    //    public string HaryanaResidence { get; set; }
    //    public int CategoryCode { get; set; }
    //    public int SubCategoryCode { get; set; }
    //    public int ExserviceMan { get; set; }
    //    public int DistrictCode { get; set; }
    //    public int PhysicalHandicapedStatus { get; set; }
    //    public int Amount { get; set; }
    //    public int ParentalIncome { get; set; }
    //    public bool IsConfirmed { get; set; }
    //    public string InstituteType { get; set; }
    //    public string InstituteLocation { get; set; }
    //    public string Pincode { get; set; }
    //    public string City { get; set; }
    //    public string AadharNo { get; set; }
    //    public string ConfirmAadharNo { get; set; }
    //    public bool InstituteVerfied { get; set; }
    //    public string InstituteVerficationDate { get; set; }
    //    public int InstituteVerficationCode { get; set; }
    //    public int InstituteUserName { get; set; }
    //    public int InstituteCodeForVerfication { get; set; }
    //    public string MatricRollNo { get; set; }
    //    public string TwelveRollNo { get; set; }
    //    [Required(ErrorMessage = "Required")]
    //    public int BoardCode { get; set; }
    //    public string VerificationRemarks { get; set; }
    //    public string AadharEnrolmentId { get; set; }
    //    public string AadharEnrolmentDate { get; set; }
    //    public string Passkey { get; set; }
    //    public bool Status { get; set; }
    //    public bool VoterIdCard { get; set; }
    //    public bool KashmirMigrant { get; set; }
    //    public bool HaryanaDomicile { get; set; }
    //    public bool Minority { get; set; }
    //    public bool MaritalStatus { get; set; }
    //    public bool FatherOccupation { get; set; }
    //    public bool MotherOccupation { get; set; }
    //    public string GuardianMobileNo { get; set; }
    //    public string GuradianEmail { get; set; }
    //    public int BloodGroup { get; set; }
    //    public int Religion { get; set; }
    //    public bool NationalyType { get; set; }
    //    public int CasteCategory { get; set; }
    //    public int Caste { get; set; }
    //    public string BPLCardNo { get; set; }
    //    public string ModeOfTransport { get; set; }
    //    public bool Hostel { get; set; }
    //    public int BankState { get; set; }
    //    public string BankAccountNumber { get; set; }
    //    public bool NationalTalentAward { get; set; }
    //    public bool NCC { get; set; }
    //    public bool PassedRural { get; set; }
    //    public bool LinkedAadhar { get; set; }
    //    public int PreviousEducation { get; set; }
    //    public bool KM { get; set; }
    //    public int Gap_Year { get; set; }
    //    public string GuradianName { get; set; }
    //    public string BankName { get; set; }
    //    public string DrivingLicenceNo { get; set; }
    //    public string PassportNo { get; set; }
    //    public string BankIFSCCode { get; set; }
    //    public int ReservationCategory { get; set; }
    //    [DataType(DataType.Password)]
    //    public string Password { get; set; }
    //    [DataType(DataType.Password)]
    //    [Compare("Password", ErrorMessage = "Both Password and Confirm Password Must be Same")]
    //    public string ConfirmPassword { get; set; }

    //}
    public class ChoiceCourse
    {
        public string RegID { get; set; }
        public string Stream { get; set; }
        public string PassingOfYear { get; set; }
        public string DisableCategory { get; set; }
        public string Status { get; set; }
        public string Percentage { get; set; }
        public string DA { get; set; }
        public string Age { get; set; }
        public string MF { get; set; }

    }
    public class CandidateDetail
    {
        public int PId { get; set; }
        public string RegID { get; set; }
        public string TwelveRollNo { get; set; }
        public string BoardCode { get; set; }
        public int ReservationCategory { get; set; }
        public int? DisableCategory { get; set; }

        public int? DA { get; set; }
        public int? Age { get; set; }
        public int? MF { get; set; }
        public string AadharNo { get; set; }
        public string ConfirmAadharNo { get; set; }
        public string VoterIdCard { get; set; }
        public string KashmirMigrant { get; set; }
        public string Minority { get; set; }
        public int MinorityData { get; set; }
        public string HaryanaDomicile { get; set; }
        public string NationalyType { get; set; }
        public string Dob { get; set; }
        public int PassingOfYear { get; set; }
        // [Required(ErrorMessage = "Name is Required")]
        public string CandidateName { get; set; }
        public string BirthDate { get; set; }
        public int Sex { get; set; }
        public string MotherName { get; set; }
        public string FatherHusbandName { get; set; }
        public int Marital_Status { get; set; }
        public int Father_Occupation { get; set; }
        public int Mother_Occupation { get; set; }
        public string Guardian_Name { get; set; }
        public string GuardianMobileNo { get; set; }
        public string GuradianEmail { get; set; }
        public int BloodGroup { get; set; }
        public int Religion { get; set; }
        public int ParentalIncome { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string IPAddress { get; set; }
        public string AuditStatus { get; set; }
        public string ErrorMsg { get; set; }
        public string ActionType { get; set; }
        public string FromType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string Pincode { get; set; }
        public int Country_Code { get; set; }
        public string RuralUrban { get; set; }
        public string C_RuralUrban { get; set; }
        public int State_Code { get; set; }
        public int District_Code { get; set; }
        public int Sub_District_Code { get; set; }
        public int C_Sub_District_Code { get; set; }
        public int Tehsil_Code { get; set; }
        public int CityTownVillage { get; set; }
        public int CTV_Code { get; set; }
        public bool Is_Correspondence { get; set; } = false;
        public string C_StreetAddress1 { get; set; }
        public string C_StreetAddress2 { get; set; }
        public string C_Pincode { get; set; }
        public int C_Country_Code { get; set; }
        public int C_State_Code { get; set; }
        public int College { get; set; }
        public int CourseSection { get; set; }
        public int SubjectCombination { get; set; }
        public int C_District_Code { get; set; }
        public int C_Tehsil_Code { get; set; }
        public int C_CityTownVillage { get; set; }
        public int C_CTV_Code { get; set; }
        public string Srno { get; set; }
        public int CasteCategory { get; set; }
        public int Caste { get; set; }
        public string ModeOfTransport { get; set; }
        public byte[] UserImage { get; set; }
        public string BPLCardNo { get; set; }
        public string BPLCategory { get; set; }
        public int Gap_Year { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankIFSCCode { get; set; }
        public string Hostel { get; set; }
        public byte[] UserSig { get; set; }
        public string IsNationalAward { get; set; }
        public string IsNccCadet { get; set; }
        public string IsRuralArea { get; set; }

        public string Reg_Id { get; set; }
        public int Subject_MaxMarks { get; set; }
        public int Subject_MarksObtained { get; set; }
        public int AllSubject { get; set; }
        public string TwelveHarana { get; set; }
        public string Course_State { get; set; }
        public string Course_District { get; set; }
        public string Course_Course { get; set; }
        public string Course_CourseSection { get; set; }
        public string Course_SubjectCombination { get; set; }
        public string Course_College { get; set; }
        public string BrowserName { get; set; }
        public string FamilyID { get; set; }
        public string DrivingLicenceNo { get; set; }
        public string DrivingLicenceText { get; set; }
        public string PassportNo { get; set; }
        public string PassportText { get; set; }
        public int? District_Code_Rural { get; set; }
        public int? District_Code_Urban { get; set; }
        public int? C_District_Code_Rural { get; set; }
        public int? C_District_Code_Urban { get; set; }
        public int? Municiplity { get; set; }
        public int? C_Municiplity { get; set; }
        public int? Block { get; set; }
        public int? C_Block { get; set; }
        public int Status { get; set; }
        public string IsApplyEducationLoan { get; set; }
        public string IsParticipateActivites { get; set; }
        public string IsMatricScholarship { get; set; }
        public string CheckAPIStatus { get; set; }
        public int Stream { get; set; }
        public string Percentage { get; set; }

        public int? Max_page { get; set; }
        public int? Current_page { get; set; }

        public string VoterCardText { get; set; }
        public string Verificationstatus { get; set; }
        public string CourseSectionId { get; set; }
        public string HasUnlocked { get; set; }
        public string TotalFee { get; set; }
        public string rno { get; set; }
        public string Board_code { get; set; }
        public string HaryanaRuralAreaSchool { get; set; }
        public string MemberId { get; set; }
        public string OTP { get; set; }
        public int N_State_Code { get; set; }
        public int N_Country_Code { get; set; }
        public string FIDUID { get; set; }
        public string isCasteVerified { get; set; }
        public string isResidenceVerified { get; set; }
        public string isDivyangVerified { get; set; }
        public string isIncomeVerified { get; set; }
        public string isNameVerified { get; set; }
        public string isFnameVerified { get; set; }
        public string isDOBVerified { get; set; }
        public string Name_PPP { get; set; }
        public string Gender_PPP { get; set; }
        public string FHName_PPP { get; set; }
        public string DOB_PPP { get; set; }
        public string DOBVerifiedFrom { get; set; }
        public string isCasteCatgMatch_WithPPP { get; set; }
        public int CasteCategory_PPP { get; set; }
        public string subCaste_name_PPP { get; set; }
        public int subCaste_code_PPP { get; set; }
        public string casteDescription_PPP { get; set; }
        public string AdhaarNo { get; set; }
        public string IsITICompleted { get; set; }
        public string ITICompletedState { get; set; }
        public string ITICompletedYear { get; set; }
        public string ITICompletedName { get; set; }
        public string ITICompletedTrade { get; set; }
        public string ITICompletedRollNo { get; set; }
        public string QualificationCode { get; set; }
        public string Json_PPP { get; set; }
        public string aSession { get; set; }
    }
    public class LoginUserDetails
    {
        public string Salt { get; set; }
        public DateTime LastLogin { get; set; }
    }
    public class LoginTrackModels
    {
        public string RegID { get; set; }
        public string BrowserName { get; set; }
        public string IPAddress { get; set; }
        public string Password { get; set; }
    }
    public class CourseModel
    {
        public string Course_State { get; set; }
        public string Course_District { get; set; }
        public string Course_College { get; set; }
        public string Course_CollegeCourse { get; set; }
        public string Course_CourseSection { get; set; }
        public string Course_SubjectCombination { get; set; }

    }
    public class postPerfData
    {
        public String pref_course { get; set; }
        public String pref_no { get; set; }

        public int? Max_page { get; set; }
        public int? Current_page { get; set; }

    }
    public class UploadDocs
    {
        public int PId { get; set; }
        public string RegID { get; set; }
        public string UserImage { get; set; }
        public string UserSign { get; set; }
        public string Docs { get; set; }
        public string DocsName { get; set; }
        public int FormId { get; set; }

    }
    public class DeclarationTab
    {
        public int PId { get; set; }
        public string RegID { get; set; }
        public string TwelveRollNo { get; set; }
        public int BoardCode { get; set; }
        public string ReservationCategory { get; set; }
        public string AadharNo { get; set; }
        public string ConfirmAadharNo { get; set; }
        public string VoterIdCard { get; set; }
        public string KashmirMigrant { get; set; }
        public string Minority { get; set; }
        public string HaryanaDomicile { get; set; }
        public string NationalyType { get; set; }
        public string Dob { get; set; }
        public int PassingOfYear { get; set; }
        public string CandidateName { get; set; }
        public string BirthDate { get; set; }
        public string Sex { get; set; }
        public string MotherName { get; set; }
        public string FatherHusbandName { get; set; }
        public int Marital_Status { get; set; }
        public int Father_Occupation { get; set; }
        public int Mother_Occupation { get; set; }
        public string MaritalStatus { get; set; }
        public string FatherOccupation { get; set; }
        public string MotherOccupation { get; set; }
        public string Guardian_Name { get; set; }
        public string GuardianMobileNo { get; set; }
        public string GuradianEmail { get; set; }
        public string BloodGroup { get; set; }
        public string Religion { get; set; }
        public string ParentalIncome { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Both Password and Confirm Password Must be Same")]
        public string ConfirmPassword { get; set; }
        public string IPAddress { get; set; }
        public string AuditStatus { get; set; }
        public string ErrorMsg { get; set; }
        public string ActionType { get; set; }
        public string FromType { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string Pincode { get; set; }
        public string Country_Code { get; set; }
        public string state { get; set; }
        public string district_rural { get; set; }
        public string district_urban { get; set; }
        public string municipility { get; set; }
        public string block { get; set; }
        public string Tehsil_Code { get; set; }
        public string CityTownVillage { get; set; }
        public int CTV_Code { get; set; }
        public bool Is_Correspondence { get; set; }
        public string C_StreetAddress1 { get; set; }
        public string C_StreetAddress2 { get; set; }
        public int C_Pincode { get; set; }
        public int C_Country_Code { get; set; }
        public int C_State_Code { get; set; }
        public int College { get; set; }
        public int CourseSection { get; set; }
        public int SubjectCombination { get; set; }
        public int C_District_Code { get; set; }
        public int C_Tehsil_Code { get; set; }
        public int C_CityTownVillage { get; set; }
        public int C_CTV_Code { get; set; }
        public string Srno { get; set; }
        public string CasteCategory { get; set; }
        public string Caste { get; set; }
        public string ModeOfTransport { get; set; }
        public string UserImage { get; set; }
        public string BPLCardNo { get; set; }
        public int Gap_Year { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankIFSCCode { get; set; }
        public string Hostel { get; set; }
        public string UserSign { get; set; }
        public string IsNationalAward { get; set; }
        public string IsNccCadet { get; set; }
        public string IsRuralArea { get; set; }
        public string Reg_Id { get; set; }
        public int Subject_MaxMarks { get; set; }
        public int Subject_MarksObtained { get; set; }
        public int AllSubject { get; set; }
        public string Salt { get; set; }
        public string base64Image { get; set; }
        public string base64Sign { get; set; }
    }
    public class eduData
    {
        public Int64 Rollno { get; set; }
        public int Boardno { get; set; }
        public string schoolName { get; set; }
        public string ApiMaxMarks { get; set; }
        public string APiMaxobt { get; set; }
        public string ApiPassStatus { get; set; }
        public string DOB { get; set; }
        public string Stream_code { get; set; }


    }

    public class UserMaxCurrentPage
    {
        public int max_page { get; set; }
        public int current_page { get; set; }
        public string Verificationstatus { get; set; }
        public string HasUnlocked { get; set; }
        public string IsDisable { get; set; }
        public string FormStatus { get; set; }
        public string IsPassWordChange { get; set; }


    }


    public class collectiondata
    {
        public IndusInd collection_data { get; set; }
    }


    public class IndusInd
    {
        public string section { get; set; }
        public string fullpart { get; set; }
        public string installment_no { get; set; }
        public string candidatename { get; set; }
        public string session { get; set; }
        public string college_id { get; set; }
        public string course_id { get; set; }
        public string combination_id { get; set; }
        public string paymenttransactionId { get; set; }
        public string registrationId { get; set; }
        public string amount { get; set; }
        public string inv_no { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string template_id { get; set; }
        public string payment_form_display { get; set; }
        public string request_id { get; set; }
    }

    public class SCCandidateFee
    {
        public string RegistrationId { get; set; }
        public string CollegeId { get; set; }
        public string CombinationId { get; set; }
    }
    public class PayLater
    {
        public string RegistrationId { get; set; }
        public string CollegeId { get; set; }
        public string CombinationId { get; set; }
        public string Concession { get; set; }
    }

    public class SCCandidateFeePG
    {
        public string RegistrationId { get; set; }
        public string CollegeId { get; set; }
        public string SectionId { get; set; }
    }
}