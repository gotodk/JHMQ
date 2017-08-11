<%@ Page Language="C#"   MasterPageFile="~/adminht/MasterPageMain.master"  AutoEventWireup="true" CodeFile="syscreatingfiles.aspx.cs" Inherits="adminht_baseset_syscreatingfiles" %>

<%@ Register Src="~/pucu/wuc_css.ascx" TagPrefix="uc1" TagName="wuc_css" %>
<%@ Register Src="~/pucu/wuc_content.ascx" TagPrefix="uc1" TagName="wuc_content" %>
<%@ Register Src="~/pucu/wuc_script.ascx" TagPrefix="uc1" TagName="wuc_script" %>


<asp:Content ID="Content1" ContentPlaceHolderID="sp_head" runat="Server">
    <!-- 往模板页附加的head内容 -->
    <uc1:wuc_css runat="server" ID="wuc_css" />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="sp_daohang" runat="Server">
    <!-- 附加的本页导航栏内容 -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="sp_pagecontent" runat="Server">
    <!-- 附加的右侧主要功能切换区内容,不含导航 -->
    <uc1:wuc_content runat="server" ID="wuc_content"  />
 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="sp_script" runat="Server">

       <!-- 附加的body底部本页专属的自定义js脚本 -->
    <uc1:wuc_script runat="server" ID="wuc_script" />

        <script type="text/javascript">
        jQuery(function ($) {
            if (getUrlParam("scleixing") == "liebiao") {
                $("#mbleixing").val("通用列表");
                $("#uimod").val("/pucu/CreatingFileMod/listonlyformod.aspx");
            }
            if (getUrlParam("scleixing") == "biaodan") {
                $("#mbleixing").val("一般表单");
                $("#uimod").val("/pucu/CreatingFileMod/formonlyformod.aspx");
            }
            
            $("#peizhizhujian").val(getUrlParam("pzzhujian"));

            

        });
    </script>
</asp:Content>