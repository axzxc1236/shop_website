Imports System.Data.SqlClient

Public Class TransactionHistory
    Inherits System.Web.UI.Page
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader

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

        Dim selected_index As Integer
        If IsPostBack Then
            selected_index = DropDownList1.SelectedIndex
        End If
        DropDownList1.Items.Clear()

        Dim userName As String = HttpContext.Current.User.Identity.Name
        myCmd.CommandText = "select transactionTime from Transactions where member=@member order by transactionTime desc"
        myCmd.Parameters.Add("@member", SqlDbType.NVarChar, 32).Value = userName
        myReader = myCmd.ExecuteReader()
        Dim transTime As Long,
            unix0 As DateTime = New DateTime(1970, 1, 1, 0, 0, 0)
        While myReader.Read
            transTime = Long.Parse(myReader.GetString(0))
            DropDownList1.Items.Add(New ListItem(unix0.AddSeconds(transTime).AddSeconds(28800).ToString, transTime))
            '台灣UTC時區差了28800秒(8小時)
        End While
        myReader.Close()

        If IsPostBack Then
            DropDownList1.SelectedIndex = selected_index
        Else
            DropDownList1_SelectedIndexChanged()
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged() Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedIndex = -1 Then Return
        Dim userName As String = HttpContext.Current.User.Identity.Name
        myCmd.CommandText = "select soldProducts.*, Product.productName from soldProducts join Product on soldProducts.productID = Product.productID where member=@member and transactionTime=@transactionTime"
        myCmd.Parameters.Add("@transactionTime", SqlDbType.NVarChar, 64).Value = DropDownList1.SelectedValue
        myReader = myCmd.ExecuteReader()
        Literal1.Text = String.Empty
        Dim totalCost As Integer
        While myReader.Read
            Literal1.Text += "<div class=""Items"">"
            Literal1.Text += "產品名稱=" + myReader.GetString(7) + "<br />"
            Literal1.Text += "購買數量=" + myReader.GetInt16(6).ToString + "<br />"
            Literal1.Text += "產品單價=" + myReader.GetInt16(5).ToString + "<br />"
            Literal1.Text += "冰塊=" + myReader.GetString(3) + "<br />"
            Literal1.Text += "甜度=" + myReader.GetString(4) + "<br />"
            Literal1.Text += "小記=" + (myReader.GetInt16(6) * myReader.GetInt16(5)).ToString + "<br />"
            totalCost += myReader.GetInt16(6) * myReader.GetInt16(5)
            Literal1.Text += "</div>"
        End While
        myReader.Close()

        myCmd.CommandText = "select name, address, number from Transactions where member=@member and transactionTime=@transactionTime"
        myReader = myCmd.ExecuteReader()
        myReader.Read()
        Literal1.Text += "<div class=""otherDetails"">"
        Literal1.Text += "交易總額:<span id=""price"">" + totalCost.ToString + "</span>元<br />"
        Literal1.Text += "購買者:" + myReader.GetString(0) + "<br />"
        Literal1.Text += "送貨地址:" + myReader.GetString(1) + "<br />"
        Literal1.Text += "聯絡電話:" + myReader.GetString(2) + "<br />"
        Literal1.Text += "</div>"
        myReader.Close()
    End Sub
End Class