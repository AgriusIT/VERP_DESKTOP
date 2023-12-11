''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
'2015-07-01 Ali Ansari Rectify delete and auto focus of tab  Against Task#201507001 
Imports System.Data
Imports System.Data.OleDb
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Shared.ExportOptions
Imports CrystalDecisions.Windows.Forms
Imports SBDal.PrintLogDAL

Public Class frmSOCashReceipt
    Implements IGeneral
    Private _VoucherId As Integer
    Public _CompanyId As Integer
    Public _CostCenterId As Integer
    Public _SaleOrderId As Integer
    Public _SaleOrderNo As String
    Public _SaleOrderDate As DateTime
    Public _NetAmount As Double
    Public _CustomerId As Integer
    Public _CustomerName As String
    Private Voucher As VouchersMaster
    Private Sub frmSOCashReceipt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtSONo.Text = _SaleOrderNo
            Me.dtpSODate.Value = _SaleOrderDate
            Me.txtCustomer.Text = _CustomerName
            Me.txtNetAmount.Text = _NetAmount
            Me.dtChequeDate.Value = Now.AddDays(15)
            Me.txtChequeNo.Text = String.Empty
            FillCombos("VoucherType")
            Me.cmbVoucherType_SelectedIndexChanged(Nothing, Nothing)
            _VoucherId = 0I
            Me.TextBox2.Text = String.Empty
            If Microsoft.VisualBasic.Left(Me.txtSONo.Text, 2) = "SO" Then
                GetAllRecords("SO")
            Else
                GetAllRecords("PO")
            End If
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVoucherType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbVoucherType.SelectedIndexChanged
        Try
            If Me.cmbVoucherType.SelectedIndex = -1 Then Exit Sub
            Me.txtVoucherNo.Text = GetVoucherNo()
            FillCombos("Account")
            If Me.cmbVoucherType.Text = "Bank" Then
                Me.GroupBox1.Visible = True
            Else
                Me.GroupBox1.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction

        Voucher = New VouchersMaster
        'Marked by Ali Ansari Against Task#201507001 to get credit value against customer payment
        'Voucher.VNo = Me.grdDataHistory.GetRow.Cells("Voucher_No").Value
        'Marked by Ali Ansari Against Task#201507001 to get credit value against customer payment
        'Altered by Ali Ansari Against Task#201507001 to get credit value against customer payment
        Voucher.VNo = Me.grdDataHistory.GetRow.Cells("V No").Value
        'Altered by Ali Ansari Against Task#201507001 to get credit value against customer payment
        Voucher.ActivityLog = New ActivityLog
        Voucher.ActivityLog.ApplicationName = "Accounts"
        Voucher.ActivityLog.FormCaption = Me.Text
        Voucher.ActivityLog.RefNo = Me.txtVoucherNo.Text
        Voucher.ActivityLog.UserID = LoginUserId
        'Altered by Ali Ansari Against Task#201507001 to get credit value against customer payment

        Try
            If New VouchersDAL().Delete(Voucher, trans) = True Then
                trans.Commit()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "VoucherType" Then
                FillDropDown(Me.cmbVoucherType, "Select Voucher_Type_Id, Voucher_Type From (Select Voucher_Type_Id, Case When Voucher_Type = 'CRV' THEN 'Cash' WHEN Voucher_Type='BRV' THEN 'Bank' end as Voucher_Type From tblDefVoucherType )a where a.voucher_type <> ''", False)
            ElseIf Condition = "Account" Then
                FillDropDown(Me.cmbCashAccount, "Select coa_detail_id, detail_title from vwCOADetail where account_type='" & Me.cmbVoucherType.Text & "'", True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Voucher = New VouchersMaster
            With Voucher
                .VoucherId = _VoucherId
                .LocationId = _CompanyId
                .VoucherNo = Me.txtVoucherNo.Text
                .VoucherCode = Me.txtSONo.Text
                .FinancialYearId = 0
                .VoucherMonth = Me.dtpSODate.Value.Month
                .VoucherDate = Me.dtpSODate.Value
                .VNo = Me.txtVoucherNo.Text
                .CoaDetailId = Me.cmbCashAccount.SelectedValue
                .Post = True
                .Posted_UserName = LoginUserId
                If Microsoft.VisualBasic.Left(Me.txtSONo.Text, 2) = "SO" Then
                    .References = "Cash Receipt From " & _CustomerName & ""
                Else
                    .References = "Cash Payment To " & _CustomerName & ""
                End If

                .Source = Me.Name
                .UserName = LoginUserName
                .VoucherTypeId = Me.cmbVoucherType.SelectedValue
                .ActivityLog = New ActivityLog
                .ActivityLog.ApplicationName = "Accounts"
                .ActivityLog.FormCaption = Me.Text
                .ActivityLog.RefNo = Me.txtVoucherNo.Text
                .ActivityLog.UserID = LoginUserId
                .VoucherDatail = New List(Of VouchersDetail)

                Dim VoucherDt As VouchersDetail
                VoucherDt = New VouchersDetail 'Create Object For Credit Amount
                VoucherDt.CoaDetailId = Me.cmbCashAccount.SelectedValue

                VoucherDt.CostCenter = _CostCenterId
                VoucherDt.Cheque_No = IIf(Me.GroupBox1.Visible = True, Me.txtChequeNo.Text, String.Empty)
                VoucherDt.Cheque_Date = IIf(Me.GroupBox1.Visible = True, Me.dtChequeDate.Value, Nothing)
                If Microsoft.VisualBasic.Left(Me.txtSONo.Text, 2) = "SO" Then
                    VoucherDt.Comments = "Cash Receipt"
                    VoucherDt.CreditAmount = 0
                    VoucherDt.DebitAmount = Val(Me.TextBox2.Text)
                Else
                    VoucherDt.Comments = "Cash Payment"
                    VoucherDt.CreditAmount = Val(Me.TextBox2.Text)
                    VoucherDt.DebitAmount = 0
                End If

                VoucherDt.Discount = 0
                VoucherDt.LocationId = _CompanyId
                VoucherDt.VoucherId = _VoucherId
                VoucherDt.SPReference = String.Empty
                .VoucherDatail.Add(VoucherDt)


                VoucherDt = New VouchersDetail 'Create Object For Debit Amount
                VoucherDt.CoaDetailId = _CustomerId

                VoucherDt.CostCenter = _CostCenterId
                VoucherDt.Cheque_No = IIf(Me.GroupBox1.Visible = True, Me.txtChequeNo.Text, String.Empty)
                VoucherDt.Cheque_Date = IIf(Me.GroupBox1.Visible = True, Me.dtChequeDate.Value, Nothing)

                If Microsoft.VisualBasic.Left(Me.txtSONo.Text, 2) = "SO" Then
                    VoucherDt.Comments = "Cash Receipt From " & _CustomerName & ""
                    VoucherDt.CreditAmount = Val(Me.TextBox2.Text)
                    VoucherDt.DebitAmount = 0
                Else
                    VoucherDt.Comments = "Cash Payment To " & _CustomerName & ""
                    VoucherDt.CreditAmount = 0
                    VoucherDt.DebitAmount = Val(Me.TextBox2.Text)
                End If

                VoucherDt.Discount = 0
                VoucherDt.LocationId = _CompanyId
                VoucherDt.VoucherId = _VoucherId
                VoucherDt.SPReference = String.Empty
                .VoucherDatail.Add(VoucherDt)
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim strSQL As String = String.Empty
            If Condition = "SO" Then
                'Marked by Ali Ansari Against Task#201507001 to get credit value against customer payment
                'strSQL = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.location_id, tblvoucher.coa_detail_id as [Cash Account], vwCOADetail.coa_detail_id, vwCOAdetail.detail_title as [Customer], dbo.tblVoucher.voucher_type_id, dbo.tblDefVoucherType.voucher_type as [V Type], dbo.tblVoucher.voucher_code as [Doc No], " _
                '        & "  dbo.tblVoucher.voucher_no as [V No], dbo.tblVoucher.voucher_date as [V Date], tblVoucher.Cheque_No, tblVoucher.Cheque_Date, ISNULL(VD.Total_Amount, 0) AS Amount, CASE WHEN Isnull(dbo.tblVoucher.post, 0) = 0 THEN 'UnPost' ELSE 'Post' END AS Post " _
                '        & "  FROM  dbo.tblVoucher INNER JOIN " _
                '        & "  dbo.SalesOrderMasterTable ON dbo.tblVoucher.voucher_code = dbo.SalesOrderMasterTable.SalesOrderNo LEFT OUTER JOIN " _
                '        & "  dbo.vwCOADetail ON dbo.SalesOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                '        & "  dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id LEFT OUTER JOIN  (SELECT  voucher_id, SUM(ISNULL(debit_amount, 0)) AS Total_Amount " _
                '        & "  FROM dbo.tblVoucherDetail WHERE tblVoucherDetail.coa_detail_id =" & _CustomerId & " GROUP BY voucher_id) AS VD ON VD.voucher_id = dbo.tblVoucher.voucher_id  WHERE vwCOADetail.coa_detail_id=" & _CustomerId & " ORDER BY tblVoucher.Voucher_Id DESC "
                'Marked by Ali Ansari Against Task#201507001 to get credit value against customer payment
                'Altered by Ali Ansari Against Task#201507001 to get credit value against customer payment
                strSQL = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.location_id, tblvoucher.coa_detail_id as [Cash Account], vwCOADetail.coa_detail_id, vwCOAdetail.detail_title as [Customer], dbo.tblVoucher.voucher_type_id, dbo.tblDefVoucherType.voucher_type as [V Type], dbo.tblVoucher.voucher_code as [Doc No], " _
                        & "  dbo.tblVoucher.voucher_no as [V No], dbo.tblVoucher.voucher_date as [V Date], tblVoucher.Cheque_No, tblVoucher.Cheque_Date, ISNULL(VD.Total_Amount, 0) AS Amount, CASE WHEN Isnull(dbo.tblVoucher.post, 0) = 0 THEN 'UnPost' ELSE 'Post' END AS Post " _
                        & "  FROM  dbo.tblVoucher INNER JOIN " _
                        & "  dbo.SalesOrderMasterTable ON dbo.tblVoucher.voucher_code = dbo.SalesOrderMasterTable.SalesOrderNo LEFT OUTER JOIN " _
                        & "  dbo.vwCOADetail ON dbo.SalesOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                        & "  dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id LEFT OUTER JOIN  (SELECT  voucher_id, SUM(ISNULL(credit_amount, 0)) AS Total_Amount " _
                        & "  FROM dbo.tblVoucherDetail WHERE tblVoucherDetail.coa_detail_id =" & _CustomerId & " GROUP BY voucher_id) AS VD ON VD.voucher_id = dbo.tblVoucher.voucher_id  WHERE vwCOADetail.coa_detail_id=" & _CustomerId & " ORDER BY tblVoucher.Voucher_Id DESC "
                'Altered by Ali Ansari Against Task#201507001 to get credit value against customer payment
            Else
                strSQL = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.location_id, tblvoucher.coa_detail_id as [Cash Account], vwCOADetail.coa_detail_id, vwCOAdetail.detail_title as [Customer], dbo.tblVoucher.voucher_type_id, dbo.tblDefVoucherType.voucher_type as [V Type], dbo.tblVoucher.voucher_code as [Doc No], " _
                                        & "  dbo.tblVoucher.voucher_no as [V No], dbo.tblVoucher.voucher_date as [V Date], tblVoucher.Cheque_No, tblVoucher.Cheque_Date, ISNULL(VD.Total_Amount, 0) AS Amount, CASE WHEN Isnull(dbo.tblVoucher.post, 0) = 0 THEN 'UnPost' ELSE 'Post' END AS Post " _
                                        & "  FROM  dbo.tblVoucher INNER JOIN " _
                                        & "  dbo.PurchaseOrderMasterTable ON dbo.tblVoucher.voucher_code = dbo.PurchaseOrderMasterTable.PurchaseOrderNo LEFT OUTER JOIN " _
                                        & "  dbo.vwCOADetail ON dbo.PurchaseOrderMasterTable.VendorId = dbo.vwCOADetail.coa_detail_id LEFT OUTER JOIN " _
                                        & "  dbo.tblDefVoucherType ON dbo.tblVoucher.voucher_type_id = dbo.tblDefVoucherType.voucher_type_id LEFT OUTER JOIN  (SELECT  voucher_id, SUM(ISNULL(debit_amount, 0)) AS Total_Amount " _
                                        & "  FROM dbo.tblVoucherDetail WHERE tblVoucherDetail.coa_detail_id =" & _CustomerId & " GROUP BY voucher_id) AS VD ON VD.voucher_id = dbo.tblVoucher.voucher_id  WHERE vwCOADetail.coa_detail_id=" & _CustomerId & " ORDER BY tblVoucher.Voucher_Id DESC "
            End If
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            Me.grdDataHistory.DataSource = dt
            Me.grdDataHistory.RetrieveStructure()

            Me.grdDataHistory.RootTable.Columns("voucher_id").Visible = False
            Me.grdDataHistory.RootTable.Columns("location_id").Visible = False
            Me.grdDataHistory.RootTable.Columns("voucher_type_id").Visible = False
            Me.grdDataHistory.RootTable.Columns("coa_detail_id").Visible = False
            Me.grdDataHistory.RootTable.Columns("Cash Account").Visible = False
            'Altered by Ali Ansari Against Task#201507001 Format Amount in grid
            Me.grdDataHistory.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            'Altered by Ali Ansari Against Task#201507001 Format Amount in grid
            Me.grdDataHistory.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If _CustomerId = 0 Then
                ShowErrorMessage("Customer code is not valid")
                Me.txtCustomer.Focus()
                Return False
            End If
            If Me.cmbVoucherType.SelectedIndex = -1 Then
                ShowErrorMessage("Please define voucher type")
                Me.cmbType.Focus()
                Return False
            End If
            If Me.cmbCashAccount.SelectedIndex = 0 Then
                ShowErrorMessage("Please select deposit account")
                Me.cmbCashAccount.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.btnEdit.Visible = False
            Me.btnPrint.Visible = False
            Me.CutToolStripButton.Visible = False
            ''''''''''''''''''''''''''''''''''''''''''
            Me.cmbType.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            If New VouchersDAL().Add(Voucher, trans) = True Then
                trans.Commit()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Dim Con As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlClient.SqlTransaction = Con.BeginTransaction
        Try
            If New VouchersDAL().Update(Voucher, trans) = True Then
                trans.Commit()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Function GetVoucherNo() As String
        Dim docNo As String = String.Empty
        Dim VType As String = String.Empty
        If Me.cmbVoucherType.SelectedIndex > 0 Then
            If Microsoft.VisualBasic.Left(Me.txtSONo.Text, 2) = "SO" Then VType = "BRV" Else VType = "BPV"
        Else
            If Microsoft.VisualBasic.Left(Me.txtSONo.Text, 2) = "SO" Then VType = "CRV" Else VType = "CPV"
        End If
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtpSODate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo(VType & "-" & Format(Me.dtpSODate.Value, "yy") & Me.dtpSODate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                    Else
                        docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                        Return docNo
                    End If
                Else
                    docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                    Return docNo
                End If
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    'If Not msg_Confirm(str_ConfirmSave) = True Then Exit Sub
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informSave)
                    DialogResult = Windows.Forms.DialogResult.Yes
                Else
                    'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
                    Me.lblProgress.Text = "Processing Please Wait ..."
                    Me.lblProgress.Visible = True
                    Application.DoEvents()
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    'msg_Information(str_informUpdate)
                    DialogResult = Windows.Forms.DialogResult.Yes
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            Dim PrintLog As SBModel.PrintLogBE
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = Me.grdDataHistory.GetRow.Cells("V NO").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            'ShowReport("rptVoucher", "{VwGlVoucherSingle.VoucherId}=" & Me.grdVouchers.CurrentRow.Cells(0).Value & "")
            'Changing Against Request No 798
            AddRptParam("@VoucherId", Me.grdDataHistory.CurrentRow.Cells("Voucher_Id").Value)
            ShowReport("rptVoucher")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmSOCashReceipt_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 8 -1-14
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
            If e.KeyCode = Keys.F2 Then
                btnEdit_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.Delete Then
                CutToolStripButton_Click(Nothing, Nothing)
                Exit Sub
            End If

            If e.KeyCode = Keys.F4 Then
                btnSave_Click(Nothing, Nothing)
            End If


            If e.KeyCode = Keys.P AndAlso e.Control = True Then
                btnPrint_Click(Nothing, Nothing)
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmSOCashReceipt_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14 

            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            Me.txtVoucherNo.Text = Me.grdDataHistory.GetRow.Cells("V No").Value.ToString
            Me.txtSONo.Text = Me.grdDataHistory.GetRow.Cells("Doc No").Value.ToString
            Me.dtpSODate.Value = Me.grdDataHistory.GetRow.Cells("V Date").Value
            _CustomerId = Me.grdDataHistory.GetRow.Cells("coa_detail_id").Value
            Me.txtCustomer.Text = Me.grdDataHistory.GetRow.Cells("Customer").Value.ToString
            Me.txtNetAmount.Text = Val(Me.grdDataHistory.GetRow.Cells("Amount").Value.ToString)
            _VoucherId = Val(Me.grdDataHistory.GetRow.Cells("Voucher_Id").Value.ToString)
            _CompanyId = Val(Me.grdDataHistory.GetRow.Cells("Location_Id").Value.ToString)
            Me.cmbVoucherType.SelectedValue = Val(Me.grdDataHistory.GetRow.Cells("Voucher_Type_Id").Value.ToString)
            Me.cmbCashAccount.SelectedValue = Val(Me.grdDataHistory.GetRow.Cells("Cash Account").Value.ToString)
            Me.txtChequeNo.Text = Me.grdDataHistory.GetRow.Cells("Cheque_No").Value.ToString
            If IsDBNull(Me.grdDataHistory.GetRow.Cells("Cheque_Date").Value) Then
                Me.dtChequeDate.Value = Now
                Me.GroupBox1.Visible = False
            Else
                Me.dtChequeDate.Value = Me.grdDataHistory.GetRow.Cells("Cheque_Date").Value
                Me.GroupBox1.Visible = True
            End If
            Me.TextBox2.Text = Val(Me.txtNetAmount.Text)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.btnSave.Text = "&Update"
            ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
            Me.CutToolStripButton.Visible = True
            Me.btnPrint.Visible = True
            ''''''''''''''''''''''''''
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        'Altered by Ali Ansari Against Task#201507001 to get credit value against customer payment
        Try

            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            Me.lblProgress.Visible = False
        End Try
        'Altered by Ali Ansari Against Task#201507001 to get credit value against customer payment
    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click
        Try
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            'R-974 Ehtisham ul Haq user friendly system modification on 9-1-14
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            'Task-2389 Ehtisham ul Haq Reload History After Delete Record on 27-1-14 
            Me.grdDataHistory.CurrentRow.Delete()
            'msg_Information(str_informDelete)
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub grdDataHistory_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdDataHistory.DoubleClick
        Try
            If Me.grdDataHistory.RowCount = 0 Then Exit Sub
            Me.btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''19-Dec-2013 R934   M Ijaz Javed       Hide Buttons Edit,Delete and Print on Load Form
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 0 Then
                Me.btnEdit.Visible = False
                Me.btnPrint.Visible = False
                Me.CutToolStripButton.Visible = False
            Else
                Me.btnEdit.Visible = True
                Me.btnPrint.Visible = True
                Me.CutToolStripButton.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

   
    Private Sub grdDataHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdDataHistory.KeyDown
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Delete Then
            CutToolStripButton_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub grd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grd.KeyDown
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.Delete Then
            CutToolStripButton_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdDataHistory.GetRow Is Nothing AndAlso grdDataHistory.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmSOCashReceipt"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdDataHistory.CurrentRow.Cells(0).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Sale Order cash receipt (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Sales
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class