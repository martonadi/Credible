<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="UserCreation.aspx.cs" Inherits="CredentialsDemo.ADMIN.UserCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">

        function CreateUser() {

            var txtusername = document.getElementById("<%=txtUserName.ClientID%>");
            if (Trim(txtusername.value) == "") {
                alert('Please enter username');
                txtusername.focus();
                return false;
            }

            var drp = document.getElementById("<%=drpRole.ClientID%>");
            if (drp.selectedIndex == 0) {
                alert('Please select user role');
                drp.focus();
                return false;
            }
            return true;
        }

        function ClearUser() {
            var txtusername = document.getElementById("<%=txtUserName.ClientID%>");
            var drp = document.getElementById("<%=drpRole.ClientID%>");
            txtusername.value = "";
            drp.selectedIndex = 0;
            return false;
        }
        function SignOutUser() {
            var r = confirm("Do you want to signout ?");
            if (r == true) {
                return true;
            }
            else {
                return false;
            }
        }
        function AlphaNumericonly(e) {
            if (!e) e = window.event;
            key = e.keyCode ? e.keyCode : e.which;
            if ((key >= 48 && key <= 57) || ((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32')) {
                return true;
            }
            else {
                return false;
            }
        }
        function Trim(str) {
            while (str.substring(0, 1) == ' ') // check for white spaces from beginning
            {
                str = str.substring(1, str.length);
            }
            while (str.substring(str.length - 1, str.length) == ' ') // check white space from end
            {
                str = str.substring(0, str.length - 1);
            }

            return str;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:HiddenField runat="server" ID="hidUser" />
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="center">
                    <div style="width: 40%; padding-top: 1px; border: 1px; border-color: #00759A; border-style: solid;"
                        align="center">
                        <table cellpadding="" cellspacing="5" border="0" width="90%">
                            <tr>
                                <td style="width: 25%;" align="right">
                                    <asp:Label ID="lblUserName" runat="server" Text="User name" CssClass="labelStyle"></asp:Label>
                                </td>
                                <td style="width: 70%;" align="left">
                                    <asp:TextBox ID="txtUserName" runat="server" Width="250px" BackColor="white" BorderStyle="Solid"
                                        BorderWidth="1px" BorderColor="Gray" MaxLength="5" ToolTip="You can Enter up to 5 Characters"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label1" runat="server" Text="User role" CssClass="labelStyle"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList runat="server" ID="drpRole" Width="257px" CssClass="drpdown">
                                        <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                        <asp:ListItem Value="1">ADMINISTRATOR</asp:ListItem>
                                        <asp:ListItem Value="2">EDITOR</asp:ListItem>
                                        <asp:ListItem Value="3">READER</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnCreateUser" runat="server" BackColor="#00759A" BorderStyle="None"
                                        CssClass="handShow" Font-Bold="True" Font-Names="VERDANA" Font-Size="XX-Small"
                                        ForeColor="White" Height="19px" Text="OK" Visible="true" Width="70px" OnClick="btnCreateUser_Click" />
                                    <asp:Button ID="btnClear" runat="server" BackColor="#00759A" BorderStyle="None" CssClass="handShow"
                                        Font-Bold="True" Font-Names="VERDANA" Font-Size="XX-Small" ForeColor="White"
                                        Height="19px" Text="CLEAR" Visible="true" Width="70px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:Label ID="Label2" Text="Note: Provide CMCK loginname as username" runat="server"
                                        CssClass="labelStyle"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:HyperLink Visible="false" CssClass="LabelText" NavigateUrl="~/Logi.aspx" ID="HyperLink1"
                                        runat="server">Back to Sign in Page</asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label Visible="false" ID="litPassword" runat="server" Text="Password" CssClass="LabelText"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox Visible="false" ID="txtPassword" runat="server" Width="250px" BackColor="white"
                                        BorderStyle="Solid" BorderWidth="1px" BorderColor="Gray" TextMode="Password"
                                        MaxLength="10" ToolTip="You can Enter up to 10 Characters"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
