'28-Mar-2014 TASK:2526 Imrana3-bank reconciliation statement
''01-Apr-2014 TASK:2534 Imran Ali Automobile Development/Problem
'' 24-11-2015 TASKTFS63 Muhammad Ameen: Clearance date in grid should be able to be edited.
Imports SBModel
Imports SBDal
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports Janus
Imports System.IO
Imports System.Net
Public Class frmBankReconciliation

    Implements IGeneral

    'Private COAList As List(Of COABE)
    Dim vlist As List(Of VouchersDetail)
    '' TASKTFS63
    Dim Voucher As VouchersMaster
    Dim Key As String = String.Empty
    Enum GridDetailEnum
        DetailCode
        DetailTitle
        VoucherDetailId
        VoucherId
        VoucherType
        VoucherNo
        VoucherDate
        ChequeNo
        ChequeDate
        Comments
        Debit
        Credit
        ClearanceDate
        ChequeStatus
        CostCenterId
        LocationId
        ChequeDescription
        CurrencyDebit
        CurrencyCredit
    End Enum

    Private Sub frmBankReconciliation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F4 Then
            btnSave_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub frmBankReconciliation_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Application.DoEvents()
            'COAList = New COABL().COAList
            FillCombos("bank")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage("An error occurred while loading record" & Chr(10) & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub
    Private Sub frmBankReconciliation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            For Each c As Janus.Windows.GridEX.GridEXColumn In Me.GridEX1.RootTable.Columns
                If c.Index <> GridDetailEnum.ChequeStatus AndAlso c.Index <> GridDetailEnum.ClearanceDate Then ''TASKTFS63
                    c.FilterEditType = Windows.GridEX.EditType.TextBox 'Task:2534 #TDO Filter Record
                    c.EditType = Windows.GridEX.EditType.NoEdit 'Task:2534 #TODO Readonly Rows
                End If

            Next
            If (Me.chkCredit.Checked = True Or Me.chkUnCredited.Checked = True) Then
                Me.GridEX1.RootTable.Columns("Debit").Visible = True
                Me.GridEX1.RootTable.Columns("Credit").Visible = False
            End If
            If (Me.chkPresented.Checked = True Or Me.chkUnPresented.Checked = True) Then
                Me.GridEX1.RootTable.Columns("Debit").Visible = False
                Me.GridEX1.RootTable.Columns("Credit").Visible = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                ToolStripButton1.Enabled = True
                'Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    ToolStripButton1.Enabled = False
                    'Me.btnSearchDelete.Enabled = False
                    'Me.btnSearchPrint.Enabled = False
                    'Me.btnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnSearchDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        'Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        'Me.btnSearchPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                'GetSecurityByPostingUser(UserPostingRights, Me.SaveToolStripButton, Me.DeleteToolStripButton)
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                ToolStripButton1.Enabled = False
                'Me.btnSearchDelete.Enabled = False
                'Me.btnSearchPrint.Enabled = False
                'Me.btnPrint.Enabled = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Cancel" Then
                        ToolStripButton1.Enabled = True
                        ' Me.btnSearchDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        ' Me.btnPrint.Enabled = True
                        'Me.btnSearchPrint.Enabled = True
                        'CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                        'ElseIf Rights.Item(i).FormControlName = "Post" Then
                        '    Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "bank" Then
                'Me.cmbBankAc.ValueMember = "DetailAcId"
                'Me.cmbBankAc.DisplayMember = "DetailAcName"
                'Dim COACashAcList As List(Of COABE) = COAList.FindAll(AddressOf FilterCOAByAccountType)
                'Dim AddZeroValueInCOA As New COABE
                'AddZeroValueInCOA.DetailAcId = 0
                'AddZeroValueInCOA.DetailAcName = "... Select Any Value ..."
                'COACashAcList.Insert(0, AddZeroValueInCOA)
                'Me.cmbBankAc.DataSource = COACashAcList
                FillDropDown(Me.cmbBankAc, "Select coa_detail_id, detail_title, detail_code From vwcoadetail where account_type='Bank'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Me.GridEX1.UpdateData()
            vlist = New List(Of VouchersDetail)
            Dim voucher As VouchersDetail
            'Dim dt As DataTable = CType(Me.GridEX1.DataSource, DataTable)
            'If dt.GetChanges Is Nothing Then Exit Sub
            'For Each row As DataRow In dt.GetChanges.Rows
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetCheckedRows
                voucher = New VouchersDetail
                voucher.VoucherDetailId = Val(row.Cells("Voucher_Detail_Id").Value.ToString)
                voucher.VoucherId = Val(row.Cells("Voucher_Id").ToString)
                voucher.Cheque_Status = row.Cells("Cheque_Status").Value
                ''Below 7 lines are commented against TASK TFS3922 ON 18-07-2018
                'If voucher.Cheque_Status = True Then
                '    If Not IsDBNull(row.Cells("Clearance Date")) Then ''TASKTFS63
                '        voucher.Cheque_Clearance_Date = CDate(row.Cells("Clearance Date").Value)
                '    End If
                'Else
                '    voucher.Cheque_Clearance_Date = Date.MinValue
                'End If
                If voucher.Cheque_Status = True Then
                    voucher.Cheque_Status = False
                    voucher.Cheque_Clearance_Date = Date.MinValue
                Else
                    voucher.Cheque_Status = True
                    If Not IsDBNull(row.Cells("Clearance Date").Value) Then ''TASKTFS63
                        voucher.Cheque_Clearance_Date = CDate(row.Cells("Clearance Date").Value)
                    Else
                        If msg_Confirm("Clearance date is not selected. Do you want to update status without clearance date?") = False Then
                            voucher.Cheque_Status = False
                            voucher.Cheque_Clearance_Date = Date.MinValue
                        End If
                    End If
                    'voucher.Cheque_Clearance_Date = Date.MinValue
                End If
                vlist.Add(voucher)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Me.cmbBankAc.SelectedIndex = -1 Then Exit Sub
            'Aashir Added Validation for bank account combobox
            If Me.cmbBankAc.SelectedValue = 0 Then ShowErrorMessage("Please Select Bank Account")
            Dim dt As DataTable
            'dt = New VouchersDAL().GetAllRec(Me.cmbBankAc.SelectedValue, Me.chkUnPresented.Checked)
            'Dim strSQL As String = "SELECT detail_code, detail_title, voucher_detail_id, voucher_id, voucher_type, voucher_no, voucher_date,  Cheque_No, Cheque_Date,  comments,  " _
            '    & " debit_amount AS Debit, credit_amount AS Credit, Convert(DateTime,Cheque_Clearance_Date, 102) As [Clearance Date], IsNull(Cheque_Status,0) as Cheque_Status " _
            '    & " FROM (SELECT VT.voucher_type, V_D.voucher_detail_id, V_D.voucher_id, V_D.coa_detail_id, V.voucher_no, V.voucher_date, COA.detail_code, COA.detail_title,  " _
            '    & " V_D.comments, V_D.Cheque_No, V_D.Cheque_Date, Isnull(V_D.Cheque_Status,0) as Cheque_Status, V_D.Cheque_Clearance_Date, V_D.debit_amount, V_D.credit_amount " _
            '    & " FROM dbo.tblVoucher AS V INNER JOIN dbo.tblVoucherDetail AS V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
            '    & " dbo.vwCOADetail AS COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
            '    & " dbo.tblDefVoucherType AS VT ON V.voucher_type_id = VT.voucher_type_id " _
            '    & " WHERE ((VT.voucher_type = 'BPV') OR(VT.voucher_type = 'BRV' OR VT.voucher_type = 'CPV' OR VT.voucher_type = 'CRV' OR VT.voucher_type = 'JV')) AND  COA.account_type <> 'Bank' and V.Voucher_Id IN(Select DISTINCT tblVoucher.Voucher_Id From tblVoucherDetail INNER JOIN tblVoucher ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id INNER JOIN vwCOADetail On vwCOADetail.coa_detail_id = tblVoucherdetail.coa_detail_id WHERE tblVoucherDetail.coa_detail_id = " & Me.cmbBankAc.SelectedValue & "  and account_type='Bank')) AS a WHERE a.coa_detail_id <> 0"
            'If (Me.chkUnPresented.Checked = True Or Me.chkPresented.Checked = True) Then
            '    strSQL += " AND a.credit_amount <> 0 And (a.Voucher_Type='BRV' OR a.Voucher_Type='CPV' OR a.Voucher_type = 'JV')"
            'End If
            'If (Me.chkCredit.Checked = True Or Me.chkUnCredited.Checked = True) Then
            '    strSQL += " AND a.debit_amount <> 0 And (a.Voucher_Type='BPV' OR a.Voucher_Type='CRV' OR a.Voucher_type = 'JV')"
            'End If
            'If Me.dtpFromDate.Checked = True Then
            '    strSQL += " AND (Convert(Varchar, a.voucher_date,102) >= Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))"
            'End If
            'If Me.dtpFromDate.Checked = True Then
            '    strSQL += " AND (Convert(Varchar, a.voucher_date,102) <= Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102))"
            'End If
            'If (Me.chkCredit.Checked = True Or Me.chkPresented.Checked = True) Then
            '    strSQL += " AND IsNull(a.Cheque_Status,0)=1"
            'End If
            'If (Me.chkUnCredited.Checked = True Or Me.chkUnPresented.Checked = True) Then
            '    strSQL += " AND IsNULL(a.Cheque_Status,0)=0"
            'End If
            'CostCenterId, LocationId, ChequeDescription, CurrencyDebit, CurrencyCredit 
            'Ali Faisal : TFS# 927 Start : Modified query to exclude the opening vouchers from the Bank Reconciliation on 13-June-2017

            Dim strSQL As String = "SELECT detail_code, detail_title, voucher_detail_id, voucher_id, voucher_type, voucher_no, voucher_date,  Cheque_No, Cheque_Date,  comments,  " _
                & " debit_amount AS Debit, credit_amount AS Credit, Convert(DateTime,Cheque_Clearance_Date, 102) As [Clearance Date], IsNull(Cheque_Status,0) as Cheque_Status, CostCenterId, LocationId, ChequeDescription, CurrencyDebit, CurrencyCredit, coa_detail_id AS COADetailId, ContraCOADetailId, EmployeeId, VoucherNo  " _
                & " FROM (SELECT VT.voucher_type, V_D.voucher_detail_id, V_D.voucher_id, V_D.coa_detail_id, V.voucher_no, V.voucher_date, COA.detail_code, COA.detail_title,  " _
                & " V_D.comments, V_D.Cheque_No, V_D.Cheque_Date, Isnull(V_D.Cheque_Status,0) as Cheque_Status, V_D.Cheque_Clearance_Date, V_D.debit_amount, V_D.credit_amount " _
                & "  , IsNull(V_D.CostCenterID, 0) As CostCenterId, IsNull(V_D.location_id, 0) As LocationId, V_D.ChequeDescription, IsNull(V_D.Currency_debit_amount, 0) AS CurrencyDebit, IsNull(V_D.Currency_Credit_Amount, 0) AS CurrencyCredit, IsNull(V_D.contra_coa_detail_id, 0) AS ContraCOADetailId, IsNull(V_D.EmpId, 0) AS EmployeeId, V.voucher_no AS VoucherNo FROM dbo.tblVoucher AS V INNER JOIN dbo.tblVoucherDetail AS V_D ON V.voucher_id = V_D.voucher_id INNER JOIN " _
                & " dbo.vwCOADetail AS COA ON V_D.coa_detail_id = COA.coa_detail_id INNER JOIN " _
                & " dbo.tblDefVoucherType AS VT ON V.voucher_type_id = VT.voucher_type_id " _
                & " WHERE ((VT.voucher_type = 'BPV') OR(VT.voucher_type = 'BRV' OR VT.voucher_type = 'CPV' OR VT.voucher_type = 'CRV' OR VT.voucher_type = 'JV')) AND  COA.account_type <> 'Bank'  and V.Voucher_Id IN(Select DISTINCT tblVoucher.Voucher_Id From tblVoucherDetail INNER JOIN tblVoucher ON tblVoucher.Voucher_Id = tblVoucherDetail.Voucher_Id INNER JOIN vwCOADetail On vwCOADetail.coa_detail_id = tblVoucherdetail.coa_detail_id WHERE tblVoucherDetail.coa_detail_id = " & Me.cmbBankAc.SelectedValue & "  and account_type='Bank') and V_D.voucher_detail_id NOT IN (Select DISTINCT ISNULL(tblVoucherDetail.ReversalVoucherDetailId, 0) AS ReversalVoucherDetailId From tblVoucherDetail WHERE ISNULL(tblVoucherDetail.ReversalVoucherDetailId, 0) > 0)) AS a WHERE a.coa_detail_id <> 0"
            If (Me.chkUnPresented.Checked = True Or Me.chkPresented.Checked = True) Then
                strSQL += " AND a.credit_amount <> 0 And (a.Voucher_Type='BRV' OR a.Voucher_Type='CPV' OR a.Voucher_type = 'JV')"
            End If
            If (Me.chkCredit.Checked = True Or Me.chkUnCredited.Checked = True) Then
                strSQL += " AND a.debit_amount <> 0 And (a.Voucher_Type='BPV' OR a.Voucher_Type='CRV' OR a.Voucher_type = 'JV')"
            End If
            If Me.dtpFromDate.Checked = True Then
                strSQL += " AND (Convert(Varchar, a.voucher_date,102) >= Convert(DateTime, '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "',102))"
            End If
            If Me.dtpFromDate.Checked = True Then
                strSQL += " AND (Convert(Varchar, a.voucher_date,102) <= Convert(DateTime, '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "',102))"
            End If
            If (Me.chkCredit.Checked = True Or Me.chkPresented.Checked = True) Then
                strSQL += " AND IsNull(a.Cheque_Status,0)=1"
            End If
            If (Me.chkUnCredited.Checked = True Or Me.chkUnPresented.Checked = True) Then
                strSQL += " AND IsNULL(a.Cheque_Status,0)=0"
            End If


            strSQL += "  AND (voucher_no <> 'Opening')"

            'Ali Faisal : TFS# 927 End

            dt = GetDataTable(strSQL)
            Dim dv As New DataView
            dt.TableName = "bank"
            dv.Table = dt
            Dim str As String = String.Empty
            str = "Voucher_No <> ''"
            'If Me.cmbBankAc.SelectedIndex > 0 Then
            '    str += "AND ........... = '" & Me.cmbBankAc.Text & "' "
            'End If
            'If Me.dtpFromDate.Checked = True Then
            '    str += " AND Voucher_Date >= '" & Me.dtpFromDate.Value.ToString("yyyy-M-d 00:00:00") & "'"
            'End If
            'If Me.dtpFromDate.Checked = True Then
            '    str += " AND Voucher_Date <= '" & Me.dtpToDate.Value.ToString("yyyy-M-d 23:59:59") & "'"
            'End If
            'If Me.chkUnPresented.Checked = True Then
            '    str += " AND Voucher_Type = 'BRV' AND Cheque_Status = 0 "
            'End If

            'If Me.chkPresented.Checked = True Then
            '    str += " AND Voucher_Type = 'BRV' AND Cheque_Status = 1 "
            'End If

            'If Me.chkUnCredited.Checked = True Then
            '    str += " AND Voucher_Type = 'BPV' AND Cheque_Status = 0 "
            'End If

            'If Me.chkCredited.Checked = True Then
            '    str += " AND Voucher_Type = 'BPV' AND Cheque_Status = 1 "
            'End If

            dv.RowFilter = str.ToString
            Me.GridEX1.DataSource = dv.ToTable
            'Me.GridEX1.RetrieveStructure()
            'Me.GridEX1.RootTable.Columns("Company_Id").Visible = False
            'Me.GridEX1.RootTable.Columns("Voucher_Id").Visible = False
            Me.GridEX1.RootTable.Columns("Voucher_Date").FormatString = "dd/MMM/yyyy"
            Me.GridEX1.RootTable.Columns("Cheque_Date").FormatString = "dd/MMM/yyyy"

            'Me.GridEX1.RootTable.Columns("Cheque_Clearance_Date").EditType = Windows.GridEX.EditType.CalendarDropDown
            Me.GridEX1.AutoSizeColumns()
            ApplyGridSettings()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IsReverseValid(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean
        Try
            If Me.cmbBankAc.SelectedValue < 1 Then
                ShowErrorMessage("Bank is required. Please select a bank.")
                Me.cmbBankAc.Focus()
                Return False
            End If
            FillModelForReverse()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbBankAc.SelectedIndex = 0
            Me.dtpFromDate.Checked = True
            Me.dtpFromDate.Value = Date.Now
            Me.chkUnPresented.Checked = True
            'Me.chkPresented.Checked = False
            Me.chkUnCredited.Checked = False
            'Me.chkCredited.Checked = False
            Me.cmbBankAc.Focus()
            Me.GridEX1.DataSource = (Nothing)
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If New VouchersDAL().UpdateChequeStatus(vlist) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function
    Public Function Reverse(Optional ByVal Condition As String = "") As Boolean
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If New VouchersDAL().Reverse(Voucher) = True Then
                msg_Information("Cancellation has been done successfully.")
                Return True
            Else
                msg_Information("Cancellation failed.")
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    'Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    'End Sub

    'Public Function Update(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    'End Function

    'Public Function FilterCOAByAccountType(ByVal COA As COABE) As Boolean
    '    Try
    '        If COA.Account_Type = "Bank" Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            GetAllRecords()

        Catch ex As Exception
            ShowErrorMessage("An error occured while loading record" & Chr(10) & ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Then
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then
                        DialogResult = System.Windows.Forms.DialogResult.Yes
                        'msg_Information(str_informSave)
                        ReSetControls()
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage("An error occur while loading record" & Chr(10) & ex.Message)
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub chkAllChequeStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllChequeStatus.CheckedChanged
        Try
            If Me.GridEX1.RowCount = 0 Then Exit Sub
            Dim voucher As New VouchersMaster
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetRows
                row.BeginEdit()
                row.Cells("Cheque_Status").Value = Me.chkAllChequeStatus.Checked
                row.EndEdit()
            Next
        Catch ex As Exception
            ShowErrorMessage("An error occured while loading record" & Chr(10) & ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Me.lblProgress.Text = "Processing Pleasae Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            Dim id As Integer = 0
            id = Me.cmbBankAc.SelectedValue
            FillCombos("bank")
            Me.cmbBankAc.SelectedValue = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub GridEX1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles GridEX1.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            If IsReverseValid() = True Then
                If Voucher.VoucherDatail.Count < 1 Then
                    ShowErrorMessage("Please select valid rows.")
                    Exit Sub
                End If
                If Not msg_Confirm("Do you want to cancel selected cheques? ") = True Then Exit Sub
                If Reverse() = True Then
                    DialogResult = System.Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage("An error occur while loading record" & Chr(10) & ex.Message)
        End Try
    End Sub
    Public Sub FillModelForReverse(Optional ByVal Condition As String = "")
        Try
            Me.GridEX1.UpdateData()
            Voucher = New VouchersMaster
            Voucher.VoucherId = 0
            Voucher.BankDescription = ""
            Voucher.VoucherCode = GetVoucherNo()
            Voucher.VoucherNo = Voucher.VoucherCode
            Voucher.VNo = Voucher.VoucherCode
            Voucher.VoucherDate = Now
            Voucher.Cheque_Status = False
            Voucher.ChequeDate = Now
            Voucher.FinancialYearId = 1
            Voucher.LocationId = 1
            Voucher.Post = True
            Voucher.Posted_UserName = LoginUserName
            Voucher.References = String.Empty
            Voucher.Remarks = String.Empty
            Voucher.Source = Me.Name
            Voucher.UserName = LoginUserName
            Voucher.VoucherTypeId = 1
            Voucher.CoaDetailId = Me.cmbBankAc.SelectedValue
            Voucher.VoucherMonth = Now.Month
            Voucher.Reversal = True
            Voucher.ActivityLog = New ActivityLog()
            Voucher.ActivityLog.ApplicationName = "Accounts"
            Voucher.ActivityLog.FormName = Me.Name
            Voucher.ActivityLog.FormCaption = Me.Text
            Voucher.ActivityLog.User_Name = LoginUserName
            Voucher.ActivityLog.UserID = LoginUserId
            Voucher.ActivityLog.Source = Me.Name
            Voucher.ActivityLog.LogComments = String.Empty
            Voucher.ActivityLog.LogDateTime = Now
            Voucher.VoucherDatail = New List(Of VouchersDetail)
            For Each row As Janus.Windows.GridEX.GridEXRow In Me.GridEX1.GetCheckedRows
                'detail_code, detail_title, voucher_detail_id, voucher_id, voucher_type, voucher_no, voucher_date, Cheque_No, Cheque_Date, comments, " _"
                '& " debit_amount AS Debit, credit_amount AS Credit, Convert(DateTime,Cheque_Clearance_Date, 102) As [Clearance Date], IsNull(Cheque_Status,0) as Cheque_Status " _
                ''Debit entry
                If row.Cells("Cheque_No").Value.ToString.Length > 0 Then
                    Dim VoucherDetail As New VouchersDetail
                    VoucherDetail.CoaDetailId = Val(row.Cells("COADetailId").Value.ToString)
                    VoucherDetail.LocationId = Val(row.Cells("LocationId").Value.ToString)
                    VoucherDetail.CostCenter = Val(row.Cells("CostCenterId").Value.ToString)
                    VoucherDetail.VoucherDetailId = Val(row.Cells("Voucher_Detail_Id").Value.ToString)
                    VoucherDetail.VoucherId = Val(row.Cells("Voucher_Id").Value.ToString)
                    VoucherDetail.ReversalVoucherDetailId = Val(row.Cells("Voucher_Detail_Id").Value.ToString)
                    VoucherDetail.Comments = "Reversal of Voucher No " & row.Cells("VoucherNo").Value.ToString & " : " & row.Cells("comments").Value.ToString
                    VoucherDetail.Cheque_No = row.Cells("Cheque_No").Value.ToString
                    If IsDBNull(row.Cells("Cheque_Date").Value) = False Then
                        VoucherDetail.Cheque_Date = row.Cells("Cheque_Date").Value
                    Else
                        VoucherDetail.Cheque_Date = Nothing
                    End If
                    'VoucherDetail.Cheque_Date = row.Cells("Cheque_Date").Value
                    VoucherDetail.DebitAmount = Val(row.Cells("Credit").Value.ToString)
                    VoucherDetail.CreditAmount = Val(row.Cells("Debit").Value.ToString)

                    VoucherDetail.CurrencyDebitAmount = IIf(Val(row.Cells("CurrencyCredit").Value.ToString) > 0, Val(row.Cells("CurrencyCredit").Value.ToString), Val(row.Cells("Credit").Value.ToString))
                    VoucherDetail.CurrencyCreditAmount = IIf(Val(row.Cells("CurrencyDebit").Value.ToString) > 0, Val(row.Cells("CurrencyDebit").Value.ToString), Val(row.Cells("Debit").Value.ToString))
                    If IsDBNull(row.Cells("Clearance Date").Value) = False Then
                        VoucherDetail.Cheque_Clearance_Date = row.Cells("Clearance Date").Value
                    Else
                        VoucherDetail.Cheque_Clearance_Date = Nothing
                    End If
                    'VoucherDetail.Cheque_Clearance_Date = row.Cells("Clearance Date").Value
                    VoucherDetail.Cheque_Status = row.Cells("Cheque_Status").Value
                    VoucherDetail.contra_coa_detail_id = row.Cells("ContraCOADetailId").Value
                    VoucherDetail.EmpId = row.Cells("EmployeeId").Value
                    Voucher.VoucherDatail.Add(VoucherDetail)

                    Dim VoucherDetail1 As New VouchersDetail
                    ''Credit entry
                    VoucherDetail1.CoaDetailId = Me.cmbBankAc.SelectedValue
                    VoucherDetail1.LocationId = Val(row.Cells("LocationId").Value.ToString)
                    VoucherDetail1.CostCenter = Val(row.Cells("CostCenterId").Value.ToString)
                    VoucherDetail1.VoucherDetailId = Val(row.Cells("Voucher_Detail_Id").Value.ToString)
                    VoucherDetail1.VoucherId = Val(row.Cells("Voucher_Id").Value.ToString)
                    VoucherDetail1.ReversalVoucherDetailId = Val(row.Cells("Voucher_Detail_Id").Value.ToString)
                    VoucherDetail1.Comments = "Reversal of Voucher No " & row.Cells("VoucherNo").Value.ToString & " : " & row.Cells("comments").Value.ToString
                    VoucherDetail1.Cheque_No = row.Cells("Cheque_No").Value.ToString
                    If IsDBNull(row.Cells("Cheque_Date").Value) = False Then
                        VoucherDetail.Cheque_Date = row.Cells("Cheque_Date").Value
                    Else
                        VoucherDetail.Cheque_Date = Nothing
                    End If
                    'VoucherDetail1.Cheque_Date = row.Cells("Cheque_Date").Value
                    VoucherDetail1.DebitAmount = Val(row.Cells("Debit").Value.ToString)
                    VoucherDetail1.CreditAmount = Val(row.Cells("Credit").Value.ToString)
                    VoucherDetail1.CurrencyDebitAmount = IIf(Val(row.Cells("CurrencyDebit").Value.ToString) > 0, Val(row.Cells("CurrencyDebit").Value.ToString), Val(row.Cells("Debit").Value.ToString))
                    VoucherDetail1.CurrencyCreditAmount = IIf(Val(row.Cells("CurrencyCredit").Value.ToString) > 0, Val(row.Cells("CurrencyCredit").Value.ToString), Val(row.Cells("Credit").Value.ToString))
                    If IsDBNull(row.Cells("Clearance Date").Value) = False Then
                        VoucherDetail1.Cheque_Clearance_Date = row.Cells("Clearance Date").Value
                    Else
                        VoucherDetail.Cheque_Clearance_Date = Nothing
                    End If
                    VoucherDetail1.Cheque_Status = row.Cells("Cheque_Status").Value
                    VoucherDetail1.contra_coa_detail_id = row.Cells("ContraCOADetailId").Value
                    VoucherDetail1.EmpId = row.Cells("EmployeeId").Value
                    Voucher.VoucherDatail.Add(VoucherDetail1)
                End If
                'CostCenterId()
                'LocationId()
                'ChequeDescription()
                'CurrencyDebit()
                'CurrencyCredit()

                'If voucher.Cheque_Status = True Then
                '    If Not IsDBNull(row.Cells("Clearance Date")) Then ''TASKTFS63
                '        voucher.Cheque_Clearance_Date = CDate(row.Cells("Clearance Date").Value)
                '    End If
                'Else
                '    voucher.Cheque_Clearance_Date = Date.MinValue
                'End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetVoucherNo() As String
        Dim VoucherNo As String = String.Empty
        'If Me.cmbVoucherType.SelectedIndex > 0 Then


        'Task#05082015 if companywise prefix configuration on then company location id attach/concatenate with voucher no
        Dim compWisePrefix As String = String.Empty
        If getConfigValueByType("CompanyWisePrefix").ToString = "True" Then
            compWisePrefix = "JV" & 1
        Else
            compWisePrefix = "JV"
        End If

        If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
            Return GetSerialNo(compWisePrefix.ToString + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "tblVoucher", "voucher_no")
        Else
            Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
            Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
            If Not dr Is Nothing Then
                If dr("config_Value") = "Monthly" Then
                    Return GetNextDocNo(compWisePrefix & "-" & Format(Now, "yy") & Now.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                Else
                    VoucherNo = GetNextDocNo(compWisePrefix, 6, "tblVoucher", "voucher_no")
                    Return VoucherNo
                End If
            Else
                VoucherNo = GetNextDocNo(compWisePrefix, 6, "tblVoucher", "voucher_no")
            End If
            'End Task#05082015
        End If
        'Else
        'Return ""
        'End If
        Return VoucherNo
    End Function

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        SaveConfiguration("NewReconcilationForm", "True")
        frmMain.LoadControl("frmReconciliation")
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
            If Config.Config_Type = key Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX1.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.GridEX1.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Bank Reconciliation"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class