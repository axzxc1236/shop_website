<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Register.aspx.vb" Inherits="final.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>註冊會員</title>
    <style>
        #TOS {
            visibility: hidden;
            background-color: rgba(105,105,105,0.5);
            position: fixed;
            padding: 0;
            margin: 0;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            opacity: 0.5;
        }

        #TOScontent {
            visibility: hidden;
            position: fixed;
            padding: 0;
            margin: 0;
            top: 10%;
            left: 10%;
            width: 80%;
            height: 80%;
            background-color: white;
        }

        #hideTOS {
            visibility: hidden;
            position: fixed;
            padding: 0;
            margin: 0;
            top: 0%;
            right: 0%;
            width: 5%;
            height: 5%;
            background-color: white;
        }

            #hideTOS h1 {
                margin: 0 auto;
            }

        #showTOS {
            color: blue;
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

        .auto-style1 {
            height: 23px;
        }

        #SignUp table {
            margin-left: auto;
            margin-right: auto;
        }

        #SignUp h3 {
            text-align: center;
        }

        #Submit1 {
            margin-left: 120px
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
        <div id="SignUp">
            <h3>註冊頁面</h3>
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
                    <td class="auto-style1">再次輸入密碼:</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="UserPass2" TextMode="Password"
                            runat="server" />
                    </td>
                    <td class="auto-style1">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            ControlToValidate="UserPass2"
                            ErrorMessage="不能留空"
                            runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>真實姓名:</td>
                    <td>
                        <asp:TextBox ID="Name" runat="server" /></td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>電話:</td>
                    <td>
                        <asp:TextBox ID="Number" runat="server" TextMode="Phone" /></td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>電子信箱:</td>
                    <td>
                        <asp:TextBox ID="Email" runat="server" TextMode="Email" /></td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>寄送地址(選填):</td>
                    <td>
                        <asp:TextBox ID="address" runat="server" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" TextAlign="Left" Text="　　　　　　" />
                    </td>
                    <td>我同意爽飲飲料店網站之<a id="showTOS" onclick="TOS()">服務條款</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Submit1" OnClick="Register_Click" Text="註冊"
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
    <div id="TOS"></div>
    <div id="TOScontent">
        爽飲飲料店並不是真的飲料店，如果有同樣名稱的飲料店，與本網站無關<br />
        *在爽飲飲料店網站訂購飲料並不會真的收到任何飲料，想喝飲料請找其他飲料店購買<br />
    </div>
    <div id="hideTOS">
        <h1>X</h1>
    </div>
    <script>
        function TOS() {
            var tos = document.getElementById("TOS");
            tos.style.visibility = "visible";
            var toscontent = document.getElementById("TOScontent");
            toscontent.style.visibility = "visible";
            var hidetos = document.getElementById("hideTOS");
            hidetos.style.visibility = "visible";
        }

        window.onload = function () {
            var hidetos = document.getElementById("hideTOS");
            hidetos.onclick = function () {
                hidetos.style.visibility = "hidden";
                var tos = document.getElementById("TOS");
                tos.style.visibility = "hidden";
                var toscontent = document.getElementById("TOScontent");
                toscontent.style.visibility = "hidden";
            }
        }
    </script>
</body>
</html>
