using System;
using System.Data;
using System.Text;
using CredentialsDemo.Common;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class EPC : System.Web.UI.Page
    {
        CallingSP objSp = new CallingSP();
		private Logger objLog = new Logger();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
				if (Session["sessionUserInfo"] != null)
				{
					hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
					objLog.LogWriter("EPC Construction : Page_Load starts", hidName.Value);
				}
				else
				{
					Response.Redirect("~/TimeOut.aspx");
				}
				if (hid_EPC_Type_Of_Contract_Other.Value == "1")
				{
					tr_EPC_Type_Of_Contract_Other.Style.Add("display", "block");
				}
				else
				{
					txt_EPC_Type_Of_Contract_Other.Text = string.Empty;
					tr_EPC_Type_Of_Contract_Other.Style.Add("display", "none");
				}
				
				if (hidSubjectMatterOther.Value == "1")
				{
					tr_EPC_Subject_Matter_Other.Visible = true;
				}
				else
				{
					txt_EPC_SubjectMatterOther.Text = string.Empty;
				}
				
				if (hid_EPC_ClientTypeOther.Value == "1")
				{
					tr_EPC_ClientTypeOther.Visible = true;
				}
				else
				{
					txt_EPC_ClientTypeOther.Text = string.Empty;
				}
				
				if (!string.IsNullOrEmpty(hid_EPC_Nature_Of_Work_Text.Value.Trim()))
				{
					txt_EPC_Nature_Of_Work.Text = hid_EPC_Nature_Of_Work_Text.Value;
				}
				else
				{
					txt_EPC_Nature_Of_Work.Text = string.Empty;
				}
				
				if (!string.IsNullOrEmpty(hid_EPC_Type_Of_Contract_Text.Value.Trim()))
				{
					txt_EPC_Type_Of_Contract.Text = hid_EPC_Type_Of_Contract_Text.Value;
				}
				else
				{
					txt_EPC_Type_Of_Contract.Text = string.Empty;
				}
				
				if (!IsPostBack)
				{
                    objSp.LoadValues("usp_SubjectMatterList", "Subject_Matter", "SubjectMatterId", "@ShowAll~0", "@BusinessGroupId~5", telrad: cbo_EPC_SubjectMatterId);
                    objSp.LoadValues("usp_ClientTypeList", "Client_Type", "ClientTypeId", "@ShowAll~0", "@BusinessGroupId~5", telrad: cbo_EPC_ClientTypeIdEPC);
                    objSp.LoadValues("usp_ClientScopeList", "Client_Scope", "ClientScopeId", "@ShowAll~0", "@BusinessGroupId~5", telrad: cbo_EPC_ClientScopeId);
					if (Session["sessEPCSS"] != null)
					{
						if (Session["sessEPCSS"].ToString().Split('|').Length > 0)
						{
							string[] strvals = Session["sessEPCSS"].ToString().Split('|');
							for (int i = 0; i < strvals.Length; i++)
							{
								if (strvals[i].Split('~').Length > 0)
								{
									for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
									{
										switch (strvals[i].Split('~')[0].ToString())
										{
										case "WorkTypeMS":
											txt_EPC_Nature_Of_Work.Text = strvals[i].Split('~')[1].ToString();
											hid_EPC_Nature_Of_Work_Text.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "WorkTypeMSId":
											hid_EPC_Nature_Of_Work.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "ClientScopeId":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_EPC_ClientScopeId);
											break;
										case "TypeOfContractMS":
											txt_EPC_Type_Of_Contract.Text = strvals[i].Split('~')[1].ToString();
											hid_EPC_Type_Of_Contract_Text.Value = strvals[i].Split('~')[1].ToString();
											if (hid_EPC_Type_Of_Contract_Text.Value.ToUpper() == "OTHERS (PLEASE SPECIFY)")
											{
												tr_EPC_Type_Of_Contract_Other.Style.Add("display", "block");
												hid_EPC_Type_Of_Contract_Other.Value = "1";
											}
											break;
										case "TypeOfContractMSId":
											hid_EPC_Type_Of_Contract.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "Type_Of_Contract_Other":
											txt_EPC_Type_Of_Contract_Other.Text = strvals[i].Split('~')[1].ToString();
											break;
										case "SubjectMatterId":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_EPC_SubjectMatterId);
											if (cbo_EPC_SubjectMatterId.SelectedItem.Text.ToUpper() == "OTHER (PLEASE SPECIFY)")
											{
												tr_EPC_Subject_Matter_Other.Visible = true;
												hidSubjectMatterOther.Value = "1";
											}
											break;
										case "Subject_Matter_Other":
											txt_EPC_SubjectMatterOther.Text = strvals[i].Split('~')[1].ToString();
											break;
										case "ClientTypeIdEPC":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_EPC_ClientTypeIdEPC);
											if (cbo_EPC_ClientTypeIdEPC.SelectedItem.Text.ToUpper() == "OTHER (PLEASE SPECIFY)")
											{
												tr_EPC_ClientTypeOther.Visible = true;
												hid_EPC_ClientTypeOther.Value = "1";
											}
											break;
										case "ClientTypeOther":
											txt_EPC_ClientTypeOther.Text = strvals[i].Split('~')[1].ToString();
											break;
										}
									}
								}
							}
						}
					}
					else
					{
						if (Session["sessEPCClear"] == null)
						{
							if (Session["SessionSearchPG"] != null)
							{
								DataTable dt = (DataTable)(Session["SessionSearchPG"]);
								CallingSP objSP = new CallingSP();
								if (dt.Rows.Count > 0)
								{
									string strUserID = dt.Rows[0]["CredentialID"].ToString();
									string[] strWorkType = new string[2];
									strWorkType = objSP.ReturnMultiselectValues("usp_CredentialNatureWorkSource", strUserID);
									if (strWorkType != null)
									{
										if (!string.IsNullOrEmpty(strWorkType[0]))
										{
											hid_EPC_Nature_Of_Work.Value = strWorkType[0].ToString();
										}
										if (!string.IsNullOrEmpty(strWorkType[1]))
										{
											txt_EPC_Nature_Of_Work.Text = strWorkType[1].ToString();
											hid_EPC_Nature_Of_Work_Text.Value = strWorkType[1].ToString();
										}
										strWorkType = null;
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["ClientScopeIDEPC"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["ClientScopeIDEPC"].ToString(), cbo_EPC_SubjectMatterId);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIDEPC"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["ClientTypeIDEPC"].ToString(), cbo_EPC_ClientTypeIdEPC);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeOtherEPC"].ToString().Trim()))
									{
										tr_EPC_ClientTypeOther.Visible = true;
										txt_EPC_ClientTypeOther.Text = dt.Rows[0]["ClientTypeOtherEPC"].ToString();
										hid_EPC_ClientTypeOther.Value = "1";
									}
									string[] strTypeContract = new string[2];
									strTypeContract = objSP.ReturnMultiselectValues("usp_CredentialTypeOfContractSource", strUserID);
									if (strTypeContract != null)
									{
										if (!string.IsNullOrEmpty(strTypeContract[0]))
										{
											hid_EPC_Nature_Of_Work.Value = strTypeContract[0].ToString();
										}
										if (!string.IsNullOrEmpty(strTypeContract[1]))
										{
											txt_EPC_Nature_Of_Work.Text = strTypeContract[1].ToString();
											hid_EPC_Nature_Of_Work_Text.Value = strTypeContract[1].ToString();
										}
										strTypeContract = null;
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["TypeofContractOtherEPC"].ToString().Trim()))
									{
										tr_EPC_Type_Of_Contract_Other.Style.Add("display", "block");
										txt_EPC_Type_Of_Contract_Other.Text = dt.Rows[0]["TypeofContractOtherEPC"].ToString();
										hid_EPC_Type_Of_Contract_Other.Value = "1";
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["SubjectMatterIDEPC"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["SubjectMatterIDEPC"].ToString(), cbo_EPC_SubjectMatterId);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["SubjectMatterOtherEPC"].ToString().Trim()))
									{
										tr_EPC_Subject_Matter_Other.Visible = true;
										txt_EPC_SubjectMatterOther.Text = dt.Rows[0]["SubjectMatterOtherEPC"].ToString();
										hidSubjectMatterOther.Value = "1";
									}
								}
								SaveEPCData();
							}
						}
					}
				}
				
				img_EPC_Nature_Of_Work.Attributes.Add("onClick", "LoadChild('" + lbl_EPC_Nature_Of_Work.Text + "','" + lbl_EPC_Nature_Of_Work.ID + "','" + hid_EPC_Nature_Of_Work.ID + "');return false;");
				img_EPC_Type_Of_Contract.Attributes.Add("onClick", "LoadChild('" + lbl_EPC_Type_Of_Contract.Text + "','" + lbl_EPC_Type_Of_Contract.ID + "','" + hid_EPC_Type_Of_Contract.ID + "');return false;");
				objLog.LogWriter("EPC Construction : Page_Load Ends", hidName.Value);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("EPC Construction : Page_Load Error" + ex.Message, hidName.Value);
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
				SaveEPCData();
				Label1.Visible = true;
				Label1.Text = "Details have been successfully captured. Click close button to close this window.";
				Session["sessEPCClear"] = null;
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("EPC Construction : btnOk_Click Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}
		private void SaveEPCData()
		{
			if (Session["sessEPCSS"] != null)
			{
				Session.Remove("sessEPCSS");
			}
			if (Session["sessEPCMS"] != null)
			{
				Session.Remove("sessEPCMS");
			}
			StringBuilder strSS = new StringBuilder();
			StringBuilder strMS = new StringBuilder();
			string strhid_EPC_Nature_Of_Work = string.Empty;
			if (!string.IsNullOrEmpty(hid_EPC_Nature_Of_Work.Value))
			{
				strhid_EPC_Nature_Of_Work = hid_EPC_Nature_Of_Work.Value;
				strMS.Append(ReturnString(lbl_EPC_Nature_Of_Work.ID));
				strMS.Append("~");
				strMS.Append(strhid_EPC_Nature_Of_Work);
				strMS.Append("|");
				strSS.Append("WorkTypeMS");
				strSS.Append("~");
				strSS.Append(txt_EPC_Nature_Of_Work.Text.Trim());
				strSS.Append("|");
				strSS.Append("WorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strhid_EPC_Nature_Of_Work);
				strSS.Append("|");
			}
			string strcbo_EPC_ClientScopeId = string.Empty;
			if (cbo_EPC_ClientScopeId.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_EPC_ClientScopeId.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_EPC_ClientScopeId = cbo_EPC_ClientScopeId.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_EPC_ClientScopeId.ID));
				strSS.Append("~");
				strSS.Append(strcbo_EPC_ClientScopeId);
				strSS.Append("|");
			}
			string strhid_EPC_Type_Of_Contract = string.Empty;
			if (!string.IsNullOrEmpty(hid_EPC_Type_Of_Contract.Value))
			{
				strhid_EPC_Type_Of_Contract = hid_EPC_Type_Of_Contract.Value;
				strMS.Append(ReturnString(lbl_EPC_Type_Of_Contract.ID));
				strMS.Append("~");
				strMS.Append(strhid_EPC_Type_Of_Contract);
				strMS.Append("|");
				strSS.Append("TypeOfContractMS");
				strSS.Append("~");
				strSS.Append(txt_EPC_Type_Of_Contract.Text.Trim());
				strSS.Append("|");
				strSS.Append("TypeOfContractMSId");
				strSS.Append("~");
				strSS.Append(strhid_EPC_Type_Of_Contract);
				strSS.Append("|");
			}
			string strtxt_EPC_Type_Of_Contract_Other = string.Empty;
			if (string.IsNullOrEmpty(txt_EPC_Type_Of_Contract_Other.Text.Trim()))
			{
				strSS.Append(ReturnString(lbl_EPC_Type_Of_Contract_Other.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strtxt_EPC_Type_Of_Contract_Other = txt_EPC_Type_Of_Contract_Other.Text.Trim();
				strSS.Append(ReturnString(lbl_EPC_Type_Of_Contract_Other.ID));
				strSS.Append("~");
				strSS.Append(strtxt_EPC_Type_Of_Contract_Other);
				strSS.Append("|");
			}
			string strcbo_EPC_SubjectMatterId = string.Empty;
			if (cbo_EPC_SubjectMatterId.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_EPC_SubjectMatterId.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_EPC_SubjectMatterId = cbo_EPC_SubjectMatterId.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_EPC_SubjectMatterId.ID));
				strSS.Append("~");
				strSS.Append(strcbo_EPC_SubjectMatterId);
				strSS.Append("|");
			}
			string strtxt_EPC_SubjectMatterOther = string.Empty;
			if (string.IsNullOrEmpty(txt_EPC_SubjectMatterOther.Text.Trim()))
			{
				strSS.Append(ReturnString(lbl_EPC_Subject_Matter_Other.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strtxt_EPC_SubjectMatterOther = txt_EPC_SubjectMatterOther.Text.Trim();
				strSS.Append(ReturnString(lbl_EPC_Subject_Matter_Other.ID));
				strSS.Append("~");
				strSS.Append(strtxt_EPC_SubjectMatterOther);
				strSS.Append("|");
			}
			string strcbo_EPC_ClientTypeIdEPC = string.Empty;
			if (cbo_EPC_ClientTypeIdEPC.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_EPC_ClientTypeIdEPC.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_EPC_ClientTypeIdEPC = cbo_EPC_ClientTypeIdEPC.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_EPC_ClientTypeIdEPC.ID));
				strSS.Append("~");
				strSS.Append(strcbo_EPC_ClientTypeIdEPC);
				strSS.Append("|");
			}
			string strtxt_EPC_ClientTypeOther = string.Empty;
			if (string.IsNullOrEmpty(txt_EPC_ClientTypeOther.Text.Trim()))
			{
				strSS.Append(ReturnString(lbl_EPC_ClientTypeOther.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strtxt_EPC_ClientTypeOther = txt_EPC_ClientTypeOther.Text.Trim();
				strSS.Append(ReturnString(lbl_EPC_ClientTypeOther.ID));
				strSS.Append("~");
				strSS.Append(strtxt_EPC_ClientTypeOther);
				strSS.Append("|");
			}
			string strMSstr = strMS.ToString();
			string strSSstr = strSS.ToString();
			if (!string.IsNullOrEmpty(strSSstr))
			{
				strSS = strSS.Remove(strSS.Length - 1, 1);
				Session.Add("sessEPCSS", strSS);
				strSS = null;
                strSSstr = null;
			}
			if (!string.IsNullOrEmpty(strMSstr))
			{
				strMS = strMS.Remove(strMS.Length - 1, 1);
				Session.Add("sessEPCMS", strMS);
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
				if (Session["sessEPCSS"] != null)
				{
					Session.Remove("sessEPCSS");
				}
				if (Session["sessEPCMS"] != null)
				{
					Session.Remove("sessEPCMS");
				}
				txt_EPC_Nature_Of_Work.Text = string.Empty;
				hid_EPC_Nature_Of_Work.Value = string.Empty;
				hid_EPC_Nature_Of_Work_Text.Value = string.Empty;
				cbo_EPC_ClientScopeId.SelectedIndex = 0;
				txt_EPC_Type_Of_Contract.Text = string.Empty;
				hid_EPC_Type_Of_Contract.Value = string.Empty;
				hid_EPC_Type_Of_Contract_Text.Value = string.Empty;
				txt_EPC_Type_Of_Contract_Other.Text = string.Empty;
				tr_EPC_Type_Of_Contract_Other.Style.Add("display", "none");
				hid_EPC_Type_Of_Contract_Other.Value = string.Empty;
				hid_EPC_Type_Of_Contract_Ctl.Value = string.Empty;
				tr_EPC_Subject_Matter_Other.Visible = false;
				cbo_EPC_SubjectMatterId.SelectedIndex = 0;
				txt_EPC_SubjectMatterOther.Text = string.Empty;
				hidSubjectMatterOther.Value = string.Empty;
				cbo_EPC_ClientTypeIdEPC.SelectedIndex = 0;
				tr_EPC_ClientTypeOther.Visible = false;
				txt_EPC_ClientTypeOther.Text = string.Empty;
				hid_EPC_ClientTypeOther_Ctl.Value = string.Empty;
				hid_EPC_ClientTypeOther.Value = string.Empty;
				if (Session["sessEPCClear"] != null)
				{
					Session.Remove("sessEPCClear");
				}
				Session.Add("sessEPCClear", "0");
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("EPC Construction : btnClear_Click Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}
		protected void cbo_EPC_SubjectMatterId_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				tr_EPC_Subject_Matter_Other.Visible = false;
				if (cbo_EPC_SubjectMatterId.SelectedItem.Text.ToUpper() == "OTHER (PLEASE SPECIFY)")
				{
					tr_EPC_Subject_Matter_Other.Visible = true;
					hidSubjectMatterOther.Value = "1";
					txt_EPC_SubjectMatterOther.Focus();
				}
				else
				{
					txt_EPC_SubjectMatterOther.Text = string.Empty;
					cbo_EPC_SubjectMatterId.Focus();
					hidSubjectMatterOther.Value = "0";
				}
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("EPC Construction : cbo_EPC_SubjectMatterId_SelectedIndexChanged Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}
		protected void cbo_EPC_ClientTypeIdEPC_SelectedIndexChanged1(object sender, EventArgs e)
		{
			try
			{
				tr_EPC_ClientTypeOther.Visible = false;
				if (cbo_EPC_ClientTypeIdEPC.SelectedItem.Text.Trim().ToUpper() == "OTHER (PLEASE SPECIFY)")
				{
					tr_EPC_ClientTypeOther.Visible = true;
					hid_EPC_ClientTypeOther.Value = "1";
					txt_EPC_ClientTypeOther.Focus();
				}
				else
				{
					txt_EPC_ClientTypeOther.Text = string.Empty;
					hid_EPC_ClientTypeOther.Value = "0";
					cbo_EPC_ClientTypeIdEPC.Focus();
				}
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("EPC Construction : cbo_EPC_ClientTypeIdEPC_SelectedIndexChanged1 Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}
	}
}
