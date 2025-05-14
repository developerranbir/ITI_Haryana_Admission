using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Configuration;
using System.Data;
using System.Web;
using HigherEducation.BusinessLayer;

namespace HigherEducation.DataAccess
{
    
    public class GrantRevokeContext
    {
        MySqlConnection vconnHE = new MySqlConnection(ConfigurationManager.ConnectionStrings["HigherEducation"].ToString());
        public DataTable BindRole()
        {
            DataTable vds = new DataTable();
            try
            {
                if (vconnHE.State == ConnectionState.Open)
                {
                    vconnHE.Close();
                }
                MySqlDataAdapter vadap = new MySqlDataAdapter("select UsertypeCd as value, UserType as text from tblusertype;", vconnHE);
                vconnHE.Open();
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.Text;
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
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "BindRole";
                clsLogger.SaveException();
            }
            if (vconnHE.State == ConnectionState.Open)
                vconnHE.Close();
            return vds;
        }
        public DataTable BindUsers(String RoleID)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlDataAdapter vadap = new MySqlDataAdapter("select UserID as value, UserName as text from tbllogin where usertype = @RoleID order by UserName", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.Text;
                vadap.SelectCommand.Parameters.AddWithValue("@RoleID", RoleID);
                vadap.Fill(dt);
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "BindUsers";
                clsLogger.SaveException();
            }
            return dt;
        }
        public DataTable BindUserBasedModules(String UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlDataAdapter vadap = new MySqlDataAdapter(@"SELECT CONCAT('1_', ModuleCode) AS value, ModuleName AS text
FROM tblmodulemaster WHERE  NOT EXISTS (  SELECT 1  FROM tbllogin  WHERE UserID = @UserID    AND FIND_IN_SET(CONCAT('1_', tblmodulemaster.ModuleCode), ModulesGranted) > 0 ) order by trim(ModuleName);", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.Text;
                vadap.SelectCommand.Parameters.AddWithValue("@UserID", UserID);
                vadap.Fill(dt);
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "BindUserBasedModules";
                clsLogger.SaveException();
            }
            return dt;
        }
        public DataTable BindRoleBasedModules(String UserTypeID, String CollegeTypeID)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlDataAdapter vadap = new MySqlDataAdapter(@"SELECT CONCAT('1_', ModuleCode) AS value, ModuleName AS text
FROM tblmodulemaster WHERE  NOT EXISTS (  SELECT 1  FROM tbllogin  
WHERE UserType=@UserTypeID and  collegeId in (select collegeid from dhe_legacy_college where collegetype=@CollegeTypeID)
    AND FIND_IN_SET(CONCAT('1_', tblmodulemaster.ModuleCode), ModulesGranted) > 0 Limit 1 ) order by trim(ModuleName);", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.Text;
                vadap.SelectCommand.Parameters.AddWithValue("@UserTypeID", UserTypeID);
                vadap.SelectCommand.Parameters.AddWithValue("@CollegeTypeID", CollegeTypeID);
                vadap.Fill(dt);
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "BindRoleBasedModules";
                clsLogger.SaveException();
            }
            return dt;
        }

        public DataTable GetGrantedUserModules(String UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlDataAdapter vadap = new MySqlDataAdapter(@"SELECT ROW_NUMBER() OVER (ORDER BY CONCAT('1_', ModuleCode)) AS SerialNumber, CONCAT('1_', ModuleCode) AS ModuleCode, ModuleName
FROM tblmodulemaster WHERE  EXISTS (  SELECT 1  FROM tbllogin  WHERE UserID = @UserID    AND FIND_IN_SET(CONCAT('1_', tblmodulemaster.ModuleCode), ModulesGranted) > 0 ) order by trim(ModuleCode);", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.Text;

                vadap.SelectCommand.Parameters.AddWithValue("@UserID", UserID);

                vadap.Fill(dt);
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "GetGrantedUserModules";
                clsLogger.SaveException();
            }
            return dt;
        }

        public DataTable GetGrantedRoleModules(String UserTypeID, string CollegeTypeID)
        {
            DataTable dt = new DataTable();
            try
            {
                MySqlDataAdapter vadap = new MySqlDataAdapter(@"SELECT ROW_NUMBER() OVER (ORDER BY CONCAT('1_', ModuleCode)) AS SerialNumber, CONCAT('1_', ModuleCode) AS ModuleCode, ModuleName
FROM tblmodulemaster WHERE  EXISTS (  SELECT 1  FROM tbllogin 
 WHERE UserType= @UserTypeID and  collegeId in (select collegeid from dhe_legacy_college where collegetype=@CollegeTypeID)
 AND FIND_IN_SET(CONCAT('1_', tblmodulemaster.ModuleCode), ModulesGranted) > 0 limit 1) order by trim(ModuleCode);", vconnHE);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes
                vadap.SelectCommand.CommandType = CommandType.Text;

                vadap.SelectCommand.Parameters.AddWithValue("@UserTypeID", UserTypeID);
                vadap.SelectCommand.Parameters.AddWithValue("@CollegeTypeID", CollegeTypeID);

                vadap.Fill(dt);
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "GetGrantedRoleModules";
                clsLogger.SaveException();
            }
            return dt;
        }

        public int UpdateGrantedUserModules(String UserID, string ModuleID)
        {
            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(@"update tbllogin set ModulesGranted = CONCAT(ModulesGranted, ',', @ModuleID)  where userID = @UserID", vconnHE)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.Text,
                };
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);

                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "UpdateGrantedUserModules";
                clsLogger.SaveException();
            }
            finally
            {
                vconnHE.Close();
            }
            return result;
        }

        public int UpdateGrantedRoleModules(String UserTypeID, String CollegeTypeID, String ModuleID)
        {
            //DataTable dt = new DataTable();
            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(@"update tbllogin set ModulesGranted = CONCAT(ModulesGranted, ',', @ModuleID)   where UserType=@UserTypeID and collegeId in (select collegeid from dhe_legacy_college where collegetype=@CollegeTypeID);", vconnHE)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.Text,
                };
                cmd.Parameters.AddWithValue("@UserTypeID", UserTypeID);
                cmd.Parameters.AddWithValue("@CollegeTypeID", CollegeTypeID);
                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);

                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "UpdateGrantedRoleModules";
                clsLogger.SaveException();
            }
            finally
            {
                vconnHE.Close();
            }
            return result;
        }

        public int UpdateRevokedUserModules(String UserID, string ModuleID)
        {
            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(@"UPDATE tbllogin
SET ModulesGranted = TRIM(BOTH ',' FROM REPLACE(CONCAT(',', ModulesGranted, ','), concat( ',', @ModuleID,',') , ','))
WHERE userid = @UserID AND FIND_IN_SET(@ModuleID, ModulesGranted) > 0; ", vconnHE)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.Text,
                };
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);

                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "UpdateRevokedUserModules";
                clsLogger.SaveException();
            }
            finally
            {
                vconnHE.Close();
            }
            return result;
        }

        public int UpdateRevokedRoleModules(String UserTypeID, String CollegeTypeID, String ModuleID)
        {
            //DataTable dt = new DataTable();
            int result = 0;
            try
            {
                MySqlCommand cmd = new MySqlCommand(@"UPDATE tbllogin
SET ModulesGranted = TRIM(BOTH ',' FROM REPLACE(CONCAT(',', ModulesGranted, ','), concat( ',', @ModuleID,',') , ','))
WHERE usertype = @UserTypeID AND FIND_IN_SET(@ModuleID, ModulesGranted) > 0  and collegeId in (select collegeid from dhe_legacy_college where collegetype=@CollegeTypeID);", vconnHE)
                {
                    CommandTimeout = 120,
                    CommandType = CommandType.Text,
                };
                cmd.Parameters.AddWithValue("@UserTypeID", UserTypeID);
                cmd.Parameters.AddWithValue("@CollegeTypeID", CollegeTypeID);
                cmd.Parameters.AddWithValue("@ModuleID", ModuleID);

                if (vconnHE.State == ConnectionState.Closed)
                {
                    vconnHE.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionError = ex.Message;
                clsLogger.ExceptionPage = "DHE/DataAccess/GrantRevokeContext";
                clsLogger.ExceptionMsg = "UpdateRevokedRoleModules";
                clsLogger.SaveException();
            }
            finally
            {
                vconnHE.Close();
            }
            return result;
        }
    }
}