using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using HigherEducation.Models;

namespace HigherEducation.DataAccess
{
    public class ReportsContext
    {

        
        Logger logger = LogManager.GetCurrentClassLogger();

        #region ConnectionString
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        static readonly string ROConStr = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);
        MySqlConnection connection_ReadOnly = new MySqlConnection(ROConStr);
        #endregion;

        public DataTable GetCourseCollegeWiseApplications(string collegeId, string courseId,String UGPG)
        {
            DataTable dt = new DataTable();

            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCourseWiseCandidate", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_collegeId", Convert.ToInt32(collegeId == null || collegeId == "" ? "0" : collegeId));
                cmd.Parameters.AddWithValue("P_courseId", Convert.ToInt32(courseId == null || courseId == "" ? "0" : courseId));
                if (UGPG != null)
                {
                    cmd.Parameters.AddWithValue("P_UGPG", UGPG);
                }
                else
                {
                    cmd.Parameters.AddWithValue("P_UGPG", "UG");
                }
                
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //  connection.Close();
                //   return dt;
            }
            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.GetCourseCollegeWiseApplications()");
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

        public DataTable GetSectionWiseCandidate(string collegeId, string courseId,String UGPG)
        {
            DataTable dt = new DataTable();

            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getSectionWiseCandidate", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_collegeId", Convert.ToInt32(collegeId == null || collegeId == "" ? "0" : collegeId));
                cmd.Parameters.AddWithValue("P_courseId", Convert.ToInt32(courseId == null || courseId == "" ? "0" : courseId));
                if (UGPG != null)
                {
                    cmd.Parameters.AddWithValue("P_UGPG", UGPG);
                }
                else
                {
                    cmd.Parameters.AddWithValue("P_UGPG", "UG");
                }


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //  connection.Close();
                //   return dt;
            }
            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.GetSectionWiseCandidate()");
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
        public DataTable ObjectionsReport(string collegeId)
        {
            DataTable dt = new DataTable();

            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_ListOfObjections", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_id", Convert.ToInt32(collegeId == null || collegeId == "" ? "0" : collegeId));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.ObjectionsReport()");
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
        public DataTable d_getCollegeNames()
        {
            DataTable dt = new DataTable();
            var collegeId = "";
            if (HttpContext.Current.Session["collegeId"] != null)
            {
                collegeId = HttpContext.Current.Session["collegeId"].ToString();
            }
            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCollegeNames", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_id", collegeId == null || collegeId == "" ? "0" : collegeId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCollegeNames()");
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
        public DataTable d_getCourseNameAsPerCollege(String collegeId)
        {
            DataTable dt = new DataTable();

            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCourseNamesByCollege", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_id", Convert.ToInt32(collegeId == null || collegeId == "" ? "0" : collegeId));

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                // connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCourseNameAsPerCollege()");
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
        public DataSet d_GetHeaderStateData()
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_Dashboarddatadatewise", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_GetHeaderStateData()");
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
        public DataSet d_GetDateWiseRegistrations()
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_DateWiseRegistrations", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_DateWiseRegistrations()");
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
        public DataSet dPG_GetDateWiseRegistrations()
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("dPG_DateWiseRegistrations", connection_ReadOnly)
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
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.dPG_DateWiseRegistrations()");
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
        public DataTable PGGetCourseCollegeWiseApplications(string collegeId, string courseId)
        {
            DataTable dt = new DataTable();

            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("dPG_getCourseWiseCandidate", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_collegeId", Convert.ToInt32(collegeId == null || collegeId == "" ? "0" : collegeId));
                cmd.Parameters.AddWithValue("P_courseId", Convert.ToInt32(courseId == null || courseId == "" ? "0" : courseId));
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //  connection.Close();
                //   return dt;
            }
            catch (Exception ex)
            {

                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.PGGetCourseCollegeWiseApplications()");
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
        #region MeritListCutoffList
        public DataSet d_getMeritList(String CollegeId, String CourseId,String SectionId, String MeritId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_GetMaritDataList", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId== "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("PMerit", Convert.ToInt32(MeritId == null || MeritId== "" ? "0" : MeritId ));
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getMeritList()");
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
        public DataSet d_getMeritList2(String CollegeId, String CourseId, String SectionId,String MeritId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("D_GetMaritDataListWithMobile", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("PMerit", Convert.ToInt32(MeritId == null || MeritId == "" ? "0" : MeritId));
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getMeritList2()");
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
        public DataSet d_getMeritFeeList(String CollegeId, String CourseId, String SectionId,String CollegeType,String ShowDuplicates)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("D_GetMaritFeeStatus", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("PCollegeType", Convert.ToInt32(CollegeType == null || CollegeType == "" ? "0" : CollegeType));
                cmd.Parameters.AddWithValue("PShowDuplicates", Convert.ToInt32(ShowDuplicates == null || ShowDuplicates == "" ? "0" : ShowDuplicates));
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.D_GetMaritFeeStatus()");
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
        public DataSet D_GetSectionList(String CollegeId, String CourseId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("D_GetSectionList", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.D_GetSectionList()");
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
        public FeeModule GetCandidatePaymentSuccesDetail(string registrationid, String TxnId)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetpaymentSuccessDetail", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_Regid", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_paymentTransactionId", TxnId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();


                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["Reg_id"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.SubjectCombination = Convert.ToString(vds.Tables[0].Rows[0]["subjectCombination"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.FeePaid = Convert.ToString(vds.Tables[0].Rows[0]["Fee_paid"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.PaymentTransactionId = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);


                        objFeepaid.FullPart = Convert.ToString(vds.Tables[0].Rows[0]["Part_Full"]);
                        objFeepaid.PaymentGateway = Convert.ToString(vds.Tables[0].Rows[0]["Payment_gateway"]);

                        objFeepaid.FatherName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_father_name"]);
                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);

                        objFeepaid.PaymentTrackingID = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
                        objFeepaid.Transactiondate = Convert.ToString(vds.Tables[0].Rows[0]["Payment_Date"]);
                        objFeepaid.OrderStatus = Convert.ToString(vds.Tables[0].Rows[0]["order_status"]);
                        //objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.PaymentMode = Convert.ToString(vds.Tables[0].Rows[0]["payment_mode"]);
                        objFeepaid.billing_state = Convert.ToString(vds.Tables[0].Rows[0]["Installment_no"]);

                        objFeepaid.order_id = Convert.ToString(vds.Tables[0].Rows[0]["orderid"]);
                        objFeepaid.Bank_ref_no = Convert.ToString(vds.Tables[0].Rows[0]["bank_ref_no"]);
                        objFeepaid.CancelAdmission = Convert.ToString(vds.Tables[0].Rows[0]["cancelRemarks"]);
                        objFeepaid.Challan_status = Convert.ToString(vds.Tables[0].Rows[0]["Challan_status"]);
                        objFeepaid.TotalFee = Convert.ToInt32(vds.Tables[0].Rows[0]["TotalFee"]);
                        objFeepaid.Concession = Convert.ToInt32(vds.Tables[0].Rows[0]["Concession"]);
                        objFeepaid.PendingFee = Convert.ToInt32(vds.Tables[0].Rows[0]["pendingFee"]);
                        objFeepaid.CollegeType = Convert.ToString(vds.Tables[0].Rows[0]["collegetype"]);



                        objFeepaid.FeePaidNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.FeePaid));

                        objFeepaid.TotalFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.TotalFee));
                        objFeepaid.ConcessionNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.Concession));
                        objFeepaid.PendingFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.PendingFee));

                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidatePaymentSuccesDetail()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objFeepaid;
        }
        public static string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        #endregion

        public DataSet d_getCancellationReport(String CollegeId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCancellationReport", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                //cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                //cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                //cmd.Parameters.AddWithValue("PMerit", Convert.ToInt32(MeritId == null || MeritId == "" ? "0" : MeritId));
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCancellationReport()");
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
        public DataSet d_getCancelAppDoc(String RegId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCancelAppDoc", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PRegId", RegId);
                
                //cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                //cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                //cmd.Parameters.AddWithValue("PMerit", Convert.ToInt32(MeritId == null || MeritId == "" ? "0" : MeritId));

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCancelAppDoc()");
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
        public DataTable d_getCollegeType()
        {
            DataTable dt = new DataTable();
            var collegeId = "";
            if (HttpContext.Current.Session["collegeId"] != null)
            {
                collegeId = HttpContext.Current.Session["collegeId"].ToString();
            }
            if (connection_ReadOnly.State == ConnectionState.Closed)
            {
                connection_ReadOnly.Open();
            }
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCollegeType", connection_ReadOnly)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_college_type", collegeId == null || collegeId == "" ? "0" : collegeId);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                //connection_ReadOnly.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCollegeType()");
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
        public DataTable d_getCollegeNamesFromCollegeType(String collegeType)
        {
            DataTable    dt = new DataTable();
            var collegeId = "";
            if (HttpContext.Current.Session["collegeId"] != null)
            {
                collegeId = HttpContext.Current.Session["collegeId"].ToString();
            }
          
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getCollegeNamesFromCollegeType", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("P_college_Id", collegeId == null || collegeId == "" ? "0" : collegeId);
                cmd.Parameters.AddWithValue("P_college_type", collegeType);
                
                //cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                //cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                //cmd.Parameters.AddWithValue("PMerit", Convert.ToInt32(MeritId == null || MeritId == "" ? "0" : MeritId));

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getCollegeNamesFromCollegeType()");
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
        public DataSet d_getVacancyReport(String CollegeId,String CourseId,String SectionId,String MeritId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getVacancyReport", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourseId", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("PSectionId", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));
                cmd.Parameters.AddWithValue("PMerit", Convert.ToInt32(MeritId == null || MeritId == "" ? "0" : MeritId));
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getVacancyReport()");
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
        public DataSet d_getStudentDetailRollNo(String CollegeId,String CourseId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getStudentDetail_ROLLNO", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourseId", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                
                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getStudentDetailRollNo()");
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
        public DataSet d_getFeeReceiptList(String CollegeId,String CourseId,String SectionId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getFeeReceiptList", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getFeeReceiptList()");
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
        public DataSet d_getFeeCollectionReport(String CollegeType,String CollegeId,String CourseId,String SectionId)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getFeeCollectionReport", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeType", Convert.ToInt32(CollegeType == null || CollegeType == "" ? "0" : CollegeType));
                cmd.Parameters.AddWithValue("PCollegeID", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourse", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));
                cmd.Parameters.AddWithValue("PSection", Convert.ToInt32(SectionId == null || SectionId == "" ? "0" : SectionId));

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getFeeReceiptList()");
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
        public DataTable d_getStudentDetailRollNo_Download(String CollegeId, String CourseId)
        {
            DataTable dt = new DataTable();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                MySqlCommand cmd = new MySqlCommand("d_getStudentDetail_ROLLNO", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.StoredProcedure,
                };

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                cmd.Parameters.AddWithValue("PCollegeId", Convert.ToInt32(CollegeId == null || CollegeId == "" ? "0" : CollegeId));
                cmd.Parameters.AddWithValue("PCourseId", Convert.ToInt32(CourseId == null || CourseId == "" ? "0" : CourseId));

                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                da.Fill(dt);

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getStudentDetailRollNo()");
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
        public DataTable d_Download_RR()
        {
            DataTable dt = new DataTable();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                if (HttpContext.Current.Session["collegeId"] != null)
                {
                    MySqlCommand cmd = new MySqlCommand("d_Download_RR", connection_ReadOnly)
                    {
                        CommandTimeout = 120,
                        CommandType = CommandType.StoredProcedure,
                    };

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    cmd.Parameters.AddWithValue("PCollegeId", Convert.ToInt32(HttpContext.Current.Session["collegeId"].ToString()));


                    if (connection_ReadOnly.State == ConnectionState.Closed)
                    {
                        connection_ReadOnly.Open();
                    }

                    da.Fill(dt);
                }

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getStudentDetailRollNo()");
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

        public String d_GetRegIDForTxnNo(String TxnNo)
        {


            String Reg_ID = "";
            try
            {
                MySqlCommand cmd = new MySqlCommand("select Reg_id from tblfeepaid where tracking_id=@PTxnNo", connection_ReadOnly)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.Text,
                };

                cmd.Parameters.AddWithValue("@PTxnNo", TxnNo);



                if (connection_ReadOnly.State == ConnectionState.Closed)
                {
                    connection_ReadOnly.Open();
                }

                 Reg_ID = cmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.ReportsContext.d_getStudentDetailRollNo()");
            }
            finally
            {
                if (connection_ReadOnly.State == ConnectionState.Open)
                {
                    connection_ReadOnly.Close();
                }
            }
            return Reg_ID;
        }
        public FeeModule GetCandidatePaymentSuccesDetail_abhi(string registrationid, String TxnId)
        {
            List<FeeModule> objAddFee = new List<FeeModule>();
            List<CandidateFee> objCandidateFee = new List<CandidateFee>();
            FeeModule objFeepaid = new FeeModule();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetpaymentSuccessDetail_abhi", connection);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.Parameters.AddWithValue("@P_Regid", registrationid);
                vadap.SelectCommand.Parameters.AddWithValue("@P_paymentBankRefNo", TxnId);
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection.State == ConnectionState.Open)
                    connection.Close();


                if (vds.Tables.Count > 0)
                {
                    if (vds.Tables[0].Rows.Count > 0)
                    {
                        objFeepaid.RegistrationId = Convert.ToString(vds.Tables[0].Rows[0]["Reg_id"]);
                        objFeepaid.Rollno = Convert.ToString(vds.Tables[0].Rows[0]["rollNo"]);
                        objFeepaid.CandidateName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_name"]);
                        objFeepaid.CollegeName = Convert.ToString(vds.Tables[0].Rows[0]["collegename"]);
                        objFeepaid.CourseName = Convert.ToString(vds.Tables[0].Rows[0]["courseName"]);
                        objFeepaid.SubjectCombination = Convert.ToString(vds.Tables[0].Rows[0]["subjectCombination"]);
                        objFeepaid.CategoryName = Convert.ToString(vds.Tables[0].Rows[0]["categoryname"]);
                        objFeepaid.FeePaid = Convert.ToString(vds.Tables[0].Rows[0]["Fee_paid"]);
                        objFeepaid.SeatAllocationCategory = Convert.ToString(vds.Tables[0].Rows[0]["SeatAllocationCategory"]);
                        objFeepaid.PaymentTransactionId = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);


                        objFeepaid.FullPart = Convert.ToString(vds.Tables[0].Rows[0]["Part_Full"]);
                        objFeepaid.PaymentGateway = Convert.ToString(vds.Tables[0].Rows[0]["Payment_gateway"]);

                        objFeepaid.FatherName = Convert.ToString(vds.Tables[0].Rows[0]["applicant_father_name"]);
                        objFeepaid.SectionName = Convert.ToString(vds.Tables[0].Rows[0]["SectionName"]);
                        objFeepaid.gender_name = Convert.ToString(vds.Tables[0].Rows[0]["gender_name"]);
                        objFeepaid.ApplicantDOB = Convert.ToString(vds.Tables[0].Rows[0]["applicant_dob"]);

                        objFeepaid.PaymentTrackingID = Convert.ToString(vds.Tables[0].Rows[0]["Payment_transactionId"]);
                        objFeepaid.Transactiondate = Convert.ToString(vds.Tables[0].Rows[0]["Payment_Date"]);
                        objFeepaid.OrderStatus = Convert.ToString(vds.Tables[0].Rows[0]["order_status"]);
                        //objFeepaid.CandidateMobile = Convert.ToString(vds.Tables[0].Rows[0]["candidatemobile"]);
                        objFeepaid.PaymentMode = Convert.ToString(vds.Tables[0].Rows[0]["payment_mode"]);
                        objFeepaid.billing_state = Convert.ToString(vds.Tables[0].Rows[0]["Installment_no"]);

                        objFeepaid.order_id = Convert.ToString(vds.Tables[0].Rows[0]["orderid"]);
                        objFeepaid.Bank_ref_no = Convert.ToString(vds.Tables[0].Rows[0]["bank_ref_no"]);
                        objFeepaid.CancelAdmission = Convert.ToString(vds.Tables[0].Rows[0]["cancelRemarks"]);
                        objFeepaid.Challan_status = Convert.ToString(vds.Tables[0].Rows[0]["Challan_status"]);
                        objFeepaid.TotalFee = Convert.ToInt32(vds.Tables[0].Rows[0]["TotalFee"]);
                        objFeepaid.Concession = Convert.ToInt32(vds.Tables[0].Rows[0]["Concession"]);
                        objFeepaid.PendingFee = Convert.ToInt32(vds.Tables[0].Rows[0]["pendingFee"]);
                        objFeepaid.CollegeType = Convert.ToString(vds.Tables[0].Rows[0]["collegetype"]);



                        objFeepaid.FeePaidNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.FeePaid));

                        objFeepaid.TotalFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.TotalFee));
                        objFeepaid.ConcessionNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.Concession));
                        objFeepaid.PendingFeeNumber = ConvertNumbertoWords(Convert.ToInt32(objFeepaid.PendingFee));

                    }
                }

            }

            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.EducationDbContext.GetCandidatePaymentSuccesDetail()" + registrationid);
            }
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            return objFeepaid;
        }
    }
}