﻿Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class DatabaseFunctions
    'set connectionString (hardcoded ACCDB path with password)
    Public Shared strConn As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=e:\hshome\marist2\mca.maristapps.com\App_Data\mca_db.accdb;Jet OLEDB:Database Password=OD3Qo5oIBy"

    Public Shared Sub eDebug(ByVal strOutput As String) 'emergency debug
        Throw New Exception(strOutput)
    End Sub
    Public Shared Function MakeSQLSafe(ByVal sql As String) As String
        If sql.Contains("'") Then
            sql = sql.Replace("'", "''")
        End If

        Return sql
    End Function
    Public Shared Function readUserPassword(ByVal email As String) As String 'function to read passwords from database.
        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
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
    Public Shared Function readUserInfo(ByVal email As String, ByVal column As String) As Integer 'function to read user IDs and Roles from database.
        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = "select " & column & " from tbl_users where str_email = '" & MakeSQLSafe(email) & "'"

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            'Return "FailConnOpen " & ex.Message
        End Try

        Dim result As Integer = 0 'this is what the function will return

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetInt32(0) 'get first value of field (because there should only be one record returned as there shouldn't be username doubleups).
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'Return "Fail due to " & ex.Message & ex.StackTrace
        End Try

        Return result
    End Function
    Public Shared Function readUserName(ByVal id As Integer) As String 'function to read user name from database.
        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = "select str_email from tbl_users where int_ID = " & id

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
    Public Shared Function readUserNameFromECR(ByVal ecr As String) As String 'function to read user name from database.
        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = "select str_email from tbl_users where str_ecrPassword = '" & ecr & "'"

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
    Public Shared Function readStreamID(ByVal streamName As String, ByVal classID As String) As String 'function to read stream IDs from database.
        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = "select int_streamID from tbl_streams where str_streamName = '" & MakeSQLSafe(streamName) & "' and str_classID = '" & classID & "'"
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
                result = reader.GetInt32(0) 'get first value of field (because there should only be one record returned as there shouldn't be stream doubleups).
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            result = "Fail due to " & ex.Message & ex.StackTrace
        End Try

        Return result
    End Function
    Public Shared Function readNotification(ByVal ID As Integer) As String 'function to read notifications (HTML) from database.
        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = "select str_message from tbl_notifications where int_ID = " & ID

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
                result = reader.GetString(0) 'get first value of field (because there should only be one record returned as there shouldn't be ID doubleups).
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
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'create command object
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
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
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = "select count(*) from tbl_users where str_email = '" & MakeSQLSafe(eml) & "'"

        'Open the connection.
        Try
            oleConn.Open()
        Catch ex As Exception

        End Try

        If oleCmd.ExecuteScalar = 1 Then 'if there is one result returned, then the username already exists in the database.
            oleConn.Close()
            Return True
        Else
            oleConn.Close()
            Return False
        End If
    End Function
    Public Shared Function getClasses(ByVal eml As String) As String() 'returns string array of class names that the user is a member of
        Dim strCommand As String = "select * from tbl_classes where int_userID = " & readUserInfo(eml, "int_ID")
        'command to select the record in the class table

        'create connection object
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = strCommand

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            ' "FailConnOpen " & ex.Message
        End Try

        Dim strValues As New Generic.List(Of String)    '|this will be the first dimension of our eventual array
        Dim strColumns As New Generic.List(Of String)   'this will be the second dimension of our eventual array
        'todo: Does the above line work as a list(of Boolean)?

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

        Dim strTable(strValues.Count, strValues.Count) As String   'New 2D string (output)
        Dim intIndex As Integer = 0                                 'new integer
        For Each itm As String In strColumns                                  'for each column
            intIndex = strColumns.IndexOf(itm)                      'get the index of the column
            If intIndex > 0 Then                                    'disregard the first column (user ID)
                strTable(intIndex - 1, 0) = itm                     'add the column name to the first dimension
                strTable(intIndex - 1, 1) = strValues(intIndex - 1) 'add the corresponding column value to the second dimension
            End If
        Next

        Dim strOutput As New Generic.List(Of String)                'Create a new list that will be our output

        Dim cols As Integer = strTable.GetUpperBound(0)             'get the cols of the 2D string
        Dim rows As Integer = strTable.GetUpperBound(1)             'get the rows of the 2D string

        For x As Integer = 0 To cols                                'for each col
            For y As Integer = 0 To rows                            'for each row
                If strTable(x, y) = "" Then Continue For 'if there's blanks in the array (because sometimes there is), skip them
                If y = 1 Then                                       'if the row is the true/false one
                    If strTable(x, y) = False Then Continue For     'if it is false, we don't want it. Skip
                    strOutput.Add(strTable(x, 0))                   'otherwise, add it's corresponding class ID to the output list
                End If
            Next
        Next

        Return strOutput.ToArray                                    'return the output list (as an array)
    End Function
    Public Shared Function getStreams(ByVal ClassID As String, ByVal eml As String) As String() 'returns string array of class names that the user is a member of
        Dim strCommand As String = "SELECT bool_isClassWide, str_streamName FROM tbl_streams WHERE str_classID = '" & ClassID & "'"
        Dim strUserName As String = eml.ToLower.Replace("@marist.vic.edu.au", "") 'get first part of email (username)

        'create connection object
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = strCommand

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            ' "FailConnOpen " & ex.Message
        End Try

        Dim strStreamName As New Generic.List(Of String)   'this will be our output

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                For i As Integer = 0 To reader.FieldCount - 1   'for each column
                    If i = 1 Then 'if the column is the second one (stream name column)
                        If (reader.GetString(i).ToLower.Contains(strUserName.ToLower)) Then 'if my name is in the stream
                            strStreamName.Add(reader.GetString(i)) 'add to output list
                        End If
                    End If

                    If i = 0 Then 'if the column is the first one (classwide column)
                        If reader.GetBoolean(i) Then 'if the boolean is true
                            strStreamName.Add(reader.GetString(i + 1)) 'add it's corresponding string name to the list
                        End If
                    End If
                Next
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'Console.WriteLine("Fail due to " & ex.Message & ex.StackTrace)
        End Try

        Return strStreamName.ToArray()
    End Function
    Public Shared Function getMessages(ByVal streamID As Integer) As String(,) 'returns 2D array. (0,0) = message body, (0,1) = message sender ID
        Dim strCommand As String = "select str_message, int_fromID from tbl_messages where int_streamID = " & streamID
        'command to select the record in the class table

        'create connection object
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = strCommand

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            ' "FailConnOpen " & ex.Message
        End Try

        Dim strMessages As New Generic.List(Of String)   'this will be the first dimension of our eventual array
        Dim intSenders As New Generic.List(Of Integer)   'this will be the second dimension of our eventual array

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                For i As Integer = 0 To reader.FieldCount - 1   'for each column
                    Try
                        strMessages.Add(reader.GetString(i))   'add the column name to our first dimension
                    Catch
                        intSenders.Add(reader.GetInt32(i)) 'add it's boolean value to our second
                    End Try
                Next
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'Console.WriteLine("Fail due to " & ex.Message & ex.StackTrace)
        End Try

        'todo: I have just realised that the following line should probably be 'strOutput(strMessages.Count, 1)', however I don't want to waste time debugging that change as it works atm
        Dim strOutput(strMessages.Count, strMessages.Count) As String   'New 2D string (output)
        Dim intIndex As Integer = 0                                     'new integer
        For Each itm As String In strMessages                                      'for each message
            intIndex = strMessages.IndexOf(itm)                          'get the index of the sender item
            strOutput(intIndex, 0) = intSenders(intIndex)              'add the corresponding message value to the second dimension
            strOutput(intIndex, 1) = itm                                'add the sender ID to the first dimension
        Next

        Return strOutput 'return output 2D array
    End Function
    Public Shared Function getAlerts(ByVal eml As String) As String(,) 'returns 2D array of alerts that the user has been sent & their IDs
        Dim strCommand As String = "SELECT str_message, int_userGroup, int_ID FROM tbl_notifications"

        'create connection object
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = strCommand

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            ' "FailConnOpen " & ex.Message
        End Try

        Dim strNotificationContent As New Generic.List(Of String)   'this is the notification body content
        Dim intIDs As New Generic.List(Of Integer)                  'these are our notification IDs
        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                For i As Integer = 0 To reader.FieldCount - 1   'for each column
                    If i = 1 Then 'if the column is the second one (role column)
                        Dim intRole As Integer = readUserInfo(eml, "int_role")  'get user's role
                        Dim validAlerts As New Generic.List(Of Integer)         'start a new list
                        Select Case intRole
                            Case 0                                              'if the user is a parent
                                validAlerts.Add(4)                              '|
                                validAlerts.Add(0)                              '|
                                validAlerts.Add(5)                              '| add all the groups they are a part of to the list, as per intRoleArray (/ strRoleArray)
                                validAlerts.Add(6)                              '|
                                validAlerts.Add(7)                              '|
                            Case 1                                              'if the user is a student
                                validAlerts.Add(4)                              '|
                                validAlerts.Add(1)                              '|
                                validAlerts.Add(5)                              '| add all the groups they are a part of to the list, 
                                validAlerts.Add(7)                              '| as per intRoleArray (/ strRoleArray)
                                validAlerts.Add(8)                              '|
                                validAlerts.Add(9)                              '|
                            Case 2                                              'if the user is an educator
                                validAlerts.Add(4)                              '|
                                validAlerts.Add(2)                              '|
                                validAlerts.Add(6)                              '| add all the groups they are a part of to the list, 
                                validAlerts.Add(7)                              '| as per intRoleArray (/ strRoleArray)
                                validAlerts.Add(8)                              '|
                                validAlerts.Add(9)                              '|
                            Case 3                                              'if the user is an admin
                                validAlerts.Add(4)                              '|
                                validAlerts.Add(3)                              '| add all the groups they are a part of to the list, as per intRoleArray (/ strRoleArray)
                                validAlerts.Add(9)                              '|
                        End Select
                        If (validAlerts.Contains(reader.GetInt32(i))) Then 'if my role is in the notification
                            strNotificationContent.Add(reader.GetString(i - 1)) 'add first column (body content) to list
                            intIDs.Add(reader.GetInt32(i + 1)) 'add third column (ID) to list
                        End If
                    End If
                Next
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'Console.WriteLine("Fail due to " & ex.Message & ex.StackTrace)
        End Try

        'todo: As in the previous function, I have realised that the following line should probably be 'strOutput(strNotifcationContent.Count, 1)', however I don't
        'want to waste time debugging that change as it works atm

        strNotificationContent.Reverse()
        intIDs.Reverse()

        Dim strOutput(strNotificationContent.Count, strNotificationContent.Count) As String   'New 2D string (output)
        Dim intIndex As Integer = 0                                     'new integer
        For Each itm As String In strNotificationContent                          'for each message
            intIndex = strNotificationContent.IndexOf(itm)              'get the index of the notification
            strOutput(intIndex, 0) = intIDs(intIndex).ToString()                   'add the corresponding notification ID to the second dimension
            strOutput(intIndex, 1) = itm                                'add the message content to the first dimension
        Next

        Return strOutput 'return output 2D array
    End Function
    Public Shared Function isAlertUrgent(ByVal alertID As Integer) As Boolean 'function to read alert urgency column
        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = "select bool_urgent from tbl_notifications where int_ID = " & alertID

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            Return "FailConnOpen " & ex.Message
        End Try

        Dim result As Boolean = False 'this is what the function will return. Assume false in case we get an error.

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetBoolean(0) 'get first value of field (because there should only be one record returned as there shouldn't be notification ID doubleups).
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'result = "Fail due to " & ex.Message & ex.StackTrace
        End Try

        Return result
    End Function
    Public Shared Function readSql(ByVal sql As String, Optional table As String = "tbl_messages") As DataSet
        Dim dsResults As New DataSet(table)

        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleAdp As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter(sql, oleConn)

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            'Return "FailConnOpen " & ex.Message
        End Try

        oleAdp.Fill(dsResults, table)

        Return dsResults
    End Function

    Public Shared Function isAuthenticated(ByVal eml As String) As Boolean 'function to read if user is verified
        'Create a Connection object.
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = "select bool_verified from tbl_users where str_email = '" & eml & "'"
        'eDebug(oleCmd.CommandText)
        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            Return "FailConnOpen " & ex.Message
        End Try

        Dim result As Boolean = False 'this is what the function will return. Assume false in case we get an error.

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                result = reader.GetBoolean(0) 'get first value of field (because there should only be one record returned as there shouldn't be user doubleups).
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'result = "Fail due to " & ex.Message & ex.StackTrace
        End Try

        Return result
    End Function

    Public Shared Function getUsers(ByVal ClassID As String) As String() 'returns string array of users in a specified class
        Dim strCommand As String = "select str_email from tbl_users where int_ID in (select int_userID from tbl_classes where " & ClassID & " = true)"

        'create connection object
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = strCommand

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            ' "FailConnOpen " & ex.Message
        End Try

        Dim strUsers As New Generic.List(Of String)   'this will be our output

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                For i = 0 To reader.FieldCount - 1
                    strUsers.Add(reader.GetString(i))
                Next
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'Console.WriteLine("Fail due to " & ex.Message & ex.StackTrace)
        End Try

        Return strUsers.ToArray()
    End Function

    Public Shared Function getAllUsers() As String() 'returns string array of users in a specified class
        Dim strCommand As String = "select str_email from tbl_users"

        'create connection object
        Dim oleConn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        oleConn.ConnectionString = strConn

        'Create a Command object.
        Dim oleCmd As OleDb.OleDbCommand = oleConn.CreateCommand
        oleCmd.CommandText = strCommand

        'Open the connection.

        Try
            oleConn.Open()
        Catch ex As Exception
            ' "FailConnOpen " & ex.Message
        End Try

        Dim strUsers As New Generic.List(Of String)   'this will be our output

        Try
            Dim reader As OleDb.OleDbDataReader = oleCmd.ExecuteReader() 'run sql script
            While reader.Read
                For i = 0 To reader.FieldCount - 1
                    strUsers.Add(reader.GetString(i))
                Next
            End While
            oleConn.Close() 'close connection
        Catch ex As Exception 'if a catastrophic error occurs
            'console.writeline(ex.ToString)
            oleConn.Close() 'close the connection
            'Console.WriteLine("Fail due to " & ex.Message & ex.StackTrace)
        End Try

        Return strUsers.ToArray()
    End Function

    'define role names and database codes for use when sending & recieving alerts.
    Public Shared strRoleArray As String() = {"All", "Parents", "Students", "Educators", "Admins", "Parents & Students", "Parents & Educators", "Parents, Students & Educators", "Students & Educators", "Students, Educators & Admins"}
    Public Shared intRoleArray As Integer() = {4, 0, 1, 2, 3, 5, 6, 7, 8, 9}
End Class
