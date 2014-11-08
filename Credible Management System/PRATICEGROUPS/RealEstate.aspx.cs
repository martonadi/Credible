using System;
using System.Data;
using System.Text;
using CredentialsDemo.Common;
using System.Data.SqlClient;
using System.Collections;
using Telerik.Web.UI;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class RealEstate : System.Web.UI.Page
    {
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
        CallingSP objSp = new CallingSP();
		Logger objLog = new Logger();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
				if (Session["sessionUserInfo"] != null)
				{
					hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
					objLog.LogWriter("RE : Page_Load starts", hidName.Value);
				}
				else
				{
					Response.Redirect("~/TimeOut.aspx");
				}
                if (!IsPostBack)
                {
                    LoadREALESTATEWorkTypeView();
                    if (Session["sessRESS"] != null)
                    {
                        if (Session["sessRESS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessRESS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "ClientTypeMS":
                                                txt_RES_Client_Type.Text = strvals[i].Split('~')[1].ToString();
                                                hid_RES_Client_Type_Text.Value = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "ClientTypeMSId":
                                                hid_RES_Client_Type.Value = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "WorkTypeMS":
                                                /* txt_RES_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                                 hid_RES_Work_Type_Text.Value = strvals[i].Split('~')[1].ToString();*/
                                                break;
                                            case "WorkTypeMSId":
                                                /* hid_RES_Work_Type.Value = strvals[i].Split('~')[1].ToString();*/
                                                CheckTheOneLevelItems(RadTreeView1, strvals[i].Split('~')[1].ToString());
                                                break;
                                            case "SubWorkTypeMSId":
                                                CheckTheTwoLevelItems(RadTreeView1, strvals[i].Split('~')[1].ToString());
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Session["sessREClear"] == null)
                        {
                            if (Session["SessionSearchPG"] != null)
                            {
                                DataTable dt = (DataTable)(Session["SessionSearchPG"]);
                                CallingSP objSP = new CallingSP();

                                if (dt.Rows.Count > 0)
                                {
                                    string strUserID = dt.Rows[0]["CredentialID"].ToString();

                                    //Client Type 
                                    string[] strClientType = new string[2];
                                    strClientType = objSP.ReturnMultiselectValues("usp_CredentialClientTypeSource", strUserID);
                                    if (strClientType != null)
                                    {
                                        if (!string.IsNullOrEmpty(strClientType[0].ToString()))
                                        {
                                            hid_RES_Client_Type.Value = strClientType[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strClientType[1]))
                                        {
                                            txt_RES_Client_Type.Text = strClientType[1].ToString();
                                            hid_RES_Client_Type_Text.Value = strClientType[1].ToString();
                                        }
                                        strClientType = null;
                                    }

                                    //Work Type 
                                    string[] strWorkType = new string[2];
                                    strWorkType = objSP.ReturnMultiselectValues("usp_CredentialWorkTypeRealEstateSource", strUserID);
                                    if (strWorkType != null)
                                    {
                                        /*if (!string.IsNullOrEmpty(strWorkType[0]))
                                        {
                                            hid_RES_Work_Type.Value = strWorkType[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strWorkType[1]))
                                        {
                                            txt_RES_Work_Type.Text = strWorkType[1].ToString();
                                            hid_RES_Work_Type_Text.Value = strWorkType[1].ToString();
                                        }*/
                                        CheckTheOneLevelItems(RadTreeView1, strWorkType[0].ToString());
                                        strWorkType = null;
                                    }

                                    //Sub WorkType TBD
                                    string[] strSubWorkType = new string[2];
                                    strSubWorkType = objSP.ReturnMultiselectValues("usp_CredentialSubWorkTypeRESource", strUserID);
                                    if (strSubWorkType != null)
                                    {
                                        /*if (!string.IsNullOrEmpty(strWorkType[0]))
                                        {
                                            hid_RES_Work_Type.Value = strWorkType[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strWorkType[1]))
                                        {
                                            txt_RES_Work_Type.Text = strWorkType[1].ToString();
                                            hid_RES_Work_Type_Text.Value = strWorkType[1].ToString();
                                        }*/
                                        CheckTheTwoLevelItems(RadTreeView1, strSubWorkType[0].ToString());
                                        strSubWorkType = null;
                                    }
                                }
                                SaveREData();
                            }
                        }
                    }
                }
                /* if (!string.IsNullOrEmpty(hid_RES_Work_Type_Text.Value.Trim()))
                 {
                     txt_RES_Work_Type.Text = hid_RES_Work_Type_Text.Value;
                 }
                 else
                 {
                     txt_RES_Work_Type.Text = string.Empty;
                 }*/
                if (!string.IsNullOrEmpty(hid_RES_Client_Type_Text.Value.Trim()))
                {
                    txt_RES_Client_Type.Text = hid_RES_Client_Type_Text.Value;
                }
                else
                {
                    txt_RES_Client_Type.Text = string.Empty;
                }
                img_RES_Client_Type.Attributes.Add("onClick", "LoadChild('" + lbl_RES_Client_Type.Text + "','" + lbl_RES_Client_Type.ID + "','" + hid_RES_Client_Type.ID + "');return false;");
                //img_RES_Work_Type.Attributes.Add("onClick", "LoadChild('" + lbl_RES_Work_Type.Text + "','" + lbl_RES_Work_Type.ID + "','" + hid_RES_Work_Type.ID + "');return false;");
				objLog.LogWriter("RE : Page_Load Ends", hidName.Value);
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("RE : Page_Load Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }
		
        private void LoadREALESTATEWorkTypeView()
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
                        case "REAL ESTATE":
                            str = "REAL ESTATE";
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

                if (str == "REAL ESTATE")
                {
                    RadTreeNode root11 = new RadTreeNode(str);
                    root11.Text = str;
					root11.Value = str;
                    root11.Checkable = false;
                    RadTreeView1.Nodes.Add(root11);

                    DataTable dt = objSp.GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='7' AND EXCLUDE ='0'");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //root11.Nodes.Add(new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));

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
                SaveREData();
                Label1.Visible = true;
                Label1.Text = "Details have been successfully captured. Click close button to close this window.";
                //hrline.Visible = true;
                Session["sessREClear"] = null;
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("RE : btnOk_Click Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void SaveREData()
        {
            if (Session["sessREMS"] != null)
            {
                Session.Remove("sessREMS");
            }
            if (Session["sessRESS"] != null)
            {
                Session.Remove("sessRESS");
            }
            StringBuilder strSS = new StringBuilder();
            StringBuilder strMS = new StringBuilder();

            string strhid_RES_Client_Type = string.Empty;
			if (!string.IsNullOrEmpty(hid_RES_Client_Type.Value))
            {
                strhid_RES_Client_Type = hid_RES_Client_Type.Value;
                strMS.Append(ReturnString(lbl_RES_Client_Type.ID));
				strMS.Append("~");
				strMS.Append(strhid_RES_Client_Type);
                strMS.Append("|");
                strSS.Append("ClientTypeMS");
				strSS.Append("~");
				strSS.Append(txt_RES_Client_Type.Text.Trim());//[1] WorkTypeId(s)
                strSS.Append("|");
                strSS.Append("ClientTypeMSId");
				strSS.Append("~");
				strSS.Append(strhid_RES_Client_Type);//[1] WorkTypeId(s)
                strSS.Append("|");
            }

            /*string strhid_RES_Work_Type = string.Empty;
            if (string.IsNullOrEmpty(hid_RES_Work_Type.Value))
            {
                strhid_RES_Work_Type = "NTA";
            }
            else
            {
                strhid_RES_Work_Type = hid_RES_Work_Type.Value;
                strMS.Append(ReturnString(lbl_RES_Work_Type.ID)); strMS.Append("~"); strMS.Append(strhid_RES_Work_Type);
                strMS.Append("|");
                strSS.Append("WorkTypeMS"); strSS.Append("~"); strSS.Append(txt_RES_Work_Type.Text.Trim());//[1] WorkTypeId(s)
                strSS.Append("|");
                strSS.Append("WorkTypeMSId"); strSS.Append("~"); strSS.Append(strhid_RES_Work_Type);//[1] WorkTypeId(s)
                strSS.Append("|");
            }
            */
            string strREWT = string.Empty;
            string strRESWT = string.Empty;
            Hashtable ht = new Hashtable();

            foreach (RadTreeNode node in RadTreeView1.Nodes)
            {
                if (node.Text.ToUpper() == "REAL ESTATE") /* Two Level */
                {
                    if (node.Nodes.Count > 0)
                    {
                        for (int i = 0; i < node.Nodes.Count; i++)
                        {
                            if (node.Nodes[i].Checked == true)
                            {
                                if (ht.Contains(node.Nodes[i].Value) == false)
                                {
                                    if (string.IsNullOrEmpty(strREWT))
                                    {
                                        strREWT = node.Nodes[i].Value + ",";
                                    }
                                    else
                                    {
                                        strREWT = strREWT + node.Nodes[i].Value + ",";
                                    }
                                    ht.Add(node.Nodes[i].Value, node.Nodes[i].Value);
                                }
                            }
                        }

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
                                            if (string.IsNullOrEmpty(strREWT))
                                            {
                                                strREWT = node.Nodes[i].Value + ",";
                                            }
                                            else
                                            {
                                                strREWT = strREWT + node.Nodes[i].Value + ",";
                                            }
                                            ht.Add(node.Nodes[i].Value, node.Nodes[i].Value);
                                        }
                                       
                                        /*if (string.IsNullOrEmpty(strText))
                                        {
                                            strText = "'" + node.Nodes[i].Text + "',";
                                        }
                                        else
                                        {
                                            strText = strText + "'" + node.Nodes[i].Text + "',";
                                        }*/
                                        bln = true;
                                    }
                                    if (string.IsNullOrEmpty(strRESWT))
                                    {
                                        strRESWT = node.Nodes[i].Nodes[j].Value + ",";
                                    }
                                    else
                                    {
                                        strRESWT = strRESWT + node.Nodes[i].Nodes[j].Value + ",";
                                    }
                                }
                            }
                        }

                        ht = null;

                        if (!string.IsNullOrEmpty(strREWT))
                        {
                            strREWT = strREWT.Substring(0, strREWT.Length - 1);
                        }
                        if (!string.IsNullOrEmpty(strRESWT))
                        {
                            strRESWT = strRESWT.Substring(0, strRESWT.Length - 1);
                        }

                    }

                }
            }
            if (!string.IsNullOrEmpty(strREWT))
            {
                strMS.Append(ReturnString(lbl_RES_Work_Type.ID));
				strMS.Append("~");
				strMS.Append(strREWT);//[1] WorkTypeId(s)
                strMS.Append("|");
                /*strSS.Append("WorkTypeMS"); strSS.Append("~"); strSS.Append(txt_Cor_Work_Type.Text.Trim());//[1] WorkTypeId(s)
                strSS.Append("|");*/
                strSS.Append("WorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strREWT);//[1] WorkTypeId(s)
                strSS.Append("|");

            }
            if (!string.IsNullOrEmpty(strRESWT))
            {
                strMS.Append("SubWork_Type");
				strMS.Append("~");
				strMS.Append(strRESWT);//[1] WorkTypeId(s)
                strMS.Append("|");
                /* strSS.Append("SubWorkTypeMS"); strSS.Append("~"); strSS.Append(txt_Cor_SubWork_Type.Text.Trim());//[1] WorkTypeId(s)
                strSS.Append("|");*/
                strSS.Append("SubWorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strRESWT);//[1] WorkTypeId(s)
                strSS.Append("|");
            }


            string strMSstr = strMS.ToString();
            string strSSstr = strSS.ToString();
            if (!(string.IsNullOrEmpty(strSSstr)))
            {
                strSS = strSS.Remove(strSS.Length - 1, 1);
                Session.Add("sessRESS", strSS);
                strSS = null;
                strSSstr = null;
            }
            if (!(string.IsNullOrEmpty(strMSstr)))
            {
                strMS = strMS.Remove(strMS.Length - 1, 1);
                Session.Add("sessREMS", strMS);
                strMS = null;
                strMSstr = null;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Label1.Visible = false;
                Label1.Text = string.Empty;
                //hrline.Visible = false;

                txt_RES_Client_Type.Text = string.Empty;
                hid_RES_Client_Type.Value = string.Empty;
                hid_RES_Client_Type_Text.Value = string.Empty;

                // txt_RES_Work_Type.Text = string.Empty;
                // hid_RES_Work_Type.Value = string.Empty;
                //hid_RES_Work_Type_Text.Value = string.Empty;
                foreach (RadTreeNode node in RadTreeView1.GetAllNodes())
                {
                    node.Checked = false;
                    node.Expanded = false;
                }

                if (Session["sessREMS"] != null)
                {
                    Session.Remove("sessREMS");
                }
                if (Session["sessRESS"] != null)
                {
                    Session.Remove("sessRESS");
                }
                if (Session["sessREClear"] != null)
                {
                    Session.Remove("sessREClear");
                }
                Session.Add("sessREClear", "0");
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("RE : btnClear_Click Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private static void CheckTheOneLevelItems(RadTreeView tv, string strItems)
        {
            foreach (RadTreeNode node in tv.Nodes)
            {
                if (node.Text.ToUpper() == "REAL ESTATE") /* One Level */
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
                if (node.Text.ToUpper() == "REAL ESTATE") /* One Level */
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