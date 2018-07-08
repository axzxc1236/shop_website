Imports System.Data.SqlClient

Public Class changeUserPassword
    Inherits System.Web.UI.Page
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            myConn = New SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("SQLcommand").ConnectionString)
            myCmd = myConn.CreateCommand
            myConn.Open()
        Catch ex As Exception
            Response.Redirect("/Errors/SQLError.html")
        End Try

        If Not IsPostBack Then
            myCmd.CommandText = "select member from Member"
            myReader = myCmd.ExecuteReader()
            DropDownList1.Items.Clear()
            While myReader.Read
                DropDownList1.Items.Add(myReader.GetString(0))
            End While
            myReader.Close()
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Label1.Text = String.Empty
        If DropDownList1.SelectedIndex = -1 Then
            Return
        End If
        If TextBox1.Text.Length < 8 Then
            Label1.Text = "請輸入新密碼"
            Return
        End If
        If TextBox1.Text <> TextBox2.Text Then
            Label1.Text = "兩次密碼需要一樣"
            Return
        End If
        myCmd.CommandText = "update Member set password=@password where member=@member"
        myCmd.Parameters.Add("@password", SqlDbType.NChar, 60).Value = BCrypt.Net.BCrypt.HashPassword(TextBox1.Text)
        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        myCmd.Parameters.Add("@member", SqlDbType.NVarChar, 32).Value = DropDownList1.SelectedValue
        If myCmd.ExecuteNonQuery() > 0 Then
            Label1.Text = "密碼更新成功"
        Else
            Label1.Text = "密碼更新失敗!"
        End If
    End Sub
End Class