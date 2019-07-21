
Partial Class Default2
    Inherits System.Web.UI.Page
    Sub addNewStreamDiv()
        Dim divNewStream As New HtmlGenericControl("div")                'New div
        divNewStream.ID = "divNewStream"                                 'Set ID
        divNewStream.Attributes.Add("class", "wizard")                   'Set CSS Class

        Me.Master.FindControl("BodyContent").Controls.Add(divNewStream)  'Add the div to the page

        Dim divNewStreamTitleBar = New HtmlGenericControl("div")         'New 'titlebar' div
        divNewStreamTitleBar.ID = "divNewStreamTitleBar"                 'Set ID
        divNewStreamTitleBar.Attributes.Add("class", "titleBar")         'Set CSS Class
        '                                                                'Add innerHTML incl. 'close' button
        divNewStreamTitleBar.InnerHtml = "<h3>New Stream Wizard<input style=""float: right"" type=""button"" onclick=""HideShow('BodyContent_divNewStream')"" value=""X"" /></h3>"
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

        Dim ClassID As String = "IT0331A"
        Dim strUsers() As String = DatabaseFunctions.getUsers(ClassID)

        Dim i As Integer = strUsers.Length - 1
        While i > -1
            Dim txtUserList As New DropDownList                                  '|
            txtUserList.ID = "txtUserList" & i                                  '| Add to div
            txtUserList.Items.Add("")
            For Each thing In strUsers
                txtUserList.Items.Add(thing)
            Next
            divNewStream.Controls.Add(txtUserList)                           '|
            i -= 1
        End While

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

    Private Sub btn_Click(sender As Object, e As EventArgs)
        Dim x As Button = CType(sender, Button)
        If x.ID = "btnWriteClass1" Then
            Dim latest As DropDownList = CType(findDynamicBodyControl("divNewClass,txtUserList3"), DropDownList)

        End If
    End Sub

    Private Sub Default2_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        addNewStreamDiv()
    End Sub
    Function findDynamicBodyControl(ByVal path As String) As Control 'a real dirty way of doing something that should be a lot easier
        Dim strIDArray As Array = path.Split(",")   'split the inputted path

        'output the control defined in the path
        Return Me.Master.FindControl("frmPage").FindControl("BodyContent").FindControl(strIDArray(0)).FindControl(strIDArray(1))
    End Function

    Function findnewclassdiv() As Control 'a real dirty way of doing something that should be a lot easier

        'output the control defined in the path
        Return Me.Master.FindControl("frmPage").FindControl("BodyContent").FindControl("divNewClass")
    End Function
End Class
