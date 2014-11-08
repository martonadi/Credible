using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace CredentialsDemo
{
    /// <summary>
    /// Summary description for ActiveDirectoryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class CredentialWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<string> GetEmailAddress(string prefixText)
        {
            List<string> strList = ConvertDataSetToArrayList(prefixText);
            var v = (from m in strList where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).ToList();
            if (v.Count == 0)
            {
                v.Clear();
                //v.Add("No Matching Record Found");
            }
            return v;
        }

        [WebMethod]
        public List<string> ConvertDataSetToArrayList(string txt)
        {
            string strConnection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string strsel = "select Client from tblCredential where client like '" + txt + "%' order by Client";
            SqlConnection con = new SqlConnection(strConnection);
			con.Open();
            SqlDataAdapter adp = new SqlDataAdapter(strsel, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            List<string> str = new List<string>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            return str;
        }

        [WebMethod]
        public List<string> GetProjectName(string prefixText)
        {
            List<string> strList = ConvertDataSetToArrayList1(prefixText);
            var v = (from m in strList where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).ToList();
            if (v.Count == 0)
            {
                v.Clear();
                //v.Add("No matching record found");
            }
            return v;
        }

        [WebMethod]
        public List<string> ConvertDataSetToArrayList1(string txt)
        {
            string strConnection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            string strsel = "select Client from tblCredential where client like '" + txt + "%' order by Client";
            SqlConnection con = new SqlConnection(strConnection);
			con.Open();
            SqlDataAdapter adp = new SqlDataAdapter(strsel, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            List<string> str = new List<string>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Add(ds.Tables[0].Rows[i][0].ToString());
                }
            }
            return str;
        }
    }
}
