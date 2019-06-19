﻿
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
    End Sub
    Protected Sub frmLogin_Authenticate(sender As Object, e As AuthenticateEventArgs) Handles frmLogin.Authenticate
        Dim strEml As String = frmLogin.UserName 'get username
        Dim strPw As String = frmLogin.Password 'get password
        'check that strEml is actually an email address
        Try
            Dim emlAddr As New Net.Mail.MailAddress(strEml)
        Catch
            e.Authenticated = False
            frmLogin.FailureText = "Please enter a valid email address and try again."
        End Try

        'encrypt password

        Dim strEcrPw As String  'new string to hold encrypted password

        Dim ecrWrapper As Encryption = New Encryption(strEml)      'make a new encrypted string with the key of username
        strEcrPw = ecrWrapper.EncryptData(strPw)                   'encrypt the password with the key of the username

        'send to database

        'get result

        'e.Authenticated = True

        'redirect to /web.aspx
    End Sub
End Class
