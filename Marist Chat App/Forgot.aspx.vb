
Partial Class Forgot
    Inherits System.Web.UI.Page

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'check if device is mobile & set master page accordingly
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"

        Dim urlReferrer As String = String.Empty
        If Request.UrlReferrer IsNot Nothing Then urlReferrer = Request.UrlReferrer.ToString 'get previous page URL

        If User.Identity.IsAuthenticated = True And Not urlReferrer.EndsWith("web.aspx") Then 'if remember me was checked
            Response.Redirect("web.aspx") 'login
        End If
    End Sub
    Protected Sub page_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim strAccount As String = Request.QueryString("account")

        'Set UI text as required

        If strAccount = Nothing Then
            lblTextHint.Text = "Email address: "
            btnSubmit.Text = "Send reset email"
        Else
            lblTextHint.Text = "New Password:"
            btnSubmit.Text = "Reset password"
            txtInput.TextMode = TextBoxMode.Password
        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If btnSubmit.Text = "Send reset email" Then
            'send reset request email & redirect
            Dim strStore As String = DatabaseFunctions.readUserPassword(DatabaseFunctions.MakeSQLSafe(txtInput.Text.ToLower))
            If Email.resetEmailMessage(txtInput.Text.ToLower, strStore) = True Then
                Response.Redirect("/Default.aspx?reset=true")
            Else
                Response.Redirect("/Default.aspx?reset=false")
            End If
        Else
            'update password in db & redirect
            Dim ecrWrapper As Encryption = New Encryption(DatabaseFunctions.readUserNameFromECR(Request.QueryString("account")))      'make a new encrypted string with the key of username
            Dim strEcrPw As String = ecrWrapper.EncryptData(txtInput.Text)                   'encrypt the password with the key of the username
            'DatabaseFunctions.eDebug(DatabaseFunctions.readUserNameFromECR(Request.QueryString("account")) & " - " & strEcrPw)
            If DatabaseFunctions.runSQL("update tbl_users set str_ecrPassword = '" & strEcrPw & "' where str_ecrPassword = '" & Request.QueryString("account") & "'").StartsWith("Success") Then
                Response.Redirect("/Default.aspx")
            Else
                Response.Redirect("/Default.aspx?reset=false")
            End If

        End If
    End Sub
End Class
