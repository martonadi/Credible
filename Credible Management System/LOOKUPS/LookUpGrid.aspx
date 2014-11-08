<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LookUpGrid.aspx.cs" Inherits="CredentialsDemo.LOOKUPS.LookUpGrid" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Look Up Screen</title>
    <style type="text/css">
        .highlight
        {
            background-color: #B0E0E6;
        }
        .panelStyle1
        {
            font-family: Arial;
            font-size: 11px;
        }
        .panelStyle11
        {
            display: none;
        }
        .txtSingleStyle
        {
            font-size: 11px;
            font-family: Arial;
            border-style: solid;
            border-width: 1px;
            height: 20px;
            border-color: Gray;
            background-color: #EDEDED;
        }
        .btnclose
        {
            border-style: none;
            background-color: White;
            font-weight: bold;
            color: #00759A;
            font-family: Arial;
            font-size: 11px;
            cursor: hand;
            height: 20px;
            width: 50px;
        }
    </style>
    <telerik:RadCodeBlock ID="id1" runat="server">
        <script type="text/javascript">

            function postBackCall(e) {
                var key;
                if (e.keyCode == 13) {
                    var cmdSearch = document.getElementById('<%=cmdSearch.ClientID %>');
                    if (cmdSearch != null) {
                        cmdSearch.click();
                    }
                }
                else {
                    key = window.event.keyCode;
                }
                return (e.keyCode != 13);
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
            function validationFields() {
                var txt1 = document.getElementById('txtSearch');
                if (txt1 != null && Trim(txt1.value) == "") {
                    alert("Please enter the text to search !!!");
                    txt1.focus();
                    return false;
                }
                var hid = document.getElementById('hidClose');
                if (hid != null) {
                    hid.value = "1";
                }
            }

            function ClearSelection() {
                var chk = confirm('Do you want to clear all the values ?');
                if (chk == true) {
                    var gridViewElem = document.getElementById('<%=gvAllLookUp.ClientID %>');
                    if (gridViewElem != null) {
                        for (i = 1; i <= gridViewElem.rows.length - 1; i++) {
                            if (gridViewElem.rows[i].cells[0].children[0].checked == true) {
                                gridViewElem.rows[i].cells[0].children[0].checked = false;
                            }
                        }
                    }
                    var txtSearch = document.getElementById('<%=txtSearch.ClientID %>');
                    if (txtSearch != null) {
                        txtSearch.value = "";
                    }
                    var hidIDS = document.getElementById("<%=hidIDS.ClientID%>");
                    if (hidIDS != null) {
                        hidIDS.value = "";
                    }
                }
            }

            function checkSelection() {
                var gridViewElem = document.getElementById('<%=gvAllLookUp.ClientID %>');
                var bln = false;
                if (gridViewElem != null) {
                    for (i = 1; i <= gridViewElem.rows.length - 1; i++) {
                        if (gridViewElem.rows[i].cells[0].children[0].checked == true) {
                            bln = true;
                            break;
                        }
                    }
                }
                if (bln == false) {
                    var chk = confirm('You have not selected any value.Do you wish to close the lookup ?');
                    if (chk == true) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }

            function ReturnChild() {
                if (checkSelection() == true) {
                    var frmCtrl = document.getElementById('hidID').value;
                    var txtValue = ""; var txtId = "";
                    var gridViewElem = document.getElementById("<%=gvAllLookUp.ClientID%>");
                    if (gridViewElem != null) {
                        for (i = 1; i <= gridViewElem.rows.length - 1; i++) {
                            if (gridViewElem.rows[i].cells[0].children[0].checked == true) {
                                if (txtValue == "") {
                                    txtValue = Trim(gridViewElem.rows[i].cells[2].innerText);
                                }
                                else {
                                    if (frmCtrl == "lbl_Tab_Other_Matter_Executive" || frmCtrl == "lbl_Tab_Lead_Partner") {
                                        txtValue = txtValue + " | " + Trim(gridViewElem.rows[i].cells[2].innerText);
                                    }
                                    else {
                                        txtValue = txtValue + "," + Trim(gridViewElem.rows[i].cells[2].innerText);
                                    }
                                }
                                if (txtId == "") {
                                    txtId = Trim(gridViewElem.rows[i].cells[1].innerText);
                                }
                                else {
                                    txtId = txtId + "," + Trim(gridViewElem.rows[i].cells[1].innerText);
                                }
                            }
                        }
                    }
                    var vReturnValue = new Object();
                    vReturnValue.info = txtId + "~~" + txtValue + "~~" + frmCtrl;
                    window.returnValue = vReturnValue;
                    window.close();
                }
            }

            function ReturnChildUnload() {
                if (document.getElementById('hidClose').value == "0") {
                    var frmCtrl = document.getElementById('hidID').value;
                    var txtValue = ""; var txtId = "";
                    var gridViewElem = document.getElementById("<%=gvAllLookUp.ClientID%>");
                    if (gridViewElem != null) {
                        for (i = 1; i <= gridViewElem.rows.length - 1; i++) {
                            if (gridViewElem.rows[i].cells[0].children[0].checked == true) {
                                if (txtValue == "") {
                                    txtValue = Trim(gridViewElem.rows[i].cells[2].innerText);
                                }
                                else {
                                    if (frmCtrl == "lbl_Tab_Other_Matter_Executive" || frmCtrl == "lbl_Tab_Lead_Partner") {
                                        txtValue = txtValue + " | " + Trim(gridViewElem.rows[i].cells[2].innerText);
                                    }
                                    else {
                                        txtValue = txtValue + "," + Trim(gridViewElem.rows[i].cells[2].innerText);
                                    }
                                }
                                if (txtId == "") {
                                    txtId = Trim(gridViewElem.rows[i].cells[1].innerText);
                                }
                                else {
                                    txtId = txtId + "," + Trim(gridViewElem.rows[i].cells[1].innerText);
                                }
                            }
                        }
                    }

                    var vReturnValue = new Object();
                    vReturnValue.info = txtId + "~~" + txtValue + "~~" + frmCtrl;
                    window.returnValue = vReturnValue;
                    window.close();
                }
            }


            function test(obj) {
                if (obj.checked == true) {
                    var txtId = "";
                    var gridViewElem = document.getElementById("<%=gvAllLookUp.ClientID%>");
                    if (gridViewElem != null) {
                        for (i = 1; i <= gridViewElem.rows.length - 1; i++) {
                            if (gridViewElem.rows[i].cells[0].children[0].checked == true) {
                                if (txtId == "") {
                                    txtId = gridViewElem.rows[i].cells[1].innerText;
                                }
                                else {
                                    txtId = txtId + "," + gridViewElem.rows[i].cells[1].innerText;
                                }
                            }

                        }
                    }
                    var hidIDS = document.getElementById("<%=hidIDS.ClientID%>");
                    if (hidIDS != null) {
                        hidIDS.value = "";
                        hidIDS.value = txtId;
                    }
                }
                else {
                    var txtId = "";
                    var gridViewElem = document.getElementById("<%=gvAllLookUp.ClientID%>");
                    if (gridViewElem != null) {
                        for (i = 1; i <= gridViewElem.rows.length - 1; i++) {
                            if (gridViewElem.rows[i].cells[0].children[0].checked == true) {
                                if (txtId == "") {
                                    txtId = gridViewElem.rows[i].cells[1].innerText;
                                }
                                else {
                                    txtId = txtId + "," + gridViewElem.rows[i].cells[1].innerText;
                                }
                            }
                        }
                    }
                    var hidIDS = document.getElementById("<%=hidIDS.ClientID%>");
                    if (hidIDS != null) {
                        hidIDS.value = "";
                        hidIDS.value = txtId;
                    }
                }
            }

        </script>
    </telerik:RadCodeBlock>
</head>
<body style="background-color: #00759A;" onunload="ReturnChildUnload();return false;">
    <form id="form1" runat="server">
    <%--<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmdSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel2" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="30"
        BackColor="#E0E0E0" InitialDelayTime="500">
        <asp:Image ID="imgLoading" Style="margin-top: 100px" runat="server" ImageUrl="~/Images/lloading.gif"
            BorderWidth="0px" AlternateText="Loading" />
    </telerik:RadAjaxLoadingPanel>--%>
    <%--<asp:Panel ID="Panel2" runat="server">--%>
    <div class="panelStyle1" style="padding-left: 15px; padding-top: 5px;">
    <asp:HiddenField runat="server" ID="hidName" />
        <asp:HiddenField runat="server" ID="hidIDS" />
        <asp:HiddenField runat="server" ID="hidID1" />
        <asp:HiddenField runat="server" ID="hidText" />
        <asp:HiddenField runat="server" ID="hidProcedureName" />
        <asp:HiddenField runat="server" ID="hidBusinessID" />
        <asp:HiddenField runat="server" ID="hidHead" />
        <asp:HiddenField runat="server" ID="hidClose" Value="0" />
        <table cellpadding="0" cellspacing="0" border="0" width="370px" style="padding-top: 5px;
            padding-left: 10px; padding-right: 2px; padding-bottom: 10px;">
            <tr>
                <td align="left" style="width: 290px;">
                    <asp:TextBox ID="txtSearch" runat="server" Width="280px" Height="19px" CssClass="txtSingleStyle"></asp:TextBox>
                </td>
                <td align="left" style="width: 70px;">
                    <asp:Button ID="cmdSearch" runat="server" Text="SEARCH" Width="68px" OnClick="cmdSearch_Click"
                        OnClientClick="return validationFields();" CssClass="btnclose" />
                    <asp:HiddenField ID="hidID" runat="server" />
                    <asp:HiddenField runat="server" ID="hid1" />
                    <asp:HiddenField runat="server" ID="hidFromParent" />
                    <asp:HiddenField runat="server" ID="hidEnter" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="padding-top: 5px;">
                    <div style="width: 360px; height: 310px; border-style: solid; border-width: 1px;
                        border-color: Gray; overflow-x: hidden; overflow-y: scroll; padding-top: 3px;
                        padding-right: 5px; padding-left: 5px; color: Black; background-color: White;">
                        <asp:GridView ID="gvAllLookUp" runat="server" CssClass="GridView" AutoGenerateColumns="false"
                            Width="366px">
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <br />
                    <asp:Button ID="btnOk" runat="server" Text="ADD" OnClientClick="ReturnChild();return false;"
                        CssClass="btnclose" />
                    <asp:Button ID="btnClear" runat="server" Text="CLEAR" OnClientClick="ClearSelection();return false;"
                        CssClass="btnclose" />
                </td>
            </tr>
        </table>
    </div>
    <%--</asp:Panel>--%>
    </form>
</body>
</html>
