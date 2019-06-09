
Partial Class testPages_Database
    Inherits System.Web.UI.Page
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
    End Sub

    Public DatabaseConnection = New OleDb.OleDbConnection

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            DatabaseConnection.close()
        Catch ex As Exception

        End Try
        DatabaseConnection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=e:\hshome\marist2\mca.maristapps.com\App_Data\TestDB1.accdb"
        DatabaseConnection.Open()

        'que bad spaghetti code that's virtually a copy/paste from my first ever database project:
        Dim query As String = "SELECT * FROM tblData"
        Dim MDBConnString_ As String = DatabaseConnection.connectionstring
        Dim DataSet As New DataSet
        Dim Command As New OleDb.OleDbCommand(query, DatabaseConnection)
        Dim DataAdapter As New OleDb.OleDbDataAdapter(Command)
        DataAdapter.Fill(DataSet, "tblData")
        Dim t1 As DataTable = DataSet.Tables("tblData")
        Dim row As DataRow
        Dim Item(1) As String

        For Each row In t1.Rows
            lblDatabase.Text = row(1)
        Next

        DatabaseConnection.close()
    End Sub
    Protected Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
        'DatabaseConnection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\asharkey268\source\repos\Marist Chat App\Marist Chat App\TestDB1.accdb"
        DatabaseConnection.Open()
        Dim Sqlstr As String = "UPDATE tblData SET Data = '" & escapeQuotes(txtAdd.Text) & "' where ID=1"
        Dim Command = New OleDb.OleDbCommand(Sqlstr, DatabaseConnection)
        Command.CommandText = Sqlstr
        Command.ExecuteNonQuery()
        DatabaseConnection.close()
        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
    End Sub

    Public Function escapeQuotes(ByVal text As String) As String
        Return text.Replace("'", "''")
    End Function
End Class
