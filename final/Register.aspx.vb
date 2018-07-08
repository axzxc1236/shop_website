Imports System.Data.SqlClient

Public Class Register
    Inherits System.Web.UI.Page
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If System.Web.HttpContext.Current.User IsNot Nothing And System.Web.HttpContext.Current.User.Identity.IsAuthenticated Then
            Dim tmp As Literal = LoginView1.FindControl("Literal1")
            tmp.Text = System.Web.HttpContext.Current.User.Identity.Name
        End If
        Try
            myConn = New SqlConnection(Web.Configuration.WebConfigurationManager.ConnectionStrings("SQLcommand").ConnectionString)
            myCmd = myConn.CreateCommand
            myConn.Open()
        Catch ex As Exception
            Response.Redirect("/Errors/SQLError.html")
        End Try
    End Sub

    Protected Sub Register_Click(sender As Object, e As EventArgs) Handles Submit1.Click
        '基本邏輯檢查
        UserID.Text = UserID.Text.Trim(" ")
        If UserID.Text Is String.Empty Then
            Msg.Text = "請取一個使用者名稱!"
            Return
        End If
        If UserID.Text.Length > 32 Then
            Msg.Text = "使用者ID太長"
            Return
        End If
        If UserPass.Text <> UserPass2.Text Then
            Msg.Text = "兩個密碼必須相同"
            Return
        End If
        If Not CheckBox1.Checked Then
            Msg.Text = "請同意使用條款"
            Return
        End If
        If UserPass.Text.Length < 8 Then
            Msg.Text = "請使用至少8個字元的密碼"
            Return
        End If
        Name.Text = Name.Text.Trim(" ")
        If Name.Text.Length > 10 Then
            Msg.Text = "姓名欄位太長"
            Return
        End If
        If Number.Text.Length <> 10 And Number.Text.Length <> 0 Then
            Msg.Text = "請輸入正確格式之電話號碼(10位數手機號碼)"
            Return
        ElseIf Number.Text.Length = 10 Then
            For i = 0 To 9
                Dim numbers As String = "0123456789"
                If Not numbers.Contains(Number.Text(i)) Then
                    Msg.Text = "請輸入正確格式之電話號碼(10位數手機號碼)"
                    Return
                End If
            Next
        End If
        Email.Text = Email.Text.Trim(" ")
        If Email.Text IsNot String.Empty And (Not Email.Text.Contains("@") Or Not Email.Text.Contains(".") Or Email.Text.Contains("+")) Then
            '這不是一個好的檢查方式，但要真的確認是否為信箱應該需要外部API
            Msg.Text = "請輸入正確格式之Email信箱"
            Return
        End If
        address.Text = address.Text.Trim(" ")
        If address.Text.Length > 60 Then
            Msg.Text = "地址欄位太長"
            Return
        End If

        '檢查使用者ID是否已被使用
        myCmd.CommandText = "SELECT * FROM Member where member=@memberID"
        myCmd.Parameters.Add("@memberID", SqlDbType.NVarChar, 32).Value = UserID.Text
        If myCmd.ExecuteScalar IsNot Nothing Then
            Msg.Text = "已有相同ID的使用者，請使用其他ID"
            Return
        End If

        '新增使用者帳號
        myCmd.CommandText = "insert into Member(member,password,name,number,email,address) values(@member,@password,@name,@number,@email,@address)"
        myCmd.Parameters.Add("@member", SqlDbType.NVarChar, 32).Value = UserID.Text
        myCmd.Parameters.Add("@password", SqlDbType.NChar, 60).Value = BCrypt.Net.BCrypt.HashPassword(UserPass.Text)
        myCmd.Parameters.Add("@name", SqlDbType.NVarChar, 10).Value = Name.Text
        myCmd.Parameters.Add("@number", SqlDbType.NVarChar, 10).Value = Number.Text
        myCmd.Parameters.Add("@email", SqlDbType.NVarChar, 32).Value = Email.Text
        myCmd.Parameters.Add("@address", SqlDbType.NVarChar, 60).Value = address.Text
        If myCmd.ExecuteNonQuery() = 1 Then
            Msg.Text = "使用者帳號已經新增，三秒鐘後進入登入畫面"
            Response.Write("<script>setTimeout(function () {
                                window.location.replace(""/Login.aspx"");
                            }, 3000);</script>")
        Else
            Msg.Text = "新增使用者時發生錯誤，請再試一次"
        End If
    End Sub
End Class