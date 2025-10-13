using System;
using System.Reflection;
using System.Web.UI;

namespace HigherEducation.PublicLibrary
{
    public partial class Home : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if user is already logged in
                if (Session["UserId"] == null)
                {
                    Response.Redirect("login.aspx");
                }
            }
        }

      
   
     
        protected void btnPremiumPlan_Click(object sender, EventArgs e)
        {
            // Redirect to payment page for premium plan
            Response.Redirect("Subscription.aspx?plan=premium");
        }

        protected void btnBasicPlan_Click(object sender, EventArgs e)
        {
            // Redirect to payment page for basic plan
            Response.Redirect("Subscription.aspx?plan=basic");
        }

        protected void btnWorkshop_Click(object sender, EventArgs e)
        {
            // Redirect to workshop booking page
            Response.Redirect("Workshop.aspx");
        }

      
    }
}
