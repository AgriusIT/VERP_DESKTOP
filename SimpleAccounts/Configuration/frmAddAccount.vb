Public Class frmAddAccount
    
    Public AccountId As Integer = 0
    Public AccountDesc As String = String.Empty
    Public AccountType As String = String.Empty
    Public IsTaxForm As Boolean = False 'Tax accounts are applicable on all accounts except Bank and Cash in Reciept,Payment screen in task #1608
    Private Sub GetAllRecord(Optional ByVal Condition As String = "")
        Try

            Dim str As String = String.Empty
            If Condition = String.Empty Then
                str = "Select coa_detail_id, detail_code, detail_title From vwCOADetail WHERE Active=1"
            ElseIf Condition = "SubSubAccount" Then
                str = "Select DISTINCT main_sub_sub_id as coa_detail_id, sub_sub_code as detail_code, sub_sub_title as detail_title From vwCOADetail WHERE Active=1 AND Sub_sub_title <> ''"
            End If
            Me.grdAddAccount.DataSource = GetDataTable(str)
            AccountType = String.Empty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'All accounts are shown except Cash and Bank accounts
    Private Sub GetAllTaxRecord(Optional ByVal Condition As String = "")
        Try
            If IsTaxForm = True Then
                Dim str As String = String.Empty
                If Condition = String.Empty Then
                    'str = "Select coa_detail_id, detail_code, detail_title From vwCOADetail WHERE Active=1 AND main_code <> 02"
                    'Ali Faisal : TFS4712 : Accounts filter with "Cash, Bank, Customer, Vendor" and Comments error on Tax Configuration Opening required on Payment and Receipt.
                    str = "Select coa_detail_id, detail_code, detail_title From vwCOADetail WHERE Active=1 AND account_type NOT IN ('Cash', 'Bank', 'Vendor', 'Customer')"
                ElseIf Condition = "SubSubAccount" Then
                    str = "Select DISTINCT main_sub_sub_id as coa_detail_id, sub_sub_code as detail_code, sub_sub_title as detail_title From vwCOADetail WHERE Active=1 AND Sub_sub_title <> ''"
                End If
                Me.grdAddAccount.DataSource = GetDataTable(str)
                AccountType = String.Empty
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmAddAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If IsTaxForm = True Then
                GetAllTaxRecord(AccountType)
            Else
                GetAllRecord(AccountType)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdAddAccount_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAddAccount.DoubleClick
        Try
            If grdAddAccount.RowCount = 0 Then Exit Sub
            DialogResult = Windows.Forms.DialogResult.OK
            AccountId = Me.grdAddAccount.GetRow.Cells(0).Value
            AccountDesc = Me.grdAddAccount.GetRow.Cells(2).Text
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmAddAccount_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class