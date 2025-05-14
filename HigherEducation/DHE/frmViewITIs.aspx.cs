using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using HigherEducation.BusinessLayer;
using NLog;

namespace HigherEducation.DHE
{
    public partial class frmViewITIs : System.Web.UI.Page
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        static readonly string HigherEducationR = ConfigurationManager.ConnectionStrings["HigherEducationR"].ConnectionString;
        readonly MySqlConnection connection_readonly = new MySqlConnection(HigherEducationR);
        protected void Page_Load(object sender, EventArgs e)
        {
            string UserType = "1";
            eDISHAutil eSessionMgmt = new eDISHAutil();
            eSessionMgmt.CheckSession(UserType);
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
            {
                Response.Redirect("~/DHE/frmlogin.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                fillGrid();
            }
            eSessionMgmt.SetCookie();

        }

        protected void fillGrid()
        {
            try
            {
                if (connection_readonly.State == ConnectionState.Closed)
                {
                    connection_readonly.Open();
                }
                DataSet vds = new DataSet();
                MySqlDataAdapter vadap = new MySqlDataAdapter("GetITIList", connection_readonly);
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.SelectCommand.CommandType = CommandType.StoredProcedure;
                vadap.SelectCommand.CommandTimeout = 600; // 10 minutes

                vadap.Fill(vds);
                if (connection_readonly.State == ConnectionState.Open)
                    connection_readonly.Close();

                grdITIList.DataSource = vds;
                grdITIList.DataBind();

            }
            catch (Exception ex)
            {
                logger = LogManager.GetLogger("databaseLogger");
                logger.Error(ex, "Error occured in HigherEducation.frmViewITIs.Page_Load");
            }
            if (connection_readonly.State == ConnectionState.Open)
            {
                connection_readonly.Close();
            }
        }
        protected void grdITIList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdITIList.PageIndex = e.NewPageIndex;
            fillGrid();
        }
    }
}