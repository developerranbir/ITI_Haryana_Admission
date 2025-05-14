using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace HigherEducation.Models
{
    public class RSHigherEdu
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["HigherEducation"].ConnectionString;

        public void BindDDLCommon(DataSet ds, DropDownList ddl, string ddlvalue, string ddltext)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl.DataSource = ds;
                    ddl.DataValueField = ddlvalue.Trim();
                    ddl.DataTextField = ddltext.Trim();
                    ddl.DataBind();
                }
                else
                {
                    ddl.DataSource = null;
                    ddl.Items.Clear();
                }
            }
            else
            {
                ddl.DataSource = null;
                ddl.Items.Clear();
            }
        }
        public DataSet GetCollageData()
        {
            DataSet dt = new DataSet();
            try
            {
                dt = MySqlConnect.DBActions.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "getcollege");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            return dt;
        }

        public DataSet GetCourseList()
        {
            DataSet dt = new DataSet();
            try
            {
                dt = MySqlConnect.DBActions.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "RSGetCourseList");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            return dt;
        }

        public DataSet BindCourse(string college_id,string session_id)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlParameter[] p = new MySqlParameter[2];
                p[0] = new MySqlParameter("@college_id", college_id);
                p[1] = new MySqlParameter("@session_id", session_id);
                dt = MySqlConnect.DBActions.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "GetCourse", p);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            return dt;
        }


        public DataSet GetMaritDataList(string PCollegeID,string PCourse)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlParameter[] p = new MySqlParameter[2];
                p[0] = new MySqlParameter("@PCollegeID", PCollegeID);
                p[1] = new MySqlParameter("@PCourse", PCourse);
                dt = MySqlConnect.DBActions.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "RSGetMaritDataList", p);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            return dt;
        }


        public DataSet BindCutOffList(string PCourse)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlParameter[] p = new MySqlParameter[1];
                p[0] = new MySqlParameter("@PCourse", PCourse);
                dt = MySqlConnect.DBActions.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "RSGetCutOffList", p);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            return dt;
        }

        public DataSet D_CutOffDdl(int? p_college, int? p_course, int? p_section, int? p_choice)
        {
            DataSet dt = new DataSet();
            try
            {
                MySqlParameter[] p = new MySqlParameter[4];
                p[0] = new MySqlParameter("@P_College_id", p_college);
                p[1] = new MySqlParameter("@P_Course_id", p_course);
                p[2] = new MySqlParameter("@P_Section_id", p_section);
                p[3] = new MySqlParameter("@P_Choice_id", p_choice);
                dt = MySqlConnect.DBActions.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "D_cutOffDdl", p);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            return dt;
        }
    }
}