<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRCode.aspx.cs" Inherits="Blog.QRCode" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        文本<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br/>
        <asp:Button ID="Button1" runat="server" Text="确定" OnClick="Button1_Click" /><br/>
        <asp:FileUpload ID="FileUpload1" runat="server" /><br/>
        <asp:Button ID="Button2" runat="server" Text="提交" OnClick="Button2_Click" /><br/>
        <asp:Image ID="Image1" runat="server" />
    </div>
    </form>
</body>
</html>
