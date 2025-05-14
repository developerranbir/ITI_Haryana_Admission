using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HigherEducation.BusinessLayer
{
    public class clsCollege
    {
        static string ConStrHE = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducationR"].ToString());

        public string collegename { get; set; }
        public string collegeAdd { get; set; }
        public string institutecode { get; set; }
        public string affiliationwith { get; set; }
        public string distcode { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string website { get; set; }
        public int collegeid { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string CollegeType { get; set; }
        public string EduMode { get; set; }
        public string collegePrincipal { get; set; }
        public string collegePrincipalPhone { get; set; }
        public string collegeNodal { get; set; }
        public string collegeNodalPhone { get; set; }
        public string collegeAttraction { get; set; }
        public string collegeCounsile { get; set; }
        public string collegeRating { get; set; }

        public string isactive { get; set; }
        public string isactivePG { get; set; }
        public string isExserviceMan { get; set; }
        public string isDumbAndDeaf { get; set; }
        public string isppp { get; set; }

        public DataTable BindCollegeType()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("BindCollegeType", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "BindCollegeType";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }


        public DataTable BindDistrict()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("BindDistrict", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "BindDistrict";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable BindCollege()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollegeDetail", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "BindCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable BindUniversity()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("BindUniversity", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "BindUniversity";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable CheckDuplicateCollege(string CollegeName)
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("CheckDuplicateCollege", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegename", CollegeName);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "CheckDuplicateCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable CheckDuplicateCollegeforUpdate(string CollegeName, int CollegeId)
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("CheckDuplicateCollegeforUpdate", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegename", CollegeName);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", CollegeId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "CheckDuplicateCollegeforUpdate";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable CheckDuplicateInstituteCode(string InstituteCode)
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("CheckDuplicateInstituteCode", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_institutecode", InstituteCode);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "CheckDuplicateInstituteCode";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable CheckDuplicateInstituteCodeforUpdate(string InstituteCode, int CollegeId)
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("CheckDuplicateInstituteCodeforUpdate", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_institutecode", InstituteCode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", CollegeId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "CheckDuplicateInstituteCodeforUpdate";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public string AddCollege()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("AddCollege", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegename", collegename);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CollegeType", CollegeType);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeAdd", collegeAdd);
                vadap.SelectCommand.Parameters.AddWithValue("@p_distcode", distcode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_email", email);
                vadap.SelectCommand.Parameters.AddWithValue("@p_mobile", mobile);
                vadap.SelectCommand.Parameters.AddWithValue("@p_website", website);
                vadap.SelectCommand.Parameters.AddWithValue("@p_EduMode", EduMode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegePrincipal", collegePrincipal);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegePrincipalPhone", collegePrincipalPhone);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeNodal", collegeNodal);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeNodalPhone", collegeNodalPhone);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeAttraction", collegeAttraction);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeCouncile", collegeCounsile);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeRating", collegeRating == "" ? null : collegeRating);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MisCode", institutecode == "" ? null : institutecode);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "AddCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }


        public string UpdateCollege()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateCollege", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegename", collegename);
                vadap.SelectCommand.Parameters.AddWithValue("@p_universityid", affiliationwith);
                vadap.SelectCommand.Parameters.AddWithValue("@p_distcode", distcode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CollegeType", CollegeType);
                vadap.SelectCommand.Parameters.AddWithValue("@p_EduMode", EduMode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_isactive", isactive);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_institutecode", institutecode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_isactive_pg", isactivePG);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    // result = vds.Tables[0].Rows[0]["success"].ToString();
                    result = vds.Tables[0].Rows[0]["Result"].ToString();
                    if (result == "0")
                    {
                        clsLogger.ExceptionError = vds.Tables[0].Rows[0]["errorMsg"].ToString();
                        clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollege";
                        clsLogger.ExceptionMsg = "UpdateCollege";
                        clsLogger.SaveException();
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "UpdateCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }
        public DataTable GetParticularCollegeUnivData(int CollegeId)
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetParticularCollegeUnivData", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@P_college_id", CollegeId);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "GetParticularCollegeUnivData";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }

        public string getCollegeID()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("getAddedCollegeID", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["UserID"].ToString();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "AddCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }
        public DataTable GetCollegeUnivData()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetCollegeUnivData", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "GetCollegeUnivData";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable BindAssociatedUniversity()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("BindAssociatedUniversity", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "BindAssociatedUniversity";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public string UpdateAssociatedUniversity(int CollegeId, int AssUnivId, string UserId, string IPAddress)
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateAssociatedUniversity", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@college_id", CollegeId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_assunivid", AssUnivId);
                vadap.SelectCommand.Parameters.AddWithValue("@user_id", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@ip_address", IPAddress);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "UpdateAssociatedUniversity";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }

        public DataTable GetITIDetails()
        {

            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetITIDetails", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", collegeid);

                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Rows.Count > 0)
                {
                    return vds;
                }


            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/BusinessLayer/clsCollegGlance";
                clsLogger.ExceptionMsg = "GetCollegeProfile";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;

        }

        public string UpdateITIDetails()
        {
            string result = "";
            DataSet vds = new DataSet();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("UpdateITIDetails", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeid", collegeid);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegename", collegename);
                vadap.SelectCommand.Parameters.AddWithValue("@p_CollegeType", CollegeType);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeAdd", collegeAdd);
                vadap.SelectCommand.Parameters.AddWithValue("@p_distcode", distcode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_email", email);
                vadap.SelectCommand.Parameters.AddWithValue("@p_mobile", mobile);
                vadap.SelectCommand.Parameters.AddWithValue("@p_website", website);
                vadap.SelectCommand.Parameters.AddWithValue("@p_EduMode", EduMode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_ESM", isExserviceMan);
                vadap.SelectCommand.Parameters.AddWithValue("@p_DD", isDumbAndDeaf);
                vadap.SelectCommand.Parameters.AddWithValue("@p_PPP", isppp);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegePrincipal", collegePrincipal);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegePrincipalPhone", collegePrincipalPhone);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeNodal", collegeNodal);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeNodalPhone", collegeNodalPhone);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeAttraction", collegeAttraction);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeCouncile", collegeCounsile);
                vadap.SelectCommand.Parameters.AddWithValue("@p_collegeRating", collegeRating == "" ? null : collegeRating);
                vadap.SelectCommand.Parameters.AddWithValue("@p_UserId", UserId);
                vadap.SelectCommand.Parameters.AddWithValue("@p_IPAddress", IPAddress);
                vadap.SelectCommand.Parameters.AddWithValue("@p_MisCode", institutecode == "" ? null : institutecode);
                vadap.SelectCommand.Parameters.AddWithValue("@p_isActive", isactive);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.Fill(vds);
                if (vconnHE.State == ConnectionState.Open)
                    vconnHE.Close();
                if (vds.Tables.Count > 0)
                {
                    result = vds.Tables[0].Rows[0]["success"].ToString();
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "HigherEducation/BusinessLayer/clsCollege";
                clsLogger.ExceptionMsg = "AddCollege";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return result;
        }
    }
}