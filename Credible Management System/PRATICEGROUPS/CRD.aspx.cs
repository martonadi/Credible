using System;
using CredentialsDemo.Common;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Telerik.Web.UI;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class CRD : System.Web.UI.Page
    {
        CallingSP objSp = new CallingSP();
		private Logger objLog = new Logger();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
				if (Session["sessionUserInfo"] != null)
				{
					hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
					objLog.LogWriter("CRD : Page_Load starts", hidName.Value);
				}
				else
				{
					Response.Redirect("~/TimeOut.aspx");
				}
                if (!IsPostBack)
                {
                    //SetValues();
                    objSp.LoadValues("usp_ClientTypeList", "Client_Type", "ClientTypeId", "@ShowAll~0", "@BusinessGroupId~4", telrad: cbo_CRD_ClientTypeIdCommercial);
                    LoadCRDWorkTypeView();

                    if (Session["sessCRDSS"] != null)
                    {
                        if (Session["sessCRDSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessCRDSS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "ClientTypeIdCommercial":
                                                objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_CRD_ClientTypeIdCommercial);
                                                break;
                                            case "WorkTypeMS":
                                                /* txt_CRD_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                                 hid_CRD_Work_Type_Text.Value = strvals[i].Split('~')[1].ToString();*/
                                                break;
                                            case "WorkTypeMSId":
                                                /*hid_CRD_Work_Type.Value = strvals[i].Split('~')[1].ToString();*/
                                                CheckCRDWTItems(RadTreeView1, strvals[i].Split('~')[1].ToString());
                                                break;
                                            case "SubWorkTypeMS":
                                                /*txt_CRD_SubWork_Type.Text = strvals[i].Split('~')[1].ToString();
                                                hid_CRD_SubWork_Type_Text.Value = strvals[i].Split('~')[1].ToString();*/
                                                break;
                                            case "SubWorkTypeMSId":
                                                //hid_CRD_SubWork_Type.Value = strvals[i].Split('~')[1].ToString();
                                                CheckCRDSWTItems(RadTreeView1, strvals[i].Split('~')[1].ToString());
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
                        if (Session["sessCRDClear"] == null)
                        {
                            if (Session["SessionSearchPG"] != null)
                            {
                                DataTable dt = (DataTable)(Session["SessionSearchPG"]);
                                CallingSP objSP = new CallingSP();

                                if (dt.Rows.Count > 0)
                                {
                                    string strUserID = dt.Rows[0]["CredentialID"].ToString();

                                    //Work Type 
                                    string[] strWorkType = new string[2];
                                    strWorkType = objSP.ReturnMultiselectValues("usp_CredentialWorkTypeCRDSource", strUserID);
                                    if (strWorkType != null)
                                    {
                                        CRD.CheckCRDWTItems(RadTreeView1, strWorkType[0].ToString());
                                        strWorkType = null;
                                    }
                                    //SubWork Type 
                                    string[] strSubWorkType = new string[2];
                                    strSubWorkType = objSP.ReturnMultiselectValues("usp_CredentialSubWorkTypeCommercialSource", strUserID);
                                    if (strSubWorkType != null)
                                    {
                                        /* if (!string.IsNullOrEmpty(strSubWorkType[0]))
                                         {
                                             hid_CRD_SubWork_Type.Value = strSubWorkType[0].ToString();
                                         }
                                         if (!string.IsNullOrEmpty(strSubWorkType[1]))
                                         {
                                             txt_CRD_SubWork_Type.Text = strSubWorkType[1].ToString();
                                             hid_CRD_SubWork_Type_Text.Value = strSubWorkType[1].ToString();
                                         }*/
                                        CRD.CheckCRDSWTItems(RadTreeView1, strWorkType[0].ToString());
                                        strSubWorkType = null;
                                    }

                                    if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIdCommercial"].ToString().Trim()))
                                    {
                                        objSP.MatchDropDownValuesText(dt.Rows[0]["ClientTypeIdCommercial"].ToString(), cbo_CRD_ClientTypeIdCommercial);
                                    }
                                }
                                SaveCRDData();
                            }
                        }
                    }
                }
				objLog.LogWriter("CRD : Page_Load Ends", hidName.Value);
                //img_CRD_SubWork_Type.Attributes.Add("onClick", "LoadChild('" + lbl_CRD_SubWork_Type.Text + "','" + lbl_CRD_SubWork_Type.ID + "');return false;");
                /* if (!string.IsNullOrEmpty(hid_CRD_Work_Type_Text.Value.Trim()))
                 {
                     txt_CRD_Work_Type.Text = hid_CRD_Work_Type_Text.Value;
                 }
                 else
                 {
                     txt_CRD_Work_Type.Text = string.Empty;
                 }*/
               /* if (!string.IsNullOrEmpty(hid_CRD_SubWork_Type_Text.Value.Trim()))
                {
                    txt_CRD_SubWork_Type.Text = hid_CRD_SubWork_Type_Text.Value;
                }
                else
                {
                    txt_CRD_SubWork_Type.Text = string.Empty;
                }*/
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

		private void LoadCRDWorkTypeView()
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
				if (!sort.Contains(i))
				{
					string str = string.Empty;

					switch (ds.Tables[0].Rows[i][1].ToString().ToUpper())
					{
					case "BAIF":
						str = "BAIF";
						break;
					case "CORPORATE":
						str = "CORPORATE";
						break;
					case "CRD":
						str = "CRD";
						break;
					case "EPC CONSTRUCTION":
						str = "EPC CONSTRUCTION";
						break;
					case "REAL ESTATE":
						str = "REAL ESTATE";
						break;
					case "EPC-ENERGY":
						str = "EPC ENERGY";
						break;
					case "HUMAN CAPITAL (HC)":
						str = "HUMAN CAPTIAL";
						break;
					case "CORPORATE TAX":
						str = "CORPORATE TAX";
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
				if (str == "CRD")
				{
					RadTreeNode root11 = new RadTreeNode(str);
					root11.Text = str;
					root11.Value = str;
					root11.Checkable = false;
					RadTreeView1.Nodes.Add(root11);
					DataTable dt = objSp.GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='4' AND EXCLUDE ='0' order by work_type asc");
					if (dt.Rows.Count > 0)
					{
						Hashtable htCRD = new Hashtable();
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							if (!htCRD.Contains(dt.Rows[i][0].ToString().Split(':')[0]))
							{
								htCRD.Add(dt.Rows[i][0].ToString().Split(':')[0], dt.Rows[i][0].ToString().Split(':')[0]);
							}
						}
						foreach (DictionaryEntry entCRD in htCRD)
						{
							RadTreeNode chd = new RadTreeNode(entCRD.Value.ToString().ToUpper());
							root11.Nodes.Add(chd);
							chd.Checkable = false;
							for (int i = 0; i < dt.Rows.Count; i++)
							{
								if (dt.Rows[i][0].ToString().ToUpper().Contains(entCRD.Value.ToString().Trim().ToUpper() + ":"))
								{
									RadTreeNode chd2 = new RadTreeNode(dt.Rows[i][0].ToString().Trim().Replace(entCRD.Value.ToString().Trim() + ":", ""));
									chd2.Text = dt.Rows[i][0].ToString().Trim().Replace(entCRD.Value.ToString().Trim() + ":", "");
									chd2.Value = dt.Rows[i][1].ToString();
									chd.Nodes.Add(chd2);
									DataTable dtSubWorkType = objSp.GetDataTable("SELECT subwork_type,subworktypeid FROM TBLsubWORKTYPE WHERE worktypeid='" + dt.Rows[i][1].ToString() + "' AND EXCLUDE ='0' order by subwork_type asc");
									if (dtSubWorkType.Rows.Count > 0)
									{
										for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
										{
											RadTreeNode chd3 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim().Replace(dt.Rows[i][0].ToString().Trim() + ":", ""));
											chd3.Text = dtSubWorkType.Rows[j][0].ToString().Trim().Replace(dt.Rows[i][0].ToString().Trim() + ":", "");
											chd3.Value = dtSubWorkType.Rows[j][1].ToString();
											chd2.Nodes.Add(chd3);
										}
									}
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
				SaveCRDData();
				Label1.Visible = true;
				Label1.Text = "Details have been successfully captured. Click close button to close this window.";
				Session["sessCRDClear"] = null;
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("CRD : btnOk_Click Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}

		private void SaveCRDData()
		{
			if (Session["sessCRDSS"] != null)
			{
				Session.Remove("sessCRDSS");
			}
			if (Session["sessCRDMS"] != null)
			{
				Session.Remove("sessCRDMS");
			}
			StringBuilder strSS = new StringBuilder();
			StringBuilder strMS = new StringBuilder();
			string strCRDWT = string.Empty;
			string strCRDSWT = string.Empty;
			Hashtable ht = new Hashtable();
			string strcbo_CRD_ClientTypeIdCommercial = string.Empty;
			if (cbo_CRD_ClientTypeIdCommercial.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_CRD_ClientTypeIdCommercial.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_CRD_ClientTypeIdCommercial = cbo_CRD_ClientTypeIdCommercial.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_CRD_ClientTypeIdCommercial.ID));
				strSS.Append("~");
				strSS.Append(strcbo_CRD_ClientTypeIdCommercial);
				strSS.Append("|");
			}
			foreach (RadTreeNode node in RadTreeView1.Nodes)
			{
				if (node.Text.ToUpper() == "CRD")
				{
					for (int i = 0; i < node.Nodes.Count; i++)
					{
						for (int j = 0; j < node.Nodes[i].Nodes.Count; j++)
						{
							if (node.Nodes[i].Nodes[j].Checked)
							{
								if (!ht.Contains(node.Nodes[i].Nodes[j].Value))
								{
									if (string.IsNullOrEmpty(strCRDWT))
									{
										strCRDWT = node.Nodes[i].Nodes[j].Value + ",";
									}
									else
									{
										strCRDWT = strCRDWT + node.Nodes[i].Nodes[j].Value + ",";
									}
									ht.Add(node.Nodes[i].Nodes[j].Value, node.Nodes[i].Nodes[j].Value);
								}
							}
						}
					}
					for (int i = 0; i < node.Nodes.Count; i++)
					{
						for (int j = 0; j < node.Nodes[i].Nodes.Count; j++)
						{
							bool bln = false;
							for (int k = 0; k < node.Nodes[i].Nodes[j].Nodes.Count; k++)
							{
								if (node.Nodes[i].Nodes[j].Nodes[k].Checked)
								{
									if (!bln)
									{
										if (!ht.Contains(node.Nodes[i].Nodes[j].Value))
										{
											if (string.IsNullOrEmpty(strCRDWT))
											{
												strCRDWT = node.Nodes[i].Nodes[j].Value + ",";
											}
											else
											{
												strCRDWT = strCRDWT + node.Nodes[i].Nodes[j].Value + ",";
											}
											ht.Add(node.Nodes[i].Nodes[j].Value, node.Nodes[i].Nodes[j].Value);
										}
										bln = true;
									}
									if (string.IsNullOrEmpty(strCRDSWT))
									{
										strCRDSWT = node.Nodes[i].Nodes[j].Nodes[k].Value + ",";
									}
									else
									{
										strCRDSWT = strCRDSWT + node.Nodes[i].Nodes[j].Nodes[k].Value + ",";
									}
								}
							}
						}
					}
					if (!string.IsNullOrEmpty(strCRDWT))
					{
						strCRDWT = strCRDWT.Substring(0, strCRDWT.Length - 1);
					}
					if (!string.IsNullOrEmpty(strCRDSWT))
					{
						strCRDSWT = strCRDSWT.Substring(0, strCRDSWT.Length - 1);
					}
				}
			}
			if (!string.IsNullOrEmpty(strCRDWT))
			{
				strMS.Append(ReturnString(lbl_CRD_Work_Type.ID));
				strMS.Append("~");
				strMS.Append(strCRDWT);
				strMS.Append("|");
				strSS.Append("WorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strCRDWT);
				strSS.Append("|");
			}
			if (!string.IsNullOrEmpty(strCRDSWT))
			{
				strMS.Append("SubWork_Type");
				strMS.Append("~");
				strMS.Append(strCRDSWT);
				strMS.Append("|");
				strSS.Append("SubWorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strCRDSWT);
				strSS.Append("|");
			}
			string strMSstr = strMS.ToString();
			string strSSstr = strSS.ToString();
			if (!string.IsNullOrEmpty(strSSstr))
			{
				strSS = strSS.Remove(strSS.Length - 1, 1);
				Session.Add("sessCRDSS", strSS);
				strSS = null;
				strSSstr = null;
			}
			if (!string.IsNullOrEmpty(strMSstr))
			{
				strMS = strMS.Remove(strMS.Length - 1, 1);
				Session.Add("sessCRDMS", strMS);
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

				if (Session["sessCRDMS"] != null)
				{
					Session.Remove("sessCRDMS");
				}
				if (Session["sessCRDSS"] != null)
				{
					Session.Remove("sessCRDSS");
				}

				foreach (RadTreeNode node in RadTreeView1.GetAllNodes())
				{
					node.Checked = false;
					node.Expanded = false;
				}

				cbo_CRD_ClientTypeIdCommercial.SelectedIndex = 0;
				if (Session["sessCRDClear"] != null)
				{
					Session.Remove("sessCRDClear");
				}
				Session.Add("sessCRDClear", "0");
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("CRD : btnClear_Click Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}

		private static void CheckCRDWTItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				if (node.Text.ToUpper() == "CRD")
				{
					for (int i = 0; i < node.Nodes.Count; i++)
					{
						for (int j = 0; j < node.Nodes[i].Nodes.Count; j++)
						{
							for (int K = 0; K < strItems.Split(',').Length; K++)
							{
								if (node.Nodes[i].Nodes.FindNodeByValue(strItems.Split(',')[K]) != null)
								{
									node.Nodes[i].Nodes.FindNodeByValue(strItems.Split(',')[K]).Checked = true;
									node.Nodes[i].Nodes.FindNodeByValue(strItems.Split(',')[K]).Expanded = true;
									node.Nodes[i].Nodes.FindNodeByValue(strItems.Split(',')[K]).ExpandParentNodes();
								}
							}
						}
					}
				}
			}
		}
		private static void CheckCRDSWTItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				if (node.Text.ToUpper() == "CRD")
				{
					for (int i = 0; i < node.Nodes.Count; i++)
					{
						for (int j = 0; j < node.Nodes[i].Nodes.Count; j++)
						{
							for (int k = 0; k < node.Nodes[i].Nodes[j].Nodes.Count; k++)
							{
								for (int x = 0; x < strItems.Split(',').Length; x++)
								{
									if (node.Nodes[i].Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[x]) != null)
									{
										node.Nodes[i].Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[x]).Checked = true;
										node.Nodes[i].Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[x]).Expanded = true;
										node.Nodes[i].Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[x]).ExpandParentNodes();
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
