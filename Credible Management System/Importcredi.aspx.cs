
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using CredentialsDemo.Common;
using System.Text;
using System.IO;
using System.Collections;

namespace CMS
{
    public partial class Importcredi : System.Web.UI.Page
    {
        CallingSP objsp = new CallingSP();
        Logger obj = new Logger();
        //protected System.Web.UI.HtmlControls.HtmlForm Form1; 
        string[] ConvertToStringArray(System.Array values)
        {

            // create a new string array 
            string[] theArray = new string[values.Length];

            // loop through the 2-D System.Array and populate the 1-D String Array 
            for (int i = 1; i <= values.Length; i++)
            {
                if (values.GetValue(1, i) == null)
                    theArray[i - 1] = "";
                else
                    theArray[i - 1] = (string)values.GetValue(1, i).ToString();
            }

            return theArray;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Excel.Application appExl;
                Excel.Workbook workbook;
                Excel.Worksheet NwSheet;
                Excel.Range ShtRange;
                appExl = new Excel.Application();


                //Opening Excel file(myData.xlsx) Warsaw_REandConst.xlsx
                //workbook = appExl.Workbooks.Open("E:\\Import\\Latest_Template_2.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                //workbook = appExl.Workbooks.Open(Server.MapPath("RealEstateCreds.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //workbook = appExl.Workbooks.Open(Server.MapPath("DisputeResolution.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
               //workbook = appExl.Workbooks.Open(Server.MapPath("HumanCapital.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
               // workbook = appExl.Workbooks.Open(Server.MapPath("Soundbites.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //workbook = appExl.Workbooks.Open(Server.MapPath("IRG_3.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //workbook = appExl.Workbooks.Open(Server.MapPath("Copy of EPCConst_1.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
               //workbook = appExl.Workbooks.Open(Server.MapPath("Tech Lit.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //workbook = appExl.Workbooks.Open(Server.MapPath("EPCENERGY.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
              // workbook = appExl.Workbooks.Open(Server.MapPath("budapest.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
               // workbook = appExl.Workbooks.Open(Server.MapPath("kyiv.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
               // workbook = appExl.Workbooks.Open(Server.MapPath("Bucharest_DR_1.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                workbook = appExl.Workbooks.Open(Server.MapPath("Copy of ReadyToMigrateWarsawLifesciences.xls"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //workbook = appExl.Workbooks.Open(Server.MapPath("Bucharest_RE_1.xlsx"), Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                //Bucharest_RE_1.xlsx
                NwSheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);

                DataTable dt = new DataTable();
                DataTable dtErrorRecords = new DataTable();
                string[] strArray;
                int iLength = 0;
                for (int i = 4; i < 5; i++)
                {
                    Excel.Range range = NwSheet.get_Range("A" + i.ToString(), "FM" + i.ToString());
                    System.Array myvalues = (System.Array)range.Cells.Value;
                    strArray = ConvertToStringArray(myvalues);
                    iLength = strArray.Length;
                    for (int k = 1; k < strArray.Length; k++)
                    {
                        dt.Columns.Add(strArray[k]);
                    }
                    for (int k = 1; k < strArray.Length; k++)
                    {
                        dtErrorRecords.Columns.Add(strArray[k]);
                    }
                }


                int Cnum = 0;

                ShtRange = NwSheet.UsedRange; //gives the used cells in sheet

                //Reading Excel file.
                //Creating datatable to read the content of the Sheet in File.


                for (int i = 7; i <= ShtRange.Rows.Count; i++)
                {
                    Excel.Range range = NwSheet.get_Range("A" + i.ToString(), "FM" + i.ToString());
                    System.Array myvaluesRows = (System.Array)range.Cells.Value;
                    string[] strArrayRows = ConvertToStringArray(myvaluesRows);

                    bool bln = false;

                    DataRow dr = dt.NewRow();

                    if (string.IsNullOrEmpty(strArrayRows[1]) || string.IsNullOrEmpty(strArrayRows[2]) || string.IsNullOrEmpty(strArrayRows[26]))
                    {
                        bln = true;
                    }
                    else
                    {
                        /*if Client name confidential =yes ShtRange.Cells[Rnum, 3]=credential description*/

                        if (strArrayRows[2].ToUpper() == "YES")
                        {
                            if (string.IsNullOrEmpty(strArrayRows[3]))
                            {
                                bln = true;
                            }
                        }
                    }

                    if (bln == false)
                    {
                        //Reading Each Column value From sheet to datatable Colunms    
                        for (Cnum = 1; Cnum < strArrayRows.Length; Cnum++)
                        {
                            if (!string.IsNullOrEmpty(strArrayRows[Cnum]))
                            {
                                dr[Cnum - 1] = strArrayRows[Cnum];
                            }
                        }

                        // dr[Rnum] = (ShtRange.Cells[Rnum] as Excel.Range).Value2.ToString();

                        dt.Rows.Add(dr); // adding Row into DataTable
                        dt.AcceptChanges();
                    }
                }


                for (int l = 0; l < dt.Rows.Count; l++)
                {
                    string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
                    SqlConnection con = new SqlConnection(strcon);
                    SqlCommand cmd = new SqlCommand();
                    con.Open();
                    var transaction = con.BeginTransaction();
                    try
                    {
                        SortedList slSectorGroupMSText = new SortedList();
                        SortedList slMatterSectorGroupMSText = new SortedList();
                        SortedList slSectorGroupMS = new SortedList();
                        SortedList slSubSectorGroupMS = new SortedList();
                        SortedList slCountryOfClientMS = new SortedList();
                        SortedList slMatterSectorGroupMS = new SortedList();
                        SortedList slMatterSubSectorGroupMS = new SortedList();
                        SortedList slCountryWhereOpenedMS = new SortedList();
                        SortedList slCountryofTransactionMS = new SortedList();

                        SortedList slLanguageOfDisputeMS = new SortedList();
                        SortedList slJurisdictionOfDisputeMS = new SortedList();
                        SortedList slCountryofArbitrationMS = new SortedList();

                        SortedList slTeamMS = new SortedList();
                        SortedList slLeadPartnerMS = new SortedList();
                        SortedList slSourceOfCredentialMS = new SortedList();
                        SortedList slOtherMatterExecutiveMS = new SortedList();
                        SortedList slCMSFirmsInvolvedMS = new SortedList();

                        SortedList slCountryOtherCMSOfficeMS = new SortedList();
                        SortedList slOtherUsesMS = new SortedList();

                        SortedList slKnowHowMS = new SortedList();

                        SortedList slhid_BAI_Work_Type = new SortedList();

                        SortedList slhid_CRD_Work_Type = new SortedList();
                        SortedList slhid_CRD_SubWork_Type = new SortedList();
                        SortedList slhid_EPC_Nature_Of_Work = new SortedList();
                        SortedList slcbo_EPC_ClientScopeId = new SortedList();
                        SortedList slhid_EPC_Type_Of_Contract = new SortedList();


                        SortedList slhid_Cor_Work_Type = new SortedList();
                        SortedList slhid_Cor_SubWork_Type = new SortedList();
                        SortedList slhid_Cor_Acting_For = new SortedList();

                        SortedList slhid_Cor_Country_Seller = new SortedList();
                        SortedList slhid_Cor_Country_Buyer = new SortedList();
                        SortedList slhid_Cor_Country_Target = new SortedList();

                        SortedList slhid_ENE_Transaction_Type = new SortedList();

                        SortedList slhid_HCC_Work_Type = new SortedList();
                        SortedList slhid_HCC_SubWork_Type = new SortedList();
                        SortedList slhid_RES_Client_Type = new SortedList();
                        SortedList slhid_RES_Work_Type = new SortedList();
                        SortedList slHCSWT = new SortedList();

                        string strSectorGroupMSText = string.Empty;
                        string strMatterSectorGroupMSText = string.Empty;
                        string strNameConfidentialSS = string.Empty;
                        string strNameConfidentialCompSS = string.Empty;
                        string strSectorGroupMS = string.Empty;
                        string strSubSectorGroupMS = string.Empty;
                        string strCountryOfClientMS = string.Empty;
                        string strPrac = string.Empty;
                        string strMatterSectorGroupMS = string.Empty;
                        string strMatterSubSectorGroupMS = string.Empty;
                        string strMatterConfidentialSS = string.Empty;
                        string strstrMatterConfidentialCompSS = string.Empty;
                        string strCountryLawSS = string.Empty;
                        string strCountryWhereOpenedMS = string.Empty;
                        string strCountryofTransactionMS = string.Empty;
                        string strContentiousSS = string.Empty;
                        string strDisputeResolutionSS = string.Empty;
                        string stLanguageOfDisputeMS = string.Empty;
                        string stJurisdictionOfDisputeMS = string.Empty;
                        string strCountryofArbitrationMS = string.Empty;
                        string strSeatOfArbitrationSS = string.Empty;
                        string strArbitralRulesSS = string.Empty;
                        string strInvestmentTreatySS = string.Empty;
                        string strInvestigationTypeSS = string.Empty;
                        string strCurrencyOfDealeSS = string.Empty;
                        string strValueCOnfidentialSS = string.Empty;
                        string strValueConfidentialCompSS = string.Empty;
                        string strTeamMS = string.Empty;
                        string strLeadPartnerMS = string.Empty;
                        string strSourceOfCredentialMS = string.Empty;
                        string strOtherMatterExecutiveMS = string.Empty;
                        string strCMSFirmsInvolvedMS = string.Empty;
                        string strLeadCMSFirmSS = string.Empty;
                        string strCountryOtherCMSOfficeMS = string.Empty;
                        string strCredentialStatusSS = string.Empty;
                        string strCredentialVersionSS = string.Empty;
                        string strCredentialTypeSS = string.Empty;
                        string strOtherUsesMS = string.Empty;
                        string strPrioritySS = string.Empty;
                        string strProbonoSS = string.Empty;
                        string strKnowHowMS = string.Empty;
                        string strcbo_BAI_ClientTypeIdBAIF = string.Empty;
                        string strtxt_BAI_LeadBanks = string.Empty;
                        string strhid_BAI_Work_Type = string.Empty;
                        string strcbo_CRD_ClientTypeIdCommercial = string.Empty;
                        string strhid_CRD_Work_Type = string.Empty;
                        string strhid_CRD_SubWork_Type = string.Empty;
                        string strhid_EPC_Nature_Of_Work = string.Empty;
                        string strcbo_EPC_ClientScopeId = string.Empty;
                        string strhid_EPC_Type_Of_Contract = string.Empty;
                        string strtxt_EPC_Type_Of_Contract_Other = string.Empty;
                        string strcbo_EPC_SubjectMatterId = string.Empty;
                        string strtxt_EPC_SubjectMatterOther = string.Empty;
                        string strcbo_EPC_ClientTypeIdEPC = string.Empty;
                        string strtxt_EPC_ClientTypeOther = string.Empty;
                        string strhid_Cor_Work_Type = string.Empty;
                        string strhid_Cor_SubWork_Type = string.Empty;
                        string strhid_Cor_Acting_For = string.Empty;
                        string strcbo_Cor_Value_Over_Pound = string.Empty;
                        string strhid_Cor_Country_Seller = string.Empty;
                        string strhid_Cor_Country_Buyer = string.Empty;
                        string strhid_Cor_Country_Target = string.Empty;
                        string strcbo_Cor_Value_Over_US = string.Empty;
                        string strcbo_Cor_Value_Over_Euro = string.Empty;
                        string strtxt_Cor_Published_Reference = string.Empty;
                        string strcbo_Cor_MAStudy = string.Empty;
                        string strcbo_Cor_PEClients = string.Empty;
                        string strcbo_Cor_QuarterDealAnnouncedId = string.Empty;
                        string strcbo_Cor_QuarterDealCompletedId = string.Empty;
                        string strtxt_Cor_YearDeal_Announced = string.Empty;
                        string strcbo_Cor_YearDealCompletedId = string.Empty;
                        string strhid_ENE_Transaction_Type = string.Empty;
                        string strcbo_ENE_ContractTypeId = string.Empty;
                        string strcbo_IPF_ClientTypeIdIPF = string.Empty;
                        string strhid_HCC_Work_Type = string.Empty;
                        string strhid_HCC_SubWork_Type = string.Empty;
                        string strhid_RES_Client_Type = string.Empty;
                        string strhid_RES_Work_Type = string.Empty;
                        string strHCSWT = string.Empty;
                        string strcbo_Crt_WorkType_CorpTax = string.Empty;
                        string strcbo_Cor_ValueRangeEuro = string.Empty;
                        string strpensionSchemeHC = string.Empty;
                        string strClient = string.Empty;
                        string strClientDesc = string.Empty;
                        string strProjDesc = string.Empty;
                        string strOtherMatterDesc = string.Empty;
                        string strProjectName = string.Empty;
                        string strKeyword = string.Empty;
                        string strBibleRef = string.Empty;
                        string strCMSPartnerName = string.Empty;
                        string strMatterNo = string.Empty;
                        string strDateOpened = string.Empty;
                        string strDateComp = string.Empty;
                        string strCountryLawOther = string.Empty;
                        string strArbitrationCityOther = string.Empty;
                        string strValueofDeal = string.Empty;
                        string strSourceOfCredOther = string.Empty;
                        string strVersionName = string.Empty;

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Columns[j].ColumnName == "0")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strClient = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "1")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strNameConfidentialSS = "1";
                                    }
                                    else
                                    {
                                        strNameConfidentialSS = "0";
                                    }

                                }
                            }
                            if (dt.Columns[j].ColumnName == "2")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strClientDesc = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "3")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strNameConfidentialCompSS = "1";
                                    }
                                    else
                                    {
                                        strNameConfidentialCompSS = "0";
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "4" || dt.Columns[j].ColumnName == "5" || dt.Columns[j].ColumnName == "6")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblClientIndustrySector", "ClientIndustrySectorId", "Client_Industry_Sector", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slSectorGroupMS.Contains(strID) == false)
                                        {
                                            slSectorGroupMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strSectorGroupMS))
                                            {
                                                strSectorGroupMS = strID;
                                            }
                                            else
                                            {
                                                strSectorGroupMS = strSectorGroupMS + "," + strID;
                                            }

                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (slSectorGroupMSText.Contains(dt.Rows[l][j].ToString().Trim()) == false)
                                    {
                                        slSectorGroupMS.Add(dt.Rows[l][j].ToString().Trim(), dt.Rows[l][j].ToString().Trim());

                                        if (string.IsNullOrEmpty(strSectorGroupMSText))
                                        {
                                            strSectorGroupMSText = dt.Rows[l][j].ToString().Replace("'", "8@9!").Trim();
                                        }
                                        else
                                        {
                                            strSectorGroupMSText = strSectorGroupMSText + "," + dt.Rows[l][j].ToString().Trim();
                                        }

                                    }
                                }

                            }

                            if (dt.Columns[j].ColumnName == "7" || dt.Columns[j].ColumnName == "8" || dt.Columns[j].ColumnName == "9")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblClientIndustryType", "ClientIndustryTypeId", "Client_Industry_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slSubSectorGroupMS.Contains(strID) == false)
                                        {
                                            slSubSectorGroupMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strSubSectorGroupMS))
                                            {
                                                strSubSectorGroupMS = strID;
                                            }
                                            else
                                            {
                                                strSubSectorGroupMS = strSubSectorGroupMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "10" || dt.Columns[j].ColumnName == "11" || dt.Columns[j].ColumnName == "12")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slCountryOfClientMS.Contains(strID) == false)
                                        {
                                            slCountryOfClientMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strCountryOfClientMS))
                                            {
                                                strCountryOfClientMS = strID;
                                            }
                                            else
                                            {
                                                strCountryOfClientMS = strCountryOfClientMS + "," + strID;
                                            }
                                        }
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "13" || dt.Columns[j].ColumnName == "14" || dt.Columns[j].ColumnName == "15")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblBusinessGroup", "BusinessGroupId", "Business_Group", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slCountryOfClientMS.Contains(strID) == false)
                                        {
                                            slCountryOfClientMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strPrac))
                                            {
                                                strPrac = strID;
                                            }
                                            else
                                            {
                                                strPrac = strPrac + "," + strID;
                                            }
                                        }
                                    }

                                }
                            }
                            if (dt.Columns[j].ColumnName == "16")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strMatterNo = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }
                            if (dt.Columns[j].ColumnName == "17")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strDateOpened = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                    //strDateOpened = "convert(datetime,'" + strDate.Replace("–", "/") + "',103)";
                                }
                            }
                            if (dt.Columns[j].ColumnName == "18")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strDateComp = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                    //strDateOpened = "convert(datetime,'" + strDate.Replace("–", "/") + "',103)";
                                }
                            }

                            if (dt.Columns[j].ColumnName == "19" || dt.Columns[j].ColumnName == "20" || dt.Columns[j].ColumnName == "21")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblTransactionIndustrySector", "TransactionIndustrySectorId", "Transaction_Industry_Sector", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slMatterSectorGroupMS.Contains(strID) == false)
                                        {
                                            slMatterSectorGroupMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strMatterSectorGroupMS))
                                            {
                                                strMatterSectorGroupMS = strID;
                                            }
                                            else
                                            {
                                                strMatterSectorGroupMS = strMatterSectorGroupMS + "," + strID;
                                            }
                                        }
                                    }
                                }

                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (slMatterSectorGroupMSText.Contains(dt.Rows[l][j].ToString().Trim()) == false)
                                    {
                                        slMatterSectorGroupMSText.Add(dt.Rows[l][j].ToString().Trim(), dt.Rows[l][j].ToString().Trim());

                                        if (string.IsNullOrEmpty(strMatterSectorGroupMSText.Trim()))
                                        {
                                            strMatterSectorGroupMSText = dt.Rows[l][j].ToString().Replace("'", "8@9!").Trim();
                                        }
                                        else
                                        {
                                            strMatterSectorGroupMSText = strMatterSectorGroupMSText + "," + dt.Rows[l][j].ToString().Trim();
                                        }

                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "22" || dt.Columns[j].ColumnName == "23" || dt.Columns[j].ColumnName == "24")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString().Trim()))
                                {
                                    string strID = GetMultiSelectValueId("tblTransactionIndustryType", "TransactionIndustryTypeId", "Transaction_Industry_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slMatterSubSectorGroupMS.Contains(strID) == false)
                                        {
                                            slMatterSubSectorGroupMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strMatterSubSectorGroupMS))
                                            {
                                                strMatterSubSectorGroupMS = strID;
                                            }
                                            else
                                            {
                                                strMatterSubSectorGroupMS = strMatterSubSectorGroupMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }
                            if (dt.Columns[j].ColumnName == "25")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strProjDesc = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }
                            if (dt.Columns[j].ColumnName == "26")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strOtherMatterDesc = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }
                            if (dt.Columns[j].ColumnName == "27")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strKeyword = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "28")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strMatterConfidentialSS = "1";
                                    }
                                    else
                                    {
                                        strMatterConfidentialSS = "0";
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "29")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strstrMatterConfidentialCompSS = "1";
                                    }
                                    else
                                    {
                                        strstrMatterConfidentialCompSS = "0";
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "30")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strProjectName = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "31")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strCountryLawSS))
                                    {//CountryLawId
                                        strCountryLawSS = GetMultiSelectValueId("tblCountryLaw", "CountryLawId", "Country_law", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "32")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strCountryLawOther = dt.Rows[l][j].ToString();
                                }
                            }

                            if (dt.Columns[j].ColumnName == "33" || dt.Columns[j].ColumnName == "34" || dt.Columns[j].ColumnName == "35")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slCountryWhereOpenedMS.Contains(strID) == false)
                                        {
                                            slCountryWhereOpenedMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strCountryWhereOpenedMS))
                                            {
                                                strCountryWhereOpenedMS = strID;
                                            }
                                            else
                                            {
                                                strCountryWhereOpenedMS = strCountryWhereOpenedMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "36" || dt.Columns[j].ColumnName == "37" || dt.Columns[j].ColumnName == "38")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slCountryofTransactionMS.Contains(strID) == false)
                                        {
                                            slCountryofTransactionMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strCountryofTransactionMS))
                                            {
                                                strCountryofTransactionMS = strID;
                                            }
                                            else
                                            {
                                                strCountryofTransactionMS = strCountryofTransactionMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }
                            /* Contentious 1 Non-Contentious 0 Both 2 */

                            if (dt.Columns[j].ColumnName == "39")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strContentiousSS))
                                    {
                                        if (dt.Rows[l][j].ToString().ToUpper() == "CONTENTIOUS")
                                        {
                                            strContentiousSS = "1";
                                        }
                                        else if (dt.Rows[l][j].ToString().ToUpper() == "NON - CONTENTIOUS"
                                            || dt.Rows[l][j].ToString().ToUpper() == "NON CONTENTIOUS"
                                            || dt.Rows[l][j].ToString().ToUpper() == "NONCONTENTIOUS"
                                            || dt.Rows[l][j].ToString().ToUpper() == "NON-CONTENTIOUS")
                                        {
                                            strContentiousSS = "0";
                                        }
                                        else if (dt.Rows[l][j].ToString().ToUpper() == "BOTH")
                                        {
                                            strContentiousSS = "2";
                                        }
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "40")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strDisputeResolutionSS))
                                    {//DisputeResolutionId
                                        strDisputeResolutionSS = GetMultiSelectValueId("tblDisputeResolution", "DisputeResolutionId", "Dispute_Resolution", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "41" || dt.Columns[j].ColumnName == "42" || dt.Columns[j].ColumnName == "43")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblLanguageOfDispute", "LanguageOfDisputeId", "Language_Of_Dispute", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slLanguageOfDisputeMS.Contains(strID) == false)
                                        {
                                            slLanguageOfDisputeMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(stLanguageOfDisputeMS))
                                            {
                                                stLanguageOfDisputeMS = strID;
                                            }
                                            else
                                            {
                                                stLanguageOfDisputeMS = stLanguageOfDisputeMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "44" || dt.Columns[j].ColumnName == "45" || dt.Columns[j].ColumnName == "46")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slJurisdictionOfDisputeMS.Contains(strID) == false)
                                        {
                                            slJurisdictionOfDisputeMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(stJurisdictionOfDisputeMS))
                                            {
                                                stJurisdictionOfDisputeMS = strID;
                                            }
                                            else
                                            {
                                                stJurisdictionOfDisputeMS = stJurisdictionOfDisputeMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "47" || dt.Columns[j].ColumnName == "48" || dt.Columns[j].ColumnName == "49")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString())) //yuva
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slCountryofArbitrationMS.Contains(strID) == false)
                                        {
                                            slCountryofArbitrationMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strCountryofArbitrationMS))
                                            {
                                                strCountryofArbitrationMS = strID;
                                            }
                                            else
                                            {
                                                strCountryofArbitrationMS = strCountryofArbitrationMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "50")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strSeatOfArbitrationSS))
                                    {//SeatOfArbitrationId 
                                        strSeatOfArbitrationSS = GetMultiSelectValueId("tblSeatOfArbitration", "SeatOfArbitrationId", "Seat_Of_Arbitration", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "51")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strArbitrationCityOther = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "52")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strArbitralRulesSS))
                                    {
                                        strArbitralRulesSS = GetMultiSelectValueId("tblArbitralRules", "ArbitralRulesId", "Arbitral_Rules", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "53")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strInvestmentTreatySS = "1";
                                    }
                                    else
                                    {
                                        strInvestmentTreatySS = "0";
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "54")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strInvestigationTypeSS))
                                    {//InvestigationTypeId
                                        strInvestigationTypeSS = GetMultiSelectValueId("tblInvestigationType", "InvestigationTypeId", "Investigation_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }
                                }
                            }

                            //strValueofDeal
                            if (dt.Columns[j].ColumnName == "55")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strValueofDeal = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "56")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strInvestigationTypeSS))
                                    {//CurrencyOfDealId
                                        strInvestigationTypeSS = GetMultiSelectValueId("tblCurrencyOfDeal", "CurrencyOfDealId", "Currency_Of_Deal", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "57")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strValueCOnfidentialSS = "1";
                                    }
                                    else
                                    {
                                        strValueCOnfidentialSS = "0";
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "58")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strValueConfidentialCompSS = "1";
                                    }
                                    else
                                    {
                                        strValueConfidentialCompSS = "0";
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "59" || dt.Columns[j].ColumnName == "60" || dt.Columns[j].ColumnName == "61")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblTeam", "TeamId", "Team", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slTeamMS.Contains(strID) == false)
                                        {
                                            slTeamMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strTeamMS))
                                            {
                                                strTeamMS = strID;
                                            }
                                            else
                                            {
                                                strTeamMS = strTeamMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "62" || dt.Columns[j].ColumnName == "63" || dt.Columns[j].ColumnName == "64")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblLeadPartner", "LeadPartnerId", "Lead_Partner", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slLeadPartnerMS.Contains(strID) == false)
                                        {
                                            slLeadPartnerMS.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strLeadPartnerMS))
                                            {
                                                strLeadPartnerMS = strID;
                                            }
                                            else
                                            {
                                                strLeadPartnerMS = strLeadPartnerMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "65")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strCMSPartnerName = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "66" || dt.Columns[j].ColumnName == "67" || dt.Columns[j].ColumnName == "68")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblSourceOfCredential", "SourceOfCredentialId", "Source_Of_Credential", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slSourceOfCredentialMS.Contains(strID) == false)
                                        {
                                            slSourceOfCredentialMS.Add(strID, strID);

                                            if (string.IsNullOrEmpty(strSourceOfCredentialMS))
                                            {
                                                strSourceOfCredentialMS = strID;
                                            }
                                            else
                                            {
                                                strSourceOfCredentialMS = strSourceOfCredentialMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "69")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strSourceOfCredOther = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "70" || dt.Columns[j].ColumnName == "71" || dt.Columns[j].ColumnName == "72")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblOtherMatterExecutive", "OtherMatterExecutiveId", "Other_Matter_Executive", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slOtherMatterExecutiveMS.Contains(strID) == false)
                                        {
                                            slOtherMatterExecutiveMS.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strOtherMatterExecutiveMS))
                                            {
                                                strOtherMatterExecutiveMS = strID;
                                            }
                                            else
                                            {
                                                strOtherMatterExecutiveMS = strOtherMatterExecutiveMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "73" || dt.Columns[j].ColumnName == "74" || dt.Columns[j].ColumnName == "75")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblReferredFromOtherCMSOffice", "ReferredFromOtherCMSOfficeId", "Referred_From_Other_CMS_Office", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slCMSFirmsInvolvedMS.Contains(strID) == false)
                                        {
                                            slCMSFirmsInvolvedMS.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strCMSFirmsInvolvedMS))
                                            {
                                                strCMSFirmsInvolvedMS = strID;
                                            }
                                            else
                                            {
                                                strCMSFirmsInvolvedMS = strCMSFirmsInvolvedMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "76")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strLeadCMSFirmSS))
                                    {//CurrencyOfDealId
                                        strLeadCMSFirmSS = GetMultiSelectValueId("tblReferredFromOtherCMSOffice", "ReferredFromOtherCMSOfficeId", "Referred_From_Other_CMS_Office", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "77" || dt.Columns[j].ColumnName == "78" || dt.Columns[j].ColumnName == "79")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slCountryOtherCMSOfficeMS.Contains(strID) == false)
                                        {
                                            slCountryOtherCMSOfficeMS.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strCountryOtherCMSOfficeMS))
                                            {
                                                strCountryOtherCMSOfficeMS = strID;
                                            }
                                            else
                                            {
                                                strCountryOtherCMSOfficeMS = strCountryOtherCMSOfficeMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "80")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strCredentialStatusSS))
                                    {//CurrencyOfDealId
                                        strCredentialStatusSS = GetMultiSelectValueId("tblCredentialStatus", "CredentialStatusId", "Credential_Status", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "81")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strCredentialVersionSS))
                                    {//CurrencyOfDealId
                                        strCredentialVersionSS = GetMultiSelectValueId("tblCredentialVersion", "CredentialVersionId", "Credential_Version", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }

                                }
                                else
                                {
                                    strCredentialVersionSS = "1";
                                }
                            }

                            if (dt.Columns[j].ColumnName == "82")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strVersionName = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "83")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strCredentialTypeSS))
                                    {//CurrencyOfDealId
                                        strCredentialTypeSS = GetMultiSelectValueId("tblCredentialType", "CredentialTypeId", "Credential_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }
                                }
                            }
                            //"tblCountry", "CountryId", "Country"
                            if (dt.Columns[j].ColumnName == "84" || dt.Columns[j].ColumnName == "85" || dt.Columns[j].ColumnName == "86")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblOtherUses", "OtherUsesId", "Other_Uses", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slOtherUsesMS.Contains(strID) == false)
                                        {
                                            slOtherUsesMS.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strOtherUsesMS))
                                            {
                                                strOtherUsesMS = strID;
                                            }
                                            else
                                            {
                                                strOtherUsesMS = strOtherUsesMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "87")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (string.IsNullOrEmpty(strPrioritySS))
                                    {
                                        strPrioritySS = GetMultiSelectValueId("tblPriority", "PriorityId", "Priority", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "88")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strProbonoSS = "1";
                                    }
                                    else
                                    {
                                        strProbonoSS = "0";
                                    }

                                }
                            }
                            //"tblCountry", "CountryId", "Country"
                            if (dt.Columns[j].ColumnName == "89" || dt.Columns[j].ColumnName == "90" || dt.Columns[j].ColumnName == "91")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblKnowHow", "KnowHowId", "Know_How", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slKnowHowMS.Contains(strID) == false)
                                        {
                                            slKnowHowMS.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strKnowHowMS))
                                            {
                                                strKnowHowMS = strID;
                                            }
                                            else
                                            {
                                                strKnowHowMS = strKnowHowMS + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "69")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strBibleRef = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            /*BAIF PG ADD DETAILS*/


                            /* neena */

                            if (dt.Columns[j].ColumnName == "93")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_BAI_ClientTypeIdBAIF = GetMultiSelectValueId("tblClientType", "ClientTypeId", "Client_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }

                            if (dt.Columns[j].ColumnName == "94")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strtxt_BAI_LeadBanks = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "95" || dt.Columns[j].ColumnName == "96" || dt.Columns[j].ColumnName == "97")
                            {


                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblWorkType", "WorkTypeId", "Work_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_BAI_Work_Type.Contains(strID) == false)
                                        {
                                            slhid_BAI_Work_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_BAI_Work_Type))
                                            {
                                                strhid_BAI_Work_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_BAI_Work_Type = strhid_BAI_Work_Type + "," + strID;
                                            }
                                        }
                                    }
                                }

                            }


                            /*BAIF Ends*/
                            /* CRD Starts */

                            if (dt.Columns[j].ColumnName == "98")
                            {


                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_CRD_ClientTypeIdCommercial = GetMultiSelectValueId("tblClientType", "ClientTypeId", "Client_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }

                            }


                            if (dt.Columns[j].ColumnName == "99" || dt.Columns[j].ColumnName == "100" || dt.Columns[j].ColumnName == "101")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblWorkType", "WorkTypeId", "Work_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_CRD_Work_Type.Contains(strID) == false)
                                        {
                                            slhid_CRD_Work_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_CRD_Work_Type))
                                            {
                                                strhid_CRD_Work_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_CRD_Work_Type = strhid_CRD_Work_Type + "," + strID;
                                            }
                                        }
                                    }

                                }

                            }

                            if (dt.Columns[j].ColumnName == "102" || dt.Columns[j].ColumnName == "103" || dt.Columns[j].ColumnName == "104")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblSubWorkType", "SubWorkTypeId", "SubWork_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_CRD_SubWork_Type.Contains(strID) == false)
                                        {
                                            slhid_CRD_SubWork_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_CRD_SubWork_Type))
                                            {
                                                strhid_CRD_SubWork_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_CRD_SubWork_Type = strhid_CRD_SubWork_Type + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            /* CRD Ends */

                            /* EPC CONSTRUCTION */



                            if (dt.Columns[j].ColumnName == "134" || dt.Columns[j].ColumnName == "135" || dt.Columns[j].ColumnName == "136")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblWorkType", "WorkTypeId", "Work_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_EPC_Nature_Of_Work.Contains(strID) == false)
                                        {
                                            slhid_EPC_Nature_Of_Work.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_EPC_Nature_Of_Work))
                                            {
                                                strhid_EPC_Nature_Of_Work = strID;
                                            }
                                            else
                                            {
                                                strhid_EPC_Nature_Of_Work = strhid_EPC_Nature_Of_Work + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "145")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_EPC_ClientScopeId = GetMultiSelectValueId("tblClientScope", "ClientScopeId", "Client_Scope", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }

                            }


                            if (dt.Columns[j].ColumnName == "137" || dt.Columns[j].ColumnName == "138" || dt.Columns[j].ColumnName == "139")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblTypeContract", "TypeContractId", "Type_Of_Contract", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_EPC_Type_Of_Contract.Contains(strID) == false)
                                        {
                                            slhid_EPC_Type_Of_Contract.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_EPC_Type_Of_Contract))
                                            {
                                                strhid_EPC_Type_Of_Contract = strID;
                                            }
                                            else
                                            {
                                                strhid_EPC_Type_Of_Contract = strhid_EPC_Type_Of_Contract + "," + strID;
                                            }
                                        }
                                    }
                                }

                            }

                            if (dt.Columns[j].ColumnName == "140")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strtxt_EPC_Type_Of_Contract_Other = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }
                            if (dt.Columns[j].ColumnName == "141")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_EPC_SubjectMatterId = GetMultiSelectValueId("tblSubjectMatter", "SubjectMatterId", "Subject_Matter", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }

                            if (dt.Columns[j].ColumnName == "142")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strtxt_EPC_SubjectMatterOther = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            if (dt.Columns[j].ColumnName == "143")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_EPC_ClientTypeIdEPC = GetMultiSelectValueId("tblClientType", "ClientTypeId", "Client_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }

                            if (dt.Columns[j].ColumnName == "144")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strtxt_EPC_ClientTypeOther = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }

                            /* Corporate */


                            if (dt.Columns[j].ColumnName == "105" || dt.Columns[j].ColumnName == "106" || dt.Columns[j].ColumnName == "107")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblWorkType", "WorkTypeId", "Work_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_Cor_Work_Type.Contains(strID) == false)
                                        {
                                            slhid_Cor_Work_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_Cor_Work_Type))
                                            {
                                                strhid_Cor_Work_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_Cor_Work_Type = strhid_Cor_Work_Type + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "108" || dt.Columns[j].ColumnName == "109" || dt.Columns[j].ColumnName == "110")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblSubWorkType", "SubWorkTypeId", "SubWork_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_Cor_SubWork_Type.Contains(strID) == false)
                                        {
                                            slhid_Cor_SubWork_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_Cor_SubWork_Type))
                                            {
                                                strhid_Cor_SubWork_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_Cor_SubWork_Type = strhid_Cor_SubWork_Type + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }


                            if (dt.Columns[j].ColumnName == "111" || dt.Columns[j].ColumnName == "112" || dt.Columns[j].ColumnName == "113")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblActingFor", "ActingForId", "Acting_For", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_Cor_Acting_For.Contains(strID) == false)
                                        {
                                            slhid_Cor_Acting_For.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_Cor_Acting_For))
                                            {
                                                strhid_Cor_Acting_For = strID;
                                            }
                                            else
                                            {
                                                strhid_Cor_Acting_For = strhid_Cor_Acting_For + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }


                            if (dt.Columns[j].ColumnName == "124")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strcbo_Cor_Value_Over_Pound = "1";
                                    }
                                    else
                                    {
                                        strcbo_Cor_Value_Over_Pound = "0";
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "114" || dt.Columns[j].ColumnName == "115" || dt.Columns[j].ColumnName == "116")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_Cor_Country_Seller.Contains(strID) == false)
                                        {
                                            slhid_Cor_Country_Seller.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_Cor_Country_Seller))
                                            {
                                                strhid_Cor_Country_Seller = strID;
                                            }
                                            else
                                            {
                                                strhid_Cor_Country_Seller = strhid_Cor_Country_Seller + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "117" || dt.Columns[j].ColumnName == "118" || dt.Columns[j].ColumnName == "119")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_Cor_Country_Buyer.Contains(strID) == false)
                                        {
                                            slhid_Cor_Country_Buyer.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_Cor_Country_Buyer))
                                            {
                                                strhid_Cor_Country_Buyer = strID;
                                            }
                                            else
                                            {
                                                strhid_Cor_Country_Buyer = strhid_Cor_Country_Buyer + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "120" || dt.Columns[j].ColumnName == "121" || dt.Columns[j].ColumnName == "122")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblCountry", "CountryId", "Country", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_Cor_Country_Target.Contains(strID) == false)
                                        {
                                            slhid_Cor_Country_Target.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_Cor_Country_Target))
                                            {
                                                strhid_Cor_Country_Target = strID;
                                            }
                                            else
                                            {
                                                strhid_Cor_Country_Target = strhid_Cor_Country_Target + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "123")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strcbo_Cor_Value_Over_US = "1";
                                    }
                                    else
                                    {
                                        strcbo_Cor_Value_Over_US = "0";
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "126")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_Cor_ValueRangeEuro = GetMultiSelectValueId("tblValueRange", "ValueRangeId", "Value_Range", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }


                            if (dt.Columns[j].ColumnName == "125")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strcbo_Cor_Value_Over_Euro = "1";
                                    }
                                    else
                                    {
                                        strcbo_Cor_Value_Over_Euro = "0";
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "133")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strtxt_Cor_Published_Reference = dt.Rows[l][j].ToString().Replace("'", "8@9!");
                                }
                            }


                            if (dt.Columns[j].ColumnName == "127")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strcbo_Cor_MAStudy = "1";
                                    }
                                    else
                                    {
                                        strcbo_Cor_MAStudy = "0";
                                    }

                                }
                            }


                            if (dt.Columns[j].ColumnName == "128")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    if (dt.Rows[l][j].ToString().ToUpper() == "YES")
                                    {
                                        strcbo_Cor_PEClients = "1";
                                    }
                                    else
                                    {
                                        strcbo_Cor_PEClients = "0";
                                    }

                                }
                            }

                            if (dt.Columns[j].ColumnName == "129")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_Cor_QuarterDealAnnouncedId = GetMultiSelectValueId("tblQuarterDealCompleted", "QuarterDealCompletedId", "Quarter_Deal_Completed", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }

                            if (dt.Columns[j].ColumnName == "130")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_Cor_QuarterDealCompletedId = GetMultiSelectValueId("tblQuarterDealCompleted", "QuarterDealCompletedId", "Quarter_Deal_Completed", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }
                            if (dt.Columns[j].ColumnName == "131")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strtxt_Cor_YearDeal_Announced = GetMultiSelectValueId("tblYearDealCompleted", "YearDealCompletedId", "Year_Deal_Completed", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }

                            }

                            if (dt.Columns[j].ColumnName == "132")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_Cor_YearDealCompletedId = GetMultiSelectValueId("tblYearDealCompleted", "YearDealCompletedId", "Year_Deal_Completed", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }


                            /* EPC Energy */

                            if (dt.Columns[j].ColumnName == "146" || dt.Columns[j].ColumnName == "147" || dt.Columns[j].ColumnName == "148")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblWorkType", "WorkTypeId", "Work_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_ENE_Transaction_Type.Contains(strID) == false)
                                        {
                                            slhid_ENE_Transaction_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_ENE_Transaction_Type))
                                            {
                                                strhid_ENE_Transaction_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_ENE_Transaction_Type = strhid_ENE_Transaction_Type + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }


                            if (dt.Columns[j].ColumnName == "149")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_ENE_ContractTypeId = GetMultiSelectValueId("tblContractType", "ContractTypeId", "Contract_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }

                            /* IPF */

                            if (dt.Columns[j].ColumnName == "150")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_IPF_ClientTypeIdIPF = GetMultiSelectValueId("tblClientType", "ClientTypeId", "Client_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }


                            /* HC */


                            if (dt.Columns[j].ColumnName == "151" || dt.Columns[j].ColumnName == "152" || dt.Columns[j].ColumnName == "153")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblWorkType", "WorkTypeId", "Work_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_HCC_Work_Type.Contains(strID) == false)
                                        {
                                            slhid_HCC_Work_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_HCC_Work_Type))
                                            {
                                                strhid_HCC_Work_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_HCC_Work_Type = strhid_HCC_Work_Type + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }


                            if (dt.Columns[j].ColumnName == "154" || dt.Columns[j].ColumnName == "155" || dt.Columns[j].ColumnName == "156")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblSubWorkType", "SubWorkTypeId", "SubWork_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_HCC_SubWork_Type.Contains(strID) == false)
                                        {
                                            slhid_HCC_SubWork_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_HCC_SubWork_Type))
                                            {
                                                strhid_HCC_SubWork_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_HCC_SubWork_Type = strhid_HCC_SubWork_Type + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "157")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strpensionSchemeHC = GetMultiSelectValueId("tblPensionScheme", "PensionSchemeId", "PensionScheme", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }


                            /* RE */


                            if (dt.Columns[j].ColumnName == "158" || dt.Columns[j].ColumnName == "159" || dt.Columns[j].ColumnName == "160")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblClientType", "ClientTypeId", "Client_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_RES_Client_Type.Contains(strID) == false)
                                        {
                                            slhid_RES_Client_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_RES_Client_Type))
                                            {
                                                strhid_RES_Client_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_RES_Client_Type = strhid_RES_Client_Type + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }

                            if (dt.Columns[j].ColumnName == "161" || dt.Columns[j].ColumnName == "162" || dt.Columns[j].ColumnName == "163")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblWorkType", "WorkTypeId", "Work_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slhid_RES_Work_Type.Contains(strID) == false)
                                        {
                                            slhid_RES_Work_Type.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strhid_RES_Work_Type))
                                            {
                                                strhid_RES_Work_Type = strID;
                                            }
                                            else
                                            {
                                                strhid_RES_Work_Type = strhid_RES_Work_Type + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }


                            if (dt.Columns[j].ColumnName == "164" || dt.Columns[j].ColumnName == "165" || dt.Columns[j].ColumnName == "166")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    string strID = GetMultiSelectValueId("tblSubWorkType", "SubWorkTypeId", "SubWork_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                    if (!strID.Equals("0"))
                                    {
                                        if (slHCSWT.Contains(strID) == false)
                                        {
                                            slHCSWT.Add(strID, strID);
                                            if (string.IsNullOrEmpty(strHCSWT))
                                            {
                                                strHCSWT = strID;
                                            }
                                            else
                                            {
                                                strHCSWT = strHCSWT + "," + strID;
                                            }
                                        }
                                    }
                                }
                            }


                            /*Corporate tax*/


                            if (dt.Columns[j].ColumnName == "167")
                            {
                                if (!string.IsNullOrEmpty(dt.Rows[l][j].ToString()))
                                {
                                    strcbo_Crt_WorkType_CorpTax = GetMultiSelectValueId("tblWorkType", "WorkTypeId", "Work_Type", dt.Rows[l][j].ToString().Replace("'", "''"));
                                }
                            }
                        }


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

                        /* neena */


                        if (string.IsNullOrEmpty(strcbo_BAI_ClientTypeIdBAIF))
                        {
                            /* strSS.Append("ClientTypeIdBAIF"); strSS.Append("~"); strSS.Append(string.Empty); //array[0] Client TypeId
                             strSS.Append("|");*/
                        }
                        else
                        {
                            strSS.Append("ClientTypeIdBAIF"); strSS.Append("~"); strSS.Append(strcbo_BAI_ClientTypeIdBAIF); //array[0] Client TypeId
                            strSS.Append("|");
                        }

                        if (string.IsNullOrEmpty(strtxt_BAI_LeadBanks))
                        {
                            /*strSS.Append("LeadBanks"); strSS.Append("~"); strSS.Append(string.Empty); //array[0] Client TypeId
                            strSS.Append("|");*/
                        }
                        else
                        {
                            strSS.Append("LeadBanks"); strSS.Append("~"); strSS.Append(strtxt_BAI_LeadBanks); //array[0] Client TypeId
                            strSS.Append("|");
                        }

                        if (string.IsNullOrEmpty(strhid_BAI_Work_Type))
                        {
                            /*strhid_BAI_Work_Type = "NTA";*/
                        }
                        else
                        {
                            strMS.Append("Work_Type"); strMS.Append("~"); strMS.Append(strhid_BAI_Work_Type);//[1] WorkTypeId(s)
                            strMS.Append("|");
                            strSS.Append("WorkTypeMSID"); strSS.Append("~"); strSS.Append(strhid_BAI_Work_Type);//[1] WorkTypeId(s)
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

                        if (Session["sessCRDSS"] != null)
                        {
                            Session.Remove("sessCRDSS");
                        }
                        if (Session["sessCRDMS"] != null)
                        {
                            Session.Remove("sessCRDMS");
                        }

                        StringBuilder strSSCRD = new StringBuilder();
                        StringBuilder strMSCRD = new StringBuilder();


                        if (!string.IsNullOrEmpty(strcbo_CRD_ClientTypeIdCommercial))
                        {
                            strSSCRD.Append("ClientTypeIdCommercial"); strSSCRD.Append("~"); strSSCRD.Append(strcbo_CRD_ClientTypeIdCommercial);//[2] TransactionValueId
                            strSSCRD.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strhid_CRD_Work_Type))
                        {
                            strMSCRD.Append("Work_Type"); strMSCRD.Append("~"); strMSCRD.Append(strhid_CRD_Work_Type);
                            strMSCRD.Append("|");
                            strSSCRD.Append("WorkTypeMSId"); strSSCRD.Append("~"); strSSCRD.Append(strhid_CRD_Work_Type);//[1] WorkTypeId(s)
                            strSSCRD.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strhid_BAI_Work_Type))
                        {
                            strMSCRD.Append("SubWork_Type"); strMSCRD.Append("~"); strMSCRD.Append(strhid_CRD_SubWork_Type);
                            strMSCRD.Append("|");
                            strSSCRD.Append("SubWorkTypeMSId"); strSSCRD.Append("~"); strSSCRD.Append(strhid_CRD_SubWork_Type);//[1] WorkTypeId(s)
                            strSSCRD.Append("|");
                        }

                        string strMSCRDstr = strMSCRD.ToString();
                        string strSSCRDstr = strSSCRD.ToString();

                        if (!(string.IsNullOrEmpty(strSSCRDstr)))
                        {
                            strSSCRD = strSSCRD.Remove(strSSCRD.Length - 1, 1);
                            Session.Add("sessCRDSS", strSSCRD);
                            strSSCRD = null;
                            strSSCRDstr = null;
                        }
                        if (!(string.IsNullOrEmpty(strMSCRDstr)))
                        {
                            strMSCRD = strMSCRD.Remove(strMSCRD.Length - 1, 1);
                            Session.Add("sessCRDMS", strMSCRD);
                            strMSCRD = null;
                            strMSCRDstr = null;
                        }

                        if (Session["sessEPCSS"] != null)
                        {
                            Session.Remove("sessEPCSS");
                        }
                        if (Session["sessEPCMS"] != null)
                        {
                            Session.Remove("sessEPCMS");
                        }
                        StringBuilder strEPCSS = new StringBuilder();
                        StringBuilder strEPCMS = new StringBuilder();


                        if (!string.IsNullOrEmpty(strhid_EPC_Nature_Of_Work))
                        {
                            strEPCMS.Append("Nature_Of_Work"); strEPCMS.Append("~"); strEPCMS.Append(strhid_EPC_Nature_Of_Work);//[1] WorkTypeId(s)
                            strEPCMS.Append("|");
                            strEPCSS.Append("WorkTypeMSId"); strEPCSS.Append("~"); strEPCSS.Append(strhid_EPC_Nature_Of_Work);//[1] WorkTypeId(s)
                            strEPCSS.Append("|");

                        }

                        if (!string.IsNullOrEmpty(strcbo_EPC_ClientScopeId))
                        {
                            strEPCSS.Append("ClientScopeId"); strEPCSS.Append("~"); strEPCSS.Append(strcbo_EPC_ClientScopeId);//[2] TransactionValueId
                            strEPCSS.Append("|");
                        }



                        if (!string.IsNullOrEmpty(strhid_EPC_Type_Of_Contract))
                        {
                            strEPCMS.Append("Type_Of_Contract"); strMS.Append("~"); strEPCMS.Append(strhid_EPC_Type_Of_Contract);//[1] WorkTypeId(s)
                            strEPCMS.Append("|");
                            strEPCSS.Append("TypeOfContractMSId"); strEPCSS.Append("~"); strEPCSS.Append(strhid_EPC_Type_Of_Contract);//[1] WorkTypeId(s)
                            strEPCSS.Append("|");

                        }
                        if (!string.IsNullOrEmpty(strtxt_EPC_Type_Of_Contract_Other))
                        {
                            strEPCSS.Append("Type_Of_Contract_Other"); strEPCSS.Append("~"); strEPCSS.Append(strtxt_EPC_Type_Of_Contract_Other);//[3] RoleOfLeadBank
                            strEPCSS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strcbo_EPC_SubjectMatterId))
                        {
                            strEPCSS.Append("SubjectMatterId"); strEPCSS.Append("~"); strEPCSS.Append(strcbo_EPC_SubjectMatterId);//[2] TransactionValueId
                            strEPCSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strtxt_EPC_SubjectMatterOther))
                        {
                            strEPCSS.Append("Subject_Matter_Other"); strEPCSS.Append("~"); strEPCSS.Append(strtxt_EPC_SubjectMatterOther);//[3] RoleOfLeadBank
                            strEPCSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strcbo_EPC_ClientTypeIdEPC))
                        {
                            strEPCSS.Append("ClientTypeIdEPC"); strEPCSS.Append("~"); strEPCSS.Append(strcbo_EPC_ClientTypeIdEPC);//[2] TransactionValueId
                            strEPCSS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strtxt_EPC_ClientTypeOther))
                        {
                            strEPCSS.Append("ClientTypeOther"); strEPCSS.Append("~"); strEPCSS.Append(strtxt_EPC_ClientTypeOther);//[3] RoleOfLeadBank
                            strEPCSS.Append("|");
                        }


                        string strEPCMSstr = strEPCMS.ToString();
                        string strEPCSSstr = strEPCSS.ToString();


                        if (!(string.IsNullOrEmpty(strEPCSSstr)))
                        {
                            strEPCSS = strEPCSS.Remove(strEPCSS.Length - 1, 1);
                            Session.Add("sessEPCSS", strEPCSS);
                            strEPCSS = null;
                            strEPCSSstr = null;
                        }
                        if (!(string.IsNullOrEmpty(strEPCMSstr)))
                        {
                            strEPCMS = strEPCMS.Remove(strEPCMS.Length - 1, 1);
                            Session.Add("sessEPCMS", strEPCMS);
                            strEPCMS = null;
                            strEPCMSstr = null;
                        }


                        if (Session["sessCORPSS"] != null)
                        {
                            Session.Remove("sessCORPSS");
                        }
                        if (Session["sessCORPMS"] != null)
                        {
                            Session.Remove("sessCORPMS");
                        }
                        StringBuilder strCorpSS = new StringBuilder();
                        StringBuilder strCorpMS = new StringBuilder();


                        if (!string.IsNullOrEmpty(strhid_Cor_Work_Type))
                        {
                            strCorpMS.Append("Work_Type"); strCorpMS.Append("~"); strCorpMS.Append(strhid_Cor_Work_Type);//[1] WorkTypeId(s)
                            strCorpMS.Append("|");

                            strCorpSS.Append("WorkTypeMSId"); strCorpSS.Append("~"); strCorpSS.Append(strhid_Cor_Work_Type);//[1] WorkTypeId(s)
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strhid_Cor_SubWork_Type))
                        {
                            strCorpMS.Append("SubWork_Type"); strCorpMS.Append("~"); strCorpMS.Append(strhid_Cor_SubWork_Type);//[1] WorkTypeId(s)
                            strCorpMS.Append("|");

                            strCorpSS.Append("SubWorkTypeMSId"); strCorpSS.Append("~"); strCorpSS.Append(strhid_Cor_SubWork_Type);//[1] WorkTypeId(s)
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strhid_Cor_Acting_For))
                        {
                            strCorpMS.Append("Acting_For"); strCorpMS.Append("~"); strCorpMS.Append(strhid_Cor_Acting_For);//[1] WorkTypeId(s)
                            strCorpMS.Append("|");

                            strCorpSS.Append("ActingForMSId"); strCorpSS.Append("~"); strCorpSS.Append(strhid_Cor_Acting_For);//[1] WorkTypeId(s)
                            strCorpSS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strcbo_Cor_Value_Over_Pound))
                        {
                            strCorpSS.Append("Value_Over_Pound"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_Value_Over_Pound);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strhid_Cor_Country_Seller))
                        {
                            strCorpMS.Append("Country_Seller"); strCorpMS.Append("~"); strCorpMS.Append(strhid_Cor_Country_Seller);//[1] WorkTypeId(s)
                            strCorpMS.Append("|");

                            strCorpSS.Append("Country_SellerMSId"); strCorpSS.Append("~"); strCorpSS.Append(strhid_Cor_Country_Seller);//[1] WorkTypeId(s)
                            strCorpSS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strhid_Cor_Country_Buyer))
                        {
                            strCorpMS.Append("Country_Buyer"); strCorpMS.Append("~"); strCorpMS.Append(strhid_Cor_Country_Buyer);//[1] WorkTypeId(s)
                            strCorpMS.Append("|");

                            strCorpSS.Append("Country_BuyerMSId"); strCorpSS.Append("~"); strCorpSS.Append(strhid_Cor_Country_Buyer);//[1] WorkTypeId(s)
                            strCorpSS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strhid_Cor_Country_Target))
                        {
                            strCorpMS.Append("Country_Target"); strCorpMS.Append("~"); strCorpMS.Append(strhid_Cor_Country_Target);//[1] WorkTypeId(s)
                            strCorpMS.Append("|");

                            strCorpSS.Append("Country_TargetMSId"); strCorpSS.Append("~"); strCorpSS.Append(strhid_Cor_Country_Target);//[1] WorkTypeId(s)
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strcbo_Cor_Value_Over_US))
                        {
                            strCorpSS.Append("Value_Over_US"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_Value_Over_US);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strcbo_Cor_ValueRangeEuro))
                        {
                            strCorpSS.Append("ValueRangeEuro"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_ValueRangeEuro);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strcbo_Cor_Value_Over_Euro))
                        {
                            strCorpSS.Append("Value_Over_Euro"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_Value_Over_Euro);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strtxt_Cor_Published_Reference))
                        {
                            strCorpSS.Append("Published_Reference"); strCorpSS.Append("~"); strCorpSS.Append(strtxt_Cor_Published_Reference);//[3] RoleOfLeadBank
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strcbo_Cor_MAStudy))
                        {
                            strCorpSS.Append("MAStudy"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_MAStudy);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strcbo_Cor_PEClients))
                        {
                            strCorpSS.Append("PEClients"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_PEClients);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strcbo_Cor_QuarterDealAnnouncedId))
                        {
                            strCorpSS.Append("QuarterDealAnnouncedId"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_QuarterDealAnnouncedId);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strcbo_Cor_QuarterDealCompletedId))
                        {
                            strCorpSS.Append("QuarterDealCompletedId"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_QuarterDealCompletedId);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strtxt_Cor_YearDeal_Announced))
                        {
                            strCorpSS.Append("YearDeal_Announced"); strCorpSS.Append("~"); strCorpSS.Append(strtxt_Cor_YearDeal_Announced);//[3] RoleOfLeadBank
                            strCorpSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strcbo_Cor_YearDealCompletedId))
                        {
                            strCorpSS.Append("YearDealCompletedId"); strCorpSS.Append("~"); strCorpSS.Append(strcbo_Cor_YearDealCompletedId);//[2] TransactionValueId
                            strCorpSS.Append("|");
                        }

                        string strMSCorpstr = strCorpMS.ToString();
                        string strSSCorpstr = strCorpSS.ToString();

                        if (!(string.IsNullOrEmpty(strSSCorpstr)))
                        {
                            strCorpSS = strCorpSS.Remove(strCorpSS.Length - 1, 1);
                            Session.Add("sessCORPSS", strCorpSS);
                            strCorpSS = null;
                            strSSstr = null;
                        }
                        if (!(string.IsNullOrEmpty(strMSCorpstr)))
                        {
                            strCorpMS = strCorpMS.Remove(strCorpMS.Length - 1, 1);
                            Session.Add("sessCORPMS", strCorpMS);
                            strCorpMS = null;
                            strMSstr = null;
                        }

                        if (Session["sessEPCESS"] != null)
                        {
                            Session.Remove("sessEPCESS");
                        }
                        if (Session["sessEPCEMS"] != null)
                        {
                            Session.Remove("sessEPCEMS");
                        }
                        StringBuilder strEPCESS = new StringBuilder();
                        StringBuilder strEPCEMS = new StringBuilder();


                        if (!string.IsNullOrEmpty(strhid_ENE_Transaction_Type))
                        {
                            strEPCEMS.Append("Transaction_Type"); strMS.Append("~"); strMS.Append(strhid_ENE_Transaction_Type);//[1] WorkTypeId(s)
                            strEPCEMS.Append("|");

                            strEPCESS.Append("TransactionTypeMSId"); strEPCESS.Append("~"); strEPCESS.Append(strhid_ENE_Transaction_Type);//[1] WorkTypeId(s)
                            strEPCESS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strcbo_ENE_ContractTypeId))
                        {
                            strEPCESS.Append("ContractTypeId"); strEPCESS.Append("~"); strEPCESS.Append(strcbo_ENE_ContractTypeId);//[2] TransactionValueId
                            strEPCESS.Append("|");
                        }


                        string strEPCEMSstr = strEPCEMS.ToString();
                        string strEPCESSstr = strEPCESS.ToString();

                        if (!(string.IsNullOrEmpty(strEPCESSstr)))
                        {
                            strEPCESS = strEPCESS.Remove(strEPCESS.Length - 1, 1);
                            Session.Add("sessEPCESS", strEPCESS);
                            strEPCESS = null;
                            strEPCESSstr = null;
                        }
                        if (!(string.IsNullOrEmpty(strEPCEMSstr)))
                        {
                            strEPCEMS = strEPCEMS.Remove(strEPCEMS.Length - 1, 1);
                            Session.Add("sessEPCEMS", strEPCEMS);
                            strEPCEMS = null;
                            strEPCEMSstr = null;
                        }


                        if (Session["sessIPFMS"] != null)
                        {
                            Session.Remove("sessIPFMS");
                        }
                        if (Session["sessIPFSS"] != null)
                        {
                            Session.Remove("sessIPFSS");
                        }
                        StringBuilder strIPFSS = new StringBuilder();
                        StringBuilder strIPFMS = new StringBuilder();

                        if (!string.IsNullOrEmpty(strcbo_IPF_ClientTypeIdIPF))
                        {
                            strIPFSS.Append("ClientTypeIdIPF"); strIPFSS.Append("~"); strIPFSS.Append(strcbo_IPF_ClientTypeIdIPF);//[2] TransactionValueId
                            strIPFSS.Append("|");
                        }


                        string strIPFMSstr = strIPFMS.ToString();
                        string strIPFSSstr = strIPFSS.ToString();

                        if (!(string.IsNullOrEmpty(strIPFSSstr)))
                        {
                            strIPFSS = strIPFSS.Remove(strIPFSS.Length - 1, 1);
                            Session.Add("sessIPFSS", strIPFSS);
                            strIPFSS = null;
                            strIPFSSstr = null;
                        }
                        if (!(string.IsNullOrEmpty(strIPFMSstr)))
                        {
                            strIPFMS = strIPFMS.Remove(strIPFMS.Length - 1, 1);
                            Session.Add("sessIPFMS", strIPFMS);
                            strIPFMS = null;
                            strIPFMSstr = null;
                        }

                        if (Session["sessHCSS"] != null)
                        {
                            Session.Remove("sessHCSS");
                        }
                        if (Session["sessHCMS"] != null)
                        {
                            Session.Remove("sessHCMS");
                        }

                        StringBuilder strHCSS = new StringBuilder();
                        StringBuilder strHCMS = new StringBuilder();


                        if (!string.IsNullOrEmpty(strhid_HCC_Work_Type))
                        {
                            strHCMS.Append("WorkType"); strMS.Append("~"); strMS.Append(strhid_HCC_Work_Type);
                            strHCMS.Append("|");
                            strHCSS.Append("WorkTypeIdHC"); strHCSS.Append("~"); strHCSS.Append(strhid_HCC_Work_Type);
                            strHCSS.Append("|");
                        }

                        if (!string.IsNullOrEmpty(strhid_HCC_SubWork_Type))
                        {
                            strHCMS.Append("SubWork_Type"); strMS.Append("~"); strMS.Append(strhid_HCC_SubWork_Type);
                            strHCMS.Append("|");
                            strHCSS.Append("SubWorkTypeMSId"); strHCSS.Append("~"); strHCSS.Append(strhid_HCC_SubWork_Type);
                            strHCSS.Append("|");
                        }


                        if (string.IsNullOrEmpty(strpensionSchemeHC))
                        {

                        }
                        else
                        {
                            strHCSS.Append("PensionSchemeHC"); strHCSS.Append("~"); strHCSS.Append(strpensionSchemeHC);
                            strHCSS.Append("|");
                        }


                        string strHCSSstr = strHCSS.ToString();
                        string strHCMSstr = strHCMS.ToString();
                        if (!(string.IsNullOrEmpty(strHCSSstr)))
                        {
                            strHCSS = strHCSS.Remove(strHCSS.Length - 1, 1);
                            Session.Add("sessHCSS", strHCSS);
                            strHCSS = null;
                            strHCSSstr = null;
                        }
                        if (!(string.IsNullOrEmpty(strHCMSstr)))
                        {
                            strHCMS = strHCMS.Remove(strHCMS.Length - 1, 1);
                            Session.Add("sessHCMS", strHCMS);
                            strHCMS = null;
                            strHCMSstr = null;
                        }

                        if (Session["sessREMS"] != null)
                        {
                            Session.Remove("sessREMS");
                        }
                        if (Session["sessRESS"] != null)
                        {
                            Session.Remove("sessRESS");
                        }
                        StringBuilder strRESS = new StringBuilder();
                        StringBuilder strREMS = new StringBuilder();


                        if (!string.IsNullOrEmpty(strhid_RES_Client_Type))
                        {
                            strREMS.Append("Client_Type"); strREMS.Append("~"); strREMS.Append(strhid_RES_Client_Type);
                            strREMS.Append("|");

                            strRESS.Append("ClientTypeMSId"); strRESS.Append("~"); strRESS.Append(strhid_RES_Client_Type);//[1] WorkTypeId(s)
                            strRESS.Append("|");

                        }

                        if (!string.IsNullOrEmpty(strhid_RES_Work_Type))
                        {
                            strREMS.Append("Work_Type"); strREMS.Append("~"); strREMS.Append(strhid_RES_Work_Type);
                            strREMS.Append("|");
                            /*strSS.Append("WorkTypeMS"); strSS.Append("~"); strSS.Append(dsNew.Tables[0].Rows[0][3].ToString());//[1] WorkTypeId(s)
                            strSS.Append("|");*/
                            strRESS.Append("WorkTypeMSId"); strRESS.Append("~"); strRESS.Append(strhid_RES_Work_Type);//[1] WorkTypeId(s)
                            strRESS.Append("|");
                        }


                        if (!string.IsNullOrEmpty(strHCSWT))
                        {
                            strREMS.Append("SubWork_Type"); strREMS.Append("~"); strREMS.Append(strHCSWT);
                            strREMS.Append("|");
                            /*strSS.Append("SubWorkTypeMS"); strSS.Append("~"); strSS.Append(txt_CRD_SubWork_Type.Text.Trim());//[1] WorkTypeId(s)
                            strSS.Append("|");*/
                            strRESS.Append("SubWorkTypeMSId"); strRESS.Append("~"); strRESS.Append(strHCSWT);//[1] WorkTypeId(s)
                            strRESS.Append("|");

                        }


                        string strMSREstr = strREMS.ToString();
                        string strSSREstr = strRESS.ToString();
                        if (!(string.IsNullOrEmpty(strSSREstr)))
                        {
                            strRESS = strRESS.Remove(strRESS.Length - 1, 1);
                            Session.Add("sessRESS", strRESS);
                            strRESS = null;
                            strSSREstr = null;
                        }
                        if (!(string.IsNullOrEmpty(strMSREstr)))
                        {
                            strREMS = strREMS.Remove(strREMS.Length - 1, 1);
                            Session.Add("sessREMS", strREMS);
                            strREMS = null;
                            strMSREstr = null;
                        }

                        if (Session["sessCorpTaxSS"] != null)
                        {
                            Session.Remove("sessCorpTaxSS");
                        }
                        if (Session["sessCorpTaxMS"] != null)
                        {
                            Session.Remove("sessCorpTaxMS");
                        }
                        StringBuilder strCorpTaxSS = new StringBuilder();
                        StringBuilder strCorpTaxMS = new StringBuilder();


                        if (!string.IsNullOrEmpty(strcbo_Crt_WorkType_CorpTax))
                        {
                            strCorpTaxSS.Append("WorkType_CorpTax"); strCorpTaxSS.Append("~"); strCorpTaxSS.Append(strcbo_Crt_WorkType_CorpTax); //array[0] Client TypeId
                            strCorpTaxSS.Append("|");
                        }

                        string strCorpTaxSSstr = strCorpTaxSS.ToString();

                        if (!string.IsNullOrEmpty(strCorpTaxSSstr))
                        {
                            strCorpTaxSS = strCorpTaxSS.Remove(strCorpTaxSS.Length - 1, 1);
                            Session.Add("sessCorpTaxSS", strCorpTaxSS);
                            strCorpTaxSS = null;
                            strCorpTaxSSstr = null;
                        }



                        bool blnCheck = false;
                        if (!string.IsNullOrEmpty(strClient) && !string.IsNullOrEmpty(strNameConfidentialSS) && !string.IsNullOrEmpty(strProjDesc))
                        {
                            if (strNameConfidentialSS == "1")
                            {
                                if (!string.IsNullOrEmpty(strClientDesc))
                                {
                                    blnCheck = true;
                                }
                                else
                                {
                                    blnCheck = true;
                                }
                            }
                            else
                            {
                                blnCheck = true;
                            }

                            if (blnCheck == true)
                            {

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
                                //StringBuilder strPrac = new StringBuilder();





                                if (!string.IsNullOrEmpty(strClient))
                                {
                                    strtblCredFields.Append("Client");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strClient.Trim());
                                    strtblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + "Client=" + strClient);
                                    }
                                }

                                /* Client Name Confidential */
                                if (!string.IsNullOrEmpty(strNameConfidentialSS))
                                {
                                    strtblCredFields.Append("Client_Name_Confidential");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strNameConfidentialSS);
                                    strtblCredValues.Append("','");


                                    /* Description , confidential on completion*/
                                    if (strNameConfidentialSS == "1")
                                    {
                                        if (!string.IsNullOrEmpty(strClientDesc))
                                        {
                                            strtblCredFields.Append("ClientDescription");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(strClientDesc);
                                            strtblCredValues.Append("','");

                                            if (strClient.Contains("8@9!"))
                                            {
                                                obj.LogWriter(hidCredId.Value + "ClientDescription =" + strClientDesc);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(strNameConfidentialCompSS))
                                        {
                                            strtblCredFields.Append("NameConfidential_Completion");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(strNameConfidentialCompSS);
                                            strtblCredValues.Append("','");
                                        }

                                    }
                                    else
                                    {
                                        strtblCredFields.Append("ClientDescription");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(string.Empty);
                                        strtblCredValues.Append("','");

                                        strtblCredFields.Append("NameConfidential_Completion");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(string.Empty);
                                        strtblCredValues.Append("','");
                                    }

                                }

                                /*Matter No*/
                                if (!string.IsNullOrEmpty(strMatterNo))
                                {
                                    strtblCredFields.Append("Matter_No");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strMatterNo);
                                    strtblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + "Matter_No =" + strMatterNo);
                                    }
                                }

                                /*Date Matter Opened*/
                                if (!string.IsNullOrEmpty(strDateOpened))
                                {
                                    strtblCredFields.Append("Date_Opened");
                                    strtblCredFields.Append(",");

                                    string str = strtblCredValues.ToString().Substring(0, strtblCredValues.Length - 1);
                                    strtblCredValues.Clear();
                                    strtblCredValues.Append(str);

                                    string strPurDate = "convert(datetime,'" + strDateOpened.Trim().Replace("–", "/") + "',103)";
                                    strtblCredValues.Append(strPurDate);
                                    strtblCredValues.Append(",'");
                                }

                                /*Matter Credential Description*/
                                if (!string.IsNullOrEmpty(strProjDesc))
                                {
                                    strtblCredFields.Append("Project_Description");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strProjDesc);
                                    strtblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + "Project_Description =" + strProjDesc);
                                    }
                                }


                                /*Matter Confidential */
                                if (!string.IsNullOrEmpty(strMatterConfidentialSS))
                                {
                                    strtblCredFields.Append("Client_Matter_Confidential");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strMatterConfidentialSS);
                                    strtblCredValues.Append("','");

                                    if (strMatterConfidentialSS.Trim().ToUpper() == "YES")
                                    {
                                        obj.LogWriter("rdo_Tab_MatterConfidential_Completion Starts");

                                        if (!string.IsNullOrEmpty(strstrMatterConfidentialCompSS))
                                        {
                                            strtblCredFields.Append("MatterConfidential_Completion");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(strstrMatterConfidentialCompSS);
                                            strtblCredValues.Append("','");
                                        }
                                        else
                                        {
                                            strtblCredFields.Append("MatterConfidential_Completion");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(string.Empty);
                                            strtblCredValues.Append("','");
                                        }
                                    }
                                }

                                /* Client Matter Confidential On Completion */


                                /*applicable Law*/
                                if (!string.IsNullOrEmpty(strCountryLawSS))
                                {
                                    strtblCredFields.Append("Country_Law");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strCountryLawSS);
                                    strtblCredValues.Append("','");

                                    if (strCountryLawSS.Trim().ToUpper() == "OTHER")
                                    {
                                        if (!string.IsNullOrEmpty(strCountryLawOther))
                                        {

                                            strtblCredFields.Append("Country_Law_Other");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(strCountryLawOther);
                                            strtblCredValues.Append("','");

                                            if (strClient.Contains("8@9!"))
                                            {
                                                obj.LogWriter(hidCredId.Value + " - Country_Law_Other =" + strCountryLawOther);
                                            }
                                        }
                                        else
                                        {
                                            strtblCredFields.Append("Country_Law_Other");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(string.Empty);
                                            strtblCredValues.Append("','");
                                        }

                                    }
                                }

                                /*Country Where Opened*/

                                /*Contentious/Non Contentious*/
                                if (!string.IsNullOrEmpty(strContentiousSS))
                                {
                                    strtblCredFields.Append("Contentious_IRG");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strContentiousSS);
                                    strtblCredValues.Append("','");

                                    if (strContentiousSS.Trim().ToUpper() == "BOTH" || strContentiousSS.Trim().ToUpper() == "CONTENTIOUS")
                                    {
                                        /* Dispute Resolution */
                                        strtblCredFields.Append("Dispute_Resolution");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(strDisputeResolutionSS);
                                        strtblCredValues.Append("','");

                                        if (strDisputeResolutionSS.Trim().ToUpper() == "ARBITRATION")
                                        {
                                            if (!string.IsNullOrEmpty(strSeatOfArbitrationSS))
                                            {
                                                /*City of Arbitration*/
                                                strtblCredFields.Append("ArbitrationCity");
                                                strtblCredFields.Append(",");

                                                strtblCredValues.Append(strSeatOfArbitrationSS);
                                                strtblCredValues.Append("','");
                                            }

                                            if (strSeatOfArbitrationSS.Trim().ToUpper() == "OTHER")
                                            {
                                                strtblCredFields.Append("ArbitrationCity_Other");
                                                strtblCredFields.Append(",");

                                                strtblCredValues.Append(strArbitrationCityOther);
                                                strtblCredValues.Append("','");

                                                if (strClient.Contains("8@9!"))
                                                {
                                                    obj.LogWriter(hidCredId.Value + " - ArbitrationCity_Other =" + strArbitrationCityOther);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(strArbitralRulesSS))
                                            {
                                                /*Arbitral Rules*/
                                                strtblCredFields.Append("Arbitral_Rules");
                                                strtblCredFields.Append(",");

                                                strtblCredValues.Append(strArbitralRulesSS);
                                                strtblCredValues.Append("','");
                                            }


                                        }
                                        else
                                        {
                                            strtblCredFields.Append("ArbitrationCity");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(string.Empty);
                                            strtblCredValues.Append("','");

                                            strtblCredFields.Append("ArbitrationCity_Other");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(string.Empty);
                                            strtblCredValues.Append("','");

                                            strtblCredFields.Append("Arbitral_Rules");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(string.Empty);
                                            strtblCredValues.Append("','");
                                        }

                                    }
                                    else
                                    {

                                        strtblCredFields.Append("Dispute_Resolution");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(string.Empty);
                                        strtblCredValues.Append("','");

                                        strtblCredFields.Append("ArbitrationCity");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(string.Empty);
                                        strtblCredValues.Append("','");

                                        strtblCredFields.Append("ArbitrationCity_Other");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(string.Empty);
                                        strtblCredValues.Append("','");

                                        strtblCredFields.Append("Arbitral_Rules");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(string.Empty);
                                        strtblCredValues.Append("','");

                                    }
                                }

                                /*Value Of Deal */
                                if (!string.IsNullOrEmpty(strValueofDeal.Trim()))
                                {
                                    strtblCredFields.Append("ValueOfDeal_Core");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strValueofDeal);
                                    strtblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + " - ValueOfDeal_Core =" + strValueofDeal);
                                    }
                                }

                                /*Currency Of Deal*/
                                if (!string.IsNullOrEmpty(strCurrencyOfDealeSS.Trim()))
                                {
                                    strtblCredFields.Append("Currency_Of_Deal");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strCurrencyOfDealeSS.Trim());
                                    strtblCredValues.Append("','");
                                }

                                /*Value Confidential*/
                                if (!string.IsNullOrEmpty(strValueCOnfidentialSS.Trim()))
                                {
                                    strtblCredFields.Append("Value_Confidential");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strValueCOnfidentialSS);
                                    strtblCredValues.Append("','");

                                    /* confidential on completion */
                                    if (strValueCOnfidentialSS.Trim().ToUpper() == "YES")
                                    {
                                        if (!string.IsNullOrEmpty(strValueConfidentialCompSS.Trim()))
                                        {
                                            strtblCredFields.Append("ValueConfidential_Completion");
                                            strtblCredFields.Append(",");

                                            strtblCredValues.Append(strValueConfidentialCompSS);
                                            strtblCredValues.Append("','");
                                        }
                                    }
                                    else
                                    {
                                        strtblCredFields.Append("ValueConfidential_Completion");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(string.Empty);
                                        strtblCredValues.Append("','");
                                    }
                                }


                                //string strLead = hid_Tab_Lead_Partner_Text.Value;
                                if (strLeadPartnerMS.Contains("CMS PARTNER"))
                                {
                                    /*Name Of CMS PArtner*/
                                    if (!string.IsNullOrEmpty(strCMSPartnerName))
                                    {
                                        strtblCredFields.Append("CMSPartnerName");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(strCMSPartnerName);
                                        strtblCredValues.Append("','");

                                        if (strClient.Contains("8@9!"))
                                        {
                                            obj.LogWriter(hidCredId.Value + " - CMSPartnerName =" + strCMSPartnerName);
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(strSourceOfCredOther))
                                    {
                                        strtblCredFields.Append("SourceOfCredential_Other");
                                        strtblCredFields.Append(",");

                                        strtblCredValues.Append(strSourceOfCredOther);
                                        strtblCredValues.Append("','");

                                        if (strClient.Contains("8@9!"))
                                        {
                                            obj.LogWriter(hidCredId.Value + " - SourceOfCredential_Other =" + strSourceOfCredOther);
                                        }
                                    }
                                }
                                else
                                {
                                    strtblCredFields.Append("CMSPartnerName");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(string.Empty);
                                    strtblCredValues.Append("','");

                                    strtblCredFields.Append("SourceOfCredential_Other");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(string.Empty);
                                    strtblCredValues.Append("','");

                                }

                                /* Credential Status */
                                if (!string.IsNullOrEmpty(strCredentialStatusSS))
                                {
                                    strtblCredFields.Append("Credential_Status");
                                    strtblCredFields.Append(",");

                                    strtblCredFields.Append(strCredentialStatusSS);
                                    strtblCredFields.Append("','");
                                }


                                /*Credntial Version*/
                                if (!string.IsNullOrEmpty(strCredentialVersionSS))
                                {
                                    strtblCredFields.Append("Credential_Version");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strCredentialVersionSS);
                                    strtblCredValues.Append("','");
                                }

                                /*Credntial Version other*/
                                if (!string.IsNullOrEmpty(strVersionName.Trim()))
                                {
                                    strtblCredFields.Append("Credential_Version_Other");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strVersionName);
                                    strtblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + " - Credential_Version_Other =" + strVersionName);
                                    }
                                }
                                else
                                {
                                    strtblCredFields.Append("Credential_Version_Other");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(string.Empty);
                                    strtblCredValues.Append("','");

                                }

                                /* Credential Type */
                                if (!string.IsNullOrEmpty(strCredentialTypeSS))
                                {
                                    strtblCredFields.Append("Credential_Type");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strCredentialTypeSS.Trim());
                                    strtblCredValues.Append("','");
                                }

                                /* Priority */
                                if (!string.IsNullOrEmpty(strPrioritySS))
                                {
                                    strtblCredFields.Append("Priority");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strPrioritySS);
                                    strtblCredValues.Append("','");
                                }


                                /*Keyword Search Appender*/
                                //obj.LogWriter("EntryScreen : Keyword Search Appender Starts", hidName.Value);
                                StringBuilder strAppender = new StringBuilder();

                                /*Client name*/
                                if (!string.IsNullOrEmpty(strClient.Trim()))
                                {
                                    strAppender.Append(strClient.Trim()); strAppender.Append("~");
                                }

                                /*Matter/credential description*/
                                if (!string.IsNullOrEmpty(strProjDesc))
                                {
                                    strAppender.Append(strProjDesc.Trim()); strAppender.Append("~");
                                }

                                /*Matter/credential other information*/
                                /*if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text.Trim()))
                                 {
                                     strAppender.Append(txt_Tab_Significant_Features.Text.Trim()); strAppender.Append("~");
                                 }*/

                                /*Keyword(s)/themes*/
                                if (!string.IsNullOrEmpty(strKeyword.Trim()))
                                {
                                    strAppender.Append(strKeyword.Trim()); strAppender.Append("~");
                                }



                                /*Sector and sub sectors (client and matter)*/
                                if (!string.IsNullOrEmpty(strSectorGroupMSText.Trim()))
                                {
                                    strAppender.Append(strSectorGroupMSText.Trim()); strAppender.Append("~");
                                    //strAppender.Append(txt_Tab_ClientIndustrySector.Text.Trim().Replace(",","1@2!")); strAppender.Append("~");
                                }


                                if (!string.IsNullOrEmpty(strMatterSectorGroupMSText.Trim()))
                                {
                                    strAppender.Append(strMatterSectorGroupMSText.Trim()); strAppender.Append("~");
                                    //strAppender.Append(txt_Tab_ClientIndustrySector.Text.Trim().Replace(",","1@2!")); strAppender.Append("~");
                                }



                                string strAppenderFinal = strAppender.ToString().Substring(0, strAppender.Length - 1);

                                if (!string.IsNullOrEmpty(strAppenderFinal))
                                {
                                    strtblCredFields.Append("KeyWordSearch");
                                    strtblCredFields.Append(",");

                                    strtblCredValues.Append(strAppenderFinal.Trim());
                                    strtblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + " - KeyWordSearch =" + strAppenderFinal);
                                    }
                                }

                                //obj.LogWriter("EntryScreen : Keyword Search Appender Ends", hidName.Value);

                                /* Insert into Keyword Search Table for Optimised Search Starts */

                                /*if (!string.IsNullOrEmpty(strClient.Trim()))
                                {
                                    InsertKeyWordSearchTable(strcon, strClient.Trim());
                                }*/

                                /*if (!string.IsNullOrEmpty(txt_Tab_Project_Description.Text.Trim()))
                                {
                                    InsertKeyWordSearchTable(strcon, txt_Tab_Project_Description.Text.Trim());
                                }

                               
                                 * if (!string.IsNullOrEmpty(txt_Tab_Significant_Features.Text.Trim()))
                                {
                                    InsertKeyWordSearchTable(strcon, txt_Tab_Significant_Features.Text.Trim());
                                }*/

                                /*if (!string.IsNullOrEmpty(strKeyword.Trim()))
                                {
                                    InsertKeyWordSearchTable(strcon, strKeyword.Trim());
                                }*/

                                /* Insert into Keyword Search Table for Optimised Search Ends */




                                /*optional fields*/

                                /*Other Matter Description*/
                                if (!string.IsNullOrEmpty(strProjDesc.Trim())) //strOtherMatterDesc
                                {
                                    strOptionaltblCredFields.Append("Significant_Features");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(strOtherMatterDesc.Trim()); // strOtherMatterDesc
                                    strOptionaltblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + " - Significant_Features =" + strOtherMatterDesc); //strOtherMatterDesc
                                    }
                                }
                                else
                                {
                                    strOptionaltblCredFields.Append("Significant_Features");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(string.Empty);
                                    strOptionaltblCredValues.Append("','");
                                }

                                /*Keyword*/
                                if (!string.IsNullOrEmpty(strKeyword.Trim()))
                                {
                                    strOptionaltblCredFields.Append("Keyword");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(strKeyword.Trim());
                                    strOptionaltblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + " - Keyword =" + strKeyword);
                                    }
                                }
                                else
                                {
                                    strOptionaltblCredFields.Append("Keyword");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(string.Empty);
                                    strOptionaltblCredValues.Append("','");
                                }

                                /*Actual Date if checked saves in ActualDate_Ongoing else Date_Completed */
                                if (strDateComp.ToUpper().Trim() == "ONGOING")
                                {
                                    if (!string.IsNullOrEmpty(strDateComp))
                                    {
                                        strOptionaltblCredFields.Append("ActualDate_Ongoing");
                                        strOptionaltblCredFields.Append(",");

                                        strOptionaltblCredValues.Append(strDateComp.Trim());
                                        strOptionaltblCredValues.Append("','");
                                    }

                                }//Not known
                                else if (strDateComp.ToUpper().Trim() == "NOT KNOWN")
                                {
                                    if (!string.IsNullOrEmpty(strDateComp))
                                    {
                                        strOptionaltblCredFields.Append("ActualDate_Ongoing");
                                        strOptionaltblCredFields.Append(",");

                                        strOptionaltblCredValues.Append(strDateComp.Trim());
                                        strOptionaltblCredValues.Append("','");
                                    }

                                }
                                else
                                {
                                    strOptionaltblCredFields.Append("ActualDate_Ongoing");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(string.Empty);
                                    strOptionaltblCredValues.Append("','");

                                    if (!string.IsNullOrEmpty(strDateComp))
                                    {
                                        if (!string.IsNullOrEmpty(strDateComp.Trim()))
                                        {
                                            strOptionaltblCredFields.Append("Date_Completed");
                                            strOptionaltblCredFields.Append(",");

                                            string str = strOptionaltblCredValues.ToString().Substring(0, strOptionaltblCredValues.Length - 1);
                                            strOptionaltblCredValues.Clear();
                                            strOptionaltblCredValues.Append(str);

                                            string strPurDate = "convert(datetime,'" + strDateComp.Trim().Replace("–", "/") + "',103)";
                                            strOptionaltblCredValues.Append(strPurDate);
                                            strOptionaltblCredValues.Append(",'");


                                            /*strOptionaltblCredValues.Append(txt_Tab_Date_Completed.Text.Trim());
                                            strOptionaltblCredValues.Append("','");*/
                                        }
                                    }
                                }

                                /* Project Name */
                                if (!string.IsNullOrEmpty(strProjectName.Trim()))
                                {
                                    strOptionaltblCredFields.Append("ProjectName_Core");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(strProjectName.Trim());
                                    strOptionaltblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + " - ProjectName_Core =" + strProjectName);
                                    }
                                }
                                else
                                {
                                    strOptionaltblCredFields.Append("ProjectName_Core");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(string.Empty);
                                    strOptionaltblCredValues.Append("','");
                                }
                                if (strContentiousSS.Trim().ToUpper() == "BOTH" || strContentiousSS.Trim().ToUpper() == "CONTENTIOUS")
                                {
                                    /*language of disptute*/
                                    if (!string.IsNullOrEmpty(stLanguageOfDisputeMS.Trim()))
                                    {
                                        //string strLanguageOfDispute = hid_Tab_Language_Of_Dispute_Other.Value;
                                        if (stLanguageOfDisputeMS.Trim().ToUpper().Contains("OTHER"))
                                        {
                                            strOptionaltblCredFields.Append("Language_Of_Dispute_Other");
                                            strOptionaltblCredFields.Append(",");

                                            strOptionaltblCredValues.Append(stLanguageOfDisputeMS.Trim());
                                            strOptionaltblCredValues.Append("','");
                                        }
                                        else
                                        {
                                            strOptionaltblCredFields.Append("Language_Of_Dispute_Other");
                                            strOptionaltblCredFields.Append(",");

                                            strOptionaltblCredValues.Append(string.Empty);
                                            strOptionaltblCredValues.Append("','");
                                        }
                                    }

                                    /*Investment treaty*/
                                    if (strDisputeResolutionSS.Trim().ToUpper() == "ARBITRATION")
                                    {
                                        if (!string.IsNullOrEmpty(strInvestmentTreatySS))
                                        {
                                            strOptionaltblCredFields.Append("InvestmentTreaty");
                                            strOptionaltblCredFields.Append(",");

                                            strOptionaltblCredValues.Append(strInvestmentTreatySS.Trim());
                                            strOptionaltblCredValues.Append("','");
                                        }
                                        else
                                        {
                                            strOptionaltblCredFields.Append("InvestmentTreaty");
                                            strOptionaltblCredFields.Append(",");

                                            strOptionaltblCredValues.Append(string.Empty);
                                            strOptionaltblCredValues.Append("','");

                                        }
                                    }

                                    //obj.LogWriter("cbo_Tab_Investigation_Type Starts");
                                    /*Investigation*/
                                    if (strDisputeResolutionSS.Trim().ToUpper() == "INVESTIGATION")
                                    {
                                        if (!string.IsNullOrEmpty(strInvestigationTypeSS))
                                        {
                                            strOptionaltblCredFields.Append("Investigation_Type");
                                            strOptionaltblCredFields.Append(",");

                                            strOptionaltblCredValues.Append(strInvestigationTypeSS.Trim());
                                            strOptionaltblCredValues.Append("','");
                                        }
                                        else
                                        {
                                            strOptionaltblCredFields.Append("Investigation_Type");
                                            strOptionaltblCredFields.Append(",");

                                            strOptionaltblCredValues.Append(string.Empty);
                                            strOptionaltblCredValues.Append("','");

                                        }

                                    }
                                }

                                /*Lead CMS Firms*/
                                if (!string.IsNullOrEmpty(strLeadCMSFirmSS))
                                {
                                    strOptionaltblCredFields.Append("Lead_CMS_Firm");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(strLeadCMSFirmSS.Trim());
                                    strOptionaltblCredValues.Append("','");
                                }
                                else
                                {
                                    strOptionaltblCredFields.Append("Lead_CMS_Firm");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(string.Empty);
                                    strOptionaltblCredValues.Append("','");
                                }

                                /*Pro Bono*/
                                if (!string.IsNullOrEmpty(strProbonoSS))
                                {
                                    strOptionaltblCredFields.Append("ProBono");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(strProbonoSS.Trim());
                                    strOptionaltblCredValues.Append("','");
                                }
                                else
                                {
                                    strOptionaltblCredFields.Append("ProBono");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(string.Empty);
                                    strOptionaltblCredValues.Append("','");
                                }

                                /*Bible Reference*/
                                if (!string.IsNullOrEmpty(strBibleRef))
                                {
                                    strOptionaltblCredFields.Append("Bible_Reference");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(strBibleRef.Trim());
                                    strOptionaltblCredValues.Append("','");

                                    if (strClient.Contains("8@9!"))
                                    {
                                        obj.LogWriter(hidCredId.Value + " - Bible_Reference =" + strBibleRef);
                                    }
                                }
                                else
                                {
                                    strOptionaltblCredFields.Append("Bible_Reference");
                                    strOptionaltblCredFields.Append(",");

                                    strOptionaltblCredValues.Append(string.Empty);
                                    strOptionaltblCredValues.Append("','");

                                }


                                /*BAIF*/

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
                                                                strBAIFtblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strBAIFtblCredFields.Append(",");

                                                                strBAIFtblCredValues.Append(string.Empty);
                                                                strBAIFtblCredValues.Append("','");

                                                            }

                                                            break;
                                                        case "LeadBanks":
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strBAIFtblCredFields.Append(",");

                                                                strBAIFtblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strBAIFtblCredValues.Append("','");

                                                                if (strClient.Contains("8@9!"))
                                                                {
                                                                    obj.LogWriter(hidCredId.Value + " - " + strvals[i].Split('~')[0] + "=" + strvals[i].Split('~')[1]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                strBAIFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strBAIFtblCredFields.Append(",");

                                                                strBAIFtblCredValues.Append(string.Empty);
                                                                strBAIFtblCredValues.Append("','");

                                                            }
                                                            break;
                                                        case "WorkTypeMS":
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }

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

                                                        case "Value_Over_US":
                                                            //objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Value_Over_US);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "Value_Over_Pound":
                                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Value_Over_Pound);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");

                                                            }
                                                            break;
                                                        case "ValueRangeEuro":
                                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_ValueRangeEuro);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "Value_Over_Euro":
                                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_Value_Over_Euro);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;

                                                        case "Published_Reference":
                                                            //txt_Cor_Published_Reference.Text = strvals[i].Split('~')[1].ToString();
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");

                                                                if (strClient.Contains("8@9!"))
                                                                {
                                                                    obj.LogWriter(hidCredId.Value + " - " + strvals[i].Split('~')[0] + "=" + strvals[i].Split('~')[1]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "MAStudy":
                                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_MAStudy);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "PEClients":
                                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_PEClients);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "QuarterDealAnnouncedId":
                                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_QuarterDealAnnouncedId);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "QuarterDealCompletedId":
                                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_QuarterDealCompletedId);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "YearDeal_Announced":
                                                            // txt_Cor_YearDeal_Announced.Text = strvals[i].Split('~')[1].ToString();
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "YearDealCompletedId":
                                                            //  objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_YearDealCompletedId);
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorptblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorptblCredFields.Append(",");

                                                                strCorptblCredValues.Append(string.Empty);
                                                                strCorptblCredValues.Append("','");
                                                            }
                                                            break;

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

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
                                                                strCRDtblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCRDtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCRDtblCredFields.Append(",");

                                                                strCRDtblCredValues.Append(string.Empty);
                                                                strCRDtblCredValues.Append("','");
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
                                                                strEPCtblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(string.Empty);
                                                                strEPCtblCredValues.Append("','");

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
                                                                strEPCtblCredValues.Append("','");

                                                                if (strClient.Contains("8@9!"))
                                                                {
                                                                    obj.LogWriter(hidCredId.Value + " - " + strvals[i].Split('~')[0] + "=" + strvals[i].Split('~')[1]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(string.Empty);
                                                                strEPCtblCredValues.Append("','");

                                                            }
                                                            break;
                                                        case "SubjectMatterId":
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strEPCtblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(string.Empty);
                                                                strEPCtblCredValues.Append("','");

                                                            }
                                                            break;
                                                        case "SubjectMatterOther":
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strEPCtblCredValues.Append("','");

                                                                if (strClient.Contains("8@9!"))
                                                                {
                                                                    obj.LogWriter(hidCredId.Value + " - " + strvals[i].Split('~')[0] + "=" + strvals[i].Split('~')[1]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(string.Empty);
                                                                strEPCtblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "ClientTypeIdEPC":
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strEPCtblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(string.Empty);
                                                                strEPCtblCredValues.Append("','");
                                                            }
                                                            break;
                                                        case "ClientTypeOther":
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(strvals[i].Split('~')[1]);
                                                                strEPCtblCredValues.Append("','");

                                                                if (strClient.Contains("8@9!"))
                                                                {
                                                                    obj.LogWriter(hidCredId.Value + " - " + strvals[i].Split('~')[0] + "=" + strvals[i].Split('~')[1]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                strEPCtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCtblCredFields.Append(",");

                                                                strEPCtblCredValues.Append(string.Empty);
                                                                strEPCtblCredValues.Append("','");
                                                            }
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

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
                                                                strEPCEnetblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strEPCEnetblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strEPCEnetblCredFields.Append(",");

                                                                strEPCEnetblCredValues.Append(string.Empty);
                                                                strEPCEnetblCredValues.Append("','");
                                                            }
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

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

                                                        case "PensionSchemeHC":
                                                            if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
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
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

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
                                                                strIPFtblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strIPFtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strIPFtblCredFields.Append(",");

                                                                strIPFtblCredValues.Append(string.Empty);
                                                                strIPFtblCredValues.Append("','");
                                                            }
                                                            // objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_IPF_ClientTypeIdIPF);
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

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
                                                                strCorpTaxtblCredValues.Append("','");
                                                            }
                                                            else
                                                            {
                                                                strCorpTaxtblCredFields.Append(strvals[i].Split('~')[0]);
                                                                strCorpTaxtblCredFields.Append(",");

                                                                strCorpTaxtblCredValues.Append(string.Empty);
                                                                strCorpTaxtblCredValues.Append("','");
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

                                obj.LogWriter("Save in tblCredential Starts");
                                int iCredentialID = 0;
                                if (!string.IsNullOrEmpty(strqueryvals.ToString()) && !string.IsNullOrEmpty(strquerycols.ToString()))
                                {
                                    iCredentialID = SaveCredentials(strqueryvals, strquerycols);
                                }

                                hidCredId.Value = iCredentialID.ToString();

                                obj.LogWriter("CredentialID : " + iCredentialID.ToString());

                                /*obj.LogWriter("Save in tblCredentialVersionRelation Starts");

                                string strcon1 = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
                                SqlConnection con1 = new SqlConnection(strcon);
                                con1.Open();

                                //objLogger.LogWriter("Insert starts");

                                string strID = string.Concat(Convert.ToString(iCredentialID), "0000");

                                string sqlquery1 = "insert into tblCredentialVersionRelation(CredentialRelationID,CredentialID,CredentialVersion,MatterNo,CredentialMasterID) values('" + strID + "','" + iCredentialID + "','" + cbo_Tab_Credential_Version.SelectedItem.Text.Trim() + "','" + txt_Tab_Matter_No.Text.Trim() + "','" + iCredentialID + "')";
                               

                                //objLogger.LogWriter(sqlquery1);
                                SqlCommand cmd1 = new SqlCommand(sqlquery1, con1);
                                int i1 = cmd1.ExecuteNonQuery();
                                //objLogger.LogWriter("Insert ends");
                                cmd1.Dispose(); con1.Close();

                                obj.LogWriter("Save in tblCredentialVersionRelation Starts");*/

                                /* if (!string.IsNullOrEmpty(hidCredentialID.Value.Trim()))
                                 {
                                     obj.LogWriter(" EntryScreen : btnAddBottom_Click Save in tblCredential Ends");

                                     string strcon1 = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
                                     SqlConnection con1 = new SqlConnection(strcon);
                                     con1.Open();

                                     string strQuery01 = "select CredentialRelationID from tblCredentialVersionRelation where credentialid='" + hidCredentialID.Value.Trim() + "'";
                                     SqlDataAdapter adp1 = new SqlDataAdapter(strQuery01, con1);
                                     DataSet ds1 = new DataSet();
                                     adp1.Fill(ds1);
                                     string strCredentialRelationID = string.Empty;
                                     if (ds1.Tables[0].Rows.Count > 0)
                                     {
                                         strCredentialRelationID = ds1.Tables[0].Rows[0][0].ToString();
                                     }
                                     adp1.Dispose(); ds1.Dispose();

                                     string strQuery02 = "select credentialid from tblCredentialVersionRelation where credentialversion='Master' and CredentialRelationID='" + strCredentialRelationID + "'";
                                     SqlDataAdapter adp2 = new SqlDataAdapter(strQuery02, con1);
                                     DataSet ds2 = new DataSet();
                                     adp2.Fill(ds2);
                                     string strCredentialID = string.Empty;
                                     if (ds2.Tables[0].Rows.Count > 0)
                                     {
                                         strCredentialID = ds2.Tables[0].Rows[0][0].ToString();
                                     }
                                     adp2.Dispose(); ds2.Dispose();


                                     string strQuery05 = "select * from tblCredentialVersionRelation where credentialrelationid !='" + strCredentialRelationID + "'  and matterno='" + txt_Tab_Matter_No.Text.Trim() + "'";
                                     SqlDataAdapter adp5 = new SqlDataAdapter(strQuery05, con1);
                                     DataSet ds5 = new DataSet();
                                     adp5.Fill(ds5);

                                     if (ds5.Tables[0].Rows.Count > 0)
                                     {
                                         System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                         sb.Append(@"<script language='javascript'>");
                                         sb.Append(@"alert('Matter Number already exists for other Master/Other Credentials.');");
                                         sb.Append(@"</script>");
                                         ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", sb.ToString(), false);
                                     }
                                     else
                                     {
                                         string strQuery03 = "insert into tblCredentialVersionRelation(CredentialRelationID,CredentialID,CredentialVersion,MatterNo) values('" + strCredentialRelationID + "','" + iCredentialID + "','" + cbo_Tab_Credential_Version.SelectedItem.Text.Trim() + "','" + txt_Tab_Matter_No.Text.Trim() + "' )";
                                         SqlCommand cmd3 = new SqlCommand(strQuery03, con1);
                                         int i3 = cmd3.ExecuteNonQuery();
                                         cmd3.Dispose();

                                         if (cbo_Tab_Credential_Version.SelectedItem.Text.Trim().ToUpper() == "MASTER")
                                         {
                                             string strQuery04 = "update tblCredentialVersionRelation set CredentialVersion = 'Other' where credentialid='" + strCredentialID + "'";
                                             SqlCommand cmd4 = new SqlCommand(strQuery04, con1);
                                             int i4 = cmd4.ExecuteNonQuery(); cmd4.Dispose();

                               

                                             string strQ1 = "update tblCredentialVersionRelation set CredentialMasterID = '" + iCredentialID + "' where CredentialRelationID='" + strCredentialRelationID + "'";
                                             SqlCommand cmdQ1 = new SqlCommand(strQ1, con1);
                                             int iQ1 = cmdQ1.ExecuteNonQuery(); cmdQ1.Dispose();
  

                                             string strQuery06 = "select CredentialID,CredentialVersion from tblCredentialVersionRelation where CredentialRelationID='" + strCredentialRelationID + "'";
                                             SqlDataAdapter adp6 = new SqlDataAdapter(strQuery06, con1);
                                             DataSet ds6 = new DataSet();
                                             adp6.Fill(ds6);
                                             string strMasterCredentialID = string.Empty;
                                             string strChildCredentialID = string.Empty;
                                             if (ds6.Tables[0].Rows.Count > 0)
                                             {
                                                 for (int i = 0; i < ds6.Tables[0].Rows.Count; i++)
                                                 {
                                                     if (ds6.Tables[0].Rows[i][1].ToString().ToUpper() == "MASTER")
                                                     {
                                                         strMasterCredentialID = "'" + ds6.Tables[0].Rows[i][0].ToString() + "'";
                                                     }
                                                     else
                                                     {
                                                         if (string.IsNullOrEmpty(strChildCredentialID))
                                                         {
                                                             strChildCredentialID = "'" + ds6.Tables[0].Rows[i][0].ToString() + "',";
                                                         }
                                                         else
                                                         {
                                                             strChildCredentialID = strChildCredentialID + "'" + ds6.Tables[0].Rows[i][0].ToString() + "',";
                                                         }
                                                     }
                                                 }
                                             }
                                             adp6.Dispose(); ds6.Dispose();


                                             string strQuery07 = "update tblCredential set Credential_Version = '1' where credentialid in(" + strMasterCredentialID + ")";
                                             SqlCommand cmd7 = new SqlCommand(strQuery07, con1);
                                             int i7 = cmd7.ExecuteNonQuery(); cmd7.Dispose();

                                             string strQuery08 = "update tblCredential set Credential_Version = '2' where credentialid in(" + strChildCredentialID.Substring(0, strChildCredentialID.Length - 1) + ")";
                                             SqlCommand cmd8 = new SqlCommand(strQuery08, con1);
                                             int i8 = cmd8.ExecuteNonQuery(); cmd8.Dispose();
                                         }
                                         else
                                         {
                               


                                             string strzQuery02 = "select CredentialMasterID from tblCredentialVersionRelation where credentialversion='Master' and CredentialRelationID='" + strCredentialRelationID + "'";
                                             SqlDataAdapter zadp2 = new SqlDataAdapter(strzQuery02, con1);
                                             DataSet zds2 = new DataSet();
                                             zadp2.Fill(zds2);
                                             string strzCredentialID = string.Empty;
                                             if (zds2.Tables[0].Rows.Count > 0)
                                             {
                                                 strzCredentialID = zds2.Tables[0].Rows[0][0].ToString();
                                             }
                                             zadp2.Dispose(); zds2.Dispose();


                                             string strQ1 = "update tblCredentialVersionRelation set CredentialMasterID = '" + strzCredentialID + "' where CredentialRelationID='" + strCredentialRelationID + "'";
                                             SqlCommand cmdQ1 = new SqlCommand(strQ1, con1);
                                             int iQ1 = cmdQ1.ExecuteNonQuery(); cmdQ1.Dispose();

                                
                                         }
                                     }

                                     adp5.Dispose(); ds5.Dispose();

                                 }*/

                                if (iCredentialID > 0)
                                {
                                    obj.LogWriter("Save in tblCredentialVersionRelation Starts");
                                    string strCredentialVersion = string.Empty;
                                    if (strCredentialVersionSS == "1")
                                    {
                                        strCredentialVersion = "Master";
                                    }
                                    else
                                    {
                                        strCredentialVersion = "Other";
                                    }


                                    string strcon1 = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
                                    SqlConnection con1 = new SqlConnection(strcon);
                                    con1.Open();

                                    string strID = string.Concat(Convert.ToString(iCredentialID), "0000");

                                    string sqlquery1 = "insert into tblCredentialVersionRelation(CredentialRelationID,CredentialID,CredentialVersion,MatterNo,CredentialMasterID) values('" + strID + "','" + iCredentialID + "','" + strCredentialVersion.Trim() + "','" + strMatterNo.Trim() + "','" + iCredentialID + "')";
                                    //string sqlquery1 = "insert into tblCredentialVersionRelation(CredentialRelationID,CredentialID,CredentialVersion,MatterNo) values('" + strID + "','" + iCredentialID + "','" + cbo_Tab_Credential_Version.SelectedItem.Text.Trim() + "','" + txt_Tab_Matter_No.Text.Trim() + "' )";

                                    obj.LogWriter(sqlquery1);
                                    obj.LogWriter("CredentialRelationID =" + strID + ",CredentialID=" + iCredentialID + ",CredentialVersion =" + strCredentialVersionSS + ",CredentialMasterID=" + iCredentialID);
                                    SqlCommand cmd1 = new SqlCommand(sqlquery1, con1);
                                    int i1 = cmd1.ExecuteNonQuery();
                                    cmd1.Dispose(); con1.Close();

                                    obj.LogWriter("Save in tblCredentialVersionRelation ends");


                                    /*sector group*/


                                    string strMainInsert = string.Empty;

                                    if (!string.IsNullOrEmpty(strPrac.ToString()))
                                    {
                                        //string strPracGrpId = strPrac.ToString().Trim().Substring(0, strPrac.Length - 1);
                                        if (strPrac.Split(',').Length > 0)
                                        {
                                            for (int i = 0; i < strPrac.Split(',').Length; i++)
                                            {
                                                string strOP = InsertingMultiSelectValues(strPrac.Split(',')[i].ToString(), "BusinessGroupId", "tblCredentialBusinessGroup", iCredentialID);

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
                                    if (strContentiousSS.Trim().ToUpper() == "BOTH" || strContentiousSS.Trim().ToUpper() == "CONTENTIOUS")
                                    {

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
                                    if (strLeadPartnerMS.Contains("CMS PARTNER"))
                                    {

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


                                    /* Matter Sub-Sector Group */
                                    if (!string.IsNullOrEmpty(strMatterSubSectorGroupMS))
                                    {
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

                                    if (!string.IsNullOrEmpty(strTeamMS))
                                    {
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

                                    if (!string.IsNullOrEmpty(strCMSFirmsInvolvedMS))
                                    {
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

                                    if (!string.IsNullOrEmpty(strCountryOtherCMSOfficeMS))
                                    {
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

                                    if (!string.IsNullOrEmpty(strOtherUsesMS))
                                    {
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
                                    if (!string.IsNullOrEmpty(strKnowHowMS))
                                    {
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
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

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
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            case "Acting_For":
                                                                string strActingForCorpMS = string.Empty;
                                                                strActingForCorpMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            case "SubWork_Type":
                                                                string strSubWork_TypeCorpMS = string.Empty;
                                                                strSubWork_TypeCorpMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            case "Country_Buyer":
                                                                string strCountryBuyerCorpMS = string.Empty;
                                                                strCountryBuyerCorpMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            case "Country_Seller":
                                                                string strCountrySellerCorpMS = string.Empty;
                                                                strCountrySellerCorpMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            case "Country_Target":
                                                                string strCountryTargetCorpMS = string.Empty;
                                                                strCountryTargetCorpMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

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
                                                                if (!string.IsNullOrEmpty(strWorkTypeCRDMS))
                                                                {
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
                                                                }
                                                                break;
                                                            case "SubWork_Type":
                                                                string strSubWorkTypeCRDMS = string.Empty;
                                                                strSubWorkTypeCRDMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            case "Type_Of_Contract":
                                                                string strTypeOfContractEPCMS = string.Empty;
                                                                strTypeOfContractEPCMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            case "Transaction_Type":
                                                                string strTransactionTypeEPCEMS = string.Empty;
                                                                strTransactionTypeEPCEMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            case "Work_Type":
                                                                string strWork_TypeREMS = string.Empty;
                                                                strWork_TypeREMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            //SubWork_Type
                                                            case "SubWork_Type":
                                                                string strSubWork_TypeREMS = string.Empty;
                                                                strSubWork_TypeREMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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
                                                                }
                                                                break;
                                                            //SubWork_Type
                                                            case "SubWork_Type":
                                                                string strSubWork_TypeHCMS = string.Empty;
                                                                strSubWork_TypeHCMS = strvals[i].Split('~')[1];
                                                                if (!string.IsNullOrEmpty(strvals[i].Split('~')[1]))
                                                                {
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

                                    obj.LogWriter("Multiselect save in DB Starts" + hidCredId.Value);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Connection = con;
                                    cmd.Transaction = transaction;
                                   // obj.LogWriter("Main Insert : " + strMainInsert.ToString());
                                    if (!string.IsNullOrEmpty(strMainInsert))
                                    {
                                        for (int k = 0; k < strMainInsert.Split('|').Length; k++)
                                        {
                                            cmd.CommandText = strMainInsert.Split('|')[k].ToString();
                                            int j = cmd.ExecuteNonQuery();
                                           // obj.LogWriter("Multiselect : " + strMainInsert.Split('|')[k].ToString());
                                        }
                                    }
                                    transaction.Commit();
                                    obj.LogWriter("Multiselect save in DB Stops" + hidCredId.Value);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        obj.LogWriter("ERROR !!! : " + ex.Message);
                        DataRow drErrorRecords = dtErrorRecords.NewRow();
                        drErrorRecords.ItemArray = dt.Rows[l].ItemArray;
                        //drErrorRecords.AcceptChanges();
                        dtErrorRecords.Rows.Add(drErrorRecords);
                        dtErrorRecords.AcceptChanges();
                        //throw ex;
                    }

                } //end of for

                workbook.Close(true, Missing.Value, Missing.Value);
                appExl.Quit();
                Gridview1.DataSource = dtErrorRecords;//DataSource to GrigView(Id:gvOne)
                Gridview1.DataBind();

                //ExportGridView();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control); 
        }

        public string GetMultiSelectValueId(string strtemptbl, string ColId, string ColValue, string strValue)
        {
            //(REPLACE('Real Estate – Transaction','–','-'))
           if (strValue.Trim() == "Owner/Occupier Sale and Purchase")
            {
                strValue = "Owner / Occupier Sale and Purchase";
            }
            if (strValue.Trim() == "Insurance / Re-Insurance/Life/ General insurance")
            {
                strValue = "Insurance / Re - Insurance / Life / General insurance";
            }
            if (strValue.Trim() == "Transport - Shipping/maritime")
            {
                strValue = "Transport - Shipping / maritime";
            }
            if (strValue.Trim() == "Price, Huw")
            {
                strValue = "Price , Huw";
            }
            if (strValue.Trim() == "Human Capital")
            {
                strValue = "Human capital (HC)";
            }
            if (strValue.Trim() == "Art/Museums/Theatres")
            {
                strValue = "Art / Museums / Theatres";
            }
          
            if (strValue.Trim() == "Insurance/ Re-Insurance/Life/General Insurance")
            {
                strValue = "Insurance / Re - Insurance / Life / General Insurance";
            }
          
            if (strValue.Trim() == "Sofia")
            {
                strValue = "CMS CMcK (UK & CEE)";
            } //Transport - Airports/Aviation
            if (strValue.Trim() == "Transport - Airports/Aviation")
            {
                strValue = "Transport - Airports / Aviation";
            }
            if (strValue.Trim() == "Brokers/traders")
            {
                strValue = "Brokers / traders";
            }
            if (strValue.Trim() == "Electricity/gas networks")
            {
                strValue = "Electricity / gas networks";
            }
           
            if (strValue.Trim() == "Transport - Airports/Aviation")
            {
                strValue = "Transport - Airports / Aviation";
            }
            if (strValue.Trim() == "Electricity - commercial/regulatory")
            {
                strValue = "Electricity - commercial / regulatory";
            }
            if (strValue.Trim() == "Freight/warehouse/logistics")
            {
                strValue = "Freight / warehouse / logistics";
            }
            if (strValue.Trim() == "Other (please specify)" && strtemptbl == "tblTypeContract") //tblTypeContract
            {
                strValue = "Others (Please specify)";
            }
            if (strValue.Trim() == "Architect/surveyor")
            {
                strValue = "Architect / surveyor";
            }
           
            if (strValue.Trim() == "Clean energy  - Biomass")
            {
                strValue = "Clean energy - Biomass";
            }
            if (strValue.Trim() == "Electricity/gas networks")
            {
                strValue = "Electricity / gas networks";
            }
    
            if (strValue.Trim() == "Gas - Shale gas/other unconventional gas")
            {
                strValue = "Gas - Shale gas / other unconventional gas";
            }
            if (strValue.Trim() == "Leisure: Pubs")
            {
                strValue = "Leisure : Pubs";
            }
            if (strValue.Trim() == "Warehouse/logistics")
            {
                strValue = "Warehouse / Logistics";
            }
           
            if (strValue.Trim() == "Central/Local Government Sector")//Central/Local government sector
            {
                strValue = "Central / Local government sector";
            }
            if (strValue.Trim() == "Forward Funding/Forward Purchase")
            {
                strValue = "Forward funding / Forward purchase";
            }
            if (strValue.Trim() == "UK institutional investors/property companies")
            {
                strValue = "UK institutional investors / Property companies";
            }//Leisure: Others CMS CMCK (UK)
            if (strValue.Trim() == "Leisure: Others")
            {
                strValue = "Leisure : Others";
            }
            if (strValue.Trim() == "CMS CMCK (UK)")
            {
                strValue = "CMS CMcK (UK & CEE)";
            }
            if (strValue.Trim() == "Real estate - Planning: Marine consents" && strtemptbl == "tblWorkType") //tblTypeContract
            {
                strValue = "Marine consents";
                strtemptbl = "tblSubWorkType";
                ColId = "SubWorkTypeId";
                ColValue = "SubWork_Type";
                //tblSubWorkType
            }
            if (strValue.Trim() == "Real Estate - Planning: Applications including EIA" && strtemptbl == "tblWorkType") //tblTypeContract
            {
                strValue = "Applications including EIA";
                strtemptbl = "tblSubWorkType";
                ColId = "SubWorkTypeId";
                ColValue = "SubWork_Type";
                //tblSubWorkType
            }
            if (strValue.Trim() == "Real estate - Planning: Planning, highways and other agreements" && strtemptbl == "tblWorkType") //tblTypeContract
            {
                strValue = "Planning, Highways and other agreements";
                strtemptbl = "tblSubWorkType";
                ColId = "SubWorkTypeId";
                ColValue = "SubWork_Type";
                //tblSubWorkType
            }

            if (strValue.Trim() == "EPC Energy")
            {
                strValue = "EPC - Energy";
            }

            if (strValue.Trim() == "M&A")
            {
                strValue = "M & A";
            }
            
            if (strValue.Trim() == "Other  (specify in other credential information)")
            {
                strValue = "Other (Specify in other credential information)";
            }
            if (strValue.Trim() == "Other - Energy/commodity market")
            {
                strValue = "Other - Energy / commodity market";
            }
            if (strValue.Trim() == "Insurance - Lloyds brokers")
            {
                strValue = "Insurance - Lloyds broker";
            }
            if (strValue.Trim() == "Lloyd''s/London Market/Broker")
            {
                strValue = "Lloyd''s / London Market / Broker";
            }            
            
            if (strValue.Trim() == "Freight/warehouse/logistics")
            {
                strValue = "Freight / warehouse / logistics";
            }
            if (strValue.Trim() == "Other (please specify)" && strtemptbl == "tblTypeContract") //tblTypeContract
            {
                strValue = "Others (Please specify)";
            }
            if (strValue.Trim() == "Architect/surveyor")
            {
                strValue = "Architect / surveyor";
            }
           
            if (strValue.Trim() == "Clean energy  - Biomass")
            {
                strValue = "Clean energy - Biomass";
            }
            if (strValue.Trim() == "Electricity/gas networks")
            {
                strValue = "Electricity / gas networks";
            }
            if (strValue.Trim() == "M&A")
            {
                strValue = "M & A";
            }
            if (strValue.Trim() == "EPC Energy")
            {
                strValue = "EPC - Energy";
            }
            if (strValue.Trim().ToUpper() == "CEE - PRAGUE  (REAL ESTATE)")
            {
                strValue = "CEE - Prague (Real Estate)";
            }
            if (strValue.Trim() == "Other:")
            {
                strValue = "Other (Please specify)";
            }
            if (strValue.Trim() == "Securities/Derivatives Trader")
            {
                strValue = "Securities / Derivatives Trader";
            }
            if (strValue.Trim() == "Warehouse/ logistics")
            {
                strValue = "Warehouse / logistics";
            }
            if (strValue.Trim() == "Clothing/Textiles")
            {
                strValue = "Clothing / Textiles";
            }
            if (strValue.Trim() == "McDowell, Hilary")
            {
                strValue = "Mc Dowell , Hilary";
            }
            if (strValue.Trim().ToUpper() == "CMS CMCK (UK)")
            {
                strValue = "CMS CMcK (UK & CEE)";
            }//Conventional power /coal
            if (strValue.Trim() == "Conventional power /coal")
            {
                strValue = "Conventional power / coal";
            }
            if (strValue.Trim() == "CMS RPA(portugal)")
            {
                strValue = "CMS RPA (Portugal)";
            }
            if (strValue.Trim().ToUpper() == "CMS REICH-ROHRWIG HAINZ RECHTANWÄLTE GMBH")
            {
                strValue = "CMS RRH (Austria)";
            }
            if (strValue.Trim() == "Sponsor/Project Company")
            {
                strValue = "Sponsor / Project Company";
            }
            if (strValue.Trim().ToUpper() == "CEE - WARSAW (CRD- LITIGATION)")
            {
                strValue = "CEE - Warsaw (CRD - Litigation)";
            }
            if (strValue.Trim().ToUpper() == "SUB-CONTRACTOR")
            {
                strValue = "Subcontractor";
            }
            if (strValue.Trim().ToUpper() == "OTHER (PLEASE SPECIFY)" && strtemptbl == "tblTypeContract")
            {
                strValue = "Other:";
            }
            if (strValue.Trim().ToUpper() == "BAIF - ASSET BASED LENDING, ASSET & STRUCTURED FINANCE")
            {
                strValue = "BAIF - Asset Based Lending , Asset & Structured Finance";
            }
            if ((strValue.Trim().ToUpper() == "NOT KNOWN" || strValue.Trim().ToUpper() == "REFINANCING-SPONSOR/PROJECT COMPANY" || strValue.Trim().ToUpper() == "MONOLINE INSURER") &&  strtemptbl == "tblClientType")
            {
                strValue = "Other:";
            }
            if (strValue.Trim().ToUpper() == "M&A:ASSETS")
            {
                strValue = "M & A: Assets";
            }
            if (strValue.Trim().ToUpper() == "LICENCES/ CONCESSIONS")
            {
                strValue = "Licences / concessions";
            }
            if (strValue.Trim().ToUpper() == "CRD_TECH LIT: IP - DOMAIN NAMES")
            {
                strValue = "CRD_TECH LIT: IP: Domain names";
            }
            if (strValue.Trim().ToUpper() == "OWNER/OCCUPIER SALE AND PURCHASE")
            {
                strValue = "Owner / Occupier sale and purchase";
            }
            
            if (strValue.Trim().ToUpper() == "CRD_IRG: CYBER/MEDIA TECH")
            {
                strValue = "CRD_IRG: Cyber / Media Tech";
            }
            if (strValue.Trim().ToUpper() == "OTHER (PLEASE SPECIFY)" && strtemptbl == "tblClientIndustryType") //tblClientIndustryType
            {
                strValue = "Other:";
            }
            if (strValue.Trim() == "Securities/Derivates Trader")
            {
                strValue = "Securities/Derivatives trader";
            }
            if (strValue.Trim() == "Other" && strtemptbl == "tblTransactionIndustryType")
            {
                strValue = "Other (Please specify)";
            }
            if (strValue.Trim() == "Transport-Rail/Metro")
            {
                strValue = "Transport - Rail/Metro";
            }
            if (strValue.Trim() == "Technology Media & Telecoms")
            {
                strValue = "Technology  Media & Telecoms";
            }
            if (strValue.Trim() == "Insurance / Re - Insurance / Life / General insurance")
            {
                strValue = "Insurance/Re-Insurance/Life/General insurance";
            }
            if (strValue.Trim() == "Energy / utilities")
            {
                strValue = "Energy/Utilities";
            }
            if (strValue.Trim() == "Food / drink")
            {
                strValue = "Food/Drink";
            }
            if (strValue.Trim() == "Technology, Media & Telecoms")
            {
                strValue = "Technology  Media & Telecoms";
            } //CORPORATE: M&A
            if (strValue.Trim() == "CORPORATE: M&A")
            {
                strValue = "M & A";
            } //
            if (strValue.Trim() == "CEE - Bucharest(Real Estate)")
            {
                strValue = "CEE - Bucharest (Real Estate)";
            }
            if (strValue.Trim() == "Hotels & Leisure" && (strtemptbl == "tblTransactionIndustryType" || strtemptbl == "tblClientIndustryType"))
            {
                strValue = "Hotels";
            }
            if (strValue.Trim() == "Hotels & Leisure" && (strtemptbl == "tblTransactionIndustrySector" || strtemptbl == "tblClientIndustrySector"))
            {
                strValue = "Hotels & Leisure";
            }
            if (strValue.Trim() == "Retailers / wholesalers")
            {
                strValue = "Retailers/Wholesalers";
            } //Insurance
            if (strValue.Trim() == "Insurance")
            {
                strValue = "Insurance/Re - Insurance/Life/General insurance";
            } //Construction / development
            if (strValue.Trim() == "Construction / development")
            {
                strValue = "Construction/Development";
            }
            if (strValue.Trim() == "Construction / development")
            {
                strValue = "Construction/Development";
            } //Agriculture
            if (strValue.Trim() == "Agriculture")
            {
                strValue = "Agricultural";
            } //Mining / quarrying
            if (strValue.Trim() == "Mining / quarrying")
            {
                strValue = "Mining/Quarrying";
            }
            if (strValue.Trim() == "Steel / metals")
            {
                strValue = "Steel/Metals";
            }

            /*if (strValue.Trim() == "Mediation/ADR")
           {
               strValue = "Mediation / ADR";
           }*/
            /*if (strValue.Trim() == "Technology/Software")
            {
                strValue = "Technology / Software";
            }*/

            /* if (strValue.Trim() == "Communications/Telecoms")
           {
               strValue = "Communications / Telecoms";
           }*/
            /*if (strValue.Trim() == "Transport - Ports/waterways")
            {
                strValue = "Transport - Ports / waterways";
            }*/
            /* if (strValue.Trim() == "Retailers / wholesalers" && strtemptbl == "tblTransactionIndustryType")
           {
               strValue = "Retail / Wholesalers";
           }*/
            /*if (strValue.Trim() == "Transport - Rail/Metro")
            {
                strValue = "Transport - Rail / Metro";
            }*/
            /*if (strValue.Trim() == "Mediation/ADR")
           {
               strValue = "Mediation / ADR";
           }*/
            /*if (strValue.Trim() == "Technology/Software")
            {
                strValue = "Technology / Software";
            }*/
            /*if (strValue.Trim() == "Advertising/PR/Marketing")
            {
                strValue = "Advertising / PR / Marketing";
            }*/
            /* if (strValue.Trim() == "Central/Local government sector" || strValue.Trim() == "Central/Local Government Sector")//Central/Local government sector
            {
                strValue = "Central / Local government sector";
            }*/
            /* if (strValue.Trim() == "Technology/Software")
            {
                strValue = "Technology / Software";
            }*/


            string sqlquery = "select " + ColId + " from " + strtemptbl + " where " + ColValue + " in(REPLACE('" + strValue + "','–','-'))";
            
            //Adi - Modificat din ReturnScalarImport in ReturnScalar
            int Id = objsp.ReturnScalar(SQL: sqlquery);
            // int Id = objsp.ReturnScalar(SQL: sqlquery);
            if (Id == 0)
            {
                obj.LogWriter(hidCredId.Value + ":" + sqlquery);
            }
            return Id.ToString();
        }


        public string InsertingMultiSelectValues(string strColValue, string strColName, string strtemptbl, int iCredentialID)
        {
            string sqlquery = "insert into " + strtemptbl + "(CredentialId , " + strColName + ")values(" + iCredentialID + "," + strColValue + ")";
            return sqlquery;
        }

        private void InsertKeyWordSearchTable(string strcon, string strValue)
        {
            SqlConnection con1 = new SqlConnection(strcon);
            con1.Open();

            string strQuery01 = "select KeywordSearch from tblKeywordSearch where KeywordSearch ='" + strValue + "'";
            SqlDataAdapter adp1 = new SqlDataAdapter(strQuery01, con1);
            DataSet ds1 = new DataSet();
            adp1.Fill(ds1);

            if (ds1.Tables[0].Rows.Count == 0)
            {
                string sql = "insert into tblKeywordSearch(KeywordSearch) values('" + strValue + "')";

                SqlCommand cmdClient = new SqlCommand(sql, con1);
                int iClient = cmdClient.ExecuteNonQuery();
                cmdClient.Dispose();
            }
            adp1.Dispose(); ds1.Dispose(); con1.Close();
        }

        public int SaveCredentials(StringBuilder strColValue, StringBuilder strColName, StringBuilder strOptionalFields = null, StringBuilder strOptionalValues = null, string strBAIFtblCredFields = null, string strBAIFtblCredValues = null, string strCorptblCredFields = null, string strCorptblCredValues = null)
        {

            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string sqlquery = string.Empty;

            sqlquery = "insert into tblCredential(username,partialflag,deleteflag," + strColName.ToString().Substring(0, strColName.ToString().Length - 1) + ")values('import','0','0','" + strColValue.ToString().Substring(0, strColValue.ToString().Length - 2) + ")";

            obj.LogWriter("Query:" + sqlquery);
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            int i = cmd.ExecuteNonQuery();

            cmd.Dispose();

            SqlCommand cmd1 = new SqlCommand();
            DataSet ds = new DataSet();
            string strgetSQL = "select top 1 credentialid from tblcredential where username='import' order by credentialid desc";
            cmd1.CommandText = strgetSQL;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = con;

            using (SqlDataAdapter da = new SqlDataAdapter(cmd1))
            {
                da.Fill(ds);
            }

            cmd1.Dispose();
            con.Close();

            return Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());

        }

        private void ExportGridView()
        {

            string attachment = "attachment; filename=Contacts.xls";

            Response.ClearContent();

            Response.AddHeader("content-disposition", attachment);

            Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Gridview1.RenderControl(htw);

            Response.Write(sw.ToString());

            Response.End();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportGridView();
        }
    }
}