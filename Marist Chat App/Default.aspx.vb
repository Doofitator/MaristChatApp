
Partial Class _Default
    Inherits System.Web.UI.Page


    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
    End Sub
End Class
