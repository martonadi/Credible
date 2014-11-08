
using System.Collections.Generic;

using System.Web;


using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Telerik.Web.UI;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Word = Microsoft.Office.Interop.Word;
using CredentialsDemo.Common;
using System.Threading;
using System.Globalization;
using System.Linq;

namespace CredentialsDemo.SEARCH
{
    public partial class ResultScreen : System.Web.UI.Page
    {
        CallingSP objSP = new CallingSP();
        Logger objLog = new Logger();
        private bool isExport;
        bool blnrdoExportlist = false;
        bool blnrdoHeader = false;
        private Hashtable _ordersExpandedState;
		private bool blnLeague = false;
        string strMID = string.Empty;
        string strOID = string.Empty;

        //Save/load expanded states Hash from the session
        //this can also be implemented in the ViewState
        private Hashtable ExpandedStates
        {
            get
            {
                if (this._ordersExpandedState == null)
                {
                    _ordersExpandedState = this.Session["_ordersExpandedState"] as Hashtable;
                    if (_ordersExpandedState == null)
                    {
                        _ordersExpandedState = new Hashtable();
                        this.Session["_ordersExpandedState"] = _ordersExpandedState;
                    }
                }

                return this._ordersExpandedState;
            }
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
			if (Session["sessionUserInfo"] != null)
			{
				hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
			}
			else
			{
				Response.Redirect("~/TimeOut.aspx");
			}
			
			if (Session["MasterWidth"] != null)
			{
				Session.Remove("MasterWidth");
				Session.Add("MasterWidth", "INC");
			}
			else
			{
				Session.Add("MasterWidth", "INC");
			}
			
			try
			{
                objLog.LogWriter("ResultScreen : Page_Load Starts ", hidName.Value);
                RadGrid1.GroupingSettings.CaseSensitive = false;

                //validationReport
                btnExport2Excel.Attributes.Add("onclick", "return validationReportStyle();");
                btnExport.Attributes.Add("onclick", "return validation();");



                if (Session["sessionResultCriteria"] != null)
                {
                    if (!string.IsNullOrEmpty(Session["sessionResultCriteria"].ToString()))
                    {
                        Label4.Text = " " + Session["sessionResultCriteria"].ToString().Substring(0, Session["sessionResultCriteria"].ToString().Length - 1).Replace("|", ",").Replace("^", ",").Replace("~!@", "or");
                    }
                }

				if (Session["SessPageIndex"] != null)
				{
					hidPageIndex.Value = Session["SessPageIndex"].ToString().Split('~')[0];
					hidReturnSortColumn.Value = Session["SessPageIndex"].ToString().Split('~')[1];
					hidReturnSortDirection.Value = Session["SessPageIndex"].ToString().Split('~')[2];

					if (!string.IsNullOrEmpty(hidReturnSortColumn.Value) && !string.IsNullOrEmpty(hidReturnSortDirection.Value))
					{
						GridSortExpression expression = new GridSortExpression();
						expression.FieldName = hidReturnSortColumn.Value.ToString();
						
						if (hidReturnSortDirection.Value.ToUpper() == "ASCENDING")
						{
							expression.SortOrder = GridSortOrder.Ascending;
						}
						else
						{
							expression.SortOrder = GridSortOrder.Descending;
						}
						
						RadGrid1.MasterTableView.SortExpressions.AddSortExpression(expression);
						hidSortColumn.Value = hidReturnSortColumn.Value.ToString();
						hidSortDirection.Value = expression.SortOrder.ToString();
					}
					else
					{
						GridSortExpression expression = new GridSortExpression();
						expression.FieldName = "ClientName";
						expression.SortOrder = GridSortOrder.Ascending;
						RadGrid1.MasterTableView.SortExpressions.AddSortExpression(expression);
					}
					Session.Remove("SessPageIndex");
				}
				else if (!Page.IsPostBack)
				{
					GridSortExpression expression = new GridSortExpression();
					expression.FieldName = "ClientName";
					expression.SortOrder = GridSortOrder.Ascending;
					RadGrid1.MasterTableView.SortExpressions.AddSortExpression(expression);
				}

                if (!Page.IsPostBack)
                {
                    //AddExpression(); Sorting
                    //reset states
					if (Request.QueryString["z"] != null)
					{
						if (Request.QueryString["z"].ToString() == "1")
						{
							hidBackFromView.Value = "1";
						}
					}
					
                    this._ordersExpandedState = null;
                    this.Session["_ordersExpandedState"] = null;
                    objSP.LoadValues("usp_SearchBuilderReportList", "SearchReportFieldText", "SearchReportFieldValue", telradlist: lstColumns, strCheck: "0");

                    if (Session["MaintainState"] != null)
                    {
                        Session["MaintainState"] = null;
                    }
					
					if (Session["sessionResultIDs"] != null)
					{
						hidMasterIDs.Value = Session["sessionResultIDs"].ToString();
						string strChildCredentialID = string.Empty;
						
						if (Session["sessionResultChildIDs"] != null)
						{
							strChildCredentialID = Session["sessionResultChildIDs"].ToString();
						}
						
						if (!string.IsNullOrEmpty(strChildCredentialID))
						{
							hidAllIDs.Value = hidMasterIDs.Value + "," + strChildCredentialID;
							hidChildIDs.Value = strChildCredentialID;
						}
						else
						{
							hidAllIDs.Value = hidMasterIDs.Value;
						}
					}
				}
				
				objLog.LogWriter("ResultScreen : Page_Load Starts ", hidName.Value);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("ResultScreen : Page_Load :: " + ex.Message, hidName.Value);
				throw ex;
			}
		}

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
        }

		protected void rdoReportStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
			foreach (RadListBoxItem item in lstColumns.CheckedItems)
			{
				if (item.Checked)
				{
					item.Checked = false;
				}
			}
			
            if (rdoReportStyle.SelectedItem.Value.ToUpper() == "SEARCH")
            {
                /*lstColumns.Visible = true; 
                divlstColumns.Visible = true;*/
                lstColumns.Style.Add("display", "block");
                divlstColumns.Style.Add("display", "block");
                Panel1.Width = Unit.Pixel(530);
            }
            else
            {
                /*lstColumns.Visible = false; 
                divlstColumns.Visible = false;*/
                lstColumns.Style.Add("display", "none");
                divlstColumns.Style.Add("display", "none");
                Panel1.Width = Unit.Pixel(250);
            }

            lblClientName.Enabled = true;
            rdoExportConfidential.Enabled = true;
            lblProjectName.Enabled = true;
            rdoProjectName.Enabled = true;

            divclientname.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
            divprojectname.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
        }

        protected void btnBackToSearchT_Click(object sender, EventArgs e)
        {
            bool blnRedirect = false;

            try
            {
                if (Session["sessionResultIDs"] != null)
                {
                    Session["sessionResultIDs"] = null;
                }
                blnRedirect = true;
				hidB2Search.Value = "1";
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : btnBackToSearchT_Click :: " + ex.Message, hidName.Value);
                throw ex;
            }
            finally
            {
                if (blnRedirect == true)
                {
					Response.Redirect("~/Search/Searchscreen.aspx", false);
                }
            }
        }

        protected void hypid_Click(object sender, EventArgs e)
        {
            objLog.LogWriter("ResultScreen : hypid_Click Starts ", hidName.Value);
            bool blnRedirect = false;

            try
            {
                LinkButton btnlnk = sender as LinkButton;
                if (btnlnk != null)
                {
                    GridDataItem item = btnlnk.NamingContainer as GridDataItem;
                    if (item != null)
                    {
                        GridTableCell linkCell = (GridTableCell)item["ClientName"];
                        if (linkCell != null)
                        {
                            HyperLink reportLink = (HyperLink)linkCell.FindControl("Link");
                            if (reportLink != null)
                            {
                                if (Session["sessionCredentialID"] != null)
                                {
                                    Session.Remove("sessionCredentialID");
                                }
                                Session.Add("sessionCredentialID", reportLink.Text.Trim());
                                blnRedirect = true;
                            }
                        }
                    }
                }

                objLog.LogWriter("ResultScreen : hypid_Click Ends ", hidName.Value);
				
				if (Session["SessPageIndex"] != null)
				{
					Session.Remove("SessPageIndex");
				}
				
				Session.Add("SessPageIndex", string.Concat(new object[]
				{
					RadGrid1.CurrentPageIndex,
					"~",
					hidSortColumn.Value,
					"~",
					hidSortDirection.Value
				}));
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : hypid_Click :: " + ex.Message, hidName.Value);
                throw ex;
            }
            finally
            {
                if (blnRedirect == true)
                {
                    Response.Redirect("~/ViewEntryDetails.aspx");
                }
            }
        }

        protected void hypidchild_Click(object sender, EventArgs e)
        {
            objLog.LogWriter("ResultScreen : hypidchild_Click Starts ", hidName.Value);
            bool blnRedirect = false;

            try
            {
                LinkButton btnlnk = sender as LinkButton;
                if (btnlnk != null)
                {
                    GridDataItem item = btnlnk.NamingContainer as GridDataItem;
                    if (item != null)
                    {
                        GridTableCell linkCell = (GridTableCell)item["ClientNameChild"];
                        if (linkCell != null)
                        {
                            HyperLink reportLink = (HyperLink)linkCell.FindControl("LinkChild");
                            if (reportLink != null)
                            {
                                if (Session["sessionCredentialID"] != null)
                                {
                                    Session.Remove("sessionCredentialID");
                                }
                                Session.Add("sessionCredentialID", reportLink.Text.Trim());
                                blnRedirect = true;
                            }
                        }
                    }
                }

                objLog.LogWriter("ResultScreen : hypidchild_Click Ends ", hidName.Value);
				
				if (Session["SessPageIndex"] != null)
				{
					Session.Remove("SessPageIndex");
				}
				Session.Add("SessPageIndex", string.Concat(new object[]
				{
					RadGrid1.CurrentPageIndex,
					"~",
					hidSortColumn.Value,
					"~",
					hidSortDirection.Value
				}));
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : hypidchild_Click Ends :: " + ex.Message, hidName.Value);
                throw ex;
            }
            finally
            {
                if (blnRedirect == true)
                {
                    Response.Redirect("~/ViewEntryDetails.aspx");
                }
            }
        }

        protected void RadGrid1_Init(object sender, System.EventArgs e)
        {
           
        }

        protected void RadGrid1_ExcelExportCellFormatting(object source, ExcelExportCellFormattingEventArgs e)
        {
            try
            {
				string uniqueName = e.FormattedColumn.UniqueName;
				if (uniqueName != null)
				{
					if (uniqueName == "DateCompleted")
					{
						if (e.Cell.Text != "&nbsp;")
						{
							e.Cell.Style["mso-number-format"] = "dd\\/mm\\/yyyy";
						}
					}
				}

                if (e.Cell.NamingContainer is GridDataItem)
                {
                    GridDataItem item = e.Cell.NamingContainer as GridDataItem;
                    string sid = string.Empty;
                    sid = item.GetDataKeyValue("CredentialVersion").ToString().Trim();
                    if (!string.IsNullOrEmpty(sid))
                    {
                        if (sid == "Master")
                        {
                            item.Style["background-color"] = "White";
                        }
                        else if (sid == "Other")
                        {
                            item.Style["background-color"] = "#DFE3E8";
                        }
                        else if (sid != "Other" && sid != "Master")
                        {
                            item.Style["background-color"] = "red";
                        }
                    }
                    sid = string.Empty;
                    e.Cell.HorizontalAlign = HorizontalAlign.Left;
                }
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : RadGrid1_ExcelExportCellFormatting Ends :: " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridPagerItem)
                {
                    GridPagerItem pagerItem = (GridPagerItem)e.Item;
                    pagerItem.CssClass = "MyPager";
                }

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    // Option 1 – export with confidential information:
                    // [confidential – name/matter] [Dunnes Stores] [A leading bank] on a dispute with a leading Irish developer over its anchor lease in a new shopping centre.

                    // Option 2 – export without confidential information:
                    // A leading bank on a dispute with a leading Irish developer over its anchor lease in a new shopping centre.
                    string strClientMatterConfidential = DataBinder.Eval(e.Item.DataItem, "ClientMatterConfidential").ToString();
                    string strValueConfidential = DataBinder.Eval(e.Item.DataItem, "ValueConfidential").ToString();
                    string strClientName = DataBinder.Eval(e.Item.DataItem, "ClientName").ToString();
                    string strMatterDescription = DataBinder.Eval(e.Item.DataItem, "MatterDescription").ToString();
                    string strClientDescription = DataBinder.Eval(e.Item.DataItem, "ClientDescription").ToString();

                    //for (int i = 0; i < strMID.Split(',').Length;i++ )
                    // {
                    //     if (strMID.Split(',')[i] == item.GetDataKeyValue("CredentialID").ToString())
                    //     {
                    //         item.Selected = true;

                    //     }
                    // }

                    string strDesc = string.Empty;
                    string strConfidentialYes = string.Empty;
                    string strClientNameConfidential = DataBinder.Eval(e.Item.DataItem, "ClientNameConfidential").ToString();

                    Label lblConfidentialNo = (Label)item["ConfidentialNo"].FindControl("lblConfidentialNo");
                    if (lblConfidentialNo != null)
                    {
                        if (strClientNameConfidential == "Yes")
                        {
                            lblConfidentialNo.Text = strClientDescription + " " + strMatterDescription;
                        }
                        else
                        {
							if (strClientMatterConfidential.ToUpper() == "NO")
							{
								lblConfidentialNo.Text = strClientName + " " + strClientDescription + " " + strMatterDescription;
							}
							else
							{
								lblConfidentialNo.Text = "[confidential - matter] " + strClientName + " " + strClientDescription + " " + strMatterDescription;
							}
						}
					}
					
					if (e.Item.OwnerTableView.Name.Equals("MasterGrid", StringComparison.InvariantCultureIgnoreCase))
					{
						string strCorpMatterExecutive = DataBinder.Eval(e.Item.DataItem, "OtherMatterExecutive").ToString();
						string strCorpLeadPartner = DataBinder.Eval(e.Item.DataItem, "LeadPartner").ToString();
						string strBlank = DataBinder.Eval(e.Item.DataItem, "LeadPartner").ToString();
						string strBAIFWorkType = string.Empty;
						strBAIFWorkType = DataBinder.Eval(e.Item.DataItem, "BAIFWorkType").ToString();
						string strCorporateWorkType = string.Empty;
						strCorporateWorkType = DataBinder.Eval(e.Item.DataItem, "CorporateWorkType").ToString();
						string strCRDWorkType = string.Empty;
						strCRDWorkType = DataBinder.Eval(e.Item.DataItem, "CRDWorkType").ToString();
						string strEPCNatureofWork = string.Empty;
						strEPCNatureofWork = DataBinder.Eval(e.Item.DataItem, "EPCNatureofWork").ToString();
						string strEPCEnergyWorkType = string.Empty;
						strEPCEnergyWorkType = DataBinder.Eval(e.Item.DataItem, "EPCEnergyWorkType").ToString();
						string strRealEstateWorkType = string.Empty;
						strRealEstateWorkType = DataBinder.Eval(e.Item.DataItem, "RealEstateWorkType").ToString();
						string strWorkTypeHC = string.Empty;
						
						strWorkTypeHC = DataBinder.Eval(e.Item.DataItem, "HCWorkType").ToString();
						
						StringBuilder strAppend = new StringBuilder();
						
						if (!string.IsNullOrEmpty(strBAIFWorkType.Trim()))
						{
							strAppend.Append(strBAIFWorkType.Trim());
							strAppend.Append("; ");
						}
						if (!string.IsNullOrEmpty(strCorporateWorkType.Trim()))
						{
							strAppend.Append(strCorporateWorkType.Trim());
							strAppend.Append("; ");
						}
						if (!string.IsNullOrEmpty(strCRDWorkType.Trim()))
						{
							strAppend.Append(strCRDWorkType.Trim());
							strAppend.Append("; ");
						}
						if (!string.IsNullOrEmpty(strEPCNatureofWork.Trim()))
						{
							strAppend.Append(strEPCNatureofWork.Trim());
							strAppend.Append("; ");
						}
						if (!string.IsNullOrEmpty(strEPCEnergyWorkType.Trim()))
						{
							strAppend.Append(strEPCEnergyWorkType.Trim());
							strAppend.Append("; ");
						}
						if (!string.IsNullOrEmpty(strRealEstateWorkType.Trim()))
						{
							strAppend.Append(strRealEstateWorkType.Trim());
							strAppend.Append("; ");
						}
						if (!string.IsNullOrEmpty(strWorkTypeHC.Trim()))
						{
							strAppend.Append(strWorkTypeHC.Trim());
							strAppend.Append("; ");
						}

						Label lblWorkType = (Label)item["WT"].FindControl("lblWorkType");
						if (lblWorkType != null)
						{
							if (string.IsNullOrEmpty(lblWorkType.Text.Trim()))
							{
								lblWorkType.Text = "&nbsp;";
							}
							else
							{
								lblWorkType.Text = lblWorkType.Text.Trim().Substring(0, lblWorkType.Text.Trim().Length - 1);
							}
							
                            if (lblWorkType.Text.Trim().Length > 35)
                            {
                                RadToolTip rdTool = (RadToolTip)item["WT"].FindControl("RadToolTip1");
                                if (rdTool != null)
                                {
                                    rdTool.Text = lblWorkType.Text;
                                    lblWorkType.Text = lblWorkType.Text.Substring(0, 35);
                                    LinkButton hypmore = (LinkButton)item["WT"].FindControl("hypmore");
                                    if (hypmore != null)
                                    {
                                        hypmore.Attributes.Add("onclick", "return false;");
                                        hypmore.Visible = true;
                                    }
                                }
                            }
                        }
						
						string strCorporateSubWorkType = string.Empty;
						strCorporateSubWorkType = DataBinder.Eval(e.Item.DataItem, "CorporateSubWorkType").ToString();
						string strCRDSubWorkType = string.Empty;
						strCRDSubWorkType = DataBinder.Eval(e.Item.DataItem, "CRDSubWorkType").ToString();
						string strRealEstateSubWorkType = string.Empty;
						strRealEstateSubWorkType = DataBinder.Eval(e.Item.DataItem, "RealEstateSubWorkType").ToString();
						string strSubWorkTypeHC = string.Empty;
						
						strSubWorkTypeHC = DataBinder.Eval(e.Item.DataItem, "HCSubWorkType").ToString();
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
							if (item["SWT"] != null)
							{
								item["SWT"].Text = strSubWTAppend.ToString().Substring(0, strSubWTAppend.ToString().Length - 1);
							}
						}
						
						/* Date Completed/Ongoing */
						string strDateOngoing = string.Empty;
						strDateOngoing = DataBinder.Eval(e.Item.DataItem, "ActualDateOngoing").ToString();
						string strDate = string.Empty;
						strDate = DataBinder.Eval(e.Item.DataItem, "DateCompleted").ToString();
						
						if (!string.IsNullOrEmpty(strDateOngoing))
						{
							item["DateCompleted"].Text = strDateOngoing;
						}
						if (item["blank"] != null)
						{
							item["blank"].Text = "";
						}
						if (!string.IsNullOrEmpty(strDateOngoing) || string.IsNullOrEmpty(strDate))
						{
							item["CorpMatterCompleted"].Text = "No";
						}
						else
						{
							item["CorpMatterCompleted"].Text = "Yes";
						}
						
						string strCredentialVersion = DataBinder.Eval(e.Item.DataItem, "CredentialVersionOther").ToString();
						string strCredentialVer = DataBinder.Eval(e.Item.DataItem, "CredentialVersion").ToString();
						
						if (string.IsNullOrEmpty(strCredentialVersion) && strCredentialVer.ToUpper() == "MASTER")
						{
							item["CredentialNameMaster"].Text = "Master";
						}
						
						Label lblCorpLeadPartner = (Label)item["CorpLeadPartner"].FindControl("lblCorpLeadPartner");
						
						if (lblCorpLeadPartner != null)
						{
							lblCorpLeadPartner.Text = strCorpLeadPartner + strCorpMatterExecutive;
						}
						
						Label lblMatterSector = (Label)item["MatterSector"].FindControl("lblMatterSector");
						
						if (lblMatterSector != null)
						{
							if (string.IsNullOrEmpty(lblMatterSector.Text.Trim()))
							{
								lblMatterSector.Text = "&nbsp;";
							}
							if (lblMatterSector.Text.Trim().Length > 35)
							{
								RadToolTip rdTool = (RadToolTip)item["MatterSector"].FindControl("radTTMatterSector");
								if (rdTool != null)
								{
									rdTool.Text = lblMatterSector.Text;
									lblMatterSector.Text = lblMatterSector.Text.Substring(0, 35);
									LinkButton hypMatterSectormore = (LinkButton)item["MatterSector"].FindControl("hypMatterSectormore");
									if (hypMatterSectormore != null)
									{
										hypMatterSectormore.Visible = true;
										hypMatterSectormore.Attributes.Add("onclick", "return false;");
									}
								}
							}
						}
						Label lblClientSector = (Label)item["ClientSector"].FindControl("lblClientSector");
						if (lblClientSector != null)
						{
							if (string.IsNullOrEmpty(lblClientSector.Text.Trim()))
							{
								lblClientSector.Text = "&nbsp;";
							}
							if (lblClientSector.Text.Trim().Length > 35)
							{
								RadToolTip rdTool = (RadToolTip)item["ClientSector"].FindControl("radTTClientSector");
								if (rdTool != null)
								{
									rdTool.Text = lblClientSector.Text;
									lblClientSector.Text = lblClientSector.Text.Substring(0, 35);
									LinkButton hypClientSector = (LinkButton)item["ClientSector"].FindControl("hypClientSectormore");
									
									if (hypClientSector != null)
									{
										hypClientSector.Visible = true;
										hypClientSector.Attributes.Add("onclick", "return false;");
									}
								}
							}
						}
						
						Label lblPracticeGroup = (Label)item["PracticeGroup"].FindControl("lblPracticeGroup");
						if (lblPracticeGroup != null)
						{
							if (string.IsNullOrEmpty(lblPracticeGroup.Text.Trim()))
							{
								lblPracticeGroup.Text = "&nbsp;";
							}
							
							if (lblPracticeGroup.Text.Trim().Length > 35)
							{
								RadToolTip rdTool = (RadToolTip)item["PracticeGroup"].FindControl("radTTPracticeGroup");
								if (rdTool != null)
								{
									rdTool.Text = lblPracticeGroup.Text;
									lblPracticeGroup.Text = lblPracticeGroup.Text.Substring(0, 35);
									LinkButton hypPracticeGroupmore = (LinkButton)item["PracticeGroup"].FindControl("hypPracticeGroupmore");
									
									if (hypPracticeGroupmore != null)
									{
										hypPracticeGroupmore.Visible = true;
										hypPracticeGroupmore.Attributes.Add("onclick", "return false;");
									}
								}
							}
						}
						
						Label lblLeadPartner = (Label)item["LeadPartner"].FindControl("lblLeadPartner");
						if (lblLeadPartner != null)
						{
							if (string.IsNullOrEmpty(lblLeadPartner.Text.Trim()))
							{
								lblLeadPartner.Text = "&nbsp;";
							}
							
							if (lblLeadPartner.Text.Trim().Length > 35)
							{
								RadToolTip rdTool = (RadToolTip)item["LeadPartner"].FindControl("radTTLeadPartner");
								if (rdTool != null)
								{
									rdTool.Text = lblLeadPartner.Text;
									lblLeadPartner.Text = lblLeadPartner.Text.Substring(0, 35);
									LinkButton hypLeadPartnermore = (LinkButton)item["LeadPartner"].FindControl("hypLeadPartnermore");
									
									if (hypLeadPartnermore != null)
									{
										hypLeadPartnermore.Visible = true;
										hypLeadPartnermore.Attributes.Add("onclick", "return false;");
									}
								}
							}
						}
						
						Label lblMatterLocation = (Label)item["MatterLocation"].FindControl("lblMatterLocation");
						if (lblMatterLocation != null)
						{
							if (string.IsNullOrEmpty(lblMatterLocation.Text.Trim()))
							{
								lblMatterLocation.Text = "&nbsp;";
							}
							
							if (lblMatterLocation.Text.Trim().Length > 35)
							{
								RadToolTip rdTool = (RadToolTip)item["MatterLocation"].FindControl("radTTMatterLocation");
								if (rdTool != null)
								{
									rdTool.Text = lblMatterLocation.Text;
									lblMatterLocation.Text = lblMatterLocation.Text.Substring(0, 35);
									LinkButton hypMatterLocationmore = (LinkButton)item["MatterLocation"].FindControl("hypMatterLocationmore");
									
									if (hypMatterLocationmore != null)
									{
										hypMatterLocationmore.Visible = true;
										hypMatterLocationmore.Attributes.Add("onclick", "return false;");
									}
								}
							}
						}
					}
					
					string strID = DataBinder.Eval(e.Item.DataItem, "CredentialID").ToString();
					CheckBox chkMasterChild = (CheckBox)item["Checkbox"].FindControl("chkMasterChild");
					if (chkMasterChild != null)
					{
						chkMasterChild.Attributes.Add("onclick", "RowSelectedChild('" + strID + "','" + chkMasterChild.ClientID + "');");
					}
					
					CheckBox chkChildChild = (CheckBox)item["Checkbox"].FindControl("chkChildChild");
					if (chkChildChild != null)
					{
						chkChildChild.Attributes.Add("onclick", "RowSelectedChild('" + strID + "','" + chkChildChild.ClientID + "');");
					}
				}
			}
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : RadGrid1_ItemDataBound Ends :: " + ex.Message, hidName.Value);
				throw ex;
			}
		}

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
                {
                    ConfigureExport();
                }
				
				if (e.CommandName == Telerik.Web.UI.RadGrid.FilterCommandName)
				{
					Pair filterPair = (Pair)e.CommandArgument;
					if (Convert.ToString(filterPair.Second) != "DateCompleted")
					{
						if (!Convert.ToString(filterPair.First).ToUpper().Equals("NOFILTER"))
						{
							string columnName = Convert.ToString(filterPair.Second);
							
							hidFilterValue.Value = ((TextBox)((GridFilteringItem)e.Item)[Convert.ToString(filterPair.Second)].Controls[0]).Text.ToString();
							hidOperator.Value = Convert.ToString(filterPair.First);
							hidFilterColumn.Value = columnName;
							hidFiltered.Value = "1";
							
							Session.Add("SessionFilteredResults", hidFilterColumn.Value + "~" + hidFilterValue.Value + "~" + hidOperator.Value);
						}
						else
						{
							hidFilterValue.Value = string.Empty;
							hidOperator.Value = string.Empty;
							hidFilterColumn.Value = string.Empty;
							hidFiltered.Value = "0";
							
							Session["SessionFilteredResults"] = null;
						}
					}
					else
					{
						if (!Convert.ToString(filterPair.First).ToUpper().Equals("NOFILTER"))
						{
							string columnName = Convert.ToString(filterPair.Second);
							RadDatePicker filterBox = (e.Item as GridFilteringItem)[filterPair.Second.ToString()].Controls[0] as RadDatePicker;
							
							if (filterBox.SelectedDate.HasValue)
							{
								hidFilterValue.Value = filterBox.SelectedDate.Value.ToString("dd/MM/yyyy");
							}
							else
							{
								hidFilterValue.Value = string.Empty;
							}
							
							hidOperator.Value = Convert.ToString(filterPair.First);
							hidFilterColumn.Value = columnName;
							hidFiltered.Value = "1";
							
							Session.Add("SessionFilteredResults", hidFilterColumn.Value + "~" + hidFilterValue.Value + "~" + hidOperator.Value);
						}
						else
						{
							hidFilterValue.Value = string.Empty;
							hidOperator.Value = string.Empty;
							hidFilterColumn.Value = string.Empty;
							hidFiltered.Value = "0";
							Session["SessionFilteredResults"] = null;
						}
					}
					rdoExportList.SelectedIndex = -1;
				}
				
				if (e.CommandName == Telerik.Web.UI.RadGrid.SortCommandName)
				{
				
				}
				
				if (e.CommandName.Contains("Export"))
				{
					isExport = true;
					if (rdoExportList.SelectedValue == "All")
					{
						if (RadGrid1.MasterTableView.HasDetailTables == true)
						{
							RadGrid1.MasterTableView.DetailTables[0].EnableNoRecordsTemplate = false;
                            RadGrid1.MasterTableView.HierarchyDefaultExpanded = true; // for the first level
                            RadGrid1.MasterTableView.DetailTables[0].HierarchyDefaultExpanded = true; // for the second level 
                        }
                    }
                }
                //save the expanded/selected state in the session
				if (e.CommandName == Telerik.Web.UI.RadGrid.ExpandCollapseCommandName)
                {
                    //Is the item about to be expanded or collapsed
                    if (!e.Item.Expanded)
                    {
                        //Save its unique index among all the items in the hierarchy
                        this.ExpandedStates[e.Item.ItemIndexHierarchical] = true;
                    }
                    else //collapsed
                    {
                        this.ExpandedStates.Remove(e.Item.ItemIndexHierarchical);
                        this.ClearExpandedChildren(e.Item.ItemIndexHierarchical);
                    }
                }
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : RadGrid1_ItemCommand Ends :: " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void RadGrid1_DataBound(object sender, EventArgs e)
        {
            try
            {
                if (rdoExportList.SelectedValue == "Expanded")
                {
                    //Expand all items using our custom storage
                    string[] indexes = new string[this.ExpandedStates.Keys.Count];
                    this.ExpandedStates.Keys.CopyTo(indexes, 0);

                    ArrayList arr = new ArrayList(indexes);
                    //Sort so we can guarantee that a parent item is expanded before any of 
                    //its children
                    arr.Sort();

                    foreach (string key in arr)
                    {
                        bool value = (bool)this.ExpandedStates[key];
                        if (value)
                        {
                            RadGrid1.Items[key].Expanded = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : RadGrid1_DataBound Starts :: ", hidName.Value);
                throw ex;
            }
        }

        //Clear the state for all expanded children if a parent item is collapsed
        private void ClearExpandedChildren(string parentHierarchicalIndex)
        {
            try
            {
                string[] indexes = new string[this.ExpandedStates.Keys.Count];
                this.ExpandedStates.Keys.CopyTo(indexes, 0);
                foreach (string index in indexes)
                {
                    //all indexes of child items
                    if (index.StartsWith(parentHierarchicalIndex + "_") ||
                        index.StartsWith(parentHierarchicalIndex + ":"))
                    {
                        this.ExpandedStates.Remove(index);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		
		private bool getColumnVisible(string strval)
		{
			bool bln = false;
			string[] strlst = hidlstColumns.Value.Split(',');
			for (int i = 0; i < strlst.Length; i++)
			{
				if (strlst[i].Trim().ToUpper() == strval.Trim().ToUpper())
				{
					bln = true;
					break;
				}
			}
			return bln;
		}

        protected void btnExport2Excel_Click(object sender, EventArgs e)
        {
            try
            {
				RadGrid1.ExportSettings.FileName = ConfigurationManager.AppSettings["filename"].ToString() + "_" + hidSheetCount.Value;
				RadGrid1.ExportSettings.Excel.FileExtension = ConfigurationManager.AppSettings["filenameext"].ToString();
				
				isExport = true;
				
				hidScaleValue.Value = string.Empty;
				hidSheetName.Value = string.Empty;
				objLog.LogWriter("ResultScreen : btnExport2Excel_Click Starts", hidName.Value);

                /*string strCredentialID = string.Empty;

                foreach (GridDataItem item in RadGrid1.SelectedItems)
                {
                    if (string.IsNullOrEmpty(strCredentialID))
                    {
                        strCredentialID = "'" + item.GetDataKeyValue("CredentialID").ToString() + "',";
                    }
                    else
                    {
                        strCredentialID = strCredentialID + "'" + item.GetDataKeyValue("CredentialID").ToString() + "',";
                    }
                }*/

				string strCredentialID = string.Empty;
				strCredentialID = hidSelectedIDs.Value;
				string[] strFilterExportIDS = new string[2];
				string strFilterExportAll = string.Empty;
				
				if (hidFiltered.Value == "1")
				{
					strFilterExportIDS = FilterExport(strCredentialID);
					if (!string.IsNullOrEmpty(strFilterExportIDS[1]))
					{
						strFilterExportAll = strFilterExportIDS[0] + "," + strFilterExportIDS[1];
					}
					else
					{
						strFilterExportAll = strFilterExportIDS[0];
					}
				}

				if (!string.IsNullOrEmpty(strCredentialID))
				{
					if (Session["sessionResultIDs"] != null)
					{
						if (rdoReportStyle.SelectedItem.Value == "Full")
						{
							ConfigureSettings();
							RadGrid1.MasterTableView.HierarchyDefaultExpanded = false;
							GridColumn[] renderColumns = RadGrid1.MasterTableView.RenderColumns;
							
							for (int j = 0; j < renderColumns.Length; j++)
							{
								GridColumn col = renderColumns[j];
								col.Visible = true;
							}
							
							RadGrid1.MasterTableView.Columns.FindByUniqueName("blank").Visible = false;
							RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialID").Visible = false;
							RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("CredentialID").Visible = false;
							RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescriptionAlone").Visible = false;
							RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("MatterDescriptionAlone").Visible = false;
							
							string selSQL = string.Empty;
							string strCIDS = string.Empty;
							
							if (hidFiltered.Value == "0")
							{
								selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")";
							}
							else if (rdoExportList.SelectedItem.Value == "All")
							{
								selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strFilterExportAll + ")";
							}
							else if (rdoExportList.SelectedItem.Value == "Master")
							{
								selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strFilterExportIDS[0] + ")";
							}
							else
							{
								selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")";
							}

							RadGrid1.DataSource = ResultScreen.GetDataTable(selSQL);
                            // RadGridExporter exporter = new RadGridExporter();
                            // exporter.FileName = "TestExport.xls";

                            RadGrid1.MasterTableView.ExportToExcel();

							ConfigureUniqueColumnSettingsFull();
							hidSheetName.Value = "Full Report";
							RadGrid1.ExportSettings.FileName = "Full Report";
							hidScaleValue.Value = "42";
						}
                        else if (rdoReportStyle.SelectedItem.Value == "League")
                        {
                            if (Session["sessionResultIDs"] != null)
                            {
                                ConfigureSettings();
								
                                RadGrid1.MasterTableView.HierarchyDefaultExpanded = false;
                                foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
                                { //ClientMatterConfidential
									if (col.UniqueName == "CName" || col.UniqueName == "QuarterDealAnnounceID" || col.UniqueName == "YearDealAnnounced"
										|| col.UniqueName == "MatterDescriptionAlone" || col.UniqueName == "ContryWhereMatterOpened"
										|| col.UniqueName == "CMSFirmsInvolved" || col.UniqueName == "PredominantCountryofClient"
										|| col.UniqueName == "MatterSector" || col.UniqueName == "ClientSector" || col.UniqueName == "CountriesofotherCMSFirms"
										|| col.UniqueName == "Keyword" || col.UniqueName == "ClientNameConfidential"
										|| col.UniqueName == "ClientMatterConfidential" || col.UniqueName == "DateCompleted"
										|| col.UniqueName == "ValueConfidential" || col.UniqueName == "CorporateActingFor"
										|| col.UniqueName == "ValueOverUS" || col.UniqueName == "ValueOverPound"
										|| col.UniqueName == "ValueOverEuro" || col.UniqueName == "ValueRangeEuro"
										|| col.UniqueName == "PublishedReference" || col.UniqueName == "CorpMatterCompleted"
										|| col.UniqueName == "CorpLeadPartner" || col.UniqueName == "ValueConfidential"
										|| col.UniqueName == "CurrencyOfDeal" || col.UniqueName == "ValueOfDeal"
										|| col.UniqueName == "PEClients" || col.UniqueName == "MAStudy"
										|| col.UniqueName == "blank" || col.UniqueName == "DealAnnouncedId")
									{
										col.Visible = true;
									}
									else
									{
										col.Visible = false;
									}
								}
								
								string selSQL = string.Empty;
								string strCIDS = string.Empty;
								
								if (hidFiltered.Value == "0")
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")";
								}
								else if (rdoExportList.SelectedItem.Value == "All")
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strFilterExportAll + ")";
								}
								else if (rdoExportList.SelectedItem.Value == "Master")
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strFilterExportIDS[0] + ")";
								}
								else
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")";
								}

								RadGrid1.DataSource = ResultScreen.GetDataTable(selSQL);

								RadGrid1.MasterTableView.ExportToExcel();
								//ConfigureUniqueColumnSettingsLeague();
								hidSheetName.Value = "League Table";
								RadGrid1.ExportSettings.FileName = "League Table";
								hidScaleValue.Value = "42";
							}
						}
                        else if (rdoReportStyle.SelectedItem.Value == "Standard")
                        {
                            if (Session["sessionResultIDs"] != null)
                            {
                                ConfigureSettings();

                                RadGrid1.MasterTableView.HierarchyDefaultExpanded = false;

                                foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
                                { //ClientMatterConfidential
									if (col.UniqueName == "ClientName" || col.UniqueName == "MatterDescription" || col.UniqueName == "CredentialNameMaster" 
										|| col.UniqueName == "ClientSector" || col.UniqueName == "MatterSector" || col.UniqueName == "WT" 
										|| col.UniqueName == "PracticeGroup" || col.UniqueName == "LeadPartner" || col.UniqueName == "MatterLocation" 
										|| col.UniqueName == "DateCompleted" || col.UniqueName == "CredentialType")
									{
										col.Visible = true;
									}
									else
									{
										col.Visible = false;
									}
								}
								
								string selSQL = string.Empty;
								string strCIDS = string.Empty;
								
								if (hidFiltered.Value == "0")
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")";
								}
								else if (rdoExportList.SelectedItem.Value == "All")
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strFilterExportAll + ")";
								}
								else if (rdoExportList.SelectedItem.Value == "Master")
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strFilterExportIDS[0] + ")";
								}
								else
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")";
								}

								RadGrid1.DataSource = ResultScreen.GetDataTable(selSQL);
								RadGrid1.MasterTableView.ExportToExcel();
								
								ConfigureUniqueColumnSettings();
								hidSheetName.Value = "Standard Report";
								RadGrid1.ExportSettings.FileName = "Standard Report";
								hidScaleValue.Value = "66";
							}
						}
                        else if (rdoReportStyle.SelectedItem.Value == "Search")
                        {
                            foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
                            {
                                col.Visible = false;
                                bool bln = getColumnVisible(col.UniqueName.Trim().ToUpper());
                                if (bln == true)
                                {
                                    col.Visible = true;
                                }
                            }

							for (int i = 0; i < hidlstColumns.Value.Split(',').Length; i++)
							{
								if (hidlstColumns.Value.Split(',')[i].Trim().ToUpper() == "MATTERDESCRIPTION")
								{
									RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescription").Visible = false;
									if (rdoExportConfidential.SelectedItem.Value == "Yes")
									{
										RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").Visible = true;
										RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").Visible = false;
									}
									else
									{
										RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").Visible = true;
										RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").Visible = false;
									}
								}
								if (hidlstColumns.Value.Split(',')[i].Trim().ToUpper() == "CLIENTNAME")
								{
									RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").Visible = true;
									RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").HeaderText = "Client Name";
									RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientName").Visible = false;
									RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("CName").Visible = true;
									RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ClientNameChild").Visible = false;
								}
							}
							
							if (rdoProjectName.SelectedItem.Value.ToUpper() == "YES")
							{
								RadGrid1.MasterTableView.Columns.FindByUniqueName("ProjectName").Visible = true;
							}
							
                            RadGrid1.MasterTableView.Columns.FindByUniqueName("Checkbox").Visible = false;
                            RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("Checkbox").Visible = false;

							ConfigureSettings();
							ExcelColumnWidthSettings();
							RadGrid1.MasterTableView.HierarchyDefaultExpanded = false;
							string selSQL = string.Empty;
							if (hidFiltered.Value == "0")
							{
								selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")";
							}
							else
							{
								if (rdoExportList.SelectedItem.Value == "All")
								{
									selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strFilterExportAll + ")";
								}
								else
								{
									if (rdoExportList.SelectedItem.Value == "Master")
									{
										selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strFilterExportIDS[0] + ")";
									}
									else
									{
										selSQL = "select * from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")";
									}
								}
							}
							
							RadGrid1.DataSource = ResultScreen.GetDataTable(selSQL);
							RadGrid1.MasterTableView.ExportToExcel();
							
							hidSheetName.Value = "Report builder";
							RadGrid1.ExportSettings.FileName = "Report builder";
							hidScaleValue.Value = "42";
						}
					}
				}
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"alert('Please select atleast one record');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", sb.ToString(), false);
                }

                objLog.LogWriter("ResultScreen : btnExport2Excel_Click Ends", hidName.Value);
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : btnExport2Excel_Click :: " + ex.Message, hidName.Value);
                throw ex;
            }
        }
		
		private string[] FilterExport(string strCredentialID)
		{
			string[] strAllIDS = new string[2];
			string strAppendQuery = string.Empty;
			
			if (hidFilterColumn.Value.ToUpper() == "MATTERDESCRIPTION")
			{
				hidFilterColumn.Value = "NameFilter";
			}
			
			if (hidOperator.Value.ToUpper() == "STARTSWITH")
			{
				strAppendQuery = "select Credentialid,CredentialVersion from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")and " + hidFilterColumn.Value + " like '" + hidFilterValue.Value.Replace("[", "\\[").Replace("]", "\\]") + "%' escape '\\'";
			}
			
			if (hidOperator.Value.ToUpper() == "CONTAINS")
			{
				strAppendQuery = "select Credentialid,CredentialVersion from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")and " + hidFilterColumn.Value + " like '%" + hidFilterValue.Value.Replace("[", "\\[").Replace("]", "\\]") + "%' escape '\\'";
			}
			
			if (hidOperator.Value.ToUpper() == "EQUALTO")
			{
				if (hidFilterColumn.Value.ToUpper() == "DATECOMPLETED")
				{
					strAppendQuery = "select Credentialid,CredentialVersion from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")and CONVERT(datetime," + hidFilterColumn.Value + ",103) =CONVERT(datetime,'" + hidFilterValue.Value.Replace("[", "\\[").Replace("]", "\\]") + "',103)";
				}
				else
				{
					strAppendQuery = "select Credentialid,CredentialVersion from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")and " + hidFilterColumn.Value + " ='" + hidFilterValue.Value.Replace("[", "\\[").Replace("]", "\\]") + "'";
				}
			}
			
			if (hidOperator.Value.ToUpper() == "GREATERTHAN")
			{
				strAppendQuery = "select Credentialid,CredentialVersion from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")and CONVERT(datetime," + hidFilterColumn.Value + ",103) >CONVERT(datetime,'" + hidFilterValue.Value.Replace("[", "\\[").Replace("]", "\\]") + "',103)";
			}
			
			if (hidOperator.Value.ToUpper() == "LESSTHAN")
			{
				strAppendQuery = "select Credentialid,CredentialVersion from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")and CONVERT(datetime," + hidFilterColumn.Value + ",103) <CONVERT(datetime,'" + hidFilterValue.Value.Replace("[", "\\[").Replace("]", "\\]") + "',103)";
			}
			
			if (hidOperator.Value.ToUpper() == "NOTEQUALTO")
			{
				strAppendQuery = "select Credentialid,CredentialVersion from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")and CONVERT(datetime," + hidFilterColumn.Value + ",103) <>CONVERT(datetime,'" + hidFilterValue.Value.Replace("[", "\\[").Replace("]", "\\]") + "',103)";
			}
			
			if (hidOperator.Value.ToUpper() == "ISNULL")
			{
				strAppendQuery = "select Credentialid,CredentialVersion from tblcredentialsearchresults where credentialid in (" + strCredentialID + ")and " + hidFilterColumn.Value + " is null";
			}
			
			string strcon = ConfigurationManager.ConnectionStrings["con"].ToString();
			SqlConnection con = new SqlConnection(strcon);
			SqlDataAdapter adp = new SqlDataAdapter(strAppendQuery, con);
			DataSet ds = new DataSet();
			adp.Fill(ds);
			
			if (hidFilterColumn.Value.ToUpper() == "NAMEFILTER")
			{
				hidFilterColumn.Value = "MatterDescription";
			}
			
			SortedList slMaster = new SortedList();
			SortedList slOther = new SortedList();
			
			for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
			{
				if (ds.Tables[0].Rows[i][1].ToString().Trim().ToUpper() == "MASTER")
				{
					if (!slMaster.Contains(ds.Tables[0].Rows[i][0].ToString()))
					{
						slMaster.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][0].ToString());
					}
				}
				
				if (ds.Tables[0].Rows[i][1].ToString().Trim().ToUpper() == "OTHER")
				{
					if (!slOther.Contains(ds.Tables[0].Rows[i][0].ToString()))
					{
						slOther.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][0].ToString());
					}
				}
			}
			
			string ConnString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
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
						if (!slMaster.Contains(ds2.Tables[0].Rows[i][0].ToString()))
						{
							slMaster.Add(ds2.Tables[0].Rows[i][0].ToString(), ds2.Tables[0].Rows[i][0].ToString());
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
			}
			
			strAllIDS[0] = strIDS.Replace("'", "");
			strAllIDS[1] = strChildCredentialID.Replace("'", "");
			return strAllIDS;
		}
		/*
		private void ConfigureUniqueColumnSettingsLeague()
		{
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LeadPartnerExport").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ActualDateOngoing").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").Visible = true;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientName").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("CName").Visible = true;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ClientNameChild").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Checkbox").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescription").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("Checkbox").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescription").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("MatterDescription").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialYes").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialNo").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescriptionAlone").Visible = true;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("MatterDescriptionAlone").Visible = true;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("blank").Visible = true;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DealAnnouncedId").Visible = true;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").HeaderStyle.Width = Unit.Pixel(150);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialVersion").HeaderStyle.Width = Unit.Pixel(105);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescriptionAlone").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientSector").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterSector").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("WT").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PracticeGroup").HeaderStyle.Width = Unit.Pixel(100);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LeadPartner").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterLocation").HeaderStyle.Width = Unit.Pixel(120);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DateCompleted").HeaderStyle.Width = Unit.Pixel(100);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialType").HeaderStyle.Width = Unit.Pixel(95);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ProjectName").HeaderStyle.Width = Unit.Pixel(120);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("QuarterDealAnnounceID").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("YearDealAnnounced").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ContryWhereMatterOpened").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CountriesofotherCMSFirms").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").HeaderStyle.Width = Unit.Pixel(120);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PredominantCountryofClient").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientSector").HeaderStyle.Width = Unit.Pixel(110);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterSector").HeaderStyle.Width = Unit.Pixel(110);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Keyword").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpLeadPartner").HeaderStyle.Width = Unit.Pixel(150);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("blank").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DealAnnouncedId").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpMatterCompleted").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DateCompleted").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescriptionAlone").HeaderStyle.Width = Unit.Pixel(150);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientNameConfidential").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientMatterConfidential").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueConfidential").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CMSFirmsInvolved").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorporateActingFor").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CurrencyOfDeal").HeaderStyle.Width = Unit.Pixel(56);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOfDeal").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueRangeEuro").HeaderStyle.Width = Unit.Pixel(75);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverEuro").HeaderStyle.Width = Unit.Pixel(75);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverUS").HeaderStyle.Width = Unit.Pixel(75);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverPound").HeaderStyle.Width = Unit.Pixel(75);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PublishedReference").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PEClients").HeaderStyle.Width = Unit.Pixel(60);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MAStudy").HeaderStyle.Width = Unit.Pixel(60);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("QuarterDealAnnounceID").HeaderText = "Quarter deal announced";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("YearDealAnnounced").HeaderText = "Year deal announced/signed";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ContryWhereMatterOpened").HeaderText = "Country where matter opened";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CountriesofotherCMSFirms").HeaderText = "Countries of other CMS firms";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").HeaderText = "Client name";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PredominantCountryofClient").HeaderText = "Predominant country of client";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientSector").HeaderText = "Client sector";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterSector").HeaderText = "Matter sector";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Keyword").HeaderText = "Keyword";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpLeadPartner").HeaderText = "Lead partner(s) and matter exective(s)";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("blank").HeaderText = "";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DealAnnouncedId").HeaderText = "Date deal announced/signed ";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpMatterCompleted").HeaderText = "Has matter completed?";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DateCompleted").HeaderText = "Date Completed";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescriptionAlone").HeaderText = "Matter/credential description";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientNameConfidential").HeaderText = "Client name confidential";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientMatterConfidential").HeaderText = "Client matter confidential";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueConfidential").HeaderText = "Value confidential";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CMSFirmsInvolved").HeaderText = "CMS firms involved";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorporateActingFor").HeaderText = "Acting for";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CurrencyOfDeal").HeaderText = "Currency of deal";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOfDeal").HeaderText = "Value of deal";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueRangeEuro").HeaderText = "Value range on deal currency";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverEuro").HeaderText = "Value over Euro 5m";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverUS").HeaderText = "Value over US$5m";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverPound").HeaderText = "Value over £500,000";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PublishedReference").HeaderText = "Published reference";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PEClients").HeaderText = "Does the deal involve PE clients on either side";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MAStudy").HeaderText = "Is the deal relevant for M&A study";
			
			GridItem[] items = RadGrid1.MasterTableView.GetItems(GridItemType.NestedView);
			
			int j;
			for (j = 0; j < items.Length; j++)
			{
				GridNestedViewItem detailViewItem = (GridNestedViewItem)items[j];
				if (detailViewItem.NestedTableViews.Length > 0 && detailViewItem.NestedTableViews[0].Items.Count > 0)
				{
					GridHeaderItem headerItem = detailViewItem.NestedTableViews[0].GetItems(GridItemType.Header)[0] as GridHeaderItem;
					headerItem.Visible = false;
				}
			}
			
			int i = 30;
			GridColumn[] renderColumns = RadGrid1.MasterTableView.RenderColumns;
			j = 0;
			
			while (j < renderColumns.Length)
			{
				GridColumn col = renderColumns[j];
				string uniqueName = col.UniqueName;
				if (uniqueName == null)
				{
					goto IL_12D7;
				}
				if (<PrivateImplementationDetails>{2031467E-B12B-4B92-93A5-60563B4AA0BF}.$$method0x60000f0-1 == null)
				{
					<PrivateImplementationDetails>{2031467E-B12B-4B92-93A5-60563B4AA0BF}.$$method0x60000f0-1 = new Dictionary<string, int>(29)
					{
						{"QuarterDealAnnounceID", 0},
						{"YearDealAnnounced", 1},
						{"ContryWhereMatterOpened", 2},
						{"CountriesofotherCMSFirms", 3},
						{"CName", 4},
						{"PredominantCountryofClient", 5},
						{"ClientSector", 6},
						{"MatterSector", 7},
						{"Keyword", 8},
						{"CorpLeadPartner", 9},
						{"blank", 10},
						{"DealAnnouncedId", 11},
						{"CorpMatterCompleted",12},
						{"DateCompleted", 13},
						{"MatterDescriptionAlone", 14 },
						{"ClientNameConfidential", 15},
						{"ClientMatterConfidential", 16},
						{"ValueConfidential", 17},
						{"CMSFirmsInvolved", 18},
						{"CorporateActingFor", 19},
						{"CurrencyOfDeal", 20},
						{"ValueOfDeal", 21},
						{"ValueRangeEuro", 22},
						{"ValueOverEuro", 23},
						{"ValueOverUS", 24},
						{"ValueOverPound", 25},
						{"PublishedReference", 26},
						{"PEClients", 27},
						{"MAStudy", 28}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{2031467E-B12B-4B92-93A5-60563B4AA0BF}.$$method0x60000f0-1.TryGetValue(uniqueName, out num))
				{
					switch (num)
					{
						case 0:
							col.OrderIndex = 1;
							break;
						case 1:
							col.OrderIndex = 2;
							break;
						case 2:
							col.OrderIndex = 3;
							break;
						case 3:
							col.OrderIndex = 4;
							break;
						case 4:
							col.OrderIndex = 5;
							break;
						case 5:
							col.OrderIndex = 6;
							break;
						case 6:
							col.OrderIndex = 7;
							break;
						case 7:
							col.OrderIndex = 8;
							break;
						case 8:
							col.OrderIndex = 9;
							break;
						case 9:
							col.OrderIndex = 10;
							break;
						case 10:
							col.OrderIndex = 11;
							break;
						case 11:
							col.OrderIndex = 12;
							break;
						case 12:
							col.OrderIndex = 13;
							break;
						case 13:
							col.OrderIndex = 14;
							break;
						case 14:
							col.OrderIndex = 15;
							break;
						case 15:
							col.OrderIndex = 16;
							break;
						case 16:
							col.OrderIndex = 17;
							break;
						case 17:
							col.OrderIndex = 18;
							break;
						case 18:
							col.OrderIndex = 19;
							break;
						case 19:
							col.OrderIndex = 20;
							break;
						case 20:
							col.OrderIndex = 21;
							break;
						case 21:
							col.OrderIndex = 22;
							break;
						case 22:
							col.OrderIndex = 23;
							break;
						case 23:
							col.OrderIndex = 24;
							break;
						case 24:
							col.OrderIndex = 25;
							break;
						case 25:
							col.OrderIndex = 26;
							break;
						case 26:
							col.OrderIndex = 27;
							break;
						case 27:
							col.OrderIndex = 28;
							break;
						case 28:
							col.OrderIndex = 29;
							break;
						default:
							break;
					}
				}
				col.OrderIndex = i++;
			}
		}*/

		private void ConfigureUniqueColumnSettingsFull()
		{
		
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LeadPartner").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialVersionOther").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ActualDateOngoing").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").Visible = true;
			
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").HeaderText = "Client name";
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("CName").HeaderText = "Client name";
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientName").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("CName").Visible = true;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ClientNameChild").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Checkbox").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescription").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("Checkbox").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialVersion").Visible = false;
			if (rdoReportStyle.SelectedItem.Value != "League")
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpLeadPartner").Visible = false;
				RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpMatterCompleted").Visible = false;
			}
			
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientNameConfidential").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientMatterConfidential").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueConfidential").Visible = false;
			ExcelColumnWidthSettings();
			if (rdoExportConfidential.SelectedItem.Value == "Yes")
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").Visible = true;
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").Visible = false;
			}
			else
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").Visible = true;
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").Visible = false;
			}
			if (rdoProjectName.SelectedItem.Value == "Yes")
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ProjectName").Visible = true;
			}
			else
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ProjectName").Visible = false;
			}
			if (RadGrid1.MasterTableView.HierarchyDefaultExpanded)
			{
				RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("MatterDescription").Visible = false;
				if (rdoExportConfidential.SelectedItem.Value == "Yes")
				{
					RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialYes").Visible = true;
					RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialNo").Visible = false;
				}
				else
				{
					RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialNo").Visible = true;
					RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialYes").Visible = false;
				}
			}
			GridItem[] items = RadGrid1.MasterTableView.GetItems(new GridItemType[]
			{
				GridItemType.NestedView
			});
			for (int i = 0; i < items.Length; i++)
			{
				GridNestedViewItem detailViewItem = (GridNestedViewItem)items[i];
				if (detailViewItem.NestedTableViews.Length > 0 && detailViewItem.NestedTableViews[0].Items.Count > 0)
				{
					GridHeaderItem headerItem = detailViewItem.NestedTableViews[0].GetItems(new GridItemType[]
					{
						GridItemType.Header
					})[0] as GridHeaderItem;
					headerItem.Visible = false;
				}
			}
		}

		private void ExcelColumnWidthSettings()
		{
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialID").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").HeaderStyle.Width = Unit.Pixel(120);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientName").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ProjectName").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescriptionAlone").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescription").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialVersion").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialNameMaster").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientSector").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterSector").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("WT").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SWT").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PracticeGroup").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LeadPartnerExport").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterLocation").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DateCompleted").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpLeadPartner").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpMatterCompleted").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("blank").HeaderStyle.Width = Unit.Pixel(80);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientSubSector").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PredominantCountryofClient").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterSubSector").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ContryWhereMatterOpened").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CountryJurisdiction").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LanguageofDispute").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Team").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SourceOfCredential").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("OtherMatterExecutive").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CMSFirmsInvolved").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CountriesofotherCMSFirms").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("OtherUses").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("KnowHow").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("BAIFWorkType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorporateWorkType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorporateActingFor").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorporateCountryBuyer").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorporateCountryTarget").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorporateCountrySeller").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CRDWorkType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CRDSubWorkType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("EPCNatureofWork").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("EPCTypeofContract").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("EPCEnergyWorkType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("RealEstateClientType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("RealEstateWorkType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("RealEstateSubWorkType").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientNameConfidential").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientNameConfidentialCompletion").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientDescription").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterNumber").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DateMatterOpened").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("OtherMatterDescription").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientMatterConfidential").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterConfidentialCompletion").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ActualDateOngoing").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ApplicableLaw").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ApplicableLawOther").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Contentious").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LanguageofDisputeOther").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DisputeResolution").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SeatofArbitration").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SeatofArbitrationOther").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CountryArbitration").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("InvestmentTreaty").HeaderStyle.Width = Unit.Pixel(80);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("InvestigationType").HeaderStyle.Width = Unit.Pixel(80);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ArbitralRules").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOfDeal").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CurrencyOfDeal").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueConfidential").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueConfidentialCompletion").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CMSPartnername").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SourceofCredentialOther").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LeadCMSFirm").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialStatus").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Priority").HeaderStyle.Width = Unit.Pixel(50);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ProBono").HeaderStyle.Width = Unit.Pixel(80);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("BibleReference").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientTypeBAIF").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LeadBanks").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverUS").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverPound").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverEuro").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueRangeEuro").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DealAnnouncedId").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PublishedReference").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MAStudy").HeaderStyle.Width = Unit.Pixel(80);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PEClients").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("QuarterDealAnnounceID").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("QuarterDealCompletedId").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("YearDealCompletedID").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("YearDealAnnounced").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientTypeIdCommercial").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientTypeIDEPC").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientScope").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientTypeOtherEPC").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("TypeofContractOtherEPC").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SubjectMatterIDEPC").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SubjectMatterOtherEPC").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ContractTypeIDEPCE").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("WorkTypeIdHC").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SubWorkTypeHC").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PensionSchemeHC").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientTypeIdIPF").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Keyword").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialVersionOther").HeaderStyle.Width = Unit.Pixel(90);
		}

        private void ConfigureUniqueColumnSettings()
        {
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LeadPartnerExport").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ActualDateOngoing").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").Visible = true;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientName").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("CName").Visible = true;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ClientNameChild").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Checkbox").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescription").Visible = false;
			RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("Checkbox").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialVersion").Visible = false;
			if (rdoReportStyle.SelectedItem.Value != "League")
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpLeadPartner").Visible = false;
				RadGrid1.MasterTableView.Columns.FindByUniqueName("CorpMatterCompleted").Visible = false;
			}
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientNameConfidential").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientMatterConfidential").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueConfidential").Visible = false;
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").HeaderStyle.Width = Unit.Pixel(150);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialVersion").HeaderStyle.Width = Unit.Pixel(105);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterDescription").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").HeaderStyle.Width = Unit.Pixel(250);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientSector").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterSector").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("WT").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PracticeGroup").HeaderStyle.Width = Unit.Pixel(100);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("LeadPartner").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("MatterLocation").HeaderStyle.Width = Unit.Pixel(120);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DateCompleted").HeaderStyle.Width = Unit.Pixel(100);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CredentialType").HeaderStyle.Width = Unit.Pixel(95);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ProjectName").HeaderStyle.Width = Unit.Pixel(120);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PredominantCountryofClient").HeaderStyle.Width = Unit.Pixel(100);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ContryWhereMatterOpened").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Team").HeaderStyle.Width = Unit.Pixel(130);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CMSFirmsInvolved").HeaderStyle.Width = Unit.Pixel(115);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CorporateActingFor").HeaderStyle.Width = Unit.Pixel(120);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientNameConfidentialCompletion").HeaderStyle.Width = Unit.Pixel(80);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("DateMatterOpened").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueConfidentialCompletion").HeaderStyle.Width = Unit.Pixel(75);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("CMSPartnername").HeaderStyle.Width = Unit.Pixel(110);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("SourceofCredentialOther").HeaderStyle.Width = Unit.Pixel(115);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverUS").HeaderStyle.Width = Unit.Pixel(100);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverPound").HeaderStyle.Width = Unit.Pixel(90);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueOverEuro").HeaderStyle.Width = Unit.Pixel(65);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("ValueRangeEuro").HeaderStyle.Width = Unit.Pixel(95);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("PublishedReference").HeaderStyle.Width = Unit.Pixel(120);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("QuarterDealAnnounceID").HeaderStyle.Width = Unit.Pixel(70);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("YearDealAnnounced").HeaderStyle.Width = Unit.Pixel(68);
			RadGrid1.MasterTableView.Columns.FindByUniqueName("Keyword").HeaderStyle.Width = Unit.Pixel(60);
			
			//child header

			if (rdoExportConfidential.SelectedItem.Value == "Yes")
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").Visible = true;
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").Visible = false;
			}
			else
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialNo").Visible = true;
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ConfidentialYes").Visible = false;
			}
			if (rdoProjectName.SelectedItem.Value == "Yes")
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ProjectName").Visible = true;

			}
			else
			{
				RadGrid1.MasterTableView.Columns.FindByUniqueName("ProjectName").Visible = false;

			}

			if (RadGrid1.MasterTableView.HierarchyDefaultExpanded)
			{

				RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("MatterDescription").Visible = false;
				if (rdoExportConfidential.SelectedItem.Value == "Yes")
				{
					RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialYes").Visible = true;
					RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialNo").Visible = false;
				}
				else
				{
					RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialNo").Visible = true;
					RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ConfidentialYes").Visible = false;
				}
			}

			GridItem[] items = RadGrid1.MasterTableView.GetItems(GridItemType.NestedView);

			for (int i = 0; i < items.Length; i++)
			{
				GridNestedViewItem detailViewItem = (GridNestedViewItem)items[i];
				if (detailViewItem.NestedTableViews.Length > 0 && detailViewItem.NestedTableViews[0].Items.Count > 0)
				{
					GridHeaderItem headerItem = detailViewItem.NestedTableViews[0].GetItems(GridItemType.Header)[0] as GridHeaderItem;
					headerItem.Visible = false;
				}
			}
		}

        private void ConfigureSettings()
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.GridLines = GridLines.Both;
            RadGrid1.AllowFilteringByColumn = false;

            RadGrid1.MasterTableView.DetailTables[0].GridLines = GridLines.Both;
            RadGrid1.MasterTableView.DetailTables[0].ShowHeader = false;

            foreach (GridColumn col in RadGrid1.MasterTableView.DetailTables[0].RenderColumns)
            {
                col.Visible = true;
            }
        }
		
 		public void ConfigureExport()
        {
            try
            {
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;
                RadGrid1.MasterTableView.GridLines = GridLines.Both;
                RadGrid1.AllowFilteringByColumn = false;

                RadGrid1.MasterTableView.DetailTables[0].GridLines = GridLines.Both;

                foreach (GridColumn col in RadGrid1.MasterTableView.DetailTables[0].RenderColumns)
                {
                    col.Visible = true;
                }

                if (rdoExportList.SelectedItem.Value == "Standard")
                {
                    RadGrid1.MasterTableView.HierarchyDefaultExpanded = false;

                    foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
                    {
                        if (col.HeaderText == "CredentialID" || col.HeaderText == "ClientName" || col.HeaderText == "ClientNameConfidential"
                        || col.HeaderText == "ClientNameConfidentialCompletion"
                        || col.HeaderText == "ClientSector" || col.HeaderText == "ClientSubSector"
                        || col.HeaderText == "LeadPartner" || col.HeaderText == "BAIFWorkType"
                        || col.HeaderText == "CorporateWorkType" || col.HeaderText == "CRDWorkType"
                        || col.HeaderText == "EPCEnergyWorkType" || col.HeaderText == "RealEstateWorkType"
                        || col.HeaderText == "HumanCaptialWorkType")
                        {
                            col.Visible = true;
                        }
                        else
                        {
                            col.Visible = false;
                        }
                    }
                }
                else if (rdoExportList.SelectedItem.Value == "Masters")
                {
                    RadGrid1.MasterTableView.HierarchyDefaultExpanded = false;

                    foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
                    {
                        col.Visible = true;
                    }
                }
                else if (rdoExportList.SelectedItem.Value == "All")
                {
                    foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
                    {
                        col.Visible = true;
                    }
                }

                RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").Visible = true;
                RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientName").Visible = false;
                RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("CName").Visible = true;
                RadGrid1.MasterTableView.DetailTables[0].Columns.FindByUniqueName("ClientNameChild").Visible = false;

                RadGrid1.MasterTableView.Columns.FindByUniqueName("Checkbox").Visible = false;

                if (rdoExportList.SelectedItem.Value == "Selected")
                {
                    RadGrid1.MasterTableView.HierarchyDefaultExpanded = false;

                    string strCredentialID = string.Empty;

                    foreach (GridDataItem item in RadGrid1.SelectedItems)
                    {
                        if (string.IsNullOrEmpty(strCredentialID))
                        {
                            strCredentialID = "'" + item.GetDataKeyValue("CredentialID").ToString() + "',";
                        }
                        else
                        {
                            strCredentialID = strCredentialID + "'" + item.GetDataKeyValue("CredentialID").ToString() + "',";
                        }
                    }

                    if (!string.IsNullOrEmpty(strCredentialID))
                    {
                        Session.Add("SID", strCredentialID.Substring(0, strCredentialID.Length - 1));
                    }

                    foreach (GridColumn col in RadGrid1.MasterTableView.RenderColumns)
                    {
                        col.Visible = true;
                    }
                    RadGrid1.MasterTableView.Columns.FindByUniqueName("CName").Visible = true;
                    RadGrid1.MasterTableView.Columns.FindByUniqueName("ClientName").Visible = false;
                    RadGrid1.MasterTableView.Columns.FindByUniqueName("Checkbox").Visible = false;
                }
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : ConfigureExport :: " + ex.Message, hidName.Value);
				throw ex;
			}
		}

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridNoRecordsItem && e.Item.OwnerTableView != RadGrid1.MasterTableView)
            {
                e.Item.OwnerTableView.Visible = false;
            }

            if (e.Item is GridHeaderItem && isExport)
            {
                foreach (TableCell cell in e.Item.Cells)
                {
                    cell.Style.Add("background-color", "aqua");
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    /*cell.Style["width"] = "100px";*/
                }
            }
			
			if (e.Item is GridFilteringItem)
			{
				RadDatePicker picker = ((GridFilteringItem)e.Item)["DateCompleted"].Controls[0] as RadDatePicker;
				if (picker != null)
				{
					picker.DateInput.DisplayDateFormat = "dd/MM/yyyy";
					picker.DateInput.DateFormat = "dd/MM/yyyy";
				}
			}
		}

        protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {

            //Adi added name variable
            String name = String.Empty;

            try
            {
                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                
                //Adi Assigned value to name 
                
                name = e.DetailTableView.Name;//This line was added by Adi

				if (e.DetailTableView.Name != null)
				{
                    
					if (name == "Orders")
					{
						string CustomerID = dataItem.GetDataKeyValue("CredentialID").ToString();
						string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();
						string strSql = "SELECT a.credentialid,b.Credential_Version_Other FROM \r\n                                        tblcredentialversionrelation as a\r\n                                        INNER JOIN tblcredential as b\r\n                                        on a.CredentialID=b.CredentialId\r\n                                        WHERE a.credentialversion ='Other' and a.credentialmasterid in (" + CustomerID + ") and b.DeleteFlag=0";
						SqlConnection con = new SqlConnection(strcon);
						con.Open();
						
						SqlDataAdapter adp = new SqlDataAdapter(strSql, con);
						DataSet dsNew = new DataSet();
						adp.Fill(dsNew);
						adp.Dispose();
						con.Close();
						
						string strChildCredentialID = string.Empty;
						string strCredOtherCaption = string.Empty;

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
								if (string.IsNullOrEmpty(strCredOtherCaption))
								{
									strCredOtherCaption = dsNew.Tables[0].Rows[i][1].ToString();
								}
								else
								{
									strCredOtherCaption = strCredOtherCaption + "," + dsNew.Tables[0].Rows[i][1].ToString();
								}
							}
							
							strChildCredentialID = strChildCredentialID.Substring(0, strChildCredentialID.Length - 1);
							string selSQL = string.Empty;
							selSQL = "select CredentialID,ClientName,ClientDescription,MatterDescription,NameFilter\r\n                        ,OtherMatterDescription,ClientNameConfidential,ClientNameConfidentialCompletion,ClientMatterConfidential,\r\n                            MatterConfidentialCompletion,ValueConfidential,ValueConfidentialCompletion,CredentialVersion,CredentialVersionOther \r\n                            from tblcredentialsearchresults where deleteflag = '0' and CREDENTIALID in (" + strChildCredentialID + ")";
							e.DetailTableView.DataSource = ResultScreen.GetDataTable(selSQL);
							
							foreach (GridDataItem itemchild in e.DetailTableView.Items)
							{
								itemchild.BackColor = System.Drawing.Color.PaleGreen;
							}
							if (dataItem != null)
							{
								Label lblCredentialVersion = (Label)dataItem["CredentialVersion"].FindControl("lblCredentialVersion");
								
								if (lblCredentialVersion != null)
								{
									RadToolTip rdTool = (RadToolTip)dataItem["CredentialVersion"].FindControl("radTTCredentialVersion");
									
									if (rdTool != null)
									{
										rdTool.Text = strCredOtherCaption;
										rdTool.Visible = true;
										LinkButton hypCredentialVersionmore = (LinkButton)dataItem["CredentialVersion"].FindControl("hypCredentialVersionmore");
										
										if (hypCredentialVersionmore != null)
										{
											hypCredentialVersionmore.Visible = true;
											hypCredentialVersionmore.Attributes.Add("onclick", "return false;");
										}
									}
								}
							}
						}
						else
						{
							e.DetailTableView.AllowFilteringByColumn = false;
							e.DetailTableView.ShowHeadersWhenNoRecords = false;
							e.DetailTableView.DataSource = new int[0];
							e.DetailTableView.Visible = false;

							if (!RadGrid1.MasterTableView.HasDetailTables)
							{
								e.DetailTableView.DetailTables[0].EnableNoRecordsTemplate = false;
								e.DetailTableView.EnableNoRecordsTemplate = false;
							}

							if (isExport)
							{
								if (!RadGrid1.MasterTableView.HasDetailTables)
								{
									e.DetailTableView.DetailTables[0].HierarchyDefaultExpanded = false;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("ResultScreen Error : RadGrid1_DetailTableDataBind Ends " + ex.Message, hidName.Value);
				throw ex;
			}
		}

		protected void RadGrid1_PreRender(object sender, EventArgs e)
		{
			try
			{
				if (hidB2Search.Value == "0")
				{
					if (hidSelectedValue.Value == "1")
					{
						rdoExportList.SelectedIndex = 2;
					}
					
					HideExpandColumnRecursive(RadGrid1.MasterTableView);
					if (Session["sessionFilterCriteria"] != null)
					{
						string strFilter = Session["sessionResultCriteria"].ToString().Replace('|', ',');
						string[] strFilterValues = strFilter.Replace('|', ',').Split(',');
						HighlightParentText(strFilterValues, strFilter);
						HighlightChildText(strFilterValues, strFilter);
					}
					
					if (!isExport)
					{
						foreach (GridDataItem dataItem in RadGrid1.Items)
						{
							if (dataItem.Expanded)
							{
								GridNestedViewItem nestedviewitem = dataItem.ChildItem;
								foreach (GridDataItem Item in nestedviewitem.NestedTableViews[0].Items)
								{
									string strSelected = hidSelectedIDs.Value.Replace("'", "");
									if (strSelected.Contains(Item.GetDataKeyValue("CredentialID").ToString()))
									{
										CheckBox chkChild = (CheckBox)Item.FindControl("chkChildChild");
										if (chkChild != null)
										{
											chkChild.Checked = true;
											dataItem.Selected = true;
										}
									}
								}
							}
						}
						
						foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
						{
							string strSelected = hidSelectedIDs.Value.Replace("'", "");
							if (strSelected.Contains(dataItem.GetDataKeyValue("CredentialID").ToString()))
							{
								CheckBox chkMaster = (CheckBox)dataItem.FindControl("chkMasterChild");
								if (chkMaster != null)
								{
									chkMaster.Checked = true;
									dataItem.Selected = true;
								}
							}
						}
					}
					
					if (rdoExportList.SelectedValue.Trim().ToUpper() == "ALL")
					{
						GridHeaderItem headerItemchld = RadGrid1.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;

						if (headerItemchld != null)
						{
							CheckBox chk = headerItemchld.FindControl("chkMasterHead") as CheckBox;
							if (chk != null)
							{
								chk.Checked = true;
							}
						}
					}
					else
					{
						GridHeaderItem headerItemchld = RadGrid1.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
						
						if (headerItemchld != null)
						{
							CheckBox chk = headerItemchld.FindControl("chkMasterHead") as CheckBox;
							if (chk != null)
							{
								chk.Checked = false;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("ResultScreen Error : RadGrid1_PreRender Ends :: " + ex.Message, hidName.Value);
				throw ex;
			}
		}

        public void HideExpandColumnRecursive(GridTableView tableView)
        {
            try
            {

                //objLog.LogWriter("ResultScreen : HideExpandColumnRecursive Starts ", hidName.Value);

                GridItem[] nestedViewItems = tableView.GetItems(GridItemType.NestedView);
                if (nestedViewItems.Length > 0)
                {
                    foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
                    {
                        foreach (GridTableView nestedView in nestedViewItem.NestedTableViews)
                        {
                            if (nestedView.Items.Count == 0)
                            {
                                TableCell cell = nestedView.ParentItem["ExpandColumn"];
                                if (cell.HasControls())
                                {
                                    cell.Controls[0].Visible = false;

                                    cell.Text = " ";
                                    nestedViewItem.Visible = false;
                                }
                            }
                            /*if (nestedView.HasDetailTables)
                            {
                                HideExpandColumnRecursive(nestedView);
                            }*/
                        }
                    }
                }

                //objLog.LogWriter("ResultScreen : HideExpandColumnRecursive Ends ", hidName.Value);
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("ResultScreen Error : HideExpandColumnRecursive Ends :: " + ex.Message, hidName.Value);
                throw ex;
            }
        }

		private void HighlightChildText(string[] strFilterValues, string strFilter)
        {
            //objLog.LogWriter("ResultScreen : HighlightChildText Starts ", hidName.Value);

            foreach (GridNestedViewItem nestedViewItem in RadGrid1.MasterTableView.GetItems(GridItemType.NestedView))
            {
				if (nestedViewItem.NestedTableViews.Length > 0)
				{
					foreach (GridDataItem dataItem in nestedViewItem.NestedTableViews[0].Items)
					{
						foreach (GridColumn col in nestedViewItem.NestedTableViews[0].Columns)
						{
							if (col.UniqueName.ToUpper() == "CLIENTNAMECHILD")
							{
								LinkButton hLink = (LinkButton)dataItem.FindControl("hypidchild");
								if (hLink != null)
								{
									if (hLink.Text != "&nbsp;")
									{
										hLink.Text = ResultScreen.HighlightKeyWords(strFilter.Trim(), hLink.Text.Trim());
									}
								}
							}
							else
							{
								if (col.UniqueName.ToUpper() == "CONFIDENTIALYES")
								{
									Label lblWork = (Label)dataItem.FindControl("lblConfidentialYes");
									if (lblWork != null)
									{
										if (lblWork.Text != "&nbsp;")
										{
											lblWork.Text = ResultScreen.HighlightKeyWords(strFilter.Trim(), lblWork.Text.Trim());
										}
									}
								}
								else
								{
									if (col.UniqueName.ToUpper() == "CONFIDENTIALNO")
									{
										Label lblWork = (Label)dataItem.FindControl("lblConfidentialNo");
										if (lblWork != null)
										{
											if (lblWork.Text != "&nbsp;")
											{
												lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
											}
										}
									}
									else
									{
										if (col.UniqueName.ToUpper() != "CHECKBOX")
										{
											if (dataItem[col.UniqueName].Text != "&nbsp;")
											{
												dataItem[col.UniqueName].Text = ResultScreen.HighlightKeyWords(strFilter, dataItem[col.UniqueName].Text.Trim());
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
		private void HighlightParentText(string[] strFilterValues, string strFilter)
		{
			foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
			{
				foreach (GridColumn col in RadGrid1.MasterTableView.Columns)
				{
					if (col.UniqueName.ToUpper() == "CLIENTNAME")
					{
						LinkButton hLink = (LinkButton)item.FindControl("hypid");
						if (hLink != null)
						{
							if (hLink.Text != "&nbsp;")
							{
								hLink.Text = ResultScreen.HighlightKeyWords(strFilter, hLink.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "CLIENTSECTOR")
					{
						Label lblWork = (Label)item.FindControl("lblClientSector");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "MATTERSECTOR")
					{
						Label lblWork = (Label)item.FindControl("lblMatterSector");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "WT")
					{
						Label lblWork = (Label)item.FindControl("lblWorkType");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "PRACTICEGROUP")
					{
						Label lblWork = (Label)item.FindControl("lblPracticeGroup");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "LEADPARTNER")
					{
						Label lblWork = (Label)item.FindControl("lblLeadPartner");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "MATTERLOCATION")
					{
						Label lblWork = (Label)item.FindControl("lblMatterLocation");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "CONFIDENTIALYES")
					{
						Label lblWork = (Label)item.FindControl("lblConfidentialYes");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "CONFIDENTIALNO")
					{
						Label lblWork = (Label)item.FindControl("lblConfidentialNo");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() == "CREDENTIALVERSION" && item.OwnerTableView.Name != "Orders")
					{
						Label lblWork = (Label)item.FindControl("lblCredentialVersion");
						if (lblWork != null)
						{
							if (lblWork.Text != "&nbsp;")
							{
								lblWork.Text = ResultScreen.HighlightKeyWords(strFilter, lblWork.Text.Trim());
							}
						}
					}
					else if (col.UniqueName.ToUpper() != "CHECKBOX")
					{
						if (item[col.UniqueName].Text != "&nbsp;")
						{
							item[col.UniqueName].Text = ResultScreen.HighlightKeyWords(strFilter, item[col.UniqueName].Text.Trim());
						}
					}
				}
			}
		}

		public string Highlight(string Search_Str, string InputTxt)
		{
			Search_Str = Search_Str.Replace("^", ",");
			string result;
			if (Search_Str.ToUpper() != "PARTIAL SAVE RECORDS")
			{
				Regex RegExp = new Regex("\\b" + Search_Str.Trim().Replace("(", "\\(").Replace(")", "\\)") + "\\b", RegexOptions.IgnoreCase);
				string strSuccess = string.Empty;
				if (InputTxt.IndexOf("<span class=highlight>") != -1)
				{
					strSuccess = InputTxt;
				}
				else
				{
					strSuccess = RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
				}
				result = strSuccess;
			}
			else
			{
				result = InputTxt;
			}
			return result;
		}

		public string ReplaceKeyWords(Match m)
		{
			return "<span class=highlight>" + m.Value + "</span>";
		}

            //From here


		protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
		{
			try
			{
				if (hidB2Search.Value != "1")
				{
					if (!string.IsNullOrEmpty(RadGrid1.MasterTableView.FilterExpression.ToString()))
					{
						string strTemp = string.Empty;
						if (Session["SessionFilteredResults"] != null)
						{
							strTemp = Session["SessionFilteredResults"].ToString();
							Session.Remove("SessionFilteredResults");
						}
						Session.Add("SessionFilteredResults", strTemp + "~" + RadGrid1.MasterTableView.FilterExpression.ToString());
					}
					if (!e.IsFromDetailTable)
					{
						if (Session["sessionResultIDs"] != null)
						{
							string selSQL = string.Empty;
							StreamReader sr = new StreamReader(Server.MapPath("~\\Queries\\SearchResultQuery.txt"));
							selSQL = sr.ReadToEnd();
							if (Session["SID"] == null)
							{
								selSQL = "select * from tblcredentialsearchresults where credentialid in (" + Session["sessionResultIDs"].ToString() + ")";
							}
							else
							{
								selSQL = "select * from tblcredentialsearchresults where credentialid in (" + Session["sessionResultIDs"].ToString() + ")";
								Session["SID"] = null;
							}
							sr.Dispose();
							if (!string.IsNullOrEmpty(hidPageIndex.Value.Trim()))
							{
								RadGrid1.CurrentPageIndex = Convert.ToInt32(hidPageIndex.Value);
								hidPageIndex.Value = string.Empty;
							}
							if (!string.IsNullOrEmpty(hidReturnSortColumn.Value.Trim()))
							{
								GridSortExpression expression = new GridSortExpression();
								expression.FieldName = hidReturnSortColumn.Value.Trim();
								expression.SetSortOrder(hidReturnSortDirection.Value.Trim());
								RadGrid1.MasterTableView.SortExpressions.AddSortExpression(expression);
								hidReturnSortColumn.Value = string.Empty;
								hidReturnSortDirection.Value = string.Empty;
							}
							RadGrid1.DataSource = ResultScreen.GetDataTable(selSQL);
						}
					}
				}
				if (!Page.IsPostBack && Session["SessionFilteredResults"] != null && hidBackFromView.Value == "1")
				{
					string filterValue = Session["SessionFilteredResults"].ToString().Split('~')[1];
					if (Session["SessionFilteredResults"].ToString().Split('~')[2].ToUpper() == "CONTAINS")
					{
						hidFilterValue.Value = filterValue;
						hidOperator.Value = Session["SessionFilteredResults"].ToString().Split('~')[2];
						hidFilterColumn.Value = Session["SessionFilteredResults"].ToString().Split('~')[0];
						RadGrid1.MasterTableView.FilterExpression = Session["SessionFilteredResults"].ToString().Split('~')[3];
						GridColumn column = RadGrid1.MasterTableView.GetColumnSafe(Session["SessionFilteredResults"].ToString().Split('~')[0]);
						column.CurrentFilterFunction = GridKnownFunction.Contains;
					}
					if (Session["SessionFilteredResults"].ToString().Split('~')[2].ToUpper() == "EQUALTO")
					{
						RadGrid1.MasterTableView.FilterExpression = Session["SessionFilteredResults"].ToString().Split('~')[3];
						GridColumn column = RadGrid1.MasterTableView.GetColumnSafe(Session["SessionFilteredResults"].ToString().Split('~')[0]);
						column.CurrentFilterFunction = GridKnownFunction.EqualTo;
					}
					if (Session["SessionFilteredResults"].ToString().Split('~')[2].ToUpper() == "STARTSWITH")
					{
						RadGrid1.MasterTableView.FilterExpression = Session["SessionFilteredResults"].ToString().Split('~')[3];
						GridColumn column = RadGrid1.MasterTableView.GetColumnSafe(Session["SessionFilteredResults"].ToString().Split('~')[0]);
						column.CurrentFilterFunction = GridKnownFunction.StartsWith;
					}
					if (Session["SessionFilteredResults"].ToString().Split('~')[2].ToUpper() == "GREATERTHAN")
					{
						RadGrid1.MasterTableView.FilterExpression = Session["SessionFilteredResults"].ToString().Split('~')[3];
						GridColumn column = RadGrid1.MasterTableView.GetColumnSafe(Session["SessionFilteredResults"].ToString().Split('~')[0]);
						column.CurrentFilterFunction = GridKnownFunction.GreaterThan;
					}
					if (Session["SessionFilteredResults"].ToString().Split('~')[2].ToUpper() == "LESSTHAN")
					{
						RadGrid1.MasterTableView.FilterExpression = Session["SessionFilteredResults"].ToString().Split('~')[3];
						GridColumn column = RadGrid1.MasterTableView.GetColumnSafe(Session["SessionFilteredResults"].ToString().Split('~')[0]);
						column.CurrentFilterFunction = GridKnownFunction.LessThan;
					}
					if (Session["SessionFilteredResults"].ToString().Split('~')[2].ToUpper() == "NOTEQUALTO")
					{
						RadGrid1.MasterTableView.FilterExpression = Session["SessionFilteredResults"].ToString().Split('~')[3];
						GridColumn column = RadGrid1.MasterTableView.GetColumnSafe(Session["SessionFilteredResults"].ToString().Split('~')[0]);
						column.CurrentFilterFunction = GridKnownFunction.NotEqualTo;
					}
					if (Session["SessionFilteredResults"].ToString().Split('~')[2].ToUpper() == "ISNULL")
					{
						RadGrid1.MasterTableView.FilterExpression = Session["SessionFilteredResults"].ToString().Split('~')[3];
						GridColumn column = RadGrid1.MasterTableView.GetColumnSafe(Session["SessionFilteredResults"].ToString().Split('~')[0]);
						column.CurrentFilterFunction = GridKnownFunction.IsNull;
					}
					hidFiltered.Value = "1";
				}
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("ResultScreen Error : RadGrid1_NeedDataSource Ends :: " + ex.Message, hidName.Value);
				throw ex;
			}
		}

        public static DataTable GetDataTable(string query)
        {
            string ConnString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);

            DataTable myDataTable = new DataTable();

            conn.Open();
            adapter.Fill(myDataTable);
            conn.Close();
            return myDataTable;
        }

		protected void rdoExportList_SelectedIndexChanged(object sender, EventArgs e)
		{
			blnrdoExportlist = true;
			if (rdoExportList.SelectedIndex == 0)
			{
				foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
				{
					(dataItem.FindControl("chkMasterChild") as CheckBox).Checked = true;
					dataItem.Selected = true;
				}

                foreach (GridNestedViewItem nestedViewItem in RadGrid1.MasterTableView.GetItems(GridItemType.NestedView))
                {
                    if (nestedViewItem.NestedTableViews.Length > 0)
                    {
                        foreach (GridDataItem dataItem in nestedViewItem.NestedTableViews[0].Items)
                        {
                            (dataItem.FindControl("chkchild") as CheckBox).Checked = true;
                            dataItem.Selected = true;
						}
					}
				}

				GridHeaderItem headerItemchld = RadGrid1.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
				(headerItemchld.FindControl("chkMasterHead") as CheckBox).Checked = true;
				blnrdoHeader = true;
				hidSelectedIDs.Value = string.Empty;
				hidSelectedIDs.Value = hidAllIDs.Value;
				hidSelectedValue.Value = "0";
			}
			else if (rdoExportList.SelectedIndex == 1)
			{
				foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
				{
					(dataItem.FindControl("chkMasterChild") as CheckBox).Checked = true;
					dataItem.Selected = true;
				}
				
                foreach (GridNestedViewItem nestedViewItem in RadGrid1.MasterTableView.GetItems(GridItemType.NestedView))
                {
                    if (nestedViewItem.NestedTableViews.Length > 0)
                    {
                        foreach (GridDataItem dataItem in nestedViewItem.NestedTableViews[0].Items)
                        {
							(dataItem.FindControl("chkChildChild") as CheckBox).Checked = false;
                            dataItem.Selected = false;
                        }
                    }
                }
				GridHeaderItem headerItemchld = RadGrid1.MasterTableView.GetItems(GridItemType.Header)[0] as GridHeaderItem;
				(headerItemchld.FindControl("chkMasterHead") as CheckBox).Checked = false;
				hidSelectedIDs.Value = string.Empty;
				hidSelectedIDs.Value = hidMasterIDs.Value;
				hidSelectedValue.Value = "0";
			}
		}

        protected void RadGrid1_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            /*RadGrid1.AllowPaging = false;
            string strMaster = string.Empty; string strOther = string.Empty;
            strMaster = GetMasterIDS();
            strOther = GetOtherIDS();
            RadGrid1.Rebind();
            RadGrid1.AllowPaging = true;*/
        }

		protected void RadGrid1_GridExporting(System.Object sender, Telerik.Web.UI.GridExportingArgs e)
		{
			try
			{
				ExportDataGridToExcel(RadGrid1, Response);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("ResultScreen : RadGrid1_GridExporting Error :: " + ex.Message, hidName.Value);
				throw ex;
			}
		}
		
        public void ExportDataGridToExcel(System.Web.UI.Control ctrl, System.Web.HttpResponse response)
        {
            response.Clear();
            response.Buffer = true;
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = "application/vnd.ms-excel";
			response.AddHeader("content-disposition", "attachment;filename=" + hidSheetName.Value.ToString() + ".xls");
            response.Charset = "";
            this.EnableViewState = false;

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            //this.ClearControls(ctrl);
            ctrl.RenderControl(oHtmlTextWriter);

            // set content type and character set to cope with european chars like the umlaut.
            response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">" + Environment.NewLine);

            // add the style props to get the page orientation
            response.Write(AddExcelStyling());
            response.Write("<span style='font-size: 7pt; font-family: Arial Narrow;'>" + RadGrid1.MasterTableView.Caption + "</span>");
            response.Write(oStringWriter.ToString());
            response.Write("</body>");
            response.Write("</html>");

            response.End();
        }

        private string AddExcelStyling()
        {
            // add the style props to get the page orientation
            StringBuilder sb = new StringBuilder();

            sb.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office'" + Environment.NewLine + "xmlns:x='urn:schemas-microsoft-com:office:excel'" + Environment.NewLine + "xmlns='http://www.w3.org/TR/REC-html40'>" + Environment.NewLine + "<head>" + Environment.NewLine);
            sb.Append("<style>" + Environment.NewLine);
            sb.Append("@page");

            //page margin can be changed based on requirement.....
            sb.Append("{margin:.10in .10in .10in .10in;" + Environment.NewLine);
            sb.Append("mso-header-margin:.10in;" + Environment.NewLine);
            sb.Append("mso-footer-margin:.10in;" + Environment.NewLine);
            sb.Append("mso-height-source:96.75pt;" + Environment.NewLine);
            sb.Append("mso-page-orientation:landscape;}" + Environment.NewLine);

            sb.Append("</style>" + Environment.NewLine);
            sb.Append("<!--[if gte mso 9]><xml>" + Environment.NewLine);
            sb.Append("<x:ExcelWorkbook>" + Environment.NewLine);
            sb.Append("<x:ExcelWorksheets>" + Environment.NewLine);
            sb.Append("<x:ExcelWorksheet>" + Environment.NewLine);
			sb.Append("<x:Name>" + hidSheetName.Value.ToString() + "</x:Name>" + Environment.NewLine);
            sb.Append("<x:WorksheetOptions>" + Environment.NewLine);
            sb.Append("<x:Print>" + Environment.NewLine);
            sb.Append("<x:ValidPrinterInfo/>" + Environment.NewLine);
            sb.Append("<x:PaperSizeIndex>9</x:PaperSizeIndex>" + Environment.NewLine);
            sb.Append("<x:HorizontalResolution>600</x:HorizontalResolution>" + Environment.NewLine);
            sb.Append("<x:VerticalResolution>600</x:VerticalResolution>" + Environment.NewLine);
			sb.Append("<x:Scale>" + hidScaleValue.Value.Trim() + "</x:Scale>" + Environment.NewLine);
            sb.Append("</x:Print>" + Environment.NewLine);
            sb.Append("<x:Selected/>" + Environment.NewLine);
            sb.Append("<x:DoNotDisplayGridlines/>" + Environment.NewLine);
            sb.Append("<x:ProtectContents>False</x:ProtectContents>" + Environment.NewLine);
            sb.Append("<x:ProtectObjects>False</x:ProtectObjects>" + Environment.NewLine);
            sb.Append("<x:ProtectScenarios>False</x:ProtectScenarios>" + Environment.NewLine);
            sb.Append("</x:WorksheetOptions>" + Environment.NewLine);
            sb.Append("</x:ExcelWorksheet>" + Environment.NewLine);
            sb.Append("</x:ExcelWorksheets>" + Environment.NewLine);
            sb.Append("<x:WindowHeight>12780</x:WindowHeight>" + Environment.NewLine);
            sb.Append("<x:WindowWidth>19035</x:WindowWidth>" + Environment.NewLine);
            sb.Append("<x:WindowTopX>0</x:WindowTopX>" + Environment.NewLine);
            sb.Append("<x:WindowTopY>0</x:WindowTopY>" + Environment.NewLine);
            sb.Append("<x:ProtectStructure>False</x:ProtectStructure>" + Environment.NewLine);
            sb.Append("<x:ProtectWindows>False</x:ProtectWindows>" + Environment.NewLine);
            sb.Append("</x:ExcelWorkbook>" + Environment.NewLine);
            sb.Append("</xml><![endif]-->" + Environment.NewLine);
            sb.Append("</head>" + Environment.NewLine);
            sb.Append("<body>" + Environment.NewLine);
            return sb.ToString();
        }

		public static string HighlightKeyWords(string Search_Str, string InputTxt)
		{
			bool fullMatch = true;
			string result;
			if (InputTxt == string.Empty || Search_Str == string.Empty || "yellow" == string.Empty)
			{
				result = InputTxt;
			}
			else
			{

                char[] array = { ',' };
                string[] words = Search_Str.Split(array, StringSplitOptions.RemoveEmptyEntries);
                

				if (!fullMatch)
				{
					result = (
						from word in words
						select word.Trim()).Aggregate(InputTxt, (string current, string pattern) => Regex.Replace(current, pattern, string.Format("<span style=\"background-color:{0}\">{1}</span>", "#FAE9AB", "$0"), RegexOptions.IgnoreCase));
				}
				else
				{
					result = (
						from word in words
						select "\\b" + word.Trim() + "\\b").Aggregate(InputTxt, (string current, string pattern) => Regex.Replace(current, pattern, string.Format("<span style=\"background-color:{0}\">{1}</span>", "#FAE9AB", "$0"), RegexOptions.IgnoreCase));
				}
			}
			return result;
		}

		protected void RadGrid1_SortCommand(object source, GridSortCommandEventArgs e)
		{
			hidSortColumn.Value = e.CommandArgument.ToString();
			hidSortDirection.Value = e.NewSortOrder.ToString();
		}
	}
}
