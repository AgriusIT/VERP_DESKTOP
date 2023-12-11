''TFS4431 : Ayesha Rehman : 07-09-2018 Configure the approval hierarchy of invoice transfer.
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class frmSalesTransfer

    ''TFS4431 : Ayesha Rehman : This Variable is Added to check ApprovalProcessId ,if it is mapped against the document
    Dim ApprovalProcessId As Integer = 0
    ''TFS4431 : Ayesha Rehman :End
    Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "InvoiceList" Then
                FillUltraDropDown(Me.cmbInvoiceList, "Select SalesID, SalesNo + '~' + coa.detail_title as [Inv No], SalesDate as [Inv Date], coa.Account_Type as [Type], coa.detail_title as [Customer],coa.coa_detail_id, SalesNo From SalesMasterTable INNER JOIN vwCOADetail coa on coa.coa_detail_id = SalesMastertable.CustomerCode Where SalesMastertable.Post = 1 ORDER BY 1 DESC")
                Me.cmbInvoiceList.Rows(0).Activate()
                If Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns("SalesID").Hidden = True
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns("SalesNo").Hidden = True
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns("Customer").Width = 200
                End If
            ElseIf Condition = "ReplaceCustomer" Then
                FillUltraDropDown(Me.cmbReplaceCustomer, "Select coa_detail_id,detail_title as [Customer], detail_code as [Account Code], Contact_Mobile as [Mobile], Contact_Email as Emil, Account_Type as [Type] From vwCOADetail WHERE coa_detail_id <> " & Val(Me.cmbInvoiceList.ActiveRow.Cells("coa_detail_id").Value.ToString) & " AND Account_Type in('Customer','Vendor') ORDER BY detail_title ASC")
                Me.cmbReplaceCustomer.Rows(0).Activate()
                Me.cmbReplaceCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                Me.cmbReplaceCustomer.DisplayLayout.Bands(0).Columns("Customer").Width = 200
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmSalesTransfer_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            If Not getConfigValueByType("SalesInvoiceTranferApproval") = "Error" Then
                ApprovalProcessId = Val(getConfigValueByType("SalesInvoiceTranferApproval"))
            End If
            FillCombo("InvoiceList")
            FillCombo("ReplaceCustomer")
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbInvoiceList_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbInvoiceList.RowSelected
        Try
            If Me.cmbInvoiceList.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbInvoiceList.IsItemInList = False Then Exit Sub
            FillCombo("ReplaceCustomer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            frmSalesTransfer_Shown(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click

        If Me.cmbInvoiceList.ActiveRow Is Nothing Then
            ShowErrorMessage("Please select invoice.")
            Me.cmbInvoiceList.Focus()
            Exit Sub
        End If
        If Me.cmbInvoiceList.Value <= 0 Then
            ShowErrorMessage("Please select invoice.")
            Me.cmbInvoiceList.Focus()
            Exit Sub
        End If
        If Me.cmbReplaceCustomer.Value <= 0 Then
            ShowErrorMessage("Please select customer.")
            Me.cmbReplaceCustomer.Focus()
            Exit Sub
        End If

        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDbCommand
        Try


            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            ''Start TFS4431
            ''insert Approval Log
            If ApprovalProcessId > 0 Then

                ' If ValidateApprovalProcessMapped(Me.cmbInvoiceList.ActiveRow.Cells("SalesNo").Value, Me.Name) Then
                'If ValidateApprovalProcessIsInProgressAgain(Me.cmbInvoiceList.ActiveRow.Cells("SalesNo").Value, Me.Name) = False Then
                cmd.CommandText = ""
                cmd.CommandText = "Update SalesMasterTable Set CustomerCode=" & Me.cmbReplaceCustomer.Value & ",Change_Customer_Code=" & Me.cmbInvoiceList.ActiveRow.Cells("coa_detail_id").Value & " , Post = 0 WHERE SalesID=" & Me.cmbInvoiceList.Value & ""
                cmd.ExecuteNonQuery()
                cmd.CommandText = ""
                cmd.CommandText = "Update tblVoucherDetail Set coa_detail_id=" & Val(Me.cmbReplaceCustomer.ActiveRow.Cells("coa_detail_id").Value.ToString) & " WHERE voucher_id in(select voucher_id from tblVoucher Where Voucher_No='" & Me.cmbInvoiceList.ActiveRow.Cells("SalesNo").Value.ToString & "') AND coa_detail_id=" & Val(Me.cmbInvoiceList.ActiveRow.Cells("coa_detail_id").Value.ToString) & ""
                cmd.ExecuteNonQuery()
                SaveApprovalLog(EnumReferenceType.SalesInvoiceTransfer, Me.cmbInvoiceList.ActiveRow.Cells("SalesID").Value, Me.cmbInvoiceList.ActiveRow.Cells("SalesNo").Value, Date.Now, "Sales Invoice Transfer ," & cmbInvoiceList.Text & "", Me.Name, 7)
                'End If
                'End If
            Else
            cmd.CommandText = ""
            cmd.CommandText = "Update SalesMasterTable Set CustomerCode=" & Me.cmbReplaceCustomer.Value & ",Change_Customer_Code=" & Me.cmbInvoiceList.ActiveRow.Cells("coa_detail_id").Value & " WHERE SalesID=" & Me.cmbInvoiceList.Value & ""
                cmd.ExecuteNonQuery()
                cmd.CommandText = ""
            cmd.CommandText = "Update tblVoucherDetail Set coa_detail_id=" & Val(Me.cmbReplaceCustomer.ActiveRow.Cells("coa_detail_id").Value.ToString) & " WHERE voucher_id in(select voucher_id from tblVoucher Where Voucher_No='" & Me.cmbInvoiceList.ActiveRow.Cells("SalesNo").Value.ToString & "') AND coa_detail_id=" & Val(Me.cmbInvoiceList.ActiveRow.Cells("coa_detail_id").Value.ToString) & ""
            cmd.ExecuteNonQuery()
            End If
            ''End TFS4431


            trans.Commit()

            ShowInformationMessage(str_informUpdate)
            FillGrid()
            FillCombo("InvoiceList")
            FillCombo("ReplaceCustomer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Sub FillGrid()
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select SalesID, SalesNo as [Inv No], SalesDate as [Inv Date], coa.detail_title as [Changed Customer], coa1.detail_title as [Replace Customer] From SalesMasterTable INNER JOIN vwCOADetail coa on coa.coa_detail_id = SalesMasterTable.CustomerCode INNER JOIN vwCOADetail coa1 on coa1.coa_detail_id = SalesMasterTable.Change_Customer_Code ORDER BY 1 DESC"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Me.GridEX1.DataSource = dt
            GridEX1.RetrieveStructure()
            Me.GridEX1.RootTable.Columns(0).Visible = False
            Me.GridEX1.RootTable.Columns("Inv Date").FormatString = "dd/MMM/yyyy"
            Me.GridEX1.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class