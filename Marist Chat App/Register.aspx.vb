
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
    Protected Sub btnSignUp_Click(sender As Object, e As EventArgs) Handles btnSignUp.Click
        Dim strEml As String = Email.Text           '|
        Dim strPW1 As String = Password.Text        '|
        Dim strPW2 As String = ConfirmPassword.Text '| Declare variables
        Dim intRole As Integer                      '|
        Dim yrGraduation As DateTime                '|
        Dim strEcrPw As String                      '|

        If DatabaseFunctions.userExists(strEml) Then 'Check that the user doesn't already exist
            ErrorMessage.Text = "A user with that email address aleady exists."
            Exit Sub
        End If

        If Not strPW1 = strPW2 Then                                 'check passwords are the same
            ErrorMessage.Text = "The passwords do not match!"       'alert if not
            Exit Sub
        End If

        Try
            Dim emlAddr As New Net.Mail.MailAddress(strEml)         'try to convert input to an 'email' data type
        Catch                                                       'if it fails
            ErrorMessage.Text = "Please enter a valid email address and try again." 'alert invalid email
            Exit Sub
        End Try

        If strEml.Contains("@marist.vic.edu.au") Then               'if email is a marist one
            If HasNumber(strEml) Then                               'if email contains numbers (student email)
                intRole = 1                                         'set role to 1 (student)
            Else
                intRole = 2                                         'set role to 2 (educator)
            End If
        Else
            intRole = 0                                             'set role to 0 (parent / friend)
        End If


        'Response.Write("<script>console.log('Role: " & intRole & "')</script>") 'test / debug role var

        'get graduation year
        yrGraduation = New DateTime(Graduation.Text, 1, 1)

        'Response.Write("<script>console.log('Date: " & yrGraduation.ToLongDateString & "')</script>") 'test / debug year var

        'encrypt password

        Dim ecrWrapper As Encryption = New Encryption(strEml)      'make a new encrypted string with the key of username
        strEcrPw = ecrWrapper.EncryptData(strPW1)                   'encrypt the password with the key of the username

        'write to database
        Dim strSql = "insert into tbl_users (str_email, str_ecrPassword, int_role, dt_graduationYear) values ('" & strEml & "', '" & DatabaseFunctions.MakeSQLSafe(strEcrPw) & "', '" & intRole & "', '" & yrGraduation & "')"
        Dim out As String = DatabaseFunctions.runSQL(strSql) 'run command
        If out = "Success" Then 'if command worked
            Response.Redirect("/Default.aspx") 'redirect to login
        Else
            ErrorMessage.Text = "There was an error creating the user: <br><br> " & out 'show error
        End If
    End Sub

    Function HasNumber(strData As String) As Boolean
        'stolen from https://www.ozgrid.com/forum/forum/help-forums/excel-general/29317-check-if-string-contains-a-number?p=1044961#post1044961
        Dim iCnt As Integer

        For iCnt = 1 To Len(strData)
            If IsNumeric(Mid(strData, iCnt, 1)) Then
                HasNumber = True
                Exit Function
            End If
        Next iCnt
        HasNumber = False
    End Function
End Class
