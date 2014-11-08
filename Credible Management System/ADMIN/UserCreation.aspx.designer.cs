using CredentialsDemo.Common;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace CredentialsDemo.ADMIN {
    
    
    public partial class UserCreation {
        protected HiddenField hidUser;
        protected Label lblUserName;
        protected TextBox txtUserName;
        protected Label Label1;
        protected DropDownList drpRole;
        protected Button btnCreateUser;
        protected Button btnClear;
        protected Label Label2;
        protected HyperLink HyperLink1;
        protected Label litPassword;
        protected TextBox txtPassword;
    }
}
