<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Blog.Default" %>

<%@ Register Src="~/UserControl/SearchComplete.ascx" TagPrefix="Html" TagName="SearchComplete" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>我是一个兵</title>
    <style>
        .file-item {
            list-style:none;
        }
        .file-item li{
            width:250px;
        }
        .file-item li a{
            text-decoration:none;
            color:#000;
            display:block;
            height:15px;
            line-height:15px;
            margin-left:10px;
        }
        .file-item li a:hover{
            color:#ffd800;
            text-decoration:underline;
        }
    </style>
    <%=res %>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%-- <ul>
             <%foreach (var item in lists)
                  { %>
                <li><%=item.id %>---<%=item.name %>----<%=item.path %>----<%=Tool.Fantion.ConvertToDateTime(item.timespan.ToString()) %></li>
                <%} %>
            </ul>
            <%=pg.Navigetion %>
            <div class="clear"></div>--%>
            <%--不使用模版--%>
            <%--<Html:List runat="server" ID="List" Count="20" Field="id,name" Model="Content" CssClassName="new-list" RedirectUrl="./details.aspx"/>--%>
            <%--使用模版--%>
            <%--<Html:List runat="server" ID="List1" Field="*" Model="Content" RedirectUrl="./details.aspx"
                Templet="<dl><dt>{id}</dt><dd>{name}</dd><dd>{content}</dd><dd>{timespan|ConvertToDateTime}</dd></dl>" Count="5" />--%>
            <%--<%=Article.Navigater %>--%>
            <%--<Html:Banner runat="server" AdType="Normal" ID="Banner" Count="10" CssClassName="banner" OrderBy="id desc" />--%>
            <%--<asp:FileUpload ID="FileUpload1" runat="server" />--%>
            <%--<asp:FileUpload ID="FileUpload2" runat="server" />--%>
            <%--<asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />--%>
            <%--<Html:Article runat="server" ID="Article" Model="Content" Templet="<dl><dt>{id}</dt><dd>{name}</dd><dd>{content}</dd><dd>{timespan|ConvertToDateTime}</dd></dl>" />--%>
            <%--<%=Article.Navigeter%>--%>
            <Html:Files runat="server" ID="Files" Model="file" Count="10" IsShowDefualt="true" 
                Templet="<dl><dt>{id}</dt><dd>{title|ToFirstWordsUpper|HighLight='方法,yellow,宋体,20px,false,bolder',###}</dd><dd>{ico|Detault='我很懒，不想留下脚印',###}</dd><dd>{path}</dd><dd>{timespan|ConvertToDateTime|ToTrim}</dd></dl>"/>
            <%--<Html:SearchComplete runat="server" ID="SearchComplete" />--%>
        </div>
    </form>
</body>
</html>
