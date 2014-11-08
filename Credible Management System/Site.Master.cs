using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CredentialsDemo
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessionUserInfo"] != null)
            {
                if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "READER")
                {
					Label1.Visible = false;
					lblVisitorCount.Visible = false;
					lnkDataCreation.Visible = false;
					lnkUserUserCreation.Visible = false;
					lnkEntry.Visible = false;
					lnkSearch.Visible = true;
				}
                else if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "EDITOR")
                {
						Label1.Visible = false;
						lblVisitorCount.Visible = false;
                    lnkDataCreation.Visible = false;
                    lnkUserUserCreation.Visible = false;
                    lnkEntry.Visible = true;
                    lnkSearch.Visible = true;
                }
                else if (Session["sessionUserInfo"].ToString().Split('~')[1].Trim().ToUpper() == "ADMINISTRATOR")
                {
							Label1.Visible = true;
							lblVisitorCount.Visible = true;
                    lnkDataCreation.Visible = true;
                    lnkUserUserCreation.Visible = true;
                    lnkEntry.Visible = true;
                    lnkSearch.Visible = true;
                }
                if (!IsPostBack)
                {
                    lbluserName.Text = lbluserName.Text + " " + Session["sessionUserInfo"].ToString().Split('~')[0];
                    lblRole.Text = "You have logged in as " + Session["sessionUserInfo"].ToString().Split('~')[1];
                }

                if (Session["MasterWidth"] != null)
                {
                    txt.Style.Add(HtmlTextWriterStyle.Width, System.Configuration.ConfigurationManager.AppSettings["MasWidth"].ToString());
                    Session["MasterWidth"] = null;
                }
				UserHitCount();
            }
            else
            {
                Response.Redirect("~/TimeOut.aspx");
            }
        }
		private void UserHitCount()
		{
			string strSql = "select COUNT(*) from tbl_userhit";
			string strcon = ConfigurationManager.ConnectionStrings["con"].ToString();
			SqlConnection con = new SqlConnection(strcon);
			con.Open();
			SqlDataAdapter adp = new SqlDataAdapter(strSql, con);
			DataSet ds = new DataSet();
			adp.Fill(ds);
			if (ds.Tables[0].Rows.Count > 0)
			{
				this.lblVisitorCount.Text = ds.Tables[0].Rows[0][0].ToString();
			}
			adp.Dispose();
			ds.Dispose();
			con.Close();
		}
	}
}
