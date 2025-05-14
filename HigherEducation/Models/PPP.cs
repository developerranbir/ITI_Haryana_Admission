using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class PPP
    {
    }
    public class RootObj
    {
        public IList<dropdown> dropdown { get; set; }
        public string familyID { get; set; }
        public string memberID { get; set; }
        public string fullName { get; set; }
        public string fatherFullName { get; set; }
        public string motherFullName { get; set; }
        public string spouseFullName { get; set; }
        public string dob { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string genderName { get; set; }
        public string occupation { get; set; }
        public string dcode { get; set; }
        public string btCode { get; set; }
        public string wvCode { get; set; }
        public string houseNo { get; set; }
        public string streetNo { get; set; }
        public string pinCode { get; set; }
        public string address_LandMark { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
        public string maritalStatus { get; set; }
        public string isBPLVerified { get; set; }
        public string bplCardNO { get; set; }
        public string bpl { get; set; }
        public string isCasteVerified { get; set; }
        public string isResidenceVerified { get; set; }
        public string isDivyangVerified { get; set; }
        public string isIncomeVerified { get; set; }

    }
    public class OTPRequestforMEMID
    {
        public string DeptCode { get; set; }
        public string ServiceCode { get; set; }
        public string DeptKey { get; set; }
        public string MemberID { get; set; }
    }
    public class VerifyOTPRequestforMEMID
    {
        public string DeptCode { get; set; }
        public string ServiceCode { get; set; }
        public string DeptKey { get; set; }
        public string MemberID { get; set; }
        public string OTP { get; set; }
        public string TXN { get; set; }
    }
    public class OTPResponseMemID
    {
        public string status { get; set; }
        public string message { get; set; }
        public string txn { get; set; }
    }
    public class FamiyID
    {
        public string DeptCode { get; set; }
        public string ServiceCode { get; set; }
        public string DeptKey { get; set; }
        public string UIDFID { get; set; }
    }
    public class dropdown {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}