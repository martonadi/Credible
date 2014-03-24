<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EPCE.aspx.cs" ValidateRequest="false" Inherits="CredentialsDemo.PRATICEGROUPS.EPCE" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Site2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function LoadChild(txt, id, hid) {
            var Return;
            var myArguments = new Object();
            var hidcheck = document.getElementById(hid);
            var qstr = txt + "~" + id + "~" + hidcheck.value + "~" + "9";
            /*myArguments.param1 = chkControl.value;*/
            Return = window.showModalDialog("../LOOKUPS/LookUpGrid.aspx?q=" + qstr + "~" + new Date().getTime(), myArguments, "dialogWidth:425px;dialogHeight:427px;")
            if (Return != undefined) {
                var str = Return.info.split('~~');
                if (str[0].length != 0 && str[1].length != 0) {
                    if (str[2] == "lbl_ENE_Transaction_Type") {
                        var txt = document.getElementById('txt_ENE_Transaction_Type');
                        txt.value = str[1];
                        var hidtext = document.getElementById('hid_ENE_Transaction_Type_Text');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('hid_ENE_Transaction_Type');
                        hidid.value = str[0];
                    }
                }
                else {
                    var txt = document.getElementById('txt_ENE_Transaction_Type');
                    txt.value = "Select work type from lookup";
                    var hidtext = document.getElementById('hid_ENE_Transaction_Type_Text');
                    hidtext.value = "";
                    var hidid = document.getElementById('hid_ENE_Transaction_Type');
                    hidid.value = "";
                }
            }
        }
        function ReturnString(cbo) {
            var cbobject = document.getElementById(cbo);
            var combo = $find(cbobject.id);
            var comboValue = combo.get_text();  //document.getElementById(combo.InputID).value;.get_value()
            return comboValue;
        }

        function Validation() {
            var txt2 = document.getElementById('txt_ENE_Transaction_Type');

            if ((txt2 != null && txt2.value == "Select work type from lookup") &&
            (ReturnString('cbo_ENE_ContractTypeId') == "Select")) {
                alert('Please enter or select atleast one value !!!');
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</head>
<body style="background-color: #00759A">
    <form id="form1" runat="server">
     <asp:HiddenField runat="server" ID="hidName" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnl1" runat="server" Width="480px">
        <table cellpadding="2" cellspacing="0" border="0" width="99%">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblname" Text="EPC Energy" runat="server" CssClass="labelStyleHeading"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td style="width: 21%; padding-left: 10px; padding-top: 10px;" align="left">
                    <asp:Label ID="lbl_ENE_Transaction_Type" Text="Work type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td style="width: 75%; padding-top: 10px;">
                    <asp:TextBox runat="server" Width="320px" ID="txt_ENE_Transaction_Type" CssClass="txtSingleStyle"
                        Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                    <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender2" runat="server" TargetControlID="txt_ENE_Transaction_Type"
                        WatermarkText="Select work type from lookup" WatermarkCssClass="watermarked" />
                    <asp:ImageButton runat="server" ID="img_ENE_Transaction_Type" ImageUrl="~/Images/bino.jpg"
                        ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Work Type Look Up" />
                    <asp:HiddenField runat="server" ID="hid_ENE_Transaction_Type" />
                    <asp:HiddenField runat="server" ID="hid_ENE_Transaction_Type_Text" />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td align="left" style="padding-left: 10px;">
                    <asp:Label ID="lbl_ENE_ContractTypeId" Text="Contract type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="cbo_ENE_ContractTypeId" AllowCustomText="false" MarkFirstMatch="true"
                        runat="server" Width="345px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
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

