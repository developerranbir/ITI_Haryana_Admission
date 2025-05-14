using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HigherEducation.Models
{
    public class RegistrationViewModel
    {
        public string RegID { get; set; }
        public string TwelveRollNo { get; set; }
        public string QualificationCode { get; set; }
        public string BoardCode { get; set; }
        [Required(ErrorMessage = "DOB is required")]
        public string Dob { get; set; }
        public string CandidateName { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Mother name is required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        [StringLength(100, ErrorMessage="Max 100 character allowed")]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "Father name is required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Use letters only please")]
        [StringLength(100, ErrorMessage = "Max 100 character allowed")]

        public string FatherHusbandName { get; set; }
        [Required(ErrorMessage = "You must provide a phone number,Phone Number at least 10 digit")]
        [StringLength(10)]
        [Display(Name = "ContactNo")]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid Mobile no")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100, ErrorMessage = "Max 100 character allowed")]

        public string Email { get; set; }
        public string IPAddress { get; set; }
        public string PassingOfYear { get; set; }
        public string Password { get; set; }
        public string BrowserName { get; set; }
        public string CheckAPIStatus { get; set; }
        public string Board_code { get; set; }
        public string Aadhaar { get; set; }
    }
}