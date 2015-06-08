<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner.ascx.cs" Inherits="Blog.UserControl.Banner" %>
<%if(_isShow){ %>
<script src="../Scripts/bannerBox.js" type="text/javascript" charset="uft-8"></script>
<style type="text/css">
    *{margin:0;padding:0;} 
    ul.banner-list-content{list-style:none;}
    ul.banner-list-content>li{color:<%=_color%>;}
    ul.banner-list-content>li.active{color:<%=_bgcolor%>;}
</style>
<%}%>
<%if (_isfloat){ %>
<script src="../Scripts/floatAd.js" type="text/javascript" charset="uft-8"></script>
<%}%>
<%if (_iscouplet){ %>
<script src="../Scripts/coupletAd.js" type="text/javascript" charset="uft-8"></script>
<%}%>
<%=_html %>