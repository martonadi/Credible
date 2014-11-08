using System;
using System.Data;
using System.Text;
using CredentialsDemo.Common;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class IPFEntry : System.Web.UI.Page
    {
        CallingSP objSp = new CallingSP();
		private Logger objLog = new Logger();
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if (this.Session["sessionUserInfo"] != null)
				{
					this.hidName.Value = this.Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
					this.objLog.LogWriter("IPF : Page_Load starts", hidName.Value);
				}
				else
				{
					Response.Redirect("~/TimeOut.aspx");
				}
                if (!IsPostBack)
                {
                    objSp.LoadValues("usp_ClientTypeList", "Client_Type", "ClientTypeId", "@ShowAll~0", "@BusinessGroupId~8", telrad: cbo_IPF_ClientTypeIdIPF);

                    if (Session["sessIPFSS"] != null)
                    {
                        if (Session["sessIPFSS"].ToString().Split('|').Length > 0)
                        {
                            string[] strvals = Session["sessIPFSS"].ToString().Split('|');

                            for (int i = 0; i < strvals.Length; i++)
                            {
                                if (strvals[i].Split('~').Length > 0)
                                {

                                    for (int j = 0; j < strvals[i].Split('~').Length - 1; j++)
                                    {
                                        switch (strvals[i].Split('~')[0].ToString())
                                        {

                                            case "ClientTypeIdIPF":
                                                objSp.MatchDropDownValues(strvals[i].Split('~')[1], cbo_IPF_ClientTypeIdIPF);
                                                break;

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Session["sessIPFClear"] == null)
                        {
                            if (Session["SessionSearchPG"] != null)
                            {
                                DataTable dt = (DataTable)(Session["SessionSearchPG"]);
                                CallingSP objSP = new CallingSP();

                                if (dt.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[0]["ClientTypeIdIPF"].ToString().Trim()))
                                    {
                                        objSP.MatchDropDownValuesText(dt.Rows[0]["ClientTypeIdIPF"].ToString(), cbo_IPF_ClientTypeIdIPF);
                                    }
                                }
                                SaveIPFData();
                            }
                        }
                    }
                }
				objLog.LogWriter("IPF : Page_Load Ends", hidName.Value);
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("IPF : Page_Load Error" + ex.Message, hidName.Value);
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
                SaveIPFData();
                Label1.Visible = true;
                Label1.Text = "Details have been successfully captured. Click close button to close this window.";
                //hrline.Visible = true;
                Session["sessIPFClear"] = null;
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("IPF : btnOk_Click Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }

        private void SaveIPFData()
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
            if (cbo_IPF_ClientTypeIdIPF.SelectedItem.Text.ToUpper() == "SELECT")
            {
                strSS.Append(ReturnString(lbl_IPF_ClientTypeIdIPF.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);//[2] TransactionValueId
                strSS.Append("|");
            }
            else
            {
                strcbo_IPF_ClientTypeIdIPF = cbo_IPF_ClientTypeIdIPF.SelectedItem.Value;
                strSS.Append(ReturnString(lbl_IPF_ClientTypeIdIPF.ID));
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Label1.Visible = false;
                Label1.Text = string.Empty;
                //hrline.Visible = false;
                if (Session["sessIPFSS"] != null)
                {
                    Session.Remove("sessIPF");
                }
				
                if (Session["sessIPFMS"] != null)
                {
                    Session.Remove("sessIPFMS");
                }

                cbo_IPF_ClientTypeIdIPF.SelectedIndex = 0;

                if (Session["sessIPFClear"] != null)
                {
                    Session.Remove("sessIPFClear");
                }
                Session.Add("sessIPFClear", "0");
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("IPF : btnClear_Click Error" + ex.Message, hidName.Value);
                throw ex;
            }
        }
    }
}
