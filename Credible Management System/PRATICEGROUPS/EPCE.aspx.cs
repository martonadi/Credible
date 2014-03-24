using System;
using System.Data;
using System.Text;
using CredentialsDemo.Common;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class EPCE : System.Web.UI.Page
    {
        CallingSP objSp = new CallingSP();
		private Logger objLog = new Logger();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsPostBack)
				{
					if (Session["sessionUserInfo"] != null)
					{
						hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
						objLog.LogWriter("EPC Energy : Page_Load starts", hidName.Value);
					}
					else
					{
						Response.Redirect("~/TimeOut.aspx");
					}
					
                    objSp.LoadValues("usp_ContractTypeList", "Contract_Type", "ContractTypeId", "@ShowAll~0", "@BusinessGroupId~9", telrad: cbo_ENE_ContractTypeId);
					if (Session["sessEPCESS"] != null)
					{
						if (Session["sessEPCESS"].ToString().Split('|').Length > 0)
						{
							string[] strvals = Session["sessEPCESS"].ToString().Split('|');

							for (int i = 0; i < strvals.Length; i++)
							{
								if (strvals[i].Split('~').Length > 0)
								{
									for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
									{
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            /*case "ProjectSectorMS":
                                                txt_ENE_EPC_Project_Sector.Text = strvals[i].Split('~')[1].ToString();
                                                hid_ENE_EPC_Project_Sector_Text.Value = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "ProjectSectorMSId":
                                                hid_ENE_EPC_Project_Sector.Value = strvals[i].Split('~')[1].ToString();
                                                break;*/
                                            case "TransactionTypeMS":
                                                txt_ENE_Transaction_Type.Text = strvals[i].Split('~')[1].ToString();
                                                hid_ENE_Transaction_Type_Text.Value = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "TransactionTypeMSId":
                                                hid_ENE_Transaction_Type.Value = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "ContractTypeId":
                                                objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_ENE_ContractTypeId);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Session["sessEPCEClear"] == null)
                        {
                            if (Session["SessionSearchPG"] != null)
                            {
                                DataTable dt = (DataTable)(Session["SessionSearchPG"]);
                                CallingSP objSP = new CallingSP();

                                if (dt.Rows.Count > 0)
                                {
                                    string strUserID = dt.Rows[0]["CredentialID"].ToString();
                                    //Project Sector
                                    /*string[] strProjectSector = new string[2];
                                    strProjectSector = objSP.ReturnMultiselectValues("usp_CredentialEPCProjectSectorSource", strUserID);
                                    if (strProjectSector != null)
                                    {
                                        if (!string.IsNullOrEmpty(strProjectSector[0]))
                                        {
                                            hid_ENE_EPC_Project_Sector.Value = strProjectSector[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strProjectSector[1]))
                                        {
                                            txt_ENE_EPC_Project_Sector.Text = strProjectSector[1].ToString();
                                            hid_ENE_EPC_Project_Sector_Text.Value = strProjectSector[1].ToString();
                                        }
                                        strProjectSector = null;
                                    }*/

                                    //Work Type  
                                    string[] strWorkType = new string[2];
                                    strWorkType = objSP.ReturnMultiselectValues("usp_CredentialTransactionTypeSource", strUserID);
                                    if (strWorkType != null)
                                    {
                                        if (!string.IsNullOrEmpty(strWorkType[0]))
                                        {
                                            hid_ENE_Transaction_Type.Value = strWorkType[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strWorkType[1]))
                                        {
                                            txt_ENE_Transaction_Type.Text = strWorkType[1].ToString();
                                            hid_ENE_Transaction_Type_Text.Value = strWorkType[1].ToString();
                                        }
                                        strWorkType = null;
                                    }
                                    if (!string.IsNullOrEmpty(dt.Rows[0]["ContractTypeIDEPCE"].ToString().Trim()))
                                    {
                                        objSP.MatchDropDownValuesText(dt.Rows[0]["ContractTypeIDEPCE"].ToString(), cbo_ENE_ContractTypeId);
                                    }

                                }
                                SaveEPCEData();
                            }
                        }
                    }
                }

                /*if (!string.IsNullOrEmpty(hid_ENE_EPC_Project_Sector_Text.Value.Trim()))
                {
                    txt_ENE_EPC_Project_Sector.Text = hid_ENE_EPC_Project_Sector_Text.Value;
                }
                else
                {
                    txt_ENE_EPC_Project_Sector.Text = string.Empty;
                }*/

                if (!string.IsNullOrEmpty(hid_ENE_Transaction_Type_Text.Value.Trim()))
                {
                    txt_ENE_Transaction_Type.Text = hid_ENE_Transaction_Type_Text.Value;
                }
                else
                {
                    txt_ENE_Transaction_Type.Text = string.Empty;

                }
				img_ENE_Transaction_Type.Attributes.Add("onClick", "LoadChild('" + lbl_ENE_Transaction_Type.Text + "','" + lbl_ENE_Transaction_Type.ID + "','" + hid_ENE_Transaction_Type.ID + "');return false;");
				objLog.LogWriter("EPC Energy : Page_Load Ends", hidName.Value);
			}
			catch (Exception ex)
			{
				objLog.LogWriter("EPC Energy : Page_Load Error" + ex.Message, hidName.Value);
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
				SaveEPCEData();
				Label1.Visible = true;
				Label1.Text = "Details have been successfully captured. Click close button to close this window.";

				Session["sessEPCEClear"] = null;
			}
			catch (Exception ex)
			{
				objLog.LogWriter("EPC Energy : btnOk_Click Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}
		private void SaveEPCEData()
		{
			if (Session["sessEPCESS"] != null)
			{
				Session.Remove("sessEPCESS");
			}
			if (Session["sessEPCEMS"] != null)
			{
				Session.Remove("sessEPCEMS");
			}
			StringBuilder strSS = new StringBuilder();
			StringBuilder strMS = new StringBuilder();

            /*string strhid_ENE_EPC_Project_Sector = string.Empty;
            if (string.IsNullOrEmpty(hid_ENE_EPC_Project_Sector.Value))
            {
                strhid_ENE_EPC_Project_Sector = "NTA";

            }
            else
            {
                strhid_ENE_EPC_Project_Sector = hid_ENE_EPC_Project_Sector.Value;
                strMS.Append(ReturnString(lbl_ENE_EPC_Project_Sector.ID)); strMS.Append("~"); strMS.Append(strhid_ENE_EPC_Project_Sector);//[1] WorkTypeId(s)
                strMS.Append("|");
                strSS.Append("ProjectSectorMS"); strSS.Append("~"); strSS.Append(txt_ENE_EPC_Project_Sector.Text.Trim());//[1] WorkTypeId(s)
                strSS.Append("|");
                strSS.Append("ProjectSectorMSId"); strSS.Append("~"); strSS.Append(strhid_ENE_EPC_Project_Sector);//[1] WorkTypeId(s)
                strSS.Append("|");
            }*/

			string strhid_ENE_Transaction_Type = string.Empty;
			if (!string.IsNullOrEmpty(hid_ENE_Transaction_Type.Value))
			{
                strhid_ENE_Transaction_Type = hid_ENE_Transaction_Type.Value;
                strMS.Append(ReturnString(lbl_ENE_Transaction_Type.ID));
				strMS.Append("~");
				strMS.Append(strhid_ENE_Transaction_Type);//[1] WorkTypeId(s)
                strMS.Append("|");
                strSS.Append("TransactionTypeMS");
				strSS.Append("~");
				strSS.Append(txt_ENE_Transaction_Type.Text.Trim());//[1] WorkTypeId(s)
                strSS.Append("|");
                strSS.Append("TransactionTypeMSId");
				strSS.Append("~");
				strSS.Append(strhid_ENE_Transaction_Type);//[1] WorkTypeId(s)
                strSS.Append("|");
            }


            string strcbo_ENE_ContractTypeId = string.Empty;
            if (cbo_ENE_ContractTypeId.SelectedItem.Text.ToUpper() == "SELECT")
            {
                strSS.Append(ReturnString(lbl_ENE_ContractTypeId.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);//[2] TransactionValueId
                strSS.Append("|");
            }
            else
            {
                strcbo_ENE_ContractTypeId = cbo_ENE_ContractTypeId.SelectedItem.Value;
                strSS.Append(ReturnString(lbl_ENE_ContractTypeId.ID));
				strSS.Append("~");
				strSS.Append(strcbo_ENE_ContractTypeId);//[2] TransactionValueId
                strSS.Append("|");
            }

            string strMSstr = strMS.ToString();
            string strSSstr = strSS.ToString();
			if (!string.IsNullOrEmpty(strSSstr))
			{
                strSS = strSS.Remove(strSS.Length - 1, 1);
                Session.Add("sessEPCESS", strSS);
                strSS = null;
                strSSstr = null;
			}
			if (!string.IsNullOrEmpty(strMSstr))
			{
                strMS = strMS.Remove(strMS.Length - 1, 1);
                Session.Add("sessEPCEMS", strMS);
                strSS = null;
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
                if (Session["sessEPCESS"] != null)
                {
                    Session.Remove("sessEPCESS");
                }
                if (Session["sessEPCEMS"] != null)
                {
                    Session.Remove("sessEPCEMS");
                }
                /* txt_ENE_EPC_Project_Sector.Text = string.Empty;
                 hid_ENE_EPC_Project_Sector.Value = string.Empty;
                 hid_ENE_EPC_Project_Sector_Text.Value = string.Empty;*/

                txt_ENE_Transaction_Type.Text = string.Empty;
                hid_ENE_Transaction_Type.Value = string.Empty;
                hid_ENE_Transaction_Type_Text.Value = string.Empty;

                cbo_ENE_ContractTypeId.SelectedIndex = 0;

                if (Session["sessEPCEClear"] != null)
                {
                    Session.Remove("sessEPCEClear");
                }
                Session.Add("sessEPCEClear", "0");
			}
			catch (Exception ex)
			{
				objLog.LogWriter("EPC Energy : btnClear_Click Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}
	}
}
