<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TransactionHistory.aspx.vb" Inherits="final.TransactionHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>交易紀錄</title>
    <style>
        .Items:nth-child(odd) {
            background-color: lightblue
        }

        .Items:nth-child(even) {
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

        .otherDetails {
            background-color:burlywood;
        }

        #price {
        color:seagreen;
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
        交易紀錄:<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
