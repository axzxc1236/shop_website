Imports System.Data.SqlClient

Public Class addItems
    Inherits System.Web.UI.Page
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private product As Integer = 0
    Private prices As List(Of Integer) = New List(Of Integer)
    Private productNames As List(Of String) = New List(Of String)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            myConn = New SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("SQLcommand").ConnectionString)
            myCmd = myConn.CreateCommand
            myConn.Open()
        Catch ex As Exception
            Response.Redirect("/Errors/SQLError.html")
        End Try

        '在清空DropDownList1之前先記錄使用者選取的欄位
        Dim tmp As Integer
        If IsPostBack Then
            tmp = DropDownList1.SelectedIndex
        End If

        '新增現有商品清單
        DropDownList1.Items.Clear()
        myCmd.CommandText = "select * from Product"
        myReader = myCmd.ExecuteReader()
        While myReader.Read
            product += 1
            DropDownList1.Items.Add(myReader.GetString(1) + "  單價:" + myReader.GetInt16(2).ToString + "元")
            prices.Add(myReader.GetInt16(2))
            productNames.Add(myReader.GetString(1))
        End While
        myReader.Close()

        '恢復DropDownList原先選取欄位
        If IsPostBack Then
            DropDownList1.SelectedIndex = tmp
        Else
            TextBox3.Text = productNames.Item(0)
            TextBox4.Text = prices.Item(0)
        End If

        Dim commandText As String = "SELECT * FROM Member where member = @memberID "
        Dim myCmd2 As SqlCommand = New SqlCommand(commandText, myConn)
        myCmd2.Parameters.Add("@memberID", SqlDbType.NChar, 32)
        myCmd2.Parameters("@memberID").Value = "admin"
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text Is String.Empty Or TextBox2.Text Is String.Empty Then
            Msg.Text = "請填寫欄位"
            Return
        End If
        If Not DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(TextBox1.Text)) = -1 Then
            Msg.Text = "有相同產品存在"
            Return
        End If

        '加入產品
        myCmd.CommandText = "insert into Product(productID,productName,price) values('" + (product + 1).ToString.PadLeft(3, "0") + "','" + TextBox1.Text + "','" + TextBox2.Text + "')"
        myReader = myCmd.ExecuteReader()
        myReader.Close()

        '檢查商品是否確實加入
        myCmd.CommandText = "select * from Product where productName='" + TextBox1.Text + "'"
        myReader = myCmd.ExecuteReader()
        If myReader.HasRows Then
            '產品成功加入後執行動作
            DropDownList1.Items.Add(TextBox1.Text + "  單價:" + TextBox2.Text + "元")
            prices.Add(Integer.Parse(TextBox2.Text))
            productNames.Add(TextBox1.Text)
            product += 1
            TextBox1.Text = String.Empty
            TextBox2.Text = String.Empty
            Msg.Text = String.Empty
        Else
            Msg.Text = "商品加入失敗"
        End If
        myReader.Close()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '更新資料
        Dim index As String = (DropDownList1.SelectedIndex).ToString.PadLeft(3, "0")
        myCmd.CommandText = "update Product set productName = '" + TextBox3.Text + "', price = '" + TextBox4.Text + "' where productID = '" + index + "'"
        myReader = myCmd.ExecuteReader()
        myReader.Close()

        '確認資料是否確實更新
        myCmd.CommandText = "select * from Product where productName = '" + TextBox3.Text + "' and price = '" + TextBox4.Text + "' and productID = '" + index + "'"
        myReader = myCmd.ExecuteReader()
        If myReader.HasRows Then
            Msg2.Text = "商品 " + TextBox3.Text + " 的資料已成功更新"
        Else
            Msg2.Text = "資料更新失敗"
        End If
        myReader.Close()

        DropDownList1.SelectedItem.Value = TextBox3.Text + "  單價:" + TextBox4.Text + "元"
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        TextBox3.Text = productNames.Item(DropDownList1.SelectedIndex)
        TextBox4.Text = prices.Item(DropDownList1.SelectedIndex)
    End Sub
End Class