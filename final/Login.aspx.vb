Imports System.Data.SqlClient

Public Class Login
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
            myConn = New SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("SQLcommand").ConnectionString)
            myCmd = myConn.CreateCommand
            myConn.Open()
        Catch ex As Exception
            Response.Redirect("/Errors/SQLError.html")
        End Try
    End Sub

    Protected Sub Logon_Click(sender As Object, e As EventArgs) Handles Submit1.Click
        myCmd.CommandText = "select password from Member where member = @userID"
        myCmd.Parameters.Add("@userID", SqlDbType.NVarChar, 32).Value = UserID.Text
        Dim hashedpwd As String = myCmd.ExecuteScalar
        If hashedpwd Is Nothing Then
            Msg.Text = "使用者帳號或密碼有誤"
            Return
        End If
        If BCrypt.Net.BCrypt.Verify(UserPass.Text, hashedpwd) Then
            Msg.Text = "使用者認證成功"
            FormsAuthentication.RedirectFromLoginPage(UserID.Text, Persist.Checked)
        Else
            Msg.Text = "使用者帳號或密碼有誤"
        End If
    End Sub
End Class