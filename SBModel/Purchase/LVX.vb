Imports System.Windows.Forms

Public Class LVX
    Inherits ListBox
    Private _par As TextBox
    Public Sub SendKeyString(ByVal strng As String)
        SendKeys.Send(strng)
    End Sub
    Public Sub New(ByVal tb As TextBox)
        Me._par = tb
    End Sub

    Private Sub LVX_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        If Me.SelectedItem = "" Then
            Me.SelectedIndex = 0
            SendKeys.Send(vbTab)
        End If
    End Sub

    Private Sub LVX_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode <> Keys.Up And e.KeyCode <> Keys.Down Then
            _par.Focus()
            SendKeys.Send(e.KeyCode)
            If e.KeyCode = Keys.Escape Then
                Me.Hide()
            ElseIf e.KeyCode = Keys.Enter Then
                Dim curLoc As Integer = _par.SelectionStart
                Dim spaceLoc As Integer = InStrRev(_par.Text, " ")
                _par.SelectionStart = spaceLoc
                _par.SelectionLength = Math.Abs(spaceLoc - curLoc)
                _par.SelectedText = Me.SelectedItem
                Me.Hide()
            End If
        Else

        End If
    End Sub
End Class