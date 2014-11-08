<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminEntryDetails.aspx.cs" Inherits="CredentialsDemo.ADMIN.AdminEntryDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
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

        function Validate() {
            var txt = document.getElementById('txtDropDownName');
            var varlbltext = document.getElementById('lblDropDownNameDisplayVisible').outerText;
            if (txt != null && Trim(txt.value) == "") {
                alert("Please enter " + varlbltext);
                txt.value = "";
                txt.focus();
                return false;
            }

            var drp = document.getElementById('<%=drpBusinessGroup.ClientID%>');
            if (drp != null) {
                var varlblddltext = document.getElementById('lblBusinessGroupVisible').outerText;
                if (drp != null) {
                    if (drp.selectedIndex == 0) {
                        alert("Please select " + varlblddltext + "");
                        return false;
                    }
                }
            }
        }

        function Alpha(e) {
            
            if (!e) e = window.event;
            key = e.keyCode ? e.keyCode : e.which;
            //alert(e.keyCode);
            if (((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32') || (key == '46') || (key == '44') || (key == '38') || (key == '40') || (key == '41') || (key == '47') || (key == '92') || (key == '45') || (key == '95') || (key == '39')) {
                return true;

            }
            else {
                return false;
            }
        }
        
    </script>
</head>
<body style="background-color: White;">
    <form id="form1" runat="server">
    <div>
     <asp:HiddenField runat="server" ID="hidName" />
        <asp:HiddenField ID="hidTableName" runat="server" />
        <asp:HiddenField ID="hidColumnCount" runat="server" />
        <asp:HiddenField ID="hidSPName" runat="server" />
        <asp:HiddenField ID="hidDBValue" runat="server" />    
        <asp:HiddenField ID="hidDisplyColumnName" runat="server" />    
        <asp:Panel runat="server" Height="150px" ID="Panel1" Width="100%">
            <table cellpadding="0" cellspacing="2" border="0" width="100%">
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblname" Text="Entry Details" runat="server" CssClass="labelStyleHeading"></asp:Label>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="3">
                    <hr />
                </td>
            </tr>
                <tr style="background-color: White;">
                    <td style="width: 36%;">
                        <asp:Label ID="lblDropDownNameDisplayVisible" runat="server" Text="" CssClass="labelStyle"></asp:Label>
                        <asp:Label ID="lblDropDownNameDisplay" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                    <td style="width: 10%;">
                        <asp:Label ID="lblExcludeVisible" runat="server" Text="" CssClass="labelStyle"></asp:Label>
                        <asp:Label ID="lblExclude" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                    <td style="width: 54%;">
                        <asp:Label ID="lblBusinessGroupVisible" runat="server" Text="" CssClass="labelStyle"></asp:Label>
                        <asp:Label ID="lblBusinessGroup" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: White;">
                    <td colspan="3">
                         <hr />
                    </td>
                </tr>
                <tr style="background-color: White;">
                    <td>
                        <asp:TextBox ID="txtDropDownName" runat="server" Width="180px"
                            CssClass="txtSingleStyle"></asp:TextBox>
                    </td>
                    <td align="center">
                        <asp:CheckBox ID="chkExclude" runat="server" Text="" CssClass="panelStyle1" />
                    </td>
                    <td>
                        <asp:DropDownList ID="drpBusinessGroup" runat="server" Width="295px" CssClass="panelStyle1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="background-color: White;">
                    <td colspan="3">
                        <hr />
                    </td>
                </tr>
                <tr style="background-color: White;">
                    <td colspan="3" align="left">
                        <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" OnClientClick="return Validate();" OnClick="btnUpdate_Click"
                             CssClass="btncolorlight" Width="60px" />
                        <asp:Button ID="btnAdd" runat="server" Text="ADD"  OnClientClick="return Validate();" OnClick="btnAdd_Click" CssClass="btncolorlight" />
                      
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Label ID="lblBusinessGroupDisplay" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblID" runat="server" Text="Label" Visible="false"></asp:Label>
        <asp:TextBox ID="txtID" runat="server" Enabled="false" Visible="false"></asp:TextBox><br />
        <br />
        <asp:TextBox ID="txtExclude" runat="server" Visible="false"></asp:TextBox>
        <br />
        <asp:Label ID="lblDropDownName" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblBusinessGroupId" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblod" runat="server" Text="" Visible="false"></asp:Label>
        <asp:TextBox ID="txtOD" runat="server" Text="" Visible="false"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
