using System;
using System.Data;
using System.Web.UI;
using CredentialsDemo.Common;

namespace CredentialsDemo.ADMIN
{
    public partial class AdminEntryDetails : System.Web.UI.Page
    {
        CallingSP cmn = new CallingSP();
        Logger objLog = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtDropDownName.Attributes.Add("onkeypress", "return Alpha(event);");
                if (Session["sessionUserInfo"] != null)
                {
					hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
                    if (Session["SPNAME"] != null)
                    {
                        hidDisplyColumnName.Value = Session["SPNAME"].ToString().Split('~')[1];
                    }
                }
                else
                {
                    Response.Redirect("~/TimeOut.aspx");
                }

                objLog.LogWriter("AdminEntryDetails : Page_Load starts ", hidName.Value);

                if (Session["SPNAME"] != null && Request.QueryString["q"] != null)
                {
                    LoadValuesInControlsForUpdate();
                    btnAdd.Visible = false;
                }
                if (Session["SPNAME"] != null && Request.QueryString["Add"] != null && Request.QueryString["Add"].Split('~')[0] == "0")
                {
                    LoadColumnNamesInControlsForAdd();
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                objLog.LogWriter("AdminEntryDetails : Page_Load Ends ", hidName.Value);
                
            }
            catch (Exception ex)
            {
                 objLog.ErrorWriter("AdminEntryDetails Error : Page_Load " +ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void GetOdMaxValue(string tblvalue)
        {
            try
            {
                string sql = "select max(Od)+1 from " + tblvalue;
                lblod.Text = Convert.ToString(cmn.ReturnScalar(SQL: sql));
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("AdminEntryDetails Error : GetOdMaxValue " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void LoadValuesInControlsForUpdate()
        {
            try
            {
                objLog.LogWriter("AdminEntryDetails : LoadValuesInControlsForUpdate starts ", hidName.Value);

                hidSPName.Value = Session["SPNAME"].ToString().Split('~')[0];

                string strID = Request.QueryString["q"].ToString().Split('~')[0];
                DataSet ds = cmn.ReturnAdminDataSet(Session["SPNAME"].ToString().Split('~')[0], "@TheRecord~" + strID);

                /*Converting usp_ActingForSource - tblActingFor Logic*/
                string str = Session["SPNAME"].ToString().Split('~')[0].Split('_')[1].ToString();
                hidTableName.Value = "tbl" + str.Substring(0, str.Length - 6);
                hidColumnCount.Value = ds.Tables[0].Columns.Count.ToString();

                Session.Remove("SPNAME");

                /*subworktype screen loads different values (work type) in combobox all others load business group*/
                if (ds.Tables[0].Columns[0].ColumnName.ToString().ToUpper() != "SUBWORKTYPEID")
                {
                    cmn.LoadValues("usp_BusinessGroupSource", "Business_Group", "BusinessGroupId", drp: drpBusinessGroup);
                }
                else
                {
                    cmn.LoadValues("usp_WorkTypeSource", "Work_Type", "WorkTypeId", drp: drpBusinessGroup);
                }

                /* Columns-Id,Text,Exclude,Od,BusinessGroupId,BusinessGroup*/
                if (ds.Tables[0].Columns.Count == 6)
                {
                    lblID.Text = ds.Tables[0].Columns[0].ColumnName.ToString(); //ID
                    lblDropDownName.Text = ds.Tables[0].Columns[1].ColumnName.ToString(); // DROPDOWNNAME
                    //lblDropDownNameDisplayVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString()); // DROPDOWNNAME
                    lblDropDownNameDisplayVisible.Text = hidDisplyColumnName.Value;
                    //tbd
                    lblDropDownNameDisplay.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString()); // DROPDOWNNAME

                    lblExcludeVisible.Text = ds.Tables[0].Columns[2].ColumnName.ToString(); //EXCLUDE
                    lblExclude.Text = ds.Tables[0].Columns[2].ColumnName.ToString(); //EXCLUDE
                    string a = ds.Tables[0].Columns[5].ColumnName.ToString();
                    string b = a.Split('_')[0].ToString() + a.Split('_')[1].ToString();
                    lblBusinessGroup.Text = b;
                    lblBusinessGroupVisible.Text = b;
                    //lblBusinessGroup.Text = ds.Tables[0].Columns[5].ColumnName.ToString(); //BUSINESSGROUPNAME
                    lblBusinessGroupId.Text = ds.Tables[0].Columns[4].ColumnName.ToString();//BUSINESSGROUPID

					txtID.Text = ds.Tables[0].Rows[0][0].ToString();
					txtDropDownName.Text = ds.Tables[0].Rows[0][1].ToString();
					txtExclude.Text = ds.Tables[0].Rows[0][2].ToString();

					if (ds.Tables[0].Rows[0][2].ToString() == "0" || ds.Tables[0].Rows[0][2].ToString().ToUpper() == "FALSE")
                    {
                        chkExclude.Checked = false;
                    }
                    else
                    {
                        chkExclude.Checked = true;
                    }
                        //Label8.Text = ds.Tables[0].Rows[i][5].ToString() + "~" + ds.Tables[0].Rows[i][4].ToString();
					cmn.MatchDropDownValues(ds.Tables[0].Rows[0][5].ToString().Trim(), drpBusinessGroup);
				}
				else if (ds.Tables[0].Columns.Count == 5)
				{
                    lblID.Text = ds.Tables[0].Columns[0].ColumnName.ToString(); //ID
					txtID.Text = ds.Tables[0].Rows[0][0].ToString();
					lblDropDownName.Text = ds.Tables[0].Columns[2].ColumnName.ToString(); // DROPDOWNNAME
                        //lblDropDownNameDisplayVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[2].ColumnName.ToString()); // DROPDOWNNAME
					lblDropDownNameDisplayVisible.Text = hidDisplyColumnName.Value;
            
            		lblDropDownNameDisplay.Text = cmn.ReturnString(ds.Tables[0].Columns[2].ColumnName.ToString()); // DROPDOWNNAME
					txtDropDownName.Text = ds.Tables[0].Rows[0][2].ToString();
                    lblExcludeVisible.Text = ds.Tables[0].Columns[4].ColumnName.ToString(); //EXCLUDE
                    lblExclude.Text = ds.Tables[0].Columns[4].ColumnName.ToString(); //EXCLUDE
					txtExclude.Text = ds.Tables[0].Rows[0][4].ToString();
					if (ds.Tables[0].Rows[0][4].ToString().ToUpper() == "FALSE" || ds.Tables[0].Rows[0][4].ToString() == "0")
					{
						chkExclude.Checked = false;
					}
					else
					{
						chkExclude.Checked = true;
					}

                    lblBusinessGroupVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[3].ColumnName.ToString());
                    lblBusinessGroup.Text = cmn.ReturnString(ds.Tables[0].Columns[3].ColumnName.ToString()); //BUSINESSGROUPNAME
                    lblBusinessGroupId.Text = ds.Tables[0].Columns[1].ColumnName.ToString();//BUSINESSGROUPID

                    /* Country loads the continents in the dropdown */
					if (ds.Tables[0].Columns[0].ColumnName.ToString().Trim().ToUpper() == "COUNTRYID")
					{
						drpBusinessGroup.Items.Clear();
                        cmn.LoadValues("usp_ContinentSource", "Continent", "ContinentID", drp: drpBusinessGroup);
					}
					cmn.MatchDropDownValues(ds.Tables[0].Rows[0][3].ToString().Trim(), drpBusinessGroup);
				}
                /*Columns(3) -Id,Text,Exclude */
                /*Cloumns(4) - Id,Text,Exclude,Od */
				else if (ds.Tables[0].Columns.Count == 4 || ds.Tables[0].Columns.Count == 3)
				{
					if (ds.Tables[0].Columns[0].ColumnName.ToString().ToUpper() != "CONTINENTID")
					{
                        lblID.Text = ds.Tables[0].Columns[0].ColumnName.ToString(); //ID
						txtID.Text = ds.Tables[0].Rows[0][0].ToString();
                        lblDropDownName.Text = ds.Tables[0].Columns[1].ColumnName.ToString(); // DROPDOWNNAME
                        lblDropDownNameDisplay.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString()); // DROPDOWNNAME
                        lblDropDownNameDisplayVisible.Text = hidDisplyColumnName.Value;
                        //lblDropDownNameDisplayVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString());
						txtDropDownName.Text = ds.Tables[0].Rows[0][1].ToString();
						lblExcludeVisible.Text = ds.Tables[0].Columns[2].ColumnName.ToString();
                        lblExclude.Text = ds.Tables[0].Columns[2].ColumnName.ToString(); //EXCLUDE
						txtExclude.Text = ds.Tables[0].Rows[0][2].ToString();
						if (ds.Tables[0].Rows[0][2].ToString().ToUpper() == "FALSE" || ds.Tables[0].Rows[0][2].ToString().ToUpper() == "0")
						{
							chkExclude.Checked = false;
						}
						else
						{
							chkExclude.Checked = true;
						}
						Label8.Visible = false;
						lblBusinessGroup.Visible = false;
						drpBusinessGroup.Visible = false;
					}
                    else /*Columns - id,Text,Text,Exclude */
                    {
                        lblID.Text = ds.Tables[0].Columns[0].ColumnName.ToString(); //ID
						txtID.Text = ds.Tables[0].Rows[0][0].ToString();
                        lblDropDownName.Text = ds.Tables[0].Columns[1].ColumnName.ToString(); // DROPDOWNNAME
                        // lblDropDownNameDisplayVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString()); // DROPDOWNNAME
                        lblDropDownNameDisplayVisible.Text = hidDisplyColumnName.Value;
                        lblDropDownNameDisplay.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString()); // DROPDOWNNAME
						txtDropDownName.Text = ds.Tables[0].Rows[0][1].ToString();
                        lblExcludeVisible.Text = ds.Tables[0].Columns[3].ColumnName.ToString();
                        lblExclude.Text = ds.Tables[0].Columns[3].ColumnName.ToString(); //EXCLUDE
						txtExclude.Text = ds.Tables[0].Rows[0][3].ToString();
						if (ds.Tables[0].Rows[0][3].ToString().ToUpper() == "FALSE" || ds.Tables[0].Rows[0][3].ToString().ToUpper() == "0")
						{
							chkExclude.Checked = false;
						}
						else
						{
							chkExclude.Checked = true;
						}
						Label8.Visible = false;
						lblBusinessGroup.Visible = false;
						drpBusinessGroup.Visible = false;
					}
				}
				
				hidDBValue.Value = txtDropDownName.Text.Trim().ToUpper();
				objLog.LogWriter("AdminEntryDetails : LoadValuesInControlsForUpdate Ends ", hidName.Value);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("AdminEntryDetails Error : LoadValuesInControlsForUpdate " + ex.Message, hidName.Value);
				throw ex;
			}
		}
        private void LoadColumnNamesInControlsForAdd()
        {
            try
            {
                objLog.LogWriter("AdminEntryDetails : LoadColumnNamesInControlsForAdd starts ", hidName.Value);

                hidSPName.Value = Session["SPNAME"].ToString().Split('~')[0];
                DataSet ds = cmn.ReturnAdminDataSet(Session["SPNAME"].ToString().Split('~')[0]);

                /*Converting usp_ActingForSource - tblActingFor Logic*/
                string str = Session["SPNAME"].ToString().Split('~')[0].Split('_')[1].ToString();
                hidTableName.Value = "tbl" + str.Substring(0, str.Length - 6);
                hidColumnCount.Value = ds.Tables[0].Columns.Count.ToString();

                Session.Remove("SPNAME");
                if (ds.Tables[0].Columns[0].ColumnName.ToString().ToUpper() != "SUBWORKTYPEID")
                {
                    cmn.LoadValues("usp_BusinessGroupSource", "Business_Group", "BusinessGroupId", drp: drpBusinessGroup);
                }
                else
                {
                    cmn.LoadValues("usp_WorkTypeSource", "Work_Type", "WorkTypeId", drp: drpBusinessGroup);
                }

                if (ds.Tables[0].Columns.Count == 6)/* Columns-Id,Text,Exclude,Od,BusinessGroupId,BusinessGroup*/
                {
                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                        lblID.Text = ds.Tables[0].Columns[0].ColumnName.ToString().Trim(); //ID
                        lblDropDownName.Text = ds.Tables[0].Columns[1].ColumnName.ToString().Trim(); // DROPDOWNNAME
                        //lblDropDownNameDisplayVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString().Trim()); // DROPDOWNNAME
                        lblDropDownNameDisplayVisible.Text = hidDisplyColumnName.Value;
                        lblDropDownNameDisplay.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString().Trim()); // DROPDOWNNAME
                        lblExcludeVisible.Text = ds.Tables[0].Columns[2].ColumnName.ToString().Trim(); //EXCLUDE
                        lblExclude.Text = ds.Tables[0].Columns[2].ColumnName.ToString().Trim(); //EXCLUDE
                        lblBusinessGroupVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[5].ColumnName.ToString().Trim()); //BUSINESSGROUP
                        lblBusinessGroup.Text = cmn.ReturnString(ds.Tables[0].Columns[5].ColumnName.ToString().Trim()); //BUSINESSGROUP
                        lblBusinessGroupId.Text = cmn.ReturnString(ds.Tables[0].Columns[4].ColumnName.ToString().Trim());//BUSINESSGROUPID
                        GetOdMaxValue(hidTableName.Value);

                    //}
                }
                else if (ds.Tables[0].Columns.Count == 5) /* Country,Quarter Deal and Year Deal Columns- Id,BusinessGroupId,Text,BusinessGroup,Exclude*/
                {
                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                        lblID.Text = ds.Tables[0].Columns[0].ColumnName.ToString().Trim(); //ID
                        lblDropDownName.Text = ds.Tables[0].Columns[2].ColumnName.ToString().Trim(); // DROPDOWNNAME
                        //lblDropDownNameDisplayVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[2].ColumnName.ToString().Trim()); // DROPDOWNNAME
                        lblDropDownNameDisplayVisible.Text = hidDisplyColumnName.Value;
                        lblDropDownNameDisplay.Text = cmn.ReturnString(ds.Tables[0].Columns[2].ColumnName.ToString().Trim()); // DROPDOWNNAME
                        lblExcludeVisible.Text = ds.Tables[0].Columns[4].ColumnName.ToString().Trim(); //EXCLUDE
                        lblExclude.Text = ds.Tables[0].Columns[4].ColumnName.ToString().Trim(); //EXCLUDE
                        lblBusinessGroupVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[3].ColumnName.ToString().Trim()); //BUSINESSGROUP
                        lblBusinessGroup.Text = cmn.ReturnString(ds.Tables[0].Columns[3].ColumnName.ToString().Trim()); //BUSINESSGROUP
                        lblBusinessGroupId.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString().Trim());//BUSINESSGROUPID
                        if (ds.Tables[0].Columns[0].ColumnName.ToString().Trim().ToUpper() == "COUNTRYID")
                        {
                            drpBusinessGroup.Items.Clear();
                            cmn.LoadValues("usp_ContinentSource", "Continent", "ContinentID", drp: drpBusinessGroup);

                        }
                    //}
                }
                else if (ds.Tables[0].Columns.Count == 4 || ds.Tables[0].Columns.Count == 3)/*Columns(3) -Id,Text,Exclude Cloumns(4) - Id,Text,Exclude,Od */
                {
                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    //{
                        if (ds.Tables[0].Columns[0].ColumnName.ToString().Trim().ToUpper() != "CONTINENTID")
                        {
                            lblID.Text = ds.Tables[0].Columns[0].ColumnName.ToString().Trim(); //ID
                            lblDropDownName.Text = ds.Tables[0].Columns[1].ColumnName.ToString().Trim(); // DROPDOWNNAME
                            //lblDropDownNameDisplay.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString().Trim()); // DROPDOWNNAME
                            lblDropDownNameDisplayVisible.Text = hidDisplyColumnName.Value;
                            lblDropDownNameDisplayVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString().Trim());
                            lblExcludeVisible.Text = ds.Tables[0].Columns[2].ColumnName.ToString().Trim(); //EXCLUDE
                            lblExclude.Text = ds.Tables[0].Columns[2].ColumnName.ToString().Trim(); //EXCLUDE
                            if (ds.Tables[0].Columns.Count == 4)/* Table with 3 columns , od column does not exist*/
                            {
                                GetOdMaxValue(hidTableName.Value);
                            }

                            Label8.Visible = false;
                            lblBusinessGroup.Visible = false;
                            drpBusinessGroup.Visible = false;
                        }
                        else
                        {
                            lblID.Text = ds.Tables[0].Columns[0].ColumnName.ToString().Trim(); //ID
                            lblDropDownName.Text = ds.Tables[0].Columns[1].ColumnName.ToString().Trim(); // DROPDOWNNAME
                            //lblDropDownNameDisplayVisible.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString().Trim()); // DROPDOWNNAME
                            lblDropDownNameDisplayVisible.Text = hidDisplyColumnName.Value;
                            lblDropDownNameDisplay.Text = cmn.ReturnString(ds.Tables[0].Columns[1].ColumnName.ToString().Trim()); // DROPDOWNNAME
                            lblExcludeVisible.Text = ds.Tables[0].Columns[3].ColumnName.ToString().Trim(); //EXCLUDE
                            lblExclude.Text = ds.Tables[0].Columns[3].ColumnName.ToString().Trim(); //EXCLUDE

                            Label8.Visible = false;
                            lblBusinessGroup.Visible = false;
                            drpBusinessGroup.Visible = false;
                        }
                    //}
                }
                objLog.LogWriter("AdminEntryDetails : LoadColumnNamesInControlsForAdd Ends ", hidName.Value);
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("AdminEntryDetails Error : LoadColumnNamesInControlsForAdd " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (hidDBValue.Value == txtDropDownName.Text.Trim().ToUpper())
                {
                    UpdateTableValues();
                }
                else
                {
                    bool chk = CheckExistingValue();
                    if (chk == true)
                    {
                        Response.Write("<script language='javascript'>alert('The value already exists.Enter another value');</script>");
                    }
                    else
                    {
                        UpdateTableValues();
                    }
                }
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("AdminEntryDetails Error : LoadColumnNamesInControlsForAdd " + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void UpdateTableValues()
        {
            try
            {
                objLog.LogWriter("AdminEntryDetails : UpdateTableValues starts ", hidName.Value);
                string strSQL = string.Empty;
                string strCheck = string.Empty;

                if (chkExclude.Checked == true)
                {
                    strCheck = "1";
                }
                else
                {
                    strCheck = "0";
                }

                if (hidColumnCount.Value.ToString() == "6")/* updates the columns-text,business group id,exclude */
                {
					strSQL = "update " + hidTableName.Value + " set " + lblDropDownName.Text.Trim() + "='" + 
						txtDropDownName.Text.Trim().Replace("'", "''") + "'," + lblBusinessGroupId.Text.Trim() + "='" + 
						drpBusinessGroup.SelectedItem.Value + "'," + lblExclude.Text.Trim() + "='" + 
						strCheck.Trim() + "' where " + lblID.Text.Trim() + "=" + txtID.Text.Trim();
				}
				else if (hidColumnCount.Value.ToString() == "5")
				{
					strSQL = "update " + hidTableName.Value + " set " + lblBusinessGroupId.Text.Trim() + " ='" +
						drpBusinessGroup.SelectedItem.Value + "'," + lblDropDownName.Text.Trim() + "='" + 
						txtDropDownName.Text.Trim().Replace("'", "''") + "'," + lblExclude.Text.Trim() + "='" + 
						strCheck.Trim() + "' where " + lblID.Text.Trim() + "=" + txtID.Text.Trim();
				}
				else if (hidColumnCount.Value.ToString() == "4" || hidColumnCount.Value.ToString() == "3")
				{
					if (lblID.Text.ToUpper() != "CONTINENTID")
					{
						strSQL = "update " + hidTableName.Value + " set " + lblDropDownName.Text.Trim() + "='" + 
							txtDropDownName.Text.Trim().Replace("'", "''") + "'," + lblExclude.Text.Trim() + "='" + strCheck.Trim() + 
							"' where " + lblID.Text.Trim() + "=" + txtID.Text.Trim();
					}
					else
					{
						strSQL = "update " + hidTableName.Value + " set " + lblDropDownName.Text.Trim() + "='" + 
							txtDropDownName.Text.Trim().Replace("'", "''") + "'," + lblExclude.Text.Trim() + "='" + strCheck.Trim() + 
							"' where " + lblID.Text.Trim() + "=" + txtID.Text.Trim();
					}
				}

                int i = cmn.ReturnNonQuery(SQL: strSQL);
                if (i > 0)
                {
                    //MessageBox
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"alert('Record Updated Successfully');");

                    sb.Append(@"</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", sb.ToString(), false);

                    //Response.Write("<script language='javascript'>alert('Record Updated Successfully');</script>");
                    Panel1.Enabled = false;
                    btnUpdate.Enabled = false;
                    txtDropDownName.Text = string.Empty;
                    chkExclude.Checked = false;
					drpBusinessGroup.SelectedIndex = 0;
                }
                objLog.LogWriter("AdminEntryDetails : UpdateTableValues Ends ", hidName.Value);
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("AdminEntryDetails Error : UpdateTableValues " + ex.Message, hidName.Value);
                throw ex;
            }
        }

		private bool CheckExistingValue()
		{
			bool result;
			try
			{
				bool check = false;
				DataSet ds = cmn.ReturnAdminDataSet(hidSPName.Value);
				if (hidColumnCount.Value.ToString() == "6")
				{
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						if (txtDropDownName.Text.Trim().ToUpper() == ds.Tables[0].Rows[i][1].ToString().Trim().ToUpper())
						{
							check = true;
							break;
						}
						check = false;
					}
				}
				else if (hidColumnCount.Value.ToString().Trim() == "5")
				{
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						if (txtDropDownName.Text.Trim().ToUpper() == ds.Tables[0].Rows[i][2].ToString().Trim().ToUpper())
						{
							check = true;
							break;
						}
						check = false;
					}
				}
				else if (hidColumnCount.Value.ToString() == "4" || hidColumnCount.Value.ToString() == "3")
				{
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						if (txtDropDownName.Text.Trim().ToUpper() == ds.Tables[0].Rows[i][1].ToString().Trim().ToUpper())
						{
							check = true;
							break;
						}
						check = false;
					}
				}

				result = check;
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("AdminEntryDetails Error : CheckExistingValue " + ex.Message, hidName.Value);
				throw ex;
			}
			return result;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                objLog.LogWriter("AdminEntryDetails : btnAdd_Click starts ", hidName.Value);
                string strSQL = string.Empty;
                string strCheck = string.Empty;
                bool chk = CheckExistingValue();

                if (chk == true)
                {
                    Response.Write("<script language='javascript'>alert('The value already exists.Enter another value');</script>");
                }
                else
                {
                    if (chkExclude.Checked == true)
                    {
                        strCheck = "1";
                    }
                    else
                    {
                        strCheck = "0";
                    }

                    if (hidColumnCount.Value.ToString() == "6")/* adds a new row with the columns-text,business group id,exclude,od */
                    {
						strSQL = "insert into " + hidTableName.Value + "(" + lblDropDownName.Text.Trim().Replace("'", "''") + "," + 
							lblBusinessGroupId.Text + "," + lblExclude.Text.Trim() + ",Od) values('" + 
							txtDropDownName.Text.Trim().Replace("'", "''") + "','" + drpBusinessGroup.SelectedItem.Value + "','" + 
							strCheck.Trim() + "','" + lblod.Text.Trim() + "')";
					}
                    else if (hidColumnCount.Value.ToString() == "5")/* adds a new row with the columns-text,business group id,exclude*/
					{
						strSQL = "insert into " + hidTableName.Value + "(" + lblDropDownName.Text.Trim().Replace("'", "''") + "," + 
							lblBusinessGroupId.Text.Trim() + "," + lblExclude.Text.Trim() + ") values('" + 
							txtDropDownName.Text.Trim().Replace("'", "''") + "','" + drpBusinessGroup.SelectedItem.Value + 
							"','" + strCheck.Trim() + "')";
					}
                    else if (hidColumnCount.Value.ToString() == "3")/* adds a new row with the columns-text,exclude*/
					{
						strSQL = "insert into " + hidTableName.Value + "(" + lblDropDownName.Text.Trim().Replace("'", "''") + "," + 
							lblExclude.Text.Trim() + ") values('" + txtDropDownName.Text.Trim().Replace("'", "''") + "','" + 
							strCheck.Trim() + "')";
					}
                    else if (hidColumnCount.Value.ToString() == "4")/* adds a new row with the columns-text,exclude,od*/
                    {
                        if (lblID.Text.ToUpper() != "CONTINENTID")
                        {
							strSQL = "insert into " + hidTableName.Value + "(" + lblDropDownName.Text.Trim().Replace("'", "''") + 
								"," + lblExclude.Text.Trim() + ",Od) values('" + 
								txtDropDownName.Text.Trim().Replace("'", "''") + "','" + strCheck.Trim() + "','" + 
								lblod.Text.Trim() + "')";
						}
						else
						{
							strSQL ="insert into " + hidTableName.Value + "(" + lblDropDownName.Text.Trim().Replace("'", "''") + "," + 
								lblExclude.Text.Trim() + ") values('" + txtDropDownName.Text.Trim().Replace("'", "''") + 
								"','" + strCheck.Trim() + "')";
						}
					}

                    int i = cmn.ReturnNonQuery(SQL: strSQL);
					if (i > 0)
					{
						Response.Write("<script language='javascript'>alert('Record added successfully');</script>");
						Panel1.Enabled = false;
						btnAdd.Enabled = false;
						txtDropDownName.Text = string.Empty;
						chkExclude.Checked = false;
						drpBusinessGroup.SelectedIndex = 0;
					}
				}
				objLog.LogWriter("AdminEntryDetails : btnAdd_Click Ends ", hidName.Value);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("AdminEntryDetails Error : btnAdd_Click " + ex.Message, hidName.Value);
				throw ex;
			}
		}
	}
}
