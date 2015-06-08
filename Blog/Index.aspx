<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Blog.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/Jquery/jquery.1.10.0.min.js"></script>
    <script>
        function delId(id) {
            if (id != '') {
                $.getJSON('Handler1.ashx', { id: id }, function (data) {
                    alert(data.status + "---" + data.msg);
                });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <ul>
            <%foreach(var item in ad){%>
                <li><a href="javascript:void(0);" onclick="delId(<%=item.id %>);"><%=item.name %></a></li>
            <%} %>
       </ul>
    </div>
    </form>
</body>
</html>
