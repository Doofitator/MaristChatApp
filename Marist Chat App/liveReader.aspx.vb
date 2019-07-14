Imports DatabaseFunctions
Partial Class liveReader
    Inherits System.Web.UI.Page
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        'ensure user is authenticated
        If Not User.Identity.IsAuthenticated Then
            Response.Redirect("/Default.aspx")
        End If

        Dim intRole As Integer = CInt(readUserInfo(User.Identity.Name, "int_role"))
        If Not intRole = 3 Then
            Response.Redirect("/Default.aspx")
        End If
    End Sub

    Private Sub tmrRead_Tick(sender As Object, e As EventArgs) Handles tmrRead.Tick
        Dim strmessages(,) As String = getMessages(Request.QueryString("ID"))

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
                            pnlUpdate.ContentTemplateContainer.Controls.Add(lblMessage)
                            pnlUpdate.ContentTemplateContainer.Controls.Add(New LiteralControl("<br>"))
                        End If
                    Next
                Next

            End If
        Next
    End Sub
End Class
