# shop_website

這是這學期學資料庫後老師出的期末作業(做一個運用到資料庫系統的網站)  (我們老師只教資料庫...完全不教網站怎麼做)  
做的不管是好是壞，我以後(應該)不會再維護這個網站  
本專案採用MIT授權，你可以照你想要的修改(詳細請看LICENSE檔案)  
請注意，本專案使用的各項NuGet套件可能有不同的授權，需要自行去觀看  




### 軟體環境:  
[.NET FRAMEWORK 4.0](https://www.microsoft.com/zh-tw/download/details.aspx?id=17718)  
使用MSSQL資料庫  
使用IIS伺服器  
以VB.NET語言開發  
用Visual Studio 2017開發  (但應該可以與之後的Visual Studio相容)

### 如何使用:
1.在電腦上架設IIS伺服器和MSSQL資料庫伺服器  
2.更改final/web.config檔案內容  
(DBNAME)更改為實際資料庫名稱 (不含括弧)  
(serverIP)更改為資料庫伺服器IP (不含括弧)  
(USER)更改為資料庫帳號名稱 (不含括弧)  
(PASSWORD)更改為資料庫帳號的登入密碼 (不含括弧)  
將web.config存檔  
3.用Visual Studio編譯並發行專案  
4.在final/bin/Release/Publish資料夾下找到編譯好的網站，複製到IIS伺服器上  
5.進入SQLcommands資料夾  
6.在各個文字檔裡把[DBNAME]更改成實際資料庫名稱  
7.在MSSQL網站上根據文字檔案名稱依序使用指令  
create_Member => create_Product => create_Cart => create_Transactions => create_soldProducts  
8.在(網站伺服器IP)/Register.aspx頁面註冊admin帳號，並登入帳號  
9.在(網站伺服器IP)/AdminTools/addItems.aspx新增一些商品(飲料)