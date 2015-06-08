<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/master/Site.Master" AutoEventWireup="true" CodeBehind="inbox.aspx.cs" Inherits="Blog.Admin.inbox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="media/css/jquery.fileupload-ui.css" rel="stylesheet" type="text/css" >
	<!-- END:File Upload Plugin CSS files-->     
	<link href="media/css/inbox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleInfor" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Breadcrumb" runat="server">  
    <a href="Default.aspx">首页</a>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BreadcrumbCurrent" runat="server">
   <a href="inbox.aspx">消息</a>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="PageContent" runat="server">
    <div class="row-fluid inbox">
		<div class="span2">
			<ul class="inbox-nav margin-bottom-10">
				<li class="compose-btn">
					<a href="javascript:;" data-title="Compose" class="btn green"> 
					<i class="icon-edit"></i> Compose
					</a>
				</li>
				<li class="inbox active">
					<a href="javascript:;" class="btn" data-title="Inbox">Inbox(3)</a>
					<b></b>
				</li>
				<li class="sent"><a class="btn" href="javascript:;"  data-title="Sent">Sent</a><b></b></li>
				<li class="draft"><a class="btn" href="javascript:;" data-title="Draft">Draft</a><b></b></li>
				<li class="trash"><a class="btn" href="javascript:;" data-title="Trash">Trash</a><b></b></li>
			</ul>
		</div>
		<div class="span10">
			<div class="inbox-header">
				<h1 class="pull-left">Inbox</h1>
				<form action="#" class="form-search pull-right">
					<div class="input-append">
						<input class="m-wrap" type="text" placeholder="Search Mail">
						<button class="btn green" type="button">Search</button>
					</div>
				</form>
			</div>
			<div class="inbox-loading">Loading...</div>
			<div class="inbox-content">
                Loading...
			</div>
		</div>
	</div>
</asp:Content>
