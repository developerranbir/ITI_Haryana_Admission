using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace HigherEducation.BAL
{
    public static class AgriSMS
    {
        class MyPolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
            {
                return true;
            }
        }

        private static string encryptedPasswod(String password)
        {
            byte[] encPwd = Encoding.UTF8.GetBytes(password);
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
            byte[] pp = sha1.ComputeHash(encPwd);
            System.Text.Encoding.UTF8.GetString(pp);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in pp)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private static string hashGenerator(String Username, String sender_id, String message, String secure_key)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Username).Append(sender_id).Append(message).Append(secure_key);
            byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");
            byte[] sec_key = sha1.ComputeHash(genkey);
            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < sec_key.Length; i++)
            {
                sb1.Append(sec_key[i].ToString("x2"));
            }
            return sb1.ToString();
        }

        public static string sendOTPMSG(string mobileNo, string message, string templateId)
        {
            string username = WebConfigurationManager.AppSettings["aSMSusrName"];
            string password = WebConfigurationManager.AppSettings["aSMSpass"];
            string senderid = WebConfigurationManager.AppSettings["aSMSsenderID"];
            string secureKey = WebConfigurationManager.AppSettings["aSMSsecurityKey"];
            string apiurl = WebConfigurationManager.AppSettings["aSMSURL"];
            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl);
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
            request.Method = "POST";
            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String encryptedPassword = encryptedPasswod(password);
            String key = hashGenerator(username.Trim(), senderid.Trim(), message.Trim(), secureKey.Trim());
            String smsservicetype = "otpmsg";
            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                "&content=" + HttpUtility.UrlEncode(message.Trim()) +
                "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +
                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
                "&key=" + HttpUtility.UrlEncode(key.Trim()) +
                "&templateid=" + HttpUtility.UrlEncode(templateId.Trim());

            byte[] byteArray = Encoding.ASCII.GetBytes(query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();

            String Status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            String responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            SMS.RSSaveSmsHistory(mobileNo, message, responseFromServer);
            return responseFromServer;
           
        }

        public static string sendSingleSMS(String mobileNo, String message, string templateId)
        {
            string username = WebConfigurationManager.AppSettings["aSMSusrName"];
            string password = WebConfigurationManager.AppSettings["aSMSpass"];
            string senderid = WebConfigurationManager.AppSettings["aSMSsenderID"];
            string secureKey = WebConfigurationManager.AppSettings["aSMSsecurityKey"];
            string apiurl = WebConfigurationManager.AppSettings["aSMSURL"];

            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl);
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;
            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String encryptedPassword = encryptedPasswod(password);
            String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), message.Trim(), secureKey.Trim());
            String smsservicetype = "singlemsg"; //For single message.

            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +

                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +

                "&content=" + HttpUtility.UrlEncode(message.Trim()) +

                "&mobileno=" + HttpUtility.UrlEncode(mobileNo) +

                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
              "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +
              "&templateid=" + HttpUtility.UrlEncode(templateId.Trim());



            byte[] byteArray = Encoding.ASCII.GetBytes(query);

            request.ContentType = "application/x-www-form-urlencoded";

            request.ContentLength = byteArray.Length;



            dataStream = request.GetRequestStream();

            dataStream.Write(byteArray, 0, byteArray.Length);

            dataStream.Close();

            WebResponse response = request.GetResponse();

            String Status = ((HttpWebResponse)response).StatusDescription;

            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            String responseFromServer = reader.ReadToEnd();

            reader.Close();

            dataStream.Close();

            response.Close();
            SMS.RSSaveSmsHistory(mobileNo, message, responseFromServer);
            return responseFromServer;
        }

        public static string sendUnicodeSMS(String mobileNos, String Unicodemessage, string templateId)
        {
            string username = WebConfigurationManager.AppSettings["aSMSusrName"];
            string password = WebConfigurationManager.AppSettings["aSMSpass"];
            string senderid = WebConfigurationManager.AppSettings["aSMSsenderID"];
            string secureKey = WebConfigurationManager.AppSettings["aSMSsecurityKey"];
            string apiurl = WebConfigurationManager.AppSettings["aSMSURL"];

            Stream dataStream;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //forcing .Net framework to use TLSv1.2

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiurl);
            request.ProtocolVersion = HttpVersion.Version10;
            request.KeepAlive = false;
            request.ServicePoint.ConnectionLimit = 1;

            ((HttpWebRequest)request).UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";

            request.Method = "POST";

            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            String U_Convertedmessage = "";

            foreach (char c in Unicodemessage)
            {
                int j = (int)c;
                String sss = "&#" + j + ";";
                U_Convertedmessage = U_Convertedmessage + sss;
            }
            String encryptedPassword = encryptedPasswod(password);
            String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), U_Convertedmessage.Trim(), secureKey.Trim());


            String smsservicetype = "unicodemsg"; // for unicode msg
            String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                "&content=" + HttpUtility.UrlEncode(U_Convertedmessage.Trim()) +
                "&bulkmobno=" + HttpUtility.UrlEncode(mobileNos) +
                "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
                "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +
              "&templateid=" + HttpUtility.UrlEncode(templateId.Trim());


            byte[] byteArray = Encoding.ASCII.GetBytes(query);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            String Status = ((HttpWebResponse)response).StatusDescription;
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            String responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }

    }
}