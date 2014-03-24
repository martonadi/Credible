using System;
using CredentialsDemo.Common;
using System.Text;
using System.Data;

namespace CredentialsDemo.PRATICEGROUPS
{
    public partial class CorpTax1 : System.Web.UI.Page
    {
        CallingSP objSP = new CallingSP();
		private Logger objLog = new Logger();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
				if (Session["sessionUserInfo"] != null)
				{
					hidName.Value = Session["sessionUserInfo"].ToString().Split('~')[0].Trim();
					objLog.LogWriter("CorpTax : Page_Load starts", hidName.Value);
				}
				else
				{
					Response.Redirect("~/TimeOut.aspx");
				}
                if (!IsPostBack)
                {
                    //SetValues();
                    objSP.LoadValues("usp_WorkTypeList_CT", "Work_Type", "WorkTypeId",telrad: cbo_Crt_WorkType_CorpTax);
                    // objSP.LoadValues("usp_TransactionCurrencyList", "Transaction_Currency", "TransactionCurrencyId", "@ShowAll~0", "@BusinessGroupId~1", telrad: cbo_BAI_Transaction_Value);

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
                                                //cbo_BAI_ClientTypeIdBAIF.SelectedItem.Text =strvals[i].Split('~')[1];
                                                objSP.MatchDropDownValues(strvals[i].Split('~')[1], cbo_Crt_WorkType_CorpTax);
                                                break;

                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Session["sessCorpTaxClear"] == null)
                        {
                            if (Session["SessionSearchPG"] != null)
                            {
                                DataTable dt = (DataTable)(Session["SessionSearchPG"]);
                                objSP = new CallingSP();

                                if (dt.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[0]["WorkTypeIdCorpTax"].ToString().Trim()))
                                    {
                                        objSP.MatchDropDownValuesText(dt.Rows[0]["WorkTypeIdCorpTax"].ToString(), cbo_Crt_WorkType_CorpTax);
                                    }
                                }
                                SaveCorpTaxData();
                            }
                        }
                    }
                }
				objLog.LogWriter("CorpTax : Page_Load Ends", hidName.Value);
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("CorpTax : Page_Load Error " + ex.Message, hidName.Value);
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
                SaveCorpTaxData();

                Label1.Visible = true;
                Label1.Text = "Details have been successfully captured. Click close button to close this window.";
               // hrline.Visible = true;

                Session["sessCorpTaxClear"] = null;
            }
            catch (Exception ex)
            {
				objLog.ErrorWriter("CorpTax : btnOk_Click Error " + ex.Message, hidName.Value);
                throw ex;
            }
        }

		private void SaveCorpTaxData()
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
			if (cbo_Crt_WorkType_CorpTax.SelectedItem.Text.ToUpper() == "SELECT")
			{
				strSS.Append(ReturnString(lbl_Crt_WorkType_CorpTax.ID));
				strSS.Append("~");
				strSS.Append(string.Empty);
				strSS.Append("|");
			}
			else
			{
				strcbo_Crt_WorkType_CorpTax = cbo_Crt_WorkType_CorpTax.SelectedItem.Value;
				strSS.Append(ReturnString(lbl_Crt_WorkType_CorpTax.ID));
				strSS.Append("~");
				strSS.Append(strcbo_Crt_WorkType_CorpTax);
				strSS.Append("|");
			}
			string strhid_BAI_Work_Type = string.Empty;
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
		protected void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				if (Session["sessCorpTaxSS"] != null)
				{
					Session.Remove("sessCorpTaxSS");
				}
				if (Session["sessCorpTaxMS"] != null)
				{
					Session.Remove("sessCorpTaxMS");
				}
				Label1.Visible = false;
				Label1.Text = string.Empty;
				cbo_Crt_WorkType_CorpTax.SelectedIndex = 0;
				if (Session["sessCorpTaxClear"] != null)
				{
					Session.Remove("sessCorpTaxClear");
				}
				Session.Add("sessCorpTaxClear", "0");
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("CorpTax : btnClear_Click Error " + ex.Message, hidName.Value);
				throw ex;
			}
		}
	}
}
