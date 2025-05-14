using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using HigherEducation.BAL;
using HigherEducation.BusinessLayer;
using HigherEducation.DataAccess;
using HigherEducation.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Ubiety.Dns.Core.Records;


namespace HigherEducation.DHE
{
    public partial class frmPaymentStatus : System.Web.UI.Page
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducationR"].ToString());
        string apiurl = "https://api.ccavenue.com/apis/servlet/DoWebTrans";

        string workingKey = "300802134DE1EF36C19F28854E040C21";
        string strAccessCode = "AVCA11JG46BU26ACUB";


        //string apiurl = "https://testapi.ccavenue.com/apis/servlet/DoWebTrans";
        //string workingKey = "2FC471E092B572DEE544CD5367D3C411";
        //string strAccessCode = "AVPI03II96BD44IPDB";

        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            // eSessionMgmt.checkreferer();
            clsCancelStudentRegId.CheckSession();
            if (string.IsNullOrEmpty(Convert.ToString(Session["CollegeId"])))
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/dhe/frmlogin.aspx", true);
                return;
            }
            if (!Page.IsPostBack)
            {

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }

        
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (txtRegId.Text == "") { return; }
            //updatePaymentStatus(apiurl, workingKey, strAccessCode, "");

            updatePaymentStatus_SINGLE(apiurl, workingKey, strAccessCode, "", txtRegId.Text, ddlPaymentType.SelectedItem.Text.ToLower());
            

            using (MySqlCommand cmd = new MySqlCommand("getPaymentStatus", vconnHE))
            {
                GdvStatus.DataSource = null;
                GdvStatus.DataBind();

                if (vconnHE.State == ConnectionState.Closed) { vconnHE.Open(); }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@regid", txtRegId.Text);
                cmd.Parameters.AddWithValue("@feetype", ddlPaymentType.SelectedItem.Value);
                using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        adp.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GdvStatus.DataSource = dt;
                            GdvStatus.DataBind();

                        }
                        else
                        {
                            GdvStatus.DataSource = null;
                            GdvStatus.DataBind();
                        }
                    }
                }
                vconnHE.Close();
            }


        }

        protected void GdvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text.ToLower() == "success" || e.Row.Cells[0].Text.ToLower() == "shipped")
                {
                    e.Row.BackColor = Color.Green;
                    e.Row.ForeColor = Color.White;
                }
                else if (e.Row.Cells[0].Text.ToLower() == "initiated" || e.Row.Cells[0].Text.ToLower() == "awaited")
                {
                    e.Row.BackColor = Color.Yellow;
                }
                else
                {
                    e.Row.BackColor = Color.OrangeRed;
                    e.Row.ForeColor = Color.White;
                }


            }
        }
        static string postPaymentRequestToGateway(String queryUrl, String urlParam)
        {

            string message = "";
        RerunTheCode:
            try
            {
                StreamWriter myWriter = null;// it will open a http connection with provided url
                WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
                objRequest.Method = "POST";
                //objRequest.ContentLength = TranRequest.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(urlParam);//send data
                myWriter.Close();//closed the myWriter object
                                 // Getting Response
                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {
                    message = sr.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                //Console.WriteLine(wex);
                //Console.ReadLine();
                if (wex.Message == "The underlying connection was closed: An unexpected error occurred on a send.")
                {
                    goto RerunTheCode;
                }
            }
            catch (Exception exception)
            {
                //Console.WriteLine(exception);
                //Console.ReadLine();

            }
            return message;
        }

        static void updatePaymentStatus_SINGLE(String apiurl, String workingKey, String strAccessCode, String Year, string regid, string feetype)
{
        CCACrypto ccaCrypto = new CCACrypto();
            APIParam obj = new APIParam();
            clsDataAccess objHDFC = new clsDataAccess();
            DataTable dtUnpaid = new DataTable();
            String pYear = "";
            dtUnpaid = objHDFC.GetUnPaidStudentData_SINGLE(Year, regid, feetype);
            if (Year == "")
            {
                pYear = "Current";
            }
            else
            {
                pYear = Year;
            }
            //Console.WriteLine("Total Records of " + pYear + " Year" + Convert.ToString(dtUnpaid.Rows.Count));
            //Console.WriteLine("==========================================");
            foreach (DataRow row in dtUnpaid.Rows)
            {
                string DBResult = "";
                obj.order_no = Convert.ToString(row["PaymentTransID"]);
                obj.order_status = "Shipped";
                obj.from_date = "24-02-2023";

                obj.page_number = "1";
                Console.WriteLine("PaymentTransID " + Convert.ToString(row["PaymentTransID"]));
                string message = JsonConvert.SerializeObject(obj);
                message = ccaCrypto.Encrypt(message, workingKey);

                string authQueryUrlParam = "enc_request=" + message + "&access_code=" + strAccessCode + "&command=orderLookup&request_type=JSON&response_type=JSON&version=1.1";
                // string authQueryUrlParam = "enc_request=7f6b80868cfb695e02cc27086a38f6455cdecb9a16f1bab0d829dc1b4bac3b626761c32183c926de6f4a7d7042b33af6975c1611668a7259f8d5fa9aec5bbe9ee75cc8572826e3fd5ad9286705cf00bece5e73e2c6391f1e81c2ecfc88b2ce41bbd4a04e70c79ec5cd4408012946549bd3c20ab8a23865b466b93a8958d71a9d&access_code=AVXU32II59BE68UXEB&command=orderLookup&request_type=JSON&response_type=JSON&version=1.1";
                string timeStamp = (DateTime.Now).ToString();

                string APIResponse = postPaymentRequestToGateway(apiurl, authQueryUrlParam);
                //Console.WriteLine(APIResponse);
                //Console.Read();

                if (!string.IsNullOrEmpty(APIResponse))
                {
                    NameValueCollection Params = new NameValueCollection();
                    string[] segments = APIResponse.Split('&');
                    foreach (string seg in segments)
                    {
                        string[] parts = seg.Split('=');
                        if (parts.Length > 0)
                        {
                            string Key = parts[0].Trim();
                            string Value = parts[1].Trim();
                            Params.Add(Key, Value);
                        }
                    }
                    if (Params[0] == "0")
                    {
                        string DecryptAPIResponse = ccaCrypto.Decrypt(Params[1].ToString(), workingKey);
                        //Console.WriteLine("Decrypt Response" + DecryptAPIResponse);
                        // Console.Read();
                        try
                        {
                            Root myDeserializedObjList2 = new Root();
                            myDeserializedObjList2 = (Root)JsonConvert.DeserializeObject(DecryptAPIResponse, typeof(Root));
                            if (myDeserializedObjList2.page_count > 0)
                            {
                                if (myDeserializedObjList2.order_Status_List.Count > 0)
                                {
                                    if (myDeserializedObjList2.order_Status_List[0].order_status == "Shipped")
                                    {
                                        

                                        if (feetype.ToLower() == "admission")
                                        {
                                            // admission  fee
                                            DBResult = objHDFC.SaveHDFCPaymentResponse2(myDeserializedObjList2, Convert.ToString(row["Reg_id"]), myDeserializedObjList2.order_Status_List[0].order_no, Convert.ToString(row["College_id"]), Year);
                                            //Console.WriteLine("DB Response " + DBResult);

                                            if (DBResult == "1")
                                            {
                                                AgriSMS.sendSingleSMS(Convert.ToString(row["Mobile"]), "Dear Student, Receipt of your payment towards " + Convert.ToString(row["Reg_id"]) + " for ITI admission is confirmed. To check, please login at https://itiharyanaadmissions.nic.in Regards, SDIT Haryana", "1007867539276723247");
                                            }
                                        }
                                        else
                                        {
                                            // registration fee
                                         
                                            DBResult = objHDFC.SaveHDFCPaymentResponse2_admission(myDeserializedObjList2, Convert.ToString(row["Reg_id"]), myDeserializedObjList2.order_Status_List[0].order_no, Convert.ToString(row["College_id"]), Year);
                                            //Console.WriteLine("DB Response " + DBResult);



                                        }
                                    }
                                }
                            }
                            else
                            {
                                // objHDFC.UpdateFailRecors(Convert.ToString(row["PaymentTransID"]));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            Console.Read();

                        }
                    }
                }
            }

        }



    }
}