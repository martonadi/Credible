using CredentialsDemo.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace CredentialsDemo.SEARCH
{
    public partial class SearchScreen : System.Web.UI.Page
    {

        CallingSP objSP = new CallingSP();
        Logger objLog = new Logger();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["sessionUserInfo"] != null)
                {
                    if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "ADMINISTRATOR")
                    {
                        tr_txt_Tab_Credential_ID.Visible = true;
                        chkPartial.Visible = true;
                    }
                    hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
                }
				else
				{
					Response.Redirect("~/TimeOut.aspx");
				}
                if (Request.QueryString["a"] != null && Request.QueryString["a"] == "2")
                {
                    if (Session["sessionFilterCriteria"] != null)
                    {
                        Session["sessionFilterCriteria"] = null;
                    }

                    if (Session["sessionResultIDs"] != null)
                    {
                        Session["sessionResultIDs"] = null;
                    }

                    if (Session["sessionResultCriteria"] != null)
                    {
                        Session["sessionResultCriteria"] = null;
                    }
                }

                objLog.LogWriter("SearchScreen : Page_Load Starts ", hidName.Value);

                LoadJavaScripts();
                LoadKeyWordSearch();

				if (!cld_Tab_Date_Opened.IsEmpty)
				{
					cld_Tab_Date_Opened.DateInput.BackColor = Color.White;
				}
				if (!cld_Tab_Date_Opened1.IsEmpty)
				{
					cld_Tab_Date_Opened1.DateInput.BackColor = Color.White;
                }

                if (!IsPostBack)
                {
                    radtxtKeywordSearch.Focus();

                    LoadDropDowns();
					LoadPracticeGroup();
                    if (Session["sessionFilterCriteria"] != null)
                    {
                        objLog.LogWriter("SearchScreen : Values Loaded from Result Screen in Search Screen Starts ", hidName.Value);

                        bool blnBasic = false;
                        bool blnAdvance = false;

                        string strFilter = Session["sessionFilterCriteria"].ToString();
                        string[] strFilterValues = strFilter.Split('|');

                        for (int i = 0; i < strFilterValues.Length - 1; i++)
                        {
                            switch (strFilterValues[i].Split('=')[0])
                            {
							case "PartialSave":
								chkPartial.Checked = true;
								break;
                                case "radtxtKeywordSearch":
                                    string[] strVal = strFilterValues[i].Split('=')[1].Split(',');
                                    for (int j = 0; j < strVal.Length; j++)
                                    {
										radtxtKeywordSearch.Entries.Add(new AutoCompleteBoxEntry(strVal[j].ToString().Replace("^", ",")));
                                    }
                                    break;
                                case "txt_Tab_Client":
                                    txt_Tab_Client.Text = strFilterValues[i].Split('=')[1];
                                    blnBasic = true;
                                    break;
                                case "rad_Tab_ClientIndustrySector":
                                    // CheckTheItems(rad_Tab_ClientIndustrySector, strFilterValues[i].Split('=')[1]);
                                    //rad_Tab_ClientIndustrySector.BackColor = System.Drawing.Color.Beige;
                                    ShowMethod(hidCS, imgexpandCS, plnCS, "usp_ClientIndustrySectorList", "Client_Industry_Sector", "ClientIndustrySectorId", RadListBox1, RadListBox2, radTTCS, lblCS);
                                    imgexpandCS.Visible = false;
                                    imgcollapseCS.Visible = true;
                                    PopulateListItems(strFilterValues[i].Split('=')[1], RadListBox1, RadListBox2);
                                    blnBasic = true;
                                    break;
                                case "rad_Tab_Client_Industry_Type":
                                    //CheckTheItems(rad_Tab_Client_Industry_Type, strFilterValues[i].Split('=')[1]);
                                    //rad_Tab_Client_Industry_Type.BackColor = System.Drawing.Color.Beige;
                                    ShowMethod(hidCSS, imgexpandCSS, plnCSS, "usp_ClientIndustryTypeList", "Client_Industry_Type", "ClientIndustryTypeId", radlstSourceCSS, radlstDestCSS, radTTCSS, lblCSS);
                                    imgexpandCSS.Visible = false;
                                    imgcollapseCSS.Visible = true;
                                    PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceCSS, radlstDestCSS);
                                    blnBasic = true;
                                    break;
                                case "rad_Tab_TransactionIndustrySector":
                                    //CheckTheItems(rad_Tab_TransactionIndustrySector, strFilterValues[i].Split('=')[1]);
                                    //rad_Tab_TransactionIndustrySector.BackColor = System.Drawing.Color.Beige;
                                    ShowMethod(hidMS, imgexpandMS, plnMS, "usp_TransactionIndustrySectorList", "Transaction_Industry_Sector", "TransactionIndustrySectorId", radlstSourceMS, radlstDestMS, radTTMS, lblMS);
                                    imgexpandMS.Visible = false;
                                    imgcollapseMS.Visible = true;
                                    PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceMS, radlstDestMS);
                                    blnBasic = true;
                                    break;
                                case "rad_Tab_Transaction_Industry_Type":
                                    //CheckTheItems(rad_Tab_Transaction_Industry_Type, strFilterValues[i].Split('=')[1]);
                                    //rad_Tab_Transaction_Industry_Type.BackColor = System.Drawing.Color.Beige;
                                    ShowMethod(hidMSS, imgexpandMSS, plnMSS, "usp_TransactionIndustryTypeList", "Transaction_Industry_Type", "TransactionIndustryTypeId", radlstSourceMSS, radlstDestMSS, radTTMSS, lblMSS);
                                    imgexpandMSS.Visible = false;
                                    imgcollapseMSS.Visible = true;
                                    PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceMSS, radlstDestMSS);
                                    blnBasic = true;
                                    break;

                                /*rad_Tab_Work_Type*/

                                case "BAIFWorkTypeId":
                                    CheckTheOneLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;
                                case "CorpTaxWorkTypeId":
                                    CheckTheOneLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;
                                case "EPCCWorkTypeId":
                                    CheckTheOneLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;
                                case "EPCEWorkTypeId":
                                    CheckTheOneLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;

                                case "CORPWorkTypeId":
                                    CheckTheOneLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;
                                case "REWorkTypeId":
                                    CheckTheOneLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;
                                case "HCWorkTypeId":
                                    CheckTheOneLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;

                                case "CORPSubWorkTypeId":
                                    CheckTheTwoLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;
                                case "RESubWorkTypeId":
                                    CheckTheTwoLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;
                                case "HCSubWorkTypeId":
                                    CheckTheTwoLevelItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;

                                case "CRDWorkTypeId":
                                    CheckCRDWTItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;
                                case "CRDSubWorkTypeId":
                                    CheckCRDSWTItems(RadTreeView1, strFilterValues[i].Split('=')[1]);
                                    blnBasic = true;
                                    break;

                                /*Date completed*/
                                case "DateCompletedFrom":
									IFormatProvider culture = new CultureInfo("en-GB", true);
									cld_Tab_Date_Opened.SelectedDate = new DateTime?(DateTime.Parse(strFilterValues[i].Split('=')[1].ToString(), culture));
									cld_Tab_Date_Opened.DateInput.DisplayText = strFilterValues[i].Split('=')[1];
									blnBasic = true;
									break;
								case "DateCompletedTo":
									IFormatProvider culture2 = new CultureInfo("en-GB", true);
									cld_Tab_Date_Opened1.SelectedDate = new DateTime?(DateTime.Parse(strFilterValues[i].Split('=')[1].ToString(), culture2));
									cld_Tab_Date_Opened1.DateInput.DisplayText = strFilterValues[i].Split('=')[1];
									blnBasic = true;
									break;
								case "chk_Tab_ActualDate_Ongoing":
									chk_Tab_ActualDate_Ongoing.Checked = true;
									blnBasic = true;
									break;
								case "chk_Tab_ActualDate_NotKnown":
									chk_Tab_ActualDate_NotKnown.Checked = true;
									blnBasic = true;
									break;
                                case "rad_Tab_Country_Matter_Close":
                                    //CheckTheItems(rad_Tab_Country_Matter_Close, strFilterValues[i].Split('=')[1]);
                                    //rad_Tab_Country_Matter_Close.BackColor = System.Drawing.Color.Beige;
                                    ShowMethod(hidML, imgexpandML, plnML, "usp_CountryList", "Country", "CountryId", radlstSourceML, radlstDestML, radTTML, lblML);
                                    imgexpandML.Visible = false;
                                    imgcollapseML.Visible = true;
                                    PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceML, radlstDestML);
                                    blnBasic = true;
                                    break;
                                case "rad_Tab_Team":
                                    //CheckTheItems(rad_Tab_Team, strFilterValues[i].Split('=')[1]);
                                    //rad_Tab_Team.BackColor = System.Drawing.Color.Beige;
                                    ShowMethod(hidTeams, imgexpandTeams, plnTeams, "usp_TeamList", "Team", "TeamId", radlstSourceTeams, radlstDestTeams, radTTTeams, lblTeams);
                                    imgexpandTeams.Visible = false;
                                    imgcollapseTeams.Visible = true;
                                    PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceTeams, radlstDestTeams);
                                    blnBasic = true;
                                    break;
                                case "rad_Tab_Lead_Partner":
                                    //CheckTheItems(rad_Tab_Lead_Partner, strFilterValues[i].Split('=')[1]);
                                    //rad_Tab_Lead_Partner.BackColor = System.Drawing.Color.Beige;
                                    ShowMethod(hidLP, imgexpandLP, plnLP, "usp_LeadPartnerList_New", "Lead_Partner", "LeadPartnerId", radlstSourceLP, radlstDestLP, radTTLP, lblLP);
                                    imgexpandLP.Visible = false;
                                    imgcollapseLP.Visible = true;
                                    PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceLP, radlstDestLP);
                                    blnBasic = true;
                                    break;
								case "Select Practicegroup(s)/BAIF/Client Type":
									SearchScreen.CheckThePraticeGroupItems1(RadTreeView2, strFilterValues[i]);
									blnBasic = true;
									break;
								case "CRDClientTypeId":
									SearchScreen.CheckCRDItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "EPCProjectsClientTypeId":
									SearchScreen.CheckEPCProjectsItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "EPCEContractTypeId":
									SearchScreen.CheckEPCENERGYItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "HCPensionSchemeId":
									SearchScreen.CheckHumanCapitalItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Client TypeID":
									SearchScreen.CheckRealEstateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Select Practicegroup(s)/CORPORATE/Country of Buyer":
									SearchScreen.CheckCorporateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Select Practicegroup(s)/CORPORATE/Country of Seller":
									SearchScreen.CheckCorporateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Select Practicegroup(s)/CORPORATE/Country of Target":
									SearchScreen.CheckCorporateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Value over US$5mID":
									SearchScreen.CheckCorporateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Value over £500,000ID":
									SearchScreen.CheckCorporateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Value over Euro 5mID":
									SearchScreen.CheckCorporateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Is the deal relevant for M&A StudyID":
									SearchScreen.CheckCorporateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "Does the deal involve PE Clients on either sideID":
									SearchScreen.CheckCorporateItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "CorporatePGText":
									SearchScreen.CheckCRDItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "BAIFPGText":
									SearchScreen.CheckPGItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "EPCConstPGText":
									SearchScreen.CheckPGItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "EPCEnergyPGText":
									SearchScreen.CheckPGItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "EPCProjectsText":
									SearchScreen.CheckPGItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "REPGText":
									SearchScreen.CheckPGItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "HCPGText":
									SearchScreen.CheckPGItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
								case "CorporateTaxPGText":
									SearchScreen.CheckPGItems(RadTreeView2, strFilterValues[i].Split('=')[1]);
									blnBasic = true;
									break;
                                case "rad_Tab_Priority":
                                    CheckTheItems(rad_Tab_Priority, strFilterValues[i].Split('=')[1]);
                                    rad_Tab_Priority.BackColor = System.Drawing.Color.Beige;
                                    blnBasic = true;
                                    break;
                                /* Advanced Field */
								case "txt_Tab_ProjectName_Core":
									txt_Tab_ProjectName_Core.Text = strFilterValues[i].Split('=')[1];
									blnAdvance = true;
									blnBasic = true;
									break;
								case "txt_tab_ClientDescription":
									txt_tab_ClientDescription.Text = strFilterValues[i].Split('=')[1];
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Matter_Executive":
									ShowMethod(hidME, imgexpandME, plnME, "usp_OtherMatterExecutiveList", "Other_Matter_Executive", "OtherMatterExecutiveId", radlstSourceME, radlstDestME, radTTME, lblME);
									imgexpandME.Visible = false;
									imgcollapseME.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceME, radlstDestME);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "txt_tab_Value_Deal":
									txt_tab_Value_Deal.Text = strFilterValues[i].Split('=')[1];
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Currency_Of_Deal":
									ShowMethod(hidCD, imgexpandCD, plnCD, "usp_CurrencyOfDealList", "Currency_Of_Deal", "CurrencyOfDealId", radlstSourceCD, radlstDestCD, radTTCD, lblCD);
									imgexpandCD.Visible = false;
									imgcollapseCD.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceCD, radlstDestCD);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Contentious_IRG":
									ShowMethod(hidContentious, imgexpandContentious, plnContentious, "usp_GetContentious", "TheDescription", "TheValue", radlstSourceContentious, radlstDestContentious, radTTContentious, lblContentious);
									imgexpandContentious.Visible = false;
									imgcollapseContentious.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceContentious, radlstDestContentious);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rdo_Tab_Dispute_Resolution":
									ShowMethod(hidDR, imgexpandDR, plnDR, "usp_DisputeResolutionList", "Dispute_Resolution", "DisputeResolutionId", radlstSourceDR, radlstDestDR, radTTDR, lblDR);
									imgexpandDR.Visible = false;
									imgcollapseDR.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceDR, radlstDestDR);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_CountryofArbitration":
									ShowMethod(hidCOA, imgexpandCOA, plnCOA, "usp_CountryList", "Country", "CountryId", radlstSourceCOA, radlstDestCOA, radTTCOA, lblCOA);
									imgexpandCOA.Visible = false;
									imgcollapseCOA.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceCOA, radlstDestCOA);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_ArbitrationCity":
									ShowMethod(hidSOA, imgexpandSOA, plnCOA, "usp_SeatOfArbitrationList", "Seat_Of_Arbitration", "SeatOfArbitrationId", radlstSourceSOA, radlstDestSOA, radTTSOA, lblSOA);
									imgexpandSOA.Visible = false;
									imgcollapseSOA.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceSOA, radlstDestSOA);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Arbitral_Rules":
									ShowMethod(hidAR, imgexpandAR, plnAR, "usp_ArbitralRulesList", "Arbitral_Rules", "ArbitralRulesId", radlstSourceAR, radlstDestAR, radTTAR, lblAR);
									imgexpandAR.Visible = false;
									imgcollapseAR.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceAR, radlstDestAR);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_InvestmentTreaty":
									CheckTheItems(rad_Tab_InvestmentTreaty, strFilterValues[i].Split('=')[1]);
									rad_Tab_InvestmentTreaty.BackColor = Color.Beige;
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Investigation_Type":
									ShowMethod(hidIVT, imgexpandIVT, plnIVT, "usp_InvestigationTypeList", "Investigation_Type", "InvestigationTypeId", radlstSourceIVT, radlstDestIVT, radTTIVT, lblIVT);
									imgexpandIVT.Visible = false;
									imgcollapseIVT.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceIVT, radlstDestIVT);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Language_Of_Dispute":
									ShowMethod(hidLOD, imgexpandLOD, plnLOD, "usp_LanguageOfDisputeList", "Language_Of_Dispute", "LanguageOfDisputeId", radlstSourceLOD, radlstDestLOD, radTTLOD, lblLOD);
									imgexpandLOD.Visible = false;
									imgcollapseLOD.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceLOD, radlstDestLOD);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Country_Jurisdiction":
									ShowMethod(hidJOD, imgexpandJOD, plnJOD, "usp_CountryList", "Country", "CountryId", radlstSourceJOD, radlstDestJOD, radTTJOD, lblJOD);
									imgexpandJOD.Visible = false;
									imgcollapseJOD.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceJOD, radlstDestJOD);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Referred_From_Other_CMS_Office":
									ShowMethod(hidCFI, imgexpandCFI, plnCFI, "usp_ReferredFromOtherCMSOfficeList", "Referred_From_Other_CMS_Office", "ReferredFromOtherCMSOfficeId", radlstSourceCFI, radlstDestCFI, radTTCFI, lblCFI);
									imgexpandCFI.Visible = false;
									imgcollapseCFI.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceCFI, radlstDestCFI);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Lead_CMS_Firms":
									ShowMethod(hidLCF, imgexpandLCF, plnLCF, "usp_ReferredFromOtherCMSOfficeList", "Referred_From_Other_CMS_Office", "ReferredFromOtherCMSOfficeId", radlstSourceLCF, radlstDestLCF, radTTLCF, lblLCF);
									imgexpandLCF.Visible = false;
									imgcollapseLCF.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceLCF, radlstDestLCF);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_tab_Countries_of_other_CMS_firms":
									ShowMethod(hidCCF, imgexpandCCF, plnCCF, "usp_CountryList", "Country", "CountryId", radlstSourceCCF, radlstDestCCF, radTTCCF, lblCCF);
									imgexpandCCF.Visible = false;
									imgcollapseCCF.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceCCF, radlstDestCCF);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Other_Uses":
									ShowMethod(hidOU, imgexpandOU, plnOU, "usp_OtherUsesList", "Other_Uses", "OtherUsesId", radlstSourceOU, radlstDestOU, radTTOU, lblOU);
									imgexpandOU.Visible = false;
									imgcollapseOU.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceOU, radlstDestOU);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "cbo_Tab_Credential_Status":
									CheckTheItems(cbo_Tab_Credential_Status, strFilterValues[i].Split('=')[1]);
									cbo_Tab_Credential_Status.BackColor = Color.Beige;
									blnBasic = true;
									blnAdvance = true;
									break;
								case "cbo_Tab_Credential_Version":
									CheckTheItems(cbo_Tab_Credential_Version, strFilterValues[i].Split('=')[1]);
									cbo_Tab_Credential_Version.BackColor = Color.Beige;
									blnBasic = true;
									blnAdvance = true;
									break;
								case "cbo_Tab_Credential_Type":
									CheckTheItems(cbo_Tab_Credential_Type, strFilterValues[i].Split('=')[1]);
									cbo_Tab_Credential_Type.BackColor = Color.Beige;
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Country_Law":
									ShowMethod(hidAL, imgexpandAL, plnAL, "usp_CountryLawList", "Country_Law", "CountryLawId", radlstSourceAL, radlstDestAL, radTTAL, lblAL);
									imgexpandAL.Visible = false;
									imgcollapseAL.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceAL, radlstDestAL);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Country_Matter_Open":
									ShowMethod(hidCMO, imgexpandCMO, plnCMO, "usp_CountryList", "Country", "CountryId", radlstSourceCMO, radlstDestCMO, radTTCMO, lblCMO);
									imgexpandCMO.Visible = false;
									imgcollapseCMO.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceCMO, radlstDestCMO);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Country_PredominantCountry":
									ShowMethod(hidPCC, imgexpandPCC, plnPCC, "usp_CountryList", "Country", "CountryId", radlstSourcePCC, radlstDestPCC, radTTPCC, lblPCC);
									imgexpandPCC.Visible = false;
									imgcollapsePCC.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourcePCC, radlstDestPCC);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "cbo_Tab_ProBono":
									CheckTheItems(cbo_Tab_ProBono, strFilterValues[i].Split('=')[1]);
									cbo_Tab_ProBono.BackColor = Color.Beige;
									blnAdvance = true;
									blnBasic = true;
									break;
								case "rad_Tab_Know_How":
									ShowMethod(hidKH, imgexpandKH, plnKH, "usp_KnowHowList", "Know_How", "KnowHowId", radlstSourceKH, radlstDestKH, radTTKH, lblKH);
									imgexpandKH.Visible = false;
									imgcollapseKH.Visible = true;
									PopulateListItems(strFilterValues[i].Split('=')[1], radlstSourceKH, radlstDestKH);
									blnAdvance = true;
									blnBasic = true;
									break;
								case "txt_Tab_Bible_Reference":
									txt_Tab_Bible_Reference.Text = strFilterValues[i].Split('=')[1];
									blnAdvance = true;
									blnBasic = true;
									break;
								case "txt_Tab_Matter_No":
									txt_Tab_Matter_No.Text = strFilterValues[i].Split('=')[1];
									blnAdvance = true;
									blnBasic = true;
								break;
								case "txt_Tab_Credential_ID":
									txt_Tab_Credential_ID.Text = strFilterValues[i].Split('=')[1];
									blnAdvance = true;
									blnBasic = true;
                                    break;

                                default:
                                    break;
                            }

                            if (blnAdvance == true)
                            {
                                divAdvancedSearch.Visible = true;
                                divBasicSearch.Visible = true;
                                lblBasicSearch.Visible = true;
                                lblAdvancedSearch.Visible = true;
                                hr_top.Visible = true;
                                tr_bottom.Visible = true;
                                tr_hrline.Visible = true;
                            }
                            else
                            {
                                divAdvancedSearch.Visible = false;
                                divBasicSearch.Visible = false;
                                lblBasicSearch.Visible = false;
                                lblAdvancedSearch.Visible = false;
                                hr_top.Visible = false;
                                tr_bottom.Visible = false;
                                tr_hrline.Visible = false;
                            }
                            if (blnBasic == true)
                            {
                                divBasicSearch.Visible = true;
                                lblBasicSearch.Visible = true;
                                hr_top.Visible = true;
                                tr_bottom.Visible = true;
                                tr_hrline.Visible = false;
                            }
                            else
                            {
                                divBasicSearch.Visible = false;
                                lblBasicSearch.Visible = false;
                                hr_top.Visible = false;
                                tr_bottom.Visible = false;
                                tr_hrline.Visible = false;
                            }
                        }
                        Session["sessionFilterCriteria"] = null;
						radtxtKeywordSearch.Focus();
                        objLog.LogWriter("SearchScreen : Values Loaded from Result Screen in Search Screen Ends ", hidName.Value);
                    }
                }

                AllListboxChangeColor();
                objLog.LogWriter("SearchScreen : Page Load Ends ", hidName.Value);
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("SearchScreen : Error in Page Load " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void AllListboxChangeColor()
        {
            ChangeColor(RadListBox2);
            ChangeColor(radlstDestAL);
            ChangeColor(radlstDestAR);
            ChangeColor(radlstDestCCF);
            ChangeColor(radlstDestCD);
            ChangeColor(radlstDestCFI);
            ChangeColor(radlstDestCMO);
            ChangeColor(radlstDestCOA);
            ChangeColor(radlstDestContentious);
            ChangeColor(radlstDestCSS);
            ChangeColor(radlstDestDR);
            ChangeColor(radlstDestIVT);
            ChangeColor(radlstDestJOD);
            ChangeColor(radlstDestKH);
            ChangeColor(radlstDestLCF);
            ChangeColor(radlstDestLOD);
            ChangeColor(radlstDestLP);
            ChangeColor(radlstDestME);
            ChangeColor(radlstDestML);
            ChangeColor(radlstDestMS);
            ChangeColor(radlstDestMSS);
            ChangeColor(radlstDestOU);
            ChangeColor(radlstDestPCC);
            ChangeColor(radlstDestSOA);
            ChangeColor(radlstDestTeams);
        }

        private void LoadRootNodes(RadTreeView treeView, TreeNodeExpandMode expandMode)
        {
            objLog.LogWriter("SearchScreen : LoadRootNodes Starts ", hidName.Value);

            DataTable data = objSP.GetDataTable("select * from tblbusinessgroup where exclude=0 order by business_group");

            RadTreeNode root12 = new RadTreeNode("Select Practice group");
            root12.Text = "Select Practice group";
			root12.Value = "Select Practice group";
            root12.Checkable = false;
            treeView.Nodes.Add(root12);

            foreach (DataRow row in data.Rows)
            {
                RadTreeNode node = new RadTreeNode();
                node.Text = row["Business_Group"].ToString();

                if (row["Business_Group"].ToString() == "Human capital (HC)")
                {
                    node.Text = "Human Capital";
                }
				
				if (row["Business_Group"].ToString() == "Real estate")
				{
					node.Text = "Real Estate";
				}
				
                if (row["Business_Group"].ToString() == "EPC - Energy")
                {
                    node.Text = "EPC Energy";
                }
                node.Value = row["BusinessGroupId"].ToString();
                if (row["Business_Group"].ToString() != "Corporate Tax")
                {
                    node.ExpandMode = expandMode;
                    node.Checkable = true;
                    root12.Nodes.Add(node);
                }
            }

            objLog.LogWriter("SearchScreen : LoadRootNodes Ends", hidName.Value);
		}
		
		private DataTable LoadPracticeGroupCorporate(string str)
		{
			DataTable dt = null;
			switch (str)
			{
				case "Acting For":
					dt = GetData("acting_for", "actingforid", "tblactingfor", "3");
					break;
				case "Country of Buyer":
					dt = GetData("Country", "CountryId", "tblCountry", null);
					break;
				case "Country of Seller":
					dt = GetData("Country", "CountryId", "tblCountry", null);
					break;
				case "Country of Target":
					dt = GetData("Country", "CountryId", "tblCountry", null);
					break;
				case "Value over US$5m":
					dt = GetData("Yes", "YesNoId", "tblYesNo", "3");
					break;
				case "Value over £500,000":
					dt = GetData("Yes", "YesNoId", "tblYesNo", "3");
					break;
				case "Value over Euro 5m":
					dt = GetData("Yes", "YesNoId", "tblYesNo", "3");
					break;
				case "Deal range in deal currency":
					dt = GetData("Value_Range", "ValueRangeId", "tblValueRange", "3");
					break;
				case "Is the deal relevant for M & A Study":
					dt = GetData("Yes", "YesNoId", "tblYesNo", "3");
					break;
				case "Does the deal involve PE Clients on either side":
					dt = GetData("Yes", "YesNoId", "tblYesNo", "3");
					break;
				case "Quarter Deal Announced":
					dt = GetData("Quarter_Deal_Completed", "QuarterDealCompletedId", "tblquarterdealcompleted", "3");
					break;
				case "Quarter Deal Completed":
					dt = GetData("Quarter_Deal_Completed", "QuarterDealCompletedId", "tblquarterdealcompleted", "3");
					break;
				case "Year Deal Announced / Signed":
					dt = GetData("Year_Deal_Completed", "YearDealCompletedId", "tblyeardealcompleted", "3");
					break;
				case "Year Deal Completed":
					dt = GetData("Year_Deal_Completed", "YearDealCompletedId", "tblyeardealcompleted", "3");
					break;
				case "Client Type":
					dt = GetData("client_type", "clienttypeid", "tblClientType", "5");
					break;
				case "Client Scope":
					dt = GetData("client_scope", "clientscopeid", "tblClientScope", "5");
					break;
				case "Subject Matter":
					dt = GetData("Subject_Matter", "SubjectMatterId", "tblSubjectMatter", "5");
					break;
				case "Type Of Contract":
					dt = GetData("Type_Of_Contract", "TypeContractId", "tblTypeContract", "5");
					break;
					default:
						break;
			}
			return dt;
		}
		
		private DataTable GetData(string strname, string strid, string strTable, string strBGId = null)
		{
			DataTable dt;
			if (strBGId != null)
			{
				dt = objSP.GetDataTable("select " + strid + "," + strname + " from  " + strTable + " where businessgroupid='" + strBGId + "' and exclude=0 order by " + strname + " asc");
			}
			else
			{
				dt = objSP.GetDataTable("select " + strid + "," + strname + " from  " + strTable + " where exclude=0 order by " + strname + " asc");
			}
			return dt;
		}
		
		private void LoadPracticeGroup()
		{
			objLog.LogWriter("SearchScreen : LoadWorkTypeView Starts ", hidName.Value);
			SqlConnection con = new SqlConnection(strcon);
			con.Open();
			
			SqlCommand cmd = new SqlCommand();
			DataSet ds = new DataSet();
			
			cmd.CommandText = "select BusinessGroupId,(case When Business_Group='EPC - Energy' Then 'EPC Energy' When Business_Group<>'EPC - Energy' Then Business_Group END)AS [Business_Group],Exclude from tblbusinessgroup where exclude=0 order by business_group asc";
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
					string text = ds.Tables[0].Rows[i][1].ToString().ToUpper();
					switch (text)
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
						case "EPC PROJECTS":
							str = "EPC PROJECTS";
							break;
						case "REAL ESTATE":
							str = "REAL ESTATE";
							break;
						case "EPC ENERGY":
							str = "EPC ENERGY";
							break;
						case "HUMAN CAPITAL (HC)":
							str = "HUMAN CAPITAL";
							break;
						case "CORPORATE TAX":
							str = "CORPORATE TAX";
							break;
						default:
							break;
					}

					if (!string.IsNullOrEmpty(str.Trim()))
					{
						sort.Add(i, str);
					}
				}
			}
			
			SortedList sortnew = new SortedList();
			for (int i = 0; i < sort.Count; i++)
			{
				if (!sortnew.Contains(i))
				{
					sortnew.Add(i, sort[i].ToString());
				}
			}
			
			RadTreeNode root12 = new RadTreeNode("Select Practicegroup(s)");
			root12.Text = "Select Practicegroup(s)";
			root12.Value = "Select Practicegroup(s)";
			root12.Checkable = false;
			
			RadTreeView2.Nodes.Add(root12);
			
			foreach (DictionaryEntry ent in sortnew)
			{
				string str = ent.Value.ToString().ToUpper();
				RadTreeNode root13 = new RadTreeNode(ent.Value.ToString().ToUpper());
				
				if (!string.IsNullOrEmpty(ent.Value.ToString().ToUpper()))
				{
					root13.Text = ent.Value.ToString().ToUpper();
					root13.Value = ent.Value.ToString().ToUpper();
					root13.Checkable = true;
					root12.Nodes.Add(root13);
				}
				
				if (str == "BAIF")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=1");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
							root13.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = GetDataTable("select client_type,clienttypeid from tblclienttype where BusinessGroupId=1 order by Client_Type asc");
							if (dtSubWorkType.Rows.Count > 0)
							{
								for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
								{
									RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim());
									chd2.Text = dtSubWorkType.Rows[j][0].ToString().Trim();
									chd2.Value = dtSubWorkType.Rows[j][1].ToString();
									chd.Nodes.Add(chd2);
								}
							}
						}
					}
				}
				
				if (str == "CORPORATE")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=3");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
							root13.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = LoadPracticeGroupCorporate(dt.Rows[i][1].ToString());
							if (dtSubWorkType != null && dtSubWorkType.Rows.Count > 0)
							{
								for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
								{
									RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][1].ToString().Trim());
									chd2.Text = dtSubWorkType.Rows[j][1].ToString().Trim();
									chd2.Value = dtSubWorkType.Rows[j][0].ToString();
									chd.Nodes.Add(chd2);
								}
							}
						}
					}
				}
				
				if (str == "CORPORATE TAX")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=11");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString().Trim(), dt.Rows[i][3].ToString());
							chd.Checkable = true;
							root13.Nodes.Add(chd);
						}
					}
				}
				
				if (str == "CRD")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=4");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
							root13.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = GetDataTable("select client_type,clienttypeid from tblclienttype where BusinessGroupId=4 order by Client_Type asc");
							if (dtSubWorkType.Rows.Count > 0)
							{
								for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
								{
									RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim());
									chd2.Text = dtSubWorkType.Rows[j][0].ToString().Trim();
									chd2.Value = dtSubWorkType.Rows[j][1].ToString();
									chd.Nodes.Add(chd2);
								}
							}
						}
					}
				}
				
				if (str == "EPC ENERGY")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=9");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
							root13.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = GetDataTable("select contract_type,contracttypeid from tblContractType where BusinessGroupId=9 order by contract_type asc");
							if (dtSubWorkType.Rows.Count > 0)
							{
								for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
								{
									RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim());
									chd2.Text = dtSubWorkType.Rows[j][0].ToString().Trim();
									chd2.Value = dtSubWorkType.Rows[j][1].ToString();
									chd.Nodes.Add(chd2);
								}
							}
						}
					}
				}
				
				if (str == "EPC CONSTRUCTION")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=5");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
							root13.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = LoadPracticeGroupCorporate(dt.Rows[i][1].ToString());
							if (dtSubWorkType != null && dtSubWorkType.Rows.Count > 0)
							{
								for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
								{
									RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][1].ToString().Trim());
									chd2.Text = dtSubWorkType.Rows[j][1].ToString().Trim();
									chd2.Value = dtSubWorkType.Rows[j][0].ToString();
									chd.Nodes.Add(chd2);
								}
							}
						}
					}
				}
				
				if (str == "EPC PROJECTS")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=8");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
							root13.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = GetDataTable("select client_type,clienttypeid from tblclienttype where BusinessGroupId=8 order by Client_Type asc");
							if (dtSubWorkType.Rows.Count > 0)
							{
								for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
								{
									RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim());
									chd2.Text = dtSubWorkType.Rows[j][0].ToString().Trim();
									chd2.Value = dtSubWorkType.Rows[j][1].ToString();
									chd.Nodes.Add(chd2);
								}
							}
						}
					}
				}
				
				if (str == "REAL ESTATE")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=7");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
							root13.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = GetDataTable("select client_type,clienttypeid from tblclienttype where BusinessGroupId=7 order by Client_Type asc");
							if (dtSubWorkType.Rows.Count > 0)
							{
								for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
								{
									RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim());
									chd2.Text = dtSubWorkType.Rows[j][0].ToString().Trim();
									chd2.Value = dtSubWorkType.Rows[j][1].ToString();
									chd.Nodes.Add(chd2);
								}
							}
						}
					}
				}
				
				if (str == "HUMAN CAPITAL")
				{
					DataTable dt = GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid=10");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][1].ToString(), dt.Rows[i][3].ToString());
							root13.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = GetDataTable("select pensionscheme,pensionschemeid from tblpensionscheme");
							if (dtSubWorkType.Rows.Count > 0)
							{
								for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
								{
									RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim());
									chd2.Text = dtSubWorkType.Rows[j][0].ToString().Trim();
									chd2.Value = dtSubWorkType.Rows[j][1].ToString();
									chd.Nodes.Add(chd2);
								}
							}
						}
					}
				}
			}
			
			objLog.LogWriter("SearchScreen : LoadWorkTypeView Ends ", hidName.Value);
		}
		
        protected void RadTreeView2_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            try
            {
                objLog.LogWriter("SearchScreen : RadTreeView2_NodeExpand Starts", hidName.Value);
                PopulateNodeOnDemand(e, TreeNodeExpandMode.ServerSideCallBack);
                objLog.LogWriter("SearchScreen : RadTreeView2_NodeExpand Ends", hidName.Value);
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("SearchScreen : Error in RadTreeView2_NodeExpand " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void PopulateNodeOnDemand(RadTreeNodeEventArgs e, TreeNodeExpandMode expandMode)
        {
            objLog.LogWriter("SearchScreen : PopulateNodeOnDemand Starts ", hidName.Value);

            DataTable data;

            switch (e.Node.FullPath)
            {
                case "Select Practice group/BAIF":
                    data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid='" + e.Node.Value + "'");

                    foreach (DataRow row in data.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["fielddescription"].ToString();
                        node.Value = row["fieldvalue"].ToString();
                        node.Checkable = true;
                        node.ExpandMode = expandMode;
                        e.Node.Nodes.Add(node);
                    }
                    //e.Node.Checkable = true;
                    e.Node.Expanded = true;
                    break;
                case "Select Practice group/CRD":
                    data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid='" + e.Node.Value + "'");

                    foreach (DataRow row in data.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["fielddescription"].ToString();
                        node.Value = row["fieldvalue"].ToString();
                        node.Checkable = true;
                        node.ExpandMode = expandMode;
                        e.Node.Nodes.Add(node);
                    }

                    e.Node.Expanded = true;
                    break;
                case "Select Practice group/EPC Construction":
                    data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid='" + e.Node.Value + "'");

                    foreach (DataRow row in data.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["fielddescription"].ToString();
                        node.Value = row["fieldvalue"].ToString();
                        node.Checkable = true;
                        node.ExpandMode = expandMode;
                        e.Node.Nodes.Add(node);
                    }

                    e.Node.Expanded = true;
                    break;
                case "Select Practice group/EPC Energy":
                    data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid='" + e.Node.Value + "'");

                    foreach (DataRow row in data.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["fielddescription"].ToString();

                        node.Value = row["fieldvalue"].ToString();
                        node.Checkable = true;
                        node.ExpandMode = expandMode;
                        e.Node.Nodes.Add(node);
                    }

                    e.Node.Expanded = true;
                    break;
                case "Select Practice group/Real estate":
                    data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid='" + e.Node.Value + "'");

                    foreach (DataRow row in data.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["fielddescription"].ToString();
                        node.Value = row["fieldvalue"].ToString();
                        node.Checkable = true;
                        node.ExpandMode = expandMode;
                        e.Node.Nodes.Add(node);
                    }

                    e.Node.Expanded = true;
                    break;
			case "Select Practice group/EPC Projects":
                    data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid='" + e.Node.Value + "'");

                    foreach (DataRow row in data.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["fielddescription"].ToString();
                        node.Value = row["fieldvalue"].ToString();
                        node.Checkable = true;
                        node.ExpandMode = expandMode;
                        e.Node.Nodes.Add(node);
                    }

                    e.Node.Expanded = true;
                    break;
                case "Select Practice group/Human Capital":
                    data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid='" + e.Node.Value + "'");

                    foreach (DataRow row in data.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["fielddescription"].ToString();

                        node.Value = row["fieldvalue"].ToString();
                        node.Checkable = true;
                        node.ExpandMode = expandMode;
                        e.Node.Nodes.Add(node);
                    }

                    e.Node.Expanded = true;
                    break;
                case "Select Practice group/Corporate":
                    data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid,fieldvalue from tblfield where practicegroupid='" + e.Node.Value + "'");

                    foreach (DataRow row in data.Rows)
                    {
                        RadTreeNode node = new RadTreeNode();
                        node.Text = row["fielddescription"].ToString();
                        node.Value = row["fieldvalue"].ToString();

                        node.Checkable = true;
                        node.ExpandMode = expandMode;
                        e.Node.Nodes.Add(node);
                    }

                    e.Node.Expanded = true;
                    break;
                case "Select Practice group/BAIF/Client Type":
                    NewMethod(e, expandMode, "client_type", "clienttypeid", "tblClientType", "1");
                    break;
                case "Select Practice group/CRD/Client Type":
                    NewMethod(e, expandMode, "client_type", "clienttypeid", "tblClientType", "4");
                    break;
                case "Select Practice group/EPC Construction/Client Type":
                    NewMethod(e, expandMode, "client_type", "clienttypeid", "tblClientType", "5");
                    break;
                case "Select Practice group/EPC Construction/Client Scope":
                    NewMethod(e, expandMode, "client_scope", "clientscopeid", "tblClientScope", "5");
                    break;
                case "Select Practice group/EPC Construction/Subject Matter":
                    NewMethod(e, expandMode, "Subject_Matter", "SubjectMatterId", "tblSubjectMatter", "5");
                    break;
                case "Select Practice group/EPC Construction/Type Of Contract":
                    NewMethod(e, expandMode, "Type_Of_Contract", "TypeContractId", "tblTypeContract", "5");
                    break;
                case "Select Practice group/EPC Energy/Contract Type":
                    NewMethod(e, expandMode, "Contract_Type", "ContractTypeId", "tblContractType", "9");
                    break;
                case "Select Practice group/EPC Projects/Client Type":
                    NewMethod(e, expandMode, "Client_Type", "ClientTypeId", "tblClientType", "8");
                    break;
                case "Select Practice group/Real Estate/Client Type":
                    NewMethod(e, expandMode, "Client_Type", "ClientTypeId", "tblClientType", "7");
                    break;

                case "Select Practice group/Human Capital/Pension Scheme":
                    NewMethod(e, expandMode, "PensionScheme", "PensionSchemeId", "tblPensionScheme");
                    break;
                case "Select Practice group/Corporate/Country of Buyer":
                    NewMethod(e, expandMode, "Country", "CountryId", "tblCountry");
                    break;
                case "Select Practice group/Corporate/Country of Seller":
                    NewMethod(e, expandMode, "Country", "CountryId", "tblCountry");
                    break;
                case "Select Practice group/Corporate/Country of Target":
                    NewMethod(e, expandMode, "Country", "CountryId", "tblCountry");
                    break;
                case "Select Practice group/Corporate/Value over US$5m":
                    NewMethod(e, expandMode, "Yes", "YesNoId", "tblYesNo", "3");
                    break;
                case "Select Practice group/Corporate/Value over £500,000":
                    NewMethod(e, expandMode, "Yes", "YesNoId", "tblYesNo", "3");
                    break;
                case "Select Practice group/Corporate/Value over Euro 5m":
                    NewMethod(e, expandMode, "Yes", "YesNoId", "tblYesNo", "3");
                    break;
                case "Select Practice group/Corporate/Value range in Euros":
                    NewMethod(e, expandMode, "Value_Range", "ValueRangeId", "tblValueRange", "3");
                    break;
                case "Select Practice group/Corporate/Is the deal relevant for M&A Study":
                    NewMethod(e, expandMode, "Yes", "YesNoId", "tblYesNo", "3");
                    break;
                case "Select Practice group/Corporate/Does the deal involve PE Clients on either side":
                    NewMethod(e, expandMode, "Yes", "YesNoId", "tblYesNo", "3");
                    break;
                case "Select Practice group/Corporate/Quarter Deal Announced":
                    NewMethod(e, expandMode, "Quarter_Deal_Completed", "QuarterDealCompletedId", "tblquarterdealcompleted", "3");
                    break;
                case "Select Practice group/Corporate/Quarter Deal Completed":
                    NewMethod(e, expandMode, "Quarter_Deal_Completed", "QuarterDealCompletedId", "tblquarterdealcompleted", "3");
                    break;
                case "Select Practice group/Corporate/Year Deal Announced/Signed":
                    NewMethod(e, expandMode, "Year_Deal_Completed", "YearDealCompletedId", "tblyeardealcompleted", "3");
                    break;
                case "Select Practice group/Corporate/Year Deal Completed":
                    NewMethod(e, expandMode, "Year_Deal_Completed", "YearDealCompletedId", "tblyeardealcompleted", "3");
                    break;

            }
            /* if (e.Node.Text == "BAIF")
             {
                 DataTable data = objSP.GetDataTable("select fieldid,fielddescription,practicegroupid from tblfield where practicegroupid='" + e.Node.Value + "'");

                 foreach (DataRow row in data.Rows)
                 {
                     RadTreeNode node = new RadTreeNode();
                     node.Text = row["fielddescription"].ToString();
                     node.Value = row["fieldid"].ToString();
                     /* if (Convert.ToInt32(row["ChildrenCount"]) > 0)
                      {
                    
                      }

                     node.ExpandMode = expandMode;
                     e.Node.Nodes.Add(node);
                 }

                 e.Node.Expanded = true;
             }*/
            objLog.LogWriter("SearchScreen : PopulateNodeOnDemand Ends ", hidName.Value);
        }

        private void NewMethod(RadTreeNodeEventArgs e, TreeNodeExpandMode expandMode, string strname, string strid, string strTable, string strBGId = null)
        {
            objLog.LogWriter("SearchScreen : NewMethod Starts ", hidName.Value);

            DataTable data = null;
            if (strBGId != null)
            {
                data = objSP.GetDataTable("select " + strid + "," + strname + " from  " + strTable + " where businessgroupid='" + strBGId + "' and exclude=0 order by " + strname);
            }
            else
            {
                data = objSP.GetDataTable("select " + strid + "," + strname + " from  " + strTable + " where exclude=0");
            }

            foreach (DataRow row in data.Rows)
            {
                RadTreeNode node = new RadTreeNode();
                node.Text = row[strname].ToString();
                node.Value = row[strid].ToString();
                //node.ExpandMode = expandMode;
                node.Checkable = true;
                e.Node.Nodes.Add(node);
            }
            // e.Node.Expanded = false;
            objLog.LogWriter("SearchScreen : NewMethod Ends", hidName.Value);
        }

        protected void imgexpandCS_Click(object sender, EventArgs e)
        {
            ShowMethod(hidCS, imgexpandCS, plnCS, "usp_ClientIndustrySectorList", "Client_Industry_Sector", "ClientIndustrySectorId", RadListBox1, RadListBox2, radTTCS, lblCS);
            imgexpandCS.Visible = false;
            imgcollapseCS.Visible = true;
        }

        protected void imgcollapseCS_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandCS, plnCS, RadListBox1, RadListBox2, radTTCS, lblCS);
            imgcollapseCS.Visible = false;
            imgexpandCS.Visible = true;
        }

        //imgexpandCSS_Click
        protected void imgexpandCSS_Click(object sender, EventArgs e)
        {
            ShowMethod(hidCSS, imgexpandCSS, plnCSS, "usp_ClientIndustryTypeList", "Client_Industry_Type", "ClientIndustryTypeId", radlstSourceCSS, radlstDestCSS, radTTCSS, lblCSS);
            imgexpandCSS.Visible = false;
            imgcollapseCSS.Visible = true;
        }
		
        protected void imgcollapseCSS_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandCSS, plnCSS, radlstSourceCSS, radlstDestCSS, radTTCSS, lblCSS);
            imgcollapseCSS.Visible = false;
            imgexpandCSS.Visible = true;
        }

        protected void imgexpandMS_Click(object sender, EventArgs e)
        {
            ShowMethod(hidMS, imgexpandMS, plnMS, "usp_TransactionIndustrySectorList", "Transaction_Industry_Sector", "TransactionIndustrySectorId", radlstSourceMS, radlstDestMS, radTTMS, lblMS);
            imgexpandMS.Visible = false;
            imgcollapseMS.Visible = true;
        }
		
        protected void imgcollapseMS_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandMS, plnMS, radlstSourceMS, radlstDestMS, radTTMS, lblMS);
            imgcollapseMS.Visible = false;
            imgexpandMS.Visible = true;
        }
		
        protected void imgexpandMSS_Click(object sender, EventArgs e)
        {
            ShowMethod(hidMSS, imgexpandMSS, plnMSS, "usp_TransactionIndustryTypeList", "Transaction_Industry_Type", "TransactionIndustryTypeId", radlstSourceMSS, radlstDestMSS, radTTMSS, lblMSS);
            imgexpandMSS.Visible = false;
            imgcollapseMSS.Visible = true;
        }
		
        protected void imgcollapseMSS_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandMSS, plnMSS, radlstSourceMSS, radlstDestMSS, radTTMSS, lblMSS);
            imgcollapseMSS.Visible = false;
            imgexpandMSS.Visible = true;
        }
		
        protected void imgexpandML_Click(object sender, EventArgs e)
        {
            ShowMethod(hidML, imgexpandML, plnML, "usp_CountryList", "Country", "CountryId", radlstSourceML, radlstDestML, radTTML, lblML);
            imgexpandML.Visible = false;
            imgcollapseML.Visible = true;
        }
		
        protected void imgcollapseML_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandML, plnML, radlstSourceML, radlstDestML, radTTML, lblML);
            imgcollapseML.Visible = false;
            imgexpandML.Visible = true;
        }

        protected void imgexpandTeams_Click(object sender, EventArgs e)
        {
            ShowMethod(hidTeams, imgexpandTeams, plnTeams, "usp_TeamList", "Team", "TeamId", radlstSourceTeams, radlstDestTeams, radTTTeams, lblTeams);
            imgexpandTeams.Visible = false;
            imgcollapseTeams.Visible = true;
        }
		
        protected void imgcollapseTeams_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandTeams, plnTeams, radlstSourceTeams, radlstDestTeams, radTTTeams, lblTeams);
            imgcollapseTeams.Visible = false;
            imgexpandTeams.Visible = true;
        }

        protected void imgexpandLP_Click(object sender, EventArgs e)
        {
            ShowMethod(hidLP, imgexpandLP, plnLP, "usp_LeadPartnerList_New", "Lead_Partner", "LeadPartnerId", radlstSourceLP, radlstDestLP, radTTLP, lblLP);
            imgexpandLP.Visible = false;
            imgcollapseLP.Visible = true;
        }
		
        protected void imgcollapseLP_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandLP, plnLP, radlstSourceLP, radlstDestLP, radTTLP, lblLP);
            imgcollapseLP.Visible = false;
            imgexpandLP.Visible = true;
        }

        protected void imgexpandME_Click(object sender, EventArgs e)
        {
            ShowMethod(hidME, imgexpandME, plnME, "usp_OtherMatterExecutiveList", "Other_Matter_Executive", "OtherMatterExecutiveId", radlstSourceME, radlstDestME, radTTME, lblME);
            imgexpandME.Visible = false;
            imgcollapseME.Visible = true;
        }
		
        protected void imgcollapseME_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandME, plnME, radlstSourceME, radlstDestME, radTTME, lblME);
            imgcollapseME.Visible = false;
            imgexpandME.Visible = true;
        }
		
        /*protected void imgexpandPR_Click(object sender, EventArgs e)
        {
            ShowMethod(hidPR, imgexpandPR, plnPR, "usp_PriorityList", "Priority", "PriorityId", radlstSourcePR, radlstDestPR, radTTPR, lblPR);
            imgexpandPR.Visible = false;
            imgcollapsePR.Visible = true;
        }
		
        protected void imgcollapsePR_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandPR, plnPR, radlstSourcePR, radlstDestPR, radTTPR, lblPR);
            imgcollapsePR.Visible = false;
            imgexpandPR.Visible = true;
        }*/
		
        protected void imgexpandCD_Click(object sender, EventArgs e)
        {
            ShowMethod(hidCD, imgexpandCD, plnCD, "usp_CurrencyOfDealList", "Currency_Of_Deal", "CurrencyOfDealId", radlstSourceCD, radlstDestCD, radTTCD, lblCD);
            imgexpandCD.Visible = false;
            imgcollapseCD.Visible = true;
        }
		
        protected void imgcollapseCD_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandCD, plnCD, radlstSourceCD, radlstDestCD, radTTCD, lblCD);
            imgcollapseCD.Visible = false;
            imgexpandCD.Visible = true;
        }

        //radlstDestContentious

        protected void imgexpandContentious_Click(object sender, EventArgs e)
        {
            ShowMethod(hidContentious, imgexpandContentious, plnContentious, "usp_GetContentious", "TheDescription", "TheValue", radlstSourceContentious, radlstDestContentious, radTTContentious, lblContentious);
            imgexpandContentious.Visible = false;
            imgcollapseContentious.Visible = true;
        }
		
        protected void imgcollapseContentious_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandContentious, plnContentious, radlstSourceContentious, radlstDestContentious, radTTContentious, lblContentious);
            imgcollapseContentious.Visible = false;
            imgexpandContentious.Visible = true;
        }

        protected void imgexpandDR_Click(object sender, EventArgs e)
        {
            ShowMethod(hidDR, imgexpandDR, plnDR, "usp_DisputeResolutionList", "Dispute_Resolution", "DisputeResolutionId", radlstSourceDR, radlstDestDR, radTTDR, lblDR);
            imgexpandDR.Visible = false;
            imgcollapseDR.Visible = true;
        }
		
        protected void imgcollapseDR_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandDR, plnDR, radlstSourceDR, radlstDestDR, radTTDR, lblDR);
            imgcollapseDR.Visible = false;
            imgexpandDR.Visible = true;
        }

        protected void imgexpandCOA_Click(object sender, EventArgs e)
        {
            ShowMethod(hidCOA, imgexpandCOA, plnCOA, "usp_CountryList", "Country", "CountryId", radlstSourceCOA, radlstDestCOA, radTTCOA, lblCOA);
            imgexpandCOA.Visible = false;
            imgcollapseCOA.Visible = true;
        }
		
        protected void imgcollapseCOA_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandCOA, plnCOA, radlstSourceCOA, radlstDestCOA, radTTCOA, lblCOA);
            imgcollapseCOA.Visible = false;
            imgexpandCOA.Visible = true;
        }

        protected void imgexpandSOA_Click(object sender, EventArgs e)
        {
            ShowMethod(hidSOA, imgexpandSOA, plnSOA, "usp_SeatOfArbitrationList", "Seat_Of_Arbitration", "SeatOfArbitrationId", radlstSourceSOA, radlstDestSOA, radTTSOA, lblSOA);
            imgexpandSOA.Visible = false;
            imgcollapseSOA.Visible = true;
        }
		
        protected void imgcollapseSOA_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandSOA, plnSOA, radlstSourceSOA, radlstDestSOA, radTTSOA, lblSOA);
            imgcollapseSOA.Visible = false;
            imgexpandSOA.Visible = true;
        }
		
        protected void imgexpandAR_Click(object sender, EventArgs e)
        {
            ShowMethod(hidAR, imgexpandAR, plnAR, "usp_ArbitralRulesList", "Arbitral_Rules", "ArbitralRulesId", radlstSourceAR, radlstDestAR, radTTAR, lblAR);
            imgexpandAR.Visible = false;
            imgcollapseAR.Visible = true;
        }
		
        protected void imgcollapseAR_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandAR, plnAR, radlstSourceAR, radlstDestAR, radTTAR, lblAR);
            imgcollapseAR.Visible = false;
            imgexpandAR.Visible = true;
        }
		
        /* protected void imgexpandIT_Click(object sender, EventArgs e)
         {
             ShowMethod(hidIT, imgexpandIT, plnIT, "usp_DisputeResolutionList", "Dispute_Resolution", "DisputeResolutionId", radlstSourceIT, radlstDestIT, radTTIT, lblIT);
             imgexpandIT.Visible = false;
             imgcollapseIT.Visible = true;
         }
		 
         protected void imgcollapseIT_Click(object sender, EventArgs e)
         {
             HideMethod(imgexpandIT, plnIT, radlstSourceIT, radlstDestIT, radTTIT, lblIT);
             imgcollapseIT.Visible = false;
             imgexpandIT.Visible = true;
         }*/
		 
        protected void imgexpandIVT_Click(object sender, EventArgs e)
        {
            ShowMethod(hidIVT, imgexpandIVT, plnIVT, "usp_InvestigationTypeList", "Investigation_Type", "InvestigationTypeId", radlstSourceIVT, radlstDestIVT, radTTIVT, lblIVT);
            imgexpandIVT.Visible = false;
            imgcollapseIVT.Visible = true;
        }
		
        protected void imgcollapseIVT_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandIVT, plnIVT, radlstSourceIVT, radlstDestIVT, radTTIVT, lblIVT);
            imgcollapseIVT.Visible = false;
            imgexpandIVT.Visible = true;
        }
		
        protected void imgexpandLOD_Click(object sender, EventArgs e)
        {
            ShowMethod(hidLOD, imgexpandLOD, plnLOD, "usp_LanguageOfDisputeList", "Language_Of_Dispute", "LanguageOfDisputeId", radlstSourceLOD, radlstDestLOD, radTTLOD, lblLOD);
            imgexpandLOD.Visible = false;
            imgcollapseLOD.Visible = true;
        }
		
        protected void imgcollapseLOD_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandLOD, plnLOD, radlstSourceLOD, radlstDestLOD, radTTLOD, lblLOD);
            imgcollapseLOD.Visible = false;
            imgexpandLOD.Visible = true;
        }
		
        protected void imgexpandJOD_Click(object sender, EventArgs e)
        {
            ShowMethod(hidJOD, imgexpandJOD, plnJOD, "usp_CountryList", "Country", "CountryId", radlstSourceJOD, radlstDestJOD, radTTJOD, lblJOD);
            imgexpandJOD.Visible = false;
            imgcollapseJOD.Visible = true;
        }
		
        protected void imgcollapseJOD_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandJOD, plnJOD, radlstSourceJOD, radlstDestJOD, radTTJOD, lblJOD);
            imgcollapseJOD.Visible = false;
            imgexpandJOD.Visible = true;
        }
		
        protected void imgexpandCFI_Click(object sender, EventArgs e)
        {
            ShowMethod(hidCFI, imgexpandCFI, plnCFI, "usp_ReferredFromOtherCMSOfficeList", "Referred_From_Other_CMS_Office", "ReferredFromOtherCMSOfficeId", radlstSourceCFI, radlstDestCFI, radTTCFI, lblCFI);
            imgexpandCFI.Visible = false;
            imgcollapseCFI.Visible = true;
        }
		
        protected void imgcollapseCFI_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandCFI, plnCFI, radlstSourceCFI, radlstDestCFI, radTTCFI, lblCFI);
            imgcollapseCFI.Visible = false;
            imgexpandCFI.Visible = true;
        }
		
        protected void imgexpandLCF_Click(object sender, EventArgs e)
        {
            ShowMethod(hidLCF, imgexpandLCF, plnLCF, "usp_ReferredFromOtherCMSOfficeList", "Referred_From_Other_CMS_Office", "ReferredFromOtherCMSOfficeId", radlstSourceLCF, radlstDestLCF, radTTLCF, lblLCF);
            imgexpandLCF.Visible = false;
            imgcollapseLCF.Visible = true;
        }
		
        protected void imgcollapseLCF_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandLCF, plnLCF, radlstSourceLCF, radlstDestLCF, radTTLCF, lblLCF);
            imgcollapseLCF.Visible = false;
            imgexpandLCF.Visible = true;
        }
		
        protected void imgexpandCCF_Click(object sender, EventArgs e)
        {
            ShowMethod(hidCCF, imgexpandCCF, plnCCF, "usp_CountryList", "Country", "CountryId", radlstSourceCCF, radlstDestCCF, radTTCCF, lblCCF);
            imgexpandCCF.Visible = false;
            imgcollapseCCF.Visible = true;
        }
		
        protected void imgcollapseCCF_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandCCF, plnCCF, radlstSourceCCF, radlstDestCCF, radTTCCF, lblCCF);
            imgcollapseCCF.Visible = false;
            imgexpandCCF.Visible = true;
        }
		
        protected void imgexpandOU_Click(object sender, EventArgs e)
        {
            ShowMethod(hidOU, imgexpandOU, plnOU, "usp_OtherUsesList", "Other_Uses", "OtherUsesId", radlstSourceOU, radlstDestOU, radTTOU, lblOU);
            imgexpandOU.Visible = false;
            imgcollapseOU.Visible = true;
        }
		
        protected void imgcollapseOU_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandOU, plnOU, radlstSourceOU, radlstDestOU, radTTOU, lblOU);
            imgcollapseOU.Visible = false;
            imgexpandOU.Visible = true;
        }
		
        protected void imgexpandAL_Click(object sender, EventArgs e)
        {
            ShowMethod(hidAL, imgexpandAL, plnAL, "usp_CountryLawList", "Country_Law", "CountryLawId", radlstSourceAL, radlstDestAL, radTTAL, lblAL);
            imgexpandAL.Visible = false;
            imgcollapseAL.Visible = true;
        }
		
        protected void imgcollapseAL_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandAL, plnAL, radlstSourceAL, radlstDestAL, radTTAL, lblAL);
            imgcollapseAL.Visible = false;
            imgexpandAL.Visible = true;
        }
		
        protected void imgexpandCMO_Click(object sender, EventArgs e)
        {
            ShowMethod(hidCMO, imgexpandCMO, plnCMO, "usp_CountryList", "Country", "CountryId", radlstSourceCMO, radlstDestCMO, radTTCMO, lblCMO);
            imgexpandCMO.Visible = false;
            imgcollapseCMO.Visible = true;
        }
		
        protected void imgcollapseCMO_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandCMO, plnCMO, radlstSourceCMO, radlstDestCMO, radTTCMO, lblCMO);
            imgcollapseCMO.Visible = false;
            imgexpandCMO.Visible = true;
        }
		
        protected void imgexpandPCC_Click(object sender, EventArgs e)
        {
            ShowMethod(hidPCC, imgexpandPCC, plnPCC, "usp_CountryList", "Country", "CountryId", radlstSourcePCC, radlstDestPCC, radTTPCC, lblPCC);
            imgexpandPCC.Visible = false;
            imgcollapsePCC.Visible = true;
        }
		
        protected void imgcollapsePCC_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandPCC, plnPCC, radlstSourcePCC, radlstDestPCC, radTTPCC, lblPCC);
            imgcollapsePCC.Visible = false;
            imgexpandPCC.Visible = true;
        }
		
        protected void imgexpandKH_Click(object sender, EventArgs e)
        {
            ShowMethod(hidKH, imgexpandKH, plnKH, "usp_KnowHowList", "Know_How", "KnowHowId", radlstSourceKH, radlstDestKH, radTTKH, lblKH);
            imgexpandKH.Visible = false;
            imgcollapseKH.Visible = true;
        }
		
        protected void imgcollapseKH_Click(object sender, EventArgs e)
        {
            HideMethod(imgexpandKH, plnKH, radlstSourceKH, radlstDestKH, radTTKH, lblKH);
            imgcollapseKH.Visible = false;
            imgexpandKH.Visible = true;
        }

        protected void ChangeColor(RadListBox radDest)
        {
            if (radDest.Items.Count > 0)
            {
                radDest.BackColor = System.Drawing.Color.Beige;
            }
            else
            {
                radDest.BackColor = System.Drawing.Color.White;
            }
        }

        private void ShowMethod(HiddenField hid, ImageButton img, Panel divdisplay, string strSpName, string strText, string strValue, RadListBox rad, RadListBox radDEstination, RadToolTip radTT, Label lblSelected)
        {
            if (hid.Value == "0")
            {
                objSP.LoadValues(strSpName, strText, strValue, telradlist: rad, strCheck: "0");
                hid.Value = Convert.ToString(Convert.ToInt32(hid.Value) + Convert.ToInt32(1));
            }

            divdisplay.Style.Add("display", "block");
            radTT.Visible = false;
            lblSelected.Visible = false;

            if (radDEstination.Items.Count > 0)
            {
                radDEstination.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5F5DC");
            }
            else
            {
                radDEstination.BackColor = System.Drawing.Color.White;
            }
        }

        private void HideMethod(ImageButton img, Panel divdisplay, RadListBox rad, RadListBox radDEstination, RadToolTip radTT, Label lblSelected)
        {
            divdisplay.Style.Add("display", "none");
            string str = string.Empty;
            string strDestText = string.Empty;

            for (int i = 0; i < radDEstination.Items.Count; i++)
            {
                if (string.IsNullOrEmpty(str))
                {
                    str = radDEstination.Items[i].Value;
                    strDestText = radDEstination.Items[i].Text;
                }
                else
                {
                    str = str + "," + radDEstination.Items[i].Value;
                    strDestText = strDestText + "," + radDEstination.Items[i].Text;
                }
            }

            if (radDEstination.Items.Count > 0)
            {
                radTT.Visible = true;
                lblSelected.Visible = true;
                radTT.Text = strDestText;
            }
            else
            {
                radTT.Visible = false;
                lblSelected.Visible = false;
            }
        }

        private void LoadDropDowns()
        {
            objLog.LogWriter("SearchScreen : LoadDropDowns Starts", hidName.Value);

            /*objSP.LoadValues("usp_ClientIndustrySectorList", "Client_Industry_Sector", "ClientIndustrySectorId", telrad: rad_Tab_ClientIndustrySector, strCheck: "0");
            objSP.LoadValues("usp_ClientIndustryTypeList", "Client_Industry_Type", "ClientIndustryTypeId", telrad: rad_Tab_Client_Industry_Type, strCheck: "0");
            objSP.LoadValues("usp_CountryList", "Country", "CountryId", telrad: rad_Tab_Country_Matter_Close, strCheck: "0");
            objSP.LoadValues("usp_TeamList", "Team", "TeamId", telrad: rad_Tab_Team, strCheck: "0");
            objSP.LoadValues("usp_LeadPartnerList_New", "Lead_Partner", "LeadPartnerId", telrad: rad_Tab_Lead_Partner, strCheck: "0");
            

            objSP.LoadValues("usp_CountryList", "Country", "CountryId", telrad: rad_Tab_Country_PredominantCountry, strCheck: "0");
            objSP.LoadValues("usp_TransactionIndustrySectorList", "Transaction_Industry_Sector", "TransactionIndustrySectorId", telrad: rad_Tab_TransactionIndustrySector, strCheck: "0");
            objSP.LoadValues("usp_TransactionIndustryTypeList", "Transaction_Industry_Type", "TransactionIndustryTypeId", telrad: rad_Tab_Transaction_Industry_Type, strCheck: "0");
            objSP.LoadValues("usp_CountryLawList", "Country_Law", "CountryLawId", telrad: rad_Tab_Country_Law, strCheck: "0");
            objSP.LoadValues("usp_CountryList", "Country", "CountryId", telrad: rad_Tab_Country_Matter_Open, strCheck: "0");
            objSP.LoadValues("usp_CountryList", "Country", "CountryId", telrad: rad_Tab_CountryofArbitration, strCheck: "0");
            objSP.LoadValues("usp_CurrencyOfDealList", "Currency_Of_Deal", "CurrencyOfDealId", telrad: rad_Tab_Currency_Of_Deal, strCheck: "0");

            objSP.LoadValues("usp_ReferredFromOtherCMSOfficeList", "Referred_From_Other_CMS_Office", "ReferredFromOtherCMSOfficeId", telrad: rad_Tab_Referred_From_Other_CMS_Office, strCheck: "0");
            objSP.LoadValues("usp_ReferredFromOtherCMSOfficeList", "Referred_From_Other_CMS_Office", "ReferredFromOtherCMSOfficeId", telrad: rad_Tab_Lead_CMS_Firms, strCheck: "0");
            objSP.LoadValues("usp_CountryList", "Country", "CountryId", telrad: rad_tab_Countries_of_other_CMS_firms, strCheck: "0");


            objSP.LoadValues("usp_OtherUsesList", "Other_Uses", "OtherUsesId", telrad: rad_Tab_Other_Uses, strCheck: "0");
            objSP.LoadValues("usp_KnowHowList", "Know_How", "KnowHowId", telrad: rad_Tab_Know_How, strCheck: "0");

            objSP.LoadValues("usp_OtherMatterExecutiveList", "Other_Matter_Executive", "OtherMatterExecutiveId", telrad: rad_Matter_Executive, strCheck: "0");*/

            //objSP.LoadValues("usp_BusinessGroupList", "Business_Group", "BusinessGroupId", telrad: rad_Tab_Practice_Group, strCheck: "0");

            //LoadWorkType();

            LoadWorkTypeView();


            /*objSP.LoadValues("usp_GetContentious", "TheDescription", "TheValue", telrad: rad_Tab_Contentious_IRG, strCheck: "0");
            objSP.LoadValues("usp_CountryList", "Country", "CountryId", telrad: rad_Tab_Country_Jurisdiction, strCheck: "0");
            objSP.LoadValues("usp_LanguageOfDisputeList", "Language_Of_Dispute", "LanguageOfDisputeId", telrad: rad_Tab_Language_Of_Dispute, strCheck: "0");
            objSP.LoadValues("usp_DisputeResolutionList", "Dispute_Resolution", "DisputeResolutionId", telrad: rdo_Tab_Dispute_Resolution, strCheck: "0");
            objSP.LoadValues("usp_CountryLawList", "Country_Law", "CountryLawId", telrad: rad_Tab_ArbitrationCity, strCheck: "0");
            objSP.LoadValues("usp_ArbitralRulesList", "Arbitral_Rules", "ArbitralRulesId", telrad: rad_Tab_Arbitral_Rules, strCheck: "0");*/
            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", telrad: rad_Tab_InvestmentTreaty, strCheck: "0");
            // objSP.LoadValues("usp_InvestigationTypeList", "Investigation_Type", "InvestigationTypeId", telrad: rad_Tab_Investigation_Type, strCheck: "0");

            objSP.LoadValues("usp_PriorityList", "Priority", "PriorityId", telrad: rad_Tab_Priority, strCheck: "0");

            objSP.LoadValues("usp_CredentialTypeList", "Credential_Type", "CredentialTypeId", telrad: cbo_Tab_Credential_Type, strCheck: "0");
            objSP.LoadValues("usp_CredentialStatusList", "Credential_Status", "CredentialStatusId", telrad: cbo_Tab_Credential_Status, strCheck: "0");
            objSP.LoadValues("usp_CredentialVersionList", "Credential_Version", "CredentialVersionId", telrad: cbo_Tab_Credential_Version, strCheck: "0");

            objSP.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", telrad: cbo_Tab_ProBono, strCheck: "0");

            objLog.LogWriter("SearchScreen : LoadDropDowns Ends", hidName.Value);
        }

        private DataTable GetDataTable(string sql)
        {
			string ConnString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
			SqlConnection conn = new SqlConnection(ConnString);
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = new SqlCommand(sql, conn);
			DataTable myDataTable = new DataTable();
			
			conn.Open();
			adapter.Fill(myDataTable);
			conn.Close();
			
			return myDataTable;
        }

        private void LoadWorkTypeView()
        {
            objLog.LogWriter("SearchScreen : LoadWorkTypeView Starts ", hidName.Value);

            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            cmd.CommandText = "select distinct(tblWorkType.businessgroupid),(case When tblBusinessGroup.Business_Group ='EPC - Energy' Then 'EPC Energy' When tblBusinessGroup.Business_Group<>'EPC - Energy' Then tblBusinessGroup.Business_Group END) AS Business_Group FROM  tblWorkType INNER JOIN tblBusinessGroup ON tblWorkType.BusinessGroupId = tblBusinessGroup.BusinessGroupId ORDER BY tblBusinessGroup.business_group asc";
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
						case "EPC PROJECTS":
							str = "EPC PROJECTS";
							break;
                        case "REAL ESTATE":
                            str = "REAL ESTATE";
                            break;
                        case "EPC ENERGY":
                            str = "EPC ENERGY";
                            break;
                        case "HUMAN CAPITAL (HC)":
                            str = "HUMAN CAPITAL";
                            break;
                        case "CORPORATE TAX":
                            str = "CORPORATE TAX";
                            break;
                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(str.Trim()))
                    {
                        sort.Add(i, str);
                    }
                }
            }
			
			SortedList sortnew = new SortedList();
			for (int i = 0; i <= sort.Count - 1; i++)
			{
				if (!sortnew.Contains(i))
				{
					if (i == 7)
					{
						i = 8;
					}
					sortnew.Add(i, sort[i].ToString());
				}
			}
			
            RadTreeNode root12 = new RadTreeNode("Select Worktype(s)");
            root12.Text = "Select Worktype(s)";
			root12.Value = "Select Worktype(s)";
            root12.Checkable = false;
            RadTreeView1.Nodes.Add(root12);

            foreach (DictionaryEntry ent in sortnew)
            {
                string str = ent.Value.ToString().ToUpper();
                RadTreeNode root13 = new RadTreeNode(ent.Value.ToString().ToUpper());

                if (!string.IsNullOrEmpty(ent.Value.ToString().ToUpper()))
                {

                    root13.Text = ent.Value.ToString().ToUpper();
					root13.Value = ent.Value.ToString().ToUpper();
                    root13.Checkable = true;
                    /*if (str == "CRD")
                    {
                        root11.Checkable = true;
                    }*/
                    root12.Nodes.Add(root13);
                }
                //RadTreeView1.Nodes.Add(root11);

                if (str == "BAIF")
                {
                    DataTable dt = GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='1' AND EXCLUDE ='0' order by work_type asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            root13.Nodes.Add(new RadTreeNode(dt.Rows[i][0].ToString().Trim(), dt.Rows[i][1].ToString()));
                        }
                    }
                }

                if (str == "CORPORATE")
                {
                    DataTable dt = GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='3' AND EXCLUDE ='0' order by work_type asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //root11.Nodes.Add(new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));
                            RadTreeNode chd = new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            root13.Nodes.Add(chd);
                            chd.Checkable = true;

                            DataTable dtSubWorkType = GetDataTable("SELECT subwork_type,subworktypeid FROM TBLsubWORKTYPE WHERE worktypeid='" + dt.Rows[i][1].ToString() + "' AND EXCLUDE ='0' order by subwork_type asc");

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

                if (str == "CRD")
                {
                    DataTable dt = GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='4' AND EXCLUDE ='0' order by work_type asc");
                    if (dt.Rows.Count > 0)
                    {
                        SortedList htCRD = new SortedList();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (htCRD.Contains(dt.Rows[i][0].ToString().Split(':')[0]) == false)
                            {
                                htCRD.Add(dt.Rows[i][0].ToString().Split(':')[0], dt.Rows[i][0].ToString().Split(':')[0]);
                            }
                        }
                        foreach (DictionaryEntry entCRD in htCRD)
                        {
                            //root11.Nodes.Add(new RadTreeNode(entCRD.Value.ToString().ToUpper()));
                            if (!string.IsNullOrEmpty(entCRD.Value.ToString().ToUpper().Trim()))
                            {
                                RadTreeNode chd = new RadTreeNode(entCRD.Value.ToString().ToUpper());
                                root13.Nodes.Add(chd);
                                chd.Checkable = true;

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][0].ToString().ToUpper().Contains(entCRD.Value.ToString().Trim().ToUpper() + ":"))
                                    {
                                        RadTreeNode chd3 = new RadTreeNode(dt.Rows[i][0].ToString().Trim().Replace(entCRD.Value.ToString().Trim() + ":", ""));

                                        chd3.Text = dt.Rows[i][0].ToString().Trim().Replace(entCRD.Value.ToString().Trim() + ":", "");
                                        chd3.Value = dt.Rows[i][1].ToString();
                                        chd.Nodes.Add(chd3);
                                        chd3.Checkable = true;

                                        DataTable dtSubWorkType = GetDataTable("SELECT subwork_type,subworktypeid FROM TBLsubWORKTYPE WHERE worktypeid='" + dt.Rows[i][1].ToString() + "' AND EXCLUDE ='0' order by subwork_type asc");

                                        if (dtSubWorkType.Rows.Count > 0)
                                        {
                                            for (int j = 0; j < dtSubWorkType.Rows.Count; j++)
                                            {
                                                RadTreeNode chd2 = new RadTreeNode(dtSubWorkType.Rows[j][0].ToString().Trim().Replace(dt.Rows[i][0].ToString().Trim() + ":", ""));
                                                chd2.Text = dtSubWorkType.Rows[j][0].ToString().Trim().Replace(dt.Rows[i][0].ToString().Trim() + ":", "");
                                                chd2.Value = dtSubWorkType.Rows[j][1].ToString();
                                                chd2.Checkable = true;
                                                chd3.Nodes.Add(chd2);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
				
                if (str == "EPC CONSTRUCTION")
                {
                    DataTable dt = GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='5' AND EXCLUDE ='0' order by work_type asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RadTreeNode chd = new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            chd.Checkable = true;
                            root13.Nodes.Add(chd);
                        }
                    }
                }
				
                if (str == "REAL ESTATE")
                {
                    DataTable dt = GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='7' AND EXCLUDE ='0' order by work_type asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //root11.Nodes.Add(new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));

                            RadTreeNode chd = new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            root13.Nodes.Add(chd);
                            chd.Checkable = true;

                            DataTable dtSubWorkType = GetDataTable("SELECT subwork_type,subworktypeid FROM TBLsubWORKTYPE WHERE worktypeid='" + dt.Rows[i][1].ToString() + "' AND EXCLUDE ='0' order by subwork_type asc");

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
				
                if (str == "EPC ENERGY")
                {
                    DataTable dt = GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='9' AND EXCLUDE ='0' order by work_type asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RadTreeNode chd = new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            chd.Checkable = true;
                            root13.Nodes.Add(chd);

                        }
                    }
                }
				
                if (str == "HUMAN CAPITAL")
                {
                    DataTable dt = GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='10' AND EXCLUDE ='0' order by work_type asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //root11.Nodes.Add(new RadTreeNode(dt.Rows[i][0].ToString()));

                            RadTreeNode chd = new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
                            root13.Nodes.Add(chd);
                            chd.Checkable = true;

                            DataTable dtSubWorkType = GetDataTable("SELECT subwork_type,subworktypeid FROM TBLsubWORKTYPE WHERE worktypeid='" + dt.Rows[i][1].ToString() + "' AND EXCLUDE ='0' order by subwork_type asc");

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
				
                if (str == "CORPORATE TAX")
                {
                    DataTable dt = GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='11' AND EXCLUDE ='0' order by work_type asc");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            RadTreeNode chd = new RadTreeNode(dt.Rows[i][0].ToString().Trim().Replace("Corporate Tax:", ""), dt.Rows[i][1].ToString());
                            chd.Checkable = true;
                            root13.Nodes.Add(chd);
                        }
                    }
                }
            }

            objLog.LogWriter("SearchScreen : LoadWorkTypeView Ends ", hidName.Value);
        }

        private void LoadJavaScripts()
        {
            objLog.LogWriter("SearchScreen : LoadJavaScripts Starts", hidName.Value);

            /*txtKeywordSearch.Attributes.Add("onkeydown", "return postBackCall(event);");
            txtKeywordSearch.Attributes.Add("onkeypress", "return AlphaNumericonlySpl(event);");*/

            txt_Tab_Client.Attributes.Add("onkeypress", "return AllowOnlyAlphabetsSpl(event);");
            txt_Tab_ProjectName_Core.Attributes.Add("onkeypress", "return AllowOnlyAlphabets(event);");
            txt_tab_Value_Deal.Attributes.Add("onkeypress", "return numbercommadotonly(event);");

            txt_Tab_Bible_Reference.Attributes.Add("onkeypress", "return AlphaNumericonly(event);");

            txt_Tab_Matter_No.Attributes.Add("onkeypress", "return AlphaNumericDotonly(event);");
            txt_Tab_Credential_ID.Attributes.Add("onkeypress", "return numberonly(event);");

            //txt_Tab_Client.Attributes.Add("onpaste", "return AlphaNumericDotonlyPaste('" + txt_Tab_Client.ClientID + "');");
            //txt_Tab_Matter_No.Attributes.Add("onpaste", "return AlphaNumericDotonlyPaste('" + txt_Tab_Matter_No.ClientID + "');");
            //txt_Tab_Bible_Reference.Attributes.Add("onpaste", "return AlphaNumericDotonlyPaste('" + txt_Tab_Bible_Reference.ClientID + "');");
            //txt_Tab_ProjectName_Core.Attributes.Add("onpaste", "return AlphaNumericDotonlyPaste('" + txt_Tab_ProjectName_Core.ClientID + "');");

            /*rad_Tab_ClientIndustrySector.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Client_Industry_Type.Filter = RadComboBoxFilter.Contains;
            rad_Tab_TransactionIndustrySector.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Transaction_Industry_Type.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Country_Matter_Close.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Team.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Lead_Partner.Filter = RadComboBoxFilter.Contains;
            //rad_Tab_Practice_Group.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Priority.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Currency_Of_Deal.Filter = RadComboBoxFilter.Contains;
            rdo_Tab_Dispute_Resolution.Filter = RadComboBoxFilter.Contains;
            rad_Tab_CountryofArbitration.Filter = RadComboBoxFilter.Contains;
            rad_Tab_ArbitrationCity.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Arbitral_Rules.Filter = RadComboBoxFilter.Contains;
            rad_Tab_InvestmentTreaty.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Investigation_Type.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Language_Of_Dispute.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Country_Jurisdiction.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Referred_From_Other_CMS_Office.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Lead_CMS_Firms.Filter = RadComboBoxFilter.Contains;
            rad_tab_Countries_of_other_CMS_firms.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Other_Uses.Filter = RadComboBoxFilter.Contains;
            cbo_Tab_Credential_Status.Filter = RadComboBoxFilter.Contains;
            cbo_Tab_Credential_Version.Filter = RadComboBoxFilter.Contains;
            cbo_Tab_Credential_Type.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Country_Law.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Country_Matter_Open.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Country_PredominantCountry.Filter = RadComboBoxFilter.Contains;
            cbo_Tab_ProBono.Filter = RadComboBoxFilter.Contains;
            rad_Tab_Know_How.Filter = RadComboBoxFilter.Contains;
            rad_Matter_Executive.Filter = RadComboBoxFilter.Contains;*/

            /*img_Matter_Executive.Attributes.Add("onclick", "return UnCheckAll('" + rad_Matter_Executive.ClientID + "');");
            img_Tab_ClientIndustrySector.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_ClientIndustrySector.ClientID + "');");
            img_Tab_Client_Industry_Type.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Client_Industry_Type.ClientID + "');");
            img_Tab_TransactionIndustrySector.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_TransactionIndustrySector.ClientID + "');");
            img_Tab_Transaction_Industry_Type.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Transaction_Industry_Type.ClientID + "');");
            img_Tab_Country_Matter_Close.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Country_Matter_Close.ClientID + "');");
            img_Tab_Team.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Team.ClientID + "');");
            img_Tab_Lead_Partner.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Lead_Partner.ClientID + "');");*/
            img_Tab_Practice_Group.Attributes.Add("onclick", "return GetCheckNodes1();");
            /*img_Tab_Priority.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Priority.ClientID + "');");
            img_Tab_Currency_Of_Deal.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Currency_Of_Deal.ClientID + "');");
            img_Tab_Contentious_IRG.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Contentious_IRG.ClientID + "');");
            img_Tab_Dispute_Resolution.Attributes.Add("onclick", "return UnCheckAll('" + rdo_Tab_Dispute_Resolution.ClientID + "');");
            img_Tab_CountryofArbitration.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_CountryofArbitration.ClientID + "');");
            img_Tab_ArbitrationCity.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_ArbitrationCity.ClientID + "');");
            img_Tab_Arbitral_Rules.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Arbitral_Rules.ClientID + "');");
            img_Tab_InvestmentTreaty.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_InvestmentTreaty.ClientID + "');");
            img_Tab_Investigation_Type.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Investigation_Type.ClientID + "');");
            img_Tab_Language_Of_Dispute.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Language_Of_Dispute.ClientID + "');");
            img_Tab_Country_Jurisdiction.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Country_Jurisdiction.ClientID + "');");
            img_Tab_Referred_From_Other_CMS_Office.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Referred_From_Other_CMS_Office.ClientID + "');");
            img_Tab_Lead_CMS_Firms.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Lead_CMS_Firms.ClientID + "');");
            img_tab_Countries_of_other_CMS_firms.Attributes.Add("onclick", "return UnCheckAll('" + rad_tab_Countries_of_other_CMS_firms.ClientID + "');");
            img_Tab_Other_Uses.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Other_Uses.ClientID + "');");
            img_Tab_Credential_Status.Attributes.Add("onclick", "return UnCheckAll('" + cbo_Tab_Credential_Status.ClientID + "');");
            img_Tab_Credential_Version.Attributes.Add("onclick", "return UnCheckAll('" + cbo_Tab_Credential_Version.ClientID + "');");
            img_Tab_Credential_Type.Attributes.Add("onclick", "return UnCheckAll('" + cbo_Tab_Credential_Type.ClientID + "');");
            img_Tab_Country_Law.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Country_Law.ClientID + "');");
            img_Tab_Country_Matter_Open.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Country_Matter_Open.ClientID + "');");
            img_Tab_Country_PredominantCountry.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Country_PredominantCountry.ClientID + "');");
            img_Tab_ProBono.Attributes.Add("onclick", "return UnCheckAll('" + cbo_Tab_ProBono.ClientID + "');");
            img_Tab_Know_How.Attributes.Add("onclick", "return UnCheckAll('" + rad_Tab_Know_How.ClientID + "');");*/
            img_Date.Attributes.Add("onclick", "return ClearDate();");
            img_WorkType.Attributes.Add("onclick", "return GetCheckNodes();");

            objLog.LogWriter("SearchScreen : LoadJavaScripts Ends", hidName.Value);
        }

        private void LoadKeyWordSearch()
        {
            objLog.LogWriter("SearchScreen : LoadKeyWordSearch Starts", hidName.Value);

            radtxtKeywordSearch.Filter = RadAutoCompleteFilter.Contains;
            radtxtKeywordSearch.Delimiter = ",";

            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter adp = new SqlDataAdapter("select keywordsearch from tblkeywordsearch", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            radtxtKeywordSearch.DataSource = ds.Tables[0];
            radtxtKeywordSearch.DataTextField = "keywordsearch";
            radtxtKeywordSearch.DataBind();

            objLog.LogWriter("SearchScreen : LoadKeyWordSearch Ends", hidName.Value);
        }

        private void CheckTheItems(RadComboBox comboBox, string strItems)
        {
			RadComboBoxItemCollection collection = comboBox.Items;

            for (int i = 0; i < collection.Count; i++)
            {
                for (int j = 0; j < strItems.Split(',').Length; j++)
                {
                    if (collection[i].Value == strItems.Split(',')[j])
                    {
                        collection[i].Checked = true;
                        break;
                    }
                }
            }
        }

        private void PopulateListItems(string strValue, RadListBox radListSource, RadListBox radListDest)
        {
            for (int i = 0; i < strValue.Split(',').Length; i++)
            {
                radListSource.Transfer(radListSource.FindItemByValue(strValue.Split(',')[i]), radListSource, radListDest);
            }
        }
		
        private void ClearListItems(RadListBox radListSource, RadListBox radListDest, ImageButton imgExpand, ImageButton imgCollapse, Panel plnDisplay, RadToolTip radTT, Label lblValues)
        {
            int i = radListDest.Items.Count;

            if (i > 0)
            {
                for (int j = i; j > 0; j--)
                {
                    radListDest.Transfer(radListDest.Items[j - 1], radListDest, radListSource);
                }
            }


            plnDisplay.Style.Add("display", "none");
            imgExpand.Visible = true;
            imgCollapse.Visible = false;
            radTT.Text = string.Empty;
            lblValues.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bool blnRedirect = false;
            bool blnCheckAndOr = false;
            objLog.LogWriter("SearchScreen : btnSearch_Click Starts ", hidName.Value);

            try
            {
                //txt_Tab_OtherLanguage.Text = txt_Tab_OtherLanguage.Text.Replace("'", string.Empty);
                StringBuilder strCriteria = new StringBuilder();
                StringBuilder strResultScreenCriteria = new StringBuilder();

                string strPartial = string.Empty;
                /*if (chkPartial.Checked == true)
                {
                    strPartial = "C.PartialFlag = '1' and "; //Include Partial Save Records
                }
                else
                {
                    strPartial = "C.PartialFlag = '0' and "; //Exclude Partial Save Records
                }*/

                string strSelect = "Select DISTINCT C.CredentialId as CredentialID,C.Credential_Version as CredentialVersion from tblCredential as C ";
                string strWhere = " Where C.DeleteFlag = '0' and ";

                StringBuilder strLEFT = new StringBuilder();
                StringBuilder strWHERECondition = new StringBuilder();

                bool blnSearch = false;

                /* Partial Keyword Starts */
                if (chkPartial.Checked == true)
                {
                    blnSearch = true;
                    blnCheckAndOr = false;
                    strPartial = "C.PartialFlag = '1' and "; //Include Partial Save Records
                    strWHERECondition.Append(strPartial);
					strCriteria.Append("PartialSave");
					strCriteria.Append("=");
					strCriteria.Append("Partial save records");
					strCriteria.Append("|");
					strResultScreenCriteria.Append("Partial save records");
					strResultScreenCriteria.Append("|");
                }
				else
				{
					blnSearch = false;
					blnCheckAndOr = false;
					strPartial = "C.PartialFlag = '0' and ";
					strWHERECondition.Append(strPartial);
				}
				
				/* KeyWord Starts */
				if (!string.IsNullOrEmpty(radtxtKeywordSearch.Text.Trim()))
				{
					string strItems = string.Empty;
					string strItemText = string.Empty;
					for (int i = 0; i < radtxtKeywordSearch.Entries.Count; i++)
					{
						if (string.IsNullOrEmpty(strItems))
						{
							strItems = " C.KeyWordSearch like N'%" + radtxtKeywordSearch.Entries[i].Text.Trim().Replace("'", "''") + "%' or ";
						}
						else
						{
							strItems = strItems + " C.KeyWordSearch like N'%" + radtxtKeywordSearch.Entries[i].Text.Trim().Replace("'", "''") + "%' or ";
						}
						if (string.IsNullOrEmpty(strItemText))
						{
							strItemText = radtxtKeywordSearch.Entries[i].Text.Trim().Replace("'", "''").Replace(",", "^");
						}
						else
						{
							strItemText = strItemText + "," + radtxtKeywordSearch.Entries[i].Text.Trim().Replace("'", "''").Replace(",", "^");
						}
					}
					
					string strKeyWordSearchWhere = "(" + strItems.Substring(0, strItems.Length - 4) + ") and ";

                    strWHERECondition.Append(strKeyWordSearchWhere);
                    blnSearch = true;
                    blnCheckAndOr = true;

					string sk = radtxtKeywordSearch.Text.Trim().Substring(0, radtxtKeywordSearch.Text.Trim().Length - 1).Replace(',', '^');
					strCriteria.Append(radtxtKeywordSearch.ID);
					strCriteria.Append("=");
					strCriteria.Append(strItemText.Replace("''", "'"));
					strCriteria.Append("|");
					strResultScreenCriteria.Append(strItemText.Replace("''", "'"));

					strResultScreenCriteria.Append("|");
				}

                /* KeyWord Ends */
                /* Basic Query Starts */
                if (!string.IsNullOrEmpty(txt_Tab_Client.Text.Trim()))
                {
					string strClientWhere = " C.Client like N'%" + txt_Tab_Client.Text.Trim().Replace("'", "''") + "%' and ";

                    strWHERECondition.Append(strClientWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(txt_Tab_Client.ID);
					strCriteria.Append("=");
					strCriteria.Append(txt_Tab_Client.Text.Trim());
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(txt_Tab_Client.Text.Trim());
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(RadListBox2)))
                {
                    string strSectorLeft = " LEFT JOIN tblCredentialClientIndustrySector as CIS ON CIS.CredentialId = C.CredentialId ";
                    string strSectorWhere = " CIS.ClientIndustrySectorId in (" + getCheckedItems(RadListBox2) + ")  and ";

                    strLEFT.Append(strSectorLeft);
                    strWHERECondition.Append(strSectorWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_ClientIndustrySector");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(RadListBox2));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_ClientIndustrySectorText");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(RadListBox2));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(RadListBox2));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestCSS)))
                {
                    string strSubSectorLeft = " LEFT JOIN tblCredentialClientIndustryType as CIT ON CIT.CredentialId = C.CredentialId ";
                    string strSubSectorWhere = " CIT.ClientIndustryTypeId in (" + getCheckedItems(radlstDestCSS) + ")  and ";

                    strLEFT.Append(strSubSectorLeft);
                    strWHERECondition.Append(strSubSectorWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Client_Industry_Type");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestCSS));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Client_Industry_Type" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestCSS));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestCSS));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestMS)))
                {
                    string strTransactionIndustrySectorLeft = " LEFT JOIN tblCredentialTransactionIndustrySector as TIS ON TIS.CredentialId = C.CredentialId ";
                    string strTransactionIndustrySectorWhere = " TIS.TransactionIndustrySectorId in (" + getCheckedItems(radlstDestMS) + ")  and ";

                    strLEFT.Append(strTransactionIndustrySectorLeft);
                    strWHERECondition.Append(strTransactionIndustrySectorWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_TransactionIndustrySector");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestMS));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_TransactionIndustrySector" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestMS));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestMS));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestMSS)))
                {
                    string strTransactionIndustryTypeLeft = " LEFT JOIN tblCredentialTransactionIndustryType as TIT ON TIT.CredentialId = C.CredentialId ";
                    string strTransactionIndustryTypeWhere = " TIT.TransactionIndustryTypeId in (" + getCheckedItems(radlstDestMSS) + ") " + " and ";

                    strLEFT.Append(strTransactionIndustryTypeLeft);
                    strWHERECondition.Append(strTransactionIndustryTypeWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Transaction_Industry_Type");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestMSS));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Transaction_Industry_Type" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestMSS));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestMSS));
                    strResultScreenCriteria.Append("|");
                }

                /* Work Type */
                string strBAIFWT = string.Empty;
                string strBAIFWTText = string.Empty;

                string strCORPORATEWT = string.Empty;
                string strCORPORATEWTText = string.Empty;
                string strCORPORATESWT = string.Empty;
                string strCORPORATESWTText = string.Empty;

                string strCRDWT = string.Empty;
                string strCRDWTText = string.Empty;
                string strCRDSWT = string.Empty;
                string strCRDSWTText = string.Empty;

                string strCORPORATETAXWT = string.Empty;
                string strCORPORATETAXWTText = string.Empty;

                string strEPCCONSTRUCTIONWT = string.Empty;
                string strEPCCONSTRUCTIONWTText = string.Empty;

                string strREWT = string.Empty;
                string strREWTText = string.Empty;
                string strRESWT = string.Empty;
                string strRESWTText = string.Empty;

                string strEPCENERGYWT = string.Empty;
                string strEPCENERGYWTText = string.Empty;

                string strHCWT = string.Empty;
                string strHCWTText = string.Empty;

                string strHCSWT = string.Empty;
                string strHCSWTText = string.Empty;
				string strBAIFWTWhere = string.Empty;
				string strCORPWTWhere = string.Empty;
				string strCORPORATESWTWhere = string.Empty;
				string strCRDWTWhere = string.Empty;
				string strCORPORATETAXWTWhere = string.Empty;
				string strCRDSWTWhere = string.Empty;
				string strEPCCONSTRUCTIONWTWhere = string.Empty;
				string strREWTWhere = string.Empty;
				string strRESWTWhere = string.Empty;
				string strEPCENERGYWTWhere = string.Empty;
				string strHCWTWhere = string.Empty;
				string strHCSWTWhere = string.Empty;
				
                foreach (RadTreeNode node in RadTreeView1.Nodes)
                {
                    foreach (RadTreeNode node2 in node.Nodes)
                    {
                        if (node2.Text.ToUpper() == "BAIF") /* One Level */
                        {
                            for (int i = 0; i < node2.Nodes.Count; i++)
                            {
                                if (node2.Nodes[i].Checked == true)
                                {
                                    if (string.IsNullOrEmpty(strBAIFWT))
                                    {
                                        strBAIFWT = "'" + node2.Nodes[i].Value + "',";
                                    }
                                    else
                                    {
                                        strBAIFWT = strBAIFWT + "'" + node2.Nodes[i].Value + "',";
                                    }

                                    if (string.IsNullOrEmpty(strBAIFWTText))
                                    {
                                        strBAIFWTText = node2.Nodes[i].Text;
                                    }
                                    else
                                    {
                                        strBAIFWTText = strBAIFWTText + "," + node2.Nodes[i].Text;
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(strBAIFWT))
                            {
                                strBAIFWT = strBAIFWT.Substring(0, strBAIFWT.Length - 1);

                                string strBAIFWTLeft = " LEFT JOIN tblCredentialWorkTypeBAIF as WTBAIF ON WTBAIF.CredentialId = C.CredentialId ";
								strBAIFWTWhere = " WTBAIF.Worktypeid in (" + strBAIFWT + ")  or ";

                                strLEFT.Append(strBAIFWTLeft);
                                blnSearch = true;
                                blnCheckAndOr = false;

                                strCriteria.Append("BAIFWorkTypeId");
								strCriteria.Append("=");
								strCriteria.Append(strBAIFWT.Replace("'", "").Trim());
                                strCriteria.Append("|");

                                strCriteria.Append("BAIFWorkTypeText");
								strCriteria.Append("=");
								strCriteria.Append(strBAIFWTText);
                                strCriteria.Append("|");

                                strResultScreenCriteria.Append(strBAIFWTText);
                                strResultScreenCriteria.Append("|");
                            }
                        }

                        if (node2.Text.ToUpper() == "CORPORATE") /* Two Level */
                        {
                            for (int i = 0; i < node2.Nodes.Count; i++)
                            {
                                if (node2.Nodes[i].Checked == true)
                                {
                                    if (string.IsNullOrEmpty(strCORPORATEWT))
                                    {
                                        strCORPORATEWT = "'" + node2.Nodes[i].Value + "',";
                                    }
                                    else
                                    {
                                        strCORPORATEWT = strCORPORATEWT + "'" + node2.Nodes[i].Value + "',";
                                    }

                                    if (string.IsNullOrEmpty(strCORPORATEWTText))
                                    {
                                        strCORPORATEWTText = node2.Nodes[i].Text;
                                    }
                                    else
                                    {
                                        strCORPORATEWTText = strCORPORATEWTText + "," + node2.Nodes[i].Text;
                                    }
                                }
                            }

                            for (int i = 0; i < node2.Nodes.Count; i++)
                            {
                                for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
                                {
                                    if (node2.Nodes[i].Nodes[j].Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(strCORPORATESWT))
                                        {
                                            strCORPORATESWT = "'" + node2.Nodes[i].Nodes[j].Value + "',";
                                        }
                                        else
                                        {
                                            strCORPORATESWT = strCORPORATESWT + "'" + node2.Nodes[i].Nodes[j].Value + "',";
                                        }

                                        if (string.IsNullOrEmpty(strCORPORATESWTText))
                                        {
                                            strCORPORATESWTText = node2.Nodes[i].Nodes[j].Text;
                                        }
                                        else
                                        {
                                            strCORPORATESWTText = strCORPORATESWTText + "," + node2.Nodes[i].Nodes[j].Text;
                                        }
                                    }
                                }
                            }
							
                            if (!string.IsNullOrEmpty(strCORPORATEWT))
                            {
                                strCORPORATEWT = strCORPORATEWT.Substring(0, strCORPORATEWT.Length - 1);

                                string strCORPWTLeft = " LEFT JOIN tblCredentialWorkType as WTCOR ON WTCOR.CredentialId = C.CredentialId ";
								strCORPWTWhere = " WTCOR.Worktypeid in (" + strCORPORATEWT + ")  or ";

                                strLEFT.Append(strCORPWTLeft);
                                blnSearch = true;
                                blnCheckAndOr = false;

                                strCriteria.Append("CORPWorkTypeId");
								strCriteria.Append("=");
								strCriteria.Append(strCORPORATEWT.Replace("'", "").Trim());
                                strCriteria.Append("|");

                                strCriteria.Append("CORPWorkTypeText");
								strCriteria.Append("=");
								strCriteria.Append(strCORPORATESWTText);
                                strCriteria.Append("|");

                                strResultScreenCriteria.Append(strCORPORATESWTText);
                                strResultScreenCriteria.Append("|");
                            }
							
                            if (!string.IsNullOrEmpty(strCORPORATESWT))
                            {
                                strCORPORATESWT = strCORPORATESWT.Substring(0, strCORPORATESWT.Length - 1);

                                string strCORPORATESWTLeft = " LEFT JOIN tblCredentialSubWorkType as SWTCOR ON SWTCOR.CredentialId = C.CredentialId ";
								strCORPORATESWTWhere = " SWTCOR.SubWorktypeid in (" + strCORPORATESWT + ")  or ";

                                strLEFT.Append(strCORPORATESWTLeft);
                                blnSearch = true;
                                blnCheckAndOr = false;

                                strCriteria.Append("CORPSubWorkTypeId");
								strCriteria.Append("=");
								strCriteria.Append(strCORPORATESWT.Replace("'", "").Trim());
                                strCriteria.Append("|");

                                strCriteria.Append("CORSubPWorkTypeText");
								strCriteria.Append("=");
								strCriteria.Append(strCORPORATESWTText);
                                strCriteria.Append("|");

                                strResultScreenCriteria.Append(strCORPORATESWTText);
                                strResultScreenCriteria.Append("|");
                            }
                        }

                        if (node2.Text.ToUpper() == "CRD") /* Three Level */
                        {
                            for (int i = 0; i < node2.Nodes.Count; i++)
                            {
                                for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
                                {
                                    if (node2.Nodes[i].Nodes[j].Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(strCRDWT))
                                        {
                                            strCRDWT = "'" + node2.Nodes[i].Nodes[j].Value + "',";
                                        }
                                        else
                                        {
                                            strCRDWT = strCRDWT + "'" + node2.Nodes[i].Nodes[j].Value + "',";
                                        }

                                        if (string.IsNullOrEmpty(strCRDWTText))
                                        {
                                            strCRDWTText = node2.Nodes[i].Nodes[j].Text;
                                        }
                                        else
                                        {
                                            strCRDWTText = strCRDWTText + "," + node2.Nodes[i].Nodes[j].Text;
                                        }

                                    }
                                }
                            }

                            for (int i = 0; i < node2.Nodes.Count; i++)
                            {
                                for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
                                {
                                    for (int k = 0; k < node2.Nodes[i].Nodes[j].Nodes.Count; k++)
                                    {
                                        if (node2.Nodes[i].Nodes[j].Nodes[k].Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(strCRDSWT))
                                            {
                                                strCRDSWT = "'" + node2.Nodes[i].Nodes[j].Nodes[k].Value + "',";
                                            }
                                            else
                                            {
                                                strCRDSWT = strCRDSWT + "'" + node2.Nodes[i].Nodes[j].Nodes[k].Value + "',";
                                            }

                                            if (string.IsNullOrEmpty(strCRDSWTText))
                                            {
                                                strCRDSWTText = node2.Nodes[i].Nodes[j].Nodes[k].Text;
                                            }
                                            else
                                            {
                                                strCRDSWTText = strCRDSWTText + "," + node2.Nodes[i].Nodes[j].Nodes[k].Text;
                                            }
                                        }
                                    }
                                }
                            }
							
                            if (!string.IsNullOrEmpty(strCRDWT))
                            {
                                strCRDWT = strCRDWT.Substring(0, strCRDWT.Length - 1);

                                string strCRDWTLeft = " LEFT JOIN tblCredentialWorkTypeCRD as WTCRD ON WTCRD.CredentialId = C.CredentialId ";
								strCRDWTWhere = " WTCRD.Worktypeid in (" + strCRDWT + ")  or ";

                                strLEFT.Append(strCRDWTLeft);
                                blnSearch = true;
                                blnCheckAndOr = false;

                                strCriteria.Append("CRDWorkTypeId");
								strCriteria.Append("=");
								strCriteria.Append(strCRDWT.Replace("'", "").Trim());
                                strCriteria.Append("|");

                                strCriteria.Append("CRDWorkTypeText");
								strCriteria.Append("=");
								strCriteria.Append(strCRDWTText);
                                strCriteria.Append("|");

                                strResultScreenCriteria.Append(strCRDWTText);
                                strResultScreenCriteria.Append("|");
                            }
                            if (!string.IsNullOrEmpty(strCRDSWT))
                            {
                                strCRDSWT = strCRDSWT.Substring(0, strCRDSWT.Length - 1);

                                string strCRDSWTLeft = " LEFT JOIN tblCredentialSubWorkTypeCommercial as SWTCRD ON SWTCRD.CredentialId = C.CredentialId ";
								strCRDSWTWhere = " SWTCRD.SubWorktypeid in (" + strCRDSWT + ")  or ";

                                strLEFT.Append(strCRDSWTLeft);
                                blnSearch = true;
                                blnCheckAndOr = false;

                                strCriteria.Append("CRDSubWorkTypeId");
								strCriteria.Append("=");
								strCriteria.Append(strCRDSWT.Replace("'", "").Trim());
                                strCriteria.Append("|");

                                strCriteria.Append("CRDSubWorkTypeText");
								strCriteria.Append("=");
								strCriteria.Append(strCRDSWTText);
                                strCriteria.Append("|");

                                strResultScreenCriteria.Append(strCRDSWTText);
                                strResultScreenCriteria.Append("|");
                            }
                        }

                        if (node2.Text.ToUpper() == "CORPORATE TAX") /* One Level */
                        {
                            for (int i = 0; i < node2.Nodes.Count; i++)
                            {
                                if (node2.Nodes[i].Checked == true)
                                {
                                    if (string.IsNullOrEmpty(strCORPORATETAXWT))
                                    {
                                        strCORPORATETAXWT = "'" + node2.Nodes[i].Value + "',";
                                    }
                                    else
                                    {
                                        strCORPORATETAXWT = strCORPORATETAXWT + "'" + node2.Nodes[i].Value + "',";
                                    }

                                    if (string.IsNullOrEmpty(strCORPORATETAXWTText))
                                    {
                                        strCORPORATETAXWTText = node2.Nodes[i].Text;
                                    }
                                    else
                                    {
                                        strCORPORATETAXWTText = strCORPORATETAXWTText + "," + node2.Nodes[i].Text;
                                    }
                                }
                            }
							
                            if (!string.IsNullOrEmpty(strCORPORATETAXWT))
                            {
                                strCORPORATETAXWT = strCORPORATETAXWT.Substring(0, strCORPORATETAXWT.Length - 1);

								strCORPORATETAXWTWhere = " C.Worktype_CorpTax in (" + strCORPORATETAXWT + ")  or ";

                                blnSearch = true;
                                blnCheckAndOr = false;

                                strCriteria.Append("CorpTaxWorkTypeId");
								strCriteria.Append("=");
								strCriteria.Append(strCORPORATETAXWT);
                                strCriteria.Append("|");

                                strCriteria.Append("CorpTaxWorkTypeText");
								strCriteria.Append("=");
								strCriteria.Append(strCORPORATETAXWTText);
                                strCriteria.Append("|");

                                strResultScreenCriteria.Append(strCORPORATETAXWTText);
                                strResultScreenCriteria.Append("|");
                            }
                        }

                        if (node2.Text.ToUpper() == "EPC CONSTRUCTION") /* One Level */
                        {
                            for (int i = 0; i < node2.Nodes.Count; i++)
                            {
                                if (node2.Nodes[i].Checked == true)
                                {
                                    if (string.IsNullOrEmpty(strEPCCONSTRUCTIONWT))
                                    {
                                        strEPCCONSTRUCTIONWT = "'" + node2.Nodes[i].Value + "',";
                                    }
                                    else
                                    {
                                        strEPCCONSTRUCTIONWT = strEPCCONSTRUCTIONWT + "'" + node2.Nodes[i].Value + "',";
                                    }

                                    if (string.IsNullOrEmpty(strEPCCONSTRUCTIONWTText))
                                    {
                                        strEPCCONSTRUCTIONWTText = node2.Nodes[i].Text;
                                    }
                                    else
                                    {
                                        strEPCCONSTRUCTIONWTText = strEPCCONSTRUCTIONWTText + "," + node2.Nodes[i].Text;
                                    }
                                }
                            }
							
                            if (!string.IsNullOrEmpty(strEPCCONSTRUCTIONWT))
                            {
                                strEPCCONSTRUCTIONWT = strEPCCONSTRUCTIONWT.Substring(0, strEPCCONSTRUCTIONWT.Length - 1);

                                string strEPCCONSTRUCTIONWTLeft = " LEFT JOIN tblCredentialNatureWork as ENOW ON ENOW.CredentialId = C.CredentialId ";
                                strEPCCONSTRUCTIONWTWhere = " ENOW.NatureWorkid in (" + strEPCCONSTRUCTIONWT + ")  or ";

                                strLEFT.Append(strEPCCONSTRUCTIONWTLeft);
                                blnSearch = true;
                                blnCheckAndOr = false;

                                strCriteria.Append("EPCCWorkTypeId");
								strCriteria.Append("=");
								strCriteria.Append(strEPCCONSTRUCTIONWT.Replace("'", "").Trim());
                                strCriteria.Append("|");

                                strCriteria.Append("EPCCWorkTypeText");
								strCriteria.Append("=");
								strCriteria.Append(strEPCCONSTRUCTIONWTText);
                                strCriteria.Append("|");

                                strResultScreenCriteria.Append(strEPCCONSTRUCTIONWTText);
                                strResultScreenCriteria.Append("|");
                            }
                        }

                        if (node2.Text.ToUpper() == "REAL ESTATE") /* Two Level */
                        {
                            if (node2.Nodes.Count > 0)
                            {
                                for (int i = 0; i < node2.Nodes.Count; i++)
                                {
                                    if (node2.Nodes[i].Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(strREWT))
                                        {
                                            strREWT = "'" + node2.Nodes[i].Value + "',";
                                        }
                                        else
                                        {
                                            strREWT = strREWT + "'" + node2.Nodes[i].Value + "',";
                                        }

                                        if (string.IsNullOrEmpty(strREWTText))
                                        {
                                            strREWTText = node2.Nodes[i].Text;
                                        }
                                        else
                                        {
                                            strREWTText = strREWTText + "," + node2.Nodes[i].Text;
                                        }
                                    }
                                }

                                for (int i = 0; i < node2.Nodes.Count; i++)
                                {
                                    for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
                                    {
                                        if (node2.Nodes[i].Nodes[j].Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(strRESWT))
                                            {
                                                strRESWT = "'" + node2.Nodes[i].Nodes[j].Value + "',";
                                            }
                                            else
                                            {
                                                strRESWT = strRESWT + "'" + node2.Nodes[i].Nodes[j].Value + "',";
                                            }

                                            if (string.IsNullOrEmpty(strRESWTText))
                                            {
                                                strRESWTText = node2.Nodes[i].Nodes[j].Text;
                                            }
                                            else
                                            {
                                                strRESWTText = strRESWTText + "," + node2.Nodes[i].Nodes[j].Text;
                                            }
                                        }
                                    }
                                }
								
                                if (!string.IsNullOrEmpty(strREWT))
                                {
                                    strREWT = strREWT.Substring(0, strREWT.Length - 1);

                                    string strREWTLeft = " LEFT JOIN tblCredentialWorkTypeRealEstate as WTRE ON WTRE.CredentialId = C.CredentialId ";
                                    strREWTWhere = " WTRE.Worktypeid in (" + strREWT + ") " + " or ";

                                    strLEFT.Append(strREWTLeft);
                                    blnSearch = true;
                                    blnCheckAndOr = false;

                                    strCriteria.Append("REWorkTypeId");
									strCriteria.Append("=");
									strCriteria.Append(strREWT.Replace("'", "").Trim());
                                    strCriteria.Append("|");

                                    strCriteria.Append("REWorkTypeText");
									strCriteria.Append("=");
									strCriteria.Append(strREWTText);
                                    strCriteria.Append("|");

                                    strResultScreenCriteria.Append(strREWTText);
                                    strResultScreenCriteria.Append("|");
                                }
								
                                if (!string.IsNullOrEmpty(strRESWT))
                                {
                                    strRESWT = strRESWT.Substring(0, strRESWT.Length - 1);

                                    string strRESWTLeft = " LEFT JOIN tblCredentialSubWorkTypeRE as SWTRE ON SWTRE.CredentialId = C.CredentialId ";
                                    strRESWTWhere = " SWTRE.SubWorktypeid in (" + strRESWT + ") " + " or ";

                                    strLEFT.Append(strRESWTLeft);
                                    blnSearch = true;
                                    blnCheckAndOr = false;

                                    strCriteria.Append("RESubWorkTypeId");
									strCriteria.Append("=");
									strCriteria.Append(strRESWT.Replace("'", "").Trim());
                                    strCriteria.Append("|");

                                    strCriteria.Append("RESubWorkTypeText");
									strCriteria.Append("=");
									strCriteria.Append(strRESWTText);
                                    strCriteria.Append("|");

                                    strResultScreenCriteria.Append(strRESWTText);
                                    strResultScreenCriteria.Append("|");
                                }
                            }
                        }

                        if (node2.Text.ToUpper() == "EPC ENERGY") /* One Level */
                        {
                            for (int i = 0; i < node2.Nodes.Count; i++)
                            {
                                if (node2.Nodes[i].Checked == true)
                                {
                                    if (string.IsNullOrEmpty(strEPCENERGYWT))
                                    {
                                        strEPCENERGYWT = "'" + node2.Nodes[i].Value + "',";
                                    }
                                    else
                                    {
                                        strEPCENERGYWT = strEPCENERGYWT + "'" + node2.Nodes[i].Value + "',";
                                    }

                                    if (string.IsNullOrEmpty(strEPCENERGYWTText))
                                    {
                                        strEPCENERGYWTText = node2.Nodes[i].Text;
                                    }
                                    else
                                    {
                                        strEPCENERGYWTText = strEPCENERGYWTText + "," + node2.Nodes[i].Text;
                                    }
                                }
                            }
							
                            if (!string.IsNullOrEmpty(strEPCENERGYWT))
                            {
                                strEPCENERGYWT = strEPCENERGYWT.Substring(0, strEPCENERGYWT.Length - 1);

                                string strEPCENERGYWTLeft = " LEFT JOIN tblCredentialTransactionType as TT ON TT.CredentialId = C.CredentialId ";
                                strEPCENERGYWTWhere = " TT.TransactionTypeid in (" + strEPCENERGYWT + ") " + " or ";

                                strLEFT.Append(strEPCENERGYWTLeft);
                                blnSearch = true;
                                blnCheckAndOr = false;

                                strCriteria.Append("EPCEWorkTypeId");
								strCriteria.Append("=");
								strCriteria.Append(strEPCENERGYWT.Replace("'", "").Trim());
                                strCriteria.Append("|");

                                strCriteria.Append("EPCEWorkTypeText");
								strCriteria.Append("=");
								strCriteria.Append(strEPCENERGYWTText);
                                strCriteria.Append("|");

                                strResultScreenCriteria.Append(strEPCENERGYWTText);
                                strResultScreenCriteria.Append("|");
                            }
                        }

                        if (node2.Text.ToUpper() == "HUMAN CAPITAL") /* Two Level */
                        {
                            if (node2.Nodes.Count > 0)
                            {
                                for (int i = 0; i < node2.Nodes.Count; i++)
                                {
                                    if (node2.Nodes[i].Checked == true)
                                    {
                                        if (string.IsNullOrEmpty(strHCWT))
                                        {
                                            strHCWT = "'" + node2.Nodes[i].Value + "',";
                                        }
                                        else
                                        {
                                            strHCWT = strHCWT + "'" + node2.Nodes[i].Value + "',";
                                        }

                                        if (string.IsNullOrEmpty(strHCWTText))
                                        {
                                            strHCWTText = node2.Nodes[i].Text;
                                        }
                                        else
                                        {
                                            strHCWTText = strHCWTText + "," + node2.Nodes[i].Text;
                                        }
                                    }
                                }

                                for (int i = 0; i < node2.Nodes.Count; i++)
                                {
                                    for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
                                    {
                                        if (node2.Nodes[i].Nodes[j].Checked == true)
                                        {
                                            if (string.IsNullOrEmpty(strHCSWT))
                                            {
                                                strHCSWT = "'" + node2.Nodes[i].Nodes[j].Value + "',";
                                            }
                                            else
                                            {
                                                strHCSWT = strHCSWT + "'" + node2.Nodes[i].Nodes[j].Value + "',";
                                            }

                                            if (string.IsNullOrEmpty(strHCSWTText))
                                            {
                                                strHCSWTText = node2.Nodes[i].Nodes[j].Text;
                                            }
                                            else
                                            {
                                                strHCSWTText = strHCSWTText + "," + node2.Nodes[i].Nodes[j].Text;
                                            }
                                        }
                                    }
                                }
								
                                if (!string.IsNullOrEmpty(strHCWT))
                                {
                                    strHCWT = strHCWT.Substring(0, strHCWT.Length - 1);

                                    string strHCWTLeft = " LEFT JOIN tblCredentialWorkTypeHC as WTHC ON WTHC.CredentialId = C.CredentialId ";
                                    strHCWTWhere = " WTHC.Worktypeid in (" + strHCWT + ") " + " or ";

                                    strLEFT.Append(strHCWTLeft);
                                    blnSearch = true;
                                    blnCheckAndOr = false;

                                    strCriteria.Append("HCWorkTypeId");
									strCriteria.Append("=");
									strCriteria.Append(strHCWT.Replace("'", "").Trim());
                                    strCriteria.Append("|");

                                    strCriteria.Append("HCWorkTypeText");
									strCriteria.Append("=");
									strCriteria.Append(strHCWTText);
                                    strCriteria.Append("|");

                                    strResultScreenCriteria.Append(strHCWTText);
                                    strResultScreenCriteria.Append("|");
                                }
								
                                if (!string.IsNullOrEmpty(strHCSWT))
                                {
                                    strHCSWT = strHCSWT.Substring(0, strHCSWT.Length - 1);

                                    string strHCSWTLeft = " LEFT JOIN tblCredentialSubWorkTypeHC as SWTHC ON SWTHC.CredentialId = C.CredentialId ";
                                    strHCSWTWhere = " SWTHC.SubWorktypeid in (" + strHCSWT + ") " + " or ";

                                    strLEFT.Append(strHCSWTLeft);
                                    blnSearch = true;
                                    blnCheckAndOr = false;

                                    strCriteria.Append("HCSubWorkTypeId");
									strCriteria.Append("=");
									strCriteria.Append(strHCSWT.Replace("'", "").Trim());
                                    strCriteria.Append("|");

                                    strCriteria.Append("HCSubWorkTypeText");
									strCriteria.Append("=");
									strCriteria.Append(strHCSWTText);
                                    strCriteria.Append("|");

                                    strResultScreenCriteria.Append(strHCSWTText);
                                    strResultScreenCriteria.Append("|");
                                }
                            }
                        }
                    }
				}
				
				StringBuilder sbWT = new StringBuilder();
				sbWT.Append("(");
				if (!string.IsNullOrEmpty(strBAIFWTWhere))
				{
					sbWT.Append(strBAIFWTWhere);
				}
				if (!string.IsNullOrEmpty(strCORPORATESWTWhere))
				{
					sbWT.Append(strCORPORATESWTWhere);
				}
				if (!string.IsNullOrEmpty(strCORPWTWhere))
				{
					sbWT.Append(strCORPWTWhere);
				}
				if (!string.IsNullOrEmpty(strCORPORATETAXWTWhere))
				{
					sbWT.Append(strCORPORATETAXWTWhere);
				}
				if (!string.IsNullOrEmpty(strCRDWTWhere))
				{
					sbWT.Append(strCRDWTWhere);
				}
				if (!string.IsNullOrEmpty(strEPCCONSTRUCTIONWTWhere))
				{
					sbWT.Append(strEPCCONSTRUCTIONWTWhere);
				}
				if (!string.IsNullOrEmpty(strCRDSWTWhere))
				{
					sbWT.Append(strCRDSWTWhere);
				}
				if (!string.IsNullOrEmpty(strEPCENERGYWTWhere))
				{
					sbWT.Append(strEPCENERGYWTWhere);
				}
				if (!string.IsNullOrEmpty(strHCSWTWhere))
				{
					sbWT.Append(strHCSWTWhere);
				}
				if (!string.IsNullOrEmpty(strHCWTWhere))
				{
					sbWT.Append(strHCWTWhere);
				}
				if (!string.IsNullOrEmpty(strRESWTWhere))
				{
					sbWT.Append(strRESWTWhere);
				}
				if (!string.IsNullOrEmpty(strREWTWhere))
				{
					sbWT.Append(strREWTWhere);
				}
				if (sbWT.ToString().Length > 1)
				{
					string wt = sbWT.ToString().Substring(0, sbWT.ToString().Length - 5);
					wt += ")  or ";
					strWHERECondition.Append(wt);
					blnSearch = true;
					blnCheckAndOr = false;
				}
				
                /* Date Completed */
				if (!cld_Tab_Date_Opened.IsEmpty && !cld_Tab_Date_Opened1.IsEmpty)
				{
					string strDate = " C.Date_Completed >= convert(datetime,'" + cld_Tab_Date_Opened.DateInput.DisplayText.Trim() + "',103) and ";
					string strDate2 = " C.Date_Completed <= convert(datetime,'" + cld_Tab_Date_Opened1.DateInput.DisplayText.Trim() + "',103)";
					string strDate3 = strDate + strDate2 + " and ";

                    strWHERECondition.Append(strDate3);
                    blnSearch = true;
                    blnCheckAndOr = false;

					strCriteria.Append("DateCompletedFrom");
					strCriteria.Append("=");
					strCriteria.Append(cld_Tab_Date_Opened.DateInput.DisplayText.Trim());
					strCriteria.Append("|");

					strCriteria.Append("DateCompletedTo");
					strCriteria.Append("=");
					strCriteria.Append(cld_Tab_Date_Opened1.DateInput.DisplayText.Trim());
					strCriteria.Append("|");

					strResultScreenCriteria.Append(cld_Tab_Date_Opened.DateInput.DisplayText.Trim());
					strResultScreenCriteria.Append("|");

					strResultScreenCriteria.Append(cld_Tab_Date_Opened1.DateInput.DisplayText.Trim());
					strResultScreenCriteria.Append("|");
				}
				else if (!cld_Tab_Date_Opened.IsEmpty)
				{
					string strDate3 = " C.Date_Completed >= convert(datetime,'" + cld_Tab_Date_Opened.DateInput.DisplayText.Trim() + "',103) and ";
					strWHERECondition.Append(strDate3);
					blnSearch = true;
					blnCheckAndOr = false;
					strCriteria.Append("DateCompletedFrom");
					strCriteria.Append("=");
					strCriteria.Append(cld_Tab_Date_Opened.DateInput.DisplayText.Trim());
					strCriteria.Append("|");
					strResultScreenCriteria.Append(cld_Tab_Date_Opened.DateInput.DisplayText.Trim());
					strResultScreenCriteria.Append("|");
				}
				else if (!cld_Tab_Date_Opened1.IsEmpty)
				{
					string strDate3 = " C.Date_Completed <= convert(datetime,'" + cld_Tab_Date_Opened1.DateInput.DisplayText.Trim() + "',103) and ";
					strWHERECondition.Append(strDate3);
					blnSearch = true;
					blnCheckAndOr = false;
					strCriteria.Append("DateCompletedTo");
					strCriteria.Append("=");
					strCriteria.Append(cld_Tab_Date_Opened1.DateInput.DisplayText.Trim());
					strCriteria.Append("|");
					strResultScreenCriteria.Append(cld_Tab_Date_Opened1.DateInput.DisplayText.Trim());
					strResultScreenCriteria.Append("|");
				}

				if (chk_Tab_ActualDate_Ongoing.Checked)
				{
					if (!cld_Tab_Date_Opened.IsEmpty || !cld_Tab_Date_Opened1.IsEmpty)
					{
						strWHERECondition.Remove(strWHERECondition.Length - 5, 5);
						strWHERECondition.Append(" or ");
					}
					string strDateOngoing = " C.ActualDate_Ongoing = '" + chk_Tab_ActualDate_Ongoing.Text.Trim() + "' and ";
					strWHERECondition.Append(strDateOngoing);
					blnSearch = true;
					blnCheckAndOr = false;
					strCriteria.Append(chk_Tab_ActualDate_Ongoing.ID);
					strCriteria.Append("=");
					strCriteria.Append(chk_Tab_ActualDate_Ongoing.Text.Trim());
					strCriteria.Append("|");
					strResultScreenCriteria.Append(chk_Tab_ActualDate_Ongoing.Text.Trim());
					strResultScreenCriteria.Append("|");
				}
				
				if (chk_Tab_ActualDate_NotKnown.Checked)
				{
					if (!cld_Tab_Date_Opened.IsEmpty || !cld_Tab_Date_Opened1.IsEmpty)
					{
						strWHERECondition.Remove(strWHERECondition.Length - 5, 5);
						strWHERECondition.Append(" or ");
					}
					string strDateNotKnown = " C.ActualDate_Ongoing = '" + chk_Tab_ActualDate_NotKnown.Text.Trim() + "' and ";
					strWHERECondition.Append(strDateNotKnown);
					blnSearch = true;
					blnCheckAndOr = false;
					strCriteria.Append("chk_Tab_ActualDate_NotKnown");
					strCriteria.Append("=");
					strCriteria.Append(chk_Tab_ActualDate_NotKnown.Text.Trim());
					strCriteria.Append("|");
					strResultScreenCriteria.Append(chk_Tab_ActualDate_NotKnown.Text.Trim());
					strResultScreenCriteria.Append("|");
				}
				
                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestML)))
                {
                    string strCountryLeft = " LEFT JOIN tblCredentialCountryMatterClose as CMC ON CMC.CredentialId = C.CredentialId ";
                    string strCountryWhere = " CMC.CountryId in (" + getCheckedItems(radlstDestML) + ") " + " and ";

                    strLEFT.Append(strCountryLeft);
                    strWHERECondition.Append(strCountryWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Country_Matter_Close");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestML));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Country_Matter_Close" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestML));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestML));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestTeams)))
                {
                    string strTeamLeft = " LEFT JOIN tblCredentialTeam as TM ON TM.CredentialId = C.CredentialId ";
                    string strTeamWhere = " TM.TeamId in (" + getCheckedItems(radlstDestTeams) + ") " + " and ";

                    strLEFT.Append(strTeamLeft);
                    strWHERECondition.Append(strTeamWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Team");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestTeams));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Team" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestTeams));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestTeams));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestLP)))
                {
                    string strLeadPartnerLeft = " LEFT JOIN tblCredentialLeadPartner as LP ON LP.CredentialId = C.CredentialId ";
                    string strLeadPartnerWhere = " LP.LeadPartnerId in (" + getCheckedItems(radlstDestLP) + ") " + " and ";

                    strLEFT.Append(strLeadPartnerLeft);
                    strWHERECondition.Append(strLeadPartnerWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Lead_Partner");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestLP));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Lead_Partner" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestLP));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestLP));
                    strResultScreenCriteria.Append("|");
                }

                /* Practice Group */
                string strBAIFPGID = string.Empty;
                string strBAIFPGText = string.Empty;
                string strCORPPGID = string.Empty;
                string strCORPPGText = string.Empty;
                string strCRDPGID = string.Empty;
                string strCRDPGText = string.Empty;
                string strEPCCPGID = string.Empty;
                string strEPCCPGText = string.Empty;
                string strEPCPPGID = string.Empty;
                string strEPCPPGText = string.Empty;
                string strEPCEPGID = string.Empty;
                string strEPCEPGText = string.Empty;
                string strHCPGID = string.Empty;
                string strHCPGText = string.Empty;
                string strREPGID = string.Empty;
                string strREPGText = string.Empty;

				StringBuilder sbPG = new StringBuilder();
				foreach (RadTreeNode node in RadTreeView2.Nodes)
				{
					foreach (RadTreeNode node2 in node.Nodes)
					{
						if (node2.Text.ToUpper() == "CORPORATE")
						{
							if (!node2.Checked)
							{
								for (int i = 0; i < node2.Nodes.Count; i++)
								{
									string strParent = string.Empty;
									strCORPPGID = string.Empty;
									strCORPPGText = string.Empty;
									for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
									{
										if (node2.Nodes[i].Nodes[j].Checked)
										{
											strParent = node2.Nodes[i].Value;
											if (string.IsNullOrEmpty(strCORPPGID))
											{
												strCORPPGID = node2.Nodes[i].Nodes[j].Value;
												strCORPPGText = node2.Nodes[i].Nodes[j].Text;
											}
											else
											{
												strCORPPGID = strCORPPGID + "," + node2.Nodes[i].Nodes[j].Value;
												strCORPPGText = strCORPPGText + "," + node2.Nodes[i].Nodes[j].Text;
											}
										}
									}
									if (!string.IsNullOrEmpty(strCORPPGID.ToString()))
									{
										if (node2.Nodes[i].Text.Trim().ToUpper() == "COUNTRY OF BUYER" || 
											node2.Nodes[i].Text.Trim().ToUpper() == "COUNTRY OF SELLER" || 
											node2.Nodes[i].Text.Trim().ToUpper() == "COUNTRY OF TARGET" || 
											node2.Nodes[i].Text.Trim().ToUpper() == "ACTING FOR")
										{
											string strAlias = "CORP" + i;
											string strPGLeft = " LEFT JOIN " + strParent.Split('~')[1] + " as " + strAlias + " ON " + strAlias + ".CredentialId = C.CredentialId ";
											string strPGWhereCORPORATE = strAlias + "." + strParent.Split('~')[0] + " in (" + strCORPPGID + ")  and ";
											strLEFT.Append(strPGLeft);
											sbPG.Append(strPGWhereCORPORATE);
											blnSearch = true;
											blnCheckAndOr = false;
											strCriteria.Append(node2.Nodes[i].FullPath);
											strCriteria.Append("=");
											strCriteria.Append(strCORPPGID);
											strCriteria.Append("|");
											strCriteria.Append(node2.Nodes[i].Text);
											strCriteria.Append("=");
											strCriteria.Append(strCORPPGText);
											strCriteria.Append("|");

											strResultScreenCriteria.Append(strCORPPGText);
											strResultScreenCriteria.Append("|");
										}
										else
										{
											string strCORPPGWhere = "C." + strParent.Split('~')[0] + " in(" + strCORPPGID + ") and ";

											sbPG.Append(strCORPPGWhere);
											blnSearch = true;
											blnCheckAndOr = false;
											strCriteria.Append(node2.Nodes[i].Text + "ID");
											strCriteria.Append("=");
											strCriteria.Append(strCORPPGID);
											strCriteria.Append("|");
											strCriteria.Append(node2.Nodes[i].Text);
											strCriteria.Append("=");
											strCriteria.Append(strCORPPGText);
											strCriteria.Append("|");

											strResultScreenCriteria.Append(strCORPPGText);
											strResultScreenCriteria.Append("|");
										}
									}
								}
							}
							else
							{
								string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGCORPORATE ON BGCORPORATE.CredentialId = C.CredentialId ";
								string strPracWhereCORPORATE = " BGCORPORATE.BusinessGroupId in ('3')  ~!@ ";
								strLEFT.Append(strPracLeft);
								sbPG.Append(strPracWhereCORPORATE);
								blnSearch = true;
								blnCheckAndOr = false;
								strCriteria.Append("PracticeGroupId");
								strCriteria.Append("=3");
								strCriteria.Append("|");
								strCriteria.Append("CorporatePGText");
								strCriteria.Append("=CORPORATE");
								strCriteria.Append("|");
								strResultScreenCriteria.Append("Corporate");
								strResultScreenCriteria.Append("|");
							}
						}
						
						if (node2.Text.ToUpper() == "BAIF")
						{
							if (!node2.Checked)
							{
								for (int i = 0; i < node2.Nodes.Count; i++)
								{
									string strParent = string.Empty;
									strBAIFPGID = string.Empty;
									strBAIFPGText = string.Empty;
									for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
									{
										if (node2.Nodes[i].Nodes[j].Checked)
										{
											strParent = node2.Nodes[i].Value;
											if (string.IsNullOrEmpty(strBAIFPGID))
											{
												strBAIFPGID = node2.Nodes[i].Nodes[j].Value;
												strBAIFPGText = node2.Nodes[i].Nodes[j].Text;
											}
											else
											{
												strBAIFPGID = strBAIFPGID + "," + node2.Nodes[i].Nodes[j].Value;
												strBAIFPGText = strBAIFPGText + "," + node2.Nodes[i].Nodes[j].Text;
											}
										}
									}
									if (!string.IsNullOrEmpty(strBAIFPGID.ToString()))
									{
										string strBAIFPGWhere = "C." + strParent.Split('~')[0] + " in(" + strBAIFPGID + ") and ";
										sbPG.Append(strBAIFPGWhere);
										blnSearch = true;
										blnCheckAndOr = false;
										
										strCriteria.Append(node2.Nodes[i].FullPath);
										strCriteria.Append("=");
										strCriteria.Append(strBAIFPGID);
										strCriteria.Append("|");
										
										strCriteria.Append("BAIFClientTypeText");
										strCriteria.Append("=");
										strCriteria.Append(strBAIFPGText);
										strCriteria.Append("|");

										strResultScreenCriteria.Append(strBAIFPGText);
										strResultScreenCriteria.Append("|");
									}
								}
							}
							else
							{
								string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGBAIF ON BGBAIF.CredentialId = C.CredentialId ";
								string strPracWhereBAIF = " BGBAIF.BusinessGroupId in ('1')  ~!@ ";
								
								strLEFT.Append(strPracLeft);
								sbPG.Append(strPracWhereBAIF);
								blnSearch = true;
								blnCheckAndOr = false;
								
								strCriteria.Append("PracticeGroupId");
								strCriteria.Append("=2");
								strCriteria.Append("|");
								
								strCriteria.Append("BAIFPGText");
								strCriteria.Append("=BAIF");
								strCriteria.Append("|");
								
								strResultScreenCriteria.Append("BAIF");
								strResultScreenCriteria.Append("|");
							}
						}
						if (node2.Text.ToUpper() == "CRD")
						{
							if (!node2.Checked)
							{
								for (int i = 0; i < node2.Nodes.Count; i++)
								{
									string strParent = string.Empty;
									strCRDPGID = string.Empty;
									strCRDPGText = string.Empty;
									for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
									{
										if (node2.Nodes[i].Nodes[j].Checked)
										{
											strParent = node2.Nodes[i].Value;

											if (string.IsNullOrEmpty(strCRDPGID))
											{
												strCRDPGID = node2.Nodes[i].Nodes[j].Value;
												strCRDPGText = node2.Nodes[i].Nodes[j].Text;
											}
											else
											{
												strCRDPGID = strCRDPGID + "," + node2.Nodes[i].Nodes[j].Value;
												strCRDPGText = strCRDPGText + "," + node2.Nodes[i].Nodes[j].Text;
											}
										}
									}

									if (!string.IsNullOrEmpty(strCRDPGID.ToString()))
									{
										string strCRDPGWhere = "C." + strParent.Split('~')[0] + " in(" + strCRDPGID + ")  and ";
										sbPG.Append(strCRDPGWhere);
										blnSearch = true;
										blnCheckAndOr = false;
										
										strCriteria.Append("CRDClientTypeId");
										strCriteria.Append("=");
										strCriteria.Append(strCRDPGID);
										strCriteria.Append("|");
										
										strCriteria.Append("CRDClientTypeText");
										strCriteria.Append("=");
										strCriteria.Append(strCRDPGText);
										strCriteria.Append("|");

										strResultScreenCriteria.Append(strCRDPGText);
										strResultScreenCriteria.Append("|");
									}
								}
							}
							else
							{
								string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGCRD ON BGCRD.CredentialId = C.CredentialId ";
								string strPracWhereCRD = " BGCRD.BusinessGroupId in ('4')  ~!@ ";
								strLEFT.Append(strPracLeft);
								sbPG.Append(strPracWhereCRD);
								blnSearch = true;
								blnCheckAndOr = false;
								strCriteria.Append("PracticeGroupId");
								strCriteria.Append("=4");
								strCriteria.Append("|");
								strCriteria.Append("CRDPGText");
								strCriteria.Append("=CRD");
								strCriteria.Append("|");
								strResultScreenCriteria.Append("CRD");
								strResultScreenCriteria.Append("|");
							}
						}

						if (node2.Text.ToUpper() == "EPC CONSTRUCTION")
						{
							if (!node2.Checked)
							{
								for (int i = 0; i < node2.Nodes.Count; i++)
								{
									string strParent = string.Empty;
									strEPCCPGID = string.Empty;
									strEPCCPGText = string.Empty;
									
									for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
									{
										if (node2.Nodes[i].Nodes[j].Checked)
										{
											strParent = node2.Nodes[i].Value;

											if (string.IsNullOrEmpty(strEPCCPGID))
											{
												strEPCCPGID = node2.Nodes[i].Nodes[j].Value;
												strEPCCPGText = node2.Nodes[i].Nodes[j].Text;
											}
											else
											{
												strEPCCPGID = strEPCCPGID + "," + node2.Nodes[i].Nodes[j].Value;
												strEPCCPGText = strEPCCPGText + "," + node2.Nodes[i].Nodes[j].Text;
											}
										}
									}

									if (!string.IsNullOrEmpty(strEPCCPGID.ToString()))
									{
										if (node2.Nodes[i].Text.Trim().ToUpper() == "TYPE OF CONTRACT")
										{
											string strAlias = "EPCC" + i;
											string strPGLeft = " LEFT JOIN " + strParent.Split('~')[1] + " as " + strAlias + " ON " + strAlias + ".CredentialId = C.CredentialId ";

											string strPGWhere = strAlias + "." + strParent.Split('~')[0] + " in (" + strEPCCPGID + ")  and ";
											strLEFT.Append(strPGLeft);
											sbPG.Append(strPGWhere);
											blnSearch = true;
											blnCheckAndOr = false;
											
											strCriteria.Append(node2.Nodes[i].Text + "ID");
											strCriteria.Append("=");
											strCriteria.Append(strEPCCPGID);
											strCriteria.Append("|");
											
											strCriteria.Append(node2.Nodes[i].Text);
											strCriteria.Append("=");
											strCriteria.Append(strEPCCPGText);
											strCriteria.Append("|");

											strResultScreenCriteria.Append(strEPCCPGText);
											strResultScreenCriteria.Append("|");
										}
										else
										{
											string strCORPPGWhere = "C." + strParent.Split('~')[0] + " in(" + strEPCCPGID + ")  and ";
											sbPG.Append(strCORPPGWhere);
											blnSearch = true;
											blnCheckAndOr = false;
											
											strCriteria.Append(node2.Nodes[i].Text + "ID");
											strCriteria.Append("=");
											strCriteria.Append(strEPCCPGID);
											strCriteria.Append("|");
											
											strCriteria.Append(node2.Nodes[i].Text);
											strCriteria.Append("=");
											strCriteria.Append(strEPCCPGText);
											strCriteria.Append("|");

											strResultScreenCriteria.Append(strEPCCPGText);
											strResultScreenCriteria.Append("|");
										}
									}
								}
							}
							else
							{
								string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGEPCC ON BGEPCC.CredentialId = C.CredentialId ";
								string strPracWhereEPCC = " BGEPCC.BusinessGroupId in ('5')  ~!@ ";
								
								strLEFT.Append(strPracLeft);
								sbPG.Append(strPracWhereEPCC);
								blnSearch = true;
								blnCheckAndOr = false;
								
								strCriteria.Append("PracticeGroupId");
								strCriteria.Append("=5");
								strCriteria.Append("|");
								
								strCriteria.Append("EPCConstPGText");
								strCriteria.Append("=EPC CONSTRUCTION");
								strCriteria.Append("|");
								
								strResultScreenCriteria.Append("EPC Construction");
								strResultScreenCriteria.Append("|");
							}
						}
						if (node2.Text.ToUpper() == "EPC PROJECTS")
						{
							if (!node2.Checked)
							{
								for (int i = 0; i < node2.Nodes.Count; i++)
								{
									string strParent = string.Empty;
									strEPCPPGID = string.Empty;
									strEPCPPGText = string.Empty;
									for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
									{
										if (node2.Nodes[i].Nodes[j].Checked)
										{
											strParent = node2.Nodes[i].Value;

											if (string.IsNullOrEmpty(strEPCPPGID))
											{
												strEPCPPGID = node2.Nodes[i].Nodes[j].Value;
												strEPCPPGText = node2.Nodes[i].Nodes[j].Text;
											}
											else
											{
												strEPCPPGID = strEPCPPGID + "," + node2.Nodes[i].Nodes[j].Value;
												strEPCPPGText = strEPCPPGText + "," + node2.Nodes[i].Nodes[j].Text;
											}
										}
									}

									if (!string.IsNullOrEmpty(strEPCPPGID.ToString()))
									{
										string strBAIFPGWhere = "C." + strParent.Split('~')[0] + " in(" + strEPCPPGID + ")  and ";

										sbPG.Append(strBAIFPGWhere);
										blnSearch = true;
										blnCheckAndOr = false;
										
										strCriteria.Append("EPCProjectsClientTypeId");
										strCriteria.Append("=");
										strCriteria.Append(strEPCPPGID);
										strCriteria.Append("|");
										
										strCriteria.Append("EPCProjectsClientTypeText");
										strCriteria.Append("=");
										strCriteria.Append(strEPCPPGText);
										strCriteria.Append("|");

										strResultScreenCriteria.Append(strEPCPPGText);
										strResultScreenCriteria.Append("|");
									}
								}
							}
							else
							{
								string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGEPCP ON BGEPCP.CredentialId = C.CredentialId ";
								string strPracWhereEPCP = " BGEPCP.BusinessGroupId in ('8')  ~!@ ";
								
								strLEFT.Append(strPracLeft);
								sbPG.Append(strPracWhereEPCP);
								blnSearch = true;
								blnCheckAndOr = false;
								
								strCriteria.Append("PracticeGroupId");
								strCriteria.Append("=8");
								strCriteria.Append("|");
								
								strCriteria.Append("EPCProjectsPGText");
								strCriteria.Append("=EPC PROJECTS");
								strCriteria.Append("|");
								
								strResultScreenCriteria.Append("EPC Projects");
								strResultScreenCriteria.Append("|");
							}
						}
						if (node2.Text.ToUpper() == "EPC ENERGY")
						{
							if (!node2.Checked)
							{
								for (int i = 0; i < node2.Nodes.Count; i++)
								{
									string strParent = string.Empty;
									strEPCEPGID = string.Empty;
									strEPCEPGText = string.Empty;
									for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
									{
										if (node2.Nodes[i].Nodes[j].Checked)
										{
											strParent = node2.Nodes[i].Value;

											if (string.IsNullOrEmpty(strEPCEPGID))
											{
												strEPCEPGID = node2.Nodes[i].Nodes[j].Value;
												strEPCEPGText = node2.Nodes[i].Nodes[j].Text;
											}
											else
											{
												strEPCEPGID = strEPCEPGID + "," + node2.Nodes[i].Nodes[j].Value;
												strEPCEPGText = strEPCEPGText + "," + node2.Nodes[i].Nodes[j].Text;
											}
										}
									}

									if (!string.IsNullOrEmpty(strEPCEPGID.ToString()))
									{
										string strBAIFPGWhere = "C." + strParent.Split('~')[0] + " in(" + strEPCEPGID + ") and ";

										sbPG.Append(strBAIFPGWhere);
										blnSearch = true;
										blnCheckAndOr = false;
										
										strCriteria.Append("EPCEContractTypeId");
										strCriteria.Append("=");
										strCriteria.Append(strEPCEPGID);
										strCriteria.Append("|");
										
										strCriteria.Append("EPCEContractTypeText");
										strCriteria.Append("=");
										strCriteria.Append(strEPCEPGText);
										strCriteria.Append("|");

										strResultScreenCriteria.Append(strEPCEPGText);
										strResultScreenCriteria.Append("|");
									}
								}
							}
							else
							{
								string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGEPCE ON BGEPCE.CredentialId = C.CredentialId ";
								string strPracWhereEPCE = " BGEPCE.BusinessGroupId in ('9')  ~!@ ";
								
								strLEFT.Append(strPracLeft);
								sbPG.Append(strPracWhereEPCE);
								blnSearch = true;
								blnCheckAndOr = false;
								
								strCriteria.Append("PracticeGroupId");
								strCriteria.Append("=9");
								strCriteria.Append("|");
								
								strCriteria.Append("EPCEnergyPGText");
								strCriteria.Append("=EPC ENERGY");
								strCriteria.Append("|");
								
								strResultScreenCriteria.Append("EPC Energy");
								strResultScreenCriteria.Append("|");
							}
						}

						if (node2.Text.ToUpper() == "HUMAN CAPITAL")
						{
							if (!node2.Checked)
							{
								for (int i = 0; i < node2.Nodes.Count; i++)
								{
									string strParent = string.Empty;
									strHCPGID = string.Empty;
									strHCPGText = string.Empty;
									
									for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
									{
										if (node2.Nodes[i].Nodes[j].Checked)
										{
											strParent = node2.Nodes[i].Value;

											if (string.IsNullOrEmpty(strHCPGID))
											{
												strHCPGID = node2.Nodes[i].Nodes[j].Value;
												strHCPGText = node2.Nodes[i].Nodes[j].Text;
											}
											else
											{
												strHCPGID = strHCPGID + "," + node2.Nodes[i].Nodes[j].Value;
												strHCPGText = strHCPGText + "," + node2.Nodes[i].Nodes[j].Text;
											}
										}
									}

									if (!string.IsNullOrEmpty(strHCPGID.ToString()))
									{
										string strBAIFPGWhere = "C." + strParent.Split('~')[0] + " in(" + strHCPGID + ") and ";
										sbPG.Append(strBAIFPGWhere);
										blnSearch = true;
										blnCheckAndOr = false;
										
										strCriteria.Append("HCPensionSchemeId");
										strCriteria.Append("=");
										strCriteria.Append(strHCPGID);
										strCriteria.Append("|");
										
										strCriteria.Append("HCPensionSchemeText");
										strCriteria.Append("=");
										strCriteria.Append(strHCPGText);
										strCriteria.Append("|");

										strResultScreenCriteria.Append(strHCPGText);
										strResultScreenCriteria.Append("|");
									}
								}
							}
							else
							{
								string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGHC ON BGHC.CredentialId = C.CredentialId ";
								string strPracWhereHC = " BGHC.BusinessGroupId in ('10')  ~!@ ";
								
								strLEFT.Append(strPracLeft);
								sbPG.Append(strPracWhereHC);
								blnSearch = true;
								blnCheckAndOr = false;
								
								strCriteria.Append("PracticeGroupId");
								strCriteria.Append("=10");
								strCriteria.Append("|");
								
								strCriteria.Append("HCPGText");
								strCriteria.Append("=HUMAN CAPITAL");
								strCriteria.Append("|");
								
								strResultScreenCriteria.Append("Human Capital");
								strResultScreenCriteria.Append("|");
							}
						}
						
						if (node2.Text.ToUpper() == "CORPORATE TAX" && node2.Checked)
						{
							string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGCT ON BGCT.CredentialId = C.CredentialId ";
							string strPracWhereCT = " BGCT.BusinessGroupId in ('11')  ~!@ ";
							
							strLEFT.Append(strPracLeft);
							sbPG.Append(strPracWhereCT);
							blnSearch = true;
							blnCheckAndOr = false;
							
							strCriteria.Append("PracticeGroupId");
							strCriteria.Append("=11");
							strCriteria.Append("|");
							
							strCriteria.Append("CorporateTaxPGText");
							strCriteria.Append("=CORPORATE TAX");
							strCriteria.Append("|");
							
							strResultScreenCriteria.Append("Corporate Tax");
							strResultScreenCriteria.Append("|");
						}
						if (node2.Text.ToUpper() == "REAL ESTATE")
						{
							if (!node2.Checked)
							{
								for (int i = 0; i < node2.Nodes.Count; i++)
								{
									string strParent = string.Empty;
									strREPGID = string.Empty;
									strREPGText = string.Empty;
									
									for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
									{
										if (node2.Nodes[i].Nodes[j].Checked)
										{
											strParent = node2.Nodes[i].Value;

											if (string.IsNullOrEmpty(strREPGID))
											{
												strREPGID = node2.Nodes[i].Nodes[j].Value;
												strREPGText = node2.Nodes[i].Nodes[j].Text;
											}
											else
											{
												strREPGID = strREPGID + "," + node2.Nodes[i].Nodes[j].Value;
												strREPGText = strREPGText + "," + node2.Nodes[i].Nodes[j].Text;
											}
										}
									}

									if (!string.IsNullOrEmpty(strREPGID.ToString()))
									{
										string strAlias = "RE" + i;
										string strPGLeft = " LEFT JOIN " + strParent.Split('~')[1] + " as " + strAlias + " ON " + strAlias + ".CredentialId = C.CredentialId ";

										string strPGWhere = strAlias + "." + strParent.Split('~')[0] + " in (" + strREPGID + ")  and ";
										
										strLEFT.Append(strPGLeft);
										sbPG.Append(strPGWhere);
										blnSearch = true;
										blnCheckAndOr = false;
										
										strCriteria.Append(node2.Nodes[i].Text + "ID");
										strCriteria.Append("=");
										strCriteria.Append(strREPGID);
										strCriteria.Append("|");
										
										strCriteria.Append(node2.Nodes[i].Text);
										strCriteria.Append("=");
										strCriteria.Append(strREPGText);
										strCriteria.Append("|");

										strResultScreenCriteria.Append(strREPGText);
										strResultScreenCriteria.Append("|");
									}
								}
							}
							else
							{
								string strPracLeft = " LEFT JOIN tblCredentialBusinessGroup as BGRE ON BGRE.CredentialId = C.CredentialId ";
								string strPracWhereRE = " BGRE.BusinessGroupId in ('7')  ~!@ ";
								
								strLEFT.Append(strPracLeft);
								sbPG.Append(strPracWhereRE);
								blnSearch = true;
								blnCheckAndOr = false;
								
								strCriteria.Append("PracticeGroupId");
								strCriteria.Append("=7");
								strCriteria.Append("|");
								
								strCriteria.Append("REPGText");
								strCriteria.Append("=REAL ESTATE");
								strCriteria.Append("|");
								
								strResultScreenCriteria.Append("Real Estate");
								strResultScreenCriteria.Append("|");
							}
						}
					}
				}
				
				if (!string.IsNullOrEmpty(sbPG.ToString()))
				{
					string strPG = "(";
					strPG += sbPG.ToString().Substring(0, sbPG.ToString().Length - 6);
					
					strPG += ")  ~!@ ";
					strWHERECondition.Append(strPG);
					
					blnSearch = true;
					blnCheckAndOr = false;
				}

                if (!string.IsNullOrEmpty(getCheckedItems(rad_Tab_Priority)))
                {
                    string strPriorityWhere = " C.Priority in (" + getCheckedItems(rad_Tab_Priority) + ") " + " and ";

                    strWHERECondition.Append(strPriorityWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(rad_Tab_Priority.ID);
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(rad_Tab_Priority));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(rad_Tab_Priority.SelectedItem.Text);
                    strResultScreenCriteria.Append("|");
                }


                /* Basic Query Ends */
                /* Advanced Query Starts */
                if (!string.IsNullOrEmpty(txt_Tab_ProjectName_Core.Text.Trim()))
                {
                    string strProjectWhere = " C.ProjectName_Core like N'%" + txt_Tab_ProjectName_Core.Text.Trim().Replace("'", "''") + "%'" + " and ";

                    strWHERECondition.Append(strProjectWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(txt_Tab_ProjectName_Core.ID);
					strCriteria.Append("=");
					strCriteria.Append(txt_Tab_ProjectName_Core.Text.Trim());
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(txt_Tab_ProjectName_Core.Text.Trim());
                    strResultScreenCriteria.Append("|");
                }
				
				if (!string.IsNullOrEmpty(txt_tab_ClientDescription.Text.Trim()))
				{
					string strProjectWhere = " C.ClientDescription like N'%" + txt_tab_ClientDescription.Text.Trim().Replace("'", "''") + "%' and ";
					strWHERECondition.Append(strProjectWhere);
					
					blnSearch = true;
					blnCheckAndOr = false;
					
					strCriteria.Append(txt_tab_ClientDescription.ID);
					strCriteria.Append("=");
					strCriteria.Append(txt_tab_ClientDescription.Text.Trim());
					strCriteria.Append("|");
					
					strResultScreenCriteria.Append(txt_tab_ClientDescription.Text.Trim());
					strResultScreenCriteria.Append("|");
				}
				
                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestME)))
                {
                    string strCountryLeft = " LEFT JOIN tblCredentialOtherMatterExecutive as OME ON OME.CredentialId = C.CredentialId ";
                    string strCountryWhere = " OME.OtherMatterExecutiveId in (" + getCheckedItems(radlstDestME) + ") " + " and ";

                    strLEFT.Append(strCountryLeft);
                    strWHERECondition.Append(strCountryWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Matter_Executive");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestME));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Matter_Executive" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestME));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestME));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(txt_tab_Value_Deal.Text.Trim()))
                {
                    string strValueWhere = " C.ValueOfDeal_Core like '%" + txt_tab_Value_Deal.Text.Trim().Replace("'", "''") + "%'" + " and ";

                    strWHERECondition.Append(strValueWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(txt_tab_Value_Deal.ID);
					strCriteria.Append("=");
					strCriteria.Append(txt_tab_Value_Deal.Text.Trim());
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(txt_tab_Value_Deal.Text.Trim());
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestCD)))
                {
                    string strCurrencyOfDealWhere = " C.Currency_Of_Deal in (" + getCheckedItems(radlstDestCD) + ") " + " and ";

                    strWHERECondition.Append(strCurrencyOfDealWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Currency_Of_Deal");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestCD));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Currency_Of_Deal" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestCD));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestCD));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestContentious)))
                {
                    string strContentiousIRGWhere = " C.Contentious_IRG in (" + getCheckedItems(radlstDestContentious) + ") " + " and ";

                    strWHERECondition.Append(strContentiousIRGWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Contentious_IRG");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestContentious));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Contentious_IRG" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestContentious));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestContentious));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestDR)))
                {
                    string strDisputeResolutionWhere = " C.Dispute_Resolution in (" + getCheckedItems(radlstDestDR) + ") " + " and ";

                    strWHERECondition.Append(strDisputeResolutionWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rdo_Tab_Dispute_Resolution");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestDR));
                    strCriteria.Append("|");

                    strCriteria.Append("rdo_Tab_Dispute_Resolution" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestDR));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestDR));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestCOA)))
                {
                    string strCountryLeft = " LEFT JOIN tblCredentialCountryArbitrationCountry as COA ON COA.CredentialId = C.CredentialId ";
                    string strCountryWhere = " COA.CountryId in (" + getCheckedItems(radlstDestCOA) + ") " + " and ";

                    strLEFT.Append(strCountryLeft);
                    strWHERECondition.Append(strCountryWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_CountryofArbitration");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestCOA));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_CountryofArbitration" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestCOA));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestCOA));
                    strResultScreenCriteria.Append("|");
                }

                /*SOA*/
                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestSOA)))
                {
                    string strArbitrationCityWhere = " C.ArbitrationCity in (" + getCheckedItems(radlstDestSOA) + ") " + " and ";

                    strWHERECondition.Append(strArbitrationCityWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_ArbitrationCity");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestSOA));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_ArbitrationCity" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestSOA));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestSOA));
                    strResultScreenCriteria.Append("|");
                }
				
                /*AR*/
                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestAR)))
                {
                    string strArbitralRulesWhere = " C.Arbitral_Rules in (" + getCheckedItems(radlstDestAR) + ") " + " and ";

                    strWHERECondition.Append(strArbitralRulesWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Arbitral_Rules");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestAR));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Arbitral_Rules" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestAR));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestAR));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(rad_Tab_InvestmentTreaty)))
                {
                    string strInvestmentTreatyWhere = " C.InvestmentTreaty in (" + getCheckedItems(rad_Tab_InvestmentTreaty) + ") " + " and ";

                    strWHERECondition.Append(strInvestmentTreatyWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(rad_Tab_InvestmentTreaty.ID);
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(rad_Tab_InvestmentTreaty));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(rad_Tab_InvestmentTreaty.SelectedItem.Text);
                    strResultScreenCriteria.Append("|");
                }

                /*ivt*/
                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestIVT)))
                {
                    string strInvestigationTypeWhere = " C.Investigation_Type in (" + getCheckedItems(radlstDestIVT) + ") " + " and ";

                    strWHERECondition.Append(strInvestigationTypeWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Investigation_Type");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestIVT));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Investigation_Type" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestIVT));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestIVT));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestLOD)))
                {
                    string strLanguageOfDisputeLeft = " LEFT JOIN tblCredentialLanguageOfDispute as LOD ON LOD.CredentialId = C.CredentialId ";
                    string strLanguageOfDisputeWhere = " LOD.LanguageOfDisputeId in (" + getCheckedItems(radlstDestLOD) + ") " + " and ";

                    strLEFT.Append(strLanguageOfDisputeLeft);
                    strWHERECondition.Append(strLanguageOfDisputeWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Language_Of_Dispute");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestLOD));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Language_Of_Dispute" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestLOD));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestLOD));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestJOD)))
                {
                    string strCountryLeft = " LEFT JOIN tblCredentialCountryJurisdiction as COJ ON COJ.CredentialId = C.CredentialId ";
                    string strCountryWhere = " COJ.CountryId in (" + getCheckedItems(radlstDestJOD) + ") " + " and ";

                    strLEFT.Append(strCountryLeft);
                    strWHERECondition.Append(strCountryWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Country_Jurisdiction");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestJOD));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Country_Jurisdiction" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestJOD));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestJOD));
                    strResultScreenCriteria.Append("|");
                }

                /*CMS firms involved*/
                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestCFI)))
                {
                    string strCountryLeft = " LEFT JOIN tblCredentialReferredFromOtherCMSOffice as CO ON CO.CredentialId = C.CredentialId ";
                    string strCountryWhere = " CO.ReferredFromOtherCMSOfficeId in (" + getCheckedItems(radlstDestCFI) + ") " + " and ";

                    strLEFT.Append(strCountryLeft);
                    strWHERECondition.Append(strCountryWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Referred_From_Other_CMS_Office");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestCFI));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Referred_From_Other_CMS_Office" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestCFI));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestCFI));
                    strResultScreenCriteria.Append("|");
                }

                /* Lead CMS firm */
                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestLCF)))
                {
                    string strLeadWhere = " C.LeadCMSFirm in (" + getCheckedItems(radlstDestLCF) + ") " + " and ";

                    strWHERECondition.Append(strLeadWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Lead_CMS_Firms");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestLCF));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Lead_CMS_Firms" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestLCF));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestLCF));
                    strResultScreenCriteria.Append("|");
                }

                /* Countries of other CMS firms */
                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestCCF)))
                {
                    string strCountryLeft = " LEFT JOIN tblCredentialCountryOtherCMSOffice as COO ON COO.CredentialId = C.CredentialId ";
                    string strCountryWhere = " COO.CountryId in (" + getCheckedItems(radlstDestCCF) + ") " + " and ";

                    strLEFT.Append(strCountryLeft);
                    strWHERECondition.Append(strCountryWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_tab_Countries_of_other_CMS_firms");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestCCF));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_tab_Countries_of_other_CMS_firms" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestCCF));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestCCF));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestOU)))
                {
                    string strOtherUsesLeft = " LEFT JOIN tblCredentialOtherUses as OU ON OU.CredentialId = C.CredentialId ";
                    string strOtherUsesWhere = " OU.OtherUsesId in (" + getCheckedItems(radlstDestOU) + ") " + " and ";

                    strLEFT.Append(strOtherUsesLeft);
                    strWHERECondition.Append(strOtherUsesWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Other_Uses");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestOU));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Other_Uses" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestOU));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestOU));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(cbo_Tab_Credential_Status)))
                {
                    string strCredentialStatusWhere = " C.Credential_Status in (" + getCheckedItems(cbo_Tab_Credential_Status) + ") " + " and ";

                    strWHERECondition.Append(strCredentialStatusWhere);

                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(cbo_Tab_Credential_Status.ID);
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(cbo_Tab_Credential_Status));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(cbo_Tab_Credential_Status.SelectedItem.Text);
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(cbo_Tab_Credential_Version)))
                {
                    string strCredentialVersionWhere = " C.Credential_Version in (" + getCheckedItems(cbo_Tab_Credential_Version) + ") " + " and ";

                    strWHERECondition.Append(strCredentialVersionWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(cbo_Tab_Credential_Version.ID);
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(cbo_Tab_Credential_Version));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(cbo_Tab_Credential_Version.SelectedItem.Text);
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(cbo_Tab_Credential_Type)))
                {
                    string strCredentialTypeWhere = " C.Credential_Type in (" + getCheckedItems(cbo_Tab_Credential_Type) + ") " + " and ";

                    strWHERECondition.Append(strCredentialTypeWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(cbo_Tab_Credential_Type.ID);
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(cbo_Tab_Credential_Type));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(cbo_Tab_Credential_Type.SelectedItem.Text);
                    strResultScreenCriteria.Append("|");
                }

                /*if (!string.IsNullOrEmpty(txt_Tab_OtherLanguage.Text.Trim()))
                {
                    string strLangugageWhere = " C.Project_Description like '%" + txt_Tab_OtherLanguage.Text.Trim() + "%'" + " and ";

                    strWHERECondition.Append(strLangugageWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(txt_Tab_OtherLanguage.ID); strCriteria.Append("="); strCriteria.Append(txt_Tab_OtherLanguage.Text.Trim());
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(txt_Tab_OtherLanguage.Text.Trim());
                    strResultScreenCriteria.Append("|");
                }*/

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestAL)))
                {
                    string strCountryLawWhere = " C.Country_Law in (" + getCheckedItems(radlstDestAL) + ") " + " and ";

                    strWHERECondition.Append(strCountryLawWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Country_Law");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestAL));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Country_Law" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestAL));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestAL));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestCMO)))
                {
                    string strCountryLeft = " LEFT JOIN tblCredentialCountryMatterOpen as CMO ON CMO.CredentialId = C.CredentialId ";
                    string strCountryWhere = " CMO.CountryId in (" + getCheckedItems(radlstDestCMO) + ") " + " and ";

                    strLEFT.Append(strCountryLeft);
                    strWHERECondition.Append(strCountryWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Country_Matter_Open");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestCMO));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Country_Matter_Open" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestCMO));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestCMO));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestPCC)))
                {
                    string strCountryLeft = " LEFT JOIN tblCredentialCountryClient as PCC ON PCC.CredentialId = C.CredentialId ";
                    string strCountryWhere = " PCC.CountryId in (" + getCheckedItems(radlstDestPCC) + ") " + " and ";

                    strLEFT.Append(strCountryLeft);
                    strWHERECondition.Append(strCountryWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Country_PredominantCountry");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestPCC));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Country_PredominantCountry" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestPCC));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestPCC));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(cbo_Tab_ProBono)))
                {
                    string strProWhere = " C.ProBono in (" + getCheckedItems(cbo_Tab_ProBono) + ") " + " and ";

                    strWHERECondition.Append(strProWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(cbo_Tab_ProBono.ID);
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(cbo_Tab_ProBono));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(cbo_Tab_ProBono.SelectedItem.Text);
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(getCheckedItems(radlstDestKH)))
                {
                    string strKnowHowLeft = " LEFT JOIN tblCredentialKnowHow as KH ON KH.CredentialId = C.CredentialId ";
                    string strKnowHowWhere = " KH.KnowHowId in (" + getCheckedItems(radlstDestKH) + ") " + " and ";

                    strLEFT.Append(strKnowHowLeft);
                    strWHERECondition.Append(strKnowHowWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append("rad_Tab_Know_How");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemsID(radlstDestKH));
                    strCriteria.Append("|");

                    strCriteria.Append("rad_Tab_Know_How" + "Text");
					strCriteria.Append("=");
					strCriteria.Append(getCheckedItemstext(radlstDestKH));
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(getCheckedItemstext(radlstDestKH));
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(txt_Tab_Bible_Reference.Text.Trim()))
                {
                    string strBibleWhere = " C.Bible_Reference like N'%" + txt_Tab_Bible_Reference.Text.Trim().Replace("'", "''") + "%'" + " and ";

                    strWHERECondition.Append(strBibleWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(txt_Tab_Bible_Reference.ID);
					strCriteria.Append("=");
					strCriteria.Append(txt_Tab_Bible_Reference.Text.Trim());
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(txt_Tab_Bible_Reference.Text.Trim());
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(txt_Tab_Matter_No.Text.Trim()))
                {
                    string strValueWhere = " C.Matter_No like '%" + txt_Tab_Matter_No.Text.Trim().Replace("'", "''") + "%'" + " and ";

                    strWHERECondition.Append(strValueWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(txt_Tab_Matter_No.ID);
					strCriteria.Append("=");
					strCriteria.Append(txt_Tab_Matter_No.Text.Trim());
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(txt_Tab_Matter_No.Text.Trim());
                    strResultScreenCriteria.Append("|");
                }

                if (!string.IsNullOrEmpty(txt_Tab_Credential_ID.Text.Trim()))
                {
                    string strIDWhere = " C.CredentialID like '%" + txt_Tab_Credential_ID.Text.Trim().Replace("'", "''") + "%'" + " and ";

                    strWHERECondition.Append(strIDWhere);
                    blnSearch = true;
                    blnCheckAndOr = false;

                    strCriteria.Append(txt_Tab_Credential_ID.ID);
					strCriteria.Append("=");
					strCriteria.Append(txt_Tab_Credential_ID.Text.Trim());
                    strCriteria.Append("|");

                    strResultScreenCriteria.Append(txt_Tab_Credential_ID.Text.Trim());
                    strResultScreenCriteria.Append("|");
                }

                /* Advanced Query Ends */
                if (blnSearch == true)
                {
                    string strFinalQuery = string.Empty;

                    if (strWHERECondition != null && strLEFT != null)
                    {
                        if (blnCheckAndOr == false)
                        {
							strFinalQuery = strSelect + strLEFT.ToString() + strWhere + strWHERECondition.ToString().Substring(0, strWHERECondition.ToString().Length - 5);
                        }
                        else
                        {
							strFinalQuery = strSelect + strLEFT.ToString() + strWhere + strWHERECondition.ToString().Substring(0, strWHERECondition.ToString().Length - 4);
						}
						
						strFinalQuery = strFinalQuery.Replace("~!@", "or");
						strFinalQuery += " ORDER BY C.CredentialId ASC";
						
						if (ConfigurationManager.AppSettings["MAIL"].ToString() == "YES")
						{
							MailQuery(strFinalQuery);
						}
					}
					
                    objSP = new CallingSP();
                    string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
                    SqlConnection con = new SqlConnection(strcon);
                    SqlDataAdapter adp = new SqlDataAdapter(strFinalQuery, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Session["sessionResultIDs"] != null)
                        {
                            Session["sessionResultIDs"] = null;
                        }
                        if (Session["sessionFilterCriteria"] != null)
                        {
                            Session["sessionFilterCriteria"] = null;
                        }
						if (Session["sessionResultChildIDs"] != null)
						{
							Session["sessionResultChildIDs"] = null;
						}
						if (Session["sessionResultCriteria"] != null)
						{
							Session["sessionResultCriteria"] = null;
						}
                        SortedList slMaster = new SortedList();
                        SortedList slOther = new SortedList();

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i][1].ToString().Trim() == "1") //Master
                            {
                                if (slMaster.Contains(ds.Tables[0].Rows[i][0].ToString()) == false)
                                {
                                    slMaster.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][0].ToString());
                                }
                            }
                            if (ds.Tables[0].Rows[i][1].ToString().Trim() == "2") //Other
                            {
                                if (slOther.Contains(ds.Tables[0].Rows[i][0].ToString()) == false)
                                {
                                    slOther.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][0].ToString());
                                }
                            }
                        }

                        objLog.LogWriter("SearchScreen : btnSearch_Click - Get master id for search results Starts ", hidName.Value);

                        string ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                        SqlConnection conn = new SqlConnection(ConnString);
                        conn.Open();

                        foreach (DictionaryEntry dic in slOther)
                        {
                            string strSQL = "select credentialmasterid from tblcredentialversionrelation where credentialid='" + dic.Value + "'";
                            SqlDataAdapter adapter = new SqlDataAdapter(strSQL, conn);
                            DataSet ds2 = new DataSet();
                            adapter.Fill(ds2);

                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                                {
                                    if (slMaster.Contains(ds2.Tables[0].Rows[i][0].ToString()) == false)
                                    {
                                        //Adi modified from ds1 to ds
                                        slMaster.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][0].ToString());
                                    }
                                }
                            }

                        }
                        conn.Close();

                        objLog.LogWriter("SearchScreen : btnSearch_Click - Get master id for search results Ends ", hidName.Value);

                        string strIDS = string.Empty;
						string strChildCredentialID = string.Empty;
						
						foreach (DictionaryEntry dic in slMaster)
						{
							if (string.IsNullOrEmpty(strIDS))
							{
								strIDS = dic.Value.ToString();
							}
							else
							{
								strIDS = strIDS + "," + dic.Value;
							}
						}
						
						string strsql = "SELECT a.credentialid FROM \r\n                                        tblcredentialversionrelation as a\r\n                                        INNER JOIN tblcredential as b\r\n                                        on a.CredentialID=b.CredentialId\r\n                                        WHERE a.credentialversion ='Other' and a.credentialmasterid in (" + strIDS + ") and b.DeleteFlag=0";
						SqlDataAdapter adpChild = new SqlDataAdapter(strsql, con);
						
						DataSet dsNew = new DataSet();
						adpChild.Fill(dsNew);
						adpChild.Dispose();
						
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
						}
						con.Close();
						if (!string.IsNullOrEmpty(strChildCredentialID))
						{
							strChildCredentialID = strChildCredentialID.Substring(0, strChildCredentialID.Length - 1);
							Session.Add("sessionResultChildIDs", strChildCredentialID.Replace("'", ""));
						}
                        slMaster = null;
                        slOther = null;

                        Session.Add("sessionResultIDs", strIDS.Replace("'", ""));
                        Session.Add("sessionFilterCriteria", strCriteria);
                        Session.Add("sessionResultCriteria", strResultScreenCriteria);

                        blnRedirect = true;
                    }
                    else
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script language='javascript'>");
                        sb.Append(@"alert('No records found.');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", sb.ToString(), false);
                        sb = null;
                    }
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"alert('No records found for the criteria.');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", sb.ToString(), false);
                    sb = null;
                }

                objLog.LogWriter("SearchScreen : btnSearch_Click Ends", hidName.Value);
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("SearchScreen Error : btnSearch_Click Ends " + ex.Message, hidName.Value);
                blnRedirect = false;
                throw ex;
            }
			finally
			{
				if (blnRedirect)
				{
					Response.BufferOutput = true;
					Response.Redirect("~/Search/ResultScreen.aspx");
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
		
		private string getCheckedItems(RadComboBox comboBox)
		{
			StringBuilder sb = new StringBuilder();
			IList<RadComboBoxItem> collection = comboBox.CheckedItems;
			string strItems = string.Empty;

			foreach (RadComboBoxItem item in collection)
			{
				if (item.Checked)
				{
					if (string.IsNullOrEmpty(strItems))
					{
						strItems = "'" + item.Value + "'";
					}
					else
					{
						strItems = strItems + ",'" + item.Value + "'";
					}
				}
			}
			return strItems;
		}
		
		private string getCheckedItems(RadListBox listbox)
		{
			StringBuilder sb = new StringBuilder();
			string strItems = string.Empty;

			for (int i = 0; i < listbox.Items.Count; i++)
			{
				if (string.IsNullOrEmpty(strItems))
				{
					strItems = "'" + listbox.Items[i].Value + "'";
				}
				else
				{
					strItems = strItems + ",'" + listbox.Items[i].Value + "'";
				}
			}
			return strItems;
		}
		private string getCheckedItemsID(RadListBox listbox)
		{
			string strItems = string.Empty;

			for (int i = 0; i < listbox.Items.Count; i++)
			{
				if (string.IsNullOrEmpty(strItems))
				{
					strItems = listbox.Items[i].Value;
				}
				else
				{
					strItems = strItems + "," + listbox.Items[i].Value;
				}
			}
			return strItems;
		}
		private string getCheckedItemstext(RadListBox listbox)
		{
			string strItems = string.Empty;

			for (int i = 0; i < listbox.Items.Count; i++)
			{
				if (string.IsNullOrEmpty(strItems))
				{
					strItems = listbox.Items[i].Text;
				}
				else
				{
					strItems = strItems + "," + listbox.Items[i].Text;
				}
			}
			return strItems;
		}
		private string getCheckedItemsID(RadComboBox comboBox)
		{
			IList<RadComboBoxItem> collection = comboBox.CheckedItems;
			string strItems = string.Empty;

			foreach (RadComboBoxItem item in collection)
			{
				if (item.Checked)
				{
					if (string.IsNullOrEmpty(strItems))
					{
						strItems = item.Value;
					}
					else
					{
						strItems = strItems + "," + item.Value;
					}
				}
			}
			return strItems;
		}

		private string getCheckedItemstext(RadComboBox comboBox)
		{
			IList<RadComboBoxItem> collection = comboBox.CheckedItems;
			string strItems = string.Empty;

			foreach (RadComboBoxItem item in collection)
			{
				if (item.Checked)
				{
					if (string.IsNullOrEmpty(strItems))
					{
						strItems = item.Text;
					}
					else
					{
						strItems = strItems + "," + item.Text;
					}
				}
			}
			return strItems;
		}

		protected void lnkBasic_Click(object sender, EventArgs e)
		{
			try
			{
				objLog.LogWriter("SearchScreen : lnkBasic_Click Starts", null);
				ClearAdvanced();
				lnkBasic.Text = "Basic Search";
				lnkAdvance.Text = "Advanced Search";

				hidadvanced.Value = "0";

				if (hidbasic.Value == "0")
				{
					txt_Tab_Client.Focus();

					divBasicSearch.Visible = true;
					lblBasicSearch.Visible = true;
					divAdvancedSearch.Visible = false;
					lblAdvancedSearch.Visible = false;
					tr_bottom.Visible = true;

					hr_top.Visible = true;
					hidbasic.Value = "1";

					lnkBasic.ForeColor = Color.RosyBrown;
					lnkAdvance.ForeColor = ColorTranslator.FromHtml("#00759A");
					lnkKeywordSearch.ForeColor = ColorTranslator.FromHtml("#00759A");
				}
				else
				{
					if (hidbasic.Value == "1")
					{
						radtxtKeywordSearch.Focus();

						divBasicSearch.Visible = false;
						lblBasicSearch.Visible = false;
						divAdvancedSearch.Visible = false;
						lblAdvancedSearch.Visible = false;
						tr_bottom.Visible = false;
						hr_top.Visible = false;
						hidbasic.Value = "0";

						lnkBasic.ForeColor = ColorTranslator.FromHtml("#00759A");
						lnkAdvance.ForeColor = ColorTranslator.FromHtml("#00759A");
						lnkKeywordSearch.ForeColor = Color.RosyBrown;
					}
				}

				objLog.LogWriter("SearchScreen : lnkBasic_Click Ends ", null);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("SearchScreen : Error in lnkBasic_Click " + ex.Message, hidName.Value);
				throw ex;
			}
		}
		
		protected void lnkAdvance_Click(object sender, EventArgs e)
		{
			try
			{
				ClearAdvanced();
				objLog.LogWriter("SearchScreen : lnkAdvance_Click Starts", null);
				lnkBasic.Text = "Basic Search";
				lnkAdvance.Text = "Advanced Search";
				hidbasic.Value = "0";
				
				if (hidadvanced.Value == "0")
				{
					txt_Tab_ProjectName_Core.Focus();
					divAdvancedSearch.Visible = true;
					divBasicSearch.Visible = true;
					lblAdvancedSearch.Visible = true;
					lblBasicSearch.Visible = true;
					tr_bottom.Visible = true;
					hr_top.Visible = true;
					hidadvanced.Value = "1";
					lnkBasic.ForeColor = ColorTranslator.FromHtml("#00759A");
					lnkAdvance.ForeColor = Color.RosyBrown;
					lnkKeywordSearch.ForeColor = ColorTranslator.FromHtml("#00759A");
				}
				else if (hidadvanced.Value == "1")
				{
					radtxtKeywordSearch.Focus();
					divAdvancedSearch.Visible = false;
					divBasicSearch.Visible = false;
					lblAdvancedSearch.Visible = false;
					lblBasicSearch.Visible = false;
					tr_bottom.Visible = false;
					hr_top.Visible = false;
					hidadvanced.Value = "0";
					lnkBasic.ForeColor = ColorTranslator.FromHtml("#00759A");
					lnkAdvance.ForeColor = ColorTranslator.FromHtml("#00759A");
					lnkKeywordSearch.ForeColor = Color.RosyBrown;
				}

				objLog.LogWriter("SearchScreen : lnkAdvance_Click Ends", null);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("SearchScreen : Error in lnkAdvance_Click" + ex.Message, hidName.Value);
				throw ex;
			}
		}
		
		protected void lnkKeywordSearch_Click(object sender, EventArgs e)
		{
			try
			{
				ClearBasicAdvanced();
				radtxtKeywordSearch.Focus();
				divBasicSearch.Visible = false;
				divAdvancedSearch.Visible = false;
				lblBasicSearch.Visible = false;
				lblAdvancedSearch.Visible = false;
				tr_bottom.Visible = false;
				hr_top.Visible = false;
				hidadvanced.Value = "0";
				hidbasic.Value = "0";
				lnkBasic.ForeColor = ColorTranslator.FromHtml("#00759A");
				lnkAdvance.ForeColor = ColorTranslator.FromHtml("#00759A");
				lnkKeywordSearch.ForeColor = Color.RosyBrown;
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("SearchScreen : Error in lnkKeywordSearch_Click" + ex.Message, hidName.Value);
				throw ex;
			}
		}
		
		protected void btnClearSearch_Click(object sender, EventArgs e)
		{
			try
			{
				RadListBox1.Items.Clear();
				RadListBox2.Items.Clear();
				radlstSourceCSS.Items.Clear();
				radlstDestCSS.Items.Clear();
				radlstSourceMS.Items.Clear();
				radlstDestMS.Items.Clear();
				radlstSourceMSS.Items.Clear();
				radlstDestMSS.Items.Clear();
				radlstSourceTeams.Items.Clear();
				radlstSourceML.Items.Clear();
				radlstDestML.Items.Clear();
				radlstDestTeams.Items.Clear();
				radlstSourceLP.Items.Clear();
				radlstDestLP.Items.Clear();
				radlstSourceME.Items.Clear();
				radlstDestME.Items.Clear();
				radlstSourceCD.Items.Clear();
				radlstDestCD.Items.Clear();
				radlstSourceContentious.Items.Clear();
				radlstDestContentious.Items.Clear();
				radlstSourceDR.Items.Clear();
				radlstDestDR.Items.Clear();
				radlstSourceCOA.Items.Clear();
				radlstDestCOA.Items.Clear();
				radlstSourceSOA.Items.Clear();
				radlstDestSOA.Items.Clear();
				radlstSourceAR.Items.Clear();
				radlstDestAR.Items.Clear();
				radlstSourceIVT.Items.Clear();
				radlstDestIVT.Items.Clear();
				radlstSourceLOD.Items.Clear();
				radlstDestLOD.Items.Clear();
				radlstSourceJOD.Items.Clear();
				radlstDestJOD.Items.Clear();
				radlstSourceCFI.Items.Clear();
				radlstDestCFI.Items.Clear();
				radlstSourceLCF.Items.Clear();
				radlstDestLCF.Items.Clear();
				radlstSourceCCF.Items.Clear();
				radlstDestCCF.Items.Clear();
				radlstSourceOU.Items.Clear();
				radlstDestOU.Items.Clear();
				radlstSourceAL.Items.Clear();
				radlstDestAL.Items.Clear();
				radlstSourceCMO.Items.Clear();
				radlstDestCMO.Items.Clear();
				radlstSourcePCC.Items.Clear();
				radlstDestPCC.Items.Clear();
				radlstSourceKH.Items.Clear();
				radlstDestKH.Items.Clear();
				hidCS.Value = "0";
				hidCSS.Value = "0";
				hidMS.Value = "0";
				hidMSS.Value = "0";
				hidTeams.Value = "0";
				hidML.Value = "0";
				hidTeams.Value = "0";
				hidLP.Value = "0";
				hidME.Value = "0";
				hidCD.Value = "0";
				hidContentious.Value = "0";
				hidDR.Value = "0";
				hidCOA.Value = "0";
				hidSOA.Value = "0";
				hidAR.Value = "0";
				hidIVT.Value = "0";
				hidLOD.Value = "0";
				hidLCF.Value = "0";
				hidCCF.Value = "0";
				hidOU.Value = "0";
				hidAL.Value = "0";
				hidCMO.Value = "0";
				hidPCC.Value = "0";
				hidKH.Value = "0";
				plnCS.Style.Add("display", "none");
				plnCSS.Style.Add("display", "none");
				plnMS.Style.Add("display", "none");
				plnMSS.Style.Add("display", "none");
				plnTeams.Style.Add("display", "none");
				plnML.Style.Add("display", "none");
				plnLP.Style.Add("display", "none");
				plnME.Style.Add("display", "none");
				plnCD.Style.Add("display", "none");
				plnContentious.Style.Add("display", "none");
				plnDR.Style.Add("display", "none");
				plnCOA.Style.Add("display", "none");
				plnSOA.Style.Add("display", "none");
				plnAR.Style.Add("display", "none");
				plnIVT.Style.Add("display", "none");
				plnLOD.Style.Add("display", "none");
				plnJOD.Style.Add("display", "none");
				plnCFI.Style.Add("display", "none");
				plnLCF.Style.Add("display", "none");
				plnCCF.Style.Add("display", "none");
				plnOU.Style.Add("display", "none");
				plnAL.Style.Add("display", "none");
				plnCMO.Style.Add("display", "none");
				plnPCC.Style.Add("display", "none");
				plnKH.Style.Add("display", "none");
				imgcollapseCS.Visible = false;
				imgexpandCS.Visible = true;
				imgcollapseCSS.Visible = false;
				imgexpandCSS.Visible = true;
				imgcollapseMS.Visible = false;
				imgexpandMS.Visible = true;
				imgcollapseMSS.Visible = false;
				imgexpandMSS.Visible = true;
				imgcollapseTeams.Visible = false;
				imgexpandTeams.Visible = true;
				imgcollapseML.Visible = false;
				imgexpandML.Visible = true;
				imgcollapseLP.Visible = false;
				imgexpandLP.Visible = true;
				imgcollapseME.Visible = false;
				imgexpandME.Visible = true;
				imgcollapseCD.Visible = false;
				imgexpandCD.Visible = true;
				imgcollapseContentious.Visible = false;
				imgexpandContentious.Visible = true;
				imgcollapseDR.Visible = false;
				imgexpandDR.Visible = true;
				imgcollapseCOA.Visible = false;
				imgexpandCOA.Visible = true;
				imgcollapseSOA.Visible = false;
				imgexpandSOA.Visible = true;
				imgcollapseAR.Visible = false;
				imgexpandAR.Visible = true;
				imgcollapseIVT.Visible = false;
				imgexpandIVT.Visible = true;
				imgcollapseLOD.Visible = false;
				imgexpandLOD.Visible = true;
				imgcollapseJOD.Visible = false;
				imgexpandJOD.Visible = true;
				imgcollapseCFI.Visible = false;
				imgexpandCFI.Visible = true;
				imgcollapseLCF.Visible = false;
				imgexpandLCF.Visible = true;
				imgcollapseCCF.Visible = false;
				imgexpandCCF.Visible = true;
				imgcollapseOU.Visible = false;
				imgexpandOU.Visible = true;
				imgcollapseAL.Visible = false;
				imgexpandAL.Visible = true;
				imgcollapseCMO.Visible = false;
				imgexpandCMO.Visible = true;
				imgcollapsePCC.Visible = false;
				imgexpandPCC.Visible = true;
				imgcollapseKH.Visible = false;
				imgexpandKH.Visible = true;
				radtxtKeywordSearch.Entries.Clear();
				txt_Tab_Client.Text = string.Empty;
				cld_Tab_Date_Opened.Clear();
				cld_Tab_Date_Opened1.Clear();
				chk_Tab_ActualDate_Ongoing.Checked = false;
				chk_Tab_ActualDate_NotKnown.Checked = false;
				SearchScreen.UnCheckedItems(rad_Tab_Priority);
				txt_Tab_ProjectName_Core.Text = string.Empty;
				txt_tab_ClientDescription.Text = string.Empty;
				txt_tab_Value_Deal.Text = string.Empty;
				SearchScreen.UnCheckedItems(cbo_Tab_Credential_Status);
				SearchScreen.UnCheckedItems(cbo_Tab_Credential_Version);
				SearchScreen.UnCheckedItems(cbo_Tab_Credential_Type);
				SearchScreen.UnCheckedItems(cbo_Tab_Credential_Status);
				SearchScreen.UnCheckedItems(cbo_Tab_Credential_Version);
				SearchScreen.UnCheckedItems(cbo_Tab_Credential_Type);
				SearchScreen.UnCheckedItems(cbo_Tab_ProBono);
				txt_Tab_Bible_Reference.Text = string.Empty;
				txt_Tab_Matter_No.Text = string.Empty;
				txt_Tab_Credential_ID.Text = string.Empty;
				lnkBasic.Text = "Basic Search";
				lnkAdvance.Text = "Advanced Search";
				hidbasic.Value = "0";
				hidadvanced.Value = "0";
				divBasicSearch.Visible = false;
				divAdvancedSearch.Visible = false;
				lblBasicSearch.Visible = false;
				lblAdvancedSearch.Visible = false;
				hr_top.Visible = false;
				tr_bottom.Visible = false;
				
				foreach (RadTreeNode node in RadTreeView1.GetAllNodes())
				{
					node.Checked = false;
					node.Expanded = false;
				}
				
				foreach (RadTreeNode node in RadTreeView2.GetAllNodes())
				{
					node.Checked = false;
					node.Expanded = false;
				}
				
				lnkBasic.ForeColor = ColorTranslator.FromHtml("#00759A");
				lnkAdvance.ForeColor = ColorTranslator.FromHtml("#00759A");
				lnkKeywordSearch.ForeColor = Color.RosyBrown;
				chkPartial.Checked = false;
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("SearchScreen : Error in btnClearSearch_Click Ends " + ex.Message, hidName.Value);
				throw ex;
			}
		}
		
		private void ClearBasicAdvanced()
		{
			RadListBox1.Items.Clear();
			RadListBox2.Items.Clear();
			radlstSourceCSS.Items.Clear();
			radlstDestCSS.Items.Clear();
			radlstSourceMS.Items.Clear();
			radlstDestMS.Items.Clear();
			radlstSourceMSS.Items.Clear();
			radlstDestMSS.Items.Clear();
			radlstSourceTeams.Items.Clear();
			radlstSourceML.Items.Clear();
			radlstDestML.Items.Clear();
			radlstDestTeams.Items.Clear();
			radlstSourceLP.Items.Clear();
			radlstDestLP.Items.Clear();
			radlstSourceME.Items.Clear();
			radlstDestME.Items.Clear();
			radlstSourceCD.Items.Clear();
			radlstDestCD.Items.Clear();
			radlstSourceContentious.Items.Clear();
			radlstDestContentious.Items.Clear();
			radlstSourceDR.Items.Clear();
			radlstDestDR.Items.Clear();
			radlstSourceCOA.Items.Clear();
			radlstDestCOA.Items.Clear();
			radlstSourceSOA.Items.Clear();
			radlstDestSOA.Items.Clear();
			radlstSourceAR.Items.Clear();
			radlstDestAR.Items.Clear();
			radlstSourceIVT.Items.Clear();
			radlstDestIVT.Items.Clear();
			radlstSourceLOD.Items.Clear();
			radlstDestLOD.Items.Clear();
			radlstSourceJOD.Items.Clear();
			radlstDestJOD.Items.Clear();
			radlstSourceCFI.Items.Clear();
			radlstDestCFI.Items.Clear();
			radlstSourceLCF.Items.Clear();
			radlstDestLCF.Items.Clear();
			radlstSourceCCF.Items.Clear();
			radlstDestCCF.Items.Clear();
			radlstSourceOU.Items.Clear();
			radlstDestOU.Items.Clear();
			radlstSourceAL.Items.Clear();
			radlstDestAL.Items.Clear();
			radlstSourceCMO.Items.Clear();
			radlstDestCMO.Items.Clear();
			radlstSourcePCC.Items.Clear();
			radlstDestPCC.Items.Clear();
			radlstSourceKH.Items.Clear();
			radlstDestKH.Items.Clear();
			hidCS.Value = "0";
			hidCSS.Value = "0";
			hidMS.Value = "0";
			hidMSS.Value = "0";
			hidTeams.Value = "0";
			hidML.Value = "0";
			hidLP.Value = "0";
			hidME.Value = "0";
			hidCD.Value = "0";
			hidContentious.Value = "0";
			hidDR.Value = "0";
			hidCOA.Value = "0";
			hidSOA.Value = "0";
			hidAR.Value = "0";
			hidIVT.Value = "0";
			hidLOD.Value = "0";
			hidLCF.Value = "0";
			hidCCF.Value = "0";
			hidOU.Value = "0";
			hidAL.Value = "0";
			hidCMO.Value = "0";
			hidPCC.Value = "0";
			hidKH.Value = "0";
			hidCS.Value = "0";
			plnCS.Style.Add("display", "none");
			plnCSS.Style.Add("display", "none");
			plnMS.Style.Add("display", "none");
			plnMSS.Style.Add("display", "none");
			plnTeams.Style.Add("display", "none");
			plnML.Style.Add("display", "none");
			plnLP.Style.Add("display", "none");
			plnME.Style.Add("display", "none");
			plnCD.Style.Add("display", "none");
			plnContentious.Style.Add("display", "none");
			plnDR.Style.Add("display", "none");
			plnCOA.Style.Add("display", "none");
			plnSOA.Style.Add("display", "none");
			plnAR.Style.Add("display", "none");
			plnIVT.Style.Add("display", "none");
			plnLOD.Style.Add("display", "none");
			plnJOD.Style.Add("display", "none");
			plnCFI.Style.Add("display", "none");
			plnLCF.Style.Add("display", "none");
			plnCCF.Style.Add("display", "none");
			plnOU.Style.Add("display", "none");
			plnAL.Style.Add("display", "none");
			plnCMO.Style.Add("display", "none");
			plnPCC.Style.Add("display", "none");
			plnKH.Style.Add("display", "none");
			imgcollapseCS.Visible = false;
			imgexpandCS.Visible = true;
			imgcollapseCSS.Visible = false;
			imgexpandCSS.Visible = true;
			imgcollapseMS.Visible = false;
			imgexpandMS.Visible = true;
			imgcollapseMSS.Visible = false;
			imgexpandMSS.Visible = true;
			imgcollapseTeams.Visible = false;
			imgexpandTeams.Visible = true;
			imgcollapseML.Visible = false;
			imgexpandML.Visible = true;
			imgcollapseLP.Visible = false;
			imgexpandLP.Visible = true;
			imgcollapseME.Visible = false;
			imgexpandME.Visible = true;
			imgcollapseCD.Visible = false;
			imgexpandCD.Visible = true;
			imgcollapseContentious.Visible = false;
			imgexpandContentious.Visible = true;
			imgcollapseDR.Visible = false;
			imgexpandDR.Visible = true;
			imgcollapseCOA.Visible = false;
			imgexpandCOA.Visible = true;
			imgcollapseSOA.Visible = false;
			imgexpandSOA.Visible = true;
			imgcollapseAR.Visible = false;
			imgexpandAR.Visible = true;
			imgcollapseIVT.Visible = false;
			imgexpandIVT.Visible = true;
			imgcollapseLOD.Visible = false;
			imgexpandLOD.Visible = true;
			imgcollapseJOD.Visible = false;
			imgexpandJOD.Visible = true;
			imgcollapseCFI.Visible = false;
			imgexpandCFI.Visible = true;
			imgcollapseLCF.Visible = false;
			imgexpandLCF.Visible = true;
			imgcollapseCCF.Visible = false;
			imgexpandCCF.Visible = true;
			imgcollapseOU.Visible = false;
			imgexpandOU.Visible = true;
			imgcollapseAL.Visible = false;
			imgexpandAL.Visible = true;
			imgcollapseCMO.Visible = false;
			imgexpandCMO.Visible = true;
			imgcollapsePCC.Visible = false;
			imgexpandPCC.Visible = true;
			imgcollapseKH.Visible = false;
			imgexpandKH.Visible = true;
			txt_Tab_Client.Text = string.Empty;
			cld_Tab_Date_Opened.Clear();
			cld_Tab_Date_Opened1.Clear();
			chk_Tab_ActualDate_Ongoing.Checked = false;
			chk_Tab_ActualDate_NotKnown.Checked = false;
			SearchScreen.UnCheckedItems(rad_Tab_Priority);
			txt_Tab_ProjectName_Core.Text = string.Empty;
			txt_tab_ClientDescription.Text = string.Empty;
			txt_tab_Value_Deal.Text = string.Empty;
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Status);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Version);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Type);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Status);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Version);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Type);
			SearchScreen.UnCheckedItems(cbo_Tab_ProBono);
			txt_Tab_Bible_Reference.Text = string.Empty;
			txt_Tab_Matter_No.Text = string.Empty;
			txt_Tab_Credential_ID.Text = string.Empty;
		}
		
		private void ClearAdvanced()
		{
			radlstSourceCD.Items.Clear();
			radlstDestCD.Items.Clear();
			radlstSourceContentious.Items.Clear();
			radlstDestContentious.Items.Clear();
			radlstSourceDR.Items.Clear();
			radlstDestDR.Items.Clear();
			radlstSourceCOA.Items.Clear();
			radlstDestCOA.Items.Clear();
			radlstSourceSOA.Items.Clear();
			radlstDestSOA.Items.Clear();
			radlstSourceAR.Items.Clear();
			radlstDestAR.Items.Clear();
			radlstSourceIVT.Items.Clear();
			radlstDestIVT.Items.Clear();
			radlstSourceLOD.Items.Clear();
			radlstDestLOD.Items.Clear();
			radlstSourceJOD.Items.Clear();
			radlstDestJOD.Items.Clear();
			radlstSourceCFI.Items.Clear();
			radlstDestCFI.Items.Clear();
			radlstSourceLCF.Items.Clear();
			radlstDestLCF.Items.Clear();
			radlstSourceCCF.Items.Clear();
			radlstDestCCF.Items.Clear();
			radlstSourceOU.Items.Clear();
			radlstDestOU.Items.Clear();
			radlstSourceAL.Items.Clear();
			radlstDestAL.Items.Clear();
			radlstSourceCMO.Items.Clear();
			radlstDestCMO.Items.Clear();
			radlstSourcePCC.Items.Clear();
			radlstDestPCC.Items.Clear();
			radlstSourceKH.Items.Clear();
			radlstDestKH.Items.Clear();
			hidCD.Value = "0";
			hidContentious.Value = "0";
			hidDR.Value = "0";
			hidCOA.Value = "0";
			hidSOA.Value = "0";
			hidAR.Value = "0";
			hidIVT.Value = "0";
			hidLOD.Value = "0";
			hidLCF.Value = "0";
			hidCCF.Value = "0";
			hidOU.Value = "0";
			hidAL.Value = "0";
			hidCMO.Value = "0";
			hidPCC.Value = "0";
			hidKH.Value = "0";
			plnCD.Style.Add("display", "none");
			plnContentious.Style.Add("display", "none");
			plnDR.Style.Add("display", "none");
			plnCOA.Style.Add("display", "none");
			plnSOA.Style.Add("display", "none");
			plnAR.Style.Add("display", "none");
			plnIVT.Style.Add("display", "none");
			plnLOD.Style.Add("display", "none");
			plnJOD.Style.Add("display", "none");
			plnCFI.Style.Add("display", "none");
			plnLCF.Style.Add("display", "none");
			plnCCF.Style.Add("display", "none");
			plnOU.Style.Add("display", "none");
			plnAL.Style.Add("display", "none");
			plnCMO.Style.Add("display", "none");
			plnPCC.Style.Add("display", "none");
			plnKH.Style.Add("display", "none");
			imgcollapseCD.Visible = false;
			imgexpandCD.Visible = true;
			imgcollapseContentious.Visible = false;
			imgexpandContentious.Visible = true;
			imgcollapseDR.Visible = false;
			imgexpandDR.Visible = true;
			imgcollapseCOA.Visible = false;
			imgexpandCOA.Visible = true;
			imgcollapseSOA.Visible = false;
			imgexpandSOA.Visible = true;
			imgcollapseAR.Visible = false;
			imgexpandAR.Visible = true;
			imgcollapseIVT.Visible = false;
			imgexpandIVT.Visible = true;
			imgcollapseLOD.Visible = false;
			imgexpandLOD.Visible = true;
			imgcollapseJOD.Visible = false;
			imgexpandJOD.Visible = true;
			imgcollapseCFI.Visible = false;
			imgexpandCFI.Visible = true;
			imgcollapseLCF.Visible = false;
			imgexpandLCF.Visible = true;
			imgcollapseCCF.Visible = false;
			imgexpandCCF.Visible = true;
			imgcollapseOU.Visible = false;
			imgexpandOU.Visible = true;
			imgcollapseAL.Visible = false;
			imgexpandAL.Visible = true;
			imgcollapseCMO.Visible = false;
			imgexpandCMO.Visible = true;
			imgcollapsePCC.Visible = false;
			imgexpandPCC.Visible = true;
			imgcollapseKH.Visible = false;
			imgexpandKH.Visible = true;
			txt_tab_Value_Deal.Text = string.Empty;
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Status);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Version);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Type);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Status);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Version);
			SearchScreen.UnCheckedItems(cbo_Tab_Credential_Type);
			SearchScreen.UnCheckedItems(cbo_Tab_ProBono);
			txt_Tab_Bible_Reference.Text = string.Empty;
			txt_Tab_Matter_No.Text = string.Empty;
			txt_Tab_Credential_ID.Text = string.Empty;
		}
		
		private static void UnCheckedItems(RadComboBox comboBox)
		{
			IList<RadComboBoxItem> collection = comboBox.CheckedItems;
			
			foreach (RadComboBoxItem item in collection)
			{
				item.Checked = false;
			}
		}
		
		private static void CheckTheOneLevelItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text.ToUpper() == "BAIF" || 
						node2.Text.ToUpper() == "CORPORATE TAX" || 
						node2.Text.ToUpper() == "EPC CONSTRUCTION" || 
						node2.Text.ToUpper() == "EPC ENERGY" || 
						node2.Text.ToUpper() == "CORPORATE" || 
						node2.Text.ToUpper() == "REAL ESTATE" || 
						node2.Text.ToUpper() == "HUMAN CAPITAL")
					{
						int i2 = 0;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							int i3 = node2.Nodes.Count;
							for (int k = 0; k < strItems.Split(',').Length; k++)
							{
								if (node2.Nodes.FindNodeByValue(strItems.Split(',')[k]) != null)
								{
									i2++;
									
									node2.Nodes.FindNodeByValue(strItems.Split(',')[k]).Checked = true;
									node2.Nodes.FindNodeByValue(strItems.Split(',')[k]).Expanded = true;
									node2.Nodes.FindNodeByValue(strItems.Split(',')[k]).ExpandParentNodes();
								}
							}
							
							if (i3 == i2)
							{
								node2.Checked = true;
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
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text.ToUpper() == "CORPORATE" || 
						node2.Text.ToUpper() == "REAL ESTATE" || 
						node2.Text.ToUpper() == "HUMAN CAPITAL")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								
								for (int l = 0; l < strItems.Split(',').Length; l++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]) != null)
									{
										i2++;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckCRDWTItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text.ToUpper() == "CRD")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								for (int K = 0; K < strItems.Split(',').Length; K++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[K]) != null)
									{
										i2++;
										
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[K]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[K]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[K]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckCRDSWTItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text.ToUpper() == "CRD")
					{
						for (int i = 0; i < node2.Nodes.Count; i++)
						{
							for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
							{
								for (int k = 0; k < node2.Nodes[i].Nodes[j].Nodes.Count; k++)
								{
									for (int x = 0; x < strItems.Split(',').Length; x++)
									{
										if (node2.Nodes[i].Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[x]) != null)
										{
											node2.Nodes[i].Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[x]).Checked = true;
											node2.Nodes[i].Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[x]).Expanded = true;
											node2.Nodes[i].Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[x]).ExpandParentNodes();
										}
									}
								}
							}
						}
					}
				}
			}
		}
		
		private static void CheckThePraticeGroupItems1(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text == "BAIF")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						if (node2.Text == strItems.Split('=')[0].Split('/')[1])
						{
							for (int j = 0; j < node2.Nodes.Count; j++)
							{
								i3++;
								
								if (node2.Nodes[j].Text == strItems.Split('=')[0].Split('/')[2])
								{
									for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
									{
										int i5 = node2.Nodes[j].Nodes.Count;
										
										for (int l = 0; l < strItems.Split(',').Length; l++)
										{
											if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split('=')[1].Split(',')[l]) != null)
											{
												i2++;
												
												node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split('=')[1].Split(',')[l]).Checked = true;
												node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split('=')[1].Split(',')[l]).Expanded = true;
												node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split('=')[1].Split(',')[l]).ExpandParentNodes();
											}
										}
										
										if (i5 == i2)
										{
											node2.Nodes[j].Checked = true;
										}
									}
								}
							}
							
							if (i4 == i3)
							{
								node2.Checked = true;
							}
						}
					}
				}
			}
		}
		
		private static void CheckCorporateItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text == "CORPORATE")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								for (int l = 0; l < strItems.Split(',').Length; l++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]) != null)
									{
										i2++;
										
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
									node2.Nodes[j].ExpandParentNodes();
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckCRDItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text == "CRD")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								for (int l = 0; l < strItems.Split(',').Length; l++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]) != null)
									{
										i2++;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
									node2.Nodes[j].ExpandParentNodes();
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckEPCENERGYItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text == "EPC ENERGY")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								
								for (int l = 0; l < strItems.Split(',').Length; l++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]) != null)
									{
										i2++;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
									node2.Nodes[j].ExpandParentNodes();
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckEPCConstItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text == "EPC CONSTRUCTION")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								for (int l = 0; l < strItems.Split(',').Length; l++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]) != null)
									{
										i2++;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
									node2.Nodes[j].ExpandParentNodes();
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckEPCProjectsItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text == "EPC PROJECTS")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								for (int l = 0; l < strItems.Split(',').Length; l++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]) != null)
									{
										i2++;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
									node2.Nodes[j].ExpandParentNodes();
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckHumanCapitalItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text == "HUMAN CAPITAL")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								for (int l = 0; l < strItems.Split(',').Length; l++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]) != null)
									{
										i2++;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
									node2.Nodes[j].ExpandParentNodes();
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckRealEstateItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text == "REAL ESTATE")
					{
						int i2 = 0;
						int i3 = 0;
						int i4 = node2.Nodes.Count;
						
						for (int j = 0; j < node2.Nodes.Count; j++)
						{
							i3++;
							
							for (int k = 0; k < node2.Nodes[j].Nodes.Count; k++)
							{
								int i5 = node2.Nodes[j].Nodes.Count;
								
								for (int l = 0; l < strItems.Split(',').Length; l++)
								{
									if (node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]) != null)
									{
										i2++;
										
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Checked = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).Expanded = true;
										node2.Nodes[j].Nodes.FindNodeByValue(strItems.Split(',')[l]).ExpandParentNodes();
									}
								}
								
								if (i5 == i2)
								{
									node2.Nodes[j].Checked = true;
									node2.Nodes[j].ExpandParentNodes();
								}
							}
						}
						
						if (i4 == i3)
						{
							node2.Checked = true;
						}
					}
				}
			}
		}
		
		private static void CheckPGItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				foreach (RadTreeNode node2 in node.Nodes)
				{
					if (node2.Text.ToUpper() == strItems.ToUpper())
					{
						node2.Checked = true;
						
						for (int i = 0; i < node2.Nodes.Count; i++)
						{
							node2.Nodes[i].Checked = true;
							
							for (int j = 0; j < node2.Nodes[i].Nodes.Count; j++)
							{
								node2.Nodes[i].Nodes[j].Checked = true;
								node2.Nodes[i].Nodes[j].Expanded = true;
								node2.Nodes[i].Nodes[j].ExpandParentNodes();
							}
						}
					}
				}
			}
		}
	}
}
