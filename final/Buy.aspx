<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Buy.aspx.vb" Inherits="final.Buy" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
    <script src="jquery-3.3.1.min.js"></script>
    <script>
        $(document).ready(function () {
            var a = $('.CartItems');
            var b = $('.cancelButton');
            for (i = 0; i < a.length; i++) {
                $(a[i]).append("<br>").append($(b[i]).detach());
            }
        });
    </script>
    <style>
        .CartItems:nth-child(odd) {
            background-color: lightblue
        }

        .CartItems:nth-child(even) {
            background-color: lightgrey
        }

        header {
            background-color: burlywood;
            display: flex;
            left: 0px;
            top: 0px;
        }

            header img {
                float: left;
            }

            header div {
                align-self: flex-end;
                margin-left: 150px;
            }

        #customerDetails {
            background-color:beige;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <a href="/Default.aspx">
                <img src="assets/logo.png" /></a>
            <div>
                <asp:LoginView ID="LoginView1" runat="server">
                    <AnonymousTemplate>
                        <asp:LoginStatus ID="LoginStatus1" runat="server" />
                        &nbsp;&nbsp;&nbsp;
                        <a href="Register.aspx">註冊會員</a>
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        歡迎&nbsp;&nbsp;<asp:Literal ID="Literal1" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LoginStatus ID="LoginStatus2" runat="server" />
                        &nbsp;&nbsp;&nbsp;
                        <a href="Profile.aspx">會員資料</a>&nbsp;&nbsp;&nbsp;
                        <a href="TransactionHistory.aspx">訂購紀錄</a>&nbsp;&nbsp;&nbsp;
                        <a href="Buy.aspx">訂購飲料</a>
                    </LoggedInTemplate>
                </asp:LoginView>
            </div>
        </header>
        <h1>訂購飲料</h1>

        <asp:Label ID="Msg" ForeColor="red" runat="server" />

        <div>
            <table style="width: 100%;">
                <tr>
                    <td>產品名稱</td>
                    <td>數量</td>
                    <td>產品單價</td>
                    <td>冰塊</td>
                    <td>甜度</td>
                    <td colspan="1" rowspan="2">
                        <asp:Button ID="plus" runat="server" Text="+" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:DropDownList ID="productName" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="quantity" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:Label ID="unitPrice" runat="server" Text=" "></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="Ice" runat="server">
                            <asp:ListItem>正常</asp:ListItem>
                            <asp:ListItem>少冰</asp:ListItem>
                            <asp:ListItem>去冰</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="Sugar" runat="server">
                            <asp:ListItem>正常</asp:ListItem>
                            <asp:ListItem>少糖</asp:ListItem>
                            <asp:ListItem>半糖</asp:ListItem>
                            <asp:ListItem>微糖</asp:ListItem>
                            <asp:ListItem>無糖</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <br />
        <div id="placeholder">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
        <br />
        <div id="customerDetails">
            請檢查下列消費者資訊<br />
            消費者姓名:<asp:TextBox ID="customerName" runat="server"></asp:TextBox>
            <br />
            送貨地址:<asp:TextBox ID="address" runat="server"></asp:TextBox>
            <br />
            連絡電話:<asp:TextBox ID="number" runat="server"></asp:TextBox>
            <br />

        <asp:Label ID="Msg2" ForeColor="Red" runat="server" />

            <br />
        </div>
        <br />
        總金額:<asp:Label ID="TotalCost" runat="server" ForeColor="#6666FF" Text="0"></asp:Label>
        元<br />
        <asp:Button ID="createOrder" runat="server" Text="訂購飲料" />
        <br />
        <br />
    </form>
</body>
</html>
