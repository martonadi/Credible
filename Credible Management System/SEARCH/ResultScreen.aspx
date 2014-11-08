<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    ValidateRequest="false" EnableViewState="true" CodeBehind="ResultScreen.aspx.cs"
    Inherits="CredentialsDemo.SEARCH.ResultScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ OutputCache Location="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <telerik:RadCodeBlock ID="id1" runat="server">
        <script type="text/javascript">
            var ModalProgress = '<%= ModalProgress.ClientID %>';

            function HideModalPopup() {
                var pop = $find('EditEntryPopup');
                pop.hide();
            }

            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
        <script type="text/javascript">
            var RadGrid1;
            var DataItems;
            function gridCreated(sender, args) {
                RadGrid1 = sender;
            }
            function UnCheckRadio(rdo, flg) {
                var chk = false;
                var chkAll = rdo.getElementsByTagName("input");
                if (chkAll != null) {
                    for (var i = 0; i < chkAll.length; i++) {
                        if (chkAll[i].checked == true) {
                            chkAll[i].checked = false;
                        }
                        if (flg == "1") {
                            chkAll[0].checked = true;
                        }
                    }
                }
            }

            function OnClientItemCheckedHandler(sender, eventArgs) {
                var hidlstColumns = document.getElementById('<%= hidlstColumns.ClientID%>');
                var item = eventArgs.get_item();
                if (item != null) {
                    item.set_selected(item.get_checked());
                    if (item.get_checked() == true) {
                        if (hidlstColumns != null) {
                            if (hidlstColumns.value.length == 0) {
                                hidlstColumns.value = item.get_value();
                            }
                            else {
                                hidlstColumns.value = hidlstColumns.value + "," + item.get_value();
                            }
                        }
                    }
                }
            }


            function MenuShowing(sender, eventArgs) {
                /* var menu = eventArgs.get_menu();
                var items = menu.get_items();*/
                var menu = sender; var items = menu.get_items();

                if (column._element.UniqueName != "DateCompleted") {
                    var i = 0;
                    while (i < items.get_count()) {
                        if (items.getItem(i).get_value() == "NoFilter" || items.getItem(i).get_value() == "Contains"
                        || items.getItem(i).get_value() == "StartsWith" || items.getItem(i).get_value() == "EqualTo") {
                            var item = items.getItem(i);
                            if (item != null) {
                                item.set_visible(true);
                            }
                        }
                        else {
                            var item = items.getItem(i);
                            if (item != null)
                                item.set_visible(false);
                        }
                        i++;
                    }
                }


                if (column.get_dataType() == "System.DateTime") {
                    var i = 0;
                    while (i < items.get_count()) {
                        if (items.getItem(i).get_value() == "NoFilter"
                        || items.getItem(i).get_value() == "GreaterThan"
                        || items.getItem(i).get_value() == "LessThan"
                        || items.getItem(i).get_value() == "EqualTo"
                        || items.getItem(i).get_value() == "NotEqualTo"
                        || items.getItem(i).get_value() == "IsNull") {
                            var item = items.getItem(i);
                            if (item != null) {
                                item.set_visible(true);
                            }
                        }
                        else {
                            var item = items.getItem(i);
                            if (item != null)
                                item.set_visible(false);
                        }
                        i++;
                    }
                }

                column = null;
                menu.repaint();
            }

            function filterMenuShowing(sender, eventArgs) {
                column = eventArgs.get_column();
            }


            function showFilterItem() {
                $find('<%=RadGrid1.ClientID %>').get_masterTableView().showFilterItem();
            }

            function hideFilterItem() {
                $find('<%=RadGrid1.ClientID %>').get_masterTableView().hideFilterItem();
            }

            function CheckItem(itemCheckBox) {
                var masterTableView = RadGrid1.get_masterTableView();
                if (DataItems == null) {
                    DataItems = masterTableView.get_dataItems();
                }
                var row = itemCheckBox.parentNode.parentNode;
                if (row.tagName === "TR" && row.id != "") {
                    var item = $find(row.id);
                    if (!item.get_selected() && itemCheckBox.checked) {
                        masterTableView.clearSelectedItems();
                        item.set_selected(true);
                    }
                    else {
                        //var chk = document.getElementById('<//%=headerChkbox.ClientID%>');
                        //var gridItemElement = masterTable.get_dataItems()[i].findElement("checkTag");
                        var checkboxes = RadGrid1.get_masterTableView().get_element().getElementsByTagName("INPUT");
                        if (typeof (checkboxes[0].checked) !== "undefined") {
                            checkboxes[0].checked = false;
                        }
                    }
                }
            }


            function CheckAll(headerCheckBox) {
                var isChecked = headerCheckBox.checked;
                var checkboxes = RadGrid1.get_masterTableView().get_element().getElementsByTagName("INPUT");
                var index;
                for (index = 0; index < checkboxes.length; index++) {
                    if (typeof (checkboxes[index].checked) !== "undefined") {
                        if (isChecked) {
                            checkboxes[index].checked = true;
                        }
                        else {
                            checkboxes[index].checked = false;
                        }
                    }
                }
            }

            function CheckRadioValidation(rdo) {
                var chk = false;
                var chkAll = rdo.getElementsByTagName("input");
                if (chkAll != null) {
                    for (var i = 0; i < chkAll.length; i++) {
                        if (chkAll[i].checked == true) {
                            chk = true;
                            break;
                        }
                    }
                    if (chk == false) {
                        return chk;
                    }
                    else {
                        return chk;
                    }
                }
            }

            function SetDefault() {

                var rdoReportStyle = document.getElementById('<%= rdoReportStyle.ClientID%>');

                var divclientname = document.getElementById('<%= divclientname.ClientID%>');
                if (divclientname != null) {
                    divclientname.style.backgroundColor = "#FFFFFF";
                    divclientname.disabled = false;
                }
                var divprojectname = document.getElementById('<%= divprojectname.ClientID%>');
                if (divprojectname != null) {
                    divprojectname.style.backgroundColor = "#FFFFFF";
                    divprojectname.disabled = false;
                }

                var lstColumns = document.getElementById('<%= lstColumns.ClientID%>');
                if (lstColumns != null) {
                    lstColumns.style.display = 'none';
                    var lst = $find(document.getElementById('<%= lstColumns.ClientID%>').id);
                    for (var i = 0; i < lst.get_items().get_count(); i++) {
                        lst.get_items()._array[i].set_checked(false);
                    }
                }

                var dv = document.getElementById('<%= divlstColumns.ClientID%>');
                if (dv != null) {
                    dv.style.display = 'none';
                }

                var pln = document.getElementById('<%= Panel1.ClientID%>');
                if (pln != null) {
                    pln.style.width = "250px";
                }

                var lblClientName = document.getElementById('<%= lblClientName.ClientID%>');
                var rdoExportConfidential = document.getElementById('<%= rdoExportConfidential.ClientID%>');
                var lblProjectName = document.getElementById('<%= lblProjectName.ClientID%>');
                var rdoProjectName = document.getElementById('<%= rdoProjectName.ClientID%>');

                lblClientName.disabled = false;
                rdoExportConfidential.disabled = false;
                lblProjectName.disabled = false;
                rdoProjectName.disabled = false;

                var chkAll = rdoReportStyle.getElementsByTagName("input");
                if (chkAll != null) {
                    for (var i = 0; i < chkAll.length; i++) {
                        if (chkAll[i].checked == true && chkAll[i].value == "Search") {
                            lstColumns.style.display = 'block';
                            dv.style.display = 'block';
                            pln.style.width = "530px";
                            /*lblClientName.disabled = false;
                            rdoExportConfidential.disabled = false;
                            lblProjectName.disabled = false;
                            rdoProjectName.disabled = false;*/
                        }
                        else if (chkAll[i].checked == true && chkAll[i].value == "League") {
                            lblClientName.disabled = true;
                            rdoExportConfidential.disabled = true;
                            lblProjectName.disabled = true;
                            rdoProjectName.disabled = true;
                            divprojectname.style.backgroundColor = "#ededed";
                            divclientname.style.backgroundColor = "#ededed";
                            CheckRadioValidation1(rdoProjectName, "None");
                            CheckRadioValidation1(rdoExportConfidential, "None");
                        }
                    }
                }
            }

            function validation() {
                var rdoExportList = document.getElementById('<%= rdoExportList.ClientID%>');
                if (CheckRadioValidation(rdoExportList) == false) {
                    alert("Please select the export option");
                    return false;
                }
                else {
                    var pop = $find('EditEntryPopup');
                    if (pop != null) {

                        var rdoExportConfidential = document.getElementById('<%= rdoExportConfidential.ClientID%>');
                        var rdoReportStyle = document.getElementById('<%= rdoReportStyle.ClientID%>');
                        var rdoProjectName = document.getElementById('<%= rdoProjectName.ClientID%>');
                        UnCheckRadio(rdoExportConfidential, "0");
                        UnCheckRadio(rdoReportStyle, "1");
                        UnCheckRadio(rdoProjectName, "0");

                        /*var divclientname = document.getElementById('<%= divclientname.ClientID%>');
                        if (divclientname != null) {
                        divclientname.style.backgroundColor = "#F5F5DC";
                        divclientname.disabled = true;
                        }
                        var divprojectname = document.getElementById('<%= divprojectname.ClientID%>');
                        if (divprojectname != null) {
                        divprojectname.style.backgroundColor = "#F5F5DC";
                        divprojectname.disabled = true;
                        }*/

                        var pln = document.getElementById('<%= Panel1.ClientID%>');
                        if (pln != null) {
                            pln.style.width = "250px";
                        }

                        var dv = document.getElementById('<%= divlstColumns.ClientID%>');
                        if (dv != null) {
                            dv.style.display = 'none';
                        }

                        var lstColumns = document.getElementById('<%= lstColumns.ClientID%>');
                        if (lstColumns != null) {
                            lstColumns.style.display = 'none';
                            var lst = $find(document.getElementById('<%= lstColumns.ClientID%>').id);
                            for (var i = 0; i < lst.get_items().get_count(); i++) {
                                lst.get_items()._array[i].set_checked(false);
                            }
                        }

                        pop.TargetControlID = document.getElementById('<%= btnExport.ClientID%>').id;
                        pop.show();
                        return false;
                    }
                }
            }
            function validationReportStyle() {
                var rdoExportConfidential = document.getElementById('<%= rdoExportConfidential.ClientID%>');
                var rdoReportStyle = document.getElementById('<%= rdoReportStyle.ClientID%>');
                var rdoProjectName = document.getElementById('<%= rdoProjectName.ClientID%>');

                if (CheckRadioValidation(rdoReportStyle) == false) {
                    alert("Please select the report style");
                    return false;
                }
                else {
                    var chkAll = rdoReportStyle.getElementsByTagName("input");
                    var chkflg = true;
                    var srchFlag = true;

                    if (chkAll != null) {
                        for (var i = 0; i < chkAll.length; i++) {
                            if (chkAll[i].checked == true && chkAll[i].value != "League") {
                                if (CheckRadioValidation(rdoExportConfidential) == false) {
                                    alert("Please select the confidential option");
                                    chkflg = false;
                                    break;
                                }
                                if (CheckRadioValidation(rdoProjectName) == false) {
                                    alert("Please select the project name");
                                    chkflg = false;
                                    break;
                                }
                            }
                            if (chkAll[i].checked == true && chkAll[i].value == "Search") {
                                srchFlag = false;
                            }

                        }
                        if (chkflg == true) {

                            var hidSheetCount = document.getElementById('<%= hidSheetCount.ClientID%>');
                            if (hidSheetCount != null && hidSheetCount.value.length > 0) {
                                if (hidSheetCount.value == "0") {
                                    hidSheetCount.value = "1";
                                }
                                else {
                                    hidSheetCount.value = parseInt(parseInt(hidSheetCount.value) + parseInt(1));
                                }
                            }

                            if (srchFlag == false) {
                                var lstColumns = $find('<%= lstColumns.ClientID%>');
                                var hidlstColumns = document.getElementById('<%= hidlstColumns.ClientID%>');
                                hidlstColumns.value = "";
                                if (lstColumns != null) {
                                    var checkitemslength = lstColumns.get_checkedItems().length;
                                    if (checkitemslength > 0) {
                                        var checkitems = lstColumns.get_checkedItems();
                                        for (var i = 0; i < checkitemslength; i++) {
                                            if (checkitems != null) {
                                                if (hidlstColumns != null) {
                                                    if (hidlstColumns.value.length == 0) {
                                                        hidlstColumns.value = checkitems[i].get_value();
                                                    }
                                                    else {
                                                        hidlstColumns.value = hidlstColumns.value + "," + checkitems[i].get_value();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else {
                                        alert('Please select a column name to export !!!');
                                        return false;
                                    }
                                }
                            }

                            return true;
                        }
                        else if (chkflg == false) {
                            return false;
                        }
                    }
                }
            }

            function CheckRadioValidation1(rdo, txt) {
                var chk = false;
                var chkAll = rdo.getElementsByTagName("input");
                if (chkAll != null) {
                    for (var i = 0; i < chkAll.length; i++) {
                        if (chkAll[i].value == txt) {
                            chkAll[i].checked = true;
                        }
                        else {
                            chkAll[i].checked = false;
                        }
                    }
                }
            }

            function SelectGridValues() {

                var hidSelectedIDs = document.getElementById('<%= hidSelectedIDs.ClientID%>');
                var hidAllIDs = document.getElementById('<%= hidAllIDs.ClientID%>');
                var hidMasterIDs = document.getElementById('<%= hidMasterIDs.ClientID%>');
                var rdoExportList = document.getElementById('<%= rdoExportList.ClientID%>');
                var hidSelectedValue = document.getElementById('<%= hidSelectedValue.ClientID%>');

                if (hidSelectedValue != null) {
                    hidSelectedValue.value = "0";
                }

                var chkAll = rdoExportList.getElementsByTagName("input");
                if (chkAll != null) {
                    for (var i = 0; i < chkAll.length; i++) {
                        if (chkAll[i].checked == true && chkAll[i].value == "All") {
                            var RadGrid1 = $find("<%=RadGrid1.ClientID %>");
                            var checkboxes = RadGrid1.get_masterTableView().get_element().getElementsByTagName("INPUT");
                            var index;
                            for (index = 0; index < checkboxes.length; index++) {
                                if (typeof (checkboxes[index].checked) !== "undefined") {
                                    checkboxes[index].checked = true;
                                }
                            }
                            hidSelectedIDs.value = "";
                            hidSelectedIDs.value = hidAllIDs.value;
                        }
                        else if (chkAll[i].checked == true && chkAll[i].value == "Master") {

                            var grid = $find("<%=RadGrid1.ClientID %>");

                            var checkboxes = grid.get_masterTableView().get_element().getElementsByTagName("INPUT");
                            var index;
                            for (index = 0; index < checkboxes.length; index++) {
                                if (typeof (checkboxes[index].checked) !== "undefined") {
                                    if (checkboxes[index].name.indexOf("chkMasterHead") != "-1") {
                                        checkboxes[index].checked = false;
                                    }
                                }
                            }

                            var dataItems = grid.get_masterTableView().get_dataItems();
                            for (var i = 0; i < dataItems.length; i++) {
                                if (dataItems[i].get_nestedViews().length > 0) {
                                    var nestedView = dataItems[i].get_nestedViews()[0];
                                    //here you can access the nested table's data items using nestedView.get_dataItems()
                                    var chk = nestedView.get_dataItems()[0].findElement("chkChildChild");
                                    if (chk != null) {
                                        chk.checked = false;
                                    }
                                }
                            }

                            var MasterTable = grid.get_masterTableView();
                            var length = MasterTable.get_dataItems().length;
                            for (var i = 0; i < length; i++) {

                                var keyvalue = MasterTable.get_dataItems()[i].getDataKeyValue("CredentialID");
                                var chk = MasterTable.get_dataItems()[i].findElement("chkMasterChild");

                                if (hidMasterIDs.value.length > 0) {
                                    if (hidMasterIDs.value.indexOf(keyvalue) != -1) {
                                        if (chk != null) {
                                            chk.checked = true;
                                        }
                                    }
                                    else {
                                        if (chk != null) {
                                            chk.checked = false;
                                        }
                                    }
                                }
                            }

                            hidSelectedIDs.value = "";
                            hidSelectedIDs.value = hidMasterIDs.value;
                        }
                    }
                }

            }

            function RowSelectedMaster(chk) {
                var hidSelectedIDs = document.getElementById('<%= hidSelectedIDs.ClientID%>');
                var hidAllIDs = document.getElementById('<%= hidAllIDs.ClientID%>');
                //hidSelectedValue
                var rdoExportList = document.getElementById('<%= rdoExportList.ClientID%>');
                var hidSelectedValue = document.getElementById('<%= hidSelectedValue.ClientID%>');


                var RadGrid1 = $find("<%=RadGrid1.ClientID %>");
                var isChecked = chk.checked;
                if (isChecked == true) {
                    CheckRadioValidation1(rdoExportList, "All");
                    var checkboxes = RadGrid1.get_masterTableView().get_element().getElementsByTagName("INPUT");
                    var index;
                    for (index = 0; index < checkboxes.length; index++) {
                        if (typeof (checkboxes[index].checked) !== "undefined") {
                            if (isChecked) {
                                checkboxes[index].checked = true;
                            }
                            else {
                                checkboxes[index].checked = false;
                            }
                        }
                    }
                    if (hidAllIDs != null && hidSelectedIDs != null) {
                        hidSelectedIDs.value = hidAllIDs.value;
                    }
                }
                else {
                    CheckRadioValidation1(rdoExportList, "None");
                    var checkboxes = RadGrid1.get_masterTableView().get_element().getElementsByTagName("INPUT");
                    var index;
                    for (index = 0; index < checkboxes.length; index++) {
                        if (typeof (checkboxes[index].checked) !== "undefined") {
                            if (isChecked) {
                                checkboxes[index].checked = true;
                            }
                            else {
                                checkboxes[index].checked = false;
                            }
                        }
                    }
                    if (hidAllIDs != null && hidSelectedIDs != null) {
                        hidSelectedIDs.value = "";
                    }
                }

            }

            function RowSelectedChild(idd, chk) {

                var hidSelectedIDs = document.getElementById('<%= hidSelectedIDs.ClientID%>');
                var hidAllIDs = document.getElementById('<%= hidAllIDs.ClientID%>');
                var hidMasterIDs = document.getElementById('<%= hidMasterIDs.ClientID%>');
                var hidChildIDs = document.getElementById('<%= hidChildIDs.ClientID%>');
                var rdoExportList = document.getElementById('<%= rdoExportList.ClientID%>');
                var hidSelectedValue = document.getElementById('<%= hidSelectedValue.ClientID%>');

                var hidFinal = "";

                var chk = document.getElementById(chk);

                if (chk.checked == false) {

                    var RadGrid1 = $find("<%=RadGrid1.ClientID %>");
                    var isChecked = false;
                    var checkboxes = RadGrid1.get_masterTableView().get_element().getElementsByTagName("INPUT");
                    var index;
                    for (index = 0; index < checkboxes.length; index++) {
                        if (typeof (checkboxes[index].checked) !== "undefined") {
                            if (checkboxes[index].name.indexOf("chkMasterHead") != "-1") {
                                checkboxes[index].checked = false;
                            }
                        }
                    }

                    if (hidSelectedIDs != null) {
                        if (hidSelectedIDs.value.length > 0) {
                            if (hidSelectedIDs.value.indexOf(idd) != -1) {
                                var s = hidSelectedIDs.value.split(',');
                                for (i = 0; i < s.length; i++) {
                                    var st = s[i].replace(/'/g, "");
                                    if (st != idd) {
                                        if (hidFinal.length == 0) {
                                            hidFinal = s[i];
                                        }
                                        else {
                                            hidFinal = hidFinal + "," + s[i];
                                        }
                                    }
                                }
                                hidSelectedIDs.value = hidFinal;
                            }
                            else {
                                hidSelectedIDs.value = hidFinal;
                            }
                        }
                    }
                }
                else {

                    if (hidSelectedIDs != null) {
                        if (hidSelectedIDs.value.length == 0) {
                            hidSelectedIDs.value = "'" + idd + "'";
                        }
                        else {
                            if (hidSelectedIDs.value.indexOf(idd) == -1) {
                                hidSelectedIDs.value = hidSelectedIDs.value + ",'" + idd + "'";
                            }
                        }
                    }

                    if (hidSelectedIDs.value.length == hidAllIDs.value.length) {

                        var RadGrid1 = $find("<%=RadGrid1.ClientID %>");
                        var isChecked = false;
                        var checkboxes = RadGrid1.get_masterTableView().get_element().getElementsByTagName("INPUT");
                        var index;
                        for (index = 0; index < checkboxes.length; index++) {
                            if (typeof (checkboxes[index].checked) !== "undefined") {
                                if (checkboxes[index].name.indexOf("chkMasterHead") != "-1") {
                                    checkboxes[index].checked = true;
                                }
                            }
                        }
                    }
                }

                if (hidSelectedIDs.value.length == hidAllIDs.value.length) {
                    CheckRadioValidation1(rdoExportList, "All");
                    hidSelectedValue.value = "0";
                }
                else if (hidSelectedIDs.value.length == hidMasterIDs.value.length) {
                    var strC = hidChildIDs.value.split(','); var bln = true;
                    for (i = 0; i < strC.length; i++) {
                        if (hidSelectedIDs.value.indexOf(strC[i]) != -1) {
                            bln = false;
                            break;
                        }
                    }
                    if (bln == false) {
                        CheckRadioValidation1(rdoExportList, "Selected");
                        hidSelectedValue.value = "1";
                    }
                    else {
                        CheckRadioValidation1(rdoExportList, "Master");
                        hidSelectedValue.value = "0";
                    }

                }
                else if (hidSelectedIDs.value.length == 0) {
                    CheckRadioValidation1(rdoExportList, "None");
                    hidSelectedValue.value = "0";
                }
                else {
                    CheckRadioValidation1(rdoExportList, "Selected");
                    hidSelectedValue.value = "1";
                }

            }
        </script>
    </telerik:RadCodeBlock>
    <style type="text/css">
        div.RadGrid .rgInfoPart
        {
            float: left;
        }
        
        div.RadGrid .rgRow td, div.RadGrid .rgAltRow td, div.RadGrid th.rgHeader, div.RadGrid th.rgResizeCol, div.RadGrid .rgFilterRow td
        {
            border-left: 1px solid gray;
            border-bottom: 1px solid gray;
        }
        
        .highlight
        {
            background-color: #FAE9AB;
        }
        .DivGrid
        {
            overflow: auto;
            overflow-y: hidden;
            border-width: 1px;
            border-color: Gray;
            background-color: White;
            vertical-align: top;
            font-family: verdana;
            font-size: 10px;
        }
        .MyPager
        {
            padding-right: 70%;
            background-color: Red;
        }
    </style>
    <link href="../Styles/Grid.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Menu.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ListBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ComboBox.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Calendar.Outlook.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <script type="text/javascript" src="../jsUpdateProgress.js"></script>
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: absolute; top: 20%; text-align: center;">
                    <img src="../Images/321.gif" style="vertical-align: middle" alt="Processing" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <ajax:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="600">
    </telerik:RadScriptManager>
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hidSheetName" />
            <asp:HiddenField runat="server" ID="hidlstColumns" />
            <asp:HiddenField runat="server" ID="hidScaleValue" />
            <asp:HiddenField runat="server" ID="hidSelectedIDs" />
            <asp:HiddenField runat="server" ID="hidAllIDs" />
            <asp:HiddenField runat="server" ID="hidMasterIDs" />
            <asp:HiddenField runat="server" ID="hidChildIDs" />
            <asp:HiddenField runat="server" ID="hidSelectedValue" Value="0" />
            <asp:HiddenField runat="server" ID="hidName" />
            <asp:HiddenField runat="server" ID="hidPageIndex" Value="0" />
            <asp:HiddenField runat="server" ID="hidB2Search" Value="0" />
            <asp:HiddenField runat="server" ID="hidOperator" Value="0" />
            <asp:HiddenField runat="server" ID="hidFilterValue" Value="0" />
            <asp:HiddenField runat="server" ID="hidSortColumn" />
            <asp:HiddenField runat="server" ID="hidSortDirection" />
            <asp:HiddenField runat="server" ID="hidReturnSortColumn" />
            <asp:HiddenField runat="server" ID="hidReturnSortDirection" />
            <asp:HiddenField runat="server" ID="hidFilterColumn" Value="0" />
            <asp:HiddenField runat="server" ID="hidFiltered" Value="0" />
            <asp:HiddenField runat="server" ID="hidBackFromView" Value="0" />
            <table cellpadding="0" cellspacing="0" border="0" width="100%" style="border: 1px solid #EDEDED;
                padding-bottom: 3px; padding-top: 3px; padding-left: 3px; padding-right: 2px;
                position: static;">
                <tr>
                    <td style="width: 100%;">
                        <table cellpadding="2" cellspacing="0" width="900px" border="0">
                            <tr>
                                <td style="width: 260px;" valign="middle">
                                    <asp:Button ID="btnBackToSearchT" runat="server" BackColor="#00759A" BorderStyle="None"
                                        CssClass="handShow" Font-Bold="True" Font-Names="VERDANA" Font-Size="XX-Small"
                                        ForeColor="White" Height="20px" OnClick="btnBackToSearchT_Click" Text="BACK TO SEARCH"
                                        Visible="true" Width="100px" /><span style="padding-left: 10px;"></span>
                                    <asp:Label runat="server" ID="lblShowFIlter" Text="Show Filter" CssClass="labelStyle"></asp:Label>
                                    <input id="Radio1" type="radio" runat="server" name="showHideGroup" onclick="showFilterItem()"
                                        class="panelStyle1" checked="true" /><label for="Radio1" class="panelStyle1">Yes</label>
                                    <input id="Radio2" type="radio" runat="server" name="showHideGroup" onclick="hideFilterItem()" /><label
                                        for="Radio2" class="panelStyle1">No</label>
                                </td>
                                <td style="width: 160px;" align="left" valign="middle">
                                    <asp:Label runat="server" ID="Label7" Text="Select credentials to export:" Font-Bold="true"
                                        CssClass="panelStyle1"></asp:Label>
                                </td>
                                <td style="width: 350px;">
                                    <asp:RadioButtonList ID="rdoExportList" runat="server" RepeatDirection="Horizontal"
                                        CssClass="panelStyle1" OnSelectedIndexChanged="rdoExportList_SelectedIndexChanged"
                                        AutoPostBack="false" onclick="javascript:SelectGridValues();return true;">
                                        <asp:ListItem Text="All versions" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="Master versions  only" Value="Master"></asp:ListItem>
                                        <asp:ListItem Text="Selected versions  only" Value="Selected" Enabled="false"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width: 50px;">
                                    <asp:Button ID="btnExport" runat="server" BackColor="#00759A" BorderStyle="None"
                                        CssClass="handShow" Font-Bold="True" Font-Names="VERDANA" Font-Size="XX-Small"
                                        ForeColor="White" Height="19px" Text="EXPORT" Visible="true" Width="70px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 5px; padding-left: 5px;">
                        <div id="divFilter" runat="server" style="display: block;">
                            <asp:Label CssClass="labelStyle" runat="server" ID="lblResultCriteria" Text="<b>You searched for</b> : "></asp:Label>
                            <asp:Label CssClass="panelStyle1" runat="server" ID="Label4" Text="" ForeColor="LightSeaGreen"></asp:Label>
                        </div>
                        <asp:HiddenField runat="server" ID="hidFilter" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 5px; padding-left: 5px;">
                        <div id="grdCharges" runat="server" style="width: 99%; overflow-x: none; overflow-y: none;">
                            <span id="span1" style="padding-bottom: 10px; padding-left: 2px; padding-top: 10px;
                                padding-right: 2px;">
                                <telerik:RadGrid ID="RadGrid1" runat="server" ShowStatusBar="false" AutoGenerateColumns="False"
                                    Width="100%" PageSize="50" AllowSorting="True" AllowMultiRowSelection="true" OnSortCommand="RadGrid1_SortCommand"
                                    AllowPaging="True" AllowFilteringByColumn="true" OnDetailTableDataBind="RadGrid1_DetailTableDataBind"
                                    OnPreRender="RadGrid1_PreRender" OnNeedDataSource="RadGrid1_NeedDataSource" GridLines="None"
                                    OnItemCommand="RadGrid1_ItemCommand" OnDataBound="RadGrid1_DataBound" OnItemDataBound="RadGrid1_ItemDataBound"
                                    OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting" OnInit="RadGrid1_Init"
                                    Skin="Myskin" OnItemCreated="RadGrid1_ItemCreated" ItemStyle-BackColor="White"
                                    AlternatingItemStyle-BackColor="White" EnableEmbeddedSkins="false" ViewStateMode="Enabled"
                                    OnPageIndexChanged="RadGrid1_PageIndexChanged" Font-Size="11px" OnGridExporting="RadGrid1_GridExporting">
                                    <%-- <PagerStyle Mode="NextPrevNumericAndAdvanced" AlwaysVisible="true" PageSizeLabelText="Results per page"
                                        ShowPagerText="true" HorizontalAlign="Left" PagerTextFormat=""></PagerStyle>--%>
                                    <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" PageSizeLabelText="Results per page"
                                        Position="TopAndBottom" ShowPagerText="true" HorizontalAlign="Left"></PagerStyle>
                                    <ExportSettings HideStructureColumns="true" />
                                    <ClientSettings EnableRowHoverStyle="false">
                                        <%--<Scrolling AllowScroll="True" UseStaticHeaders="true" SaveScrollPosition="true">
                                        </Scrolling>--%>
                                        <Selecting AllowRowSelect="false" />
                                        <ClientEvents OnFilterMenuShowing="filterMenuShowing" />
                                    </ClientSettings>
                                    <FilterMenu OnClientShown="MenuShowing" />
                                    <SortingSettings EnableSkinSortStyles="false" />
                                    <SelectedItemStyle CssClass="SelectedItem" />
                                    <MasterTableView ClientDataKeyNames="CredentialID" DataKeyNames="CredentialID,CredentialVersion,ClientSector"
                                        AllowMultiColumnSorting="false" AllowNaturalSort="false" CommandItemDisplay="None"
                                        EnableNoRecordsTemplate="false" HierarchyDefaultExpanded="false" TableLayout="Fixed"
                                        HierarchyLoadMode="ServerBind" ItemStyle-BackColor="LightGoldenrodYellow" AlternatingItemStyle-BackColor="LightGoldenrodYellow"
                                        Name="MasterGrid">
                                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                                        <NoRecordsTemplate>
                                            No Master Records Found
                                        </NoRecordsTemplate>
                                        <ExpandCollapseColumn HeaderStyle-Width="20px">
                                        </ExpandCollapseColumn>
                                        <DetailTables>
                                            <telerik:GridTableView DataKeyNames="CredentialID,CredentialVersion" Name="Orders"
                                                EnableNoRecordsTemplate="false" AllowPaging="false" AllowFilteringByColumn="false"
                                                TableLayout="Fixed" AllowSorting="false" ShowHeader="false" ItemStyle-BackColor="#F0F8FF"
                                                AlternatingItemStyle-BackColor="#F0F8FF" Width="856px">
                                                <NoRecordsTemplate>
                                                    <asp:Label Text="No Records Found in Credential Version (Other)" CssClass="panelStyle1"
                                                        runat="server" ID="lblChildNoRecord" ForeColor="Pink" Font-Bold="true"></asp:Label>
                                                </NoRecordsTemplate>
                                                <Columns>
                                                    <telerik:GridTemplateColumn UniqueName="Checkbox" Display="true" AllowFiltering="false">
                                                        <HeaderStyle Width="29px" />
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="headerChildChkbox" runat="server" AutoPostBack="true" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkChildChild" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn SortExpression="CredentialID" HeaderText="Credential id"
                                                        Visible="false" HeaderButtonType="TextButton" DataField="CredentialID" UniqueName="CredentialID">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn SortExpression="ClientName" HeaderText="Client name" HeaderButtonType="TextButton"
                                                        DataField="ClientName" Visible="false" UniqueName="CName">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn SortExpression="ClientName" UniqueName="ClientNameChild"
                                                        HeaderText="Client name" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="LinkChild" runat="server" Text='<%#Bind("CredentialID") %>' Visible="false"></asp:HyperLink>
                                                            <asp:LinkButton runat="server" ID="hypidchild" Text='<%#Bind("ClientName") %>' Font-Bold="true"
                                                                OnClick="hypidchild_Click" ForeColor="Black"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="165px" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn UniqueName="ProjectName" HeaderText="Project name" Visible="false"
                                                        HeaderButtonType="TextButton" DataField="ProjectName" SortExpression="ProjectName">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemStyle Width="10%" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn UniqueName="MatterDescriptionAlone" HeaderText="Matter/credential description"
                                                        Visible="false" HeaderButtonType="TextButton" DataField="MatterDescription">
                                                    </telerik:GridBoundColumn>
                                                    <%--<telerik:GridTemplateColumn HeaderText="Matter/credential description" UniqueName="MatterDescription"
                                                        DataField="MatterDescription" AllowFiltering="false">
                                                        <HeaderStyle Width="552px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMatterDescription" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Matter/credential description" UniqueName="ConfidentialYes"
                                                        DataField="MatterDescription" AllowFiltering="false" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblConfidentialYes" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                                    <telerik:GridBoundColumn HeaderText="Matter/credential description" HeaderButtonType="TextButton"
                                                        DataField="NameFilter" Visible="true" UniqueName="MatterDescription" AllowFiltering="false">
                                                        <HeaderStyle Width="552px" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="Matter/credential description" HeaderButtonType="TextButton"
                                                        DataField="NameFilter" Visible="false" UniqueName="ConfidentialYes" AllowFiltering="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="Matter/credential description" UniqueName="ConfidentialNo"
                                                        DataField="MatterDescription" AllowFiltering="false" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblConfidentialNo" runat="server" Text=""></asp:Label>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn UniqueName="CredentialVersionChild" HeaderText="Credential version"
                                                        Visible="true" HeaderButtonType="TextButton" DataField="CredentialVersionOther"
                                                        AllowFiltering="false">
                                                        <HeaderStyle Width="110px" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </telerik:GridTableView>
                                        </DetailTables>
                                        <Columns>
                                            <telerik:GridTemplateColumn UniqueName="Checkbox" Display="true" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkMasterChild" runat="server" AutoPostBack="false" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="chkMasterHead" runat="server" AutoPostBack="false" onclick="RowSelectedMaster(this);" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <HeaderStyle Width="30px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn SortExpression="CredentialID" HeaderText="Credential id"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CredentialID" UniqueName="CredentialID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn SortExpression="ClientName" HeaderText="Client name" HeaderButtonType="TextButton"
                                                DataField="ClientName" Visible="false" UniqueName="CName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn SortExpression="ClientName" UniqueName="ClientName" HeaderText="Client name"
                                                DataField="ClientName" Visible="true" FilterControlWidth="110px">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="Link" runat="server" Text='<%#Bind("CredentialID") %>' Visible="false"></asp:HyperLink>
                                                    <asp:LinkButton runat="server" ID="hypid" Text='<%#Bind("ClientName") %>' Font-Bold="true"
                                                        OnClick="hypid_Click" ForeColor="Black"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="150px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="ProjectName" HeaderText="Project name" Visible="false"
                                                HeaderButtonType="TextButton" DataField="ProjectName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="MatterDescriptionAlone" HeaderText="Matter/credential description"
                                                Visible="false" HeaderButtonType="TextButton" DataField="MatterDescription">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Matter/credential description" HeaderButtonType="TextButton"
                                                DataField="NameFilter" Visible="true" UniqueName="MatterDescription" FilterControlWidth="300px"
                                                DataType="System.String">
                                                <HeaderStyle Width="500px" />
                                            </telerik:GridBoundColumn>
                                            <%--<telerik:GridTemplateColumn HeaderText="Matter/credential description" UniqueName="MatterDescription"
                                                DataField="MatterDescription" FilterControlWidth="300px" DataType="System.String">
                                                <HeaderStyle Width="500px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMatterDescription" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                            <%--<telerik:GridTemplateColumn HeaderText="Matter/credential description" UniqueName="ConfidentialYes"
                                                DataField="MatterDescription" AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConfidentialYes" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>--%>
                                            <telerik:GridBoundColumn HeaderText="Matter/credential description" HeaderButtonType="TextButton"
                                                DataField="NameFilter" Visible="false" UniqueName="ConfidentialYes" FilterControlWidth="300px"
                                                DataType="System.String">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Matter/credential description" UniqueName="ConfidentialNo"
                                                DataField="MatterDescription" AllowFiltering="false" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblConfidentialNo" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="OtherMatterDescription" HeaderText="Other useful matter/credential description"
                                                Visible="false" HeaderButtonType="TextButton" DataField="OtherMatterDescription">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn SortExpression="" UniqueName="CredentialVersion" HeaderText="Credential version"
                                                DataField="CredentialVersion" Visible="true" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCredentialVersion" Text='<%#Bind("CredentialVersion")%>'
                                                        CssClass="panelStyle1"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="hypCredentialVersionmore" Text="...more" Font-Bold="true"
                                                        ForeColor="BurlyWood" Visible="false" CssClass="panelStyle1"></asp:LinkButton>
                                                    <telerik:RadToolTip ID="radTTCredentialVersion" runat="server" TargetControlID="hypCredentialVersionmore"
                                                        Width="150px" RelativeTo="Element" Position="MiddleRight" Visible="false">
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="CredentialNameMaster" HeaderText="Credential version"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CredentialVersionOther"
                                                AllowFiltering="false">
                                                <HeaderStyle Width="111px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Keyword" HeaderText="Keywords/Themes" Visible="false"
                                                HeaderButtonType="TextButton" DataField="Keyword">
                                                <%-- <HeaderStyle Width="10%" />
                                                <ItemStyle Width="10%" />--%>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn SortExpression="ClientSector" UniqueName="ClientSector"
                                                HeaderText="Client sector" DataField="ClientSector" AllowFiltering="true" DataType="System.String"
                                                ShowFilterIcon="true" Visible="true" FilterControlWidth="130px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblClientSector" Text='<%#Bind("ClientSector")%>' CssClass="panelStyle1"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="hypClientSectormore" Text="...more" Font-Bold="true"
                                                        ForeColor="BurlyWood" Visible="false" CssClass="panelStyle1"></asp:LinkButton>
                                                    <telerik:RadToolTip ID="radTTClientSector" runat="server" TargetControlID="hypClientSectormore"
                                                        Width="150px" RelativeTo="Element" Position="MiddleRight">
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                                <HeaderStyle Width="160px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientSubSector" HeaderText="Client sub sector"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientSubSector">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn SortExpression="MatterSector" UniqueName="MatterSector"
                                                DataType="System.String" HeaderText="Matter sector" DataField="MatterSector"
                                                Visible="true" FilterControlWidth="130px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblMatterSector" Text='<%#Bind("MatterSector")%>' CssClass="panelStyle1"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="hypMatterSectormore" Text="...more" Font-Bold="true"
                                                        ForeColor="BurlyWood" Visible="false" CssClass="panelStyle1"></asp:LinkButton>
                                                    <telerik:RadToolTip ID="radTTMatterSector" runat="server" TargetControlID="hypMatterSectormore"
                                                        Width="180px" RelativeTo="Element" Position="MiddleRight">
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                                <HeaderStyle Width="160px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="MatterSubSector" HeaderText="Matter sub sector"
                                                Visible="false" HeaderButtonType="TextButton" DataField="MatterSubSector">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn SortExpression="PracticeGroup" UniqueName="PracticeGroup"
                                                DataType="System.String" HeaderText="Practice group" DataField="PracticeGroup"
                                                Visible="true" FilterControlWidth="130px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblPracticeGroup" Text='<%#Bind("PracticeGroup")%>'
                                                        CssClass="panelStyle1"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="hypPracticeGroupmore" Text="...more" Font-Bold="true"
                                                        ForeColor="BurlyWood" Visible="false" CssClass="panelStyle1"></asp:LinkButton>
                                                    <telerik:RadToolTip ID="radTTPracticeGroup" runat="server" TargetControlID="hypPracticeGroupmore"
                                                        Width="150px" RelativeTo="Element" Position="MiddleRight">
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                                <HeaderStyle Width="160px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Work type" UniqueName="WT" DataField="WT"
                                                SortExpression="WT" AllowFiltering="true" FilterControlWidth="145px">
                                                <HeaderStyle Width="170px" />
                                                <ItemStyle Wrap="true" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkType" runat="server" Text='<%#Bind("WT")%>' CssClass="panelStyle1"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="hypmore" Text="...more" Font-Bold="true" ForeColor="BurlyWood"
                                                        Visible="false"></asp:LinkButton>
                                                    <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="hypmore" Width="150px"
                                                        RelativeTo="Element" Position="MiddleRight">
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="SWT" HeaderText="SubWork type" Visible="false"
                                                HeaderButtonType="TextButton" DataField="CorporateSubWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Team" HeaderText="Team(s)" Visible="false" HeaderButtonType="TextButton"
                                                DataField="Team">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="LeadPartnerExport" HeaderText="Lead partner"
                                                Visible="false" HeaderButtonType="TextButton" DataField="LeadPartner">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn SortExpression="LeadPartner" UniqueName="LeadPartner"
                                                DataType="System.String" HeaderText="Lead partner" DataField="LeadPartner" Visible="true"
                                                FilterControlWidth="130px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLeadPartner" Text='<%#Bind("LeadPartner")%>' CssClass="panelStyle1"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="hypLeadPartnermore" Text="...more" Font-Bold="true"
                                                        ForeColor="BurlyWood" Visible="false" CssClass="panelStyle1"></asp:LinkButton>
                                                    <telerik:RadToolTip ID="radTTLeadPartner" runat="server" TargetControlID="hypLeadPartnermore"
                                                        Width="150px" RelativeTo="Element" Position="MiddleRight">
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                                <HeaderStyle Width="160px" />
                                                <%--<ItemStyle Width="5%" />--%>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn UniqueName="CorpLeadPartner" HeaderText="Lead partner(s) and matter exective(s)"
                                                DataField="LeadPartner" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCorpLeadPartner" Text='<%#Bind("LeadPartner")%>'
                                                        CssClass="panelStyle1"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="OtherMatterExecutive" HeaderText="Other matter executive"
                                                Visible="false" HeaderButtonType="TextButton" DataField="OtherMatterExecutive">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn SortExpression="MatterLocation" UniqueName="MatterLocation"
                                                DataType="System.String" HeaderText="Matter location" DataField="MatterLocation"
                                                Visible="true" FilterControlWidth="110px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblMatterLocation" Text='<%#Bind("MatterLocation")%>'
                                                        CssClass="panelStyle1"></asp:Label>
                                                    <asp:LinkButton runat="server" ID="hypMatterLocationmore" Text="...more" Font-Bold="true"
                                                        ForeColor="BurlyWood" Visible="false" CssClass="panelStyle1"></asp:LinkButton>
                                                    <telerik:RadToolTip ID="radTTMatterLocation" runat="server" TargetControlID="hypMatterLocationmore"
                                                        Width="150px" RelativeTo="Element" Position="MiddleRight">
                                                    </telerik:RadToolTip>
                                                </ItemTemplate>
                                                <HeaderStyle Width="140px" />
                                                <%--<ItemStyle Width="5%" />--%>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="PredominantCountryofClient" HeaderText="Predominant country of client"
                                                Visible="false" HeaderButtonType="TextButton" DataField="PredominantCountryofClient">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ContryWhereMatterOpened" HeaderText="Country where matter opened"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ContryWhereMatterOpened">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="MatterNumber" HeaderText="Matter number" Visible="false"
                                                HeaderButtonType="TextButton" DataField="MatterNumber">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="DateMatterOpened" HeaderText="Date matter opened"
                                                Visible="false" HeaderButtonType="TextButton" DataField="DateMatterOpened">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridDateTimeColumn SortExpression="DateCompleted" HeaderText="Date completed"
                                                AllowFiltering="true" DataField="DateCompleted" DataType="System.DateTime" UniqueName="DateCompleted"
                                                PickerType="DatePicker" DataFormatString="{0:dd/MM/yyyy}" FilterControlWidth="70px">
                                                <HeaderStyle Width="100px" />
                                            </telerik:GridDateTimeColumn>
                                            <telerik:GridBoundColumn UniqueName="CredentialType" HeaderText="Credential type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CredentialType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ValueOfDeal" HeaderText="Value of deal" Visible="false"
                                                HeaderButtonType="TextButton" DataField="ValueOfDeal">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CurrencyOfDeal" HeaderText="Currency of deal"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CurrencyOfDeal">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ValueConfidentialCompletion" HeaderText="Value confidential completion"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ValueConfidentialCompletion">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientNameConfidentialCompletion" HeaderText="Client name confidential completion"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientNameConfidentialCompletion">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="MatterConfidentialCompletion" HeaderText="Matter confidential completion"
                                                Visible="false" HeaderButtonType="TextButton" DataField="MatterConfidentialCompletion">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SourceOfCredential" HeaderText="Source of credential"
                                                Visible="false" HeaderButtonType="TextButton" DataField="SourceOfCredential">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SourceofCredentialOther" HeaderText="Source of credential-other"
                                                Visible="false" HeaderButtonType="TextButton" DataField="SourceofCredentialOther">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CMSFirmsInvolved" HeaderText="CMS firms involved"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CMSFirmsInvolved">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CountriesofotherCMSFirms" HeaderText="Countries of other CMS firms"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CountriesofotherCMSFirms">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="OtherUses" HeaderText="Other uses" Visible="false"
                                                HeaderButtonType="TextButton" DataField="OtherUses">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="KnowHow" HeaderText="Know how" Visible="false"
                                                HeaderButtonType="TextButton" DataField="KnowHow">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CorpMatterCompleted" HeaderText="Has matter completed?"
                                                Visible="false" HeaderButtonType="TextButton" DataField="DateCompleted">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="blank" HeaderText="Blank" Visible="false" HeaderButtonType="TextButton"
                                                DataField="LeadPartner">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientNameConfidential" HeaderText="Client name confidential"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientNameConfidential">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientDescription" HeaderText="Confidential client generic description"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientDescription">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientMatterConfidential" HeaderText="Client matter confidential"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientMatterConfidential">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ActualDateOngoing" HeaderText="Date of completion"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ActualDateOngoing">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ApplicableLaw" HeaderText="Applicable law" Visible="false"
                                                HeaderButtonType="TextButton" DataField="ApplicableLaw">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ApplicableLawOther" HeaderText="Applicable law-other"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ApplicableLawOther">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Contentious" HeaderText="Contentious" Visible="false"
                                                HeaderButtonType="TextButton" DataField="Contentious">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="DisputeResolution" HeaderText="Dispute resolution"
                                                Visible="false" HeaderButtonType="TextButton" DataField="DisputeResolution">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CountryJurisdiction" HeaderText="Country jurisdiction"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CountryJurisdiction">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="LanguageofDispute" HeaderText="Language of dispute"
                                                Visible="false" HeaderButtonType="TextButton" DataField="LanguageofDispute">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="LanguageofDisputeOther" HeaderText="Language of dispute-other"
                                                Visible="false" HeaderButtonType="TextButton" DataField="LanguageofDisputeOther">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SeatofArbitration" HeaderText="Seat of arbitration"
                                                Visible="false" HeaderButtonType="TextButton" DataField="SeatofArbitration">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SeatofArbitrationOther" HeaderText="Seat of arbitration-other"
                                                Visible="false" HeaderButtonType="TextButton" DataField="SeatofArbitrationOther">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CountryArbitration" HeaderText="Country of arbitration"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CountryArbitration">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ArbitralRules" HeaderText="Arbitral rules" Visible="false"
                                                HeaderButtonType="TextButton" DataField="ArbitralRules">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="InvestmentTreaty" HeaderText="Investment treaty"
                                                Visible="false" HeaderButtonType="TextButton" DataField="InvestmentTreaty">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="InvestigationType" HeaderText="Investigation type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="InvestigationType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ValueConfidential" HeaderText="Value confidential"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ValueConfidential">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CMSPartnername" HeaderText="CMS partner name"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CMSPartnername">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="LeadCMSFirm" HeaderText="Lead CMS firm" Visible="false"
                                                HeaderButtonType="TextButton" DataField="LeadCMSFirm">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CredentialStatus" HeaderText="Credential status"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CredentialStatus">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Priority" HeaderText="Priority" Visible="false"
                                                HeaderButtonType="TextButton" DataField="Priority">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ProBono" HeaderText="Pro bono" Visible="false"
                                                HeaderButtonType="TextButton" DataField="ProBono">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="BibleReference" HeaderText="Bible reference"
                                                Visible="false" HeaderButtonType="TextButton" DataField="BibleReference">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientTypeBAIF" HeaderText="BAIF - Client type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientTypeBAIF">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="LeadBanks" HeaderText="BAIF - Lead banks" Visible="false"
                                                HeaderButtonType="TextButton" DataField="LeadBanks">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="BAIFWorkType" HeaderText="BAIF - Work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="BAIFWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CorporateWorkType" HeaderText="Corporate - Work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CorporateWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CorporateActingFor" HeaderText="Corporate - Acting for"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CorporateActingFor">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CorporateCountryBuyer" HeaderText="Corporate - Country buyer"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CorporateCountryBuyer">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CorporateCountryTarget" HeaderText="Corporate - Country target"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CorporateCountryTarget">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CorporateCountrySeller" HeaderText="Corporate - Country seller"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CorporateCountrySeller">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ValueOverUS" HeaderText="Corporate - Value over US $5m"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ValueOverUS">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ValueOverPound" HeaderText="Corporate - Value over pound £500k"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ValueOverPound">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ValueOverEuro" HeaderText="Corporate - Value over EUR 5m"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ValueOverEuro">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ValueRangeEuro" HeaderText="Corporate - Value range in deal currency"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ValueRangeEuro">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="DealAnnouncedId" HeaderText="Corporate - Date deal announced"
                                                Visible="false" HeaderButtonType="TextButton" DataField="DealAnnouncedId">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="PublishedReference" HeaderText="Corporate - Published reference"
                                                Visible="false" HeaderButtonType="TextButton" DataField="PublishedReference">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="MAStudy" HeaderText="Corporate - relevant to M&A study"
                                                Visible="false" HeaderButtonType="TextButton" DataField="MAStudy">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="PEClients" HeaderText="Corporate - deal involve PE clients"
                                                Visible="false" HeaderButtonType="TextButton" DataField="PEClients">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="QuarterDealAnnounceID" HeaderText="Corporate - Quarter deal announced"
                                                Visible="false" HeaderButtonType="TextButton" DataField="QuarterDealAnnounceID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="QuarterDealCompletedId" HeaderText="Corporate - Quarter deal completed"
                                                Visible="false" HeaderButtonType="TextButton" DataField="QuarterDealCompletedId">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="YearDealCompletedID" HeaderText="Corporate - Year deal completed"
                                                Visible="false" HeaderButtonType="TextButton" DataField="YearDealCompletedID">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="YearDealAnnounced" HeaderText="Corporate - Year deal announced"
                                                Visible="false" HeaderButtonType="TextButton" DataField="YearDealAnnounced">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CRDWorkType" HeaderText="CRD - Work type" Visible="false"
                                                HeaderButtonType="TextButton" DataField="CRDWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CRDSubWorkType" HeaderText="CRD - Sub work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CRDSubWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientTypeIdCommercial" HeaderText="CRD - Client type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientTypeIdCommercial">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="EPCNatureofWork" HeaderText="EPC Construction - Work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="EPCNatureofWork">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="EPCTypeofContract" HeaderText="EPC Construction - Type of contract"
                                                Visible="false" HeaderButtonType="TextButton" DataField="EPCTypeofContract">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientTypeIDEPC" HeaderText="EPC Construction - Client type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientTypeIDEPC">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientScope" HeaderText="EPC Construction - Client scope"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientScope">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientTypeOtherEPC" HeaderText="EPC Construction - Client type other"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientTypeOtherEPC">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="TypeofContractOtherEPC" HeaderText="EPC Construction - Type of contract Other"
                                                Visible="false" HeaderButtonType="TextButton" DataField="TypeofContractOtherEPC">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SubjectMatterIDEPC" HeaderText="EPC Construction - Subject matter"
                                                Visible="false" HeaderButtonType="TextButton" DataField="SubjectMatterIDEPC">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SubjectMatterOtherEPC" HeaderText="EPC Construction - Subject matter other"
                                                Visible="false" HeaderButtonType="TextButton" DataField="SubjectMatterOtherEPC">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ContractTypeIDEPCE" HeaderText="EPC Energy - Contract type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ContractTypeIDEPCE">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="EPCEnergyWorkType" HeaderText="EPC Energy - Work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="EPCEnergyWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="ClientTypeIdIPF" HeaderText="EPC Projects - Client type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="ClientTypeIdIPF">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="WorkTypeIdHC" HeaderText="Human Captial - Work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="HCWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="SubWorkTypeHC" HeaderText="Human Captial - Sub work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="HCSubWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="PensionSchemeHC" HeaderText="Human Captial - Pension scheme"
                                                Visible="false" HeaderButtonType="TextButton" DataField="PensionSchemeHC">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="RealEstateClientType" HeaderText="Real Estate - Client type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="RealEstateClientType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="RealEstateWorkType" HeaderText="Real Estate - Work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="RealEstateWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="RealEstateSubWorkType" HeaderText="Real Estate - Sub work type"
                                                Visible="false" HeaderButtonType="TextButton" DataField="RealEstateSubWorkType">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CredentialVersionOther" HeaderText="Credential version other"
                                                Visible="false" HeaderButtonType="TextButton" DataField="CredentialVersionOther">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Date_Created" HeaderText="Date created" Visible="false"
                                                HeaderButtonType="TextButton" DataField="Date_Created">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Created_By" HeaderText="Created by" Visible="false"
                                                HeaderButtonType="TextButton" DataField="Created_By">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Date_Updated" HeaderText="Date updated" Visible="false"
                                                HeaderButtonType="TextButton" DataField="Date_Updated">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Updated_By" HeaderText="Updated by" Visible="false"
                                                HeaderButtonType="TextButton" DataField="Updated_By">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnHide" runat="server" CssClass="hideButton" />
                        <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnHide"
                            PopupControlID="plnEntry" BackgroundCssClass="modalBackground" BehaviorID="EditEntryPopup"
                            DropShadow="false">
                        </ajax:ModalPopupExtender>
                        <asp:Panel ID="plnEntry" runat="server" CssClass="modalSS" Style="display: none;">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField runat="server" ID="hidSheetCount" Value="0" />
                                    <asp:Panel ID="Panel1" runat="server" Style="background-color: #00759A; padding: 20px;"
                                        Width="250px">
                                        <table cellpadding="2" cellspacing="0" border="0" width="100%" style="background-color: White;">
                                            <tr>
                                                <td valign="top">
                                                    <table cellpadding="2" cellspacing="0" border="0" width="100%" style="background-color: White;">
                                                        <tr>
                                                            <td align="left">
                                                                <div style="border-color: gray; border-width: 1px; border-style: solid; height: 135px;
                                                                    width: 240px; padding-left: 3px;">
                                                                    <asp:Label runat="server" ID="Label5" Text="Select report style:" CssClass="panelStyle1"
                                                                        Font-Bold="true"></asp:Label>
                                                                    <asp:RadioButtonList ID="rdoReportStyle" runat="server" RepeatDirection="Vertical"
                                                                        AutoPostBack="false" Enabled="true" CssClass="panelStyle1" RepeatLayout="Table"
                                                                        onclick="javascript:SetDefault();return true;" Visible="true" OnSelectedIndexChanged="rdoReportStyle_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Standard report" Value="Standard" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Full report" Value="Full"></asp:ListItem>
                                                                        <%--<asp:ListItem Text="Pratice group" Value="PG"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="Report builder" Value="Search"></asp:ListItem>
                                                                        <asp:ListItem Text="League table (Corporate only)" Value="League"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <br />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <%--Export to include client confidential formatting: --%>
                                                                <div runat="server" id="divclientname" style="border-color: gray; border-width: 1px;
                                                                    border-style: solid; height: 80px; width: 240px; padding-left: 3px; padding-top: 3px;">
                                                                    <asp:Label runat="server" ID="lblClientName" Text="Include client name if confidential:"
                                                                        Font-Bold="true" CssClass="panelStyle1" Enabled="true"></asp:Label>
                                                                    <asp:RadioButtonList ID="rdoExportConfidential" runat="server" RepeatDirection="Vertical"
                                                                        CssClass="panelStyle1" Enabled="true">
                                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <br />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <%--Include project name: --%>
                                                                <div runat="server" id="divprojectname" style="border-color: gray; border-width: 1px;
                                                                    border-style: solid; height: 80px; width: 240px; padding-left: 3px; padding-top: 3px;">
                                                                    <asp:Label runat="server" ID="lblProjectName" Text="Include project name:" Font-Bold="true"
                                                                        CssClass="panelStyle1" Enabled="true"></asp:Label>
                                                                    <asp:RadioButtonList ID="rdoProjectName" runat="server" RepeatDirection="Vertical"
                                                                        CssClass="panelStyle1" Enabled="true">
                                                                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                    <br />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="padding-right: 20px; padding-top: 5px;">
                                                                <asp:Button ID="btnExport2Excel" runat="server" BackColor="#00759A" BorderStyle="None"
                                                                    CssClass="handShow" Font-Bold="True" Font-Names="VERDANA" Font-Size="XX-Small"
                                                                    Enabled="true" ForeColor="White" Height="19px" Text="EXPORT" Visible="true" Width="80px"
                                                                    OnClick="btnExport2Excel_Click" />
                                                                <asp:Button ID="btnCancelEntry" runat="server" BackColor="#00759A" BorderStyle="None"
                                                                    CssClass="handShow" Font-Bold="True" Font-Names="VERDANA" Font-Size="XX-Small"
                                                                    Enabled="true" ForeColor="White" Height="19px" OnClientClick="return HideModalPopup();"
                                                                    Text="CLOSE" Visible="true" Width="80px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">
                                                    <div style="border-color: gray; border-width: 1px; border-style: none; height: 314px;
                                                        width: 280px; padding-left: 3px; padding-top: 3px; overflow-y: hidden; display: none;"
                                                        runat="server" id="divlstColumns">
                                                        <telerik:RadListBox ID="lstColumns" runat="server" CheckBoxes="true" Width="280px"
                                                            EnableViewState="true" Height="313px" CssClass="labelStyle10telerik" Skin="Myskin"
                                                            EnableEmbeddedSkins="false" OnClientItemChecked="OnClientItemCheckedHandler">
                                                        </telerik:RadListBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport2Excel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
