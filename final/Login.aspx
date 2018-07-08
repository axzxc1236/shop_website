<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="final.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登入頁面</title>
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

        #Login table {
            margin-left: auto;
            margin-right: auto;
        }

        #Login h3 {
            text-align: center;
        }

        #Submit1 {
            margin-left: 73px
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
        <div id="Login">
            <h3>登入頁面</h3>
            <table>
                <tr>
                    <td>使用者ID:</td>
                    <td>
                        <asp:TextBox ID="UserID" runat="server" /></td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            ControlToValidate="UserID"
                            Display="Dynamic"
                            ErrorMessage="不能留空"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>密碼:</td>
                    <td>
                        <asp:TextBox ID="UserPass" TextMode="Password"
                            runat="server" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            ControlToValidate="UserPass"
                            ErrorMessage="不能留空"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:CheckBox ID="Persist" runat="server" Text="記住我" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Submit1" OnClick="Logon_Click" Text="登入"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Msg" ForeColor="red" runat="server" />
                    </td>
                </tr>
            </table>
            <p>
                &nbsp;
            </p>
        </div>
    </form>
</body>
</html>
