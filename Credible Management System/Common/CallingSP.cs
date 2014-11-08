using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Aj = AjaxControlToolkit;
using Telerik.Web.UI;

namespace CredentialsDemo.Common
{
    public class CallingSP
    {
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();

        public void LoadValues(string strSPName, string strTextField = null, string strValueField = null, string strParam1 = null, string strParam2 = null, Aj.ComboBox cbo = null, CheckBoxList chk = null, GridView grd = null, DropDownList drp = null, RadioButtonList rdo = null, RadComboBox telrad = null, string strCheck = null, RadListBox telradlist = null)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            cmd.CommandText = strSPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            if (strParam1 != null && strParam2 != null)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter(strParam1.Split('~')[0].ToString(), strParam1.Split('~')[1].ToString());
                param[1] = new SqlParameter(strParam2.Split('~')[0].ToString(), strParam2.Split('~')[1].ToString());
                cmd.Parameters.AddRange(param);
            }

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds);
            }

            if (cbo != null)
            {
                cbo.DataSource = ds.Tables[0];
                cbo.DataTextField = strTextField;
                cbo.DataValueField = strValueField;
                cbo.DataBind();

                if (strSPName.ToUpper() != "USP_LANGUAGEOFDISPUTELIST")
                {
                    cbo.Items.Insert(0, new ListItem("Select", "-1"));
                }
            }

            if (drp != null)
            {
                drp.DataSource = ds.Tables[0];
                drp.DataTextField = strTextField;
                drp.DataValueField = strValueField;
                drp.DataBind();
                drp.Items.Insert(0, new ListItem("Select", "-1"));
            }

            if (telrad != null)
            {
                telrad.DataSource = ds.Tables[0];
                telrad.DataTextField = strTextField;
                telrad.DataValueField = strValueField;
                telrad.DataBind();
                telrad.Filter = (RadComboBoxFilter)Convert.ToInt32(0);

                if (strCheck == null || strCheck != "0")
                {
                    if (strSPName.ToUpper() != "USP_LANGUAGEOFDISPUTELIST")
                    {
                        telrad.Items.Insert(0, new RadComboBoxItem("Select", "-1"));
                        telrad.SelectedIndex = -1;
                    }
                }
            }

            if (chk != null)
            {
                chk.DataSource = ds.Tables[0];
                chk.DataTextField = strTextField;
                chk.DataValueField = strValueField;
                chk.DataBind();
            }

            if (grd != null)
            {
                grd.DataSource = ds.Tables[0];

                grd.DataBind();
            }
            if (rdo != null)
            {
                rdo.DataSource = ds.Tables[0];
                rdo.DataTextField = strTextField;
                rdo.DataValueField = strValueField;
                rdo.DataBind();
            }
            if (telradlist != null)
            {
                telradlist.DataSource = ds.Tables[0];
                telradlist.DataTextField = strTextField;
                telradlist.DataValueField = strValueField;
                telradlist.DataBind();
            }


            cmd.Dispose();
            ds.Dispose();
            con.Close();
        }

        public DataSet ReturnDataSet(string strSPName, string strParam1 = null, string strParam2 = null)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            cmd.CommandText = strSPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            if (strParam1 != null && strParam2 != null)
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter(strParam1.Split('~')[0].ToString(), strParam1.Split('~')[1].ToString());
                param[1] = new SqlParameter(strParam2.Split('~')[0].ToString(), strParam2.Split('~')[1].ToString());
                cmd.Parameters.AddRange(param);
            }
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds);
            }

            cmd.Dispose();
            con.Close();
            return ds;
        }
		
        public DataSet ReturnDataSetText(string strMatterNo)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            //cmd.CommandText = "select matter_no from tblCredential where matter_no is not null";
            cmd.CommandText = "select * from tblCredentialVersionRelation where matterno='" + strMatterNo + "'";
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds);
            }
            cmd.Dispose();
            con.Close();
            return ds;
        }
		
        public DataSet ReturnAdminDataSet(string strSPName, string strParam = null)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            cmd.CommandText = strSPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;

            if (strParam != null)
            {
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(strParam.Split('~')[0].ToString(), strParam.Split('~')[1].ToString());
                cmd.Parameters.AddRange(param);
            }

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds);
            }

            cmd.Dispose();
            con.Close();
            return ds;
        }
		
        public void MatchDropDownValues(string dr, AjaxControlToolkit.ComboBox drp)
        {
            if (!string.IsNullOrEmpty(dr))
            {
                drp.SelectedIndex = 0;
                if (drp.Items.FindByValue(dr) != null)
                {
                    ListItem itm = new ListItem();

                    itm.Text = drp.Items.FindByValue(dr).Text;
					itm.Value = dr;
                    drp.SelectedIndex = drp.Items.IndexOf(itm);
                }
            }
        }
		
        public void MatchDropDownValues(string dr, RadComboBox drp)
        {
            if (!string.IsNullOrEmpty(dr))
            {
                drp.SelectedIndex = 0;
                if (drp.Items.FindItemByValue(dr) != null)
                {
                    RadComboBoxItem itm = new RadComboBoxItem();
                    itm.Text = dr;
					itm.Value = drp.Items.FindItemByValue(dr).Value;
                    drp.SelectedIndex = (int)Convert.ToInt16(drp.Items.FindItemIndexByValue(itm.Value, false));
                }
            }
        }

        public void MatchDropDownValuesText(string dr, AjaxControlToolkit.ComboBox drp)
        {
            if (!string.IsNullOrEmpty(dr))
            {
                drp.SelectedIndex = 0;
                if (drp.Items.FindByText(dr) != null)
                {
                    ListItem itm = new ListItem();

                    itm.Text = dr;
					itm.Value = drp.Items.FindByText(dr).Value;
                    drp.SelectedIndex = drp.Items.IndexOf(itm);
                }
            }
        }

        public void MatchDropDownValuesText(string dr, RadComboBox drp)
        {
            if (!string.IsNullOrEmpty(dr))
            {
                drp.SelectedIndex = 0;
                if (drp.Items.FindItemByText(dr) != null)
                {
                    RadComboBoxItem itm = new RadComboBoxItem();

                    itm.Text = dr;
					itm.Value = drp.Items.FindItemByText(dr).Value;
                    //drp.SelectedIndex = drp.Items.IndexOf(itm1);
                    drp.SelectedIndex = (int)Convert.ToInt16(drp.Items.FindItemIndexByText(itm.Text, false));
                }
            }
        }

        public void MatchDropDownValues(string dr, DropDownList drp)
        {
            if (!string.IsNullOrEmpty(dr))
            {
                drp.SelectedIndex = 0;
                if (drp.Items.FindByText(dr) != null)
                {
                    ListItem itm = new ListItem();
                    itm.Value = drp.Items.FindByText(dr).Value;
					itm.Text = dr;
                    drp.SelectedIndex = drp.Items.IndexOf(itm);
                }
            }
        }

        public string ReturnString(string strArray)
        {
            string str = string.Empty;

            if (strArray.Split('_').Length == 1)
            {
                str = strArray;
            }
            else if (strArray.Split('_').Length == 2)
            {
                str = strArray.Split('_')[0] + " " + strArray.Split('_')[1];
            }
            else if (strArray.Split('_').Length == 3)
            {
                str = strArray.Split('_')[0] + " " + strArray.Split('_')[1] + " " + strArray.Split('_')[2];
            }
            else if (strArray.Split('_').Length == 5)
            {
                str = strArray.Split('_')[0] + " " + strArray.Split('_')[1] + " " + strArray.Split('_')[2] + 
					" " + strArray.Split('_')[3] + " " + strArray.Split('_')[4];
            }

            return str;
        }
		
        public int ReturnScalar(string strSPName = null, string SQL = null)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand(SQL, con);

            object o = cmd.ExecuteScalar();
            string oo = o.ToString();
            int io = Convert.ToInt32(oo);

            cmd.Dispose();
            con.Close();
            return io;
        }
		
		
        public int ReturnNonQuery(string strSPName = null, string SQL = null)
        {

            SqlConnection con = new SqlConnection(strcon);
            con.Open();

            SqlCommand cmd = new SqlCommand(SQL, con);
            int i = cmd.ExecuteNonQuery();


            cmd.Dispose();
            con.Close();
            return i;
        }
		
        public string[] ReturnMultiselectValues(string strSPName, string strCredID)
        {
            string[] strReturnVals = new string[2];
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = strSPName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            SqlParameter[] par = new SqlParameter[1];
            par[0] = new SqlParameter("@TheRecord", Convert.ToInt32(strCredID));
            cmd.Parameters.AddRange(par);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(strReturnVals[0]))
                    {
                        strReturnVals[0] = dt.Rows[i][2].ToString().Trim();
                    }
                    else
                    {
                        strReturnVals[0] = strReturnVals[0] + "," + dt.Rows[i][2].ToString().Trim();
                    }
                    if (string.IsNullOrEmpty(strReturnVals[1]))
                    {
                        strReturnVals[1] = dt.Rows[i][3].ToString().Trim();
                    }
                    else
                    {
                        strReturnVals[1] = strReturnVals[1] + "," + dt.Rows[i][3].ToString().Trim();
                    }
                }
            }
            else
            {
                strReturnVals = null;
            }
            return strReturnVals;
        }
		
        public SqlDataReader GetDataReader(string strSQL)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            cmd.Dispose();
            return dr;
        }
        
        public int GetExecuteNonQuery(string strSQL)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand(strSQL, con);
            int i = cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            return i;
        }

        public DataTable GetDataTable(string query)
        {
            String ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);

            DataTable myDataTable = new DataTable();
            conn.Open();
            try
            {
                adapter.Fill(myDataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return myDataTable;
        }
    }
}
