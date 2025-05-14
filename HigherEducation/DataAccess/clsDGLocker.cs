
using System;
using System.IO;
using System.Web;
using System.Net;
using System.Xml;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using NLog;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for clsDGLocker
/// </summary>

public class clsDGLocker
{
    static string API_Timestamp = "";
    static string appId = ConfigurationManager.AppSettings["appId"];
    static string appKey = ConfigurationManager.AppSettings["appKey"];
    static string verifier = ConfigurationManager.AppSettings["verifierId"];
    static string APIUrl = ConfigurationManager.AppSettings["APIUrl_CBSE"];
    static string APIUrl_HR = ConfigurationManager.AppSettings["APIUrl_HR"];


    static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;

    private string _FUllName;
    public string FUllName
    {
        get { return _FUllName; }
        set { _FUllName = value; }
    }

    private string _Year;
    public string Year
    {
        get { return _Year; }
        set { _Year = value; }
    }

    private string _RollNo;
    public string RollNo
    {
        get { return _RollNo; }
        set { _RollNo = value; }
    }

    private string _Board;
    public string Board
    {
        get { return _Board; }
        set { _Board = value; }
    }

    public static DataSet GetDocumentNew(clsDGLocker objDGLocker, string QualificationCode)
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        //API_Timestamp = (DateTime.Now).ToString("yyyy-MM-dd") + "T" + (DateTime.Now).ToString("HH:mm:ss") + "+05:30";
        API_Timestamp = (DateTime.Now).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

        string key_APItimestamp = sha256_hash(appKey + API_Timestamp);
        if (objDGLocker.Board == "119" && QualificationCode == "10")
        {
            APIUrl = ConfigurationManager.AppSettings["APIHBSE10"];
        }
        else if (objDGLocker.Board == "68" && QualificationCode == "10")
        {
            APIUrl = ConfigurationManager.AppSettings["APICBSE10"];
        }
        else if (objDGLocker.Board == "119")
        {
            APIUrl = ConfigurationManager.AppSettings["APIUrl_HR"];
        }
        //else if (objDGLocker.Board == "280")
        //{
        //    APIUrl = ConfigurationManager.AppSettings["APIUrl_UP"];
        //}
        //else if (objDGLocker.Board == "220")
        //{
        //    APIUrl = ConfigurationManager.AppSettings["APIUrl_PU"];
        //}
        //else if (objDGLocker.Board == "226")
        //{
        //    APIUrl = ConfigurationManager.AppSettings["APIUrl_RJ"];
        //}
        else if (objDGLocker.Board == "68") // CBSE
        {
            APIUrl = ConfigurationManager.AppSettings["APIUrl_CBSE"];
        }

        Root Objroot = new Root();
        CertificateParameters certificateParameters = new CertificateParameters();
        ConsentArtifact consentArtifact = new ConsentArtifact();
        Consent consent = new Consent();
        Purpose purpose = new Purpose();
        User user = new User();
        Data data = new Data();
        Permission permission = new Permission();
        Frequency frequency = new Frequency();
        Signature signature = new Signature();
        DataProvider dataProvider = new DataProvider();
        DataConsumer dataConsumer = new DataConsumer();
        DateRange dateRange = new DateRange();

        Guid myuuid = Guid.NewGuid();
        string myuuidAsString = myuuid.ToString();

        Objroot.txnId = myuuidAsString;
        Objroot.format = "xml";
        if (objDGLocker.Board == "119") // HR
        {
            certificateParameters.CNAME = objDGLocker.FUllName;
            certificateParameters.RROLL = objDGLocker.RollNo;
            certificateParameters.YEAR = objDGLocker.Year;
        }
        //else  if (objDGLocker.Board == "220") // PU
        //{
        //    certificateParameters.FullName = objDGLocker.FUllName;
        //    certificateParameters.rollno = objDGLocker.RollNo;
        //    certificateParameters.year = objDGLocker.Year;
        //    certificateParameters.UID = "549559472776";
        //}
        //else if (objDGLocker.Board == "226")  // RJ
        //{
        //    certificateParameters.CNAME = objDGLocker.FUllName;
        //    certificateParameters.ROLL = objDGLocker.RollNo;
        //    certificateParameters.YEAR = objDGLocker.Year;
        //}
        else   // CBSE // UP
        {
            certificateParameters.FullName = objDGLocker.FUllName;
            certificateParameters.rollno = objDGLocker.RollNo;
            certificateParameters.year = objDGLocker.Year;
        }


        Objroot.certificateParameters = certificateParameters;

        consent.consentId = "ea9c43aa-7f5a-4bf3-a0be-e1caa24737ba";
        consent.timestamp = API_Timestamp;
        dataConsumer.id = "com.highereduhry";
        consent.dataConsumer = dataConsumer;

        if (objDGLocker.Board == "119")
        {

            dataProvider.id = "in.org.bseh";

        }
        else
        {
            dataProvider.id = "in.gov.cbse";

        }
        consent.dataProvider = dataProvider;

        purpose.description = "For Verification";
        consent.purpose = purpose;


        user.idType = "234";
        user.idNumber = "549559472776";
        user.mobile = "9878244447";
        user.email = "aa@gmail.com";
        consent.user = user;

        data.id = "1";
        consent.data = data;

        permission.access = "1";
        dateRange.from = API_Timestamp;
        dateRange.to = API_Timestamp;
        permission.dateRange = dateRange;

        frequency.unit = "1";
        frequency.value = 0;
        frequency.repeats = 0;

        permission.frequency = frequency;
        consent.permission = permission;


        signature.signature = "1";
        consentArtifact.consent = consent;
        consentArtifact.signature = signature;

        Objroot.consentArtifact = consentArtifact;


        var json = new JavaScriptSerializer().Serialize(Objroot);
        DataSet ds = new DataSet();
        string checkapiworking = callApi(json);
        StringReader theReader = new StringReader(checkapiworking);
        if (checkapiworking != "")
        {
            ds.ReadXml(new XmlTextReader(new StringReader(checkapiworking)));
            string code = "", name = "";
            DataSet dsgetdate = GetDetial(objDGLocker);
            int uu = Convert.ToInt32(dsgetdate.Tables[0].Rows[0][0]);

            try
            {
                if (uu == 0)
                {
                    string resultsubject_status = "";
                    for (int count = 0; count < ds.Tables.Count; count++)
                    {

                        DataTable table = ds.Tables[count];
                        int i = table.Rows.Count;
                        if (1 > 0)
                        {
                            MySqlConnection con = new MySqlConnection(ConStr);
                            if (table.TableName == "School")
                            {

                                code = table.Rows[0]["code"].ToString();
                                name = table.Rows[0]["name"].ToString();
                                if (con.State == ConnectionState.Closed)
                                    con.Open();

                                MySqlCommand cmd = new MySqlCommand("saveSchoolDetail", con)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };
                                cmd.Parameters.AddWithValue("P_code", code);
                                cmd.Parameters.AddWithValue("P_name", name);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                con.Close();
                            }
                            if (table.TableName == "Performance")
                            {

                                string result, marksTotal, marksMax, percentage, cgpa, cgpaMax, resultDate;
                                result = table.Rows[0]["result"].ToString();
                                marksTotal = table.Rows[0]["marksTotal"].ToString();
                                marksMax = table.Rows[0]["marksMax"].ToString();
                                percentage = table.Rows[0]["percentage"].ToString();
                                cgpa = table.Rows[0]["cgpa"].ToString();
                                cgpaMax = table.Rows[0]["cgpaMax"].ToString();
                                resultDate = table.Rows[0]["resultDate"].ToString();

                                resultsubject_status = result;

                                if (con.State == ConnectionState.Closed)
                                    con.Open();

                                MySqlCommand cmd = new MySqlCommand("savePerformanceDetail", con)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };


                                cmd.Parameters.AddWithValue("P_result", result);
                                cmd.Parameters.AddWithValue("P_marksTotal", marksTotal);
                                cmd.Parameters.AddWithValue("P_marksMax", marksMax);
                                cmd.Parameters.AddWithValue("P_percentage", percentage);
                                cmd.Parameters.AddWithValue("P_cgpa", cgpa);
                                cmd.Parameters.AddWithValue("P_cgpaMax", cgpaMax);
                                cmd.Parameters.AddWithValue("P_resultDate", resultDate);
                                cmd.Parameters.AddWithValue("P_RollNo", objDGLocker.RollNo);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                con.Close();

                            }
                            if (table.TableName == "Subject")
                            {
                                Regex regex = new Regex(@"^[0-9]+$");

                                for (int j = 0; j < table.Rows.Count; j++)
                                {
                                    if (!regex.IsMatch(table.Rows[j]["marksTotal"].ToString()))
                                    {
                                        table.Rows[j]["marksTotal"] = 0;
                                    }
                                }

                                table.DefaultView.Sort = "marksTotal DESC";
                                table = table.DefaultView.ToTable();
                                int increment = 0;
                                int totalsubjet = table.Rows.Count;
                                string sub2 = "";
                                if (totalsubjet >= 3)
                                {

                                    for (int subjectid = 0; subjectid < table.Rows.Count; subjectid++)
                                    {

                                        string namesuject, codeSubject, marksTheory, marksMaxTheory, marksPractical, marksMaxPractical, marksTotal, marksMax, gp, gpMax, grade, SubjectNo, subcode;
                                        namesuject = table.Rows[subjectid]["name"].ToString();
                                        codeSubject = table.Rows[subjectid]["code"].ToString();
                                        marksTheory = table.Rows[subjectid]["marksTheory"].ToString();
                                        marksMaxTheory = table.Rows[subjectid]["marksMaxTheory"].ToString();
                                        marksPractical = table.Rows[subjectid]["marksPractical"].ToString();
                                        marksMaxPractical = table.Rows[subjectid]["marksMaxPractical"].ToString();
                                        marksTotal = table.Rows[subjectid]["marksTotal"].ToString();
                                        marksMax = table.Rows[subjectid]["marksMax"].ToString();
                                        gp = table.Rows[subjectid]["gp"].ToString();
                                        gpMax = table.Rows[subjectid]["gpMax"].ToString();
                                        grade = table.Rows[subjectid]["grade"].ToString();
                                        subcode = table.Rows[subjectid]["code"].ToString();

                                        SubjectNo = (subjectid + 1).ToString();
                                        //if (objDGLocker.Board == "119")
                                        //{
                                        //    sub2 = "ENC";
                                        //}
                                        //else
                                        //{
                                        //    sub2 = "ENGLISH";
                                        //}
                                        //if (namesuject.Contains(sub2))
                                        //{
                                        //    SubjectNo = "1";
                                        //    if (increment != 0)
                                        //    {
                                        //        increment = increment - 1;
                                        //    }
                                        //    else
                                        //    {
                                        //        increment = 1;
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    if (subjectid == 0)
                                        //    {
                                        //        increment = 2;
                                        //        SubjectNo = (subjectid + increment).ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        if (increment == 0)
                                        //        {
                                        //            increment = 1;
                                        //        }
                                        //        SubjectNo = (subjectid + increment).ToString();
                                        //    }
                                        //}


                                        if (!regex.IsMatch(marksTheory))
                                        {
                                            marksTheory = "0";
                                        }
                                        if (!regex.IsMatch(marksMaxTheory))
                                        {
                                            marksMaxTheory = "0";
                                        }
                                        if (!regex.IsMatch(marksPractical))

                                        {
                                            marksPractical = "0";
                                        }
                                        if (!regex.IsMatch(marksTotal))

                                        {
                                            marksTotal = "0";
                                        }
                                        if (!regex.IsMatch(marksMax))

                                        {
                                            marksMax = "0";
                                        }
                                        if (!regex.IsMatch(marksMaxPractical))
                                        {
                                            marksMaxPractical = "0";
                                        }




                                        if (con.State == ConnectionState.Closed)
                                            con.Open();

                                        MySqlCommand cmd = new MySqlCommand("savesjectDetail", con)
                                        {
                                            CommandType = CommandType.StoredProcedure
                                        };

                                        cmd.Parameters.AddWithValue("P_name", namesuject);
                                        cmd.Parameters.AddWithValue("P_code", codeSubject);
                                        cmd.Parameters.AddWithValue("P_marksTheory", Convert.ToInt32(marksTheory));
                                        cmd.Parameters.AddWithValue("P_marksMaxTheory", Convert.ToInt32(marksMaxTheory));
                                        cmd.Parameters.AddWithValue("P_marksPractical", Convert.ToInt32(marksPractical));
                                        cmd.Parameters.AddWithValue("P_marksMaxPractical", Convert.ToInt32(marksMaxPractical));
                                        cmd.Parameters.AddWithValue("P_marksTotal", Convert.ToInt32(marksTotal));
                                        cmd.Parameters.AddWithValue("P_marksMax", Convert.ToInt32(marksMax));
                                        cmd.Parameters.AddWithValue("P_gp", gp);
                                        cmd.Parameters.AddWithValue("P_gpMax", gpMax);
                                        cmd.Parameters.AddWithValue("P_grade", grade);
                                        cmd.Parameters.AddWithValue("P_Board", objDGLocker.Board);
                                        cmd.Parameters.AddWithValue("P_rollNumber", objDGLocker.RollNo);
                                        cmd.Parameters.AddWithValue("P_SubjectNo", SubjectNo);
                                        cmd.Parameters.AddWithValue("P_SchoolName", name);
                                        cmd.Parameters.AddWithValue("P_resultpass", resultsubject_status);
                                        cmd.Parameters.AddWithValue("P_QualificationCode", QualificationCode);
                                        cmd.Parameters.AddWithValue("P_year", objDGLocker.Year);
                                        cmd.Parameters.AddWithValue("subid", subcode);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                        con.Close();
                                    }

                                }

                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.clsDGLocker.[HttpPost] GetDocumentNew()");
            }
        }
        return ds;
    }

    public static DataSet GetDocumentNew2(clsDGLocker objDGLocker, string QualificationCode, string Reg_Id)
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        //API_Timestamp = (DateTime.Now).ToString("yyyy-MM-dd") + "T" + (DateTime.Now).ToString("HH:mm:ss") + "+05:30";
        API_Timestamp = (DateTime.Now).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

        string key_APItimestamp = sha256_hash(appKey + API_Timestamp);
        if (objDGLocker.Board == "119" && QualificationCode == "10")
        {
            APIUrl = ConfigurationManager.AppSettings["APIHBSE10"];
        }
        else if (objDGLocker.Board == "68" && QualificationCode == "10")
        {
            APIUrl = ConfigurationManager.AppSettings["APICBSE10"];
        }
        else if (objDGLocker.Board == "119")
        {
            APIUrl = ConfigurationManager.AppSettings["APIUrl_HR"];
        }
        else if (objDGLocker.Board == "68") // CBSE
        {
            APIUrl = ConfigurationManager.AppSettings["APIUrl_CBSE"];
        }

        Root Objroot = new Root();
        CertificateParameters certificateParameters = new CertificateParameters();
        ConsentArtifact consentArtifact = new ConsentArtifact();
        Consent consent = new Consent();
        Purpose purpose = new Purpose();
        User user = new User();
        Data data = new Data();
        Permission permission = new Permission();
        Frequency frequency = new Frequency();
        Signature signature = new Signature();
        DataProvider dataProvider = new DataProvider();
        DataConsumer dataConsumer = new DataConsumer();
        DateRange dateRange = new DateRange();

        Guid myuuid = Guid.NewGuid();
        string myuuidAsString = myuuid.ToString();

        Objroot.txnId = myuuidAsString;
        Objroot.format = "xml";
        if (objDGLocker.Board == "119") // HR
        {
            certificateParameters.CNAME = objDGLocker.FUllName;
            certificateParameters.RROLL = objDGLocker.RollNo;
            certificateParameters.YEAR = objDGLocker.Year;
        }
        else   // CBSE // UP
        {
            certificateParameters.FullName = objDGLocker.FUllName;
            certificateParameters.rollno = objDGLocker.RollNo;
            certificateParameters.year = objDGLocker.Year;
        }


        Objroot.certificateParameters = certificateParameters;

        consent.consentId = "ea9c43aa-7f5a-4bf3-a0be-e1caa24737ba";
        consent.timestamp = API_Timestamp;
        dataConsumer.id = "com.highereduhry";
        consent.dataConsumer = dataConsumer;

        if (objDGLocker.Board == "119")
        {

            dataProvider.id = "in.org.bseh";

        }
        else
        {
            dataProvider.id = "in.gov.cbse";

        }
        consent.dataProvider = dataProvider;

        purpose.description = "For Verification";
        consent.purpose = purpose;


        user.idType = "234";
        user.idNumber = "549559472776";
        user.mobile = "9878244447";
        user.email = "aa@gmail.com";
        consent.user = user;

        data.id = "1";
        consent.data = data;

        permission.access = "1";
        dateRange.from = API_Timestamp;
        dateRange.to = API_Timestamp;
        permission.dateRange = dateRange;

        frequency.unit = "1";
        frequency.value = 0;
        frequency.repeats = 0;

        permission.frequency = frequency;
        consent.permission = permission;


        signature.signature = "1";
        consentArtifact.consent = consent;
        consentArtifact.signature = signature;

        Objroot.consentArtifact = consentArtifact;


        var json = new JavaScriptSerializer().Serialize(Objroot);
        DataSet ds = new DataSet();
        string checkapiworking = callApi(json);
        StringReader theReader = new StringReader(checkapiworking);
        if (checkapiworking != "")
        {
            ds.ReadXml(new XmlTextReader(new StringReader(checkapiworking)));
            string code = "", name = "";
            DataSet dsgetdate = GetDetial(objDGLocker);
            int uu = Convert.ToInt32(dsgetdate.Tables[0].Rows[0][0]);

            try
            {
                if (uu == 0)
                {
                    string resultsubject_status = "";
                    for (int count = 0; count < ds.Tables.Count; count++)
                    {

                        DataTable table = ds.Tables[count];
                        int i = table.Rows.Count;
                        if (1 > 0)
                        {
                            MySqlConnection con = new MySqlConnection(ConStr);
                            if (table.TableName == "School")
                            {

                                code = table.Rows[0]["code"].ToString();
                                name = table.Rows[0]["name"].ToString();
                                if (con.State == ConnectionState.Closed)
                                    con.Open();

                                MySqlCommand cmd = new MySqlCommand("saveSchoolDetail", con)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };
                                cmd.Parameters.AddWithValue("P_code", code);
                                cmd.Parameters.AddWithValue("P_name", name);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                con.Close();
                            }
                            if (table.TableName == "Performance")
                            {

                                string result, marksTotal, marksMax, percentage, cgpa, cgpaMax, resultDate;
                                result = table.Rows[0]["result"].ToString();
                                marksTotal = table.Rows[0]["marksTotal"].ToString();
                                marksMax = table.Rows[0]["marksMax"].ToString();
                                percentage = table.Rows[0]["percentage"].ToString();
                                cgpa = table.Rows[0]["cgpa"].ToString();
                                cgpaMax = table.Rows[0]["cgpaMax"].ToString();
                                resultDate = table.Rows[0]["resultDate"].ToString();

                                resultsubject_status = result;

                                if (con.State == ConnectionState.Closed)
                                    con.Open();

                                MySqlCommand cmd = new MySqlCommand("savePerformanceDetail", con)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };


                                cmd.Parameters.AddWithValue("P_result", result);
                                cmd.Parameters.AddWithValue("P_marksTotal", marksTotal);
                                cmd.Parameters.AddWithValue("P_marksMax", marksMax);
                                cmd.Parameters.AddWithValue("P_percentage", percentage);
                                cmd.Parameters.AddWithValue("P_cgpa", cgpa);
                                cmd.Parameters.AddWithValue("P_cgpaMax", cgpaMax);
                                cmd.Parameters.AddWithValue("P_resultDate", resultDate);
                                cmd.Parameters.AddWithValue("P_RollNo", objDGLocker.RollNo);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                con.Close();

                            }
                            if (table.TableName == "Subject")
                            {
                                Regex regex = new Regex(@"^[0-9]+$");

                                for (int j = 0; j < table.Rows.Count; j++)
                                {
                                    if (!regex.IsMatch(table.Rows[j]["marksTotal"].ToString()))
                                    {
                                        table.Rows[j]["marksTotal"] = 0;
                                    }
                                }

                                table.DefaultView.Sort = "marksTotal DESC";
                                table = table.DefaultView.ToTable();
                                int increment = 0;
                                int totalsubjet = table.Rows.Count;
                                string sub2 = "";
                                if (totalsubjet >= 3)
                                {

                                    for (int subjectid = 0; subjectid < table.Rows.Count; subjectid++)
                                    {

                                        string namesuject, codeSubject, marksTheory, marksMaxTheory, marksPractical, marksMaxPractical, marksTotal, marksMax, gp, gpMax, grade, SubjectNo, subcode;
                                        namesuject = table.Rows[subjectid]["name"].ToString();
                                        codeSubject = table.Rows[subjectid]["code"].ToString();
                                        marksTheory = table.Rows[subjectid]["marksTheory"].ToString();
                                        marksMaxTheory = table.Rows[subjectid]["marksMaxTheory"].ToString();
                                        marksPractical = table.Rows[subjectid]["marksPractical"].ToString();
                                        marksMaxPractical = table.Rows[subjectid]["marksMaxPractical"].ToString();
                                        marksTotal = table.Rows[subjectid]["marksTotal"].ToString();
                                        marksMax = table.Rows[subjectid]["marksMax"].ToString();
                                        gp = table.Rows[subjectid]["gp"].ToString();
                                        gpMax = table.Rows[subjectid]["gpMax"].ToString();
                                        grade = table.Rows[subjectid]["grade"].ToString();
                                        subcode = table.Rows[subjectid]["code"].ToString();

                                        SubjectNo = (subjectid + 1).ToString();

                                        if (!regex.IsMatch(marksTheory))
                                        {
                                            marksTheory = "0";
                                        }
                                        if (!regex.IsMatch(marksMaxTheory))
                                        {
                                            marksMaxTheory = "0";
                                        }
                                        if (!regex.IsMatch(marksPractical))

                                        {
                                            marksPractical = "0";
                                        }
                                        if (!regex.IsMatch(marksTotal))

                                        {
                                            marksTotal = "0";
                                        }
                                        if (!regex.IsMatch(marksMax))

                                        {
                                            marksMax = "0";
                                        }
                                        if (!regex.IsMatch(marksMaxPractical))
                                        {
                                            marksMaxPractical = "0";
                                        }




                                        if (con.State == ConnectionState.Closed)
                                            con.Open();

                                        MySqlCommand cmd = new MySqlCommand("savesubjectDetailNew2", con)
                                        {
                                            CommandType = CommandType.StoredProcedure
                                        };

                                        cmd.Parameters.AddWithValue("P_name", namesuject);
                                        cmd.Parameters.AddWithValue("P_code", codeSubject);
                                        cmd.Parameters.AddWithValue("P_marksTheory", Convert.ToInt32(marksTheory));
                                        cmd.Parameters.AddWithValue("P_marksMaxTheory", Convert.ToInt32(marksMaxTheory));
                                        cmd.Parameters.AddWithValue("P_marksPractical", Convert.ToInt32(marksPractical));
                                        cmd.Parameters.AddWithValue("P_marksMaxPractical", Convert.ToInt32(marksMaxPractical));
                                        cmd.Parameters.AddWithValue("P_marksTotal", Convert.ToInt32(marksTotal));
                                        cmd.Parameters.AddWithValue("P_marksMax", Convert.ToInt32(marksMax));
                                        cmd.Parameters.AddWithValue("P_gp", gp);
                                        cmd.Parameters.AddWithValue("P_gpMax", gpMax);
                                        cmd.Parameters.AddWithValue("P_grade", grade);
                                        cmd.Parameters.AddWithValue("P_Board", objDGLocker.Board);
                                        cmd.Parameters.AddWithValue("P_rollNumber", objDGLocker.RollNo);
                                        cmd.Parameters.AddWithValue("P_SubjectNo", SubjectNo);
                                        cmd.Parameters.AddWithValue("P_SchoolName", name);
                                        cmd.Parameters.AddWithValue("P_resultpass", resultsubject_status);
                                        cmd.Parameters.AddWithValue("P_QualificationCode", QualificationCode);
                                        cmd.Parameters.AddWithValue("P_Reg_Id", Reg_Id);
                                        cmd.Parameters.AddWithValue("P_year", objDGLocker.Year);
                                        cmd.Parameters.AddWithValue("subid", subcode);
                                        cmd.ExecuteNonQuery();
                                        cmd.Dispose();
                                        con.Close();
                                    }

                                }

                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.clsDGLocker.[HttpPost] GetDocumentNew()");
            }
        }
        return ds;
    }

    public static string GetMarkSheet(clsDGLocker objDGLocker, string QualificationCode, string Reg_Id)
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        //API_Timestamp = (DateTime.Now).ToString("yyyy-MM-dd") + "T" + (DateTime.Now).ToString("HH:mm:ss") + "+05:30";
        API_Timestamp = (DateTime.Now).ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

        string key_APItimestamp = sha256_hash(appKey + API_Timestamp);
        if (objDGLocker.Board == "119" && QualificationCode == "10")
        {
            APIUrl = ConfigurationManager.AppSettings["APIHBSE10"];
        }
        else if (objDGLocker.Board == "68" && QualificationCode == "10")
        {
            APIUrl = ConfigurationManager.AppSettings["APICBSE10"];
        }
        else if (objDGLocker.Board == "119")
        {
            APIUrl = ConfigurationManager.AppSettings["APIUrl_HR"];
        }
        else if (objDGLocker.Board == "68") // CBSE
        {
            APIUrl = ConfigurationManager.AppSettings["APIUrl_CBSE"];
        }

        Root Objroot = new Root();
        CertificateParameters certificateParameters = new CertificateParameters();
        ConsentArtifact consentArtifact = new ConsentArtifact();
        Consent consent = new Consent();
        Purpose purpose = new Purpose();
        User user = new User();
        Data data = new Data();
        Permission permission = new Permission();
        Frequency frequency = new Frequency();
        Signature signature = new Signature();
        DataProvider dataProvider = new DataProvider();
        DataConsumer dataConsumer = new DataConsumer();
        DateRange dateRange = new DateRange();

        Guid myuuid = Guid.NewGuid();
        string myuuidAsString = myuuid.ToString();

        Objroot.txnId = myuuidAsString;
        Objroot.format = "pdf";
        if (objDGLocker.Board == "119") // HR
        {
            certificateParameters.CNAME = objDGLocker.FUllName;
            certificateParameters.RROLL = objDGLocker.RollNo;
            certificateParameters.YEAR = objDGLocker.Year;
        }
        else   // CBSE // UP
        {
            certificateParameters.FullName = objDGLocker.FUllName;
            certificateParameters.rollno = objDGLocker.RollNo;
            certificateParameters.year = objDGLocker.Year;
        }


        Objroot.certificateParameters = certificateParameters;

        consent.consentId = "ea9c43aa-7f5a-4bf3-a0be-e1caa24737ba";
        consent.timestamp = API_Timestamp;
        dataConsumer.id = "com.highereduhry";
        consent.dataConsumer = dataConsumer;

        if (objDGLocker.Board == "119")
        {

            dataProvider.id = "in.org.bseh";

        }
        else
        {
            dataProvider.id = "in.gov.cbse";

        }
        consent.dataProvider = dataProvider;

        purpose.description = "For Verification";
        consent.purpose = purpose;


        user.idType = "234";
        user.idNumber = "549559472776";
        user.mobile = "9878244447";
        user.email = "aa@gmail.com";
        consent.user = user;

        data.id = "1";
        consent.data = data;

        permission.access = "1";
        dateRange.from = API_Timestamp;
        dateRange.to = API_Timestamp;
        permission.dateRange = dateRange;

        frequency.unit = "1";
        frequency.value = 0;
        frequency.repeats = 0;

        permission.frequency = frequency;
        consent.permission = permission;


        signature.signature = "1";
        consentArtifact.consent = consent;
        consentArtifact.signature = signature;

        Objroot.consentArtifact = consentArtifact;


        var json = new JavaScriptSerializer().Serialize(Objroot);
        DataSet ds = new DataSet();
        string checkapiworking = callApimarksheet(json);
        return checkapiworking;
    }

    public static string callApimarksheet(string requestXML)
    {
        Logger logger = LogManager.GetCurrentClassLogger();

    RerunTheCode:
        try
        {
            HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create(APIUrl));
            req.Method = "POST";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            req.ContentType = "application/json";
            req.ProtocolVersion = HttpVersion.Version11;

            //send header param 
            req.Headers.Add("X-APISETU-CLIENTID", appId);
            req.Headers.Add("X-APISETU-APIKEY", appKey);

            var data = System.Text.Encoding.UTF8.GetBytes(requestXML);
            req.ContentLength = data.Length;
            using (var stream = req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var httpResponse = (HttpWebResponse)req.GetResponse();


            var stream1 = new MemoryStream();
            httpResponse.GetResponseStream().CopyTo(stream1);
            var bytes = stream1.ToArray();
            string base64ImageRepresentation = Convert.ToBase64String(bytes);
            return base64ImageRepresentation;
        }

        catch (WebException wex)
        {
            if (wex.Message == "The underlying connection was closed: An unexpected error occurred on a send.")
            {
                goto RerunTheCode;
            }
            if (wex.Message == "The remote server returned an error: (400) Bad Request." || wex.Message == "The remote server returned an error: (500) Internal Server Error." || wex.Message == "The remote server returned an error: (403) Forbidden.")
            {

            }
            else
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(wex, "Error occured in HigherEducation.clsDGLocker.[HttpPost] callApimarksheet()");
            }

        }
        catch (Exception ex)
        {
        }
        return null;
    }
    public static string callApi(string requestXML)
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        string result = "";
    RerunTheCode:
        try
        {
            HttpWebRequest req = (HttpWebRequest)(HttpWebRequest.Create(APIUrl));
            req.Method = "POST";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            req.ContentType = "application/json";
            req.ProtocolVersion = HttpVersion.Version11;

            //send header param 
            req.Headers.Add("X-APISETU-CLIENTID", appId);
            req.Headers.Add("X-APISETU-APIKEY", appKey);

            var data = System.Text.Encoding.UTF8.GetBytes(requestXML);
            req.ContentLength = data.Length;
            using (var stream = req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var httpResponse = (HttpWebResponse)req.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
        }

        catch (WebException wex)
        {
            if (wex.Message == "The underlying connection was closed: An unexpected error occurred on a send.")
            {
                goto RerunTheCode;
            }
            if (wex.Message == "The remote server returned an error: (400) Bad Request." || wex.Message == "The remote server returned an error: (500) Internal Server Error." || wex.Message == "The remote server returned an error: (403) Forbidden.")
            {

            }
            else
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(wex, "Error occured in HigherEducation.clsDGLocker.[HttpPost] callApi()");
            }

        }
        catch (Exception ex)
        {
        }
        return result;
    }
    public static String sha256_hash(String value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }
        return Sb.ToString();
    }
    public static long ConvertToTimestamp(DateTime value)
    {
        long epoch = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        return epoch;
    }

    public static DataSet GetDetial(clsDGLocker objDGLocker)
    {
        DataSet ds = new DataSet();
        MySqlConnection con = new MySqlConnection(ConStr);
        if (con.State == ConnectionState.Closed)
            con.Open();

        MySqlCommand cmd = new MySqlCommand("GetdataTable", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("P_Board", objDGLocker._Board);
        cmd.Parameters.AddWithValue("P_RollNo", objDGLocker._RollNo);
        cmd.Parameters.AddWithValue("P_YEARdata", objDGLocker._Year);

        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.Dispose();
        con.Close();

        return ds;
    }
    public static DataSet Get12HBSEData(clsDGLocker objDGLocker, string QualificationData)
    {
        DataSet ds = new DataSet();
        MySqlConnection con = new MySqlConnection(ConStr);
        if (con.State == ConnectionState.Closed)
            con.Open();

        MySqlCommand cmd = new MySqlCommand("getHaryanaStudentData", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("P_cname", objDGLocker._FUllName);
        cmd.Parameters.AddWithValue("P_YEARdata", objDGLocker._Year);
        cmd.Parameters.AddWithValue("P_RollNo", objDGLocker._RollNo);
        cmd.Parameters.AddWithValue("P_QualificationData", QualificationData);
        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.Dispose();
        con.Close();

        return ds;
    }

    public static DataSet Get10HBSEData(clsDGLocker objDGLocker, string QualificationData)
    {
        DataSet ds = new DataSet();
        MySqlConnection con = new MySqlConnection(ConStr);
        if (con.State == ConnectionState.Closed)
            con.Open();

        MySqlCommand cmd = new MySqlCommand("get10HaryanaStudentData", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("P_cname", objDGLocker._FUllName);
        cmd.Parameters.AddWithValue("P_YEARdata", objDGLocker._Year);
        cmd.Parameters.AddWithValue("P_RollNo", objDGLocker._RollNo);
        cmd.Parameters.AddWithValue("P_QualificationData", QualificationData);
        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.Dispose();
        con.Close();

        return ds;
    }
    public static DataSet GetHBSEDataRegistrationtable(clsDGLocker objDGLocker)
    {
        DataSet ds = new DataSet();
        MySqlConnection con = new MySqlConnection(ConStr);
        if (con.State == ConnectionState.Closed)
            con.Open();

        MySqlCommand cmd = new MySqlCommand("getHaryanaStudentDataRegistrationtable", con)
        {
            CommandType = CommandType.StoredProcedure
        };
        cmd.Parameters.AddWithValue("P_cname", objDGLocker.FUllName);
        cmd.Parameters.AddWithValue("P_Board", objDGLocker.Board);
        cmd.Parameters.AddWithValue("P_YEARdata", objDGLocker.Year);
        cmd.Parameters.AddWithValue("P_RollNo", objDGLocker.RollNo);
        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.Dispose();
        con.Close();

        return ds;
    }
}


public class CertificateParameters
{
    public string YEAR { get; set; }
    public string RROLL { get; set; }
    public string CNAME { get; set; }

    public string year { get; set; }
    public string rollno { get; set; }

    public string FullName { get; set; }
    //public string UID { get; set; }
    //public string ROLL { get; set; }
}

public class DataConsumer
{
    public string id { get; set; }
}

public class DataProvider
{
    public string id { get; set; }
}

public class Purpose
{
    public string description { get; set; }
}

public class User
{
    public string idType { get; set; }
    public string idNumber { get; set; }
    public string mobile { get; set; }
    public string email { get; set; }
}

public class Data
{
    public string id { get; set; }
}

public class DateRange
{
    public string from { get; set; }
    public string to { get; set; }
}

public class Frequency
{
    public string unit { get; set; }
    public int value { get; set; }
    public int repeats { get; set; }
}

public class Permission
{
    public string access { get; set; }
    public DateRange dateRange { get; set; }
    public Frequency frequency { get; set; }
}

public class Consent
{
    public string consentId { get; set; }
    public string timestamp { get; set; }
    public DataConsumer dataConsumer { get; set; }
    public DataProvider dataProvider { get; set; }
    public Purpose purpose { get; set; }
    public User user { get; set; }
    public Data data { get; set; }
    public Permission permission { get; set; }
}

public class Signature
{
    public string signature { get; set; }
}

public class ConsentArtifact
{
    public Consent consent { get; set; }
    public Signature signature { get; set; }
}

public class Root
{
    public string txnId { get; set; }
    public string format { get; set; }
    public CertificateParameters certificateParameters { get; set; }
    public ConsentArtifact consentArtifact { get; set; }
}

