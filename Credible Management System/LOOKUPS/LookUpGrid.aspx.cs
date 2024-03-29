﻿using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using CredentialsDemo.Common;
using System.Drawing;

namespace CredentialsDemo.LOOKUPS
{
    public partial class LookUpGrid : System.Web.UI.Page
    {
		public class AddTemplateToGridView : System.Web.UI.Page, ITemplate
        {
            ListItemType _type;
            string _colName;

            public AddTemplateToGridView(ListItemType type, string colname)
            {
                _type = type;
                _colName = colname;
            }

            void ITemplate.InstantiateIn(System.Web.UI.Control container)
            {
                switch (_type)
                {
                    case ListItemType.Item:
                        CheckBox ch = new CheckBox();
                        /*HyperLink ht = new HyperLink();
                        if (ht != null)
                        {
                            if (_colName.EndsWith("Id"))
                            {
                                ht.ID = "hypID";
                            }
                            if (!_colName.EndsWith("Id"))
                            {
                                ht.ID = "hypText";
                            }
                            
                            ht.DataBinding += new EventHandler(ht_DataBinding);
                            container.Controls.Add(ht);
                        }*/
                        if (ch != null)
                        {
                            ch.ID = "chkSelect";
                            //ch.CheckedChanged+=new EventHandler(ch_CheckedChanged);
                            container.Controls.Add(ch);
                        }
                        break;
                }
            }
			
            void ht_DataBinding(object sender, EventArgs e)
            {
                HyperLink lnk = (HyperLink)sender;
                if (lnk != null)
                {
                    GridViewRow container = (GridViewRow)lnk.NamingContainer;
                    if (container != null)
                    {
                        object dataValue = DataBinder.Eval(container.DataItem, _colName);
                        if (dataValue != DBNull.Value)
                        {
                            lnk.Text = dataValue.ToString();
                            /*lnk.NavigateUrl = "WebForm5.aspx?q=" + ((DataRowView)(container.DataItem)).Row.ItemArray[0].ToString();*/
                        }
                    }
                }
            }
        }

        public class AddTemplateToGridView1 : System.Web.UI.Page, ITemplate
        {
            ListItemType _type;
            string _colName;
            string _searchword;

            public AddTemplateToGridView1(ListItemType type, string colname, string searchword)
            {
                _type = type;
                _colName = colname;
                _searchword = searchword;
            }
			
            void ITemplate.InstantiateIn(System.Web.UI.Control container)
            {
                switch (_type)
                {
                    case ListItemType.Item:
                        Label lbl = new Label();

                        if (lbl != null)
                        {
                            lbl.DataBinding += new EventHandler(ht_DataBinding);
                            container.Controls.Add(lbl);
                        }
                        //ch.CheckedChanged+=new EventHandler(ch_CheckedChanged);
                        break;
                }
            }
			
            public string HighlightText(string searchWord, string inputText)
            {
                // Replace spaces by | for Regular Expressions
                Regex expression = new Regex(_searchword.Replace(" ", "|"), RegexOptions.IgnoreCase);
                return expression.Replace(inputText, new MatchEvaluator(ReplaceKeywords));
            }
			
            public string ReplaceKeywords(Match m)
            {
                return "<SPAN class=highlight>" + m.Value + "</SPAN>";
            }
			
            void ht_DataBinding(object sender, EventArgs e)
            {
                Label lbl = (Label)sender;
                if (lbl != null)
                {
                    GridViewRow container = (GridViewRow)lbl.NamingContainer;
                    if (container != null)
                    {
                        object dataValue = DataBinder.Eval(container.DataItem, _colName);
                        if (dataValue != DBNull.Value)
                        {
                            // lbl.Text = dataValue.ToString();
                            lbl.Text = HighlightText(_searchword, dataValue.ToString());
                        }
                    }
                }
            }
        }

        public class AddTemplateToGridViewlbl : System.Web.UI.Page, ITemplate
        {
            ListItemType _type;
            string _colName;
            int count;
            string _flag;
            public AddTemplateToGridViewlbl(ListItemType type, string colname, int k, string flag = null)
            {
                _type = type;
                _colName = colname;
                count = k;
                if (flag != null)
                {
                    _flag = flag;
                }
            }
			
            void ITemplate.InstantiateIn(System.Web.UI.Control container)
            {
                switch (_type)
                {
                    case ListItemType.Item:
                        Label lbl = new Label();
                        CheckBox ch = new CheckBox();

                        if (lbl != null)
                        {
                            if (_colName.ToUpper().EndsWith("ID"))
                            {
                                lbl.ID = "lblCountryID";
                            }
                            if (!_colName.ToUpper().EndsWith("ID"))
                            {
                                if (_flag == "A" && _flag != null)
                                {
                                    lbl.ID = "id1";
                                }
                                else if (_flag == "B" && _flag != null)
                                {
                                    lbl.ID = "lblCountryName";
                                }
                            }
                            lbl.DataBinding += new EventHandler(ht_DataBinding);
                            container.Controls.Add(lbl);
                            lbl.Attributes.Add("style", "display:none");
                        }
                        break;
                }
            }

            void ht_DataBinding(object sender, EventArgs e)
            {

                Label lbl = (Label)sender;
                if (lbl != null)
                {
                    GridViewRow container = (GridViewRow)lbl.NamingContainer;
                    if (container != null)
                    {
                        object dataValue = DataBinder.Eval(container.DataItem, _colName);
                        if (dataValue != DBNull.Value)
                        {
                            lbl.Text = dataValue.ToString();

                        }

                    }
                }
            }
        }
        public int i = 1;
        int k = 0;
        string strProcedureName = string.Empty;
        string strText = string.Empty;
        string strID = string.Empty;
        string strLabelID = string.Empty;
        protected string search_Word = String.Empty;
        string strCheckId = string.Empty;
        CallingSP objSP = new CallingSP();
		private Logger objLog = new Logger();
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
					base.Response.Redirect("~/TimeOut.aspx");
				}
                txtSearch.Attributes.Add("onkeydown", "return postBackCall(event);");

                if (!IsPostBack && Request.QueryString["q"] != null)
                {
                    hidHead.Value = Request.QueryString["q"].Split('~')[0].ToString();
                    hidID.Value = Request.QueryString["q"].Split('~')[1].ToString();
                    strID = Request.QueryString["q"].Split('~')[1].ToString();

                    strProcedureName = "usp_" + strID.Substring(8, strID.Length - 8).Replace("_", "") + "List";
                    strText = strID.Substring(8, strID.Length - 8);
                    strID = strID.Substring(8, strID.Length - 8).Replace("_", "") + "Id";
                    strCheckId = Request.QueryString["q"].Split('~')[2].ToString();

                    hidFromParent.Value = strCheckId;
                    hidIDS.Value = strCheckId;

                    if (strProcedureName == "usp_LegalIssuesList")
                    {
                        strProcedureName = "usp_LegalIssueList";
                    }
					else if (strProcedureName == "usp_LeadPartnerList")
					{
						strProcedureName = "usp_LeadPartnerList_New";
					}
					else if (strProcedureName == "usp_NatureOfWorkList")
					{
						strProcedureName = "usp_WorkTypeList";
						strID = "WorkTypeId";
						strText = "Work_Type";
					}
					else if (strProcedureName == "usp_TransactionTypeList")
					{
						strProcedureName = "usp_WorkTypeList";
						strID = "WorkTypeId";
						strText = "Work_Type";
					}
                    else if (strProcedureName == "usp_TypeOfContractList")
                    {
                        strID = "TypeContractId";
                    }

                    if (strText == "ClientIndustrySector")
                    {
                        strText = "Client_Industry_Sector";
                    }
                    else if (strText == "TransactionIndustrySector")
                    {
                        strText = "Transaction_Industry_Sector";
                    }

                    if (Request.QueryString["q"].Split('~').Length == 5)
                    {
                        GridBind(strID, strText, strProcedureName, Request.QueryString["q"].Split('~')[3].ToString());
                        hidBusinessID.Value = Request.QueryString["q"].Split('~')[3].ToString();
                    }
                    else
                    {
                        GridBind(strID, strText, strProcedureName);

                    }
                    hidID1.Value = strID;
                    hidText.Value = strText;
                    hidProcedureName.Value = strProcedureName;
                    SetIDForCheckSelection(strCheckId);
                }
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("LookUpGrid : Page_Load Error " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void GridBind(string strID, string strText, string strProcedureName, string BusinessGroupId = null)
        {
			try
			{
            gvAllLookUp.RowDataBound += new GridViewRowEventHandler(gvAllLookUp_RowDataBound);

            if (BusinessGroupId != null)
            {
                DataSet ds = objSP.ReturnDataSet(strProcedureName, "@ShowAll~0", "@BusinessGroupId~" + BusinessGroupId);
                gvAllLookUp.Columns.Clear();

					foreach (DataColumn dcc in ds.Tables[0].Columns)
					{
						string strValue = string.Empty;
						if (strID == "TypeContractId")
						{
							strValue = "Type_Of_Contract";
						}
						else if (strID == "NatureWorkId")
						{
							strValue = "Nature_Of_Work";
						}
						else
						{
							strValue = strText;
						}

						if (strID.ToUpper() == dcc.ColumnName.ToUpper())
						{
							BoundField mycol = new BoundField();
							mycol.DataField = dcc.Caption.ToString();
							mycol.Visible = false;
							gvAllLookUp.Columns.Add(mycol);
						}
						if (strID.ToUpper() == dcc.ColumnName.ToUpper())
						{
							TemplateField mytemp = new TemplateField();
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridView(ListItemType.Item, dcc.Caption.ToString());
							gvAllLookUp.Columns.Add(mytemp);
							mytemp.ItemStyle.Width = Unit.Pixel(20);
							mytemp.Visible = true;
						}
						if (strValue.ToUpper() == dcc.ColumnName.ToUpper())
						{
							TemplateField mytemp = new TemplateField();
							string flag = "A";
							k = 0;
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridViewlbl(ListItemType.Item, dcc.Caption.ToString(), k, flag);
							mytemp.HeaderStyle.Width = Unit.Pixel(200);
							mytemp.ItemStyle.Width = Unit.Pixel(200);
							mytemp.HeaderStyle.Wrap = false;

							mytemp.ItemStyle.CssClass = "panelStyle11";
							mytemp.HeaderStyle.CssClass = "panelStyle11";
							mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
							gvAllLookUp.Columns.Add(mytemp);

							mytemp.Visible = true;
						}
						if (strID.ToUpper() == dcc.ColumnName.ToUpper())
						{
							k = 0;
							TemplateField mytemp = new TemplateField();
	                        mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridViewlbl(ListItemType.Item, dcc.Caption.ToString(), k);
	                        /*mytemp.HeaderStyle.Width = Unit.Pixel(200);
	                        mytemp.ItemStyle.Width = Unit.Pixel(200);*/
							mytemp.HeaderStyle.Wrap = false;
							mytemp.ItemStyle.CssClass = "panelStyle11";
							mytemp.HeaderStyle.CssClass = "panelStyle11";
							mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
							gvAllLookUp.Columns.Add(mytemp);
							mytemp.Visible = true;
						}
						if (strValue.ToUpper() == dcc.ColumnName.ToUpper())
						{
							k = 2;
							TemplateField mytemp = new TemplateField();
							string flag = "B";
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridViewlbl(ListItemType.Item, dcc.Caption.ToString(), k, flag);
							mytemp.HeaderStyle.Width = Unit.Pixel(200);
							mytemp.ItemStyle.Width = Unit.Pixel(200);
							mytemp.HeaderStyle.Wrap = false;
							mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
							gvAllLookUp.Columns.Add(mytemp);
							mytemp.Visible = false;
						}
						if (strValue.ToUpper() == dcc.ColumnName.ToUpper())
						{
							k = 0;
							TemplateField mytemp = new TemplateField();
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridView1(ListItemType.Item, dcc.Caption.ToString(), search_Word);
							mytemp.HeaderStyle.Width = Unit.Pixel(330);
							mytemp.ItemStyle.Width = Unit.Pixel(330);
							mytemp.HeaderText = hidHead.Value;
							mytemp.HeaderStyle.Wrap = false;
							mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
							gvAllLookUp.Columns.Add(mytemp);
							mytemp.Visible = true;
						}
					}
					gvAllLookUp.DataSource = ds.Tables[0];
					gvAllLookUp.DataBind();
				}
				else
				{
					DataSet ds = objSP.ReturnDataSet(strProcedureName);
					gvAllLookUp.Columns.Clear();
					foreach (DataColumn dcc in ds.Tables[0].Columns)
					{
						string strValue = string.Empty;
						if (strID == "TypeContractId")
						{
							strValue = "Type_Of_Contract";
						}
						else if (strID == "NatureWorkId")
						{
							strValue = "Nature_Of_Work";
						}
						else
						{
							strValue = strText;
						}

						if (strID.ToUpper() == dcc.ColumnName.ToUpper())
						{
							BoundField mycol = new BoundField();
							mycol.DataField = dcc.Caption.ToString();
							mycol.Visible = false;
							gvAllLookUp.Columns.Add(mycol);
						}

						if (strID.ToUpper() == dcc.ColumnName.ToUpper())
						{
							TemplateField mytemp = new TemplateField();
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridView(ListItemType.Item, dcc.Caption.ToString());
							gvAllLookUp.Columns.Add(mytemp);
							mytemp.ItemStyle.Width = Unit.Pixel(20);
							mytemp.Visible = true;
						}

						if (strValue.ToUpper() == dcc.ColumnName.ToUpper())
						{
							TemplateField mytemp = new TemplateField();
							string flag = "A";
							k = 0;
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridViewlbl(ListItemType.Item, dcc.Caption.ToString(), k, flag);
							mytemp.HeaderStyle.Width = Unit.Pixel(200);
							mytemp.ItemStyle.Width = Unit.Pixel(200);
							mytemp.HeaderStyle.Wrap = false;
							mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
							gvAllLookUp.Columns.Add(mytemp);

							mytemp.Visible = false;
						}
						
						if (strID.ToUpper() == dcc.ColumnName.ToUpper())
						{
							k = 0;
							TemplateField mytemp = new TemplateField();
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridViewlbl(ListItemType.Item, dcc.Caption.ToString(), k);
							mytemp.HeaderStyle.Wrap = false;
							mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
							mytemp.ItemStyle.CssClass = "panelStyle11";
							mytemp.HeaderStyle.CssClass = "panelStyle11";
							gvAllLookUp.Columns.Add(mytemp);

							mytemp.Visible = true;
						}
						
						if (strValue.ToUpper() == dcc.ColumnName.ToUpper())
						{
							k = 2;
							TemplateField mytemp = new TemplateField();
							string flag = "B";
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridViewlbl(ListItemType.Item, dcc.Caption.ToString(), k, flag);
							mytemp.HeaderStyle.Width = Unit.Pixel(200);
							mytemp.ItemStyle.Width = Unit.Pixel(200);
							mytemp.HeaderStyle.Wrap = false;
							mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
							gvAllLookUp.Columns.Add(mytemp);

							mytemp.Visible = false;
						}
						if (strValue.ToUpper() == dcc.ColumnName.ToUpper())
						{
							k = 0;
							TemplateField mytemp = new TemplateField();
							mytemp.ItemTemplate = new LookUpGrid.AddTemplateToGridView1(ListItemType.Item, dcc.Caption.ToString(), search_Word);
							mytemp.HeaderStyle.Width = Unit.Pixel(330);
							mytemp.ItemStyle.Width = Unit.Pixel(330);
							mytemp.HeaderText = hidHead.Value;
							mytemp.HeaderStyle.Wrap = false;
							mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
							gvAllLookUp.Columns.Add(mytemp);

							mytemp.Visible = true;
						}
					}
					gvAllLookUp.DataSource = ds.Tables[0];
					gvAllLookUp.DataBind();
				}
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("LookUpGrid : GridBind Error " + ex.Message, hidName.Value);
				throw ex;
			}
		}
		private void SetIDForCheckSelection(string strid)
		{
			if (!string.IsNullOrEmpty(strid.Trim()))
			{
				foreach (GridViewRow row in gvAllLookUp.Rows)
				{
					for (int i = 0; i < strid.Split(',').Length; i++)
					{
						Label lbl = (Label)row.FindControl("lblCountryID");
						if (lbl.Text == strid.Split(',')[i].ToString())
						{
							CheckBox chk = (CheckBox)row.FindControl("chkSelect");
							chk.Checked = true;
							row.BackColor = System.Drawing.Color.PowderBlue;
						}
					}
				}
			}
		}
		private string GetIDForCheckSelection()
		{
			string strid = string.Empty;

			foreach (GridViewRow row in gvAllLookUp.Rows)
			{
				CheckBox chkl = (CheckBox)row.FindControl("chkSelect");

				if (chkl != null && chkl.Checked)
				{
					Label lbl = (Label)row.FindControl("lblCountryID");
					if (lbl != null)
					{
						if (string.IsNullOrEmpty(strid))
						{
							strid = lbl.Text;
						}
						else
						{
							strid = strid + "," + lbl.Text;
						}
					}
				}
			}
			return strid;
		}
		protected void cmdSearch_Click(object sender, EventArgs e)
		{
			try
			{
				search_Word = txtSearch.Text.Replace("'", "");

				if (string.IsNullOrEmpty(hidBusinessID.Value.ToString().Trim()))
				{
					GridBind(hidID1.Value, hidText.Value, hidProcedureName.Value);
				}
				else
				{
					GridBind(hidID1.Value, hidText.Value, hidProcedureName.Value, hidBusinessID.Value.ToString().Trim());
				}
				
				string str = string.Empty;
				
				for (int i = 0; i < hidIDS.Value.Trim().ToString().Split(',').Length; i++)
				{
					if (hidFromParent.Value.Trim().Contains(hidIDS.Value.Trim().ToString().Split(',')[i]))
					{
						if (string.IsNullOrEmpty(str))
						{
							str = hidIDS.Value.Trim().ToString().Split(',')[i];
						}
						else
						{
							str = str + "," + hidIDS.Value.Trim().ToString().Split(',')[i];
						}
					}
				}

				string strTemp = string.Empty;

				if (!string.IsNullOrEmpty(str))
				{
					strTemp = hidIDS.Value.Trim() + "," + str;
				}
				else
				{
					strTemp = hidIDS.Value.Trim();
				}

				SetIDForCheckSelection(strTemp);
				SetGridViewFocus();

				hidClose.Value = "0";
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("LookUpGrid : cmdSearch_Click Error " + ex.Message, hidName.Value);
				throw ex;
			}
		}
		protected void gvAllLookUp_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				if (e.Row.RowType == DataControlRowType.DataRow)
				{
					Label lbl = (Label)(e.Row.Cells[0].FindControl("id1"));
					string str = lbl.Text.Trim().ToUpper();
					if (str.Contains(txtSearch.Text.Trim().ToUpper()))
					{
						if (i == 1)
						{
							e.Row.BackColor = System.Drawing.Color.LightGray;
							Label lbl2 = (Label)(e.Row.Cells[0].FindControl("lblCountryID"));
							string str2 = lbl2.Text.Trim().ToUpper();
							hid1.Value = str2;
						}
						i++;
					}
					CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkSelect");
					chk.Attributes.Add("onclick", "test(this);");
					if (chk.Checked)
					{
						e.Row.BackColor = Color.Red;
					}
				}
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("LookUpGrid : gvAllLookUp_RowDataBound Error " + ex.Message, hidName.Value);
				throw ex;
			}
		}
		private void SetGridViewFocus()
		{
			foreach (GridViewRow row in gvAllLookUp.Rows)
			{
				Label lbl = (Label)row.FindControl("lblCountryID");
				if (lbl.Text == hid1.Value)
				{
					CheckBox chk = (CheckBox)row.FindControl("chkSelect");
					chk.Focus();
				}
			}
		}
	}
}
