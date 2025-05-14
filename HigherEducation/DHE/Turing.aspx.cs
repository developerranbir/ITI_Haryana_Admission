using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;

namespace HigherEducation.HigherEducation
{
    public partial class captcha_Turing : System.Web.UI.Page
    {
        public string randomStr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.checkreferer();
            // SESSION MANAGEMENT
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
            string browser = Request.Browser.Browser;
            if ((browser == "IE"))
            {
                Response.CacheControl = "No-Cache";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
            else
            {
                Response.Cache.SetAllowResponseInBrowserHistory(false);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Expires = 0;
                Response.Cache.SetNoStore();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            int valueencrypt1;
            Random randomclassencypt1 = new Random();
            valueencrypt1 = randomclassencypt1.Next(1111, 8888);
            string viewstatevalue = Convert.ToString(valueencrypt1);
            ViewState.Add(viewstatevalue, 0);
            Bitmap objBMP = new System.Drawing.Bitmap(50, 22);
            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
            objGraphics.Clear(Color.Green);
            objGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //' Configure font to use for text
            Font objFont = new Font("Arial", 8, FontStyle.Bold);
            //int[] myIntArray = new int[5] ;
            //int x;
            ////That is to create the random # and add it to our string 
            //Random autoRand = new Random();
            //for (x=0;x<5;x++)
            //{
            //myIntArray[x] = System.Convert.ToInt32 (autoRand.Next(0,9));
            //randomStr+= (myIntArray[x].ToString ());
            //}
            int length = 2;
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();
            char low_letter, up_letter;
            for (int i = 0; i < length; i++)
            {

                Int32 capletter = random.Next(65, 90);
                up_letter = Convert.ToChar(capletter);
                str_build.Append(up_letter);
                Int32 lowletter = random.Next(97, 122);
                low_letter = Convert.ToChar(lowletter);
                str_build.Append(low_letter);
                Int32 num = random.Next(1, 9);
                str_build.Append(num);
            }
            randomStr = str_build.ToString();

            //This is to add the string to session cookie, to be compared later
            Session.Add("randomStr", randomStr);
            // Session["dk"] = randomStr;
            //' Write out the text
            objGraphics.DrawString(randomStr, objFont, Brushes.White, 3, 3);
            //' Set the content type and return the image
            Response.ContentType = "image/GIF";
            objBMP.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();

        }
    }
}