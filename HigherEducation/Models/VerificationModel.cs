using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class VerificationModel
    {


        public string reg_id { get; set; }
        public string personal_verified { get; set; }
        public string education_verified { get; set; }
        public string reservation_verified { get; set; }
        public string caste_verified { get; set; }
        public string domicile_verified { get; set; }
        public string widow_orphan_verified { get; set; }
        public string panchyat_verified { get; set; }
        public string higher_edu_grad12_verified { get; set; }
        public string personal_remarks { get; set; }
        public string education_remarks { get; set; }
        public string reservation_remarks { get; set; }
        public string caste_remarks { get; set; }
        public string domicile_remarks { get; set; }
        public string widow_orphan_remarks { get; set; }
        public string panchyat_remarks { get; set; }
        public string higher_edu_grad12_remarks { get; set; }
        public string final_verification { get; set; }
        public string verifiedby { get; set; }

        public string ChangeUser { get; set; }

        public string collegeid { get; set; }
        public string userid { get; set; }
        public string ipaddress { get; set; }

        public string OfferSeat { get; set; }
        public string EligibleForPMSBenefits { get; set; }
    }
}