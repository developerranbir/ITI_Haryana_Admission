using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class ErrorLogViewModel
    {
        public string Exc_Date { get; set; }
        public string Exc_Location { get; set; }
        public string Exc_Type { get; set; }
        public string Exc_Method { get; set; }
        public string Full_Exc { get; set; }
        public string Exc_Message { get; set; }
        public string Exc_Level { get; set; }
    }
}