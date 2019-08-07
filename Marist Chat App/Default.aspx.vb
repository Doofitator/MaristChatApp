
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"

        If User.Identity.IsAuthenticated = True Then 'if remember me was checked
            Response.Redirect("web.aspx") 'login
        End If
    End Sub

    Protected Sub frmLogin_Authenticate(sender As Object, e As AuthenticateEventArgs) Handles frmLogin.Authenticate
        Dim strEml As String = frmLogin.UserName.ToLower() 'get username
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

        If Not DatabaseFunctions.isAuthenticated(strEml) Then
            frmLogin.FailureText = "That address is not in our system or has not been authenticated. Please try again."
            e.Authenticated = False
            Exit Sub
        End If

        'send to database
        Dim strStore As String = DatabaseFunctions.readUserPassword(strEml)
        If strStore = strEcrPw Then
            'password is correct
            FormsAuthentication.SetAuthCookie(frmLogin.UserName, frmLogin.RememberMeSet)
            e.Authenticated = True 'login
        ElseIf strStore.StartsWith("FailConnOpen") Then
            'Database unreachable
            frmLogin.FailureText = "There was an error logging on. Please check your Internet connection."
        Else
            'password incorrect
            frmLogin.FailureText = "Your login attempt was not successful. Please try again."
            e.Authenticated = False
        End If
    End Sub

    Private Sub _Default_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        'Code to handle various GET requests sent from other files

        If Request.QueryString("verify") = "true" Then
            lblEmailHint.Text = "Please check your emails for a verification link before proceeding."
        End If
        If Request.QueryString("verified") = "true" Then
            lblEmailHint.Text = "Verified user successfully. Please login."
        ElseIf Request.QueryString("verified") = "false" Then
            lblEmailHint.Text = "User verification failed."
        End If
        If Request.QueryString("reset") = "true" Then
            lblEmailHint.Text = "Please check your emails for a password reset link."
        ElseIf Request.QueryString("reset") = "false" Then
            lblEmailHint.Text = "Password reset failed."
        End If
    End Sub
End Class
