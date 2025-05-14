using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using HigherEducation.HigherEducation;
using HigherEducation.Models;

/// <summary>
/// Summary description for clsConnString
/// </summary>
namespace HigherEducation.BusinessLayer
{
    public class clsConnString
    {
        public clsConnString()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string ConnStringVC
        {
            get
            {
                return (ConfigurationManager.ConnectionStrings["Constring_VC"].ConnectionString);
            }
        }


    }

}