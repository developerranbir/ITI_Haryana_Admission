using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class TradeDetails
    {
        public String districtcode { get; set; }
        public String districtname { get; set; }

        public String collegeid { get; set; }
        public String ITI { get; set; } //collegename

        public String courseid { get; set; }
        public String Trade { get; set; }   //name of the course


        public String Type { get; set; }
        public String Shift { get; set; }
        public String Unit { get; set; }
        public String Scheme { get; set; }
    }
    public class TradeDetailsCSV
    {
        //public TradeDetailsCSV()
        //{
        //    noOfIMCSEATS = 0;
        //}

        public String id { get; set; }
        public String District { get; set; }
        //[DisplayName("ITI Name")]
        public String ITIName { get; set; }
        //[DisplayName("standardTradeName")]
        public String StandardTradeName { get; set; }
        //[DisplayName("TradeType")]
        public String TradeType { get; set; }
        //[DisplayName("unitSize")]
        public String UnitSize { get; set; }
        //[DisplayName("PPP ITI")]
        public String PPPITI { get; set; }
        //[DisplayName("Duration")]
        public String duration { get; set; }
        //[DisplayName("1st Shift22")]
        public String IstShift22 { get; set; }
        //[DisplayName("1st Unit22")]
        public String IstUnit22 { get; set; }
        //[DisplayName("SchemeByDept")]
        public String schemeByDept { get; set; }
        //[DisplayName("scheme_for_seatAllotment")]
        public String Scheme_for_seatAllotment { get; set; }
        //[DisplayName("no. of Units")]
        public String noOfUnits { get; set; }
        //[DisplayName("no.of Seats")]
        public String noOfSeats { get; set; }
        //[DisplayName("NCVT/ SCVT type")]
        public String NCVTSCVTtype { get; set; }
        //[DisplayName("no.of IMC SEATS")]
        public String noOfIMCSEATS { get; set; }
        //[DisplayName("calculated FinalSeat")]
        public String calculatedFinalSeat { get; set; }
        //[DisplayName("Trade fee_Tuition fee_Boy_Haryana domicile")]
        public String Tradefee_Tuitionfee_Boy_Haryanadomicile { get; set; }
        //[DisplayName("Trade fee_Tuition fee_Boy_non Haryana domicile")]
        public String Tradefee_Tuitionfee_Boy_nonHaryanadomicile { get; set; }
        //[DisplayName("Trade fee_Tuition fee_Girl")]
        public String Tradefee_Tuitionfee_Girl { get; set; }  
        //[DisplayName("IMC seat fee(addl.fee_applicable to boys only)")]
        public String IMCseatfee { get; set; }
        public String comment21 { get; set; }
        public String ispaidSeat { get; set; }
        public String iswomen { get; set; }
        public String issteno { get; set; }

    }
}