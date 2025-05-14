using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using HigherEducation.BusinessLayer;
using System.Reflection.Emit;
/// <summary>
/// Summary description for SMS
/// </summary>
public class SMS
{
    static string vconnHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
    public SMS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void SendSMS(string mobileNo, string msgText)
    {
        StreamReader reader = null;
        HttpWebResponse response = null; ;
        try
        {
            string req = "http://smsgateway.spicedigital.in/MessagingGateway/MessagePush?username=DeptOEdu&password=Dept@1234&messageType=text&mobile=" + mobileNo + "&senderId=DHEEDU&message=" + msgText;
            ServicePointManager.ServerCertificateValidationCallback = delegate (Object obj1, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return (true); };
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(req);
            response = (HttpWebResponse)request.GetResponse();
            String ver = response.ProtocolVersion.ToString();
             reader = new StreamReader(response.GetResponseStream());
            string APIResponse = Convert.ToString(reader.ReadToEnd());
            RSSaveSmsHistory(mobileNo, msgText, APIResponse);


        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
        finally
        {
            if (reader != null)
                reader.Close();
            if (response != null)
                response.Close();
        }
    }
    public static void SendEmail(string Email_ID, string Email_Subject, string Email_Body)
    {
        string result = string.Empty;
    RerunTheCode:

        try
        {
            string APIurl = "https://ws1.edisha.gov.in/api/Values/Send_ITI_Mail?Email_ID=" + Email_ID + "&Email_Subject=" + Email_Subject + "&Email_Body=" + Email_Body + "";
            HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create(APIurl));
            req.Method = "POST";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            req.ContentType = "application/text";
            req.ProtocolVersion = HttpVersion.Version11;

            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
            }
            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }



        }
        catch (WebException wex)
        {
            if (wex.Message == "The underlying connection was closed: An unexpected error occurred on a send.")
            {
                goto RerunTheCode;
            }


        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }

    }
    public static void RSSaveSmsHistory(string MobileNo, string SmsText, string APIResponse)
    {
        MySqlConnection con = new MySqlConnection(vconnHE);
        try
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }

            using (MySqlCommand cmd = new MySqlCommand("RSSaveSmsHistory", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@PMobileNo", MobileNo);
                cmd.Parameters.AddWithValue("@PSmsText", SmsText);
                cmd.Parameters.AddWithValue("@PAPIResponse", APIResponse);
                cmd.ExecuteNonQuery();
                cmd.Dispose();


            }

        }
        catch (Exception ex)
        {
            clsLogger.ExceptionError = ex.Message;
            clsLogger.ExceptionPage = "SMS.CS";
            clsLogger.ExceptionMsg = "RSSaveSmsHistory";
            clsLogger.ExceptionDetail = "MobileNo" + MobileNo + "APIResponse" + APIResponse;
            clsLogger.SaveException();
        }
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
    }
}
