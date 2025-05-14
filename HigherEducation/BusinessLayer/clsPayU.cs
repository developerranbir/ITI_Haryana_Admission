using System;
using System.IO;
using System.Web;
using System.Net;
using System.Text;
using System.Data;
using EncryptDecryptPG;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;

namespace PayUIntegration
{
    public class clsPayU
    {
        CultureInfo enGB = new CultureInfo("en-GB");

        private string _EdishaXtnID;
        public string EdishaXtnID
        {
            get { return _EdishaXtnID; }
            set { _EdishaXtnID = value; }
        }
        private string _PayUResponse;
        public string PayUResponse
        {
            get { return _PayUResponse; }
            set { _PayUResponse = value; }
        }
        private string _ResponseSource;
        public string ResponseSource
        {
            get { return _ResponseSource; }
            set { _ResponseSource = value; }
        }
        private string _ApplicantName;
        public string ApplicantName
        {
            get { return _ApplicantName; }
            set { _ApplicantName = value; }
        }
        private string _ApplicantFHName;
        public string ApplicantFHName
        {
            get { return _ApplicantFHName; }
            set { _ApplicantFHName = value; }
        }
        private string _ApplicantAddress;
        public string ApplicantAddress
        {
            get { return _ApplicantAddress; }
            set { _ApplicantAddress = value; }
        }
        private string _MobileNo;
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        private string _EmailId;
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        private string _PANNo;
        public string PANNo
        {
            get { return _PANNo; }
            set { _PANNo = value; }
        }
        private decimal _TotalAmount;
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set { _TotalAmount = value; }
        }
        private string _Address1_PROPNO;
        public string Address1_PROPNO
        {
            get { return _Address1_PROPNO; }
            set { _Address1_PROPNO = value; }
        }
        private string _Address2_COLNAME;
        public string Address2_COLNAME
        {
            get { return _Address2_COLNAME; }
            set { _Address2_COLNAME = value; }
        }
        private string _City_MCNAME;
        public string City_MCNAME
        {
            get { return _City_MCNAME; }
            set { _City_MCNAME = value; }
        }
        private string _udf1_PID;
        public string udf1_PID
        {
            get { return _udf1_PID; }
            set { _udf1_PID = value; }
        }
        private string _udf2_MCODE;
        public string udf2_MCODE
        {
            get { return _udf2_MCODE; }
            set { _udf2_MCODE = value; }
        }
        private string _udf3_YEAR;
        public string udf3_YEAR
        {
            get { return _udf3_YEAR; }
            set { _udf3_YEAR = value; }
        }
        private string _udf4;
        public string udf4
        {
            get { return _udf4; }
            set { _udf4 = value; }
        }
        private string _AppName;
        public string AppName
        {
            get { return _AppName; }
            set { _AppName = value; }
        }
        private string _AppCode;
        public string AppCode
        {
            get { return _AppCode; }
            set { _AppCode = value; }
        }
        private string _MKEY;
        public string MKEY
        {
            get { return _MKEY; }
            set { _MKEY = value; }
        }
        private string _SALT;
        public string SALT
        {
            get { return _SALT; }
            set { _SALT = value; }
        }
        private string _MihpayID;
        public string MihpayID
        {
            get { return _MihpayID; }
            set { _MihpayID = value; }
        }
        private string _Mode;
        public string Mode
        {
            get { return _Mode; }
            set { _Mode = value; }
        }
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private decimal _Net_Amount_Debit;
        public decimal Net_Amount_Debit
        {
            get { return _Net_Amount_Debit; }
            set { _Net_Amount_Debit = value; }
        }
        private string _payment_source;
        public string Payment_Source
        {
            get { return _payment_source; }
            set { _payment_source = value; }
        }
        private string _Issuing_Bank;
        public string Issuing_Bank
        {
            get { return _Issuing_Bank; }
            set { _Issuing_Bank = value; }
        }
        private string _BankCode;
        public string BankCode
        {
            get { return _BankCode; }
            set { _BankCode = value; }
        }
        private string _PG_Type;
        public string PG_Type
        {
            get { return _PG_Type; }
            set { _PG_Type = value; }
        }
        private string _Bank_Ref_No;
        public string Bank_Ref_No
        {
            get { return _Bank_Ref_No; }
            set { _Bank_Ref_No = value; }
        }
        private string _Name_On_Card;
        public string Name_On_Card
        {
            get { return _Name_On_Card; }
            set { _Name_On_Card = value; }
        }
        private string _CardNo;
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }
        private string _Card_Type;
        public string Card_Type
        {
            get { return _Card_Type; }
            set { _Card_Type = value; }
        }
        private string _UnMappedStatus;
        public string UnMappedStatus
        {
            get { return _UnMappedStatus; }
            set { _UnMappedStatus = value; }
        }
        private string _Error;
        public string Error
        {
            get { return _Error; }
            set { _Error = value; }
        }
        private string _Error_Msg;
        public string Error_Msg
        {
            get { return _Error_Msg; }
            set { _Error_Msg = value; }
        }
        private string _addedon;
        public string addedon
        {
            get { return _addedon; }
            set { _addedon = value; }
        }

        private string _udf4_PTaxFlag;
        public string udf4_PTaxFlag
        {
            get { return _udf4_PTaxFlag; }
            set { _udf4_PTaxFlag = value; }
        }

        private string _udf5_ServiceName;
        public string udf5_ServiceName
        {
            get { return _udf5_ServiceName; }
            set { _udf5_ServiceName = value; }
        }

        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        MySqlConnection ConnectionString = new MySqlConnection(ConStr);
        //string ConnectionString = "Data Source=10.145.32.152;port=3306;Initial Catalog=higher_edu;User Id=app1;password=passWord@2020";
       // string ConnectionString = "Data Source=localhost;port=3306;Initial Catalog=higher_edu;User Id=root;password=admin@123";

        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            try
            {
                //Set a name for the form
                string formID = "PostForm";
                //Build the form using the specified data to be posted.
                StringBuilder strForm = new StringBuilder();
                strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
                foreach (System.Collections.DictionaryEntry key in data)
                {
                    strForm.Append("<input type=\"hidden\" name=\"" + key.Key + "\" value=\"" + key.Value + "\">");
                }
                strForm.Append("</form>");
                //Build the JavaScript which will do the Posting operation.
                StringBuilder strScript = new StringBuilder();
                strScript.Append("<script language='javascript'>");
                strScript.Append("var v" + formID + " = document." + formID + ";");
                strScript.Append("v" + formID + ".submit();");
                strScript.Append("</script>");
                //Return the form and the script concatenated.
                //(The order is important, Form then JavaScript)
                return strForm.ToString() + strScript.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Generatehash512(string text)
        {
            try
            {
                string hex = "";
                byte[] hashValue;
                byte[] message = Encoding.UTF8.GetBytes(text);
                UnicodeEncoding UE = new UnicodeEncoding();
                SHA512Managed hashString = new SHA512Managed();
                hashValue = hashString.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hex += String.Format("{0:x2}", x);
                }
                return hex;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string[] GetTxnStatus(string ReceiptNo, string salt, string key)
        {
            HttpContext.Current.Session["VerifiedArray"] = null;
            string[] strstatus = new string[6];
            try
            {
                string Url = ConfigurationManager.AppSettings["Verify_URL"].ToString();
                string method = "verify_payment";
                string toHash = key + "|" + method + "|" + ReceiptNo + "|" + salt;
                string Hashed = Generatehash512(toHash);
                string postString = "key=" + key + "&command=" + method + "&hash=" + Hashed + "&var1=" + ReceiptNo;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.DefaultConnectionLimit = 9999;
                WebRequest myWebRequest = WebRequest.Create(Url);
                myWebRequest.Method = "POST";
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                myWebRequest.ContentType = "application/x-www-form-urlencoded";
                myWebRequest.Timeout = 180000;
                StreamWriter requestWriter = new StreamWriter(myWebRequest.GetRequestStream());
                requestWriter.Write(postString);
                requestWriter.Close();
                StreamReader responseReader = new StreamReader(myWebRequest.GetResponse().GetResponseStream());
                WebResponse myWebResponse = myWebRequest.GetResponse();
                Stream ReceiveStream = myWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                _PayUResponse = readStream.ReadToEnd();
                var ParsedJSON = JObject.Parse(_PayUResponse);
                string[] strArr = _PayUResponse.Replace("{", "").Replace("}", "").Split(',');
                HttpContext.Current.Session["VerifiedArray"] = strArr;
                string status = ParsedJSON.SelectToken("status").Value<string>();
                if (status == "1")
                {
                    strstatus[0] = "1";
                    strstatus[1] = ParsedJSON.SelectToken("transaction_details." + ReceiptNo + ".unmappedstatus").Value<string>();
                    if (Convert.ToString(strstatus[1]) != "captured")
                    {
                        strstatus[2] = ParsedJSON.SelectToken("transaction_details." + ReceiptNo + ".mihpayid").Value<string>();
                        strstatus[3] = ParsedJSON.SelectToken("transaction_details." + ReceiptNo + ".amt").Value<string>();
                        strstatus[4] = ParsedJSON.SelectToken("transaction_details." + ReceiptNo + ".field9").Value<string>();
                        strstatus[5] = ParsedJSON.SelectToken("transaction_details." + ReceiptNo + ".status").Value<string>();
                    }
                    else
                    {
                        strstatus[2] = "";
                        strstatus[3] = "";
                        strstatus[4] = "";
                        strstatus[5] = "";
                    }
                }
                else
                {
                    strstatus[0] = "0";
                    strstatus[1] = "Failed";
                    strstatus[2] = "";
                    strstatus[3] = "";
                    strstatus[4] = "";
                    strstatus[5] = "";
                }
            }
            catch (Exception ex)
            {
                strstatus[0] = "2";
                strstatus[1] = "Exception";
                strstatus[2] = "";
                strstatus[3] = "";
                strstatus[4] = "";
                strstatus[5] = "";

            }
            return strstatus;
        }
        public string TransferToPayGateWay()
        {
            try
            {
                string[] hashVarsSeq;
                string hash_string = string.Empty;
                string txnid1 = string.Empty;
                string txnid2 = string.Empty;
                string hash1 = string.Empty;
                string action1 = string.Empty;
                hash_string = "";
                hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
                foreach (string hash_var in hashVarsSeq)
                {
                    if (hash_var == "key")
                    {
                        hash_string = hash_string + _MKEY;
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "txnid")
                    {
                        hash_string = hash_string + _EdishaXtnID;
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "amount")
                    {
                        hash_string = hash_string + _TotalAmount; //Amount
                        hash_string = hash_string + '|';
                    }
                }
                hash_string = hash_string + _AppCode + "|" + _ApplicantName + "|" + _EmailId + "|" + _udf1_PID + "|" + _udf2_MCODE + "|" + _udf3_YEAR + "|" + _udf4_PTaxFlag + "|" + _udf5_ServiceName + "|||||";
                hash_string = hash_string + '|';
                hash_string += _SALT;// appending SALT
                hash1 = Generatehash512(hash_string).ToLower();
                action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";// setting URL        
                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in hash table for data post
                data.Add("hash", hash1);
                data.Add("txnid", _EdishaXtnID);
                data.Add("key", _MKEY);
                data.Add("amount", _TotalAmount);
                data.Add("firstname", _ApplicantName);
                data.Add("email", _EmailId);
                data.Add("phone", _MobileNo);
                data.Add("productinfo", _AppCode);
                data.Add("surl", ConfigurationManager.AppSettings["SuccessURL"].ToString());
                data.Add("furl", ConfigurationManager.AppSettings["FailureURL"].ToString());
                data.Add("curl", ConfigurationManager.AppSettings["CancelURL"].ToString());
                data.Add("address1", _ApplicantAddress);
                data.Add("address2", _City_MCNAME);
                data.Add("city", _City_MCNAME);
                data.Add("state", "Haryana");
                data.Add("country", "India");
                data.Add("udf1", _udf1_PID);
                data.Add("udf2", _udf2_MCODE);
                data.Add("udf3", _udf3_YEAR);
                data.Add("udf4", _udf4_PTaxFlag);
                data.Add("udf5", _udf5_ServiceName);
                string strForm = PreparePOSTForm(action1, data);
                return strForm;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public DataTable SavePGDetail()
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlParameter[] p = new MySqlParameter[29];
                p[0] = new MySqlParameter("@PMihpayID", _MihpayID);
                p[1] = new MySqlParameter("@Pmode", _Mode);
                p[2] = new MySqlParameter("@Pstatus", _Status);
                p[3] = new MySqlParameter("@Ptxnid", _EdishaXtnID);
                p[4] = new MySqlParameter("@Pamount", _TotalAmount);
                p[5] = new MySqlParameter("@Pnet_amount_debit", _Net_Amount_Debit);
                p[6] = new MySqlParameter("@Pproductinfo", _AppCode);
                p[7] = new MySqlParameter("@Pfirstname", _ApplicantName);
                p[8] = new MySqlParameter("@Paddress1", _Address1_PROPNO);
                p[9] = new MySqlParameter("@Pcity", _City_MCNAME);
                p[10] = new MySqlParameter("@Pemail", _EmailId);
                p[11] = new MySqlParameter("@Pphone", _MobileNo);
                p[12] = new MySqlParameter("@Pbankcode", _BankCode);
                p[13] = new MySqlParameter("@Pissuing_bank", _Issuing_Bank);
                p[14] = new MySqlParameter("@Pbank_ref_num", _Bank_Ref_No);
                p[15] = new MySqlParameter("@Ppg_type", _PG_Type);
                p[16] = new MySqlParameter("@Ppayment_source", _payment_source);
                p[17] = new MySqlParameter("@Pcardnum", _CardNo);
                p[18] = new MySqlParameter("@Pcard_type", _Card_Type);
                p[19] = new MySqlParameter("@Pname_on_card", _Name_On_Card);
                p[20] = new MySqlParameter("@Punmappedstatus", _UnMappedStatus);
                p[21] = new MySqlParameter("@Perror", _Error);
                p[22] = new MySqlParameter("@Perror_Message", _Error_Msg);
                p[23] = new MySqlParameter("@Paddedon", _addedon);
                p[24] = new MySqlParameter("@Pudf1_CourseID", _udf1_PID);
                p[25] = new MySqlParameter("@Pudf2_CollegeID", _udf2_MCODE);
                p[26] = new MySqlParameter("@Pudf3_Session", _udf3_YEAR);
                p[27] = new MySqlParameter("@Pudf5_CombinationID", _udf5_ServiceName);
                p[28] = new MySqlParameter("@PSource", _ResponseSource);
                dt = MySqlConnect.DBActions.ExecuteDataTable(ConnectionString, CommandType.StoredProcedure, "RSSavePayUDetail", p);
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        public void SavePayUResponse()
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlParameter[] p = new MySqlParameter[2];
                p[0] = new MySqlParameter("@PPaymentTransID", _EdishaXtnID);
                p[1] = new MySqlParameter("@PPayUResponse", _PayUResponse);
                dt = MySqlConnect.DBActions.ExecuteDataTable(ConnectionString, CommandType.StoredProcedure, "RSSavePayUResponse", p);
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class PayUResponse
    {
        public static string MihpayID { get; set; }
        public static string Mode { get; set; }
        public static string Status { get; set; }
        public static string EdishXtnID { get; set; }
        public static string TotalAmount { get; set; }
        public static string Net_Amount_Debit { get; set; }
        public static string Error_Msg { get; set; }
        public static string AppCode { get; set; }
        public static string productinfo { get; set; }
        public static string ApplicantName { get; set; }
        public static string firstname { get; set; }
        public static string Address1 { get; set; }
        public static string Address2 { get; set; }
        public static string City { get; set; }
        public static string email { get; set; }
        public static string phone { get; set; }
        public static string MobileNo { get; set; }
        public static string BankCode { get; set; }
        public static string Error { get; set; }
        public static string Bank_Ref_No { get; set; }
        public static string PG_Type { get; set; }
        public static string Payment_Source { get; set; }
        public static string CardNo { get; set; }
        public static string UnMappedStatus { get; set; }
        public static string Issuing_Bank { get; set; }
        public static string Card_Type { get; set; }
        public static string Name_On_Card { get; set; }
        public static string PID { get; set; }
        public static string MCode { get; set; }
        public static string FinYear { get; set; }
        public static string ResponseSource { get; set; }
        public static string AddedOn { get; set; }
        public static string MerchantID { get; set; }
        public static string Hash { get; set; }
        public static string udf1 { get; set; }
        public static string udf2 { get; set; }
        public static string udf3 { get; set; }
        public static string udf4 { get; set; }
        public static string udf5 { get; set; }
        public static string error_code { get; set; }
        public static string field2 { get; set; }
        public static string field9 { get; set; }
        public static string amount { get; set; }
        public static string txnid { get; set; }
        public static string SALT { get; set; }
        public static string key { get; set; }
        public static string ServiceName { get; set; }
        public static string MCName { get; set; }
        public static string PTax_Flag { get; set; }
    }
}