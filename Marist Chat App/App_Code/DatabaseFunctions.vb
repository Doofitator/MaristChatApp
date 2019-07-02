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
        Dim intAffectedRows As Integer
        Try
            intAffectedRows = oleCmd.ExecuteNonQuery() 'execute query
            oleConn.Close() 'close connection
        Catch ex As Exception
            oleConn.Close() 'close connection
            Return "Command Failed: " & ex.Message 'command failed for some reason
        End Try

        Return "Success: " & intAffectedRows
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

    Public Shared Function getClasses(ByVal eml As String) As Generic.List(Of String)
        Dim strOutput As New Generic.List(Of String)
        Dim strCommand As String = "select * from tbl_classes where int_userID = """ & readUserInfo(eml, "int_ID") & """"

        Dim oleConn = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd = oleConn.CreateCommand
        oleCmd.CommandText = strCommand

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            ' "FailConnOpen " & ex.Message
        End Try

        Dim result As String = "False" 'this is what the function will return

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                strOutput.Add(reader.GetBoolean(1)) 'todo: even if this worked it would only output the first column
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            ' "Fail due to " & ex.Message & ex.StackTrace
        End Try

        Return strOutput
    End Function
End Class
