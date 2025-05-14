using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace HigherEducation.DataAccess
{
    public class MasterContext
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        #region ConnectionString
        static readonly string ConStr = ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        MySqlConnection connection = new MySqlConnection(ConStr);

        static readonly string HigherEducationR = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        readonly MySqlConnection connection_readonly = new MySqlConnection(HigherEducationR);
        #endregion;


        public DataSet getCandidateMaterData(string RegID)
        {
            DataSet dt = new DataSet();
            //List<CandidateDetail> lst = new List<CandidateDetail>();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand cmd = new MySqlCommand("GetCandiateMasterData", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("P_Reg_Id", RegID);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.DataAccess.MasterDbContext.getCandidateMaterData(),'" + RegID + "'");
            }
            connection.Close();
            return dt;

        }

    }
}