Imports System.Data.SqlClient

Public Class Buy
    Inherits System.Web.UI.Page
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim userName As String = HttpContext.Current.User.Identity.Name
        If System.Web.HttpContext.Current.User IsNot Nothing And System.Web.HttpContext.Current.User.Identity.IsAuthenticated Then
            Dim tmp_ As Literal = LoginView1.FindControl("Literal1")
            tmp_.Text = userName
        End If
        Try
            myConn = New SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings("SQLcommand").ConnectionString)
            myCmd = myConn.CreateCommand
            myConn.Open()
        Catch ex As Exception
            Response.Redirect("/Errors/SQLError.html")
        End Try

        Dim tmp As Integer
        If IsPostBack Then
            tmp = productName.SelectedIndex
        End If

        productName.Items.Clear()
        myCmd.CommandText = "select * from Product"
        myReader = myCmd.ExecuteReader()
        While myReader.Read
            productName.Items.Add(New ListItem(myReader.GetString(1), myReader.GetString(0)))
        End While
        myReader.Close()

        myCmd.Parameters.Add("@member", SqlDbType.NVarChar, 32).Value = userName
        If IsPostBack Then
            productName.SelectedIndex = tmp
            myCmd.Parameters.Add("@productID", SqlDbType.NVarChar, 10).Value = productName.SelectedValue
        Else
            '取得選取物品之單價
            myCmd.CommandText = "select price from Product where productID=@productID"
            myCmd.Parameters.Add("@productID", SqlDbType.NVarChar, 10).Value = productName.SelectedValue
            unitPrice.Text = myCmd.ExecuteScalar

            '取得已輸入之消費者資訊
            myCmd.CommandText = "select name,address,number from Member where member=@member"
            myReader = myCmd.ExecuteReader()
            myReader.Read()
            customerName.Text = myReader.GetString(0)
            address.Text = myReader.GetString(1)
            number.Text = myReader.GetString(2)
            myReader.Close()
        End If
        RefreshCart()
    End Sub

    Protected Sub productname_selectedindexchanged(sender As Object, e As EventArgs) Handles productName.SelectedIndexChanged
        '取得選取物品之單價
        myCmd.CommandText = "select price from product where productid=@productid"
        unitPrice.Text = myCmd.ExecuteScalar
    End Sub

    Protected Sub plus_Click(sender As Object, e As EventArgs) Handles plus.Click
        '檢查購買數量
        quantity.Text = quantity.Text.Trim(" ").TrimStart("0")
        Dim numbers As String = "0123456789"
        If quantity.Text Is String.Empty Then
            Msg.Text = "請輸入商品數量"
            Return
        End If
        For i = 0 To quantity.Text.Length - 1
            If Not numbers.Contains(quantity.Text(i)) Then
                Msg.Text = "商品數量欄位中只能輸入正整數"
                Return
            End If
        Next
        Msg.Text = String.Empty
        Dim userName As String = HttpContext.Current.User.Identity.Name
        myCmd.CommandText = "select productName, price from Product where productID=@productID"
        myReader = myCmd.ExecuteReader()
        myReader.Read()

        If myReader.GetString(0) = productName.SelectedItem.Text And myReader.GetInt16(1) = Integer.Parse(unitPrice.Text) Then
            myReader.Close()

            '確認購物車裡是否已經有同樣產品ID而且冰塊、甜度一樣的紀錄
            myCmd.CommandText = "select * from Cart where member=@member and productID=@productID and ice=@ice and sugar=@sugar"
            myCmd.Parameters.Add("@ice", SqlDbType.NChar, 2).Value = Ice.SelectedValue
            myCmd.Parameters.Add("@sugar", SqlDbType.NChar, 2).Value = Sugar.SelectedValue
            myReader = myCmd.ExecuteReader()
            If myReader.HasRows Then
                '如果購物車裡已經有同樣產品ID而且冰塊、甜度一樣的紀錄  只需要更新數量欄位和unixTime
                myReader.Read()
                Dim newQuantity As Integer = Integer.Parse(quantity.Text) + myReader.GetInt16(2)
                Dim oldUnixTime As String = myReader.GetString(5)
                myReader.Close()
                Dim newUnixTime As String = getUnixTime()
                myCmd.CommandText = "update Cart set quantity=@quantity, unixTime=@newUnixTime where member=@member and unixTime=@oldUnixTime"
                myCmd.Parameters.Add("@quantity", SqlDbType.SmallInt).Value = newQuantity
                myCmd.Parameters.Add("@newUnixTime", SqlDbType.NVarChar, 64).Value = newUnixTime
                myCmd.Parameters.Add("@oldUnixTime", SqlDbType.NVarChar, 64).Value = oldUnixTime
                If myCmd.ExecuteNonQuery <= 0 Then
                    Msg.Text = "無法新增這筆物品到購物車，請再試一次"
                End If
            Else
                myReader.Close()
                '新增記錄到購物車
                Dim unixTime As String = getUnixTime()
                myCmd.CommandText = "insert into Cart(member,productID,quantity,ice,sugar,unixTime) values(@member,@productID,@quantity,@ice,@sugar,@unixTime)"
                myCmd.Parameters.Add("@quantity", SqlDbType.SmallInt).Value = Integer.Parse(quantity.Text)
                myCmd.Parameters.Add("@unixTime", SqlDbType.NVarChar, 64).Value = unixTime
                If myCmd.ExecuteNonQuery <= 0 Then
                    Msg.Text = "無法新增這筆物品到購物車，請再試一次"
                End If
            End If
            RefreshCart()
        Else
            Msg.Text = "交易時發生嚴重錯誤"
            'Response.Write("<script>location.reload()</script>")
        End If
    End Sub

    Sub RefreshCart()
        Dim userName As String = HttpContext.Current.User.Identity.Name
        myCmd.CommandText = "select ProductName,quantity,quantity*product.price,sugar,ice from Cart join Product on Cart.productID = Product.productID where member=@member order by unixTime"
        myReader = myCmd.ExecuteReader()
        Dim count, cost As Integer
        Dim tmp As PlaceHolder = form1.FindControl("PlaceHolder1")
        tmp.Controls.Clear()
        createOrder.Visible = myReader.HasRows  '購物車裡有物品才能訂購飲料
        While myReader.Read()
            count += 1
            Dim cancel As Button = New Button
            cancel.ID = "cancel" + count.ToString
            cancel.Text = "取消訂單"
            cancel.CssClass = "cancelButton"
            AddHandler cancel.Click, AddressOf Me.cancel_Click

            Dim tmp2 As Literal = New Literal
            tmp2.Text = "<div id=""cartItem" + count.ToString + """ class=""CartItems"">"
            tmp2.Text += "產品名稱:<span id=""Name"">" + myReader.GetString(0) + "</span><br/>"
            tmp2.Text += "數量:<span id=""Quantity"">" + myReader.GetInt16(1).ToString + "</span><br/>"
            cost += myReader.GetInt16(2)
            tmp2.Text += "小記:<span id=""price"" >" + myReader.GetInt16(2).ToString + "</span><br/>"
            tmp2.Text += "冰塊:<span id=""ice"">" + myReader.GetString(4).ToString + "</span><br/>"
            tmp2.Text += "甜度:<span id = ""sugar"">" + myReader.GetString(3).ToString + "</span></div>"
            tmp.Controls.Add(tmp2)
            tmp.Controls.Add(cancel)
        End While
        TotalCost.Text = cost.ToString
        myReader.Close()
    End Sub

    Protected Sub cancel_Click(sender As Object, e As EventArgs)
        Dim userName As String = HttpContext.Current.User.Identity.Name
        '查詢交易詳細資料
        Dim btn As Button = sender
        Dim ID As Integer = Integer.Parse(btn.ID.Replace("cancel", ""))
        myCmd.CommandText = "select * from Cart where member=@member"
        myReader = myCmd.ExecuteReader()
        If myReader.HasRows Then

            Try
                For i = 1 To ID
                    myReader.Read()
                Next
            Catch ex As Exception
                Msg.Text = "該交易紀錄不存在"
                myReader.Close()
                Return
            End Try
            myCmd.CommandText = "delete from Cart where member=@member and unixTime=@unixTime"
            myCmd.Parameters.Add("@unixTime", SqlDbType.NVarChar, 64).Value = myReader.GetString(5)
            myReader.Close()
            If myCmd.ExecuteNonQuery <= 0 Then
                Msg.Text = "無法刪除交易紀錄，請再次嘗試"
            Else
                RefreshCart()
            End If
        Else
            Msg.Text = "該交易紀錄不存在"
            myReader.Close()
        End If
    End Sub

    Protected Sub createOrder_Click(sender As Object, e As EventArgs) Handles createOrder.Click
        '消費者資訊基本邏輯檢查
        customerName.Text = customerName.Text.Trim(" ")
        If customerName.Text.Length > 32 Then
            Msg2.Text = "姓名欄位太長"
            Return
        End If
        If number.Text.Length <> 10 Then
            Msg2.Text = "請輸入正確格式之電話號碼(10位數手機號碼)"
            Return
        End If
        For i = 0 To 9
            Dim numbers As String = "0123456789"
            If Not numbers.Contains(number.Text(i)) Then
                Msg2.Text = "請輸入正確格式之電話號碼(10位數手機號碼)"
                Return
            End If
        Next
        address.Text = address.Text.Trim(" ")
        If address.Text.Length > 60 Then
            Msg2.Text = "地址欄位太長"
            Return
        End If
        If address.Text Is String.Empty Then
            Msg2.Text = "請輸入送貨地址!"
            Return
        End If

        Dim userName As String = HttpContext.Current.User.Identity.Name
        Dim transaction As SqlTransaction = myConn.BeginTransaction("createOrder_" & userName)
        myCmd.Transaction = transaction
        Try
            Dim transTime As String = getUnixTime()
            myCmd.CommandText = "insert into Transactions(member,transactionTime,name,number,address) values(@member,@transactionTime,@name,@number,@address)"
            myCmd.Parameters.Add("@transactionTime", SqlDbType.NVarChar, 64).Value = transTime
            myCmd.Parameters.Add("@name", SqlDbType.NVarChar, 32).Value = customerName.Text
            myCmd.Parameters.Add("@number", SqlDbType.NVarChar, 10).Value = number.Text
            myCmd.Parameters.Add("@address", SqlDbType.NVarChar, 60).Value = address.Text
            If myCmd.ExecuteNonQuery() <= 0 Then
                Throw New Exception("交易錯誤")
            End If

            myCmd.CommandText = "select * from Cart where member=@member"
            myReader = myCmd.ExecuteReader()
            Dim productIds As List(Of String) = New List(Of String),
                quantities As List(Of UInt16) = New List(Of UShort),
                icechoices As List(Of String) = New List(Of String),
                sugarchoices As List(Of String) = New List(Of String)
            While myReader.Read
                productIds.Add(myReader.GetString(1))
                quantities.Add(myReader.GetInt16(2))
                icechoices.Add(myReader.GetString(3))
                sugarchoices.Add(myReader.GetString(4))
            End While
            myReader.Close()

            myCmd.Parameters.Add("@ice", SqlDbType.NChar, 2).Value = ""
            myCmd.Parameters.Add("@sugar", SqlDbType.NChar, 2).Value = ""
            myCmd.Parameters.Add("@unitPrice", SqlDbType.SmallInt).Value = 0
            myCmd.Parameters.Add("@quantity", SqlDbType.SmallInt).Value = 0
            For i = 0 To productIds.Count - 1
                Dim unitPrice As UShort
                myCmd.CommandText = "select price from Product where productID=@productID"
                myCmd.Parameters("@productID").Value = productIds(i)
                unitPrice = myCmd.ExecuteScalar

                myCmd.CommandText = "insert into soldProducts(member,transactionTime,productID,ice,sugar,unitPrice,quantity) values(@member,@transactionTime,@productID,@ice,@sugar,@unitPrice,@quantity)"
                myCmd.Parameters("@ice").Value = icechoices.Item(i)
                myCmd.Parameters("@sugar").Value = sugarchoices(i)
                myCmd.Parameters("@unitPrice").Value = unitPrice
                myCmd.Parameters("@quantity").Value = quantities.Item(i)
                If myCmd.ExecuteNonQuery() <= 0 Then
                    Throw New Exception("交易錯誤")
                End If
            Next

            myCmd.CommandText = "delete from Cart where member=@member"
            If myCmd.ExecuteNonQuery() <= 0 Then
                Throw New Exception("交易錯誤")
            End If

            transaction.Commit()

            Msg.Text = String.Empty
            Msg2.Text = String.Empty

            RefreshCart()
        Catch ex As Exception
            MsgBox(ex.data.tostring)
            myreader.close()
            Try
                transaction.rollback()
                Msg.Text = "交易時發生錯誤! (rolled back)"
            Catch ey As exception
                msg.text = "交易時發生嚴重錯誤! (failed to roll back)"
            End Try
        End Try
    End Sub

    Function getUnixTime() As ULong
        Return (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
    End Function
End Class