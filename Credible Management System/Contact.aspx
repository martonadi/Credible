<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="CredentialsDemo.Contact" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: #EEf3fa;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td align="center">
                <div style="padding-top: 200px; font-family: Arial; font-size: 11px; font-weight: bold;
                    color: #006699;">
                    You dont have sufficient rights to access this site. Please <asp:LinkButton ID="btnRequestAccess" OnClick="btnRequestAccess_Click" runat="server" Text="Click Here"></asp:LinkButton> to request the access from Administrator.
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" style="padding-top: 50px;">
                <asp:Image ImageUrl="~/Images/AccessDenied.jpg" runat="server" ID="img1" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
