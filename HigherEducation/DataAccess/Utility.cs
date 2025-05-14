using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HigherEducation.DataAccess
{
    public class Utility
    {
        public static String GetConString()
        {
            return ConfigurationManager.ConnectionStrings["Dbconnection"].ConnectionString;
        }
    }
}