Public Class frmMixPayment

    'Private Sub txtCash_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCash.KeyDown, txtBank.KeyDown, txtCredit.KeyDown, txtCreditCard.KeyDown, txtChequeNo.KeyDown
    '    Try
    '        If e.KeyCode = Keys.Enter Then
    '            If Val(txtBank.Text) + Val(txtCash.Text) + Val(txtCredit.Text) + Val(txtCreditCard.Text) = Val(frmPOSEntry.txtNetTotal.Text) Then
    '                Me.Close()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FillCombos(Optional Condition As String = "")
        Try
            Dim Str As String
            If Condition = "CreditCard" Then
                Str = "SELECT CreditCardId, MachineTitle, BankAccountId from tblCreditCardAccount where POStitle = '" & frmPOSEntry.Title & "'"
                FillDropDown(cmbCCAccount, Str, True)
            ElseIf Condition = "Bank" Then
                Str = "If  exists(select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1 And Account_Id Is Not Null) " _
                    & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'Bank' And coa_detail_id in (select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1) " _
                    & " Else " _
                    & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'Bank'"
                FillDropDown(cmbBank, Str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmMixPayment_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            FillCombos("Bank")
            FillCombos("CreditCard")
            ResetControl()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ResetControl()
        Try
            Me.txtCreditCard.Text = ""
            Me.txtCredit.Text = ""
            Me.txtCash.Text = ""
            Me.txtBank.Text = ""
            Me.txtChequeNo.Text = ""
            If cmbCCAccount.SelectedValue > 0 Then
                Me.cmbCCAccount.SelectedIndex = 0
            End If
            If cmbBank.SelectedValue > 0 Then
                Me.cmbBank.SelectedIndex = 0
            End If
            chkOnline.Checked = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Val(txtBank.Text) + Val(txtCash.Text) + Val(txtCredit.Text) + Val(txtCreditCard.Text) = Val(frmPOSEntry.txtNetTotal.Text) Then
                frmPOSEntry.Cash = Val(txtCash.Text)
                frmPOSEntry.Credit = Val(txtCredit.Text)
                frmPOSEntry.Bank = Val(txtBank.Text)
                frmPOSEntry.CreditCard = Val(txtCreditCard.Text)
                If cmbBank.SelectedValue > 0 AndAlso Val(txtBank.Text) > 0 Then
                    frmPOSEntry.BAID = cmbBank.SelectedValue
                    If chkOnline.Checked = True Then
                        frmPOSEntry.Online = True
                    Else
                        frmPOSEntry.ChequeNo = Val(txtChequeNo.Text)
                    End If
                Else
                    ShowErrorMessage("Please Check you have entered Bank Amount and Account")
                End If
                If cmbCCAccount.SelectedValue > 0 AndAlso Val(txtCreditCard.Text) > 0 Then
                    frmPOSEntry.CCAID = Val(CType(Me.cmbCCAccount.SelectedItem, DataRowView).Item("BankAccountId").ToString)
                Else
                    ShowErrorMessage("Please Check you have entered Credit Card Amount and Account")
                End If
                Me.Close()
            Else
                ShowErrorMessage("Sum of these Values not euqal to POS Total")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class