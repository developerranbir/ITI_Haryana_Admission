using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;


namespace HigherEducation.DHE
{
    public class clsDataAccess
    {
        string ConnectionString = "server=10.31.139.5;port=3306;database=skill_development;User Id=rohit;password=rohit@123; Pooling=false;SslMode=None;Keepalive=60";
        //Data Source=10.31.139.5;port=3306;Initial Catalog=skill_development;User Id=rohit;password=rohit@123
        //string ConnectionString = "server=10.88.250.71;port=3306;database=skill_development;User Id=root;password=admin@757";
        //string ConnectionString = "server=127.0.0.1;port=3306;database=skill_development;User Id=root;password=root; Pooling=false;SslMode=None;Keepalive=60;AllowPublicKeyRetrieval=True";
        
        //ranbir singh
        public DataTable GetUnPaidStudentData_SINGLE(String Year,string regid ,string feetype)
        {
            DataTable vds = new DataTable();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    //Console.WriteLine("trying to open connection........");
                    connection.Open();
                    //Console.WriteLine("connection open ");
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("RSGetUnpaidTransactionId_singleSTD", connection);
                vadap.SelectCommand.Parameters.AddWithValue("P_Year", Year);
                vadap.SelectCommand.Parameters.AddWithValue("p_Regid", regid);
                vadap.SelectCommand.Parameters.AddWithValue("feetype", feetype);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                //Console.WriteLine("total rows get ==" + vds.Rows.Count);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                //Console.ReadLine();
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return vds;
        }
        public DataTable GetUnPaidStudentData(String Year)
        {
            DataTable vds = new DataTable();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    //Console.WriteLine("trying to open connection........");
                    connection.Open();
                    //Console.WriteLine("connection open ");
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("RSGetUnpaidTransactionIdQTR", connection);
                vadap.SelectCommand.Parameters.AddWithValue("P_Year", Year);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                //Console.WriteLine("total rows get ==" + vds.Rows.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return vds;
        }

        public string SaveHDFCPaymentResponse2(Root feeresponse, string Reg_ID, string PaymentTransactionId, string collegeId, String Year)
        {
            string result = "0";
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseDataQTR", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", Reg_ID);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.order_Status_List[0].reference_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.order_Status_List[0].order_bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_Status_List[0].order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.order_Status_List[0].order_card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.order_Status_List[0].order_card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.order_Status_List[0].order_bank_response);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.order_Status_List[0].order_date_time);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", "API");
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", "10.31.43.11");
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", PaymentTransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.order_Status_List[0].order_amt);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", null);
                //vadap.SelectCommand.Parameters.AddWithValue("v_counselling", "7");
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", collegeId);
                vadap.SelectCommand.Parameters.AddWithValue("P_Year", Year);




                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        public string SaveHDFCPaymentResponse2_admission(Root feeresponse, string Reg_ID, string PaymentTransactionId, string collegeId, String Year)
        {
            string result = "0";
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseData_admission", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", Reg_ID);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.order_Status_List[0].reference_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.order_Status_List[0].order_bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_Status_List[0].order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.order_Status_List[0].order_card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.order_Status_List[0].order_card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.order_Status_List[0].order_bank_response);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.order_Status_List[0].order_date_time);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", "API");
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", "10.31.43.11");
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", PaymentTransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.order_Status_List[0].order_amt);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", null);
                //vadap.SelectCommand.Parameters.AddWithValue("v_counselling", "7");
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", collegeId);
                vadap.SelectCommand.Parameters.AddWithValue("P_Year", Year);




                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.Fill(vds);
                if (vds.Rows.Count > 0)
                {
                    result = vds.Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

    }
    class APIParam
    {
        public string order_no { get; set; }
        public string order_status { get; set; }
        public string from_date { get; set; }
        public string page_number { get; set; }
    }

    public class OrderStatusList
    {
        public long reference_no { get; set; }
        public string order_no { get; set; }
        public string order_currncy { get; set; }
        public double order_amt { get; set; }
        public string order_date_time { get; set; }
        public string order_bill_name { get; set; }
        public string order_bill_address { get; set; }
        public string order_bill_zip { get; set; }
        public string order_bill_tel { get; set; }
        public string order_bill_email { get; set; }
        public string order_bill_country { get; set; }
        public string order_ship_name { get; set; }
        public string order_ship_address { get; set; }
        public string order_ship_country { get; set; }
        public string order_ship_tel { get; set; }
        public string order_bill_city { get; set; }
        public string order_bill_state { get; set; }
        public string order_ship_city { get; set; }
        public string order_ship_state { get; set; }
        public string order_ship_zip { get; set; }
        public string order_ship_email { get; set; }
        public string order_notes { get; set; }
        public string order_ip { get; set; }
        public string order_status { get; set; }
        public string order_fraud_status { get; set; }
        public string order_status_date_time { get; set; }
        public double order_capt_amt { get; set; }
        public string order_card_name { get; set; }
        public string order_delivery_details { get; set; }
        public double order_fee_perc_value { get; set; }
        public double order_fee_perc { get; set; }
        public double order_fee_flat { get; set; }
        public double order_gross_amt { get; set; }
        public double order_discount { get; set; }
        public double order_tax { get; set; }
        public string order_bank_ref_no { get; set; }
        public string order_gtw_id { get; set; }
        public string order_bank_response { get; set; }
        public string order_option_type { get; set; }
        public string order_TDS { get; set; }
        public string order_device_type { get; set; }
        public string order_type { get; set; }
        public string ship_date_time { get; set; }
        public string payment_mode { get; set; }
        public string card_type { get; set; }
        public string sub_acc_id { get; set; }
        public string order_bin_country { get; set; }
        public string order_stlmt_date { get; set; }
        public string card_enrolled { get; set; }
        public string merchant_param1 { get; set; }
        public string merchant_param2 { get; set; }
        public string merchant_param3 { get; set; }
        public string merchant_param4 { get; set; }
        public string merchant_param5 { get; set; }
        public string order_bank_arn_no { get; set; }
        public string order_split_payout { get; set; }
        public string emi_issuing_bank { get; set; }
        public string tenure_duration { get; set; }
        public string cc { get; set; }
        public string emi_discount_value { get; set; }
    }

    public class Root
    {
        public List<OrderStatusList> order_Status_List { get; set; }
        public int page_count { get; set; }
        public int total_records { get; set; }
        public string error_desc { get; set; }
        public string error_code { get; set; }
    }
}
