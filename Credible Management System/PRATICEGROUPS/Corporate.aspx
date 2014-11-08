<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Corporate.aspx.cs" ValidateRequest="false"
    Inherits="CredentialsDemo.PRATICEGROUPS.Corporate" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Site2.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TreeView.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Calendar.Outlook.css" rel="stylesheet" type="text/css" />
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
            function AlphaNumericonly(e, ctrl) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if ((key >= 48 && key <= 57) || ((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32')) {
                    return true;
                }
                else {

                    return false;
                }
            }

            function numberonly(e) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if ((key >= '48' && key <= '57') || (key == '8') || (key == '37') || (key == '38') || (key == '39') || (key == '40') || (key == '9'))
                    return true;
                else
                    return false;
            }

            function CheckComboValidation(cbo) {
                var cbobject = document.getElementById(cbo);
                var combo = $find(cbobject.id);
                var comboValue = combo.get_text();
                return comboValue;
            }
            function LoadCountryChild(txt, id, hid) {
                var Return;
                var myArguments = new Object();
                var hidcheck = document.getElementById(hid);
                var qstr = txt + "~" + id + "~" + hidcheck.value;
                /*myArguments.param1 = chkControl.value;*/
                Return = window.showModalDialog("../LOOKUPS/CountryLookUpGrid.aspx?q=" + qstr + "~" + new Date().getTime(), myArguments, "dialogWidth:425px;dialogHeight:427px;")
                if (Return != undefined) {
                    var str = Return.info.split('~~');
                    if (str[0].length != 0 && str[1].length != 0) {
                        if (str[2] == "lbl_Cor_Country_Buyer") {
                            var txt = document.getElementById('txt_Cor_Country_Buyer');
                            txt.value = str[0].replace(/&amp;/g, "&");
                            var hidtext = document.getElementById('hid_Cor_Country_Buyer_Text');
                            hidtext.value = str[0];
                            var hidid = document.getElementById('hid_Cor_Country_Buyer');
                            hidid.value = str[1];
                        }
                        if (str[2] == "lbl_Cor_Country_Seller") {
                            var txt = document.getElementById('txt_Cor_Country_Seller');
                            txt.value = str[0];
                            var hidtext = document.getElementById('hid_Cor_Country_Seller_Text');
                            hidtext.value = str[0];
                            var hidid = document.getElementById('hid_Cor_Country_Seller');
                            hidid.value = str[1];
                        }
                        if (str[2] == "lbl_Cor_Country_Target") {
                            var txt = document.getElementById('txt_Cor_Country_Target');
                            txt.value = str[0];
                            var hidtext = document.getElementById('hid_Cor_Country_Target_Text');
                            hidtext.value = str[0];
                            var hidid = document.getElementById('hid_Cor_Country_Target');
                            hidid.value = str[1];
                        }
                    }
                }
            }
            function GetSelectedNodes() {
                //var rtv = document.getElementById(cbo);
                var tree = $find("<%= RadTreeView1.ClientID %>");
                var nodes = tree.get_checkedNodes();
                var i = nodes.length;
                return i;
            }

            function ValidationCorp() {

                var txt_Cor_Acting_For = document.getElementById('txt_Cor_Acting_For');
                var txt_Cor_Country_Buyer = document.getElementById('txt_Cor_Country_Buyer');
                var txt_Cor_Country_Seller = document.getElementById('txt_Cor_Country_Seller');
                var txt_Cor_Country_Target = document.getElementById('txt_Cor_Country_Target');
                var txt_Cor_Published_Reference = document.getElementById('txt_Cor_Published_Reference');
                var txt_Cor_YearDeal_Announced = document.getElementById('txt_Cor_YearDeal_Announced');

                var cbo_Cor_ValueRangeEuro = document.getElementById('cbo_Cor_ValueRangeEuro');

                var cbo_Cor_MAStudy = document.getElementById('cbo_Cor_MAStudy');
                var cbo_Cor_QuarterDealAnnouncedId = document.getElementById('cbo_Cor_QuarterDealAnnouncedId');
                var cbo_Cor_QuarterDealCompletedId = document.getElementById('cbo_Cor_QuarterDealCompletedId');
                var cbo_Cor_YearDealCompletedId = document.getElementById('cbo_Cor_YearDealCompletedId'); //
                var cbo_Cor_YearDeal_Announced = document.getElementById('cbo_Cor_YearDeal_Announced');

                var cbo_Cor_Value_Over_US = $find('cbo_Cor_Value_Over_US').get_checkedItems();
                var cbo_Cor_Value_Over_Pound = $find('cbo_Cor_Value_Over_Pound').get_checkedItems();
                var cbo_Cor_Value_Over_Euro = $find('cbo_Cor_Value_Over_Euro').get_checkedItems();


                if (GetSelectedNodes() <= 0) {
                    alert("Please select worktype !!!");
                    return false;
                }
                if (txt_Cor_Country_Buyer != null && Trim(txt_Cor_Country_Buyer.value) == "Select country of buyer from lookup") {
                    alert("Please select country of buyer !!!");
                    return false;
                }
                if (txt_Cor_Country_Seller != null && Trim(txt_Cor_Country_Seller.value) == "Select country of seller from lookup") {
                    alert("Please select country of seller !!!");
                    return false;
                }
                if (txt_Cor_Country_Target != null && Trim(txt_Cor_Country_Target.value) == "Select country of target from lookup") {
                    alert("Please select country of target !!!");
                    return false;
                }

                if (cbo_Cor_Value_Over_US != null && cbo_Cor_Value_Over_US.length == 0) {
                    alert("Please select value over US$5m !!!");
                    return false;
                }
                if (cbo_Cor_Value_Over_Pound != null && cbo_Cor_Value_Over_Pound.length == 0) {
                    alert("Please select value over £500,000 !!!");
                    return false;
                }
                if (cbo_Cor_Value_Over_Euro != null && cbo_Cor_Value_Over_Euro.length == 0) {
                    alert("Please select value over euro 5m !!!");
                    return false;
                }
                if (cbo_Cor_ValueRangeEuro != null && CheckComboValidation(cbo_Cor_ValueRangeEuro.id) == "Select") {
                    alert("Please select Value range on deal currency !!!");
                    return false;
                }
                if (txt_Cor_Acting_For != null && Trim(txt_Cor_Acting_For.value) == "Select acting type from lookup") {
                    alert("Please select acting for !!!");
                    return false;
                }

                if (txt_Cor_Published_Reference != null && Trim(txt_Cor_Published_Reference.value) == "Type published reference") {
                    alert("Please select published reference !!!");
                    return false;
                }
                if (cbo_Cor_MAStudy != null && CheckComboValidation(cbo_Cor_MAStudy.id) == "Select") {
                    alert("Please select is the deal relevant for M&A study !!!");
                    return false;
                }
                if (cbo_Cor_QuarterDealAnnouncedId != null && CheckComboValidation(cbo_Cor_QuarterDealAnnouncedId.id) == "Select") {
                    alert("Please select quarter deal announced !!!");
                    return false;
                }

                if (cbo_Cor_QuarterDealCompletedId != null && CheckComboValidation(cbo_Cor_QuarterDealCompletedId.id) == "Select") {
                    alert("Please select quarter deal completed");
                    return false;
                }
                if (cbo_Cor_YearDeal_Announced != null && CheckComboValidation(cbo_Cor_YearDeal_Announced.id) == "Select") {
                    alert("Please select year deal announced/signed");
                    return false;
                }
                if (cbo_Cor_YearDealCompletedId != null && CheckComboValidation(cbo_Cor_YearDealCompletedId.id) == "Select") {
                    alert("Please select year deal completed");
                    return false;
                }

                return true;

            }
            function LoadChild(txt, id, hid) {
                var Return;
                var myArguments = new Object();
                var hidcheck = document.getElementById(hid);
                var qstr = txt + "~" + id + "~" + hidcheck.value + "~" + "3";
                /*myArguments.param1 = chkControl.value;*/
                Return = window.showModalDialog("../LOOKUPS/LookUpGrid.aspx?q=" + qstr + "~" + new Date().getTime(), myArguments, "dialogWidth:425px;dialogHeight:427px;")
                if (Return != undefined) {
                    var str = Return.info.split('~~');

                    if (str[2] == "lbl_Cor_Acting_For") {
                        var txt = document.getElementById('txt_Cor_Acting_For');
                        txt.value = str[1];
                        var hidtext = document.getElementById('hid_Cor_Acting_For_Text');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('hid_Cor_Acting_For');
                        hidid.value = str[0];
                    }

                    else if (str[2] == "lbl_Cor_Value_Over_Euro") {
                        var txt = document.getElementById('txt_Cor_Value_Over_Euro');
                        txt.value = str[1];
                        var hidtext = document.getElementById('hid_Cor_Value_Over_Euro_Text');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('hid_Cor_Value_Over_Euro');
                        hidid.value = str[0];
                    }
                }
            }


            function ReturnString(cbo) {
                var combo = $get(cbo);
                var nm = cbo + "_HiddenField"
                var comboHidden = $get(nm);
                var selectedValue = combo.getElementsByTagName('li')[comboHidden.value].innerHTML;
                return selectedValue;
            }

            function Validation() {

                var txt3 = document.getElementById('txt_Cor_Acting_For');
                var txt4 = document.getElementById('txt_Cor_Country_Buyer');
                var txt5 = document.getElementById('txt_Cor_Country_Seller');
                var txt6 = document.getElementById('txt_Cor_Country_Target');
                var txt10 = document.getElementById('txt_Cor_Published_Reference');
                var txt11 = document.getElementById('txt_Cor_YearDeal_Announced');

                if (
            (txt3 != null && txt3.value == "Select acting type from lookup") &&
            (txt4 != null && txt4.value == "Select country of buyer from lookup") &&
            (txt5 != null && txt5.value == "Select country of seller from lookup") &&
            (txt6 != null && txt6.value == "Select country of target from lookup") &&
            (txt10 != null && txt10.value == "Type published reference") &&
            (txt11 != null && txt11.value == "Type year deal announced/signed") &&
            (ReturnString('cbo_Cor_Value_Over_US') == "Select") &&
            (ReturnString('cbo_Cor_Value_Over_Pound') == "Select") &&
            (ReturnString('cbo_Cor_ValueRangeEuro') == "Select") &&
            (ReturnString('cbo_Cor_Value_Over_Euro') == "Select") &&
            (ReturnString('cbo_Cor_MAStudy') == "Select") &&
            (ReturnString('cbo_Cor_PEClients') == "Select") &&
            (ReturnString('cbo_Cor_QuarterDealAnnouncedId') == "Select") &&

            (ReturnString('cbo_Cor_QuarterDealCompletedId') == "Select") &&
            (ReturnString('cbo_Cor_YearDealCompletedId') == "Select")) {
                    alert('All Fields cannot be empty. Please select or enter the values in the required field');
                    return false;
                }
                else {
                    return true;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <style type="text/css">
        * + html body span.riSingle input[type="text"].riTextBox
        {
            margin-top: 0;
        }
    </style>
</head>
<body style="background-color: #00759A;">
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="hidName" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Panel ID="pnl1" runat="server" Width="800px">
        <table cellpadding="2" cellspacing="0" border="0" width="100%" style="background-color: White;">
            <tr style="display: block; background-color: #00759A;">
                <td style="width: 15%;">
                </td>
                <td style="width: 30%">
                </td>
                <td style="width: 15%;">
                </td>
                <td style="width: 40%">
                </td>
            </tr>
            <tr style="background-color: #00759A;">
                <td colspan="4" align="center">
                    <asp:Label ID="lblname" Text="Corporate" runat="server" CssClass="labelStyleHeading"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px;" align="left" colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_Work_Type" Text="Work type" runat="server" CssClass="labelStyle">
                    </asp:Label>
                    <div style="border: 1px solid #000080; overflow: scroll; width: 360px;
                        padding-left: 5px; height: 162px; background-color: #F0F8FF;">
                        <telerik:RadTreeView runat="server" ID="RadTreeView1" CheckBoxes="true" OnClientNodeChecked="clientNodeChecked"
                            ShowLineImages="true" EnableEmbeddedSkins="false" Skin="MySkin">
                            <CollapseAnimation Duration="100" Type="OutQuint" />
                            <ExpandAnimation Duration="100" Type="OutBounce" />
                        </telerik:RadTreeView>
                    </div>
                </td>
                <td colspan="2">
                    <table cellpadding="2" cellspacing="0" width="100%" style="margin-top: 10px;">
                        <tr>
                            <td style="padding-left: 5px;">
                                <asp:Label ID="lbl_Cor_Country_Buyer" Text="Country of buyer" runat="server" CssClass="labelStyle">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" Width="260px" ID="txt_Cor_Country_Buyer" CssClass="txtSingleStyle"
                                    Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender4" runat="server" TargetControlID="txt_Cor_Country_Buyer"
                                    WatermarkText="Select country of buyer from lookup" WatermarkCssClass="watermarked" />
                                <asp:ImageButton runat="server" ID="img_Cor_Country_Buyer" ImageUrl="~/Images/bino.jpg"
                                    ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Predominant Country Of Buyer Look Up" />
                                <asp:HiddenField runat="server" ID="hid_Cor_Country_Buyer" />
                                <asp:HiddenField runat="server" ID="hid_Cor_Country_Buyer_Text" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">
                                <asp:Label ID="lbl_Cor_Country_Seller" Text="Country of seller" runat="server" CssClass="labelStyle">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" Width="260px" ID="txt_Cor_Country_Seller" CssClass="txtSingleStyle"
                                    Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender5" runat="server" TargetControlID="txt_Cor_Country_Seller"
                                    WatermarkText="Select country of seller from lookup" WatermarkCssClass="watermarked" />
                                <asp:ImageButton runat="server" ID="img_Cor_Country_Seller" ImageUrl="~/Images/bino.jpg"
                                    ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Predominant Country Of Seller Look Up" />
                                <asp:HiddenField runat="server" ID="hid_Cor_Country_Seller" />
                                <asp:HiddenField runat="server" ID="hid_Cor_Country_Seller_Text" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">
                                <asp:Label ID="lbl_Cor_Country_Target" Text="Country of target" runat="server" CssClass="labelStyle">
                                </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" Width="260px" ID="txt_Cor_Country_Target" CssClass="txtSingleStyle"
                                    Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                                <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender6" runat="server" TargetControlID="txt_Cor_Country_Target"
                                    WatermarkText="Select country of target from lookup" WatermarkCssClass="watermarked" />
                                <asp:ImageButton runat="server" ID="img_Cor_Country_Target" ImageUrl="~/Images/bino.jpg"
                                    ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Predominant Country Of Target Look Up" />
                                <asp:HiddenField runat="server" ID="hid_Cor_Country_Target" />
                                <asp:HiddenField runat="server" ID="hid_Cor_Country_Target_Text" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">
                                <asp:Label ID="lbl_Cor_Value_Over_US" Text="Value over US$5m" runat="server" CssClass="labelStyle">
                                </asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cbo_Cor_Value_Over_US" AllowCustomText="false" MarkFirstMatch="true"
                                    CheckedItemsTexts="DisplayAllInInput" runat="server" Width="282px" CheckBoxes="true"
                                    EnableCheckAllItemsCheckBox="false" EnableEmbeddedSkins="false" Skin="MySkin"
                                    Height="70px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">
                                <asp:Label ID="lbl_Cor_Value_Over_Pound" Text="Value over £500,000" runat="server"
                                    CssClass="labelStyle">
                                </asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cbo_Cor_Value_Over_Pound" AllowCustomText="false" MarkFirstMatch="true"
                                    CheckedItemsTexts="DisplayAllInInput" runat="server" Width="282px" CheckBoxes="true"
                                    EnableCheckAllItemsCheckBox="false" EnableEmbeddedSkins="false" Skin="MySkin"
                                    Height="70px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">
                                <asp:Label ID="lbl_Cor_Value_Over_Euro" Text="Value over Euro 5m" runat="server"
                                    CssClass="labelStyle">
                                </asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cbo_Cor_Value_Over_Euro" AllowCustomText="false" MarkFirstMatch="true"
                                    CheckedItemsTexts="DisplayAllInInput" runat="server" Width="282px" CheckBoxes="true"
                                    EnableCheckAllItemsCheckBox="false" EnableEmbeddedSkins="false" Skin="MySkin"
                                    Height="70px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <%--Value range in Euros -> Value range on deal currency--%>
                    <asp:Label ID="lbl_Cor_ValueRangeEuro" Text="Value range on deal currency" runat="server"
                        CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_DealAnnouncedId" Text="Date deal announced/signed" runat="server"
                        CssClass="labelStyle">
                    </asp:Label>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <telerik:RadComboBox ID="cbo_Cor_ValueRangeEuro" AllowCustomText="false" MarkFirstMatch="true"
                        CheckedItemsTexts="DisplayAllInInput" runat="server" Width="372px" CheckBoxes="false"
                        EnableCheckAllItemsCheckBox="false" EnableEmbeddedSkins="false" Skin="MySkin"
                        Height="110px">
                    </telerik:RadComboBox>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <telerik:RadDatePicker Width="360px" EnableEmbeddedSkins="false" Skin="Myskin" Font-Size="11px"
                        DateInput-Font-Size="11px" ShowPopupOnFocus="true" ID="cld_Cor_DealAnnouncedId"
                        runat="server" Culture="en-GB" DateInput-DisplayDateFormat="dd/MM/yyyy" Calendar-FastNavigationStep="12"
                        DateInput-ReadOnly="true" DateInput-DateFormat="dd/MM/yyyy" DateInput-EmptyMessage=""
                        DateInput-Height="30px">
                        <DateInput ID="DateInput2" CssClass="txtSingleStyle " Width="130px" runat="server">
                        </DateInput>
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_Acting_For" Text="Acting for" runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_Published_Reference" Text="Published reference" runat="server"
                        CssClass="labelStyle">
                    </asp:Label>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:TextBox runat="server" Width="347px" ID="txt_Cor_Acting_For" CssClass="txtSingleStyle"
                        Visible="true" ReadOnly="true" Enabled="false"></asp:TextBox>
                    <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender3" runat="server" TargetControlID="txt_Cor_Acting_For"
                        WatermarkText="Select acting type from lookup" WatermarkCssClass="watermarked" />
                    <asp:ImageButton runat="server" ID="img_Cor_Acting_For" ImageUrl="~/Images/bino.jpg"
                        ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Acting Type Look Up" />
                    <asp:HiddenField runat="server" ID="hid_Cor_Acting_For" />
                    <asp:HiddenField runat="server" ID="hid_Cor_Acting_For_Text" />
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:TextBox runat="server" Width="393px" ID="txt_Cor_Published_Reference" CssClass="txtSingleStyle"
                        MaxLength="450"></asp:TextBox>
                    <ajax:TextBoxWatermarkExtender ID="Textboxwatermarkextender8" runat="server" TargetControlID="txt_Cor_Published_Reference"
                        WatermarkText="Type published reference" WatermarkCssClass="watermarked" />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_MAStudy" Text="Is the deal relevant for M&A study" runat="server"
                        CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_PEClients" Text="Does the deal involve PE clients on either side"
                        runat="server" CssClass="labelStyle">
                    </asp:Label>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <telerik:RadComboBox ID="cbo_Cor_MAStudy" AllowCustomText="false" MarkFirstMatch="true"
                        runat="server" Width="372px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                        EnableEmbeddedSkins="false" Skin="MySkin" Height="60px">
                    </telerik:RadComboBox>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <telerik:RadComboBox ID="cbo_Cor_PEClients" AllowCustomText="false" MarkFirstMatch="true"
                        runat="server" Width="398px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                        EnableEmbeddedSkins="false" Skin="MySkin" Height="60px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_QuarterDealAnnouncedId" Text="Quarter deal announced" runat="server"
                        CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_QuarterDealCompletedId" Text="Quarter deal completed" runat="server"
                        CssClass="labelStyle">
                    </asp:Label>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <telerik:RadComboBox ID="cbo_Cor_QuarterDealAnnouncedId" AllowCustomText="false"
                        MarkFirstMatch="true" runat="server" Width="372px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                        EnableEmbeddedSkins="false" Skin="MySkin" Height="110px">
                    </telerik:RadComboBox>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <telerik:RadComboBox ID="cbo_Cor_QuarterDealCompletedId" AllowCustomText="false"
                        MarkFirstMatch="true" runat="server" Width="398px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                        EnableEmbeddedSkins="false" Skin="MySkin" Height="110px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_YearDeal_Announced" Text="Year deal announced/signed" runat="server"
                        CssClass="labelStyle">
                    </asp:Label>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <asp:Label ID="lbl_Cor_YearDealCompletedId" Text="Year deal completed" runat="server"
                        CssClass="labelStyle">
                    </asp:Label>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <telerik:RadComboBox ID="cbo_Cor_YearDeal_Announced" AllowCustomText="false" MarkFirstMatch="true"
                        runat="server" Width="372px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                        EnableEmbeddedSkins="false" Skin="MySkin" Height="110px">
                    </telerik:RadComboBox>
                </td>
                <td colspan="2" align="left" style="padding-left: 7px;">
                    <telerik:RadComboBox ID="cbo_Cor_YearDealCompletedId" AllowCustomText="false" MarkFirstMatch="true"
                        runat="server" Width="398px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                        EnableEmbeddedSkins="false" Skin="MySkin" Height="110px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="4" align="center">
                    <asp:Label ID="Label1" Text="" runat="server" CssClass="labelStyledetails" Visible="false"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
            <tr style="background-color: White;">
                <td colspan="4" align="center">
                    <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" CssClass="btncolor" />
                    <%--<asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" OnClientClick="return ValidationCorp();"
                        CssClass="btncolor" />--%>
                    <asp:Button ID="btnClear" runat="server" Text="CLEAR" OnClick="btnClear_Click" CssClass="btncolor" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
