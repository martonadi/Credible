<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aLandscape.aspx.cs" Inherits="CMS.aLandscape" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <asp:Button ID="Button1" runat="server" Text="Export" OnClick="Button1_Click" />
        <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource"
            OnGridExporting="RadGrid1_GridExporting" Width="200px" AllowPaging="true">
            <MasterTableView Caption="Report test" />
            <ExportSettings Excel-Format="Html" IgnorePaging="true" ExportOnlyData="true" OpenInNewWindow="true" />
        </telerik:RadGrid>
    </div>
    </form>
</body>
</html>
