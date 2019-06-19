Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class DatabaseFunctions
    Public Function MakeSQLSafe(ByVal sql As String) As String
        If sql.Contains("'") Then
            sql = sql.Replace("'", "''")
        End If

        Return sql
    End Function

    Public Shared Function readUserPassword(ByVal email As String) As String 'function to read passwords from database.
        'Create a Connection object.
        Dim MyConn = New SqlConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=e:\hshome\marist2\mca.maristapps.com\App_Data\TestDB.accdb")

        'Create a Command object.
        Dim myCmd = MyConn.CreateCommand
        myCmd.CommandText = "select str_ecrPassword from tbl_users where convert(varchar, str_email) = '" & MakeSQLSafe(email) & "'"

        'Open the connection.

        Try
            MyConn.Open()
        Catch
            Return "Fail"
        End Try

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As SqlDataReader = myCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetString(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            MyConn.Close() 'close connection
            Dim decryptor As New Encryption(email) 'new instance of encryption module
            result = decryptor.DecryptData(result) 'decript with username key
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            MyConn.Close() 'close the connection
            Return "Fail"
        End Try

        Return result
    End Function

End Class
