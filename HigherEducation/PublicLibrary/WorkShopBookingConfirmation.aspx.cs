﻿using HigherEducation.BusinessLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HigherEducation.PublicLibrary
{
    public partial class WorkShopBookingConfirmation : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LastBookingID"] != null)
                {
                    int bookingId = Convert.ToInt32(Session["LastBookingID"]);
                    LoadBookingDetails(bookingId);
                }
                else
                {
                    // If no booking ID, redirect back to booking page
                    Response.Redirect("WorkshopSlotBooking.aspx");
                }
            }
        }

        private void LoadBookingDetails(int bookingId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_GetWorkShopBookingDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_BookingID", bookingId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate the confirmation card
                                litBookingId.Text = reader["BookingID"].ToString();
                                litConfName.Text = reader["FullName"].ToString();
                                litConfMobile.Text = reader["MobileNumber"].ToString();
                                litConfEmail.Text = reader["Email"].ToString();
                                litConfITI.Text = reader["ITI_Name"].ToString();
                                litConfDistrict.Text = reader["District"].ToString();

                                // Format date
                                DateTime workshopDate = Convert.ToDateTime(reader["WorkshopDate"]);
                                litConfDate.Text = workshopDate.ToString("dd-MM-yyyy");

                                // Format time
                                TimeSpan startTime = TimeSpan.Parse(reader["StartTime"].ToString());
                                TimeSpan endTime = TimeSpan.Parse(reader["EndTime"].ToString());
                                DateTime startDateTime = DateTime.Today.Add(startTime);
                                DateTime endDateTime = DateTime.Today.Add(endTime);
                                litConfTime.Text = $"{startDateTime:hh:mm tt} - {endDateTime:hh:mm tt}";

                                // Duration and amount
                                litConfDuration.Text = $"{reader["WorkshopDuration"]} hour{(Convert.ToInt32(reader["WorkshopDuration"]) > 1 ? "s" : "")}";
                                litConfAmount.Text = Convert.ToDecimal(reader["BookingAmount"]).ToString("0");
                            }
                            else
                            {
                                // This shouldn't happen with the alternative stored procedure version
                                Response.Redirect("WorkshopSlotBooking.aspx");
                            }
                        }
                    }
                }
                catch (MySqlException ex) when (ex.Message.Contains("Booking not found"))
                {
                    // Specific handling for "not found" scenario
                    Response.Redirect("WorkshopSlotBooking.aspx");
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionError = ex.Message;
                    clsLogger.ExceptionPage = "PublicLibrary/WorkShopBookingConfirmation";
                    clsLogger.ExceptionMsg = "LoadBookingDetails";
                    clsLogger.SaveException();
                    Response.Redirect("WorkshopSlotBooking.aspx");
                }
            }
        }

        protected void btnGoHome_Click(object sender, EventArgs e)
        {
            // Redirect to your home page
            Response.Redirect("Home.aspx");
        }
    }
}