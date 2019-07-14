Imports System.Data
Imports DatabaseFunctions
Partial Class _Default
    Inherits System.Web.UI.Page
    'TODO: If someone reloads, their last request is re-processed. This is relatively harmless if their last request was opening a stream, but an issue if it was something like writing a new class.

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
        'ensure user is authenticated
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("/Default.aspx")
        End If

        Dim intRole As Integer = CInt(readUserInfo(User.Identity.Name, "int_role"))
        LoadSidebar(intRole)
    End Sub
    Sub addSidebarBtn(ByVal controlID As String, ByVal controlContent As String, Optional isUrgentAlert As Boolean = False)
        Dim btn As New Button                               'New button
        Dim pnl As New Panel                                'New panel
        If isUrgentAlert Then
            pnl.CssClass = "urgent"
            Me.Master.FindControl("sidebar").Controls.Add(pnl)
        End If
        btn.ID = controlID                                  'set button ID
        btn.Text = controlContent                           'set button text
        'TODO: The following line needs to be uncommented, however it can't because it breaks something in javascript.
        'btn.UseSubmitBehavior = False                       'disable button from submitting form on enter
        AddHandler btn.Click, AddressOf Me.btn_Click        'add button onclick event
        If isUrgentAlert Then pnl.Controls.Add(btn) Else Me.Master.FindControl("sidebar").Controls.Add(btn)  'add button to sidebar / form accordingly
    End Sub
    Sub addSidebarLbl(ByVal controlID As String, ByVal controlContent As String)
        Dim lbl As New Literal                              'New Label
        lbl.ID = controlID                                  'set Label ID
        lbl.Text = "<li onclick=""collapse(this)"">" & controlContent & "</li>"        'set Label text
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
    'Sub debug(ByVal strOutput As String)
    'write to javascript console for immediate output
    ' Try
    'Dim strEscaped As String = strOutput.Replace("'", "\'")                 'Javascript escape quotes
    '       Response.Write("<script>console.log('" & strEscaped & "')</script>")    'write javascript to DOM
    'Catch ex As Exception
    '       debug("Failed to write to console: " & ex.Message)                      'Write fail to console
    'End Try
    'End Sub
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
        divNewAlert.Controls.Add(New LiteralControl("<br>"))

        Dim cbxUrgent As New CheckBox                                   '|
        cbxUrgent.ID = "cbxUrgent"                                      '|
        cbxUrgent.Text = "Urgent?"                                      '| New checkbox, add to div
        cbxUrgent.TextAlign = TextAlign.Left                            '|
        divNewAlert.Controls.Add(cbxUrgent)                             '|

        'add newline to div
        divNewAlert.Controls.Add(New LiteralControl("<br>"))

        Dim lblRoles As New Label                                       '|
        lblRoles.Text = "User groups:"                                  '| New label, add to div
        divNewAlert.Controls.Add(lblRoles)                              '|

        Dim ddlRoles As New DropDownList                                '|
        ddlRoles.ID = "ddlRoles"                                        '| New combobox
        ddlRoles.DataSource = strRoleArray                              '|
        ddlRoles.DataBind()                                             '| Add options & add to div.
        divNewAlert.Controls.Add(ddlRoles)                              '|

        'add newlines to div
        divNewAlert.Controls.Add(New LiteralControl("<br>"))
        divNewAlert.Controls.Add(New LiteralControl("<br>"))

        Dim btnWriteAlert As New Button                                 '|
        btnWriteAlert.Text = "Write Alert"                              '|
        btnWriteAlert.ID = "btnWriteAlert"                              '| New button, link to btn_Click & Add to div.
        btnWriteAlert.UseSubmitBehavior = False                         '|
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
        divNewClass.Controls.Add(New LiteralControl("<br>"))

        Dim lblUserList As New Label                                    '|
        lblUserList.Text = "CSV user list:"                             '| New label, add to div
        divNewClass.Controls.Add(lblUserList)                           '|

        Dim txtUserList As New TextBox                                  '|
        txtUserList.TextMode = TextBoxMode.MultiLine                    '| New textbox,
        txtUserList.ID = "txtUserList"                                  '| Add to div
        divNewClass.Controls.Add(txtUserList)                           '|

        'add newline to div
        divNewClass.Controls.Add(New LiteralControl("<br>"))

        Dim lblStreamName As New Label                                  '|
        lblUserList.Text = "Classwide Stream Name:"                     '| New label, add to div
        divNewClass.Controls.Add(lblUserList)                           '|

        Dim txtClassStreamName As New TextBox                           '|
        txtClassStreamName.ID = "txtClassStreamName"                    '| Add to div
        divNewClass.Controls.Add(txtClassStreamName)                    '|

        'add newlines to div
        divNewClass.Controls.Add(New LiteralControl("<br>"))
        divNewClass.Controls.Add(New LiteralControl("<br>"))

        Dim btnWriteClass As New Button                                 '|
        btnWriteClass.Text = "Write Class"                              '|
        btnWriteClass.ID = "btnWriteClass"                              '| New button, link to btn_Click & Add to div.
        btnWriteClass.UseSubmitBehavior = False                         '|
        AddHandler btnWriteClass.Click, AddressOf Me.btn_Click          '|
        divNewClass.Controls.Add(btnWriteClass)                         '|
    End Sub
    Sub addNewStreamDiv()
        Dim divNewStream As New HtmlGenericControl("div")                'New div
        divNewStream.ID = "divNewStream"                                 'Set ID
        divNewStream.Attributes.Add("class", "wizard")                   'Set CSS Class

        Me.Master.FindControl("BodyContent").Controls.Add(divNewStream)  'Add the div to the page

        Dim divNewStreamTitleBar = New HtmlGenericControl("div")         'New 'titlebar' div
        divNewStreamTitleBar.ID = "divNewStreamTitleBar"                 'Set ID
        divNewStreamTitleBar.Attributes.Add("class", "titleBar")         'Set CSS Class
        '                                                                'Add innerHTML incl. 'close' button
        divNewStreamTitleBar.InnerHtml = "<h3>New Stream Wizard<input style=""float: right;"" type=""button"" onclick=""HideShow('BodyContent_divNewStream')"" value=""X"" /></h3>"
        divNewStream.Controls.Add(divNewStreamTitleBar)                  'Add titlebar to div

        Dim lblClassID As New Label                                      '|
        lblClassID.Text = "Class Name:"                                  '| New label, add to div
        divNewStream.Controls.Add(lblClassID)                            '|

        Dim txtStreamID As New TextBox                                   '|
        txtStreamID.TextMode = TextBoxMode.SingleLine                    '| New textbox,
        txtStreamID.ID = "txtStreamID"                                   '| Add to div.
        divNewStream.Controls.Add(txtStreamID)                           '|

        'add newline to div
        divNewStream.Controls.Add(New LiteralControl("<br>"))

        Dim lblUserList As New Label                                     '|
        lblUserList.Text = "CSV user list:"                              '| New label, add to div
        divNewStream.Controls.Add(lblUserList)                           '|

        Dim txtStreamUserList As New TextBox                             '|
        txtStreamUserList.TextMode = TextBoxMode.MultiLine               '| New textbox,
        txtStreamUserList.ID = "txtStreamUserList"                       '| Add to div
        divNewStream.Controls.Add(txtStreamUserList)                     '|

        'add newlines to div
        divNewStream.Controls.Add(New LiteralControl("<br>"))
        divNewStream.Controls.Add(New LiteralControl("<br>"))

        Dim btnWriteStream As New Button                                 '|
        btnWriteStream.Text = "Write Stream"                             '|
        btnWriteStream.ID = "btnWriteStream"                             '| New button, link to btn_Click & Add to div.
        btnWriteStream.UseSubmitBehavior = False                         '|
        AddHandler btnWriteStream.Click, AddressOf Me.btn_Click          '|
        divNewStream.Controls.Add(btnWriteStream)                        '|
    End Sub
    Sub addReaderDiv()
        Dim divReader As New HtmlGenericControl("div")                  'New div
        divReader.ID = "divReader"                                      'Set ID
        divReader.Attributes.Add("class", "wizard")                     'Set CSS Class

        Me.Master.FindControl("BodyContent").Controls.Add(divReader)    'Add the div to the page

        Dim divReaderTitleBar = New HtmlGenericControl("div")           'New 'titlebar' div
        divReaderTitleBar.ID = "divReaderTitleBar"                      'Set ID
        divReaderTitleBar.Attributes.Add("class", "titleBar")           'Set CSS Class
        '                                                               'Add innerHTML incl. 'close' button
        divReaderTitleBar.InnerHtml = "<h3>Administrator Data Reader<input style=""float: right;"" type=""button"" onclick=""HideShow('BodyContent_divReader')"" value=""X"" /></h3>"
        divReader.Controls.Add(divReaderTitleBar)                       'Add titlebar to div

        Dim lblSearch As New Label                                      '|
        lblSearch.Text = "Search for messages by:"                      '| New label, add to div
        divReader.Controls.Add(lblSearch)                               '|

        divReader.Controls.Add(New LiteralControl("<br>"))              'Add linebreak to div

        Dim rbtnList As New RadioButtonList                             '|
        rbtnList.ID = "rbtnList"                                        '| New radiobuttonlist, add to div
        divReader.Controls.Add(rbtnList)                                '|

        Dim rbtnUser As New ListItem                                    '|
        rbtnUser.Text = "User (eg. 'asharkey268')"                      '|
        '                                                               '|
        Dim rbtnClass As New ListItem                                   '|
        rbtnClass.Text = "Class (eg. 'IT0331A')"                        '| New listItems,
        '                                                               '| Set text values,
        Dim rbtnYearLevel As New ListItem                               '| Add to radioButtonList
        rbtnYearLevel.Text = "Year Level (eg. '11')"                    '|
        '                                                               '|
        rbtnList.Items.Add(rbtnUser)                                    '|
        rbtnList.Items.Add(rbtnClass)                                   '|
        rbtnList.Items.Add(rbtnYearLevel)                               '|

        divReader.Controls.Add(New LiteralControl("<br>"))              'Add linebreak to div

        Dim lblTerm As New Label                                        '|
        lblTerm.Text = "Search Query: "                                 '| New label, add to div
        divReader.Controls.Add(lblTerm)                                 '|

        Dim txtTerm As New TextBox                                      '|
        txtTerm.ID = "txtTerm"                                          '| New textbox, add to div
        divReader.Controls.Add(txtTerm)                                 '|

        divReader.Controls.Add(New LiteralControl("<br>"))              'Add linebreak to div
        divReader.Controls.Add(New LiteralControl("<br>"))              'Add linebreak to div
        divReader.Controls.Add(New LiteralControl("<hr>"))              'Add horizontal line to div
        divReader.Controls.Add(New LiteralControl("<br>"))              'Add linebreak to div

        Dim lblAdvanced As New Label                                    '|
        lblAdvanced.Text = "Or type an Access SQL query: "              '| New label, add to div
        divReader.Controls.Add(lblAdvanced)                             '|

        Dim txtAdvanced As New TextBox                                  '|
        txtAdvanced.ID = "txtAdvanced"                                  '| New textbox, add to div
        divReader.Controls.Add(txtAdvanced)                             '|

        divReader.Controls.Add(New LiteralControl("<br>"))              'Add linebreak to div
        divReader.Controls.Add(New LiteralControl("<br>"))              'Add linebreak to div

        Dim btnQueryDB As New Button                                    '|
        btnQueryDB.Text = "Query Database"                              '|
        btnQueryDB.ID = "btnQueryDB"                                    '| New button, link to btn_Click & Add to div.
        btnQueryDB.UseSubmitBehavior = False                            '|
        AddHandler btnQueryDB.Click, AddressOf Me.btn_Click             '|
        divReader.Controls.Add(btnQueryDB)                              '|
    End Sub
    Sub addDataTableDiv()
        Dim divDataTable As New HtmlGenericControl("div")                'New div
        divDataTable.ID = "divDataTable"                                 'Set ID
        divDataTable.Attributes.Add("class", "wizard")                   'Set CSS Class

        Me.Master.FindControl("BodyContent").Controls.Add(divDataTable)  'Add the div to the page

        Dim divDataTableTitleBar = New HtmlGenericControl("div")         'New 'titlebar' div
        divDataTableTitleBar.ID = "divDataTableTitleBar"                 'Set ID
        divDataTableTitleBar.Attributes.Add("class", "titleBar")         'Set CSS Class
        '                                                                'Add innerHTML incl. 'close' button
        divDataTableTitleBar.InnerHtml = "<h3>Results DataTable<input style=""float: right;"" type=""button"" onclick=""HideShow('BodyContent_divDataTable')"" value=""X"" /></h3>"
        divDataTable.Controls.Add(divDataTableTitleBar)                  'Add titlebar to div

        Dim btnExport As New Button                                     '|
        btnExport.Text = "Export to Excel"                              '|
        btnExport.ID = "btnExport"                                      '| New button, add to div
        AddHandler btnExport.Click, AddressOf btn_Click                 '|
        divDataTable.Controls.Add(btnExport)                            '|

        Dim gvResults As New GridView                                   '|
        gvResults.ID = "gvResults"                                      '| new gridview, add to div
        divDataTable.Controls.Add(gvResults)                            '|
    End Sub
    Sub LoadSidebar(ByVal intRole As Integer)

        LoadAlerts()

        If intRole > 0 Then 'load class streams if you're not a parent / friend
            'query the database for the names of classes that I'm part of
            Dim strClassesArr() = getClasses(User.Identity.Name) 'get array of classes
            For Each item In strClassesArr
                'get array of streams
                Dim strStreamsArr() = getStreams(item, User.Identity.Name)
                'add class header to sidebar
                addSidebarLbl("lbl" & item, item)
                'add streams to sidebar
                For Each stream In strStreamsArr
                    addSidebarBtn("btn" & item & stream, stream)
                Next
                addSidebarClientBtn("NewStream('BodyContent_divNewStream', this); hamburger(document.getElementsByClassName('container')[0])", "NEW STREAM IN " & item) 'add new stream button
                'next class
            Next
        End If

        If intRole >= 2 Then 'if user is an educator or admin
            addSidebarDivider() 'add <hr>
            addSidebarClientBtn("HideShow('BodyContent_divNewClass'); hamburger(document.getElementsByClassName('container')[0])", "NEW CLASS") 'add new class button
        End If

        If intRole = 3 Then 'if user is an admin
            addSidebarClientBtn("HideShow('BodyContent_divNewAlert'); hamburger(document.getElementsByClassName('container')[0])", "NEW ALERT") 'add new alert button
            addSidebarClientBtn("HideShow('BodyContent_divReader'); hamburger(document.getElementsByClassName('container')[0])", "SQL READER") 'add reader button
        End If

        LoadContent(intRole) 'load other required DOM elements
    End Sub
    Sub LoadAlerts(Optional inSidebar As Boolean = True)
        Try
            clearPanelControls()            'Clear main controls
        Catch
        End Try

        If inSidebar Then
            addSidebarLbl("lblAlerts", "Alerts")    'Add heading in sidebar
        End If

        Dim strNotificationsArr(,) = getAlerts(User.Identity.Name)  'Get notifications for this user

        Dim intAlertButtonCount = 0 'New integer

        For Each notification In strNotificationsArr                                                             'for every message
            If (Not notification = Nothing) And (Not IsNumeric(notification)) Then                               'if the message isn't blank

                intAlertButtonCount += 1    'increase button count

                Dim cols As Integer = strNotificationsArr.GetUpperBound(0)                                       '|Get the array's rows and columns
                Dim rows As Integer = strNotificationsArr.GetUpperBound(1)                                       '|

                Dim toFind As String = notification     'set the data that we need to locate
                'Dim xIndex As Integer   '| Define eventual location points
                'Dim yIndex As Integer   '| for debugging purposes

                For x As Integer = 0 To cols - 1                                    'For each column
                    For y As Integer = 0 To rows - 1                                'For each row
                        If strNotificationsArr(x, y) = toFind Then                  'If the point we are at is the location we want
                            'xIndex = x                                              '| Set the vars to the location we are at now
                            'yIndex = y                                              '|
                            'debug("""" & toFind & """ [(" & x & "," & y & ")] has the ID " & strNotificationsArr(x, y - 1)) 'write point to javascript console
                            Dim strShortText As String                              'New variable to hold the first few words of the alert
                            Try
                                strShortText = Regex.Replace(toFind.Replace("''", "'"), "<.*?>", "") '|get the first few characters of the text, minus the HTML tags and double quotation marks that make the string SQL-safe
                                strShortText = strShortText.Substring(0, 25) & "…"                   '|
                            Catch                                                                                           'if that failes because the string isn't 30 characters long
                                strShortText = Regex.Replace(toFind.Replace("''", "'"), "<.*?>", "")                        'do it all again without stripping down to 30 characters
                            End Try
                            If (inSidebar) And (intAlertButtonCount < 4) Then addSidebarBtn("btnAlert" & strNotificationsArr(x, y - 1), strShortText, isAlertUrgent(strNotificationsArr(x, y - 1))) 'add button to sidebar for the alert including urgent class if applicable

                            Dim btn As New Button                                           '|
                            btn.ID = "btnAlert" & strNotificationsArr(x, y - 1)             '|
                            btn.Text = strShortText                                         '|
                            Dim pnl As New Panel                                            '|
                            If isAlertUrgent(strNotificationsArr(x, y - 1)) Then            '|
                                pnl.CssClass = "urgent"                                     '| Add buttons to main content as well.
                            End If                                                          '| This allows the 'Show older...' button to work
                            AddHandler btn.Click, AddressOf btn_Click                       '| And resolves the issue of the dynamic controls
                            If inSidebar Then pnl.Attributes.Add("style", "display: none")  '| not being there after postback
                            pnl.Controls.Add(btn)                                           '|
                            pnlUpdate.ContentTemplateContainer.Controls.Add(pnl)            '|

                            If (intAlertButtonCount = 3) And (inSidebar) Then
                                addSidebarBtn("btnMoreAlerts", "Show older...")             'Add button to show older alerts than the three in the sidebar
                            End If
                        End If
                    Next
                Next

            End If
        Next
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
                addReaderDiv()
                addDataTableDiv()
            Case Else
                'do nothing
        End Select

        If intRole > 0 Then
            Dim divMessageControls As New HtmlGenericControl("div")                 'New div
            divMessageControls.ID = "divMessageControls"                            'Set ID
            divMessageControls.Attributes.Add("class", "messageControls")           'Set class
            pnlUpdate.ContentTemplateContainer.Controls.Add(divMessageControls)     'Add to page

            Dim txtBody As New TextBox                          'New Textbox
            Dim btnSend As New Button                           'New button

            btnSend.ID = "btnSend"                              'Set button ID
            txtBody.ID = "txtBody"                              'Set textbox ID

            txtBody.Attributes.Add("placeholder", "Message...") 'Set textbox placeholder
            btnSend.Text = "Send"                               'Set button text
            btnSend.UseSubmitBehavior = False                   'Disable button from submitting form on enter

            AddHandler btnSend.Click, AddressOf Me.btn_Click    'Assign button click function
            txtBody.Attributes.Add("onkeypress", "button_click(event)") 'get it to run some javascript on click

            divMessageControls.Controls.Add(txtBody)            'Add textbox to page
            divMessageControls.Controls.Add(btnSend)            'Add button to page

            '--------------------------------------------------------------------------------------

            Dim divStreamHeading As New HtmlGenericControl("div")                 'New div
            divStreamHeading.ID = "divStreamHeading"                            'Set ID
            divStreamHeading.Attributes.Add("class", "streamHeading")           'Set class
            Me.Master.FindControl("topBar").Controls.Add(divStreamHeading)   'Add to page

            Dim lblStreamName As New Label                            'New Textbox

            lblStreamName.ID = "lblStreamName"                        'Set textbox ID

            divStreamHeading.Controls.Add(lblStreamName)            'Add textbox to page

            '--------------------------------------------------------------------------------------

            addNewStreamDiv()
        End If
    End Sub
    Public Function cleanHTML(ByVal badHTML As String) As String
        'https://stackoverflow.com/questions/307013/how-do-i-filter-all-html-tags-except-a-certain-whitelist
        'todo: this still doesn't block iframes
        Dim AcceptableTags As String = "i|b|u|sup|sub|ol|ul|li|br|h2|h3|h4|h5|span|div|p|a|img|blockquote"
        Dim WhiteListPattern As String = "</?(?(?=" & AcceptableTags & ")notag|[a-zA-Z0-9]+)(?:\s[a-zA-Z0-9\-]+=?(?:([""']?).*?\1?)?)*\s*/?>"
        'debug(Regex.Replace(badHTML, WhiteListPattern, "", RegexOptions.Compiled))
        Return Regex.Replace(badHTML, WhiteListPattern, "", RegexOptions.Compiled)
    End Function
    Sub btn_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)  'get button that called the event
        Dim lblStreamName As Label = findDynamicTopBarControl("divStreamHeading,lblStreamName") 'get heading label

        Try
            Dim strID As String = btn.ID    '| Try to get the button ID and 
            'debug("btn clicked: " & strID)  '| write it to the javascript console
        Catch ex As Exception
            'debug("Button click failed.")   ' If that fails (because the button was not properly created and given an ID) write that to javascript console too.
            Exit Sub
        End Try

        'if button is a new class button
        If btn.ID = "btnWriteClass" Then
            ' make a New field in tbl_classes called classIdentifier

            Dim txtClassStreamName As TextBox = CType(findDynamicBodyControl("divNewClass,txtClassStreamName"), TextBox) 'find the stream name textbox
            Dim txtClassID As TextBox = CType(findDynamicBodyControl("divNewClass,txtClassID"), TextBox) 'find the class identifier textbox
            runSQL("ALTER TABLE tbl_classes ADD COLUMN " & MakeSQLSafe(txtClassID.Text) & " YESNO NOT NULL")

            Dim txtUserList As TextBox = CType(findDynamicBodyControl("divNewClass,txtUserList"), TextBox) 'find the user list textbox
            Dim strUsers As String = txtUserList.Text.Replace(",", "@marist.vic.edu.au,")   'add email domains to each username
            Dim arrEmls As String() = strUsers.Replace(" ", "").Split(",")                  'remove spaces between commas if the user put them there & split the string up into an array
            Dim listUserIDs As New Generic.List(Of Integer)                                 'new list
            For Each strEmail In arrEmls                                                    'for each email in our array
                Try
                    listUserIDs.Add(readUserInfo(strEmail, "int_ID"))     'add the userID from tbl_users to our list
                Catch
                    'debug("Invalid username: " & strEmail)                                  'invalid u/n error
                End Try
            Next
            listUserIDs.Add(readUserInfo(User.Identity.Name, "int_ID"))   'Add me to the class
            For Each intID In listUserIDs                                                   'for each id in the list
                '                                                                            attempt to update the row
                Dim strUpdateCmd As String = runSQL("update tbl_classes set " & MakeSQLSafe(txtClassID.Text) & " = 1 where int_UserID = " & intID)
                If strUpdateCmd.StartsWith("Success") Then                                  'if the command was successful
                    Dim intAffectedRows As Integer                                          '|
                    intAffectedRows = CInt(strUpdateCmd.Replace("Success: ", ""))           '|New integer, get amount of affected rows
                    If Not intAffectedRows < 1 Then                                         'if the amount of affected rows is at least one,
                        'its worked
                        GoTo QueryComplete                                                  'move on to the next user
                    End If
                End If

                'updating the row didn't work, so we will insert (as it obviously wasn't there).
                runSQL("insert into tbl_classes (int_userID, " & MakeSQLSafe(txtClassID.Text) & ") values (" & intID & ", YES)")

QueryComplete:
            Next
            'todo: this stuff adds after the "new class" / "new alert" / "SQL reader" buttons. I want it to add before that
            addSidebarLbl("lbl" & txtClassID.Text, txtClassID.Text) 'add new label to sidebar
            'add classwide stream
            runSQL("insert into tbl_streams (str_ClassID, bool_isClassWide, str_StreamName) VALUES ('" & MakeSQLSafe(txtClassID.Text) & "', TRUE, '" & txtClassStreamName.Text & "')")
            addSidebarBtn("btn" & txtClassID.Text & txtClassStreamName.Text, txtClassStreamName.Text)
            addSidebarClientBtn("NewStream('BodyContent_divNewStream', this); hamburger(document.getElementsByClassName('container')[0])", "NEW STREAM IN " & txtClassID.Text) 'add new stream button
            addSidebarDivider()

        ElseIf btn.ID = "btnWriteAlert" Then 'if button is a new alert button
            'run script to update tbl_alerts with new message

            Dim strAccessBoolFixer As String = ""                                                               'New string for use later.
            Dim cbxUrgent As CheckBox = CType(findDynamicBodyControl("divNewAlert,cbxUrgent"), CheckBox)        'Get our checkbox
            Dim txtMessage As TextBox = CType(findDynamicBodyControl("divNewAlert,txtMessage"), TextBox)        'Get our message
            Dim ddlRoles As DropDownList = CType(findDynamicBodyControl("divNewAlert,ddlRoles"), DropDownList)  'get our dropdownlist
            If cbxUrgent.Checked Then strAccessBoolFixer = "YES" Else strAccessBoolFixer = "NO"                 'If the checkbox is TRUE, set our string to YES, else set it to NO
            Dim intUserCode As Integer                                                                          'New integer
            intUserCode = intRoleArray(ddlRoles.SelectedIndex)                                                  'Set the integer to the selected index of the dropdownlist
            'run sql
            runSQL("INSERT INTO tbl_notifications (str_message, int_userGroup, bool_urgent, dt_timeStamp) VALUES (""" & MakeSQLSafe(cleanHTML(txtMessage.Text)) & """, " & intUserCode & ", " & strAccessBoolFixer & ", """ & DateTime.Now & """)")

        ElseIf btn.ID = "btnWriteStream" Then 'if button is a new stream button

            Dim txtStreamID As TextBox = CType(findDynamicBodyControl("divNewStream,txtStreamID"), TextBox) 'find the class identifier textbox
            Dim txtStreamUserList As TextBox = CType(findDynamicBodyControl("divNewStream,txtStreamUserList"), TextBox) 'find csv textbox

            Dim strMembersArr() As String = txtStreamUserList.Text.Replace(" ", "").Split(",")  'Get list of members
            Dim strStreamName As String = ""                                                    'New string

            For Each strMember In strMembersArr                                                 'For each member
                strStreamName += strMember                                                      'Add their name to the stream name
                If Array.IndexOf(strMembersArr, strMember) = strMembersArr.Length - 2 Then      'If the member is the second last one in the list
                    strStreamName += " and "                                                    'Add the word 'and' after their name
                ElseIf Array.IndexOf(strMembersArr, strMember) = strMembersArr.Length - 1 Then  'If they are the last,
                    'add nothing after their name
                Else                                                                            'If they're before second last
                    strStreamName += ", "                                                       'Add a comma
                End If
                'If there's the marist domain in the stream name, remove it.
                If strStreamName.ToLower.Contains("@marist.vic.edu.au") Then strStreamName = strStreamName.ToLower.Replace("@marist.vic.edu.au", "")
            Next

            'run sql
            Dim strSql As String = "insert into tbl_streams (str_ClassID, bool_isClassWide, str_StreamName) VALUES ('" & MakeSQLSafe(txtStreamID.Text) & "', FALSE, '" & strStreamName & "')"
            runSQL(strSql)
            'todo: add button now in the right spot
        ElseIf btn.ID = "btnSend" Then 'if button is the send message button
            Dim txtBody As TextBox = findDynamicBodyControl("divMessageControls,txtBody")   'get the textbox
            Dim strMessage As String = Server.HtmlEncode(cleanHTML(txtBody.Text))           'Get the message
            'Write the message to the database
            runSQL("insert into tbl_messages (int_streamID, int_fromID, dt_timeStamp, str_message, bool_active, bool_read) VALUES (" & readStreamID(lblStreamName.Text.Split(">")(1).Substring(1), lblStreamName.Text.Split(">")(0).Replace(" ", "")) & ", " & readUserInfo(User.Identity.Name, "int_ID") & ", """ & DateTime.Now & """, """ & MakeSQLSafe(strMessage) & """, True, False)")
            txtBody.Text = ""   'clear the textbox
            tmrUpdate_Tick(tmrUpdate, EventArgs.Empty) 'update messages
            'TODO: On clicking the send button, there is a bit of a delay. Can this be removed?

        ElseIf btn.ID.StartsWith("btnAlert") Then

            Dim strAlertName As String = btn.Text     'get alert name
            'debug("alert: " & strAlertName & " pressed.") 'debugging
            lblStreamName.Text = strAlertName                                    'set heading label text
            clearPanelControls()                                                 'empty main body content

            Dim litNotificationHTML As New LiteralControl()                      '| New literal HTML control
            '                                                                    '| Set literal HTML to the message body (which is HTML formatted)
            litNotificationHTML.Text = Server.HtmlDecode(readNotification(btn.ID.Replace("btnAlert", "")).Replace("''", "'"))
            pnlUpdate.ContentTemplateContainer.Controls.Add(litNotificationHTML) '| Add literal control to message div

        ElseIf btn.ID = "btnMoreAlerts" Then

            lblStreamName.Text = "All Alerts"   'set heading label text
            LoadAlerts(False)                   'load alerts in main content window

        ElseIf btn.ID = "btnQueryDB" Then
            Dim rbtnList As RadioButtonList = CType(findDynamicBodyControl("divReader,rbtnList"), RadioButtonList)  '|
            Dim txtTerm As TextBox = CType(findDynamicBodyControl("divReader,txtTerm"), TextBox)                    '| Get controls
            Dim txtAdvanced As TextBox = CType(findDynamicBodyControl("divReader,txtAdvanced"), TextBox)            '|

            Dim dsResult As New DataSet 'new dataset for storing database output

            If txtAdvanced.Text = "" Then   'if the advanced section wasn't used
                Dim intToQuery As String = rbtnList.SelectedIndex   'get the selected radio button
                Dim strQueryData As String = MakeSQLSafe(txtTerm.Text)  'get the textbox search query
                Dim strSql As String    'new string for storing database input
                Select Case intToQuery
                    Case 0  'If the radiobutton selected was the 'user' one
                        strSql = "select * from tbl_messages where int_fromID = " & readUserInfo(strQueryData & "@marist.vic.edu.au", "int_ID")
                    Case 1  'If the radiobutton selected was the 'class' one
                        strSql = "select * from tbl_messages where int_streamID in (select int_streamID from tbl_streams where str_classID = '" & strQueryData & "')"
                    Case 2  'If the radiobutton selected was the 'yearlevel' one
                        Dim dtCurrentYear As Date = Date.Now    'get current date
                        Dim intGradYear As Integer = 12 - (CInt(strQueryData) - dtCurrentYear.Date.Year) 'calculate graduation year of that year level
                        strSql = "select * from tbl_messages where int_fromID in (select int_ID from tbl_users where dt_graduationYear = #" & New Date(intGradYear, "1", "1") & "#)"
                    Case Else 'if all else fails, just grab all the messages
                        strSql = "select str_message from tbl_messages"
                End Select
                dsResult = readSql(strSql)  'set output to sql results

            Else    'if the advanced section was used
                Dim strTable As String  'new string
                Dim strWordsArr() As String = txtAdvanced.Text.Split(" ") 'split the query into words
                For Each word In strWordsArr        'for each word
                    If word.StartsWith("tbl") Then  'if it starts with 'tbl'
                        strTable = word             'then that is the table we are querying
                        Exit For                    'disregard all the other words
                    End If
                Next
                dsResult = readSql(txtAdvanced.Text, strTable)  'query the requested table, set output to the results
            End If

            Dim gvResults As GridView = CType(findDynamicBodyControl("divDataTable,gvResults"), GridView)   'find control
            gvResults.DataSource = dsResult 'set datasource to the results
            gvResults.DataBind()            'bind datasource

            'show results
            Response.Write("<script>document.addEventListener(""DOMContentLoaded"", function(event) { HideShow('BodyContent_divDataTable'); });</script>")

        ElseIf btn.ID = "btnExport" Then

            Dim strFile As String = "attachment;filename=results.xls"                                           'delcare attachment type
            Response.ClearContent()                                                                             'clear page
            Response.AddHeader("content-disposition", strFile)                                                  'tell the browser that we are sending a file
            Response.ContentType = "application/ms-excel"                                                       'tell the browser it's an excel file
            Dim sw As New IO.StringWriter                                                                       'new stringwriter
            Dim htw As New HtmlTextWriter(sw)                                                                   'new htmlTextWriter
            Dim gvResults As GridView = CType(findDynamicBodyControl("divDataTable,gvResults"), GridView)       'Find the gridview
            gvResults.RenderControl(htw)
            Response.Write(sw.ToString())                                                                       'Write the stringwriter to the excel file we are sending
            Response.[End]()                                                                                    'end the transfer

        Else 'if button is a regular, existing stream button

            Dim strStreamName As String = btn.ID.Replace("btn", "")             '|get stream name
            Dim strClassID As String = strStreamName.Substring(0, 7)
            strStreamName = strStreamName.Remove(0, 7)                          '|remove SIMON ID
            'debug("You pressed the """ & strStreamName & """ stream!")         'debug to make sure that worked
            lblStreamName.Text = strClassID & " > " & strStreamName              'set heading label text

            'Load stream messages
            Dim strMessages(,) = getMessages(readStreamID(strStreamName, strClassID))       'get the messages

            loadMessages(strMessages)
        End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)

    End Sub
    Private Sub loadMessages(ByVal messagesArr(,) As String)
        clearPanelControls()

        Dim intMessageCount = 0

        For Each message In messagesArr                                     'for every message
            If (Not message = Nothing) And (Not IsNumeric(message)) Then    'if the message isn't blank
                'debug(message)
                intMessageCount += 1                                    'Add one to the count

                Dim cols As Integer = messagesArr.GetUpperBound(0)      'Get cols
                Dim rows As Integer = messagesArr.GetUpperBound(1)      'Get rows
                Dim toFind As String = message  'Set what we are looking for
                Dim xIndex As Integer   'for debugging
                Dim yIndex As Integer   'for debugging

                For x As Integer = 0 To cols        'for each col
                    For y As Integer = 0 To rows    'for each row
                        If messagesArr(x, y) = toFind Then 'if we find what we are looking for
                            xIndex = x  'for debugging
                            yIndex = y  'for debugging
                            'debug(toFind & " [(" & x & "," & y & ")] has the fromID " & messagesArr(x, y - 1)) 'for debugging

                            Dim lblMessage As New Label                         'New label
                            lblMessage.ID = "lblMessage" & intMessageCount      'Set label ID to message count
                            'remove SQL-injection prevention double quotes
                            lblMessage.Text = Server.HtmlDecode(cleanHTML(toFind.Replace("''", "'")))
                            If readUserInfo(User.Identity.Name, "int_ID") = messagesArr(x, y - 1) Then
                                'If the current user sent the message, set css class accordingly
                                lblMessage.CssClass = "yourMessage"
                            Else
                                'If not, set the other class then.
                                lblMessage.CssClass = "theirMessage"
                                lblMessage.Text = "<span style=""font-weight: 700"">" & readUserName(messagesArr(x, y - 1)).Replace("@marist.vic.edu.au", "") & "></span> " & lblMessage.Text
                            End If
                            'add label to main content
                            pnlUpdate.ContentTemplateContainer.Controls.Add(lblMessage)
                        End If
                    Next
                Next

            End If
        Next
    End Sub
    Sub clearPanelControls()
        Dim lblList As New Generic.List(Of Label)
        For Each ctrl In pnlUpdate.ContentTemplateContainer.Controls
            If TypeOf ctrl Is Label Then
                lblList.Add(ctrl)
            End If
        Next

        For Each lbl In lblList
            pnlUpdate.ContentTemplateContainer.Controls.Remove(lbl)
        Next
    End Sub
    Function findDynamicBodyControl(ByVal path As String) As Control 'a real dirty way of doing something that should be a lot easier
        Dim strIDArray As Array = path.Split(",")   'split the inputted path

        'output the control defined in the path
        Return Me.Master.FindControl("frmPage").FindControl("BodyContent").FindControl(strIDArray(0)).FindControl(strIDArray(1))
    End Function
    Function findDynamicTopBarControl(ByVal path As String) As Control 'a real dirty way of doing something that should be a lot easier
        Dim strIDArray As Array = path.Split(",")   'split the inputted path

        'output the control defined in the path
        Return Me.Master.FindControl("frmPage").FindControl("topBar").FindControl(strIDArray(0)).FindControl(strIDArray(1))
    End Function
    Function findDynamicSidebarControl(ByVal id As String) As Control 'a real dirty way of doing something that should be a lot easier

        'output the control defined in the path
        Return Me.Master.FindControl("frmPage").FindControl("Sidebar").FindControl(id)
    End Function
    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdate.Tick
        Dim lblStreamName As Label = findDynamicTopBarControl("divStreamHeading,lblStreamName") 'get heading label

        Dim strClassID As String = lblStreamName.Text.Substring(0, 7)
        Dim strStreamName = lblStreamName.Text.Remove(0, 7).Replace(" > ", "")                          '|remove SIMON ID

        Dim strMessages(,) = getMessages(readStreamID(strStreamName, strClassID))       'get the messages

        loadMessages(strMessages)
    End Sub
End Class