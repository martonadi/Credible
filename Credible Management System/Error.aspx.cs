using System;
using System.Configuration;
using System.Net.Mail;

namespace CredentialsDemo
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRequestAccess_Click(object sender, EventArgs e)
        {
            try
            {
                bool EmailOption = false;
                string EmailFromAddress = ConfigurationManager.AppSettings["EmailFromAddress"];
                string EmailToAddress = ConfigurationManager.AppSettings["EmailFromAddress"]; //Request.QueryString["un"].ToString() + "@cms-cmck.com";
                string EmailSubject = "Requesting Login Access for Credential Management System Website";
                string SMTPSERVER = ConfigurationManager.AppSettings["SMTPSERVER"];
                string str = "User <b>" + Request.QueryString["un"].ToString().ToUpper().Trim() + "</b> has been requested Access for Credential Management System Website";
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                MailAddress mailAddress = new MailAddress(EmailFromAddress);
                mailMessage.From = mailAddress;
                mailMessage.To.Add(EmailToAddress);
                mailMessage.Subject = EmailSubject;
                if (!EmailOption)
                {
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Body = str;
                }
                SmtpClient smtp = new SmtpClient(SMTPSERVER);
                smtp.Send(mailMessage);
                ClientScript.RegisterStartupScript(this.GetType(), "Click", "<script language=\"javascript\">window.alert('An Email has been sent to Administrator for requesting access.');window.close();</script>");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}