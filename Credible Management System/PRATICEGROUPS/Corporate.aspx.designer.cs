
using AjaxControlToolkit;
using CredentialsDemo.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace CredentialsDemo.PRATICEGROUPS {
    
    
    public partial class Corporate {

        protected HtmlHead Head1;
        protected RadCodeBlock id1;
        protected HtmlForm form1;
        protected HiddenField hidName;
        protected ScriptManager ScriptManager1;
        protected Panel pnl1;
        protected Label lblname;
        protected Label lbl_Cor_Work_Type;
        protected RadTreeView RadTreeView1;
        protected Label lbl_Cor_Country_Buyer;
        protected TextBox txt_Cor_Country_Buyer;
        protected TextBoxWatermarkExtender Textboxwatermarkextender4;
        protected ImageButton img_Cor_Country_Buyer;
        protected HiddenField hid_Cor_Country_Buyer;
        protected HiddenField hid_Cor_Country_Buyer_Text;
        protected Label lbl_Cor_Country_Seller;
        protected TextBox txt_Cor_Country_Seller;
        protected TextBoxWatermarkExtender Textboxwatermarkextender5;
        protected ImageButton img_Cor_Country_Seller;
        protected HiddenField hid_Cor_Country_Seller;
        protected HiddenField hid_Cor_Country_Seller_Text;
        protected Label lbl_Cor_Country_Target;
        protected TextBox txt_Cor_Country_Target;
        protected TextBoxWatermarkExtender Textboxwatermarkextender6;
        protected ImageButton img_Cor_Country_Target;
        protected HiddenField hid_Cor_Country_Target;
        protected HiddenField hid_Cor_Country_Target_Text;
        protected Label lbl_Cor_Value_Over_US;
        protected RadComboBox cbo_Cor_Value_Over_US;
        protected Label lbl_Cor_Value_Over_Pound;
        protected RadComboBox cbo_Cor_Value_Over_Pound;
        protected Label lbl_Cor_Value_Over_Euro;
        protected RadComboBox cbo_Cor_Value_Over_Euro;
        protected Label lbl_Cor_ValueRangeEuro;
        protected Label lbl_Cor_DealAnnouncedId;
        protected RadComboBox cbo_Cor_ValueRangeEuro;
        protected RadDatePicker cld_Cor_DealAnnouncedId;
        protected Label lbl_Cor_Acting_For;
        protected Label lbl_Cor_Published_Reference;
        protected TextBox txt_Cor_Acting_For;
        protected TextBoxWatermarkExtender Textboxwatermarkextender3;
        protected ImageButton img_Cor_Acting_For;
        protected HiddenField hid_Cor_Acting_For;
        protected HiddenField hid_Cor_Acting_For_Text;
        protected TextBox txt_Cor_Published_Reference;
        protected TextBoxWatermarkExtender Textboxwatermarkextender8;
        protected Label lbl_Cor_MAStudy;
        protected Label lbl_Cor_PEClients;
        protected RadComboBox cbo_Cor_MAStudy;
        protected RadComboBox cbo_Cor_PEClients;
        protected Label lbl_Cor_QuarterDealAnnouncedId;
        protected Label lbl_Cor_QuarterDealCompletedId;
        protected RadComboBox cbo_Cor_QuarterDealAnnouncedId;
        protected RadComboBox cbo_Cor_QuarterDealCompletedId;
        protected Label lbl_Cor_YearDeal_Announced;
        protected Label lbl_Cor_YearDealCompletedId;
        protected RadComboBox cbo_Cor_YearDeal_Announced;
        protected RadComboBox cbo_Cor_YearDealCompletedId;
        protected Label Label1;
        protected Button btnOK;
        protected Button btnClear;
    }
}
