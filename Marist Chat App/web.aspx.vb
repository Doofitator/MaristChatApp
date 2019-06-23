
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
        'ensure user is authenticated
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("/Default.aspx")
        End If

        addSidebarBtn("PP", "XYZ")
    End Sub

    Function addSidebarBtn(ByVal controlID As String, ByVal controlContent As String)
        Dim btn As New Button
        btn.ID = controlID
        btn.Text = controlContent
        Me.Master.FindControl("sidebar").Controls.Add(btn)
    End Function
End Class
