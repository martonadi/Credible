using System;
using CredentialsDemo.Common;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Telerik.Web.UI;
using System.Globalization;
using System.Collections.Generic;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class Corporate : System.Web.UI.Page
    {
        CallingSP objSp = new CallingSP();
		private Logger objLog = new Logger();
        string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //txt_Cor_YearDeal_Announced.Attributes.Add("onkeypress", "return numberonly(event);");
				if (this.Session["sessionUserInfo"] != null)
				{
					this.hidName.Value = this.Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
					this.objLog.LogWriter("Corporate : Page_Load starts", this.hidName.Value);
				}
				else
				{
					base.Response.Redirect("~/TimeOut.aspx");
				}
                txt_Cor_Published_Reference.Attributes.Add("onkeypress", "return AlphaNumericonly(event);");

                if (!IsPostBack)
                {
                    //SetValues();
                    // objSp.LoadValues("usp_QuarterDealCompletedList", "Quarter_Deal_Completed", "QuarterDealCompletedId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_QuarterDealAnnouncedId);
                    objSp.LoadValues("usp_QuarterDealCompletedList", "Quarter_Deal_Completed", "QuarterDealCompletedId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_QuarterDealCompletedId);

                    objSp.LoadValues("usp_YearDealCompletedList", "Year_Deal_Completed", "YearDealCompletedId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_YearDealCompletedId);

                    objSp.LoadValues("usp_YearDealCompletedList", "Year_Deal_Completed", "YearDealCompletedId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_YearDeal_Announced);

                    objSp.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", telrad: cbo_Cor_MAStudy);

                    objSp.LoadValues("usp_QuarterDealCompletedList", "Quarter_Deal_Completed", "QuarterDealCompletedId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_QuarterDealAnnouncedId);
					
					string strCheck = "0";
				//	objSp.LoadValues("usp_YesNoList", "Yes", "YesNoId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_Value_Over_US, strCheck);

					strCheck = "0";
				//	objSp.LoadValues("usp_YesNoList", "Yes", "YesNoId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_Value_Over_Pound, strCheck);

					strCheck = "0";
				//	objSp.LoadValues("usp_YesNoList", "Yes", "YesNoId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_Value_Over_Euro, strCheck);

					objSp.LoadValues("usp_GetYesNo", "TheDescription", "TheValue", telrad: cbo_Cor_PEClients);

                    objSp.LoadValues("usp_ValueRangeList", "Value_Range", "ValueRangeId", "@ShowAll~0", "@BusinessGroupId~3", telrad: cbo_Cor_ValueRangeEuro);
                    /*No Changes done in StoredProcedure*/
                    
                    LoadCORPORATEWorkTypeView();

                    if (Session["sessCORPSS"] != null)
                    {
                        if (Session["sessCORPSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessCORPSS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {
                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
										switch (strvals[i].Split('~')[0].ToString())
										{
										case "WorkTypeMSId":
											CheckTheOneLevelItems(RadTreeView1, strvals[i].Split('~')[1].ToString());
											break;
										case "SubWorkTypeMSId":
											CheckTheTwoLevelItems(RadTreeView1, strvals[i].Split('~')[1].ToString());
											break;
										case "ActingForMS":
											txt_Cor_Acting_For.Text = strvals[i].Split('~')[1].ToString();
											hid_Cor_Acting_For_Text.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "ActingForMSId":
											hid_Cor_Acting_For.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "Country_BuyerMS":
											txt_Cor_Country_Buyer.Text = strvals[i].Split('~')[1].ToString();
											hid_Cor_Country_Buyer_Text.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "Country_BuyerMSId":
											hid_Cor_Country_Buyer.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "Country_SellerMS":
											txt_Cor_Country_Seller.Text = strvals[i].Split('~')[1].ToString();
											hid_Cor_Country_Seller_Text.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "Country_SellerMSId":
											hid_Cor_Country_Seller.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "Country_TargetMS":
											txt_Cor_Country_Target.Text = strvals[i].Split('~')[1].ToString();
											hid_Cor_Country_Target_Text.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "Country_TargetMSId":
											hid_Cor_Country_Target.Value = strvals[i].Split('~')[1].ToString();
											break;
										case "Value_Over_US":
											CheckTheItems(cbo_Cor_Value_Over_US, strvals[i].Split('~')[1]);
											break;
										case "Value_Over_Pound":
											CheckTheItems(cbo_Cor_Value_Over_Pound, strvals[i].Split('~')[1]);
											break;
										case "ValueRangeEuro":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_ValueRangeEuro);
											break;
										case "Value_Over_Euro":
											CheckTheItems(cbo_Cor_Value_Over_Euro, strvals[i].Split('~')[1]);
											break;
										case "Published_Reference":
											txt_Cor_Published_Reference.Text = strvals[i].Split('~')[1].ToString();
											break;
										case "MAStudy":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_MAStudy);
											break;
										case "PEClients":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_PEClients);
											break;
										case "QuarterDealAnnouncedId":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_QuarterDealAnnouncedId);
											break;
										case "QuarterDealCompletedId":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_QuarterDealCompletedId);
											break;
										case "YearDeal_Announced":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_YearDeal_Announced);
											break;
										case "YearDealCompletedId":
											objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Cor_YearDealCompletedId);
											break;
										case "DealAnnouncedId":
											{
												IFormatProvider culture = new CultureInfo("en-GB", true);
												cld_Cor_DealAnnouncedId.SelectedDate = new DateTime?(DateTime.Parse(strvals[i].Split('~')[1], culture));
												cld_Cor_DealAnnouncedId.DateInput.DisplayText = strvals[i].Split('~')[1];
												break;
											}
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Session["sessCorpClear"] == null)
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
                                    strWorkType = objSP.ReturnMultiselectValues("usp_CredentialWorkTypeSource", strUserID);
                                    if (strWorkType != null)
                                    {
                                        /* if (!string.IsNullOrEmpty(strWorkType[0]))
                                         {
                                             hid_Cor_Work_Type.Value = strWorkType[0].ToString();
                                         }
                                         if (!string.IsNullOrEmpty(strWorkType[1]))
                                         {
                                             txt_Cor_Work_Type.Text = strWorkType[1].ToString();
                                             hid_Cor_Work_Type_Text.Value = strWorkType[1].ToString();
                                         }*/
                                        CheckTheOneLevelItems(RadTreeView1, strWorkType[0].ToString());
                                        strWorkType = null;
                                    }

                                    //Sub Work Type  
                                    string[] strSubWorkType = new string[2];
                                    strSubWorkType = objSP.ReturnMultiselectValues("usp_CredentialSubWorkTypeSource", strUserID);
                                    if (strSubWorkType != null)
                                    {
                                        /* if (!string.IsNullOrEmpty(strSubWorkType[0]))
                                         {
                                             hid_Cor_SubWork_Type.Value = strSubWorkType[0].ToString();
                                         }
                                         if (!string.IsNullOrEmpty(strSubWorkType[1]))
                                         {
                                             txt_Cor_SubWork_Type.Text = strSubWorkType[1].ToString();
                                             hid_Cor_SubWork_Type_Text.Value = strSubWorkType[1].ToString();
                                         }*/
                                        CheckTheTwoLevelItems(RadTreeView1, strSubWorkType[0].ToString());
                                        strSubWorkType = null;
                                    }

                                    //Acting For 
                                    string[] strActingFor = new string[2];
                                    strActingFor = objSP.ReturnMultiselectValues("usp_CredentialActingForSource", strUserID);
                                    if (strActingFor != null)
                                    {
                                        if (!string.IsNullOrEmpty(strActingFor[0]))
                                        {
                                            hid_Cor_Acting_For.Value = strActingFor[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strActingFor[1]))
                                        {
                                            txt_Cor_Acting_For.Text = strActingFor[1].ToString();
                                            hid_Cor_Acting_For_Text.Value = strActingFor[1].ToString();
                                        }
                                        strActingFor = null;
                                    }

                                    //Country of Buyer
                                    string[] strCountryBuyer = new string[2];
                                    strCountryBuyer = objSP.ReturnMultiselectValues("usp_CredentialCountryBuyerSource", strUserID);
                                    if (strCountryBuyer != null)
                                    {
                                        if (!string.IsNullOrEmpty(strCountryBuyer[0]))
                                        {
                                            hid_Cor_Country_Buyer.Value = strCountryBuyer[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strCountryBuyer[1]))
                                        {
                                            txt_Cor_Country_Buyer.Text = strCountryBuyer[1].ToString();
                                            hid_Cor_Country_Buyer_Text.Value = strCountryBuyer[1].ToString();
                                        }
                                        strCountryBuyer = null;
                                    }
									
                                    //Country of Seller 
                                    string[] strCountrySeller = new string[2];
                                    strCountrySeller = objSP.ReturnMultiselectValues("usp_CredentialCountrySellerSource", strUserID);
                                    if (strCountrySeller != null)
                                    {
                                        if (!string.IsNullOrEmpty(strCountrySeller[0]))
                                        {
                                            hid_Cor_Country_Seller.Value = strCountrySeller[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strCountrySeller[1]))
                                        {
                                            txt_Cor_Country_Seller.Text = strCountrySeller[1].ToString();
                                            hid_Cor_Country_Seller.Value = strCountrySeller[1].ToString();
                                        }
                                        strCountrySeller = null;
                                    }

                                    //Country of Target 
                                    string[] strCountryTarget = new string[2];
                                    strCountryTarget = objSP.ReturnMultiselectValues("usp_CredentialCountryTargetSource", strUserID);
                                    if (strCountryTarget != null)
                                    {
                                        if (!string.IsNullOrEmpty(strCountryTarget[0]))
                                        {
                                            hid_Cor_Country_Target.Value = strCountryTarget[0].ToString();
                                        }
                                        if (!string.IsNullOrEmpty(strCountryTarget[1]))
                                        {
                                            txt_Cor_Country_Target.Text = strCountryTarget[1].ToString();
                                            hid_Cor_Country_Target_Text.Value = strCountryTarget[1].ToString();
                                        }
                                        strCountryTarget = null;
                                    }
									if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverUS"].ToString().Trim()))
									{
										CheckTheItems(cbo_Cor_Value_Over_US, dt.Rows[0]["ValueOverUS"].ToString());
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverPound"].ToString().Trim()))
									{
										//Might be a bug
										CheckTheItems(cbo_Cor_Value_Over_Euro, dt.Rows[0]["ValueOverPound"].ToString());
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["ValueOverEuro"].ToString().Trim()))
									{
										CheckTheItems(cbo_Cor_Value_Over_Euro, dt.Rows[0]["ValueOverEuro"].ToString());
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["ValueRangeEuro"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["ValueRangeEuro"].ToString(), cbo_Cor_ValueRangeEuro);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["PublishedReference"].ToString().Trim()))
									{
										txt_Cor_Published_Reference.Text = dt.Rows[0]["PublishedReference"].ToString();
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["MAStudy"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["MAStudy"].ToString(), cbo_Cor_MAStudy);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["PEClients"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["PEClients"].ToString(), cbo_Cor_PEClients);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["QuarterDealAnnounceID"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["QuarterDealAnnounceID"].ToString(), cbo_Cor_QuarterDealAnnouncedId);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["QuarterDealCompletedId"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["QuarterDealCompletedId"].ToString(), cbo_Cor_QuarterDealCompletedId);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["YearDealAnnounced"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["YearDealAnnounced"].ToString(), cbo_Cor_QuarterDealCompletedId);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["YearDealCompletedID"].ToString().Trim()))
									{
										objSP.MatchDropDownValuesText(dt.Rows[0]["YearDealCompletedID"].ToString(), cbo_Cor_YearDealCompletedId);
									}
									if (!string.IsNullOrEmpty(dt.Rows[0]["DealAnnouncedID"].ToString().Trim()))
									{
										IFormatProvider culture = new CultureInfo("en-GB", true);
										cld_Cor_DealAnnouncedId.SelectedDate = new DateTime?(DateTime.Parse(dt.Rows[0]["DealAnnouncedID"].ToString().Trim(), culture));
										cld_Cor_DealAnnouncedId.DateInput.DisplayText = dt.Rows[0]["DealAnnouncedID"].ToString().Trim();
									}
								}
								SaveCorpData();
							}
						}
					}
				}
                /*if (!string.IsNullOrEmpty(hid_Cor_Work_Type_Text.Value.Trim()))
                {
                    txt_Cor_Work_Type.Text = hid_Cor_Work_Type_Text.Value;
                }
                else
                {
                    txt_Cor_Work_Type.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(hid_Cor_SubWork_Type_Text.Value.Trim()))
                {
                    txt_Cor_SubWork_Type.Text = hid_Cor_SubWork_Type_Text.Value;
                }
                else
                {
                    txt_Cor_SubWork_Type.Text = string.Empty;
                }*/
				if (!string.IsNullOrEmpty(hid_Cor_Acting_For_Text.Value.Trim()))
				{
					txt_Cor_Acting_For.Text = hid_Cor_Acting_For_Text.Value;
				}
				else
				{
					txt_Cor_Acting_For.Text = string.Empty;
				}
				if (!string.IsNullOrEmpty(hid_Cor_Country_Buyer_Text.Value.Trim()))
				{
					txt_Cor_Country_Buyer.Text = hid_Cor_Country_Buyer_Text.Value;
				}
				else
				{
					txt_Cor_Country_Buyer.Text = string.Empty;
				}
				if (!string.IsNullOrEmpty(hid_Cor_Country_Seller_Text.Value.Trim()))
				{
					txt_Cor_Country_Seller.Text = hid_Cor_Country_Seller_Text.Value;
				}
				else
				{
					txt_Cor_Country_Seller.Text = string.Empty;
				}
				if (!string.IsNullOrEmpty(hid_Cor_Country_Target_Text.Value.Trim()))
				{
					txt_Cor_Country_Target.Text = hid_Cor_Country_Target_Text.Value;
				}
				else
				{
					txt_Cor_Country_Target.Text = string.Empty;
				}
                //img_Cor_Work_Type.Attributes.Add("onClick", "LoadWorkChild('" + lbl_Cor_Work_Type.Text + "','" + lbl_Cor_Work_Type.ID + "');return false;");
                //img_Cor_SubWork_Type.Attributes.Add("onClick", "LoadChild('" + lbl_Cor_SubWork_Type.Text + "','" + lbl_Cor_SubWork_Type.ID + "');return false;");
                img_Cor_Acting_For.Attributes.Add("onClick", "LoadChild('" + lbl_Cor_Acting_For.Text + "','" + lbl_Cor_Acting_For.ID + "','" + hid_Cor_Acting_For.ID + "');return false;");
                //img_Cor_Country_Buyer.Attributes.Add("onClick", "return ShowGridModal('" + img_Cor_Country_Buyer.ID + "','" + lbl_Cor_Country_Buyer.Text + "','" + lbl_Cor_Country_Buyer.ID + "','" + IframeGrid.ID + "');");
                // img_Cor_Country_Seller.Attributes.Add("onClick", "return ShowGridModal('" + img_Cor_Country_Seller.ID + "','" + lbl_Cor_Country_Seller.Text + "','" + lbl_Cor_Country_Seller.ID + "','" + IframeGrid.ID + "');");
                // img_Cor_Country_Target.Attributes.Add("onClick", "return ShowGridModal('" + img_Cor_Country_Target.ID + "','" + lbl_Cor_Country_Target.Text + "','" + lbl_Cor_Country_Target.ID + "','" + IframeGrid.ID + "');");
                //   img_Cor_Corporate_Know_How.Attributes.Add("onClick", "LoadChild('" + lbl_Cor_Corporate_Know_How.Text + "','" + lbl_Cor_Corporate_Know_How.ID + "');return false;");
                img_Cor_Country_Buyer.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Cor_Country_Buyer.Text + "','" + lbl_Cor_Country_Buyer.ID + "','" + hid_Cor_Country_Buyer.ID + "');return false;");
                img_Cor_Country_Seller.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Cor_Country_Seller.Text + "','" + lbl_Cor_Country_Seller.ID + "','" + hid_Cor_Country_Seller.ID + "');return false;");
                img_Cor_Country_Target.Attributes.Add("onClick", "LoadCountryChild('" + lbl_Cor_Country_Target.Text + "','" + lbl_Cor_Country_Target.ID + "','" + hid_Cor_Country_Target.ID + "');return false;");

				objLog.LogWriter("Corporate : Page_Load Ends", hidName.Value);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("Corporate : Page_Load error" + ex.Message, hidName.Value);
				throw ex;
			}
		}
		private void LoadCORPORATEWorkTypeView()
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
				if (str == "CORPORATE")
				{
					RadTreeNode root11 = new RadTreeNode(str);
					root11.Text = str;
					root11.Value = str;
					root11.Checkable = false;
					RadTreeView1.Nodes.Add(root11);
					DataTable dt = Corporate.GetDataTable("SELECT work_type,worktypeid FROM TBLWORKTYPE WHERE BUSINESSGROUPID='3' AND EXCLUDE ='0'");
					if (dt.Rows.Count > 0)
					{
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							RadTreeNode chd = new RadTreeNode(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
							root11.Nodes.Add(chd);
							chd.Checkable = true;
							DataTable dtSubWorkType = Corporate.GetDataTable("SELECT subwork_type,subworktypeid FROM TBLsubWORKTYPE WHERE worktypeid='" + dt.Rows[i][1].ToString() + "' AND EXCLUDE ='0' order by subwork_type asc");
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
		public static DataTable GetDataTable(string query)
		{
			string ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
			SqlConnection conn = new SqlConnection(ConnString);
			SqlDataAdapter adapter = new SqlDataAdapter();
			adapter.SelectCommand = new SqlCommand(query, conn);
			DataTable myDataTable = new DataTable();
			conn.Open();
			adapter.Fill(myDataTable);
			conn.Close();
			return myDataTable;
		}
		protected string ReturnString(string str)
		{
			return str.Substring(8, str.Length - 8);
		}
		protected void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				SaveCorpData();
				Label1.Visible = true;
				Label1.Text = "Details have been successfully captured. Click close button to close this page .";
				Session["sessCorpClear"] = null;
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("Corporate : btnOK_Click Error" + ex.Message, hidName.Value);
				throw ex;
			}
		}
		public void MatchDropDownValues(string dr, RadTreeView rtv, string Subdr)
		{
			if (dr != null)
			{
				for (int i = 0; i < dr.Split(',').Length; i++)
				{
					if (rtv.FindNodeByText(dr.Split(',')[i]) != null)
					{
						RadTreeNode itm = new RadTreeNode();
						itm.Value = rtv.Nodes.FindNodeByText(dr.Split(',')[i]).Value;
						itm.Text = dr.Split(',')[i];

						int index = rtv.Nodes.IndexOf(itm);
						rtv.Nodes[index].Checked = true;
					}
				}
			}
            //rtv.Nodes[0].Nodes[1].Nodes.FindNodeByText("MBI")

			if (Subdr != null)
			{
				for (int j = 0; j < Subdr.Split(',').Length; j++)
				{
					for (int i = 0; i < rtv.Nodes.Count; i++)
					{
						for (int k = 0; k < rtv.Nodes[i].Nodes.Count; k++)
						{
							if (rtv.Nodes[i].Nodes[k].Nodes.FindNodeByText(Subdr.Split(',')[j]) != null)
							{
								RadTreeNode itm = new RadTreeNode();
								itm.Value = rtv.Nodes[i].Nodes[k].Nodes.FindNodeByText(Subdr.Split(',')[j]).Value;
								itm.Text = Subdr.Split(',')[j];

								int index = rtv.Nodes[i].Nodes[k].Nodes.IndexOf(itm);
								rtv.Nodes[i].Nodes[k].Nodes[index].Checked = true;
							}
						}
					}
				}
			}
		}
		public void MatchDropDownIds(string dr, RadTreeView rtv, string Subdr)
		{
			if (dr != null)
			{
				for (int i = 0; i < dr.Split(',').Length; i++)
				{
					if (rtv.FindNodeByValue(dr.Split(',')[i]) != null)
					{
						RadTreeNode itm = new RadTreeNode();
						itm.Value = dr.Split(',')[i];
						itm.Text = rtv.Nodes.FindNodeByText(dr.Split(',')[i]).Text;
						int index = rtv.Nodes.IndexOf(itm);
						rtv.Nodes[index].Checked = true;
					}
				}
			}
			if (Subdr != null)
			{
				for (int j = 0; j < Subdr.Split(',').Length; j++)
				{
					for (int i = 0; i < rtv.Nodes.Count; i++)
					{
						for (int k = 0; k < rtv.Nodes[i].Nodes.Count; k++)
						{
							if (rtv.Nodes[i].Nodes[k].Nodes.FindNodeByValue(Subdr.Split(',')[j]) != null)
							{
								RadTreeNode itm = new RadTreeNode();
								itm.Value = Subdr.Split(',')[j];
								itm.Text = rtv.Nodes[i].Nodes[k].Nodes.FindNodeByValue(Subdr.Split(',')[j]).Text;
								int index = rtv.Nodes[i].Nodes[k].Nodes.IndexOf(itm);
								rtv.Nodes[i].Nodes[k].Nodes[index].Checked = true;
							}
						}
					}
				}
			}
		}
		private void SaveCorpData()
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
			string strCORPORATEWT = string.Empty;
			string strCORPORATESWT = string.Empty;
			Hashtable ht = new Hashtable();
			foreach (RadTreeNode node in RadTreeView1.Nodes)
			{
				if (node.Text.ToUpper() == "CORPORATE")
				{
					if (node.Nodes.Count > 0)
					{
						for (int i = 0; i < node.Nodes.Count; i++)
						{
							if (node.Nodes[i].Checked)
							{
								if (!ht.Contains(node.Nodes[i].Value))
								{
									if (string.IsNullOrEmpty(strCORPORATEWT))
									{
										strCORPORATEWT = node.Nodes[i].Value + ",";
									}
									else
									{
										strCORPORATEWT = strCORPORATEWT + node.Nodes[i].Value + ",";
									}
									ht.Add(node.Nodes[i].Value, node.Nodes[i].Value);
								}
							}
						}
						string strText = string.Empty;
						for (int i = 0; i < node.Nodes.Count; i++)
						{
							bool bln = false;
							for (int j = 0; j < node.Nodes[i].Nodes.Count; j++)
							{
								if (node.Nodes[i].Nodes[j].Checked)
								{
									if (!bln)
									{
										if (!ht.Contains(node.Nodes[i].Value))
										{
											if (string.IsNullOrEmpty(strCORPORATEWT))
											{
												strCORPORATEWT = node.Nodes[i].Value + ",";
											}
											else
											{
												strCORPORATEWT = strCORPORATEWT + node.Nodes[i].Value + ",";
											}
											ht.Add(node.Nodes[i].Value, node.Nodes[i].Value);
										}
										if (string.IsNullOrEmpty(strText))
										{
											strText = "'" + node.Nodes[i].Text + "',";
										}
										else
										{
											strText = strText + "'" + node.Nodes[i].Text + "',";
										}
										bln = true;
									}
									if (string.IsNullOrEmpty(strCORPORATESWT))
									{
										strCORPORATESWT = node.Nodes[i].Nodes[j].Value + ",";
									}
									else
									{
										strCORPORATESWT = strCORPORATESWT + node.Nodes[i].Nodes[j].Value + ",";
									}
								}
							}
						}
						ht = null;
						if (!string.IsNullOrEmpty(strCORPORATEWT))
						{
							strCORPORATEWT = strCORPORATEWT.Substring(0, strCORPORATEWT.Length - 1);
						}
						if (!string.IsNullOrEmpty(strCORPORATESWT))
						{
							strCORPORATESWT = strCORPORATESWT.Substring(0, strCORPORATESWT.Length - 1);
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(strCORPORATEWT))
			{
				strMS.Append(ReturnString(lbl_Cor_Work_Type.ID));
				strMS.Append("~");
				strMS.Append(strCORPORATEWT);
				strMS.Append("|");
				strSS.Append("WorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strCORPORATEWT);
				strSS.Append("|");
			}
			if (!string.IsNullOrEmpty(strCORPORATESWT))
			{
				strMS.Append("SubWork_Type");
				strMS.Append("~");
				strMS.Append(strCORPORATESWT);
				strMS.Append("|");
				strSS.Append("SubWorkTypeMSId");
				strSS.Append("~");
				strSS.Append(strCORPORATESWT);
				strSS.Append("|");
			}
			string strhid_Cor_Acting_For = string.Empty;
			if (!string.IsNullOrEmpty(hid_Cor_Acting_For.Value))
			{
				strhid_Cor_Acting_For = hid_Cor_Acting_For.Value;
				strMS.Append(ReturnString(lbl_Cor_Acting_For.ID));
				strMS.Append("~");
				strMS.Append(strhid_Cor_Acting_For);
				strMS.Append("|");
				strSS.Append("ActingForMS");
				strSS.Append("~");
				strSS.Append(txt_Cor_Acting_For.Text.Trim());
				strSS.Append("|");
				strSS.Append("ActingForMSId");
				strSS.Append("~");
				strSS.Append(strhid_Cor_Acting_For);
				strSS.Append("|");
			}
			string strcbo_Cor_Value_Over_Pound = string.Empty;
			if (cbo_Cor_Value_Over_Pound.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_Value_Over_Pound.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_Value_Over_Pound = getCheckedItems(cbo_Cor_Value_Over_Pound);
				strMS.Append("Value_Over_Pound_MS");
				strMS.Append("~");
				strMS.Append(strcbo_Cor_Value_Over_Pound);
				strMS.Append("|");
				strSS.Append(ReturnString(lbl_Cor_Value_Over_Pound.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_Value_Over_Pound);
				strSS.Append("|");
			}
			string strhid_Cor_Country_Seller = string.Empty;
			if (!string.IsNullOrEmpty(hid_Cor_Country_Seller.Value))
			{
				strhid_Cor_Country_Seller = hid_Cor_Country_Seller.Value;
				strMS.Append(ReturnString(lbl_Cor_Country_Seller.ID));
				strMS.Append("~");
				strMS.Append(strhid_Cor_Country_Seller);
				strMS.Append("|");
				strSS.Append("Country_SellerMS");
				strSS.Append("~");
				strSS.Append(txt_Cor_Country_Seller.Text.Trim());
				strSS.Append("|");
				strSS.Append("Country_SellerMSId");
				strSS.Append("~");
				strSS.Append(strhid_Cor_Country_Seller);
				strSS.Append("|");
			}
			string strhid_Cor_Country_Buyer = string.Empty;
			if (!string.IsNullOrEmpty(hid_Cor_Country_Buyer.Value))
			{
				strhid_Cor_Country_Buyer = hid_Cor_Country_Buyer.Value;
				strMS.Append(ReturnString(lbl_Cor_Country_Buyer.ID));
				strMS.Append("~");
				strMS.Append(strhid_Cor_Country_Buyer);
				strMS.Append("|");
				strSS.Append("Country_BuyerMS");
				strSS.Append("~");
				strSS.Append(txt_Cor_Country_Buyer.Text.Trim());
				strSS.Append("|");
				strSS.Append("Country_BuyerMSId");
				strSS.Append("~");
				strSS.Append(strhid_Cor_Country_Buyer);
				strSS.Append("|");
			}
			string strhid_Cor_Country_Target = string.Empty;
			if (!string.IsNullOrEmpty(hid_Cor_Country_Target.Value))
			{
				strhid_Cor_Country_Target = hid_Cor_Country_Target.Value;
				strMS.Append(ReturnString(lbl_Cor_Country_Target.ID));
				strMS.Append("~");
				strMS.Append(strhid_Cor_Country_Target);
				strMS.Append("|");
				strSS.Append("Country_TargetMS");
				strSS.Append("~");
				strSS.Append(txt_Cor_Country_Target.Text.Trim());
				strSS.Append("|");
				strSS.Append("Country_TargetMSId");
				strSS.Append("~");
				strSS.Append(strhid_Cor_Country_Target);
				strSS.Append("|");
			}
			string strcbo_Cor_Value_Over_US = string.Empty;
			if (cbo_Cor_Value_Over_US.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_Value_Over_US.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_Value_Over_US = getCheckedItems(cbo_Cor_Value_Over_US);
				strMS.Append("Value_Over_US_MS");
				strMS.Append("~");
				strMS.Append(strcbo_Cor_Value_Over_US);
				strMS.Append("|");
				strSS.Append(ReturnString(lbl_Cor_Value_Over_US.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_Value_Over_US);
				strSS.Append("|");
			}
			string strcbo_Cor_ValueRangeEuro = string.Empty;
			if (cbo_Cor_ValueRangeEuro.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_ValueRangeEuro.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_ValueRangeEuro = cbo_Cor_ValueRangeEuro.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_Cor_ValueRangeEuro.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_ValueRangeEuro);
				strSS.Append("|");
			}
			string strcbo_Cor_Value_Over_Euro = string.Empty;
			if (cbo_Cor_Value_Over_Euro.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_Value_Over_Euro.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_Value_Over_Euro = getCheckedItems(cbo_Cor_Value_Over_Euro);
				strMS.Append("Value_Over_Euro_MS");
				strMS.Append("~");
				strMS.Append(strcbo_Cor_Value_Over_Euro);
				strMS.Append("|");
				strSS.Append(ReturnString(lbl_Cor_Value_Over_Euro.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_Value_Over_Euro);
				strSS.Append("|");
			}
			string strtxt_Cor_Published_Reference = string.Empty;
			if (string.IsNullOrEmpty(txt_Cor_Published_Reference.Text.Trim()))
			{
				strSS.Append(ReturnString(lbl_Cor_Published_Reference.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strtxt_Cor_Published_Reference = txt_Cor_Published_Reference.Text.Trim();
				strSS.Append(ReturnString(lbl_Cor_Published_Reference.ID));
				strSS.Append("~");
				strSS.Append(strtxt_Cor_Published_Reference);
				strSS.Append("|");
			}
			string strcbo_Cor_MAStudy = string.Empty;
			if (cbo_Cor_MAStudy.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_MAStudy.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_MAStudy = cbo_Cor_MAStudy.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_Cor_MAStudy.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_MAStudy);
				strSS.Append("|");
			}
			string strcbo_Cor_PEClients = string.Empty;
			if (cbo_Cor_PEClients.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_PEClients.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_PEClients = cbo_Cor_PEClients.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_Cor_PEClients.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_PEClients);
				strSS.Append("|");
			}
			string strcbo_Cor_QuarterDealAnnouncedId = string.Empty;
			if (cbo_Cor_QuarterDealAnnouncedId.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_QuarterDealAnnouncedId.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_QuarterDealAnnouncedId = cbo_Cor_QuarterDealAnnouncedId.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_Cor_QuarterDealAnnouncedId.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_QuarterDealAnnouncedId);
				strSS.Append("|");
			}
			string strcbo_Cor_QuarterDealCompletedId = string.Empty;
			if (cbo_Cor_QuarterDealCompletedId.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_QuarterDealCompletedId.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_QuarterDealCompletedId = cbo_Cor_QuarterDealCompletedId.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_Cor_QuarterDealCompletedId.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_QuarterDealCompletedId);
				strSS.Append("|");
			}
			string strtxt_Cor_YearDeal_Announced = string.Empty;
			if (cbo_Cor_YearDeal_Announced.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_YearDeal_Announced.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strtxt_Cor_YearDeal_Announced = cbo_Cor_YearDeal_Announced.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_Cor_YearDeal_Announced.ID));
				strSS.Append("~");
				strSS.Append(strtxt_Cor_YearDeal_Announced);
				strSS.Append("|");
			}
			string strcbo_Cor_YearDealCompletedId = string.Empty;
			if (cbo_Cor_YearDealCompletedId.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Cor_YearDealCompletedId.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Cor_YearDealCompletedId = cbo_Cor_YearDealCompletedId.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_Cor_YearDealCompletedId.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Cor_YearDealCompletedId);
				strSS.Append("|");
			}
			string strcld_Cor_DealAnnouncedId = string.Empty;
			if (!cld_Cor_DealAnnouncedId.IsEmpty)
			{
				string strD = string.Concat(new object[]
				{
					cld_Cor_DealAnnouncedId.DateInput.SelectedDate.Value.Day,
					"/",
					cld_Cor_DealAnnouncedId.DateInput.SelectedDate.Value.Month,
					"/",
					cld_Cor_DealAnnouncedId.DateInput.SelectedDate.Value.Year
				});
				strcld_Cor_DealAnnouncedId = strD;
				strSS.Append(ReturnString(lbl_Cor_DealAnnouncedId.ID));
				strSS.Append("~");
				strSS.Append(strcld_Cor_DealAnnouncedId);
				strSS.Append("|");
			}
			string strMSstr = strMS.ToString();
			string strSSstr = strSS.ToString();
			if (!string.IsNullOrEmpty(strSSstr))
			{
				strSS = strSS.Remove(strSS.Length - 1, 1);
				Session.Add("sessCORPSS", strSS);
				strSS = null;
				strSSstr = null;
			}
			if (!string.IsNullOrEmpty(strMSstr))
			{
				strMS = strMS.Remove(strMS.Length - 1, 1);
				Session.Add("sessCORPMS", strMS);
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
				if (Session["sessCORPSS"] != null)
				{
					Session.Remove("sessCORPSS");
				}
				if (Session["sessCORPMS"] != null)
				{
					Session.Remove("sessCORPMS");
				}
				txt_Cor_Acting_For.Text = string.Empty;
				hid_Cor_Acting_For.Value = string.Empty;
				hid_Cor_Acting_For_Text.Value = string.Empty;
				txt_Cor_Published_Reference.Text = string.Empty;
				foreach (RadTreeNode node in RadTreeView1.GetAllNodes())
				{
					node.Checked = false;
					node.Expanded = false;
				}
				cbo_Cor_YearDeal_Announced.SelectedIndex = 0;
				cbo_Cor_MAStudy.SelectedIndex = 0;
				cbo_Cor_PEClients.SelectedIndex = 0;
				cbo_Cor_QuarterDealAnnouncedId.SelectedIndex = 0;
				cbo_Cor_QuarterDealCompletedId.SelectedIndex = 0;
				cbo_Cor_Value_Over_Pound.ClearCheckedItems();
				cbo_Cor_Value_Over_US.ClearCheckedItems();
				cbo_Cor_ValueRangeEuro.SelectedIndex = 0;
				cbo_Cor_YearDealCompletedId.SelectedIndex = 0;
				cbo_Cor_Value_Over_Euro.ClearCheckedItems();
				txt_Cor_Country_Buyer.Text = string.Empty;
				hid_Cor_Country_Buyer.Value = string.Empty;
				hid_Cor_Country_Buyer_Text.Value = string.Empty;
				txt_Cor_Country_Seller.Text = string.Empty;
				hid_Cor_Country_Seller.Value = string.Empty;
				hid_Cor_Country_Seller_Text.Value = string.Empty;
				txt_Cor_Country_Target.Text = string.Empty;
				hid_Cor_Country_Target.Value = string.Empty;
				hid_Cor_Country_Target_Text.Value = string.Empty;
				cld_Cor_DealAnnouncedId.Clear();
				if (Session["sessCorpClear"] != null)
				{
					Session.Remove("sessCorpClear");
				}
				Session.Add("sessCorpClear", "0");
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("Corporate : btnClear_Click Error" + ex.Message, hidName.Value);
				throw ex;
			}
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
						strItems = (item.Value.Trim() ?? "");
					}
					else
					{
						strItems = strItems + "," + item.Value.Trim();
					}
				}
			}
			return strItems;
		}
		
		private void CheckTheItems(RadComboBox comboBox, string strItems)
		{
			RadComboBoxItemCollection collection = comboBox.Items;
			for (int i = 0; i < collection.Count; i++)
			{
				for (int j = 0; j < strItems.Split(new char[]
				{
					','
				}).Length; j++)
				{
					if (collection[i].Value == strItems.Split(new char[]
					{
						','
					})[j].Trim())
					{
						collection[i].Checked = true;
						break;
					}
				}
			}
		}
		
		private static void CheckTheOneLevelItems(RadTreeView tv, string strItems)
		{
			foreach (RadTreeNode node in tv.Nodes)
			{
				if (node.Text.ToUpper() == "CORPORATE")
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
				if (node.Text.ToUpper() == "CORPORATE")
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
