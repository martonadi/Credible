<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="EntryDetails.aspx.cs" Inherits="CredentialsDemo.EntryDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ OutputCache Location="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <telerik:RadCodeBlock runat="server" ID="rd1">
        <script language="javascript" type="text/javascript">
            var ModalProgress = '<%= ModalProgress.ClientID %>';
            function BlockEnter(e) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if (navigator.appName == "Microsoft Internet Explorer" && key == 27) {
                    e.cancelBubble = true
                    e.returnValue = false;
                    e.preventDefault();
                }
                if (navigator.appName == "Netscape" && key == '27') {
                    sKey = e.which ? e.which : e.keyCode;
                    e.stopPropagation();
                    e.preventDefault();
                }
            }

            function maxLength(field, strid) {
                var strid = document.getElementById(strid);
                if (strid != null && strid.value.length >= 950) {
                    event.returnValue = false;
                    //alert("more than " + 10 + " chars");
                    return false;
                }
            }

            function ClearDateOpened() {
                var cld_Tab_Date_Opened = $find("<%= cld_Tab_Date_Opened.ClientID %>");
                var txtdatevalue = cld_Tab_Date_Opened.get_selectedDate(); //get_textBox() 
                var txtdate = cld_Tab_Date_Opened.get_textBox();

                if (txtdate != null) {
                    cld_Tab_Date_Opened.clear();
                    //txtdate.value = ""; 
                }
                txtdate.style.background = "#F0F8FF";
            }

            function BlockBackspace(e) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if (navigator.appName == "Microsoft Internet Explorer" && key == 8) {
                    e.cancelBubble = true
                    e.returnValue = false;
                    e.preventDefault();
                }
                if (navigator.appName == "Netscape" && key == '8') {
                    sKey = e.which ? e.which : e.keyCode;
                    e.stopPropagation();
                    e.preventDefault();
                }
            }

            function Confirm() {
                alert('Matter no already exists!!!');
            }

            function WaterMarktext(txtid, evt, vartxt) {

                //var txtid = document.getElementById(txtid);
                var td = Trim(txtid.value);

                if (td.length == 0 && evt.type == "blur") {
                    txtid.style.background = "#F0F8FF";
                    txtid.style.color = "gray"
                    txtid.value = vartxt;
                }

                if (td.length > 0 && evt.type == "blur") {
                    txtid.style.background = "#FFFFFF";
                    txtid.style.color = "black"
                    txtid.value = td;
                }

                if (td == vartxt && evt.type == "focus") {
                    txtid.style.background = "#FFFFFF";
                    txtid.style.color = "black"
                    txtid.value = "";
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

            function numberonly(e) {
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if ((key >= '48' && key <= '57') || (key == '8') || (key == '37') || (key == '38') || (key == '39') || (key == '40') || (key == '9'))
                    return true;
                else
                    return false;
            }

            function AlphaNumericDotonlyPaste(txtid) {
                var txtbox = document.getElementById(txtid);
                var pasteData = window.clipboardData.getData("Text");
                txtbox.value = pasteData.replace("'", "");
                return false;
            }

            function AlphaNumericDotonly(e, ctrl) {
                var erroricon = document.getElementById('<%= erroricon.ClientID%>');
                if (erroricon != null) {
                    erroricon.style.display = 'none';
                }
                var divstyle = document.getElementById('<%= errorinfo.ClientID%>');
                if (divstyle != null) {
                    divstyle.style.display = 'none';
                }
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if ((key >= 48 && key <= 57) || ((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32') || (key == '46')) {
                    return true;
                }
                else {

                    return false;
                }
            }

            function AlphaNumericonly(e, ctrl) {
                var erroricon = document.getElementById('<%= erroricon.ClientID%>');
                if (erroricon != null) {
                    erroricon.style.display = 'none';
                }
                var divstyle = document.getElementById('<%= errorinfo.ClientID%>');
                if (divstyle != null) {
                    divstyle.style.display = 'none';
                }
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if ((key >= 48 && key <= 57) || ((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32')) {
                    return true;
                }
                else {
                    return false;
                }
            }

            function AllowOnlyAlphabets(e) {
                var erroricon = document.getElementById('<%= erroricon.ClientID%>');
                if (erroricon != null) {
                    erroricon.style.display = 'none';
                }
                var divstyle = document.getElementById('<%= errorinfo.ClientID%>');
                if (divstyle != null) {
                    divstyle.style.display = 'none';
                }
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if (((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32')) {
                    return true;
                }
                else {
                    return false;
                }
            }

            function AllowOnlyAlphabetsSpl(e) {
                var erroricon = document.getElementById('<%= erroricon.ClientID%>');
                if (erroricon != null) {
                    erroricon.style.display = 'none';
                }
                var divstyle = document.getElementById('<%= errorinfo.ClientID%>');
                if (divstyle != null) {
                    divstyle.style.display = 'none';
                }
                if (!e) e = window.event;
                key = e.keyCode ? e.keyCode : e.which;
                if (((key >= '65') && (key <= '90')) || ((key >= '97') && (key <= '122')) || (key == '8') || (key == '32') || (key == '46') || (key == '44') || (key == '38') || (key == '39') || (key == '40') || (key == '41') || (key == '47') || (key == '92') || (key == '45') || (key == '95') || ((key >= '48') && (key <= '57'))) {
                    return true;
                }
                else {
                    return false;
                }
            }


            function ConfirmClear() {
                if (confirm("Do you want to clear all the fields ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

            function ConfirmDelete() {
                if (confirm("Do you want to delete the record ? ")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            function ConfirmSave() {
                if (confirm("Do you want to save the record ? ")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            function ConfirmUpdate() {
                if (confirm("Do you want to update the record ? ")) {
                    return true;
                }
                else {
                    return false;
                }
            }

            function ClearDate() {
                var cld_Tab_Date_Completed = $find("<%= cld_Tab_Date_Completed.ClientID %>");
                var txtdatecompvalue = cld_Tab_Date_Completed.get_selectedDate(); //get_textBox() 
                var txtdatecomp = cld_Tab_Date_Completed.get_textBox();
                var chk_Tab_ActualDate_Ongoing = document.getElementById('<%= chk_Tab_ActualDate_Ongoing.ClientID%>');
                var txt_Tab_Date_Completed = document.getElementById('<%= txt_Tab_Date_Completed.ClientID%>');

                var hid_Tab_ActualDate_Ongoing = document.getElementById('<%= hid_Tab_ActualDate_Ongoing.ClientID%>');
                var hid_Tab_Date_Completed = document.getElementById('<%= hid_Tab_Date_Completed.ClientID%>');


                if (txt_Tab_Date_Completed != null) {
                    txt_Tab_Date_Completed.value = "";
                    txt_Tab_Date_Completed.style.display = 'none';
                }
                if (txtdatecomp != null) {
                    cld_Tab_Date_Completed.clear();
                    //txtdatecomp.value = ""; 
                }
                if (hid_Tab_Date_Completed != null) {
                    hid_Tab_Date_Completed.value = "";
                }
                if (hid_Tab_ActualDate_Ongoing != null) {
                    hid_Tab_ActualDate_Ongoing.value = "0";
                }
                if (chk_Tab_ActualDate_Ongoing != null) {
                    chk_Tab_ActualDate_Ongoing.checked = false;
                }
                cld_Tab_Date_Completed.get_element().parentNode.style["display"] = "inline-block";
                txtdatecomp.style.background = "#F0F8FF";
                txt_Tab_Date_Completed.style.background = "#F0F8FF";
            }


            function checkOngoing() {

                var cld_Tab_Date_Completed = $find("<%= cld_Tab_Date_Completed.ClientID %>");

                var chk_Tab_ActualDate_Ongoing = document.getElementById('<%= chk_Tab_ActualDate_Ongoing.ClientID%>');
                var chk_Tab_ActualDate_Ongoing_1 = document.getElementById('<%= chk_Tab_ActualDate_Ongoing_1.ClientID%>');
                var txt_Tab_Date_Completed = document.getElementById('<%= txt_Tab_Date_Completed.ClientID%>');

                var hid_Tab_ActualDate_Ongoing = document.getElementById('<%= hid_Tab_ActualDate_Ongoing.ClientID%>');
                var hid_Tab_Date_Completed = document.getElementById('<%= hid_Tab_Date_Completed.ClientID%>');
                var hid_Tab_ActualDate_Ongoing_1 = document.getElementById('<%= hid_Tab_ActualDate_Ongoing_1.ClientID%>');

                if (chk_Tab_ActualDate_Ongoing.checked == true) {
                    txt_Tab_Date_Completed.style.display = 'block';
                    cld_Tab_Date_Completed.get_element().parentNode.style["display"] = "none";
                    var txtdatecomp = cld_Tab_Date_Completed.get_textBox();
                    txtdatecomp.value = "";
                    txt_Tab_Date_Completed.value = "Ongoing";
                    /*if (img_Tab_Date_Completed != null) { 
                    img_Tab_Date_Completed.style.display = 'none'; 
                    }*/
                    txt_Tab_Date_Completed.style.background = "#FFFFFF";
                    hid_Tab_ActualDate_Ongoing.value = "1";
                    hid_Tab_Date_Completed.value = "";
                    chk_Tab_ActualDate_Ongoing_1.checked = false;
                    hid_Tab_ActualDate_Ongoing_1.value = "0";
                }
                else {
                    txt_Tab_Date_Completed.value = "Select date from calendar icon or select ongoing";
                    txt_Tab_Date_Completed.style.display = 'none';
                    cld_Tab_Date_Completed.get_element().parentNode.style["display"] = "inline-block";
                    /*if (img_Tab_Date_Completed != null) { 
                    img_Tab_Date_Completed.style.display = "inline"; 
                    }*/
                    hid_Tab_ActualDate_Ongoing.value = "0";                    
                }
            }

            function checkNotKnown() {

                var cld_Tab_Date_Completed = $find("<%= cld_Tab_Date_Completed.ClientID %>");

                var chk_Tab_ActualDate_Ongoing = document.getElementById('<%= chk_Tab_ActualDate_Ongoing.ClientID%>');
                var chk_Tab_ActualDate_Ongoing_1 = document.getElementById('<%= chk_Tab_ActualDate_Ongoing_1.ClientID%>');
                var txt_Tab_Date_Completed = document.getElementById('<%= txt_Tab_Date_Completed.ClientID%>');

                var hid_Tab_ActualDate_Ongoing = document.getElementById('<%= hid_Tab_ActualDate_Ongoing.ClientID%>');
                var hid_Tab_Date_Completed = document.getElementById('<%= hid_Tab_Date_Completed.ClientID%>');
                var hid_Tab_ActualDate_Ongoing_1 = document.getElementById('<%= hid_Tab_ActualDate_Ongoing_1.ClientID%>');

                if (chk_Tab_ActualDate_Ongoing_1.checked == true) {
                    txt_Tab_Date_Completed.style.display = 'block';
                    cld_Tab_Date_Completed.get_element().parentNode.style["display"] = "none";
                    var txtdatecomp = cld_Tab_Date_Completed.get_textBox();
                    txtdatecomp.value = "";
                    txt_Tab_Date_Completed.value = "Not known";
                    /*if (img_Tab_Date_Completed != null) { 
                    img_Tab_Date_Completed.style.display = 'none'; 
                    }*/
                    txt_Tab_Date_Completed.style.background = "#FFFFFF";
                    hid_Tab_ActualDate_Ongoing_1.value = "1";
                    hid_Tab_Date_Completed.value = "";
                    hid_Tab_ActualDate_Ongoing.value = "0";                  
                    chk_Tab_ActualDate_Ongoing.checked = false;
                }
                else {
                    txt_Tab_Date_Completed.value = "Select date from calendar icon or select ongoing";
                    txt_Tab_Date_Completed.style.display = 'none';
                    cld_Tab_Date_Completed.get_element().parentNode.style["display"] = "inline-block";
                    /*if (img_Tab_Date_Completed != null) { 
                    img_Tab_Date_Completed.style.display = "inline"; 
                    }*/
                   
                    hid_Tab_ActualDate_Ongoing_1.value = "0";
                }
            }

            function ShowProcessImage() {
                var txt = document.getElementById('<%=txt_Tab_Client.ClientID%>');
                if (txt != null) {
                    txt.style.backgroundImage = 'url(images/Load.gif)';
                    txt.style.backgroundRepeat = 'no-repeat';
                    txt.style.backgroundPosition = 'right';
                }
            }

            function HideProcessImage() {
                var autocomplete = document.getElementById('<%=txt_Tab_Client.ClientID%>');
                if (autocomplete != null) {
                    autocomplete.style.backgroundImage = 'none';
                }
            }

            function ShowProcessImage2() {
                var txt = document.getElementById('<%=txt_Tab_ProjectName_Core.ClientID%>');

                if (txt != null) {
                    txt.style.backgroundImage = 'url(images/Load.gif)';
                    txt.style.backgroundRepeat = 'no-repeat';
                    txt.style.backgroundPosition = 'right';
                }
            }

            function HideProcessImage2() {
                var autocomplete = document.getElementById('<%=txt_Tab_ProjectName_Core.ClientID%>');
                if (autocomplete != null) {
                    autocomplete.style.backgroundImage = 'none';
                }

            }
            function HideProcessImage1() {
                var autocomplete = $("[id$=_txt_Tab_Client]");
                if (autocomplete != null) {
                    autocomplete.get(0).style.backgroundImage = 'none';
                }
            }

            function CheckComboValidation(cbo) {
                var combo = $find(cbo.id);
                var comboValue = combo.get_text();  
                return comboValue;
            }

            function CheckRadioValidation(rdo, flg) {
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
                        //rdo.focus();
                        return false;
                    }
                }
            }

            function GetRadioValue(rdo, flg) {

                var chk = false;
                var chkAll = rdo.getElementsByTagName("input");
                var chkValue = "";
                if (chkAll != null) {
                    for (var i = 0; i < chkAll.length; i++) {
                        if (chkAll[i].checked == true) {
                            chk = true;
                            chkValue = chkAll[i].nextSibling.innerHTML;
                            break;
                        }
                    }
                    if (chk == true) {
                        return chkValue;
                    }
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


            function validationFields(str) {

                var txt1 = document.getElementById('<%= txt_Tab_Client.ClientID%>');
                var txt2 = document.getElementById('<%= txt_Tab_ClientIndustrySector.ClientID%>');
                var txt4 = document.getElementById('<%= txt_Tab_Lead_Partner.ClientID%>');
                var txt_Tab_Team = document.getElementById('<%= txt_Tab_Team.ClientID%>');
                var lbl_Tab_Team = document.getElementById('<%= lbl_Tab_Team.ClientID%>');
                var lbl_Tab_Team_Msg = document.getElementById('<%= lbl_Tab_Team_Msg.ClientID%>');
                var txt6 = document.getElementById('<%= txt_Tab_Matter_No.ClientID%>');
                var txt7 = document.getElementById('<%= txt_Tab_TransactionIndustrySector.ClientID%>');
                var txt8 = document.getElementById('<%= txt_Tab_Project_Description.ClientID%>');

                var txt12 = document.getElementById('<%= txt_Tab_Country_Matter_Close.ClientID%>');

                var cld_Tab_Date_Opened = $find("<%= cld_Tab_Date_Opened.ClientID %>");
                var txtdatevalue = cld_Tab_Date_Opened.get_selectedDate(); //get_textBox() 
                var txtdate = cld_Tab_Date_Opened.get_textBox();

                var cld_Tab_Date_Completed = $find("<%= cld_Tab_Date_Completed.ClientID %>");
                var txtdatecompvalue = cld_Tab_Date_Completed.get_selectedDate(); //get_textBox() 
                var txtdatecomp = cld_Tab_Date_Completed.get_textBox();


                var txt_Tab_Date_Completed = document.getElementById('<%= txt_Tab_Date_Completed.ClientID%>');
                var lbl9 = document.getElementById('<%= lbl_Tab_Client.ClientID%>');
                var lbl_Tab_Client_Msg = document.getElementById('<%= lbl_Tab_Client_Msg.ClientID%>');
                var lbl10 = document.getElementById('<%= lbl_Tab_ClientIndustrySector.ClientID%>');
                var lbl_Tab_ClientIndustrySector_Msg = document.getElementById('<%= lbl_Tab_ClientIndustrySector_Msg.ClientID%>');
                var lbl11 = document.getElementById('<%= lbl_Tab_Lead_Partner.ClientID%>');
                var lbl12 = document.getElementById('<%= lbl_Tab_Matter_No.ClientID%>');
                var lbl_Tab_MatterNo_Msg = document.getElementById('<%= lbl_Tab_MatterNo_Msg.ClientID%>');
                var lbl13 = document.getElementById('<%= lbl_Tab_TransactionIndustrySector.ClientID%>');
                var lbl_Tab_TransactionIndustrySector_Msg = document.getElementById('<%= lbl_Tab_TransactionIndustrySector_Msg.ClientID%>');
                var lbl14 = document.getElementById('<%= lbl_Tab_Project_Description.ClientID%>');
                var lbl_Tab_Project_Description_Msg = document.getElementById('<%= lbl_Tab_Project_Description_Msg.ClientID%>');


                var lbl16 = document.getElementById('<%= lbl_Tab_Country_Matter_Close.ClientID%>');
                var lbl_Tab_Country_Matter_Close_Msg = document.getElementById('<%= lbl_Tab_Country_Matter_Close_Msg.ClientID%>');

                var lbl18 = document.getElementById('<%= lbl_Tab_Date_Opened.ClientID%>');
                var lbl_Tab_Date_Opened_Msg = document.getElementById('<%= lbl_Tab_Date_Opened_Msg.ClientID%>');

                //var cbo_Tab_ClientDescription_Language = document.getElementById('<//%=cbo_Tab_ClientDescription_Language.ClientID%>');
                var cbo1 = document.getElementById('<%=cbo_Tab_Contentious_IRG.ClientID%>');
                var cbo2 = document.getElementById('<%=cbo_Tab_Credential_Status.ClientID%>');
                var cbo3 = document.getElementById('<%=cbo_Tab_Credential_Version.ClientID%>');
                var cbo4 = document.getElementById('<%=cbo_Tab_Credential_Type.ClientID%>');

                var txt_Tab_Country_Matter_Openval = document.getElementById('<%=txt_Tab_Country_Matter_Open.ClientID%>');
                // var cbo_Tab_Country_Lawval = document.getElementById('<//%=cbo_Tab_Country_Law.ClientID%>');
                var lbl1 = document.getElementById('<%=lbl_Tab_Contentious_IRG.ClientID%>');
                var lbl_Tab_Contentious_IRG_Msg = document.getElementById('<%=lbl_Tab_Contentious_IRG_Msg.ClientID%>');
                var lbl2 = document.getElementById('<%=lbl_Tab_Credential_Status.ClientID%>');
                var lbl_Tab_Credential_Status_Msg = document.getElementById('<%= lbl_Tab_Credential_Status_Msg.ClientID%>');
                var lbl3 = document.getElementById('<%=lbl_Tab_Credential_Version.ClientID%>');
                var lbl_Tab_Credential_Version_Msg = document.getElementById('<%= lbl_Tab_Credential_Version_Msg.ClientID%>');
                var lbl4 = document.getElementById('<%=lbl_Tab_Credential_Type.ClientID%>');
                var lbl_Tab_Credential_Type_Msg = document.getElementById('<%= lbl_Tab_Credential_Type_Msg.ClientID%>');

                var lbl7 = document.getElementById('<%=lbl_Tab_Country_Matter_Open.ClientID%>');
                var lbl_Tab_Country_Matter_Open_Msg = document.getElementById('<%=lbl_Tab_Country_Matter_Open_Msg.ClientID%>');
                //var lbl8 = document.getElementById('<//%=lbl_Tab_Country_Law.ClientID%>');
                //var lbl_Tab_Country_Law_Msg = document.getElementById('<//%=lbl_Tab_Country_Law_Msg.ClientID%>');

                var rdo1 = document.getElementById('<%=rdo_Tab_Client_Name_Confidential.ClientID%>');
                var rdo2 = document.getElementById('<%=rdo_Tab_Value_Confidential.ClientID%>');
                var rdo3 = document.getElementById('<%=rdo_Tab_Client_Matter_Confidential.ClientID%>');

                var lblrdo1 = document.getElementById('<%=lbl_Tab_Client_Name_Confidential.ClientID%>');
                var lbl_Tab_Client_Name_Confidential_Msg = document.getElementById('<%=lbl_Tab_Client_Name_Confidential_Msg.ClientID%>');
                var lblrdo3 = document.getElementById('<%=lbl_Tab_Client_Matter_Confidential.ClientID%>');
                var lbl_Tab_Client_Matter_Confidential_Msg = document.getElementById('<%=lbl_Tab_Client_Matter_Confidential_Msg.ClientID%>');

                var chKBAIFjs = document.getElementById('<%=chKBAIF.ClientID%>');
                var chKCRDjs = document.getElementById('<%=chKCRD.ClientID%>');
                var chKCorpjs = document.getElementById('<%=chKCorp.ClientID%>');
                var chKCorpTaxjs = document.getElementById('<%=chkCorpTax.ClientID%>');
                var chkEPCjs = document.getElementById('<%=chkEPC.ClientID%>');
                var chkEPCEjs = document.getElementById('<%=chkEPCE.ClientID%>');
                var chkIPFjs = document.getElementById('<%=chkIPF.ClientID%>');
                var chkREjs = document.getElementById('<%=chkRE.ClientID%>');
                var chkHCjs = document.getElementById('<%=chkHC.ClientID%>');

                var divnameconifendentialrdo = document.getElementById('divnameconifendential');
                var divmatterconfidentialrdo = document.getElementById('divmatterconfidential');

                // var cbo_Tab_Language = document.getElementById('<//%=cbo_Tab_Language.ClientID%>');
                // var cbo_Tab_ClientDescription_Language = document.getElementById('<//%=cbo_Tab_ClientDescription_Language.ClientID%>');
                var lbl_Tab_Date_Completed = document.getElementById('<%=lbl_Tab_Date_Completed.ClientID%>');
                var lbl_Tab_Date_Completed_Msg = document.getElementById('<%=lbl_Tab_Date_Completed_Msg.ClientID%>');

                var hidPartial = document.getElementById('<%=hidPartial.ClientID%>');
                if (hidPartial != null) {
                    hidPartial.value = "0";
                }

                if (txt1 != null && Trim(txt1.value) == "Insert client name in full") {
                    lbl_Tab_Client_Msg.style.display = 'inline';
                    txt1.style.borderColor = "red";
                    lbl9.style.color = "red";
                    txt1.focus();
                    return false;
                }
                txt1.style.borderColor = "gray";
                lbl9.style.color = "";
                lbl_Tab_Client_Msg.style.display = 'none';


                if (rdo1 != null) {
                    if (CheckRadioValidation(rdo1, false) == false) {
                        lbl_Tab_Client_Name_Confidential_Msg.style.display = 'inline-table';
                        if (divnameconifendentialrdo != null) {
                            divnameconifendentialrdo.style.borderColor = "red";
                            divnameconifendentialrdo.style.borderWidth = "1px";
                            divnameconifendentialrdo.style.borderStyle = "solid";
                            divnameconifendentialrdo.style.display = "inline-table";
                            rdo1.focus();
                        }
                        if (lblrdo1 != null) {
                            lblrdo1.style.color = "red";

                        }
                        return false;
                    }

                    if (divnameconifendentialrdo != null) {
                        divnameconifendentialrdo.style.borderColor = "";
                        divnameconifendentialrdo.style.borderWidth = "";
                        divnameconifendentialrdo.style.borderStyle = "";
                    }
                    if (lblrdo1 != null) {
                        lblrdo1.style.color = "";
                        lbl_Tab_Client_Name_Confidential_Msg.style.display = 'none';
                    }

                    if (GetRadioValue(rdo1, false) == "Yes") {
                        var txtdesc = document.getElementById('<%= txt_Tab_ClientDescription.ClientID%>');
                        var lbldesc = document.getElementById('<%= lbl_Tab_ClientDescription.ClientID%>');
                        var lbl_Tab_ClientDescription_Msg = document.getElementById('<%= lbl_Tab_ClientDescription_Msg.ClientID%>');
                        if (txtdesc != null && lbldesc != null) {
                            if (txtdesc != null && Trim(txtdesc.value) == "Eg. a leading retail bank, an international IT company etc") {
                                lbl_Tab_ClientDescription_Msg.style.display = 'inline';
                                txtdesc.style.borderColor = "red";
                                lbldesc.style.color = "red";
                                txtdesc.focus();
                                return false;
                            }

                            txtdesc.style.borderColor = "gray";
                            lbldesc.style.color = "";
                            lbl_Tab_ClientDescription_Msg.style.display = 'none';
                        }
                        var divnamecompletionrdo = document.getElementById('<%=divnamecompletion.ClientID %>');
                        var rdonameconfidential = document.getElementById('<%= rdo_Tab_NameConfidential_Completion.ClientID%>');
                        var lblnameconfidential = document.getElementById('<%= lbl_Tab_NameConfidential_Completion.ClientID%>');
                        var lbl_Tab_NameConfidential_Completion_Msg = document.getElementById('<%= lbl_Tab_NameConfidential_Completion_Msg.ClientID%>');
                        if (divnamecompletionrdo != null && rdonameconfidential != null && lblnameconfidential != null) {
                            if (CheckRadioValidation(rdonameconfidential, false) == false) {
                                lbl_Tab_NameConfidential_Completion_Msg.style.display = 'inline-table';
                                divnamecompletionrdo.style.borderColor = "red";
                                divnamecompletionrdo.style.borderWidth = "1px";
                                divnamecompletionrdo.style.borderStyle = "solid";
                                divnamecompletionrdo.style.display = "inline-table";
                                lblnameconfidential.style.color = "red";
                                rdonameconfidential.focus();
                                return false;
                            }
                            divnamecompletionrdo.style.borderColor = "";
                            divnamecompletionrdo.style.borderWidth = "";
                            divnamecompletionrdo.style.borderStyle = "";
                            lblnameconfidential.style.color = "";
                            lbl_Tab_NameConfidential_Completion_Msg.style.display = 'none';
                        }
                        /*cbo_Tab_ClientDescription_Language */
                        /*if (CheckComboValidation(cbo_Tab_ClientDescription_Language) != "English") {
                        var txt_Tab_ClientDescription_OtherLanguage = document.getElementById('<//%= txt_Tab_ClientDescription_OtherLanguage.ClientID%>');
                        var lbl_Tab_ClientDescription_OtherLanguage = document.getElementById('<//%= lbl_Tab_ClientDescription_OtherLanguage.ClientID%>');
                        var lbl_Tab_ClientDescription_OtherLanguage_Msg = document.getElementById('<//%= lbl_Tab_ClientDescription_OtherLanguage_Msg.ClientID%>');

                        if (txt_Tab_ClientDescription_OtherLanguage != null && Trim(txt_Tab_ClientDescription_OtherLanguage.value) == "") {
                        lbl_Tab_ClientDescription_OtherLanguage_Msg.style.display = 'inline';
                        txt_Tab_ClientDescription_OtherLanguage.style.borderColor = "red";
                        lbl_Tab_ClientDescription_OtherLanguage_Msg.style.color = "red";
                        txt_Tab_ClientDescription_OtherLanguage.focus();
                        return false;
                        }


                        txt_Tab_ClientDescription_OtherLanguage.style.borderColor = "gray";
                        lbl_Tab_ClientDescription_OtherLanguage.style.color = "";
                        lbl_Tab_ClientDescription_OtherLanguage_Msg.style.display = 'none';
                        }*/

                    }
                }

                if (txt2 != null && lbl10 != null) {
                    if (txt2 != null && Trim(txt2.value) == "Select the sector of the client company from look up") {
                        lbl_Tab_ClientIndustrySector_Msg.style.display = "inline";
                        lbl10.style.color = "red";
                        txt2.disabled = false;
                        txt2.focus();
                        txt2.disabled = true;
                        return false;
                    }
                    txt2.style.borderColor = "gray";
                    lbl10.style.color = "";
                    lbl_Tab_ClientIndustrySector_Msg.style.display = "none";
                }

                var lbl_Tab_PracticeGroup = document.getElementById('<%=lbl_Tab_PracticeGroup.ClientID %>');
                if (chKBAIFjs != null && chKCRDjs != null && chKCorpjs != null && chkEPCjs != null && chkEPCEjs != null && chkIPFjs != null && chkREjs != null && chkHCjs != null && chKCorpTaxjs != null) {
                    if (chKBAIFjs.checked == false && chKCRDjs.checked == false && chKCorpjs.checked == false && chkEPCjs.checked == false && chkEPCEjs.checked == false && chkIPFjs.checked == false && chkREjs.checked == false && chkHCjs.checked == false && chKCorpTaxjs.checked == false) {

                        lbl_Tab_PracticeGroup.style.display = 'inline';
                        alert("Please select atleast on practice group");
                        chKBAIFjs.focus();
                        return false;
                    }
                    lbl_Tab_PracticeGroup.style.display = 'none';
                }

                if (txt6 != null && lbl12 != null) {
                    if (txt6 != null && Trim(txt6.value) == "Eg. 123456.00001") {
                        lbl_Tab_MatterNo_Msg.style.display = "inline";
                        txt6.style.borderColor = "red";
                        lbl12.style.color = "red";
                        txt6.focus();
                        return false;
                    }
                    txt6.style.borderColor = "gray";
                    lbl12.style.color = "";
                    lbl_Tab_MatterNo_Msg.style.display = "none";
                }

                if (txtdate != null && lbl18 != null) {
                    if (txtdate != null && Trim(txtdate.value) == "") {
                        lbl_Tab_Date_Opened_Msg.style.display = "inline";
                        txtdate.style.borderColor = "red";
                        lbl18.style.color = "red";
                        //txtdate.disabled = false; 
                        txtdate.focus();
                        //txtdate.disabled = true; 
                        return false;
                    }
                    else {
                        /* var hid = document.getElementById('<//%= hid_Tab_Date_Opened.ClientID%>'); 
                        hid.value = txtdate.value;*/
                    }
                    txtdate.style.borderColor = "gray";
                    lbl18.style.color = "";
                    lbl_Tab_Date_Opened_Msg.style.display = "none";
                }
                if (txt_Tab_Date_Completed != null && lbl_Tab_Date_Completed != null) {
                    if (txt_Tab_Date_Completed != null && Trim(txt_Tab_Date_Completed.value) == "Select date from calendar icon or select ongoing" && txtdatecomp.value == "") {

                        lbl_Tab_Date_Completed_Msg.style.display = "inline";
                        //txt_Tab_Date_Completed.style.borderColor = "red"; 
                        lbl_Tab_Date_Completed.style.color = "red";
                        //txt_Tab_Date_Completed.disabled = false; 
                        txtdatecomp.focus();
                        // txt_Tab_Date_Completed.disabled = true; 
                        return false;
                    }

                    txtdatecomp.style.borderColor = "gray";
                    lbl_Tab_Date_Completed.style.color = "";
                    lbl_Tab_Date_Completed_Msg.style.display = "none";
                }


                var objFromDate = txtdate;
                var objToDate = txtdatecomp;
                if (objFromDate != null && objToDate != null && txt_Tab_Date_Completed.value != "Ongoing") {
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
                        alert('Date matter completed should be less than the date matter opened');
                        lbl_Tab_Date_Completed_Msg.style.display = "inline";
                        //txt_Tab_Date_Completed.style.borderColor = "red"; 
                        lbl_Tab_Date_Completed.style.color = "red";
                        txt_Tab_Date_Completed.disabled = false;
                        //txt_Tab_Date_Completed.focus(); 
                        txt_Tab_Date_Completed.disabled = true;
                        return false;
                    }
                }



                txt_Tab_Date_Completed.style.borderColor = "gray";
                lbl_Tab_Date_Completed.style.color = "";
                lbl_Tab_Date_Completed_Msg.style.display = "none";

                if (txt7 != null && lbl13 != null) {
                    if (txt7 != null && Trim(txt7.value) == "Select the sector the matter relates to (not worktype) from look up") {
                        lbl_Tab_TransactionIndustrySector_Msg.style.display = 'inline';
                        txt7.style.borderColor = "red";
                        lbl13.style.color = "red";
                        txt7.disabled = false;
                        txt7.focus();
                        txt7.disabled = true;
                        return false;
                    }
                    txt7.style.borderColor = "gray";
                    lbl13.style.color = "";
                    lbl_Tab_TransactionIndustrySector_Msg.style.display = 'none';
                }
                var lbl_Tab_Project_Description_Msg = document.getElementById('<%=lbl_Tab_Project_Description_Msg.ClientID%>');
                //if (CheckComboValidation(cbo_Tab_Language) == "English")

                if (txt8 != null && Trim(txt8.value) == "Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion.") {
                    lbl_Tab_Project_Description_Msg.style.display = 'inline';
                    txt8.style.borderColor = "red";
                    lbl14.style.color = "red";
                    txt8.focus();
                    return false;
                }

                txt8.style.borderColor = "gray";
                lbl14.style.color = "";
                lbl_Tab_Project_Description_Msg.style.display = 'none';

                /*if (CheckComboValidation(cbo_Tab_Language) != "English") {
                var txt15 = document.getElementById('<//%=txt_Tab_ProjectDescription_Polish.ClientID%>');
                var lblPolish = document.getElementById('<//%=lbl_Tab_ProjectDescription_Polish.ClientID%>');
                var lbl_Tab_ProjectDescription_Polish_Msg = document.getElementById('<//%=lbl_Tab_ProjectDescription_Polish_Msg.ClientID%>');
                if (txt15 != null && Trim(txt15.value) == "") {
                txt15.style.borderColor = "red";
                lblPolish.style.color = "red";
                lbl_Tab_ProjectDescription_Polish_Msg.style.display = 'inline';
                txt15.focus();
                return false;
                }
                if (txt15 != null && lblPolish != null) {
                txt15.style.borderColor = "gray";
                lblPolish.style.color = "";
                lbl_Tab_ProjectDescription_Polish_Msg.style.display = 'none';
                }
                if (txt8 != null && Trim(txt8.value) == "") {
                txt8.style.borderColor = "red";
                lbl_Tab_Project_Description_Msg.style.display = 'inline';
                lbl14.style.color = "red";
                txt8.focus();
                return false;
                }
                txt8.style.borderColor = "gray";
                lbl14.style.color = "";
                lbl_Tab_Project_Description_Msg.style.display = 'none';
                }*/
                if (rdo3 != null) {
                    if (CheckRadioValidation(rdo3, false) == false) {
                        lbl_Tab_Client_Matter_Confidential_Msg.style.display = 'inline';
                        if (divmatterconfidential != null) {
                            divmatterconfidential.style.borderColor = "red";
                            divmatterconfidential.style.borderWidth = "1px";
                            divmatterconfidential.style.borderStyle = "solid";

                        }
                        if (lblrdo3 != null) {
                            lblrdo3.style.color = "red";
                            rdo3.focus();
                        }

                        return false;
                    }
                    if (divmatterconfidential != null && lblrdo3 != null) {
                        divmatterconfidential.style.borderColor = "";
                        divmatterconfidential.style.borderWidth = "";
                        divmatterconfidential.style.borderStyle = "";

                    }
                    lblrdo3.style.color = "";
                    lbl_Tab_Client_Matter_Confidential_Msg.style.display = 'none';
                    if (GetRadioValue(rdo3, false) == "Yes") {
                        var divmattercompletionrdo = document.getElementById('<%=divmattercompletion.ClientID %>');
                        var rdomatterconfidential = document.getElementById('<%= rdo_Tab_MatterConfidential_Completion.ClientID%>');
                        var lblmatterconfidential = document.getElementById('<%= lbl_Tab_MatterConfidential_Completion.ClientID%>');
                        var lbl_Tab_MatterConfidential_Completion_Msg = document.getElementById('<%= lbl_Tab_MatterConfidential_Completion_Msg.ClientID%>');
                        if (CheckRadioValidation(rdomatterconfidential, false) == false) {
                            lbl_Tab_MatterConfidential_Completion_Msg.style.display = 'inline';
                            if (divmattercompletionrdo != null) {
                                divmattercompletionrdo.style.borderColor = "red";
                                divmattercompletionrdo.style.borderWidth = "1px";
                                divmattercompletionrdo.style.borderStyle = "solid";
                                divmattercompletionrdo.style.display = "inline-table";
                            }
                            if (lblmatterconfidential != null) {
                                lblmatterconfidential.style.color = "red";
                                rdomatterconfidential.focus();
                            }
                            return false;
                        }
                        if (divmattercompletionrdo != null && lblmatterconfidential != null) {
                            divmattercompletionrdo.style.borderColor = "";
                            divmattercompletionrdo.style.borderWidth = "";
                            divmattercompletionrdo.style.borderStyle = "";

                        }
                        lblmatterconfidential.style.color = "";
                        lbl_Tab_MatterConfidential_Completion_Msg.style.display = 'none';
                    }

                }

                /*if (cbo_Tab_Country_Lawval != null) {
                if (CheckComboValidation(cbo_Tab_Country_Lawval) == "Select") {
                lbl_Tab_Country_Law_Msg.style.display = 'inline';
                cbo_Tab_Country_Lawval.focus();
                if (lbl8 != null) {
                lbl8.style.color = "red";
                }
                return false;
                }
                if (lbl8 != null) {
                lbl8.style.color = "";
                lbl_Tab_Country_Law_Msg.style.display = 'none';
                }*/
                /*if (CheckComboValidation(cbo_Tab_Country_Lawval) == "Other") {
                var Label50 = document.getElementById('<//%= Label50.ClientID%>');
                var txt_Tab_Country_Law_Other = document.getElementById('<//%= txt_Tab_Country_Law_Other.ClientID%>');
                var lbl_Tab_Country_Law_Other = document.getElementById('<//%= lbl_Tab_Country_Law_Other.ClientID%>');
                Label50.style.display = 'inline';
                if (txt_Tab_Country_Law_Other != null && Trim(txt_Tab_Country_Law_Other.value) == "") {
                txt_Tab_Country_Law_Other.focus();
                if (lbl_Tab_Country_Law_Other != null) {
                lbl_Tab_Country_Law_Other.style.color = "red";
                }
                return false;
                }
                if (lbl_Tab_Country_Law_Other != null) {
                lbl_Tab_Country_Law_Other.style.color = "";
                Label50.style.display = 'none';
                }

                }
                }*/
                if (txt_Tab_Country_Matter_Openval != null) {
                    if (txt_Tab_Country_Matter_Openval != null && Trim(txt_Tab_Country_Matter_Openval.value) == "Select the country where matter opened from look up") {
                        lbl_Tab_Country_Matter_Open_Msg.style.display = 'inline';
                        txt_Tab_Country_Matter_Openval.style.borderColor = "red";
                        txt_Tab_Country_Matter_Openval.disabled = false;
                        txt_Tab_Country_Matter_Openval.focus();
                        txt_Tab_Country_Matter_Openval.disabled = true;
                        if (lbl7 != null) {
                            lbl7.style.color = "red";
                        }
                        return false;
                    }
                    if (lbl7 != null) {
                        lbl7.style.color = "";
                    }
                    lbl_Tab_Country_Matter_Open_Msg.style.display = 'none';
                    txt_Tab_Country_Matter_Openval.style.borderColor = "gray";
                }
                if (txt12 != null && lbl16 != null) {
                    if (txt12 != null && Trim(txt12.value) == "Select the country(s) of the matter/transaction from look up") {
                        lbl_Tab_Country_Matter_Close_Msg.style.display = 'inline';
                        txt12.style.borderColor = "red";
                        lbl16.style.color = "red";
                        txt12.disabled = false;
                        txt12.focus();
                        txt12.disabled = true;
                        return false;
                    }
                    txt12.style.borderColor = "gray";
                    lbl16.style.color = "";
                    lbl_Tab_Country_Matter_Close_Msg.style.display = 'none';
                }

                if (cbo1 != null) {
                    if (CheckComboValidation(cbo1) == "Select") {
                        lbl_Tab_Contentious_IRG_Msg.style.display = 'inline';
                        cbo1.focus();
                        if (lbl1 != null) {
                            lbl1.style.color = "red";
                        }
                        return false;
                    }
                    if (lbl1 != null) {
                        lbl1.style.color = "";
                    }

                    lbl_Tab_Contentious_IRG_Msg.style.display = 'none';

                    /*if (CheckComboValidation(cbo1) == "Both" || CheckComboValidation(cbo1) == "Contentious") {
                    var cboTab_Dispute_Resolution = document.getElementById('<//%= cbo_Tab_Dispute_Resolution.ClientID%>');
                    var lblTab_Dispute_Resolution = document.getElementById('<//%= lbl_Tab_Dispute_Resolution.ClientID%>');
                    var lbl_Tab_Dispute_Resolution_Msg = document.getElementById('<//%= lbl_Tab_Dispute_Resolution_Msg.ClientID%>');
                    if (cboTab_Dispute_Resolution != null && lblTab_Dispute_Resolution != null) {
                    if (CheckComboValidation(cboTab_Dispute_Resolution) == "Select") {
                    lbl_Tab_Dispute_Resolution_Msg.style.display = 'inline';
                    lblTab_Dispute_Resolution.style.color = "red";
                    cboTab_Dispute_Resolution.focus();
                    return false;
                    }
                    lblTab_Dispute_Resolution.style.color = "";
                    lbl_Tab_Dispute_Resolution_Msg.style.display = 'none';
                    }
                    }*/
                }
                if (txt_Tab_Team != null && lbl_Tab_Team != null) {
                    if (txt_Tab_Team != null && Trim(txt_Tab_Team.value) == "Multi select from look up") {
                        lbl_Tab_Team_Msg.style.display = 'inline';
                        txt_Tab_Team.style.borderColor = "red";
                        lbl_Tab_Team.style.color = "red";
                        txt_Tab_Team.disabled = false;
                        txt_Tab_Team.focus();
                        txt_Tab_Team.disabled = true;
                        return false;
                    }
                    txt_Tab_Team.style.borderColor = "gray";
                    lbl_Tab_Team.style.color = "";
                    lbl_Tab_Team_Msg.style.display = 'none';
                }

                if (txt4 != null && lbl11 != null) {
                    if (txt4 != null && Trim(txt4.value) == "Multi select from look up") {
                        txt4.style.borderColor = "red";
                        lbl11.style.color = "red";
                        txt4.disabled = false;
                        txt4.focus();
                        txt4.disabled = true;
                        return false;
                    }
                    txt4.style.borderColor = "gray";
                    lbl11.style.color = "";
                }


                if (cbo2 != null && lbl2 != null) {
                    if (CheckComboValidation(cbo2) == "Select") {
                        lbl_Tab_Credential_Status_Msg.style.display = 'inline';
                        lbl2.style.color = "red";
                        cbo2.focus();
                        return false;
                    }
                    lbl2.style.color = "";
                    lbl_Tab_Credential_Status_Msg.style.display = 'none';
                }
                if (cbo3 != null && lbl3 != null) {
                    if (CheckComboValidation(cbo3) == "Select") {
                        lbl_Tab_Credential_Version_Msg.style.display = 'inline';
                        lbl3.style.color = "red";
                        cbo3.focus();
                        return false;
                    }
                    lbl3.style.color = "";
                    lbl_Tab_Credential_Version_Msg.style.display = 'none';
                } //cbo3
                if (cbo3 != null) {
                    if (CheckComboValidation(cbo3) == "Other") {
                        var txt_Tab_Credential_Version_Other = document.getElementById('<%= txt_Tab_Credential_Version_Other.ClientID%>');
                        var txt_Tab_Credential_Version_Other_Msg = document.getElementById('<%= txt_Tab_Credential_Version_Other_Msg.ClientID%>');
                        var lbl_Tab_Credential_Version_Other = document.getElementById('<%= lbl_Tab_Credential_Version_Other.ClientID%>');
                        if (txt_Tab_Credential_Version_Other != null && txt_Tab_Credential_Version_Other_Msg != null && lbl_Tab_Credential_Version_Other != null) {
                            if (txt_Tab_Credential_Version_Other != null && Trim(txt_Tab_Credential_Version_Other.value) == "") {
                                txt_Tab_Credential_Version_Other_Msg.style.display = 'inline';
                                txt_Tab_Credential_Version_Other.style.borderColor = "red";
                                lbl_Tab_Credential_Version_Other.style.color = "red";
                                txt_Tab_Team.focus();
                                return false;

                            }

                            txt_Tab_Credential_Version_Other.style.borderColor = "gray";
                            lbl_Tab_Credential_Version_Other.style.color = "";
                        }
                    }
                }

                if (cbo4 != null && lbl4 != null) {
                    if (CheckComboValidation(cbo4) == "Select") {
                        lbl_Tab_Credential_Type_Msg.style.display = 'inline';
                        lbl4.style.color = "red";
                        cbo4.focus();
                        return false;
                    }
                    lbl4.style.color = "";
                    lbl_Tab_Credential_Type_Msg.style.display = 'none';
                }

                var txt = document.getElementById('<%= txt_Tab_Date_Completed.ClientID%>');
                if (txt != null && Trim(txt.value) != "") {
                    var hid = document.getElementById('<%= hid_Tab_Date_Completed.ClientID%>');
                    if (hid != null) {
                        hid.value = txt.value;
                    }
                }

                var chk = confirm(str);
                if (chk == true) {
                    return true;
                }
                else {
                    return false;
                }
            }


            function validationPartialFields(str) {

                var txt_Tab_Date_Completed = document.getElementById('<%= txt_Tab_Date_Completed.ClientID%>');
                var txt1 = document.getElementById('<%= txt_Tab_Client.ClientID%>');
                var txt8 = document.getElementById('<%= txt_Tab_Project_Description.ClientID%>');

                var lbl9 = document.getElementById('<%= lbl_Tab_Client.ClientID%>');
                var lbl_Tab_Client_Msg = document.getElementById('<%= lbl_Tab_Client_Msg.ClientID%>');
                var lbl14 = document.getElementById('<%= lbl_Tab_Project_Description.ClientID%>');
                var lbl_Tab_Project_Description_Msg = document.getElementById('<%= lbl_Tab_Project_Description_Msg.ClientID%>');

                var rdo1 = document.getElementById('<%=rdo_Tab_Client_Name_Confidential.ClientID%>');
                var rdo2 = document.getElementById('<%=rdo_Tab_Value_Confidential.ClientID%>');
                var rdo3 = document.getElementById('<%=rdo_Tab_Client_Matter_Confidential.ClientID%>');

                var lblrdo1 = document.getElementById('<%=lbl_Tab_Client_Name_Confidential.ClientID%>');
                var lbl_Tab_Client_Name_Confidential_Msg = document.getElementById('<%=lbl_Tab_Client_Name_Confidential_Msg.ClientID%>');

                var divnameconifendentialrdo = document.getElementById('divnameconifendential');

                var hidPartial = document.getElementById('<%=hidPartial.ClientID%>');
                if (hidPartial != null) {
                    hidPartial.value = "1";
                }

                if (txt1 != null && Trim(txt1.value) == "Insert client name in full") {
                    lbl_Tab_Client_Msg.style.display = 'inline';
                    txt1.style.borderColor = "red";
                    lbl9.style.color = "red";
                    txt1.focus();
                    return false;
                }
                txt1.style.borderColor = "gray";
                lbl9.style.color = "";
                lbl_Tab_Client_Msg.style.display = 'none';


                if (rdo1 != null) {
                    if (CheckRadioValidation(rdo1, false) == false) {
                        lbl_Tab_Client_Name_Confidential_Msg.style.display = 'inline-table';
                        if (divnameconifendentialrdo != null) {
                            divnameconifendentialrdo.style.borderColor = "red";
                            divnameconifendentialrdo.style.borderWidth = "1px";
                            divnameconifendentialrdo.style.borderStyle = "solid";
                            divnameconifendentialrdo.style.display = "inline-table";
                            rdo1.focus();
                        }
                        if (lblrdo1 != null) {
                            lblrdo1.style.color = "red";

                        }
                        return false;
                    }

                    if (divnameconifendentialrdo != null) {
                        divnameconifendentialrdo.style.borderColor = "";
                        divnameconifendentialrdo.style.borderWidth = "";
                        divnameconifendentialrdo.style.borderStyle = "";
                    }
                    if (lblrdo1 != null) {
                        lblrdo1.style.color = "";
                        lbl_Tab_Client_Name_Confidential_Msg.style.display = 'none';
                    }

                    if (GetRadioValue(rdo1, false) == "Yes") {
                        var txtdesc = document.getElementById('<%= txt_Tab_ClientDescription.ClientID%>');
                        var lbldesc = document.getElementById('<%= lbl_Tab_ClientDescription.ClientID%>');
                        var lbl_Tab_ClientDescription_Msg = document.getElementById('<%= lbl_Tab_ClientDescription_Msg.ClientID%>');
                        if (txtdesc != null && lbldesc != null) {
                            if (txtdesc != null && Trim(txtdesc.value) == "Eg. a leading retail bank, an international IT company etc") {
                                lbl_Tab_ClientDescription_Msg.style.display = 'inline';
                                txtdesc.style.borderColor = "red";
                                lbldesc.style.color = "red";
                                txtdesc.focus();
                                return false;
                            }

                            txtdesc.style.borderColor = "gray";
                            lbldesc.style.color = "";
                            lbl_Tab_ClientDescription_Msg.style.display = 'none';
                        }
                        var divnamecompletionrdo = document.getElementById('<%=divnamecompletion.ClientID %>');
                        var rdonameconfidential = document.getElementById('<%= rdo_Tab_NameConfidential_Completion.ClientID%>');
                        var lblnameconfidential = document.getElementById('<%= lbl_Tab_NameConfidential_Completion.ClientID%>');
                        var lbl_Tab_NameConfidential_Completion_Msg = document.getElementById('<%= lbl_Tab_NameConfidential_Completion_Msg.ClientID%>');
                        if (divnamecompletionrdo != null && rdonameconfidential != null && lblnameconfidential != null) {
                            if (CheckRadioValidation(rdonameconfidential, false) == false) {
                                lbl_Tab_NameConfidential_Completion_Msg.style.display = 'inline-table';
                                divnamecompletionrdo.style.borderColor = "red";
                                divnamecompletionrdo.style.borderWidth = "1px";
                                divnamecompletionrdo.style.borderStyle = "solid";
                                divnamecompletionrdo.style.display = "inline-table";
                                lblnameconfidential.style.color = "red";
                                rdonameconfidential.focus();
                                return false;
                            }
                            divnamecompletionrdo.style.borderColor = "";
                            divnamecompletionrdo.style.borderWidth = "";
                            divnamecompletionrdo.style.borderStyle = "";
                            lblnameconfidential.style.color = "";
                            lbl_Tab_NameConfidential_Completion_Msg.style.display = 'none';
                        }
                    }
                }

                var lbl_Tab_Project_Description_Msg = document.getElementById('<%=lbl_Tab_Project_Description_Msg.ClientID%>');

                if (txt8 != null && Trim(txt8.value) == "Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion.") {
                    lbl_Tab_Project_Description_Msg.style.display = 'inline';
                    txt8.style.borderColor = "red";
                    lbl14.style.color = "red";
                    txt8.focus();
                    return false;
                }

                txt8.style.borderColor = "gray";
                lbl14.style.color = "";
                lbl_Tab_Project_Description_Msg.style.display = 'none';



                var cld_Tab_Date_Opened = $find("<%= cld_Tab_Date_Opened.ClientID %>");
                var cld_Tab_Date_Completed = $find("<%= cld_Tab_Date_Completed.ClientID %>");

                if (cld_Tab_Date_Opened != null && cld_Tab_Date_Completed != null) {
                    var txtdatevalue = cld_Tab_Date_Opened.get_selectedDate(); //get_textBox() 
                    var txtdate = cld_Tab_Date_Opened.get_textBox();


                    var txtdatecompvalue = cld_Tab_Date_Completed.get_selectedDate(); //get_textBox() 
                    var txtdatecomp = cld_Tab_Date_Completed.get_textBox();

                    var objFromDate = txtdate;
                    var objToDate = txtdatecomp;
                    if (objFromDate != null && objToDate != null && txt_Tab_Date_Completed.value != "Ongoing") {
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
                            alert('Date matter completed should be less than the date matter opened');
                            lbl_Tab_Date_Completed_Msg.style.display = "inline";
                            //txt_Tab_Date_Completed.style.borderColor = "red"; 
                            lbl_Tab_Date_Completed.style.color = "red";
                            txt_Tab_Date_Completed.disabled = false;
                            //txt_Tab_Date_Completed.focus(); 
                            txt_Tab_Date_Completed.disabled = true;
                            return false;
                        }
                    }
                }

                /*txt_Tab_Date_Completed.style.borderColor = "gray";
                lbl_Tab_Date_Completed.style.color = "";
                lbl_Tab_Date_Completed_Msg.style.display = "none";*/



                var chk = confirm(str);
                if (chk == true) {
                    return true;
                }
                else {
                    return false;
                }
            }


            function validationFullSubmitFields(str) {

                var txt_Tab_Date_Completed = document.getElementById('<%= txt_Tab_Date_Completed.ClientID%>');
                var txt1 = document.getElementById('<%= txt_Tab_Client.ClientID%>');
                var txt8 = document.getElementById('<%= txt_Tab_Project_Description.ClientID%>');

                var lbl9 = document.getElementById('<%= lbl_Tab_Client.ClientID%>');
                var lbl_Tab_Client_Msg = document.getElementById('<%= lbl_Tab_Client_Msg.ClientID%>');
                var lbl14 = document.getElementById('<%= lbl_Tab_Project_Description.ClientID%>');
                var lbl_Tab_Project_Description_Msg = document.getElementById('<%= lbl_Tab_Project_Description_Msg.ClientID%>');

                var rdo1 = document.getElementById('<%=rdo_Tab_Client_Name_Confidential.ClientID%>');
                var rdo2 = document.getElementById('<%=rdo_Tab_Value_Confidential.ClientID%>');
                var rdo3 = document.getElementById('<%=rdo_Tab_Client_Matter_Confidential.ClientID%>');

                var lblrdo1 = document.getElementById('<%=lbl_Tab_Client_Name_Confidential.ClientID%>');
                var lbl_Tab_Client_Name_Confidential_Msg = document.getElementById('<%=lbl_Tab_Client_Name_Confidential_Msg.ClientID%>');

                var divnameconifendentialrdo = document.getElementById('divnameconifendential');

                var hidPartial = document.getElementById('<%=hidPartial.ClientID%>');
                if (hidPartial != null) {
                    hidPartial.value = "0";
                }

                if (txt1 != null && Trim(txt1.value) == "Insert client name in full") {
                    lbl_Tab_Client_Msg.style.display = 'inline';
                    txt1.style.borderColor = "red";
                    lbl9.style.color = "red";
                    txt1.focus();
                    return false;
                }
                txt1.style.borderColor = "gray";
                lbl9.style.color = "";
                lbl_Tab_Client_Msg.style.display = 'none';


                if (rdo1 != null) {
                    if (CheckRadioValidation(rdo1, false) == false) {
                        lbl_Tab_Client_Name_Confidential_Msg.style.display = 'inline-table';
                        if (divnameconifendentialrdo != null) {
                            divnameconifendentialrdo.style.borderColor = "red";
                            divnameconifendentialrdo.style.borderWidth = "1px";
                            divnameconifendentialrdo.style.borderStyle = "solid";
                            divnameconifendentialrdo.style.display = "inline-table";
                            rdo1.focus();
                        }
                        if (lblrdo1 != null) {
                            lblrdo1.style.color = "red";

                        }
                        return false;
                    }

                    if (divnameconifendentialrdo != null) {
                        divnameconifendentialrdo.style.borderColor = "";
                        divnameconifendentialrdo.style.borderWidth = "";
                        divnameconifendentialrdo.style.borderStyle = "";
                    }
                    if (lblrdo1 != null) {
                        lblrdo1.style.color = "";
                        lbl_Tab_Client_Name_Confidential_Msg.style.display = 'none';
                    }

                    if (GetRadioValue(rdo1, false) == "Yes") {
                        var txtdesc = document.getElementById('<%= txt_Tab_ClientDescription.ClientID%>');
                        var lbldesc = document.getElementById('<%= lbl_Tab_ClientDescription.ClientID%>');
                        var lbl_Tab_ClientDescription_Msg = document.getElementById('<%= lbl_Tab_ClientDescription_Msg.ClientID%>');
                        if (txtdesc != null && lbldesc != null) {
                            if (txtdesc != null && Trim(txtdesc.value) == "Eg. a leading retail bank, an international IT company etc") {
                                lbl_Tab_ClientDescription_Msg.style.display = 'inline';
                                txtdesc.style.borderColor = "red";
                                lbldesc.style.color = "red";
                                txtdesc.focus();
                                return false;
                            }

                            txtdesc.style.borderColor = "gray";
                            lbldesc.style.color = "";
                            lbl_Tab_ClientDescription_Msg.style.display = 'none';
                        }
                        var divnamecompletionrdo = document.getElementById('<%=divnamecompletion.ClientID %>');
                        var rdonameconfidential = document.getElementById('<%= rdo_Tab_NameConfidential_Completion.ClientID%>');
                        var lblnameconfidential = document.getElementById('<%= lbl_Tab_NameConfidential_Completion.ClientID%>');
                        var lbl_Tab_NameConfidential_Completion_Msg = document.getElementById('<%= lbl_Tab_NameConfidential_Completion_Msg.ClientID%>');
                        if (divnamecompletionrdo != null && rdonameconfidential != null && lblnameconfidential != null) {
                            if (CheckRadioValidation(rdonameconfidential, false) == false) {
                                lbl_Tab_NameConfidential_Completion_Msg.style.display = 'inline-table';
                                divnamecompletionrdo.style.borderColor = "red";
                                divnamecompletionrdo.style.borderWidth = "1px";
                                divnamecompletionrdo.style.borderStyle = "solid";
                                divnamecompletionrdo.style.display = "inline-table";
                                lblnameconfidential.style.color = "red";
                                rdonameconfidential.focus();
                                return false;
                            }
                            divnamecompletionrdo.style.borderColor = "";
                            divnamecompletionrdo.style.borderWidth = "";
                            divnamecompletionrdo.style.borderStyle = "";
                            lblnameconfidential.style.color = "";
                            lbl_Tab_NameConfidential_Completion_Msg.style.display = 'none';
                        }
                    }
                }

                var lbl_Tab_Project_Description_Msg = document.getElementById('<%=lbl_Tab_Project_Description_Msg.ClientID%>');

                if (txt8 != null && Trim(txt8.value) == "Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion.") {
                    lbl_Tab_Project_Description_Msg.style.display = 'inline';
                    txt8.style.borderColor = "red";
                    lbl14.style.color = "red";
                    txt8.focus();
                    return false;
                }

                txt8.style.borderColor = "gray";
                lbl14.style.color = "";
                lbl_Tab_Project_Description_Msg.style.display = 'none';



                var cld_Tab_Date_Opened = $find("<%= cld_Tab_Date_Opened.ClientID %>");
                var cld_Tab_Date_Completed = $find("<%= cld_Tab_Date_Completed.ClientID %>");

                if (cld_Tab_Date_Opened != null && cld_Tab_Date_Completed != null) {
                    var txtdatevalue = cld_Tab_Date_Opened.get_selectedDate(); //get_textBox() 
                    var txtdate = cld_Tab_Date_Opened.get_textBox();


                    var txtdatecompvalue = cld_Tab_Date_Completed.get_selectedDate(); //get_textBox() 
                    var txtdatecomp = cld_Tab_Date_Completed.get_textBox();

                    var objFromDate = txtdate;
                    var objToDate = txtdatecomp;
                    if (objFromDate != null && objToDate != null && txt_Tab_Date_Completed.value != "Ongoing") {
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
                            alert('Date matter completed should be less than the date matter opened');
                            lbl_Tab_Date_Completed_Msg.style.display = "inline";
                            //txt_Tab_Date_Completed.style.borderColor = "red"; 
                            lbl_Tab_Date_Completed.style.color = "red";
                            txt_Tab_Date_Completed.disabled = false;
                            //txt_Tab_Date_Completed.focus(); 
                            txt_Tab_Date_Completed.disabled = true;
                            return false;
                        }
                    }
                }

                /*txt_Tab_Date_Completed.style.borderColor = "gray";
                lbl_Tab_Date_Completed.style.color = "";
                lbl_Tab_Date_Completed_Msg.style.display = "none";*/



                var chk = confirm(str);
                if (chk == true) {
                    return true;
                }
                else {
                    return false;
                }
            }


            function LoadCountryChild(txt, id, hid) {
                var Return;
                var myArguments = new Object();
                var hidcheck = document.getElementById(hid);
                var qstr = txt + "~" + id + "~" + hidcheck.value;
                /*myArguments.param1 = chkControl.value;*/
                Return = window.showModalDialog("LOOKUPS/CountryLookUpGrid.aspx?q=" + qstr + "~" + new Date().getTime(), myArguments, "dialogWidth:425px;dialogHeight:427px;")
                if (Return != undefined) {
                    var str = Return.info.split('~~'); /*lbl_Tab_Language_Of_Dispute tr_Tab_Language_Of_Dispute_Other*/

                    if (str[2] == "lbl_Tab_Country_PredominantCountry") {
                        var txt = document.getElementById('<%=txt_Tab_Country_PredominantCountry.ClientID%>');
                        txt.value = str[0].replace(/&amp;/g, "&");
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Eg. where head quartered";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Country_PredominantCountry_Text.ClientID%>');
                        hidtext.value = str[0];
                        var hidid = document.getElementById('<%=hid_Tab_Country_PredominantCountry.ClientID%>');
                        hidid.value = str[1];
                    }
                    if (str[2] == "lbl_Tab_Country_Matter_Close") {
                        var txt = document.getElementById('<%=txt_Tab_Country_Matter_Close.ClientID%>');
                        txt.value = str[0];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the country(s) of the matter/transaction from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Country_Matter_Close_Text.ClientID%>');
                        hidtext.value = str[0];
                        var hidid = document.getElementById('<%=hid_Tab_Country_Matter_Close.ClientID%>');
                        hidid.value = str[1];
                    }
                    if (str[2] == "lbl_Tab_Country_Jurisdiction") {
                        var txt = document.getElementById('<%=txt_Tab_Country_Jurisdiction.ClientID%>');
                        txt.value = str[0];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the country of dispute from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Country_Jurisdiction_Text.ClientID%>');
                        hidtext.value = str[0];
                        var hidid = document.getElementById('<%=hid_Tab_Country_Jurisdiction.ClientID%>');
                        hidid.value = str[1];
                    }
                    if (str[2] == "lbl_Tab_Country_OtherCMSOffice") {
                        var txt = document.getElementById('<%=txt_Tab_Country_OtherCMSOffice.ClientID%>');
                        txt.value = str[0];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Multi select from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Country_OtherCMSOffice_Text.ClientID%>');
                        hidtext.value = str[0];
                        var hidid = document.getElementById('<%=hid_Tab_Country_OtherCMSOffice.ClientID%>');
                        hidid.value = str[1];
                    }
                    if (str[2] == "lbl_Tab_Country_ArbitrationCountry") {
                        var txt = document.getElementById('<%=txt_Tab_Country_ArbitrationCountry.ClientID%>');
                        txt.value = str[0];
                        var hidtext = document.getElementById('<%=hid_Tab_Country_ArbitrationCountry_Text.ClientID%>');
                        hidtext.value = str[0];
                        var hidid = document.getElementById('<%=hid_Tab_Country_ArbitrationCountry.ClientID%>');
                        hidid.value = str[1];
                    }
                    if (str[2] == "lbl_Tab_Country_Matter_Open") {
                        var txt = document.getElementById('<%=txt_Tab_Country_Matter_Open.ClientID%>');
                        txt.value = str[0];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the country where matter opened from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Country_Matter_Open_Text.ClientID%>');
                        hidtext.value = str[0];
                        var hidid = document.getElementById('<%=hid_Tab_Country_Matter_Open.ClientID%>');
                        hidid.value = str[1];
                    }
                }
            }
            function LoadChild(txt, id, hid) {
                var Return;
                var myArguments = new Object();
                var hidcheck = document.getElementById(hid);
                var qstr = txt + "~" + id + "~" + hidcheck.value;
                /*myArguments.param1 = chkControl.value;*/
                Return = window.showModalDialog("LOOKUPS/LookUpGrid.aspx?q=" + qstr + "~" + new Date().getTime(), myArguments, "dialogWidth:425px;dialogHeight:427px;")
                if (Return != undefined) {
                    var str = Return.info.split('~~');
                    if (str[2] == "lbl_Tab_Language_Of_Dispute") {
                        var txt = document.getElementById('<%=txt_Tab_Language_Of_Dispute.ClientID%>');
                        txt.value = str[1].replace(/&amp;/g, "&");
                        var strother = str[1];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the language of dispute from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Language_Of_Dispute_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Language_Of_Dispute.ClientID%>');
                        hidid.value = str[0];

                        var n = strother.indexOf("Other");
                        if (n > -1) {

                            var tr_Tab_Language_Of_Dispute_Other = document.getElementById('<%=tr_Tab_Language_Of_Dispute_Other.ClientID%>');
                            tr_Tab_Language_Of_Dispute_Other.style.display = 'block';
                            var hid_Tab_Language_Of_Dispute_Other = document.getElementById('<%=hid_Tab_Language_Of_Dispute_Other.ClientID%>');
                            hid_Tab_Language_Of_Dispute_Other.value = "1";
                            /*var hid_Tab_Language_Of_Dispute_Other_Text = document.getElementById('<%=hid_Tab_Language_Of_Dispute_Other_Text.ClientID%>');
                            var txt_Tab_Language_Of_Dispute_Other = document.getElementById('<%=txt_Tab_Language_Of_Dispute_Other.ClientID%>');
                            hid_Tab_Language_Of_Dispute_Other_Text.value = txt_Tab_Language_Of_Dispute_Other.value;*/
                        }
                    }
                    if (str[2] == "lbl_Tab_Client_Industry_Type") {
                        var txt = document.getElementById('<%=txt_Tab_Client_Industry_Type.ClientID%>');
                        //txt.value = str[1];
                        txt.value = str[1].replace(/&amp;/g, "&");
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the sub-sector of the client company from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Client_Industry_Type_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Client_Industry_Type.ClientID%>');
                        hidid.value = str[0];
                    }
                    if (str[2] == "lbl_Tab_ClientIndustrySector") {
                        var txt = document.getElementById('<%=txt_Tab_ClientIndustrySector.ClientID%>');
                        //txt.value = str[1];
                        txt.value = str[1].replace(/&amp;/g, "&");
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the sector of the client company from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_ClientIndustrySector_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_ClientIndustrySector.ClientID%>');
                        hidid.value = str[0];
                    }

                    if (str[2] == "lbl_Tab_Team") {
                        var txt = document.getElementById('<%=txt_Tab_Team.ClientID%>');
                        // txt.value = str[1];
                        txt.value = str[1].replace(/&amp;/g, "&");
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Multi select from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Team_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Team.ClientID%>');
                        hidid.value = str[0];
                    }
                    if (str[2] == "lbl_Tab_Lead_Partner") {
                        var txt = document.getElementById('<%=txt_Tab_Lead_Partner.ClientID%>');
                        txt.value = str[1];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Multi select from look up";
                        }
                        var strother = str[1];
                        var hidtext = document.getElementById('<%=hid_Tab_Lead_Partner_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Lead_Partner.ClientID%>');
                        hidid.value = str[0];
                        var hidctl = document.getElementById('<%=hid_Tab_Lead_Partner_Ctl.ClientID %>');
                        var tr_Tab_CMSPartnerName = document.getElementById('<%=tr_Tab_CMSPartnerName.ClientID%>');
                        var tr_Tab_Source_Of_Credential = document.getElementById('<%=tr_Tab_Source_Of_Credential.ClientID %>');
                        var tr_Tab_SourceOfCredential_Other = document.getElementById('<%=tr_Tab_SourceOfCredential_Other.ClientID%>');
                        //var n = strother.indexOf("Other");
                        // if (n > -1) {}
                        var n = strother.indexOf("CMS Partner");

                        if (n > -1) {
                            if (hidctl != null && tr_Tab_CMSPartnerName != null && tr_Tab_Source_Of_Credential != null) {
                                hidctl.value = "1";
                                tr_Tab_CMSPartnerName.style.display = 'inline';
                                tr_Tab_Source_Of_Credential.style.display = 'inline';

                                var txt_Tab_CMSPartnerName = document.getElementById('<%=txt_Tab_CMSPartnerName.ClientID %>');
                                var txt_Tab_Source_Of_Credential = document.getElementById('<%=txt_Tab_Source_Of_Credential.ClientID %>');
                                txt_Tab_Source_Of_Credential.value = "";
                                txt_Tab_CMSPartnerName.value = "";

                                var hid_Tab_Source_Of_Credential_Text = document.getElementById('<%=hid_Tab_Source_Of_Credential_Text.ClientID%>');
                                var hid_Tab_Source_Of_Credential = document.getElementById('<%=hid_Tab_Source_Of_Credential.ClientID%>');
                                hid_Tab_Source_Of_Credential_Text.value = "";
                                hid_Tab_Source_Of_Credential.value = "";

                                var txt_Tab_SourceOfCredential_Other = document.getElementById('<%=txt_Tab_SourceOfCredential_Other.ClientID%>');
                                txt_Tab_SourceOfCredential_Other.value = "";

                                var hidother = document.getElementById('<%=hid_Tab_SourceOfCredential_Other.ClientID%>');
                                hidother.value = "0";

                            }
                        }
                        else {
                            if (hidctl != null && tr_Tab_CMSPartnerName != null && tr_Tab_Source_Of_Credential != null && tr_Tab_SourceOfCredential_Other != null) {
                                tr_Tab_CMSPartnerName.style.display = 'none';
                                tr_Tab_Source_Of_Credential.style.display = 'none';
                                tr_Tab_SourceOfCredential_Other.style.display = 'none';
                                hidctl.value = "0";

                            }
                            var txt_Tab_CMSPartnerName = document.getElementById('<%=txt_Tab_CMSPartnerName.ClientID %>');
                            var txt_Tab_Source_Of_Credential = document.getElementById('<%=txt_Tab_Source_Of_Credential.ClientID %>');
                            txt_Tab_Source_Of_Credential.value = "";
                            txt_Tab_CMSPartnerName.value = "";

                            var hid_Tab_Source_Of_Credential_Text = document.getElementById('<%=hid_Tab_Source_Of_Credential_Text.ClientID%>');
                            var hid_Tab_Source_Of_Credential = document.getElementById('<%=hid_Tab_Source_Of_Credential.ClientID%>');
                            hid_Tab_Source_Of_Credential_Text.value = "";
                            hid_Tab_Source_Of_Credential.value = "";

                            var txt_Tab_SourceOfCredential_Other = document.getElementById('<%=txt_Tab_SourceOfCredential_Other.ClientID%>');
                            txt_Tab_SourceOfCredential_Other.value = "";

                            var hidother = document.getElementById('<%=hid_Tab_SourceOfCredential_Other.ClientID%>');
                            hidother.value = "0";
                            /*var txt_Tab_CMSPartnerName = document.getElementById('<%=txt_Tab_CMSPartnerName.ClientID %>');
                            var txt_Tab_Source_Of_Credential = document.getElementById('<%=txt_Tab_Source_Of_Credential.ClientID %>');
                            var hid_Tab_Source_Of_Credential_Text = document.getElementById('<%=hid_Tab_Source_Of_Credential_Text.ClientID%>');
                            hid_Tab_Source_Of_Credential_Text.value = "";
                            var hid_Tab_Source_Of_Credential = document.getElementById('<%=hid_Tab_Source_Of_Credential.ClientID%>');
                            hid_Tab_Source_Of_Credential.value = "";
                            if (txt_Tab_Source_Of_Credential != null && txt_Tab_CMSPartnerName != null) {
                            txt_Tab_Source_Of_Credential.value = "";
                            txt_Tab_CMSPartnerName.value = "";
                            }
                            var txt_Tab_SourceOfCredential_Other = document.getElementById('<%=txt_Tab_SourceOfCredential_Other.ClientID%>');
                            txt_Tab_SourceOfCredential_Other.value = "";
                            var hidother = document.getElementById('<%=hid_Tab_SourceOfCredential_Other.ClientID%>');
                            hidother.value = "0";*/
                        }
                    }
                    if (str[2] == "lbl_Tab_Source_Of_Credential") {
                        var txt = document.getElementById('<%=txt_Tab_Source_Of_Credential.ClientID%>');
                        txt.value = str[1];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the source of credential from look up";
                        }
                        var strother = str[1];

                        var hidtext = document.getElementById('<%=hid_Tab_Source_Of_Credential_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Source_Of_Credential.ClientID%>');
                        hidid.value = str[0];
                        var tr_Tab_SourceOfCredential_Other = document.getElementById('<%=tr_Tab_SourceOfCredential_Other.ClientID%>');
                        var txt_Tab_SourceOfCredential_Other = document.getElementById('<%=txt_Tab_SourceOfCredential_Other.ClientID%>');
                        var hidother = document.getElementById('<%=hid_Tab_SourceOfCredential_Other.ClientID%>');

                        if (hidid.value == "") {
                            hidtext.value = "";
                            txt.value = "";
                        }

                        var n = strother.indexOf("Other");
                        if (n > -1) {

                            if (tr_Tab_SourceOfCredential_Other != null && hidother != null) {
                                tr_Tab_SourceOfCredential_Other.style.display = 'inline';
                                /*var lblother1 = document.getElementById('<%=lbl_Tab_SourceOfCredential_Other.ClientID%>');
                                lblother1.style.display = 'inline';*/

                                hidother.value = "1";
                            }
                            /*else {
                            txt_Tab_SourceOfCredential_Other.value = "";
                            if (tr_Tab_SourceOfCredential_Other != null && hidother != null) {
                            tr_Tab_SourceOfCredential_Other.style.display = 'none';
                            hidother.value = "0";
                            }

                            }*/

                        }
                        else {
                            if (tr_Tab_SourceOfCredential_Other != null && hidother != null) {
                                tr_Tab_SourceOfCredential_Other.style.display = 'none';
                                txt_Tab_SourceOfCredential_Other.value = "";
                                hidother.value = "0";
                            }
                        }
                    }
                    if (str[2] == "lbl_Tab_Other_Matter_Executive") {
                        var txt = document.getElementById('<%=txt_Tab_Other_Matter_Executive.ClientID%>');
                        txt.value = str[1];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Multi select from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Other_Matter_Executive_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Other_Matter_Executive.ClientID%>');
                        hidid.value = str[0];
                    }
                    if (str[2] == "lbl_Tab_TransactionIndustrySector") {
                        var txt = document.getElementById('<%=txt_Tab_TransactionIndustrySector.ClientID%>');
                        /*txt.value = str[1];*/
                        txt.value = str[1].replace(/&amp;/g, "&");
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the sector the matter relates to (not worktype) from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_TransactionIndustrySector_Text.ClientID%>');
                        hidtext.value = str[1].replace(/&amp;/g, "&");
                        var hidid = document.getElementById('<%=hid_Tab_TransactionIndustrySector.ClientID%>');
                        hidid.value = str[0];
                    }
                    if (str[2] == "lbl_Tab_Transaction_Industry_Type") {
                        var txt = document.getElementById('<%=txt_Tab_Transaction_Industry_Type.ClientID%>');
                        txt.value = str[1];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Select the sub-sector of the matter from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Transaction_Industry_Type_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Transaction_Industry_Type.ClientID%>');
                        hidid.value = str[0];
                    }
                    if (str[2] == "lbl_Tab_Know_How") {
                        var txt = document.getElementById('<%=txt_Tab_Know_How.ClientID%>');
                        txt.value = str[1];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "For corporate deals only select relevant theme from look up";
                        }
                        var hidid = document.getElementById('<%=hid_Tab_Know_How_Text.ClientID%>');
                        hidid.value = str[1];
                        var hidtext = document.getElementById('<%=hid_Tab_Know_How.ClientID%>');
                        hidtext.value = str[0];
                    }




                    if (str[2] == "lbl_Tab_Referred_From_Other_CMS_Office") {
                        var txt = document.getElementById('<%=txt_Tab_Referred_From_Other_CMS_Office.ClientID%>');
                        txt.value = str[1];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Multi select from look up";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Referred_From_Other_CMS_Office_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Referred_From_Other_CMS_Office.ClientID%>');
                        hidid.value = str[0];
                    }

                    if (str[2] == "lbl_Tab_Other_Uses") {
                        var txt = document.getElementById('<%=txt_Tab_Other_Uses.ClientID%>');
                        txt.value = str[1];
                        var txtval = txt.value;
                        if (txtval.length > 0) {
                            txt.style.background = "#FFFFFF";
                        }
                        else {
                            txt.style.background = "#F0F8FF";
                            txt.value = "Eg. case study/directory submission select from look-up and enter docs# in other useful credential information box";
                        }
                        var hidtext = document.getElementById('<%=hid_Tab_Other_Uses_Text.ClientID%>');
                        hidtext.value = str[1];
                        var hidid = document.getElementById('<%=hid_Tab_Other_Uses.ClientID%>');
                        hidid.value = str[0];
                    }
                }
            }
            function ShowLabelEntry(img, iframe, hght, wght, pgName, plnEntry, plnEntryHght, strColor, plnEntryWght) {
                if (img != null && iframe != null && hght != null && wght != null && pgName != null && plnEntry != null && plnEntryHght != null) {
                    //img = img.split('_')[1] + '_' + img.split('_')[2];
                    switch (img) {
                        case "MainContent_lblBAIF":
                            var chk = document.getElementById('<%=chKBAIF.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;
                        case "MainContent_lblCorp":
                            var chk = document.getElementById('<%=chKCorp.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;
                        case "MainContent_lblCorpTax":
                            var chk = document.getElementById('<%=chkCorpTax.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;
                        case "MainContent_lblCRD":
                            var chk = document.getElementById('<%=chKCRD.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;
                        case "MainContent_lblEPC":
                            var chk = document.getElementById('<%=chkEPC.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;
                        case "MainContent_lblRE":
                            var chk = document.getElementById('<%=chkRE.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;
                        case "MainContent_lblIPF":
                            var chk = document.getElementById('<%=chkIPF.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;
                        case "MainContent_lblENE":
                            var chk = document.getElementById('<%=chkEPCE.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;
                        case "MainContent_lblHC":
                            var chk = document.getElementById('<%=chkHC.ClientID%>');
                            if (chk.checked != true) {
                                chk.checked = true;
                            }
                            break;

                    }
                    var plnEntry = document.getElementById(plnEntry);

                    if (plnEntry != null) {
                        plnEntry.style.height = plnEntryHght;
                        plnEntry.style.width = plnEntryWght;
                        plnEntry.style.backgroundColor = strColor;
                        var frame = $get(iframe);
                        if (frame != null) {
                            frame.src = "PRATICEGROUPS/" + pgName + ".aspx?q=" + img + "~" + new Date().getTime();
                            //frame.src = "PRATICEGROUPS/EPC.aspx?q=" + img + "~" + new Date().getTime();
                            frame.style.height = hght;
                            frame.style.width = wght;
                            frame.style.backgroundColor = strColor;
                            var pop = $find('EditEntryPopup');
                            if (pop != null) {
                                pop.TargetControlID = img;
                                pop.show();
                                return false;
                            }
                        }
                    }
                }
            }
            function ShowPracticeLabel(chk, label) {
                var grpchk = document.getElementById(chk);
                if (grpchk != null) {
                    if (grpchk.checked == true) {
                        var shwlbl = document.getElementById(label);
                        shwlbl.style.display = 'block';
                    }
                    else {
                        var shwlbl = document.getElementById(label);
                        shwlbl.style.display = 'none';
                    }
                }
            }
            function ShowModalEntry(img, chk, iframe, hght, wght, pgName, plnEntry, plnEntryHght, strName, strColor, hid, plnEntryWght) {
                var grpchk = document.getElementById(chk);
                if (grpchk != null) {
                    if (grpchk.checked == true) {
                        if (hid != null) {
                            /* var hidval = document.getElementById(hid);
                            hidval.value = "1";*/
                        }
                        var shwlbl = document.getElementById(img);
                        shwlbl.style.display = 'block';

                        if (img != null && iframe != null && hght != null && wght != null && pgName != null && plnEntry != null && plnEntryHght != null) {
                            var plnEntry = document.getElementById(plnEntry);
                            if (plnEntry != null) {
                                plnEntry.style.height = plnEntryHght;
                                plnEntry.style.width = plnEntryWght;
                                plnEntry.style.backgroundColor = strColor;
                                var frame = $get(iframe);
                                if (frame != null) {
                                    frame.src = "PRATICEGROUPS/" + pgName + ".aspx?q=" + img + "~" + new Date().getTime();
                                    //frame.src = "PRATICEGROUPS/EPC.aspx?q=" + img + "~" + new Date().getTime();
                                    frame.style.height = hght;
                                    frame.style.width = wght;
                                    frame.style.backgroundColor = strColor;
                                    var pop = $find('EditEntryPopup');
                                    if (pop != null) {
                                        pop.TargetControlID = grpchk;
                                        pop.show();
                                        //return false;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        var shwlbl = document.getElementById(img);
                        shwlbl.style.display = 'block';

                        var chk = confirm('Do you want to remove the practice group selection ?');
                        if (chk == true) {
                            /* var hidval = document.getElementById(hid);
                            hidval.value = "0";*/
                            var hid_PracticeChk = document.getElementById('<%=hid_PracticeChk.ClientID %>');
                            hid_PracticeChk.value = grpchk.id.split('_')[1];
                            var dc = document.getElementById('<%=btnHidePG.ClientID %>');
                            dc.click();

                            return true;
                        }
                        else {
                            return false;
                        }
                        /*grpchk.nextSibling.style.fontSize = "12px";
                        grpchk.nextSibling.style.color = "black";
                        grpchk.nextSibling.style.fontWeight = "normal";
                        grpchk.nextSibling.style.fontStyle = "normal";*/
                    }
                }
            }
            function zoomin(lbl) {
                var lbl = document.getElementById(lbl);
                lbl.style.fontSize = "11px";
                lbl.style.color = "red";
                /*lbl.style.fontWeight = "bold";*/
                lbl.style.cursor = "hand";
            }
            function zoomout(lbl) {
                var lbl = document.getElementById(lbl);
                lbl.style.fontSize = "11px";
                lbl.style.color = "#00759A";
                lbl.style.fontWeight = "bold";
                lbl.style.cursor = "none";
            }
        </script>
        <%-- <script language="javascript" type="text/javascript">
        $(function () {
            /* 
            * this swallows backspace keys on any non-input element. 
            * stops backspace -> back 
            */
            var rx = /INPUT|SELECT|TEXTAREA/i;

            $(document).bind("keydown keypress", function (e) {
                if (e.which == 8) { // 8 == backspace 
                    if (!rx.test(e.target.tagName) || e.target.disabled || e.target.readOnly) {
                        e.preventDefault();
                    }
                }
            });
        }); 
    </script>--%>
        <%--<script language="javascript" type="text/javascript"> //Working Code Yuva
        document.onkeypress = function (event) {
            if (typeof window.event != 'undefined') { // ie
                event = window.event;
                event.target = event.srcElement; // make ie confirm to standards !!
            }
            var kc = event.keyCode;
            var tt = event.target.type;
            if ((kc == 13) && (event.target.getAttribute('enter_ok') == 'true')) {
                return true; // ok
            }

            // alert('kc='+kc+", tt="+tt);
            if ((kc != 8 && kc != 13) || ((tt == 'text' || tt == 'password') && kc != 13) ||
 (tt == 'textarea') || (tt == 'submit' && kc == 13))
                return true;
            //            alert('Bksp/Enter is not allowed here');
            return false;
        }

        if (typeof window.event != 'undefined') // ie
            document.onkeydown = document.onkeypress; // Trap bksp in ie. !! Note: does not trap enter, but onkeypress does !!
    </script>--%>
    </telerik:RadCodeBlock>
    <style type="text/css">
        * + html body span.riSingle input[type="text"].riTextBox
        {
            margin-top: 0;
        }
    </style>
    <style type="text/css">
        .autocomplete_completionListElement
        {
            margin: 0px !important;
            background-color: Silver;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            cursor: 'default';
            overflow: auto;
            height: 200px;
            text-align: left;
            list-style-type: none;
        }
        
        /* AutoComplete highlighted item */
        
        .autocomplete_highlightedListItem
        {
            background-color: #ffff99;
            color: black;
            padding: 1px;
        }
        
        /* AutoComplete item */
        
        .autocomplete_listItem
        {
            background-color: White;
            color: windowtext;
            padding: 1px;
        }
    </style>
    <link href="Styles/Menu.Office2010Silver.css" rel="stylesheet" type="text/css" />
    <link href="Styles/ComboBox.Office2010Silver.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField runat="server" ID="hidName" />
    <%--<ajax:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajax:ToolkitScriptManager>--%>
    <script type="text/javascript" src="jsUpdateProgress.js"></script>
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
            <ProgressTemplate>
                <div style="position: relative; top: 20%; text-align: center;">
                    <img src="Images/321.gif" style="vertical-align: middle" alt="Processing" />
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
            <asp:HiddenField runat="server" ID="hidddate" />
            <asp:HiddenField runat="server" ID="hidCredentialID" />
            <asp:HiddenField runat="server" ID="hidLeadCMSPartner" />
            <asp:HiddenField runat="server" ID="hidSourceOfCredentialOther" />
            <asp:HiddenField runat="server" ID="hidPartialSave" Value="0" />
            <asp:HiddenField runat="server" ID="hidPartial" Value="0" />
            <asp:Panel ID="Panel3" runat="server" ScrollBars="None" Width="100%">
                <table width="100%" cellpadding="1" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 30%;">
                        </td>
                        <td style="width: 40%;">
                        </td>
                        <td style="width: 25%;">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:CheckBox ID="chkPartial" runat="server" Text="Partial save" Visible="false"
                                AutoPostBack="true" OnCheckedChanged="chkPartial_CheckedChanged" CssClass="labelStyle" />
                        </td>
                        <td align="left" colspan="2">
                            <div class="gradientbuttons">
                                <ul>
                                    <li runat="server" id="liAddTop"><a>
                                        <asp:Button ID="btnAddTop" Text="SUBMIT" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Font-Bold="True" ForeColor="White" Font-Names="VERDANA" Font-Size="X-Small" OnClick="btnAddBottom_Click"
                                            CssClass="handShow" Width="60px" /></a></li>
                                    <li runat="server" id="liPartialSave" visible="false"><a>
                                        <asp:Button ID="btnPartialSave" Text="SAVE" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Font-Bold="True" ForeColor="White" Font-Names="VERDANA" Font-Size="X-Small" OnClick="btnAddBottom_Click"
                                            CssClass="handShow" Width="60px" Visible="false" /></a></li>
                                    <li runat="server" id="liEditTop" visible="false"><a>
                                        <asp:Button ID="btnEditTop" Text="UPDATE" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Font-Bold="True" ForeColor="White" Visible="false" Font-Names="VERDANA" Font-Size="X-Small"
                                            CssClass="handShow" Width="60px" OnClick="btnEditBottom_Click" /></a></li>
                                    <li runat="server" id="liDeleteTop" visible="false"><a>
                                        <asp:Button ID="btnDeleteTop" Text="DELETE" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Font-Bold="True" ForeColor="White" Visible="false" OnClick="btnDeleteBottom_Click"
                                            OnClientClick="return ConfirmDelete();" Font-Names="VERDANA" Font-Size="X-Small"
                                            CssClass="handShow" Width="60px" /></a></li>
                                    <li runat="server" id="liClearTop"><a>
                                        <asp:Button ID="btnClearTop" Text="CLEAR" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Font-Bold="True" ForeColor="White" Font-Names="VERDANA" Font-Size="X-Small" CssClass="handShow"
                                            OnClick="btnClearTop_Click" Width="60px" OnClientClick="return ConfirmClear();"
                                            Visible="false" /></a></li>
                                    <li runat="server" id="liSearchTop"><a>
                                        <asp:Button ID="btnSearchTop" Text="SEARCH" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Font-Bold="True" ForeColor="White" Font-Names="VERDANA" Font-Size="X-Small" CssClass="handShow"
                                            OnClick="btnSearchBottom_Click" Width="60px" /></a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="D" runat="server" Width="100%">
                <table width="100%" cellpadding="1" cellspacing="0" border="0">
                    <tr>
                        <td width="2%">
                            <img src="images/icon.gif" height="15" width="15" style="display: none; border-color: Red;"
                                id="erroricon" runat="server" />
                        </td>
                        <td>
                            <div id="errorinfo" runat="server" style="display: none;" class="divstyle">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel Width="99%" ID="Panel4" runat="server" Height="11px" CssClass="accordionHeader">
                                <asp:Label ID="Label2" runat="server" Text="Client Details"></asp:Label>
                            </asp:Panel>
                            <asp:Panel ID="PanelClientInformationShow" runat="server" CssClass="panelStylestatic">
                                <table width="100%" cellpadding="3" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="plnClientInfo" runat="server" CssClass="PanelSingleStyle">
                                                <table width="100%" cellpadding="1" cellspacing="0" border="0">
                                                    <tr style="display: inline;">
                                                        <td style="width: 30%;">
                                                        </td>
                                                        <td style="width: 40%;">
                                                        </td>
                                                        <td style="width: 25%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label Text="Client name" runat="server" CssClass="labelStyle" ID="lbl_Tab_Client">
                                                            </asp:Label>
                                                            <asp:Label ID="star1" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Client" CssClass="txtSingleStyle"
                                                                Text="Insert client name in full" MaxLength="200"></asp:TextBox>
                                                            <%-- <ajax:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx1" ID="AutoCompleteExtender1"
                                                                TargetControlID="txt_Tab_Client" ServicePath="~/CredentialWebService.asmx" ServiceMethod="GetEmailAddress"
                                                                MinimumPrefixLength="1" CompletionInterval="500" EnableCaching="true" CompletionSetCount="20"
                                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" ShowOnlyCurrentWordInCompletionListItem="true"
                                                                OnClientPopulating="ShowProcessImage" OnClientPopulated="HideProcessImage" FirstRowSelected="true">
                                                            </ajax:AutoCompleteExtender>--%>
                                                            <asp:Label runat="server" ID="lbl_Tab_Client_Msg" Text="Mandatory Field" CssClass="labelmandatory"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_Client_Name_Confidential" Text="Client name confidential"
                                                                runat="server" CssClass="labelStyle">
                                                            </asp:Label>
                                                            <asp:Label ID="Label5" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <div id="divnameconifendential" style="width: 320px; display: inline-table;">
                                                                <asp:RadioButtonList runat="server" ID="rdo_Tab_Client_Name_Confidential" RepeatDirection="Horizontal"
                                                                    CssClass="rdolist" AutoPostBack="true" OnSelectedIndexChanged="rdo_Tab_Client_Name_Confidential_SelectedIndexChanged"
                                                                    Width="90px">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <asp:Label runat="server" ID="lbl_Tab_Client_Name_Confidential_Msg" Text="Mandatory Field"
                                                                CssClass="labelmandatory"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr_Tab_ClientDescription" runat="server" visible="false">
                                                        <td align="left" valign="top">
                                                            <asp:Label ID="lbl_Tab_ClientDescription" Text="Confidential client generic description"
                                                                runat="server" CssClass="labelStyle" Visible="true">
                                                            </asp:Label>
                                                            <asp:Label ID="Label17" Text="*" runat="server" CssClass="labelStarStyle" Visible="true"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2" valign="top">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_ClientDescription" CssClass="txtSingleStyle"
                                                                Visible="true" TextMode="MultiLine" Height="50px" Text="Eg. a leading retail bank, an international IT company etc"></asp:TextBox>
                                                            <asp:Label runat="server" ID="lbl_Tab_ClientDescription_Msg" Text="Mandatory Field"
                                                                CssClass="labelmandatory"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr id="tr_Tab_ClientDescription_Language" runat="server" visible="false">
                                                        <td align="left" valign="top">
                                                            <asp:Label ID="lbl_Tab_ClientDescription_Language" Text="Language of generic description"
                                                                runat="server" CssClass="labelStyle">
                                                            </asp:Label>
                                                            <asp:Label ID="Label35" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="top" colspan="2">
                                                            <telerik:RadComboBox ID="cbo_Tab_ClientDescription_Language" AutoPostBack="true"
                                                                AllowCustomText="false" MarkFirstMatch="true" runat="server" Width="429px" CheckBoxes="false"
                                                                EnableCheckAllItemsCheckBox="false" Height="200px" OnSelectedIndexChanged="rad_Tab_ClientDescription_Language_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr_Tab_ClientDescription_OtherLanguage" runat="server" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_ClientDescription_OtherLanguage" Text="Description" runat="server"
                                                                CssClass="labelStyle" Visible="true">
                                                            </asp:Label>
                                                            <asp:Label ID="Label41" Text="*" runat="server" CssClass="labelStarStyle" Visible="true"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2" valign="top">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_ClientDescription_OtherLanguage"
                                                                CssClass="txtSingleStyle" Visible="true" ToolTip="Generic e.g. A leading bank"
                                                                MaxLength="50" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                            <asp:Label runat="server" ID="lbl_Tab_ClientDescription_OtherLanguage_Msg" Text="Mandatory Field"
                                                                CssClass="labelmandatory"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                    <tr id="tr_Tab_NameConfidential_Completion" runat="server" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_NameConfidential_Completion" Text="Client name confidential on completion"
                                                                runat="server" CssClass="labelStyle" Visible="true">
                                                            </asp:Label>
                                                            <asp:Label ID="Label18" Text="*" runat="server" CssClass="labelStarStyle" Visible="true"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <div id="divnamecompletion" style="width: 314px; display: block;" runat="server">
                                                                <asp:RadioButtonList runat="server" ID="rdo_Tab_NameConfidential_Completion" RepeatDirection="Horizontal"
                                                                    Width="90px" Visible="true" CssClass="rdolist">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <asp:Label runat="server" ID="lbl_Tab_NameConfidential_Completion_Msg" Text="Mandatory Field"
                                                                CssClass="labelmandatory"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_ClientIndustrySector" Text="Client sector" runat="server"
                                                                CssClass="labelStyle">
                                                            </asp:Label>
                                                            <asp:Label ID="Label19" Text="*" runat="server" CssClass="labelStarStyle" Visible="true"></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_ClientIndustrySector" CssClass="txtSingleStyle"
                                                                Visible="true" Enabled="false" Text="Select the sector of the client company from look up"
                                                                ClientEnabled="true"></asp:TextBox>
                                                            <asp:ImageButton runat="server" ID="img_Tab_ClientIndustrySector" ImageUrl="~/Images/bino.jpg"
                                                                ImageAlign="AbsMiddle" Width="15px" Height="13px" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_ClientIndustrySector" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_ClientIndustrySector_Text" />
                                                            <asp:Label runat="server" ID="lbl_Tab_ClientIndustrySector_Msg" Text="Mandatory Field"
                                                                CssClass="labelmandatory"> </asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label CssClass="labelStyle" ID="lbl_Tab_Client_Industry_Type" runat="server"
                                                                Text="Client sub-sector">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" valign="bottom" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Client_Industry_Type" CssClass="txtSingleStyle"
                                                                Enabled="false" Visible="true" ReadOnly="true" Text="Select the sub-sector of the client company from look up"></asp:TextBox>
                                                            <asp:ImageButton runat="server" ID="img_Tab_Client_Industry_Type" ImageUrl="~/Images/bino.jpg"
                                                                ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Client Industry Sub Sector Look Up" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Client_Industry_Type" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Client_Industry_Type_Text" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label CssClass="labelStyle" ID="lbl_Tab_Country_PredominantCountry" runat="server"
                                                                Text="Predominant country of client "></asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Country_PredominantCountry"
                                                                Enabled="false" CssClass="txtSingleStyle" ReadOnly="true" Text="Eg. where head quartered"></asp:TextBox>
                                                            <asp:ImageButton runat="server" ID="img_Tab_Country_PredominantCountry" ImageUrl="~/Images/bino.jpg"
                                                                ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Predominant country of client" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Country_PredominantCountry" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Country_PredominantCountry_Text" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="3">
                                                            <asp:Label runat="server" ID="lbl_Tab_PracticeGroup" Text="Please select atleast one Practice Group"
                                                                CssClass="labelmandatory"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Panel ID="plnPracticeGroups" runat="server" GroupingText="Main Practice Group"
                                                CssClass="PanelSingleStylePG" Width="100%">
                                                <asp:HiddenField ID="hid_PracticeChk" runat="server" />
                                                <asp:Button ID="btnHidePG" runat="server" CssClass="btnhide" OnClick="btnHidePG_Click" />
                                                <table border="0" width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chKBAIF" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chKBAIF_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_BAIF" runat="server" Value="1" />
                                                        </td>
                                                        <td style="width: 70px;">
                                                            <asp:Label runat="server" ID="lblBAIF" Text="BAIF" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chKCorp" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chKCorp_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_Corp" runat="server" Value="3" />
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:Label runat="server" ID="lblCorp" Text="Corporate" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chkCorpTax" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkCorpTax_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_CorpTax" runat="server" Value="11" />
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:Label runat="server" ID="lblCorpTax" Text="Corporate Tax" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chKCRD" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chKCRD_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_CRD" runat="server" Value="4" />
                                                        </td>
                                                        <td style="width: 70px;">
                                                            <asp:Label runat="server" ID="lblCRD" Text="CRD" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chkEPC" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkEPC_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_EPC" runat="server" Value="5" />
                                                        </td>
                                                        <td style="width: 140px;">
                                                            <asp:Label runat="server" ID="lblEPC" Text="EPC Construction" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chkEPCE" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkEPCE_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_EPCE" runat="server" Value="9" />
                                                        </td>
                                                        <td style="width: 100px;">
                                                            <asp:Label runat="server" ID="lblENE" Text="EPC Energy" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chkIPF" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkIPF_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_IPF" runat="server" Value="8" />
                                                        </td>
                                                        <td style="width: 120px;">
                                                            <asp:Label runat="server" ID="lblIPF" Text="EPC Projects" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chkHC" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkHC_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_HC" runat="server" Value="10" />
                                                        </td>
                                                        <td style="width: 120px;">
                                                            <asp:Label runat="server" ID="lblHC" Text="Human Capital" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                        <td style="width: 2px;">
                                                            <asp:CheckBox ID="chkRE" runat="server" Text="" AutoPostBack="true" OnCheckedChanged="chkRE_CheckedChanged" />
                                                            <asp:HiddenField ID="hid_RE" runat="server" Value="7" />
                                                        </td>
                                                        <td style="width: 110px;">
                                                            <asp:Label runat="server" ID="lblRE" Text="Real Estate" CssClass="labelStylePG"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table width="100%" cellpadding="1" cellspacing="0" border="0">
                                <tr>
                                    <td>
                                        <asp:Panel Width="99%" ID="PnlMatterEntryHead" runat="server" Height="11px" CssClass="accordionHeader">
                                            <asp:Label ID="Label3" runat="server" Text="Matter Details"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 2px;">
                                        <asp:Panel ID="PnlMatterEntryDetails" runat="server" GroupingText="" CssClass="PanelSingleStyle">
                                            <table width="100%" cellpadding="0" cellspacing="3" border="0">
                                                <tr>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 40%;">
                                                    </td>
                                                    <td style="width: 25%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Matter_No" Text="Matter number" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label29" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Matter_No" CssClass="txtSingleStyle"
                                                            Visible="true" MaxLength="15" Text="Eg. 123456.00001"></asp:TextBox>
                                                        <asp:Label runat="server" ID="lbl_Tab_MatterNo_Msg" Text="Mandatory Field" CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Date_Opened" Text="Date matter opened" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label28" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td valign="bottom" align="left" colspan="2">
                                                        <%--<ajax:CalendarExtender ID="cld_Tab_Date_Opened" runat="server" TargetControlID="txt_Tab_Date_Opened"
                                                            Format="dd/MM/yyyy" PopupButtonID="img_Tab_Date_Opened" PopupPosition="BottomRight"
                                                            OnClientDateSelectionChanged="dateSelectionChanged">
                                                        </ajax:CalendarExtender>
                                                        <asp:TextBox ID="txt_Tab_Date_Opened" runat="server" CssClass="txtSingleStyle" Width="425px"
                                                            ReadOnly="true" Enabled="false"></asp:TextBox>
                                                        <asp:HiddenField ID="hid_Tab_Date_Opened" runat="server" />
                                                        <asp:ImageButton runat="Server" ID="img_Tab_Date_Opened" ImageUrl="~/images/Calendar_scheduleHS.png"
                                                            AlternateText="Click to show calendar" ImageAlign="Baseline" />--%>
                                                        <telerik:RadDatePicker Width="412px" EnableEmbeddedSkins="false" Skin="Myskin" Font-Size="11px"
                                                            DateInput-Font-Size="11px" ShowPopupOnFocus="true" ID="cld_Tab_Date_Opened" runat="server"
                                                            Calendar-FastNavigationStep="12" DateInput-ReadOnly="true" DateInput-DateFormat="dd/MM/yyyy"
                                                            Culture="en-GB" DateInput-EmptyMessageStyle-BackColor="#F0F8FF" DateInput-Height="30px">
                                                            <DateInput ID="DateInputO" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                                                                runat="server" CssClass="txtSingleStyle" Width="130px" Enabled="false">
                                                            </DateInput>
                                                        </telerik:RadDatePicker>
                                                        <asp:Image runat="server" ID="img_DateOpened" ImageUrl="~/Images/close.png" ToolTip="Click here to clear the selected values"
                                                            ImageAlign="Top" AlternateText="Clear" />
                                                        <asp:Label runat="server" ID="lbl_Tab_Date_Opened_Msg" Text="Mandatory Field" CssClass="labelmandatory"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Date_Completed" Text="Date matter completed" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label4" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" valign="bottom" colspan="2">
                                                        <asp:HiddenField ID="hid_Tab_Date_Completed" runat="server" />
                                                        <telerik:RadDatePicker Width="350px" EnableEmbeddedSkins="false" Skin="Myskin" Font-Size="11px"
                                                            DateInput-Font-Size="11px" ShowPopupOnFocus="true" ID="cld_Tab_Date_Completed"
                                                            Culture="en-GB" runat="server" Calendar-FastNavigationStep="12" DateInput-ReadOnly="true"
                                                            DateInput-DateFormat="dd/MM/yyyy" DateInput-EmptyMessageStyle-BackColor="#F0F8FF"
                                                            DateInput-Height="30px">
                                                            <DateInput ID="DateInputC" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                                                                runat="server" CssClass="txtSingleStyle" Width="130px" Enabled="false">
                                                            </DateInput>
                                                        </telerik:RadDatePicker>
                                                        <asp:TextBox ID="txt_Tab_Date_Completed" runat="server" CssClass="txtSingleStylehidden"
                                                            Visible="true" Width="350px" ReadOnly="true" Text="Select date from calendar icon or select ongoing"
                                                            Enabled="false"></asp:TextBox>
                                                        <asp:Image runat="server" ID="img_Date" ImageUrl="~/Images/close.png" ToolTip="Click here to clear the selected values"
                                                            ImageAlign="Top" AlternateText="Clear" />
                                                        <asp:CheckBox ID="chk_Tab_ActualDate_Ongoing" runat="server" Text="Ongoing" />
                                                        <asp:CheckBox ID="chk_Tab_ActualDate_Ongoing_1" runat="server" Text="Not known" />
                                                        <asp:HiddenField ID="hid_Tab_ActualDate_Ongoing" Value="0" runat="server" />
                                                        <asp:HiddenField ID="hid_Tab_ActualDate_Ongoing_1" Value="0" runat="server" />
                                                        <asp:Label runat="server" ID="lbl_Tab_Date_Completed_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"> </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_TransactionIndustrySector" Text="Matter sector" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label30" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_TransactionIndustrySector"
                                                            Enabled="false" CssClass="txtSingleStyle" ReadOnly="true" Text="Select the sector the matter relates to (not worktype) from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_TransactionIndustrySector" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Matter sector" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_TransactionIndustrySector" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_TransactionIndustrySector_Text" />
                                                        <asp:Label runat="server" ID="lbl_Tab_TransactionIndustrySector_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Transaction_Industry_Type" Text="Matter sub-sector" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Transaction_Industry_Type"
                                                            Enabled="false" CssClass="txtSingleStyle" ReadOnly="true" Text="Select the sub-sector of the matter from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Transaction_Industry_Type" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Matter sub-sector" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Transaction_Industry_Type" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Transaction_Industry_Type_Text" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:Label ID="lbl_Tab_Project_Description" Text="Matter/credential description"
                                                            runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label32" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2" valign="top">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Project_Description" CssClass="txtSingleStyle"
                                                            TextMode="MultiLine" Height="75px" Visible="true" MaxLength="950" Text="Enter a brief overview of the work we did on the matter, do not include the client’s name and finish with a full stop Eg. on the acquisition of one of the country’s largest banks for EUR 23 billion."></asp:TextBox>
                                                        <asp:Label runat="server" ID="lbl_Tab_Project_Description_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td align="left" valign="top">
                                                        <asp:Label ID="lbl_Tab_Language" Text="Language of matter/credential description"
                                                            runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label31" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" valign="top" colspan="2">
                                                        <telerik:RadComboBox ID="cbo_Tab_Language" AutoPostBack="true" AllowCustomText="false"
                                                            MarkFirstMatch="true" runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                            Height="200px" OnSelectedIndexChanged="cbo_Language_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>--%>
                                                <%--<tr id="tr_Tab_ProjectDescription_Polish" runat="server" visible="false">
                                                    <td align="left" valign="top">
                                                        <asp:Label ID="lbl_Tab_ProjectDescription_Polish" Text="Matter/credential description"
                                                            runat="server" CssClass="labelStyle" Visible="true">
                                                        </asp:Label>
                                                        <asp:Label ID="Label33" Text="*" runat="server" CssClass="labelStarStyle" Visible="true"></asp:Label>
                                                    </td>
                                                    <td align="left" valign="top" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_ProjectDescription_Polish"
                                                            CssClass="txtSingleStyle" TextMode="MultiLine" Height="50px" Visible="true" ToolTip="Type Mattter/Credential description"
                                                            MaxLength="400"></asp:TextBox>
                                                        <asp:Label runat="server" ID="lbl_Tab_ProjectDescription_Polish_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:Label ID="lbl_Tab_Significant_Features" Text="Other useful matter/credential information"
                                                            runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td valign="middle" align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Significant_Features" CssClass="txtSingleStyle"
                                                            TextMode="MultiLine" Height="50px" Visible="true" MaxLength="950" Text="Insert any other useful information about the credential that will be useful for future reference purposes"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:Label ID="lbl_Tab_Keyword" Text="Keyword(s)/themes" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td valign="middle" align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Keyword" CssClass="txtSingleStyle"
                                                            Visible="true" Text="Include any other key words associated with the matter "></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Client_Matter_Confidential" Text="Matter confidential" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label34" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <div id="divmatterconfidential" style="width: 314px; display: inline-table;">
                                                            <asp:RadioButtonList runat="server" ID="rdo_Tab_Client_Matter_Confidential" RepeatDirection="Horizontal"
                                                                CssClass="rdolist" AutoPostBack="true" OnSelectedIndexChanged="rdo_Tab_Client_Matter_Confidential_SelectedIndexChanged"
                                                                Width="90px">
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <asp:Label runat="server" ID="lbl_Tab_Client_Matter_Confidential_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tr_Tab_MatterConfidential_Completion" runat="server" visible="false">
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_MatterConfidential_Completion" Text="Matter confidential on completion"
                                                            runat="server" CssClass="labelStyle" Visible="true">
                                                        </asp:Label>
                                                        <asp:Label ID="Label36" Text="*" runat="server" CssClass="labelStarStyle" Visible="true"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <div id="divmattercompletion" style="width: 314px; display: inline-table;" runat="server">
                                                            <asp:RadioButtonList runat="server" ID="rdo_Tab_MatterConfidential_Completion" RepeatDirection="Horizontal"
                                                                Width="90px" Visible="true" CssClass="rdolist">
                                                            </asp:RadioButtonList>
                                                        </div>
                                                        <asp:Label runat="server" ID="lbl_Tab_MatterConfidential_Completion_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_ProjectName_Core" Text="Project name" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_ProjectName_Core" CssClass="txtSingleStyle"
                                                            Visible="true" MaxLength="50" Text="If applicable e.g. Project Camden"></asp:TextBox>
                                                        <%-- <ajax:AutoCompleteExtender runat="server" BehaviorID="AutoCompleteEx2" ID="AutoCompleteExtender2"
                                                            TargetControlID="txt_Tab_ProjectName_Core" ServicePath="~/CredentialWebService.asmx"
                                                            ServiceMethod="GetProjectName" MinimumPrefixLength="1" CompletionInterval="500"
                                                            EnableCaching="false" CompletionSetCount="20" CompletionListCssClass="autocomplete_completionListElement"
                                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                            DelimiterCharacters="," ShowOnlyCurrentWordInCompletionListItem="true" OnClientPopulating="ShowProcessImage2"
                                                            OnClientPopulated="HideProcessImage2" FirstRowSelected="true">
                                                        </ajax:AutoCompleteExtender>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Country_Law" Text="Applicable law" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                        <%-- <asp:Label ID="Label37" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>--%>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <telerik:RadComboBox ID="cbo_Tab_Country_Law" AllowCustomText="false" AutoPostBack="true"
                                                            EnableEmbeddedSkins="false" Skin="MySkin" MarkFirstMatch="true" runat="server"
                                                            Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false" Height="200px"
                                                            OnSelectedIndexChanged="cbo_Tab_Country_Law_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                        <%-- <asp:Label runat="server" ID="lbl_Tab_Country_Law_Msg" Text="Mandatory Field" CssClass="labelmandatory"></asp:Label>--%>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_Tab_Country_Law_Other" visible="false">
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Country_Law_Other" Text="Other (please specify)" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label15" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Country_Law_Other" CssClass="txtSingleStyle"></asp:TextBox>
                                                        <asp:Label runat="server" ID="Label50" Text="Mandatory Field" CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Country_Matter_Open" Text="Country where matter opened" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label38" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Country_Matter_Open" CssClass="txtSingleStyle"
                                                            Enabled="false" ReadOnly="true" Text="Select the country where matter opened from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Country_Matter_Open" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Country where matter opened" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Country_Matter_Open" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Country_Matter_Open_Text" />
                                                        <asp:Label runat="server" ID="lbl_Tab_Country_Matter_Open_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Country_Matter_Close" Text="Matter location(s)" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label39" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" valign="bottom" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Country_Matter_Close" CssClass="txtSingleStyle"
                                                            Enabled="false" ReadOnly="true" Text="Select the country(s) of the matter/transaction from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Country_Matter_Close" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Matter location(s)" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Country_Matter_Close" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Country_Matter_Close_Text" />
                                                        <asp:Label runat="server" ID="lbl_Tab_Country_Matter_Close_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Contentious_IRG" runat="server" Text="Contentious" CssClass="labelStyle"></asp:Label>
                                                        <asp:Label ID="Label40" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <telerik:RadComboBox ID="cbo_Tab_Contentious_IRG" AllowCustomText="false" MarkFirstMatch="true"
                                                            runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                            EnableEmbeddedSkins="false" Skin="MySkin" Height="100px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="cbo_Tab_Contentious_IRG_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                        <asp:Label runat="server" ID="lbl_Tab_Contentious_IRG_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="plnContentiousDetails" runat="server" Visible="false">
                                                <table width="100%" cellpadding="2" cellspacing="0" border="0">
                                                    <tr style="display: inline;">
                                                        <td style="width: 30%;">
                                                        </td>
                                                        <td style="width: 40%;">
                                                        </td>
                                                        <td style="width: 25%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_Dispute_Resolution" Text="Dispute resolution" runat="server"
                                                                CssClass="labelStyle">
                                                            </asp:Label>
                                                            <%-- <asp:Label ID="Label43" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>--%>
                                                        </td>
                                                        <td align="left" valign="top" colspan="2">
                                                            <telerik:RadComboBox ID="cbo_Tab_Dispute_Resolution" AllowCustomText="false" MarkFirstMatch="true"
                                                                runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                                EnableEmbeddedSkins="false" Skin="MySkin" Height="145px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cbo_Tab_Dispute_Resolution_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                            <%-- <asp:Label runat="server" ID="lbl_Tab_Dispute_Resolution_Msg" Text="Mandatory Field"
                                                                CssClass="labelmandatory"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr_Tab_Country_ArbitrationCountry" runat="server" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_Country_ArbitrationCountry" Text="Country of arbitration"
                                                                runat="server" CssClass="labelStyle" Visible="true">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Country_ArbitrationCountry"
                                                                Enabled="false" CssClass="txtSingleStyle" Visible="true" ReadOnly="true"></asp:TextBox>
                                                            <asp:ImageButton runat="server" ID="img_Tab_Country_ArbitrationCountry" ImageUrl="~/Images/bino.jpg"
                                                                ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Country of arbitration"
                                                                Visible="true" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Country_ArbitrationCountry" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Country_ArbitrationCountry_Text" />
                                                        </td>
                                                    </tr>
                                                    <tr id="tr_Tab_ArbitrationCity" runat="server" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_ArbitrationCity" Text="Seat of arbitration" runat="server"
                                                                CssClass="labelStyle" Visible="true">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" valign="top" colspan="2">
                                                            <telerik:RadComboBox ID="cbo_Tab_ArbitrationCity" AllowCustomText="false" MarkFirstMatch="true"
                                                                runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                                EnableEmbeddedSkins="false" Skin="MySkin" Height="200px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="cbo_Tab_ArbitrationCity_SelectedIndexChanged">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="tr_Tab_ArbitrationCity_Other" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_ArbitrationCity_Other" Text="Other (please specify)" runat="server"
                                                                CssClass="labelStyle">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_ArbitrationCity_Other" CssClass="txtSingleStyle"
                                                                ToolTip="Type Project Name"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr_Tab_Arbitral_Rules" runat="server" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_Arbitral_Rules" Text="Arbitral rules" runat="server" CssClass="labelStyle"
                                                                Visible="true">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <telerik:RadComboBox ID="cbo_Tab_Arbitral_Rules" AllowCustomText="false" MarkFirstMatch="true"
                                                                runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                                EnableEmbeddedSkins="false" Skin="MySkin" Height="200px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="tr_Tab_InvestmentTreaty" runat="server" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_InvestmentTreaty" Text="Investment treaty" runat="server"
                                                                CssClass="labelStyle">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" valign="top" colspan="2">
                                                            <div id="div13" style="width: 316px; display: inline-table;">
                                                                <asp:RadioButtonList runat="server" ID="rdo_Tab_InvestmentTreaty" RepeatDirection="Horizontal"
                                                                    CssClass="rdolist" Width="90px">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="tr_Tab_Investigation_Type" visible="false">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_Investigation_Type" Text="Investigation type" runat="server"
                                                                CssClass="labelStyle">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" valign="top" colspan="2">
                                                            <telerik:RadComboBox ID="cbo_Tab_Investigation_Type" AllowCustomText="false" MarkFirstMatch="true"
                                                                runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                                EnableEmbeddedSkins="false" Skin="MySkin" Height="120px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_Language_Of_Dispute" Text="Language of dispute" runat="server"
                                                                CssClass="labelStyle">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Language_Of_Dispute" CssClass="txtSingleStyle"
                                                                Enabled="false" ReadOnly="true" Text="Select the language of dispute from look up"></asp:TextBox>
                                                            <asp:ImageButton runat="server" ID="img_Tab_Language_Of_Dispute" ImageUrl="~/Images/bino.jpg"
                                                                ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Language of dispute" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Language_Of_Dispute" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Language_Of_Dispute_Text" />
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="tr_Tab_Language_Of_Dispute_Other" style="display: none;">
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_Language_Of_Dispute_Other" Text="Other (please specify)" runat="server"
                                                                CssClass="labelStyle">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Language_Of_Dispute_Other"
                                                                CssClass="txtSingleStyle" ToolTip="Type Other"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Language_Of_Dispute_Other" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Language_Of_Dispute_Other_Text" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:Label ID="lbl_Tab_Country_Jurisdiction" Text="Jurisidiction of dispute" runat="server"
                                                                CssClass="labelStyle">
                                                            </asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Country_Jurisdiction" CssClass="txtSingleStyle"
                                                                Enabled="false" ReadOnly="true" Text="Select the country of dispute from look up"></asp:TextBox>
                                                            <asp:ImageButton runat="server" ID="img_Tab_Country_Jurisdiction" ImageUrl="~/Images/bino.jpg"
                                                                ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Jurisidiction of dispute" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Country_Jurisdiction" />
                                                            <asp:HiddenField runat="server" ID="hid_Tab_Country_Jurisdiction_Text" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </asp:Panel>
                                        <asp:Panel ID="plnValue" runat="server" GroupingText="" CssClass="PanelSingleStyle">
                                            <table width="100%" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 40%;">
                                                    </td>
                                                    <td style="width: 25%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_ValueOfDeal_Core" Text="Value of deal" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <%-- <asp:TextBox runat="server" Width="425px" ID="txt_Tab_ValueOfDeal_Core" CssClass="txtSingleStyle"
                                                            Visible="true" ToolTip="Numerical separated by commas" MaxLength="20" Text="Eg. 100,000"></asp:TextBox>--%>
                                                        <telerik:RadNumericTextBox ID="txt_Tab_ValueOfDeal_Core" runat="server" NumberFormat-DecimalDigits="2"
                                                            CssClass="txtSingleStyle" NumberFormat-DecimalSeparator="," DataType="System.Decimal" AllowOutOfRangeAutoCorrect="false"
                                                            EmptyMessage="Eg. 100,000" Width="429px" MaxLength="14">
                                                            <NumberFormat ZeroPattern="n" DecimalDigits="2" DecimalSeparator="." GroupSeparator=",">
                                                            </NumberFormat>
                                                        </telerik:RadNumericTextBox>
                                                        <asp:Label runat="server" ID="lbl_Tab_ValueOfDeal_Core_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Currency_Of_Deal" Text="Currency of deal" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2" valign="middle">
                                                        <telerik:RadComboBox ID="cbo_Tab_Currency_Of_Deal" AllowCustomText="false" MarkFirstMatch="true"
                                                            runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                            EnableEmbeddedSkins="false" Skin="MySkin" Height="200px">
                                                        </telerik:RadComboBox>
                                                        <asp:Label runat="server" ID="lbl_Tab_Currency_Of_Deal_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Value_Confidential" Text="Value confidential" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" valign="top" colspan="2">
                                                        <div id="divvalueconfidential" style="width: 314px; display: inline-table;">
                                                            <asp:RadioButtonList runat="server" ID="rdo_Tab_Value_Confidential" RepeatDirection="Horizontal"
                                                                CssClass="rdolist" AutoPostBack="true" OnSelectedIndexChanged="rdo_Tab_Value_Confidential_SelectedIndexChanged"
                                                                Width="90px">
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="trr_Tab_ValueConfidential_Completion" runat="server" visible="false">
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_ValueConfidential_Completion" Text="Value confidential on completion"
                                                            runat="server" CssClass="labelStyle" Visible="true">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <div id="divvaluecompletion" style="width: 314px; display: inline-table;" runat="server">
                                                            <asp:RadioButtonList runat="server" ID="rdo_Tab_ValueConfidential_Completion" RepeatDirection="Horizontal"
                                                                Width="90px" Visible="true" CssClass="rdolist">
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel Width="99%" ID="plnCMSEntryHead" runat="server" Height="11px" CssClass="accordionHeader">
                                            <asp:Label ID="Label6" runat="server" Text="People Details"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="plnTeamEntryDetails" runat="server" GroupingText="" CssClass="PanelSingleStyle">
                                            <table width="100%" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 40%;">
                                                    </td>
                                                    <td style="width: 25%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Team" Text="Team(s)" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label24" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Team" CssClass="txtSingleStyle"
                                                            Enabled="false" ReadOnly="true" Text="Multi select from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Team" ImageUrl="~/Images/bino.jpg" ImageAlign="AbsMiddle"
                                                            Width="15px" Height="13px" ToolTip="Team(s)" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Team" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Team_Text" />
                                                        <asp:Label runat="server" ID="lbl_Tab_Team_Msg" Text="Mandatory Field" CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Lead_Partner" Text="Lead partners" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label21" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Lead_Partner" CssClass="txtSingleStyle"
                                                            Enabled="false" ReadOnly="true" AutoPostBack="True" OnTextChanged="txt_Tab_Lead_Partner_TextChanged"
                                                            Text="Multi select from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Lead_Partner" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Lead partners" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Lead_Partner" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Lead_Partner_Text" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Lead_Partner_Ctl" Value="0" />
                                                        <asp:Label runat="server" ID="lbl_Tab_Lead_Partner_Msg" Text="Mandatory Field" CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="tr_Tab_CMSPartnerName" runat="server" style="display: none;">
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_CMSPartnerName" Text="Name of CMS partner" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_CMSPartnerName" CssClass="txtSingleStyle"
                                                            Text="Open field – format last name first name"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr id="tr_Tab_Source_Of_Credential" runat="server" style="display: none;">
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Source_Of_Credential" Text="Source of CMS credential " runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Source_Of_Credential" CssClass="txtSingleStyle"
                                                            Enabled="false" ReadOnly="true" Text="Select the source of credential from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Source_Of_Credential" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" CssClass="imgSingleStyle" ToolTip="Source of CMS credential" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Source_Of_Credential" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Source_Of_Credential_Text" />
                                                    </td>
                                                </tr>
                                                <tr id="tr_Tab_SourceOfCredential_Other" runat="server" style="display: none;">
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_SourceOfCredential_Other" runat="server" CssClass="labelStyle"
                                                            Text="Other">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox ID="txt_Tab_SourceOfCredential_Other" runat="server" ToolTip="Type Other Source Of Credential"
                                                            CssClass="txtSingleStyle" Width="425px"></asp:TextBox>
                                                        <asp:HiddenField ID="hid_Tab_SourceOfCredential_Other" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Other_Matter_Executive" runat="server" CssClass="labelStyle"
                                                            Text="Matter executive(s)" Width="135px">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox ID="txt_Tab_Other_Matter_Executive" runat="server" CssClass="txtSingleStyle"
                                                            Enabled="false" ReadOnly="true" Width="425px" Text="Multi select from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Other_Matter_Executive" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Other matter executive" />
                                                        <asp:HiddenField ID="hid_Tab_Other_Matter_Executive" runat="server" />
                                                        <asp:HiddenField ID="hid_Tab_Other_Matter_Executive_Text" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="plnCMSEntryDetails" runat="server" GroupingText="" CssClass="PanelSingleStyle">
                                            <table width="100%" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 40%;">
                                                    </td>
                                                    <td style="width: 25%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Referred_From_Other_CMS_Office" Text="CMS firms involved"
                                                            runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Referred_From_Other_CMS_Office"
                                                            Enabled="false" CssClass="txtSingleStyle" ReadOnly="true" Text="Multi select from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Referred_From_Other_CMS_Office" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="CMS firms involved" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Referred_From_Other_CMS_Office" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Referred_From_Other_CMS_Office_Text" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Lead_CMS_Firm" Text="Lead CMS firm" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td colspan="2" align="left">
                                                        <telerik:RadComboBox ID="cbo_Tab_Lead_CMS_Firm" AllowCustomText="false" MarkFirstMatch="true"
                                                            runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                            EnableEmbeddedSkins="false" Skin="MySkin" Height="200px">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Country_OtherCMSOffice" Text="Countries of other CMS firms"
                                                            runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Country_OtherCMSOffice" CssClass="txtSingleStyle"
                                                            Enabled="false" ReadOnly="true" Text="Multi select from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Country_OtherCMSOffice" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Countries of other CMS firms" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Country_OtherCMSOffice" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Country_OtherCMSOffice_Text" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel Width="99%" ID="plnCredentialHead" runat="server" Height="11px" CssClass="accordionHeader">
                                            <asp:Label ID="Label7" runat="server" Text="Other Useful Information"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="plnCredentialDetails" runat="server" GroupingText="" CssClass="PanelSingleStyle">
                                            <table width="100%" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 40%;">
                                                    </td>
                                                    <td style="width: 25%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Credential_Status" Text="Credential status" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label44" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <telerik:RadComboBox ID="cbo_Tab_Credential_Status" AllowCustomText="false" MarkFirstMatch="true"
                                                            runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                            EnableEmbeddedSkins="false" Skin="MySkin" Height="70px">
                                                        </telerik:RadComboBox>
                                                        <asp:Label runat="server" ID="lbl_Tab_Credential_Status_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Credential_Version" Text="Credential version" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label48" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <telerik:RadComboBox ID="cbo_Tab_Credential_Version" AllowCustomText="false" MarkFirstMatch="true"
                                                            runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                            EnableEmbeddedSkins="false" Skin="MySkin" AutoPostBack="true" Height="70px" OnSelectedIndexChanged="cbo_Tab_Credential_Version_SelectedIndexChanged">
                                                        </telerik:RadComboBox>
                                                        <asp:HiddenField runat="server" ID="hid_Credential_Version" />
                                                        <asp:Label runat="server" ID="lbl_Tab_Credential_Version_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_Credential_Version_Other" visible="false">
                                                    <td align="left" valign="top">
                                                        <asp:Label ID="lbl_Tab_Credential_Version_Other" Text="Version name" runat="server"
                                                            CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label45" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Credential_Version_Other" CssClass="txtSingleStyle"
                                                            Visible="true" ToolTip="Type Description" MaxLength="950" TextMode="MultiLine"
                                                            Height="50px"></asp:TextBox>
                                                        <asp:Label runat="server" ID="txt_Tab_Credential_Version_Other_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="tr_Credential_Copy" visible="false" style="height: 20px;">
                                                    <td align="left" valign="top">
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:CheckBox ID="chkCredentialCopy" runat="server" Text="Duplicate" AutoPostBack="true"
                                                            OnCheckedChanged="chkCredentialCopy_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Credential_Type" Text="Credential type" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                        <asp:Label ID="Label49" Text="*" runat="server" CssClass="labelStarStyle"></asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <telerik:RadComboBox ID="cbo_Tab_Credential_Type" AllowCustomText="false" MarkFirstMatch="true"
                                                            runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                            EnableEmbeddedSkins="false" Skin="MySkin" Height="70px">
                                                        </telerik:RadComboBox>
                                                        <asp:Label runat="server" ID="lbl_Tab_Credential_Type_Msg" Text="Mandatory Field"
                                                            CssClass="labelmandatory"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Other_Uses" Text="Other uses" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Other_Uses" CssClass="txtSingleStyle"
                                                            Enabled="false" ReadOnly="true" Text="Eg. case study/directory submission select from look-up and enter docs# in other useful credential information box"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Other_Uses" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Other uses" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Other_Uses" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Other_Uses_Text" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Priority" Text="Credential priority" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <telerik:RadComboBox ID="cbo_Tab_Priority" AllowCustomText="false" MarkFirstMatch="true"
                                                            runat="server" Width="429px" CheckBoxes="false" EnableCheckAllItemsCheckBox="false"
                                                            EnableEmbeddedSkins="false" Skin="MySkin" Height="70px">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="plnTBD" runat="server" GroupingText="" CssClass="PanelSingleStyle">
                                            <table width="100%" cellpadding="1" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 40%;">
                                                    </td>
                                                    <td style="width: 25%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_ProBono" Text="Pro bono" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <div id="div12" style="width: 316px; display: inline-table;">
                                                            <asp:RadioButtonList runat="server" ID="rdo_Tab_ProBono" RepeatDirection="Horizontal"
                                                                CssClass="rdolist" Width="90px">
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Know_How" Text="Know how" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Know_How" CssClass="txtSingleStyle"
                                                            Enabled="false" Visible="true" ReadOnly="true" ToolTip="Corporate only" Text="For corporate deals only select relevant theme from look up"></asp:TextBox>
                                                        <asp:ImageButton runat="server" ID="img_Tab_Know_How" ImageUrl="~/Images/bino.jpg"
                                                            ImageAlign="AbsMiddle" Width="15px" Height="13px" ToolTip="Know How Look Up" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Know_How" />
                                                        <asp:HiddenField runat="server" ID="hid_Tab_Know_How_Text" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Bible_Reference" Text="Bible reference" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:TextBox runat="server" Width="425px" ID="txt_Tab_Bible_Reference" CssClass="txtSingleStyle"
                                                            ToolTip="Corporate only" MaxLength="50" Text="For corporate deals only"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel Width="99%" ID="PnlAdditionalHead" runat="server" Height="11px" CssClass="accordionHeader"
                                            Visible="false">
                                            <asp:Label ID="Label16" runat="server" Text="Additional details"></asp:Label>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 3px; padding-right: 5px;">
                                        <asp:Panel ID="pnlcred" runat="server" CssClass="panelStyle" Visible="false">
                                            <table width="100%" cellpadding="3" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 40%;">
                                                    </td>
                                                    <td style="width: 25%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label Text="Created by" runat="server" CssClass="labelStyle" ID="Label1">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:Label Text="" runat="server" CssClass="labelStyle" ID="lbl_tab_Created_By">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label11" Text="Created date" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Date_Created" Text="" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label8" Text="Last updated by" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:Label ID="lbl_tab_Updated_By" Text="" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label ID="Label12" Text="Last updated date" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lbl_Tab_Date_Updated" Text="" runat="server" CssClass="labelStyle">
                                                        </asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnHide" runat="server" CssClass="hideButton" />
                            <ajax:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                                TargetControlID="btnHide" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
                                BehaviorID="EditModalPopup" DropShadow="false">
                            </ajax:ModalPopupExtender>
                            <asp:Panel ID="Panel1" runat="server" CssClass="modal" Width="350px" BorderStyle="Solid"
                                Height="290px" BorderWidth="1px" GroupingText="LookUp">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center">
                                            <iframe id="IframeEdit" frameborder="0" scrolling="no" height="240px" width="335px"
                                                runat="server" style="background-color: #D3DEEF;"></iframe>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnOk" runat="server" Text="Add" />
                                            <asp:Button ID="btnCancel" Text="Close" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnhide1" runat="server" CssClass="hideButton" />
                            <ajax:ModalPopupExtender ID="ModalPopupExtender2" runat="server" CancelControlID="btnGrdClose"
                                TargetControlID="btnHide" PopupControlID="Panel2" BackgroundCssClass="modalBackground"
                                BehaviorID="EditGridPopup" DropShadow="false">
                            </ajax:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="modallookup" Width="327px" Height="425px">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center">
                                            <iframe id="IframeGrid" frameborder="0" scrolling="no" height="425px" width="429px"
                                                runat="server" style="background-color: #8AC0D1;"></iframe>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="padding-bottom: 10px;">
                                            <asp:Button ID="btnGrdOk" runat="server" Text="ADD" CssClass="btnclose" />
                                            <asp:Button ID="btnGrdClose" Text="CLOSE" runat="server" CssClass="btnclose" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button2" runat="server" CssClass="hideButton" />
                            <ajax:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="Button2"
                                PopupControlID="plnEntry" BackgroundCssClass="modalBackground" BehaviorID="EditEntryPopup"
                                DropShadow="false">
                            </ajax:ModalPopupExtender>
                            <asp:Panel ID="plnEntry" runat="server" CssClass="modal">
                                <table cellpadding="5" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="center">
                                            <iframe id="IframeEntry" frameborder="0" scrolling="no" runat="server" style="background-color: #D3DEEF;">
                                            </iframe>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="padding-right: 20px; padding-top: 5px;">
                                            <asp:Button ID="btnCancelEntry" Text="CLOSE" runat="server" CssClass="btnclose" OnClick="btnCancelEntry_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="plnButton" runat="server" ScrollBars="None" Width="99%">
                <table width="98%" cellpadding="1" cellspacing="0" border="0">
                    <tr>
                        <td style="width: 30%;">
                        </td>
                        <td style="width: 40%;">
                        </td>
                        <td style="width: 25%;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left" style="padding-bottom: 5px;" colspan="2">
                            <div class="gradientbuttons">
                                <ul>
                                    <li runat="server" id="liAddBottom"><a>
                                        <asp:Button ID="btnAddBottom" Text="SUBMIT" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Width="60px" Font-Bold="True" ForeColor="White" Font-Names="VERDANA" Font-Size="X-Small"
                                            OnClick="btnAddBottom_Click" CssClass="handShow" /></a></li>
                                    <li runat="server" id="liPartialSaveBottom" visible="false"><a>
                                        <asp:Button ID="btnPartialSaveBottom" Text="SAVE" runat="server" BorderStyle="None"
                                            BackColor="#00759A" Width="60px" Font-Bold="True" ForeColor="White" Font-Names="VERDANA"
                                            Font-Size="X-Small" OnClick="btnAddBottom_Click" CssClass="handShow" Visible="false" /></a></li>
                                    <li runat="server" id="liEditBottom"><a>
                                        <asp:Button ID="btnEditBottom" Text="UPDATE" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Width="60px" Font-Bold="True" ForeColor="White" Font-Names="VERDANA" Font-Size="X-Small"
                                            CssClass="handShow" OnClick="btnEditBottom_Click" Visible="false" /></a></li>
                                    <li runat="server" id="liDeleteBottom"><a>
                                        <asp:Button ID="btnDeleteBottom" Text="DELETE" runat="server" BorderStyle="None"
                                            BackColor="#00759A" Width="60px" Font-Bold="True" ForeColor="White" Font-Names="VERDANA"
                                            Font-Size="X-Small" CssClass="handShow" Visible="false" OnClick="btnDeleteBottom_Click"
                                            OnClientClick="return ConfirmDelete();" /></a></li>
                                    <li runat="server" id="liClearBottom"><a>
                                        <asp:Button ID="btnClearBottom" Text="CLEAR" runat="server" BorderStyle="None" BackColor="#00759A"
                                            Width="60px" Font-Bold="True" ForeColor="White" Font-Names="VERDANA" Font-Size="X-Small"
                                            CssClass="handShow" OnClick="btnClearTop_Click" OnClientClick="return ConfirmClear();" /></a></li>
                                    <li runat="server" id="liSearchBottom"><a>
                                        <asp:Button ID="btnSearchBottom" Text="SEARCH" runat="server" BorderStyle="None"
                                            BackColor="#00759A" Width="60px" Font-Bold="True" ForeColor="White" Font-Names="VERDANA"
                                            Font-Size="X-Small" CssClass="handShow" OnClick="btnSearchBottom_Click" /></a></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
