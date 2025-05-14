using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class WeightageViewModel
    {

        //public string IsNationalAward { get; set; }
        //[Required(ErrorMessage = "required")]

        //public string IsNccCadet { get; set; }
        //[Required(ErrorMessage = "required")]

        //public string IsRuralArea { get; set; }
        //[Required(ErrorMessage = "required")]
        //public string HaryanaRuralAreaSchool { get; set; }
        [Required(ErrorMessage = "required")]
        public string Widow_Ward { get; set; }

        [Required(ErrorMessage = "required")]
        public string Orphan { get; set; }

        [Required(ErrorMessage = "required")]
        public string Panch_Weightage { get; set; }
        public string Panch_Weightage_Village { get; set; }


        public string Reg_id { get; set; }
    }
}