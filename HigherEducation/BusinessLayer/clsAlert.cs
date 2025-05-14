using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using HigherEducation.HigherEducation;
using HigherEducation.Models;
namespace HigherEducation.BusinessLayer
{
    public class clsAlert
    {
        public clsAlert()
        {
        }
        public static void AlertMsg(Page Sender, string Text)
        {
            ScriptManager.RegisterClientScriptBlock(Sender, Sender.GetType(), "alertMessage", "swal('','" + Text + "','')", true);
            // string script = "<script language=JavaScript> swal('','" + Text + "',''); </script>";
            //Sender.ClientScript.RegisterStartupScript(Sender.GetType(), "alertKey", script);
        }
        public static void Alert(Page Sender, string Text, string PageUrl)
        {
            string script = "<script language=JavaScript> alert('" + Text + "'); window.location='" + PageUrl + "'; </script>";
            Sender.ClientScript.RegisterStartupScript(Sender.GetType(), "alertKey", script);
        }
        public static void WindowOpen(Page Sender, string WindowUrl)
        {
            StringBuilder script = new StringBuilder();
            script.Append("<script language=javascript>");
            script.Append("window.open('" + WindowUrl + "', '_blank', 'toolbar=no, location=no, directories=no, resizable=yes,status=no, menubar=no, scrollbars=yes, copyhistory=yes,width=790,height=520,left=0,top=0')");
            script.Append("</script>");
            Sender.ClientScript.RegisterStartupScript(Sender.GetType(), "alertKey", script.ToString());
        }
        public static void WindowClose(Page Sender)
        {
            StringBuilder script = new StringBuilder();
            script.Append("<script language=javascript>");
            script.Append("self.close();");
            script.Append("</script>");
            Sender.ClientScript.RegisterStartupScript(Sender.GetType(), "alertKey", script.ToString());
        }
    }
}