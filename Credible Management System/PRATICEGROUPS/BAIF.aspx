<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BAIF.aspx.cs" ValidateRequest="false" Inherits="CredentialsDemo.PRATICEGROUPS.BAIF" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Site2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        document.onkeypress = function (event) {
            if (typeof window.event != 'undefined') { // ie
                event = window.event;
                event.target = event.srcElement; // make ie confirm to standards !!
            }
            var kc = event.keyCode;
            var tt = event.target.type;
            if ((kc == 13) && (event.target.getAttribute('enter_ok') == 'true')) {
                return true; // ok
            }

            // alert('kc='+kc+", tt="+tt);
            if ((kc != 8 && kc != 13) || ((tt == 'text' || tt == 'password') && kc != 13) ||
 (tt == 'textarea') || (tt == 'submit' && kc == 13))
                return true;
            //            alert('Bksp/Enter is not allowed here');
            return false;
        }

        if (typeof window.event != 'undefined') // ie
            document.onkeydown = document.onkeypress; // Trap bksp in ie. !! Note: does not trap enter, but onkeypress does !!
    </script>
    <script type="text/javascript">
        function LoadChild(txt, id, hid) {
            var Return;
            var myArguments = new Object();
            var hidcheck = document.getElementById(hid);
            var qstr = txt + "~" + id + "~" + hidcheck.value + "~" + "1";
            /*myArguments.param1 = chkControl.value;*/
            Return = window.showModalDialog("../LOOKUPS/LookUpGrid.aspx?q=" + qstr + "~" + new Date().getTime(), myArguments, "dialogWidth:425px;dialogHeight:427px;")
            if (Return != undefined) {
                var str = Return.info.split('~~');
                if (str[0].length != 0 && str[1].length != 0) {
                    if (str[2] == "lbl_BAI_Work_Type") {
                        var txt = document.getElementById('txt_BAI_Work_Type');
                        txt.value = str[1];
                        var hidtext = document.getElementById('hid_BAI_Work_Type_Text');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('hid_BAI_Work_Type');
                        hidid.value = str[0];
                    }
                }
                else {
                    var txt = document.getElementById('txt_BAI_Work_Type');
                    txt.value = "Select Work Type from Look-Up";
                    var hidtext = document.getElementById('hid_BAI_Work_Type_Text');
                    hidtext.value = "";
                    var hidid = document.getElementById('hid_BAI_Work_Type');
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
            var txt1 = document.getElementById('txt_BAI_Work_Type');
            /*var txt2 = document.getElementById('txt_BAI_Lead_Bank_Role');*/
            var txt3 = document.getElementById('txt_BAI_LeadBanks');
            /*(txt2 != null && txt2.value == "Type Role Of Lead Bank") &&*/
            if ((txt1 != null && txt1.value == "Select work type from lookup") &&
            (ReturnString('cbo_BAI_ClientTypeIdBAIF') == "Select")) {
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
    <asp:Panel ID="pnl1" runat="server" Width="480px">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="lblname" Text="BAIF" runat="server" CssClass="labelStyleHeading"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td style="width: 21%; padding-left: 10px; padding-top: 10px;" align="left">
                    <asp:Label ID="lbl_BAI_ClientTypeIdBAIF" Text="Client type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td style="width: 75%; padding-top: 10px;">
                    <telerik:RadComboBox ID="cbo_BAI_ClientTypeIdBAIF" AllowCustomText="false" MarkFirstMatch="true"
                        runat="server" Width="345px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                        EnableEmbeddedSkins="false" Skin="MySkin" Height="110px" AutoPostBack="true"
                        OnSelectedIndexChanged="cbo_BAI_ClientTypeIdBAIF_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="background-color: White;" runat="server" id="tr_BAI_LeadBanks" visible="false">
                <td align="left" style="padding-left: 10px;" valign="top">
                    <asp:Label ID="lbl_BAI_LeadBanks" Text="Lead bank(s)" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" Width="339px" ID="txt_BAI_LeadBanks" CssClass="txtSingleStyle"
                        TextMode="MultiLine" Height="50px" MaxLength="300"></asp:TextBox>
                    <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender4" runat="server" TargetControlID="txt_BAI_LeadBanks"
                        WatermarkText="Type lead bank(s)" WatermarkCssClass="watermarked" />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td align="left" style="padding-left: 10px;">
                    <asp:Label ID="lbl_BAI_Work_Type" Text="Work type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" Width="320px" ID="txt_BAI_Work_Type" CssClass="txtSingleStyle"
                        Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                    <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender1" runat="server" TargetControlID="txt_BAI_Work_Type"
                        WatermarkText="Select work type from lookup" WatermarkCssClass="watermarked" />
                    <asp:ImageButton runat="server" ID="img_BAI_Work_Type" ImageUrl="~/Images/bino.jpg"
                        ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Work Type Look Up" />
                    <asp:HiddenField runat="server" ID="hid_BAI_Work_Type" />
                    <asp:HiddenField runat="server" ID="hid_BAI_Work_Type_Text" />
                    <br />
                    <br />
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
