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
using Org.BouncyCastle.Utilities.Net;
using Ubiety.Dns.Core.Records;

namespace HigherEducation.DHE
{
    public partial class frm_Ranking_Conditions : System.Web.UI.Page
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        //Read_Only
        static string ConStrHE_ReadOnly = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection vconnHE_ReadOnly = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducationR"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            eDISHAutil eSessionMgmt = new eDISHAutil();
            if (!Page.IsPostBack)
            {
                bind_ddlcounselling();
                //bind_gdvconditions();

            }
            //Security Check
            eSessionMgmt.AntiFixationInit();
            eSessionMgmt.AntiHijackInit();
            //Security Check
        }

        //bind ddlcounselling
        private void bind_ddlcounselling()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("GetActivecounselling", vconnHE_ReadOnly))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (vconnHE_ReadOnly.State == ConnectionState.Closed)
                    {
                        vconnHE_ReadOnly.Open();
                    }
                    using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                ddlcounselling.DataTextField = "counsellingName";
                                ddlcounselling.DataValueField = "id";
                                ddlcounselling.DataSource = dt;
                                ddlcounselling.DataBind();
                                vconnHE_ReadOnly.Close();

                                ddlcounselling.Items.Insert(0, new ListItem("Select counselling", "0"));
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frm_Ranking_Conditions";
                clsLogger.ExceptionMsg = "GetActivecounselling";
                clsLogger.SaveException();
            }

        }


        protected void ddlcounselling_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (
               ddlcounselling.SelectedItem.Text != "Select counselling" ||
               ddlcounselling.SelectedItem.Value != "0"

               )
                { bind_gdvconditions(); btnGenRanking.Visible = true; }
                else
                {
                    gdvconditions.DataSource = null;
                    gdvconditions.DataBind();
                    btnGenRanking.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frm_Ranking_Conditions";
                clsLogger.ExceptionMsg = "ddlcounselling_SelectedIndexChanged";
                clsLogger.SaveException();
            }

        }
        //bind gdvconditions
        private void bind_gdvconditions()
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("Getconditionforranking", vconnHE_ReadOnly))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@counsellingid", ddlcounselling.SelectedItem.Value);
                    if (vconnHE_ReadOnly.State == ConnectionState.Closed)
                    {
                        vconnHE_ReadOnly.Open();
                    }
                    using (MySqlDataAdapter adp = new MySqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            adp.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                gdvconditions.DataSource = dt;
                                gdvconditions.DataBind();
                                vconnHE_ReadOnly.Close();

                                btnGenRanking.Visible = true;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frm_Ranking_Conditions";
                clsLogger.ExceptionMsg = "Getconditionforranking";
                clsLogger.SaveException();
            }

        }
        public string GetIPAddress()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            return ipAddress;
        }
        protected void gdvconditions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string userid = Convert.ToString(Session["UserId"]);
                string ipaddress = GetIPAddress();

                if (e.CommandName == "cmdapply")
                {
                    // Get the value of command argument
                    int id = Convert.ToInt32(e.CommandArgument.ToString());
                    // Do whatever operation you want.  
                    using (MySqlCommand cmd = new MySqlCommand("Apply_conditionforranking", vconnHE))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id", id);
                        cmd.Parameters.AddWithValue("@p_ipaddress", ipaddress);
                        cmd.Parameters.AddWithValue("@p_userid", userid);
                        cmd.Parameters.AddWithValue("@p_IsApplied", "applied");
                        if (vconnHE.State == ConnectionState.Closed)
                        {
                            vconnHE.Open();
                        }
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        bind_gdvconditions();
                    }
                }

                if (e.CommandName == "cmdremove")
                {
                    // Get the value of command argument
                    int id = Convert.ToInt32(e.CommandArgument.ToString());
                    // Do whatever operation you want.  
                    using (MySqlCommand cmd = new MySqlCommand("Apply_conditionforranking", vconnHE))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id", id);
                        cmd.Parameters.AddWithValue("@p_ipaddress", ipaddress);
                        cmd.Parameters.AddWithValue("@p_userid", userid);
                        cmd.Parameters.AddWithValue("@p_IsApplied", "notapplied");

                        if (vconnHE.State == ConnectionState.Closed)
                        {
                            vconnHE.Open();
                        }
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        bind_gdvconditions();
                    }

                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frm_Ranking_Conditions";
                clsLogger.ExceptionMsg = "Apply_conditionforranking";
                clsLogger.SaveException();
            }

        }

        protected void gdvconditions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Button btnapply = e.Row.FindControl("btnapply") as Button;
                    Button btnremove = e.Row.FindControl("btnremove") as Button;
                    Label lblstatus = e.Row.FindControl("lblstatus") as Label;
                    if (lblstatus.Text == "notapplied")
                    {
                        btnapply.Visible = true;
                        btnremove.Visible = false;
                    }
                    else
                    {
                        btnapply.Visible = false;
                        btnremove.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frm_Ranking_Conditions";
                clsLogger.ExceptionMsg = "gdvconditions_RowDataBound";
                clsLogger.SaveException();
            }




        }

        protected void btnGenRanking_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlcounselling.SelectedItem.Text == "Select counselling" || ddlcounselling.SelectedItem.Value == "0" ) 
                { 
                    return; 
                }
                else
                {
                    using (MySqlCommand cmd = new MySqlCommand("GenrateRankingFromFrontEnd", vconnHE))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_Couselling", ddlcounselling.SelectedItem.Value);


                        if (vconnHE.State == ConnectionState.Closed)
                        {
                            vconnHE.Open();
                        }
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        Response.Write("<script>alert('Ranking list Genrated Successfully');</script>");
                    }
                }

            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/frm_Ranking_Conditions";
                clsLogger.ExceptionMsg = "GenrateRankingFromFrontEnd";
                clsLogger.SaveException();
            }

        }
    }
}