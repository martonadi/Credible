<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HC.aspx.cs" ValidateRequest="false" Inherits="CredentialsDemo.PRATICEGROUPS.HCEntry" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Site2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TreeView.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <telerik:RadCodeBlock ID="id1" runat="server">
        <script type="text/javascript" language="javascript">
            function UpdateAllChildren(nodes, checked) {
                var i;
                var test;
                for (i = 0; i < nodes.get_count(); i++) {
                    if (checked) {
                        nodes.getNode(i).check();
                    }
                    else {
                        nodes.getNode(i).set_checked(false);
                    }

                    if (nodes.getNode(i).get_nodes().get_count() > 0) {
                        UpdateAllChildren(nodes.getNode(i).get_nodes(), checked);
                    }
                }
            }
            function clientNodeChecked(sender, eventArgs) {
                var childNodes = eventArgs.get_node().get_nodes();
                var isChecked = eventArgs.get_node().get_checked();
                UpdateAllChildren(childNodes, isChecked);
            }
            function ReturnString(cbo) {
                var cbobject = document.getElementById(cbo);
                var combo = $find(cbobject.id);
                var comboValue = combo.get_text();  //document.getElementById(combo.InputID).value;.get_value()
                return comboValue;
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
            function GetSelectedNodes() {
                //var rtv = document.getElementById(cbo);
                var tree = $find("<%= RadTreeView1.ClientID %>");
                var nodes = tree.get_checkedNodes();
                var i = nodes.length;
                return i;
            }
            function Validation() {
                if (GetSelectedNodes() <= 0) {
                    alert('Please enter or select atleast one value !!!');
                    return false;
                }
                else {

                }
                return true;
            }
        </script>
    </telerik:RadCodeBlock>
</head>
<body style="background-color: #00759A">
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hidName" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnl1" runat="server" Width="480px">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblname" Text="Human Capital" runat="server" CssClass="labelStyleHeading"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="background-color: White;" valign="top">
                <td style="width: 21%; padding-left: 10px; padding-top: 10px;" align="left">
                    <asp:Label ID="lbl_HCC_WorkTypeIdHC" Text="Work type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td style="width: 75%; padding-top: 10px;">
                    <div style="border: 1px solid #000080; overflow: scroll; width: 345px;
                        padding-left: 5px; height: 150px;background-color: #F0F8FF;">
                        <telerik:RadTreeView runat="server" ID="RadTreeView1" CheckBoxes="true" ShowLineImages="true"
                            OnClientNodeChecked="clientNodeChecked" EnableEmbeddedSkins="false" Skin="MySkin">
                            <CollapseAnimation Duration="100" Type="OutQuint" />
                            <ExpandAnimation Duration="100" Type="OutBounce" />
                        </telerik:RadTreeView>
                    </div>
                </td>
            </tr>
            <tr style="background-color: White;" runat="server" id="tr_HCC_PensionSchemeHC" visible="true">
                <td align="left" style="padding-left: 10px;" valign="middle">
                    <asp:Label ID="lbl_HCC_PensionSchemeHC" Text="Pension scheme" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cbo_HCC_PensionSchemeHC" AllowCustomText="false" MarkFirstMatch="true"
                        runat="server" Width="350px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                         EnableEmbeddedSkins="false" Skin="MySkin" Height="110px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 10px;">
                    <asp:Label ID="Label2" Text="" runat="server" CssClass="labelStyledetails" Visible="false"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="center" style="padding-bottom: 10px;">
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
