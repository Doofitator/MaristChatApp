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

    Public Shared Function getClasses(ByVal eml As String) As String(,) 'returns 2D array. (0,0) = class name, (0,1) = is a member or not
        Dim strCommand As String = "select * from tbl_classes where int_userID = " & readUserInfo(eml, "int_ID")
        'command to select the record in the class table

        'create connection object
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

        Dim strValues As New Generic.List(Of String)    '|this will be the first dimension of our eventual array
        Dim strColumns As New Generic.List(Of String)   'this will be the second dimension of our eventual array
        'TODO: Does the above line work as a list(of Boolean)?

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                For i As Integer = 0 To reader.FieldCount - 1   'for each column
                    Try
                        strColumns.Add(reader.GetName(i))   'add the column name to our first dimension
                        strValues.Add(reader.GetBoolean(i)) 'add it's boolean value to our second
                    Catch
                        'first column throws an error as it is not a boolean but instead the user ID integer
                    End Try
                Next
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'Console.WriteLine("Fail due to " & ex.Message & ex.StackTrace)
        End Try

        Dim strOutput(strValues.Count, strValues.Count) As String   'New 2D string (output)
        Dim intIndex As Integer = 0                                 'new integer
        For Each itm In strColumns                                  'for each column
            intIndex = strColumns.IndexOf(itm)                      'get the index of the column
            If intIndex > 0 Then                                    'disregard the first column (user ID)
                strOutput(intIndex - 1, 0) = itm                    'add the column name to the first dimension
                strOutput(intIndex - 1, 1) = strValues(intIndex - 1) 'add the corresponding column value to the second dimension
            End If
        Next

        Return strOutput 'return output 2D array
    End Function
End Class
