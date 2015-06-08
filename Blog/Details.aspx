<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="Blog.Details" %>
<%@ Register Src="~/UserControl/Article.ascx" TagPrefix="Html" TagName="Article" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        * {
            margin:0px;
            padding:0px;
        }
        .article-navigater {
            display:block;
        }
        .article-navigater ul li{
            list-style-type:none;
            float:left;
         }
        .article-navigater ul li a{
            display:block;
            padding:5px;
            color:#808080;
            text-decoration:none;
        }
        .article-navigater ul li a:hover{
            text-decoration:underline;
            color:#0094ff;
        }
    </style>
</head>
<body>
    <form id="form1">
        <div>
            <Html:Article runat="server" ID="Article" Templet="<h1>{name}<h1><div>{timespan|ConvertToDateTime}</div>"  />
        </div>
        <%=Article.Navigater %>
    </form>
</body>
</html>
