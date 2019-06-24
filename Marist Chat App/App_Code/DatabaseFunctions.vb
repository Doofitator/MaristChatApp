Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class DatabaseFunctions
    Public Shared strConn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=e:\hshome\marist2\mca.maristapps.com\App_Data\mca_db.accdb"

    Public Shared Function MakeSQLSafe(ByVal sql As String) As String
        If sql.Contains("'") Then
            sql = sql.Replace("'", "''")
        End If

        Return sql
    End Function

    Public Shared Function readUserPassword(ByVal email As String) As String 'function to read passwords from database.
        'Create a Connection object.
        Dim oleConn = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd = oleConn.CreateCommand
        oleCmd.CommandText = "select str_ecrPassword from tbl_users where str_email = '" & MakeSQLSafe(email) & "'"

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            Return "FailConnOpen " & ex.Message
        End Try

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetString(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            Return "Fail due to " & ex.Message & ex.StackTrace
        End Try

        Return result
    End Function

    Public Shared Function readUserInfo(ByVal email As String, ByVal column As String) As String 'function to read passwords from database.
        'Create a Connection object.
        Dim oleConn = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd = oleConn.CreateCommand
        oleCmd.CommandText = "select " & column & " from tbl_users where str_email = '" & MakeSQLSafe(email) & "'"

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            Return "FailConnOpen " & ex.Message
        End Try

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetInt32(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            Return "Fail due to " & ex.Message & ex.StackTrace
        End Try

        Return result
    End Function

    Public Shared Function runSQL(ByVal sql As String) As String 'function to write to the database
        'create connection object
        Dim oleConn = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'create command object
        Dim oleCmd = oleConn.CreateCommand
        oleCmd.CommandText = sql 'set command object's sql command as the inputted sql string

        'open the connection

        Try
            oleConn.Open()
        Catch ex As Exception
            Return "Connection Failed: " & ex.Message 'connection didn't open for some reason
        End Try

        'run the command

        Try
            oleCmd.ExecuteNonQuery() 'execute query
            oleConn.Close() 'close connection
        Catch ex As Exception
            oleConn.Close() 'close connection
            Return "Command Failed: " & ex.Message 'command failed for some reason
        End Try

        Return "Success"
    End Function

    Public Shared Function NewColumn(ByVal table As String, ByVal columnName As String, ByVal fieldType As String, ByVal defaultValue As String, ByVal NullOption As String) As String
        'TODO: this is kinda dodgey because it has the posibility of a command running between the other two. Can the default value be put in the first command?

        Dim strCmd1 As String = runSQL("ALTER TABLE " & table & " ADD COLUMN " & columnName & " " & fieldType & " " & NullOption)
        Dim strCmd2 As String = runSQL("ALTER TABLE " & table & " ALTER COLUMN " & columnName & " SET DEFAULT " & defaultValue)
        Dim strOutput

        If (strCmd1 = "Success") And (strCmd2 = "Success") Then
            strOutput = "Success"
        ElseIf strCmd1 = "Success" Then
            strOutput = strCmd2
        ElseIf strCmd2 = "Success" Then
            strOutput = strCmd1
        Else
            strOutput = strCmd1
        End If

        Return strOutput
    End Function

    Public Shared Function userExists(ByVal eml As String) As Boolean
        'Create a Connection object.
        Dim oleConn = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd = oleConn.CreateCommand
        oleCmd.CommandText = "select count(*) from tbl_users where str_email = '" & MakeSQLSafe(eml) & "'"

        'Open the connection.
        Try
            oleConn.Open()
        Catch ex As Exception
            Return "FailConnOpen " & ex.Message
        End Try

        If oleCmd.ExecuteScalar = 1 Then 'if there is one result returned, then the username already exists in the database.
            oleConn.Close()
            Return True
        Else
            oleConn.Close()
            Return False
        End If
    End Function
End Class
