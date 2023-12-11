Imports Microsoft.VisualBasic.MsgBoxStyle
Imports System.Windows.Forms

Public Class frmMessage
    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property
    Public Function MsgBox(ByVal strMessage As String, ByVal str_MessageHeader As String) As MsgBoxResult
        Try
            _message = strMessage
            lblMessage.Text = _message
            Me.Text = str_MessageHeader
            'ApplyStyleSheet(Me)
            Me.ShowDialog()
            Return MsgBoxResult.Ok
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub frmMessage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblMessage.Text = _message
    End Sub
End Class
