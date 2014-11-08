<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealEstate.aspx.cs" ValidateRequest="false" Inherits="CredentialsDemo.PRATICEGROUPS.RealEstate" %>

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
            function LoadChild(txt, id, hid) {
                var Return;
                var myArguments = new Object();
                var hidcheck = document.getElementById(hid);
                var qstr = txt + "~" + id + "~" + hidcheck.value + "~" + "7";
                /*myArguments.param1 = chkControl.value;*/
                Return = window.showModalDialog("../LOOKUPS/LookUpGrid.aspx?q=" + qstr + "~" + new Date().getTime(), myArguments, "dialogWidth:425px;dialogHeight:427px;")
                if (Return != undefined) {
                    var str = Return.info.split('~~');
                    if (str[0].length != 0 && str[1].length != 0) {
                        if (str[2] == "lbl_RES_Client_Type") {
                            var txt = document.getElementById('txt_RES_Client_Type');
                            txt.value = str[1];
                            var hidtext = document.getElementById('hid_RES_Client_Type_Text');
                            hidtext.value = str[1];
                            var hidid = document.getElementById('hid_RES_Client_Type');
                            hidid.value = str[0];
                        }
                        /*else if (str[2] == "lbl_RES_Work_Type") {
                        var txt = document.getElementById('txt_RES_Work_Type');
                        txt.value = str[1];
                        var hidtext = document.getElementById('hid_RES_Work_Type_Text');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('hid_RES_Work_Type');
                        hidid.value = str[0];
                        }*/
                    }
                    else {
                        var txt = document.getElementById('txt_RES_Client_Type');
                        txt.value = "Select client type from lookup";
                        var hidtext = document.getElementById('hid_RES_Client_Type_Text');
                        hidtext.value = "";
                        var hidid = document.getElementById('hid_RES_Client_Type');
                        hidid.value = "";
                    }
                }
            }
            /* function ReturnString(cbo) {
            var combo = $get(cbo);
            var nm = cbo + "_HiddenField";
            var comboHidden = $get(nm);
            var selectedValue = combo.getElementsByTagName('li')[comboHidden.value].innerHTML;
            return selectedValue;
            }*/
            function ReturnString(cbo) {
                var cbobject = document.getElementById(cbo);
                var combo = $find(cbobject.id);
                var comboValue = combo.get_text();  //document.getElementById(combo.InputID).value;.get_value()
                return comboValue;
            }
            function GetSelectedNodes() {
                //var rtv = document.getElementById(cbo);
                var tree = $find("<%= RadTreeView1.ClientID %>");
                var nodes = tree.get_checkedNodes();
                var i = nodes.length;
                return i;
            }

            function Validation() {
                var txt1 = document.getElementById('txt_RES_Client_Type');
                //var txt2 = document.getElementById('txt_RES_Work_Type');

                if ((txt1 != null && txt1.value == "Select Client Type from Look-Up") && (GetSelectedNodes() <= 0)) {
                    alert('Please enter or select atleast one value !!!');
                    return false;
                }
                else {
                    return true;
                }
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
                    <asp:Label ID="lblname" Text="Real Estate" runat="server" CssClass="labelStyleHeading"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td style="width: 21%; padding-left: 10px; padding-top: 10px;" align="left">
                    <asp:Label ID="lbl_RES_Client_Type" Text="Client type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td style="width: 75%; padding-top: 10px;">
                    <asp:TextBox runat="server" Width="330px" ID="txt_RES_Client_Type" CssClass="txtSingleStyle"
                        Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                    <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender1" runat="server" TargetControlID="txt_RES_Client_Type"
                        WatermarkText="Select client type from lookup" WatermarkCssClass="watermarked" />
                    <asp:ImageButton runat="server" ID="img_RES_Client_Type" ImageUrl="~/Images/bino.jpg"
                        ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Client Type Look Up" />
                    <asp:HiddenField runat="server" ID="hid_RES_Client_Type" />
                    <asp:HiddenField runat="server" ID="hid_RES_Client_Type_Text" />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td align="left" style="padding-left: 10px;" valign="top">
                    <asp:Label ID="lbl_RES_Work_Type" Text="Work type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td>
                    <div style="border: 1px solid #000080; overflow: scroll; width: 345px;
                        padding-left: 5px; height: 150px;background-color:#F0F8FF;">
                        <telerik:RadTreeView runat="server" ID="RadTreeView1" CheckBoxes="true" ShowLineImages="true"
                            OnClientNodeChecked="clientNodeChecked" EnableEmbeddedSkins="false" Skin="MySkin">
                            <CollapseAnimation Duration="100" Type="OutQuint" />
                            <ExpandAnimation Duration="100" Type="OutBounce" />
                        </telerik:RadTreeView>
                    </div>
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
