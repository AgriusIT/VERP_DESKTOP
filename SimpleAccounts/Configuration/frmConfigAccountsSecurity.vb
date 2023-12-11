''TFS3274 : Ayesha Rehman : 22-05-2018 :Configurations implementation on new design Accounts
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Public Class frmConfigAccountsSecurity
    Public isFormOpen As Boolean = False

    Private Sub linkGeneralAccount_Click(sender As Object, e As EventArgs) Handles linkGeneralAccount.Click
        Try
            If frmConfigAccounts.isFormOpen = True Then
                frmConfigAccounts.Dispose()
            End If

            frmConfigAccounts.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub linkAccount_Click(sender As Object, e As EventArgs) Handles linkAccount.Click
        Try
            If frmConfigAccountsAcc.isFormOpen = True Then
                frmConfigAccountsAcc.Dispose()
            End If

            frmConfigAccountsAcc.ShowDialog()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetAllRecords(Optional ByVal Condition As String = "")

        Try
            If Not getConfigValueByType("DirectVoucherPrinting").ToString = "Error" Then
                Me.chkDirectVoucherPrinting.Checked = Convert.ToBoolean(getConfigValueByType("DirectVoucherPrinting").ToString)
            End If
            Me.chkEnabledDuplicateVoucherLog.Checked = Convert.ToBoolean(getConfigValueByType("EnabledDuplicateVoucherLog"))
            Me.chkAllowPaymentZeroBalance.Checked = Convert.ToBoolean(getConfigValueByType("AllowPaymentZeroBalance"))
            Me.ChkDirectPrinting.Checked = Convert.ToBoolean(getConfigValueByType("Print").ToString)
            Me.chkAccountHeadReadonly.Checked = Convert.ToBoolean(getConfigValueByType("AccountHeadReadOnly").ToString)
            Me.chkChangeVoucherType.Checked = Convert.ToBoolean(getConfigValueByType("ChangeVoucherType").ToString)
            Me.chkPreviouseRecordShow.Checked = Convert.ToBoolean(getConfigValueByType("PreviouseRecordShow").ToString)
            Me.chkUpdatePostedVoucher.Checked = Convert.ToBoolean(getConfigValueByType("UpdatePostedVoucher").ToString)
            If Me.chkPreviouseRecordShow.Checked = False Then
                Me.chkPreviouseRecordShowNot.Checked = True
            End If
            If Me.chkDirectVoucherPrinting.Checked = False Then
                Me.chkDirectVoucherPrintingNot.Checked = True
            End If
            If Me.chkEnabledDuplicateVoucherLog.Checked = False Then
                Me.chkEnabledDuplicateVoucherLogNot.Checked = True
            End If
            If Me.chkAllowPaymentZeroBalance.Checked = False Then
                Me.chkAllowPaymentZeroBalanceNot.Checked = True
            End If
            If Me.ChkDirectPrinting.Checked = False Then
                Me.ChkDirectPrintingNot.Checked = True
            End If
            If Me.chkAccountHeadReadonly.Checked = False Then
                Me.chkAccountHeadReadonlyNot.Checked = True
            End If
            If Me.chkChangeVoucherType.Checked = False Then
                Me.chkChangeVoucherTypeNot.Checked = True
            End If
            If Me.chkUpdatePostedVoucher.Checked = False Then
                Me.chkUpdatePostedVoucherNot.Checked = True
            End If


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub frmConfigAccountsSecurity_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.isFormOpen = True
            getConfigValueList()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ChkDirectPrinting_CheckedChanged(sender As Object, e As EventArgs) Handles ChkDirectPrinting.CheckedChanged, chkAccountHeadReadonly.CheckedChanged, _
        chkAllowPaymentZeroBalance.CheckedChanged, chkChangeVoucherType.CheckedChanged, chkDirectVoucherPrinting.CheckedChanged, _
        chkEnabledDuplicateVoucherLog.CheckedChanged, chkPreviouseRecordShow.CheckedChanged, chkUpdatePostedVoucher.CheckedChanged
        Try
            frmConfigCompany.saveRadioBtnConfig(sender)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class