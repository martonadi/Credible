<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="ViewEntryDetails.aspx.cs" Inherits="CredentialsDemo.ViewEntryDetails" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ OutputCache Location="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .labelStyleT
        {
            font-size: 11px;
            font-family: Arial;
        }
        .labelStyleV
        {
            font-size: 11px;
            font-family: Arial;
            font-weight: bold;
        }
        .labelStyleH
        {
            font-size: 11px;
            font-family: Arial;
            font-weight: bold;
            color: #00759A;
        }
        .accordionHeader
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #00759A; /*RGB(0,117,154);*/
            font-family: Arial;
            font-size: 11px;
            font-weight: bold;
            padding: 4px;
            margin-top: 1px;
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">
        var ModalProgress = '<%= ModalProgress.ClientID %>';

        function PrintContent() {
            var DocumentContainer = document.getElementById('divPrint');
            if (DocumentContainer != null) {
                var obj = window.open('', 'PrintWindow', 'width=950,height=650,top=50,left=50,toolbars=no,scrollbars=yes,status=no,resizable=yes');
                obj.document.writeln(DocumentContainer.innerHTML);
                obj.document.close();
                obj.focus();
                obj.print();
                obj.close();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
    <div>
        <table cellpadding="2" cellspacing="0" border="0" width="100%" align="center" style="border: 1px solid #808080">
            <tr>
                <td align="center">
                    <asp:Button ID="btnEdit" runat="server" BackColor="#00759A" BorderStyle="None" CssClass="handShow"
                        Font-Bold="True" Font-Names="VERDANA" Font-Size="X-Small" ForeColor="White" Height="20px"
                        Text="EDIT" Visible="false" Width="70px" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnBack" runat="server" BackColor="#00759A" BorderStyle="None" CssClass="handShow"
                        Font-Bold="True" Font-Names="VERDANA" Font-Size="X-Small" ForeColor="White" Height="20px"
                        Text="BACK TO SEARCH RESULTS" Visible="true" Width="170px" OnClick="btnBack_Click" />
                    <asp:Button ID="btnPrint" runat="server" BackColor="#00759A" BorderStyle="None" CssClass="handShow"
                        Font-Bold="True" Font-Names="VERDANA" Font-Size="X-Small" ForeColor="White" Height="20px"
                        Text="PRINT" Visible="true" Width="70px" OnClientClick="PrintContent();" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <div id="divPrint" style="width: 600px; display: none;">
                        <table cellpadding="0" cellspacing="0" border="1" width="580px" align="center">
                            <tr>
                                <td valign="top" style="width: 200px; display: none;">
                                </td>
                                <td style="width: 380px; display: none;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label14" Text="Credential Details" Width="400px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 200px;" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Client" Text="Client name" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td style="width: 380px;" align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Client" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Project_Description" Text="Matter/credential description"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Project_Description" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Significant_Features">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Significant_Features" Text="Other useful matter/credential description"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Significant_Features" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_Tab_Client_Name_Confidential">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Client_Name_Confidential" Text="Client name confidential"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="rdo_Tab_Client_Name_Confidential" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="tr_Tab_Client_Matter_Confidential">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Client_Matter_Confidential" Text="Matter confidential"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="rdo_Tab_Client_Matter_Confidential" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Value_Confidential">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Value_Confidential" Text="Value confidential"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="rdo_Tab_Value_Confidential" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_ValueOfDeal_Core">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_ValueOfDeal_Core" Text="Value of deal" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_ValueOfDeal_Core" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Currency_Of_Deal">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Currency_Of_Deal" Text="Currency of deal" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Currency_Of_Deal" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_ClientIndustrySector" Text="Client sector"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_ClientIndustrySector" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Client_Industry_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Client_Industry_Type" Text="Client sub-sector"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Client_Industry_Type" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_TransactionIndustrySector" Text="Matter sector"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_TransactionIndustrySector" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Transaction_Industry_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Transaction_Industry_Type" Text="Matter sub-sector"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Transaction_Industry_Type" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_PracticeGroup" Text="Practice group" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_PracticeGroup" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_WorkType">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_WorkType" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_WorkType" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_SubWorkType">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_SubWorkType" Text="SubWork type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_SubWorkType" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Team" Text="Teams" CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Team" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Lead_Partner" Text="Lead partners" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Lead_Partner" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_ClientDescription">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_ClientDescription" Text="Confidential client generic description"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_ClientDescription" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_NameConfidential_Completion">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_NameConfidential_Completion" Text="Client name confidential on completion"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="rdo_Tab_NameConfidential_Completion" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Country_PredominantCountry">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Country_PredominantCountry" Text="Predominant country of client"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Country_PredominantCountry" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Matter_No" Text="Matter number" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Matter_No" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Date_Opened" Text="Date matter opened" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Date_Opened" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Date_Completed" Text="Date matter completed"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Date_Completed" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Keyword">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Keyword" Text="Keyword(s)/themes" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Keyword" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_MatterConfidential_Completion">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_MatterConfidential_Completion" Text="Matter confidential on completion"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="rdo_Tab_MatterConfidential_Completion" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_ProjectName_Core">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_ProjectName_Core" Text="Project name" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_ProjectName_Core" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Country_Law">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Country_Law" Text="Applicable law" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Country_Law" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Country_Law_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Country_Law_Other" Text="Other" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Country_Law_Other" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Country_Matter_Open" Text="Country where matter opened"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Country_Matter_Open" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Country_Matter_Close" Text="Matter location(s)"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Country_Matter_Close" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Contentious_IRG" Text="Contentious/non-contentious"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Contentious_IRG" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Dispute_Resolution">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Dispute_Resolution" Text="Dispute resolution"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Dispute_Resolution" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Country_ArbitrationCountry">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Country_ArbitrationCountry" Text="Country of arbitration"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Country_ArbitrationCountry" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_ArbitrationCity">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_ArbitrationCity" Text="Seat of arbitration"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_ArbitrationCity" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_ArbitrationCity_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_ArbitrationCity_Other" Text="Other" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_ArbitrationCity_Other" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Arbitral_Rules">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Arbitral_Rules" Text="Arbitral rules" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Arbitral_Rules" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_InvestmentTreaty">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_InvestmentTreaty" Text="Investment treaty"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="rdo_Tab_InvestmentTreaty" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Investigation_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Investigation_Type" Text="Investigation Type"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Investigation_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Language_Of_Dispute">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Language_Of_Dispute" Text="Language of Dispute"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Language_Of_Dispute" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Language_Of_Dispute_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Language_Of_Dispute_Other" Text="Other" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Language_Of_Dispute_Other" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Country_Jurisdiction">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Country_Jurisdiction" Text="Jurisidiction of Dispute"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Country_Jurisdiction" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_ValueConfidential_Completion">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_ValueConfidential_Completion" Text="Value confidential on completion"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="rdo_Tab_ValueConfidential_Completion" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_CMSPartnerName">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_CMSPartnerName" Text="Name of CMS partner"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_CMSPartnerName" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Source_Of_Credential">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Source_Of_Credential" Text="Source of CMS credential"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Source_Of_Credential" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_SourceOfCredential_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_SourceOfCredential_Other" Text="Other" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_SourceOfCredential_Other" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Other_Matter_Executive">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Other_Matter_Executive" Text="Matter executive(s)"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Other_Matter_Executive" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Referred_From_Other_CMS_Office">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Referred_From_Other_CMS_Office" Text="CMS firms involved"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Referred_From_Other_CMS_Office" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Lead_CMS_Firm">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Lead_CMS_Firm" Text="Lead CMS firm" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Lead_CMS_Firm" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Country_OtherCMSOffice">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Country_OtherCMSOffice" Text="Countries of other CMS firms"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Country_OtherCMSOffice" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Credential_Status" Text="Credential status"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Credential_Status" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Credential_Version" Text="Credential version"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Credential_Version" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Credential_Version_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Credential_Version_Other" Text="Other" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Credential_Version_Other" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Credential_Type" Text="Credential type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Credential_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Other_Uses">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Other_Uses" Text="Other uses" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Other_Uses" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Priority">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Priority" Text="Credential priority" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Tab_Priority" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_ProBono">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_ProBono" Text="Pro bono" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="rdo_Tab_ProBono" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Know_How">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Know_How" Text="Know how" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Know_How" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Tab_Bible_Reference">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Bible_Reference" Text="Bible reference" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Bible_Reference" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trBAIF">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label1" Text="BAIF details" Width="400px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_BAI_ClientTypeIdBAIF">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_BAI_ClientTypeIdBAIF" Text="Client type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_BAI_ClientTypeIdBAIF" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_BAI_LeadBanks">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_BAI_LeadBanks" Text="Lead bank(s)" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_BAI_LeadBanks" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_BAI_Work_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_BAI_Work_Type" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_BAI_Work_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trCorp">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label2" Text="Corporate details" Width="400px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Work_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Work_Type" Text="Work Type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Cor_Work_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_SubWork_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_SubWork_Type" Text="Sub work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Cor_SubWork_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Acting_For">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Acting_For" Text="Acting for" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Cor_Acting_For" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Country_Buyer">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Country_Buyer" Text="Country of buyer" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Cor_Country_Buyer" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Country_Seller">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Country_Seller" Text="Country of seller" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Cor_Country_Seller" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Country_Target">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Country_Target" Text="Country of target" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Cor_Country_Target" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Value_Over_US">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Value_Over_US" Text="Value over US$5m" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_Value_Over_US" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Value_Over_Pound">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Value_Over_Pound" Text="Value over £500,000"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_Value_Over_Pound" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Value_Over_Euro">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Value_Over_Euro" Text="Value over euro 5m"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_Value_Over_Euro" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_ValueRangeEuro">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_ValueRangeEuro" Text="Range in deal currency"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_ValueRangeEuro" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_Published_Reference">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_Published_Reference" Text="Published reference"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Cor_Published_Reference" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_DealAnnouncedId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_DealAnnouncedId" Text="Date deal announced / signed"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Cor_DealAnnouncedId" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_MAStudy">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_MAStudy" Text="Is the deal relevant for M&A study"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_MAStudy" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_PEClients">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_PEClients" Text="Does the deal involve PE clients on either side"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_PEClients" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_QuarterDealAnnouncedId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_QuarterDealAnnouncedId" Text="Quarter deal announced"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_QuarterDealAnnouncedId" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_QuarterDealCompletedId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_QuarterDealCompletedId" Text="Quarter Deal Completed"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_QuarterDealCompletedId" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_YearDeal_Announced">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_YearDeal_Announced" Text="Year deal announced/signed"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_YearDeal_Announced" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Cor_YearDealCompletedId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Cor_YearDealCompletedId" Text="Year deal completed"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Cor_YearDealCompletedId" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trCorpTax">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label3" Text="Corporate Tax details" Width="400px"
                                        CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_Crt_WorkType_CorpTax">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Crt_WorkType_CorpTax" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_Crt_WorkType_CorpTax" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trCRD">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label4" Text="CRD details" Width="400px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_CRD_Work_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_CRD_Work_Type" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_CRD_Work_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_CRD_SubWork_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_CRD_SubWork_Type" Text="SubWork type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_CRD_SubWork_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_CRD_ClientTypeIdCommercial">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_CRD_ClientTypeIdCommercial" Text="Client type"
                                        CssClass="labelStyleT" Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_CRD_ClientTypeIdCommercial" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trEPC">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label5" Text="EPC Construction details" Width="400px"
                                        CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_EPC_Nature_Of_Work">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_EPC_Nature_Of_Work" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_EPC_Nature_Of_Work" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_EPC_ClientScopeId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_EPC_ClientScopeId" Text="Client scope" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_EPC_ClientScopeId" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_EPC_ClientTypeIdEPC">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_EPC_ClientTypeIdEPC" Text="Client type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_EPC_ClientTypeIdEPC" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_EPC_ClientTypeOther">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_EPC_ClientTypeOther" Text="Other" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_EPC_ClientTypeOther" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_EPC_Type_Of_Contract">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_EPC_Type_Of_Contract" Text="Type of contract" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_EPC_Type_Of_Contract" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_EPC_Type_Of_Contract_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_EPC_Type_Of_Contract_Other" Text="Other" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_EPC_Type_Of_Contract_Other" Text="" Width="380px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_EPC_SubjectMatterId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_EPC_SubjectMatterId" Text="Subject matter" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_EPC_SubjectMatterId" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_EPC_Subject_Matter_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_EPC_Subject_Matter_Other" Text="Other" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_EPC_SubjectMatterOther" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trENE">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label6" Text="EPC Energy details" Width="400px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_ENE_Transaction_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_ENE_Transaction_Type" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_ENE_Transaction_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_ENE_ContractTypeId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_ENE_ContractTypeId" Text="Contract type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_ENE_ContractTypeId" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trHC">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label7" Text="Human Capital details" Width="400px"
                                        CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_HCC_WorkTypeIdHC">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_HCC_WorkTypeIdHC" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_HCC_WorkTypeIdHC" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr2">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_HCC_SubWorkTypeIdHC" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_HCC_SubWorkTypeIdHC" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_HCC_PensionSchemeHC">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_HCC_PensionSchemeHC" Text="Pension scheme" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_HCC_PensionSchemeHC" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trIPF">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label8" Text="EPC Projects details" Width="400px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_IPF_ClientTypeIdIPF">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_IPF_ClientTypeIdIPF" Text="Client type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="cbo_IPF_ClientTypeIdIPF" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="trRE">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label9" Text="Real estate details" Width="400px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_RES_Client_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_RES_Client_Type" Text="Client type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_RES_Client_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr_RES_Work_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_RES_Work_Type" Text="Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_RES_Work_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr1">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_RES_SubWork_Type" Text="Sub - Work type" CssClass="labelStyleT"
                                        Width="200px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_RES_SubWork_Type" Text="" Width="380px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1">
                        <table cellpadding="2" cellspacing="0" border="1" width="100%" align="center" style="border: 0px solid #808080">
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label10" Text="Credential Details" Width="600px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 30%;" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Client" Text="Client name" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td style="width: 70%;" align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Client" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Project_Description" Text="Matter/credential description"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Project_Description" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Significant_Features">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Significant_Features" Text="Other useful matter/credential description"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Significant_Features" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="V1_tr_Tab_Client_Name_Confidential">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Client_Name_Confidential" Text="Client name confidential"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_rdo_Tab_Client_Name_Confidential" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" id="V1_tr_Tab_Client_Matter_Confidential">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Client_Matter_Confidential" Text="Matter confidential"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_rdo_Tab_Client_Matter_Confidential" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Value_Confidential">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Value_Confidential" Text="Value confidential"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_rdo_Tab_Value_Confidential" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_ValueOfDeal_Core">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_ValueOfDeal_Core" Text="Value of deal" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_ValueOfDeal_Core" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Currency_Of_Deal">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Currency_Of_Deal" Text="Currency of deal"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Currency_Of_Deal" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_ClientIndustrySector" Text="Client sector"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_ClientIndustrySector" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Client_Industry_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Client_Industry_Type" Text="Client sub-sector"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Client_Industry_Type" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_TransactionIndustrySector" Text="Matter sector"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_TransactionIndustrySector" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Transaction_Industry_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Transaction_Industry_Type" Text="Matter sub-sector"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Transaction_Industry_Type" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_PracticeGroup" Text="Practice group" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_PracticeGroup" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_WorkType">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_WorkType" Text="Work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_WorkType" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_SubWorkType">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_SubWorkType" Text="SubWork type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_SubWorkType" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Team" Text="Teams" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Team" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Lead_Partner" Text="Lead partners" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Lead_Partner" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_ClientDescription">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_ClientDescription" Text="Confidential client generic description"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_ClientDescription" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_NameConfidential_Completion">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_NameConfidential_Completion" Text="Client name confidential on completion"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_rdo_Tab_NameConfidential_Completion" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Country_PredominantCountry">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Country_PredominantCountry" Text="Predominant country of client"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Country_PredominantCountry" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Matter_No" Text="Matter number" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Matter_No" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Date_Opened" Text="Date matter opened" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Date_Opened" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Date_Completed" Text="Date matter completed"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Date_Completed" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Keyword">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Keyword" Text="Keyword(s)/themes" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Keyword" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_MatterConfidential_Completion">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_MatterConfidential_Completion" Text="Matter confidential on completion"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_rdo_Tab_MatterConfidential_Completion" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_ProjectName_Core">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_ProjectName_Core" Text="Project name" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_ProjectName_Core" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Country_Law">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Country_Law" Text="Applicable law" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Country_Law" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Country_Law_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Country_Law_Other" Text="Other" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Country_Law_Other" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Country_Matter_Open" Text="Country where matter opened"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Country_Matter_Open" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Country_Matter_Close" Text="Matter location(s)"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Country_Matter_Close" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Contentious_IRG" Text="Contentious/non-contentious"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Contentious_IRG" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Dispute_Resolution">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Dispute_Resolution" Text="Dispute resolution"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Dispute_Resolution" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Country_ArbitrationCountry">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Country_ArbitrationCountry" Text="Country of arbitration"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Country_ArbitrationCountry" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_ArbitrationCity">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_ArbitrationCity" Text="Seat of arbitration"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_ArbitrationCity" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_ArbitrationCity_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_ArbitrationCity_Other" Text="Other" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_ArbitrationCity_Other" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Arbitral_Rules">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Arbitral_Rules" Text="Arbitral rules" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Arbitral_Rules" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_InvestmentTreaty">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_InvestmentTreaty" Text="Investment treaty"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_rdo_Tab_InvestmentTreaty" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Investigation_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Investigation_Type" Text="Investigation Type"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Investigation_Type" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Language_Of_Dispute">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Language_Of_Dispute" Text="Language of Dispute"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Language_Of_Dispute" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Language_Of_Dispute_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Language_Of_Dispute_Other" Text="Other"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Language_Of_Dispute_Other" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Country_Jurisdiction">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Country_Jurisdiction" Text="Jurisidiction of Dispute"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Country_Jurisdiction" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_ValueConfidential_Completion">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_ValueConfidential_Completion" Text="Value confidential on completion"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_rdo_Tab_ValueConfidential_Completion" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_CMSPartnerName">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_CMSPartnerName" Text="Name of CMS partner"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_CMSPartnerName" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Source_Of_Credential">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Source_Of_Credential" Text="Source of CMS credential"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Source_Of_Credential" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_SourceOfCredential_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_SourceOfCredential_Other" Text="Other" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_SourceOfCredential_Other" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Other_Matter_Executive">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Other_Matter_Executive" Text="Matter executive(s)"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Other_Matter_Executive" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Referred_From_Other_CMS_Office">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Referred_From_Other_CMS_Office" Text="CMS firms involved"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Referred_From_Other_CMS_Office" Text=""
                                        Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Lead_CMS_Firm">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Lead_CMS_Firm" Text="Lead CMS firm" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Lead_CMS_Firm" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Country_OtherCMSOffice">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Country_OtherCMSOffice" Text="Countries of other CMS firms"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Country_OtherCMSOffice" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Credential_Status" Text="Credential status"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Credential_Status" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Credential_Version" Text="Credential version"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Credential_Version" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Credential_Version_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Credential_Version_Other" Text="Other" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Credential_Version_Other" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Credential_Type" Text="Credential type"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Credential_Type" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Other_Uses">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Other_Uses" Text="Other uses" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Other_Uses" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Priority">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Priority" Text="Credential priority" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Tab_Priority" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_ProBono">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_ProBono" Text="Pro bono" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_rdo_Tab_ProBono" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Know_How">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Know_How" Text="Know how" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Know_How" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Tab_Bible_Reference">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Tab_Bible_Reference" Text="Bible reference"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Tab_Bible_Reference" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr3">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label11" Text="BAIF details" Width="600px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_BAI_ClientTypeIdBAIF">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_BAI_ClientTypeIdBAIF" Text="Client type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_BAI_ClientTypeIdBAIF" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_BAI_LeadBanks">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_BAI_LeadBanks" Text="Lead bank(s)" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_BAI_LeadBanks" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_BAI_Work_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_BAI_Work_Type" Text="Work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_BAI_Work_Type" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr4">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label12" Text="Corporate details" Width="600px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Work_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Work_Type" Text="Work Type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Cor_Work_Type" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_SubWork_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_SubWork_Type" Text="Sub work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Cor_SubWork_Type" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Acting_For">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Acting_For" Text="Acting for" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Cor_Acting_For" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Country_Buyer">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Country_Buyer" Text="Country of buyer" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Cor_Country_Buyer" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Country_Seller">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Country_Seller" Text="Country of seller"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Cor_Country_Seller" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Country_Target">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Country_Target" Text="Country of target"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Cor_Country_Target" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Value_Over_US">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Value_Over_US" Text="Value over US$5m" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_Value_Over_US" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Value_Over_Pound">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Value_Over_Pound" Text="Value over £500,000"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_Value_Over_Pound" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Value_Over_Euro">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Value_Over_Euro" Text="Value over euro 5m"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_Value_Over_Euro" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_ValueRangeEuro">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_ValueRangeEuro" Text="Range in deal currency"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_ValueRangeEuro" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_Published_Reference">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_Published_Reference" Text="Published reference"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Cor_Published_Reference" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_DealAnnouncedId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_DealAnnouncedId" Text="Date deal announced / signed"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_Cor_DealAnnouncedId" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_MAStudy">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_MAStudy" Text="Is the deal relevant for M&A study"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_MAStudy" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_PEClients">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_PEClients" Text="Does the deal involve PE clients on either side"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_PEClients" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_QuarterDealAnnouncedId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_QuarterDealAnnouncedId" Text="Quarter deal announced"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_QuarterDealAnnouncedId" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_QuarterDealCompletedId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_QuarterDealCompletedId" Text="Quarter Deal Completed"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_QuarterDealCompletedId" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_YearDeal_Announced">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_YearDeal_Announced" Text="Year deal announced/signed"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_YearDeal_Announced" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Cor_YearDealCompletedId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Cor_YearDealCompletedId" Text="Year deal completed"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Cor_YearDealCompletedId" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr5">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label13" Text="Corporate-tax details" Width="600px"
                                        CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_Crt_WorkType_CorpTax">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_Crt_WorkType_CorpTax" Text="Work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_Crt_WorkType_CorpTax" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr6">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label15" Text="CRD details" Width="600px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_CRD_Work_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_CRD_Work_Type" Text="Work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_CRD_Work_Type" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_CRD_SubWork_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_CRD_SubWork_Type" Text="SubWork type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_CRD_SubWork_Type" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_CRD_ClientTypeIdCommercial">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_CRD_ClientTypeIdCommercial" Text="Client type"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_CRD_ClientTypeIdCommercial" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr7">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label16" Text="EPC Construction details" Width="600px"
                                        CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_EPC_Nature_Of_Work">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_EPC_Nature_Of_Work" Text="Work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_EPC_Nature_Of_Work" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_EPC_ClientScopeId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_EPC_ClientScopeId" Text="Client scope" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_EPC_ClientScopeId" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_EPC_ClientTypeIdEPC">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_EPC_ClientTypeIdEPC" Text="Client type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_EPC_ClientTypeIdEPC" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_EPC_ClientTypeOther">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_EPC_ClientTypeOther" Text="Other" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_EPC_ClientTypeOther" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_EPC_Type_Of_Contract">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_EPC_Type_Of_Contract" Text="Type of contract"
                                        CssClass="labelStyleT" Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_EPC_Type_Of_Contract" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_EPC_Type_Of_Contract_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_EPC_Type_Of_Contract_Other" Text="Other" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_EPC_Type_Of_Contract_Other" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_EPC_SubjectMatterId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_EPC_SubjectMatterId" Text="Subject matter" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_EPC_SubjectMatterId" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_EPC_Subject_Matter_Other">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_EPC_Subject_Matter_Other" Text="Other" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_EPC_SubjectMatterOther" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr8">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label17" Text="EPC Energy details" Width="600px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_ENE_Transaction_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_ENE_Transaction_Type" Text="Work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_ENE_Transaction_Type" Text="" Width="600px"
                                        CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_ENE_ContractTypeId">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_ENE_ContractTypeId" Text="Contract type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_ENE_ContractTypeId" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr9">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label18" Text="Human Capital details" Width="600px"
                                        CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_HCC_WorkTypeIdHC">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_HCC_WorkTypeIdHC" Text="Work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_HCC_WorkTypeIdHC" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_HCC_PensionSchemeHC">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_HCC_PensionSchemeHC" Text="Pension scheme" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_HCC_PensionSchemeHC" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr10">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label19" Text="EPC Projects details" Width="600px"
                                        CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_IPF_ClientTypeIdIPF">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_IPF_ClientTypeIdIPF" Text="Client type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_cbo_IPF_ClientTypeIdIPF" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr11">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label20" Text="Real Estate details" Width="600px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_RES_Client_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_RES_Client_Type" Text="Client type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_RES_Client_Type" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="V1_tr_RES_Work_Type">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="V1_lbl_RES_Work_Type" Text="Work type" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="V1_txt_RES_Work_Type" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="tr16">
                                <td colspan="2" align="center">
                                    <asp:Label runat="server" ID="Label21" Text="Additional details" Width="600px" CssClass="labelStyleH"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="Tr12">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Date_Created" Text="Date created" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Date_Created" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr runat="server" visible="true" id="Tr13">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Created_By" Text="Created by" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Created_By" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr visible="true" id="Tr14">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Date_Updated" Text="Date updated" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Date_Updated" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>
                            <tr visible="true" id="Tr15">
                                <td valign="top" align="left">
                                    <asp:Label runat="server" ID="lbl_Tab_Updated_By" Text="Updated by" CssClass="labelStyleT"
                                        Width="300px"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:Label runat="server" ID="txt_Tab_Updated_By" Text="" Width="600px" CssClass="labelStyleV"></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
