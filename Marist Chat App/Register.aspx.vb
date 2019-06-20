
Partial Class Register
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"

        'check that the user isn't here a second time accidentally
        If User.Identity.IsAuthenticated Then
            Response.Redirect("/Default.aspx")
        End If
    End Sub
End Class
