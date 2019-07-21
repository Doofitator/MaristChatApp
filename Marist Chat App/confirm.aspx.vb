
Partial Class confirm
    Inherits System.Web.UI.Page
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"

        If User.Identity.IsAuthenticated = True Then 'if remember me was checked
            Response.Redirect("web.aspx") 'login
        End If

        If DatabaseFunctions.runSQL("update tbl_users set bool_verified = true where str_ecrPassword='" & Request.QueryString("account") & "'").StartsWith("Success") Then
            Response.Redirect("Default.aspx?verified=true")
        Else
            Response.Redirect("Default.aspx?verified=false")
        End If
    End Sub
End Class
