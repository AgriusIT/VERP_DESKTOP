Imports System.Windows.Forms
Imports SBDal
Public Class frmImportPopup
    Private _message As String = String.Empty
    Public Shared Print As Boolean = False
    Public Shared DirectVoucherPrinting As Boolean = False
    Public Shared FinancialImpact As Boolean = False
    Public Shared UpdateVoucher As Boolean = False
    Public Shared AddNewVoucher As Boolean = False
    Public Shared Save As Boolean = False
    Public VoucherNo As String = ""
    Public FirstVoucherNo As String = ""
    Public VoucherReference As String = String.Empty
    Public VoucherDate As DateTime
    Public Shared IsNewVoucher As Boolean = False
    'Public DirectVoucherPrinting As Boolean

    Public Sub PrintingCheck()
        'Me.chkEnableSlipPrints.Visible = True
        'Me.chkEnableVoucherPrints.Visible = True
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
            'lblMessage.Text = _message
            Me.Text = str_MessageHeader
            If Save = True Then
                Me.lblMessage.Visible = False
                Me.lblMessage.Text = ""
                Me.rbUpdateVoucher.Visible = False
                Me.rbAddNewVoucher.Visible = False
                Me.lblVoucherNo.Visible = False
                Me.txtVoucherNo.Visible = False
                Me.txtVoucherNo.Text = VoucherNo
                FirstVoucherNo = FirstVoucherNo
                Me.lblVoucherDate.Visible = False
                Me.dtpVoucherDate.Visible = False
                Me.txtVoucherReference.Visible = False
                Me.txtVoucherReference.Text = ""
                Me.lblVoucherReference.Visible = True
                lblVoucherReference.Text = _message
                cbPrint.Visible = True
                Me.cbPrint.Visible = DirectVoucherPrinting
                Me.cbPrint.Checked = DirectVoucherPrinting
                rbAddNewVoucher.Visible = False
                rbUpdateVoucher.Visible = False
                If _message.Length > 35 Then
                    Me.cbPrint.Location = New System.Drawing.Point(315, 83)
                Else
                    Me.cbPrint.Location = New System.Drawing.Point(226, 83)
                    'Me.cbPrint.Location = New System.Drawing.Point(304, 57)

                End If
                Me.btnYes.Text = "Yes"
                Me.btnNo.Text = "No"
            Else
                If AddNewVoucher = False Then
                    rbAddNewVoucher.Enabled = False
                End If
                If AddNewVoucher = True Then
                    rbAddNewVoucher.Enabled = True
                End If
                Me.lblMessage.Visible = False
                Me.rbUpdateVoucher.Visible = True
                Me.rbUpdateVoucher.Checked = True
                Me.rbAddNewVoucher.Visible = True
                Me.lblVoucherNo.Visible = True
                Me.txtVoucherNo.Visible = True
                Me.txtVoucherNo.Text = VoucherNo
                FirstVoucherNo = FirstVoucherNo
                Me.lblVoucherDate.Visible = True
                Me.dtpVoucherDate.Visible = True
                Me.txtVoucherReference.Visible = True

                Me.lblVoucherReference.Visible = True
                lblVoucherReference.Text = "Voucher Reference"
                Me.cbPrint.Visible = DirectVoucherPrinting
                Me.cbPrint.Checked = DirectVoucherPrinting
                Me.txtVoucherReference.Text = VoucherReference
                Me.cbPrint.Location = New System.Drawing.Point(304, 57)
                lblMessage.Text = _message
                If Save = True Then
                    rbAddNewVoucher.Visible = False
                    rbUpdateVoucher.Visible = False
                    Me.btnYes.Text = "Save"
                    'Me.btnNo.Text = "No"
                Else
                    rbAddNewVoucher.Visible = True
                    rbUpdateVoucher.Visible = True
                    Me.btnYes.Text = "Update"
                    'Me.btnNo.Text = "No"
                End If
            End If
            'Me.btnYes.Text = "Update"
            'Me.btnNo.Text = "Cancel"
            'End If
            'Me.chkEnableSlipPrints.Visible = True
            'Me.chkEnableVoucherPrints.Visible = DirectVoucherPrinting
            'Me.chkEnablePrint.Visible = Print

            'Me.chkEnableVoucherPrints.Checked = DirectVoucherPrinting
            'Me.chkEnablePrint.Checked = Print
            'ApplyStyleSheet(Me)
            Me.ShowDialog()
            If DialogResult = Windows.Forms.DialogResult.Yes Then
                DirectVoucherPrinting = Me.cbPrint.Checked
                'Print = Me.chkEnablePrint.Checked
                VoucherNo = Me.txtVoucherNo.Text
                VoucherReference = Me.txtVoucherReference.Text
                If rbAddNewVoucher.Checked = True Then
                    IsNewVoucher = True
                Else
                    IsNewVoucher = False
                End If
                VoucherDate = Me.dtpVoucherDate.Value
                Me.txtVoucherNo.Text = ""
                Me.txtVoucherReference.Text = ""
                Me.dtpVoucherDate.Value = Now
                Return Windows.Forms.DialogResult.Yes
            Else
                Me.txtVoucherNo.Text = ""
                Me.txtVoucherReference.Text = ""
                Me.dtpVoucherDate.Value = Now
                Return Windows.Forms.DialogResult.No
            End If
            strMessage = String.Empty

            'VoucherNo = Me.txtVoucherNo.Text
            'VoucherReference = Me.txtVoucherReference.Text
            'If rbAddNewVoucher.Checked = True Then
            '    IsNewVoucher = True
            'Else
            '    IsNewVoucher = False
            'End If
            'VoucherDate = Me.dtpVoucherDate.Value
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

    Private Sub frmImportPopup_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub rbAddNewVoucher_CheckedChanged(sender As Object, e As EventArgs) Handles rbAddNewVoucher.CheckedChanged
        Try
            If rbAddNewVoucher.Checked = True Then
                Me.txtVoucherNo.Text = New LCDAL().GetNextVoucherNo(FirstVoucherNo)
                Me.btnYes.Text = "New"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lblTitle_Click(sender As Object, e As EventArgs) Handles lblTitle.Click

    End Sub
End Class
