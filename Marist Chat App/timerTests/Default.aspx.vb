Imports DatabaseFunctions
Partial Class timerTests_Default
    Inherits System.Web.UI.Page

    Private Sub x(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim strmessages(,) As String = getMessages(2)

        Dim intMessageCount = 0

        For Each message In strmessages                                     'for every message
            If (Not message = Nothing) And (Not IsNumeric(message)) Then    'if the message isn't blank
                intMessageCount += 1                                    'Add one to the count

                Dim cols As Integer = strmessages.GetUpperBound(0)      'Get cols
                Dim rows As Integer = strmessages.GetUpperBound(1)      'Get rows
                Dim toFind As String = message  'Set what we are looking for
                Dim xIndex As Integer   'for debugging
                Dim yIndex As Integer   'for debugging

                For x As Integer = 0 To cols        'for each col
                    For y As Integer = 0 To rows    'for each row
                        If strmessages(x, y) = toFind Then 'if we find what we are looking for
                            xIndex = x  'for debugging
                            yIndex = y  'for debugging
                            'debug(toFind & " [(" & x & "," & y & ")] has the fromID " & strMessages(x, y - 1)) 'for debugging

                            Dim lblMessage As New Label                         'New label
                            lblMessage.ID = "lblMessage" & intMessageCount      'Set label ID to message count
                            'remove SQL-injection prevention double quotes
                            lblMessage.Text = Server.HtmlDecode(toFind.Replace("''", "'"))
                            If readUserInfo(User.Identity.Name, "int_ID") = strmessages(x, y - 1) Then
                                'If the current user sent the message, set css class accordingly
                                lblMessage.CssClass = "yourMessage"
                            Else
                                'If not, set the other class then.
                                lblMessage.CssClass = "theirMessage"
                                lblMessage.Text = "<span style=""font-weight: 700"">" & readUserName(strmessages(x, y - 1)).Replace("@marist.vic.edu.au", "") & "></span> " & lblMessage.Text
                            End If
                            'add label to main content
                            UpdatePanel1.ContentTemplateContainer.Controls.Add(lblMessage)
                            UpdatePanel1.ContentTemplateContainer.Controls.Add(New LiteralControl("<br>"))
                        End If
                    Next
                Next

            End If
        Next
    End Sub
End Class
