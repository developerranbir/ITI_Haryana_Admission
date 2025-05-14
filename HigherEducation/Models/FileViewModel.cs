using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class FileViewModel
    {
        public string Applicant_Name { get; set; }
        public int Caste { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public int IsDocExists { get; set; }
        public List<DocumentList> documentLists { get; set; }
    }
    public class DocumentList
    {
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        [ValidateFile]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.pdf|.png|.jpg|.jpeg)$", ErrorMessage = "Only pdf/jpeg/jpg/png allowed")]
        public HttpPostedFileBase files { get; set; }
        public string DocumentName { get; set; }
        public string DocumentNo { get; set; }
        public int DocumentId { get; set; }
        public string EdishaServiceId { get; set; }
        public string IsDocVerify { get; set; }

    }
    //Customized data annotation validator for uploading file
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int MaxContentLength = 1024 * 1024 * 3; //3 MB
            string[] AllowedFileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".pdf" , ".JPEG", ".JPG", ".PNG", ".PDF" };

            var file = value as HttpPostedFileBase;

            if (file == null)
                return false;
            else if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "Please upload Your Photo of type: " + string.Join(", ", AllowedFileExtensions);
                return false;
            }
            else if (file.ContentLength > MaxContentLength)
            {
                ErrorMessage = "Your Photo is too large, maximum allowed size is : " + (MaxContentLength / 1024).ToString() + "MB";
                return false;
            }
            else
                return true;
        }
    }
}