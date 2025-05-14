using System.Configuration;

namespace HigherEducation.Models
{
    public static class ConnectionString
    {
        public static string HEBL = ConfigurationManager.ConnectionStrings["Dbconnection"].ToString();
        public static string Hconn = ConfigurationManager.ConnectionStrings["mysqlconstr"].ToString();


    }
}