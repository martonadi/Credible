<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="SearchScreen.aspx.cs" Inherits="CredentialsDemo.SEARCH.SearchScreen" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ OutputCache Location="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <telerik:RadCodeBlock ID="id1" runat="server">
        <link href="../Styles/ListBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
        <link href="../Styles/Button.Office2010Silver.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            * + html body span.riSingle input[type="text"].riTextBox
            {
                margin-top: 0;
            }
        </style>
        <style type="text/css">
            .rbDecorated
            {
                line-height: normal !important;
            }
        </style>
        <script type="text/javascript" language="javascript">

            function clientTransferringA(sender, args) {
                var listbox = $find(sender.get_id());
                if (listbox != null && listbox.get_items().get_count() >= 0) {
                    listbox.get_element().style.backgroundColor = "#F5F5DC";
                }
            }
            function clientTransferringB(sender, args) {
                var listbox = $find(sender.get_id());
                if (listbox != null && listbox.get_items().get_count() >= 0) {
                    listbox.get_element().style.backgroundColor = "#F0F8FF";
                }

                if (args != null && args.get_sourceListBox().get_items().get_count() == 1) {
                    args.get_sourceListBox().get_element().style.backgroundColor = "#FFFFFF";
                }
            }


            function ValidateDates() {
                var cld_Tab_Date_Opened = $find("<%= cld_Tab_Date_Opened.ClientID %>");
                var cld_Tab_Date_Opened1 = $find("<%= cld_Tab_Date_Opened1.ClientID %>");
                if (cld_Tab_Date_Opened != null && cld_Tab_Date_Opened1 != null) {
                    var txtdate = cld_Tab_Date_Opened.get_textBox();

                    var objFromDate = cld_Tab_Date_Opened.get_textBox();
                    var objToDate = cld_Tab_Date_Opened1.get_textBox();

                    if (objFromDate != null && objToDate != null
                    && objFromDate.value.length > 0 && objToDate.value.length > 0) {
                        var Fromdate = objFromDate.value.split('/');
                        var ToDate = objToDate.value.split('/');
                        var d1 = new Date();
                        d1.setDate(Fromdate[0]);
                        d1.setMonth(Fromdate[1] - 1);
                        d1.setYear(Fromdate[2]);

                        var d2 = new Date();
                        d2.setDate(ToDate[0]);
                        d2.setMonth(ToDate[1] - 1);
                        d2.setYear(ToDate[2]);
                        if (d2 < d1) {
                            alert('To date should be always greater than from date');
                            objFromDate.disabled = false;
                            objFromDate.focus();
                            objFromDate.disabled = true;
                            return false;
                        }
                    }
                }
            }



            function ClearDate() {
                var cld_Tab_Date_Opened = $find("<%= cld_Tab_Date_Opened.ClientID %>");
                var cld_Tab_Date_Opened1 = $find("<%= cld_Tab_Date_Opened1.ClientID %>");
                var chk_Tab_ActualDate_Ongoing = document.getElementById('<%= chk_Tab_ActualDate_Ongoing.ClientID %>');
                var chk_Tab_ActualDate_NotKnown = document.getElementById('<%= chk_Tab_ActualDate_NotKnown.ClientID %>');

                if (cld_Tab_Date_Opened != null) {
                    cld_Tab_Date_Opened.clear();
                }
                if (cld_Tab_Date_Opened1 != null) {
                    cld_Tab_Date_Opened1.clear();
                }
                if (chk_Tab_ActualDate_Ongoing != null) {
                    chk_Tab_ActualDate_Ongoing.checked = false;
                }
                if (chk_Tab_ActualDate_NotKnown != null) {
                    chk_Tab_ActualDate_NotKnown.checked = false;
                }
            }


        </script>
        <script type="text/javascript" language="javascript">

            function TextBoxPaste(txtid) {
                var txtbox = document.getElementById(txtid);
                var pasteData = window.clipboardData.getData("Text");
                txtbox.value = pasteData.replace("'", "");
                return false;
            }

            function GetCheckNodes() {
                var tree = $find("<%= RadTreeView1.ClientID %>");
                var nodes = tree.get_checkedNodes();
                for (i = 0; i < nodes.length; i++) {
                    nodes[i].set_checked(false);
                }
            }
            function GetCheckNodes1() {
                var tree = $find("<%= RadTreeView2.ClientID %>");
                var nodes = tree.get_checkedNodes();
                for (i = 0; i < nodes.length; i++) {
                    nodes[i].set_checked(false);
                }
            }
            function ChangeColor(sender, eventArgs) {
                var combo = $find(sender.get_id());
                if (combo != null) {
                    var items = combo.get_checkedItems();
                    var inputElement = combo.get_inputDomElement();
                    if (items != null) {
                        if (items.length > 0) {
                            inputElement.style.backgroundColor = "#F5F5DC";
                        }
                        else {
                            combo.set_text("");
                            inputElement.style.backgroundColor = "#F0F8FF";
                        }
                    }
                }
            }

            function UnCheckAll(cid) {
                var combo = $find(cid);
                var items = combo.get_checkedItems();
                var i = 0;
                while (i < items.length) {
                    items[i].uncheck();
                    i++;
                }
                combo.clearSelection();
                var inputElement = combo.get_inputDomElement();
                if (inputElement != null) {
                    inputElement.style.backgroundColor = "#F0F8FF";
                }
            }

            /*function UpdateAllChildren(nodes, checked) {
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
            }*/

            function clientNodeChecked(sender, eventArgs) {
                //1- Uncheck Parent Node in upward fashion
                var node = eventArgs.get_node();
                if (!node.get_checked()) {
                    while (node.get_parent().set_checked != null) {
                        node.get_parent().set_checked(false);
                        node = node.get_parent();
                    }
                }

                //2- Check Child Nodes (if any)
                var childNodes = eventArgs.get_node().get_nodes();
                var isChecked = eventArgs.get_node().get_checked();
                UpdateAllChildren(childNodes, isChecked);

                //3- If all sibling nodes are checked then check parent as well
                //Get Sibling nodes first
                var siblingNodes = eventArgs.get_node().get_parent().get_nodes();
                var i;
                var allSiblingsChecked = true;

                //4- Loop through all sibling nodes and see if all of them are checked
                for (i = 0; i < siblingNodes.get_count(); i++) {
                    isChecked = siblingNodes.getNode(i).get_checked();
                    if (!isChecked) {
                        allSiblingsChecked = false;
                        break;
                    }
                }
                if (allSiblingsChecked) {
                    //Check parent node now
                    eventArgs.get_node().get_parent().set_checked(true);
                }
            }

            function UpdateAllChildren(nodes, checked) {
                var i;
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

            function ConfirmClear() {
                if (confirm("Do you want to clear all the search criteria entered values ?")) {
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


            function AlphaNumericonlySpl(e, ctrl) {
                /*/ ( ) , &  - 47 40  41 44  38 45 */
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if ((key >= 48 && key <= 57) || ((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32') || (key == '47') || (key == '40') || (key == '41') || (key == '44') || (key == '38') || (key == '45')) {
                    return true;
                }
                else {
                    return false;
                }
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
            function AllowOnlyAlphabetsSpl(e) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if (((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32') || (key == '46') || (key == '44') || (key == '38') || (key == '40') || (key == '41') || (key == '47') || (key == '92') || (key == '45') || (key == '95')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            function AllowOnlyAlphabets(e) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if (((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            function numbercommadotonly(e) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if ((key >= '48' && key <= '57') || (key == '8') || (key == '44') || (key == '46'))
                    return true;
                else
                    return false;
            }
            function AlphaNumericDotonly(e, ctrl) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if ((key >= 48 && key <= 57) || ((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32') || (key == '46')) {
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
            function BlockEnter(e) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if (navigator.appName == "Microsoft Internet Explorer" && key == 13) {
                    e.cancelBubble = true
                    e.returnValue = false;
                    e.preventDefault();
                }
                if (navigator.appName == "Netscape" && key == '13') {
                    sKey = e.which ? e.which : e.keyCode;
                    e.stopPropagation();
                    e.preventDefault();
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <style type="text/css">
        .radsearch
        {
            height: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <asp:HiddenField runat="server" ID="hidName" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="600">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="Panel2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="30"
        BackColor="#E0E0E0" InitialDelayTime="500">
        <asp:Image ID="imgLoading" Style="margin-top: 10%" runat="server" ImageUrl="~/Images/321.gif"
            BorderWidth="0px" AlternateText="Loading" ImageAlign="AbsMiddle" />
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="Panel2" runat="server">
        <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <tr>
                <td style="width: 26%;" valign="top">
                    <div id="div1" style="border: 1px solid #00759A; line-height: 15px; width: 175px;
                        height: 25px; padding-top: 5px; padding-bottom: 5px; padding-left: 5px; color: #00759A;
                        font-size: 11px;" runat="server">
                        <b>
                            <label>
                                Need help finding credentials?</label>
                            <label>
                                Call extn: 2424</label></b>
                    </div>
                </td>
                <td style="width: 59%;" align="center">
                    <table cellpadding="2" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td style="width: 78%;" align="center">
                                <asp:Image ID="d" runat="server" ImageUrl="~/Images/Credible1.jpg" />
                            </td>
                            <td style="width: 22%;">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 15%;" valign="top" align="center">
                    <div id="divlink" style="border: 1px solid #00759A; line-height: 20px; width: 150px;
                        height: 95px; padding-top: 10px; padding-bottom: 10px;" runat="server">
                        <asp:LinkButton runat="server" ID="lnkKeywordSearch" Text="Quick Search" OnClick="lnkKeywordSearch_Click"
                            CssClass="labelStyleheading" ForeColor="RosyBrown" Visible="true"></asp:LinkButton><br />
                        <asp:LinkButton runat="server" ID="lnkBasic" Text="Basic Search" OnClick="lnkBasic_Click"
                            CssClass="labelStyleheading" ForeColor="#00759A" Visible="true"></asp:LinkButton>
                        <br />
                        <asp:LinkButton runat="server" ID="lnkAdvance" Text="Advanced Search" OnClick="lnkAdvance_Click"
                            CssClass="labelStyleheading" ForeColor="#00759A" Visible="true"></asp:LinkButton>
                        <asp:HiddenField ID="hidbasic" runat="server" Value="0" />
                        <asp:HiddenField ID="hidadvanced" runat="server" Value="0" />
                        <br />
                        <asp:LinkButton runat="server" ID="lnkClearSearch" Text="Reset" OnClick="btnClearSearch_Click"
                            CssClass="labelStyleheading" ForeColor="#00759A" Visible="true" OnClientClick="return ConfirmClear();"></asp:LinkButton>
                        <br />
                        <a href="../FAQ/Credible_Quick_Reference_Guide_FAQ.doc" class="labelStyleheading"
                            style="color: #00759A;">FAQs</a>
                    </div>
                    <span style="margin-top: 5px;"></span>
                    <asp:CheckBox ID="chkPartial" runat="server" Text="Include partial save" Visible="false"
                        CssClass="labelStyleheading" ForeColor="#00759A" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left">
                    <table cellpadding="2" cellspacing="0" width="100%" border="0">
                        <tr style="display: block;">
                            <td style="width: 78%;" align="center">
                                <asp:Label ID="lnkQuick" Text="Quick Search" runat="server" CssClass="labelStyle"
                                    Font-Size="14px" ForeColor="RosyBrown">
                                </asp:Label>
                            </td>
                            <td style="width: 22%;">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <telerik:RadAutoCompleteBox runat="server" ID="radtxtKeywordSearch" InputType="Token"
                                    AllowCustomToken="true" Height="200px" Width="500px" DropDownWidth="500px" DropDownHeight="200px"
                                    BorderColor="Gray" BorderWidth="1px" BorderStyle="Solid" CssClass="panelStyle1"
                                    BackColor="#F0F8FF">
                                </telerik:RadAutoCompleteBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label runat="server" ID="lblHT" Text="Searches: Client name,sector and keywords"
                                    CssClass="labelStyle10spa"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <telerik:RadButton ID="btnSearch" runat="server" Text="SEARCH" BorderStyle="None"
                                    BackColor="#00759A" Width="65px" Height="23px" Font-Bold="True" ForeColor="White"
                                    Font-Names="VERDANA" OnClientClick="return ValidateDates();" Font-Size="X-Small"
                                    Visible="true" OnClick="btnSearch_Click" EnableEmbeddedSkins="false" Skin="Default">
                                </telerik:RadButton>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr runat="server" id="hr_top" visible="false">
                <td colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                </td>
                <td align="center">
                    <div runat="server" id="divBasicSearch" visible="false" style="width: 100%;">
                        <table cellpadding="4" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:Label ID="lblBasicSearch" Text="Basic Search" runat="server" CssClass="labelStyle"
                                        Visible="false" Font-Size="14px" ForeColor="RosyBrown">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:Label ID="Label1" Text="Client name" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:TextBox runat="server" Width="495px" ID="txt_Tab_Client" CssClass="txtSingleStyle"
                                        Visible="true" MaxLength="200" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label16" Text="Client sector" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandCS" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Client sector drop down" OnClick="imgexpandCS_Click" />
                                                <asp:Label ID="lblCS" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidCS" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseCS" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="Right" Width="13px" Height="12px" ToolTip="Click to hide Client sector drop down"
                                                    OnClick="imgcollapseCS_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnCS" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="RadListBox1" TransferToID="RadListBox2"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        Height="200px" Width="245px" EnableEmbeddedSkins="false" Skin="RadListBox_Myskin"
                                                                        SelectionMode="Multiple">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="RadListBox2" TransferToID="RadListBox1" AllowTransferOnDoubleClick="true"
                                                                        OnClientTransferring="clientTransferringA" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        SelectionMode="Multiple" Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTCS" runat="server" Text="" TargetControlID="lblCS" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label18" Text="Client sub sector" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandCSS" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Client sub sector drop down"
                                                    OnClick="imgexpandCSS_Click" />
                                                <asp:Label ID="lblCSS" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidCSS" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseCSS" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Client sub sector drop down"
                                                    OnClick="imgcollapseCSS_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnCSS" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceCSS" TransferToID="radlstDestCSS"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestCSS" TransferToID="radlstSourceCSS"
                                                                        SelectionMode="Multiple" AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        Height="200px" Width="245px" EnableEmbeddedSkins="false" Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTCSS" runat="server" Text="" TargetControlID="lblCSS"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label19" Text="Matter sector" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandMS" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Matter sector drop down" OnClick="imgexpandMS_Click" />
                                                <asp:Label ID="lblMS" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidMS" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseMS" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Matter sector drop down"
                                                    OnClick="imgcollapseMS_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnMS" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceMS" TransferToID="radlstDestMS"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestMS" TransferToID="radlstSourceMS"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTMS" runat="server" Text="" TargetControlID="lblMS" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label29" Text="Matter sub sector" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandMSS" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Matter sub sector drop down"
                                                    OnClick="imgexpandMSS_Click" />
                                                <asp:Label ID="lblMSS" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidMSS" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseMSS" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Matter sub sector drop down"
                                                    OnClick="imgcollapseMSS_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnMSS" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceMSS" TransferToID="radlstDestMSS"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestMSS" TransferToID="radlstSourceMSS"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTMSS" runat="server" Text="" TargetControlID="lblMSS"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" valign="top">
                                    <asp:Label ID="Label13" Text="Work type(s)" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" valign="top">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td style="width: 497px;">
                                                <div style="border: 1px solid gray; overflow: scroll; overflow-x: hidden; width: 498px;
                                                    padding-left: 5px; background-color: #F0F8FF;">
                                                    <telerik:RadTreeView runat="server" ID="RadTreeView1" CheckBoxes="true" OnClientNodeChecked="clientNodeChecked"
                                                        ShowLineImages="true">
                                                        <CollapseAnimation Duration="100" Type="OutQuint" />
                                                        <ExpandAnimation Duration="100" Type="OutBounce" />
                                                    </telerik:RadTreeView>
                                                </div>
                                            </td>
                                            <td style="width: 495px;" valign="top">
                                                <span style="padding-right: 8px;"></span>
                                                <asp:Image runat="server" ID="img_WorkType" ImageUrl="~/Images/close.png" ToolTip="Click here to clear the selected values"
                                                    ImageAlign="Top" AlternateText="Clear" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" valign="top">
                                    <asp:Label ID="Label38" Text="Practice group fields" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" valign="top">
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td style="width: 497px;">
                                                <div style="border: 1px solid gray; overflow: scroll; overflow-x: hidden; width: 498px;
                                                    padding-left: 5px; background-color: #F0F8FF;">
                                                    <telerik:RadTreeView runat="server" ID="RadTreeView2" CheckBoxes="true" OnClientNodeChecked="clientNodeChecked"
                                                        ShowLineImages="true" OnNodeExpand="RadTreeView2_NodeExpand">
                                                        <CollapseAnimation Duration="100" Type="OutQuint" />
                                                        <ExpandAnimation Duration="100" Type="OutBounce" />
                                                    </telerik:RadTreeView>
                                                </div>
                                            </td>
                                            <td style="width: 495px;" valign="top">
                                                <span style="padding-right: 8px;"></span>
                                                <asp:Image runat="server" ID="img_Tab_Practice_Group" ImageUrl="~/Images/close.png"
                                                    ToolTip="Click here to clear the selected values" ImageAlign="Top" AlternateText="Clear" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" valign="top">
                                    <asp:Label ID="Label4" Text="Date matter completed" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" valign="top">
                                    <asp:Label ID="Label6" Text="From : " runat="server" CssClass="labelStyleSearch"></asp:Label>
                                    <telerik:RadDatePicker Width="130px" EnableEmbeddedSkins="false" Skin="Myskin" Font-Size="11px"
                                        DateInput-Font-Size="11px" ShowPopupOnFocus="true" ID="cld_Tab_Date_Opened" runat="server"
                                        Culture="en-GB" DateInput-DisplayDateFormat="dd/MM/yyyy" Calendar-FastNavigationStep="12"
                                        DateInput-ReadOnly="true" DateInput-DateFormat="dd/MM/yyyy" DateInput-EmptyMessage=""
                                        DateInput-Height="30px">
                                        <DateInput CssClass="txtSingleStyle " Width="130px" runat="server" Enabled="false">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <asp:Label ID="Label5" Text="To : " runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                    <telerik:RadDatePicker Width="130px" EnableEmbeddedSkins="false" Skin="Myskin" Font-Size="11px"
                                        DateInput-Font-Size="11px" ShowPopupOnFocus="true" ID="cld_Tab_Date_Opened1"
                                        runat="server" Culture="en-GB" DateInput-DisplayDateFormat="dd/MM/yyyy" Calendar-FastNavigationStep="12"
                                        DateInput-ReadOnly="true" DateInput-DateFormat="dd/MM/yyyy" DateInput-Height="30px">
                                        <DateInput ID="DateInput1" CssClass="txtSingleStyle" Width="130px" runat="server"
                                            Enabled="false">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <span style="padding-right: 7px;"></span>
                                    <asp:CheckBox ID="chk_Tab_ActualDate_Ongoing" runat="server" Text="Ongoing" />
                                    <span style="padding-right: 7px;"></span>
                                    <asp:CheckBox ID="chk_Tab_ActualDate_NotKnown" runat="server" Text="Not Known" />
                                    <span style="padding-right: 5px;"></span>
                                    <asp:Image runat="server" ID="img_Date" ImageUrl="~/Images/close.png" ToolTip="Click here to clear the selected values"
                                        ImageAlign="Top" AlternateText="Clear" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label48" Text="Matter location(s)" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandML" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Matter location(s) drop down"
                                                    OnClick="imgexpandML_Click" />
                                                <asp:Label ID="lblML" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidML" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseML" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Matter location(s) drop down"
                                                    OnClick="imgcollapseML_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnML" CssClass="panelStylehidden">
                                                    <table cellpadding="1" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 245px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceML" TransferToID="radlstDestML"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 245px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestML" TransferToID="radlstSourceML"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTML" runat="server" Text="" TargetControlID="lblML" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label49" Text="Team(s)" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandTeams" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Team(s) drop down"
                                                    OnClick="imgexpandTeams_Click" />
                                                <asp:Label ID="lblTeams" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidTeams" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseTeams" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Team(s) drop down"
                                                    OnClick="imgcollapseTeams_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnTeams" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceTeams" TransferToID="radlstDestTeams"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestTeams" TransferToID="radlstSourceTeams"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTTeams" runat="server" Text="" TargetControlID="lblTeams"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label50" Text="Lead partner(s)" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandLP" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Lead partner(s) drop down"
                                                    OnClick="imgexpandLP_Click" />
                                                <asp:Label ID="lblLP" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidLP" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseLP" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Lead partner(s) drop down"
                                                    OnClick="imgcollapseLP_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnLP" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceLP" TransferToID="radlstDestLP"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestLP" TransferToID="radlstSourceLP"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTLP" runat="server" Text="" TargetControlID="lblLP" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label51" Text="Matter executive(s)" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandME" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Matter executive(s) drop down"
                                                    OnClick="imgexpandME_Click" />
                                                <asp:Label ID="lblME" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidME" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseME" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Matter executive(s) drop down"
                                                    OnClick="imgcollapseME_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnME" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceME" TransferToID="radlstDestME"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestME" TransferToID="radlstSourceME"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTME" runat="server" Text="" TargetControlID="lblME" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:Label ID="Label10" Text="Priority" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <telerik:RadComboBox CheckedItemsTexts="FitInInput" BackColor="#F0F8FF" OnClientItemChecked="ChangeColor"
                                        ID="rad_Tab_Priority" runat="server" CheckBoxes="true" EmptyMessage="Select Priority"
                                        AllowCustomText="false" MarkFirstMatch="true" EnableCheckAllItemsCheckBox="false"
                                        Height="50px" Width="505px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <telerik:RadButton ID="Button1" runat="server" Text="SEARCH" OnClientClick="return ValidateDates();"
                                        OnClick="btnSearch_Click" EnableEmbeddedSkins="False" Skin="Default" BackColor="#00759A"
                                        ForeColor="White" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr runat="server" id="tr_bottom" visible="false">
                <td colspan="3">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                </td>
                <td align="center">
                    <div runat="server" id="divAdvancedSearch" visible="false" style="width: 100%;">
                        <table cellpadding="4" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td colspan="2" align="left">
                                    <asp:Label ID="lblAdvancedSearch" Text="Advanced Search" runat="server" CssClass="labelStyleSearch"
                                        Font-Size="14px" ForeColor="RosyBrown" Visible="false">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label21" Text="Project name" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TextBox runat="server" Width="500px" ID="txt_Tab_ProjectName_Core" CssClass="txtSingleStyle"
                                        Visible="true" MaxLength="50" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label2" Text="Confidential client generic description" runat="server"
                                        CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TextBox runat="server" Width="500px" ID="txt_tab_ClientDescription" CssClass="txtSingleStyle"
                                        Visible="true" MaxLength="20" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label11" Text="Value of deal(greater than or equal to)" runat="server"
                                        CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TextBox runat="server" Width="500px" ID="txt_tab_Value_Deal" CssClass="txtSingleStyle"
                                        Visible="true" MaxLength="20" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label53" Text="Currency of deal" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandCD" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Currency of deal drop down"
                                                    OnClick="imgexpandCD_Click" />
                                                <asp:Label ID="lblCD" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidCD" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseCD" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Currency of deal drop down"
                                                    OnClick="imgcollapseCD_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnCD" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceCD" TransferToID="radlstDestCD"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestCD" TransferToID="radlstSourceCD"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTCD" runat="server" Text="" TargetControlID="lblCD" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label54" Text="Contentious" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandContentious" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Contentious drop down"
                                                    OnClick="imgexpandContentious_Click" />
                                                <asp:Label ID="lblContentious" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidContentious" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseContentious" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Contentious drop down"
                                                    OnClick="imgcollapseContentious_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnContentious" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceContentious"
                                                                        SelectionMode="Multiple" TransferToID="radlstDestContentious" AllowTransferOnDoubleClick="true"
                                                                        OnClientTransferring="clientTransferringB" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestContentious" TransferToID="radlstSourceContentious"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTContentious" runat="server" Text="" TargetControlID="lblCD"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label55" Text="Dispute resolution" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandDR" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Dispute resolution drop down"
                                                    OnClick="imgexpandDR_Click" />
                                                <asp:Label ID="lblDR" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidDR" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseDR" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Dispute resolution drop down"
                                                    OnClick="imgcollapseDR_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnDR" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceDR" TransferToID="radlstDestDR"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestDR" TransferToID="radlstSourceDR"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTDR" runat="server" Text="" TargetControlID="lblDR" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label56" Text="Country of arbitration" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandCOA" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Country of arbitration drop down"
                                                    OnClick="imgexpandCOA_Click" />
                                                <asp:Label ID="lblCOA" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidCOA" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseCOA" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Country of arbitration drop down"
                                                    OnClick="imgcollapseCOA_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnCOA" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceCOA" TransferToID="radlstDestCOA"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestCOA" TransferToID="radlstSourceCOA"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTCOA" runat="server" Text="" TargetControlID="lblCOA"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label57" Text="Seat of arbitration" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandSOA" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Seat of arbitration drop down"
                                                    OnClick="imgexpandSOA_Click" />
                                                <asp:Label ID="lblSOA" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidSOA" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseSOA" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Seat of arbitration drop down"
                                                    OnClick="imgcollapseSOA_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnSOA" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceSOA" TransferToID="radlstDestSOA"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestSOA" TransferToID="radlstSourceSOA"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTSOA" runat="server" Text="" TargetControlID="lblSOA"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label58" Text="Arbitral rules" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandAR" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Arbitral rules drop down" OnClick="imgexpandAR_Click" />
                                                <asp:Label ID="lblAR" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidAR" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseAR" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Arbitral rules drop down"
                                                    OnClick="imgcollapseAR_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnAR" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceAR" TransferToID="radlstDestAR"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestAR" TransferToID="radlstSourceAR"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTAR" runat="server" Text="" TargetControlID="lblAR" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label36" Text="Investment treaty" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <telerik:RadComboBox CheckedItemsTexts="FitInInput" BackColor="#F0F8FF" OnClientItemChecked="ChangeColor"
                                        ID="rad_Tab_InvestmentTreaty" runat="server" CheckBoxes="true" EmptyMessage="Select Investment treaty"
                                        AllowCustomText="false" MarkFirstMatch="true" EnableCheckAllItemsCheckBox="false"
                                        Height="50px" Width="505px">
                                    </telerik:RadComboBox>
                                    <span style="padding-right: 5px;"></span>
                                    <%-- <asp:Image runat="server" ID="img_Tab_InvestmentTreaty" ImageUrl="~/Images/close.png"
                                        ToolTip="Click here to clear the selected values" ImageAlign="Top" AlternateText="Clear" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label60" Text="Investigation type" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandIVT" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Investigation type drop down"
                                                    OnClick="imgexpandIVT_Click" />
                                                <asp:Label ID="lblIVT" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidIVT" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseIVT" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Investigation type drop down"
                                                    OnClick="imgcollapseIVT_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnIVT" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceIVT" TransferToID="radlstDestIVT"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestIVT" TransferToID="radlstSourceIVT"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTIVT" runat="server" Text="" TargetControlID="lblIVT"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label61" Text="Language of dispute" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandLOD" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Language of dispute drop down"
                                                    OnClick="imgexpandLOD_Click" />
                                                <asp:Label ID="lblLOD" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidLOD" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseLOD" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Language of dispute drop down"
                                                    OnClick="imgcollapseLOD_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnLOD" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceLOD" TransferToID="radlstDestLOD"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestLOD" TransferToID="radlstSourceLOD"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTLOD" runat="server" Text="" TargetControlID="lblLOD"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label62" Text="Jurisidiction of dispute" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandJOD" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Jurisidiction of dispute drop down"
                                                    OnClick="imgexpandJOD_Click" />
                                                <asp:Label ID="lblJOD" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidJOD" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseJOD" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Jurisidiction of dispute drop down"
                                                    OnClick="imgcollapseJOD_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnJOD" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceJOD" TransferToID="radlstDestJOD"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestJOD" TransferToID="radlstSourceJOD"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTJOD" runat="server" Text="" TargetControlID="lblJOD"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label63" Text="CMS firms involved" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandCFI" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show CMS firms involved drop down"
                                                    OnClick="imgexpandCFI_Click" />
                                                <asp:Label ID="lblCFI" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidCFI" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseCFI" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide CMS firms involved drop down"
                                                    OnClick="imgcollapseCFI_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnCFI" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceCFI" TransferToID="radlstDestCFI"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestCFI" TransferToID="radlstSourceCFI"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTCFI" runat="server" Text="" TargetControlID="lblCFI"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label64" Text="Lead CMS firm" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandLCF" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Lead CMS firm drop down"
                                                    OnClick="imgexpandLCF_Click" />
                                                <asp:Label ID="lblLCF" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidLCF" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseLCF" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Lead CMS firm drop down"
                                                    OnClick="imgcollapseLCF_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnLCF" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceLCF" TransferToID="radlstDestLCF"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestLCF" TransferToID="radlstSourceLCF"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTLCF" runat="server" Text="" TargetControlID="lblLCF"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label65" Text="Countries of other CMS firms" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandCCF" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Countries of other CMS firms drop down"
                                                    OnClick="imgexpandCCF_Click" />
                                                <asp:Label ID="lblCCF" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidCCF" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseCCF" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Countries of other CMS firms drop down"
                                                    OnClick="imgcollapseCCF_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnCCF" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceCCF" TransferToID="radlstDestCCF"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestCCF" TransferToID="radlstSourceCCF"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTCCF" runat="server" Text="" TargetControlID="lblCCF"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label66" Text="Other uses" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandOU" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Other uses drop down" OnClick="imgexpandOU_Click" />
                                                <asp:Label ID="lblOU" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidOU" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseOU" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Other uses drop down"
                                                    OnClick="imgcollapseOU_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnOU" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceOU" TransferToID="radlstDestOU"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestOU" TransferToID="radlstSourceOU"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTOU" runat="server" Text="" TargetControlID="lblOU" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label42" Text="Credential status" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <telerik:RadComboBox CheckedItemsTexts="FitInInput" BackColor="#F0F8FF" OnClientItemChecked="ChangeColor"
                                        ID="cbo_Tab_Credential_Status" runat="server" CheckBoxes="true" EmptyMessage="Select Credential status"
                                        AllowCustomText="false" MarkFirstMatch="true" EnableCheckAllItemsCheckBox="false"
                                        Height="50px" Width="505px">
                                    </telerik:RadComboBox>
                                    <span style="padding-right: 5px;"></span>
                                    <%--<asp:Image runat="server" ID="img_Tab_Credential_Status" ImageUrl="~/Images/close.png"
                                        ToolTip="Click here to clear the selected values" ImageAlign="Top" AlternateText="Clear" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label43" Text="Credential version" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <telerik:RadComboBox CheckedItemsTexts="FitInInput" BackColor="#F0F8FF" OnClientItemChecked="ChangeColor"
                                        ID="cbo_Tab_Credential_Version" runat="server" CheckBoxes="true" EmptyMessage="Select Credential version"
                                        AllowCustomText="false" MarkFirstMatch="true" EnableCheckAllItemsCheckBox="false"
                                        Height="50px" Width="505px">
                                    </telerik:RadComboBox>
                                    <span style="padding-right: 5px;"></span>
                                    <%--<asp:Image runat="server" ID="img_Tab_Credential_Version" ImageUrl="~/Images/close.png"
                                        ToolTip="Click here to clear the selected values" ImageAlign="Top" AlternateText="Clear" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label44" Text="Credential type" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <telerik:RadComboBox CheckedItemsTexts="FitInInput" BackColor="#F0F8FF" OnClientItemChecked="ChangeColor"
                                        ID="cbo_Tab_Credential_Type" runat="server" CheckBoxes="true" EmptyMessage="Select Credential type"
                                        AllowCustomText="false" MarkFirstMatch="true" EnableCheckAllItemsCheckBox="false"
                                        Height="50px" Width="505px">
                                    </telerik:RadComboBox>
                                    <span style="padding-right: 5px;"></span>
                                    <%--<asp:Image runat="server" ID="img_Tab_Credential_Type" ImageUrl="~/Images/close.png"
                                        ToolTip="Click here to clear the selected values" ImageAlign="Top" AlternateText="Clear" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label67" Text="Applicable law" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandAL" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Applicable law drop down" OnClick="imgexpandAL_Click" />
                                                <asp:Label ID="lblAL" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidAL" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseAL" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Applicable lawdrop down"
                                                    OnClick="imgcollapseAL_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnAL" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceAL" TransferToID="radlstDestAL"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestAL" TransferToID="radlstSourceAL"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTAL" runat="server" Text="" TargetControlID="lblAL" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label68" Text="Country where matter opened" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandCMO" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Country where matter opened drop down"
                                                    OnClick="imgexpandCMO_Click" />
                                                <asp:Label ID="lblCMO" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidCMO" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseCMO" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Country where matter opened drop down"
                                                    OnClick="imgcollapseCMO_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnCMO" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceCMO" TransferToID="radlstDestCMO"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestCMO" TransferToID="radlstSourceCMO"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTCMO" runat="server" Text="" TargetControlID="lblCMO"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label69" Text="Predominant country of client" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandPCC" ImageUrl="~/Images/expand.jpg"
                                                    ImageAlign="AbsMiddle" Width="13px" Height="12px" ToolTip="Click to show Predominant country of client drop down"
                                                    OnClick="imgexpandPCC_Click" />
                                                <asp:Label ID="lblPCC" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidPCC" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px; padding-right: 3px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapsePCC" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Predominant country of client drop down"
                                                    OnClick="imgcollapsePCC_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnPCC" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourcePCC" TransferToID="radlstDestPCC"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestPCC" TransferToID="radlstSourcePCC"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTPCC" runat="server" Text="" TargetControlID="lblPCC"
                                                    Visible="false" ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label45" Text="Pro bono" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <telerik:RadComboBox CheckedItemsTexts="FitInInput" BackColor="#F0F8FF" OnClientItemChecked="ChangeColor"
                                        ID="cbo_Tab_ProBono" runat="server" CheckBoxes="true" EmptyMessage="Select Pro bono"
                                        AllowCustomText="false" MarkFirstMatch="true" EnableCheckAllItemsCheckBox="false"
                                        Height="50px" Width="505px">
                                    </telerik:RadComboBox>
                                    <span style="padding-right: 5px;"></span>
                                    <%--<asp:Image runat="server" ID="img_Tab_ProBono" ImageUrl="~/Images/close.png" ToolTip="Click here to clear the selected values"
                                        ImageAlign="Top" AlternateText="Clear" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left">
                                    <table cellpadding="0" cellspacing="0" width="500px" border="0">
                                        <tr>
                                            <td style="width: 245px;" align="left">
                                                <asp:Label ID="Label70" Text="Know how" runat="server" CssClass="labelStyleSearch">
                                                </asp:Label><span style="padding-left: 5px;"></span>
                                                <asp:ImageButton runat="server" ID="imgexpandKH" ImageUrl="~/Images/expand.jpg" ImageAlign="AbsMiddle"
                                                    Width="13px" Height="12px" ToolTip="Click to show Know how drop down" OnClick="imgexpandKH_Click" />
                                                <asp:Label ID="lblKH" Text="Values selected..." runat="server" Visible="false"></asp:Label>
                                                <asp:HiddenField ID="hidKH" Value="0" runat="server" />
                                            </td>
                                            <td style="width: 245px;" align="right">
                                                <asp:ImageButton runat="server" ID="imgcollapseKH" ImageUrl="~/Images/collapse.jpg"
                                                    ImageAlign="right" Width="13px" Height="12px" ToolTip="Click to hide Know how drop down"
                                                    OnClick="imgcollapseKH_Click" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Panel runat="server" ID="plnKH" CssClass="panelStylehidden">
                                                    <table cellpadding="0" cellspacing="0" width="500px" border="0" style="padding-top: 8px;
                                                        padding-left: 2px;">
                                                        <tr>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" BackColor="#F0F8FF" ID="radlstSourceKH" TransferToID="radlstDestKH"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringB"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                            <td style="width: 250px;">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 200px;
                                                                    width: 245px; overflow-y: hidden;">
                                                                    <telerik:RadListBox runat="server" ID="radlstDestKH" TransferToID="radlstSourceKH"
                                                                        AllowTransferOnDoubleClick="true" OnClientTransferring="clientTransferringA"
                                                                        SelectionMode="Multiple" Height="200px" Width="245px" EnableEmbeddedSkins="false"
                                                                        Skin="RadListBox_Myskin">
                                                                    </telerik:RadListBox>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <telerik:RadToolTip ID="radTTKH" runat="server" Text="" TargetControlID="lblKH" Visible="false"
                                                    ForeColor="Red" Width="400px" ManualCloseButtonText="Close" ManualClose="false">
                                                </telerik:RadToolTip>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label28" Text="Bible reference" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TextBox runat="server" Width="500px" ID="txt_Tab_Bible_Reference" CssClass="txtSingleStyle"
                                        Visible="true" MaxLength="50" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label46" Text="Matter number" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TextBox runat="server" Width="500px" ID="txt_Tab_Matter_No" CssClass="txtSingleStyle"
                                        Visible="true" MaxLength="50" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_txt_Tab_Credential_ID" visible="false">
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label47" Text="Credential ID" runat="server" CssClass="labelStyleSearch">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:TextBox runat="server" Width="500px" ID="txt_Tab_Credential_ID" CssClass="txtSingleStyle"
                                        Visible="true" MaxLength="50" TextMode="SingleLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <telerik:RadButton ID="RadButton1" runat="server" Text="SEARCH" BorderStyle="None"
                                        BackColor="#00759A" Width="65px" Height="23px" Font-Bold="True" ForeColor="White"
                                        Font-Names="VERDANA" OnClientClick="return ValidateDates();" Font-Size="X-Small"
                                        Skin="Default" Visible="true" OnClick="btnSearch_Click" EnableEmbeddedSkins="false">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                </td>
            </tr>
            <tr runat="server" id="tr_hrline" visible="false">
                <td colspan="3">
                    <hr />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
