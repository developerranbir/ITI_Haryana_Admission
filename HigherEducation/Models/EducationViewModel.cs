using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HigherEducation.Models
{
    public class EducationViewModel
    {
        public int P_Id { get; set; }
        public List<SelectListItem> Stream { get; set; }
        public List<SelectListItem> BoardList { get; set; }
        public List<SelectListItem> UniversityList { get; set; }
        [Required(ErrorMessage = "Stream is required")]
        public string SelectedStream { get; set; }
        public string SelectedBoard { get; set; }

        public string OldSelectedStream { get; set; }

        public string Gapyear { get; set; }
        public int Max_page { get; set; } = 2;
        public int Current_page { get; set; } = 2;
        public string Reg_Id { get; set; }
        public string IsFromApi_8th { get; set; }
        public string ExamPassed_8th { get; set; } = "8th";
        [Required(ErrorMessage = "8th board is required")]
        public string Uniboard_8th { get; set; }
        [Required(ErrorMessage = "School is required")]
        [RegularExpression(@"^[a-zA-Z0-9\.\,\s\/\|\-\&\']+$", ErrorMessage = "Only Alphanmeric with (,./|-&) Allowed in school")]
        [StringLength(400)]
        public string School_8th { get; set; }
        [Required(ErrorMessage = "Rollno is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only number allowed")]
        [StringLength(50)]
        public string Rollno_8th { get; set; }
        [Required(ErrorMessage = "Result is required")]
        public string Result_8th { get; set; } = "Pass";
        [Required(ErrorMessage = "Passing Year is required")]
        [Range(1950, 2024, ErrorMessage = "Must be between 1950 and 2024")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only number allowed")]
        [StringLength(4)]
        public string PassingYear_8th { get; set; }
        public Boolean CGPA_8th { get; set; }
        [Required(ErrorMessage = "Max Marks is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only number allowed")]
        [Range(100, 1500, ErrorMessage = "Must be between 100 and 1500")]
        public int? MaxMarks_8th { get; set; }
        [Required(ErrorMessage = "Max Obtain is required")]
        [RegularExpression(@"^[0-9\.]+$", ErrorMessage = "Only number allowed")]
        public decimal? MarksObtain_8th { get; set; }
        [Required(ErrorMessage = "Percentage is required")]
        [RegularExpression(@"^[0-9\.]+$", ErrorMessage = "Only number allowed")]
        [Range(33, 100, ErrorMessage = "Must be between 33 and 100")]

        public decimal? Percentage_8th { get; set; }
        //10th
        public string IsFromApi_10th { get; set; }
        public string ExamPassed_10th { get; set; } = "10th";
        [Required(ErrorMessage = "10th board is required")]
        public string Uniboard_10th { get; set; }
        [Required(ErrorMessage = "School is required")]
        [RegularExpression(@"^[a-zA-Z0-9\.\,\s\/\|\-\&\']+$", ErrorMessage = "Only Alphanmeric with (,./|-&) Allowed in school")]
        [StringLength(400)]
        public string School_10th { get; set; }
        [Required(ErrorMessage = "Rollno is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only number allowed")]
        [StringLength(50)]
        public string Rollno_10th { get; set; }
        [Required(ErrorMessage = "Result is required")]
        public string Result_10th { get; set; } = "Pass";
        [Required(ErrorMessage = "Passing Year is required")]
        [Range(1950, 2024, ErrorMessage = "Must be between 1950 and 2024")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only number allowed")]
        [StringLength(4)]
        public string PassingYear_10th { get; set; }
        public Boolean CGPA_10th { get; set; }
        [Required(ErrorMessage = "Max Marks is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only number allowed")]
        [Range(100, 1500, ErrorMessage = "Must be between 100 and 1500")]
        public int? MaxMarks_10th { get; set; }
        [Required(ErrorMessage = "Max Obtain is required")]
        [RegularExpression(@"^[0-9\.]+$", ErrorMessage = "Only number allowed")]
        public decimal? MarksObtain_10th { get; set; }
        [Required(ErrorMessage = "Percentage is required")]
        [RegularExpression(@"^[0-9\.]+$", ErrorMessage = "Only number allowed")]
        [Range(33, 100, ErrorMessage = "Must be between 33 and 100")]
        public decimal? Percentage_10th { get; set; }

        //12th
        public string IsFromApi_12th { get; set; }
        public string ExamPassed_12th { get; set; } = "12th";
        [Required]
        public string Uniboard_12th { get; set; }
        [Required(ErrorMessage = "School is required")]
        [RegularExpression(@"^[a-zA-Z0-9\.\,\s\/\|\-\&\'\`]+$", ErrorMessage = "Alphanmeric with (,./|-&'`) Allowed")]
        [StringLength(400)]
        public string School_12th { get; set; }
        [Required(ErrorMessage = "Rollno is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only number allowed")]
        [StringLength(50)]
        public string Rollno_12th { get; set; }
        [Required(ErrorMessage = "Result is required")]
        public string Result_12th { get; set; }
        [Required(ErrorMessage = "Passing Year is required")]
        [Range(1950, 2024, ErrorMessage = "Must be between 1950 and 2024")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only number allowed")]
        [StringLength(4)]
        public string PassingYear_12th { get; set; }
        public Boolean CGPA_12th { get; set; }
        [Required(ErrorMessage = "Max Marks is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid")]
        [Range(100, 1500, ErrorMessage = "Must be between 100 and 1500")]
        public int? MaxMarks_12th { get; set; }
        [Required(ErrorMessage = "Max Obtain is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only number allowed")]
        public int? MarksObtain_12th { get; set; }
        [Required(ErrorMessage = "Percentage is required")]
        [RegularExpression(@"^[0-9\.]+$", ErrorMessage = "Only number allowed")]
        [Range(33, 100, ErrorMessage = "Must be between 33 and 100")]
        public decimal? Percentage_12th { get; set; }

        //Diploma
        public string ExamPassed_Diploma { get; set; } = "Graduation";
        public string Uniboard_Diploma { get; set; } = "";
        [Required(ErrorMessage = "School is required")]
        [RegularExpression(@"^[a-zA-Z0-9\.\,\s\/\|\-\&\']+$", ErrorMessage = "Only Alphanmeric with (,./|-&) Allowed in school")]
        [StringLength(400)]
        public string School_Diploma { get; set; }
        [Required(ErrorMessage = "Rollno is required")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only number allowed")]
        [StringLength(50)]
        public string Rollno_Diploma { get; set; }
        [Required(ErrorMessage = "Result is required")]
        public string Result_Diploma { get; set; }
        [Required(ErrorMessage = "Passing Year is required")]
        [Range(1950, 2024, ErrorMessage = "Must be between 1950 and 2024")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only number allowed")]
        [StringLength(4)]
        public string PassingYear_Diploma { get; set; }
        public Boolean CGPA_Diploma { get; set; }
        [Required(ErrorMessage = "Max Marks is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid")]
        [Range(100, 9000, ErrorMessage = "Must be between 100 and 9000")]
        public int? MaxMarks_Diploma { get; set; }
        [Required(ErrorMessage = "Max Obtain is required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Only number allowed")]
        [Range(100, 9000, ErrorMessage = "Must be between 100 and 9000")]

        public int? MarksObtain_Diploma { get; set; }
        [Required(ErrorMessage = "Percentage is required")]
        [RegularExpression(@"^[0-9\.]+$", ErrorMessage = "Only number allowed")]
        [Range(33, 100, ErrorMessage = "Must be between 33 and 100")]
        public decimal? Percentage_Diploma { get; set; }
        public List<SubjectDetail> subjectDetails { get; set; }
        public string IsAPI { get; set; }
        public decimal BestFive_Percentage { get; set; }
        public int DOB_Year { get; set; }
        public string QualificationCode { get; set; }
    }

    public class SubjectDetail
    {
        public List<SelectListItem> SubjectList { get; set; }
        [Required(ErrorMessage = "Required")]
        public int? SelectedSubjectId { get; set; }
        public int P_id { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[0-9\.]+$", ErrorMessage = "Invalid")]
        [Range(0, 200, ErrorMessage = "Must be between 0 and 200")]
        public string MarksObtain { get; set; }
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid")]
        [Range(0, 200, ErrorMessage = "Must be between 0 and 200")]
        public string MaxMarks { get; set; }
        public decimal Best5Percentage { get; set; }

    }

}