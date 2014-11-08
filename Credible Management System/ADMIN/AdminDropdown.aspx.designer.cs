using AjaxControlToolkit;
using CredentialsDemo.Common;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CredentialsDemo.ADMIN {
    
    
    public partial class AdminDropdown {

        protected HiddenField hidDisplayColumnName;
        protected HiddenField hidName;
        protected ToolkitScriptManager ToolkitScriptManager1;
        protected UpdatePanel up;
        protected HiddenField hidSPNAME;
        protected HtmlGenericControl div1;
        protected Label lblDropsAndGrids;
        protected DropDownList drpAllDropsAndGrids;
        protected Button btnReset;
        protected Button btnAdd;
        protected HtmlGenericControl divRecords;
        protected GridView gvAllDropsandGrids;
        protected Panel DivEditWindow;
        protected HtmlGenericControl IframeEdit;
        protected Button btnClose;
        protected ModalPopupExtender ModalPopupExtender1;
        protected Button ButtonEdit;
        protected ModalPopupExtender ModalPopupExtender2;
      
    }
}
