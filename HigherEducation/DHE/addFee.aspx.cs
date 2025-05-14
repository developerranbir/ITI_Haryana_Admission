using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HigherEducation.BusinessLayer;

namespace HigherEducation.HigherEducation
{

    public partial class addFee : System.Web.UI.Page
    {
        HEBL bb = new HEBL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.RequestType == "GET")
                {
                    throw new HttpException(405, "GET not allowed for this.");
                }
                else
                {

                    string abc1, abc2, abc3, abc4;
                    abc1 = TextBox1.Text;
                    abc2 = TextBox2.Text;
                    abc3 = TextBox3.Text;
                    abc4 = TextBox4.Text;
                    string result = bb.AddFee(abc1, abc2, abc3, abc4).ToString();
                    if (result == "0")
                    {
                        lblerror.Text = "data is not Saved.";
                    }
                    else
                    {
                        lblerror.Text = "data is Saved.";
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    }