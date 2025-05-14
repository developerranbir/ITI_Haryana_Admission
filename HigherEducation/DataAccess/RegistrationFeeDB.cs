using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;
using HigherEducation.Models;
using System.Data;
using NLog;
using System.Globalization;
using HigherEducation.BusinessLayer;
using HigherEducation.Controllers;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using PayUIntegration;
using System.Web.Mvc;

namespace HigherEducation.DataAccess
{
    public class RegistrationFeeDB
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);

        // string IPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        string IPAddress = GetIPAddress();
        string UserId = Convert.ToString(HttpContext.Current.Session["UserId"]);
        static string Counselling = ConfigurationManager.AppSettings["Counselling"];

        public static string GetIPAddress()
        {
            string ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }
        public RegistrationPGViewModel GetCandidateRegFee(string registrationid)
        {
            RegistrationPGViewModel objFeepaid = new RegistrationPGViewModel();
            List<PaymentSuccessDetail> objpaymentSuccessDetails = new List<PaymentSuccessDetail>();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetRegistrationFeeITI", connection);  //
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_Reg_id", registrationid);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objFeepaid.amount = Convert.ToInt32(vds.Tables[0].Rows[0]["FeeAmount"]);
                        objFeepaid.gender = vds.Tables[0].Rows[0]["gender"].ToString();

                    }
                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        objpaymentSuccessDetails = (from DataRow row in vds.Tables[1].Rows
                                                    select new PaymentSuccessDetail
                                                    {
                                                        Amount = Convert.ToString(row["Amount"]),
                                                        Tracking_id = Convert.ToString(row["tracking_id"]),
                                                        Bank_ref_no = Convert.ToString(row["bank_ref_no"]),
                                                        Order_status = Convert.ToString(row["order_status"]),
                                                        Payment_mode = Convert.ToString(row["payment_mode"]),
                                                        PaymentDate = Convert.ToString(row["CreateDate"]),
                                                        Payment_transactionId = Convert.ToString(row["Payment_transactionId"])
                                                    }).ToList();
                    }

                    objFeepaid.paymentSuccessDetails = objpaymentSuccessDetails;

                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.GetCandidateRegFee()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objFeepaid;
        }

        public RegistrationPGViewModel GetCandidatePenaltyFee(string registrationid)
        {
            RegistrationPGViewModel objFeepaid = new RegistrationPGViewModel();
            List<PaymentSuccessDetail> objpaymentSuccessDetails = new List<PaymentSuccessDetail>();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetPanrltyFeeITI", connection);  //GetRegistrationFeeITI
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_Reg_id", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_Counselling", Counselling);


                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objFeepaid.amount = Convert.ToInt32(vds.Tables[0].Rows[0]["FeeAmount"]);
                    }
                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        objpaymentSuccessDetails = (from DataRow row in vds.Tables[1].Rows
                                                    select new PaymentSuccessDetail
                                                    {
                                                        Amount = Convert.ToString(row["Amount"]),
                                                        Tracking_id = Convert.ToString(row["tracking_id"]),
                                                        Bank_ref_no = Convert.ToString(row["bank_ref_no"]),
                                                        Order_status = Convert.ToString(row["order_status"]),
                                                        Payment_mode = Convert.ToString(row["payment_mode"]),
                                                        PaymentDate = Convert.ToString(row["CreateDate"]),
                                                        Payment_transactionId = Convert.ToString(row["Payment_transactionId"])
                                                    }).ToList();
                    }
                    objFeepaid.paymentSuccessDetails = objpaymentSuccessDetails;

                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.GetCandidateRegFee()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objFeepaid;
        }
        public string InitiateRegFee(RegistrationPGViewModel feeModule)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("ITI_InitiateRegfee", connection); // ITI_InitiateRegfee
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeModule.Reg_Id);
                vadap.SelectCommand.Parameters.AddWithValue("P_TotalFee", feeModule.amount);
                vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", feeModule.PaymentId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_feeType", feeModule.FeeType);




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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.InitiateRegFee()" + feeModule.Reg_Id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }


        public string SaveRegistrationFeeSuccess(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveRegistrationFeeITI", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SaveRegistrationFeeSuccess()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SaveRegistrationFeeFailure(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveRegistrationFeeITI_Cancel", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SaveRegistrationFeeFailure()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SavePGResponse(string responseText, string regid, string responseBank)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("RSSavePG_response_Registration", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("p_responsetext", responseText);
                vadap.SelectCommand.Parameters.AddWithValue("p_reg_id", regid);
                vadap.SelectCommand.Parameters.AddWithValue("p_responseBank", responseBank);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SavePGResponse()" + regid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string SaveMaxPageOnly(string reg_id)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveMaxPageOnly", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", reg_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_IPAddress", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_CreateUser", IPAddress);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SaveMaxPageOnly()" + reg_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        public string SaveMaxPageOnlywithZerofee(string reg_id)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveMaxPageOnlywithZerofee", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", reg_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_IPAddress", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_CreateUser", IPAddress);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SaveMaxPageOnly()" + reg_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string AdmissionFeeSuccess(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseData", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", feeresponse.merchant_param3);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.AdmissionFeeSuccess()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string QuarterFeeSuccess(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseDataQ", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", feeresponse.merchant_param3);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.QuarterFeeSuccess()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }


        public string AdmissionFeeFailure(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseData_cancel", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);

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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SaveRegistrationFeeFailure()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        public string QuarterFeeFailure(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseData_cancel", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);

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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.QuarterFeeFailure()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }


        public SeatAllotmentLetter GetSeatAllotmentLetter(string Counselling, string registrationid, string Meritid)
        {
            SeatAllotmentLetter objFeepaid = new SeatAllotmentLetter();
            List<SeatAllotmentDetail> objSeatAllotmentLetter = new List<SeatAllotmentDetail>();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetSeatAllotmentLetterDetails", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("P_counselling", Counselling);
                vadap.SelectCommand.Parameters.AddWithValue("P_Meritid", Meritid);


                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.Gender = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.FatherName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_father_name"]);
                        objFeepaid.MotherName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_mother_name"]);
                        objFeepaid.Address = Convert.ToString(vds.Tables[0].Rows[0]["address"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);
                        objFeepaid.Photo = Convert.ToString(vds.Tables[0].Rows[0]["photo"]);
                        objFeepaid.CounsellingRound = Convert.ToString(vds.Tables[0].Rows[0]["Counselling_round"]);
                        objFeepaid.DateFromTo = Convert.ToString(vds.Tables[0].Rows[0]["Date_from_to"]);

                        objSeatAllotmentLetter = (from DataRow row in vds.Tables[0].Rows
                                                  select new SeatAllotmentDetail
                                                  {
                                                      ApplicationNo = Convert.ToString(row["registrationID"]),
                                                      MeritNumber = Convert.ToString(row["merit_id"]),
                                                      CollegeName = Convert.ToString(row["collegename"]),
                                                      Trade = Convert.ToString(row["section"]),
                                                      CandidateCategory = Convert.ToString(row["categoryname"]),
                                                      SeatAllotedCategory = Convert.ToString(row["SeatAllocationCategory"]),
                                                      Fee = Convert.ToString(row["Fee"]),
                                                      LastDateFee = Convert.ToString(row["lastFeedate"]),
                                                      ModeOfPayment = Convert.ToString(row["modeofpayment"]),
                                                  }).ToList();

                    }
                    objFeepaid.seatAllotmentDetails = objSeatAllotmentLetter;
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.GetSeatAllotmentLetter()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objFeepaid;
        }
        public RankCard RankCard(string registrationid)
        {
            RankCard rankCard = new RankCard();
            List<AcademicDetail> academicDetails = new List<AcademicDetail>();
            List<MarksandWeightage> objMarksandWeightage = new List<MarksandWeightage>();

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetRankCardDetail", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_Reg_id", registrationid);

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        rankCard.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["Candidate_FullName"]);
                        rankCard.Gender = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        rankCard.FatherName = Convert.ToString(vds.Tables[0].Rows[0]["FatherName"]);
                        rankCard.MotherName = Convert.ToString(vds.Tables[0].Rows[0]["MotherName"]);
                        rankCard.Category = Convert.ToString(vds.Tables[0].Rows[0]["Reservation_Cat"]);
                        rankCard.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["BirthDate"]);
                        rankCard.Photo = Convert.ToString(vds.Tables[0].Rows[0]["photo"]);
                        rankCard.DateFromTo = Convert.ToString(vds.Tables[0].Rows[0]["Date_from_to"]);
                        rankCard.HighestQualification = Convert.ToString(vds.Tables[0].Rows[0]["QualifyingExam"]);
                        rankCard.Reg_id = Convert.ToString(vds.Tables[0].Rows[0]["Reg_Id"]);
                        rankCard.GramPanchayatweightage = Convert.ToString(vds.Tables[0].Rows[0]["Panch_Weightage"]);

                    }
                    if (vds.Tables[1].Rows.Count > 0)
                    {
                        academicDetails = (from DataRow row in vds.Tables[1].Rows
                                           select new AcademicDetail
                                           {
                                               ExamPassed = Convert.ToString(row["ExamPassed"]),
                                           }).ToList();
                    }

                    if (vds.Tables[2].Rows.Count > 0)
                    {
                        objMarksandWeightage = (from DataRow row in vds.Tables[2].Rows
                                                select new MarksandWeightage
                                                {
                                                    Qualification = Convert.ToString(row["Qualification"]),
                                                    MarksWeightage = Convert.ToString(row["weightageofMarks"]),
                                                    EducationWeightage = Convert.ToString(row["Twelth_Weightage"]),
                                                    WidowWeightage = Convert.ToString(row["Widow_Orphan_weightage"]),
                                                    TotalMarks = Convert.ToString(row["totalmarks_weightage"]),
                                                    PanchayatWeightage = Convert.ToString(row["Panch_WeightagePercentage"]),



                                                }).ToList();
                    }
                    rankCard.academicDetails = academicDetails;
                    rankCard.marksandWeightages = objMarksandWeightage;

                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFee.RankCard()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return rankCard;
        }
        public string SaveFeeModuleCollege(FeeModule feeModule)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveCandidatePaidFeeCollege", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes         q   
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeModule.RegistrationId);
                vadap.SelectCommand.Parameters.AddWithValue("P_feepaid", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_TotalFee", feeModule.TotalFee);
                vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", feeModule.PaymentTransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_Counselling", feeModule.Counselling);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SaveFeeModuleCollege()" + feeModule.RegistrationId);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        //public DataSet GetAdmissionDetailForShift(string RegId)
        //{
        //    DataSet result = new DataSet();
        //    try
        //    {
        //        if (connection.State == ConnectionState.Closed)
        //        {
        //            connection.Open();
        //        }
        //        DataSet vds = new DataSet();
        //        MySqlDataAdapter vadap = new MySqlDataAdapter();
        //        vadap = new MySqlDataAdapter("FreezeCourseCombination", connection);
        //        vadap.SelectCommand.Parameters.AddWithValue("@p_SessionId", RegId);
        //        vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
        //        vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        vadap.Fill(vds);
        //        if (connection.State == ConnectionState.Open)
        //        {
        //            connection.Close();
        //        }
        //        if (vds.Tables.Count > 0)
        //        {
        //            result = vds;
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.RegistrationFeeDB.[HttpGet] GetAdmissionDetailForShift()");
        //        return null;
        //    }
        //}

        public string SaveFeeofSCCandidate(string Regid, string CollegeId, string PaymentTransactionId)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SaveAdmissionZeroFee", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_Regid", Regid);
                vadap.SelectCommand.Parameters.AddWithValue("P_CollegeId", CollegeId);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_PaymentTransactionId", PaymentTransactionId);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);
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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SaveFeeofSCCandidate()" + Regid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }

        #region for Second Year Quarterly Fee
        public string SecondYearQuarterFeeSuccess(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseDataQ_2021", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", feeresponse.merchant_param3);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SecondYearQuarterFeeSuccess()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        public string SecondYearQuarterFeeFailure(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseData_cancel_2021", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);

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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.SecondYearQuarterFeeFailure()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        #endregion

        #region for Old Session Quarterly Fee
        public string OldSessionQuarterFeeSuccess(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseDataQTR", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("P_collegeid", feeresponse.merchant_param3);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);


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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.OldSessionQuarterFeeSuccess()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        public string OldSessionQuarterFeeFailure(PaymentResponse feeresponse)
        {
            string result = "0";

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            try
            {
                DataTable vds = new DataTable();
                MySqlDataAdapter vadap = new MySqlDataAdapter("SavePaymentResponseData_cancel_QTR", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("P_reg_id", feeresponse.merchant_param1);
                vadap.SelectCommand.Parameters.AddWithValue("P_tracking_id", feeresponse.tracking_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_bank_ref_no", feeresponse.bank_ref_no);
                vadap.SelectCommand.Parameters.AddWithValue("P_order_status", feeresponse.order_status);
                vadap.SelectCommand.Parameters.AddWithValue("P_payment_mode", feeresponse.payment_mode);
                vadap.SelectCommand.Parameters.AddWithValue("P_card_name", feeresponse.card_name);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_code", feeresponse.status_code);
                vadap.SelectCommand.Parameters.AddWithValue("P_status_message", feeresponse.status_message);
                vadap.SelectCommand.Parameters.AddWithValue("P_trans_date", feeresponse.trans_date);
                vadap.SelectCommand.Parameters.AddWithValue("P_createuser", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("P_ipaddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("P_transactionid", feeresponse.order_id);
                vadap.SelectCommand.Parameters.AddWithValue("P_amount", feeresponse.amount);
                vadap.SelectCommand.Parameters.AddWithValue("v_counselling", Counselling);

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
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.RegistrationFeeDB.OldSessionQuarterFeeFailure()" + feeresponse.order_id);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return result;
        }
        #endregion


    }
}
