﻿
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
    End Sub
    Protected Sub frmLogin_Authenticate(sender As Object, e As AuthenticateEventArgs) Handles frmLogin.Authenticate
        e.Authenticated = True
    End Sub
End Class
