using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HigherEducation.Models
{
    public class RegistrationPGViewModel
    {
        public string Reg_Id { get; set; }
        public string tid { get; set; }
        public string merchant_id { get; set; }
        public string order_id { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string redirect_url { get; set; }
        public string cancel_url { get; set; }
        public string language { get; set; }
        public string Amount { get; set; }
        public string PaymentId { get; set; }
        public string merchant_param1 { get; set; }
        public string FeeType { get; set; }
        public string merchant_param3 { get; set; }
        public List<PaymentSuccessDetail> paymentSuccessDetails { get; set; }
        public string gender { get; set; }
    }
    public class PaymentSuccessDetail
    {
        public string Amount { get; set; }
        public string Tracking_id { get; set; }
        public string Bank_ref_no { get; set; }
        public string Order_status { get; set; }
        public string Payment_mode { get; set; }
        public string PaymentDate { get; set; }
        public string Payment_transactionId { get; set; }


    }
    public class PaymentResponse
    {
        public string order_id { get; set; }
        public string tracking_id { get; set; }
        public string bank_ref_no { get; set; }
        public string order_status { get; set; }
        public string failure_message { get; set; }
        public string payment_mode { get; set; }
        public string card_name { get; set; }
        public string status_code { get; set; }
        public string status_message { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string billing_name { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public string billing_state { get; set; }
        public string billing_tel { get; set; }
        public string billing_email { get; set; }
        public string merchant_param1 { get; set; }
        public string merchant_param2 { get; set; }
        public string merchant_param3 { get; set; }
        public string merchant_param4 { get; set; }
        public string merchant_param5 { get; set; }
        public string vault { get; set; }
        public string offer_type { get; set; }
        public string offer_code { get; set; }
        public string discount_value { get; set; }
        public string mer_amount { get; set; }
        public string eci_value { get; set; }
        public string retry { get; set; }
        public string response_code { get; set; }
        public string billing_notes { get; set; }
        public string trans_date { get; set; }
        public string bin_country { get; set; }
    }
}