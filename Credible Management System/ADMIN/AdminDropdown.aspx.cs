using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using CredentialsDemo.Common;

namespace CredentialsDemo.ADMIN
{
    public partial class AdminDropdown : System.Web.UI.Page
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
                        HyperLink ht = new HyperLink();
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
                            /*ht.Target = "_self";*/
                            ht.DataBinding += new EventHandler(ht_DataBinding);
                            container.Controls.Add(ht);
                        }
                        break;
                }
            }
			
            void ht_DataBinding(object sender, EventArgs e)
            {
                try
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
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        CallingSP cmn = new CallingSP();
        Logger objLog = new Logger();

        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["sessionUserInfo"] != null)
                {
                    hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
                    objLog.LogWriter("ManageDataScreen : Page_Load starts ", hidName.Value);
                }
                else
                {
                    Response.Redirect("~/TimeOut.aspx"); 
                }
               
                gvAllDropsandGrids.RowCreated += new GridViewRowEventHandler(gvAllDropsandGrids_RowCreated);
                gvAllDropsandGrids.RowDataBound += new GridViewRowEventHandler(gvAllDropsandGrids_RowDataBound);
                if (!IsPostBack)
                {
                    cmn.LoadValues("usp_AllDropsandGrids_New", "ItemText", "tblSwitchBoardText", drp: drpAllDropsAndGrids);                    
                }
                objLog.LogWriter("ManageDataScreen : Page_Load Ends ", hidName.Value);
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("ManageDataScreen Error : Page_Load " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void gvAllDropsandGrids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HyperLink hypID = e.Row.FindControl("hypID") as HyperLink;
                    HyperLink hypText = e.Row.FindControl("hypText") as HyperLink;

                    if (hypID != null && hypText != null)
                    {
                        hypText.Attributes.Add("onClick", "ShowEditModal('" + hypID.Text + "');");
                        hypText.Attributes.Add("onMouseOver", "this.style.cursor='hand'");
                        hypText.Attributes.Add("onMouseOut", "this.style.cursor='hand'");
                    }
                    if (e.Row.Cells[2].Text.ToUpper() == "TRUE")
                    {
                        e.Row.Cells[2].Font.Bold = true;
                    }
                }
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("ManageDataScreen Error : gvAllDropsandGrids_RowDataBound " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void gvAllDropsandGrids_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    bool isnew = (bool)DataBinder.Eval(e.Row.DataItem, "Exclude");
                    if (isnew)
                    {
                        e.Row.BackColor = Color.FromName("#99FFCC");
                        e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                        e.Row.Attributes.Add("onMouseOut", "this.style.background='#99FFCC'");
                    }
                    else
                    {
                        e.Row.Attributes.Add("onMouseOver", "this.style.background='#eeff00'");
                        e.Row.Attributes.Add("onMouseOut", "this.style.background='#ffffff'");
                    }
                }
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("ManageDataScreen Error : gvAllDropsandGrids_RowCreated " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void drpAllDropsAndGrids_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {  // divRecords
                if (drpAllDropsAndGrids.SelectedItem.Text.Trim().ToUpper() != "SELECT")
                {
                    //divNoRecords.Visible = false;
                    btnReset.Visible = true;
                    if (Session["SPNAME"] != null)
                    {
                        Session.Remove("SPNAME");
                    }

                    hidDisplayColumnName.Value = string.Empty;
                    hidDisplayColumnName.Value = drpAllDropsAndGrids.SelectedItem.Text.Trim();

                    if (drpAllDropsAndGrids.SelectedItem.Text.Trim().ToUpper() != "SELECT")
                    {
                        hidSPNAME.Value = string.Empty;
                        Session.Add("SPNAME", "usp_" + drpAllDropsAndGrids.SelectedItem.Value.Trim().Replace(" ", "") + "Source~" + hidDisplayColumnName.Value);
                        hidSPNAME.Value = "usp_" + drpAllDropsAndGrids.SelectedItem.Value.Trim().Replace(" ", "") + "Source";
                        if (drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF BUYER" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF SELLER" ||
                            drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF TARGET" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "MATTER LOCATION(S)"
                            || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY WHERE MATTER OPENED" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "PREDOMINANT COUNTRY OF CLIENT"
                            || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "JURISIDICTION OF DISPUTE" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRIES OF OTHER CMS FIRMS")
                        {
                            if (Session["SPNAME"] != null)
                            {
                                Session["SPNAME"] = null;
                            }

                            Session.Add("SPNAME", "usp_CountrySource~" + hidDisplayColumnName.Value);
                            hidSPNAME.Value = "usp_CountrySource";
                        }

                        //CMS Firms Involved
                        if (drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "CMS FIRMS INVOLVED")
                        {
                            if (Session["SPNAME"] != null)
                            {
                                Session["SPNAME"] = null;
                            }

                            Session.Add("SPNAME", "usp_ReferredFromOtherCMSOfficeSource~" + hidDisplayColumnName.Value);
                            hidSPNAME.Value = "usp_ReferredFromOtherCMSOfficeSource";
                        }

                        hidDisplayColumnName.Value = drpAllDropsAndGrids.SelectedItem.Text.Trim();
                        ViewState["sortOrder"] = string.Empty;
                        gvAllDropsandGrids.PageIndex = 0;
                        GridBind();
                        btnAdd.Visible = true;
                        divRecords.Visible = true;
                        //divNoRecords.Visible = false;
                    }
                }
                else
                {
                    divRecords.Visible = false;
                }
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("ManageDataScreen Error : drpAllDropsAndGrids_SelectedIndexChanged " + ex.Message, hidName.Value);                
                throw ex;
            }
        }

        private void GridBind(string sortexpr = null, string sortdir = null)
        {
            try
            {
                DataSet ds = null;

                if (drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF BUYER" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF SELLER" ||
                            drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF TARGET" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "MATTER LOCATION(S)"
                            || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY WHERE MATTER OPENED" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "PREDOMINANT COUNTRY OF CLIENT"
                            || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "JURISIDICTION OF DISPUTE" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRIES OF OTHER CMS FIRMS")
                {
                    ds = cmn.ReturnAdminDataSet("usp_CountrySource");
                }
                else if (drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "CMS FIRMS INVOLVED")
                {
                    ds = cmn.ReturnAdminDataSet("usp_ReferredFromOtherCMSOfficeSource");
                }
                else
                {
                    ds = cmn.ReturnAdminDataSet("usp_" + drpAllDropsAndGrids.SelectedItem.Value.Trim().Replace(" ", "") + "Source");
                }


                gvAllDropsandGrids.Columns.Clear();
                //gvAllDropsandGrids.DataSource = ds.Tables[0];
                DataView myDataView = new DataView();
                myDataView = ds.Tables[0].DefaultView;

                foreach (DataColumn dcc in ds.Tables[0].Columns)
                {
                    string strID = drpAllDropsAndGrids.SelectedItem.Value.Trim().Replace(" ", "") + "Id";
                    string strValue = string.Empty;

                    if (strID == "TypeContractId")
                    {
                        strValue = "Type_Of_Contract";
                    }
                    else if (strID == "NatureWorkId")
                    {
                        strValue = "Nature_Of_Work";
                    }
                    else if (drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF BUYER" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF SELLER" ||
                            drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF TARGET" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "MATTER LOCATION(S)"
                            || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY WHERE MATTER OPENED" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "PREDOMINANT COUNTRY OF CLIENT"
                            || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "JURISIDICTION OF DISPUTE" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRIES OF OTHER CMS FIRMS")
                    {
                        strID = "CountryId";
                        strValue = "Country";
                    }
                    else if (drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "CMS FIRMS INVOLVED")
                    {
                        strID = "ReferredFromOtherCMSOfficeId";
                        strValue = "Referred_From_Other_CMS_Office";
                    }
                    else
                    {
                        strValue = drpAllDropsAndGrids.SelectedItem.Value.Trim().Replace(" ", "_");
                    }

                    if (strID.ToUpper() == dcc.ColumnName.ToUpper())
                    {
                        TemplateField mytemp = new TemplateField();
                        mytemp.HeaderText = dcc.Caption.ToString();
                        mytemp.ItemTemplate = new AddTemplateToGridView(ListItemType.Item, dcc.Caption.ToString());
                        gvAllDropsandGrids.Columns.Add(mytemp);
                        //mytemp.ItemStyle.Width = Unit.Pixel(150);
                        mytemp.Visible = false;
                    }
                    else if (strValue.ToUpper() == dcc.ColumnName.ToUpper())
                    {
                        TemplateField mytemp = new TemplateField();
                        // mytemp.HeaderText = cmn.ReturnString(dcc.Caption.ToString());
                        mytemp.HeaderText = hidDisplayColumnName.Value;
                        mytemp.ItemTemplate = new AddTemplateToGridView(ListItemType.Item, dcc.Caption.ToString());
                        //mytemp.HeaderStyle.Width = Unit.Pixel(350);hidDisplayColumnName.Value
                        //mytemp.ItemStyle.Width = Unit.Pixel(350);
                        mytemp.SortExpression = dcc.Caption.ToString();
                        mytemp.ItemStyle.Font.Underline = true;

                        mytemp.ItemStyle.Font.Bold = true;
                        mytemp.HeaderStyle.Wrap = false;
                        //mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Justify;

                        mytemp.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                        gvAllDropsandGrids.Columns.Add(mytemp);

                        mytemp.Visible = true;
                    }
                    else
                    {
                        BoundField mycol = new BoundField();
                        mycol.DataField = dcc.Caption.ToString();
                        mycol.HeaderText = cmn.ReturnString(dcc.Caption.ToString());

                        mycol.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
                        mycol.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                        mycol.ItemStyle.ForeColor = System.Drawing.Color.Black;
                        if (dcc.Caption.ToString().ToUpper() != "EXCLUDE")
                        {
                            mycol.SortExpression = dcc.Caption.ToString();                            
                        }
                        else
                        {
                            //mycol.HeaderStyle.Width = Unit.Pixel(100);
                            //mycol.ItemStyle.Width = Unit.Pixel(100);
                        }

                        gvAllDropsandGrids.Columns.Add(mycol);

                        if (dcc.ColumnName.Trim().ToUpper() == "OD" || dcc.ColumnName.Trim().ToUpper() == "BUSINESSGROUPID" || dcc.ColumnName.Trim().ToUpper() == "CONTINENTID" || dcc.ColumnName.Trim().ToUpper() == "CONTINENT")
                        {
                            mycol.Visible = false;
                        }
                        else
                        {
                            mycol.Visible = true;
                        }
                    }
                }

                if (sortexpr != null && sortdir != null)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortexpr, sortdir);
                }

                gvAllDropsandGrids.DataSource = myDataView;                  
                gvAllDropsandGrids.DataBind();
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("ManageDataScreen Error : GridBind " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm5.aspx?Add=0");
        }

        protected void gvAllDropsandGrids_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAllDropsandGrids.PageIndex = e.NewPageIndex;
            GridBind();
        }

        protected void gvAllDropsandGrids_Sorting(object sender, GridViewSortEventArgs e)
        {
            hidDisplayColumnName.Value = string.Empty;
            hidDisplayColumnName.Value = drpAllDropsAndGrids.SelectedItem.Text.Trim();
            gvAllDropsandGrids.PageIndex = 0;
            GridBind(e.SortExpression, sortOrder);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                hidSPNAME.Value = string.Empty;
                hidDisplayColumnName.Value = string.Empty;
                hidDisplayColumnName.Value = drpAllDropsAndGrids.SelectedItem.Text.Trim();
                if (Session["SPNAME"] != null)
                {
                    Session.Remove("SPNAME");
                }
                Session.Add("SPNAME", "usp_" + drpAllDropsAndGrids.SelectedItem.Value.Trim().Replace(" ", "") + "Source~" + hidDisplayColumnName.Value);
                hidSPNAME.Value = "usp_" + drpAllDropsAndGrids.SelectedItem.Text.Trim().Replace(" ", "") + "Source";
                if (drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF BUYER" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF SELLER" ||
                        drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY OF TARGET" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "MATTER LOCATION(S)"
                        || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRY WHERE MATTER OPENED" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "PREDOMINANT COUNTRY OF CLIENT"
                        || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "JURISIDICTION OF DISPUTE" || drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "COUNTRIES OF OTHER CMS FIRMS")
                {
                    if (Session["SPNAME"] != null)
                    {
                        Session["SPNAME"] = null;
                    }

                    //Session.Add("SPNAME", "usp_CountrySource");
                    Session.Add("SPNAME", "usp_CountrySource~" + hidDisplayColumnName.Value);
                    hidSPNAME.Value = "usp_CountrySource";
                }

                //CMS Firms Involved
                if (drpAllDropsAndGrids.SelectedItem.Value.Trim().ToUpper() == "CMS FIRMS INVOLVED")
                {
                    if (Session["SPNAME"] != null)
                    {
                        Session["SPNAME"] = null;
                    }
                    //Session.Add("SPNAME", "usp_ReferredFromOtherCMSOfficeSource" + hidDisplayColumnName.Value);
                    Session.Add("SPNAME", "usp_ReferredFromOtherCMSOfficeSource~" + hidDisplayColumnName.Value);
                    hidSPNAME.Value = "usp_ReferredFromOtherCMSOfficeSource";
                }
                ViewState["sortOrder"] = string.Empty;
                gvAllDropsandGrids.PageIndex = 0;
                GridBind();/*fixed for naveen*/
                btnAdd.Visible = true;
                divRecords.Visible = true;
                //divNoRecords.Visible = false;
                ModalPopupExtender2.Hide();
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("ManageDataScreen Error : btnClose_Click " + ex.Message, hidName.Value);
                
                throw ex;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                objLog.LogWriter("ManageDataScreen : btnReset_Click Starts", hidName.Value);
                drpAllDropsAndGrids.SelectedIndex = -1;
                divRecords.Visible = false;
                //divNoRecords.Visible = true;
                btnAdd.Visible = false;
                objLog.LogWriter("ManageDataScreen : btnReset_Click Ends", hidName.Value);
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("ManageDataScreen Error : btnReset_Click " + ex.Message, hidName.Value);
                throw ex;
            }
        }
    }
}
