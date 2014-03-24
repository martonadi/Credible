using System;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace CredentialsDemo.Common
{
    public class Logger
    {
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();

        public void LogWriter(string strmessage, string strCreatedBy = null)
        {
			SqlConnection con = new SqlConnection(strcon);
			con.Open();
			string strSql = "insert into tbl_LogDetails(LogName,CreatedBy) values('" + strmessage.Replace("'", "") + 
				"','" + strCreatedBy + "')";
			SqlCommand cmd = new SqlCommand(strSql, con);
			cmd.ExecuteNonQuery();
			cmd.Dispose();
			con.Close();
		}
		public void ErrorWriter(string strmessage, string strUserName)
		{
			SqlConnection con = new SqlConnection(strcon);
			con.Open();
			string strSql = "insert into tbl_ErrorDetails(ErrorName,CreatedBy) values('" + strmessage.Replace("'", "") + 
				"','" + strUserName + "')";
			SqlCommand cmd = new SqlCommand(strSql, con);
			cmd.ExecuteNonQuery();
			cmd.Dispose();
			con.Close();
		}
	}
}
