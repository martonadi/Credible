using System;

using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;

namespace CredentialsDemo
{
    public partial class ViewEntryDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			try
			{
				if (!IsPostBack && Session["sessionUserInfo"] != null)
				{
					if (Session["sessionCredentialID"] != null)
					{
						PopulateDataFromSearch(Session["sessionCredentialID"].ToString());
						PopulateDataFromSearchPrint(Session["sessionCredentialID"].ToString());
					}
					else
					{
						if (Request.QueryString["v"] != null)
						{
							if (Session["sessionCredentialID"] != null)
							{
								Session["sessionCredentialID"] = null;
							}
							Session.Add("sessionCredentialID", Request.QueryString["v"].ToString());
							PopulateDataFromSearch(Session["sessionCredentialID"].ToString());
							PopulateDataFromSearchPrint(Session["sessionCredentialID"].ToString());
						}
					}
					
					if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper(CultureInfo.InvariantCulture) == "ADMINISTRATOR" ||
							Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper(CultureInfo.InvariantCulture) == "EDITOR")
					{
						btnEdit.Visible = true;
					}
				}
				else
				{
					Response.Redirect("~/TimeOut.aspx");
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		protected void btnEdit_Click(object sender, EventArgs e)
		{
			bool blnRedirect = false;
			try
			{
				if (Session["sessionCredentialID"] != null)
				{
					blnRedirect = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (blnRedirect)
				{
					Response.Redirect("~/EntryDetails.aspx");
				}
			}
		}
		
		protected void btnBack_Click(object sender, EventArgs e)
		{
			if (Session["SessPageIndex"] != null)
			{
				Response.Redirect("~/Search/ResultScreen.aspx?z=1");
			}
		}
		
        private void PopulateDataFromSearch(string strCredentialID)
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
            SqlConnection con = new SqlConnection(strcon);

            string selSQL = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath("~\\Queries\\SearchResultQuery_View.txt"));

			//string strCIDS = string.Empty;
            selSQL = sr.ReadToEnd();
            selSQL = selSQL.Replace("strCredentialID", "'" + strCredentialID + "'");
            sr.Dispose();

            SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
            DataSet dsNew = new DataSet();
            DataTable dt = new DataTable();
            adp.Fill(dt);
            adp.Dispose();




            /* SqlCommand cmd = new SqlCommand();
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
             adp.Dispose(); cmd.Dispose();*/

            if (dt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["KeyWord"].ToString().Trim()))
                {
                    tr_Tab_Keyword.Visible = true;
                    txt_Tab_Keyword.Text = dt.Rows[0]["KeyWord"].ToString().Trim();
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientName"].ToString().Trim()))
                {
                    //tr_Tab_Client.Visible = false;
                    txt_Tab_Client.Text = dt.Rows[0]["ClientName"].ToString().Trim();
                }

                rdo_Tab_Client_Name_Confidential.Text = dt.Rows[0]["ClientNameConfidential"].ToString();
                tr_Tab_Client_Name_Confidential.BgColor = "#F5F5DC";
                //dt.Rows[0]["ClientNameConfidentialCompletion"].ToString()
                if (dt.Rows[0]["ClientNameConfidential"].ToString().ToUpper() == "YES")
                {
                    //tr_Tab_ClientDescription_Language.Visible = true;
                    tr_Tab_ClientDescription.Visible = true;
                    tr_Tab_NameConfidential_Completion.Visible = true;
                    //tr_Tab_ClientDescription_Language.Visible = true;

                    //cbo_Tab_ClientDescription_Language.Text = dt.Rows[0]["ClientDescriptionLanguage"].ToString();
                    txt_Tab_ClientDescription.Text = dt.Rows[0]["ClientDescription"].ToString();
                    rdo_Tab_NameConfidential_Completion.Text = dt.Rows[0]["ClientNameConfidentialCompletion"].ToString();
                }

                //Sector Group 
                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientSector"].ToString()))
                {
                    txt_Tab_ClientIndustrySector.Text = dt.Rows[0]["ClientSector"].ToString();
                }

                //Sub-Sector Group 
                if (!string.IsNullOrEmpty(dt.Rows[0]["ClientSubSector"].ToString()))
                {
                    tr_Tab_Client_Industry_Type.Visible = true;
                    txt_Tab_Client_Industry_Type.Text = dt.Rows[0]["ClientSubSector"].ToString();
                }

                //Predominant Country of Client 
                if (!string.IsNullOrEmpty(dt.Rows[0]["PredominantCountryofClient"].ToString()))
                {
                    tr_Tab_Country_PredominantCountry.Visible = true;
                    txt_Tab_Country_PredominantCountry.Text = dt.Rows[0]["PredominantCountryofClient"].ToString();
                }

                //Main Practice Group usp_CredentialBusinessGroupSource
                if (!string.IsNullOrEmpty(dt.Rows[0]["PracticeGroup"].ToString()))
                {
                    txt_Tab_PracticeGroup.Text = dt.Rows[0]["PracticeGroup"].ToString();
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["MatterNumber"].ToString()))
                {
                    txt_Tab_Matter_No.Text = dt.Rows[0]["MatterNumber"].ToString();
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["DateMatterOpened"].ToString()))
                {
                    txt_Tab_Date_Opened.Text = dt.Rows[0]["DateMatterOpened"].ToString();
                }

                //Sector Group
                if (!string.IsNullOrEmpty(dt.Rows[0]["MatterSector"].ToString()))
                {
                    txt_Tab_TransactionIndustrySector.Text = dt.Rows[0]["MatterSector"].ToString();
                }

                //Sub-Sector Group 
                if (!string.IsNullOrEmpty(dt.Rows[0]["MatterSubSector"].ToString()))
                {
                    txt_Tab_Transaction_Industry_Type.Text = dt.Rows[0]["MatterSubSector"].ToString();
                }

                //cbo_Tab_Language.Text = dt.Rows[0]["MatterLanguage"].ToString();
                string strClientName = dt.Rows[0]["ClientName"].ToString().Trim();
                string strClientDescription = dt.Rows[0]["ClientDescription"].ToString();
                string strMatterDescription = dt.Rows[0]["MatterDescription"].ToString();
                string strDesc = string.Empty;

                if ((dt.Rows[0]["ClientNameConfidential"].ToString() == "Yes") && (dt.Rows[0]["ClientMatterConfidential"].ToString() == "Yes") &&
                        (dt.Rows[0]["ValueConfidential"].ToString() == "Yes"))
                {
                    strDesc = "[confidential – name/matter/value] [" + strClientName + "] " + strClientDescription + " " + strMatterDescription;
                }
                else if ((dt.Rows[0]["ClientNameConfidential"].ToString() == "Yes") && (dt.Rows[0]["ClientMatterConfidential"].ToString() == "Yes"))
                {
                    strDesc = "[confidential – name/matter] [" + strClientName + "] " + strClientDescription + " " + strMatterDescription;
                    //strConfidentialYes = "[confidential – name/matter] [" + strClientName + "] " + strClientDescription + " " + strMatterDescription;
                }
                else if ((dt.Rows[0]["ClientNameConfidential"].ToString() == "Yes") && (dt.Rows[0]["ValueConfidential"].ToString() == "Yes"))
                {
                    strDesc = "[confidential – name/value] [" + strClientName + "] " + strClientDescription + " " + strMatterDescription;
                    // strConfidentialYes = "[confidential – client name] [" + strClientName + "] " + strClientDescription + " " + strMatterDescription;
                }
                else if ((dt.Rows[0]["ClientMatterConfidential"].ToString() == "Yes") && (dt.Rows[0]["ValueConfidential"].ToString() == "Yes"))
                {
                    strDesc = "[confidential – matter/value] " + strClientName + " " + strMatterDescription;
                }
                else if (dt.Rows[0]["ClientNameConfidential"].ToString() == "Yes")
                {
                    strDesc = "[confidential – client name] [" + strClientName + "] " + strClientDescription + " " + strMatterDescription;
                    //strConfidentialYes = "[confidential – client name] [" + strClientName + "] " + strClientDescription + " " + strMatterDescription;
                }
                else if (dt.Rows[0]["ClientMatterConfidential"].ToString() == "Yes")
                {
                    strDesc = "[confidential – matter] " + strClientName + " " + strMatterDescription;
                    // strConfidentialYes = "[confidential – matter] " + strClientName + " " + strMatterDescription;
                }
                else if (dt.Rows[0]["ValueConfidential"].ToString() == "Yes")
                {
                    strDesc = "[confidential – value] " + strClientName + " " + strMatterDescription;
                }
                else
                {
                    strDesc = strClientName + " " + strMatterDescription;
                    //strConfidentialYes = strClientName + " " + strMatterDescription; ;
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["MatterDescription"].ToString()))
                {
                    //txt_Tab_Project_Description.Text = dt.Rows[0]["MatterDescription"].ToString();
                    txt_Tab_Project_Description.Text = strDesc;
                }

				if (!string.IsNullOrEmpty(dt.Rows[0]["OtherMatterDescription"].ToString()))
				{
					tr_Tab_Significant_Features.Visible = true;
					txt_Tab_Significant_Features.Text = dt.Rows[0]["OtherMatterDescription"].ToString();
				}

                /* if (!string.IsNullOrEmpty(dt.Rows[0]["MatterLanguageDescription"].ToString().Trim()))
                 {
                     tr_Tab_ProjectDescription_Polish.Visible = true;
                     txt_Tab_ProjectDescription_Polish.Text = dt.Rows[0]["MatterLanguageDescription"].ToString();
                 }*/

                StringBuilder strAppend = new StringBuilder();

                if (!string.IsNullOrEmpty(dt.Rows[0]["BAIFWorkType"].ToString().Trim()))
                {
                    strAppend.Append(dt.Rows[0]["BAIFWorkType"].ToString().Trim());
					strAppend.Append("; ");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateWorkType"].ToString().Trim()))
                {
                    strAppend.Append(dt.Rows[0]["CorporateWorkType"].ToString().Trim());
					strAppend.Append("; ");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["CRDWorkType"].ToString().Trim()))
                {
                    strAppend.Append(dt.Rows[0]["CRDWorkType"].ToString().Trim());
					strAppend.Append("; ");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["EPCNatureofWork"].ToString().Trim()))
                {
                    strAppend.Append(dt.Rows[0]["EPCNatureofWork"].ToString().Trim());
					strAppend.Append("; ");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim()))
                {
                    strAppend.Append(dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim());
					strAppend.Append("; ");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["HCWorkType"].ToString().Trim()))
                {
                    strAppend.Append(dt.Rows[0]["HCWorkType"].ToString().Trim());
					strAppend.Append("; ");
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateWorkType"].ToString().Trim()))
                {
                    strAppend.Append(dt.Rows[0]["RealEstateWorkType"].ToString().Trim());
					strAppend.Append("; ");
                }

				if (!string.IsNullOrEmpty(strAppend.ToString()))
				{
					txt_Tab_WorkType.Text = strAppend.ToString().Trim().Substring(0, strAppend.ToString().Trim().Length - 1);
					tr_Tab_WorkType.Visible = true;
				}
				string strCorporateSubWorkType = string.Empty;
				strCorporateSubWorkType = dt.Rows[0]["CorporateSubWorkType"].ToString().Trim();
				string strCRDSubWorkType = string.Empty;
				strCRDSubWorkType = dt.Rows[0]["CRDSubWorkType"].ToString().Trim();
				string strRealEstateSubWorkType = string.Empty;
				strRealEstateSubWorkType = dt.Rows[0]["RealEstateSubWorkType"].ToString().Trim();
				string strSubWorkTypeHC = string.Empty;
				strSubWorkTypeHC = dt.Rows[0]["HCSubWorkType"].ToString().Trim();
				StringBuilder strSubWTAppend = new StringBuilder();
				if (!string.IsNullOrEmpty(strCorporateSubWorkType.Trim()))
				{
					strSubWTAppend.Append(strCorporateSubWorkType.Trim());
					strSubWTAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(strCRDSubWorkType.Trim()))
				{
					strSubWTAppend.Append(strCRDSubWorkType.Trim());
					strSubWTAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(strRealEstateSubWorkType.Trim()))
				{
					strSubWTAppend.Append(strRealEstateSubWorkType.Trim());
					strSubWTAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(strSubWorkTypeHC.Trim()))
				{
					strSubWTAppend.Append(strSubWorkTypeHC.Trim());
					strSubWTAppend.Append("; ");
				}
				if (strSubWTAppend != null && strSubWTAppend.ToString().Length > 1)
				{
					tr_Tab_SubWorkType.Visible = true;
					txt_Tab_SubWorkType.Text = strSubWTAppend.ToString().Substring(0, strSubWTAppend.ToString().Length - 1);
				}

				rdo_Tab_Client_Matter_Confidential.Text = dt.Rows[0]["ClientMatterConfidential"].ToString();
				tr_Tab_Client_Matter_Confidential.BgColor = "#F5F5DC";

				if (dt.Rows[0]["ClientMatterConfidential"].ToString().ToUpper() == "YES")
				{
					tr_Tab_MatterConfidential_Completion.Visible = true;
					rdo_Tab_MatterConfidential_Completion.Text = dt.Rows[0]["MatterConfidentialCompletion"].ToString();
				}
				else
				{
					tr_Tab_MatterConfidential_Completion.Visible = true;
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["ActualDateOngoing"].ToString().Trim()))
				{
					txt_Tab_Date_Completed.Text = dt.Rows[0]["ActualDateOngoing"].ToString().Trim();
				}
				else
				{
					txt_Tab_Date_Completed.Text = dt.Rows[0]["DateCompleted"].ToString();
				}
				
				if (!string.IsNullOrEmpty(dt.Rows[0]["ProjectName"].ToString()))
				{
					tr_Tab_ProjectName_Core.Visible = true;
					txt_Tab_ProjectName_Core.Text = dt.Rows[0]["ProjectName"].ToString();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["ApplicableLaw"].ToString()))
				{
					tr_Tab_Country_Law.Visible = true;
					cbo_Tab_Country_Law.Text = dt.Rows[0]["ApplicableLaw"].ToString();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["ApplicableLawOther"].ToString()))
				{
					tr_Tab_Country_Law_Other.Visible = true;
					txt_Tab_Country_Law_Other.Text = dt.Rows[0]["ApplicableLawOther"].ToString();
				}

                //Country Where Opened 
				if (!string.IsNullOrEmpty(dt.Rows[0]["ContryWhereMatterOpened"].ToString()))
				{
					txt_Tab_Country_Matter_Open.Text = dt.Rows[0]["ContryWhereMatterOpened"].ToString();
				}
                //Matter Location(s)
				if (!string.IsNullOrEmpty(dt.Rows[0]["MatterLocation"].ToString()))
				{
					txt_Tab_Country_Matter_Close.Text = dt.Rows[0]["MatterLocation"].ToString();
				}

				cbo_Tab_Contentious_IRG.Text = dt.Rows[0]["Contentious"].ToString();
				if (!string.IsNullOrEmpty(dt.Rows[0]["Contentious"].ToString()))
				{
					if (cbo_Tab_Contentious_IRG.Text.Trim().ToUpper() == "BOTH" || cbo_Tab_Contentious_IRG.Text.Trim().ToUpper() == "CONTENTIOUS")
					{
						tr_Tab_Dispute_Resolution.Visible = true;
						cbo_Tab_Dispute_Resolution.Text = dt.Rows[0]["DisputeResolution"].ToString();

                        //Jurisidiction of Dispute
						if (!string.IsNullOrEmpty(dt.Rows[0]["CountryJurisdiction"].ToString()))
						{
							tr_Tab_Country_Jurisdiction.Visible = true;
							txt_Tab_Country_Jurisdiction.Text = dt.Rows[0]["CountryJurisdiction"].ToString();
						}

                        //Language Of Dispute 
						if (!string.IsNullOrEmpty(dt.Rows[0]["LanguageofDispute"].ToString()))
						{
							tr_Tab_Language_Of_Dispute.Visible = true;
							txt_Tab_Language_Of_Dispute.Text = dt.Rows[0]["LanguageofDispute"].ToString();
						}

						if (!string.IsNullOrEmpty(dt.Rows[0]["DisputeResolution"].ToString()))
						{
							if (cbo_Tab_Dispute_Resolution.Text.Trim().ToUpper() == "ARBITRATION")
							{
                                // country of arbitration
								if (!string.IsNullOrEmpty(dt.Rows[0]["CountryArbitration"].ToString().Trim()))
								{
									tr_Tab_Country_ArbitrationCountry.Visible = true;
									txt_Tab_Country_ArbitrationCountry.Text = dt.Rows[0]["CountryArbitration"].ToString();
								}

                                //tr_Tab_Country_ArbitrationCountry.Visible = true;
								if (!string.IsNullOrEmpty(dt.Rows[0]["SeatofArbitration"].ToString().Trim()))
								{
									tr_Tab_ArbitrationCity.Visible = true;
									cbo_Tab_ArbitrationCity.Text = dt.Rows[0]["SeatofArbitration"].ToString();
								}

								if (!string.IsNullOrEmpty(dt.Rows[0]["SeatofArbitrationOther"].ToString().Trim()))
								{
									tr_Tab_ArbitrationCity_Other.Visible = true;
									txt_Tab_ArbitrationCity_Other.Text = dt.Rows[0]["SeatofArbitrationOther"].ToString();
								}
								if (!string.IsNullOrEmpty(dt.Rows[0]["ArbitralRules"].ToString()))
								{
									tr_Tab_Arbitral_Rules.Visible = true;
									cbo_Tab_Arbitral_Rules.Text = dt.Rows[0]["ArbitralRules"].ToString();
								}

								if (!string.IsNullOrEmpty(dt.Rows[0]["InvestmentTreaty"].ToString()))
								{
									tr_Tab_InvestmentTreaty.Visible = true;
									rdo_Tab_InvestmentTreaty.Text = dt.Rows[0]["InvestmentTreaty"].ToString();
								}
							}
							else if (cbo_Tab_Dispute_Resolution.Text.Trim().ToUpper() == "INVESTIGATION")
							{
								if (!string.IsNullOrEmpty(dt.Rows[0]["InvestigationType"].ToString()))
								{
									tr_Tab_Investigation_Type.Visible = true;
									cbo_Tab_Investigation_Type.Text = dt.Rows[0]["InvestigationType"].ToString();
								}
							}
						}
					}
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOfDeal"].ToString().Trim()))
				{
					tr_Tab_ValueOfDeal_Core.Visible = true;
					txt_Tab_ValueOfDeal_Core.Text = dt.Rows[0]["ValueOfDeal"].ToString();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["CurrencyOfDeal"].ToString().Trim()))
				{
					tr_Tab_Currency_Of_Deal.Visible = true;
					cbo_Tab_Currency_Of_Deal.Text = dt.Rows[0]["CurrencyOfDeal"].ToString();
				}

				rdo_Tab_Value_Confidential.Text = dt.Rows[0]["ValueConfidential"].ToString();
				tr_Tab_Value_Confidential.BgColor = "#F5F5DC";
				if (dt.Rows[0]["ValueConfidential"].ToString().ToUpper() == "YES")
				{
					tr_Tab_ValueConfidential_Completion.Visible = true;
					rdo_Tab_ValueConfidential_Completion.Text = dt.Rows[0]["ValueConfidentialCompletion"].ToString();
				}
				else
				{
					rdo_Tab_ValueConfidential_Completion.Text = dt.Rows[0]["ValueConfidentialCompletion"].ToString();
					tr_Tab_ValueConfidential_Completion.Visible = true;
				}

                //Teams 
				if (!string.IsNullOrEmpty(dt.Rows[0]["Team"].ToString()))
				{
					txt_Tab_Team.Text = dt.Rows[0]["Team"].ToString();
				}

                //Lead PartnersLeadPartner
				if (!string.IsNullOrEmpty(dt.Rows[0]["LeadPartner"].ToString()))
				{
					txt_Tab_Lead_Partner.Text = dt.Rows[0]["LeadPartner"].ToString();
				}
				if (txt_Tab_Lead_Partner.Text.ToString().ToUpper().Contains("CMS PARTNER"))
				{
                    /*CMS Parnter Name*/
					if (!string.IsNullOrEmpty(dt.Rows[0]["CMSPartnername"].ToString().Trim()))
					{
						tr_Tab_CMSPartnerName.Visible = true;
                        //dt.Rows[0]["CMSPartnername"].ToString()
						txt_Tab_CMSPartnerName.Text = dt.Rows[0]["CMSPartnername"].ToString().Trim();
					}
					
                    /*Source of credential*/
					if (!string.IsNullOrEmpty(dt.Rows[0]["SourceOfCredential"].ToString()))
					{
						tr_Tab_Source_Of_Credential.Visible = true;
						txt_Tab_Source_Of_Credential.Text = dt.Rows[0]["SourceOfCredential"].ToString();
					}
                    /*oTHER*/
					if (txt_Tab_Source_Of_Credential.Text.ToString().ToUpper().Contains("OTHER"))
					{
						if (!string.IsNullOrEmpty(dt.Rows[0]["SourceofCredentialOther"].ToString().Trim()))
						{
							tr_Tab_SourceOfCredential_Other.Visible = true;
							txt_Tab_SourceOfCredential_Other.Text = dt.Rows[0]["SourceofCredentialOther"].ToString().Trim();
						}
					}
				}

                //dt.Rows[0]["SourceofCredentialOther"].ToString()
                //Other Matter Executives
				if (!string.IsNullOrEmpty(dt.Rows[0]["OtherMatterExecutive"].ToString()))
				{
					tr_Tab_Other_Matter_Executive.Visible = true;
					txt_Tab_Other_Matter_Executive.Text = dt.Rows[0]["OtherMatterExecutive"].ToString();
				}

                //CMS Firms Involved
				if (!string.IsNullOrEmpty(dt.Rows[0]["CMSFirmsInvolved"].ToString()))
				{
					tr_Tab_Referred_From_Other_CMS_Office.Visible = true;
					txt_Tab_Referred_From_Other_CMS_Office.Text = dt.Rows[0]["CMSFirmsInvolved"].ToString();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["LeadCMSFirm"].ToString().Trim()))
				{
					tr_Tab_Lead_CMS_Firm.Visible = true;
					cbo_Tab_Lead_CMS_Firm.Text = dt.Rows[0]["LeadCMSFirm"].ToString();
				}

                //Countries of other CMS firms
				if (!string.IsNullOrEmpty(dt.Rows[0]["CountriesofotherCMSFirms"].ToString()))
				{
					tr_Tab_Country_OtherCMSOffice.Visible = true;
					txt_Tab_Country_OtherCMSOffice.Text = dt.Rows[0]["CountriesofotherCMSFirms"].ToString();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialStatus"].ToString().Trim()))
				{
					cbo_Tab_Credential_Status.Text = dt.Rows[0]["CredentialStatus"].ToString();
				}

                /* CredentialVersion */
				if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialVersion"].ToString().Trim()))
				{
					cbo_Tab_Credential_Version.Text = dt.Rows[0]["CredentialVersion"].ToString();
				}

                /*Credntial Version other*/
				if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialVersionOther"].ToString().Trim()))
				{
					tr_Tab_Credential_Version_Other.Visible = true;
					txt_Tab_Credential_Version_Other.Text = dt.Rows[0]["CredentialVersionOther"].ToString();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialType"].ToString().Trim()))
				{
					cbo_Tab_Credential_Type.Text = dt.Rows[0]["CredentialType"].ToString();
				}
                //Other Uses
				if (!string.IsNullOrEmpty(dt.Rows[0]["OtherUses"].ToString()))
				{
					tr_Tab_Other_Uses.Visible = true;
					txt_Tab_Other_Uses.Text = dt.Rows[0]["OtherUses"].ToString();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["Priority"].ToString().Trim()))
				{
					tr_Tab_Priority.Visible = true;
					cbo_Tab_Priority.Text = dt.Rows[0]["Priority"].ToString().Trim();
				}
                //Know How 
				if (!string.IsNullOrEmpty(dt.Rows[0]["KnowHow"].ToString()))
				{
					tr_Tab_Know_How.Visible = true;
					txt_Tab_Know_How.Text = dt.Rows[0]["KnowHow"].ToString();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["ProBono"].ToString().Trim()))
				{
					tr_Tab_ProBono.Visible = true;
					rdo_Tab_ProBono.Text = dt.Rows[0]["ProBono"].ToString().Trim();
				}

				if (!string.IsNullOrEmpty(dt.Rows[0]["BibleReference"].ToString().Trim()))
				{
					tr_Tab_Bible_Reference.Visible = true;
					txt_Tab_Bible_Reference.Text = dt.Rows[0]["BibleReference"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["Date_Created"].ToString().Trim()))
				{
					txt_Tab_Date_Created.Text = dt.Rows[0]["Date_Created"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["Created_By"].ToString().Trim()))
				{
					txt_Tab_Created_By.Text = dt.Rows[0]["Created_By"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["Date_Updated"].ToString().Trim()))
				{
					txt_Tab_Date_Updated.Text = dt.Rows[0]["Date_Updated"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["Updated_By"].ToString().Trim()))
				{
					txt_Tab_Updated_By.Text = dt.Rows[0]["Updated_By"].ToString();
				}

                /* BAIF Fields */
				if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeBAIF"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["BAIFWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["LeadBanks"].ToString().Trim()))
				{
					trBAIF.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeBAIF"].ToString().Trim()))
					{
						tr_BAI_ClientTypeIdBAIF.Visible = true;
						cbo_BAI_ClientTypeIdBAIF.Text = dt.Rows[0]["ClientTypeBAIF"].ToString();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["BAIFWorkType"].ToString().Trim()))
					{
						tr_BAI_Work_Type.Visible = true;
						txt_BAI_Work_Type.Text = dt.Rows[0]["BAIFWorkType"].ToString();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["LeadBanks"].ToString().Trim()))
					{
						tr_BAI_LeadBanks.Visible = true;
						txt_BAI_LeadBanks.Text = dt.Rows[0]["LeadBanks"].ToString().Trim();
					}
				}

                /* CORPORATE Fields */
				if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["PEClients"].ToString().Trim()))
				{
					trCorp.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateWorkType"].ToString().Trim()))
					{
						tr_Cor_Work_Type.Visible = true;
						txt_Cor_Work_Type.Text = dt.Rows[0]["CorporateWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateSubWorkType"].ToString().Trim()))
					{
						txt_Cor_SubWork_Type.Text = dt.Rows[0]["CorporateSubWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateActingFor"].ToString().Trim()))
					{
						tr_Cor_Acting_For.Visible = true;
						txt_Cor_Acting_For.Text = dt.Rows[0]["CorporateActingFor"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateCountryBuyer"].ToString().Trim()))
					{
						tr_Cor_Country_Buyer.Visible = true;
						txt_Cor_Country_Buyer.Text = dt.Rows[0]["CorporateCountryBuyer"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateCountrySeller"].ToString().Trim()))
					{
						tr_Cor_Country_Seller.Visible = true;
						txt_Cor_Country_Seller.Text = dt.Rows[0]["CorporateCountrySeller"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateCountryTarget"].ToString().Trim()))
					{
						tr_Cor_Country_Target.Visible = true;
						txt_Cor_Country_Target.Text = dt.Rows[0]["CorporateCountryTarget"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverUS"].ToString().Trim()))
					{
						tr_Cor_Value_Over_US.Visible = true;
						cbo_Cor_Value_Over_US.Text = dt.Rows[0]["ValueOverUS"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverPound"].ToString().Trim()))
					{
						tr_Cor_Value_Over_Pound.Visible = true;
						cbo_Cor_Value_Over_Pound.Text = dt.Rows[0]["ValueOverPound"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverEuro"].ToString().Trim()))
					{
						tr_Cor_Value_Over_Euro.Visible = true;
						cbo_Cor_Value_Over_Euro.Text = dt.Rows[0]["ValueOverEuro"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ValueRangeEuro"].ToString().Trim()))
					{
						tr_Cor_ValueRangeEuro.Visible = true;
						cbo_Cor_ValueRangeEuro.Text = dt.Rows[0]["ValueRangeEuro"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["PublishedReference"].ToString().Trim()))
					{
						tr_Cor_Published_Reference.Visible = true;
						txt_Cor_Published_Reference.Text = dt.Rows[0]["PublishedReference"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["MAStudy"].ToString().Trim()))
					{
						tr_Cor_MAStudy.Visible = true;
						cbo_Cor_MAStudy.Text = dt.Rows[0]["MAStudy"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["PEClients"].ToString().Trim()))
					{
						tr_Cor_PEClients.Visible = true;
						cbo_Cor_PEClients.Text = dt.Rows[0]["PEClients"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["QuarterDealAnnounceID"].ToString().Trim()))
					{
						tr_Cor_QuarterDealAnnouncedId.Visible = true;
						cbo_Cor_QuarterDealAnnouncedId.Text = dt.Rows[0]["QuarterDealAnnounceID"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["QuarterDealCompletedId"].ToString().Trim()))
					{
						tr_Cor_QuarterDealCompletedId.Visible = true;
						cbo_Cor_QuarterDealCompletedId.Text = dt.Rows[0]["QuarterDealCompletedId"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["YearDealCompletedID"].ToString().Trim()))
					{
						tr_Cor_YearDealCompletedId.Visible = true;
						cbo_Cor_YearDealCompletedId.Text = dt.Rows[0]["YearDealCompletedID"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["YearDealAnnounced"].ToString().Trim()))
					{
						tr_Cor_YearDeal_Announced.Visible = true;
						cbo_Cor_YearDeal_Announced.Text = dt.Rows[0]["YearDealAnnounced"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["DealAnnouncedID"].ToString().Trim()))
					{
						tr_Cor_DealAnnouncedId.Visible = true;
						txt_Cor_DealAnnouncedId.Text = dt.Rows[0]["DealAnnouncedID"].ToString().Trim();
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["WorkTypeCorpTax"].ToString().Trim()))
				{
					trCorpTax.Visible = true;
					cbo_Crt_WorkType_CorpTax.Text = dt.Rows[0]["WorkTypeCorpTax"].ToString().Trim();
				}

                /* CRD */
				if (!string.IsNullOrEmpty(dt.Rows[0]["CRDWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["CRDSubWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIdCommercial"].ToString().Trim()))
				{
					trCRD.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["CRDWorkType"].ToString().Trim()))
					{
						tr_CRD_Work_Type.Visible = true;
						txt_CRD_Work_Type.Text = dt.Rows[0]["CRDWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CRDSubWorkType"].ToString().Trim()))
					{
						tr_CRD_SubWork_Type.Visible = true;
						txt_CRD_SubWork_Type.Text = dt.Rows[0]["CRDSubWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIdCommercial"].ToString().Trim()))
					{
						tr_CRD_ClientTypeIdCommercial.Visible = true;
						cbo_CRD_ClientTypeIdCommercial.Text = dt.Rows[0]["ClientTypeIdCommercial"].ToString().Trim();
					}
				}

                /* EPC */
				if (!string.IsNullOrEmpty(dt.Rows[0]["EPCNatureofWork"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ClientScope"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIDEPC"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["EPCTypeofContract"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["SubjectMatterIDEPC"].ToString().Trim()))
				{
					trEPC.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["EPCNatureofWork"].ToString().Trim()))
					{
						tr_EPC_Nature_Of_Work.Visible = true;
						txt_EPC_Nature_Of_Work.Text = dt.Rows[0]["EPCNatureofWork"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientScope"].ToString().Trim()))
					{
						tr_EPC_ClientScopeId.Visible = true;
						cbo_EPC_ClientScopeId.Text = dt.Rows[0]["ClientScope"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIDEPC"].ToString().Trim()))
					{
						tr_EPC_ClientTypeIdEPC.Visible = true;
						cbo_EPC_ClientTypeIdEPC.Text = dt.Rows[0]["ClientTypeIDEPC"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeOtherEPC"].ToString().Trim()))
					{
						tr_EPC_ClientTypeOther.Visible = true;
						txt_EPC_ClientTypeOther.Text = dt.Rows[0]["ClientTypeOtherEPC"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["EPCTypeofContract"].ToString().Trim()))
					{
						tr_EPC_Type_Of_Contract.Visible = true;
						txt_EPC_Type_Of_Contract.Text = dt.Rows[0]["EPCTypeofContract"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["TypeofContractOtherEPC"].ToString().Trim()))
					{
						tr_EPC_Type_Of_Contract_Other.Visible = true;
						txt_EPC_Type_Of_Contract_Other.Text = dt.Rows[0]["TypeofContractOtherEPC"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["SubjectMatterIDEPC"].ToString().Trim()))
					{
						tr_EPC_SubjectMatterId.Visible = true;
						cbo_EPC_SubjectMatterId.Text = dt.Rows[0]["SubjectMatterIDEPC"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["SubjectMatterOtherEPC"].ToString().Trim()))
					{
						tr_EPC_Subject_Matter_Other.Visible = true;
						txt_EPC_SubjectMatterOther.Text = dt.Rows[0]["SubjectMatterOtherEPC"].ToString().Trim();
					}
				}

                /* EPC Energy */
				if (!string.IsNullOrEmpty(dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ContractTypeIDEPCE"].ToString().Trim()))
				{
					trENE.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim()))
					{
						tr_ENE_Transaction_Type.Visible = true;
						txt_ENE_Transaction_Type.Text = dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ContractTypeIDEPCE"].ToString().Trim()))
					{
						tr_ENE_ContractTypeId.Visible = true;
						cbo_ENE_ContractTypeId.Text = dt.Rows[0]["ContractTypeIDEPCE"].ToString().Trim();
					}
				}
				
				if (!string.IsNullOrEmpty(dt.Rows[0]["HCWorkType"].ToString().Trim()))
				{
					tr_HCC_WorkTypeIdHC.Visible = true;
					cbo_HCC_WorkTypeIdHC.Text = dt.Rows[0]["HCWorkType"].ToString().Trim();
				}
				
				if (!string.IsNullOrEmpty(dt.Rows[0]["HCSubWorkType"].ToString().Trim()))
				{
					txt_HCC_SubWorkTypeIdHC.Text = dt.Rows[0]["HCSubWorkType"].ToString().Trim();
				}
				
				if (!string.IsNullOrEmpty(dt.Rows[0]["PensionSchemeHC"].ToString().Trim()))
				{
					trHC.Visible = true;
					tr_HCC_PensionSchemeHC.Visible = true;
					cbo_HCC_PensionSchemeHC.Text = dt.Rows[0]["PensionSchemeHC"].ToString().Trim();
				}

                /* IPF */
				if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIdIPF"].ToString().Trim()))
				{
					trIPF.Visible = true;
					tr_IPF_ClientTypeIdIPF.Visible = true;
					cbo_IPF_ClientTypeIdIPF.Text = dt.Rows[0]["ClientTypeIdIPF"].ToString().Trim();
				}

                /* Real Estate */
				if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateClientType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["RealEstateWorkType"].ToString().Trim()))
				{
					trRE.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateClientType"].ToString().Trim()))
					{
						tr_RES_Client_Type.Visible = true;
						txt_RES_Client_Type.Text = dt.Rows[0]["RealEstateClientType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateWorkType"].ToString().Trim()))
					{
						tr_RES_Work_Type.Visible = true;
						txt_RES_Work_Type.Text = dt.Rows[0]["RealEstateWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateSubWorkType"].ToString().Trim()))
					{
						txt_RES_SubWork_Type.Text = dt.Rows[0]["RealEstateSubWorkType"].ToString().Trim();
					}
				}
			}
		}
		
		private void PopulateDataFromSearchPrint(string strCredentialID)
		{
			string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
			SqlConnection con = new SqlConnection(strcon);
			string selSQL = string.Empty;
			StreamReader sr = new StreamReader(base.Server.MapPath("~\\Queries\\SearchResultQuery_View.txt"));
			selSQL = sr.ReadToEnd();
			selSQL = selSQL.Replace("strCredentialID", "'" + strCredentialID + "'");
			sr.Dispose();
			SqlDataAdapter adp = new SqlDataAdapter(selSQL, con);
			DataTable dt = new DataTable();
			adp.Fill(dt);
			adp.Dispose();
			if (dt.Rows.Count > 0)
			{
				if (!string.IsNullOrEmpty(dt.Rows[0]["KeyWord"].ToString().Trim()))
				{
					V1_tr_Tab_Keyword.Visible = true;
					V1_txt_Tab_Keyword.Text = dt.Rows[0]["KeyWord"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ClientName"].ToString().Trim()))
				{
					V1_txt_Tab_Client.Text = dt.Rows[0]["ClientName"].ToString().Trim();
				}
				V1_rdo_Tab_Client_Name_Confidential.Text = dt.Rows[0]["ClientNameConfidential"].ToString();
				V1_tr_Tab_Client_Name_Confidential.BgColor = "#F5F5DC";
				if (dt.Rows[0]["ClientNameConfidential"].ToString().ToUpper() == "YES")
				{
					V1_tr_Tab_ClientDescription.Visible = true;
					V1_tr_Tab_NameConfidential_Completion.Visible = true;
					V1_txt_Tab_ClientDescription.Text = dt.Rows[0]["ClientDescription"].ToString();
					V1_rdo_Tab_NameConfidential_Completion.Text = dt.Rows[0]["ClientNameConfidentialCompletion"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ClientSector"].ToString()))
				{
					V1_txt_Tab_ClientIndustrySector.Text = dt.Rows[0]["ClientSector"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ClientSubSector"].ToString()))
				{
					V1_tr_Tab_Client_Industry_Type.Visible = true;
					V1_txt_Tab_Client_Industry_Type.Text = dt.Rows[0]["ClientSubSector"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["PredominantCountryofClient"].ToString()))
				{
					V1_tr_Tab_Country_PredominantCountry.Visible = true;
					V1_txt_Tab_Country_PredominantCountry.Text = dt.Rows[0]["PredominantCountryofClient"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["PracticeGroup"].ToString()))
				{
					V1_txt_Tab_PracticeGroup.Text = dt.Rows[0]["PracticeGroup"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["MatterNumber"].ToString()))
				{
					V1_txt_Tab_Matter_No.Text = dt.Rows[0]["MatterNumber"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["DateMatterOpened"].ToString()))
				{
					V1_txt_Tab_Date_Opened.Text = dt.Rows[0]["DateMatterOpened"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["MatterSector"].ToString()))
				{
					V1_txt_Tab_TransactionIndustrySector.Text = dt.Rows[0]["MatterSector"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["MatterSubSector"].ToString()))
				{
					V1_txt_Tab_Transaction_Industry_Type.Text = dt.Rows[0]["MatterSubSector"].ToString();
				}
				string strClientName = dt.Rows[0]["ClientName"].ToString().Trim();
				string strClientDescription = dt.Rows[0]["ClientDescription"].ToString();
				string strMatterDescription = dt.Rows[0]["MatterDescription"].ToString();
				string strDesc = string.Empty;
				string strClientNameandMatter = string.Empty;
				if (strMatterDescription.Trim().StartsWith(","))
				{
					strClientNameandMatter = strClientName + strMatterDescription;
				}
				else
				{
					strClientNameandMatter = strClientName + " " + strMatterDescription;
				}
				string strClientDescMatterDesc = string.Empty;
				if (strMatterDescription.Trim().StartsWith(","))
				{
					strClientDescMatterDesc = strClientDescription + strMatterDescription;
				}
				else
				{
					strClientDescMatterDesc = strClientDescription + " " + strMatterDescription;
				}
				if (dt.Rows[0]["ClientNameConfidential"].ToString() == "Yes" && dt.Rows[0]["ClientMatterConfidential"].ToString() == "Yes" && dt.Rows[0]["ValueConfidential"].ToString() == "Yes")
				{
					strDesc = "[confidential – name/matter/value] [" + strClientName + "] " + strClientDescMatterDesc;
				}
				else
				{
					if (dt.Rows[0]["ClientNameConfidential"].ToString() == "Yes" && dt.Rows[0]["ClientMatterConfidential"].ToString() == "Yes")
					{
						strDesc = "[confidential – name/matter] [" + strClientName + "] " + strClientDescMatterDesc;
					}
					else
					{
						if (dt.Rows[0]["ClientNameConfidential"].ToString() == "Yes" && dt.Rows[0]["ValueConfidential"].ToString() == "Yes")
						{
							strDesc = "[confidential – name/value] [" + strClientName + "] " + strClientDescMatterDesc;
						}
						else
						{
							if (dt.Rows[0]["ClientMatterConfidential"].ToString() == "Yes" && dt.Rows[0]["ValueConfidential"].ToString() == "Yes")
							{
								strDesc = "[confidential – matter/value] " + strClientNameandMatter;
							}
							else
							{
								if (dt.Rows[0]["ClientNameConfidential"].ToString() == "Yes")
								{
									strDesc = "[confidential – client name] [" + strClientName + "] " + strClientDescMatterDesc;
								}
								else
								{
									if (dt.Rows[0]["ClientMatterConfidential"].ToString() == "Yes")
									{
										strDesc = "[confidential – matter] " + strClientNameandMatter;
									}
									else
									{
										if (dt.Rows[0]["ValueConfidential"].ToString() == "Yes")
										{
											strDesc = "[confidential – value] " + strClientNameandMatter;
										}
										else
										{
											strDesc = strClientNameandMatter;
										}
									}
								}
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["MatterDescription"].ToString()))
				{
					V1_txt_Tab_Project_Description.Text = strDesc;
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["OtherMatterDescription"].ToString()))
				{
					V1_tr_Tab_Significant_Features.Visible = true;
					V1_txt_Tab_Significant_Features.Text = dt.Rows[0]["OtherMatterDescription"].ToString();
				}
				StringBuilder strAppend = new StringBuilder();
				if (!string.IsNullOrEmpty(dt.Rows[0]["BAIFWorkType"].ToString().Trim()))
				{
					strAppend.Append(dt.Rows[0]["BAIFWorkType"].ToString().Trim());
					strAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateWorkType"].ToString().Trim()))
				{
					strAppend.Append(dt.Rows[0]["CorporateWorkType"].ToString().Trim());
					strAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CRDWorkType"].ToString().Trim()))
				{
					strAppend.Append(dt.Rows[0]["CRDWorkType"].ToString().Trim());
					strAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["EPCNatureofWork"].ToString().Trim()))
				{
					strAppend.Append(dt.Rows[0]["EPCNatureofWork"].ToString().Trim());
					strAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim()))
				{
					strAppend.Append(dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim());
					strAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["HCWorkType"].ToString().Trim()))
				{
					strAppend.Append(dt.Rows[0]["HCWorkType"].ToString().Trim());
					strAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateWorkType"].ToString().Trim()))
				{
					strAppend.Append(dt.Rows[0]["RealEstateWorkType"].ToString().Trim());
					strAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(strAppend.ToString()))
				{
					V1_txt_Tab_WorkType.Text = strAppend.ToString().Trim().Substring(0, strAppend.ToString().Trim().Length - 1);
					V1_tr_Tab_WorkType.Visible = true;
				}
				string strCorporateSubWorkType = string.Empty;
				strCorporateSubWorkType = dt.Rows[0]["CorporateSubWorkType"].ToString().Trim();
				string strCRDSubWorkType = string.Empty;
				strCRDSubWorkType = dt.Rows[0]["CRDSubWorkType"].ToString().Trim();
				string strRealEstateSubWorkType = string.Empty;
				strRealEstateSubWorkType = dt.Rows[0]["RealEstateSubWorkType"].ToString().Trim();
				string strSubWorkTypeHC = string.Empty;
				strSubWorkTypeHC = dt.Rows[0]["HCSubWorkType"].ToString().Trim();
				StringBuilder strSubWTAppend = new StringBuilder();
				if (!string.IsNullOrEmpty(strCorporateSubWorkType.Trim()))
				{
					strSubWTAppend.Append(strCorporateSubWorkType.Trim());
					strSubWTAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(strCRDSubWorkType.Trim()))
				{
					strSubWTAppend.Append(strCRDSubWorkType.Trim());
					strSubWTAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(strRealEstateSubWorkType.Trim()))
				{
					strSubWTAppend.Append(strRealEstateSubWorkType.Trim());
					strSubWTAppend.Append("; ");
				}
				if (!string.IsNullOrEmpty(strSubWorkTypeHC.Trim()))
				{
					strSubWTAppend.Append(strSubWorkTypeHC.Trim());
					strSubWTAppend.Append("; ");
				}
				if (strSubWTAppend != null && strSubWTAppend.ToString().Length > 1)
				{
					V1_tr_Tab_SubWorkType.Visible = true;
					V1_txt_Tab_SubWorkType.Text = strSubWTAppend.ToString().Substring(0, strSubWTAppend.ToString().Length - 1);
				}
				V1_rdo_Tab_Client_Matter_Confidential.Text = dt.Rows[0]["ClientMatterConfidential"].ToString();
				V1_tr_Tab_Client_Matter_Confidential.BgColor = "#F5F5DC";
				if (dt.Rows[0]["ClientMatterConfidential"].ToString().ToUpper() == "YES")
				{
					V1_tr_Tab_MatterConfidential_Completion.Visible = true;
					V1_rdo_Tab_MatterConfidential_Completion.Text = dt.Rows[0]["MatterConfidentialCompletion"].ToString();
				}
				else
				{
					V1_tr_Tab_MatterConfidential_Completion.Visible = true;
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ActualDateOngoing"].ToString().Trim()))
				{
					V1_txt_Tab_Date_Completed.Text = dt.Rows[0]["ActualDateOngoing"].ToString().Trim();
				}
				else
				{
					V1_txt_Tab_Date_Completed.Text = dt.Rows[0]["DateCompleted"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ProjectName"].ToString()))
				{
					V1_tr_Tab_ProjectName_Core.Visible = true;
					V1_txt_Tab_ProjectName_Core.Text = dt.Rows[0]["ProjectName"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ApplicableLaw"].ToString()))
				{
					V1_tr_Tab_Country_Law.Visible = true;
					V1_cbo_Tab_Country_Law.Text = dt.Rows[0]["ApplicableLaw"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ApplicableLawOther"].ToString()))
				{
					V1_tr_Tab_Country_Law_Other.Visible = true;
					V1_txt_Tab_Country_Law_Other.Text = dt.Rows[0]["ApplicableLawOther"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ContryWhereMatterOpened"].ToString()))
				{
					V1_txt_Tab_Country_Matter_Open.Text = dt.Rows[0]["ContryWhereMatterOpened"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["MatterLocation"].ToString()))
				{
					V1_txt_Tab_Country_Matter_Close.Text = dt.Rows[0]["MatterLocation"].ToString();
				}
				V1_cbo_Tab_Contentious_IRG.Text = dt.Rows[0]["Contentious"].ToString();
				if (!string.IsNullOrEmpty(dt.Rows[0]["Contentious"].ToString()))
				{
					if (V1_cbo_Tab_Contentious_IRG.Text.Trim().ToUpper() == "BOTH" || V1_cbo_Tab_Contentious_IRG.Text.Trim().ToUpper() == "CONTENTIOUS")
					{
						V1_tr_Tab_Dispute_Resolution.Visible = true;
						V1_cbo_Tab_Dispute_Resolution.Text = dt.Rows[0]["DisputeResolution"].ToString();
						if (!string.IsNullOrEmpty(dt.Rows[0]["CountryJurisdiction"].ToString()))
						{
							V1_tr_Tab_Country_Jurisdiction.Visible = true;
							V1_txt_Tab_Country_Jurisdiction.Text = dt.Rows[0]["CountryJurisdiction"].ToString();
						}
						if (!string.IsNullOrEmpty(dt.Rows[0]["LanguageofDispute"].ToString()))
						{
							V1_tr_Tab_Language_Of_Dispute.Visible = true;
							V1_txt_Tab_Language_Of_Dispute.Text = dt.Rows[0]["LanguageofDispute"].ToString();
						}
						if (!string.IsNullOrEmpty(dt.Rows[0]["DisputeResolution"].ToString()))
						{
							if (V1_cbo_Tab_Dispute_Resolution.Text.Trim().ToUpper() == "ARBITRATION")
							{
								if (!string.IsNullOrEmpty(dt.Rows[0]["CountryArbitration"].ToString().Trim()))
								{
									V1_tr_Tab_Country_ArbitrationCountry.Visible = true;
									V1_txt_Tab_Country_ArbitrationCountry.Text = dt.Rows[0]["CountryArbitration"].ToString();
								}
								if (!string.IsNullOrEmpty(dt.Rows[0]["SeatofArbitration"].ToString().Trim()))
								{
									V1_tr_Tab_ArbitrationCity.Visible = true;
									V1_cbo_Tab_ArbitrationCity.Text = dt.Rows[0]["SeatofArbitration"].ToString();
								}
								if (!string.IsNullOrEmpty(dt.Rows[0]["SeatofArbitrationOther"].ToString().Trim()))
								{
									V1_tr_Tab_ArbitrationCity_Other.Visible = true;
									V1_txt_Tab_ArbitrationCity_Other.Text = dt.Rows[0]["SeatofArbitrationOther"].ToString();
								}
								if (!string.IsNullOrEmpty(dt.Rows[0]["ArbitralRules"].ToString()))
								{
									V1_tr_Tab_Arbitral_Rules.Visible = true;
									V1_cbo_Tab_Arbitral_Rules.Text = dt.Rows[0]["ArbitralRules"].ToString();
								}
								if (!string.IsNullOrEmpty(dt.Rows[0]["InvestmentTreaty"].ToString()))
								{
									V1_tr_Tab_InvestmentTreaty.Visible = true;
									V1_rdo_Tab_InvestmentTreaty.Text = dt.Rows[0]["InvestmentTreaty"].ToString();
								}
							}
							else
							{
								if (V1_cbo_Tab_Dispute_Resolution.Text.Trim().ToUpper() == "INVESTIGATION")
								{
									if (!string.IsNullOrEmpty(dt.Rows[0]["InvestigationType"].ToString()))
									{
										V1_tr_Tab_Investigation_Type.Visible = true;
										V1_cbo_Tab_Investigation_Type.Text = dt.Rows[0]["InvestigationType"].ToString();
									}
								}
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOfDeal"].ToString().Trim()))
				{
					V1_tr_Tab_ValueOfDeal_Core.Visible = true;
					V1_txt_Tab_ValueOfDeal_Core.Text = dt.Rows[0]["ValueOfDeal"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CurrencyOfDeal"].ToString().Trim()))
				{
					V1_tr_Tab_Currency_Of_Deal.Visible = true;
					V1_cbo_Tab_Currency_Of_Deal.Text = dt.Rows[0]["CurrencyOfDeal"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ValueConfidential"].ToString().Trim()))
				{
					V1_tr_Tab_Value_Confidential.BgColor = "#F5F5DC";
					V1_tr_Tab_Value_Confidential.Visible = true;
					V1_rdo_Tab_Value_Confidential.Text = dt.Rows[0]["ValueConfidential"].ToString();
				}
				if (dt.Rows[0]["ValueConfidential"].ToString().ToUpper() == "YES")
				{
					V1_tr_Tab_ValueConfidential_Completion.Visible = true;
					V1_rdo_Tab_ValueConfidential_Completion.Text = dt.Rows[0]["ValueConfidentialCompletion"].ToString();
				}
				else
				{
					V1_tr_Tab_ValueConfidential_Completion.Visible = true;
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["Team"].ToString()))
				{
					V1_txt_Tab_Team.Text = dt.Rows[0]["Team"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["LeadPartner"].ToString()))
				{
					V1_txt_Tab_Lead_Partner.Text = dt.Rows[0]["LeadPartner"].ToString();
				}
				if (V1_txt_Tab_Lead_Partner.Text.ToString().ToUpper().Contains("CMS PARTNER"))
				{
					if (!string.IsNullOrEmpty(dt.Rows[0]["CMSPartnername"].ToString().Trim()))
					{
						V1_tr_Tab_CMSPartnerName.Visible = true;
						V1_txt_Tab_CMSPartnerName.Text = dt.Rows[0]["CMSPartnername"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["SourceOfCredential"].ToString()))
					{
						V1_tr_Tab_Source_Of_Credential.Visible = true;
						V1_txt_Tab_Source_Of_Credential.Text = dt.Rows[0]["SourceOfCredential"].ToString();
					}
					if (V1_txt_Tab_Source_Of_Credential.Text.ToString().ToUpper().Contains("OTHER"))
					{
						if (!string.IsNullOrEmpty(dt.Rows[0]["SourceofCredentialOther"].ToString().Trim()))
						{
							V1_tr_Tab_SourceOfCredential_Other.Visible = true;
							V1_txt_Tab_SourceOfCredential_Other.Text = dt.Rows[0]["SourceofCredentialOther"].ToString().Trim();
						}
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["OtherMatterExecutive"].ToString()))
				{
					V1_tr_Tab_Other_Matter_Executive.Visible = true;
					V1_txt_Tab_Other_Matter_Executive.Text = dt.Rows[0]["OtherMatterExecutive"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CMSFirmsInvolved"].ToString()))
				{
					V1_tr_Tab_Referred_From_Other_CMS_Office.Visible = true;
					V1_txt_Tab_Referred_From_Other_CMS_Office.Text = dt.Rows[0]["CMSFirmsInvolved"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["LeadCMSFirm"].ToString().Trim()))
				{
					V1_cbo_Tab_Lead_CMS_Firm.Text = dt.Rows[0]["LeadCMSFirm"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CountriesofotherCMSFirms"].ToString()))
				{
					V1_tr_Tab_Country_OtherCMSOffice.Visible = true;
					V1_txt_Tab_Country_OtherCMSOffice.Text = dt.Rows[0]["CountriesofotherCMSFirms"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialStatus"].ToString().Trim()))
				{
					V1_cbo_Tab_Credential_Status.Text = dt.Rows[0]["CredentialStatus"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialVersion"].ToString().Trim()))
				{
					V1_cbo_Tab_Credential_Version.Text = dt.Rows[0]["CredentialVersion"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialVersionOther"].ToString().Trim()))
				{
					V1_tr_Tab_Credential_Version_Other.Visible = true;
					V1_txt_Tab_Credential_Version_Other.Text = dt.Rows[0]["CredentialVersionOther"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CredentialType"].ToString().Trim()))
				{
					V1_cbo_Tab_Credential_Type.Text = dt.Rows[0]["CredentialType"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["OtherUses"].ToString()))
				{
					V1_tr_Tab_Other_Uses.Visible = true;
					V1_txt_Tab_Other_Uses.Text = dt.Rows[0]["OtherUses"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["Priority"].ToString().Trim()))
				{
					V1_tr_Tab_Priority.Visible = true;
					V1_cbo_Tab_Priority.Text = dt.Rows[0]["Priority"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["KnowHow"].ToString()))
				{
					V1_tr_Tab_Know_How.Visible = true;
					V1_txt_Tab_Know_How.Text = dt.Rows[0]["KnowHow"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ProBono"].ToString().Trim()))
				{
					V1_tr_Tab_ProBono.Visible = true;
					V1_rdo_Tab_ProBono.Text = dt.Rows[0]["ProBono"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["BibleReference"].ToString().Trim()))
				{
					V1_tr_Tab_Bible_Reference.Visible = true;
					V1_txt_Tab_Bible_Reference.Text = dt.Rows[0]["BibleReference"].ToString();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeBAIF"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["BAIFWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["LeadBanks"].ToString().Trim()))
				{
					trBAIF.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeBAIF"].ToString().Trim()))
					{
						V1_tr_BAI_ClientTypeIdBAIF.Visible = true;
						V1_cbo_BAI_ClientTypeIdBAIF.Text = dt.Rows[0]["ClientTypeBAIF"].ToString();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["BAIFWorkType"].ToString().Trim()))
					{
						V1_tr_BAI_Work_Type.Visible = true;
						V1_txt_BAI_Work_Type.Text = dt.Rows[0]["BAIFWorkType"].ToString();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["LeadBanks"].ToString().Trim()))
					{
						V1_tr_BAI_LeadBanks.Visible = true;
						V1_txt_BAI_LeadBanks.Text = dt.Rows[0]["LeadBanks"].ToString().Trim();
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["PEClients"].ToString().Trim()))
				{
					trCorp.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateWorkType"].ToString().Trim()))
					{
						V1_tr_Cor_Work_Type.Visible = true;
						V1_txt_Cor_Work_Type.Text = dt.Rows[0]["CorporateWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateSubWorkType"].ToString().Trim()))
					{
						V1_txt_Cor_SubWork_Type.Text = dt.Rows[0]["CorporateSubWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateActingFor"].ToString().Trim()))
					{
						V1_tr_Cor_Acting_For.Visible = true;
						V1_txt_Cor_Acting_For.Text = dt.Rows[0]["CorporateActingFor"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateCountryBuyer"].ToString().Trim()))
					{
						V1_tr_Cor_Country_Buyer.Visible = true;
						V1_txt_Cor_Country_Buyer.Text = dt.Rows[0]["CorporateCountryBuyer"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateCountrySeller"].ToString().Trim()))
					{
						V1_tr_Cor_Country_Seller.Visible = true;
						V1_txt_Cor_Country_Seller.Text = dt.Rows[0]["CorporateCountrySeller"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CorporateCountryTarget"].ToString().Trim()))
					{
						V1_tr_Cor_Country_Target.Visible = true;
						V1_txt_Cor_Country_Target.Text = dt.Rows[0]["CorporateCountryTarget"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverUS"].ToString().Trim()))
					{
						V1_tr_Cor_Value_Over_US.Visible = true;
						V1_cbo_Cor_Value_Over_US.Text = dt.Rows[0]["ValueOverUS"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverPound"].ToString().Trim()))
					{
						V1_tr_Cor_Value_Over_Pound.Visible = true;
						V1_cbo_Cor_Value_Over_Pound.Text = dt.Rows[0]["ValueOverPound"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverEuro"].ToString().Trim()))
					{
						V1_tr_Cor_Value_Over_Euro.Visible = true;
						V1_cbo_Cor_Value_Over_Euro.Text = dt.Rows[0]["ValueOverEuro"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ValueRangeEuro"].ToString().Trim()))
					{
						V1_tr_Cor_ValueRangeEuro.Visible = true;
						V1_cbo_Cor_ValueRangeEuro.Text = dt.Rows[0]["ValueRangeEuro"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["PublishedReference"].ToString().Trim()))
					{
						V1_tr_Cor_Published_Reference.Visible = true;
						V1_txt_Cor_Published_Reference.Text = dt.Rows[0]["PublishedReference"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["MAStudy"].ToString().Trim()))
					{
						V1_tr_Cor_MAStudy.Visible = true;
						V1_cbo_Cor_MAStudy.Text = dt.Rows[0]["MAStudy"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["PEClients"].ToString().Trim()))
					{
						V1_tr_Cor_PEClients.Visible = true;
						V1_cbo_Cor_PEClients.Text = dt.Rows[0]["PEClients"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["QuarterDealAnnounceID"].ToString().Trim()))
					{
						V1_tr_Cor_QuarterDealAnnouncedId.Visible = true;
						V1_cbo_Cor_QuarterDealAnnouncedId.Text = dt.Rows[0]["QuarterDealAnnounceID"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["QuarterDealCompletedId"].ToString().Trim()))
					{
						V1_tr_Cor_QuarterDealCompletedId.Visible = true;
						V1_cbo_Cor_QuarterDealCompletedId.Text = dt.Rows[0]["QuarterDealCompletedId"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["YearDealAnnounced"].ToString().Trim()))
					{
						V1_tr_Cor_YearDeal_Announced.Visible = true;
						V1_cbo_Cor_YearDeal_Announced.Text = dt.Rows[0]["YearDealAnnounced"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["YearDealCompletedID"].ToString().Trim()))
					{
						V1_tr_Cor_YearDealCompletedId.Visible = true;
						V1_cbo_Cor_YearDealCompletedId.Text = dt.Rows[0]["YearDealCompletedID"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["DealAnnouncedID"].ToString().Trim()))
					{
						V1_tr_Cor_DealAnnouncedId.Visible = true;
						V1_txt_Cor_DealAnnouncedId.Text = dt.Rows[0]["DealAnnouncedID"].ToString().Trim();
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["WorkTypeCorpTax"].ToString().Trim()))
				{
					trCorpTax.Visible = true;
					V1_cbo_Crt_WorkType_CorpTax.Text = dt.Rows[0]["WorkTypeCorpTax"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["CRDWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["CRDSubWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIdCommercial"].ToString().Trim()))
				{
					trCRD.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["CRDWorkType"].ToString().Trim()))
					{
						V1_tr_CRD_Work_Type.Visible = true;
						V1_txt_CRD_Work_Type.Text = dt.Rows[0]["CRDWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["CRDSubWorkType"].ToString().Trim()))
					{
						V1_tr_CRD_SubWork_Type.Visible = true;
						V1_txt_CRD_SubWork_Type.Text = dt.Rows[0]["CRDSubWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIdCommercial"].ToString().Trim()))
					{
						V1_tr_CRD_ClientTypeIdCommercial.Visible = true;
						V1_cbo_CRD_ClientTypeIdCommercial.Text = dt.Rows[0]["ClientTypeIdCommercial"].ToString().Trim();
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["EPCNatureofWork"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ClientScope"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIDEPC"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["EPCTypeofContract"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["SubjectMatterIDEPC"].ToString().Trim()))
				{
					trEPC.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["EPCNatureofWork"].ToString().Trim()))
					{
						V1_tr_EPC_Nature_Of_Work.Visible = true;
						V1_txt_EPC_Nature_Of_Work.Text = dt.Rows[0]["EPCNatureofWork"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientScope"].ToString().Trim()))
					{
						V1_tr_EPC_ClientScopeId.Visible = true;
						V1_cbo_EPC_ClientScopeId.Text = dt.Rows[0]["ClientScope"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIDEPC"].ToString().Trim()))
					{
						V1_tr_EPC_ClientTypeIdEPC.Visible = true;
						V1_cbo_EPC_ClientTypeIdEPC.Text = dt.Rows[0]["ClientTypeIDEPC"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeOtherEPC"].ToString().Trim()))
					{
						V1_tr_EPC_ClientTypeOther.Visible = true;
						V1_txt_EPC_ClientTypeOther.Text = dt.Rows[0]["ClientTypeOtherEPC"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["EPCTypeofContract"].ToString().Trim()))
					{
						V1_tr_EPC_Type_Of_Contract.Visible = true;
						V1_txt_EPC_Type_Of_Contract.Text = dt.Rows[0]["EPCTypeofContract"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["TypeofContractOtherEPC"].ToString().Trim()))
					{
						V1_tr_EPC_Type_Of_Contract_Other.Visible = true;
						V1_txt_EPC_Type_Of_Contract_Other.Text = dt.Rows[0]["TypeofContractOtherEPC"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["SubjectMatterIDEPC"].ToString().Trim()))
					{
						V1_tr_EPC_SubjectMatterId.Visible = true;
						V1_cbo_EPC_SubjectMatterId.Text = dt.Rows[0]["SubjectMatterIDEPC"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["SubjectMatterOtherEPC"].ToString().Trim()))
					{
						V1_tr_EPC_Subject_Matter_Other.Visible = true;
						V1_txt_EPC_SubjectMatterOther.Text = dt.Rows[0]["SubjectMatterOtherEPC"].ToString().Trim();
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["ContractTypeIDEPCE"].ToString().Trim()))
				{
					trENE.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim()))
					{
						V1_tr_ENE_Transaction_Type.Visible = true;
						V1_txt_ENE_Transaction_Type.Text = dt.Rows[0]["EPCEnergyWorkType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["ContractTypeIDEPCE"].ToString().Trim()))
					{
						V1_tr_ENE_ContractTypeId.Visible = true;
						V1_cbo_ENE_ContractTypeId.Text = dt.Rows[0]["ContractTypeIDEPCE"].ToString().Trim();
					}
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["HCWorkType"].ToString().Trim()))
				{
					V1_tr_HCC_WorkTypeIdHC.Visible = true;
					V1_cbo_HCC_WorkTypeIdHC.Text = dt.Rows[0]["HCWorkType"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["PensionSchemeHC"].ToString().Trim()))
				{
					trHC.Visible = true;
					V1_tr_HCC_PensionSchemeHC.Visible = true;
					V1_cbo_HCC_PensionSchemeHC.Text = dt.Rows[0]["PensionSchemeHC"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIdIPF"].ToString().Trim()))
				{
					trIPF.Visible = true;
					V1_tr_IPF_ClientTypeIdIPF.Visible = true;
					V1_cbo_IPF_ClientTypeIdIPF.Text = dt.Rows[0]["ClientTypeIdIPF"].ToString().Trim();
				}
				if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateClientType"].ToString().Trim()) || !string.IsNullOrEmpty(dt.Rows[0]["RealEstateWorkType"].ToString().Trim()))
				{
					trRE.Visible = true;
					if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateClientType"].ToString().Trim()))
					{
						V1_tr_RES_Client_Type.Visible = true;
						V1_txt_RES_Client_Type.Text = dt.Rows[0]["RealEstateClientType"].ToString().Trim();
					}
					if (!string.IsNullOrEmpty(dt.Rows[0]["RealEstateWorkType"].ToString().Trim()))
					{
						V1_tr_RES_Work_Type.Visible = true;
						V1_txt_RES_Work_Type.Text = dt.Rows[0]["RealEstateWorkType"].ToString().Trim();
					}
				}
			}
		}
	}
}
