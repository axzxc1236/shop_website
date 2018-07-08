Imports System.Data.SqlClient

Public Class Profile
    Inherits System.Web.UI.Page
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userName As String = System.Web.HttpContext.Current.User.Identity.Name
        If System.Web.HttpContext.Current.User IsNot Nothing And System.Web.HttpContext.Current.User.Identity.IsAuthenticated Then
            Dim tmp As Literal = LoginView1.FindControl("Literal1")
            tmp.Text = userName
        End If
        member.Text = userName

        Try
            myConn = New SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("SQLcommand").ConnectionString)
            myCmd = myConn.CreateCommand
            myConn.Open()
        Catch ex As Exception
            Response.Redirect("/Errors/SQLError.html")
        End Try
        If Not IsPostBack Then
            myCmd.CommandText = "select * from Member where member='" + userName + "'"
            myReader = myCmd.ExecuteReader()
            myReader.Read()
            name.Text = myReader.GetString(2).TrimEnd(" ")
            number.Text = myReader.GetString(3).TrimEnd(" ")
            email.Text = myReader.GetString(4).TrimEnd(" ")
            address.Text = myReader.GetString(5).TrimEnd(" ")
            myReader.Close()
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim userName As String = System.Web.HttpContext.Current.User.Identity.Name
        name.Text = name.Text.Trim(" ")
        email.Text = email.Text.Trim(" ")
        address.Text = address.Text.Trim(" ")
        '檢查是否更改密碼還有檢查新密碼是否符合規則
        Dim changepassword As Boolean
        If newPassword.Text IsNot String.Empty Or newPassword2.Text IsNot String.Empty Then
            If newPassword.Text.Length < 8 Then
                Msg.Text = "請使用至少8字元長度的密碼"
                Return
            ElseIf newPassword.Text <> newPassword2.Text Then
                Msg.Text = "輸入的兩次新密碼不一樣"
                Return
            Else
                Msg.Text = ""
                changepassword = True
            End If
        Else
            Msg.Text = ""
            changepassword = False
        End If
        '基本邏輯檢查
        If name.Text.Length > 32 Then
            Msg.Text = "姓名欄位太長"
            Return
        End If
        If number.Text.Length <> 10 And number.Text.Length <> 0 Then
            Msg.Text = "請輸入正確格式之電話號碼(10位數手機號碼)"
            Return
        ElseIf number.Text.Length = 10 Then
            For i = 0 To 9
                Dim numbers As String = "0123456789"
                If Not numbers.Contains(number.Text(i)) Then
                    Msg.Text = "請輸入正確格式之電話號碼(10位數手機號碼)"
                    Return
                End If
            Next
        End If
        If email.Text IsNot String.Empty And (Not email.Text.Contains("@") Or Not email.Text.Contains(".") Or email.Text.Contains("+")) Then
            '這不是一個好的檢查方式，但要真的確認是否為信箱應該需要外部API
            Msg.Text = "請輸入正確格式之Email信箱"
            Return
        End If
        If address.Text.Length > 60 Then
            Msg.Text = "地址欄位太長"
            Return
        End If

        Dim transaction As SqlTransaction = myConn.BeginTransaction("changeUserProfile_" & userName)
        myCmd.Transaction = transaction
        Try
            myCmd.CommandText = "update Member set name=@name, number=@number, email=@email, address=@address where member=@member"
            myCmd.Parameters.Add("@name", SqlDbType.NVarChar, 10).Value = name.Text
            myCmd.Parameters.Add("@number", SqlDbType.NVarChar, 10).Value = number.Text
            myCmd.Parameters.Add("@email", SqlDbType.NVarChar, 32).Value = email.Text
            myCmd.Parameters.Add("@address", SqlDbType.NVarChar, 60).Value = address.Text
            myCmd.Parameters.Add("@member", SqlDbType.NVarChar, 32).Value = userName
            If myCmd.ExecuteNonQuery = -1 Then
                Msg.Text = "更新基本資料失敗" & vbCrLf
                MsgBox(myCmd.CommandText)
                Throw New Exception("更新基本資料失敗")
            End If
            Msg.Text = "更改會員資料成功"

            If changepassword Then
                myCmd.CommandText = "update Member set password=@password where member=@member"
                myCmd.Parameters.Add("@password", SqlDbType.NChar, 60).Value = BCrypt.Net.BCrypt.HashPassword(newPassword.Text)
                If myCmd.ExecuteNonQuery = -1 Then
                    Msg.Text = "更新用戶密碼失敗" & vbCrLf
                    Throw New Exception("更新用戶密碼失敗")
                End If
                Msg.Text += vbCrLf
                Msg.Text += "  密碼修改成功，下次登入時請使用新密碼"
            End If

            transaction.Commit()
        Catch ex As Exception
            Try
                transaction.Rollback()
                Msg.Text += "更改會員資料時發生錯誤! (rolled back)"
            Catch ey As Exception
                Msg.Text += "更改會員資料時發生嚴重錯誤! (Failed to rollback)"
            End Try
        End Try

    End Sub
End Class