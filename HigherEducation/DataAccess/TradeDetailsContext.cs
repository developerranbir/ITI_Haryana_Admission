using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Configuration;
using System.Data;
using System.Web;

namespace HigherEducation.DataAccess
{
    public class TradeDetailsContext
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        #region ConnectionString
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        static readonly string ROConStr = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);
        MySqlConnection connection_ReadOnly = new MySqlConnection(ROConStr);
        #endregion;

        public DataTable SaveFinalTradeDetails(String data)
        {
            //DataTable dt = new DataTable();
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SectionAndSeatAllocation", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_tradedetails", data);
                cmd.Parameters.AddWithValue("p_createuser", Convert.ToString(HttpContext.Current.Session["UserID"]));
                cmd.Parameters.AddWithValue("p_ipaddress", Common.Common.GetIp());


                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.SaveFinalTradeDetails()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;
        }
        public DataTable ValidateFinalTradeDetailsViaCSV(String data)
        {
            //DataTable dt = new DataTable();
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("ValidateCSV", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_tradedetails", data);
                cmd.Parameters.AddWithValue("p_createuser", Convert.ToString(HttpContext.Current.Session["UserID"]));
                cmd.Parameters.AddWithValue("p_ipaddress", Common.Common.GetIp());


                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.ValidateFinalTradeDetailsViaCSV()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;
        }
        public DataTable SaveFinalTradeDetailsViaCSV(String data)
        {
            //DataTable dt = new DataTable();
            DataTable dt = new DataTable();
            try
            {
                MySqlCommand cmd = new MySqlCommand("SectionAndSeatAllocationViaCSV", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_tradedetails", data);
                cmd.Parameters.AddWithValue("p_createuser", Convert.ToString(HttpContext.Current.Session["UserID"]));
                cmd.Parameters.AddWithValue("p_ipaddress", Common.Common.GetIp());


                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.SaveFinalTradeDetails()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;
        }

        public DataTable UnitShiftExistingData()
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("UnitShiftExistingData", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);


                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.UnitShiftExistingData()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;
        }
        public DataTable GetITIs(String districtcode)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("BindITIs", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_districtcode", districtcode);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.GetITIs()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;
        }

        public DataSet GetTrades(String ITI)
        {
            DataSet ds = new DataSet();

            try
            {
                MySqlCommand cmd = new MySqlCommand("BindTrades", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("P_college_id", ITI);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.GetTrades()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return ds;
        }
        //GetUnitSize

        public DataTable GetUnitSize(String Trade)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("BindUnitSize", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_courseid", Trade);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.GetUnitSize()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;
        }

        public DataTable getType(String ITI)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("BindType", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("P_college_id", ITI);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.getType()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;

        }
        public DataTable getShift(String Type)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("BindShift", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_type", Type);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.getShift()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;

        }
        public DataTable getUnit(String Type)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("BindUnit", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_type", Type);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.getUnit()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;

        }

        public DataTable getTradeDetails(String districtcode, String ITI, String Trade)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("BindFinalTradeDetails", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_districtcode", districtcode);
                cmd.Parameters.AddWithValue("p_collegeid", ITI);
                cmd.Parameters.AddWithValue("p_courseid", Trade);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.getTradeDetails()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;
        }

        public DataTable UnitShiftDownload(String districtcode, String collegeid, String courseid)
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("UnitShiftDownload", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("p_districtcode", districtcode);
                cmd.Parameters.AddWithValue("p_collegeid", collegeid);
                cmd.Parameters.AddWithValue("p_courseid", courseid);

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.UnitShiftDownload()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;
        }


        public DataSet UpdateTradeDetail(String guid, String Scheme)
        {
            DataSet dt = new DataSet();

            try
            {
                MySqlCommand cmd = new MySqlCommand("UpdateTradeDetail", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("p_guid", guid);
                cmd.Parameters.AddWithValue("p_Scheme", Scheme);
                cmd.Parameters.AddWithValue("p_changeuser", Convert.ToString(HttpContext.Current.Session["UserID"]));
                cmd.Parameters.AddWithValue("p_ipaddress", Common.Common.GetIp());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.UpdateTradeDetail()");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dt;
        }
        public DataSet ActivateDeactivateTradeDetail(String guid, String Action, String collegeid, String courseid, String Type, String coursesectionid)
        {
            DataSet dt = new DataSet();

            try
            {
                MySqlCommand cmd = new MySqlCommand("ActivateDeactivateTradeDetail", connection)
                {
                    CommandType = CommandType.StoredProcedure

                };
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("p_guid", guid);
                cmd.Parameters.AddWithValue("p_action", Action);
                cmd.Parameters.AddWithValue("p_collegeid", collegeid);
                cmd.Parameters.AddWithValue("p_courseid", courseid);
                cmd.Parameters.AddWithValue("p_changeuser", Convert.ToString(HttpContext.Current.Session["UserID"]));
                cmd.Parameters.AddWithValue("p_ipaddress", Common.Common.GetIp());
                cmd.Parameters.AddWithValue("p_type", Type);
                cmd.Parameters.AddWithValue("p_coursesectionid", coursesectionid);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.ActivateDeactivateTradeDetail()");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dt;

        }
        public DataTable getScheme()
        {
            DataTable dt = new DataTable();

            try
            {
                MySqlCommand cmd = new MySqlCommand("BindScheme", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.TradeDetailsContext.getScheme()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return dt;

        }


        //public DataTable getCollegeListAsPerCollegeType(String collegeType)
        //{
        //    DataTable dt = new DataTable();
        //    var collegeId = "";
        //    if (HttpContext.Current.Session["collegeId"] != null)
        //    {
        //        collegeId = HttpContext.Current.Session["collegeId"].ToString();
        //    }

        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("GetCollegeAllotment", connection_ReadOnly)
        //        {
        //            CommandTimeout = 120,
        //            CommandType = CommandType.StoredProcedure,
        //        };

        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);

        //        cmd.Parameters.AddWithValue("P_college_id", collegeId == null || collegeId == "" ? "0" : collegeId);
        //        cmd.Parameters.AddWithValue("P_college_type", collegeType);
        //        if (connection_ReadOnly.State == ConnectionState.Closed)
        //        {
        //            connection_ReadOnly.Open();
        //        }

        //        da.Fill(dt);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCollegeNamesFromCollegeType()");
        //    }
        //    finally
        //    {
        //        if (connection_ReadOnly.State == ConnectionState.Open)
        //        {
        //            connection_ReadOnly.Close();
        //        }
        //    }
        //    return dt;

        //}
        //public DataTable GetTradeListByITI(String collegeId)
        //{
        //    DataTable dt = new DataTable();

        //    if (connection_ReadOnly.State == ConnectionState.Closed)
        //    {
        //        connection_ReadOnly.Open();
        //    }
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("GetTradebyITI", connection_ReadOnly)
        //        {
        //            CommandType = CommandType.StoredProcedure
        //        };
        //        cmd.CommandTimeout = 600;
        //        cmd.Parameters.AddWithValue("P_college_id", Convert.ToInt32(collegeId == null || collegeId == "" ? "0" : collegeId));
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        // connection_ReadOnly.Close();
        //        //return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCourseNameAsPerCollegeUGPG()");
        //    }
        //    finally
        //    {
        //        if (connection_ReadOnly.State == ConnectionState.Open)
        //        {
        //            connection_ReadOnly.Close();
        //        }
        //    }
        //    return dt;

        //}
        //public DataSet GetSectionList(String CollegeId, String CourseId)
        //{
        //    DataSet dt = new DataSet();
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("GetSectionListbyITI", connection_ReadOnly)
        //        {
        //            CommandTimeout = 120,
        //            CommandType = CommandType.StoredProcedure,
        //        };

        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);

        //        cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
        //        cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));

        //        if (connection_ReadOnly.State == ConnectionState.Closed)
        //        {
        //            connection_ReadOnly.Open();
        //        }

        //        da.Fill(dt);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.D_GetSectionListUGPG()");
        //    }
        //    finally
        //    {
        //        if (connection_ReadOnly.State == ConnectionState.Open)
        //        {
        //            connection_ReadOnly.Close();
        //        }
        //    }
        //    return dt;

        //}
        //public DataSet getStudentDetailsList(String CollegeId, String CourseId, String SectionId)
        //{
        //    DataSet dt = new DataSet();
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("GetCandidateDetalsITIWise", connection_ReadOnly)
        //        {
        //            CommandTimeout = 120,
        //            CommandType = CommandType.StoredProcedure,
        //        };
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
        //        cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
        //        cmd.Parameters.AddWithValue("P_Section", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
        //        if (connection_ReadOnly.State == ConnectionState.Closed)
        //        {
        //            connection_ReadOnly.Open();
        //        }
        //        da.Fill(dt);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getFeeReceiptList()");
        //    }
        //    finally
        //    {
        //        if (connection_ReadOnly.State == ConnectionState.Open)
        //        {
        //            connection_ReadOnly.Close();
        //        }
        //    }
        //    return dt;

        //}

        //public DataSet d_getStudentDetailsList(String CollegeId, String CourseId, String SectionId)
        //{
        //    DataSet dt = new DataSet();
        //    try
        //    {
        //        MySqlCommand cmd = new MySqlCommand("d_GetCandidateDetalsITIWise", connection_ReadOnly)
        //        {
        //            CommandTimeout = 120,
        //            CommandType = CommandType.StoredProcedure,
        //        };
        //        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //        cmd.Parameters.AddWithValue("p_CollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
        //        cmd.Parameters.AddWithValue("p_Course_id", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
        //        cmd.Parameters.AddWithValue("P_Section", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
        //        if (connection_ReadOnly.State == ConnectionState.Closed)
        //        {
        //            connection_ReadOnly.Open();
        //        }
        //        da.Fill(dt);

        //    }
        //    catch (Exception ex)
        //    {
        //        logger = LogManager.GetLogger("databaseLogger");
        //        logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getFeeReceiptList()");
        //    }
        //    finally
        //    {
        //        if (connection_ReadOnly.State == ConnectionState.Open)
        //        {
        //            connection_ReadOnly.Close();
        //        }
        //    }
        //    return dt;

        //}

    }
}