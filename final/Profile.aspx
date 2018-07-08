<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Profile.aspx.vb" Inherits="final.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>爽飲飲料店</title>
    <style>
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

        #Profile table {
            margin-left: auto;
            margin-right: auto;
        }

        #Profile h3 {
        text-align: center;
        }

        #Submit1 {
            margin:159px;
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
        <div id="Profile">
            <h3>會員資料</h3>
            <table>
                <tr>
                    <td>會員ID</td>
                    <td>
        <asp:Label ID="member" runat="server" Text="Label"></asp:Label>
            &nbsp;(不開放修改)</td>
                </tr>
                <tr>
                    <td>真實姓名</td>
                    <td>
         
        <asp:TextBox ID="name" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>電話</td>
                    <td>
         
        <asp:TextBox ID="number" runat="server" TextMode="Phone"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>電子信箱
         
        </td>
                    <td>
         
        <asp:TextBox ID="email" runat="server" TextMode="Email"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>寄送地址</td>
                    <td>
         
        <asp:TextBox ID="address" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新密碼(不更改則留空)</td>
                    <td>
        <asp:TextBox ID="newPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>確認新密碼</td>
                    <td> 
        <asp:TextBox ID="newPassword2" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
            <asp:Button ID="Button1" runat="server" Text="更改資料" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">

            <asp:Label ID="Msg" ForeColor="red" runat="server" />

                    </td>
                </tr>
                </table>
            <br />

            <br />
        </div>
    </form>
</body>
</html>
