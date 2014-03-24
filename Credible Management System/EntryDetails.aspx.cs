using AjaxControlToolkit;
using CredentialsDemo.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace CredentialsDemo
{
    public partial class EntryDetails : System.Web.UI.Page
    {
        CallingSP objSP = new CallingSP();
        Logger objLogger = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["sessionUserInfo"] != null)
                {
                    hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
                }
				else
				{
					Response.Redirect("~/TimeOut.aspx");
				}
				
				if (Request.QueryString["a"] != null && Request.QueryString["a"] == "1")
				{
					if (Session["sessionCredentialID"] != null)
					{
						Session["sessionCredentialID"] = null;
					}
					
					if (Session["SessionSearchPG"] != null)
					{
						Session["SessionSearchPG"] = null;
					}
					
					RemovePracticeGroupSession();
				}
				
				objLogger.LogWriter("EntryScreen : Page_Load Starts ", hidName.Value);
				if (!IsPostBack && Session["sessionUserInfo"] != null)
				{
					if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "ADMINISTRATOR")
					{
						liEditBottom.Visible = false;
						liEditTop.Visible = false;
						btnEditBottom.Visible = false;
						btnEditTop.Visible = false;
						liAddBottom.Visible = true;
						liAddTop.Visible = true;
						btnAddTop.Visible = true;
						btnAddBottom.Visible = true;
						liSearchBottom.Visible = true;
						liSearchTop.Visible = true;
						btnSearchBottom.Visible = true;
						btnSearchTop.Visible = true;
						liDeleteBottom.Visible = false;
						liDeleteTop.Visible = false;
						btnDeleteBottom.Visible = false;
						btnDeleteTop.Visible = false;
						btnClearBottom.Visible = true;
						btnClearTop.Visible = true;
						liClearBottom.Visible = true;
						liClearTop.Visible = true;
						chkPartial.Visible = true;
					}
					else if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "EDITOR")
					{
						liEditBottom.Visible = false;
						liEditTop.Visible = false;
						btnEditBottom.Visible = false;
						btnEditTop.Visible = false;
						liAddBottom.Visible = true;
						liAddTop.Visible = true;
						btnAddTop.Visible = true;
						btnAddBottom.Visible = true;
						liSearchBottom.Visible = true;
						liSearchTop.Visible = true;
						btnSearchBottom.Visible = true;
						btnSearchTop.Visible = true;
						liDeleteBottom.Visible = false;
						liDeleteTop.Visible = false;
						btnDeleteBottom.Visible = false;
						btnDeleteTop.Visible = false;
						btnClearBottom.Visible = true;
						btnClearTop.Visible = true;
						liClearBottom.Visible = true;
						liClearTop.Visible = true;
					}
					else if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "READER")
					{
						liEditBottom.Visible = false;
						liEditTop.Visible = false;
						btnEditBottom.Visible = false;
						btnEditTop.Visible = false;
						liAddBottom.Visible = false;
						liAddTop.Visible = false;
						btnAddTop.Visible = false;
						btnAddBottom.Visible = false;
						liSearchBottom.Visible = true;
						liSearchTop.Visible = true;
						btnSearchBottom.Visible = true;
						btnSearchTop.Visible = true;
						liDeleteBottom.Visible = false;
						liDeleteTop.Visible = false;
						btnDeleteBottom.Visible = false;
						btnDeleteTop.Visible = false;
						btnClearBottom.Visible = false;
						btnClearTop.Visible = false;
						liClearBottom.Visible = false;
						liClearTop.Visible = false;
					}
				}

				SetJavascriptClientEvents();

				RemovePracticeGroupSession();

				if (!IsPostBack)
				{

					LoadingDropDowns();

					//objSP.MatchDropDownValuesText("English", cbo_Tab_Language);

					cbo_Tab_Credential_Version.Enabled = false;
					tr_Credential_Copy.Visible = false;

					objSP.MatchDropDownValuesText("Master", cbo_Tab_Credential_Version);
					objLogger.LogWriter("Setting drp language and credential version enabled false ", Session["sessionUserInfo"].ToString().Split('~')[0].Trim().ToUpper());
					//SetValues();

					if (Session["sessionCredentialID"] != null)
					{
						if (Session["sessionUserInfo"] != null)
						{
							btnClearBottom.Visible = false;
							btnClearTop.Visible = false;
							liClearBottom.Visible = false;
							liClearTop.Visible = false;

							if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "ADMINISTRATOR")
							{
								liEditBottom.Visible = true;
								liEditTop.Visible = true;
								btnEditBottom.Visible = true;
								btnEditTop.Visible = true;
								liAddBottom.Visible = false;
								liAddTop.Visible = false;
								btnAddTop.Visible = false;
								btnAddBottom.Visible = false;
								liSearchBottom.Visible = true;
								liSearchTop.Visible = true;
								btnSearchBottom.Visible = true;
								btnSearchTop.Visible = true;
								liDeleteBottom.Visible = true;
								liDeleteTop.Visible = true;
								btnDeleteBottom.Visible = true;
								btnDeleteTop.Visible = true;
							}
							else if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "EDITOR")
							{
								liEditBottom.Visible = true;
								liEditTop.Visible = true;
								btnEditBottom.Visible = true;
								btnEditTop.Visible = true;
								liAddBottom.Visible = false;
								liAddTop.Visible = false;
								btnAddTop.Visible = false;
								btnAddBottom.Visible = false;
								liSearchBottom.Visible = true;
								liSearchTop.Visible = true;
								btnSearchBottom.Visible = true;
								btnSearchTop.Visible = true;
								liDeleteBottom.Visible = false;
								liDeleteTop.Visible = false;
								btnDeleteBottom.Visible = false;
								btnDeleteTop.Visible = false;
							}
							else if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "READER")
							{
								liEditBottom.Visible = false;
								liEditTop.Visible = false;
								btnEditBottom.Visible = false;
								btnEditTop.Visible = false;
								liAddBottom.Visible = false;
								liAddTop.Visible = false;
								btnAddTop.Visible = false;
								btnAddBottom.Visible = false;
								liSearchBottom.Visible = true;
								liSearchTop.Visible = true;
								btnSearchBottom.Visible = true;
								btnSearchTop.Visible = true;
								liDeleteBottom.Visible = false;
								liDeleteTop.Visible = false;
								btnDeleteBottom.Visible = false;
								btnDeleteTop.Visible = false;
							}
						}

						hidCredentialID.Value = Session["sessionCredentialID"].ToString();
						Session["sessionCredentialID"] = null;
						PopulateDataFromSearch(hidCredentialID.Value);
						//txt_Tab_Matter_No.Enabled = false;
					}
				}

				if (!string.IsNullOrEmpty(cld_Tab_Date_Opened.DateInput.Text.Trim()))
				{
					cld_Tab_Date_Opened.DateInput.BackColor = Color.White;
				}
				if (!string.IsNullOrEmpty(txt_Tab_Keyword.Text.Trim()) && txt_Tab_Keyword.Text.Trim() != "Include any other key words associated with the matter")
				{
					txt_Tab_Keyword.BackColor = Color.White;
				}
				if (!string.IsNullOrEmpty(txt_Tab_Client.Text) && txt_Tab_Client.Text != "Insert client name in full")
				{
					txt_Tab_Client.BackColor = Color.White;
				}
				if (!string.IsNullOrEmpty(hid_Tab_ActualDate_Ongoing.Value.Trim()))
				{
					if (hid_Tab_ActualDate_Ongoing.Value.Trim() == "1" && hid_Tab_ActualDate_Ongoing_1.Value.Trim() == "0")
					{
						txt_Tab_Date_Completed.Text = "Ongoing";
						txt_Tab_Date_Completed.BackColor = Color.White;
						txt_Tab_Date_Completed.Style.Add("display", "block");
						cld_Tab_Date_Completed.Style.Add("display", "none");
						cld_Tab_Date_Completed.Clear();
					}
					else if (hid_Tab_ActualDate_Ongoing.Value.Trim() == "0" && hid_Tab_ActualDate_Ongoing_1.Value.Trim() == "1")
					{
						txt_Tab_Date_Completed.Text = "Not known";
						txt_Tab_Date_Completed.BackColor = Color.White;
						txt_Tab_Date_Completed.Style.Add("display", "block");
						cld_Tab_Date_Completed.Style.Add("display", "none");
						cld_Tab_Date_Completed.Clear();
					}

					if (hid_Tab_ActualDate_Ongoing.Value.Trim() == "0" && hid_Tab_ActualDate_Ongoing_1.Value.Trim() == "0")
					{
						txt_Tab_Date_Completed.Text = "Select date from calendar icon or select ongoing";
						txt_Tab_Date_Completed.Style.Add("display", "none");
						cld_Tab_Date_Completed.Style.Add("display", "inline-block");
						
						if (cld_Tab_Date_Completed.SelectedDate.HasValue && cld_Tab_Date_Completed.SelectedDate.HasValue)
						{
							cld_Tab_Date_Completed.DateInput.BackColor = Color.White;
						}
					}
				}

				if (!string.IsNullOrEmpty(hid_Tab_Client_Industry_Type_Text.Value.Trim()))
				{
					txt_Tab_Client_Industry_Type.Text = hid_Tab_Client_Industry_Type_Text.Value;
					txt_Tab_Client_Industry_Type.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Client_Industry_Type.Text = "Select the sub-sector of the client company from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_ClientIndustrySector_Text.Value.Trim()))
				{
					txt_Tab_ClientIndustrySector.Text = hid_Tab_ClientIndustrySector_Text.Value;
					txt_Tab_ClientIndustrySector.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_ClientIndustrySector.Text = "Select the sector of the client company from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Know_How_Text.Value.Trim()))
				{
					txt_Tab_Know_How.Text = hid_Tab_Know_How_Text.Value;
					txt_Tab_Know_How.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Know_How.Text = "For corporate deals only select relevant theme from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Country_Jurisdiction_Text.Value.Trim()))
				{
					txt_Tab_Country_Jurisdiction.Text = hid_Tab_Country_Jurisdiction_Text.Value;
					txt_Tab_Country_Jurisdiction.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Country_Jurisdiction.Text = "Select the country of dispute from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Country_Matter_Close_Text.Value))
				{
					txt_Tab_Country_Matter_Close.Text = hid_Tab_Country_Matter_Close_Text.Value;
					txt_Tab_Country_Matter_Close.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Country_Matter_Close.Text = "Select the country(s) of the matter/transaction from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Country_Matter_Open_Text.Value.Trim()))
				{
					txt_Tab_Country_Matter_Open.Text = hid_Tab_Country_Matter_Open_Text.Value;
					txt_Tab_Country_Matter_Open.BackColor = System.Drawing.Color.White;
				}
				else
				{
					//SetWaterMark(txt_Tab_Country_Matter_Open, "Select the country where matter opened from look up");
					txt_Tab_Country_Matter_Open.Text = "Select the country where matter opened from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Language_Of_Dispute_Text.Value.Trim()))
				{
					txt_Tab_Language_Of_Dispute.Text = hid_Tab_Language_Of_Dispute_Text.Value;
					txt_Tab_Language_Of_Dispute.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Language_Of_Dispute.Text = "Select the language of dispute from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Country_OtherCMSOffice_Text.Value.Trim()))
				{
					txt_Tab_Country_OtherCMSOffice.Text = hid_Tab_Country_OtherCMSOffice_Text.Value;
					txt_Tab_Country_OtherCMSOffice.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Country_OtherCMSOffice.Text = "Multi select from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Country_PredominantCountry_Text.Value.Trim()))
				{
					txt_Tab_Country_PredominantCountry.Text = hid_Tab_Country_PredominantCountry_Text.Value;
					txt_Tab_Country_PredominantCountry.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Country_PredominantCountry.Text = "Eg. where head quartered";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Lead_Partner_Text.Value.Trim()))
				{
					txt_Tab_Lead_Partner.Text = hid_Tab_Lead_Partner_Text.Value;
					txt_Tab_Lead_Partner.BackColor = System.Drawing.Color.White;
					/* if (txt_Tab_Lead_Partner.Text.ToUpper().Contains("CMS PARTNER") == true)
					 {
					     hidLeadCMSPartner.Value = "0";
					     /*lbl_Tab_CMSPartnerName.Visible = true; txt_Tab_CMSPartnerName.Visible = true;
					     lbl_Tab_Source_Of_Credential.Visible = true; txt_Tab_Source_Of_Credential.Visible = true; img_Tab_Source_Of_Credential.Visible = true;*/
					/*tr_Tab_Source_Of_Credential.Style.Add("display", "inline");
					tr_Tab_CMSPartnerName.Style.Add("display", "inline");
				}*/
				}
				else
				{
					txt_Tab_Lead_Partner.Text = "Multi select from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_SourceOfCredential_Other.Value.Trim()))
				{
					if (hid_Tab_SourceOfCredential_Other.Value.Trim() == "1")
					{
						/*txt_Tab_SourceOfCredential_Other.Style.Add("display", "inline");
						lbl_Tab_SourceOfCredential_Other.Style.Add("display", "inline");*/
						tr_Tab_SourceOfCredential_Other.Style.Add("display", "inline");
					}
					else
					{
						tr_Tab_SourceOfCredential_Other.Style.Add("display", "none");
						txt_Tab_SourceOfCredential_Other.Text = string.Empty;
					}
				}
				if (!string.IsNullOrEmpty(hid_Tab_Source_Of_Credential_Text.Value.Trim()))
				{
					txt_Tab_Source_Of_Credential.Text = hid_Tab_Source_Of_Credential_Text.Value;
					txt_Tab_Source_Of_Credential.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Source_Of_Credential.Text = "Select the source of credential from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Lead_Partner_Ctl.Value.Trim()))/*hid_Tab_Lead_Partner_Ctl*/
				{
					if (hid_Tab_Lead_Partner_Ctl.Value.Trim() == "1")
					{
						tr_Tab_Source_Of_Credential.Style.Add("display", "inline");
						tr_Tab_CMSPartnerName.Style.Add("display", "inline");
					}
					else
					{
						tr_Tab_Source_Of_Credential.Style.Add("display", "none");
						tr_Tab_CMSPartnerName.Style.Add("display", "none");
					}
				}
				if (!string.IsNullOrEmpty(hid_Tab_Other_Uses_Text.Value.Trim()))
				{
					txt_Tab_Other_Uses.Text = hid_Tab_Other_Uses_Text.Value;
					txt_Tab_Other_Uses.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Other_Uses.Text = "Eg. case study/directory submission select from look-up and enter docs# in other useful credential information box";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Other_Matter_Executive_Text.Value.Trim()))
				{
					txt_Tab_Other_Matter_Executive.Text = hid_Tab_Other_Matter_Executive_Text.Value;
					txt_Tab_Other_Matter_Executive.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Other_Matter_Executive.Text = "Multi select from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Referred_From_Other_CMS_Office_Text.Value.Trim()))
				{
					txt_Tab_Referred_From_Other_CMS_Office.Text = hid_Tab_Referred_From_Other_CMS_Office_Text.Value;
					txt_Tab_Referred_From_Other_CMS_Office.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Referred_From_Other_CMS_Office.Text = "Multi select from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Team_Text.Value.Trim()))
				{
					txt_Tab_Team.Text = hid_Tab_Team_Text.Value;
					txt_Tab_Team.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Team.Text = "Multi select from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Transaction_Industry_Type_Text.Value.Trim()))
				{
					txt_Tab_Transaction_Industry_Type.Text = hid_Tab_Transaction_Industry_Type_Text.Value;
					txt_Tab_Transaction_Industry_Type.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Transaction_Industry_Type.Text = "Select the sub-sector of the matter from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_TransactionIndustrySector_Text.Value.Trim()))
				{
					txt_Tab_TransactionIndustrySector.Text = hid_Tab_TransactionIndustrySector_Text.Value;
					txt_Tab_TransactionIndustrySector.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_TransactionIndustrySector.Text = "Select the sector the matter relates to (not worktype) from look up";
				}
				if (!string.IsNullOrEmpty(hid_Tab_Country_ArbitrationCountry_Text.Value.Trim()))
				{
					txt_Tab_Country_ArbitrationCountry.Text = hid_Tab_Country_ArbitrationCountry_Text.Value;
					txt_Tab_Country_ArbitrationCountry.BackColor = System.Drawing.Color.White;
				}
				else
				{
					txt_Tab_Country_ArbitrationCountry.Text = string.Empty;
				}

				if (!string.IsNullOrEmpty(hid_Tab_Language_Of_Dispute_Other.Value.Trim()))/*hid_Tab_Lead_Partner_Ctl*/
				{
					if (hid_Tab_Language_Of_Dispute_Other.Value.Trim() == "1")
					{
						tr_Tab_Language_Of_Dispute_Other.Style.Add("display", "inline");
					}
					else
					{
						tr_Tab_Language_Of_Dispute_Other.Style.Add("display", "none");
					}
				}
				if (!string.IsNullOrEmpty(txt_Tab_ProjectName_Core.Text) && txt_Tab_ProjectName_Core.Text != "If applicable e.g. Project Camden")
				{
					txt_Tab_ProjectName_Core.BackColor = System.Drawing.Color.White;
				}
				if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text) && txt_Tab_Significant_Features.Text != "Insert any other useful information about the credential that will be useful for future reference purposes")
				{
					txt_Tab_Significant_Features.BackColor = System.Drawing.Color.White;
				}
				if (!string.IsNullOrEmpty(txt_Tab_Bible_Reference.Text) && txt_Tab_Bible_Reference.Text != "For corporate deals only")
				{
					txt_Tab_Bible_Reference.BackColor = System.Drawing.Color.White;
				}
				/*if (!string.IsNullOrEmpty(txt_Tab_ProjectDescription_Polish.Text))
				{
					txt_Tab_ProjectDescription_Polish.BackColor = System.Drawing.Color.White;
				}*/
				if (!string.IsNullOrEmpty(txt_Tab_Project_Description.Text) && txt_Tab_Project_Description.Text != "Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion.")
				{
					txt_Tab_Project_Description.BackColor = System.Drawing.Color.White;
				}
				if (!string.IsNullOrEmpty(txt_Tab_ClientDescription.Text) && txt_Tab_ClientDescription.Text != "Eg. a leading retail bank, an international IT company etc")
				{
					txt_Tab_ClientDescription.BackColor = System.Drawing.Color.White;
				}
				/*if (!string.IsNullOrEmpty(txt_Tab_ClientDescription_OtherLanguage.Text))
				{
					txt_Tab_ClientDescription_OtherLanguage.BackColor = System.Drawing.Color.White;
				}*/
				objLogger.LogWriter("EntryScreen : Page_Load Ends ", hidName.Value);
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryScreen Error : Page_Load Ends " + ex.Message, hidName.Value);
				throw ex;
			}
		}

		private void SetTextBoxValues()
		{
			objLogger.LogWriter("EntryScreen : SetTextBoxValues Starts ", hidName.Value);

			//Select the country where matter opened from look up
			SetWaterMark(txt_Tab_Client, "Insert client name in full");
			SetWaterMark(txt_Tab_ClientDescription, "Eg. a leading retail bank, an international IT company etc");
			SetWaterMark(txt_Tab_ClientIndustrySector, "Select the sector of the client company from look up");
			SetWaterMark(txt_Tab_Client_Industry_Type, "Select the sub-sector of the client company from look up");
			SetWaterMark(txt_Tab_Country_PredominantCountry, "Eg. where head quartered");
			SetWaterMark(txt_Tab_Matter_No, "Eg. 123456.00001");
			SetWaterMark(txt_Tab_Date_Completed, "Select date from calendar icon or select ongoing");
			SetWaterMark(txt_Tab_TransactionIndustrySector, "Select the sector the matter relates to (not worktype) from look up");
			SetWaterMark(txt_Tab_Transaction_Industry_Type, "Select the sub-sector of the matter from look up");
			SetWaterMark(txt_Tab_Project_Description, "Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion.");
			SetWaterMark(txt_Tab_Significant_Features, "Insert any other useful information about the credential that will be useful for future reference purposes");
			SetWaterMark(txt_Tab_Keyword, "Include any other key words associated with the matter");
			SetWaterMark(txt_Tab_Country_Matter_Close, "Select the country(s) of the matter/transaction from look up");
			SetWaterMark(txt_Tab_Language_Of_Dispute, "Select the language of dispute from look up");
			SetWaterMark(txt_Tab_Country_Jurisdiction, "Select the country of dispute from look up");
			SetWaterMark(txt_Tab_Team, "Multi select from look up");
			SetWaterMark(txt_Tab_Lead_Partner, "Multi select from look up");
			SetWaterMark(txt_Tab_CMSPartnerName, "Open field – format last name first name");
			SetWaterMark(txt_Tab_Other_Matter_Executive, "Multi select from look up");
			SetWaterMark(txt_Tab_Referred_From_Other_CMS_Office, "Multi select from look up");
			SetWaterMark(txt_Tab_Country_OtherCMSOffice, "Multi select from look up");
			SetWaterMark(txt_Tab_Other_Uses, "Eg. case study/directory submission select from look-up and enter docs# in other useful credential information box");
			SetWaterMark(txt_Tab_Know_How, "For corporate deals only select relevant theme from look up");
			SetWaterMark(txt_Tab_Bible_Reference, "For corporate deals only");
			SetWaterMark(txt_Tab_ProjectName_Core, "If applicable e.g. Project Camden");
			SetWaterMark(txt_Tab_Country_Matter_Open, "Select the country where matter opened from look up");
			SetWaterMark(txt_Tab_Source_Of_Credential, "Select the source of credential from look up");

			objLogger.LogWriter("EntryScreen : SetTextBoxValues Ends ", hidName.Value);
		}

		private void SetWaterMark(TextBox txt, string strWaterMessage)
		{
			if (!string.IsNullOrEmpty(txt.Text.Trim()) && txt.Text.Trim() != strWaterMessage)
			{
				txt.Text = txt.Text.Trim();
			}
			else
			{
				txt.Text = string.Empty;
			}
		}

		private void PopulateDataFromSearch(string strCredentialID)
		{
			objLogger.LogWriter("EntryScreen : PopulateDataFromSearch Starts ", hidName.Value);
			
			if (ConfigurationManager.AppSettings["LOGGING"] == "YES")
			{
				PnlAdditionalHead.Visible = true;
				pnlcred.Visible = true;
			}
			
			string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
			SqlConnection con = new SqlConnection(strcon);
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "usp_CredentialInsert";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = con;
			SqlDataAdapter adp = new SqlDataAdapter(cmd);
			SqlParameter[] par = new SqlParameter[1];

			//string strCredentialID = "6398";

			par[0] = new SqlParameter("@TheRecord", Convert.ToInt32(strCredentialID));
			cmd.Parameters.AddRange(par);
			DataTable dt = new DataTable();
			adp.Fill(dt);
			adp.Dispose();
			cmd.Dispose();

            if (dt.Rows.Count > 0)
            {
                if (Session["SessionSearchPG"] != null)
                {
                    Session.Remove("SessionSearchPG");
                }
                Session.Add("SessionSearchPG", dt);

                PopulateHCPraticeGroup(strCredentialID, con);
                PopulateREPraticeGroup(strCredentialID, con);
                PopulateEPCEnergyPraticeGroup(strCredentialID, con);
                PopulateEPCConstructionPraticeGroup(strCredentialID, con);
                PopulateCRDPraticeGroup(strCredentialID, con);
                PopulateCorporatePraticeGroup(strCredentialID, con);
                PopulateBAIFPraticeGroup(strCredentialID, con);
                PopulateCorpTaxPraticeGroup(strCredentialID, con);
                PopulateIPFPraticeGroup(strCredentialID, con);
				if (!string.IsNullOrEmpty(dt.Rows[0]["PartialFlag"].ToString().Trim()) && dt.Rows[0]["PartialFlag"].ToString().Trim() == "1")
				{
					chkPartial.Visible = true;
					chkPartial.Text = "Partial update";
				}
				else
				{
					chkPartial.Visible = false;
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CreatedBy"].ToString().Trim()))
				{
					lbl_tab_Created_By.Text = dt.Rows[0]["CreatedBy"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["UpdatedBy"].ToString().Trim()))
				{
					lbl_tab_Updated_By.Text = dt.Rows[0]["UpdatedBy"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["DateCreated"].ToString().Trim()))
				{
					lbl_Tab_Date_Created.Text = dt.Rows[0]["DateCreated"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["DateUpdated"].ToString().Trim()))
				{
					lbl_Tab_Date_Updated.Text = dt.Rows[0]["DateUpdated"].ToString().Trim();
				}
                if (!string.IsNullOrEmpty(dt.Rows[0]["KeyWord"].ToString().Trim()))
                {
                    txt_Tab_Keyword.Text = dt.Rows[0]["KeyWord"].ToString().Trim();
                    txt_Tab_Keyword.BackColor = System.Drawing.Color.White;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientName"].ToString().Trim()))
                {
                    txt_Tab_Client.Text = dt.Rows[0]["ClientName"].ToString().Trim();
                    txt_Tab_Keyword.BackColor = System.Drawing.Color.White;
                }

                //dt.Rows[0]["ClientNameConfidentialCompletion"].ToString()
                if (dt.Rows[0]["ClientNameConfidential"].ToString().ToUpper() == "YES")
                {
                    rdo_Tab_Client_Name_Confidential.SelectedIndex = 0;
                    //tr_Tab_ClientDescription_Language.Visible = true;
                    tr_Tab_ClientDescription.Visible = true;
                    tr_Tab_NameConfidential_Completion.Visible = true;
                    //objSP.MatchDropDownValuesText(dt.Rows[0]["ClientDescriptionLanguage"].ToString(), cbo_Tab_ClientDescription_Language);
                    txt_Tab_ClientDescription.Text = dt.Rows[0]["ClientDescription"].ToString();
                    if (dt.Rows[0]["ClientNameConfidentialCompletion"].ToString() == "YES")
                    {
                        rdo_Tab_NameConfidential_Completion.SelectedIndex = 0;
                    }
                    else
                    {
                        rdo_Tab_NameConfidential_Completion.SelectedIndex = 1;
                    }
                    /*if (!string.IsNullOrEmpty(dt.Rows[0]["ClientDescriptionOtherLanguage"].ToString()))
                    {
                        tr_Tab_ClientDescription_OtherLanguage.Visible = true;
                        txt_Tab_ClientDescription_OtherLanguage.Text = dt.Rows[0]["ClientDescriptionOtherLanguage"].ToString();
                        txt_Tab_ClientDescription_OtherLanguage.BackColor = System.Drawing.Color.White;
                    }*/
                }
				else
				{
					if (dt.Rows[0]["ClientNameConfidential"].ToString().ToUpper() == "NO")
					{
						rdo_Tab_Client_Name_Confidential.SelectedIndex = 1;
					}
				}
				
                //Sector Group 
                string[] strSectorGroup = new string[2];
                strSectorGroup = objSP.ReturnMultiselectValues("usp_CredentialClientIndustrySectorSource", strCredentialID);
                if (strSectorGroup != null)
                {
                    if (!string.IsNullOrEmpty(strSectorGroup[0]))
                    {
                        hid_Tab_ClientIndustrySector.Value = strSectorGroup[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strSectorGroup[1]))
                    {
                        txt_Tab_ClientIndustrySector.Text = strSectorGroup[1].ToString();
                        txt_Tab_ClientIndustrySector.BackColor = System.Drawing.Color.White;
                        hid_Tab_ClientIndustrySector_Text.Value = strSectorGroup[1].ToString();
                    }
                    strSectorGroup = null;
                }

                //Sub-Sector Group 
                string[] strSubSectorGroup = new string[2];
                strSubSectorGroup = objSP.ReturnMultiselectValues("usp_CredentialClientIndustryTypeSource", strCredentialID);
                if (strSubSectorGroup != null)
                {
                    if (!string.IsNullOrEmpty(strSubSectorGroup[0]))
                    {
                        hid_Tab_Client_Industry_Type.Value = strSubSectorGroup[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strSubSectorGroup[1]))
                    {
                        txt_Tab_Client_Industry_Type.Text = strSubSectorGroup[1].ToString();
                        txt_Tab_Client_Industry_Type.BackColor = System.Drawing.Color.White;
                        hid_Tab_Client_Industry_Type_Text.Value = strSubSectorGroup[1].ToString();
                    }
                    strSubSectorGroup = null;
                }

                //Predominant Country of Client 
                string[] strPredominant = new string[2];
                strPredominant = objSP.ReturnMultiselectValues("usp_CredentialCountryClientSource", strCredentialID);
                if (strPredominant != null)
                {
                    if (!string.IsNullOrEmpty(strPredominant[0]))
                    {
                        hid_Tab_Country_PredominantCountry.Value = strPredominant[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strPredominant[1]))
                    {
                        txt_Tab_Country_PredominantCountry.Text = strPredominant[1].ToString();
                        txt_Tab_Country_PredominantCountry.BackColor = System.Drawing.Color.White;
                        hid_Tab_Country_PredominantCountry_Text.Value = strPredominant[1].ToString();
                    }
                    strPredominant = null;
                }

                //Main Practice Group usp_CredentialBusinessGroupSource
                string[] strPracticeGroup = new string[2];
                strPracticeGroup = objSP.ReturnMultiselectValues("usp_CredentialBusinessGroupSource", strCredentialID);
                if (strPracticeGroup != null)
                {
                    for (int i = 0; i < strPracticeGroup[0].Split(',').Length; i++)
                    {
                        switch (strPracticeGroup[0].Split(',')[i])
                        {
                            case "1":
                                chKBAIF.Checked = true;
                                break;
                            case "3":
                                chKCorp.Checked = true;
                                break;
                            case "4":
                                chKCRD.Checked = true;
                                break;
                            case "5":
                                chkEPC.Checked = true;
                                break;
                            case "7":
                                chkRE.Checked = true;
                                break;
                            case "8":
                                chkIPF.Checked = true;
                                break;
                            case "9":
                                chkEPCE.Checked = true;
                                break;
                            case "10":
                                chkHC.Checked = true;
                                break;
                            case "11":
                                chkCorpTax.Checked = true;
                                break;
                            default:
                                break;
                        }
                    }
                    strPracticeGroup = null;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["MatterNumber"].ToString()))
                {
                    txt_Tab_Matter_No.Text = dt.Rows[0]["MatterNumber"].ToString();
                    txt_Tab_Matter_No.BackColor = System.Drawing.Color.White;
                }

				if (!string.IsNullOrEmpty(dt.Rows[0]["DateMatterOpened"].ToString()))
				{
					IFormatProvider culture = new CultureInfo("en-GB", true);
					cld_Tab_Date_Opened.SelectedDate = new DateTime?(DateTime.Parse(dt.Rows[0]["DateMatterOpened"].ToString(), culture));
					cld_Tab_Date_Opened.DateInput.DisplayText = dt.Rows[0]["DateMatterOpened"].ToString();
					hidddate.Value = dt.Rows[0]["DateMatterOpened"].ToString();
				}

                //Sector Group
                string[] strMatterSectorGroup = new string[2];
                strMatterSectorGroup = objSP.ReturnMultiselectValues("usp_CredentialTransactionIndustrySectorSource", strCredentialID);
                if (strMatterSectorGroup != null)
                {
                    if (!string.IsNullOrEmpty(strMatterSectorGroup[0]))
                    {
                        hid_Tab_TransactionIndustrySector.Value = strMatterSectorGroup[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strMatterSectorGroup[1]))
                    {
                        txt_Tab_TransactionIndustrySector.Text = strMatterSectorGroup[1].ToString();
                        txt_Tab_TransactionIndustrySector.BackColor = System.Drawing.Color.White;
                        hid_Tab_TransactionIndustrySector_Text.Value = strMatterSectorGroup[1].ToString();
                    }
                    strMatterSectorGroup = null;
                }
				
                //Sub-Sector Group 
                string[] strMatterSubSectorGroup = new string[2];
                strMatterSubSectorGroup = objSP.ReturnMultiselectValues("usp_CredentialTransactionIndustryTypeSource", strCredentialID);
                if (strMatterSubSectorGroup != null)
                {
                    if (!string.IsNullOrEmpty(strMatterSubSectorGroup[0]))
                    {
                        hid_Tab_Transaction_Industry_Type.Value = strMatterSubSectorGroup[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strMatterSubSectorGroup[1]))
                    {
                        txt_Tab_Transaction_Industry_Type.Text = strMatterSubSectorGroup[1].ToString();
                        txt_Tab_Transaction_Industry_Type.BackColor = System.Drawing.Color.White;
                        hid_Tab_Transaction_Industry_Type_Text.Value = strMatterSubSectorGroup[1].ToString();
                    }
                    strMatterSubSectorGroup = null;
                }

                // objSP.MatchDropDownValuesText(dt.Rows[0]["MatterLanguage"].ToString(), cbo_Tab_Language);
                if (!string.IsNullOrEmpty(dt.Rows[0]["MatterDescription"].ToString()))
                {
                    txt_Tab_Project_Description.Text = dt.Rows[0]["MatterDescription"].ToString();
                    txt_Tab_Project_Description.BackColor = System.Drawing.Color.White;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["OtherMatterDescription"].ToString()))
                {
                    txt_Tab_Significant_Features.Text = dt.Rows[0]["OtherMatterDescription"].ToString();
                    txt_Tab_Significant_Features.BackColor = System.Drawing.Color.White;
                }

                /* if (!string.IsNullOrEmpty(dt.Rows[0]["MatterLanguageDescription"].ToString().Trim()))
                 {
                     tr_Tab_ProjectDescription_Polish.Visible = true;
                     txt_Tab_ProjectDescription_Polish.Text = dt.Rows[0]["MatterLanguageDescription"].ToString();
                     txt_Tab_ProjectDescription_Polish.BackColor = System.Drawing.Color.White;
                 }*/

				if (dt.Rows[0]["ClientMatterConfidential"].ToString().ToUpper() == "YES")
				{
					rdo_Tab_Client_Matter_Confidential.SelectedIndex = 0;
					tr_Tab_MatterConfidential_Completion.Visible = true;
					if (dt.Rows[0]["MatterConfidentialCompletion"].ToString().ToUpper() == "YES")
					{
						rdo_Tab_MatterConfidential_Completion.SelectedIndex = 0;
					}
					else if (dt.Rows[0]["MatterConfidentialCompletion"].ToString().ToUpper() == "NO")
					{
						rdo_Tab_MatterConfidential_Completion.SelectedIndex = 1;
					}
				}
				else if (dt.Rows[0]["ClientMatterConfidential"].ToString().ToUpper() == "NO")
				{
					rdo_Tab_Client_Matter_Confidential.SelectedIndex = 1;
					tr_Tab_MatterConfidential_Completion.Visible = false;
				}

				if (string.IsNullOrEmpty(dt.Rows[0]["ActualDateOngoing"].ToString().Trim()))
				{
					if (!string.IsNullOrEmpty(dt.Rows[0]["ActualDate"].ToString().Trim()))
					{
						IFormatProvider culture = new CultureInfo("en-GB", true);
						cld_Tab_Date_Completed.SelectedDate = new DateTime?(DateTime.Parse(dt.Rows[0]["ActualDate"].ToString(), culture));
						cld_Tab_Date_Completed.DateInput.DisplayText = dt.Rows[0]["ActualDate"].ToString();
					}
					
					txt_Tab_Date_Completed.Style.Add("display", "none");
					cld_Tab_Date_Completed.Style.Add("display", "inline-block");
					chk_Tab_ActualDate_Ongoing.Checked = false;
					hid_Tab_Date_Completed.Value = dt.Rows[0]["ActualDate"].ToString();
					hid_Tab_ActualDate_Ongoing.Value = "0";
				}
				
				if (!string.IsNullOrEmpty(dt.Rows[0]["ActualDateOngoing"].ToString()))
				{
					txt_Tab_Date_Completed.Text = dt.Rows[0]["ActualDateOngoing"].ToString();
					txt_Tab_Date_Completed.Style.Add("display", "block");
					cld_Tab_Date_Completed.Style.Add("display", "none");
					if (dt.Rows[0]["ActualDateOngoing"].ToString().Trim().ToUpper() == "ONGOING")
					{
						chk_Tab_ActualDate_Ongoing.Checked = true;
						hid_Tab_ActualDate_Ongoing.Value = "1";
					}
					else
					{
						if (dt.Rows[0]["ActualDateOngoing"].ToString().Trim().ToUpper() == "NOT KNOWN")
						{
							chk_Tab_ActualDate_Ongoing_1.Checked = true;
							hid_Tab_ActualDate_Ongoing_1.Value = "1";
						}
					}
				}
				
                if (!string.IsNullOrEmpty(dt.Rows[0]["ActualDateOngoing"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ActualDate"].ToString()))
                {
                    txt_Tab_Date_Completed.BackColor = System.Drawing.Color.White;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["ProjectName"].ToString()))
                {
                    txt_Tab_ProjectName_Core.Text = dt.Rows[0]["ProjectName"].ToString();
                    txt_Tab_ProjectName_Core.BackColor = System.Drawing.Color.White;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["ApplicableLaw"].ToString()))
                {
                    objSP.MatchDropDownValuesText(dt.Rows[0]["ApplicableLaw"].ToString(), cbo_Tab_Country_Law);
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["ApplicableLawOther"].ToString()))
                {
                    tr_Tab_Country_Law_Other.Visible = true;
                    txt_Tab_Country_Law_Other.Text = dt.Rows[0]["ApplicableLawOther"].ToString();
                    txt_Tab_Country_Law_Other.BackColor = System.Drawing.Color.White;
                }

                //Country Where Opened 
                string[] strCountryOpened = new string[2];
                strCountryOpened = objSP.ReturnMultiselectValues("usp_CredentialCountryMatterOpenSource", strCredentialID);
                if (strCountryOpened != null)
                {
                    if (!string.IsNullOrEmpty(strCountryOpened[0]))
                    {
                        hid_Tab_Country_Matter_Open.Value = strCountryOpened[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strCountryOpened[1]))
                    {
                        txt_Tab_Country_Matter_Open.Text = strCountryOpened[1].ToString();
                        txt_Tab_Country_Matter_Open.BackColor = System.Drawing.Color.White;
                        hid_Tab_Country_Matter_Open_Text.Value = strCountryOpened[1].ToString();
                    }
                    strCountryOpened = null;
                }

                //Matter Location(s)
                string[] strCountryClose = new string[2];
                strCountryClose = objSP.ReturnMultiselectValues("usp_CredentialCountryMatterCloseSource", strCredentialID);
                if (strCountryClose != null)
                {
                    if (!string.IsNullOrEmpty(strCountryClose[0]))
                    {
                        hid_Tab_Country_Matter_Close.Value = strCountryClose[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strCountryClose[1]))
                    {
                        txt_Tab_Country_Matter_Close.Text = strCountryClose[1].ToString();
                        txt_Tab_Country_Matter_Close.BackColor = System.Drawing.Color.White;
                        hid_Tab_Country_Matter_Close_Text.Value = strCountryClose[1].ToString();
                    }
                    strCountryClose = null;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["Contentious"].ToString()))
                {
                    objSP.MatchDropDownValuesText(dt.Rows[0]["Contentious"].ToString(), cbo_Tab_Contentious_IRG);

                    if (cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "CONTENTIOUS")
                    {
                        plnContentiousDetails.Visible = true;
                        //Jurisidiction of Dispute
                        string[] strCountryJurisdiction = new string[2];
                        strCountryJurisdiction = objSP.ReturnMultiselectValues("usp_CredentialCountryJurisdictionSource", strCredentialID);
                        if (strCountryJurisdiction != null)
                        {
                            if (!string.IsNullOrEmpty(strCountryJurisdiction[0]))
                            {
                                hid_Tab_Country_Jurisdiction.Value = strCountryJurisdiction[0].ToString();
                            }
                            if (!string.IsNullOrEmpty(strCountryJurisdiction[1]))
                            {
                                txt_Tab_Country_Jurisdiction.Text = strCountryJurisdiction[1].ToString();
                                txt_Tab_Country_Jurisdiction.BackColor = System.Drawing.Color.White;
                                hid_Tab_Country_Jurisdiction_Text.Value = strCountryJurisdiction[1].ToString();
                            }
                            strCountryJurisdiction = null;
                        }

                        //Language Of Dispute 
                        string[] strLanguageDispute = new string[2];
                        strLanguageDispute = objSP.ReturnMultiselectValues("usp_CredentialLanguageOfDisputeSource", strCredentialID);
                        if (strLanguageDispute != null)
                        {
                            if (!string.IsNullOrEmpty(strLanguageDispute[0]))
                            {
                                hid_Tab_Language_Of_Dispute.Value = strLanguageDispute[0].ToString();
                            }
                            if (!string.IsNullOrEmpty(strLanguageDispute[1]))
                            {
                                txt_Tab_Language_Of_Dispute.Text = strLanguageDispute[1].ToString();
                                txt_Tab_Language_Of_Dispute.BackColor = System.Drawing.Color.White;
                                hid_Tab_Language_Of_Dispute_Text.Value = strLanguageDispute[1].ToString();
                            }
                            strLanguageDispute = null;
                        }

                        if (!string.IsNullOrEmpty(dt.Rows[0]["DisputeResolution"].ToString()))
                        {
                            objSP.MatchDropDownValuesText(dt.Rows[0]["DisputeResolution"].ToString(), cbo_Tab_Dispute_Resolution);

                            if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "ARBITRATION")
                            {
                                tr_Tab_ArbitrationCity.Visible = true;
                                objSP.MatchDropDownValuesText(dt.Rows[0]["SeatofArbitration"].ToString(), cbo_Tab_ArbitrationCity);

                                if (!string.IsNullOrEmpty(dt.Rows[0]["SeatofArbitrationOther"].ToString()))
                                {
                                    tr_Tab_ArbitrationCity_Other.Visible = true;
                                    txt_Tab_ArbitrationCity_Other.BackColor = System.Drawing.Color.White;
                                    txt_Tab_ArbitrationCity_Other.Text = dt.Rows[0]["SeatofArbitrationOther"].ToString();
                                }

                                tr_Tab_Arbitral_Rules.Visible = true;
                                objSP.MatchDropDownValuesText(dt.Rows[0]["ArbitralRules"].ToString(), cbo_Tab_Arbitral_Rules);

                                tr_Tab_InvestmentTreaty.Visible = true;
                                if (!string.IsNullOrEmpty(dt.Rows[0]["InvestmentTreaty"].ToString()))
                                {
                                    if (dt.Rows[0]["InvestmentTreaty"].ToString().Trim().ToUpper() == "YES")
                                    {
                                        rdo_Tab_InvestmentTreaty.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        rdo_Tab_InvestmentTreaty.SelectedIndex = 1;
                                    }
                                }
                            }
                            else if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "INVESTIGATION")
                            {
                                tr_Tab_Investigation_Type.Visible = true;
                                objSP.MatchDropDownValuesText(dt.Rows[0]["InvestigationType"].ToString(), cbo_Tab_Investigation_Type);
                            }
                        }
                    }
                    else
                    {
                        objSP.MatchDropDownValuesText(dt.Rows[0]["Contentious"].ToString(), cbo_Tab_Contentious_IRG);
                    }
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOfDeal"].ToString().Trim()))
                {
                    txt_Tab_ValueOfDeal_Core.Text = dt.Rows[0]["ValueOfDeal"].ToString();
                    txt_Tab_ValueOfDeal_Core.BackColor = System.Drawing.Color.White;
                }

				if (!string.IsNullOrEmpty(dt.Rows[0]["CurrencyOfDeal"].ToString().Trim()))
				{
					objSP.MatchDropDownValuesText(dt.Rows[0]["CurrencyOfDeal"].ToString(), cbo_Tab_Currency_Of_Deal);
				}

				if (dt.Rows[0]["ValueConfidential"].ToString().ToUpper() == "YES")
				{
					rdo_Tab_Value_Confidential.SelectedIndex = 0;
					trr_Tab_ValueConfidential_Completion.Visible = true;
					if (dt.Rows[0]["ValueConfidentialCompletion"].ToString().ToUpper() == "YES")
					{
						rdo_Tab_ValueConfidential_Completion.SelectedIndex = 0;
					}
					else if (dt.Rows[0]["ValueConfidentialCompletion"].ToString().ToUpper() == "NO")
					{
						rdo_Tab_ValueConfidential_Completion.SelectedIndex = 1;
					}
				}
				else if (dt.Rows[0]["ValueConfidential"].ToString().ToUpper() == "NO")
				{
					rdo_Tab_Value_Confidential.SelectedIndex = 1;
					trr_Tab_ValueConfidential_Completion.Visible = false;
				}

                //Teams 
                string[] strteams = new string[2];
                strteams = objSP.ReturnMultiselectValues("usp_CredentialTeamSource", strCredentialID);
                if (strteams != null)
                {
                    if (!string.IsNullOrEmpty(strteams[0]))
                    {
                        hid_Tab_Team.Value = strteams[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strteams[1]))
                    {
                        txt_Tab_Team.Text = strteams[1].ToString();
                        txt_Tab_Team.BackColor = System.Drawing.Color.White;
                        hid_Tab_Team_Text.Value = strteams[1].ToString();
                    }
                    strteams = null;
                }

                //Lead Partners
                string[] strLeadPartners = new string[2];
                strLeadPartners = objSP.ReturnMultiselectValues("usp_CredentialLeadPartnerSource", strCredentialID);
                if (strLeadPartners != null)
                {
                    if (!string.IsNullOrEmpty(strLeadPartners[0]))
                    {
                        hid_Tab_Lead_Partner.Value = strLeadPartners[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strLeadPartners[1]))
                    {
                        txt_Tab_Lead_Partner.Text = strLeadPartners[1].ToString();
                        txt_Tab_Lead_Partner.BackColor = System.Drawing.Color.White;
                        hid_Tab_Lead_Partner_Text.Value = strLeadPartners[1].ToString();
                    }
                    strLeadPartners = null;
                }

                if (hid_Tab_Lead_Partner_Text.Value.ToString().ToUpper().Contains("CMS PARTNER"))
                {
                    tr_Tab_CMSPartnerName.Style.Add("display", "inline");
                    tr_Tab_Source_Of_Credential.Style.Add("display", "inline");

                    hid_Tab_Lead_Partner_Ctl.Value = "1";
                    /*CMS Parnter Name*/
                    if (!string.IsNullOrEmpty(dt.Rows[0]["CMSPartnername"].ToString().Trim()))
                    {
                        //dt.Rows[0]["CMSPartnername"].ToString()
                        txt_Tab_CMSPartnerName.Text = dt.Rows[0]["CMSPartnername"].ToString().Trim();
                        txt_Tab_CMSPartnerName.BackColor = System.Drawing.Color.White;
                    }
                    /*Source of credential*/
                    string[] strSourceCred = new string[2];
                    strSourceCred = objSP.ReturnMultiselectValues("usp_CredentialSourceOfCredentialSource", strCredentialID);
                    if (strSourceCred != null)
                    {
                        if (!string.IsNullOrEmpty(strSourceCred[0]))
                        {
                            hid_Tab_Source_Of_Credential.Value = strSourceCred[0].ToString();
                        }
                        if (!string.IsNullOrEmpty(strSourceCred[1]))
                        {

                            txt_Tab_Source_Of_Credential.Text = strSourceCred[1].ToString();
                            txt_Tab_Source_Of_Credential.BackColor = System.Drawing.Color.White;
                            hid_Tab_Source_Of_Credential_Text.Value = strSourceCred[1].ToString();
                        }
                        strSourceCred = null;
                    }
                    /*oTHER*/
                    if (hid_Tab_Source_Of_Credential.Value.ToString().ToUpper().Contains("OTHER"))
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["SourceofCredentialOther"].ToString().Trim()))
                        {
                            tr_Tab_SourceOfCredential_Other.Style.Add("display", "inline");
                            txt_Tab_SourceOfCredential_Other.Text = dt.Rows[0]["SourceofCredentialOther"].ToString().Trim();
                            txt_Tab_SourceOfCredential_Other.BackColor = System.Drawing.Color.White;
                            hid_Tab_SourceOfCredential_Other.Value = "1";
                        }

                    }
                }

                //dt.Rows[0]["SourceofCredentialOther"].ToString()
                //Other Matter Executives
                string[] strExecutive = new string[2];
                strExecutive = objSP.ReturnMultiselectValues("usp_CredentialOtherMatterExecutiveSource", strCredentialID);
                if (strExecutive != null)
                {
                    if (!string.IsNullOrEmpty(strExecutive[0]))
                    {
                        hid_Tab_Other_Matter_Executive.Value = strExecutive[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strExecutive[1]))
                    {
                        txt_Tab_Other_Matter_Executive.Text = strExecutive[1].ToString();
                        txt_Tab_Other_Matter_Executive.BackColor = System.Drawing.Color.White;
                        hid_Tab_Other_Matter_Executive_Text.Value = strExecutive[1].ToString();
                    }
                    strExecutive = null;
                }

                //CMS Firms Involved
                string[] strCMSFirmsInvolved = new string[2];
                strCMSFirmsInvolved = objSP.ReturnMultiselectValues("usp_CredentialReferredFromOtherCMSOfficeSource", strCredentialID);
                if (strCMSFirmsInvolved != null)
                {
                    if (!string.IsNullOrEmpty(strCMSFirmsInvolved[0]))
                    {
                        hid_Tab_Referred_From_Other_CMS_Office.Value = strCMSFirmsInvolved[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strCMSFirmsInvolved[1]))
                    {
                        txt_Tab_Referred_From_Other_CMS_Office.Text = strCMSFirmsInvolved[1].ToString();
                        txt_Tab_Referred_From_Other_CMS_Office.BackColor = System.Drawing.Color.White;
                        hid_Tab_Referred_From_Other_CMS_Office_Text.Value = strCMSFirmsInvolved[1].ToString();
                    }
                    strCMSFirmsInvolved = null;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["LeadCMSFirm"].ToString().Trim()))
                {
                    objSP.MatchDropDownValuesText(dt.Rows[0]["LeadCMSFirm"].ToString(), cbo_Tab_Lead_CMS_Firm);
                }

                //Countries of other CMS firms
                string[] strCountryCMS = new string[2];
                strCountryCMS = objSP.ReturnMultiselectValues("usp_CredentialCountryOtherCMSOfficeSource", strCredentialID);
                if (strCountryCMS != null)
                {
                    if (!string.IsNullOrEmpty(strCountryCMS[0]))
                    {
                        hid_Tab_Country_OtherCMSOffice.Value = strCountryCMS[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strCountryCMS[1]))
                    {
                        txt_Tab_Country_OtherCMSOffice.Text = strCountryCMS[1].ToString();
                        txt_Tab_Country_OtherCMSOffice.BackColor = System.Drawing.Color.White;
                        hid_Tab_Country_OtherCMSOffice_Text.Value = strCountryCMS[1].ToString();
                    }
                    strCountryCMS = null;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialStatus"].ToString().Trim()))
                {
                    objSP.MatchDropDownValuesText(dt.Rows[0]["CredentialStatus"].ToString(), cbo_Tab_Credential_Status);
                }

                /* CredentialVersion */
                cbo_Tab_Credential_Version.Enabled = false;

				if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() != "READER" && dt.Rows[0]["PartialFlag"].ToString().Trim() == "0")
                {
                    tr_Credential_Copy.Visible = true;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialVersion"].ToString().Trim()))
                {
                    objSP.MatchDropDownValuesText(dt.Rows[0]["CredentialVersion"].ToString(), cbo_Tab_Credential_Version);
                    hid_Credential_Version.Value = dt.Rows[0]["CredentialVersion"].ToString();
                    if (dt.Rows[0]["CredentialVersion"].ToString().Trim().ToUpper() == "OTHER")
                    {
                        tr_Credential_Version_Other.Visible = true;
                    }
                    else
                    {
                        tr_Credential_Version_Other.Visible = false;
                    }
                }

                /*Credntial Version other*/
                if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialVersionOther"].ToString().Trim()))
                {
                    txt_Tab_Credential_Version_Other.Text = dt.Rows[0]["CredentialVersionOther"].ToString();
                    txt_Tab_Credential_Version_Other.BackColor = System.Drawing.Color.White;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialType"].ToString().Trim()))
                {
                    objSP.MatchDropDownValuesText(dt.Rows[0]["CredentialType"].ToString(), cbo_Tab_Credential_Type);
                }

                //Other Uses
                string[] strOtherUses = new string[2];
                strOtherUses = objSP.ReturnMultiselectValues("usp_CredentialOtherUsesSource", strCredentialID);
                if (strOtherUses != null)
                {
                    if (!string.IsNullOrEmpty(strOtherUses[0]))
                    {
                        hid_Tab_Other_Uses.Value = strOtherUses[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strOtherUses[1]))
                    {
                        txt_Tab_Other_Uses.Text = strOtherUses[1].ToString();
                        txt_Tab_Other_Uses.BackColor = System.Drawing.Color.White;
                        hid_Tab_Other_Uses_Text.Value = strOtherUses[1].ToString();
                    }
                    strOtherUses = null;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["Priority"].ToString().Trim()))
                {
                    objSP.MatchDropDownValuesText(dt.Rows[0]["Priority"].ToString().Trim(), cbo_Tab_Priority);
                }

                //Know How 
                string[] strknowHow = new string[2];
                strknowHow = objSP.ReturnMultiselectValues("usp_CredentialKnowHowSource", strCredentialID);
                if (strknowHow != null)
                {
                    if (!string.IsNullOrEmpty(strknowHow[0]))
                    {
                        hid_Tab_Know_How.Value = strknowHow[0].ToString();
                    }
                    if (!string.IsNullOrEmpty(strknowHow[1]))
                    {
                        txt_Tab_Know_How.Text = strknowHow[1].ToString();
                        txt_Tab_Know_How.BackColor = System.Drawing.Color.White;
                        hid_Tab_Know_How_Text.Value = strknowHow[1].ToString();
                    }
                    strknowHow = null;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["ProBono"].ToString().Trim()))
                {
                    if (dt.Rows[0]["ProBono"].ToString().ToUpper() == "YES")
                    {
                        rdo_Tab_ProBono.SelectedIndex = 0;
                    }
                    else
                    {
                        rdo_Tab_ProBono.SelectedIndex = 1;
                    }
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["BibleReference"].ToString().Trim()))
                {
                    txt_Tab_Bible_Reference.Text = dt.Rows[0]["BibleReference"].ToString();
                    txt_Tab_Bible_Reference.BackColor = System.Drawing.Color.White;
                }
            }
            objLogger.LogWriter("EntryScreen : PopulateDataFromSearch Ends ", hidName.Value);
        }

        private void PopulateHCPraticeGroup(string strCredentialID, SqlConnection con)
        {
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\HCPopulateQuery.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
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
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
				{
					strSS.Append("WorkTypeIdHC");
					strSS.Append("~");
					strSS.Append(string.Empty);
					strSS.Append("|");
				}
				else
				{
					strSS.Append("WorkTypeIdHC");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][2].ToString());
                    strSS.Append("|");
                }
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
                {
                    strSS.Append("SubWorkTypeMSId");
					strSS.Append("~");
					strSS.Append(string.Empty);
					strSS.Append("|");
				}
				else
				{
					strSS.Append("SubWorkTypeMSId");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][5].ToString());
                    strSS.Append("|");
                }

                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][4].ToString()))
                {
                    strSS.Append("PensionSchemeHC");
					strSS.Append("~");
					strSS.Append(string.Empty);
                    strSS.Append("|");
                }
                else
                {
                    strSS.Append("PensionSchemeHC");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][4].ToString());
					strSS.Append("|");
				}
				string strSSstr = strSS.ToString();
				if (!string.IsNullOrEmpty(strSSstr))
				{
					strSS = strSS.Remove(strSS.Length - 1, 1);
					Session.Add("sessHCSS", strSS);
				}
			}
		}

        private void PopulateREPraticeGroup(string strCredentialID, SqlConnection con)
        {
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\REPopulateQuery.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
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
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
				{
                    strhid_RES_Client_Type = dsNew.Tables[0].Rows[0][2].ToString();
                    strMS.Append("Client_Type");
					strMS.Append("~");
					strMS.Append(strhid_RES_Client_Type);
                    strMS.Append("|");
                    strSS.Append("ClientTypeMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][1].ToString());//[1] WorkTypeId(s)
                    strSS.Append("|");
                    strSS.Append("ClientTypeMSId");
					strSS.Append("~");
					strSS.Append(strhid_RES_Client_Type);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }


                string strhid_RES_Work_Type = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][4].ToString()))
				{
                    strhid_RES_Work_Type = dsNew.Tables[0].Rows[0][4].ToString();
                    strMS.Append("Work_Type");
					strMS.Append("~");
					strMS.Append(strhid_RES_Work_Type);
                    strMS.Append("|");
                    /*strSS.Append("WorkTypeMS"); strSS.Append("~"); strSS.Append(dsNew.Tables[0].Rows[0][3].ToString());//[1] WorkTypeId(s)
                    strSS.Append("|");*/
                    strSS.Append("WorkTypeMSId");
					strSS.Append("~");
					strSS.Append(strhid_RES_Work_Type);//[1] WorkTypeId(s)
                    strSS.Append("|");
				}
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][5].ToString()))
				{
					strMS.Append("SubWork_Type");
					strMS.Append("~");
					strMS.Append(dsNew.Tables[0].Rows[0][5].ToString());
                    strMS.Append("|");
                    /*strSS.Append("SubWorkTypeMS"); strSS.Append("~"); strSS.Append(txt_CRD_SubWork_Type.Text.Trim());//[1] WorkTypeId(s)
                    strSS.Append("|");*/
                    strSS.Append("SubWorkTypeMSId");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][5].ToString());
					strSS.Append("|");
				}
				string strMSstr = strMS.ToString();
				string strSSstr = strSS.ToString();
				if (!string.IsNullOrEmpty(strSSstr))
				{
					strSS = strSS.Remove(strSS.Length - 1, 1);
					Session.Add("sessRESS", strSS);
				}
				if (!string.IsNullOrEmpty(strMSstr))
				{
					strMS = strMS.Remove(strMS.Length - 1, 1);
					Session.Add("sessREMS", strMS);
				}
			}
		}

        private void PopulateEPCEnergyPraticeGroup(string strCredentialID, SqlConnection con)
        {
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\EPCENERGYPopulateQuery.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
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

                string strhid_ENE_Transaction_Type = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
                {
                    strhid_ENE_Transaction_Type = dsNew.Tables[0].Rows[0][2].ToString();
                    strMS.Append("Transaction_Type");
					strMS.Append("~");
					strMS.Append(strhid_ENE_Transaction_Type);//[1] WorkTypeId(s)
                    strMS.Append("|");
                    strSS.Append("TransactionTypeMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][1].ToString());//[1] WorkTypeId(s)
                    strSS.Append("|");
                    strSS.Append("TransactionTypeMSId");
					strSS.Append("~");
					strSS.Append(strhid_ENE_Transaction_Type);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }

                string strcbo_ENE_ContractTypeId = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][4].ToString()))
                {
                    strSS.Append("ContractTypeId");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
                {
                    strcbo_ENE_ContractTypeId = dsNew.Tables[0].Rows[0][4].ToString();
                    strSS.Append("ContractTypeId");
					strSS.Append("~");
					strSS.Append(strcbo_ENE_ContractTypeId);//[2] TransactionValueId
                    strSS.Append("|");
                }
                string strMSstr = strMS.ToString();
                string strSSstr = strSS.ToString();

                if (!(string.IsNullOrEmpty(strSSstr)))
                {
                    strSS = strSS.Remove(strSS.Length - 1, 1);
                    Session.Add("sessEPCESS", strSS);
                    strSS = null;
                    strSSstr = null;
                }
                if (!(string.IsNullOrEmpty(strMSstr)))
                {
                    strMS = strMS.Remove(strMS.Length - 1, 1);
                    Session.Add("sessEPCEMS", strMS);
                    strSS = null;
                    strMSstr = null;
                }
            }
        }

        private void PopulateEPCConstructionPraticeGroup(string strCredentialID, SqlConnection con)
        {
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\EPCPopulateQueryText.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
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
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
                {
                    strhid_EPC_Nature_Of_Work = dsNew.Tables[0].Rows[0][2].ToString();
                    strMS.Append("Nature_Of_Work");
					strMS.Append("~");
					strMS.Append(strhid_EPC_Nature_Of_Work);//[1] WorkTypeId(s)
                    strMS.Append("|");
                    strSS.Append("WorkTypeMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][1].ToString());//[1] WorkTypeId(s)
                    strSS.Append("|");
                    strSS.Append("WorkTypeMSId");
					strSS.Append("~");
					strSS.Append(strhid_EPC_Nature_Of_Work);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }

                string strcbo_EPC_ClientScopeId = string.Empty;
                if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][13].ToString()))
                {
                    strSS.Append("ClientScopeId");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
                {
                    strcbo_EPC_ClientScopeId = dsNew.Tables[0].Rows[0][13].ToString();
                    strSS.Append("ClientScopeId");
					strSS.Append("~");
					strSS.Append(strcbo_EPC_ClientScopeId);//[2] TransactionValueId
                    strSS.Append("|");
                }

                string strhid_EPC_Type_Of_Contract = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][4].ToString()))
                {
                    strhid_EPC_Type_Of_Contract = dsNew.Tables[0].Rows[0][4].ToString();
                    strMS.Append("Type_Of_Contract");
					strMS.Append("~");
					strMS.Append(strhid_EPC_Type_Of_Contract);//[1] WorkTypeId(s)
                    strMS.Append("|");
                    strSS.Append("TypeOfContractMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][3].ToString());//[1] WorkTypeId(s)
                    strSS.Append("|");
                    strSS.Append("TypeOfContractMSId");
					strSS.Append("~");
					strSS.Append(strhid_EPC_Type_Of_Contract);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }

                string strtxt_EPC_Type_Of_Contract_Other = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][7].ToString()))
                {
                    strSS.Append("Type_Of_Contract_Other");
					strSS.Append("~");
					strSS.Append(string.Empty);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }
                else
                {
                    strtxt_EPC_Type_Of_Contract_Other = dsNew.Tables[0].Rows[0][8].ToString();
                    strSS.Append("Type_Of_Contract_Other");
					strSS.Append("~");
					strSS.Append(strtxt_EPC_Type_Of_Contract_Other);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }

                string strcbo_EPC_SubjectMatterId = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][10].ToString()))
                {
                    strSS.Append("SubjectMatterId");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
                {
                    strcbo_EPC_SubjectMatterId = dsNew.Tables[0].Rows[0][10].ToString();
                    strSS.Append("SubjectMatterId");
					strSS.Append("~");
					strSS.Append(strcbo_EPC_SubjectMatterId);//[2] TransactionValueId
                    strSS.Append("|");
                }

                string strtxt_EPC_SubjectMatterOther = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][11].ToString()))
                {
                    strSS.Append("Subject_Matter_Other");
					strSS.Append("~");
					strSS.Append(string.Empty);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }
                else
                {
                    strtxt_EPC_SubjectMatterOther = dsNew.Tables[0].Rows[0][11].ToString();
                    strSS.Append("Subject_Matter_Other");
					strSS.Append("~");
					strSS.Append(strtxt_EPC_SubjectMatterOther);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }

                string strcbo_EPC_ClientTypeIdEPC = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][6].ToString()))
                {
                    strSS.Append("ClientTypeIdEPC");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
                {
                    strcbo_EPC_ClientTypeIdEPC = dsNew.Tables[0].Rows[0][6].ToString();
                    strSS.Append("ClientTypeIdEPC");
					strSS.Append("~");
					strSS.Append(strcbo_EPC_ClientTypeIdEPC);//[2] TransactionValueId
                    strSS.Append("|");
                }

                string strtxt_EPC_ClientTypeOther = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][7].ToString()))
                {
                    strSS.Append("ClientTypeOther");
					strSS.Append("~");
					strSS.Append(string.Empty);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }
                else
                {
                    strtxt_EPC_ClientTypeOther = dsNew.Tables[0].Rows[0][7].ToString();
                    strSS.Append("ClientTypeOther");
					strSS.Append("~");
					strSS.Append(strtxt_EPC_ClientTypeOther);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }

                string strMSstr = strMS.ToString();
                string strSSstr = strSS.ToString();

                if (!(string.IsNullOrEmpty(strSSstr)))
                {
                    strSS = strSS.Remove(strSS.Length - 1, 1);
                    Session.Add("sessEPCSS", strSS);
                    strSS = null;
                    strSSstr = null;
                }
                if (!(string.IsNullOrEmpty(strMSstr)))
                {
                    strMS = strMS.Remove(strMS.Length - 1, 1);
                    Session.Add("sessEPCMS", strMS);
                    strMS = null;
                    strMSstr = null;
                }
            }
        }

		private void PopulateCRDPraticeGroup(string strCredentialID, SqlConnection con)
		{
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\CRDPopulateQuery.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
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

                string strcbo_CRD_ClientTypeIdCommercial = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][6].ToString()))
                {
                    strSS.Append("ClientTypeIdCommercial");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
                {
                    strcbo_CRD_ClientTypeIdCommercial = dsNew.Tables[0].Rows[0][6].ToString();
                    strSS.Append("ClientTypeIdCommercial");
					strSS.Append("~");
					strSS.Append(strcbo_CRD_ClientTypeIdCommercial);//[2] TransactionValueId
                    strSS.Append("|");
                }

                string strhid_CRD_Work_Type = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
                {
                    strhid_CRD_Work_Type = dsNew.Tables[0].Rows[0][2].ToString();
                    strMS.Append("Work_Type");
					strMS.Append("~");
					strMS.Append(strhid_CRD_Work_Type);
                    strMS.Append("|");
                    strSS.Append("WorkTypeMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][1].ToString());//[1] WorkTypeId(s)
                    strSS.Append("|");
                    strSS.Append("WorkTypeMSId");
					strSS.Append("~");
					strSS.Append(strhid_CRD_Work_Type);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }

                string strhid_CRD_SubWork_Type = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][4].ToString()))
                {
                    strhid_CRD_SubWork_Type = dsNew.Tables[0].Rows[0][4].ToString();
                    strMS.Append("SubWork_Type");
					strMS.Append("~");
					strMS.Append(strhid_CRD_SubWork_Type);
                    strMS.Append("|");
                    strSS.Append("SubWorkTypeMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][3].ToString());//[1] WorkTypeId(s)
                    strSS.Append("|");
                    strSS.Append("SubWorkTypeMSId");
					strSS.Append("~");
					strSS.Append(strhid_CRD_SubWork_Type);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }
                string strMSstr = strMS.ToString();
                string strSSstr = strSS.ToString();

                if (!(string.IsNullOrEmpty(strSSstr)))
                {
                    strSS = strSS.Remove(strSS.Length - 1, 1);
                    Session.Add("sessCRDSS", strSS);
                    strSS = null;
                    strSSstr = null;
                }
                if (!(string.IsNullOrEmpty(strMSstr)))
                {
                    strMS = strMS.Remove(strMS.Length - 1, 1);
                    Session.Add("sessCRDMS", strMS);
                    strMS = null;
                    strMSstr = null;
                }
            }
        }

        private void PopulateCorporatePraticeGroup(string strCredentialID, SqlConnection con)
        {
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\CorporatePopulateQuery.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
            {
                if (Session["sessCORPSS"] != null)
                {
                    Session.Remove("sessCORPSS");
                }
                if (Session["sessCORPMS"] != null)
                {
                    Session.Remove("sessCORPMS");
                }
                StringBuilder strSS = new StringBuilder();
                StringBuilder strMS = new StringBuilder();

                string strhid_Cor_Work_Type = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
                {
					strhid_Cor_Work_Type = dsNew.Tables[0].Rows[0]["CorporateWorkTypeId"].ToString();
                    strMS.Append("Work_Type");
					strMS.Append("~");
					strMS.Append(strhid_Cor_Work_Type);
					strMS.Append("|");
					strSS.Append("WorkTypeMSId");
					strSS.Append("~");
					strSS.Append(strhid_Cor_Work_Type);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }

				string strhid_Cor_SubWork_Type = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["CorporateSubWorkTypeID"].ToString()))
				{
					strhid_Cor_SubWork_Type = dsNew.Tables[0].Rows[0]["CorporateSubWorkTypeID"].ToString();
					strMS.Append("SubWork_Type");
					strMS.Append("~");
					strMS.Append(strhid_Cor_SubWork_Type);
					strMS.Append("|");
					strSS.Append("SubWorkTypeMSId");
					strSS.Append("~");
					strSS.Append(strhid_Cor_SubWork_Type);
					strSS.Append("|");
				}
				
				string strhid_Cor_Acting_For = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["CorporateActingForId"].ToString()))
				{
					strhid_Cor_Acting_For = dsNew.Tables[0].Rows[0]["CorporateActingForId"].ToString();
					strMS.Append("Acting_For");
					strMS.Append("~");
					strMS.Append(strhid_Cor_Acting_For);//[1] WorkTypeId(s)
                    strMS.Append("|");
                    strSS.Append("ActingForMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0]["CorporateActingForValue"].ToString());
					strSS.Append("|");
					strSS.Append("ActingForMSId");
					strSS.Append("~");
					strSS.Append(strhid_Cor_Acting_For);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }

				string strcbo_Cor_Value_Over_Pound = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["ValueOverPoundID"].ToString()))
				{
					strSS.Append("Value_Over_Pound");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
				{
					strcbo_Cor_Value_Over_Pound = dsNew.Tables[0].Rows[0]["ValueOverPoundID"].ToString();
					strMS.Append("Value_Over_Pound_MS");
					strMS.Append("~");
					strMS.Append(strcbo_Cor_Value_Over_Pound);
					strMS.Append("|");
					strSS.Append("Value_Over_Pound");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_Value_Over_Pound);
					strSS.Append("|");
				}
				
				string strhid_Cor_Country_Seller = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["CorporateCountrySellerId"].ToString()))
				{
					strhid_Cor_Country_Seller = dsNew.Tables[0].Rows[0]["CorporateCountrySellerId"].ToString();
					strMS.Append("Country_Seller");
					strMS.Append("~");
					strMS.Append(strhid_Cor_Country_Seller);//[1] WorkTypeId(s)
                    strMS.Append("|");
                    strSS.Append("Country_SellerMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0]["CorporateCountrySellerValue"].ToString());
					strSS.Append("|");
					strSS.Append("Country_SellerMSId");
					strSS.Append("~");
					strSS.Append(strhid_Cor_Country_Seller);
					strSS.Append("|");
				}
				
				string strhid_Cor_Country_Buyer = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["CorporateCountryBuyerId"].ToString()))
				{
					strhid_Cor_Country_Buyer = dsNew.Tables[0].Rows[0]["CorporateCountryBuyerId"].ToString();
					strMS.Append("Country_Buyer");
					strMS.Append("~");
					strMS.Append(strhid_Cor_Country_Buyer);//[1] WorkTypeId(s)
                    strMS.Append("|");
                    strSS.Append("Country_BuyerMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0]["CorporateCountryBuyerValue"].ToString());
					strSS.Append("|");
					strSS.Append("Country_BuyerMSId");
					strSS.Append("~");
					strSS.Append(strhid_Cor_Country_Buyer);
					strSS.Append("|");
				}
				
				string strhid_Cor_Country_Target = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["CorporateCountryTargetId"].ToString()))
				{
					strhid_Cor_Country_Target = dsNew.Tables[0].Rows[0]["CorporateCountryTargetId"].ToString();
					strMS.Append("Country_Target");
					strMS.Append("~");
					strMS.Append(strhid_Cor_Country_Target);
					strMS.Append("|");
					strSS.Append("Country_TargetMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0]["CorporateCountryTargetValue"].ToString());
					strSS.Append("|");
					strSS.Append("Country_TargetMSId");
					strSS.Append("~");
					strSS.Append(strhid_Cor_Country_Target);
					strSS.Append("|");
				}

				string strcbo_Cor_Value_Over_US = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["ValueOverUSID"].ToString()))
				{
					strSS.Append("Value_Over_US");
					strSS.Append("~");
					strSS.Append(string.Empty);
					strSS.Append("|");
				}
				else
				{
					strcbo_Cor_Value_Over_US = dsNew.Tables[0].Rows[0]["ValueOverUSID"].ToString();
					strMS.Append("Value_Over_US_MS");
					strMS.Append("~");
					strMS.Append(strcbo_Cor_Value_Over_US);
					strMS.Append("|");
					strSS.Append("Value_Over_US");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_Value_Over_US);
					strSS.Append("|");
				}

				string strcbo_Cor_ValueRangeEuro = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["ValueRangeEuro1"].ToString()))
				{
					strSS.Append("ValueRangeEuro");
					strSS.Append("~");
					strSS.Append(string.Empty);
					strSS.Append("|");
				}
				else
				{
					strcbo_Cor_ValueRangeEuro = dsNew.Tables[0].Rows[0]["ValueRangeEuro1"].ToString();
					strSS.Append("ValueRangeEuro");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_ValueRangeEuro);//[2] TransactionValueId
                    strSS.Append("|");
                }

				string strcbo_Cor_Value_Over_Euro = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["ValueOverEuroID"].ToString()))
				{
					strSS.Append("Value_Over_Euro");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
				{
					strcbo_Cor_Value_Over_Euro = dsNew.Tables[0].Rows[0]["ValueOverEuroID"].ToString();
					strMS.Append("Value_Over_Euro_MS");
					strMS.Append("~");
					strMS.Append(strcbo_Cor_Value_Over_Euro);
					strMS.Append("|");
					strSS.Append("Value_Over_Euro");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_Value_Over_Euro);//[2] TransactionValueId
                    strSS.Append("|");
                }

				string strtxt_Cor_Published_Reference = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["PublishedReference"].ToString()))
				{
					strSS.Append("Published_Reference");
					strSS.Append("~");
					strSS.Append(string.Empty);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }
                else
				{
					strtxt_Cor_Published_Reference = dsNew.Tables[0].Rows[0]["PublishedReference"].ToString();
					strSS.Append("Published_Reference");
					strSS.Append("~");
					strSS.Append(strtxt_Cor_Published_Reference);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }

				string strcbo_Cor_MAStudy = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["MAStudy1"].ToString()))
				{
					strSS.Append("MAStudy");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
				{
					strcbo_Cor_MAStudy = dsNew.Tables[0].Rows[0]["MAStudy1"].ToString();
					strSS.Append("MAStudy");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_MAStudy);//[2] TransactionValueId
                    strSS.Append("|");
                }

				string strcbo_Cor_PEClients = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["PEClients1"].ToString()))
				{
					strSS.Append("PEClients");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
				{
					strcbo_Cor_PEClients = dsNew.Tables[0].Rows[0]["PEClients1"].ToString();
					strSS.Append("PEClients");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_PEClients);//[2] TransactionValueId
                    strSS.Append("|");
                }

				string strcbo_Cor_QuarterDealAnnouncedId = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["QuarterDealAnnounceID1"].ToString()))
				{
					strSS.Append("QuarterDealAnnouncedId");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
                {
					strcbo_Cor_QuarterDealAnnouncedId = dsNew.Tables[0].Rows[0]["QuarterDealAnnounceID1"].ToString();
					strSS.Append("QuarterDealAnnouncedId");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_QuarterDealAnnouncedId);//[2] TransactionValueId
                    strSS.Append("|");
                }

				string strcbo_Cor_QuarterDealCompletedId = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["QuarterDealCompletedID1"].ToString()))
				{
					strSS.Append("QuarterDealCompletedId");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
				{
					strcbo_Cor_QuarterDealCompletedId = dsNew.Tables[0].Rows[0]["QuarterDealCompletedID1"].ToString();
					strSS.Append("QuarterDealCompletedId");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_QuarterDealCompletedId);//[2] TransactionValueId
                    strSS.Append("|");
                }

				string strtxt_Cor_YearDeal_Announced = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["YearDealAnnouncedId"].ToString()))
				{
					strSS.Append("YearDeal_Announced");
					strSS.Append("~");
					strSS.Append(string.Empty);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }
                else
				{
					strtxt_Cor_YearDeal_Announced = dsNew.Tables[0].Rows[0]["YearDealAnnouncedId"].ToString();
					strSS.Append("YearDeal_Announced");
					strSS.Append("~");
					strSS.Append(strtxt_Cor_YearDeal_Announced);//[3] RoleOfLeadBank
                    strSS.Append("|");
                }

				string strcbo_Cor_YearDealCompletedId = string.Empty;
				if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["YearDealCompletedID1"].ToString()))
				{
					strSS.Append("YearDealCompletedId");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
				{
					strcbo_Cor_YearDealCompletedId = dsNew.Tables[0].Rows[0]["YearDealCompletedID1"].ToString();
					strSS.Append("YearDealCompletedId");
					strSS.Append("~");
					strSS.Append(strcbo_Cor_YearDealCompletedId);//[2] TransactionValueId
                    strSS.Append("|");
                }
				
				string strcld_Cor_DealAnnouncedId = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0]["DealAnnouncedID"].ToString()))
				{
					strcld_Cor_DealAnnouncedId = dsNew.Tables[0].Rows[0]["DealAnnouncedID"].ToString();
					strSS.Append("DealAnnouncedId");
					strSS.Append("~");
					strSS.Append(strcld_Cor_DealAnnouncedId);
					strSS.Append("|");
				}
				
				string strMSstr = strMS.ToString();
				string strSSstr = strSS.ToString();

                if (!(string.IsNullOrEmpty(strSSstr)))
                {
                    strSS = strSS.Remove(strSS.Length - 1, 1);
                    Session.Add("sessCORPSS", strSS);
                    strSS = null;
                    strSSstr = null;
                }
                if (!(string.IsNullOrEmpty(strMSstr)))
                {
                    strMS = strMS.Remove(strMS.Length - 1, 1);
                    Session.Add("sessCORPMS", strMS);
                    strMS = null;
                    strMSstr = null;
                }
            }
        }

        private void PopulateBAIFPraticeGroup(string strCredentialID, SqlConnection con)
        {
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\BAIFPopulateQuery.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
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
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][4].ToString()))
                {
                    strSS.Append("ClientTypeIdBAIF");
					strSS.Append("~");
					strSS.Append(string.Empty); //array[0] Client TypeId
                    strSS.Append("|");
                }
                else
                {
                    strcbo_BAI_ClientTypeIdBAIF = dsNew.Tables[0].Rows[0][4].ToString();
                    strSS.Append("ClientTypeIdBAIF");
					strSS.Append("~");
					strSS.Append(strcbo_BAI_ClientTypeIdBAIF); //array[0] Client TypeId
                    strSS.Append("|");
                }

                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][5].ToString()))
                {
                    string strtxt_BAI_LeadBanks = string.Empty;
                    if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][5].ToString()))
                    {
                        strSS.Append("LeadBanks");
						strSS.Append("~");
						strSS.Append(string.Empty); //array[0] Client TypeId
                        strSS.Append("|");
                    }
                    else
                    {
                        strtxt_BAI_LeadBanks = dsNew.Tables[0].Rows[0][5].ToString();
                        strSS.Append("LeadBanks");
						strSS.Append("~");
						strSS.Append(strtxt_BAI_LeadBanks); //array[0] Client TypeId
                        strSS.Append("|");
                    }
                }
                string strhid_BAI_Work_Type = string.Empty;
				if (!string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
				{
                    strhid_BAI_Work_Type = dsNew.Tables[0].Rows[0][2].ToString();
                    strMS.Append("Work_Type");
					strMS.Append("~");
					strMS.Append(strhid_BAI_Work_Type);//[1] WorkTypeId(s)
                    strMS.Append("|");
                    strSS.Append("WorkTypeMS");
					strSS.Append("~");
					strSS.Append(dsNew.Tables[0].Rows[0][1].ToString());//[1] WorkTypeId(s)
                    strSS.Append("|");
                    strSS.Append("WorkTypeMSID");
					strSS.Append("~");
					strSS.Append(strhid_BAI_Work_Type);//[1] WorkTypeId(s)
                    strSS.Append("|");
                }

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
        }

        private void PopulateIPFPraticeGroup(string strCredentialID, SqlConnection con)
        {
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\IPFPopulateQuery.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
            {
                if (Session["sessIPFMS"] != null)
                {
                    Session.Remove("sessIPFMS");
                }
                if (Session["sessIPFSS"] != null)
                {
                    Session.Remove("sessIPFSS");
                }
                StringBuilder strSS = new StringBuilder();
                StringBuilder strMS = new StringBuilder();
                string strcbo_IPF_ClientTypeIdIPF = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
                {
                    strSS.Append("ClientTypeIdIPF");
					strSS.Append("~");
					strSS.Append(string.Empty);//[2] TransactionValueId
                    strSS.Append("|");
                }
                else
                {
                    strcbo_IPF_ClientTypeIdIPF = dsNew.Tables[0].Rows[0][2].ToString();
                    strSS.Append("ClientTypeIdIPF");
					strSS.Append("~");
					strSS.Append(strcbo_IPF_ClientTypeIdIPF);//[2] TransactionValueId
                    strSS.Append("|");
                }

                string strMSstr = strMS.ToString();
                string strSSstr = strSS.ToString();

                if (!(string.IsNullOrEmpty(strSSstr)))
                {
                    strSS = strSS.Remove(strSS.Length - 1, 1);
                    Session.Add("sessIPFSS", strSS);
                    strSS = null;
                    strSSstr = null;
                }
                if (!(string.IsNullOrEmpty(strMSstr)))
                {
                    strMS = strMS.Remove(strMS.Length - 1, 1);
                    Session.Add("sessIPFMS", strMS);
                    strMS = null;
                    strMSstr = null;
                }
            }
        }

        private void PopulateCorpTaxPraticeGroup(string strCredentialID, SqlConnection con)
        {
            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath(@"~\\Queries\\CorpTaxPopulateQuery.txt"));
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCREDENTIALID", "'" + strCredentialID + "'");
            sr.Dispose();

			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataSet dsNew = new DataSet();
			adp.Fill(dsNew);
			adp.Dispose();

            if (dsNew.Tables[0].Rows.Count > 0)
            {
                if (Session["sessCorpTaxSS"] != null)
                {
                    Session.Remove("sessCorpTaxSS");
                }
                if (Session["sessCorpTaxMS"] != null)
                {
                    Session.Remove("sessCorpTaxMS");
                }
                StringBuilder strSS = new StringBuilder();
                StringBuilder strMS = new StringBuilder();
                string strcbo_Crt_WorkType_CorpTax = string.Empty;
                if (string.IsNullOrEmpty(dsNew.Tables[0].Rows[0][2].ToString()))
                {
                    strSS.Append("WorkType_CorpTax");
					strSS.Append("~");
					strSS.Append(string.Empty); //array[0] Client TypeId
                    strSS.Append("|");
                }
                else
                {
                    strcbo_Crt_WorkType_CorpTax = dsNew.Tables[0].Rows[0][2].ToString();
                    strSS.Append("WorkType_CorpTax");
					strSS.Append("~");
					strSS.Append(strcbo_Crt_WorkType_CorpTax); //array[0] Client TypeId
                    strSS.Append("|");
                }

                string strMSstr = strMS.ToString();
                string strSSstr = strSS.ToString();
                if (!string.IsNullOrEmpty(strSSstr))
                {
                    strSS = strSS.Remove(strSS.Length - 1, 1);
                    Session.Add("sessCorpTaxSS", strSS);
                    strSS = null;
                    strSSstr = null;
                }
                if (!string.IsNullOrEmpty(strMSstr))
                {
                    strMS = strMS.Remove(strMS.Length - 1, 1);
                    Session.Add("sessCorpTaxMS", strMS);
                    strMS = null;
                    strMSstr = null;
                }
            }
        }

        private void RemovePracticeGroupSession()
        {
            objLogger.LogWriter("EntryScreen : RemovePracticeGroupSession Starts ", hidName.Value);

            if (chKBAIF.Checked == false)
            {
                Session["sessBAIFSS"] = null;
                Session["sessBAIFMS"] = null;

            }
            if (chKCRD.Checked == false)
            {
                Session["sessCRDSS"] = null;
                Session["sessCRDMS"] = null;
            }
            if (chKCorp.Checked == false)
            {
                Session["sessCORPSS"] = null;
                Session["sessCORPMS"] = null;
            }
            if (chkEPC.Checked == false)
            {
                Session["sessEPCSS"] = null;
                Session["sessEPCMS"] = null;
            }
            if (chkEPCE.Checked == false)
            {
                Session["sessEPCESS"] = null;
                Session["sessEPCEMS"] = null;
            }
            if (chkIPF.Checked == false)
            {
                Session["sessIPFSS"] = null;
                Session["sessIPFMS"] = null;
            }
            if (chkRE.Checked == false)
            {
                Session["sessRESS"] = null;
                Session["sessREMS"] = null;
            }
            if (chkHC.Checked == false)
            {
                Session["sessHCSS"] = null;
                Session["sessHCMS"] = null;
            }
            if (chkCorpTax.Checked == false)
            {
                Session["sessCorpTaxSS"] = null;
                Session["sessCorpTaxMS"] = null;
            }

            objLogger.LogWriter("EntryScreen : RemovePracticeGroupSession Ends ", hidName.Value);
        }

        private void LoadingDropDowns()
        {
            objLogger.LogWriter("EntryScreen : LoadingDropDowns Starts ", hidName.Value);

            objSP.LoadValues("usp_CredentialTypeList", "Credential_Type", "CredentialTypeId", telrad: cbo_Tab_Credential_Type);
            objSP.LoadValues("usp_CredentialStatusList", "Credential_Status", "CredentialStatusId", telrad: cbo_Tab_Credential_Status);
            objSP.LoadValues("usp_CredentialVersionList", "Credential_Version", "CredentialVersionId", telrad: cbo_Tab_Credential_Version, strCheck: "0");
            objSP.LoadValues("usp_CurrencyOfDealList", "Currency_Of_Deal", "CurrencyOfDealId", telrad: cbo_Tab_Currency_Of_Deal);
            objSP.LoadValues("usp_GetContentious", "TheDescription", "TheValue", telrad: cbo_Tab_Contentious_IRG);
            objSP.LoadValues("usp_DisputeResolutionList", "Dispute_Resolution", "DisputeResolutionId", telrad: cbo_Tab_Dispute_Resolution);
            // objSP.LoadValues("usp_TypeOfDisputeList", "Type_Of_Dispute", "TypeOfDisputeId", cbo: cbo_Tab_Type_Of_Dispute);
            objSP.LoadValues("usp_ArbitralRulesList", "Arbitral_Rules", "ArbitralRulesId", telrad: cbo_Tab_Arbitral_Rules);
            objSP.LoadValues("usp_ReferredFromOtherCMSOfficeList", "Referred_From_Other_CMS_Office", "ReferredFromOtherCMSOfficeId", telrad: cbo_Tab_Lead_CMS_Firm);
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", rdo: rdo_Tab_Client_Matter_Confidential);
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", rdo: rdo_Tab_Client_Name_Confidential);
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", rdo: rdo_Tab_MatterConfidential_Completion);
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", rdo: rdo_Tab_NameConfidential_Completion);
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", rdo: rdo_Tab_Value_Confidential);
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", rdo: rdo_Tab_ValueConfidential_Completion);
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", rdo: rdo_Tab_ProBono);
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", rdo: rdo_Tab_InvestmentTreaty);
            objSP.LoadValues("usp_CountryLawList", "Country_Law", "CountryLawId", telrad: cbo_Tab_Country_Law);
            objSP.LoadValues("usp_SeatOfArbitrationList", "Seat_Of_Arbitration", "SeatOfArbitrationId", telrad: cbo_Tab_ArbitrationCity);
            objSP.LoadValues("usp_InvestigationTypeList", "Investigation_Type", "InvestigationTypeId", telrad: cbo_Tab_Investigation_Type);
            //objSP.LoadValues("usp_LanguageList", "Language", "LanguageId", telrad: cbo_Tab_Language);
            //objSP.LoadValues("usp_LanguageList", "Language", "LanguageId", telrad: cbo_Tab_ClientDescription_Language);
            //objSP.LoadValues("usp_LanguageOfDisputeList", "Language_Of_Dispute", "LanguageOfDisputeId", telrad: rad_Tab_ClientDescription_Language);
            objSP.LoadValues("usp_PriorityList", "Priority", "PriorityId", telrad: cbo_Tab_Priority);
            //objSP.LoadValues("usp_CountryList", "Country", "CountryId", cbo: cbo_Tab_Country_Matter_Open);
            //objSP.LoadValues("usp_CountryList", "Country", "CountryId", cbo: cbo_Tab_Country_ArbitrationCountry);
            objLogger.LogWriter("EntryScreen : LoadingDropDowns Ends ", hidName.Value);
        }

        private void SetJavascriptClientEvents()
        {
            objLogger.LogWriter("EntryScreen : SetJavascriptClientEvents Starts ", hidName.Value);

			btnAddBottom.Attributes.Add("onclick", "return validationFullSubmitFields('Do you want to save the record ?');");
			btnAddTop.Attributes.Add("onclick", "return validationFullSubmitFields('Do you want to save the record ?');");

            btnPartialSave.Attributes.Add("onclick", "return validationPartialFields('Do you want to do partial save ?');");
            btnPartialSaveBottom.Attributes.Add("onclick", "return validationPartialFields('Do you want to do partial save ?');");

			btnEditBottom.Attributes.Add("onclick", "return validationFullSubmitFields('Do you want to update the record ?');");
			btnEditTop.Attributes.Add("onclick", "return validationFullSubmitFields('Do you want to update the record ?');");

            txt_Tab_ValueOfDeal_Core.Attributes.Add("onkeypress", "return numbercommadotonly(event);");

            txt_Tab_Client.Attributes.Add("onkeypress", "return AllowOnlyAlphabetsSpl(event);");

			txt_Tab_ProjectName_Core.Attributes.Add("onkeypress", "return AllowOnlyAlphabets(event);");

            txt_Tab_Bible_Reference.Attributes.Add("onkeypress", "return AlphaNumericonly(event);");

			txt_Tab_Matter_No.Attributes.Add("onkeypress", "return AlphaNumericDotonly(event);");


            /* Block escape */
            txt_Tab_Client.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_ClientDescription.Attributes.Add("onkeydown", "return BlockEnter(event);");
			txt_Tab_ClientDescription.Attributes.Add("onkeypress", "return maxLength(event,'" + txt_Tab_ClientDescription.ClientID + "');");
            txt_Tab_ClientIndustrySector.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Client_Industry_Type.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Country_PredominantCountry.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Matter_No.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Date_Completed.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_TransactionIndustrySector.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Transaction_Industry_Type.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Project_Description.Attributes.Add("onkeydown", "return BlockEnter(event);");
			txt_Tab_Project_Description.Attributes.Add("onkeypress", "return maxLength(event,'" + txt_Tab_Project_Description.ClientID + "');");
            txt_Tab_Significant_Features.Attributes.Add("onkeydown", "return BlockEnter(event);");
			txt_Tab_Significant_Features.Attributes.Add("onkeypress", "return maxLength(event,'" + txt_Tab_Significant_Features.ClientID + "');");
            txt_Tab_Keyword.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_ProjectName_Core.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Country_Matter_Close.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Country_Matter_Open.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Country_ArbitrationCountry.Attributes.Add("onkeydown", "return BlockEnter(event);");

            txt_Tab_Language_Of_Dispute.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Country_Jurisdiction.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Team.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Lead_Partner.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Source_Of_Credential.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_SourceOfCredential_Other.Attributes.Add("onkeydown", "return BlockEnter(event);");

            txt_Tab_Other_Matter_Executive.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Other_Uses.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Country_OtherCMSOffice.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Know_How.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Bible_Reference.Attributes.Add("onkeydown", "return BlockEnter(event);");

            txt_Tab_Country_Law_Other.Attributes.Add("onkeydown", "return BlockEnter(event);");
            txt_Tab_Language_Of_Dispute_Other.Attributes.Add("onkeydown", "return BlockEnter(event);");

            txt_Tab_ClientIndustrySector.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Client_Industry_Type.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Country_PredominantCountry.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Date_Completed.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_TransactionIndustrySector.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Transaction_Industry_Type.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Country_Matter_Close.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Country_Matter_Open.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Country_ArbitrationCountry.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Language_Of_Dispute.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Country_Jurisdiction.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Team.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Lead_Partner.Attributes.Add("onkeydown", "return BlockBackspace(event);");

            txt_Tab_Other_Matter_Executive.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Other_Uses.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Country_OtherCMSOffice.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Know_How.Attributes.Add("onkeydown", "return BlockBackspace(event);");
            txt_Tab_Source_Of_Credential.Attributes.Add("onkeydown", "return BlockBackspace(event);");

            /*rdo_Tab_Client_Name_Confidential.Attributes.Add("onkeypress", "return BlockEnter(event);");BlockBackspace
            rdo_Tab_NameConfidential_Completion.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_CMSPartnerName.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_ClientIndustrySector.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Client_Industry_Type.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Country_PredominantCountry.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chKBAIF.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chKCorp.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chKCRD.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chkEPC.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chkEPCE.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chkHC.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chkIPF.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chkRE.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Matter_No.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Date_Opened.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_TransactionIndustrySector.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Transaction_Industry_Type.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Language.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Project_Description.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_ProjectDescription_Polish.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Significant_Features.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Country_Matter_Open.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Country_Matter_Close.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Contentious_IRG.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Country_Jurisdiction.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Language_Of_Dispute.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Dispute_Resolution.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_ArbitrationCity.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Arbitral_Rules.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Investigation_Type.Attributes.Add("onkeypress", "return BlockEnter(event);");
            rdo_Tab_InvestmentTreaty.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_ValueOfDeal_Core.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Currency_Of_Deal.Attributes.Add("onkeypress", "return BlockEnter(event);");
            rdo_Tab_Value_Confidential.Attributes.Add("onkeypress", "return BlockEnter(event);");
            rdo_Tab_ValueConfidential_Completion.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Team.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Lead_Partner.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_CMSPartnerName.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Source_Of_Credential.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Other_Matter_Executive.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Referred_From_Other_CMS_Office.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Lead_CMS_Firm.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Country_OtherCMSOffice.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Credential_Status.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Credential_Type.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Credential_Version.Attributes.Add("onkeypress", "return BlockEnter(event);");
            cbo_Tab_Priority.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Know_How.Attributes.Add("onkeypress", "return BlockEnter(event);");
            rdo_Tab_ProBono.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Bible_Reference.Attributes.Add("onkeypress", "return BlockEnter(event);");
            rdo_Tab_MatterConfidential_Completion.Attributes.Add("onkeypress", "return BlockEnter(event);");
            rdo_Tab_Client_Matter_Confidential.Attributes.Add("onkeypress", "return BlockEnter(event);");
            txt_Tab_Date_Completed.Attributes.Add("onkeypress", "return BlockEnter(event);");
            chk_Tab_ActualDate_Ongoing.Attributes.Add("onkeypress", "return BlockEnter(event);");
            */

            txt_Tab_Client.Attributes.Add("onblur", "WaterMarktext(this, event , 'Insert client name in full')");
            txt_Tab_Client.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Insert client name in full')");
            txt_Tab_ClientDescription.Attributes.Add("onblur", "WaterMarktext(this, event , 'Eg. a leading retail bank, an international IT company etc')");
            txt_Tab_ClientDescription.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Eg. a leading retail bank, an international IT company etc')");
            /*change text*/
            //txt_Tab_ClientDescription_OtherLanguage.Attributes.Add("onblur", "WaterMarktext(this, event , 'Accurately type full name')");
            //txt_Tab_ClientDescription_OtherLanguage.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Accurately type full name')");

            txt_Tab_ClientIndustrySector.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the sector of the client company from look up')");
            txt_Tab_ClientIndustrySector.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the sector of the client company from look up')");
            txt_Tab_Client_Industry_Type.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the sub-sector of the client company from look up')");
            txt_Tab_Client_Industry_Type.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the sub-sector of the client company from look up')");
            txt_Tab_Country_PredominantCountry.Attributes.Add("onblur", "WaterMarktext(this, event , 'Eg. where head quartered')");
            txt_Tab_Country_PredominantCountry.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Eg. where head quartered')");

            txt_Tab_Matter_No.Attributes.Add("onblur", "WaterMarktext(this, event , 'Eg. 123456.00001')");
            txt_Tab_Matter_No.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Eg. 123456.00001')");
            txt_Tab_Date_Completed.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select date from calendar icon or select ongoing')");
            txt_Tab_Date_Completed.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select date from calendar icon or select ongoing')");


            txt_Tab_TransactionIndustrySector.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the sector the matter relates to (not worktype) from look up')");
            txt_Tab_TransactionIndustrySector.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the sector the matter relates to (not worktype) from look up')");
            txt_Tab_Transaction_Industry_Type.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the sub-sector of the matter from look up')");
            txt_Tab_Transaction_Industry_Type.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the sub-sector of the matter from look up')");
            txt_Tab_Project_Description.Attributes.Add("onblur", "WaterMarktext(this, event , 'Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion.')");
            txt_Tab_Project_Description.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion.')");
            txt_Tab_Significant_Features.Attributes.Add("onblur", "WaterMarktext(this, event , 'Insert any other useful information about the credential that will be useful for future reference purposes')");
            txt_Tab_Significant_Features.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Insert any other useful information about the credential that will be useful for future reference purposes')");
            txt_Tab_Keyword.Attributes.Add("onblur", "WaterMarktext(this, event , 'Include any other key words associated with the matter')");
            txt_Tab_Keyword.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Include any other key words associated with the matter')");

            txt_Tab_Country_Matter_Close.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the country(s) of the matter/transaction from look up')");
            txt_Tab_Country_Matter_Close.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the country(s) of the matter/transaction from look up')");
            txt_Tab_Language_Of_Dispute.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the language of dispute from look up')");
            txt_Tab_Language_Of_Dispute.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the language of dispute from look up')");
            txt_Tab_Country_Jurisdiction.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the country of dispute from look up')");
            txt_Tab_Country_Jurisdiction.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the country of dispute from look up')");

            txt_Tab_Country_Matter_Open.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the country where matter opened from look up')");
            txt_Tab_Country_Matter_Open.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the country where matter opened from look up')");

            txt_Tab_Source_Of_Credential.Attributes.Add("onblur", "WaterMarktext(this, event , 'Select the source of credential from look up')");
            txt_Tab_Source_Of_Credential.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Select the source of credential from look up')");

            //Select the source of credential from look up
            txt_Tab_Team.Attributes.Add("onblur", "WaterMarktext(this, event , 'Multi select from look up')");
            txt_Tab_Team.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Multi select from look up')");//Multi select from look up
            txt_Tab_Lead_Partner.Attributes.Add("onblur", "WaterMarktext(this, event , 'Multi select from look up')");
            txt_Tab_Lead_Partner.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Multi select from look up')");
            txt_Tab_CMSPartnerName.Attributes.Add("onblur", "WaterMarktext(this, event , 'Open field – format last name first name')");
            txt_Tab_CMSPartnerName.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Open field – format last name first name')");
            txt_Tab_Other_Matter_Executive.Attributes.Add("onblur", "WaterMarktext(this, event , 'Multi select from look up')");
            txt_Tab_Other_Matter_Executive.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Multi select from look up')");
            txt_Tab_Referred_From_Other_CMS_Office.Attributes.Add("onblur", "WaterMarktext(this, event , 'Multi select from look up')");
            txt_Tab_Referred_From_Other_CMS_Office.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Multi select from look up')");
            txt_Tab_Country_OtherCMSOffice.Attributes.Add("onblur", "WaterMarktext(this, event , 'Multi select from look up')");
            txt_Tab_Country_OtherCMSOffice.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Multi select from look up')");


            txt_Tab_Other_Uses.Attributes.Add("onblur", "WaterMarktext(this, event , 'Eg. case study/directory submission select from look-up and enter docs# in other useful credential information box')");
            txt_Tab_Other_Uses.Attributes.Add("onfocus", "WaterMarktext(this, event , 'Eg. case study/directory submission select from look-up and enter docs# in other useful credential information box')");
            txt_Tab_Know_How.Attributes.Add("onblur", "WaterMarktext(this, event , 'For corporate deals only select relevant theme from look up')");
            txt_Tab_Know_How.Attributes.Add("onfocus", "WaterMarktext(this, event , 'For corporate deals only select relevant theme from look up')");
            txt_Tab_Bible_Reference.Attributes.Add("onblur", "WaterMarktext(this, event , 'For corporate deals only')");
            txt_Tab_Bible_Reference.Attributes.Add("onfocus", "WaterMarktext(this, event , 'For corporate deals only')");
            txt_Tab_ProjectName_Core.Attributes.Add("onblur", "WaterMarktext(this, event , 'If applicable e.g. Project Camden')");
            txt_Tab_ProjectName_Core.Attributes.Add("onfocus", "WaterMarktext(this, event , 'If applicable e.g. Project Camden')");

            chKBAIF.Attributes.Add("onClick", "return ShowModalEntry('" + lblBAIF.ClientID + "','" + chKBAIF.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strBAIFHeight + "','" + Constants.strBAIFWidth + "','" + Constants.strBAIFPageName + "','" + plnEntry.ClientID + "','" + Constants.strBAIFPanelHeight + "','" + lblBAIF.Text + "','" + Constants.strBAIFColor + "','" + hid_BAIF.ClientID + "','" + Constants.strBAIFPanelWidth + "');");
            chKCorp.Attributes.Add("onClick", "return ShowModalEntry('" + lblCorp.ClientID + "','" + chKCorp.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strCorpHeight + "','" + Constants.strCorpWidth + "','" + Constants.strCorpPageName + "','" + plnEntry.ClientID + "','" + Constants.strCorpPanelHeight + "','" + lblCorp.Text + "','" + Constants.strCorpColor + "','" + hid_Corp.ClientID + "','" + Constants.strCorpPanelWidth + "');");
            chKCRD.Attributes.Add("onClick", "return ShowModalEntry('" + lblCRD.ClientID + "','" + chKCRD.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strCRDHeight + "','" + Constants.strCRDWidth + "','" + Constants.strCRDPageName + "','" + plnEntry.ClientID + "','" + Constants.strCRDPanelHeight + "','" + lblCRD.Text + "','" + Constants.strCRDColor + "','" + hid_CRD.ClientID + "','" + Constants.strBAIFPanelWidth + "');");
            chkEPC.Attributes.Add("onClick", "return ShowModalEntry('" + lblEPC.ClientID + "','" + chkEPC.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strEPCHeight + "','" + Constants.strEPCWidth + "','" + Constants.strEPCPageName + "','" + plnEntry.ClientID + "','" + Constants.strEPCPanelHeight + "','" + lblEPC.Text + "','" + Constants.strEPCCColor + "','" + hid_EPC.ClientID + "','" + Constants.strBAIFPanelWidth + "');");
            chkRE.Attributes.Add("onClick", "return ShowModalEntry('" + lblRE.ClientID + "','" + chkRE.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strREHeight + "','" + Constants.strREWidth + "','" + Constants.strREPageName + "','" + plnEntry.ClientID + "','" + Constants.strREPanelHeight + "','" + lblRE.Text + "','" + Constants.strREColor + "','" + hid_RE.ClientID + "','" + Constants.strBAIFPanelWidth + "');");
            chkIPF.Attributes.Add("onClick", "return ShowModalEntry('" + lblIPF.ClientID + "','" + chkIPF.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strIPFHeight + "','" + Constants.strIPFWidth + "','" + Constants.strIPFPageName + "','" + plnEntry.ClientID + "','" + Constants.strIPFPanelHeight + "','" + lblIPF.Text + "','" + Constants.strIPFColor + "','" + hid_IPF.ClientID + "','" + Constants.strBAIFPanelWidth + "');");
            chkEPCE.Attributes.Add("onClick", "return ShowModalEntry('" + lblENE.ClientID + "','" + chkEPCE.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strEPCEHeight + "','" + Constants.strEPCEWidth + "','" + Constants.strEPCEPageName + "','" + plnEntry.ClientID + "','" + Constants.strEPCEPanelHeight + "','" + lblENE.Text + "','" + Constants.strEPCEColor + "','" + hid_EPCE.ClientID + "','" + Constants.strBAIFPanelWidth + "');");
            chkCorpTax.Attributes.Add("onClick", "return ShowModalEntry('" + lblCorpTax.ClientID + "','" + chkCorpTax.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strCorpTaxHeight + "','" + Constants.strCorpTaxWidth + "','" + Constants.strCorpTaxPageName + "','" + plnEntry.ClientID + "','" + Constants.strCorpTaxPanelHeight + "','" + lblCorpTax.Text + "','" + Constants.strCorpTaxColor + "','" + hid_CorpTax.ClientID + "','" + Constants.strCorpTaxPanelWidth + "');");
            chkHC.Attributes.Add("onClick", "return ShowModalEntry('" + lblHC.ClientID + "','" + chkHC.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strHCHeight + "','" + Constants.strHCWidth + "','" + Constants.strHCPageName + "','" + plnEntry.ClientID + "','" + Constants.strHCPanelHeight + "','" + lblHC.Text + "','" + Constants.strHCColor + "','" + hid_HC.ClientID + "','" + Constants.strBAIFPanelWidth + "');");

            lblBAIF.Attributes.Add("onClick", "return ShowLabelEntry('" + lblBAIF.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strBAIFHeight + "','" + Constants.strBAIFWidth + "','" + Constants.strBAIFPageName + "','" + plnEntry.ClientID + "','" + Constants.strBAIFPanelHeight + "','" + Constants.strBAIFColor + "','" + Constants.strBAIFPanelWidth + "');");
            lblCorp.Attributes.Add("onClick", "return ShowLabelEntry('" + lblCorp.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strCorpHeight + "','" + Constants.strCorpWidth + "','" + Constants.strCorpPageName + "','" + plnEntry.ClientID + "','" + Constants.strCorpPanelHeight + "','" + Constants.strCorpColor + "','" + Constants.strCorpPanelWidth + "');");
            lblCRD.Attributes.Add("onClick", "return ShowLabelEntry('" + lblCRD.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strCRDHeight + "','" + Constants.strCRDWidth + "','" + Constants.strCRDPageName + "','" + plnEntry.ClientID + "','" + Constants.strCRDPanelHeight + "','" + Constants.strCRDColor + "','" + Constants.strBAIFPanelWidth + "');");
            lblEPC.Attributes.Add("onClick", "return ShowLabelEntry('" + lblEPC.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strEPCHeight + "','" + Constants.strEPCWidth + "','" + Constants.strEPCPageName + "','" + plnEntry.ClientID + "','" + Constants.strEPCPanelHeight + "','" + Constants.strEPCCColor + "','" + Constants.strBAIFPanelWidth + "');");
            lblRE.Attributes.Add("onClick", "return ShowLabelEntry('" + lblRE.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strREHeight + "','" + Constants.strREWidth + "','" + Constants.strREPageName + "','" + plnEntry.ClientID + "','" + Constants.strREPanelHeight + "','" + Constants.strREColor + "','" + Constants.strBAIFPanelWidth + "');");
            lblIPF.Attributes.Add("onClick", "return ShowLabelEntry('" + lblIPF.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strIPFHeight + "','" + Constants.strIPFWidth + "','" + Constants.strIPFPageName + "','" + plnEntry.ClientID + "','" + Constants.strIPFPanelHeight + "','" + Constants.strIPFColor + "','" + Constants.strBAIFPanelWidth + "');");
            lblENE.Attributes.Add("onClick", "return ShowLabelEntry('" + lblENE.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strEPCEHeight + "','" + Constants.strEPCEWidth + "','" + Constants.strEPCEPageName + "','" + plnEntry.ClientID + "','" + Constants.strEPCEPanelHeight + "','" + Constants.strEPCEColor + "','" + Constants.strBAIFPanelWidth + "');");
            lblHC.Attributes.Add("onClick", "return ShowLabelEntry('" + lblHC.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strHCHeight + "','" + Constants.strHCWidth + "','" + Constants.strHCPageName + "','" + plnEntry.ClientID + "','" + Constants.strHCPanelHeight + "','" + Constants.strHCColor + "','" + Constants.strBAIFPanelWidth + "');");
            lblCorpTax.Attributes.Add("onClick", "return ShowLabelEntry('" + lblCorpTax.ClientID + "','" + IframeEntry.ClientID + "','" + Constants.strCorpTaxHeight + "','" + Constants.strCorpTaxWidth + "','" + Constants.strCorpTaxPageName + "','" + plnEntry.ClientID + "','" + Constants.strCorpTaxPanelHeight + "','" + Constants.strCorpTaxColor + "','" + Constants.strCorpTaxPanelWidth + "');");
	
            chk_Tab_ActualDate_Ongoing.Attributes.Add("onClick", "checkOngoing();");
			chk_Tab_ActualDate_Ongoing_1.Attributes.Add("onClick", "checkNotKnown();");
            lblBAIF.Attributes.Add("onmouseover", "zoomin('" + lblBAIF.ClientID + "')");
            lblBAIF.Attributes.Add("onmouseout", "zoomout('" + lblBAIF.ClientID + "')");

            lblCRD.Attributes.Add("onmouseover", "zoomin('" + lblCRD.ClientID + "')");
            lblCRD.Attributes.Add("onmouseout", "zoomout('" + lblCRD.ClientID + "')");

            lblCorp.Attributes.Add("onmouseover", "zoomin('" + lblCorp.ClientID + "')");
            lblCorp.Attributes.Add("onmouseout", "zoomout('" + lblCorp.ClientID + "')");

            lblEPC.Attributes.Add("onmouseover", "zoomin('" + lblEPC.ClientID + "')");
            lblEPC.Attributes.Add("onmouseout", "zoomout('" + lblEPC.ClientID + "')");

            lblENE.Attributes.Add("onmouseover", "zoomin('" + lblENE.ClientID + "')");
            lblENE.Attributes.Add("onmouseout", "zoomout('" + lblENE.ClientID + "')");

            lblIPF.Attributes.Add("onmouseover", "zoomin('" + lblIPF.ClientID + "')");
            lblIPF.Attributes.Add("onmouseout", "zoomout('" + lblIPF.ClientID + "')");

            lblRE.Attributes.Add("onmouseover", "zoomin('" + lblRE.ClientID + "')");
            lblRE.Attributes.Add("onmouseout", "zoomout('" + lblRE.ClientID + "')");

            lblHC.Attributes.Add("onmouseover", "zoomin('" + lblHC.ClientID + "')");
            lblHC.Attributes.Add("onmouseout", "zoomout('" + lblHC.ClientID + "')");

            lblCorpTax.Attributes.Add("onmouseover", "zoomin('" + lblCorpTax.ClientID + "')");
            lblCorpTax.Attributes.Add("onmouseout", "zoomout('" + lblCorpTax.ClientID + "')");

            if (chKBAIF.Checked == true)
            {
                lblBAIF.Style.Add("display", "block");
            }
            if (chKCRD.Checked == true)
            {
                lblCRD.Style.Add("display", "block");
            }
            if (chKCorp.Checked == true)
            {
                lblCorp.Style.Add("display", "block");
            }
            if (chkEPC.Checked == true)
            {
                lblEPC.Style.Add("display", "block");
            }
            if (chkEPCE.Checked == true)
            {
                lblENE.Style.Add("display", "block");
            }
            if (chkIPF.Checked == true)
            {
                lblIPF.Style.Add("display", "block");
            }
            if (chkRE.Checked == true)
            {
                lblRE.Style.Add("display", "block");
            }
            if (chkHC.Checked == true)
            {
                lblHC.Style.Add("display", "block");
            }
            if (chkCorpTax.Checked == true)
            {
                lblCorpTax.Style.Add("display", "block");
            }

            //img_Tab_Country_PredominantCountry.Attributes.Add("onClick", "return ShowGridModal('" + img_Tab_Country_PredominantCountry.ClientID + "','" + lbl_Tab_Country_PredominantCountry.Text + "','" + lbl_Tab_Country_PredominantCountry.ID + "','" + IframeGrid.ClientID + "');");
            img_Tab_Country_PredominantCountry.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Tab_Country_PredominantCountry.Text + "','" + lbl_Tab_Country_PredominantCountry.ID + "','" + hid_Tab_Country_PredominantCountry.ClientID + "');return false;");
            img_Tab_Country_Jurisdiction.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Tab_Country_Jurisdiction.Text + "','" + lbl_Tab_Country_Jurisdiction.ID + "','" + hid_Tab_Country_Jurisdiction.ClientID + "');return false;");
            img_Tab_Country_OtherCMSOffice.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Tab_Country_OtherCMSOffice.Text + "','" + lbl_Tab_Country_OtherCMSOffice.ID + "','" + hid_Tab_Country_OtherCMSOffice.ClientID + "');return false;");
            img_Tab_Country_Matter_Open.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Tab_Country_Matter_Open.Text + "','" + lbl_Tab_Country_Matter_Open.ID + "','" + hid_Tab_Country_Matter_Open.ClientID + "');return false;");
            img_Tab_Country_ArbitrationCountry.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Tab_Country_ArbitrationCountry.Text + "','" + lbl_Tab_Country_ArbitrationCountry.ID + "','" + hid_Tab_Country_ArbitrationCountry.ClientID + "');return false;");
            img_Tab_Country_Matter_Close.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Tab_Country_Matter_Close.Text + "','" + lbl_Tab_Country_Matter_Close.ID + "','" + hid_Tab_Country_Matter_Close.ClientID + "');return false;");

            // img_Tab_Source_Of_Credential.Attributes.Add("onClick", "return ShowEditModal('" + img_Tab_Source_Of_Credential.ClientID + "','" + lbl_Tab_Source_Of_Credential.Text + "','" + lbl_Tab_Source_Of_Credential.ID + "','" + IframeEdit.ClientID + "');");
            img_Tab_Client_Industry_Type.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Client_Industry_Type.Text + "','" + lbl_Tab_Client_Industry_Type.ID + "','" + hid_Tab_Client_Industry_Type.ClientID + "');return false;");
            img_Tab_Transaction_Industry_Type.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Transaction_Industry_Type.Text + "','" + lbl_Tab_Transaction_Industry_Type.ID + "','" + hid_Tab_Transaction_Industry_Type.ClientID + "');return false;");
            img_Tab_Team.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Team.Text + "','" + lbl_Tab_Team.ID + "','" + hid_Tab_Team.ClientID + "');return false;");
            img_Tab_Lead_Partner.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Lead_Partner.Text + "','" + lbl_Tab_Lead_Partner.ID + "','" + hid_Tab_Lead_Partner.ClientID + "');return false;");
            img_Tab_Other_Matter_Executive.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Other_Matter_Executive.Text + "','" + lbl_Tab_Other_Matter_Executive.ID + "','" + hid_Tab_Other_Matter_Executive.ClientID + "');return false;");
            img_Tab_Referred_From_Other_CMS_Office.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Referred_From_Other_CMS_Office.Text + "','" + lbl_Tab_Referred_From_Other_CMS_Office.ID + "','" + hid_Tab_Referred_From_Other_CMS_Office.ClientID + "');return false;");
            img_Tab_Source_Of_Credential.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Source_Of_Credential.Text + "','" + lbl_Tab_Source_Of_Credential.ID + "','" + hid_Tab_Source_Of_Credential.ClientID + "');return false;");
            img_Tab_TransactionIndustrySector.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_TransactionIndustrySector.Text + "','" + lbl_Tab_TransactionIndustrySector.ID + "','" + hid_Tab_TransactionIndustrySector.ClientID + "');return false;");
            img_Tab_ClientIndustrySector.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_ClientIndustrySector.Text + "','" + lbl_Tab_ClientIndustrySector.ID + "','" + hid_Tab_ClientIndustrySector.ClientID + "');return false;");
            img_Tab_Other_Uses.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Other_Uses.Text + "','" + lbl_Tab_Other_Uses.ID + "','" + hid_Tab_Other_Uses.ClientID + "');return false;");
            img_Tab_Know_How.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Know_How.Text + "','" + lbl_Tab_Know_How.ID + "','" + hid_Tab_Know_How.ClientID + "');return false;");
            img_Tab_Language_Of_Dispute.Attributes.Add("onClick", "LoadChild('" + lbl_Tab_Language_Of_Dispute.Text + "','" + lbl_Tab_Language_Of_Dispute.ID + "','" + hid_Tab_Language_Of_Dispute.ClientID + "');return false;");
			
			img_Date.Attributes.Add("onclick", "return ClearDate();");
			img_DateOpened.Attributes.Add("onclick", "return ClearDateOpened();");
			
            objLogger.LogWriter("EntryScreen : SetJavascriptClientEvents Ends ", hidName.Value);
        }

        protected void rdo_Tab_Client_Name_Confidential_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_Tab_ClientIndustrySector.Focus();
				rdo_Tab_Client_Name_Confidential.Focus();
                tr_Tab_ClientDescription.Visible = false;
				tr_Tab_NameConfidential_Completion.Visible = false;               

                if (rdo_Tab_Client_Name_Confidential.SelectedItem.Text.Trim().ToUpper() == "YES") //YES
                {
            		tr_Tab_ClientDescription.Visible = true;
					tr_Tab_NameConfidential_Completion.Visible = true;
                    tr_Tab_ClientDescription.Visible = true;                   
					txt_Tab_ClientDescription.Text = "Eg. a leading retail bank, an international IT company etc";
                }
                else
                {
                    rdo_Tab_NameConfidential_Completion.SelectedIndex = -1;
                    txt_Tab_ClientDescription.Text = string.Empty;
                    txt_Tab_ClientDescription.Text = string.Empty;
                    //cbo_Tab_ClientDescription_Language.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails : rdo_Tab_Client_Name_Confidential_SelectedIndexChanged Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void rdo_Tab_Client_Matter_Confidential_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tr_Tab_MatterConfidential_Completion.Visible = false;
                rdo_Tab_Client_Matter_Confidential.Focus();


                if (rdo_Tab_Client_Matter_Confidential.SelectedItem.Text.Trim().ToUpper() == "YES")
                {
                    tr_Tab_MatterConfidential_Completion.Visible = true;
                    rdo_Tab_MatterConfidential_Completion.Focus();
                }
                else
                {
                    rdo_Tab_MatterConfidential_Completion.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails : rdo_Tab_Client_Matter_Confidential_SelectedIndexChanged Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void rdo_Tab_Value_Confidential_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                trr_Tab_ValueConfidential_Completion.Visible = false;
                rdo_Tab_Value_Confidential.Focus();
                if (rdo_Tab_Value_Confidential.SelectedItem.Text.Trim().ToUpper() == "YES")
                {
                    trr_Tab_ValueConfidential_Completion.Visible = true;
                    rdo_Tab_ValueConfidential_Completion.Focus();
                }
                else
                {
                    rdo_Tab_ValueConfidential_Completion.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails : rdo_Tab_Value_Confidential_SelectedIndexChanged Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void cbo_Tab_Contentious_IRG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                plnContentiousDetails.Visible = false;
                cbo_Tab_Contentious_IRG.Focus();
                if (cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "CONTENTIOUS")
                {
                    plnContentiousDetails.Visible = true;
                    //cbo_Tab_Type_Of_Dispute.Focus();
                }
                else
                {
                    txt_Tab_Country_Jurisdiction.Text = string.Empty;
                    hid_Tab_Country_Jurisdiction.Value = string.Empty;
                    hid_Tab_Country_Jurisdiction_Text.Value = string.Empty;
                    txt_Tab_Language_Of_Dispute.Text = string.Empty;
                    hid_Tab_Language_Of_Dispute.Value = string.Empty;
                    hid_Tab_Language_Of_Dispute_Text.Value = string.Empty;

                    cbo_Tab_Dispute_Resolution.SelectedIndex = -1;

                    tr_Tab_Investigation_Type.Visible = false;
                    cbo_Tab_Investigation_Type.SelectedIndex = -1;

                    tr_Tab_Arbitral_Rules.Visible = false;
                    cbo_Tab_Arbitral_Rules.SelectedIndex = -1;

                    tr_Tab_ArbitrationCity.Visible = false;
                    cbo_Tab_ArbitrationCity.SelectedIndex = -1;

                    tr_Tab_InvestmentTreaty.Visible = false;
                    rdo_Tab_InvestmentTreaty.SelectedIndex = -1;

                    txt_Tab_Language_Of_Dispute_Other.Text = string.Empty;
                    hid_Tab_Country_Jurisdiction_Text.Value = string.Empty;
                    hid_Tab_Language_Of_Dispute_Other_Text.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails : cbo_Tab_Contentious_IRG_SelectedIndexChanged Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void cbo_Tab_Dispute_Resolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tr_Tab_Arbitral_Rules.Visible = false;
                tr_Tab_ArbitrationCity.Visible = false;
                tr_Tab_Country_ArbitrationCountry.Visible = false;
                tr_Tab_InvestmentTreaty.Visible = false;
                tr_Tab_Investigation_Type.Visible = false;
                cbo_Tab_Dispute_Resolution.Focus();

                if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "ARBITRATION")
                {
                    tr_Tab_Arbitral_Rules.Visible = true;
                    tr_Tab_ArbitrationCity.Visible = true;
                    tr_Tab_InvestmentTreaty.Visible = true;
                    tr_Tab_Country_ArbitrationCountry.Visible = true;
                }
                else
                {
                    cbo_Tab_Arbitral_Rules.SelectedIndex = -1;
                    cbo_Tab_ArbitrationCity.SelectedIndex = -1;
                    txt_Tab_ArbitrationCity_Other.Text = string.Empty;
                    rdo_Tab_InvestmentTreaty.SelectedIndex = -1;
                }
                if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "INVESTIGATION - EXTERNAL" || cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "INVESTIGATION - INTERNAL")
                {
                    tr_Tab_Investigation_Type.Visible = true;
                }
                else
                {
                    tr_Tab_Investigation_Type.Visible = false;
                    cbo_Tab_Investigation_Type.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails : cbo_Tab_Dispute_Resolution_SelectedIndexChanged Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void btnCancelEntry_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["sessBAIFSS"] == null)
                {
                    chKBAIF.Checked = false;
                }
                if (Session["sessCRDSS"] == null)
                {
                    chKCRD.Checked = false;
                }
                if (Session["sessCORPSS"] == null)
                {
                    chKCorp.Checked = false;
                }
                if (Session["sessEPCSS"] == null)
                {
                    chkEPC.Checked = false;
                }
                if (Session["sessEPCESS"] == null && Session["sessEPCEMS"] == null)
                {
                    chkEPCE.Checked = false;
                }
                if (Session["sessHCSS"] == null)
                {
                    chkHC.Checked = false;
                }
                if (Session["sessIPFSS"] == null)
                {
                    chkIPF.Checked = false;
                }
                if (Session["sessRESS"] == null && Session["sessREMS"] == null)
                {
                    chkRE.Checked = false;
                }
                if (Session["sessCorpTaxSS"] == null)
                {
                    chkCorpTax.Checked = false;
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails : btnCancelEntry_Click Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void btnAddBottom_Click(object sender, EventArgs e)
        {
            //bool blnAdd = false;
            string strMainInsert = string.Empty;
            //System.Threading.Thread.Sleep(4000);
            Logger obj = new Logger();
			string strid = string.Empty;
            objLogger.LogWriter("EntryScreen : btnAddBottom_Click Starts ", hidName.Value);

            StringBuilder strtblCredFields = new StringBuilder();
            StringBuilder strtblCredValues = new StringBuilder();
            StringBuilder strOptionaltblCredValues = new StringBuilder();
            StringBuilder strOptionaltblCredFields = new StringBuilder();
            StringBuilder strBAIFtblCredFields = new StringBuilder();
            StringBuilder strBAIFtblCredValues = new StringBuilder();
            StringBuilder strCorptblCredFields = new StringBuilder();
            StringBuilder strCorptblCredValues = new StringBuilder();
            StringBuilder strCRDtblCredFields = new StringBuilder();
            StringBuilder strCRDtblCredValues = new StringBuilder();
            StringBuilder strEPCtblCredFields = new StringBuilder();
            StringBuilder strEPCtblCredValues = new StringBuilder();
            StringBuilder strEPCEnetblCredFields = new StringBuilder();
            StringBuilder strEPCEnetblCredValues = new StringBuilder();
            StringBuilder strHCtblCredFields = new StringBuilder();
            StringBuilder strHCtblCredValues = new StringBuilder();
            StringBuilder strIPFtblCredFields = new StringBuilder();
            StringBuilder strIPFtblCredValues = new StringBuilder();
            StringBuilder strREtblCredFields = new StringBuilder();
            StringBuilder strREtblCredValues = new StringBuilder();
            StringBuilder strCorpTaxtblCredFields = new StringBuilder();
            StringBuilder strCorpTaxtblCredValues = new StringBuilder();
            StringBuilder strquerycols = new StringBuilder();
            StringBuilder strqueryvals = new StringBuilder();
            StringBuilder strPrac = new StringBuilder();

            bool blnCheck = false;
			
            SetTextBoxValues();

            if (blnCheck == false)
            {
                string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
                SqlConnection con = new SqlConnection(strcon);
                SqlCommand cmd = new SqlCommand();
                con.Open();
				SqlTransaction transaction = con.BeginTransaction();
				try
				{
					txt_Tab_Client.Text = txt_Tab_Client.Text.Replace("'", "''");
					txt_Tab_ClientDescription.Text = txt_Tab_ClientDescription.Text.Replace("'", "''");
					txt_Tab_Project_Description.Text = txt_Tab_Project_Description.Text.Replace("'", "''");

					txt_Tab_Significant_Features.Text = txt_Tab_Significant_Features.Text.Replace("'", "''");
					txt_Tab_Keyword.Text = txt_Tab_Keyword.Text.Replace("'", "''");
					txt_Tab_ProjectName_Core.Text = txt_Tab_ProjectName_Core.Text.Replace("'", "''");
					txt_Tab_Matter_No.Text = txt_Tab_Matter_No.Text.Replace("'", "''");
					txt_Tab_Bible_Reference.Text = txt_Tab_Bible_Reference.Text.Replace("'", "''");
					txt_Tab_CMSPartnerName.Text = txt_Tab_CMSPartnerName.Text.Replace("'", "''");
					txt_Tab_Country_Law_Other.Text = txt_Tab_Country_Law_Other.Text.Replace("'", "''");
					txt_Tab_ArbitrationCity_Other.Text = txt_Tab_ArbitrationCity_Other.Text.Replace("'", "''");
					txt_Tab_SourceOfCredential_Other.Text = txt_Tab_SourceOfCredential_Other.Text.Replace("'", "''");
					txt_Tab_Language_Of_Dispute_Other.Text = txt_Tab_Language_Of_Dispute_Other.Text.Replace("'", "''");

                    /* Client Name */
                    if (!string.IsNullOrEmpty(txt_Tab_Client.Text.Trim()))
                    {
                        strtblCredFields.Append(lbl_Tab_Client.ID.Substring(8, lbl_Tab_Client.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(txt_Tab_Client.Text.Trim());
						strtblCredValues.Append("',N'");
                    }

                    /* Client Name Confidential */
                    if (rdo_Tab_Client_Name_Confidential.SelectedIndex != -1)
                    {
                        strtblCredFields.Append(lbl_Tab_Client_Name_Confidential.ID.Substring(8, lbl_Tab_Client_Name_Confidential.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(rdo_Tab_Client_Name_Confidential.SelectedItem.Value);
						strtblCredValues.Append("',N'");

                        /* Description , confidential on completion*/
                        if (rdo_Tab_Client_Name_Confidential.SelectedItem.Text.Trim().ToUpper() == "YES")
                        {
                            if (!string.IsNullOrEmpty(txt_Tab_ClientDescription.Text.Trim()))
                            {
                                strtblCredFields.Append(lbl_Tab_ClientDescription.ID.Substring(8, lbl_Tab_ClientDescription.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(txt_Tab_ClientDescription.Text.Trim());
								strtblCredValues.Append("',N'");
                            }

                            if (rdo_Tab_NameConfidential_Completion.SelectedIndex != -1)
                            {
                                strtblCredFields.Append(lbl_Tab_NameConfidential_Completion.ID.Substring(8, lbl_Tab_NameConfidential_Completion.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(rdo_Tab_NameConfidential_Completion.SelectedItem.Value);
								strtblCredValues.Append("',N'");
                            }
                        }
                        else
                        {
                            strtblCredFields.Append(lbl_Tab_ClientDescription.ID.Substring(8, lbl_Tab_ClientDescription.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");

                            strtblCredFields.Append(lbl_Tab_NameConfidential_Completion.ID.Substring(8, lbl_Tab_NameConfidential_Completion.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");
                        }

                    }

                    /*Matter No*/
                    if (!string.IsNullOrEmpty(txt_Tab_Matter_No.Text.Trim()))
                    {
                        strtblCredFields.Append(lbl_Tab_Matter_No.ID.Substring(8, lbl_Tab_Matter_No.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(txt_Tab_Matter_No.Text.Trim());
						strtblCredValues.Append("',N'");
                    }
					
					if (cld_Tab_Date_Opened.SelectedDate.HasValue && cld_Tab_Date_Opened.SelectedDate.HasValue)
					{
						strtblCredFields.Append(lbl_Tab_Date_Opened.ID.Substring(8, lbl_Tab_Date_Opened.ID.Length - 8));
						strtblCredFields.Append(",");

						string str = strtblCredValues.ToString().Substring(0, strtblCredValues.Length - 2);
						strtblCredValues.Clear();
						strtblCredValues.Append(str);

						string strPurDate = "convert(datetime,'" + cld_Tab_Date_Opened.DateInput.DisplayText + "',103)";
						strtblCredValues.Append(strPurDate);
						strtblCredValues.Append(",N'");
					}

                    /*Matter Credential Description*/
                    if (!string.IsNullOrEmpty(txt_Tab_Project_Description.Text.Trim()))
                    {
                        strtblCredFields.Append(lbl_Tab_Project_Description.ID.Substring(8, lbl_Tab_Project_Description.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(txt_Tab_Project_Description.Text.Trim());
						strtblCredValues.Append("',N'");
                    }

                    /*Matter Confidential */
                    if (rdo_Tab_Client_Matter_Confidential.SelectedIndex != -1)
                    {
                        strtblCredFields.Append(lbl_Tab_Client_Matter_Confidential.ID.Substring(8, lbl_Tab_Client_Matter_Confidential.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(rdo_Tab_Client_Matter_Confidential.SelectedItem.Value);
						strtblCredValues.Append("',N'");

						if (rdo_Tab_Client_Matter_Confidential.SelectedItem.Text.Trim().ToUpper() == "YES")
						{
							obj.LogWriter("rdo_Tab_MatterConfidential_Completion Starts");
							if (rdo_Tab_MatterConfidential_Completion.SelectedItem != null)
							{
								if (rdo_Tab_MatterConfidential_Completion.SelectedItem.Value != "-1")
								{
									strtblCredFields.Append(lbl_Tab_MatterConfidential_Completion.ID.Substring(8, lbl_Tab_MatterConfidential_Completion.ID.Length - 8));
									strtblCredFields.Append(",");

									strtblCredValues.Append(rdo_Tab_MatterConfidential_Completion.SelectedItem.Value);
									strtblCredValues.Append("',N'");
								}
								else
								{
									strtblCredFields.Append(lbl_Tab_MatterConfidential_Completion.ID.Substring(8, lbl_Tab_MatterConfidential_Completion.ID.Length - 8));
									strtblCredFields.Append(",");

									strtblCredValues.Append(string.Empty);
									strtblCredValues.Append("',N'");
								}
							}
						}
					}
					
                    /* Client Matter Confidential On Completion */
                    /*Estimated End date*/
                    /*strtblCredFields.Append(lbl_Tab_Expected_Date_Of_Completion.ID.Substring(8, lbl_Tab_Expected_Date_Of_Completion.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(txt_Tab_Expected_Date_Of_Completion.Text.Trim());
                    strtblCredValues.Append("','");*/

                    /*applicable Law*/
                    if (cbo_Tab_Country_Law.SelectedItem.Value != "-1")
                    {
                        strtblCredFields.Append(lbl_Tab_Country_Law.ID.Substring(8, lbl_Tab_Country_Law.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Country_Law.SelectedItem.Value.Trim());
						strtblCredValues.Append("',N'");

                        if (cbo_Tab_Country_Law.SelectedItem.Text.Trim().ToUpper() == "OTHER")
                        {
                            if (!string.IsNullOrEmpty(txt_Tab_Country_Law_Other.Text.Trim()))
                            {

                                strtblCredFields.Append(lbl_Tab_Country_Law_Other.ID.Substring(8, lbl_Tab_Country_Law_Other.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(txt_Tab_Country_Law_Other.Text.Trim());
								strtblCredValues.Append("',N'");
                            }
                            else
                            {
                                strtblCredFields.Append(lbl_Tab_Country_Law_Other.ID.Substring(8, lbl_Tab_Country_Law_Other.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(string.Empty);
								strtblCredValues.Append("',N'");
                            }
                        }
                    }

                    /*Country Where Opened*/
                    /*Contentious/Non Contentious*/
                    if (cbo_Tab_Contentious_IRG.SelectedItem.Value != "-1")
                    {
                        strtblCredFields.Append(lbl_Tab_Contentious_IRG.ID.Substring(8, lbl_Tab_Contentious_IRG.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Contentious_IRG.SelectedItem.Value.Trim());
						strtblCredValues.Append("',N'");

                        if (cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "CONTENTIOUS")
                        {
                            /* Dispute Resolution */
                            strtblCredFields.Append(lbl_Tab_Dispute_Resolution.ID.Substring(8, lbl_Tab_Dispute_Resolution.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(cbo_Tab_Dispute_Resolution.SelectedItem.Value.Trim());
							strtblCredValues.Append("',N'");

                            if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "ARBITRATION")
                            {
                                if (cbo_Tab_ArbitrationCity.SelectedItem.Value != "-1")
                                {
                                    /*City of Arbitration*/
                                    strtblCredFields.Append(lbl_Tab_ArbitrationCity.ID.Substring(8, lbl_Tab_ArbitrationCity.ID.Length - 8));
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(cbo_Tab_ArbitrationCity.SelectedItem.Value.Trim());
									strtblCredValues.Append("',N'");
                                }

                                if (cbo_Tab_ArbitrationCity.SelectedItem.Text.Trim().ToUpper() == "OTHER")
                                {
                                    strtblCredFields.Append(lbl_Tab_ArbitrationCity_Other.ID.Substring(8, lbl_Tab_ArbitrationCity_Other.ID.Length - 8));
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(txt_Tab_ArbitrationCity_Other.Text.Trim());
									strtblCredValues.Append("',N'");
                                }

                                if (cbo_Tab_Arbitral_Rules.SelectedItem.Value != "-1")
                                {
                                    /*Arbitral Rules*/
                                    strtblCredFields.Append(lbl_Tab_Arbitral_Rules.ID.Substring(8, lbl_Tab_Arbitral_Rules.ID.Length - 8));
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(cbo_Tab_Arbitral_Rules.SelectedItem.Value.Trim());
									strtblCredValues.Append("',N'");
                                }


                            }
                            else
                            {
                                strtblCredFields.Append(lbl_Tab_ArbitrationCity.ID.Substring(8, lbl_Tab_ArbitrationCity.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(string.Empty);
								strtblCredValues.Append("',N'");

                                strtblCredFields.Append(lbl_Tab_ArbitrationCity_Other.ID.Substring(8, lbl_Tab_ArbitrationCity_Other.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(string.Empty);
								strtblCredValues.Append("',N'");

                                strtblCredFields.Append(lbl_Tab_Arbitral_Rules.ID.Substring(8, lbl_Tab_Arbitral_Rules.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(string.Empty);
								strtblCredValues.Append("',N'");
                            }

                        }
                        else
                        {

                            strtblCredFields.Append(lbl_Tab_Dispute_Resolution.ID.Substring(8, lbl_Tab_Dispute_Resolution.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");

                            strtblCredFields.Append(lbl_Tab_ArbitrationCity.ID.Substring(8, lbl_Tab_ArbitrationCity.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");

                            strtblCredFields.Append(lbl_Tab_ArbitrationCity_Other.ID.Substring(8, lbl_Tab_ArbitrationCity_Other.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");

                            strtblCredFields.Append(lbl_Tab_Arbitral_Rules.ID.Substring(8, lbl_Tab_Arbitral_Rules.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");

                        }
                    }

                    /*Value Of Deal */
                    if (!string.IsNullOrEmpty(txt_Tab_ValueOfDeal_Core.Text.Trim()))
                    {
                        strtblCredFields.Append(lbl_Tab_ValueOfDeal_Core.ID.Substring(8, lbl_Tab_ValueOfDeal_Core.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(txt_Tab_ValueOfDeal_Core.Text.Trim());
						strtblCredValues.Append("',N'");
                    }

                    /*Currency Of Deal*/
                    if (cbo_Tab_Currency_Of_Deal.SelectedItem.Value != "-1")
                    {
                        strtblCredFields.Append(lbl_Tab_Currency_Of_Deal.ID.Substring(8, lbl_Tab_Currency_Of_Deal.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Currency_Of_Deal.SelectedItem.Value.Trim());
						strtblCredValues.Append("',N'");
                    }

                    /*Value Confidential*/
                    if (rdo_Tab_Value_Confidential.SelectedIndex != -1)
                    {
                        strtblCredFields.Append(lbl_Tab_Value_Confidential.ID.Substring(8, lbl_Tab_Value_Confidential.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(rdo_Tab_Value_Confidential.SelectedItem.Value);
						strtblCredValues.Append("',N'");

                        /* confidential on completion */
						if (rdo_Tab_ValueConfidential_Completion.SelectedItem != null)
                        {
                            if (rdo_Tab_ValueConfidential_Completion.SelectedIndex != -1)
                            {
                                strtblCredFields.Append(lbl_Tab_ValueConfidential_Completion.ID.Substring(8, lbl_Tab_ValueConfidential_Completion.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(rdo_Tab_ValueConfidential_Completion.SelectedItem.Value);
								strtblCredValues.Append("',N'");
							}
							else
							{
								strtblCredFields.Append(lbl_Tab_ValueConfidential_Completion.ID.Substring(8, lbl_Tab_ValueConfidential_Completion.ID.Length - 8));
								strtblCredFields.Append(",");
								strtblCredValues.Append(string.Empty);
								strtblCredValues.Append("',N'");
							}
						}
                        else
                        {
                            strtblCredFields.Append(lbl_Tab_ValueConfidential_Completion.ID.Substring(8, lbl_Tab_ValueConfidential_Completion.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");
                        }
                    }

                    string strLead = hid_Tab_Lead_Partner_Text.Value;
                    if (strLead.Contains("CMS PARTNER"))
                    {
                        /*Name Of CMS PArtner*/
                        if (!string.IsNullOrEmpty(txt_Tab_CMSPartnerName.Text.Trim()))
                        {
                            strtblCredFields.Append(lbl_Tab_CMSPartnerName.ID.Substring(8, lbl_Tab_CMSPartnerName.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(txt_Tab_CMSPartnerName.Text.Trim());
							strtblCredValues.Append("',N'");
                        }

                        if (!string.IsNullOrEmpty(txt_Tab_SourceOfCredential_Other.Text.Trim()))
                        {
                            strtblCredFields.Append(lbl_Tab_SourceOfCredential_Other.ID.Substring(8, lbl_Tab_SourceOfCredential_Other.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(txt_Tab_SourceOfCredential_Other.Text.Trim());
							strtblCredValues.Append("',N'");
                        }
                    }
                    else
                    {
                        strtblCredFields.Append(lbl_Tab_CMSPartnerName.ID.Substring(8, lbl_Tab_CMSPartnerName.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");

                        strtblCredFields.Append(lbl_Tab_SourceOfCredential_Other.ID.Substring(8, lbl_Tab_SourceOfCredential_Other.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");
                    }

                    /* Credential Status */
                    if (cbo_Tab_Credential_Status.SelectedItem.Value != "-1")
                    {
                        strtblCredFields.Append(lbl_Tab_Credential_Status.ID.Substring(8, lbl_Tab_Credential_Status.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Credential_Status.SelectedItem.Value.Trim());
						strtblCredValues.Append("',N'");
                    }

                    /*Credntial Version*/
                    if (cbo_Tab_Credential_Version.SelectedItem.Value != "-1")
                    {
                        strtblCredFields.Append(lbl_Tab_Credential_Version.ID.Substring(8, lbl_Tab_Credential_Version.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Credential_Version.SelectedItem.Value.Trim());
						strtblCredValues.Append("',N'");
                    }

                    /*Credntial Version other*/
                    if (!string.IsNullOrEmpty(txt_Tab_Credential_Version_Other.Text.Trim()))
                    {
                        strtblCredFields.Append(lbl_Tab_Credential_Version_Other.ID.Substring(8, lbl_Tab_Credential_Version_Other.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(txt_Tab_Credential_Version_Other.Text.Trim());
						strtblCredValues.Append("',N'");
                    }
                    else
                    {
                        strtblCredFields.Append(lbl_Tab_Credential_Version_Other.ID.Substring(8, lbl_Tab_Credential_Version_Other.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");
                    }

                    /* Credential Type */
                    if (cbo_Tab_Credential_Type.SelectedItem.Value != "-1")
                    {
                        strtblCredFields.Append(lbl_Tab_Credential_Type.ID.Substring(8, lbl_Tab_Credential_Type.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Credential_Type.SelectedItem.Value.Trim());
						strtblCredValues.Append("',N'");
                    }

                    /* Priority */
                    if (cbo_Tab_Priority.SelectedItem.Value != "-1")
                    {
                        strtblCredFields.Append(lbl_Tab_Priority.ID.Substring(8, lbl_Tab_Priority.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Priority.SelectedItem.Value.Trim());
						strtblCredValues.Append("',N'");
                    }

                    /*Keyword Search Appender*/
                    obj.LogWriter("EntryScreen : Keyword Search Appender Starts", hidName.Value);
                    StringBuilder strAppender = new StringBuilder();

                    /*Client name*/
                    if (!string.IsNullOrEmpty(txt_Tab_Client.Text.Trim()))
                    {
                        strAppender.Append(txt_Tab_Client.Text.Trim());
						strAppender.Append("~");
                    }

                    /*Matter/credential description*/
                    if (!string.IsNullOrEmpty(txt_Tab_Project_Description.Text.Trim()))
                    {
                        strAppender.Append(txt_Tab_Project_Description.Text.Trim());
						strAppender.Append("~");
                    }

                    /*Matter/credential other information*/
                    if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text.Trim()))
					{
						strAppender.Append(txt_Tab_Significant_Features.Text.Trim());
						strAppender.Append("~");
					}

                    /*Keyword(s)/themes*/
                    if (!string.IsNullOrEmpty(txt_Tab_Keyword.Text.Trim()))
                    {
                        strAppender.Append(txt_Tab_Keyword.Text.Trim());
						strAppender.Append("~");
                    }

                    /*Sector and sub sectors (client and matter)*/
                    if (!string.IsNullOrEmpty(txt_Tab_ClientIndustrySector.Text.Trim()))
                    {
                        strAppender.Append(txt_Tab_ClientIndustrySector.Text.Trim());
						strAppender.Append("~");
                        //strAppender.Append(txt_Tab_ClientIndustrySector.Text.Trim().Replace(",","1@2!"));
						//strAppender.Append("~");
                    }

                    if (!string.IsNullOrEmpty(txt_Tab_TransactionIndustrySector.Text.Trim()))
                    {
                        strAppender.Append(txt_Tab_TransactionIndustrySector.Text.Trim());
						strAppender.Append("~");
                        //strAppender.Append(txt_Tab_ClientIndustrySector.Text.Trim().Replace(",","1@2!"));
						//strAppender.Append("~");
                    }

                    string strAppenderFinal = strAppender.ToString().Substring(0, strAppender.Length - 1);

                    if (!string.IsNullOrEmpty(strAppenderFinal))
                    {
                        strtblCredFields.Append("KeyWordSearch");
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(strAppenderFinal.Trim());
                        strtblCredValues.Append("','");
                    }

                    obj.LogWriter("EntryScreen : Keyword Search Appender Ends", hidName.Value);

                    /* Insert into Keyword Search Table for Optimised Search Starts */
                    if (!string.IsNullOrEmpty(txt_Tab_Client.Text.Trim()))
                    {
                        InsertKeyWordSearchTable(strcon, txt_Tab_Client.Text.Trim());
                    }

                    /*if (!string.IsNullOrEmpty(txt_Tab_Project_Description.Text.Trim()))
                    {
                        InsertKeyWordSearchTable(strcon, txt_Tab_Project_Description.Text.Trim());
                    }

                    if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text.Trim()))
                    {
                        InsertKeyWordSearchTable(strcon, txt_Tab_Significant_Features.Text.Trim());
                    }*/

                    if (!string.IsNullOrEmpty(txt_Tab_Keyword.Text.Trim()))
                    {
                        InsertKeyWordSearchTable(strcon, txt_Tab_Keyword.Text.Trim());
                    }

                    /* Insert into Keyword Search Table for Optimised Search Ends */
					
                    /*optional fields*/

                    /*Other Matter Description*/
                    if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text.Trim()))
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_Significant_Features.ID.Substring(8, lbl_Tab_Significant_Features.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(txt_Tab_Significant_Features.Text.Trim());
                        strOptionaltblCredValues.Append("','");
                    }
                    else
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_Significant_Features.ID.Substring(8, lbl_Tab_Significant_Features.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(string.Empty);
						strOptionaltblCredValues.Append("',N'");
                    }

                    /*Keyword*/
                    if (!string.IsNullOrEmpty(txt_Tab_Keyword.Text.Trim()))
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_Keyword.ID.Substring(8, lbl_Tab_Keyword.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(txt_Tab_Keyword.Text.Trim());
						strOptionaltblCredValues.Append("',N'");
                    }
                    else
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_Keyword.ID.Substring(8, lbl_Tab_Keyword.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(string.Empty);
						strOptionaltblCredValues.Append("',N'");
                    }

                    /*Actual Date if checked saves in ActualDate_Ongoing else Date_Completed */
                    if (chk_Tab_ActualDate_Ongoing.Checked == true)
                    {
                        strOptionaltblCredFields.Append(chk_Tab_ActualDate_Ongoing.ID.Substring(8, chk_Tab_ActualDate_Ongoing.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(chk_Tab_ActualDate_Ongoing.Text.Trim());
						strOptionaltblCredValues.Append("',N'");
					}
					else
					{
						if (chk_Tab_ActualDate_Ongoing_1.Checked)
						{
							strOptionaltblCredFields.Append(chk_Tab_ActualDate_Ongoing.ID.Substring(8, chk_Tab_ActualDate_Ongoing.ID.Length - 8));
							strOptionaltblCredFields.Append(",");
							strOptionaltblCredValues.Append(chk_Tab_ActualDate_Ongoing_1.Text.Trim());
							strOptionaltblCredValues.Append("',N'");
						}
						else
						{
							strOptionaltblCredFields.Append(chk_Tab_ActualDate_Ongoing.ID.Substring(8, chk_Tab_ActualDate_Ongoing.ID.Length - 8));
							strOptionaltblCredFields.Append(",");
							strOptionaltblCredValues.Append(string.Empty);
							strOptionaltblCredValues.Append("',N'");

							if (!chk_Tab_ActualDate_Ongoing.Checked && !chk_Tab_ActualDate_Ongoing_1.Checked)
							{
								if (cld_Tab_Date_Completed.SelectedDate.HasValue && cld_Tab_Date_Completed.SelectedDate.HasValue)
								{
									strOptionaltblCredFields.Append(lbl_Tab_Date_Completed.ID.Substring(8, lbl_Tab_Date_Completed.ID.Length - 8));
									strOptionaltblCredFields.Append(",");

									string str = strOptionaltblCredValues.ToString().Substring(0, strOptionaltblCredValues.Length - 2);
									strOptionaltblCredValues.Clear();
									strOptionaltblCredValues.Append(str);

									string strPurDate = "convert(datetime,'" + cld_Tab_Date_Completed.DateInput.DisplayText + "',103)";
									strOptionaltblCredValues.Append(strPurDate);
									strOptionaltblCredValues.Append(",N'");
								}
                                /*strOptionaltblCredValues.Append(txt_Tab_Date_Completed.Text.Trim());
                                strOptionaltblCredValues.Append("','");*/
                            }
                        }
                    }

                    /* Project Name */
                    if (!string.IsNullOrEmpty(txt_Tab_ProjectName_Core.Text.Trim()))
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_ProjectName_Core.ID.Substring(8, lbl_Tab_ProjectName_Core.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(txt_Tab_ProjectName_Core.Text.Trim());
						strOptionaltblCredValues.Append("',N'");
                    }
                    else
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_ProjectName_Core.ID.Substring(8, lbl_Tab_ProjectName_Core.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(string.Empty);
						strOptionaltblCredValues.Append("',N'");
                    }
                    if (cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "CONTENTIOUS")
                    {
                        /*language of disptute*/
                        if (!string.IsNullOrEmpty(txt_Tab_Language_Of_Dispute.Text.Trim()))
                        {
                            string strLanguageOfDispute = hid_Tab_Language_Of_Dispute_Other.Value;
                            if (strLanguageOfDispute.Trim().ToUpper().Contains("OTHER"))
                            {
                                strOptionaltblCredFields.Append(lbl_Tab_Language_Of_Dispute_Other.ID.Substring(8, lbl_Tab_Language_Of_Dispute_Other.ID.Length - 8));
                                strOptionaltblCredFields.Append(",");

                                strOptionaltblCredValues.Append(txt_Tab_Language_Of_Dispute_Other.Text.Trim());
								strOptionaltblCredValues.Append("',N'");
                            }
                            else
                            {
                                strOptionaltblCredFields.Append(lbl_Tab_Language_Of_Dispute_Other.ID.Substring(8, lbl_Tab_Language_Of_Dispute_Other.ID.Length - 8));
                                strOptionaltblCredFields.Append(",");

                                strOptionaltblCredValues.Append(string.Empty);
								strOptionaltblCredValues.Append("',N'");
                            }
                        }

                        /*Investment treaty*/
                        if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "ARBITRATION")
                        {
                            if (rdo_Tab_InvestmentTreaty.SelectedIndex != -1)
                            {
                                strOptionaltblCredFields.Append(lbl_Tab_InvestmentTreaty.ID.Substring(8, lbl_Tab_InvestmentTreaty.ID.Length - 8));
                                strOptionaltblCredFields.Append(",");

                                strOptionaltblCredValues.Append(rdo_Tab_InvestmentTreaty.SelectedItem.Value.Trim());
								strOptionaltblCredValues.Append("',N'");
                            }
                            else
                            {
                                strOptionaltblCredFields.Append(lbl_Tab_InvestmentTreaty.ID.Substring(8, lbl_Tab_InvestmentTreaty.ID.Length - 8));
                                strOptionaltblCredFields.Append(",");

                                strOptionaltblCredValues.Append(string.Empty);
								strOptionaltblCredValues.Append("',N'");

                            }
                        }

                        obj.LogWriter("cbo_Tab_Investigation_Type Starts");
                        /*Investigation*/
                        if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "INVESTIGATION")
                        {
                            if (cbo_Tab_Investigation_Type.SelectedItem.Value != "-1")
                            {
                                strOptionaltblCredFields.Append(lbl_Tab_Investigation_Type.ID.Substring(8, lbl_Tab_Investigation_Type.ID.Length - 8));
                                strOptionaltblCredFields.Append(",");

                                strOptionaltblCredValues.Append(cbo_Tab_Investigation_Type.SelectedItem.Value.Trim());
								strOptionaltblCredValues.Append("',N'");
                            }
                            else
                            {
                                strOptionaltblCredFields.Append(lbl_Tab_Investigation_Type.ID.Substring(8, lbl_Tab_Investigation_Type.ID.Length - 8));
                                strOptionaltblCredFields.Append(",");

                                strOptionaltblCredValues.Append(string.Empty);
								strOptionaltblCredValues.Append("',N'");
                            }
                        }
                    }

                    /*Lead CMS Firms*/
                    if (cbo_Tab_Lead_CMS_Firm.SelectedItem.Value != "-1")
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_Lead_CMS_Firm.ID.Substring(8, lbl_Tab_Lead_CMS_Firm.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(cbo_Tab_Lead_CMS_Firm.SelectedItem.Value.Trim());
						strOptionaltblCredValues.Append("',N'");
                    }
                    else
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_Lead_CMS_Firm.ID.Substring(8, lbl_Tab_Lead_CMS_Firm.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(string.Empty);
						strOptionaltblCredValues.Append("',N'");
                    }

                    /*Pro Bono*/
                    if (rdo_Tab_ProBono.SelectedIndex != -1)
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_ProBono.ID.Substring(8, lbl_Tab_ProBono.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(rdo_Tab_ProBono.SelectedItem.Value.Trim());
						strOptionaltblCredValues.Append("',N'");
                    }

                    /*Bible Reference*/
                    if (!string.IsNullOrEmpty(txt_Tab_Bible_Reference.Text.Trim()))
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_Bible_Reference.ID.Substring(8, txt_Tab_Bible_Reference.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(txt_Tab_Bible_Reference.Text.Trim());
						strOptionaltblCredValues.Append("',N'");
                    }
                    else
                    {
                        strOptionaltblCredFields.Append(lbl_Tab_Bible_Reference.ID.Substring(8, txt_Tab_Bible_Reference.ID.Length - 8));
                        strOptionaltblCredFields.Append(",");

                        strOptionaltblCredValues.Append(string.Empty);
						strOptionaltblCredValues.Append("',N'");
                    }

                    /*BAIF*/
                    obj.LogWriter("BAIF Values Fetched from Session Starts", hidName.Value);

                    if (Session["sessBAIFSS"] != null)
                    {
                        if (Session["sessBAIFSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessBAIFSS"].ToString().Split('|');
                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;
                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "ClientTypeIdBAIF":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strBAIFtblCredFields.Append(",");

                                                    strBAIFtblCredValues.Append(strvals[i].Split('~')[1]);
														strBAIFtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strBAIFtblCredFields.Append(",");

                                                    strBAIFtblCredValues.Append(string.Empty);
														strBAIFtblCredValues.Append("',N'");
                                                }
                                                break;
                                            case "LeadBanks":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strBAIFtblCredFields.Append(",");

                                                    strBAIFtblCredValues.Append(strvals[i].Split('~')[1]);
													strBAIFtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strBAIFtblCredFields.Append(",");

                                                    strBAIFtblCredValues.Append(string.Empty);
													strBAIFtblCredValues.Append("',N'");
                                                }
                                                break;
                                            /* case "Transaction_Value":
                                                 // cbo_BAI_ClientTypeIdBAIF.SelectedItem.Text = strvals[i].Split('~')[1];
                                                 if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                 {
                                                     strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                     strBAIFtblCredFields.Append(",");

                                                     strBAIFtblCredValues.Append(strvals[i].Split('~')[1]);
                                                     strBAIFtblCredValues.Append("','");
                                                 }
                                                 else
                                                 {
                                                     strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                     strBAIFtblCredFields.Append(",");

                                                     strBAIFtblCredValues.Append(string.Empty);
                                                     strBAIFtblCredValues.Append("','");

                                                 }
                                                 break;*/
                                            case "WorkTypeMS":
                                                // txt_BAI_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    obj.LogWriter("BAIF Values Fetched from Session Stops");
                    obj.LogWriter("Corporate Values Fetched from Session Starts");
					
                    if (Session["sessCORPSS"] != null)
                    {
                        if (Session["sessCORPSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessCORPSS"].ToString().Split('|');
                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "WorkTypeMS":
                                                //txt_Cor_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "SubWorkTypeMS":
                                                //txt_Cor_SubWork_Type.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "ActingForMS":
                                                // txt_Cor_Acting_For.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "Country_BuyerMS":
                                                //objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Country_Buyer);
                                                // txt_Cor_Country_Buyer.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "Country_SellerMS":
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Country_Seller);
                                                // txt_Cor_Country_Seller.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "Country_TargetMS":
                                                //objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Country_Target);
                                                // txt_Cor_Country_Target.Text = strvals[i].Split('~')[1].ToString();
                                                break;
											case "ValueRangeEuro":
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_ValueRangeEuro);
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(strvals[i].Split('~')[1]);
													strCorptblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(string.Empty);
													strCorptblCredValues.Append("',N'");
                                                }
                                                break;
											case "Published_Reference":
                                                //txt_Cor_Published_Reference.Text = strvals[i].Split('~')[1].ToString();
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(strvals[i].Split('~')[1]);
													strCorptblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(string.Empty);
													strCorptblCredValues.Append("',N'");
                                                }
                                                break;
											case "MAStudy":
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_MAStudy);
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(strvals[i].Split('~')[1]);
													strCorptblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(string.Empty);
													strCorptblCredValues.Append("',N'");
                                                }
                                                break;
											case "PEClients":
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_PEClients);
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(strvals[i].Split('~')[1]);
													strCorptblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(string.Empty);
													strCorptblCredValues.Append("',N'");
                                                }
                                                break;
                                            case "QuarterDealAnnouncedId":
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_QuarterDealAnnouncedId);
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(strvals[i].Split('~')[1]);
													strCorptblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(string.Empty);
													strCorptblCredValues.Append("',N'");
                                                }
                                                break;
                                            case "QuarterDealCompletedId":
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_QuarterDealCompletedId);
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(strvals[i].Split('~')[1]);
													strCorptblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(string.Empty);
													strCorptblCredValues.Append("',N'");
                                                }
                                                break;
                                            case "YearDeal_Announced":
                                                // txt_Cor_YearDeal_Announced.Text = strvals[i].Split('~')[1].ToString();
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(strvals[i].Split('~')[1]);
													strCorptblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(string.Empty);
													strCorptblCredValues.Append("',N'");
                                                }
                                                break;
                                            case "YearDealCompletedId":
                                                //  objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_YearDealCompletedId);
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(strvals[i].Split('~')[1]);
													strCorptblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorptblCredFields.Append(",");

                                                    strCorptblCredValues.Append(string.Empty);
													strCorptblCredValues.Append("',N'");
                                                }
                                                break;
											case "DealAnnouncedId":
												string strCorp = strCorptblCredValues.ToString().Substring(0, strCorptblCredValues.Length - 2);
												strCorptblCredValues.Clear();
												strCorptblCredValues.Append(strCorp);
												string strDealDate = "convert(datetime,'" + strvals[i].Split('~')[1].Trim() + "',103)";
												if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
												{
													strCorptblCredFields.Append(strvals[i].Split('~')[0]);
													strCorptblCredFields.Append(",");
													strCorptblCredValues.Append(strDealDate);
													strCorptblCredValues.Append(",N'");
												}
												break;
                                            /*  case "CorporateMS":
                                                  txt_Cor_Corporate_Know_How.Text = strvals[i].Split('~')[1].ToString();
                                                  break;
                                              case "Bible_Reference":
                                                  txt_Cor_Bible_Reference.Text = strvals[i].Split('~')[1].ToString();
                                                  break;*/
                                        }
                                    }
                                }
                            }
                        }
                    }
					
					obj.LogWriter("Corporate Values Fetched from Session Starts", null);
					obj.LogWriter("CRD Values Fetched from Session Starts", null);
					
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
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_CRD_ClientTypeIdCommercial);
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCRDtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCRDtblCredFields.Append(",");

                                                    strCRDtblCredValues.Append(strvals[i].Split('~')[1]);
													strCRDtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCRDtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCRDtblCredFields.Append(",");

                                                    strCRDtblCredValues.Append(string.Empty);
													strCRDtblCredValues.Append("',N'");
                                                }
                                                break;
                                            case "WorkTypeMS":
                                                // txt_CRD_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "SubWorkTypeMS":
                                                // txt_CRD_SubWork_Type.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("CRD Values Fetched from Session Ends");
                    obj.LogWriter("EPC Construction Values Fetched from Session Starts");
					
                    if (Session["sessEPCSS"] != null)
                    {
                        if (Session["sessEPCSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessEPCSS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "WorkTypeMS":
                                                // txt_EPC_Nature_Of_Work.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "ClientScopeId":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
													strEPCtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(string.Empty);
													strEPCtblCredValues.Append("',N'");

                                                }
                                                break;
                                            case "TypeOfContractMS":
                                                // txt_EPC_Type_Of_Contract.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "Type_Of_Contract_Other":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
																	strEPCtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(string.Empty);
													strEPCtblCredValues.Append("',N'");

                                                }
                                                break;
                                            case "SubjectMatterId":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
																strEPCtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(string.Empty);
													strEPCtblCredValues.Append("',N'");

                                                }
                                                break;
                                            case "SubjectMatterOther":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
													strEPCtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(string.Empty);
													strEPCtblCredValues.Append("',N'");
                                                }
                                                break;
                                            case "ClientTypeIdEPC":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
													strEPCtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(string.Empty);
													strEPCtblCredValues.Append("',N'");
                                                }
                                                break;
                                            case "ClientTypeOther":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
													strEPCtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCtblCredFields.Append(",");

                                                    strEPCtblCredValues.Append(string.Empty);
													strEPCtblCredValues.Append("',N'");
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("EPC Construction Values Fetched from Session Ends");
                    obj.LogWriter("EPC Energy Values Fetched from Session Starts");
					
                    if (Session["sessEPCESS"] != null)
                    {
                        if (Session["sessEPCESS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessEPCESS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;
									
                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "ProjectSectorMS":
                                                // txt_ENE_EPC_Project_Sector.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "TransactionTypeMS":
                                                //txt_ENE_Transaction_Type.Text = strvals[i].Split('~')[1].ToString();
                                                break;
                                            case "ContractTypeId":
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_ENE_ContractTypeId);
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strEPCEnetblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCEnetblCredFields.Append(",");

                                                    strEPCEnetblCredValues.Append(strvals[i].Split('~')[1]);
                                                    strEPCEnetblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strEPCEnetblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strEPCEnetblCredFields.Append(",");

                                                    strEPCEnetblCredValues.Append(string.Empty);
                                                    strEPCEnetblCredValues.Append("',N'");
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("EPC Energy Values Fetched from Session Ends");
                    obj.LogWriter("HC Values Fetched from Session Starts");
					
                    if (Session["sessHCSS"] != null)
                    {
                        if (Session["sessHCSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessHCSS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {

                                            case "WorkTypeIdHC":
                                                /* if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                 {
                                                     strHCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                     strHCtblCredFields.Append(",");

                                                     strHCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                     strHCtblCredValues.Append("','");
                                                 }
                                                 else
                                                 {
                                                     strHCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                     strHCtblCredFields.Append(",");

                                                     strHCtblCredValues.Append(string.Empty);
                                                     strHCtblCredValues.Append("','");
                                                 }
                                                 //objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_HCC_WorkTypeIdHC);*/
                                                break;
                                            case "PensionSchemeHC":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strHCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strHCtblCredFields.Append(",");

                                                    strHCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                    strHCtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strHCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strHCtblCredFields.Append(",");

                                                    strHCtblCredValues.Append(string.Empty);
                                                    strHCtblCredValues.Append("',N'");
                                                }
                                                // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_HCC_PensionSchemeHC);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("EPC Energy Values Fetched from Session Ends");
                    obj.LogWriter("IPF Values Fetched from Session Starts");
					
                    if (Session["sessIPFSS"] != null)
                    {
                        if (Session["sessIPFSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessIPFSS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "ClientTypeIdIPF":
                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strIPFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strIPFtblCredFields.Append(",");

                                                    strIPFtblCredValues.Append(strvals[i].Split('~')[1]);
                                                    strIPFtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strIPFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strIPFtblCredFields.Append(",");

                                                    strIPFtblCredValues.Append(string.Empty);
                                                    strIPFtblCredValues.Append("',N'");
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("IPF Values Fetched from Session Ends");
                    obj.LogWriter("CorpTax Values Fetched from Session Start");
					
                    if (Session["sessCorpTaxSS"] != null)
                    {
                        if (Session["sessCorpTaxSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessCorpTaxSS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "WorkType_CorpTax":

                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                {
                                                    strCorpTaxtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorpTaxtblCredFields.Append(",");

                                                    strCorpTaxtblCredValues.Append(strvals[i].Split('~')[1]);
                                                    strCorpTaxtblCredValues.Append("',N'");
                                                }
                                                else
                                                {
                                                    strCorpTaxtblCredFields.Append(strvals[i].Split('~')[0]);
                                                    strCorpTaxtblCredFields.Append(",");

                                                    strCorpTaxtblCredValues.Append(string.Empty);
                                                    strCorpTaxtblCredValues.Append("',N'");
                                                }
                                                break;

                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("CorpTax Values Fetched from Session Ends");

                    if (strtblCredFields != null && strtblCredValues != null)
                    {
                        strquerycols.Append(strtblCredFields);
                        strqueryvals.Append(strtblCredValues);
                    }
                    if (strOptionaltblCredFields != null && strOptionaltblCredValues != null)
                    {
                        strquerycols.Append(strOptionaltblCredFields);
                        strqueryvals.Append(strOptionaltblCredValues);
                    }
                    if (Session["sessBAIFSS"] != null)
                    {
                        strquerycols.Append(strBAIFtblCredFields);
                        strqueryvals.Append(strBAIFtblCredValues);
                    }
                    if (Session["sessCORPSS"] != null)
                    {
                        strquerycols.Append(strCorptblCredFields);
                        strqueryvals.Append(strCorptblCredValues);
                    }
                    if (Session["sessCRDSS"] != null)
                    {
                        strquerycols.Append(strCRDtblCredFields);
                        strqueryvals.Append(strCRDtblCredValues);
                    }
                    if (Session["sessEPCSS"] != null)
                    {
                        strquerycols.Append(strEPCtblCredFields);
                        strqueryvals.Append(strEPCtblCredValues);
                    }
                    if (Session["sessEPCESS"] != null)
                    {
                        strquerycols.Append(strEPCEnetblCredFields);
                        strqueryvals.Append(strEPCEnetblCredValues);
                    }
                    if (Session["sessHCSS"] != null)
                    {
                        strquerycols.Append(strHCtblCredFields);
                        strqueryvals.Append(strHCtblCredValues);
                    }
                    if (Session["sessIPFSS"] != null)
                    {
                        strquerycols.Append(strIPFtblCredFields);
                        strqueryvals.Append(strIPFtblCredValues);
                    }
                    if (Session["sessRESS"] != null)
                    {
                        strquerycols.Append(strREtblCredFields);
                        strqueryvals.Append(strREtblCredValues);
                    }
                    if (Session["sessCorpTaxSS"] != null)
                    {
                        strquerycols.Append(strCorpTaxtblCredFields);
                        strqueryvals.Append(strCorpTaxtblCredValues);
                    }

                    //strquerycols.ToString().Substring(0, strquerycols.ToString().Length - 1);
                    //strqueryvals.ToString().Substring(0, strqueryvals.ToString().Length - 2);
                    obj.LogWriter(" EntryScreen : btnAddBottom_Click Save in tblCredential Starts");
                    obj.LogWriter(strqueryvals.ToString());
                    obj.LogWriter(strquerycols.ToString());
                    int iCredentialID = SaveCredentials(strqueryvals, strquerycols);

                    obj.LogWriter(" EntryScreen : btnAddBottom_Click Save in tblCredential Ends");

                    if (!string.IsNullOrEmpty(hidCredentialID.Value.Trim()))
                    {
                        obj.LogWriter(" EntryScreen : btnAddBottom_Click Save in tblCredential Ends");

						SqlConnection con2 = new SqlConnection(strcon);
						con2.Open();

						string strQuery = "select CredentialRelationID from tblCredentialVersionRelation where credentialid='" + hidCredentialID.Value.Trim() + "'";
						SqlDataAdapter adp = new SqlDataAdapter(strQuery, con2);
						DataSet ds = new DataSet();
						adp.Fill(ds);
						string strCredentialRelationID = string.Empty;
						
						if (ds.Tables[0].Rows.Count > 0)
						{
							strCredentialRelationID = ds.Tables[0].Rows[0][0].ToString();
						}
						
						adp.Dispose();
						ds.Dispose();

						string strQuery2 = "select credentialid from tblCredentialVersionRelation where credentialversion='Master' and CredentialRelationID='" + strCredentialRelationID + "'";
						SqlDataAdapter adp2 = new SqlDataAdapter(strQuery2, con2);
						
						DataSet ds2 = new DataSet();
						adp2.Fill(ds2);
						string strCredentialID = string.Empty;
						
						if (ds2.Tables[0].Rows.Count > 0)
						{
							strCredentialID = ds2.Tables[0].Rows[0][0].ToString();
						}
						
						adp2.Dispose();
						ds2.Dispose();
						
						string strQuery3 = "insert into tblCredentialVersionRelation(CredentialRelationID,CredentialID,CredentialVersion,MatterNo) values('" + strCredentialRelationID + "','" + iCredentialID + "','" + cbo_Tab_Credential_Version.SelectedItem.Text.Trim() + "','" + txt_Tab_Matter_No.Text.Trim() + "' )";
						SqlCommand cmd2 = new SqlCommand(strQuery3, con2);
						cmd2.ExecuteNonQuery();
						cmd2.Dispose();
						
						if (cbo_Tab_Credential_Version.SelectedItem.Text.Trim().ToUpper() == "MASTER")
						{
							string strQuery4 = "update tblCredentialVersionRelation set CredentialVersion = 'Other' where credentialid='" + strCredentialID + "'";
							SqlCommand cmd3 = new SqlCommand(strQuery4, con2);
							cmd3.ExecuteNonQuery();
							cmd3.Dispose();
							string strQ = "update tblCredentialVersionRelation set CredentialMasterID = '" + iCredentialID + "' where CredentialRelationID='" + strCredentialRelationID + "'";

							SqlCommand cmdQ = new SqlCommand(strQ, con2);
							int iQ = cmdQ.ExecuteNonQuery();
							cmdQ.Dispose();

							string strQuery5 = "select CredentialID,CredentialVersion from tblCredentialVersionRelation where CredentialRelationID='" + strCredentialRelationID + "'";
							SqlDataAdapter adp3 = new SqlDataAdapter(strQuery5, con2);
							DataSet ds3 = new DataSet();
							adp3.Fill(ds3);
							string strMasterCredentialID = string.Empty;
							string strChildCredentialID = string.Empty;
							if (ds3.Tables[0].Rows.Count > 0)
							{
								for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
								{
									if (ds3.Tables[0].Rows[i][1].ToString().ToUpper() == "MASTER")
									{
										strMasterCredentialID = "'" + ds3.Tables[0].Rows[i][0].ToString() + "'";
									}
									else
									{
										if (string.IsNullOrEmpty(strChildCredentialID))
										{
											strChildCredentialID = "'" + ds3.Tables[0].Rows[i][0].ToString() + "',";
										}
										else
										{
											strChildCredentialID = strChildCredentialID + "'" + ds3.Tables[0].Rows[i][0].ToString() + "',";
										}
									}
								}
							}
							adp3.Dispose();
							ds3.Dispose();

							string strQuery6 = "update tblCredential set Credential_Version = '1' where credentialid in(" + strMasterCredentialID + ")";
							SqlCommand cmd4 = new SqlCommand(strQuery6, con2);
							cmd4.ExecuteNonQuery();
							cmd4.Dispose();

							string strQuery7 = "update tblCredential set Credential_Version = '2' where credentialid in(" + strChildCredentialID.Substring(0, strChildCredentialID.Length - 1) + ")";
							SqlCommand cmd5 = new SqlCommand(strQuery7, con2);
							cmd5.ExecuteNonQuery();
							cmd5.Dispose();
						}
						else
						{
                        	/* Master Credential Relation for Other Starts */
							string strzQuery2 = "select CredentialMasterID from tblCredentialVersionRelation where credentialversion='Master' and CredentialRelationID='" + strCredentialRelationID + "'";
							SqlDataAdapter zadp2 = new SqlDataAdapter(strzQuery2, con2);
							DataSet zds2 = new DataSet();
							zadp2.Fill(zds2);
							string strzCredentialID = string.Empty;
							if (zds2.Tables[0].Rows.Count > 0)
							{
								strzCredentialID = zds2.Tables[0].Rows[0][0].ToString();
							}
							zadp2.Dispose();
							zds2.Dispose();
							string strQ = "update tblCredentialVersionRelation set CredentialMasterID = '" + strzCredentialID + "' where CredentialRelationID='" + strCredentialRelationID + "'";

							SqlCommand cmdQ = new SqlCommand(strQ, con2);
							cmdQ.ExecuteNonQuery();
							cmdQ.Dispose();
						}
					}
					else
					{
						obj.LogWriter("Save in tblCredentialVersionRelation Starts", null);
						string strcon2 = ConfigurationManager.ConnectionStrings["con"].ToString();
						SqlConnection con2 = new SqlConnection(strcon);
						con2.Open();

						objLogger.LogWriter("Insert starts", null);

						string strID = Convert.ToString(iCredentialID) + "0000";
						string sqlquery = "insert into tblCredentialVersionRelation(CredentialRelationID,CredentialID,CredentialVersion,MatterNo,CredentialMasterID) values('" + strID + "','" + iCredentialID + "','" + cbo_Tab_Credential_Version.SelectedItem.Text.Trim() + "','" + txt_Tab_Matter_No.Text.Trim() + "','" + iCredentialID + "')";

						objLogger.LogWriter(sqlquery, null);
						SqlCommand cmd6 = new SqlCommand(sqlquery, con2);
						cmd6.ExecuteNonQuery();
						objLogger.LogWriter("Insert ends", null);
						cmd6.Dispose();
						con2.Close();
						obj.LogWriter("Save in tblCredentialVersionRelation Starts", null);
					}

                    /*sector group*/
                    obj.LogWriter("Multiselect query -save Starts");

                    if (chKBAIF.Checked == true && Session["sessBAIFSS"] != null)
                    {
                        strPrac.Append(hid_BAIF.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (chKCRD.Checked == true && Session["sessCRDSS"] != null)
                    {
                        strPrac.Append(hid_CRD.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (chKCorp.Checked == true && Session["sessCORPSS"] != null)
                    {
                        strPrac.Append(hid_Corp.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (chkEPC.Checked == true && Session["sessEPCSS"] != null)
                    {
                        strPrac.Append(hid_EPC.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (chkEPCE.Checked == true && Session["sessEPCESS"] != null)
                    {
                        strPrac.Append(hid_EPCE.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (chkHC.Checked == true && Session["sessHCSS"] != null)
                    {
                        strPrac.Append(hid_HC.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (chkIPF.Checked == true && Session["sessIPFSS"] != null)
                    {
                        strPrac.Append(hid_IPF.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (chkRE.Checked == true && Session["sessRESS"] != null)
                    {
                        strPrac.Append(hid_RE.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (chkCorpTax.Checked == true && Session["sessCorpTaxSS"] != null)
                    {
                        strPrac.Append(hid_CorpTax.Value.Trim());
                        strPrac.Append(",");
                    }
                    if (!string.IsNullOrEmpty(strPrac.ToString()))
                    {
                        string strPracGrpId = strPrac.ToString().Trim().Substring(0, strPrac.Length - 1);
                        if (strPracGrpId.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strPracGrpId.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strPracGrpId.Split(',')[i].ToString(), "BusinessGroupId", "tblCredentialBusinessGroup", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }
                    string strSectorGroupMS = string.Empty;
                    strSectorGroupMS = hid_Tab_ClientIndustrySector.Value;
                    if (!string.IsNullOrEmpty(strSectorGroupMS))
                    {
                        if (strSectorGroupMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strSectorGroupMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strSectorGroupMS.Split(',')[i].ToString(), "ClientIndustrySectorId", "tblCredentialClientIndustrySector", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /*predominant country of client*/
                    string strCountryOfClientMS = string.Empty;
                    strCountryOfClientMS = hid_Tab_Country_PredominantCountry.Value;
                    if (!string.IsNullOrEmpty(strCountryOfClientMS))
                    {
                        if (strCountryOfClientMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strCountryOfClientMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strCountryOfClientMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryClient", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /*Sector Group*/
                    string strMatterSectorGroupMS = string.Empty;
                    strMatterSectorGroupMS = hid_Tab_TransactionIndustrySector.Value;
                    if (!string.IsNullOrEmpty(strMatterSectorGroupMS))
                    {
                        if (strMatterSectorGroupMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strMatterSectorGroupMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strMatterSectorGroupMS.Split(',')[i].ToString(), "TransactionIndustrySectorId", "tblCredentialTransactionIndustrySector", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }
					
                    /*Country where opened*/
                    string strCountryWhereOpenedMS = string.Empty;
                    strCountryWhereOpenedMS = hid_Tab_Country_Matter_Open.Value;
                    if (!string.IsNullOrEmpty(strCountryWhereOpenedMS))
                    {
                        if (strCountryWhereOpenedMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strCountryWhereOpenedMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strCountryWhereOpenedMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryMatterOpen", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /*stCountry of Transaction*/
                    string strCountryofTransactionMS = string.Empty;
                    strCountryofTransactionMS = hid_Tab_Country_Matter_Close.Value;
                    if (!string.IsNullOrEmpty(strCountryofTransactionMS))
                    {
                        if (strCountryofTransactionMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strCountryofTransactionMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strCountryofTransactionMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryMatterClose", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /* Jurisdiction Of Dispute */
                    if (cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "CONTENTIOUS")
                    {
                        string stJurisdictionOfDisputeMS = string.Empty;
                        stJurisdictionOfDisputeMS = hid_Tab_Country_Jurisdiction.Value;
                        if (!string.IsNullOrEmpty(stJurisdictionOfDisputeMS))
                        {
                            if (stJurisdictionOfDisputeMS.Split(',').Length > 0)
                            {
                                for (int i = 0; i < stJurisdictionOfDisputeMS.Split(',').Length; i++)
                                {
                                    string strOP = InsertingMultiSelectValues(stJurisdictionOfDisputeMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryJurisdiction", iCredentialID);

                                    if (string.IsNullOrEmpty(strMainInsert))
                                    {
                                        strMainInsert = strOP;
                                    }
                                    else
                                    {
                                        strMainInsert = strMainInsert + "|" + strOP;
                                    }
                                }
                            }
                        }

                        string stLanguageOfDisputeMS = string.Empty;
                        stLanguageOfDisputeMS = hid_Tab_Language_Of_Dispute.Value;
                        if (!string.IsNullOrEmpty(stLanguageOfDisputeMS))
                        {
                            if (stLanguageOfDisputeMS.Split(',').Length > 0)
                            {
                                for (int i = 0; i < stLanguageOfDisputeMS.Split(',').Length; i++)
                                {
                                    string strOP = InsertingMultiSelectValues(stLanguageOfDisputeMS.Split(',')[i].ToString(), "LanguageOfDisputeId", "tblCredentialLanguageOfDispute", iCredentialID);

                                    if (string.IsNullOrEmpty(strMainInsert))
                                    {
                                        strMainInsert = strOP;
                                    }
                                    else
                                    {
                                        strMainInsert = strMainInsert + "|" + strOP;
                                    }
                                }
                            }
                        }
                    }

                    /*Counrty Of Arbitration*/
                    string strCountryofArbitrationMS = string.Empty;
                    strCountryofArbitrationMS = hid_Tab_Country_ArbitrationCountry.Value;
                    if (!string.IsNullOrEmpty(strCountryofArbitrationMS))
                    {
                        if (strCountryofArbitrationMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strCountryofArbitrationMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strCountryofArbitrationMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryArbitrationCountry", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /*Lead Partner*/
                    string strLeadPartnerMS = string.Empty;
                    strLeadPartnerMS = hid_Tab_Lead_Partner.Value.ToString().ToUpper();
                    if (!string.IsNullOrEmpty(strLeadPartnerMS))
                    {
                        if (strLeadPartnerMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strLeadPartnerMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strLeadPartnerMS.Split(',')[i].ToString(), "LeadPartnerId", "tblCredentialLeadPartner", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }
					
                    /* Source Of Credential */
                    if (strLead.Contains("CMS PARTNER"))
                    {
                        string strSourceOfCredentialMS = string.Empty;
                        strSourceOfCredentialMS = hid_Tab_Source_Of_Credential.Value;
                        if (!string.IsNullOrEmpty(strSourceOfCredentialMS))
                        {
                            if (strSourceOfCredentialMS.Split(',').Length > 0)
                            {
                                for (int i = 0; i < strSourceOfCredentialMS.Split(',').Length; i++)
                                {
                                    string strOP = InsertingMultiSelectValues(strSourceOfCredentialMS.Split(',')[i].ToString(), "SourceOfCredentialId", "tblCredentialSourceOfCredential", iCredentialID);

                                    if (string.IsNullOrEmpty(strMainInsert))
                                    {
                                        strMainInsert = strOP;
                                    }
                                    else
                                    {
                                        strMainInsert = strMainInsert + "|" + strOP;
                                    }
                                }
                            }
                        }
                    }

                    /* Other Matter Executive */
                    string strOtherMatterExecutiveMS = string.Empty;
                    strOtherMatterExecutiveMS = hid_Tab_Other_Matter_Executive.Value;
                    if (!string.IsNullOrEmpty(strOtherMatterExecutiveMS))
                    {
                        if (strOtherMatterExecutiveMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strOtherMatterExecutiveMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strOtherMatterExecutiveMS.Split(',')[i].ToString(), "OtherMatterExecutiveId", "tblCredentialOtherMatterExecutive", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /*strOptionaltblCredValues/Client Sub-Sector Group*/
                    if (!string.IsNullOrEmpty(hid_Tab_Client_Industry_Type.Value.Trim()))
                    {
                        string strSubSectorGroupMS = string.Empty;
                        strSubSectorGroupMS = hid_Tab_Client_Industry_Type.Value;
                        if (!string.IsNullOrEmpty(strSubSectorGroupMS))
                        {
                            if (strSubSectorGroupMS.Split(',').Length > 0)
                            {
                                for (int i = 0; i < strSubSectorGroupMS.Split(',').Length; i++)
                                {
                                    string strOP = InsertingMultiSelectValues(strSubSectorGroupMS.Split(',')[i].ToString(), "ClientIndustryTypeId", "tblCredentialClientIndustryType", iCredentialID);

                                    if (string.IsNullOrEmpty(strMainInsert))
                                    {
                                        strMainInsert = strOP;
                                    }
                                    else
                                    {
                                        strMainInsert = strMainInsert + "|" + strOP;
                                    }
                                }
                            }
                        }
                    }

                    /* Matter Sub-Sector Group */
                    if (!string.IsNullOrEmpty(hid_Tab_Transaction_Industry_Type.Value.Trim()))
                    {
                        string strMatterSubSectorGroupMS = string.Empty;
                        strMatterSubSectorGroupMS = hid_Tab_Transaction_Industry_Type.Value;
                        if (strMatterSubSectorGroupMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strMatterSubSectorGroupMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strMatterSubSectorGroupMS.Split(',')[i].ToString(), "TransactionIndustryTypeId", "tblCredentialTransactionIndustryType", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /* Teams */
                    if (!string.IsNullOrEmpty(hid_Tab_Team.Value.Trim()))
                    {
                        string strTeamMS = string.Empty;
                        strTeamMS = hid_Tab_Team.Value;
                        if (strTeamMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strTeamMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strTeamMS.Split(',')[i].ToString(), "TeamId", "tblCredentialTeam", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /* CMS Firms Involved */
                    if (!string.IsNullOrEmpty(txt_Tab_Referred_From_Other_CMS_Office.Text.Trim()))
                    {
                        string strCMSFirmsInvolvedMS = string.Empty;
                        strCMSFirmsInvolvedMS = hid_Tab_Referred_From_Other_CMS_Office.Value;
                        if (strCMSFirmsInvolvedMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strCMSFirmsInvolvedMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strCMSFirmsInvolvedMS.Split(',')[i].ToString(), "ReferredFromOtherCMSOfficeId", "tblCredentialReferredFromOtherCMSOffice", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }
					
                    /* Other CMS Firms Involved */
                    if (!string.IsNullOrEmpty(txt_Tab_Country_OtherCMSOffice.Text.Trim()))
                    {
                        string strCountryOtherCMSOfficeMS = string.Empty;
                        strCountryOtherCMSOfficeMS = hid_Tab_Country_OtherCMSOffice.Value;

                        if (strCountryOtherCMSOfficeMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strCountryOtherCMSOfficeMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strCountryOtherCMSOfficeMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryOtherCMSOffice", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /* Other Uses */
                    if (!string.IsNullOrEmpty(txt_Tab_Other_Uses.Text.Trim()))
                    {
                        string strOtherUsesMS = string.Empty;
                        strOtherUsesMS = hid_Tab_Other_Uses.Value;
                        if (strOtherUsesMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strOtherUsesMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strOtherUsesMS.Split(',')[i].ToString(), "OtherUsesId", "tblCredentialOtherUses", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }

                    /*Know how */
                    if (!string.IsNullOrEmpty(txt_Tab_Know_How.Text.Trim()))
                    {
                        string strKnowHowMS = string.Empty;
                        strKnowHowMS = hid_Tab_Know_How.Value;
                        if (strKnowHowMS.Split(',').Length > 0)
                        {
                            for (int i = 0; i < strKnowHowMS.Split(',').Length; i++)
                            {
                                string strOP = InsertingMultiSelectValues(strKnowHowMS.Split(',')[i].ToString(), "KnowHowId", "tblCredentialKnowHow", iCredentialID);

                                if (string.IsNullOrEmpty(strMainInsert))
                                {
                                    strMainInsert = strOP;
                                }
                                else
                                {
                                    strMainInsert = strMainInsert + "|" + strOP;
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("Multiselect query -save Stops");
                    obj.LogWriter("Multiselect BAIF query -save Starts");
					
                    if (Session["sessBAIFMS"] != null)
                    {
                        if (Session["sessBAIFMS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessBAIFMS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "Work_Type":
                                                string strWorkTypeBAIFMS = string.Empty;
                                                strWorkTypeBAIFMS = strvals[i].Split('~')[1];
                                                if (strWorkTypeBAIFMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strWorkTypeBAIFMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strWorkTypeBAIFMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkTypeBAIF", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("Multiselect BAIF query -save Stops");
                    obj.LogWriter("Multiselect CORP query -save Starts");
					
                    if (Session["sessCORPMS"] != null)
                    {
                        if (Session["sessCORPMS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessCORPMS"].ToString().Split('|');
                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "Work_Type":
                                                string strWorkTypeCorpMS = string.Empty;
                                                strWorkTypeCorpMS = strvals[i].Split('~')[1];
                                                if (strWorkTypeCorpMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strWorkTypeCorpMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strWorkTypeCorpMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkType", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "Acting_For":
                                                string strActingForCorpMS = string.Empty;
                                                strActingForCorpMS = strvals[i].Split('~')[1];
                                                if (strActingForCorpMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strActingForCorpMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strActingForCorpMS.Split(',')[k].ToString(), "ActingForId", "tblCredentialActingFor", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "SubWork_Type":
                                                string strSubWork_TypeCorpMS = string.Empty;
                                                strSubWork_TypeCorpMS = strvals[i].Split('~')[1];
                                                if (strSubWork_TypeCorpMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strSubWork_TypeCorpMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strSubWork_TypeCorpMS.Split(',')[k].ToString(), "SubWorkTypeId", "tblCredentialSubWorkType", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "Country_Buyer":
                                                string strCountryBuyerCorpMS = string.Empty;
                                                strCountryBuyerCorpMS = strvals[i].Split('~')[1];
                                                if (strCountryBuyerCorpMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strCountryBuyerCorpMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strCountryBuyerCorpMS.Split(',')[k].ToString(), "CountryId", "tblCredentialCountryBuyer", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "Country_Seller":
                                                string strCountrySellerCorpMS = string.Empty;
                                                strCountrySellerCorpMS = strvals[i].Split('~')[1];
                                                if (strCountrySellerCorpMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strCountrySellerCorpMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strCountrySellerCorpMS.Split(',')[k].ToString(), "CountryId", "tblCredentialCountrySeller", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "Country_Target":
                                                string strCountryTargetCorpMS = string.Empty;
                                                strCountryTargetCorpMS = strvals[i].Split('~')[1];
                                                if (strCountryTargetCorpMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strCountryTargetCorpMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strCountryTargetCorpMS.Split(',')[k].ToString(), "CountryId", "tblCredentialCountryTarget", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
										case "Value_Over_Pound_MS":
											string strValue_Over_Pound_MS = string.Empty;
											strValue_Over_Pound_MS = strvals[i].Split('~')[1];
											if (strValue_Over_Pound_MS.Split(',').Length > 0)
											{
												for (int k = 0; k < strValue_Over_Pound_MS.Split(',').Length; k++)
												{
													string strOP = InsertingMultiSelectValues(strValue_Over_Pound_MS.Split(',')[k].ToString(), "YesNoId", "tblCredentialPoundValue", iCredentialID);
													if (string.IsNullOrEmpty(strMainInsert))
													{
														strMainInsert = strOP;
													}
													else
													{
														strMainInsert = strMainInsert + "|" + strOP;
													}
												}
											}
											break;
										case "Value_Over_US_MS":
											string strValue_Over_US_MS = string.Empty;
											strValue_Over_US_MS = strvals[i].Split('~')[1];
											if (strValue_Over_US_MS.Split(',').Length > 0)
											{
												for (int k = 0; k < strValue_Over_US_MS.Split(',').Length; k++)
												{
													string strOP = InsertingMultiSelectValues(strValue_Over_US_MS.Split(',')[k].ToString(), "YesNoId", "tblCredentialDollarValue", iCredentialID);
													if (string.IsNullOrEmpty(strMainInsert))
													{
														strMainInsert = strOP;
													}
													else
													{
														strMainInsert = strMainInsert + "|" + strOP;
													}
												}
											}
											break;
										case "Value_Over_Euro_MS":
											string strValue_Over_Euro_MS = string.Empty;
											strValue_Over_Euro_MS = strvals[i].Split('~')[1];
											if (strValue_Over_Euro_MS.Split(',').Length > 0)
											{
												for (int k = 0; k < strValue_Over_Euro_MS.Split(',').Length; k++)
												{
													string strOP = InsertingMultiSelectValues(strValue_Over_Euro_MS.Split(',')[k].ToString(), "YesNoId", "tblCredentialEuroValue", iCredentialID);
													if (string.IsNullOrEmpty(strMainInsert))
													{
														strMainInsert = strOP;
													}
													else
													{
														strMainInsert = strMainInsert + "|" + strOP;
													}
												}
											}
											break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("Multiselect CORP query -save Stops");
                    obj.LogWriter("Multiselect CRD query -save Starts");
					
                    if (Session["sessCRDMS"] != null)
                    {
                        if (Session["sessCRDMS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessCRDMS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "Work_Type":
                                                string strWorkTypeCRDMS = string.Empty;
                                                strWorkTypeCRDMS = strvals[i].Split('~')[1];
                                                if (strWorkTypeCRDMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strWorkTypeCRDMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strWorkTypeCRDMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkTypeCRD", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "SubWork_Type":
                                                string strSubWorkTypeCRDMS = string.Empty;
                                                strSubWorkTypeCRDMS = strvals[i].Split('~')[1];
                                                if (strSubWorkTypeCRDMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strSubWorkTypeCRDMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strSubWorkTypeCRDMS.Split(',')[k].ToString(), "SubWorkTypeId", "tblCredentialSubWorkTypeCommercial", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("Multiselect CRD query -save Stops");
                    obj.LogWriter("Multiselect EPC Const query -save Starts");
					
                    if (Session["sessEPCMS"] != null)
                    {
                        if (Session["sessEPCMS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessEPCMS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "Nature_Of_Work":
                                                string strNature_Of_WorkEPCMS = string.Empty;
                                                strNature_Of_WorkEPCMS = strvals[i].Split('~')[1];
                                                if (strNature_Of_WorkEPCMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strNature_Of_WorkEPCMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strNature_Of_WorkEPCMS.Split(',')[k].ToString(), "NatureWorkId", "tblCredentialNatureWork", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "Type_Of_Contract":
                                                string strTypeOfContractEPCMS = string.Empty;
                                                strTypeOfContractEPCMS = strvals[i].Split('~')[1];
                                                if (strTypeOfContractEPCMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strTypeOfContractEPCMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strTypeOfContractEPCMS.Split(',')[k].ToString(), "TypeContractId", "tblCredentialTypeOfContract", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("Multiselect EPC Const query -save Stops");
                    obj.LogWriter("Multiselect EPC Energy query -save Starts");
					
                    if (Session["sessEPCEMS"] != null)
                    {
                        if (Session["sessEPCEMS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessEPCEMS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "EPC_Project_Sector":
                                                string strEPCProjectSectorEPCMS = string.Empty;
                                                strEPCProjectSectorEPCMS = strvals[i].Split('~')[1];
                                                if (strEPCProjectSectorEPCMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strEPCProjectSectorEPCMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strEPCProjectSectorEPCMS.Split(',')[k].ToString(), "EPCProjectSectorId", "tblCredentialEPCProjectSector", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "Transaction_Type":
                                                string strTransactionTypeEPCEMS = string.Empty;
                                                strTransactionTypeEPCEMS = strvals[i].Split('~')[1];
                                                if (strTransactionTypeEPCEMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strTransactionTypeEPCEMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strTransactionTypeEPCEMS.Split(',')[k].ToString(), "TransactionTypeId", "tblCredentialTransactionType", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                break;

                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("Multiselect EPC Energy query -save Stops");
                    obj.LogWriter("Multiselect IPF query -save Starts");
					
                    if (Session["sessIPFMS"] != null)
                    {
                        if (Session["sessIPFMS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessIPFMS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {
                                            case "Project_Sector":
                                                string strProject_SectorIPFMS = string.Empty;
                                                strProject_SectorIPFMS = strvals[i].Split('~')[1];
                                                if (strProject_SectorIPFMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strProject_SectorIPFMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strProject_SectorIPFMS.Split(',')[k].ToString(), "ProjectSectorId", "tblCredentialProjectSector", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("Multiselect IPF query -save Stops");
                    obj.LogWriter("Multiselect RE query -save Starts");

                    if (Session["sessREMS"] != null)
                    {
                        if (Session["sessREMS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessREMS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {

                                            case "Client_Type":
                                                string strClientTypeREMS = string.Empty;
                                                strClientTypeREMS = strvals[i].Split('~')[1];
                                                if (strClientTypeREMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strClientTypeREMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strClientTypeREMS.Split(',')[k].ToString(), "ClientTypeId", "tblCredentialClientTypeRealEstate", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            case "Work_Type":
                                                string strWork_TypeREMS = string.Empty;
                                                strWork_TypeREMS = strvals[i].Split('~')[1];
                                                if (strWork_TypeREMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strWork_TypeREMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strWork_TypeREMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkTypeRealEstate", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            //SubWork_Type
                                            case "SubWork_Type":
                                                string strSubWork_TypeREMS = string.Empty;
                                                strSubWork_TypeREMS = strvals[i].Split('~')[1];
                                                if (strSubWork_TypeREMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strSubWork_TypeREMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strSubWork_TypeREMS.Split(',')[k].ToString(), "SubWorkTypeId", "tblCredentialSubWorkTypeRE", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
					
                    obj.LogWriter("Multiselect RE query -save Stops");
                    obj.LogWriter("Multiselect HC query -save Starts");

                    if (Session["sessHCMS"] != null)
                    {
                        if (Session["sessHCMS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessHCMS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    string str = string.Empty;

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {

                                            case "WorkTypeIdHC":
                                                string strWork_TypeHCMS = string.Empty;
                                                strWork_TypeHCMS = strvals[i].Split('~')[1];
                                                if (strWork_TypeHCMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strWork_TypeHCMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strWork_TypeHCMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkTypeHC", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            //SubWork_Type
                                            case "SubWork_Type":
                                                string strSubWork_TypeHCMS = string.Empty;
                                                strSubWork_TypeHCMS = strvals[i].Split('~')[1];
                                                if (strSubWork_TypeHCMS.Split(',').Length > 0)
                                                {
                                                    for (int k = 0; k < strSubWork_TypeHCMS.Split(',').Length; k++)
                                                    {
                                                        string strOP = InsertingMultiSelectValues(strSubWork_TypeHCMS.Split(',')[k].ToString(), "SubWorkTypeId", "tblCredentialSubWorkTypeHC", iCredentialID);

                                                        if (string.IsNullOrEmpty(strMainInsert))
                                                        {
                                                            strMainInsert = strOP;
                                                        }
                                                        else
                                                        {
                                                            strMainInsert = strMainInsert + "|" + strOP;
                                                        }
                                                    }
                                                }
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    obj.LogWriter("Multiselect HC query -save Stops");
                    obj.LogWriter("Multiselect save in DB Starts");
					
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    cmd.Transaction = transaction;
                    if (!string.IsNullOrEmpty(strMainInsert))
                    {
                        for (int i = 0; i < strMainInsert.Split('|').Length; i++)
                        {
                            cmd.CommandText = strMainInsert.Split('|')[i].ToString();
                            int j = cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();

					strid = iCredentialID.ToString();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"alert('Record saved sucessfully');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", sb.ToString(), false);
                    obj.LogWriter("Multiselect save in DB Stops");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    obj.ErrorWriter("EntryDetails Error : btnAddBottom_Click ends  " + ex.Message, hidName.Value);
                    //blnAdd = false;
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    con.Close();
                    ClearAllValues();
                   
                }
            }
        }
		
		private void MailQuery(string str)
		{
			bool EmailOption = false;
			
			string EmailFromAddress = ConfigurationManager.AppSettings["From"];
			string EmailToAddress = ConfigurationManager.AppSettings["To"];
			string EmailSubject = "Query";
			string SMTPSERVER = ConfigurationManager.AppSettings["SMTPSERVER"];
			
			MailMessage mailMessage = new MailMessage();
			MailAddress mailAddress = new MailAddress(EmailFromAddress);
			mailMessage.From = mailAddress;
			mailMessage.To.Add(EmailToAddress);
			mailMessage.Subject = EmailSubject;
			
			if (!EmailOption)
			{
				mailMessage.IsBodyHtml = true;
				mailMessage.Body = str;
			}
			
			SmtpClient smtp = new SmtpClient(SMTPSERVER);
			smtp.Send(mailMessage);
		}
		
		private void InsertSearchResultTable(string strcon, string strValue)
		{
			objLogger.LogWriter("EntryScreen : InsertSearchResultTable Starts", hidName.Value);
			SqlConnection con = new SqlConnection(strcon);
			con.Open();
			
			SqlCommand cmd = new SqlCommand("Delete from tblCredentialSearchResults where CredentialId=" + strValue, con);
			cmd.ExecuteNonQuery();
			
			cmd.Dispose();
			con.Close();
			
			string selSQL = string.Empty;
			StreamReader sr = new StreamReader(Server.MapPath("~\\Queries\\SearchResultQuery_Insert.txt"));
			
			selSQL = sr.ReadToEnd();
			selSQL = selSQL.Replace("strCredentialID", strValue);
			selSQL = "insert into tblCredentialSearchResults " + selSQL;
			
			sr.Dispose();
			con.Open();
			
			SqlCommand cmd2 = new SqlCommand(selSQL, con);
			cmd2.ExecuteNonQuery();
			
			cmd2.Dispose();
			con.Close();
			
			objLogger.LogWriter("EntryScreen : InsertSearchResultTable Ends", hidName.Value);
		}
		
		private void InsertKeyWordSearchTable(string strcon, string strValue)
		{
			objLogger.LogWriter("EntryScreen : InsertKeyWordSearchTable Starts", hidName.Value);
			SqlConnection con = new SqlConnection(strcon);
			con.Open();

			string strQuery = "select KeywordSearch from tblKeywordSearch where KeywordSearch ='" + strValue.Replace("1@2!", ",") + "'";
			SqlDataAdapter adp = new SqlDataAdapter(strQuery, con);
			DataSet ds = new DataSet();
			adp.Fill(ds);

			if (ds.Tables[0].Rows.Count == 0)
			{
				string sql = "insert into tblKeywordSearch(KeywordSearch) values(N'" + strValue.Replace("1@2!", ",") + "')";

				objLogger.LogWriter("EntryScreen : InsertKeyWordSearchTable Query - " + sql, hidName.Value);

				SqlCommand cmdClient = new SqlCommand(sql, con);
				cmdClient.ExecuteNonQuery();
				cmdClient.Dispose();
			}
			adp.Dispose();
			ds.Dispose();
			con.Close();
			objLogger.LogWriter("EntryScreen : InsertKeyWordSearchTable Ends", hidName.Value);
		}

		protected void btnEditBottom_Click(object sender, EventArgs e)
		{
			string strMainDelete = string.Empty;
			string strMainInsert = string.Empty;

			Logger obj = new Logger();

            bool blnUpdate = false;

            obj.LogWriter("btnEditBottom_Click Starts");

            StringBuilder strtblCredFields = new StringBuilder();
            StringBuilder strtblCredValues = new StringBuilder();
            StringBuilder strOptionaltblCredValues = new StringBuilder();
            StringBuilder strOptionaltblCredFields = new StringBuilder();
            StringBuilder strBAIFtblCredFields = new StringBuilder();
            StringBuilder strBAIFtblCredValues = new StringBuilder();
            StringBuilder strCorptblCredFields = new StringBuilder();
            StringBuilder strCorptblCredValues = new StringBuilder();
            StringBuilder strCRDtblCredFields = new StringBuilder();
            StringBuilder strCRDtblCredValues = new StringBuilder();
            StringBuilder strEPCtblCredFields = new StringBuilder();
            StringBuilder strEPCtblCredValues = new StringBuilder();
            StringBuilder strEPCEnetblCredFields = new StringBuilder();
            StringBuilder strEPCEnetblCredValues = new StringBuilder();
            StringBuilder strHCtblCredFields = new StringBuilder();
            StringBuilder strHCtblCredValues = new StringBuilder();
            StringBuilder strIPFtblCredFields = new StringBuilder();
            StringBuilder strIPFtblCredValues = new StringBuilder();
            StringBuilder strREtblCredFields = new StringBuilder();
            StringBuilder strREtblCredValues = new StringBuilder();
            StringBuilder strCorpTaxtblCredFields = new StringBuilder();
            StringBuilder strCorpTaxtblCredValues = new StringBuilder();
            StringBuilder strquerycols = new StringBuilder();
            StringBuilder strqueryvals = new StringBuilder();
            StringBuilder strPrac = new StringBuilder();

            SetTextBoxValues();

			string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
			SqlConnection con = new SqlConnection(strcon);
			SqlCommand cmd = new SqlCommand();
			con.Open();
			SqlTransaction transaction = con.BeginTransaction();

			try
			{
				txt_Tab_Client.Text = txt_Tab_Client.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_Bible_Reference.Text = txt_Tab_Bible_Reference.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_ProjectName_Core.Text = txt_Tab_ProjectName_Core.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_Matter_No.Text = txt_Tab_Matter_No.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_Keyword.Text = txt_Tab_Keyword.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_CMSPartnerName.Text = txt_Tab_CMSPartnerName.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_ClientDescription.Text = txt_Tab_ClientDescription.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_Project_Description.Text = txt_Tab_Project_Description.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_Significant_Features.Text = txt_Tab_Significant_Features.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_Country_Law_Other.Text = txt_Tab_Country_Law_Other.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_ArbitrationCity_Other.Text = txt_Tab_ArbitrationCity_Other.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_SourceOfCredential_Other.Text = txt_Tab_SourceOfCredential_Other.Text.Replace("'", "''").Replace(",", "1@2!");
				txt_Tab_Language_Of_Dispute_Other.Text = txt_Tab_Language_Of_Dispute_Other.Text.Replace("'", "''").Replace(",", "1@2!");

                /* Client Name */
                if (!string.IsNullOrEmpty(txt_Tab_Client.Text.Trim()))
                {
                    strtblCredFields.Append(lbl_Tab_Client.ID.Substring(8, lbl_Tab_Client.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(txt_Tab_Client.Text.Trim().Replace(",", "1@2!"));
					strtblCredValues.Append("',N'");
                }
				else
				{
					strtblCredFields.Append(lbl_Tab_Client.ID.Substring(8, lbl_Tab_Client.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}

                /* Client Name Confidential */
                if (rdo_Tab_Client_Name_Confidential.SelectedIndex != -1)
                {
                    strtblCredFields.Append(lbl_Tab_Client_Name_Confidential.ID.Substring(8, lbl_Tab_Client_Name_Confidential.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(rdo_Tab_Client_Name_Confidential.SelectedItem.Value);
					strtblCredValues.Append("',N'");

                    /* Description , confidential on completion*/
                    if (rdo_Tab_Client_Name_Confidential.SelectedItem.Text.Trim().ToUpper() == "YES")
                    {
                        if (!string.IsNullOrEmpty(txt_Tab_ClientDescription.Text.Trim()))
                        {
                            strtblCredFields.Append(lbl_Tab_ClientDescription.ID.Substring(8, lbl_Tab_ClientDescription.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(txt_Tab_ClientDescription.Text.Trim().Replace(",", "1@2!"));
							strtblCredValues.Append("',N'");
						}
						else
						{
							strtblCredFields.Append(lbl_Tab_ClientDescription.ID.Substring(8, lbl_Tab_ClientDescription.ID.Length - 8));
							strtblCredFields.Append(",");
							strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");
						}
						
                        if (rdo_Tab_NameConfidential_Completion.SelectedIndex != -1)
                        {
                            strtblCredFields.Append(lbl_Tab_NameConfidential_Completion.ID.Substring(8, lbl_Tab_NameConfidential_Completion.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(rdo_Tab_NameConfidential_Completion.SelectedItem.Value);
							strtblCredValues.Append("',N'");
						}
						else
						{
							strtblCredFields.Append(lbl_Tab_NameConfidential_Completion.ID.Substring(8, lbl_Tab_NameConfidential_Completion.ID.Length - 8));
							strtblCredFields.Append(",");
							strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");
						}
					}
                    else
                    {
                        strtblCredFields.Append(lbl_Tab_ClientDescription.ID.Substring(8, lbl_Tab_ClientDescription.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
                        strtblCredValues.Append("','");

                        strtblCredFields.Append(lbl_Tab_NameConfidential_Completion.ID.Substring(8, lbl_Tab_NameConfidential_Completion.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");
					}
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Client_Name_Confidential.ID.Substring(8, lbl_Tab_Client_Name_Confidential.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}

                /*Matter No*/
                if (!string.IsNullOrEmpty(txt_Tab_Matter_No.Text.Trim()))
                {
                    strtblCredFields.Append(lbl_Tab_Matter_No.ID.Substring(8, lbl_Tab_Matter_No.ID.Length - 8));
                    strtblCredFields.Append(",");

					strtblCredValues.Append(txt_Tab_Matter_No.Text.Trim().Replace(",", "1@2!"));
					strtblCredValues.Append("',N'");
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Matter_No.ID.Substring(8, lbl_Tab_Matter_No.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}
				if (cld_Tab_Date_Opened.SelectedDate.HasValue && cld_Tab_Date_Opened.SelectedDate.HasValue)
				{
					strtblCredFields.Append(lbl_Tab_Date_Opened.ID.Substring(8, lbl_Tab_Date_Opened.ID.Length - 8));
					strtblCredFields.Append(",");
					string str = strtblCredValues.ToString().Substring(0, strtblCredValues.Length - 2);
					strtblCredValues.Clear();
					strtblCredValues.Append(str);


                    string strD = String.Empty;//Adi  Commnted line , could not find this variable :strD= scld_Tab_Date_Opened.DateInput.SelectedDate.Value.Day + "/" + cld_Tab_Date_Opened.DateInput.SelectedDate.Value.Month + "/" + cld_Tab_Date_Opened.DateInput.SelectedDate.Value.Year;
					string strPurDate = "convert(datetime|'" + cld_Tab_Date_Opened.DateInput.DisplayText + "'|103)";
					strtblCredValues.Append(strPurDate);
					strtblCredValues.Append(",N'");
				}
                /*Matter Credential Description*/
                if (!string.IsNullOrEmpty(txt_Tab_Project_Description.Text.Trim()))
                {
                    strtblCredFields.Append(lbl_Tab_Project_Description.ID.Substring(8, lbl_Tab_Project_Description.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(txt_Tab_Project_Description.Text.Trim().Replace(",", "1@2!"));
					strtblCredValues.Append("',N'");
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Project_Description.ID.Substring(8, lbl_Tab_Project_Description.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}
				
                /*Matter Confidential */
                if (rdo_Tab_Client_Matter_Confidential.SelectedIndex != -1)
                {
                    strtblCredFields.Append(lbl_Tab_Client_Matter_Confidential.ID.Substring(8, lbl_Tab_Client_Matter_Confidential.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(rdo_Tab_Client_Matter_Confidential.SelectedItem.Value);
					strtblCredValues.Append("',N'");

                    if (rdo_Tab_Client_Matter_Confidential.SelectedItem.Text.Trim().ToUpper() == "YES")
                    {
                        obj.LogWriter("rdo_Tab_MatterConfidential_Completion Starts");
						if (rdo_Tab_MatterConfidential_Completion.SelectedItem != null)
						{
							if (rdo_Tab_MatterConfidential_Completion.SelectedItem.Value != "-1")
							{
								strtblCredFields.Append(lbl_Tab_MatterConfidential_Completion.ID.Substring(8, lbl_Tab_MatterConfidential_Completion.ID.Length - 8));
								strtblCredFields.Append(",");
								
								strtblCredValues.Append(rdo_Tab_MatterConfidential_Completion.SelectedItem.Value);
								strtblCredValues.Append("',N'");
							}
							else
							{
								strtblCredFields.Append(lbl_Tab_MatterConfidential_Completion.ID.Substring(8, lbl_Tab_MatterConfidential_Completion.ID.Length - 8));
								strtblCredFields.Append(",");

								strtblCredValues.Append(string.Empty);
								strtblCredValues.Append("',N'");
							}
						}
					}
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Client_Matter_Confidential.ID.Substring(8, lbl_Tab_Client_Matter_Confidential.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}
				
                /*applicable Law*/
                if (cbo_Tab_Country_Law.SelectedItem.Value != "-1")
                {
                    strtblCredFields.Append(lbl_Tab_Country_Law.ID.Substring(8, lbl_Tab_Country_Law.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(cbo_Tab_Country_Law.SelectedItem.Value.Trim());
					strtblCredValues.Append("',N'");

                    if (cbo_Tab_Country_Law.SelectedItem.Text.Trim().ToUpper() == "OTHER")
                    {
                        if (!string.IsNullOrEmpty(txt_Tab_Country_Law_Other.Text.Trim()))
                        {

                            strtblCredFields.Append(lbl_Tab_Country_Law_Other.ID.Substring(8, lbl_Tab_Country_Law_Other.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(txt_Tab_Country_Law_Other.Text.Trim());
							strtblCredValues.Append("',N'");
                        }
                        else
                        {
                            strtblCredFields.Append(lbl_Tab_Country_Law_Other.ID.Substring(8, lbl_Tab_Country_Law_Other.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");
						}
					}
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Country_Law.ID.Substring(8, lbl_Tab_Country_Law.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}

                /*Country Where Opened*/
                /*Contentious/Non Contentious*/
                if (cbo_Tab_Contentious_IRG.SelectedItem.Value != "-1")
                {
                    strtblCredFields.Append(lbl_Tab_Contentious_IRG.ID.Substring(8, lbl_Tab_Contentious_IRG.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(cbo_Tab_Contentious_IRG.SelectedItem.Value.Trim());
					strtblCredValues.Append("',N'");

                    if (cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "CONTENTIOUS")
                    {
                        /* Type of dispute */
                        /*strtblCredFields.Append(lbl_Tab_Type_Of_Dispute.ID.Substring(8, lbl_Tab_Type_Of_Dispute.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Type_Of_Dispute.SelectedItem.Value.Trim());
                        strtblCredValues.Append("','");*/

                        /* Dispute Resolution */
                        strtblCredFields.Append(lbl_Tab_Dispute_Resolution.ID.Substring(8, lbl_Tab_Dispute_Resolution.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(cbo_Tab_Dispute_Resolution.SelectedItem.Value.Trim());
						strtblCredValues.Append("',N'");

                        if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "ARBITRATION")
                        {
                            if (cbo_Tab_ArbitrationCity.SelectedItem.Value != "-1")
                            {
                                /*City of Arbitration*/
                                strtblCredFields.Append(lbl_Tab_ArbitrationCity.ID.Substring(8, lbl_Tab_ArbitrationCity.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(cbo_Tab_ArbitrationCity.SelectedItem.Value.Trim());
								strtblCredValues.Append("',N'");
							}
							
                            if (cbo_Tab_ArbitrationCity.SelectedItem.Text.Trim().ToUpper() == "OTHER")
                            {
                                strtblCredFields.Append(lbl_Tab_ArbitrationCity_Other.ID.Substring(8, lbl_Tab_ArbitrationCity_Other.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(txt_Tab_ArbitrationCity_Other.Text.Trim());
								strtblCredValues.Append("',N'");
                            }

                            if (cbo_Tab_Arbitral_Rules.SelectedItem.Value != "-1")
                            {
                                /*Arbitral Rules*/
                                strtblCredFields.Append(lbl_Tab_Arbitral_Rules.ID.Substring(8, lbl_Tab_Arbitral_Rules.ID.Length - 8));
                                strtblCredFields.Append(",");

                                strtblCredValues.Append(cbo_Tab_Arbitral_Rules.SelectedItem.Value.Trim());
								strtblCredValues.Append("',N'");
                            }
                        }
                        else
                        {
                            strtblCredFields.Append(lbl_Tab_ArbitrationCity.ID.Substring(8, lbl_Tab_ArbitrationCity.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");

                            strtblCredFields.Append(lbl_Tab_ArbitrationCity_Other.ID.Substring(8, lbl_Tab_ArbitrationCity_Other.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");

                            strtblCredFields.Append(lbl_Tab_Arbitral_Rules.ID.Substring(8, lbl_Tab_Arbitral_Rules.ID.Length - 8));
                            strtblCredFields.Append(",");

                            strtblCredValues.Append(string.Empty);
							strtblCredValues.Append("',N'");
                        }
                    }
                    else
                    {
                        strtblCredFields.Append(lbl_Tab_Dispute_Resolution.ID.Substring(8, lbl_Tab_Dispute_Resolution.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");

                        strtblCredFields.Append(lbl_Tab_ArbitrationCity.ID.Substring(8, lbl_Tab_ArbitrationCity.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");

                        strtblCredFields.Append(lbl_Tab_ArbitrationCity_Other.ID.Substring(8, lbl_Tab_ArbitrationCity_Other.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");

                        strtblCredFields.Append(lbl_Tab_Arbitral_Rules.ID.Substring(8, lbl_Tab_Arbitral_Rules.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");
					}
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Contentious_IRG.ID.Substring(8, lbl_Tab_Contentious_IRG.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}
				
                /*Value Of Deal */
                if (!string.IsNullOrEmpty(txt_Tab_ValueOfDeal_Core.Text.Trim()))
                {
                    strtblCredFields.Append(lbl_Tab_ValueOfDeal_Core.ID.Substring(8, lbl_Tab_ValueOfDeal_Core.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(txt_Tab_ValueOfDeal_Core.Text.Trim());
					strtblCredValues.Append("',N'");
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_ValueOfDeal_Core.ID.Substring(8, lbl_Tab_ValueOfDeal_Core.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}
				
				/*Currency Of Deal*/
				if (cbo_Tab_Currency_Of_Deal.SelectedItem.Value != "-1")
				{
					strtblCredFields.Append(lbl_Tab_Currency_Of_Deal.ID.Substring(8, lbl_Tab_Currency_Of_Deal.ID.Length - 8));
					strtblCredFields.Append(",");

					strtblCredValues.Append(cbo_Tab_Currency_Of_Deal.SelectedItem.Value.Trim());
					strtblCredValues.Append("',N'");
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Currency_Of_Deal.ID.Substring(8, lbl_Tab_Currency_Of_Deal.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}
				
				/*Value Confidential*/
				if (rdo_Tab_Value_Confidential.SelectedIndex != -1)
				{
					strtblCredFields.Append(lbl_Tab_Value_Confidential.ID.Substring(8, lbl_Tab_Value_Confidential.ID.Length - 8));
					strtblCredFields.Append(",");

					strtblCredValues.Append(rdo_Tab_Value_Confidential.SelectedItem.Value);
					strtblCredValues.Append("',N'");
					
                    /* confidential on completion */
					if (rdo_Tab_Client_Name_Confidential.SelectedItem.Text.Trim().ToUpper() == "YES")
					{
						if (rdo_Tab_ValueConfidential_Completion.SelectedItem != null)
						{
							if (rdo_Tab_ValueConfidential_Completion.SelectedItem.Value != "-1")
							{
								strtblCredFields.Append(lbl_Tab_ValueConfidential_Completion.ID.Substring(8, lbl_Tab_ValueConfidential_Completion.ID.Length - 8));
								strtblCredFields.Append(",");

								strtblCredValues.Append(rdo_Tab_ValueConfidential_Completion.SelectedItem.Value);
								strtblCredValues.Append("',N'");
							}
							else
							{
								strtblCredFields.Append(lbl_Tab_ValueConfidential_Completion.ID.Substring(8, lbl_Tab_ValueConfidential_Completion.ID.Length - 8));
								strtblCredFields.Append(",");
								strtblCredValues.Append(string.Empty);
								strtblCredValues.Append("',N'");
							}
						}
					}
					else
					{
						strtblCredFields.Append(lbl_Tab_ValueConfidential_Completion.ID.Substring(8, lbl_Tab_ValueConfidential_Completion.ID.Length - 8));
						strtblCredFields.Append(",");
						strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");
					}
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Value_Confidential.ID.Substring(8, lbl_Tab_Value_Confidential.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}
				
                string strLead = hid_Tab_Lead_Partner_Text.Value;
                if (strLead.Contains("CMS PARTNER"))
                {
                    /*Name Of CMS PArtner*/
                    if (!string.IsNullOrEmpty(txt_Tab_CMSPartnerName.Text.Trim()))
                    {
                        strtblCredFields.Append(lbl_Tab_CMSPartnerName.ID.Substring(8, lbl_Tab_CMSPartnerName.ID.Length - 8));
                        strtblCredFields.Append(",");

                        strtblCredValues.Append(txt_Tab_CMSPartnerName.Text.Trim());
						strtblCredValues.Append("',N'");
					}
					else
					{
						strtblCredFields.Append(lbl_Tab_CMSPartnerName.ID.Substring(8, lbl_Tab_CMSPartnerName.ID.Length - 8));
						strtblCredFields.Append(",");
						strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");
					}
					
					if (!string.IsNullOrEmpty(txt_Tab_SourceOfCredential_Other.Text.Trim()))
					{
						strtblCredFields.Append(lbl_Tab_SourceOfCredential_Other.ID.Substring(8, lbl_Tab_SourceOfCredential_Other.ID.Length - 8));
						strtblCredFields.Append(",");

						strtblCredValues.Append(txt_Tab_SourceOfCredential_Other.Text.Trim());
						strtblCredValues.Append("',N'");
					}
					else
					{
						strtblCredFields.Append(lbl_Tab_SourceOfCredential_Other.ID.Substring(8, lbl_Tab_SourceOfCredential_Other.ID.Length - 8));
						strtblCredFields.Append(",");
						strtblCredValues.Append(string.Empty);
						strtblCredValues.Append("',N'");
					}
				}
                else
                {
                    strtblCredFields.Append(lbl_Tab_CMSPartnerName.ID.Substring(8, lbl_Tab_CMSPartnerName.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");

                    strtblCredFields.Append(lbl_Tab_SourceOfCredential_Other.ID.Substring(8, lbl_Tab_SourceOfCredential_Other.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
                }

                /* Credential Status */
                if (cbo_Tab_Credential_Status.SelectedItem.Value != "-1")
                {
                    strtblCredFields.Append(lbl_Tab_Credential_Status.ID.Substring(8, lbl_Tab_Credential_Status.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(cbo_Tab_Credential_Status.SelectedItem.Value.Trim());
					strtblCredValues.Append("',N'");
				}
				else
				{
					strtblCredFields.Append(lbl_Tab_Credential_Status.ID.Substring(8, lbl_Tab_Credential_Status.ID.Length - 8));
					strtblCredFields.Append(",");
					strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
				}
				
				/*Credntial Version*/
                if (cbo_Tab_Credential_Version.SelectedItem.Value != "-1")
                {
                    strtblCredFields.Append(lbl_Tab_Credential_Version.ID.Substring(8, lbl_Tab_Credential_Version.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(cbo_Tab_Credential_Version.SelectedItem.Value.Trim());
					strtblCredValues.Append("',N'");
                }

                /*Credntial Version other*/
                if (!string.IsNullOrEmpty(txt_Tab_Credential_Version_Other.Text.Trim()))
                {
                    strtblCredFields.Append(lbl_Tab_Credential_Version_Other.ID.Substring(8, lbl_Tab_Credential_Version_Other.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(txt_Tab_Credential_Version_Other.Text.Trim());
					strtblCredValues.Append("',N'");
                }
                else
                {
                    strtblCredFields.Append(lbl_Tab_Credential_Version_Other.ID.Substring(8, lbl_Tab_Credential_Version_Other.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(string.Empty);
					strtblCredValues.Append("',N'");
                }

                /* Credential Type */
                if (cbo_Tab_Credential_Type.SelectedItem.Value != "-1")
                {
                    strtblCredFields.Append(lbl_Tab_Credential_Type.ID.Substring(8, lbl_Tab_Credential_Type.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(cbo_Tab_Credential_Type.SelectedItem.Value.Trim());
					strtblCredValues.Append("',N'");
                }

                /* Priority */
                if (cbo_Tab_Priority.SelectedItem.Value != "-1")
                {
                    strtblCredFields.Append(lbl_Tab_Priority.ID.Substring(8, lbl_Tab_Priority.ID.Length - 8));
                    strtblCredFields.Append(",");

                    strtblCredValues.Append(cbo_Tab_Priority.SelectedItem.Value.Trim());
					strtblCredValues.Append("',N'");
                }

                /*Keyword Search Appender*/
                obj.LogWriter("Keyword Search Appender Starts");
                StringBuilder strAppender = new StringBuilder();

                if (!string.IsNullOrEmpty(txt_Tab_Client.Text.Trim()))
                {
					strAppender.Append(txt_Tab_Client.Text.Trim().Replace(",", "1@2!"));
					strAppender.Append("~");
                }

                /*Matter/credential description*/
                if (!string.IsNullOrEmpty(txt_Tab_Project_Description.Text.Trim()))
                {
					strAppender.Append(txt_Tab_Project_Description.Text.Trim().Replace(",", "1@2!"));
					strAppender.Append("~");
                }

                /*Matter/credential other information*/
                if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text.Trim()))
                {
                    strAppender.Append(txt_Tab_Significant_Features.Text.Trim());
					strAppender.Append("~");
                }

                /*Keyword(s)/themes*/
                if (!string.IsNullOrEmpty(txt_Tab_Keyword.Text.Trim()))
                {
                    strAppender.Append(txt_Tab_Keyword.Text.Trim().Replace(",", "1@2!"));
					strAppender.Append("~");
                }

                if (!string.IsNullOrEmpty(txt_Tab_ClientIndustrySector.Text.Trim()))
                {
                    strAppender.Append(txt_Tab_ClientIndustrySector.Text.Trim().Replace(",", "1@2!"));
					strAppender.Append("~");
                    //strAppender.Append(txt_Tab_ClientIndustrySector.Text.Trim()); strAppender.Append("~");
                }

                if (!string.IsNullOrEmpty(txt_Tab_TransactionIndustrySector.Text.Trim()))
                {
                    strAppender.Append(txt_Tab_TransactionIndustrySector.Text.Trim().Replace(",", "1@2!"));
					strAppender.Append("~");
                }

                string strAppenderFinal = strAppender.ToString().Substring(0, strAppender.Length - 1);
                if (!string.IsNullOrEmpty(strAppenderFinal))
                {
                    strtblCredFields.Append("KeyWordSearch");
                    strtblCredFields.Append(",");

					strtblCredValues.Append(strAppenderFinal.Trim().Replace(",", "1@2!"));
					strtblCredValues.Append("',N'");
                }

                /* Insert into Keyword Search Table for Optimised Search Starts */
                if (!string.IsNullOrEmpty(txt_Tab_Client.Text.Trim()))
                {
                    InsertKeyWordSearchTable(strcon, txt_Tab_Client.Text.Trim());
                }

                /*if (!string.IsNullOrEmpty(txt_Tab_Project_Description.Text.Trim()))
                {
                    InsertKeyWordSearchTable(strcon, txt_Tab_Project_Description.Text.Trim());
                }

                if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text.Trim()))
                {
                    InsertKeyWordSearchTable(strcon, txt_Tab_Significant_Features.Text.Trim());
                }*/

                if (!string.IsNullOrEmpty(txt_Tab_Keyword.Text.Trim()))
                {
                    InsertKeyWordSearchTable(strcon, txt_Tab_Keyword.Text.Trim());
                }

                /* Insert into Keyword Search Table for Optimised Search Ends */
                /*optional fields*/
                /*Other Matter Description*/
                if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text.Trim()))
                {
                    strOptionaltblCredFields.Append(lbl_Tab_Significant_Features.ID.Substring(8, lbl_Tab_Significant_Features.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(txt_Tab_Significant_Features.Text.Trim().Replace(",", "1@2!"));
					strOptionaltblCredValues.Append("',N'");
                }
                else
                {
                    strOptionaltblCredFields.Append(lbl_Tab_Significant_Features.ID.Substring(8, lbl_Tab_Significant_Features.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(string.Empty);
					strOptionaltblCredValues.Append("',N'");
                }

                /*Keyword*/
                if (!string.IsNullOrEmpty(txt_Tab_Keyword.Text.Trim()))
                {
                    strOptionaltblCredFields.Append(lbl_Tab_Keyword.ID.Substring(8, lbl_Tab_Keyword.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(txt_Tab_Keyword.Text.Trim());
					strOptionaltblCredValues.Append("',N'");
                }
                else
                {
                    strOptionaltblCredFields.Append(lbl_Tab_Keyword.ID.Substring(8, lbl_Tab_Keyword.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(string.Empty);
					strOptionaltblCredValues.Append("',N'");
                }

                /*Actual Date if checked saves in ActualDate_Ongoing else Date_Completed */
                if (chk_Tab_ActualDate_Ongoing.Checked == true)
                {
                    strOptionaltblCredFields.Append(chk_Tab_ActualDate_Ongoing.ID.Substring(8, chk_Tab_ActualDate_Ongoing.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(chk_Tab_ActualDate_Ongoing.Text.Trim());
					strOptionaltblCredValues.Append("',N'");
                }
                else
				{
					if (chk_Tab_ActualDate_Ongoing_1.Checked)
					{
						strOptionaltblCredFields.Append(chk_Tab_ActualDate_Ongoing.ID.Substring(8, chk_Tab_ActualDate_Ongoing.ID.Length - 8));
						strOptionaltblCredFields.Append(",");
						strOptionaltblCredValues.Append(chk_Tab_ActualDate_Ongoing_1.Text.Trim());
						strOptionaltblCredValues.Append("',N'");
					}
					else
					{
						strOptionaltblCredFields.Append(chk_Tab_ActualDate_Ongoing.ID.Substring(8, chk_Tab_ActualDate_Ongoing.ID.Length - 8));
						strOptionaltblCredFields.Append(",");
						strOptionaltblCredValues.Append(string.Empty);
						strOptionaltblCredValues.Append("',N'");

						if (!chk_Tab_ActualDate_Ongoing.Checked && !chk_Tab_ActualDate_Ongoing_1.Checked)
						{
							if (cld_Tab_Date_Completed.SelectedDate.HasValue && cld_Tab_Date_Completed.SelectedDate.HasValue)
							{
								strOptionaltblCredFields.Append(lbl_Tab_Date_Completed.ID.Substring(8, lbl_Tab_Date_Completed.ID.Length - 8));
								strOptionaltblCredFields.Append(",");

								string str = strOptionaltblCredValues.ToString().Substring(0, strOptionaltblCredValues.Length - 2);
								strOptionaltblCredValues.Clear();
								strOptionaltblCredValues.Append(str);
								string strD = cld_Tab_Date_Completed.DateInput.SelectedDate.Value.Day + "/" + cld_Tab_Date_Completed.DateInput.SelectedDate.Value.Month + "/" + cld_Tab_Date_Completed.DateInput.SelectedDate.Value.Year;

								string strPurDate = "convert(datetime|'" + cld_Tab_Date_Completed.DateInput.DisplayText + "'|103)";
								strOptionaltblCredValues.Append(strPurDate);
								strOptionaltblCredValues.Append(",N'");
							}
						}
					}
				}
                /* Project Name */
                if (!string.IsNullOrEmpty(txt_Tab_ProjectName_Core.Text.Trim()))
                {
                    strOptionaltblCredFields.Append(lbl_Tab_ProjectName_Core.ID.Substring(8, lbl_Tab_ProjectName_Core.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(txt_Tab_ProjectName_Core.Text.Trim().Replace(",", "1@2!"));
					strOptionaltblCredValues.Append("',N'");
                }
                else
                {
                    strOptionaltblCredFields.Append(lbl_Tab_ProjectName_Core.ID.Substring(8, lbl_Tab_ProjectName_Core.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(string.Empty);
					strOptionaltblCredValues.Append("',N'");
                }
                if (cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "CONTENTIOUS")
                {
				
                    /*language of disptute*/
                    if (!string.IsNullOrEmpty(txt_Tab_Language_Of_Dispute.Text.Trim()))
                    {
                        string strLanguageOfDispute = hid_Tab_Language_Of_Dispute_Other.Value;
                        if (strLanguageOfDispute.Trim().ToUpper().Contains("OTHER"))
                        {
                            strOptionaltblCredFields.Append(lbl_Tab_Language_Of_Dispute_Other.ID.Substring(8, lbl_Tab_Language_Of_Dispute_Other.ID.Length - 8));
                            strOptionaltblCredFields.Append(",");

                            strOptionaltblCredValues.Append(txt_Tab_Language_Of_Dispute_Other.Text.Trim());
							strOptionaltblCredValues.Append("',N'");
                        }
                        else
                        {
                            strOptionaltblCredFields.Append(lbl_Tab_Language_Of_Dispute_Other.ID.Substring(8, lbl_Tab_Language_Of_Dispute_Other.ID.Length - 8));
                            strOptionaltblCredFields.Append(",");

                            strOptionaltblCredValues.Append(string.Empty);
							strOptionaltblCredValues.Append("',N'");
                        }
                    }
					
                    /*Investment treaty*/
                    if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "ARBITRATION")
                    {
                        if (rdo_Tab_InvestmentTreaty.SelectedIndex != -1)
                        {
                            strOptionaltblCredFields.Append(lbl_Tab_InvestmentTreaty.ID.Substring(8, lbl_Tab_InvestmentTreaty.ID.Length - 8));
                            strOptionaltblCredFields.Append(",");

                            strOptionaltblCredValues.Append(rdo_Tab_InvestmentTreaty.SelectedItem.Value.Trim());
							strOptionaltblCredValues.Append("',N'");
                        }
                        else
                        {
                            strOptionaltblCredFields.Append(lbl_Tab_InvestmentTreaty.ID.Substring(8, lbl_Tab_InvestmentTreaty.ID.Length - 8));
                            strOptionaltblCredFields.Append(",");

                            strOptionaltblCredValues.Append(string.Empty);
							strOptionaltblCredValues.Append("',N'");
                        }
                    }

                    /*Investigation*/
                    if (cbo_Tab_Dispute_Resolution.SelectedItem.Text.Trim().ToUpper() == "INVESTIGATION")
                    {
                        if (cbo_Tab_Investigation_Type.SelectedItem.Value != "-1")
                        {
                            strOptionaltblCredFields.Append(lbl_Tab_Investigation_Type.ID.Substring(8, lbl_Tab_Investigation_Type.ID.Length - 8));
                            strOptionaltblCredFields.Append(",");

                            strOptionaltblCredValues.Append(cbo_Tab_Investigation_Type.SelectedItem.Value.Trim());
							strOptionaltblCredValues.Append("',N'");
                        }
                        else
                        {
                            strOptionaltblCredFields.Append(lbl_Tab_Investigation_Type.ID.Substring(8, lbl_Tab_Investigation_Type.ID.Length - 8));
                            strOptionaltblCredFields.Append(",");

                            strOptionaltblCredValues.Append(string.Empty);
							strOptionaltblCredValues.Append("',N'");
                        }
                    }
                }

                /*Lead CMS Firms*/
                if (cbo_Tab_Lead_CMS_Firm.SelectedItem.Value != "-1")
                {
                    strOptionaltblCredFields.Append(lbl_Tab_Lead_CMS_Firm.ID.Substring(8, lbl_Tab_Lead_CMS_Firm.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(cbo_Tab_Lead_CMS_Firm.SelectedItem.Value.Trim());
					strOptionaltblCredValues.Append("',N'");
                }
                else
                {
                    strOptionaltblCredFields.Append(lbl_Tab_Lead_CMS_Firm.ID.Substring(8, lbl_Tab_Lead_CMS_Firm.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(string.Empty);
					strOptionaltblCredValues.Append("',N'");
                }

                /*Pro Bono*/
                if (rdo_Tab_ProBono.SelectedIndex != -1)
                {
                    strOptionaltblCredFields.Append(lbl_Tab_ProBono.ID.Substring(8, lbl_Tab_ProBono.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(rdo_Tab_ProBono.SelectedItem.Value.Trim());
					strOptionaltblCredValues.Append("',N'");
                }
                
                /*Bible Reference*/
                if (!string.IsNullOrEmpty(txt_Tab_Bible_Reference.Text.Trim()))
                {
                    strOptionaltblCredFields.Append(lbl_Tab_Bible_Reference.ID.Substring(8, txt_Tab_Bible_Reference.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(txt_Tab_Bible_Reference.Text.Trim().Replace(",", "1@2!"));
					strOptionaltblCredValues.Append("',N'");
                }
                else
                {
                    strOptionaltblCredFields.Append(lbl_Tab_Bible_Reference.ID.Substring(8, txt_Tab_Bible_Reference.ID.Length - 8));
                    strOptionaltblCredFields.Append(",");

                    strOptionaltblCredValues.Append(string.Empty);
					strOptionaltblCredValues.Append("',N'");
                }

                //  SaveCredentials(strtblCredValues, strtblCredFields);
                /*BAIF*/
                obj.LogWriter("BAIF Values Fetched from Session Starts");

                if (Session["sessBAIFSS"] != null)
                {
                    if (Session["sessBAIFSS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessBAIFSS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "ClientTypeIdBAIF":
                                            //cbo_BAI_ClientTypeIdBAIF.SelectedItem.Text = strvals[i].Split('~')[1];
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strBAIFtblCredFields.Append(",");

                                                strBAIFtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strBAIFtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strBAIFtblCredFields.Append(",");

                                                strBAIFtblCredValues.Append(string.Empty);
                                                strBAIFtblCredValues.Append("',N'");

                                            }

                                            break;
                                        case "LeadBanks":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strBAIFtblCredFields.Append(",");

                                                strBAIFtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strBAIFtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strBAIFtblCredFields.Append(",");

                                                strBAIFtblCredValues.Append(string.Empty);
                                                strBAIFtblCredValues.Append("',N'");

                                            }
                                            break;
                                        /* case "Transaction_Value":
                                             // cbo_BAI_ClientTypeIdBAIF.SelectedItem.Text = strvals[i].Split('~')[1];
                                             if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                             {
                                                 strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                 strBAIFtblCredFields.Append(",");

                                                 strBAIFtblCredValues.Append(strvals[i].Split('~')[1]);
                                                 strBAIFtblCredValues.Append("','");
                                             }
                                             else
                                             {
                                                 strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                 strBAIFtblCredFields.Append(",");

                                                 strBAIFtblCredValues.Append(string.Empty);
                                                 strBAIFtblCredValues.Append("','");

                                             }
                                             break;*/
                                        case "WorkTypeMS":
                                            // txt_BAI_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                obj.LogWriter("BAIF Values Fetched from Session Stops");
                obj.LogWriter("Corporate Values Fetched from Session Starts");
				
                if (Session["sessCORPSS"] != null)
                {
                    if (Session["sessCORPSS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessCORPSS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "WorkTypeMS":
                                            //txt_Cor_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "SubWorkTypeMS":
                                            //txt_Cor_SubWork_Type.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "ActingForMS":
                                            // txt_Cor_Acting_For.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "Country_BuyerMS":
                                            //objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Country_Buyer);
                                            // txt_Cor_Country_Buyer.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "Country_SellerMS":
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Country_Seller);
                                            // txt_Cor_Country_Seller.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "Country_TargetMS":
                                            //objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Country_Target);
                                            // txt_Cor_Country_Target.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "ValueRangeEuro":
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_ValueRangeEuro);
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
												strCorptblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(string.Empty);
												strCorptblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "Published_Reference":
                                            //txt_Cor_Published_Reference.Text = strvals[i].Split('~')[1].ToString();
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
												strCorptblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(string.Empty);
												strCorptblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "MAStudy":
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_MAStudy);
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
												strCorptblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(string.Empty);
												strCorptblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "PEClients":
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_PEClients);
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(string.Empty);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "QuarterDealAnnouncedId":
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_QuarterDealAnnouncedId);
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(string.Empty);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "QuarterDealCompletedId":
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_QuarterDealCompletedId);
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(string.Empty);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "YearDeal_Announced":
                                            // txt_Cor_YearDeal_Announced.Text = strvals[i].Split('~')[1].ToString();
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(string.Empty);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "YearDealCompletedId":
                                            //  objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_YearDealCompletedId);
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorptblCredFields.Append(",");

                                                strCorptblCredValues.Append(string.Empty);
                                                strCorptblCredValues.Append("',N'");
                                            }
                                            break;
										case "DealAnnouncedId":
											string strCorp = strCorptblCredValues.ToString().Substring(0, strCorptblCredValues.Length - 2);
											strCorptblCredValues.Clear();
											strCorptblCredValues.Append(strCorp);
											string strDealDate = "convert(datetime|'" + strvals[i].Split('~')[1].Trim() + "'|103)";
											if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
											{
												strCorptblCredFields.Append(strvals[i].Split('~')[0]);
												strCorptblCredFields.Append(",");
												strCorptblCredValues.Append(strDealDate);
												strCorptblCredValues.Append(",'");
											}
											break;
									}
								}
							}
						}
					}
				}
				
                obj.LogWriter("Corporate Values Fetched from Session Starts");
                obj.LogWriter("CRD Values Fetched from Session Starts");
				
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
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_CRD_ClientTypeIdCommercial);
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCRDtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCRDtblCredFields.Append(",");

                                                strCRDtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strCRDtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCRDtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCRDtblCredFields.Append(",");

                                                strCRDtblCredValues.Append(string.Empty);
                                                strCRDtblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "WorkTypeMS":
                                            // txt_CRD_Work_Type.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "SubWorkTypeMS":
                                            // txt_CRD_SubWork_Type.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("CRD Values Fetched from Session Ends");
                obj.LogWriter("EPC Construction Values Fetched from Session Starts");
				
                if (Session["sessEPCSS"] != null)
                {
                    if (Session["sessEPCSS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessEPCSS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "WorkTypeMS":
                                            // txt_EPC_Nature_Of_Work.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "TypeOfContractMS":
                                            // txt_EPC_Type_Of_Contract.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "ClientScopeId":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(string.Empty);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "Type_Of_Contract_Other":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(string.Empty);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "SubjectMatterId":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(string.Empty);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "SubjectMatterOther":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(string.Empty);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "ClientTypeIdEPC":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(string.Empty);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            break;
                                        case "ClientTypeOther":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCtblCredFields.Append(",");

                                                strEPCtblCredValues.Append(string.Empty);
                                                strEPCtblCredValues.Append("',N'");
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("EPC Construction Values Fetched from Session Ends");
                obj.LogWriter("EPC Energy Values Fetched from Session Starts");
				
                if (Session["sessEPCESS"] != null)
                {
                    if (Session["sessEPCESS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessEPCESS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "ProjectSectorMS":
                                            // txt_ENE_EPC_Project_Sector.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "TransactionTypeMS":
                                            //txt_ENE_Transaction_Type.Text = strvals[i].Split('~')[1].ToString();
                                            break;
                                        case "ContractTypeId":
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_ENE_ContractTypeId);
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strEPCEnetblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCEnetblCredFields.Append(",");

                                                strEPCEnetblCredValues.Append(strvals[i].Split('~')[1]);
                                                strEPCEnetblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strEPCEnetblCredFields.Append(strvals[i].Split('~')[0]);
                                                strEPCEnetblCredFields.Append(",");

                                                strEPCEnetblCredValues.Append(string.Empty);
                                                strEPCEnetblCredValues.Append("',N'");
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("EPC Energy Values Fetched from Session Ends");
                obj.LogWriter("HC Values Fetched from Session Starts");
				
                if (Session["sessHCSS"] != null)
                {
                    if (Session["sessHCSS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessHCSS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "WorkTypeIdHC":
                                            /*  if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                              {
                                                  strHCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                  strHCtblCredFields.Append(",");

                                                  strHCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                  strHCtblCredValues.Append("','");
                                              }
                                              else
                                              {
                                                  strHCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                  strHCtblCredFields.Append(",");

                                                  strHCtblCredValues.Append(string.Empty);
                                                  strHCtblCredValues.Append("','");
                                              }
                                              //objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_HCC_WorkTypeIdHC);*/
                                            break;
                                        case "PensionSchemeHC":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strHCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strHCtblCredFields.Append(",");

                                                strHCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strHCtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strHCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strHCtblCredFields.Append(",");

                                                strHCtblCredValues.Append(string.Empty);
                                                strHCtblCredValues.Append("',N'");
                                            }
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_HCC_PensionSchemeHC);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("EPC Energy Values Fetched from Session Ends");
                obj.LogWriter("IPF Values Fetched from Session Starts");
				
                if (Session["sessIPFSS"] != null)
                {
                    if (Session["sessIPFSS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessIPFSS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {

                                        case "ClientTypeIdIPF":
                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strIPFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strIPFtblCredFields.Append(",");

                                                strIPFtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strIPFtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strIPFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strIPFtblCredFields.Append(",");

                                                strIPFtblCredValues.Append(string.Empty);
                                                strIPFtblCredValues.Append("',N'");
                                            }
                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_IPF_ClientTypeIdIPF);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("IPF Values Fetched from Session Ends");
                obj.LogWriter("CorpTax Values Fetched from Session Start");
				
                if (Session["sessCorpTaxSS"] != null)
                {
                    if (Session["sessCorpTaxSS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessCorpTaxSS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "WorkType_CorpTax":

                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                            {
                                                strCorpTaxtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorpTaxtblCredFields.Append(",");

                                                strCorpTaxtblCredValues.Append(strvals[i].Split('~')[1]);
                                                strCorpTaxtblCredValues.Append("',N'");
                                            }
                                            else
                                            {
                                                strCorpTaxtblCredFields.Append(strvals[i].Split('~')[0]);
                                                strCorpTaxtblCredFields.Append(",");

                                                strCorpTaxtblCredValues.Append(string.Empty);
                                                strCorpTaxtblCredValues.Append("',N'");
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("CorpTax Values Fetched from Session Ends");

                if (strtblCredFields != null && strtblCredValues != null)
                {
                    strquerycols.Append(strtblCredFields);
                    strqueryvals.Append(strtblCredValues);
                }
                if (strOptionaltblCredFields != null && strOptionaltblCredValues != null)
                {
                    strquerycols.Append(strOptionaltblCredFields);
                    strqueryvals.Append(strOptionaltblCredValues);
                }
                if (Session["sessBAIFSS"] != null)
                {
                    strquerycols.Append(strBAIFtblCredFields);
                    strqueryvals.Append(strBAIFtblCredValues);
                }
                if (Session["sessCORPSS"] != null)
                {
                    strquerycols.Append(strCorptblCredFields);
                    strqueryvals.Append(strCorptblCredValues);
                }
                if (Session["sessCRDSS"] != null)
                {
                    strquerycols.Append(strCRDtblCredFields);
                    strqueryvals.Append(strCRDtblCredValues);
                }
                if (Session["sessEPCSS"] != null)
                {
                    strquerycols.Append(strEPCtblCredFields);
                    strqueryvals.Append(strEPCtblCredValues);
                }
                if (Session["sessEPCESS"] != null)
                {
                    strquerycols.Append(strEPCEnetblCredFields);
                    strqueryvals.Append(strEPCEnetblCredValues);
                }
                if (Session["sessHCSS"] != null)
                {
                    strquerycols.Append(strHCtblCredFields);
                    strqueryvals.Append(strHCtblCredValues);
                }
                if (Session["sessIPFSS"] != null)
                {
                    strquerycols.Append(strIPFtblCredFields);
                    strqueryvals.Append(strIPFtblCredValues);
                }
                if (Session["sessRESS"] != null)
                {
                    strquerycols.Append(strREtblCredFields);
                    strqueryvals.Append(strREtblCredValues);
                }
                if (Session["sessCorpTaxSS"] != null)
                {
                    strquerycols.Append(strCorpTaxtblCredFields);
                    strqueryvals.Append(strCorpTaxtblCredValues);
                }
                //strquerycols.ToString().Substring(0, strquerycols.ToString().Length - 1);
                //strqueryvals.ToString().Substring(0, strqueryvals.ToString().Length - 2);
                obj.LogWriter("Save in tblCredential Starts");

                obj.LogWriter(strqueryvals.ToString());
                obj.LogWriter(strquerycols.ToString());

                UpdateCredentials(strqueryvals, strquerycols, hidCredentialID.Value);
                int iCredentialID = Convert.ToInt32(hidCredentialID.Value);

                obj.LogWriter("Save in tblCredential Stops");

                /*sector group*/
                obj.LogWriter("Multiselect query -save Starts");
                if (chKBAIF.Checked == true)
                {
                    strPrac.Append(hid_BAIF.Value.Trim());
                    strPrac.Append(",");
                }
                if (chKCRD.Checked == true)
                {
                    strPrac.Append(hid_CRD.Value.Trim());
                    strPrac.Append(",");
                }
                if (chKCorp.Checked == true)
                {
                    strPrac.Append(hid_Corp.Value.Trim());
                    strPrac.Append(",");
                }
                if (chkEPC.Checked == true)
                {
                    strPrac.Append(hid_EPC.Value.Trim());
                    strPrac.Append(",");
                }
                if (chkEPCE.Checked == true)
                {
                    strPrac.Append(hid_EPCE.Value.Trim());
                    strPrac.Append(",");
                }
                if (chkHC.Checked == true)
                {
                    strPrac.Append(hid_HC.Value.Trim());
                    strPrac.Append(",");
                }
                if (chkIPF.Checked == true)
                {
                    strPrac.Append(hid_IPF.Value.Trim());
                    strPrac.Append(",");
                }
                if (chkRE.Checked == true)
                {
                    strPrac.Append(hid_RE.Value.Trim());
                    strPrac.Append(",");
                }
                if (chkCorpTax.Checked == true)
                {
                    strPrac.Append(hid_CorpTax.Value.Trim());
                    strPrac.Append(",");
                }
				
				strMainDelete = DeleteAllMultiSelectValues(strMainDelete, iCredentialID);
				
				if (!string.IsNullOrEmpty(strPrac.ToString()))
				{
					string strPracGrpId = strPrac.ToString().Trim().Substring(0, strPrac.Length - 1);
					if (strPracGrpId.Split(',').Length > 0)
					{
						for (int i = 0; i < strPracGrpId.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strPracGrpId.Split(',')[i].ToString(), "BusinessGroupId", "tblCredentialBusinessGroup", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

				string strSectorGroupMS = string.Empty;
				strSectorGroupMS = hid_Tab_ClientIndustrySector.Value;
				if (!string.IsNullOrEmpty(strSectorGroupMS))
				{
					if (strSectorGroupMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strSectorGroupMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strSectorGroupMS.Split(',')[i].ToString(), "ClientIndustrySectorId", "tblCredentialClientIndustrySector", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /*predominant country of client*/
				string strCountryOfClientMS = string.Empty;
				strCountryOfClientMS = hid_Tab_Country_PredominantCountry.Value;
				if (!string.IsNullOrEmpty(strCountryOfClientMS))
				{
					if (strCountryOfClientMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strCountryOfClientMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strCountryOfClientMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryClient", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /*Sector Group*/
				string strMatterSectorGroupMS = string.Empty;
				strMatterSectorGroupMS = hid_Tab_TransactionIndustrySector.Value;
				if (!string.IsNullOrEmpty(strMatterSectorGroupMS))
				{
					if (strMatterSectorGroupMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strMatterSectorGroupMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strMatterSectorGroupMS.Split(',')[i].ToString(), "TransactionIndustrySectorId", "tblCredentialTransactionIndustrySector", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /*Country where opened*/
				string strCountryWhereOpenedMS = string.Empty;
				strCountryWhereOpenedMS = hid_Tab_Country_Matter_Open.Value;
				if (!string.IsNullOrEmpty(strCountryWhereOpenedMS))
				{
					if (strCountryWhereOpenedMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strCountryWhereOpenedMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strCountryWhereOpenedMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryMatterOpen", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /*stCountry of Transaction*/
				string strCountryofTransactionMS = string.Empty;
				strCountryofTransactionMS = hid_Tab_Country_Matter_Close.Value;
				if (!string.IsNullOrEmpty(strCountryofTransactionMS))
				{
					if (strCountryofTransactionMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strCountryofTransactionMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strCountryofTransactionMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryMatterClose", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}
				
                /*neena*/
                /* Jurisdiction Of Dispute */
				if (cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.SelectedItem.Text.Trim().ToUpper() == "CONTENTIOUS")
				{
					string stJurisdictionOfDisputeMS = string.Empty;
					stJurisdictionOfDisputeMS = hid_Tab_Country_Jurisdiction.Value;
					if (!string.IsNullOrEmpty(stJurisdictionOfDisputeMS))
					{
						if (stJurisdictionOfDisputeMS.Split(',').Length > 0)
						{
							for (int i = 0; i < stJurisdictionOfDisputeMS.Split(',').Length; i++)
							{
								string strOP = InsertingMultiSelectValues(stJurisdictionOfDisputeMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryJurisdiction", iCredentialID);
								if (string.IsNullOrEmpty(strMainInsert))
								{
									strMainInsert = strOP;
								}
								else
								{
									strMainInsert = strMainInsert + "|" + strOP;
								}
							}
						}
					}

					string stLanguageOfDisputeMS = string.Empty;
					stLanguageOfDisputeMS = hid_Tab_Language_Of_Dispute.Value;
					if (!string.IsNullOrEmpty(stLanguageOfDisputeMS))
					{
						if (stLanguageOfDisputeMS.Split(',').Length > 0)
						{
							for (int i = 0; i < stLanguageOfDisputeMS.Split(',').Length; i++)
							{
								string strOP = InsertingMultiSelectValues(stLanguageOfDisputeMS.Split(',')[i].ToString(), "LanguageOfDisputeId", "tblCredentialLanguageOfDispute", iCredentialID);
								if (string.IsNullOrEmpty(strMainInsert))
								{
									strMainInsert = strOP;
								}
								else
								{
									strMainInsert = strMainInsert + "|" + strOP;
								}
							}
						}
					}
				}

                /*Counrty Of Arbitration*/
				string strCountryofArbitrationMS = string.Empty;
				strCountryofArbitrationMS = hid_Tab_Country_ArbitrationCountry.Value;
				if (!string.IsNullOrEmpty(strCountryofArbitrationMS))
				{
					if (strCountryofArbitrationMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strCountryofArbitrationMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strCountryofArbitrationMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryArbitrationCountry", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /*Lead Partner*/
				string strLeadPartnerMS = string.Empty;
				strLeadPartnerMS = hid_Tab_Lead_Partner.Value.ToString().ToUpper();
				if (!string.IsNullOrEmpty(strLeadPartnerMS))
				{
					if (strLeadPartnerMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strLeadPartnerMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strLeadPartnerMS.Split(',')[i].ToString(), "LeadPartnerId", "tblCredentialLeadPartner", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /* Source Of Credential */
				if (strLead.Contains("CMS PARTNER"))
				{
					string strSourceOfCredentialMS = string.Empty;
					strSourceOfCredentialMS = hid_Tab_Source_Of_Credential.Value;
					if (!string.IsNullOrEmpty(strSourceOfCredentialMS))
					{
						if (strSourceOfCredentialMS.Split(',').Length > 0)
						{
							for (int i = 0; i < strSourceOfCredentialMS.Split(',').Length; i++)
							{
								string strOP = InsertingMultiSelectValues(strSourceOfCredentialMS.Split(',')[i].ToString(), "SourceOfCredentialId", "tblCredentialSourceOfCredential", iCredentialID);
								if (string.IsNullOrEmpty(strMainInsert))
								{
									strMainInsert = strOP;
								}
								else
								{
									strMainInsert = strMainInsert + "|" + strOP;
								}
							}
						}
					}
				}

                /* Other Matter Executive */
				string strOtherMatterExecutiveMS = string.Empty;
				strOtherMatterExecutiveMS = hid_Tab_Other_Matter_Executive.Value;
				if (!string.IsNullOrEmpty(strOtherMatterExecutiveMS))
				{
					if (strOtherMatterExecutiveMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strOtherMatterExecutiveMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strOtherMatterExecutiveMS.Split(',')[i].ToString(), "OtherMatterExecutiveId", "tblCredentialOtherMatterExecutive", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /*strOptionaltblCredValues/Client Sub-Sector Group*/
				if (!string.IsNullOrEmpty(hid_Tab_Client_Industry_Type.Value.Trim()))
				{
					string strSubSectorGroupMS = string.Empty;
					strSubSectorGroupMS = hid_Tab_Client_Industry_Type.Value;
					if (!string.IsNullOrEmpty(strSubSectorGroupMS))
					{
						if (strSubSectorGroupMS.Split(',').Length > 0)
						{
							for (int i = 0; i < strSubSectorGroupMS.Split(',').Length; i++)
							{
								string strOP = InsertingMultiSelectValues(strSubSectorGroupMS.Split(',')[i].ToString(), "ClientIndustryTypeId", "tblCredentialClientIndustryType", iCredentialID);
								if (string.IsNullOrEmpty(strMainInsert))
								{
									strMainInsert = strOP;
								}
								else
								{
									strMainInsert = strMainInsert + "|" + strOP;
								}
							}
						}
					}
				}

                /* Matter Sub-Sector Group */
				if (!string.IsNullOrEmpty(hid_Tab_Transaction_Industry_Type.Value.Trim()))
				{
					string strMatterSubSectorGroupMS = string.Empty;
					strMatterSubSectorGroupMS = hid_Tab_Transaction_Industry_Type.Value;
					if (strMatterSubSectorGroupMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strMatterSubSectorGroupMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strMatterSubSectorGroupMS.Split(',')[i].ToString(), "TransactionIndustryTypeId", "tblCredentialTransactionIndustryType", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /* Teams */
				if (!string.IsNullOrEmpty(hid_Tab_Team.Value.Trim()))
				{
					string strTeamMS = string.Empty;
					strTeamMS = hid_Tab_Team.Value;
					if (strTeamMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strTeamMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strTeamMS.Split(',')[i].ToString(), "TeamId", "tblCredentialTeam", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /* CMS Firms Involved */
				if (!string.IsNullOrEmpty(txt_Tab_Referred_From_Other_CMS_Office.Text.Trim()))
				{
					string strCMSFirmsInvolvedMS = string.Empty;
					strCMSFirmsInvolvedMS = hid_Tab_Referred_From_Other_CMS_Office.Value;
					if (strCMSFirmsInvolvedMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strCMSFirmsInvolvedMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strCMSFirmsInvolvedMS.Split(',')[i].ToString(), "ReferredFromOtherCMSOfficeId", "tblCredentialReferredFromOtherCMSOffice", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}
				
                /* Other CMS Firms Involved */
				if (!string.IsNullOrEmpty(txt_Tab_Country_OtherCMSOffice.Text.Trim()))
				{
					string strCountryOtherCMSOfficeMS = string.Empty;
					strCountryOtherCMSOfficeMS = hid_Tab_Country_OtherCMSOffice.Value;
					if (strCountryOtherCMSOfficeMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strCountryOtherCMSOfficeMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strCountryOtherCMSOfficeMS.Split(',')[i].ToString(), "CountryId", "tblCredentialCountryOtherCMSOffice", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /* Other Uses */
				if (!string.IsNullOrEmpty(txt_Tab_Other_Uses.Text.Trim()))
				{
					string strOtherUsesMS = string.Empty;
					strOtherUsesMS = hid_Tab_Other_Uses.Value;
					if (strOtherUsesMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strOtherUsesMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strOtherUsesMS.Split(',')[i].ToString(), "OtherUsesId", "tblCredentialOtherUses", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                /*Know how */
				if (!string.IsNullOrEmpty(txt_Tab_Know_How.Text.Trim()))
				{
					string strKnowHowMS = string.Empty;
					strKnowHowMS = hid_Tab_Know_How.Value;
					if (strKnowHowMS.Split(',').Length > 0)
					{
						for (int i = 0; i < strKnowHowMS.Split(',').Length; i++)
						{
							string strOP = InsertingMultiSelectValues(strKnowHowMS.Split(',')[i].ToString(), "KnowHowId", "tblCredentialKnowHow", iCredentialID);
							if (string.IsNullOrEmpty(strMainInsert))
							{
								strMainInsert = strOP;
							}
							else
							{
								strMainInsert = strMainInsert + "|" + strOP;
							}
						}
					}
				}

                obj.LogWriter("Multiselect query -save Stops");
                obj.LogWriter("Multiselect BAIF query -save Starts");
				
                if (Session["sessBAIFMS"] != null)
                {
                    if (Session["sessBAIFMS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessBAIFMS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "Work_Type":
                                            string strWorkTypeBAIFMS = string.Empty;
                                            strWorkTypeBAIFMS = strvals[i].Split('~')[1];
                                            if (strWorkTypeBAIFMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strWorkTypeBAIFMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strWorkTypeBAIFMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkTypeBAIF", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                obj.LogWriter("Multiselect BAIF query -save Stops");
                obj.LogWriter("Multiselect CORP query -save Starts");

                if (Session["sessCORPMS"] != null)
                {
                    if (Session["sessCORPMS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessCORPMS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "Work_Type":
                                            string strWorkTypeCorpMS = string.Empty;
                                            strWorkTypeCorpMS = strvals[i].Split('~')[1];
                                            if (strWorkTypeCorpMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strWorkTypeCorpMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strWorkTypeCorpMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkType", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "Acting_For":
                                            string strActingForCorpMS = string.Empty;
                                            strActingForCorpMS = strvals[i].Split('~')[1];
                                            if (strActingForCorpMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strActingForCorpMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strActingForCorpMS.Split(',')[k].ToString(), "ActingForId", "tblCredentialActingFor", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "SubWork_Type":
                                            string strSubWork_TypeCorpMS = string.Empty;
                                            strSubWork_TypeCorpMS = strvals[i].Split('~')[1];
                                            if (strSubWork_TypeCorpMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strSubWork_TypeCorpMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strSubWork_TypeCorpMS.Split(',')[k].ToString(), "SubWorkTypeId", "tblCredentialSubWorkType", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "Country_Buyer":
                                            string strCountryBuyerCorpMS = string.Empty;
                                            strCountryBuyerCorpMS = strvals[i].Split('~')[1];
                                            if (strCountryBuyerCorpMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strCountryBuyerCorpMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strCountryBuyerCorpMS.Split(',')[k].ToString(), "CountryId", "tblCredentialCountryBuyer", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "Country_Seller":
                                            string strCountrySellerCorpMS = string.Empty;
                                            strCountrySellerCorpMS = strvals[i].Split('~')[1];
                                            if (strCountrySellerCorpMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strCountrySellerCorpMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strCountrySellerCorpMS.Split(',')[k].ToString(), "CountryId", "tblCredentialCountrySeller", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
										case "Country_Target":
											string strCountryTargetCorpMS = string.Empty;
											strCountryTargetCorpMS = strvals[i].Split('~')[1];
											if (strCountryTargetCorpMS.Split(',').Length > 0)
											{
												for (int k = 0; k < strCountryTargetCorpMS.Split(',').Length; k++)
												{
													string strOP = InsertingMultiSelectValues(strCountryTargetCorpMS.Split(',')[k].ToString(), "CountryId", "tblCredentialCountryTarget", iCredentialID);
													if (string.IsNullOrEmpty(strMainInsert))
													{
														strMainInsert = strOP;
													}
													else
													{
														strMainInsert = strMainInsert + "|" + strOP;
													}
												}
											}
											break;
										case "Value_Over_Pound_MS":
											string strValue_Over_Pound_MS = string.Empty;
											strValue_Over_Pound_MS = strvals[i].Split('~')[1];
											if (strValue_Over_Pound_MS.Split(',').Length > 0)
											{
												for (int k = 0; k < strValue_Over_Pound_MS.Split(',').Length; k++)
												{
													string strOP = InsertingMultiSelectValues(strValue_Over_Pound_MS.Split(',')[k].ToString(), "YesNoId", "tblCredentialPoundValue", iCredentialID);
													if (string.IsNullOrEmpty(strMainInsert))
													{
														strMainInsert = strOP;
													}
													else
													{
														strMainInsert = strMainInsert + "|" + strOP;
													}
												}
											}
											break;
										case "Value_Over_US_MS":
											string strValue_Over_US_MS = string.Empty;
											strValue_Over_US_MS = strvals[i].Split('~')[1];
											if (strValue_Over_US_MS.Split(',').Length > 0)
											{
												for (int k = 0; k < strValue_Over_US_MS.Split(',').Length; k++)
												{
													string strOP = InsertingMultiSelectValues(strValue_Over_US_MS.Split(',')[k].ToString(), "YesNoId", "tblCredentialDollarValue", iCredentialID);
													if (string.IsNullOrEmpty(strMainInsert))
													{
														strMainInsert = strOP;
													}
													else
													{
														strMainInsert = strMainInsert + "|" + strOP;
													}
												}
											}
											break;
										case "Value_Over_Euro_MS":
											string strValue_Over_Euro_MS = string.Empty;
											strValue_Over_Euro_MS = strvals[i].Split('~')[1];
											if (strValue_Over_Euro_MS.Split(',').Length > 0)
											{
												for (int k = 0; k < strValue_Over_Euro_MS.Split(',').Length; k++)
												{
													string strOP = InsertingMultiSelectValues(strValue_Over_Euro_MS.Split(',')[k].ToString(), "YesNoId", "tblCredentialEuroValue", iCredentialID);
	                                                    if (string.IsNullOrEmpty(strMainInsert))
	                                                    {
	                                                        strMainInsert = strOP;
	                                                    }
	                                                    else
	                                                    {
	                                                        strMainInsert = strMainInsert + "|" + strOP;
	                                                    }
	                                                }
	                                            }
	                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("Multiselect CORP query -save Stops");
                obj.LogWriter("Multiselect CRD query -save Starts");
				
                if (Session["sessCRDMS"] != null)
                {
                    if (Session["sessCRDMS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessCRDMS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "Work_Type":
                                            string strWorkTypeCRDMS = string.Empty;
                                            strWorkTypeCRDMS = strvals[i].Split('~')[1];
                                            if (strWorkTypeCRDMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strWorkTypeCRDMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strWorkTypeCRDMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkTypeCRD", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "SubWork_Type":
                                            string strSubWorkTypeCRDMS = string.Empty;
                                            strSubWorkTypeCRDMS = strvals[i].Split('~')[1];
                                            if (strSubWorkTypeCRDMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strSubWorkTypeCRDMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strSubWorkTypeCRDMS.Split(',')[k].ToString(), "SubWorkTypeId", "tblCredentialSubWorkTypeCommercial", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("Multiselect CRD query -save Stops");
                obj.LogWriter("Multiselect EPC Const query -save Starts");
				
                if (Session["sessEPCMS"] != null)
                {
                    if (Session["sessEPCMS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessEPCMS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "Nature_Of_Work":
                                            string strNature_Of_WorkEPCMS = string.Empty;
                                            strNature_Of_WorkEPCMS = strvals[i].Split('~')[1];
                                            if (strNature_Of_WorkEPCMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strNature_Of_WorkEPCMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strNature_Of_WorkEPCMS.Split(',')[k].ToString(), "NatureWorkId", "tblCredentialNatureWork", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "Type_Of_Contract":
                                            string strTypeOfContractEPCMS = string.Empty;
                                            strTypeOfContractEPCMS = strvals[i].Split('~')[1];
                                            if (strTypeOfContractEPCMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strTypeOfContractEPCMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strTypeOfContractEPCMS.Split(',')[k].ToString(), "TypeContractId", "tblCredentialTypeOfContract", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("Multiselect EPC Const query -save Stops");
                obj.LogWriter("Multiselect EPC Energy query -save Starts");
				
                if (Session["sessEPCEMS"] != null)
                {
                    if (Session["sessEPCEMS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessEPCEMS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "EPC_Project_Sector":
                                            string strEPCProjectSectorEPCMS = string.Empty;
                                            strEPCProjectSectorEPCMS = strvals[i].Split('~')[1];
                                            if (strEPCProjectSectorEPCMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strEPCProjectSectorEPCMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strEPCProjectSectorEPCMS.Split(',')[k].ToString(), "EPCProjectSectorId", "tblCredentialEPCProjectSector", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "Transaction_Type":
                                            string strTransactionTypeEPCEMS = string.Empty;
                                            strTransactionTypeEPCEMS = strvals[i].Split('~')[1];
                                            if (strTransactionTypeEPCEMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strTransactionTypeEPCEMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strTransactionTypeEPCEMS.Split(',')[k].ToString(), "TransactionTypeId", "tblCredentialTransactionType", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            break;

                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("Multiselect EPC Energy query -save Stops");
                obj.LogWriter("Multiselect IPF query -save Starts");
				
                if (Session["sessIPFMS"] != null)
                {
                    if (Session["sessIPFMS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessIPFMS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {
                                        case "Project_Sector":
                                            string strProject_SectorIPFMS = string.Empty;
                                            strProject_SectorIPFMS = strvals[i].Split('~')[1];
                                            if (strProject_SectorIPFMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strProject_SectorIPFMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strProject_SectorIPFMS.Split(',')[k].ToString(), "ProjectSectorId", "tblCredentialProjectSector", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("Multiselect IPF query -save Stops");
                obj.LogWriter("Multiselect RE query -save Starts");

                if (Session["sessREMS"] != null)
                {
                    if (Session["sessREMS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessREMS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {

                                        case "Client_Type":
                                            string strClientTypeREMS = string.Empty;
                                            strClientTypeREMS = strvals[i].Split('~')[1];
                                            if (strClientTypeREMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strClientTypeREMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strClientTypeREMS.Split(',')[k].ToString(), "ClientTypeId", "tblCredentialClientTypeRealEstate", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "Work_Type":
                                            string strWork_TypeREMS = string.Empty;
                                            strWork_TypeREMS = strvals[i].Split('~')[1];
                                            if (strWork_TypeREMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strWork_TypeREMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strWork_TypeREMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkTypeRealEstate", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        case "SubWork_Type":
                                            string strSubWork_TypeREMS = string.Empty;
                                            strSubWork_TypeREMS = strvals[i].Split('~')[1];
                                            if (strSubWork_TypeREMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strSubWork_TypeREMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strSubWork_TypeREMS.Split(',')[k].ToString(), "SubWorkTypeId", "tblCredentialSubWorkTypeRE", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
				
                obj.LogWriter("Multiselect RE query -save Stops");
                obj.LogWriter("Multiselect HC query -save Starts");

                if (Session["sessHCMS"] != null)
                {
                    if (Session["sessHCMS"].ToString().Split('|').Length > 0)
                    {
                        string[] strvals = Session["sessHCMS"].ToString().Split('|');

                        for (int i = 0; i < strvals.Length; i++)
                        {
                            if (strvals[i].Split('~').Length > 0)
                            {
                                string str = string.Empty;

                                for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                {
                                    switch (strvals[i].Split('~')[0].ToString())
                                    {

                                        case "WorkTypeIdHC":
                                            string strWork_TypeHCMS = string.Empty;
                                            strWork_TypeHCMS = strvals[i].Split('~')[1];
                                            if (strWork_TypeHCMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strWork_TypeHCMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strWork_TypeHCMS.Split(',')[k].ToString(), "WorkTypeId", "tblCredentialWorkTypeHC", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        //SubWork_Type
                                        case "SubWork_Type":
                                            string strSubWork_TypeHCMS = string.Empty;
                                            strSubWork_TypeHCMS = strvals[i].Split('~')[1];
                                            if (strSubWork_TypeHCMS.Split(',').Length > 0)
                                            {
                                                for (int k = 0; k < strSubWork_TypeHCMS.Split(',').Length; k++)
                                                {
                                                    string strOP = InsertingMultiSelectValues(strSubWork_TypeHCMS.Split(',')[k].ToString(), "SubWorkTypeId", "tblCredentialSubWorkTypeHC", iCredentialID);

                                                    if (string.IsNullOrEmpty(strMainInsert))
                                                    {
                                                        strMainInsert = strOP;
                                                    }
                                                    else
                                                    {
                                                        strMainInsert = strMainInsert + "|" + strOP;
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                obj.LogWriter("Multiselect HC query -save Stops");
                obj.LogWriter("Multiselect save in DB Starts");
				
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.Transaction = transaction;

                obj.LogWriter("MultiDelete in DB Starts");
                if (!string.IsNullOrEmpty(strMainDelete))
                {
                    for (int i = 0; i < strMainDelete.Split('|').Length; i++)
                    {
                        cmd.CommandText = strMainDelete.Split('|')[i].ToString();
                        int j = cmd.ExecuteNonQuery();
                    }
                }
                obj.LogWriter("MultiDelete in DB Ends");

                obj.LogWriter("MultiUpdate in DB Starts");
                if (!string.IsNullOrEmpty(strMainInsert))
                {
                    for (int i = 0; i < strMainInsert.Split('|').Length; i++)
                    {
                        cmd.CommandText = strMainInsert.Split('|')[i].ToString();
                        int j = cmd.ExecuteNonQuery();
                    }
                }
                obj.LogWriter("MultiUpdate in DB Ends");

                transaction.Commit();
                /*System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");
                sb.Append(@"alert('Record updated sucessfully');");
                sb.Append(@"</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", sb.ToString(), false);*/

                blnUpdate = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                obj.ErrorWriter("EntryDetails Error : btnEditBottom_Click Ends " + ex.Message, hidName.Value);
                blnUpdate = false;
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();

                ClearAllValues();

				InsertSearchResultTable(strcon, hidCredentialID.Value);

				cbo_Tab_Credential_Version.Enabled = false;

				objSP.MatchDropDownValuesText("Master", cbo_Tab_Credential_Version);

				tr_Credential_Copy.Visible = false;

				Session.Remove("sessionCredentialID");
				Session.Remove("SessionSearchPG");
				if (blnUpdate)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append(@"<script language='javascript'>");
					sb.Append(@"alert('Record updated successfully');");
					sb.Append(@"window.navigate('ViewEntryDetails.aspx?v=" + hidCredentialID.Value + "');");
					sb.Append(@"</script>");
					ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", sb.ToString(), false);
				}
			}
		}
		
		private string DeleteAllMultiSelectValues(string strMainDelete, int iCredentialID)
		{
			if (string.IsNullOrEmpty(strMainDelete))
			{
				strMainDelete = DeleteMultiSelectValues("tblCredentialBusinessGroup", iCredentialID);
			}
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialClientIndustrySector", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountryClient", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialTransactionIndustrySector", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountryMatterOpen", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountryMatterClose", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountryJurisdiction", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialLanguageOfDispute", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountryArbitrationCountry", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialLeadPartner", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialSourceOfCredential", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialOtherMatterExecutive", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialClientIndustryType", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialTransactionIndustryType", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialTeam", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialReferredFromOtherCMSOffice", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountryOtherCMSOffice", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialOtherUses", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialKnowHow", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialWorkTypeBAIF", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialWorkType", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialActingFor", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialSubWorkType", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountryBuyer", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountrySeller", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialCountryTarget", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialDollarValue", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialPoundValue", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialEuroValue", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialWorkTypeCRD", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialSubWorkTypeCommercial", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialNatureWork", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialTypeOfContract", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialEPCProjectSector", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialTransactionType", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialProjectSector", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialClientTypeRealEstate", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialWorkTypeRealEstate", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialSubWorkTypeRE", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialWorkTypeHC", iCredentialID);
			strMainDelete = strMainDelete + "|" + DeleteMultiSelectValues("tblCredentialSubWorkTypeHC", iCredentialID);
			return strMainDelete;
		}
		
        private bool checkMatterNumber()
        {
            bool blnCheck = false;
            DataSet ds = objSP.ReturnDataSetText(txt_Tab_Matter_No.Text.Trim());

            if (ds.Tables[0].Rows.Count > 0)
            {
                blnCheck = true;
            }
            return blnCheck;
        }

        public void insload(string strColValue, string strColName)
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string sqlquery = "insert into tblCredential(" + strColName + ")values(" + strColValue + ")";
            SqlCommand cmd = new SqlCommand(sqlquery, con);

            cmd.ExecuteNonQuery();
        }

        public void UpdateCredentials(StringBuilder strColValue, StringBuilder strColName, string strCredentialID)
        {

            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            con.Open();

            string strUpdate = string.Empty;

			string[] strCol = strColName.ToString().Split(',');
			string[] strCol2 = strColValue.ToString().Split(',');

			if (strCol.Length == strCol2.Length)
			{
				int i = strCol.Length - 1;

				for (int j = 0; j < i; j++)
				{
					if (string.IsNullOrEmpty(strUpdate))
					{
						if (strCol2[j].ToString().Contains("1@2!"))
						{
							strCol2[j] = strCol2[j].ToString().Replace("1@2!", ",");
						}
						strUpdate = strCol[j] + "='" + strCol2[j];
					}
					else
					{
						if (cld_Tab_Date_Opened.SelectedDate.HasValue && cld_Tab_Date_Opened.SelectedDate.HasValue)
						{
							if (strCol2[j].ToString() == "convert(datetime|'" + cld_Tab_Date_Opened.DateInput.DisplayText + "'|103)")
							{
								strCol2[j] = "convert(datetime,'" + cld_Tab_Date_Opened.DateInput.DisplayText + "',103)";
							}
						}
						if (cld_Tab_Date_Completed.SelectedDate.HasValue && cld_Tab_Date_Completed.SelectedDate.HasValue)
						{
							if (strCol2[j].ToString() == "convert(datetime|'" + cld_Tab_Date_Completed.DateInput.DisplayText + "'|103)")
							{
								strCol2[j] = "convert(datetime,'" + cld_Tab_Date_Completed.DateInput.DisplayText + "',103)";
							}
						}
						if (strCol2[j].ToString().Contains("1@2!"))
						{
							strCol2[j] = strCol2[j].ToString().Replace("1@2!", ",");
						}
						if (strCol[j].ToString().ToUpper() == "DEALANNOUNCEDID")
						{
							strCol2[j] = strCol2[j].Replace("|", ",");
						}
						strUpdate = strUpdate + "," + strCol[j] + "=" + strCol2[j];
					}
				}
			}
			if (!string.IsNullOrEmpty(strUpdate))
			{
				if (hidPartial.Value == "0")
				{
					strUpdate += " ,partialflag='0'";
				}
				else
				{
					if (hidPartial.Value == "1")
					{
						strUpdate += " ,partialflag='1'";
					}
				}
				
				strUpdate = strUpdate + ",updated_by='" + Session["sessionUserInfo"].ToString().Split('~')[0] + "',date_updated=convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103)";
				string sqlquery = "update tblcredential set " + strUpdate + (" where credentialid='" + strCredentialID + "'");

                objLogger.LogWriter(sqlquery);

                //string sqlquery = "insert into tblCredential(username," + strColName.ToString().Substring(0, strColName.ToString().Length - 1) + ")values('" + Session["mainSession"].ToString() + "','" + strColValue.ToString().Substring(0, strColValue.ToString().Length - 2) + ")";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
				cmd.ExecuteNonQuery();

                cmd.Dispose();
            }
            else
            {
                objLogger.ErrorWriter("EntryDetails Error : UpdateCredentials method having difference in column matching ", hidName.Value);
            }
        }

        public int SaveCredentials(StringBuilder strColValue, StringBuilder strColName, StringBuilder strOptionalFields = null, StringBuilder strOptionalValues = null, string strBAIFtblCredFields = null, string strBAIFtblCredValues = null, string strCorptblCredFields = null, string strCorptblCredValues = null)
        {

            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string sqlquery = string.Empty;
            objLogger.LogWriter("EntryScreen : SaveCredentials starts", hidName.Value);
			sqlquery = "insert into tblCredential(username,partialflag,deleteflag,created_by,date_created,updated_by,date_updated," + strColName.ToString().Substring(0, strColName.ToString().Length - 1) + ")values('" + Session["sessionUserInfo"].ToString().Split('~')[0] + "','" + hidPartialSave.Value.Trim() + "','0','" + Session["sessionUserInfo"].ToString().Split('~')[0] + "',convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103),'" + Session["sessionUserInfo"].ToString().Split('~')[0] + "',convert(datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103),N'" + strColValue.ToString().Substring(0, strColValue.ToString().Length - 3) + ")";

			objLogger.LogWriter("EntryScreen : SaveCredentials Query - " + sqlquery.Replace("'", "''"), hidName.Value);
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            int i = cmd.ExecuteNonQuery();
            objLogger.LogWriter("EntryScreen : SaveCredentials Ends", hidName.Value);
            cmd.Dispose();

            objLogger.LogWriter("EntryScreen : SaveCredentials - Get credentialid for multiselect save Starts", hidName.Value);
			SqlCommand cmd2 = new SqlCommand();
			DataSet ds = new DataSet();
			string strgetSQL = "select top 1 credentialid from tblcredential where username='" + Session["sessionUserInfo"].ToString().Split('~')[0] + "' order by credentialid desc";
			cmd2.CommandText = strgetSQL;
			cmd2.CommandType = CommandType.Text;
			cmd2.Connection = con;

			using (SqlDataAdapter da = new SqlDataAdapter(cmd2))
			{
				da.Fill(ds);
			}
			objLogger.LogWriter("EntryScreen : SaveCredentials - Get credentialid for multiselect save Ends", hidName.Value);
			cmd2.Dispose();
			con.Close();

            return (int)Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());
        }

		public string DeleteMultiSelectValues(string strtemptbl, int iCredentialID)
		{
			return "delete from " + strtemptbl + " where CredentialId=" + iCredentialID;
		}

		public string InsertingMultiSelectValues(string strColValue, string strColName, string strtemptbl, int iCredentialID)
		{
			return "insert into " + strtemptbl + "(CredentialId , " + strColName + ")values(" + iCredentialID + "," + strColValue + ")";
		}

		public string UpdateMultiSelectValues(string strColValue, string strColName, string strtemptbl, int iCredentialID)
		{
			return "update " + strtemptbl + " set " + strColName + "=" + strColValue + " where CredentialId=" + iCredentialID;
		}

        protected void txt_Tab_Lead_Partner_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_Tab_Lead_Partner.Text.ToUpper().Contains("NICK") == true)
                {
                    hidLeadCMSPartner.Value = "0";
                    lbl_Tab_CMSPartnerName.Visible = true;
					txt_Tab_CMSPartnerName.Visible = true;
                    lbl_Tab_Source_Of_Credential.Visible = true;
					txt_Tab_Source_Of_Credential.Visible = true;
					img_Tab_Source_Of_Credential.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnClearTop_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllValues();
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails btnClearTop_Click Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        public void ClearTextWater()
        {
            txt_Tab_Client.Text = "Insert client name in full";
            txt_Tab_Client.Style.Add("background", "#F0F8FF");

            txt_Tab_ClientDescription.Text = "Eg. a leading retail bank, an international IT company etc";
            txt_Tab_ClientDescription.Style.Add("background", "#F0F8FF");

            txt_Tab_ClientIndustrySector.Text = "Select the sector of the client company from look up";
            txt_Tab_ClientIndustrySector.Style.Add("background", "#F0F8FF");

            txt_Tab_Client_Industry_Type.Text = "Select the sub-sector of the client company from look up";
            txt_Tab_Client_Industry_Type.Style.Add("background", "#F0F8FF");

            txt_Tab_Country_PredominantCountry.Text = "Eg. where head quartered";
            txt_Tab_Country_PredominantCountry.Style.Add("background", "#F0F8FF");

            txt_Tab_Matter_No.Text = "Eg. 123456.00001";
            txt_Tab_Matter_No.Style.Add("background", "#F0F8FF");

            txt_Tab_Date_Completed.Text = "Select date from calendar icon or select ongoing";
            txt_Tab_Date_Completed.Style.Add("background", "#F0F8FF");

            txt_Tab_TransactionIndustrySector.Text = "Select the sector the matter relates to (not worktype) from look up";
            txt_Tab_TransactionIndustrySector.Style.Add("background", "#F0F8FF");

            txt_Tab_Transaction_Industry_Type.Text = "Select the sub-sector of the matter from look up";
            txt_Tab_Transaction_Industry_Type.Style.Add("background", "#F0F8FF");

            txt_Tab_Project_Description.Text = "Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion.";
            txt_Tab_Project_Description.Style.Add("background", "#F0F8FF");

            txt_Tab_Significant_Features.Text = "Insert any other useful information about the credential that will be useful for future reference purposes";
            txt_Tab_Significant_Features.Style.Add("background", "#F0F8FF");

            txt_Tab_Keyword.Text = "Include any other key words associated with the matter";
            txt_Tab_Keyword.Style.Add("background", "#F0F8FF");

            txt_Tab_Country_Matter_Close.Text = "Select the country(s) of the matter/transaction from look up";
            txt_Tab_Country_Matter_Close.Style.Add("background", "#F0F8FF");

            txt_Tab_Language_Of_Dispute.Text = "Select the language of dispute from look up";
            txt_Tab_Language_Of_Dispute.Style.Add("background", "#F0F8FF");

            txt_Tab_Country_Jurisdiction.Text = "Select the country of dispute from look up";
            txt_Tab_Country_Jurisdiction.Style.Add("background", "#F0F8FF");

            //Select the source of credential from look up
            txt_Tab_Country_Matter_Open.Text = "Select the country where matter opened from look up";
            txt_Tab_Country_Matter_Open.Style.Add("background", "#F0F8FF");

            txt_Tab_Source_Of_Credential.Text = "Select the source of credential from look up";
            txt_Tab_Source_Of_Credential.Style.Add("background", "#F0F8FF");

            txt_Tab_Team.Text = "Multi select from look up";
            txt_Tab_Team.Style.Add("background", "#F0F8FF");

            txt_Tab_Lead_Partner.Text = "Multi select from look up";
            txt_Tab_Lead_Partner.Style.Add("background", "#F0F8FF");

            txt_Tab_CMSPartnerName.Text = "Open field – format last name first name";
            txt_Tab_CMSPartnerName.Style.Add("background", "#F0F8FF");

            txt_Tab_Other_Matter_Executive.Text = "Multi select from look up";
            txt_Tab_Other_Matter_Executive.Style.Add("background", "#F0F8FF");

            txt_Tab_Referred_From_Other_CMS_Office.Text = "Multi select from look up";
            txt_Tab_Referred_From_Other_CMS_Office.Style.Add("background", "#F0F8FF");

            txt_Tab_Country_OtherCMSOffice.Text = "Multi select from look up";
            txt_Tab_Country_OtherCMSOffice.Style.Add("background", "#F0F8FF");

            txt_Tab_Other_Uses.Text = "Eg. case study/directory submission select from look-up and enter docs# in other useful credential information box";
            txt_Tab_Other_Uses.Style.Add("background", "#F0F8FF");

            txt_Tab_Know_How.Text = "For corporate deals only select relevant theme from look up";//
            txt_Tab_Know_How.Style.Add("background", "#F0F8FF");

            txt_Tab_Bible_Reference.Text = "For corporate deals only";
            txt_Tab_Bible_Reference.Style.Add("background", "#F0F8FF");

            txt_Tab_ProjectName_Core.Text = "If applicable e.g. Project Camden";
            txt_Tab_ProjectName_Core.Style.Add("background", "#F0F8FF");
        }

        private void ClearAllValues()
        {
            pnlcred.Visible = false;
            /*Client*/
            txt_Tab_Client.Text = string.Empty;

            /*Name Confidential`*/
            rdo_Tab_Client_Name_Confidential.SelectedIndex = -1;
            //tr_Tab_ClientDescription_Language.Visible = false;
            tr_Tab_ClientDescription.Visible = false;
            //tr_Tab_ClientDescription_OtherLanguage.Visible = false;
            tr_Tab_NameConfidential_Completion.Visible = false;

            txt_Tab_ClientDescription.Text = string.Empty;
            rdo_Tab_NameConfidential_Completion.SelectedIndex = -1;
            //txt_Tab_ClientDescription_OtherLanguage.Text = string.Empty;
            // objSP.MatchDropDownValuesText("English", cbo_Tab_ClientDescription_Language);

            /*Sector Group*/
            txt_Tab_ClientIndustrySector.Text = string.Empty;
            hid_Tab_ClientIndustrySector.Value = string.Empty;
            hid_Tab_ClientIndustrySector_Text.Value = string.Empty;

            /*Sub SSectorGroup*/
            txt_Tab_Client_Industry_Type.Text = string.Empty;
            hid_Tab_Client_Industry_Type_Text.Value = null;
            hid_Tab_Client_Industry_Type.Value = string.Empty;

            /*Predominant Country Of Client*/
            txt_Tab_Country_PredominantCountry.Text = string.Empty;
            hid_Tab_Country_PredominantCountry.Value = string.Empty;
            hid_Tab_Country_PredominantCountry_Text.Value = string.Empty;

            /*Pratice Group*/
            chKBAIF.Checked = false;
            chKCorp.Checked = false;
            chKCRD.Checked = false;
            chkEPC.Checked = false;
            chkEPCE.Checked = false;
            chkHC.Checked = false;
            chkIPF.Checked = false;
            chkRE.Checked = false;
            chkCorpTax.Checked = false;

            /*Matter Number*/
            txt_Tab_Matter_No.Text = string.Empty;

            /*Date Opened*/
			cld_Tab_Date_Opened.Clear();

            /*Sector Group*/
            txt_Tab_TransactionIndustrySector.Text = string.Empty;
            hid_Tab_TransactionIndustrySector.Value = string.Empty;
            hid_Tab_TransactionIndustrySector_Text.Value = string.Empty;

            /*Sub sector group*/
            txt_Tab_Transaction_Industry_Type.Text = string.Empty;
            hid_Tab_Transaction_Industry_Type_Text.Value = string.Empty;
            hid_Tab_Transaction_Industry_Type.Value = string.Empty;

            /*Language dropdown*/
            //objSP.MatchDropDownValuesText("English", cbo_Tab_Language);
            //txt_Tab_ProjectDescription_Polish.Text = string.Empty;
            // tr_Tab_ProjectDescription_Polish.Visible = false;

            /*Matter Credential Desc*/
            txt_Tab_Project_Description.Text = string.Empty;

            /*Other Language*/
            // txt_Tab_ProjectDescription_Polish.Text = string.Empty;

            /*Other Matter Desc*/
            txt_Tab_Significant_Features.Text = string.Empty;

            /*Keyword*/
            txt_Tab_Keyword.Text = string.Empty;

            /*Matter Confidential*/
            rdo_Tab_Client_Matter_Confidential.SelectedIndex = -1;
            rdo_Tab_MatterConfidential_Completion.SelectedIndex = -1;
            tr_Tab_MatterConfidential_Completion.Visible = false;

            /*Actual Date*/
            txt_Tab_Date_Completed.Text = string.Empty;
            chk_Tab_ActualDate_Ongoing.Checked = false;
			
			cld_Tab_Date_Completed.Clear();
			cld_Tab_Date_Completed.Style.Add("display", "inline-block");
			txt_Tab_Date_Completed.Style.Add("display", "none");
			hid_Tab_ActualDate_Ongoing.Value = string.Empty;
			hid_Tab_Date_Completed.Value = string.Empty;
			chk_Tab_ActualDate_Ongoing_1.Checked = false;
			hid_Tab_ActualDate_Ongoing_1.Value = string.Empty;
			
            /*Project name*/
            txt_Tab_ProjectName_Core.Text = string.Empty;

            /*Appilicable Law*/
            cbo_Tab_Country_Law.SelectedIndex = 0;
            tr_Tab_Country_Law_Other.Visible = false;
            txt_Tab_Country_Law_Other.Text = string.Empty;

            /*Country where opened*/
            txt_Tab_Country_Matter_Open.Text = string.Empty;
            hid_Tab_Country_Matter_Open.Value = string.Empty;
            hid_Tab_Country_Matter_Open_Text.Value = string.Empty;

            /*Matter Location*/
            txt_Tab_Country_Matter_Close.Text = string.Empty;
            hid_Tab_Country_Matter_Close_Text.Value = string.Empty;
            hid_Tab_Country_Matter_Close.Value = string.Empty;

            /*Conterntious*/
            cbo_Tab_Contentious_IRG.SelectedIndex = 0;
            plnContentiousDetails.Visible = false;
            tr_Tab_ArbitrationCity.Visible = false;
            tr_Tab_Arbitral_Rules.Visible = false;
            tr_Tab_ArbitrationCity_Other.Visible = false;
            tr_Tab_InvestmentTreaty.Visible = false;
            tr_Tab_Investigation_Type.Visible = false;
            tr_Tab_Country_ArbitrationCountry.Visible = false;

            txt_Tab_Country_ArbitrationCountry.Text = string.Empty;
            hid_Tab_Country_ArbitrationCountry.Value = string.Empty;
            hid_Tab_Country_ArbitrationCountry_Text.Value = string.Empty;

            txt_Tab_Country_Jurisdiction.Text = string.Empty;
            hid_Tab_Country_Jurisdiction.Value = string.Empty;
            hid_Tab_Country_Jurisdiction_Text.Value = string.Empty;

            txt_Tab_Language_Of_Dispute.Text = string.Empty;
            hid_Tab_Language_Of_Dispute.Value = string.Empty;
            hid_Tab_Language_Of_Dispute_Text.Value = string.Empty;

            txt_Tab_Language_Of_Dispute_Other.Text = string.Empty;
            hid_Tab_Language_Of_Dispute_Other.Value = string.Empty;
            hid_Tab_Language_Of_Dispute_Other_Text.Value = string.Empty;

            cbo_Tab_Dispute_Resolution.SelectedIndex = 0;
            cbo_Tab_ArbitrationCity.SelectedIndex = -1;
            cbo_Tab_Arbitral_Rules.SelectedIndex = 0;
            cbo_Tab_Investigation_Type.SelectedIndex = -1;
            rdo_Tab_InvestmentTreaty.SelectedIndex = -1;

            //cbo_Tab_Country_ArbitrationCountry.SelectedIndex = 0;
            //txt_Tab_Country_ArbitrationCountry.Text = string.Empty;
            //txt_Tab_ArbitrationCity.Text = string.Empty;

            /* Value of deal */
            txt_Tab_ValueOfDeal_Core.Text = string.Empty;

            /*Currency of deal*/
            cbo_Tab_Currency_Of_Deal.SelectedIndex = 0;

            rdo_Tab_Value_Confidential.SelectedIndex = -1;
            rdo_Tab_ValueConfidential_Completion.SelectedIndex = -1;
            trr_Tab_ValueConfidential_Completion.Visible = false;


            /*Teams*/
            txt_Tab_Team.Text = string.Empty;
            hid_Tab_Team_Text.Value = string.Empty;
            hid_Tab_Team.Value = string.Empty;

            /*Lead Partner*/
            txt_Tab_Lead_Partner.Text = string.Empty;
            hid_Tab_Lead_Partner_Text.Value = string.Empty;
            hid_Tab_Lead_Partner.Value = string.Empty;
            hid_Tab_Lead_Partner_Ctl.Value = string.Empty;

            tr_Tab_Source_Of_Credential.Style.Add("display", "none");
            tr_Tab_SourceOfCredential_Other.Style.Add("display", "none");
            tr_Tab_CMSPartnerName.Style.Add("display", "none");

            /*CMS Partner Name*/
            txt_Tab_CMSPartnerName.Text = string.Empty;

            /*Source Of Credential*/
            txt_Tab_Source_Of_Credential.Text = string.Empty;
            hid_Tab_Source_Of_Credential_Text.Value = string.Empty;
            hid_Tab_Source_Of_Credential.Value = string.Empty;

            /*Source of other*/
            txt_Tab_SourceOfCredential_Other.Text = string.Empty;
            hid_Tab_SourceOfCredential_Other.Value = string.Empty;

            /*Other Matter Executive*/
            txt_Tab_Other_Matter_Executive.Text = string.Empty;
            hid_Tab_Other_Matter_Executive_Text.Value = string.Empty;
            hid_Tab_Other_Matter_Executive.Value = string.Empty;

            /* CMS Firms Involved */
            txt_Tab_Referred_From_Other_CMS_Office.Text = string.Empty;
            hid_Tab_Referred_From_Other_CMS_Office.Value = string.Empty;
            hid_Tab_Referred_From_Other_CMS_Office_Text.Value = string.Empty;

            /*Lead CMS Firms*/
            cbo_Tab_Lead_CMS_Firm.SelectedIndex = 0;

            /* Country other cms firms */
            txt_Tab_Country_OtherCMSOffice.Text = string.Empty;
            hid_Tab_Country_OtherCMSOffice.Value = string.Empty;
            hid_Tab_Country_OtherCMSOffice_Text.Value = string.Empty;

            /*Credential*/
            cbo_Tab_Credential_Status.SelectedIndex = 0;
            cbo_Tab_Credential_Type.SelectedIndex = 0;
            cbo_Tab_Credential_Version.SelectedIndex = 0;

            chkCredentialCopy.Checked = false;
            tr_Credential_Copy.Visible = false;
            txt_Tab_Credential_Version_Other.Text = string.Empty;
            tr_Credential_Version_Other.Visible = false;

            /*Other Uses*/
            txt_Tab_Other_Uses.Text = string.Empty;
            hid_Tab_Other_Uses.Value = string.Empty;
            hid_Tab_Other_Uses_Text.Value = string.Empty;

            /*Priority*/

            cbo_Tab_Priority.SelectedIndex = -1;

            /* Know How */
            txt_Tab_Know_How.Text = string.Empty;
            hid_Tab_Know_How.Value = string.Empty;
            hid_Tab_Know_How_Text.Value = string.Empty;

            rdo_Tab_ProBono.SelectedIndex = -1;
            txt_Tab_Bible_Reference.Text = string.Empty;


            Session["sessBAIFSS"] = null;
            Session["sessBAIFMS"] = null;

            Session["sessCRDSS"] = null;
            Session["sessCRDMS"] = null;

            Session["sessCORPSS"] = null;
            Session["sessCORPMS"] = null;

            Session["sessEPCSS"] = null;
            Session["sessEPCMS"] = null;

            Session["sessEPCESS"] = null;
            Session["sessEPCEMS"] = null;

            Session["sessIPFSS"] = null;
            Session["sessIPFMS"] = null;

            Session["sessRESS"] = null;
            Session["sessREMS"] = null;

            Session["sessHCSS"] = null;
            Session["sessHCMS"] = null;

            Session["sessCorpTaxSS"] = null;
            Session["sessCorpTaxMS"] = null;

            if (Session["sessBAIFClear"] != null)
            {
                Session.Remove("sessBAIFClear");
            }
            Session.Add("sessBAIFClear", "0");


            if (Session["sessCorpClear"] != null)
            {
                Session.Remove("sessCorpClear");
            }
            Session.Add("sessCorpClear", "0");


            if (Session["sessCRDClear"] != null)
            {
                Session.Remove("sessCRDClear");
            }
            Session.Add("sessCRDClear", "0");


            if (Session["sessEPCClear"] != null)
            {
                Session.Remove("sessEPCClear");
            }
            Session.Add("sessEPCClear", "0");


            if (Session["sessEPCEClear"] != null)
            {
                Session.Remove("sessEPCEClear");
            }
            Session.Add("sessEPCEClear", "0");


            if (Session["sessHCClear"] != null)
            {
                Session.Remove("sessHCClear");
            }
            Session.Add("sessHCClear", "0");


            if (Session["sessIPFClear"] != null)
            {
                Session.Remove("sessIPFClear");
            }
            Session.Add("sessIPFClear", "0");


            if (Session["sessREClear"] != null)
            {
                Session.Remove("sessREClear");
            }
            Session.Add("sessREClear", "0");

            if (Session["sessCorpTaxClear"] != null)
            {
                Session.Remove("sessCorpTaxClear");
            }
            Session.Add("sessCorpTaxClear", "0");


            ClearTextWater();

            liPartialSave.Visible = false;
            btnPartialSave.Visible = false;
            liPartialSaveBottom.Visible = false;
            btnPartialSaveBottom.Visible = false;

            liAddTop.Visible = true;
            btnAddTop.Visible = true;
            liAddBottom.Visible = true;
            btnAddBottom.Visible = true;

            chkPartial.Checked = false;
        }

        protected void cbo_Tab_Country_Law_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tr_Tab_Country_Law_Other.Visible = false;
                if (cbo_Tab_Country_Law.SelectedItem.Text.ToUpper() == "OTHER")
                {
                    tr_Tab_Country_Law_Other.Visible = true;
                    tr_Tab_Country_Law_Other.Focus();
                }
                else
                {
                    txt_Tab_Country_Law_Other.Text = string.Empty;
                    cbo_Tab_Country_Law.Focus();
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails cbo_Tab_Country_Law_SelectedIndexChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void cbo_Tab_Credential_Version_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tr_Credential_Version_Other.Visible = false;
                if (cbo_Tab_Credential_Version.SelectedItem.Text.ToUpper() == "OTHER")
                {
                    tr_Credential_Version_Other.Visible = true;
                    txt_Tab_Credential_Version_Other.Focus();
                }
                else
                {
                    txt_Tab_Credential_Version_Other.Text = string.Empty;
                    cbo_Tab_Credential_Version.Focus();
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails cbo_Tab_Credential_Version_SelectedIndexChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

		protected void cbo_Tab_ArbitrationCity_SelectedIndexChanged(object sender, EventArgs e)
		{
			tr_Tab_ArbitrationCity_Other.Visible = false;
			if (cbo_Tab_ArbitrationCity.SelectedItem.Text.ToUpper() == "OTHER")
			{
				tr_Tab_ArbitrationCity_Other.Visible = true;
				txt_Tab_ArbitrationCity_Other.Focus();
			}
			else
			{
				txt_Tab_ArbitrationCity_Other.Text = string.Empty;
				cbo_Tab_ArbitrationCity.Focus();
			}
		}
		
		protected void btnSearchBottom_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Search/SearchScreen.aspx?a=2");
		}

        protected void btnDeleteBottom_Click(object sender, EventArgs e)
        {
            try
            {
                string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
                SqlConnection con = new SqlConnection(strcon);
				
				con.Open();
				string strUserID = hidCredentialID.Value;
				
				if (hid_Credential_Version.Value.Trim().ToUpper() == "MASTER")
				{
					string strSql = "SELECT credentialid FROM tblcredentialversionrelation WHERE credentialversion ='Other' and credentialmasterid = '" + strUserID + "'";
					SqlDataAdapter adp = new SqlDataAdapter(strSql, con);
					
					DataSet dsNew = new DataSet();
					adp.Fill(dsNew);
					adp.Dispose();
					
					string strChildCredentialID = string.Empty;
					if (dsNew.Tables[0].Rows.Count > 0)
					{
						for (int i = 0; i < dsNew.Tables[0].Rows.Count; i++)
						{
							if (string.IsNullOrEmpty(strChildCredentialID))
							{
								strChildCredentialID = "'" + dsNew.Tables[0].Rows[i][0].ToString() + "',";
							}
							else
							{
								strChildCredentialID = strChildCredentialID + "'" + dsNew.Tables[0].Rows[i][0].ToString() + "',";
							}
						}
						
						strChildCredentialID = strChildCredentialID.Substring(0, strChildCredentialID.Length - 1);
						string strdel = "update tblcredential set deleteflag='1' where credentialid in (" + strChildCredentialID + ")";
						SqlCommand cmddel = new SqlCommand(strdel, con);
						
						cmddel.ExecuteNonQuery();
						cmddel.Dispose();
					}
				}
				
				SqlCommand cmd = new SqlCommand();
				cmd.CommandText = "usp_CredentialDelete";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = con;

				SqlParameter[] par = new SqlParameter[1];
				par[0] = new SqlParameter("@TheRecord", Convert.ToInt32(strUserID));

                cmd.Parameters.AddRange(par);

                int iDelete = cmd.ExecuteNonQuery();

                if (iDelete > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
					sb.Append(@"<script language='javascript'>");
					sb.Append(@"alert('Record deleted successfully');");
					sb.Append(@"window.navigate('Search/SearchScreen.aspx?a=2');");
					sb.Append(@"</script>");
					ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", sb.ToString(), false);
				}
				
				cmd.Dispose();
				con.Close();
				ClearAllValues();
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryDetails btnDeleteBottom_Click Error : " + ex.Message, hidName.Value);
				throw ex;
			}
			finally
			{
				Response.Redirect("~/Search/ResultScreen.aspx");
			}
		}
		
		protected void btnHidePG_Click(object sender, EventArgs e)
		{
			string text = hid_PracticeChk.Value.Trim().ToUpper();
			switch (text)
			{
			case "CHKBAIF":
				chKBAIF_CheckedChanged(sender, e);
				break;
			case "CHKCORP":
				chKCorp_CheckedChanged(sender, e);
				break;
			case "CHKCORPTAX":
				chkCorpTax_CheckedChanged(sender, e);
				break;
			case "CHKCRD":
				chKCRD_CheckedChanged(sender, e);
				break;
			case "CHKEPC":
				chkEPC_CheckedChanged(sender, e);
				break;
			case "CHKEPCE":
				chkEPCE_CheckedChanged(sender, e);
				break;
			case "CHKIPF":
				chkIPF_CheckedChanged(sender, e);
				break;
			case "CHKHC":
				chkHC_CheckedChanged(sender, e);
				break;
			case "CHKRE":
				chkRE_CheckedChanged(sender, e);
				break;
			}
		}
		
        protected void chKBAIF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chKBAIF.Checked == false)
                {
                    if (Session["sessBAIFClear"] != null)
                    {
                        Session.Remove("sessBAIFClear");
                    }
                    Session.Add("sessBAIFClear", "0");

                    if (Session["sessBAIFSS"] != null)
                    {
                        Session.Remove("sessBAIFSS");
                    }
                    if (Session["sessBAIFMS"] != null)
                    {
                        Session.Remove("sessBAIFMS");
                    }
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails chKBAIF_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chKCorp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chKCorp.Checked == false)
                {
                    if (Session["sessCorpClear"] != null)
                    {
                        Session.Remove("sessCorpClear");
                    }
                    Session.Add("sessCorpClear", "0");
					if (Session["sessCorpSS"] != null)
					{
						Session.Remove("sessCorpSS");
					}
					if (Session["sessCorpMS"] != null)
					{
						Session.Remove("sessCorpMS");
					}
				}
			}
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails chKCorp_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chkCorpTax_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCorpTax.Checked == false)
                {
                    if (Session["sessCorpTaxClear"] != null)
                    {
                        Session.Remove("sessCorpTaxClear");
                    }
                    Session.Add("sessCorpTaxClear", "0");
					if (Session["sessCorpTaxSS"] != null)
					{
						Session.Remove("sessCorpTaxSS");
					}
					if (Session["sessCorpTaxMS"] != null)
					{
						Session.Remove("sessCorpTaxMS");
					}
				}
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryDetails chkCorpTax_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chKCRD_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chKCRD.Checked == false)
                {
                    if (Session["sessCRDClear"] != null)
                    {
                        Session.Remove("sessCRDClear");
                    }
                    Session.Add("sessCRDClear", "0");
					if (Session["sessCRDSS"] != null)
					{
						Session.Remove("sessCRDSS");
					}
					if (Session["sessCRDMS"] != null)
					{
						Session.Remove("sessCRDMS");
					}
				}
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryDetails chKCRD_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chkEPC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEPC.Checked == false)
                {
                    if (Session["sessEPCClear"] != null)
                    {
                        Session.Remove("sessEPCClear");
                    }
                    Session.Add("sessEPCClear", "0");
					if (Session["sessEPCSS"] != null)
					{
						Session.Remove("sessEPCSS");
					}
					if (Session["sessEPCMS"] != null)
					{
						Session.Remove("sessEPCMS");
					}
				}
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryDetails chkEPC_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chkEPCE_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEPCE.Checked == false)
                {
                    if (Session["sessEPCEClear"] != null)
                    {
                        Session.Remove("sessEPCEClear");
                    }
                    Session.Add("sessEPCEClear", "0");
					if (Session["sessEPCESS"] != null)
					{
						Session.Remove("sessEPCESS");
					}
					if (Session["sessEPCEMS"] != null)
					{
						Session.Remove("sessEPCEMS");
					}
				}
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryDetails chkEPCE_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chkHC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkHC.Checked == false)
                {
                    if (Session["sessHCClear"] != null)
                    {
                        Session.Remove("sessHCClear");
                    }
                    Session.Add("sessHCClear", "0");
					if (Session["sessHCSS"] != null)
					{
						Session.Remove("sessHCSS");
					}
					if (Session["sessHCMS"] != null)
					{
						Session.Remove("sessHCMS");
					}
				}
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryDetails chkHC_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chkIPF_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIPF.Checked == false)
                {
                    if (Session["sessIPFClear"] != null)
                    {
                        Session.Remove("sessIPFClear");
                    }
                    Session.Add("sessIPFClear", "0");
					if (Session["sessIPFSS"] != null)
					{
						Session.Remove("sessIPFSS");
					}
					if (Session["sessIPFMS"] != null)
					{
						Session.Remove("sessIPFMS");
					}
				}
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryDetails chkIPF_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chkRE_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRE.Checked == false)
                {
                    if (Session["sessREClear"] != null)
                    {
                        Session.Remove("sessREClear");
                    }
                    Session.Add("sessREClear", "0");
					if (Session["sessRESS"] != null)
					{
						Session.Remove("sessRESS");
					}
					if (Session["sessREMS"] != null)
					{
						Session.Remove("sessREMS");
					}
				}
			}
			catch (Exception ex)
			{
				objLogger.ErrorWriter("EntryDetails chkRE_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chkCredentialCopy_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCredentialCopy.Checked == true)
                {
                    liAddBottom.Visible = true;
					liAddTop.Visible = true;
					btnAddBottom.Visible = true;
					btnAddTop.Visible = true;
                    liEditBottom.Visible = false;
					liEditTop.Visible = false;
					btnEditBottom.Visible = false;
					btnEditTop.Visible = false;
                    cbo_Tab_Credential_Version.Enabled = true;
                }
                else
                {
                    liAddBottom.Visible = false;
					liAddTop.Visible = false;
					btnAddBottom.Visible = false;
					btnAddTop.Visible = false;
                    liEditBottom.Visible = true;
					liEditTop.Visible = true;
					btnEditBottom.Visible = true;
					btnEditTop.Visible = true;
                    cbo_Tab_Credential_Version.Enabled = false;

                    objSP.MatchDropDownValuesText(hid_Credential_Version.Value.Trim(), cbo_Tab_Credential_Version);
                }
            }
            catch (Exception ex)
            {
				objLogger.ErrorWriter("EntryDetails chkCredentialCopy_CheckedChanged Error : " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void chkPartial_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hidCredentialID.Value.Trim()))
            {
                if (chkPartial.Checked == true)
                {
                    liPartialSave.Visible = true;
                    btnPartialSave.Visible = true;
                    liPartialSaveBottom.Visible = true;
                    btnPartialSaveBottom.Visible = true;
                    hidPartialSave.Value = "1";

                    liAddTop.Visible = false;
                    btnAddTop.Visible = false;
                    liAddBottom.Visible = false;
                    btnAddBottom.Visible = false;
                }
                else
                {
                    liPartialSave.Visible = false;
                    btnPartialSave.Visible = false;
                    liPartialSaveBottom.Visible = false;
                    btnPartialSaveBottom.Visible = false;
                    hidPartialSave.Value = "0";

                    liAddTop.Visible = true;
                    btnAddTop.Visible = true;
                    liAddBottom.Visible = true;
                    btnAddBottom.Visible = true;
                }
            }
            else
            {
                if (chkPartial.Checked == true)
                {
                    hidPartialSave.Value = "1";

                    /*liPartialSave.Visible = true;
                    btnPartialSave.Visible = true;
                    liPartialSaveBottom.Visible = true;
                    btnPartialSaveBottom.Visible = true;
                    

                    liAddTop.Visible = false;
                    btnAddTop.Visible = false;
                    liAddBottom.Visible = false;
                    btnAddBottom.Visible = false;*/
                    
                    btnEditBottom.Attributes.Add("onclick", "return validationPartialFields('Do you want to do partial update ?');");
                    btnEditTop.Attributes.Add("onclick", "return validationPartialFields('Do you want to do partial update ?');");
                }
                else
                {
                    hidPartialSave.Value = "0";

                    /*liPartialSave.Visible = false;
                    btnPartialSave.Visible = false;
                    liPartialSaveBottom.Visible = false;
                    btnPartialSaveBottom.Visible = false;
                    

                    liAddTop.Visible = false;
                    btnAddTop.Visible = false;
                    liAddBottom.Visible = false;
                    btnAddBottom.Visible = false;*/

					btnEditBottom.Attributes.Add("onclick", "return validationFullSubmitFields('Do you want to update the record ?');");
					btnEditTop.Attributes.Add("onclick", "return validationFullSubmitFields('Do you want to update the record ?');");
                }
            }
        }

    }
}
