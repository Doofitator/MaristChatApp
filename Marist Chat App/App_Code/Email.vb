﻿Imports Microsoft.VisualBasic
Imports System.Net.Mail

Public Class Email

    Public Shared Function sendEmailMessage(ByVal toAddress As String, ByVal subject As String, ByVal body As String) As Boolean
        Dim result As Boolean
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("noreply@mca.maristapps.com", "M@ri3t!!")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = False
            'Smtp_Server.Host = "mail.mca.maristapps.com"
            Smtp_Server.Host = "mail13.qnetau.com"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("noreply@mca.maristapps.com")
            e_mail.To.Add(toAddress)
            e_mail.Subject = subject
            e_mail.IsBodyHtml = True '
            e_mail.Body = body
            Smtp_Server.Send(e_mail)

            result = True
        Catch ex As Exception
            DatabaseFunctions.eDebug(ex.StackTrace)
            result = False
        End Try
        Return result
    End Function

    Public Shared Function confirmEmailMessage(ByVal emailAddr As String, ByVal ecrPass As String) As String
        'write email message
        Dim eml As String = "Hello, " & emailAddr & "<br><br>You have signed up to <a href=""http://mca.maristapps.com"">mca.maristapps.com</a> and your email address needs to be confirmed before you can log in. Please go to <a href=""http://mca.maristapps.com/confirm.aspx?account=" & ecrPass & """>this link</a> to complete the signup process."
        Return sendEmailMessage(emailAddr, "Confirm your account", eml)
    End Function

    Public Shared Function resetEmailMessage(ByVal emailAddr As String, ByVal ecrPass As String) As String
        'write email message
        Dim eml As String = "Hello, " & emailAddr & "<br><br>You have requested a password reset at <a href=""http://mca.maristapps.com"">mca.maristapps.com</a>. Please go to <a href=""http://mca.maristapps.com/Forgot.aspx?account=" & ecrPass & """>this link</a> to complete the process."
        Return sendEmailMessage(emailAddr, "Reset your password", eml)
    End Function
End Class
