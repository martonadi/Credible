using System;
using CredentialsDemo.Common;
using System.Text;
using System.Data;
using Telerik.Web.UI;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class BAIF : System.Web.UI.Page
    {
        CallingSP objSP = new CallingSP();
		private Logger objLog = new Logger();
	
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsPostBack)
				{
					if (this.Session["sessionUserInfo"] != null)
					{
						this.hidName.Value = this.Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
						this.objLog.LogWriter("BAIF : Page_Load starts", this.hidName.Value);
					}
					else
					{
						base.Response.Redirect("~/TimeOut.aspx");
					}
				
                    objSP.LoadValues("usp_ClientTypeList", "Client_Type", "ClientTypeId", "@ShowAll~0", "@BusinessGroupId~1", telrad: cbo_BAI_ClientTypeIdBAIF);
                    //objSP.LoadValues("usp_TransactionCurrencyList", "Transaction_Currency", "TransactionCurrencyId", "@ShowAll~0", "@BusinessGroupId~1", telrad: cbo_BAI_Transaction_Value);

                    if (Session["sessBAIFSS"] != null)
					{
						if (Session["sessBAIFSS"].ToString().Split('|').Length > 0)
						{
							string[] strvals = Session["sessBAIFSS"].ToString().Split('|');
							for (int i = 0; i < strvals.Length; i++)
							{
								if (strvals[i].Split('~').Length > 0)
								{
									for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
									{
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "ClientTypeIdBAIF":
                                                objSP.MatchDropDownValues(strvals[i].Split('~')[1], cbo_BAI_ClientTypeIdBAIF);
                                                break;
                                            /* case "Transaction_Value":
                                                 //cbo_BAI_ClientTypeIdBAIF.SelectedItem.Text =strvals[i].Split('~')[1];
                                                 objSP.MatchDropDownValues(strvals[i].Split('~')[1], cbo_BAI_Transaction_Value);
                                                 break;*/
                                            case "LeadBanks":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    tr_BAI_LeadBanks.Visible = true;
                                                    txt_BAI_LeadBanks.Text = strvals[i].Split('~')[1];
                                                }
                                                break;
                                            case "WorkTypeMS":
                                                txt_BAI_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                                hid_BAI_Work_Type_Text.Value = txt_BAI_Work_Type.Text;
                                                break;
                                            case "WorkTypeMSID":
                                                hid_BAI_Work_Type.Value = strvals[i].Split('~')[1].ToString();
                                                break;
                                            //case "Lead_Bank":
                                            //    txt_BAI_Lead_Bank.Text = strvals[i].Split('~')[1].ToString();
                                            //    break;

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
						if (Session["sessBAIFClear"] == null)
						{
							if (Session["SessionSearchPG"] != null)
							{
								DataTable dt = (DataTable)(Session["SessionSearchPG"]);
								objSP = new CallingSP();
								if (dt.Rows.Count > 0)
								{
									if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeBAIF"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["ClientTypeBAIF"].ToString(), cbo_BAI_ClientTypeIdBAIF);
									}
									string strUserID = dt.Rows[0]["CredentialID"].ToString();
									string[] strWorkType = new string[2];
									if (strWorkType != null)
									{
										strWorkType = objSP.ReturnMultiselectValues("usp_CredentialWorkTypeBAIFSource", strUserID);
										if (strWorkType != null)
										{
											if (!string.IsNullOrEmpty(strWorkType[0]))
											{
												hid_BAI_Work_Type.Value = strWorkType[0].ToString();
											}
											if (!string.IsNullOrEmpty(strWorkType[1]))
											{
												txt_BAI_Work_Type.Text = strWorkType[1].ToString();
												hid_BAI_Work_Type_Text.Value = strWorkType[1].ToString();
											}
										}
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["LeadBanks"].ToString().Trim()))
									{
										tr_BAI_LeadBanks.Visible = true;
										txt_BAI_LeadBanks.Text = dt.Rows[0]["LeadBanks"].ToString().Trim();
									}
								}
								SaveBAIFData();
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(hid_BAI_Work_Type_Text.Value.Trim()))
				{
					txt_BAI_Work_Type.Text = hid_BAI_Work_Type_Text.Value;
				}
				else
				{
					txt_BAI_Work_Type.Text = string.Empty;
				}
				
				img_BAI_Work_Type.Attributes.Add("onClick", "LoadChild('" + lbl_BAI_Work_Type.Text + "','" + lbl_BAI_Work_Type.ID + "','" + hid_BAI_Work_Type.ID + "');return false;");
				this.objLog.LogWriter("BAIF : Page_Load Ends", this.hidName.Value);
			}
			catch (Exception ex)
			{
				this.objLog.ErrorWriter("BAIF : Page_Load Error" + ex.Message, this.hidName.Value);
				throw ex;
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
				SaveBAIFData();
				Label1.Visible = true;
				Label1.Text = "Details have been successfully captured. Click close button to close this window.";
				Session["sessBAIFClear"] = null;
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("BAIF : btnOk_Click Error" + ex.Message, this.hidName.Value);
				throw ex;
			}
		}
		private void SaveBAIFData()
		{
			if (Session["sessBAIFSS"] != null)
			{
				Session.Remove("sessBAIFSS");
			}
			if (Session["sessBAIFMS"] != null)
			{
				Session.Remove("sessBAIFMS");
			}
			StringBuilder strSS = new StringBuilder();
			StringBuilder strMS = new StringBuilder();
			string strcbo_BAI_ClientTypeIdBAIF = string.Empty;
			if (cbo_BAI_ClientTypeIdBAIF.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_BAI_ClientTypeIdBAIF.ID));
				strSS.Append("~");
				strSS.Append(string.Empty); //array[0] Client TypeId
				strSS.Append("|");
			}
			else
			{
				strcbo_BAI_ClientTypeIdBAIF = cbo_BAI_ClientTypeIdBAIF.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_BAI_ClientTypeIdBAIF.ID));
				strSS.Append("~");
				strSS.Append(strcbo_BAI_ClientTypeIdBAIF); //array[0] Client TypeId
				strSS.Append("|");
			}
			if (cbo_BAI_ClientTypeIdBAIF.SelectedItem.Text.ToUpper() == "LENDER")
			{
				string strtxt_BAI_LeadBanks = string.Empty;
				if (string.IsNullOrEmpty(txt_BAI_LeadBanks.Text.Trim()))
				{
					strSS.Append(ReturnString(lbl_BAI_LeadBanks.ID));
					strSS.Append("~");
					strSS.Append(string.Empty); //array[0] Client TypeId
					strSS.Append("|");
				}
				else
				{
					strtxt_BAI_LeadBanks = txt_BAI_LeadBanks.Text.Trim();
					strSS.Append(ReturnString(lbl_BAI_LeadBanks.ID));
					strSS.Append("~");
					strSS.Append(strtxt_BAI_LeadBanks);
					strSS.Append("|");
				}
			}
			string strhid_BAI_Work_Type = string.Empty;
			if (!string.IsNullOrEmpty(hid_BAI_Work_Type.Value))
			{
				strhid_BAI_Work_Type = hid_BAI_Work_Type.Value;
				strMS.Append(ReturnString(lbl_BAI_Work_Type.ID));
				strMS.Append("~");
				strMS.Append(strhid_BAI_Work_Type);//[1] WorkTypeId(s)
				strMS.Append("|");
				strSS.Append("WorkTypeMS");
				strSS.Append("~");
				strSS.Append(txt_BAI_Work_Type.Text.Trim());//[1] WorkTypeId(s)
				strSS.Append("|");
				strSS.Append("WorkTypeMSID");
				strSS.Append("~");
				strSS.Append(strhid_BAI_Work_Type);//[1] WorkTypeId(s)
				strSS.Append("|");
			}

            /* string strcbo_BAI_Transaction_Value = string.Empty;
             if (cbo_BAI_Transaction_Value.SelectedItem.Text.ToUpper() == "SELECT")
             {
                 strSS.Append(ReturnString(lbl_BAI_Transaction_Value.ID)); strSS.Append("~"); strSS.Append(string.Empty);//[2] TransactionValueId
                 strSS.Append("|");
             }
             else
             {
                 strcbo_BAI_Transaction_Value = cbo_BAI_Transaction_Value.SelectedItem.Value;
                 strSS.Append(ReturnString(lbl_BAI_Transaction_Value.ID)); strSS.Append("~"); strSS.Append(strcbo_BAI_Transaction_Value);//[2] TransactionValueId
                 strSS.Append("|");
             }*/
            string strMSstr = strMS.ToString();
            string strSSstr = strSS.ToString();
			
            if (!string.IsNullOrEmpty(strSSstr))
            {
                strSS = strSS.Remove(strSS.Length - 1, 1);
                Session.Add("sessBAIFSS", strSS);
                strSS = null;
                strSSstr = null;
            }

            if (!string.IsNullOrEmpty(strMSstr))
            {
                strMS = strMS.Remove(strMS.Length - 1, 1);
                Session.Add("sessBAIFMS", strMS);
                strMS = null;
                strMSstr = null;
            }
		}
		protected void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session["sessBAIFSS"] != null)
				{
					Session.Remove("sessBAIFSS");
				}
				if (Session["sessBAIFMS"] != null)
				{
					Session.Remove("sessBAIFMS");
				}
				Label1.Visible = false;
				Label1.Text = string.Empty;
				cbo_BAI_ClientTypeIdBAIF.SelectedIndex = 0;
				txt_BAI_LeadBanks.Text = string.Empty;
				tr_BAI_LeadBanks.Visible = false;
				txt_BAI_Work_Type.Text = string.Empty;
				hid_BAI_Work_Type.Value = string.Empty;
				hid_BAI_Work_Type_Text.Value = string.Empty;
				if (Session["sessBAIFClear"] != null)
				{
					Session.Remove("sessBAIFClear");
				}
				Session.Add("sessBAIFClear", "0");
			}
			catch (Exception ex)
			{
				this.objLog.ErrorWriter("BAIF : btnClear_Click Error" + ex.Message, this.hidName.Value);
				throw ex;
			}
		}
		protected void cbo_BAI_ClientTypeIdBAIF_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
		{
			try
			{
				tr_BAI_LeadBanks.Visible = false;
				txt_BAI_LeadBanks.Text = string.Empty;
				if (cbo_BAI_ClientTypeIdBAIF.SelectedItem.Text.Trim().ToUpper() == "LENDER")
				{
					tr_BAI_LeadBanks.Visible = true;
				}
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("BAIF : cbo_BAI_ClientTypeIdBAIF_SelectedIndexChanged Error" + ex.Message, this.hidName.Value);
				throw ex;
			}
		}
	}
}
