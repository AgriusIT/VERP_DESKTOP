Imports System.Net
Imports System.IO
Public Class frmUserServay
    Private Sub frmUserServay_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                DialogResult = Windows.Forms.DialogResult.Yes
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            frmHome.SetSurveyNo()
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Try
            frmHome.SetSurveyYes()
            OpenLink()
            DialogResult = Windows.Forms.DialogResult.Yes
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmUserServay_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Protected Sub OpenLink()
        Try
            Process.Start("http://1drv.ms/1Yzw7nC")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class