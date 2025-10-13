using HigherEducation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class libraryPayment : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    // 1️⃣ Session validation
                    if (Session["UserId"] == null || string.IsNullOrEmpty(Session["UserId"].ToString()))
                    {
                        Response.Redirect("Login.aspx");
                        return;
                    }

                    string regId = Session["RegId"].ToString();

                    // 2️⃣ Generate transaction ID
                    string currentDateTime = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
                    string onlyNumRegId = Regex.Replace(regId, "[^0-9]", "");
                    string transactionId = onlyNumRegId + currentDateTime;
                    if (transactionId.Length > 30)
                        transactionId = transactionId.Substring(transactionId.Length - 30);

                    // 3️⃣ Prepare Fee Details Object
                    FeeModule fee = new FeeModule
                    {
                        RegistrationId = regId,
                        amount = 1000,  // Example amount — replace with actual
                        merchant_id = WebConfigurationManager.AppSettings["strMerchantId_ITI"],
                        order_id = transactionId,
                        currency = "INR",
                        redirect_url = "https://secure.ccavenue.com/transaction/transaction.do?command=initiateTransaction",
                        cancel_url = WebConfigurationManager.AppSettings["cancel_url_ITI_Q"],
                        language = "EN",
                        merchant_param1 = regId
                    };

                    // 4️⃣ Prepare request string
                    string ccaRequest = "";
                    PropertyInfo[] properties = fee.GetType().GetProperties();
                    foreach (var prop in properties)
                    {
                        var name = prop.Name;
                        var value = prop.GetValue(fee, null);
                        if (value != null && !name.StartsWith("_"))
                        {
                            ccaRequest += name + "=" + HttpUtility.UrlEncode(value.ToString()) + "&";
                        }
                    }

                    // 5️⃣ Encrypt using working key
                    string workingKey = WebConfigurationManager.AppSettings["workingKey_ITI"];
                    string accessCode = WebConfigurationManager.AppSettings["strAccessCode_ITI"];

                    CCAvenueCrypto crypto = new CCAvenueCrypto();
                    string encryptedRequest = crypto.Encrypt(ccaRequest, workingKey);

                    // 6️⃣ Bind hidden fields
                    hdnEncRequest.Value = encryptedRequest;
                    hdnAccessCode.Value = accessCode;
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }
}