using HigherEducation.BusinessLayer;
using MessagingToolkit.QRCode.Codec;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class libraryPrintPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if SubscriptionId exists in query string
                if (Request.QueryString["id"] != null || Request.QueryString["subscriptionid"] != null)
                {
                    string subscriptionId = Request.QueryString["id"] ?? Request.QueryString["subscriptionid"];
                    LoadPassDetails(subscriptionId);
                }
                else
                {
                    ShowError("Invalid request. No Subscription ID provided.");
                }
            }
        }


        public string GenerateQrCode(String lblPassholderName, String lblAmountPaid, String lblValidUpto, String lblCourse, String InstituteName)
        {

            try
            {
                String UserData = String.Format("Passholder Name:{0}\n, Amount Paid:{1}\n, Valid Up To:{2}\n, Access Type:{3}\n, InstituteName:{4}", lblPassholderName, lblAmountPaid, lblValidUpto, lblCourse, InstituteName);
                QRCodeEncoder qe = new QRCodeEncoder();
                qe.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qe.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                qe.QRCodeVersion = 0;
                System.Drawing.Image im = qe.Encode(UserData.ToString());
                MemoryStream ms = new MemoryStream();
                im.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] bt = ms.ToArray();
                String base64File = Convert.ToBase64String(bt);
                return base64File;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private void LoadPassDetails(string subscriptionId)
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    string query = "sp_LoadPassDetails";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_SubscriptionId", subscriptionId);
                        cmd.Parameters.AddWithValue("@p_UserId", Session["UserId"]);
                        conn.Open();

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the pass details
                                lblSubscriptionId.Text = reader["SubscriptionId"].ToString();
                                lblPassholderName.Text = reader["UserName"].ToString();
                                lblAmountPaid.Text = string.Format("₹{0:0.00}", reader["Amount"]);
                                lblCourse.Text = reader["SubscriptionType"].ToString();
                                InstituteName.Text = reader["collegename"].ToString();

                                // Format dates
                                DateTime validUpto = Convert.ToDateTime(reader["EndDate"]);
                                lblValidUpto.Text = validUpto.ToString("dd MMMM yyyy");

                                DateTime issuedDate = Convert.ToDateTime(reader["StartDate"]);
                                lblIssuedDate.Text = issuedDate.ToString("dd MMMM yyyy");

                                // Set QrCode available
                                string QrCodeImgBase64;
                              QrCodeImgBase64 = GenerateQrCode(reader["UserName"].ToString(), string.Format("₹{0:0.00}", reader["Amount"]), validUpto.ToString("dd MMMM yyyy"), reader["SubscriptionType"].ToString(), reader["collegename"].ToString());
                                imgQrCode.ImageUrl = "data:image/png;base64," + QrCodeImgBase64;
                                
                                // Set page title
                                Page.Title = "ITI Pass - " + reader["PassholderName"].ToString();

                                // Auto print after 1 second (optional)
                                ScriptManager.RegisterStartupScript(this, GetType(), "Print", "setTimeout(function() { window.print(); }, 1000);", true);
                            }
                            else
                            {
                                ShowError("Pass not found or payment is still pending. Please complete the payment first.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "PublicLibrary/libraryPrintPass";
                clsLogger.ExceptionMsg = "LoadPassDetails";
                clsLogger.SaveException();
            }
}

        private void ShowError(string errorMessage)
        {
            pnlError.Visible = true;
            lblError.Text = errorMessage;

            // Hide other elements
            lblSubscriptionId.Visible = false;
            lblPassholderName.Visible = false;
            lblAmountPaid.Visible = false;
            lblValidUpto.Visible = false;
            lblCourse.Visible = false;
            lblIssuedDate.Visible = false;
            imgLogo.Visible = false;
        }

       
    }
}

