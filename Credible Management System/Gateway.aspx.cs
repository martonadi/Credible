using System;
using System.Data.SqlClient;
using System.DirectoryServices;
using CredentialsDemo.Common;
using System.Configuration;
using System.Data;
using System.Security.Principal;
using System.Web;

namespace CredentialsDemo
{
    public partial class Gateway : System.Web.UI.Page
    {
        CallingSP objSP = new CallingSP();
        private string strcon = ConfigurationManager.ConnectionStrings["con"].ToString();
        private Logger objLog = new Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            bool flag = false;
            string text = string.Empty;
            try
            {
                IPrincipal user = HttpContext.Current.User;
                string name = user.Identity.Name;
                string text2 = string.Empty;
                string text3 = ConfigurationManager.AppSettings["DOMAIN"].ToString();
                if (!string.IsNullOrEmpty(name.Trim()))
                {
                    text = name.Split(new char[]
					{
						'\\'
					})[1];
                    if (text.Trim().Length > 0)
                    {
                        string text4 = string.Empty;
                        if (text3.Equals("ITEST", StringComparison.InvariantCultureIgnoreCase))
                        {
                            text4 = ConfigurationManager.AppSettings["ITESTLDAPVal"].ToString();
                        }
                        else
                        {
                            if (text3.Equals("LIVE", StringComparison.InvariantCultureIgnoreCase))
                            {
                                text4 = ConfigurationManager.AppSettings["LDAPVal"].ToString();
                            }
                        }
                        for (int i = 0; i < text4.Split(new char[]
						{
							','
						}).Length; i++)
                        {
                            SearchResult searchResult;
                            using (DirectoryEntry directoryEntry = new DirectoryEntry(text4.Split(new char[]
							{
								','
							})[i].ToString()))
                            {
                                using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry))
                                {
                                    directorySearcher.Filter = "(sAMAccountName=" + text + ")";
                                    searchResult = directorySearcher.FindOne();
                                }
                            }
                            if (searchResult != null)
                            {
                                if (text3.Equals("ITEST", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    string text5 = searchResult.GetDirectoryEntry().Name.Split(new char[]
									{
										'='
									})[1].ToString();
                                    text2 = text5.Replace(",", "").Replace("/", "");
                                }
                                else
                                {
                                    if (text3.Equals("LIVE", StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        if (searchResult.GetDirectoryEntry().Name.Split(new char[]
										{
											'='
										})[1].Contains("\\"))
                                        {
                                            string text5 = searchResult.GetDirectoryEntry().Name.Split(new char[]
											{
												'='
											})[1].Split(new char[]
											{
												'\\'
											})[1].ToString();
                                            string str = text5.Substring(1, text5.Length - 1).Trim();
                                            string str2 = searchResult.GetDirectoryEntry().Name.Split(new char[]
											{
												'='
											})[1].Split(new char[]
											{
												'\\'
											})[0].ToString();
                                            text2 = str2 + ", " + str;
                                        }
                                        else
                                        {
                                            text2 = searchResult.GetDirectoryEntry().Name.Split(new char[]
											{
												'='
											})[1];
                                        }
                                    }
                                }
                                searchResult = null;
                                break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(text2))
                    {
                        flag = true;
                        string strSQL = "select * from  tbl_UserLoginDetails where UserName='" + text.Trim().ToUpperInvariant() + "'";
                        SqlDataReader dataReader = this.objSP.GetDataReader(strSQL);
                        this.Session.Clear();
                        if (dataReader.HasRows)
                        {
                            dataReader.Read();
                            if (dataReader.GetValue(4).ToString() == "ENABLE")
                            {
                                this.Session.Add("sessionUserInfo", text2 + "~" + dataReader["UserRole"].ToString());
                            }
                            else
                            {
                                base.ClientScript.RegisterStartupScript(base.GetType(), "Click", "<script language=\"javascript\">window.alert('User Account has been disabled. Please Contact Admin');</script>");
                            }
                        }
                        else
                        {
                            string text6 = "ENABLE";
                            string strSQL2 = string.Concat(new string[]
							{
								"insert into tbl_UserLoginDetails(UserName,Email,Status,UserRole) values('",
								text.Trim().ToUpper(),
								"','",
								text.Trim().ToUpper(),
								"@cms-cmck.com','",
								text6,
								"','READER')"
							});
                            this.objSP.GetExecuteNonQuery(strSQL2);
                            this.Session.Add("sessionUserInfo", text2 + "~READER");
                        }
                        dataReader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
            finally
            {
                this.UserHitCount(text);
                if (flag)
                {
                    base.Response.Redirect("~/Search/SearchScreen.aspx");
                }
                else
                {
                    base.Response.Redirect("~/Contact.aspx?un=" + text);
                }
            }
        }
        private void UserHitCount(string str4Letter)
        {
            string selectCommandText = "select Usercount from  tbl_UserHit where UserName='" + str4Letter.Trim().ToUpperInvariant() + "'";
            SqlConnection sqlConnection = new SqlConnection(this.strcon);
            sqlConnection.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommandText, sqlConnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                int num = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString());
                num++;
                SqlCommand sqlCommand = new SqlCommand("update tbl_UserHit set Usercount='" + num + "'", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            else
            {
                int num = 1;
                SqlCommand sqlCommand2 = new SqlCommand(string.Concat(new object[]
				{
					"insert into tbl_UserHit(UserName,Usercount) values('",
					str4Letter.ToUpper(),
					"','",
					num,
					"')"
				}), sqlConnection);
                sqlCommand2.ExecuteNonQuery();
                sqlCommand2.Dispose();
            }
            dataSet.Dispose();
        }

    }
}