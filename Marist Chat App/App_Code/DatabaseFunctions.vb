Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class DatabaseFunctions
    Public Shared Function MakeSQLSafe(ByVal sql As String) As String
        If sql.Contains("'") Then
            sql = sql.Replace("'", "''")
        End If

        Return sql
    End Function

    Public Shared Function readUserPassword(ByVal email As String) As String 'function to read passwords from database.
        'Create a Connection object.
        Dim MyConn = New OleDb.OleDbConnection
        MyConn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=e:\hshome\marist2\mca.maristapps.com\App_Data\TestDB.accdb"

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select str_ecrPassword from tbl_users where str_email = '" & MakeSQLSafe(email) & "'"

        'Open the connection.

        Try
            MyConn.Open()
        Catch ex As Exception
            Return "FailConnOpen " & ex.Message
        End Try

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As OleDb.OleDbDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetString(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            MyConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            Return "Fail due to " & ex.Message & ex.StackTrace
        End Try

        Return result
    End Function

End Class
