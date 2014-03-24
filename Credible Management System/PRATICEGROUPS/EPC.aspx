<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EPC.aspx.cs" ValidateRequest="false"
    Inherits="CredentialsDemo.PRATICEGROUPS.EPC" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Site2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function zoomin(lbl) {
            var lbl = document.getElementById(lbl);
            lbl.style.display = "block";
        }
        function LoadChild(txt, id, hid) {
            var Return;
            var myArguments = new Object();
            var hidcheck = document.getElementById(hid);
            var qstr = txt + "~" + id + "~" + hidcheck.value + "~" + "5";
            /*myArguments.param1 = chkControl.value;*/
            Return = window.showModalDialog("../LOOKUPS/LookUpGrid.aspx?q=" + qstr + "~" + new Date().getTime(), myArguments, "dialogWidth:425px;dialogHeight:427px;")
            if (Return != undefined) {
                var str = Return.info.split('~~');
                if (str[0].length != 0 && str[1].length != 0) {
                    if (str[2] == "lbl_EPC_Nature_Of_Work") {
                        var txt = document.getElementById('txt_EPC_Nature_Of_Work');
                        txt.value = str[1];
                        var hidtext = document.getElementById('hid_EPC_Nature_Of_Work_Text');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('hid_EPC_Nature_Of_Work');
                        hidid.value = str[0];
                    }
                    else if (str[2] == "lbl_EPC_Type_Of_Contract") {
                        var txt = document.getElementById('txt_EPC_Type_Of_Contract');
                        //txt.value = str[1];
                        txt.value = str[1].replace(/&amp;/g, "&");
                        var strother = str[1];
                        var hidtext = document.getElementById('hid_EPC_Type_Of_Contract_Text');
                        //hidtext.value = str[1];
                        hidtext.value = str[1].replace(/&amp;/g, "&");
                        var hidid = document.getElementById('hid_EPC_Type_Of_Contract');
                        hidid.value = str[0];

                        var tr_EPC_Type_Of_Contract_Other = document.getElementById('tr_EPC_Type_Of_Contract_Other');
                        tr_EPC_Type_Of_Contract_Other.style.display = 'none';
                        var n = strother.indexOf("Others");
                        if (n > -1) {

                            tr_EPC_Type_Of_Contract_Other.style.display = 'block';
                            /* var lblother1 = document.getElementById('lbl_EPC_Type_Of_Contract_Other');
                            lblother1.style.display = 'block';*/
                            var hidother = document.getElementById('hid_EPC_Type_Of_Contract_Other');
                            hidother.value = 1;
                            var hid_EPC_Type_Of_Contract_Ctl = document.getElementById('hid_EPC_Type_Of_Contract_Ctl');
                            hid_EPC_Type_Of_Contract_Ctl.value = 1;
                        }
                        else {
                            var hidother = document.getElementById('hid_EPC_Type_Of_Contract_Other');
                            hidother.value = 0;
                            var hid_EPC_Type_Of_Contract_Ctl = document.getElementById('hid_EPC_Type_Of_Contract_Ctl');
                            hid_EPC_Type_Of_Contract_Ctl.value = 0;
                            var txtOther = document.getElementById('txt_EPC_Type_Of_Contract_Other');
                            txtOther.value = "";

                        }
                    }
                }
                else {
                    var txt = document.getElementById('txt_EPC_Nature_Of_Work');
                    txt.value = "Select work type from lookup";
                    var hidtext = document.getElementById('hid_EPC_Nature_Of_Work_Text');
                    hidtext.value = "";
                    var hidid = document.getElementById('hid_EPC_Nature_Of_Work');
                    hidid.value = "";

                    var txt = document.getElementById('txt_EPC_Type_Of_Contract');
                    txt.value = "Select type of contract from lookup";
                    var strother = "";
                    var hidtext = document.getElementById('hid_EPC_Type_Of_Contract_Text');
                    hidtext.value = "";
                    var hidid = document.getElementById('hid_EPC_Type_Of_Contract');
                    hidid.value = "";
                }
            }
        }
        function ReturnString(cbo) {
            var cbobject = document.getElementById(cbo);
            var combo = $find(cbobject.id);
            var comboValue = combo.get_text();
            return comboValue;
        }
        function ctlClientTypevisible() {
            var txt1 = document.getElementById('txt_EPC_Type_Of_Contract');
            var txt2 = document.getElementById('txt_EPC_Type_Of_Contract_Other');
            var txt3 = document.getElementById('txt_EPC_LegalIssueOther');
            var txt4 = document.getElementById('txt_EPC_Legal_Issues');
            var txt5 = document.getElementById('txt_EPC_SubjectMatterOther');

            if (ReturnString('cbo_EPC_SubjectMatterId') == "Other") {
                txt5.disabled = true;
            }
        }
        function ctlClientTypevisible(cbo, txt) {
            var txt6 = document.getElementById(txt);
            if (ReturnString(cbo) == "Other") {
                txt6.disabled = true;
            }
        }

        function Validation() {
            var txt1 = document.getElementById('txt_EPC_Nature_Of_Work');
            var txt2 = document.getElementById('txt_EPC_Type_Of_Contract');

            if ((txt1 != null && txt1.value == "Select work type from lookup") &&
            (txt2 != null && txt2.value == "Select type of contract from lookup") &&
            (ReturnString('cbo_EPC_SubjectMatterId') == "Select") &&
            (ReturnString('cbo_EPC_ClientScopeId') == "Select") &&
            (ReturnString('cbo_EPC_ClientTypeIdEPC') == "Select")) {
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnl1" runat="server" Width="500px">
                <table cellpadding="2" cellspacing="0" border="0" width="100%" style="background-color: White;">
                    <tr style="background-color: #00759A;">
                        <td colspan="2" align="center">
                            <asp:Label ID="lblname" Text="EPC Construction" runat="server" CssClass="labelStyleHeading"></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr style="background-color: White;">
                        <td align="left" style="width: 27%; padding-left: 10px; padding-top: 10px;">
                            <asp:Label ID="lbl_EPC_Nature_Of_Work" Text="Work type" runat="server" CssClass="labelStyle">
                            </asp:Label>
                        </td>
                        <td align="left" style="width: 75%; padding-top: 10px;">
                            <asp:TextBox runat="server" Width="320px" ID="txt_EPC_Nature_Of_Work" CssClass="txtSingleStyle"
                                Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender1" runat="server" TargetControlID="txt_EPC_Nature_Of_Work"
                                WatermarkText="Select work type from lookup" WatermarkCssClass="watermarked" />
                            <asp:ImageButton runat="server" ID="img_EPC_Nature_Of_Work" ImageUrl="~/Images/bino.jpg"
                                ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Work Type Look Up" />
                            <asp:HiddenField runat="server" ID="hid_EPC_Nature_Of_Work" />
                            <asp:HiddenField runat="server" ID="hid_EPC_Nature_Of_Work_Text" />
                        </td>
                    </tr>
                    <tr style="background-color: White;">
                        <td align="left" style="padding-left: 10px;">
                            <asp:Label ID="lbl_EPC_ClientScopeId" Text="Client scope" runat="server" CssClass="labelStyle">
                            </asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cbo_EPC_ClientScopeId" AllowCustomText="false" MarkFirstMatch="true"
                                runat="server" Width="345px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                EnableEmbeddedSkins="false" Skin="MySkin" Height="70px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr style="background-color: White;">
                        <td align="left" style="padding-left: 10px;">
                            <asp:Label ID="lbl_EPC_ClientTypeIdEPC" Text="Client type" runat="server" CssClass="labelStyle">
                            </asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cbo_EPC_ClientTypeIdEPC" AllowCustomText="false" MarkFirstMatch="true"
                                runat="server" Width="345px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                EnableEmbeddedSkins="false" Skin="MySkin" Height="120px" OnSelectedIndexChanged="cbo_EPC_ClientTypeIdEPC_SelectedIndexChanged1"
                                AutoPostBack="true">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr style="background-color: White;" runat="server" id="tr_EPC_ClientTypeOther" visible="false">
                        <td align="left" style="padding-left: 10px;">
                            <asp:Label ID="lbl_EPC_ClientTypeOther" Text="Other (please specify)" runat="server"
                                CssClass="labelStyle">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" Width="339px" ID="txt_EPC_ClientTypeOther" CssClass="txtSingleStyle"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hid_EPC_ClientTypeOther" Value="0" />
                            <asp:HiddenField runat="server" ID="hid_EPC_ClientTypeOther_Ctl" Value="0" />
                        </td>
                    </tr>
                    <tr style="background-color: White;">
                        <td align="left" style="padding-left: 10px;">
                            <asp:Label ID="lbl_EPC_Type_Of_Contract" Text="Type of contract" runat="server" CssClass="labelStyle">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" Width="320px" ID="txt_EPC_Type_Of_Contract" CssClass="txtSingleStyle"
                                Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender3" runat="server" TargetControlID="txt_EPC_Type_Of_Contract"
                                WatermarkText="Select type of contract from lookup" WatermarkCssClass="watermarked" />
                            <asp:ImageButton runat="server" ID="img_EPC_Type_Of_Contract" ImageUrl="~/Images/bino.jpg"
                                ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Type Of Contract Look Up" />
                            <asp:HiddenField runat="server" ID="hid_EPC_Type_Of_Contract" />
                            <asp:HiddenField runat="server" ID="hid_EPC_Type_Of_Contract_Text" />
                        </td>
                    </tr>
                    <tr style="background-color: White; display: none;" runat="server" id="tr_EPC_Type_Of_Contract_Other">
                        <td align="left" style="padding-left: 10px;">
                            <asp:Label ID="lbl_EPC_Type_Of_Contract_Other" Text="Other (please specify)" runat="server"
                                CssClass="labelStyle">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" Width="339px" ID="txt_EPC_Type_Of_Contract_Other" CssClass="txtSingleStyle"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hid_EPC_Type_Of_Contract_Other" Value="0" />
                            <asp:HiddenField runat="server" ID="hid_EPC_Type_Of_Contract_Ctl" Value="0" />
                        </td>
                    </tr>
                    <tr style="background-color: White;">
                        <td align="left" style="padding-left: 10px;">
                            <asp:Label ID="lbl_EPC_SubjectMatterId" Text="Subject matter" runat="server" CssClass="labelStyle">
                            </asp:Label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cbo_EPC_SubjectMatterId" AllowCustomText="false" MarkFirstMatch="true"
                                runat="server" Width="343px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                EnableEmbeddedSkins="false" Skin="MySkin" Height="120px" OnSelectedIndexChanged="cbo_EPC_SubjectMatterId_SelectedIndexChanged"
                                AutoPostBack="true">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr style="background-color: White;" runat="server" id="tr_EPC_Subject_Matter_Other"
                        visible="false">
                        <td align="left" style="padding-left: 10px;">
                            <asp:Label ID="lbl_EPC_Subject_Matter_Other" Text="Other (please specify)" runat="server"
                                CssClass="labelStyle" Visible="true">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:TextBox runat="server" Width="339px" ID="txt_EPC_SubjectMatterOther" CssClass="txtSingleStyle"
                                Visible="true"></asp:TextBox>
                            <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender7" runat="server" TargetControlID="txt_EPC_SubjectMatterOther"
                                WatermarkText="Type other description" WatermarkCssClass="watermarked" />
                            <asp:HiddenField runat="server" ID="hidSubjectMatterOther" Value="0" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
