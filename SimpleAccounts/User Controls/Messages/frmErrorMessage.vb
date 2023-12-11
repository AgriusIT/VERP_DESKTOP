Imports System.Windows.Forms
Public Class frmErrorMessage

    Private _Message As String = String.Empty
    Public Property Message() As String
        Get
            Return _Message
        End Get
        Set(ByVal value As String)
            _Message = value
        End Set
    End Property
    Public Function ShowMsg(ByVal str_Message As String) As MsgBoxResult
        Try
            Message = str_Message
            Me.Text = str_MessageHeader
            Me.lblMessage.Text = Message
            ' Me.BackColor = System.Drawing.Color.FromArgb(226, 235, 247)
            ' ApplyStyleSheet(Me)
            'lblMessage.Size = New Size(260, 87)
         
            '   Me.lblMessage.BackColor = Color.Transparent
            '  Me.PictureBox1.BackColor = Color.Transparent
            Me.ShowDialog()
            Me.BringToFront()
            Return MsgBoxResult.Ok
            str_Message = String.Empty
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub frmErrorMessage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.lblMessage.Text = _Message
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
