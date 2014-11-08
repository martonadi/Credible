using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

namespace CMS
{
    public partial class ImportData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
           /* string sFilePath = "E:\\Migration sheets\\REALESTATE.xls";
            string sConnection = "Provider=Microsoft.Jet.OLEDB.4.0;DataSource=" + sFilePath + ";Extended Properties=Excel 8.0;";
            OleDbConnection dbCon = new OleDbConnection(sConnection);
            dbCon.Open();

            // Get All Sheets Name
            DataTable dtSheetName = dbCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // Retrive the Data by Sheetwise

            DataSet dsOutput = new DataSet();
            for (int nCount = 0; nCount < dtSheetName.Rows.Count; nCount++)
            {
                string sSheetName = dtSheetName.Rows[nCount]["TABLE_NAME"].ToString();
                string sQuery = "Select * From [" + sSheetName + "]";
                OleDbCommand dbCmd = new OleDbCommand(sQuery, dbCon);
                OleDbDataAdapter dbDa = new OleDbDataAdapter(dbCmd);
                DataTable dtData = new DataTable();
                dbDa.Fill(dtData);
                dsOutput.Tables.Add(dtData);
            }
            dbCon.Close();
            //return dsOutput;*/

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=REALESTATE.xls;Extended Properties='Excel 8.0;HDR=Yes;'");
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", con);
            DataSet ds = new DataSet(); 
            da.Fill(ds);

        }
    }
}