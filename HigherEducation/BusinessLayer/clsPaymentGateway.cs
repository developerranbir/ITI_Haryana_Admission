using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HigherEducation.BusinessLayer
{

    public class clsPaymentGateway
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        string ConnectionString = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;

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

        //IndusInd
        public string inv_no { get; set; }
        public string trn_ref { get; set; }
        public string request_id { get; set; }
        public string status { get; set; }

        public DataTable SaveHDFCPGData(clsPaymentGateway PGData)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlParameter[] p = new MySqlParameter[33];
                //p[0] = new MySqlParameter("@Porder_id", PGData.order_id);
                p[0] = new MySqlParameter("@Porder_id", PGData.merchant_param1);
                p[1] = new MySqlParameter("@Ptracking_id", PGData.tracking_id);
                p[2] = new MySqlParameter("@Pbank_ref_no", PGData.bank_ref_no);
                p[3] = new MySqlParameter("@Porder_status", PGData.order_status);
                p[4] = new MySqlParameter("@Pfailure_message", PGData.failure_message);
                p[5] = new MySqlParameter("@Ppayment_mode", PGData.payment_mode);
                p[6] = new MySqlParameter("@Pcard_name", PGData.card_name);
                p[7] = new MySqlParameter("@Pstatus_code", PGData.status_code);
                p[8] = new MySqlParameter("@Pstatus_message", PGData.status_message);
                p[9] = new MySqlParameter("@Pcurrency", PGData.currency);
                p[10] = new MySqlParameter("@Pamount", PGData.amount);
                p[11] = new MySqlParameter("@Pbilling_name", PGData.billing_name);
                p[12] = new MySqlParameter("@Pbilling_address", PGData.billing_address);
                p[13] = new MySqlParameter("@Pbilling_city", PGData.billing_city);
                p[14] = new MySqlParameter("@Pbilling_state", PGData.billing_state);
                p[15] = new MySqlParameter("@Pbilling_tel", PGData.billing_tel);
                p[16] = new MySqlParameter("@Pbilling_email", PGData.billing_email);
                //p[17] = new MySqlParameter("@Pmerchant_param1", PGData.merchant_param1);
                p[17] = new MySqlParameter("@Pmerchant_param1", PGData.order_id);
                p[18] = new MySqlParameter("@Pmerchant_param2", PGData.merchant_param2);
                p[19] = new MySqlParameter("@Pmerchant_param3", PGData.merchant_param3);
                p[20] = new MySqlParameter("@Pmerchant_param4", PGData.merchant_param4);
                p[21] = new MySqlParameter("@Pmerchant_param5", PGData.merchant_param5);
                p[22] = new MySqlParameter("@Pvault", PGData.vault);
                p[23] = new MySqlParameter("@Poffer_type", PGData.offer_type);
                p[24] = new MySqlParameter("@Poffer_code", PGData.offer_code);
                p[25] = new MySqlParameter("@Pdiscount_value", PGData.discount_value);
                p[26] = new MySqlParameter("@Pmer_amount", PGData.mer_amount);
                p[27] = new MySqlParameter("@Peci_value", PGData.eci_value);
                p[28] = new MySqlParameter("@Pretry", PGData.retry);
                p[29] = new MySqlParameter("@Presponse_code", PGData.response_code);
                p[30] = new MySqlParameter("@Pbilling_notes", PGData.billing_notes);
                p[31] = new MySqlParameter("@Ptrans_date", PGData.trans_date);
                p[32] = new MySqlParameter("@Pbin_country", PGData.bin_country);
                dt = MySqlConnect.DBActions.ExecuteDataTable(ConnectionString, CommandType.StoredProcedure, "RSSavehdfcpgdetail", p);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveHDFCPGData()");
            }
            return dt;
        }

        public DataTable SaveIndusIndPGData(clsPaymentGateway PGData)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlParameter[] p = new MySqlParameter[5];

                p[0] = new MySqlParameter("@Pinv_no", PGData.inv_no);
                p[1] = new MySqlParameter("@Ptrn_ref", PGData.trn_ref);
                p[2] = new MySqlParameter("@Pamount", PGData.amount);
                p[3] = new MySqlParameter("@Prequest_id", PGData.request_id);
                p[4] = new MySqlParameter("@Pstatus", PGData.status);

                dt = MySqlConnect.DBActions.ExecuteDataTable(ConnectionString, CommandType.StoredProcedure, "RSSaveInduspgdetail", p);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SaveIndusIndPGData()");
            }
            return dt;
        }
        public DataTable SavePGResponse(string responseText, string regid, string responseBank)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlParameter[] p = new MySqlParameter[3];
                p[0] = new MySqlParameter("@p_responsetext", responseText);
                p[1] = new MySqlParameter("@p_reg_id", regid);
                p[2] = new MySqlParameter("@p_responseBank", responseBank);
                dt = MySqlConnect.DBActions.ExecuteDataTable(ConnectionString, CommandType.StoredProcedure, "RSSavePG_response", p);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.SavePGResponse()");
            }
            return dt;
        }
        public string EncryptString(string plainText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

            // Convert the plainText string into a byte array
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            // Encrypt the input plaintext string
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);

            // Complete the encryption process
            cryptoStream.FlushFinalBlock();

            // Convert the encrypted data from a MemoryStream to a byte array
            byte[] cipherBytes = memoryStream.ToArray();

            // Close both the MemoryStream and the CryptoStream
            memoryStream.Close();
            cryptoStream.Close();

            // Convert the encrypted byte array to a base64 encoded string
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            // Return the encrypted data as a string
            return cipherText;
        }

        public string DecryptString(string cipherText, byte[] key, byte[] iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            byte[] aesKey = new byte[32];
            Array.Copy(key, 0, aesKey, 0, 32);
            encryptor.Key = aesKey;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            // Will contain decrypted plaintext
            string plainText = String.Empty;

            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.DecryptString()");
            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }

            // Return the decrypted data as a string
            return plainText;
        }
    }


}