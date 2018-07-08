<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="final.WebForm1" %>

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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <a href="/Default.aspx"><img src="assets/logo.png" /></a>
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
    </form>
</body>
</html>
