using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;

namespace CMS
{
    public partial class aLandscape : System.Web.UI.Page
    {
        bool Export = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataTable table = new DataTable();
            for (int cols = 0; cols < 100; cols++)
                table.Columns.Add("Column" + cols);
            for (int rows = 0; rows < 100; rows++)
            {
                object[] rowdata = new object[100];
                for (int cols = 0; cols < 100; cols++)
                    rowdata[cols] = "test" + cols;
                table.Rows.Add(rowdata);
            }
            RadGrid1.DataSource = table;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Export = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

        

        protected void RadGrid1_DataBound(System.Object sender, System.EventArgs e)
        {
            if (Export == true)
            {
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.FileName = "Report Name";
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.HideStructureColumns = true;
                RadGrid1.FilterMenu.Visible = false;

                RadGrid1.HeaderContextMenu.Visible = false;
                RadGrid1.EnableHeaderContextMenu = false;
                RadGrid1.EnableHeaderContextFilterMenu = false;
                RadGrid1.AllowSorting = false;
                RadGrid1.AllowPaging = false;
                RadGrid1.AllowFilteringByColumn = false;
                RadGrid1.MasterTableView.AllowFilteringByColumn = false;
                RadGrid1.MasterTableView.AllowSorting = false;
                RadGrid1.MasterTableView.AllowPaging = false;
                RadGrid1.MasterTableView.EnableHeaderContextFilterMenu = false;
                RadGrid1.MasterTableView.EnableHeaderContextMenu = false;
            }
        }

        protected void RadGrid1_GridExporting(System.Object source, Telerik.Web.UI.GridExportingArgs e)
        {
            ExportDataGridToExcel(RadGrid1, Response);
        }

        public void ExportDataGridToExcel(System.Web.UI.Control ctrl, System.Web.HttpResponse response)
        {
            response.Clear();
            response.Buffer = true;
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("content-disposition", "attachment;filename=Flash Report.xls");
            response.Charset = "";
            this.EnableViewState = false;

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            //this.ClearControls(ctrl);
            ctrl.RenderControl(oHtmlTextWriter);

            // set content type and character set to cope with european chars like the umlaut.
            response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">" + Environment.NewLine);

            // add the style props to get the page orientation
            response.Write(AddExcelStyling());
            response.Write("<span style='font-size: 7pt; font-family: Arial Narrow;'>" + RadGrid1.MasterTableView.Caption + "</span>");
            response.Write(oStringWriter.ToString());
            response.Write("</body>");
            response.Write("</html>");

            response.End();
        }

        private string AddExcelStyling()
        {
            // add the style props to get the page orientation
            StringBuilder sb = new StringBuilder();

            sb.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office'" + Environment.NewLine + "xmlns:x='urn:schemas-microsoft-com:office:excel'" + Environment.NewLine + "xmlns='http://www.w3.org/TR/REC-html40'>" + Environment.NewLine + "<head>" + Environment.NewLine);
            sb.Append("<style>" + Environment.NewLine);
            sb.Append("@page");

            //page margin can be changed based on requirement.....
            sb.Append("{margin:.10in .10in .10in .10in;" + Environment.NewLine);
            sb.Append("mso-header-margin:.10in;" + Environment.NewLine);
            sb.Append("mso-footer-margin:.10in;" + Environment.NewLine);
            sb.Append("mso-height-source:96.75pt;" + Environment.NewLine);
            sb.Append("mso-page-orientation:landscape;}" + Environment.NewLine);

            sb.Append("</style>" + Environment.NewLine);
            sb.Append("<!--[if gte mso 9]><xml>" + Environment.NewLine);
            sb.Append("<x:ExcelWorkbook>" + Environment.NewLine);
            sb.Append("<x:ExcelWorksheets>" + Environment.NewLine);
            sb.Append("<x:ExcelWorksheet>" + Environment.NewLine);
            sb.Append("<x:Name>Flash Report</x:Name>" + Environment.NewLine);
            sb.Append("<x:WorksheetOptions>" + Environment.NewLine);
            sb.Append("<x:Print>" + Environment.NewLine);
            sb.Append("<x:ValidPrinterInfo/>" + Environment.NewLine);
            sb.Append("<x:PaperSizeIndex>9</x:PaperSizeIndex>" + Environment.NewLine);
            sb.Append("<x:HorizontalResolution>600</x:HorizontalResolution>" + Environment.NewLine);
            sb.Append("<x:VerticalResolution>600</x:VerticalResolution>" + Environment.NewLine);
            sb.Append("<x:Scale>30</x:Scale>" + Environment.NewLine);
            sb.Append("</x:Print>" + Environment.NewLine);
            sb.Append("<x:Selected/>" + Environment.NewLine);
            sb.Append("<x:DoNotDisplayGridlines/>" + Environment.NewLine);
            sb.Append("<x:ProtectContents>False</x:ProtectContents>" + Environment.NewLine);
            sb.Append("<x:ProtectObjects>False</x:ProtectObjects>" + Environment.NewLine);
            sb.Append("<x:ProtectScenarios>False</x:ProtectScenarios>" + Environment.NewLine);
            sb.Append("</x:WorksheetOptions>" + Environment.NewLine);
            sb.Append("</x:ExcelWorksheet>" + Environment.NewLine);
            sb.Append("</x:ExcelWorksheets>" + Environment.NewLine);
            sb.Append("<x:WindowHeight>12780</x:WindowHeight>" + Environment.NewLine);
            sb.Append("<x:WindowWidth>19035</x:WindowWidth>" + Environment.NewLine);
            sb.Append("<x:WindowTopX>0</x:WindowTopX>" + Environment.NewLine);
            sb.Append("<x:WindowTopY>0</x:WindowTopY>" + Environment.NewLine);
            sb.Append("<x:ProtectStructure>False</x:ProtectStructure>" + Environment.NewLine);
            sb.Append("<x:ProtectWindows>False</x:ProtectWindows>" + Environment.NewLine);
            sb.Append("</x:ExcelWorkbook>" + Environment.NewLine);
            sb.Append("</xml><![endif]-->" + Environment.NewLine);
            sb.Append("</head>" + Environment.NewLine);
            sb.Append("<body>" + Environment.NewLine);
            return sb.ToString();
        }
    }
}