<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="changeUserPassword.aspx.vb" Inherits="final.changeUserPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            請選取要被更改密碼的User<br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <br />
            新密碼:<asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            請再輸入一次新密碼:<asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="更改密碼" />
        </div>
    </form>
</body>
</html>
