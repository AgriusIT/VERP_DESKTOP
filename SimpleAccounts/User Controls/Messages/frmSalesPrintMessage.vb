Imports System.Windows.Forms
Public Class frmSalesPrintMessage
    Private _message As String = String.Empty

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
            'ApplyStyleSheet(Me)
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
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        frmSales.Paid = rdoPaid.Checked
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


    Private Sub frmSalesPrintMessage_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try

            If e.KeyCode = Keys.Y Then

                OK_Button_Click(Me, Nothing)

            ElseIf e.KeyCode = Keys.N Then
                ''
                btnNo_Click(Me, Nothing)
            ElseIf e.KeyCode = Keys.P Then
                rdoPaid.Checked = True
            ElseIf e.KeyCode = Keys.U Then
                rdoUnPaid.Checked = True


            End If
        Catch ex As Exception
            msg_Error(ex.Message)


        End Try
    End Sub



End Class
