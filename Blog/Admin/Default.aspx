<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Blog.Admin.Default" %>

<%@ Register Src="~/Admin/UserControl/DataTable.ascx" TagPrefix="Html" TagName="DataTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Style/common.css" rel="stylesheet" />
    <script src="Script/common.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
    首页
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleInfor" runat="server">
    首页介绍
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Breadcrumb" runat="server">
    <a href="Default.aspx">首页</a>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbCurrent" runat="server">
    <a href="Default.aspx">首页</a>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">
    <Html:DataTable runat="server" ID="DataTable" />
</asp:Content>
