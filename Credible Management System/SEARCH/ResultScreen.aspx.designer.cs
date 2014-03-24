using AjaxControlToolkit;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace CredentialsDemo.SEARCH {
    
    
    public partial class ResultScreen {

        protected RadCodeBlock id1;
        protected RadStyleSheetManager RadStyleSheetManager1;
        protected Panel panelUpdateProgress;
        protected UpdateProgress UpdateProg1;
        protected ModalPopupExtender ModalProgress;
        protected RadScriptManager RadScriptManager1;
        protected UpdatePanel up1;
        protected HiddenField hidSheetName;
        protected HiddenField hidlstColumns;
        protected HiddenField hidScaleValue;
        protected HiddenField hidSelectedIDs;
        protected HiddenField hidAllIDs;
        protected HiddenField hidMasterIDs;
        protected HiddenField hidChildIDs;
        protected HiddenField hidSelectedValue;
        protected HiddenField hidName;
        protected HiddenField hidPageIndex;
        protected HiddenField hidB2Search;
        protected HiddenField hidOperator;
        protected HiddenField hidFilterValue;
        protected HiddenField hidSortColumn;
        protected HiddenField hidSortDirection;
        protected HiddenField hidReturnSortColumn;
        protected HiddenField hidReturnSortDirection;
        protected HiddenField hidFilterColumn;
        protected HiddenField hidFiltered;
        protected HiddenField hidBackFromView;
        protected Button btnBackToSearchT;
        protected Label lblShowFIlter;
        protected HtmlInputRadioButton Radio1;
        protected HtmlInputRadioButton Radio2;
        protected Label Label7;
        protected RadioButtonList rdoExportList;
        protected Button btnExport;
        protected HtmlGenericControl divFilter;
        protected Label lblResultCriteria;
        protected Label Label4;
        protected HiddenField hidFilter;
        protected HtmlGenericControl grdCharges;
        protected RadGrid RadGrid1;
        protected Button btnHide;
        protected ModalPopupExtender ModalPopupExtender1;
        protected Panel plnEntry;
        protected UpdatePanel UpdatePanel3;
        protected HiddenField hidSheetCount;
        protected Panel Panel1;
        protected Label Label5;
        protected RadioButtonList rdoReportStyle;
        protected HtmlGenericControl divclientname;
        protected Label lblClientName;
        protected RadioButtonList rdoExportConfidential;
        protected HtmlGenericControl divprojectname;
        protected Label lblProjectName;
        protected RadioButtonList rdoProjectName;
        protected Button btnExport2Excel;
        protected Button btnCancelEntry;
        protected HtmlGenericControl divlstColumns;
        protected RadListBox lstColumns;
    }
}
