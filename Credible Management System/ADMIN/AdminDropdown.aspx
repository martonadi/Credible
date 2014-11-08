<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AdminDropdown.aspx.cs" Inherits="CredentialsDemo.ADMIN.AdminDropdown" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /*Modal Popup*/
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        
        .modalPopup
        {
            background-color: #00759A;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 250px;
            color: White;
        }
        
        .modalPopup p
        {
            padding: 5px;
        }
        /*Popup Control*/
        .popupControl
        {
            background-color: White;
            position: absolute;
            visibility: hidden;
        }
        .accordionContent
        {
            background-color: #D3DEEF;
            border: 1px dashed #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
        
        .greenBorder
        {
            border: 1px solid Gray;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function ShowEditModal(hypID) {
            if (hypID != null) {
                var frame = $get('<%=IframeEdit.ClientID%>');
                if (frame != null) {
                    frame.src = "AdminEntryDetails.aspx?q=" + hypID + "~" + new Date().getTime();
                    var pop = $find('EditModalPopup');
                    if (pop != null) {
                        pop.show();
                    }
                }
            }
        }
        function ShowAddModal() {
            var frame = $get('<%=IframeEdit.ClientID%>');
            if (frame != null) {
                frame.src = "AdminEntryDetails.aspx?Add=0" + "~" + new Date().getTime();
                var pop = $find('AddModalPopup');
                if (pop != null) {
                    pop.show();
                }
            }
        }

        function validateName() {
            var element = document.getElementById("txtName");
            if (element.value.length > 0)
                return true;
            return false;
        }
        
    </script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hidDisplayColumnName" runat="server" />
    <asp:HiddenField runat="server" ID="hidName" />
    <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hidSPNAME" runat="server" />
            <table width="100%" cellpadding="2" cellspacing="0" border="0">
                <tr>
                    <td align="center">
                        <div style="height: 450px; width: 850px; font-family: Arial; font-size: 11px; border-style: solid;
                            border-width: 1px; border-color: Gray; padding-top: 5px;" runat="server" id="div1"
                            visible="true">
                            <table width="100%" cellpadding="2" cellspacing="0" border="0">
                                <tr>
                                    <td style="width: 100%;" align="center">
                                        <asp:Label ID="lblDropsAndGrids" CssClass="labelStyle" runat="server" Text="Field name"></asp:Label>
                                        <asp:DropDownList ID="drpAllDropsAndGrids" CssClass="panelStyle1" runat="server"
                                            AutoPostBack="true" OnSelectedIndexChanged="drpAllDropsAndGrids_SelectedIndexChanged"
                                            Width="250px">
                                        </asp:DropDownList>
                                        <asp:Button ID="btnReset" runat="server" BackColor="#00759A" BorderStyle="None" CssClass="handShow"
                                            Font-Bold="True" Font-Names="VERDANA" Font-Size="XX-Small" ForeColor="White"
                                            Height="19px" Text="RESET" Visible="true" Width="70px" OnClick="btnReset_Click" />
                                        <asp:Button ID="btnAdd" runat="server" BackColor="#00759A" BorderStyle="None" CssClass="handShow"
                                            Font-Bold="True" Font-Names="VERDANA" Font-Size="XX-Small" ForeColor="White"
                                            Height="19px" Text="ADD" Visible="false" Width="70px" OnClientClick=" return ShowAddModal();" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div style="font-family: Arial; font-size: 11px;">
                                            <hr />
                                        </div>
                                        <div style="width: 800px; height: 400px; border-style: solid; border-width: 1px;
                                            border-color: Gray; overflow-x: hidden; overflow-y: scroll; padding-left: 3px;
                                            padding-top: 3px; color: Black; background-color: White;" runat="server" visible="false"
                                            id="divRecords">
                                            <asp:GridView ID="gvAllDropsandGrids" runat="server" AutoGenerateColumns="false"
                                                OnPageIndexChanging="gvAllDropsandGrids_PageIndexChanging" AllowPaging="false"
                                                PagerSettings-Mode="Numeric" GridLines="Both" PageSize="20" AllowSorting="true"
                                                OnSorting="gvAllDropsandGrids_Sorting" HeaderStyle-BackColor="#00759A" HeaderStyle-ForeColor="white"
                                                Width="780px">
                                                <RowStyle HorizontalAlign="left" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                                <Columns>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="DivEditWindow" runat="server" CssClass="modalPopup" Width="580px"
                                            Font-Bold="True" BorderStyle="Solid" BorderWidth="1px" GroupingText="" Font-Size="X-Small">
                                            <br />
                                            <table width="98%" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <iframe id="IframeEdit" frameborder="0" scrolling="no" height="155px" width="558px"
                                                            runat="server" style="background-color: #ffffff;"></iframe>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="padding:10px;">
                                                        <asp:Button ID="btnClose" Text="CLOSE" runat="server" OnClick="btnClose_Click" UseSubmitBehavior="false"
                                                            CssClass="btnclose" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" OkControlID="btnClose"
                                            TargetControlID="btnAdd" PopupControlID="DivEditWindow" BackgroundCssClass="modalBackground"
                                            BehaviorID="AddModalPopup" DropShadow="false">
                                        </ajax:ModalPopupExtender>
                                        <asp:Button ID="ButtonEdit" Style="display: none" runat="server" Text="Edit Expanse"
                                            BorderStyle="None" BackColor="#4EACC5" Font-Bold="True" ForeColor="White" Font-Names="ARIAL"
                                            Font-Size="10" CssClass="handShow" />
                                        <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" OkControlID="btnClose"
                                            TargetControlID="ButtonEdit" PopupControlID="DivEditWindow" BackgroundCssClass="modalBackground"
                                            BehaviorID="EditModalPopup" DropShadow="false">
                                        </ajax:ModalPopupExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
