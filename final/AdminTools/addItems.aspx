<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="addItems.aspx.vb" Inherits="final.addItems" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>新增產品</h1>
            商品名稱:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            商品單價:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="新增商品" style="height: 21px" />
            <br />
            <asp:Label ID="Msg" ForeColor="red" runat="server" />
            <br />
            <br />
            商品列表:<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            更改資料:<br />
            商品名稱:<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            商品單價:<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button2" runat="server" Text="更改資料" />
            <br />
            <asp:Label ID="Msg2" ForeColor="Red" runat="server" />
        </div>
    </form>
</body>
</html>
