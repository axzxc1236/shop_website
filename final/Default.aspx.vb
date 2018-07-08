Public Class WebForm1
    Inherits System.Web.UI.Page

    Private Sub WebForm1_Load(sender As Object, e As EventArgs) Handles Me.Load
        If System.Web.HttpContext.Current.User IsNot Nothing And System.Web.HttpContext.Current.User.Identity.IsAuthenticated Then
            Dim tmp As Literal = LoginView1.FindControl("Literal1")
            tmp.Text = System.Web.HttpContext.Current.User.Identity.Name
        End If
    End Sub
End Class