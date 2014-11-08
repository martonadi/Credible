<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorpTax.aspx.cs" ValidateRequest="false" Inherits="CredentialsDemo.PRATICEGROUPS.CorpTax" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Site2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ReturnString(cbo) {
            var cbobject = document.getElementById(cbo);
            var combo = $find(cbobject.id);
            var comboValue = combo.get_text();  //document.getElementById(combo.InputID).value;.get_value()
            return comboValue;
        }
        function Validation() {/*lbl_IPF_CurrencyProjectId*/

            if (ReturnString('cbo_Crt_WorkType_CorpTax') == "Select") {
                alert('Please enter or select atleast one value !!!');
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body style="background-color: #00759A;">
    <form id="form1" runat="server">
     <asp:HiddenField runat="server" ID="hidName" />
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:Panel ID="pnl1" runat="server" Width="450px">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblname" Text="Corporate Tax" runat="server" CssClass="labelStyleHeading"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td style="width: 21%; padding-left: 10px; padding-top: 10px;" align="left">
                    <asp:Label ID="lbl_Crt_WorkType_CorpTax" Text="Work type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td style="width: 75%; padding-top: 10px;">
                    <telerik:RadComboBox ID="cbo_Crt_WorkType_CorpTax" AllowCustomText="false" MarkFirstMatch="true"
                        runat="server" Width="343px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                        EnableEmbeddedSkins="false" Skin="MySkin" Height="100px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" Text="" runat="server" CssClass="labelStyledetails" Visible="false"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="center">
                    <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" OnClientClick="return Validation();"
                        CssClass="btncolor" />
                    <asp:Button ID="btnClear" Text="CLEAR" runat="server" OnClick="btnClear_Click" CssClass="btncolor" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
