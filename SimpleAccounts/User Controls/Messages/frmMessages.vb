Imports System.Windows.Forms
Public Class frmMessages
    Private _message As String = String.Empty
    Public Shared Print As Boolean = False
    Public Shared DirectVoucherPrinting As Boolean = False
    'Public DirectVoucherPrinting As Boolean
    
    Public Sub PrintingCheck()
        Me.chkEnableSlipPrints.Visible = True
        Me.chkEnableVoucherPrints.Visible = True
    End Sub



    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set

    End Property
    Public Function MsgBox_Print(ByVal strMessage As String) As MsgBoxResult
        Try
            PrintingCheck()
            _message = strMessage
            lblMessage.Text = _message
            Me.Text = str_MessageHeader
            ApplyStyleSheet(Me)
            Me.ShowDialog()
            If DialogResult = Windows.Forms.DialogResult.Yes Then
                Return Windows.Forms.DialogResult.Yes
            Else
                Return Windows.Forms.DialogResult.No
            End If
            strMessage = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function MsgBox(ByVal strMessage As String) As MsgBoxResult
        Try
            _message = strMessage
            lblMessage.Text = _message
            Me.Text = str_MessageHeader
            'Me.chkEnableSlipPrints.Visible = True
            Me.chkEnableVoucherPrints.Visible = DirectVoucherPrinting
            Me.chkEnablePrint.Visible = Print

            Me.chkEnableVoucherPrints.Checked = DirectVoucherPrinting
            Me.chkEnablePrint.Checked = Print
            'ApplyStyleSheet(Me)
            Me.ShowDialog()
            If DialogResult = Windows.Forms.DialogResult.Yes Then
                DirectVoucherPrinting = Me.chkEnableVoucherPrints.Checked
                Print = Me.chkEnablePrint.Checked
                Return Windows.Forms.DialogResult.Yes
            Else
                Return Windows.Forms.DialogResult.No
            End If
            strMessage = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub
    Private Sub frmMessages_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            lblMessage.Text = _message
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Private Sub frmMessages_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Y Then
                OK_Button_Click(Me, Nothing)
            ElseIf e.KeyCode = Keys.N Then
                btnNo_Click(Me, Nothing)
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
End Class
