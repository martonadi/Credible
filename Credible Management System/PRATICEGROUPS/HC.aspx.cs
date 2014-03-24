using System;
using System.Data;
using System.Text;
using CredentialsDemo.Common;
using System.Data.SqlClient;
using System.Collections;
using Telerik.Web.UI;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class HCEntry : System.Web.UI.Page
    {
        CallingSP objSp = new CallingSP();
		Logger objLog = new Logger();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();

		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsPostBack)
				{
					if (Session["sessionUserInfo"] != null)
					{
						hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
						objLog.LogWriter("HC : Page_Load starts", hidName.Value);
					}
					else
					{
						Response.Redirect("~/TimeOut.aspx");
					}
					
                    objSp.LoadValues("usp_PensionSchemeList", "PensionScheme", "PensionSchemeId", telrad: cbo_HCC_PensionSchemeHC);
                    LoadHUMANCAPTIALWorkTypeView();

                    if (Session["sessHCSS"] != null)
                    {
                        if (Session["sessHCSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessHCSS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {

                                            case "WorkTypeIdHC":
                                                //objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_HCC_WorkTypeIdHC);
                                                CheckTheOneLevelItems(RadTreeView1, strvals[i].Split('~')[1]);
                                                break;
                                            case "PensionSchemeHC":
                                                objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_HCC_PensionSchemeHC);
                                                cbo_HCC_PensionSchemeHC.Visible = true;
                                                break;
                                            case "SubWorkTypeMSId":
                                                CheckTheTwoLevelItems(RadTreeView1, strvals[i].Split('~')[1]);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Session["sessHCClear"] == null)
                        {
                            if (Session["SessionSearchPG"] != null)
                            {
                                DataTable dt = (DataTable)(Session["SessionSearchPG"]);
                                CallingSP objSP = new CallingSP();

                                if (dt.Rows.Count > 0)
                                {
                                    string strUserID = dt.Rows[0]["CredentialID"].ToString();

                                    /*if (!string.IsNullOrEmpty(dt.Rows[0]["WorkTypeIdHC"].ToString().Trim()))
                                    {
                                        
                                    }*/

                                    //WorkType TBD

                                    string[] strWorkType = new string[2];
                                    strWorkType = objSP.ReturnMultiselectValues("usp_CredentialWorkTypeHCSource", strUserID);
                                    if (strWorkType != null)
                                    {
                                        CheckTheOneLevelItems(RadTreeView1, strWorkType[0].ToString());
                                        strWorkType = null;
                                    }
                                    //Sub WorkType TBD
                                    string[] strSubWorkType = new string[2];
                                    strSubWorkType = objSP.ReturnMultiselectValues("usp_CredentialSubWorkTypeHCSource", strUserID);
                                    if (strSubWorkType != null)
                                    {
                                        CheckTheTwoLevelItems(RadTreeView1, strSubWorkType[0].ToString());
                                        strSubWorkType = null;
                                    }

                                    if (!string.IsNullOrEmpty(dt.Rows[0]["PensionSchemeHC"].ToString().Trim()))
                                    {
                                        objSP.MatchDropDownValuesText(dt.Rows[0]["PensionSchemeHC"].ToString(), cbo_HCC_PensionSchemeHC);
                                        cbo_HCC_PensionSchemeHC.Visible = true;
                                    }
                                    SaveHCData();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadHUMANCAPTIALWorkTypeView()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            cmd.CommandText = "select distinct(tblWorkType.businessgroupid),tblBusinessGroup.Business_Group FROM  tblWorkType INNER JOIN tblBusinessGroup ON tblWorkType.BusinessGroupId = tblBusinessGroup.BusinessGroupId ORDER BY tblBusinessGroup.business_group asc";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(ds);
            }

            cmd.Dispose();
            con.Close();


            SortedList sort = new SortedList();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (sort.Contains(i) == false)
                {
                    string str = string.Empty;

                    switch (ds.Tables[0].Rows[i][1].ToString().ToUpper())
                    {

                        case "HUMAN CAPITAL (HC)":
                            str = "HUMAN CAPTIAL";
                            break;

                        default:
                            break;
                    }
                    sort.Add(i, str);
                }
            }

            foreach (DictionaryEntry ent in sort)
            {
                string str = ent.Value.ToString().ToUpper();

                if (str == "HUMAN CAPTIAL")
                {
                    RadTreeNode root11 = new RadTreeNode(str);
                    root11.Text = str;
					root11.Value = str;
                    root11.Checkable = false;
                    RadTreeView1.Nodes.Add(root11);

                    DataTable dt = objSp.GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='10' AND EXCLUDE ='0'");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //root11.Nodes.Add(new RadTreeNode(dt.Rows[i][0].ToString()));

                            RadTreeNode chd = new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            root11.Nodes.Add(chd);
                            chd.Checkable = true;

                            DataTable dtSubWorkType = objSp.GetDataTable("SELECT subwork_type,subworktypeid FROM TBLsubWORKTYPE WHERE worktypeid='" + dt.Rows[i][1].ToString() + "' AND EXCLUDE ='0' order by subwork_type asc");

                            if (dtSubWorkType.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
                                {
                                    RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim().Replace(dt.Rows[i][0].ToString().Trim() + ":", ""));
                                    chd2.Text = dtSubWorkType.Rows[j][0].ToString().Trim().Replace(dt.Rows[i][0].ToString().Trim() + ":", "");
                                    chd2.Value = dtSubWorkType.Rows[j][1].ToString();
                                    chd.Nodes.Add(chd2);
                                }
                            }
                        }
                    }
                }
            }
        }
		protected string ReturnString(string str)
		{
			return str.Substring(8, str.Length - 8);
		}

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SaveHCData();
                Label2.Visible = true;
                Label2.Text = "Details have been successfully captured. Click close button to close this window.";
                //hrline.Visible = true;
                Session["sessHCClear"] = null;
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("HC : btnOk_Click Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void SaveHCData()
        {
            if (Session["sessHCSS"] != null)
            {
                Session.Remove("sessHCSS");
            }
            if (Session["sessHCMS"] != null)
            {
                Session.Remove("sessHCMS");
            }

            StringBuilder strSS = new StringBuilder();
            StringBuilder strMS = new StringBuilder();
            string strHCSWT = string.Empty;
            string strHCWT = string.Empty;
            Hashtable ht = new Hashtable();

            foreach (RadTreeNode node in RadTreeView1.Nodes)
            {
                if (node.Text.ToUpper() == "HUMAN CAPTIAL") /* Two Level */
                {
                    if (node.Nodes.Count > 0)
                    {
                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            if (node.Nodes[i].Checked == true)
                            {
                                if (ht.Contains(node.Nodes[i].Value) == false)
                                {
                                    if (string.IsNullOrEmpty(strHCWT))
                                    {
                                        strHCWT = node.Nodes[i].Value + ",";
                                    }
                                    else
                                    {
                                        strHCWT = strHCWT + node.Nodes[i].Value + ",";
                                    }
                                    ht.Add(node.Nodes[i].Value, node.Nodes[i].Value);
                                }
                            }
                        }

                        string strID = string.Empty;

                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            bool bln = false;
                            for (int j = 0; j < node.Nodes[i].Nodes.Count; j++)
                            {

                                if (node.Nodes[i].Nodes[j].Checked == true)
                                {
                                    if (bln == false)
                                    {
                                        if (ht.Contains(node.Nodes[i].Value) == false)
                                        {
                                            if (string.IsNullOrEmpty(strHCWT))
                                            {
                                                strHCWT = node.Nodes[i].Value + ",";
                                            }
                                            else
                                            {
                                                strHCWT = strHCWT + node.Nodes[i].Value + ",";
                                            }
                                            /*if (string.IsNullOrEmpty(strText))
                                            {
                                                strText = "'" + node.Nodes[i].Text + "',";
                                            }
                                            else
                                            {
                                                strText = strText + "'" + node.Nodes[i].Text + "',";
                                            }*/
                                            ht.Add(node.Nodes[i].Value, node.Nodes[i].Value);
                                        }

                                        
                                        bln = true;
                                    }
                                    if (string.IsNullOrEmpty(strHCSWT))
                                    {
                                        strHCSWT = node.Nodes[i].Nodes[j].Value + ",";
                                    }
                                    else
                                    {
                                        strHCSWT = strHCSWT + node.Nodes[i].Nodes[j].Value + ",";
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(strHCWT))
                        {
                            strHCWT = strHCWT.Substring(0, strHCWT.Length - 1);
                        }
                        if (!string.IsNullOrEmpty(strHCSWT))
                        {
                            strHCSWT = strHCSWT.Substring(0, strHCSWT.Length - 1);
                        }
                    }
                }
            }
			
            if (!string.IsNullOrEmpty(strHCWT))
            {
                strMS.Append(ReturnString(lbl_HCC_WorkTypeIdHC.ID));
				strMS.Append("~");
				strMS.Append(strHCWT);
                strMS.Append("|");
                /*strSS.Append("WorkTypeMS"); strSS.Append("~"); strSS.Append(txt_CRD_Work_Type.Text.Trim());//[1] WorkTypeId(s)
                strSS.Append("|");*/
                strSS.Append("WorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strHCWT);//[1] WorkTypeId(s)
                strSS.Append("|");
            }

            if (!string.IsNullOrEmpty(strHCSWT))
            {
                strMS.Append("SubWork_Type");
				strMS.Append("~");
				strMS.Append(strHCSWT);
                strMS.Append("|");
                /*strSS.Append("SubWorkTypeMS"); strSS.Append("~"); strSS.Append(txt_CRD_SubWork_Type.Text.Trim());//[1] WorkTypeId(s)
                strSS.Append("|");*/
                strSS.Append("SubWorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strHCSWT);//[1] WorkTypeId(s)
                strSS.Append("|");
            }

            string strcbo_HCC_PensionSchemeHC = string.Empty;
            if (cbo_HCC_PensionSchemeHC.SelectedItem.Text.ToUpper() == "SELECT")
            {
                strSS.Append(ReturnString(lbl_HCC_PensionSchemeHC.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);//[2] TransactionValueId
                strSS.Append("|");
            }
            else
            {
                strcbo_HCC_PensionSchemeHC = cbo_HCC_PensionSchemeHC.SelectedItem.Value;
                strSS.Append(ReturnString(lbl_HCC_PensionSchemeHC.ID));
				strSS.Append("~");
				strSS.Append(strcbo_HCC_PensionSchemeHC);//[2] TransactionValueId
                strSS.Append("|");
            }

            string strSSstr = strSS.ToString();
            if (!(string.IsNullOrEmpty(strSSstr)))
            {
                strSS = strSS.Remove(strSS.Length - 1, 1);
                Session.Add("sessHCSS", strSS);
                strSS = null;
                strSSstr = null;
            }
            string strMSstr = strMS.ToString();
            if (!(string.IsNullOrEmpty(strMSstr)))
            {
                strMS = strMS.Remove(strMS.Length - 1, 1);
                Session.Add("sessHCMS", strMS);
                strMS = null;
                strMSstr = null;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Label2.Visible = false;
                Label2.Text = string.Empty;
                //hrline.Visible = false;
                if (Session["sessHCSS"] != null)
                {
                    Session.Remove("sessHCSS");
                }

                cbo_HCC_PensionSchemeHC.SelectedIndex = 0;
                tr_HCC_PensionSchemeHC.Visible = false;

                foreach (RadTreeNode node in RadTreeView1.GetAllNodes())
                {
                    node.Checked = false;
                    node.Expanded = false;
                }
                //cbo_HCC_WorkTypeIdHC.SelectedIndex = 0;

                if (Session["sessHCClear"] != null)
                {
                    Session.Remove("sessHCClear");
                }
                Session.Add("sessHCClear", "0");
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("HC : btnClear_Click Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private static void CheckTheOneLevelItems(RadTreeView tv, string strItems)
        {
            foreach (RadTreeNode node in tv.Nodes)
            {
                if (node.Text.ToUpper() == "HUMAN CAPTIAL") /* One Level */
                {
                    for (int i = 0; i < node.Nodes.Count; i++)
                    {
                        for (int j = 0; j < strItems.Split(',').Length; j++)
                        {
                            if (node.Nodes.FindNodeByValue(strItems.Split(',')[j]) != null)
                            {
                                node.Nodes.FindNodeByValue(strItems.Split(',')[j]).Checked = true;
                                node.Nodes.FindNodeByValue(strItems.Split(',')[j]).Expanded = true;
								node.Nodes.FindNodeByValue(strItems.Split(',')[j]).ExpandParentNodes();
                            }
                        }
                    }
                }
            }
        }

		private static void CheckTheTwoLevelItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				if (node.Text.ToUpper() == "HUMAN CAPTIAL")
				{
					for (int i = 0; i < node.Nodes.Count; i++)
					{
						for (int j = 0; j < node.Nodes[i].Nodes.Count; j++)
						{
							for (int k = 0; k < strItems.Split(',').Length; k++)
							{
								if (node.Nodes[i].Nodes.FindNodeByValue(strItems.Split(',')[k]) != null)
								{
									node.Nodes[i].Nodes.FindNodeByValue(strItems.Split(',')[k]).Checked = true;
									node.Nodes[i].Nodes.FindNodeByValue(strItems.Split(',')[k]).Expanded = true;
									node.Nodes[i].Nodes.FindNodeByValue(strItems.Split(',')[k]).ExpandParentNodes();
								}
							}
						}
					}
				}
			}
		}
	}
}
