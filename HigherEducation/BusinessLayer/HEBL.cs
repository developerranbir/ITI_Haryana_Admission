using HigherEducation.HigherEducation;
using HigherEducation.Models;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Diagnostics;
using HigherEducation.DataAccess;

namespace HigherEducation.BusinessLayer
{
    public class HEBL
    {
        SqlConnection vconn = new SqlConnection(ConnectionString.HEBL);
        Log objlog = new Log();

        public object AddFee(string asd1, string asd12, string asd13, string asd14)
        {
            string result = "1";
            SqlConnection vconn = new SqlConnection(ConnectionString.HEBL);
            if (vconn.State == ConnectionState.Closed)
                vconn.Open();
            SqlDataAdapter vcomm = new SqlDataAdapter("LoginUser", vconn);
            vcomm.SelectCommand.CommandType = CommandType.StoredProcedure;
            vcomm.SelectCommand.Parameters.AddWithValue("@asd1", asd1);
            vcomm.SelectCommand.Parameters.AddWithValue("@asd12", asd12);
            vcomm.SelectCommand.Parameters.AddWithValue("@asd13", asd13);
            vcomm.SelectCommand.Parameters.AddWithValue("@asd14", asd14);
          
            vcomm.SelectCommand.CommandTimeout = 600;
            DataSet vds = new DataSet();
            vcomm.Fill(vds);
            if (vconn.State == ConnectionState.Open)
                vconn.Close();
            
            return result;
        }

    }
}