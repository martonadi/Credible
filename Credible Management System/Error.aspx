<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CredentialsDemo.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: #EEf3fa;">
    <form id="form1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td align="center" style="padding-top: 50px;">
                <div style="padding-top: 200px; font-family: Arial; font-size: 11px; font-weight: bold;
                    color: #006699;">
                    <asp:Image ImageUrl="~/Images/msg_error.gif" runat="server" ID="Image1" />
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">
                <div style="padding-top: 10px; font-family: Arial; font-size: 11px; font-weight: bold;
                    color: #006699;">
                    This page encountered a system error while attempting to process your request. The
                    error has been logged for our site administrators.
                </div>
                <div style="padding-top: 10px; font-family: Arial; font-size: 11px; font-weight: bold;
                    color: #006699;">
                    If you require immediate assistance, please contact <b>IT, Service Desk(ServiceDesk.IT@cms-cmck.com)</b> or <asp:LinkButton ID="btnRequestAccess" PostBackUrl="~/Gateway.aspx" runat="server" Text="click here"></asp:LinkButton> to login again.
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
