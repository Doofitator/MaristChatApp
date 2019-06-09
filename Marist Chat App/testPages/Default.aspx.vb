﻿
Partial Class testPages_Default
    Inherits System.Web.UI.Page
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        If Request.Browser.IsMobileDevice Then MasterPageFile = "~/MobileMasterPage.master"
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If RadioButtonList1.SelectedIndex = 0 Then
            TextBox1.Text = "You selected Test1"
        ElseIf RadioButtonList1.SelectedIndex = 1 Then
            TextBox1.Text = "You selected Test2"
        ElseIf RadioButtonList1.SelectedIndex = 2 Then
            TextBox1.Text = "You selected Test3"
        Else
            TextBox1.Text = "You didn't select anything."
        End If
    End Sub
End Class
