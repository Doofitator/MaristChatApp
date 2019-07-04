
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
        'ensure user is authenticated
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("/Default.aspx")
        End If

        Dim intRole As Integer = CInt(DatabaseFunctions.readUserInfo(User.Identity.Name, "int_role"))
        LoadSidebar(intRole)
    End Sub

    Sub addSidebarBtn(ByVal controlID As String, ByVal controlContent As String)
        Dim btn As New Button                               'New button
        btn.ID = controlID                                  'set button ID
        btn.Text = controlContent                           'set button text
        AddHandler btn.Click, AddressOf Me.btn_Click        'add button onclick event
        Me.Master.FindControl("sidebar").Controls.Add(btn)  'add button to sidebar
    End Sub
    Sub addSidebarLbl(ByVal controlID As String, ByVal controlContent As String)
        Dim lbl As New Literal                              'New Label
        lbl.ID = controlID                                  'set Label ID
        lbl.Text = "<li>" & controlContent & "</li>"        'set Label text
        Me.Master.FindControl("sidebar").Controls.Add(lbl)  'add Label to sidebar
    End Sub
    Sub addSidebarClientBtn(ByVal controlOnclick As String, ByVal controlText As String) 'basically addSidebarBtn except it runs javascript instead of VB code
        Dim lit As New Literal
        lit.Text = "<input type=""button"" onclick=""" & controlOnclick & """ value=""" & controlText & """ />"
        Me.Master.FindControl("sidebar").Controls.Add(lit)
    End Sub
    Sub addSidebarDivider()
        Dim lit As New Literal                              'new HTML control
        lit.Text = "<hr />"                                 'set it as a <hr> tag
        Me.Master.FindControl("sidebar").Controls.Add(lit)  'add to sidebar
    End Sub

    Sub debug(ByVal strOutput As String)
        'write to javascript console for immediate output
        Dim strEscaped As String = strOutput.Replace("'", "\'")
        Response.Write("<script>console.log('" & strEscaped & "')</script>")
    End Sub

    'define role names and database codes for use when sending & recieving alerts.
    Dim strRoleArray As String() = {"All", "Parents", "Students", "Educators", "Admins", "Parents & Students", "Parents & Educators", "Parents, Students & Educators", "Students & Educators", "Students, Educators & Admins"}
    Dim intRoleArray As Integer() = {4, 0, 1, 2, 3, 5, 6, 7, 8, 9}

    Sub addNewAlertsDiv()
        Dim divNewAlert As New HtmlGenericControl("div")                'New div
        divNewAlert.ID = "divNewAlert"                                  'Set ID
        divNewAlert.Attributes.Add("class", "wizard")                   'Set CSS class

        Me.Master.FindControl("BodyContent").Controls.Add(divNewAlert)  'Add the div to the page

        Dim divNewAlertTitleBar = New HtmlGenericControl("div")         'New 'titlebar' div
        divNewAlertTitleBar.ID = "divNewAlertTitleBar"                  'Set ID
        divNewAlertTitleBar.Attributes.Add("class", "titleBar")         'Set CSS class
        '                                                                Add innerHTML incl. 'close' button
        divNewAlertTitleBar.InnerHtml = "<h3>New Alert Wizard<input style=""float: right;"" type=""button"" onclick=""HideShow('BodyContent_divNewAlert')"" value=""X"" /></h3>"
        divNewAlert.Controls.Add(divNewAlertTitleBar)                   ' Add titlebar to div

        Dim lblMessage As New Label                                     '|
        lblMessage.Text = "Message:"                                    '| New label, add to div
        divNewAlert.Controls.Add(lblMessage)                            '|

        Dim txtMessage As New TextBox                                   '|
        txtMessage.TextMode = TextBoxMode.MultiLine                     '| New textbox,
        txtMessage.ID = "txtMessage"                                    '| Add to div.
        divNewAlert.Controls.Add(txtMessage)                            '|

        'Add newline to div
        Dim newLine1 As New LiteralControl("<br>") : divNewAlert.Controls.Add(newLine1)

        Dim cbxUrgent As New CheckBox                                   '|
        cbxUrgent.ID = "cbxUrgent"                                      '|
        cbxUrgent.Text = "Urgent?"                                      '| New checkbox, add to div
        cbxUrgent.TextAlign = TextAlign.Left                            '|
        divNewAlert.Controls.Add(cbxUrgent)                             '|

        'add newline to div
        Dim newLine2 As New LiteralControl("<br>") : divNewAlert.Controls.Add(newLine2)

        Dim lblRoles As New Label                                       '|
        lblRoles.Text = "User groups:"                                  '| New label, add to div
        divNewAlert.Controls.Add(lblRoles)                              '|

        Dim ddlRoles As New DropDownList                                '|
        ddlRoles.ID = "ddlRoles"                                        '| New combobox
        ddlRoles.DataSource = strRoleArray                              '|
        ddlRoles.DataBind()                                             '| Add options & add to div.
        divNewAlert.Controls.Add(ddlRoles)                              '|

        'add newlines to div
        Dim newLine3 As New LiteralControl("<br>") : divNewAlert.Controls.Add(newLine3)
        Dim newLine4 As New LiteralControl("<br>") : divNewAlert.Controls.Add(newLine4)

        Dim btnWriteAlert As New Button                                 '|
        btnWriteAlert.Text = "Write Alert"                              '|
        btnWriteAlert.ID = "btnWriteAlert"                              '| New button, link to btn_Click & Add to div.
        AddHandler btnWriteAlert.Click, AddressOf Me.btn_Click          '|
        divNewAlert.Controls.Add(btnWriteAlert)                         '|
    End Sub
    Sub addNewClassDiv()
        Dim divNewClass As New HtmlGenericControl("div")                'New div
        divNewClass.ID = "divNewClass"                                  'Set ID
        divNewClass.Attributes.Add("class", "wizard")                   'Set CSS class

        Me.Master.FindControl("BodyContent").Controls.Add(divNewClass)  'Add the div to the page

        Dim divNewClassTitleBar = New HtmlGenericControl("div")         'New 'titlebar' div
        divNewClassTitleBar.ID = "divNewClassTitleBar"                  'Set ID
        divNewClassTitleBar.Attributes.Add("class", "titleBar")         'Set CSS class
        '                                                                Add innerHTML incl. 'close' button
        divNewClassTitleBar.InnerHtml = "<h3>New Class Wizard<input style=""float: right;"" type=""button"" onclick=""HideShow('BodyContent_divNewClass')"" value=""X"" /></h3>"
        divNewClass.Controls.Add(divNewClassTitleBar)                   'Add titlebar to div

        Dim lblClassID As New Label                                     '|
        lblClassID.Text = "Class Identifier:"                           '| New label, add to div
        divNewClass.Controls.Add(lblClassID)                            '|

        Dim txtClassID As New TextBox                                   '|
        txtClassID.TextMode = TextBoxMode.SingleLine                    '| New textbox,
        txtClassID.ID = "txtClassID"                                    '| Add to div.
        divNewClass.Controls.Add(txtClassID)                            '|

        'add newline to div
        Dim newLine1 As New LiteralControl("<br>") : divNewClass.Controls.Add(newLine1)

        Dim lblUserList As New Label                                    '|
        lblUserList.Text = "CSV user list:"                             '| New label, add to div
        divNewClass.Controls.Add(lblUserList)                           '|

        Dim txtUserList As New TextBox                                  '|
        txtUserList.TextMode = TextBoxMode.MultiLine                    '| New textbox,
        txtUserList.ID = "txtUserList"                                  '| Add to div
        divNewClass.Controls.Add(txtUserList)                           '|

        'add newlines to div
        Dim newLine3 As New LiteralControl("<br>") : divNewClass.Controls.Add(newLine3)
        Dim newLine4 As New LiteralControl("<br>") : divNewClass.Controls.Add(newLine4)

        Dim btnWriteClass As New Button                                 '|
        btnWriteClass.Text = "Write Class"                              '|
        btnWriteClass.ID = "btnWriteClass"                              '| New button, link to btn_Click & Add to div.
        AddHandler btnWriteClass.Click, AddressOf Me.btn_Click          '|
        divNewClass.Controls.Add(btnWriteClass)                         '|
    End Sub
    Sub addNewStreamDiv()
        Dim divNewStream As New HtmlGenericControl("div")                'New div
        divNewStream.ID = "divNewStream"                                 'Set ID
        divNewStream.Attributes.Add("Stream", "wizard")                  'Set CSS Class

        Me.Master.FindControl("BodyContent").Controls.Add(divNewStream)  'Add the div to the page

        Dim divNewStreamTitleBar = New HtmlGenericControl("div")         'New 'titlebar' div
        divNewStreamTitleBar.ID = "divNewStreamTitleBar"                 'Set ID
        divNewStreamTitleBar.Attributes.Add("Stream", "titleBar")        'Set CSS Class
        '                                                                'Add innerHTML incl. 'close' button
        divNewStreamTitleBar.InnerHtml = "<h3>New Stream Wizard<input style=""float: right;"" type=""button"" onclick=""HideShow('BodyContent_divNewStream')"" value=""X"" /></h3>"
        divNewStream.Controls.Add(divNewStreamTitleBar)                  'Add titlebar to div

        Dim lblStreamID As New Label                                     '|
        lblStreamID.Text = "Stream Name:"                                '| New label, add to div
        divNewStream.Controls.Add(lblStreamID)                           '|

        Dim txtStreamID As New TextBox                                   '|
        txtStreamID.TextMode = TextBoxMode.SingleLine                    '| New textbox,
        txtStreamID.ID = "txtStreamID"                                   '| Add to div.
        divNewStream.Controls.Add(txtStreamID)                           '|

        'add newline to div
        Dim newLine1 As New LiteralControl("<br>") : divNewStream.Controls.Add(newLine1)

        Dim lblUserList As New Label                                    '|
        lblUserList.Text = "CSV user list:"                             '| New label, add to div
        divNewStream.Controls.Add(lblUserList)                          '|

        Dim txtUserList As New TextBox                                  '|
        txtUserList.TextMode = TextBoxMode.MultiLine                    '| New textbox,
        txtUserList.ID = "txtUserList"                                  '| Add to div
        divNewStream.Controls.Add(txtUserList)                          '|

        'add newlines to div
        Dim newLine3 As New LiteralControl("<br>") : divNewStream.Controls.Add(newLine3)
        Dim newLine4 As New LiteralControl("<br>") : divNewStream.Controls.Add(newLine4)

        Dim btnWriteStream As New Button                                 '|
        btnWriteStream.Text = "Write Stream"                             '|
        btnWriteStream.ID = "btnWriteStream"                             '| New button, link to btn_Click & Add to div.
        AddHandler btnWriteStream.Click, AddressOf Me.btn_Click          '|
        divNewStream.Controls.Add(btnWriteStream)                        '|
    End Sub

    Sub LoadSidebar(ByVal intRole As Integer)
        Select Case intRole
            Case 0 'user is a parent
            'load just alerts

            Case 1 'user is a student
            'load alerts and class streams

            Case 2 'user is an educator
                'load alerts and class streams (including add class buttons)
                addSidebarLbl("lblAlerts", "Alerts")
                addSidebarDivider()
                addSidebarDivider()
                addSidebarClientBtn("HideShow('BodyContent_divNewClass'); hamburger(document.getElementsByClassName('container')[0])", "NEW CLASS")
            Case 3 'user is an administrator
                'load alerts (including new alert buttons), class streams (including add class buttons)
                'client buttons show divs, close mobile hamburger menus onclick
                addSidebarLbl("lblAlerts", "Alerts")
                addSidebarClientBtn("HideShow('BodyContent_divNewAlert'); hamburger(document.getElementsByClassName('container')[0])", "NEW ALERT")
                addSidebarDivider()
                addSidebarDivider()
                addSidebarClientBtn("HideShow('BodyContent_divNewClass'); hamburger(document.getElementsByClassName('container')[0])", "NEW CLASS")
        End Select

        If intRole > 0 Then
            'TODO: load classes & streams that i'm a member of
            'query the database for the names of classes that I'm part of
            For Each item In DatabaseFunctions.getClasses(User.Identity.Name)
                debug(item)
            Next
        End If

        LoadContent(intRole)
    End Sub

    Sub LoadContent(ByVal intRole As Integer)
        Select Case intRole
            Case 2 'user is an educator
                'load new class div
                addNewClassDiv()
            Case 3 'user is an administrator
                'load new alert and new class divs
                addNewAlertsDiv()
                addNewClassDiv()

            Case Else
                'do nothing
        End Select
        Response.Write("<script src=""/Scripts/scripts.js""></script>")
    End Sub

    Sub btn_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)  'get button that called the event
        '
        'if button is a new class button
        If btn.ID = "btnWriteClass" Then
            ' make a New field in tbl_classes called classIdentifier

            Dim txtClassID As TextBox = CType(findDynamicBodyControl("divNewClass,txtClassID"), TextBox) 'find the class identifier textbox
            debug(DatabaseFunctions.runSQL("ALTER TABLE tbl_classes ADD COLUMN " & DatabaseFunctions.MakeSQLSafe(txtClassID.Text) & " YESNO NOT NULL")) 'debugging

            Dim txtUserList As TextBox = CType(findDynamicBodyControl("divNewClass,txtUserList"), TextBox) 'find the user list textbox
            Dim strUsers As String = txtUserList.Text.Replace(",", "@marist.vic.edu.au,")   'add email domains to each username
            Dim arrEmls As String() = strUsers.Replace(" ", "").Split(",")                  'remove spaces between commas if the user put them there & split the string up into an array
            Dim listUserIDs As New Generic.List(Of Integer)                                 'new list
            For Each strEmail In arrEmls                                                    'for each email in our array
                Try
                    listUserIDs.Add(DatabaseFunctions.readUserInfo(strEmail, "int_ID"))     'add the userID from tbl_users to our list
                Catch
                    debug("Invalid username: " & strEmail)                                  'invalid u/n error
                End Try
            Next
            listUserIDs.Add(DatabaseFunctions.readUserInfo(User.Identity.Name, "int_ID"))   'Add me to the class
            Dim strSql As String = "update tbl_classes set "                                'begin writing sql
            For Each intID In listUserIDs                                                   'for each id in the list
                '                                                                            attempt to update the row
                Dim strUpdateCmd As String = DatabaseFunctions.runSQL("update tbl_classes set " & DatabaseFunctions.MakeSQLSafe(txtClassID.Text) & " = 1 where int_UserID = " & intID)
                If strUpdateCmd.StartsWith("Success") Then                                  'if the command was successful
                    Dim intAffectedRows As Integer                                          '|
                    intAffectedRows = CInt(strUpdateCmd.Replace("Success: ", ""))           '|New integer, get amount of affected rows
                    If Not intAffectedRows < 1 Then                                         'if the amount of affected rows is at least one,
                        'its worked
                        GoTo QueryComplete                                                  'move on to the next user
                    End If
                End If

                'updating the row didn't work, so we will insert (as it obviously wasn't there).
                DatabaseFunctions.runSQL("insert into tbl_classes (int_userID, " & DatabaseFunctions.MakeSQLSafe(txtClassID.Text) & ") values (" & intID & ", YES)")

QueryComplete:
            Next
            'TODO: Document this
            addSidebarLbl("lbl" & txtClassID.Text, txtClassID.Text)
            addSidebarClientBtn("NewStream('BodyContent_divNewStream', this); hamburger(document.getElementsByClassName('container')[0])", "NEW STREAM IN " & txtClassID.Text)
            'TODO: make that client button work
            addSidebarDivider()
        End If

        'if button is a new alert button
        If btn.ID = "btnWriteAlert" Then
            'run script to update tbl_alerts with new message
            Dim strAccessBoolFixer As String = ""
            Dim cbxUrgent As CheckBox = CType(findDynamicBodyControl("divNewAlert,cbxUrgent"), CheckBox)
            Dim txtMessage As TextBox = CType(findDynamicBodyControl("divNewAlert,txtMessage"), TextBox)
            Dim ddlRoles As DropDownList = CType(findDynamicBodyControl("divNewAlert,ddlRoles"), DropDownList)
            If cbxUrgent.Checked Then strAccessBoolFixer = "YES" Else strAccessBoolFixer = "NO"
            Dim intUserCode As Integer
            intUserCode = intRoleArray(ddlRoles.SelectedIndex)
            debug(DatabaseFunctions.runSQL("INSERT INTO tbl_notifications ( str_message, int_userGroup, bool_urgent, dt_timeStamp ) VALUES (""" & DatabaseFunctions.MakeSQLSafe(txtMessage.Text) & """, " & intUserCode & ", " & strAccessBoolFixer & ", """ & DateTime.Now & """)"))
            'TODO: The above script writes user groups that are not bound as they are in the database. This is instead a job for the client end to decipher if they are part of the alert's user group.
        End If
        'if button is a new stream button

        'if button is a regular, existing stream button
    End Sub

    Function findDynamicBodyControl(ByVal path As String) As Control 'a real dirty way of doing something that should be a lot easier
        Dim strIDArray As Array = path.Split(",")   'split the inputted path

        'output the control defined in the path
        Return Me.Master.FindControl("form1").FindControl("BodyContent").FindControl(strIDArray(0)).FindControl(strIDArray(1))
    End Function
End Class
