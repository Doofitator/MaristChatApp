
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
        'ensure user is authenticated
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("/Default.aspx")
        End If

        LoadSidebar()
    End Sub

    Function addSidebarBtn(ByVal controlID As String, ByVal controlContent As String)
        Dim btn As New Button                               'New button
        btn.ID = controlID                                  'set button ID
        btn.Text = controlContent                           'set button text
        AddHandler btn.Click, AddressOf Me.btn_Click        'add button onclick event
        Me.Master.FindControl("sidebar").Controls.Add(btn)  'add button to sidebar
    End Function
    Function addSidebarLbl(ByVal controlID As String, ByVal controlContent As String)
        Dim lbl As New Literal                              'New Label
        lbl.ID = controlID                                  'set Label ID
        lbl.Text = "<li>" & controlContent & "</li>"        'set Label text
        Me.Master.FindControl("sidebar").Controls.Add(lbl)  'add Label to sidebar
    End Function
    Function addSidebarClientBtn(ByVal controlOnclick As String, ByVal controlText As String) 'basically addSidebarBtn except it runs javascript instead of VB code
        Dim lit As New Literal
        lit.Text = "<input type=""button"" onclick=""" & controlOnclick & """ value=""" & controlText & """ />"
        Me.Master.FindControl("sidebar").Controls.Add(lit)
    End Function

    Function LoadSidebar()
        Dim intRole As Integer = CInt(DatabaseFunctions.readUserRole(User.Identity.Name))
        If intRole = 0 Then 'user is a parent
            'load just alerts
        ElseIf intRole = 1 Then 'user is a student
            'load alerts and class streams
        ElseIf intRole = 2 Then 'user is an educator
            'load alerts and class streams (including add class buttons)
        ElseIf intRole = 3 Then 'user is an administrator
            'load alerts (including new alert buttons), class streams (including add class buttons)
            addSidebarLbl("lblAlerts", "Alerts")
            addSidebarBtn("btnNewAlert", "NEW ALERT")
            addSidebarLbl("lblClasses", "Classes")
            addSidebarClientBtn("HideShow('divNewClass')", "NEW CLASS")
        End If
    End Function

    Sub btn_Click(sender As Object, e As EventArgs) Handles btnWriteClass.Click
        Dim btn As Button = CType(sender, Button)  'get button that called the event

        'if button is a new class button
        If btn.ID = "btnWriteClass" Then
            ' make a New field in tbl_classes called classIdentifier
            '   that could be "alter table tbl_classes add classIdentifier boolean NOT NULL Default (False) With Values False
            ' update tbl_classes set classIdentifier = 1 where userID = getUserID(student1), getUserID(student2), etc.
            '   that could be "update tbl_classes set classIdentifier = 1 where userID in ('getUserID(user1)', 'getUserID(user2)', etc.)"
        End If

        'if button is a new alert button

        'if button is a new stream button

        'if button is a regular, existing stream button
    End Sub
End Class
