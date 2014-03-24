using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.DirectoryServices;
using System.IO;
using CredentialsDemo.Common;
using System.Text;

namespace CredentialsDemo.ADMIN
{
    public partial class UserCreation : System.Web.UI.Page
    {
        string strConnection = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        CallingSP objsp = new CallingSP();
        Logger objLog = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                objLog.LogWriter("UserCreation : Page_Load starts ", hidUser.Value);
                if (Session["sessionUserInfo"] != null)
                {
                    hidUser.Value = Session["sessionUserInfo"].ToString().Split('~')[0].ToUpper();
                }
                else
                {
                    Response.Redirect("~/TimeOut.aspx");
                }
                btnCreateUser.Attributes.Add("onclick", "return CreateUser();");
                btnClear.Attributes.Add("onclick", "return ClearUser();");
				txtUserName.Attributes.Add("onkeypress", "return AlphaNumericonly(event);");
                objLog.LogWriter("UserCreation : Page_Load Ends ", hidUser.Value);
            }
            catch (Exception ex)
            {
                objLog.ErrorWriter("UserCreation Error : Page_Load " + ex.Message, hidUser.Value);
                throw ex;
            }
        }
		protected void btnCreateUser_Click(object sender, EventArgs e)
		{
			try
			{
				objLog.LogWriter("UserCreation : btnCreateUser_Click starts ", hidUser.Value);
				string str = txtUserName.Text.Trim().ToUpper();
				string sDomain = ConfigurationManager.AppSettings["DOMAIN"].ToString();
				string sAD = string.Empty;
				if (sDomain.Equals("ITEST", StringComparison.InvariantCultureIgnoreCase))
				{
					sAD = ConfigurationManager.AppSettings["ITESTLDAPVal"].ToString();
				}
				else
				{
					if (sDomain.Equals("LIVE", StringComparison.InvariantCultureIgnoreCase))
					{
						sAD = ConfigurationManager.AppSettings["LDAPVal"].ToString();
					}
				}
				for (int i = 0; i < sAD.Split(',').Length; i++)
				{
					SearchResult adSearchResult;
					using (DirectoryEntry de = new DirectoryEntry(sAD.Split(',')[i].ToString()))
					{
						using (DirectorySearcher adSearch = new DirectorySearcher(de))
						{
							adSearch.Filter = "(sAMAccountName=" + str + ")";
							adSearchResult = adSearch.FindOne();
						}
					}
					if (adSearchResult != null)
					{
						string strSql = "select * from  tbl_UserLoginDetails where UserName='" + txtUserName.Text.Trim().ToUpper() + "'";
						SqlDataReader dr = objsp.GetDataReader(strSql);
						if (dr.HasRows)
						{
							string insSql = "UPDATE tbl_UserLoginDetails set UserRole='" + drpRole.SelectedItem.Text.ToUpper() + 
								"',CreatedBy='" + hidUser.Value + "' where username='" + txtUserName.Text.Trim().ToUpper() + "'";
							int j = objsp.GetExecuteNonQuery(insSql);
							if (!SendEmailtoUser(adSearchResult.GetDirectoryEntry().Properties["mail"].Value.ToString(), txtUserName.Text.Trim(), txtPassword.Text.Trim(), "0", drpRole.SelectedItem.Text.ToUpper()))
							{
								ClientScript.RegisterStartupScript(GetType(), "Click", "<script language=\"javascript\">window.alert('User account created cuccessfully but unable to send an email. Please inform to user');</script>");
							}
							else
							{
								ClientScript.RegisterStartupScript(GetType(), "Click", "<script language=\"javascript\">window.alert('Access has been granted successfully');</script>");
							}
							txtUserName.Text = string.Empty;
							txtPassword.Text = string.Empty;
							drpRole.SelectedIndex = 0;
						}
						else
						{
							string strEnable = "ENABLE";
							string insSql = "insert into tbl_UserLoginDetails(UserName,Password,Email,Status,UserRole,CreatedBy) values('" + 
								txtUserName.Text.Trim().ToUpper() + "','" + txtPassword.Text + "','" + 
								adSearchResult.GetDirectoryEntry().Properties["mail"].ToString() + "','" + strEnable + 
								"','" + drpRole.SelectedItem.Text.ToUpper() + "','" + hidUser.Value + "')";
							int j = objsp.GetExecuteNonQuery(insSql);
							if (!SendEmailtoUser(adSearchResult.GetDirectoryEntry().Properties["mail"].Value.ToString(), txtUserName.Text.Trim(), txtPassword.Text.Trim(), "1", null))
							{
								base.ClientScript.RegisterStartupScript(base.GetType(), "Click", "<script language=\"javascript\">window.alert('User Account Created Successfully but unable to Send an EMail. Please inform to user');</script>");
							}
							else
							{
								base.ClientScript.RegisterStartupScript(base.GetType(), "Click", "<script language=\"javascript\">window.alert('Access has been granted successfully');</script>");
							}
							txtUserName.Text = string.Empty;
							txtPassword.Text = string.Empty;
							drpRole.SelectedIndex = 0;
						}
						dr.Close();
						adSearchResult = null;
					}
					else
					{
						base.ClientScript.RegisterStartupScript(base.GetType(), "Click", "<script language=\"javascript\">window.alert('User does not exists in CMCK domain');</script>");
						txtUserName.Focus();
					}
				}
				objLog.LogWriter("UserCreation : btnCreateUser_Click Ends ", hidUser.Value);
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("UserCreation Error : btnCreateUser_Click ", hidUser.Value);
				throw ex;
			}
		}
		public bool SendEmailtoUser(string ToAddress, string UserName, string Password, string strPage, string strRole = null)
		{
			bool result;
			try
			{
				bool EmailOption = false;
				string EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
				string EmailSubject = ConfigurationManager.AppSettings["EmailSubject"];
				StringBuilder sb = new StringBuilder();
				sb.Append(ConfigurationManager.AppSettings["EmailBody1"].ToString());
				sb.Append(ConfigurationManager.AppSettings["EmailBody2"].ToString());
				if (strPage == "0")
				{
					sb.Append(ConfigurationManager.AppSettings["EmailBody31"].ToString().Replace("RoleType", strRole));
				}
				else
				{
					sb.Append(ConfigurationManager.AppSettings["EmailBody3"].ToString());
				}
				sb.Append(ConfigurationManager.AppSettings["EmailBody5"].ToString());
				sb.Append(ConfigurationManager.AppSettings["EmailBody6"].ToString());
				sb.Append(ConfigurationManager.AppSettings["EmailBody7"].ToString());
				sb.Append(ConfigurationManager.AppSettings["EmailBody8"].ToString());
				string str = sb.ToString();
				string SMTPSERVER = ConfigurationManager.AppSettings["SMTPSERVER"];
				MailMessage mailMessage = new MailMessage();
				MailAddress mailAddress = new MailAddress(EmailFromAddress, ConfigurationManager.AppSettings["EmailFromAddressName"].ToString());
				mailMessage.From = mailAddress;
				mailMessage.To.Add(ToAddress);
				mailMessage.Subject = EmailSubject;
				if (!EmailOption)
				{
					mailMessage.IsBodyHtml = true;
					mailMessage.Body = str;
				}
				SmtpClient smtp = new SmtpClient(SMTPSERVER);
				smtp.Send(mailMessage);
				result = true;
			}
			catch (Exception ex)
			{
				objLog.ErrorWriter("UserCreation Error : SendEmailtoUser " + ex.Message, hidUser.Value);
				throw ex;
			}
			return result;
		}
	}
}
