Imports SBDal
Imports SBModel
Imports Janus
Imports System.Data.SqlClient

Public Class frmReconciliation
    Implements IGeneral
    Dim objModel As New BankReconciliationBE
    Dim objDAL As New BankReconciliationDAL
    Public DrillDown As Boolean = False
    Dim vlist As List(Of VouchersDetail)
    Dim Key As String = String.Empty
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Set Indexing for Detail grid
    ''' </summary>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Enum grdDetail
        Selector
        StatementId
        BankId
        BankCode
        BankName
        VoucherType
        Source
        VoucherId
        VoucherNo
        VoucherDate
        VoucherDetailId
        ChequeNo
        ChequeDate
        Debit
        Credit
        Balance
        Comments
        ReconciliationDate
        ReconciliationStatus
    End Enum
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Apply grid settings to set columns hide/show and also to implement column totals
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                If c.Index <> grdDetail.ReconciliationStatus AndAlso c.Index <> grdDetail.ReconciliationDate Then
                    c.FilterEditType = Windows.GridEX.EditType.TextBox
                    c.EditType = Windows.GridEX.EditType.NoEdit
                End If
            Next

            Me.grd.RootTable.Columns(grdDetail.Selector).BoundMode = Windows.GridEX.ColumnBoundMode.UnboundFetch
            Me.grd.RootTable.Columns(grdDetail.Selector).EditType = Windows.GridEX.EditType.CheckBox
            Me.grd.RootTable.Columns(grdDetail.Selector).ActAsSelector = True
            Me.grd.RootTable.Columns(grdDetail.Selector).AllowDrag = False
            Me.grd.RootTable.Columns(grdDetail.Selector).ColumnType = Windows.GridEX.ColumnType.CheckBox
            Me.grd.RootTable.Columns(grdDetail.Selector).Width = 20

            Me.grd.RootTable.Columns(grdDetail.VoucherNo).ColumnType = Windows.GridEX.ColumnType.Link

            Me.grd.RootTable.Columns(grdDetail.Debit).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.Debit).TotalFormatString = "N"
            Me.grd.RootTable.Columns(grdDetail.Debit).TextAlignment = Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.Debit).HeaderAlignment = Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.Debit).AggregateFunction = Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns(grdDetail.Credit).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.Credit).TotalFormatString = "N"
            Me.grd.RootTable.Columns(grdDetail.Credit).TextAlignment = Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.Credit).HeaderAlignment = Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.Credit).AggregateFunction = Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns(grdDetail.Balance).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.Balance).TotalFormatString = "N"
            Me.grd.RootTable.Columns(grdDetail.Balance).TextAlignment = Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.Balance).HeaderAlignment = Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.Balance).AggregateFunction = Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns(grdDetail.StatementId).Visible = False
            Me.grd.RootTable.Columns(grdDetail.BankId).Visible = False
            Me.grd.RootTable.Columns(grdDetail.VoucherId).Visible = False
            Me.grd.RootTable.Columns(grdDetail.VoucherDetailId).Visible = False
            Me.grd.RootTable.Columns(grdDetail.BankCode).Visible = False
            Me.grd.RootTable.Columns(grdDetail.BankName).Visible = False
            Me.grd.RootTable.Columns(grdDetail.ReconciliationStatus).Visible = False
            Me.grd.RootTable.Columns(grdDetail.Source).Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnEdit.Enabled = True
                Me.btnUnReconcile.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            Me.btnUnReconcile.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Reconcile" Then
                    If Me.btnSave.Text = "Reconcile" Then Me.btnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "UnReconcile" Then
                    If Me.btnUnReconcile.Text = "UnReconcile" Then Me.btnUnReconcile.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.btnDelete.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Delete the Statement
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New BankReconciliationDAL
            FillModel()
            If Me.grd.GetCheckedRows.Length > 0 Then
                If objDAL.Delete(vlist, objModel) = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Fillcombos to show the Bank accounts in dropdown
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Bank" Then
                str = "SELECT coa_detail_id, detail_title FROM vwCOADetail WHERE account_type = 'Bank' And Active = 1"
                FillDropDown(Me.cmbBankAccount, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Fillmodel to fill all values in controls
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New BankReconciliationBE
            objModel.StatementId = Me.lblStatementId.Text
            objModel.BankId = Me.cmbBankAccount.SelectedValue
            objModel.StatementDate = Me.dtpStatementDate.Value
            objModel.StatementTitle = Me.txtStatementTitle.Text
            objModel.EndingBalance = Me.txtEndingBalance.Text
            vlist = New List(Of VouchersDetail)
            Dim voucher As VouchersDetail
            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            For Each row As Janus.Windows.GridEX.GridEXRow In grd.GetCheckedRows
                voucher = New VouchersDetail
                voucher.VoucherDetailId = Val(row.Cells(grdDetail.VoucherDetailId).Value.ToString)
                voucher.VoucherId = Val(row.Cells(grdDetail.VoucherId).Value.ToString)
                voucher.Cheque_Status = row.Cells(grdDetail.Selector).Value.ToString
                If voucher.Cheque_Status = True Then
                    If IsDBNull(row.Cells(grdDetail.ReconciliationDate).Value) Then
                        If Not IsDBNull(row.Cells(grdDetail.ChequeDate).Value) Then
                            voucher.Cheque_Clearance_Date = row.Cells(grdDetail.ChequeDate).Value
                        Else
                            If Not IsDBNull(row.Cells(grdDetail.VoucherDate).Value) Then
                                voucher.Cheque_Clearance_Date = row.Cells(grdDetail.VoucherDate).Value
                            Else
                                voucher.Cheque_Clearance_Date = Date.MinValue
                            End If
                        End If
                    Else
                        voucher.Cheque_Clearance_Date = CDate(row.Cells(grdDetail.ReconciliationDate).Value)
                    End If
                Else
                    voucher.Cheque_Clearance_Date = Date.MinValue
                End If
                vlist.Add(voucher)
                objModel.ReconciledBalance += Me.grd.CurrentRow.Cells(grdDetail.Balance).Value.ToString
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Get reconciled/ unreconciled and history records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Condition = "UnReconciled" Then
                str = "SELECT 0 As Selector, 0 As StatementId, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_code As [A/C Code], vwCOADetail.detail_title [A/C Title], tblDefVoucherType.voucher_type As VoucherType, tblForm.AccessKey As Source, tblVoucher.voucher_id, tblVoucher.voucher_no As VoucherNo, tblVoucher.voucher_date As VoucherDate, tblVoucherDetail.voucher_detail_id, tblVoucherDetail.Cheque_No, tblVoucherDetail.Cheque_Date, tblVoucherDetail.debit_amount As Debit, tblVoucherDetail.credit_amount As Credit, 0 As Balance, tblVoucherDetail.comments As Comments, Convert(DateTime,tblVoucherDetail.Cheque_Clearance_Date,102) As ReconciliationDate, tblVoucherDetail.Cheque_Status As ReconciliationStatus FROM tblVoucher INNER JOIN tblDefVoucherType ON tblVoucher.voucher_type_id = tblDefVoucherType.voucher_type_id INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id INNER JOIN tblForm ON tblVoucher.source = tblForm.Form_Name WHERE tblVoucherDetail.coa_detail_id = " & Me.cmbBankAccount.SelectedValue & " AND tblVoucher.voucher_date <= Convert(Datetime, N'" & Me.dtpStatementDate.Value.ToString("yyyy-M-d 23:59:59") & "', 102) And (vwCOADetail.account_type = 'Bank') AND tblVoucherDetail.Cheque_Status is null or tblVoucherDetail.Cheque_Status =0 "
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                dt.Columns(grdDetail.Balance).Expression = "Debit - Credit"
                ApplyGridSettings()
            ElseIf Condition = "History" Then
                str = "SELECT tblBankStatement.StatementId, tblBankStatement.BankId, tblBankStatement.StatementDate, tblBankStatement.StatementTitle, tblBankStatement.EndingBalance,SUM( tblVoucherDetail.debit_amount) As Debit, SUM(tblVoucherDetail.credit_amount) Credit, SUM( tblVoucherDetail.debit_amount)- SUM(tblVoucherDetail.credit_amount) As Balance, tblBankStatement.StatementStatus FROM tblBankStatement LEFT OUTER JOIN tblVoucherDetail ON tblBankStatement.StatementId = tblVoucherDetail.StatementId GROUP BY tblBankStatement.StatementId, tblBankStatement.BankId, tblBankStatement.StatementDate, tblBankStatement.StatementTitle, tblBankStatement.EndingBalance, tblBankStatement.StatementStatus Order By tblBankStatement.StatementId Desc"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                Me.grdSaved.RootTable.Columns("StatementId").Visible = False
                Me.grdSaved.RootTable.Columns("BankId").Visible = False
                Me.grdSaved.RootTable.Columns("Debit").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("Credit").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("Balance").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("EndingBalance").FormatString = "N" & DecimalPointInValue
                Me.grdSaved.RootTable.Columns("Debit").TextAlignment = Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Credit").TextAlignment = Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Balance").TextAlignment = Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("EndingBalance").TextAlignment = Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Debit").HeaderAlignment = Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Credit").HeaderAlignment = Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("Balance").HeaderAlignment = Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("EndingBalance").HeaderAlignment = Windows.GridEX.TextAlignment.Far
                Me.grdSaved.RootTable.Columns("StatementStatus").ColumnType = Windows.GridEX.ColumnType.CheckBox
            ElseIf Condition = "Reconciled" Then
                str = "SELECT 0 AS Selector, tblBankStatement.StatementId, tblVoucherDetail.coa_detail_id, vwCOADetail.detail_code AS [A/C Code], vwCOADetail.detail_title AS [A/C Title], tblDefVoucherType.voucher_type AS VoucherType, tblForm.AccessKey As Source, tblVoucher.voucher_id, tblVoucher.voucher_no AS VoucherNo, tblVoucher.voucher_date AS VoucherDate, tblVoucherDetail.voucher_detail_id, tblVoucherDetail.Cheque_No, tblVoucherDetail.Cheque_Date, tblVoucherDetail.debit_amount AS Debit, tblVoucherDetail.credit_amount AS Credit, 0 AS Balance, tblVoucherDetail.comments AS Comments, CONVERT(DateTime, tblVoucherDetail.Cheque_Clearance_Date, 102) AS ReconciliationDate, tblVoucherDetail.Cheque_Status AS ReconciliationStatus FROM tblVoucher INNER JOIN tblDefVoucherType ON tblVoucher.voucher_type_id = tblDefVoucherType.voucher_type_id INNER JOIN tblVoucherDetail ON tblVoucher.voucher_id = tblVoucherDetail.voucher_id INNER JOIN vwCOADetail ON tblVoucherDetail.coa_detail_id = vwCOADetail.coa_detail_id INNER JOIN tblBankStatement ON tblVoucherDetail.StatementId = tblBankStatement.StatementId INNER JOIN tblForm ON tblVoucher.source = tblForm.Form_Name WHERE tblVoucherDetail.coa_detail_id = " & Me.cmbBankAccount.SelectedValue & " AND (vwCOADetail.account_type = 'Bank') AND (tblVoucherDetail.Cheque_Status = 1) AND tblBankStatement.StatementId = " & Me.grdSaved.CurrentRow.Cells("StatementId").Value.ToString & ""
                dt = GetDataTable(str)
                Me.grd.DataSource = dt
                Me.grd.RetrieveStructure()
                dt.Columns(grdDetail.Balance).Expression = "Debit - Credit"
                ApplyGridSettings()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Fill all controls in edit mode
    ''' </summary>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Sub EditRecords()
        Try
            Me.lblStatementId.Text = Me.grdSaved.CurrentRow.Cells("StatementId").Value
            Me.cmbBankAccount.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("BankId").Value)
            Me.dtpStatementDate.Value = Me.grdSaved.CurrentRow.Cells("StatementDate").Value.ToString
            Me.txtStatementTitle.Text = Me.grdSaved.CurrentRow.Cells("StatementTitle").Value.ToString
            Me.txtEndingBalance.Text = Me.grdSaved.CurrentRow.Cells("EndingBalance").Value.ToString
            Me.txtReconciledBalance.Text = Me.grdSaved.CurrentRow.Cells("Balance").Value.ToString
            GetAllRecords("Reconciled")
            If Val(Me.txtEndingBalance.Text) = Val(Me.txtReconciledBalance.Text) Then
                btnProcess.Text = "Complete Reconcile"
                Me.btnSave.Enabled = False
            End If
            Me.btnUnReconcile.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Validate all controls before save and update
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbBankAccount.SelectedValue = 0 Then
                msg_Information("Please select any Bank account name")
                Return False
            End If
            If Me.txtStatementTitle.Text = "" Then
                msg_Information("Please enter the valid Title of statement")
                Return False
            End If
            If Me.txtEndingBalance.Text = 0 Or Me.txtEndingBalance.Text = "" Then
                msg_Information("Please enter the Ending balance")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Reset all controls to default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbBankAccount.SelectedValue = 0
            Me.dtpStatementDate.Value = Now
            Me.txtStatementTitle.Text = ""
            Me.txtEndingBalance.Text = Math.Round(0, DecimalPointInValue)
            Me.txtReconciledBalance.Text = Math.Round(0, DecimalPointInValue)
            Me.btnDelete.Visible = False
            Me.btnSave.Enabled = True
            Me.btnUnReconcile.Visible = False
            Me.btnProcess.Text = "Process"
            Me.cmbLoadRecords.Text = "Un Reconciled"
            Me.UltraTabControl1.Tabs(0).Selected = True
            FillCombos("Bank")
            Me.grd.DataSource = (Nothing)
            GetAllRecords("History")
            ApplySecurity(SBUtility.Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Save Statement and also update the voucher cheque status
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New BankReconciliationDAL
            Me.Cursor = Cursors.WaitCursor
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If IsValidate() = True Then
                objDAL.UpdateChequeStatus(vlist, objModel)
                ReSetControls()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If IsValidate() = True Then

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    ''' <summary>
    ''' TSK1162 : Ali Faisal : keydown to shortly perform the button events
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub frmReconciliation_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If Me.btnSave.Enabled = True Then
                    btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                btnNew_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Delete Then
                If Me.btnDelete.Enabled = True Then
                    btnDelete_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Form load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub frmReconciliation_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
            Me.SplitContainer1.Panel1Collapsed = True
            Me.cmbBankAccount.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Reset controls on New button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Save
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If Me.btnSave.Text = "Reconcile" Then
                If Save() = True Then
                    ReSetControls()
                End If
            Else
                If Update1() = True Then
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Refresh controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0
            id = Me.cmbBankAccount.SelectedValue
            FillCombos("Bank")
            Me.cmbBankAccount.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Delete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If Delete() = True Then
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Edit button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
            Me.btnDelete.Visible = True
            Me.btnSave.Enabled = False
            Me.cmbLoadRecords.Text = "Reconciled"
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Control grid bar load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Bank Reconciliation"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Process button click event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        Try
            If IsValidate() = True Then
                If objDAL.ValidateStatus(Me.cmbBankAccount.SelectedValue) = False Then
                    objDAL.Save(objModel)
                    GetAllRecords("History")
                End If
                If Me.cmbLoadRecords.Text = "Un Reconciled" Then
                    GetAllRecords("UnReconciled")
                    Me.btnSave.Visible = True
                    Me.btnUnReconcile.Visible = False
                Else
                    GetAllRecords("Reconciled")
                    Me.btnSave.Visible = False
                    Me.btnUnReconcile.Visible = True
                End If
                If Me.btnProcess.Text = "Complete Reconcile" Then
                    If objDAL.Update(objModel) = True Then
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : History grid double click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Bank Index change
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub cmbBankAccount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBankAccount.SelectedIndexChanged
        Try
            If Me.cmbBankAccount.SelectedValue > 0 Then
                For Each row As Janus.Windows.GridEX.GridEXRow In grdSaved.GetDataRows
                    If Me.cmbBankAccount.SelectedValue = row.Cells("BankId").Value AndAlso row.Cells("StatementStatus").Value = False Then
                        Me.lblStatementId.Text = row.Cells("StatementId").Value
                        Me.dtpStatementDate.Value = row.Cells("StatementDate").Value
                        Me.txtStatementTitle.Text = row.Cells("StatementTitle").Value.ToString
                        Me.txtEndingBalance.Text = row.Cells("EndingBalance").Value.ToString
                        Me.txtReconciledBalance.Text = row.Cells("Balance").Value.ToString
                        If Val(Me.txtEndingBalance.Text) = Val(Me.txtReconciledBalance.Text) Then
                            btnProcess.Text = "Complete Reconcile"
                            Me.btnSave.Enabled = False
                            Me.btnSave.Visible = True
                        End If
                        GetAllRecords("UnReconciled")
                        Exit Sub
                    End If
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lnkOpenLedger_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkOpenLedger.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.cmbBankAccount.SelectedValue = 0 Then
                ShowErrorMessage("Please select any account")
                Me.cmbBankAccount.Focus()
                Exit Sub
            End If
            rptLedger.CoaDetailId = Me.cmbBankAccount.SelectedValue
            rptLedger.dptFromDate = Me.dtpStatementDate.Value.AddMonths(-1).ToString("yyyy-M-d 00:00:00")
            rptLedger.dptToDate = Me.dtpStatementDate.Value.ToString("yyyy-M-d 23:59:59")
            DrillDown = True
            frmMain.LoadControl("rptLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub VoucherEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VoucherEntryToolStripMenuItem.Click
        Try
            frmMain.LoadControl("frmVoucher")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub PaymentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PaymentToolStripMenuItem.Click
        Try
            frmMain.LoadControl("VendorPayments")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ReceiptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReceiptToolStripMenuItem.Click
        Try
            frmMain.LoadControl("CustomerCollection")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub ExpenseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExpenseToolStripMenuItem.Click
        Try
            frmMain.LoadControl("frmExpense")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub OpenLedgerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenLedgerToolStripMenuItem.Click
        Try
            frmMain.LoadControl("rptLedger")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnUnReconcile_Click(sender As Object, e As EventArgs) Handles btnUnReconcile.Click
        Try
            objDAL = New BankReconciliationDAL
            If IsValidate() = True Then
                objDAL.UpdateVDetail(vlist, objModel)
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' TSK1162 : Ali Faisal : Open voucher screens on voucher no click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>TSK1162 : Ali Faisal : 02-August-2017</remarks>
    Private Sub grd_LinkClicked(sender As Object, e As Windows.GridEX.ColumnActionEventArgs) Handles grd.LinkClicked
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            frmModProperty.Tags = String.Empty
            If Me.cmbBankAccount.SelectedValue = 0 Then
                ShowErrorMessage("Please select any account")
                Me.cmbBankAccount.Focus()
                Exit Sub
            End If
            If e.Column.Key = "VoucherNo" Then
                frmModProperty.Tags = Me.grd.GetRow.Cells("VoucherNo").Text
                If IsDrillDown = True Then
                    frmMain.LoadControl(Me.grd.GetRow.Cells("source").Text.ToString)
                    System.Threading.Thread.Sleep(500)
                Else
                    frmMain.LoadControl(Me.grd.GetRow.Cells("source").Value.ToString)
                    System.Threading.Thread.Sleep(500)
                    frmModProperty.Tags = Me.grd.GetRow.Cells("VoucherNo").Text
                    frmMain.LoadControl(Me.grd.GetRow.Cells("source").Value.ToString)
                    System.Threading.Thread.Sleep(500)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BackToOld_Click(sender As Object, e As EventArgs) Handles BackToOld.Click
        Try
            SaveConfiguration("NewReconcilationForm", "False")
            frmMain.LoadControl("frmBankReconciliation")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function SaveConfiguration(ByVal KeyType As String, ByVal KeyValue As String, Optional ByVal CreateNewKey As Boolean = False) As Boolean
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Dim Cmd As New SqlCommand
        Cmd.Connection = Con
        Cmd.Transaction = trans
        Try
            Dim strSQL As String = String.Empty

            If CreateNewKey = True Then

                strSQL = "IF NOT EXISTS(SELECT * From ConfigValuesTable  WHERE Config_Type='" & KeyType & "' ) " _
                        & " Insert into ConfigValuesTable(config_id, config_Type, config_Value, Comments, IsActive) Select Max(config_id) + 1, '" & KeyType & "', '" & KeyValue & "', '', 1 from ConfigValuesTable " _
                        & "Else " _
                        & " UPDATE ConfigValuesTable SET Config_Value=N'" & KeyValue & "' WHERE Config_Type=N'" & KeyType & "'"

            Else

                strSQL = "UPDATE ConfigValuesTable SET Config_Value=N'" & KeyValue & "' WHERE Config_Type=N'" & KeyType & "'"

            End If

            Cmd.CommandType = CommandType.Text
            Cmd.CommandText = strSQL
            Cmd.ExecuteNonQuery()
            trans.Commit()

            SaveActivityLog("Configuration", Me.Text.ToString, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, KeyType, , , , , Me.Name.ToString)
            Key = KeyType
            Dim config As ConfigSystem = objConfigValueList.Find(AddressOf GetObj)

            objConfigValueList.Remove(config)
            Dim AddConfig As New ConfigSystem
            AddConfig.Config_Type = KeyType.ToString
            AddConfig.Config_Value = KeyValue.ToString
            If config IsNot Nothing Then
                If config.Comments IsNot Nothing Then
                    AddConfig.Comments = config.Comments
                Else
                    AddConfig.Comments = Nothing
                End If
                AddConfig.IsActive = config.IsActive
            End If

            objConfigValueList.Add(AddConfig)
            Key = String.Empty
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetObj(ByVal Config As ConfigSystem) As Boolean
        Try
            If Config.Config_Type = Key Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class