Imports SBDal
Imports SBModel
Public Class frmStockReport
    Implements IGeneral
    Dim Obj As StockAuditReportMasterBE
    Dim ObjDetail As StockAuditReportDetailBE
    Dim ObjDAL As StockAuditReportMasterDAL
    Dim ObjDetailDAL As StockAuditReportDetailDAL
    Dim dtStock As DataTable
    Dim ID As Integer = 0
    Dim IsEditMode As Boolean = False
    Dim HasDeleteRights As Boolean = False
    Structure Detail
        'strSQL = " Select Detail.ID, Detail.StockAuditReportId, Detail.ArticleId, Article.ArticleDescription As Article, Detail.Unit, Detail.PackQty, Detail.Qty, Detail.TotalQty FROM StockAuditReportDetail AS Detail INNER JOIN ArticleDefTable AS Article ON Detail.ArticleId = Detail.ArticleId   WHERE StockAuditReportId = "& StockAuditReportId &""
        Public Shared ID As String = "ID"
        Public Shared StockAuditReportId As String = "StockAuditReportId"
        Public Shared LocationId As String = "LocationId"
        Public Shared ArticleId As String = "ArticleId"
        Public Shared Article As String = "Article"
        Public Shared Unit As String = "Unit"
        Public Shared Rate As String = "Rate"
        Public Shared PackQty As String = "PackQty"
        Public Shared Qty As String = "Qty"
        Public Shared PackStockQty As String = "PackStockQty"
        Public Shared StockQty As String = "StockQty"
        Public Shared TotalQty As String = "TotalQty"
        Public Shared BalancePackQty As String = "BalancePackQty"
        Public Shared BalanceQty As String = "BalanceQty"
    End Structure
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If UltraTabControl1.SelectedTab.Index = 0 Then
                CtrlGrdBar1.MyGrid = grdStockComparison
                CtrlGrdBar1.FormName = Me
            ElseIf UltraTabControl1.SelectedTab.Index = 1 Then
                CtrlGrdBar1.MyGrid = grdStockDetail
                CtrlGrdBar1.FormName = Me
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPackQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalQty_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTotalQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombo()
        Try
            Me.cmbUnit.ValueMember = "ArticlePackId"
            Me.cmbUnit.DisplayMember = "PackName"
            Me.cmbUnit.DataSource = GetPackData(0)
            Me.cmbUnit.DisplayLayout.Bands(0).Columns("ArticlePackId").Hidden = True
            FillUltraDropDown(Me.cmbStockAuditName, "SELECT ID, StockAuditName AS [Stock Audit Name], SessionName AS [Session Name], AuditDate AS [Date] FROM StockAuditTable Where IsNull(IsClosed, 0)=0")
            Me.cmbStockAuditName.Rows(0).Activate()
            Me.cmbStockAuditName.DisplayLayout.Bands(0).Columns("ID").Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbStockAuditName_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbStockAuditName.RowSelected
        Try
            If Me.cmbStockAuditName.Value > 0 Then
                Me.txtSessionName.Text = Me.cmbStockAuditName.ActiveRow.Cells("Session Name").Value.ToString
                FillLocations(Me.cmbStockAuditName.Value)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmStockReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'ObjDAL = New StockAuditReportMasterDAL()
            FillCombo()
            'InitializeStockComparison()
            'dtStock = ObjDAL.GetStock()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub AddToGrid()
        Try
            Dim dtGrid As DataTable = CType(Me.grdStockComparison.DataSource, DataTable)
            Dim dtGridDetail As DataTable = CType(Me.grdStockDetail.DataSource, DataTable)
            Dim dtMerging As New DataTable
            Dim newRow As DataRow = dtGrid.NewRow
            Dim newRowDetail As DataRow = dtGridDetail.NewRow
            If Me.txtBarcode.Text.Length > 0 AndAlso Me.cmbLocation.Value > 0 Then
                Dim dtFilter As New DataTable
                If dtStock.Select("ArticleCode = '" & Me.txtBarcode.Text.Replace("'", "''") & "' OR ArticleBARCode = '" & Me.txtBarcode.Text.Replace("'", "''") & "' AND LocationId = " & Me.cmbLocation.Value & "").Length > 0 Then
                    dtFilter = dtStock.Select("ArticleCode = '" & Me.txtBarcode.Text.Replace("'", "''") & "' OR ArticleBARCode = '" & Me.txtBarcode.Text.Replace("'", "''") & "'  AND LocationId = " & Me.cmbLocation.Value & "").CopyToDataTable()
                Else
                    ShowErrorMessage("No record found in stock")
                    Exit Sub
                End If
                If dtGrid.Rows.Count > 0 Then
                    If dtGrid.Select("ArticleCode = '" & Me.txtBarcode.Text.Replace("'", "''") & "' OR ArticleBARCode = '" & Me.txtBarcode.Text.Replace("'", "''") & "' AND PackQty = " & Val(Me.txtPackQty.Text) & " AND LocationId = " & Me.cmbLocation.Value & "").Length > 0 Then
                        dtMerging = dtGrid.Select("ArticleCode = '" & Me.txtBarcode.Text.Replace("'", "''") & "' OR ArticleBARCode = '" & Me.txtBarcode.Text.Replace("'", "''") & "' AND PackQty = " & Val(Me.txtPackQty.Text) & " AND LocationId = " & Me.cmbLocation.Value & "").CopyToDataTable
                    End If
                End If
                If dtMerging.Rows.Count > 0 Then
                    For Each _Row As DataRow In dtGrid.Rows
                        If _Row.Item(Detail.ArticleId) = dtMerging.Rows(0).Item(Detail.ArticleId) AndAlso _Row.Item(Detail.LocationId) = dtMerging.Rows(0).Item(Detail.LocationId) AndAlso _Row.Item(Detail.PackQty) = dtMerging.Rows(0).Item(Detail.PackQty) Then
                            _Row.BeginEdit()
                            _Row.Item(Detail.Qty) += Val(Me.txtQty.Text)
                            _Row.Item(Detail.TotalQty) += Val(Me.txtTotalQty.Text)
                            ''
                            _Row.Item(Detail.BalancePackQty) += Val(Me.txtQty.Text)
                            _Row.Item(Detail.BalanceQty) += Val(Me.txtTotalQty.Text)
                            _Row.EndEdit()
                        End If
                    Next
                    'dtMerging.Rows(0).BeginEdit()
                    ''dtMerging.Rows(0).Item(Detail.PackQty) += Val(Me.txtPackQty.Text)
                    'dtMerging.Rows(0).Item(Detail.Qty) += Val(Me.txtQty.Text)
                    'dtMerging.Rows(0).Item(Detail.TotalQty) += Val(Me.txtTotalQty.Text)
                    'dtMerging.Rows(0).EndEdit()
                    'dtMerging.AcceptChanges()
                Else
                    If dtFilter.Rows.Count > 0 Then
                        ''Addition to comparison grid
                        newRow("LocationId") = dtFilter.Rows(0).Item("LocationId")
                        newRow("Location") = cmbLocation.Text
                        newRow("ArticleId") = dtFilter.Rows(0).Item("ArticleId")
                        newRow("ArticleCode") = dtFilter.Rows(0).Item("ArticleCode").ToString
                        newRow("ArticleDescription") = dtFilter.Rows(0).Item("ArticleDescription").ToString
                        newRow("Unit") = Me.cmbUnit.Text
                        newRow("Rate") = dtFilter.Rows(0).Item("Rate")
                        newRow("PackStockQty") = dtFilter.Rows(0).Item("PackStockQty")
                        newRow("StockQty") = dtFilter.Rows(0).Item("StockQty")
                        newRow("PackQty") = Val(Me.txtPackQty.Text)
                        newRow("Qty") = Val(Me.txtQty.Text)
                        newRow("TotalQty") = Val(Me.txtTotalQty.Text)
                        newRow("ArticleBARCode") = dtFilter.Rows(0).Item("ArticleBARCode").ToString
                        newRow(Detail.BalancePackQty) = dtFilter.Rows(0).Item("PackStockQty") + Val(Me.txtQty.Text)
                        newRow(Detail.BalanceQty) = dtFilter.Rows(0).Item("StockQty") + Val(Me.txtTotalQty.Text)
                        dtGrid.Rows.Add(newRow)
                    End If
                    'For Each _Row As DataRow In dtGrid.Rows
                    'Next
                End If
                If dtFilter.Rows.Count > 0 Then
                    ''Addition to comparison grid
                    newRowDetail("LocationId") = dtFilter.Rows(0).Item("LocationId")
                    newRowDetail("Location") = cmbLocation.Text
                    newRowDetail("ArticleId") = dtFilter.Rows(0).Item("ArticleId")
                    newRowDetail("ArticleCode") = dtFilter.Rows(0).Item("ArticleCode").ToString
                    newRowDetail("ArticleDescription") = dtFilter.Rows(0).Item("ArticleDescription").ToString
                    newRowDetail("Unit") = Me.cmbUnit.Text
                    newRowDetail("Rate") = dtFilter.Rows(0).Item("Rate")
                    newRowDetail("PackStockQty") = dtFilter.Rows(0).Item("PackStockQty")
                    newRowDetail("StockQty") = dtFilter.Rows(0).Item("StockQty")
                    newRowDetail("PackQty") = Val(Me.txtPackQty.Text)
                    newRowDetail("Qty") = Val(Me.txtQty.Text)
                    newRowDetail("TotalQty") = Val(Me.txtTotalQty.Text)
                    'newRow(Detail.BalancePackQty) = dtFilter.Rows(0).Item("BalancePackQty") + Val(Me.txtQty.Text)
                    'newRow(Detail.BalancePackQty) = dtFilter.Rows(0).Item("BalancePackQty") + Val(Me.txtTotalQty.Text)
                    dtGridDetail.Rows.Add(newRowDetail)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub InitializeStockComparison()
        Try
            ObjDetailDAL = New StockAuditReportDetailDAL()
            Me.grdStockComparison.DataSource = ObjDetailDAL.GetAll(-1)
            Me.grdStockComparison.RootTable.Columns("StockQty").FormatString = "N" & DecimalPointInQty
            Me.grdStockComparison.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdStockComparison.RootTable.Columns("PackQty").FormatString = "N" & DecimalPointInQty
            Me.grdStockComparison.RootTable.Columns("TotalQty").FormatString = "N" & DecimalPointInQty
            Me.grdStockComparison.RootTable.Columns("BalanceQty").FormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub DisplayDetail(ByVal ID As Integer)
        Try
            ObjDetailDAL = New StockAuditReportDetailDAL()
            Me.grdStockDetail.DataSource = ObjDetailDAL.GetAll(ID)
            Me.grdStockDetail.RootTable.Columns("StockQty").FormatString = "N" & DecimalPointInQty
            Me.grdStockDetail.RootTable.Columns("Qty").FormatString = "N" & DecimalPointInQty
            Me.grdStockDetail.RootTable.Columns("PackQty").FormatString = "N" & DecimalPointInQty
            Me.grdStockDetail.RootTable.Columns("TotalQty").FormatString = "N" & DecimalPointInQty
            Me.grdStockDetail.RootTable.Columns("BalanceQty").FormatString = "N" & DecimalPointInQty
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            'Select StockAuditReportMaster.ID,  StockAuditReportMaster.DocumentNo, StockAuditReportMaster.DocumentDate, StockAuditReportMaster.StockAuditId, StockAuditTable.StockAuditName
            If HasDeleteRights = False Then
                ShowErrorMessage("You do not have delete rights.")
                Exit Function
            End If
            If msg_Confirm(str_ConfirmDelete) = False Then Exit Function
            ObjDAL = New StockAuditReportMasterDAL()
            Obj = New StockAuditReportMasterBE()
            Obj.ID = Val(Me.GridEX2.GetRow.Cells("ID").Value.ToString)
            Obj.Stock.StockTransId = StockTransId(Me.GridEX2.GetRow.Cells("DocumentNo").Value.ToString)
            'Obj.DocumentNo = Me.GridEX2.GetRow.Cells("DocumentNo").Value.ToString
            'Obj.DocumentNo = Me.GridEX2.GetRow.Cells("DocumentDate").Value
            Obj.ActivityLog.ActivityName = "Delete"
            Obj.ActivityLog.ApplicationName = String.Empty
            Obj.ActivityLog.FormCaption = "Stock Audit Report"
            Obj.ActivityLog.FormName = "frmStockReport"
            Obj.ActivityLog.LogDateTime = Now
            Obj.ActivityLog.RecordType = String.Empty
            Obj.ActivityLog.RefNo = String.Empty
            Obj.ActivityLog.Source = "frmStockReport"
            Obj.ActivityLog.User_Name = LoginUserName
            Obj.ActivityLog.UserID = LoginUserId
            If ObjDAL.Delete(Obj) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            Obj = New StockAuditReportMasterBE()
            Obj.DocumentNo = Me.txtDocumentNo.Text
            Obj.DocumentDate = Now
            Obj.StockAuditId = Me.cmbStockAuditName.Value
            Obj.ID = ID
            ''Stock entry 
            Obj.Stock.DocDate = Obj.DocumentDate
            Obj.Stock.DocNo = Obj.DocumentNo
            Obj.Stock.DocType = Val(StockDocTypeDAL.GetStockDocTypeId("Dispatch"))
            Obj.Stock.Project = 0
            Obj.Stock.AccountId = 0
            Obj.Stock.Remaks = "Stock Audit"
            Obj.Stock.StockDetailList = New List(Of StockDetail)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdStockDetail.GetRows
                Dim ObjDetail As New StockAuditReportDetailBE
                ObjDetail.ID = Val(Row.Cells(Detail.ID).Value.ToString)
                ObjDetail.StockAuditReportId = ID
                ObjDetail.LocationId = Val(Row.Cells(Detail.LocationId).Value.ToString)
                ObjDetail.ArticleId = Val(Row.Cells(Detail.ArticleId).Value.ToString)
                ObjDetail.PackQty = Val(Row.Cells(Detail.PackQty).Value.ToString)
                ObjDetail.Qty = Val(Row.Cells(Detail.Qty).Value.ToString)
                ObjDetail.TotalQty = Val(Row.Cells(Detail.TotalQty).Value.ToString)
                ObjDetail.Unit = Row.Cells(Detail.Unit).Value.ToString
                ObjDetail.Rate = Row.Cells(Detail.Rate).Value.ToString
                ObjDetail.PackStockQty = Val(Row.Cells(Detail.PackStockQty).Value.ToString)
                ObjDetail.StockQty = Val(Row.Cells(Detail.StockQty).Value.ToString)
                ObjDetail.BalancePackQty = Val(Row.Cells(Detail.BalancePackQty).Value.ToString)
                ObjDetail.BalanceQty = Val(Row.Cells(Detail.BalanceQty).Value.ToString)
                Obj.Detail.Add(ObjDetail)
                ''Stock detail entry
            Next
            ''Stock entry
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdStockComparison.GetCheckedRows
                ''Stock detail entry
                Dim StockDetail As New StockDetail
                StockDetail.ArticleDefId = Val(Row.Cells("ArticleId").Value.ToString)
                StockDetail.LocationId = Val(Row.Cells("LocationId").Value.ToString)
                StockDetail.InQty = IIf(Val(Row.Cells("TotalQty").Value.ToString) > 0, Val(Row.Cells("TotalQty").Value.ToString), 0)
                StockDetail.InAmount = IIf(Val(Row.Cells("TotalQty").Value.ToString) > 0, Val(Row.Cells("TotalQty").Value.ToString) * Val(Row.Cells("Rate").Value.ToString), 0)
                StockDetail.OutQty = IIf(Val(Row.Cells("TotalQty").Value.ToString) < 0, Math.Abs(Val(Row.Cells("TotalQty").Value.ToString)), 0)
                StockDetail.OutAmount = IIf(Val(Row.Cells("TotalQty").Value.ToString) < 0, Math.Abs(Val(Row.Cells("TotalQty").Value.ToString)) * Val(Row.Cells("Rate").Value.ToString), 0)
                StockDetail.In_PackQty = IIf(Val(Row.Cells("Qty").Value.ToString) > 0, Val(Row.Cells("Qty").Value.ToString), 0)
                StockDetail.Out_PackQty = IIf(Val(Row.Cells("Qty").Value.ToString) < 0, Math.Abs(Val(Row.Cells("Qty").Value.ToString)), 0)
                StockDetail.PackQty = Val(Row.Cells("PackQty").Value.ToString)
                StockDetail.Rate = Val(Row.Cells("Rate").Value.ToString)
                StockDetail.BatchNo = String.Empty
                StockDetail.Chassis_No = String.Empty
                StockDetail.Engine_No = String.Empty
                StockDetail.Remarks = "Stock Audit"
                Obj.Stock.StockDetailList.Add(StockDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            'Select ID, DocumentDate, DocumentNo, StockAuditId From StockAuditReportMaster LEFT OUTER JOIN StockAuditMaster ON StockAuditReportMaster.StockAuditId = StockAuditMaster.ID 
            ObjDAL = New StockAuditReportMasterDAL()
            Me.GridEX2.DataSource = ObjDAL.GetAll()
            Me.GridEX2.RetrieveStructure()
            If Me.GridEX2.RootTable.Columns.Contains("Delete") = False Then
                Me.GridEX2.RootTable.Columns.Add("Delete")
                Me.GridEX2.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.GridEX2.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.GridEX2.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.GridEX2.RootTable.Columns("Delete").Key = "Delete"
                Me.GridEX2.RootTable.Columns("Delete").Caption = "Action"
                Me.GridEX2.RootTable.Columns("Delete").Width = 70
            End If
            Me.GridEX2.RootTable.Columns("ID").Visible = False
            Me.GridEX2.RootTable.Columns("StockAuditId").Visible = False
            Me.GridEX2.RootTable.Columns("DocumentDate").FormatString = str_DisplayDateFormat
            Me.GridEX2.RootTable.Columns("DocumentNo").Caption = "Document No"
            Me.GridEX2.RootTable.Columns("StockAuditName").Caption = "Stock Audit Name"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDocumentNo.Text.Length < 1 Then
                ShowErrorMessage("Document No is required.")
                Me.txtDocumentNo.Focus()
                Return False
            End If
            If Me.cmbStockAuditName.Value < 1 Then
                ShowErrorMessage("Stock Audit is required.")
                Me.cmbStockAuditName.Focus()
                Return False
            End If
            If Me.grdStockDetail.RowCount = 0 Then
                ShowErrorMessage("Detail grid is empty.")
                Me.grdStockDetail.Focus()
                Return False
            End If
            If Me.grdStockComparison.GetCheckedRows.Length = 0 Then
                ShowErrorMessage("No row is selected. Please select at least one row.")
                Me.grdStockComparison.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            IsEditMode = False
            Me.btnSave.Text = "&Save"
            GetSecurityRights()
            Me.txtDocumentNo.Text = GetDocumentNo()
            Me.txtDocumentNo.Focus()
            Me.cmbStockAuditName.Rows(0).Activate()
            Me.txtSessionName.Text = String.Empty
            'Me.btnSave.Enabled = True
            Me.cmbUnit.Text = "Loose"
            Me.txtBarcode.Text = String.Empty
            Me.txtQty.Text = 0
            GetAllRecords()
            DisplayDetail(-1)
            InitializeStockComparison()
            ObjDAL = New StockAuditReportMasterDAL()
            dtStock = ObjDAL.GetStock()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            ObjDAL = New StockAuditReportMasterDAL()
            Obj.ActivityLog.ActivityName = "Save"
            Obj.ActivityLog.ApplicationName = String.Empty
            Obj.ActivityLog.FormCaption = "Stock Audit Report"
            Obj.ActivityLog.FormName = "frmStockReport"
            Obj.ActivityLog.LogDateTime = Now
            Obj.ActivityLog.RecordType = String.Empty
            Obj.ActivityLog.RefNo = String.Empty
            Obj.ActivityLog.Source = "frmStockReport"
            Obj.ActivityLog.User_Name = LoginUserName
            Obj.ActivityLog.UserID = LoginUserId
            If ObjDAL.Add(Obj) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
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
            ObjDAL = New StockAuditReportMasterDAL()
            Obj.ActivityLog.ActivityName = "Update"
            Obj.ActivityLog.ApplicationName = String.Empty
            Obj.ActivityLog.FormCaption = "Stock Audit Report"
            Obj.ActivityLog.FormName = "frmStockReport"
            Obj.ActivityLog.LogDateTime = Now
            Obj.ActivityLog.RecordType = String.Empty
            Obj.ActivityLog.RefNo = String.Empty
            Obj.ActivityLog.Source = "frmStockReport"
            Obj.ActivityLog.User_Name = LoginUserName
            Obj.ActivityLog.UserID = LoginUserId
            If ObjDAL.Update(Obj) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If IsValidate() Then
            If IsEditMode = False Then
                If Save() Then
                    msg_Information("Record has been saved successfully.")
                    ReSetControls()
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() Then
                    msg_Information("Record has been updated successfully.")
                    ReSetControls()
                End If
            End If
        End If
    End Sub

    Function GetDocumentNo() As String
        Try
            'If Me.txtPONo.Text = "" Then
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("SAR" + "-" + Microsoft.VisualBasic.Right(Now.Year, 2) + "-", "StockAuditReportMaster", "DocumentNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("SAR" & "-" & Format(Now, "yy") & Now.Month.ToString("00"), 4, "StockAuditReportMaster", "DocumentNo")
            Else
                Return GetNextDocNo("SAR", 6, "StockAuditReportMaster", "DocumentNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cmbUnit_RowSelected(sender As Object, e As Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbUnit.RowSelected
        Try
            If Me.cmbUnit.Text = "Loose" Then
                txtPackQty.Text = 1
                Me.txtPackQty.Enabled = False
                Me.txtPackQty.TabStop = False
                Me.txtTotalQty.Enabled = False
            Else
                Me.txtPackQty.Enabled = True
                Me.txtPackQty.TabStop = True
                Me.txtTotalQty.Enabled = True
                If Me.cmbUnit.ActiveRow IsNot Nothing Then
                    Me.txtPackQty.Text = Val(cmbUnit.ActiveRow.Cells("PackQty").ToString)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_TextChanged(sender As Object, e As EventArgs) Handles txtPackQty.TextChanged
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                txtTotalQty.Text = Val(txtQty.Text)
            Else
                txtTotalQty.Text = Val(txtQty.Text) * Val(txtPackQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged
        Try
            If Val(Me.txtPackQty.Text) = 0 Then
                txtPackQty.Text = 1
                txtTotalQty.Text = Val(txtQty.Text)
            Else
                txtTotalQty.Text = Val(txtQty.Text) * Val(txtPackQty.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBarcode_TextChanged(sender As Object, e As EventArgs) Handles txtBarcode.TextChanged
        'Try
        '    If txtBarcode.Text.Length > 0 Then
        '        AddToGrid()
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub txtBarcode_Leave(sender As Object, e As EventArgs) Handles txtBarcode.Leave
        'Try
        '    If txtBarcode.Text.Length > 0 Then
        '        AddToGrid()
        '    End If
        'Catch ex As Exception
        '    ShowErrorMessage(ex.Message)
        'End Try
    End Sub

    Private Sub FillLocations(ByVal StockAuditId As Integer)
        Try
            FillUltraDropDown(cmbLocation, "SELECT LocationId , location_name AS Location FROM StockAuditLocations INNER JOIN tblDefLocation ON location_id = LocationId WHERE StockAuditId = " & StockAuditId & "", False)
            Me.cmbLocation.Rows(0).Activate()
            Me.cmbLocation.DisplayLayout.Bands(0).Columns("Location").Width = 300
            Me.cmbLocation.DisplayLayout.Bands(0).Columns("LocationId").Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdStockComparison_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStockComparison.ColumnButtonClick
        Try
            If Me.grdStockComparison.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If msg_Confirm("Are you sure to delete the record?") = False Then Exit Sub
                Me.grdStockComparison.GetRow.Delete()
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdStockDetail_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdStockDetail.ColumnButtonClick
        Try
            If Me.grdStockDetail.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                If msg_Confirm("Are you sure to delete the record?") = False Then Exit Sub
                Me.grdStockDetail.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            ObjDAL = New StockAuditReportMasterDAL()
            FillCombo()
            'InitializeStockComparison()
            dtStock = ObjDAL.GetStock()
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBarcode_Enter(sender As Object, e As EventArgs) Handles txtBarcode.Enter

    End Sub
    Private Function ValidateInsertion() As Boolean
        Try
            If Me.cmbLocation.Value < 1 Then
                ShowErrorMessage("Please select a location.")
                Me.cmbLocation.Focus()
                Return False
            End If
            If Me.txtBarcode.Text.Length = 0 Then
                ShowErrorMessage("Please insert barcode.")
                Me.txtBarcode.Focus()
                Return False
            End If
            If Val(Me.txtTotalQty.Text) = 0 Then
                ShowErrorMessage("Quantity other than zero is required.")
                Me.txtTotalQty.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub EditRecord()
        Try
            IsEditMode = True
            If Me.GridEX2.RowCount = 0 Then Exit Sub
            ID = Val(Me.GridEX2.CurrentRow.Cells("ID").Value.ToString)
            Me.txtDocumentNo.Text = Me.GridEX2.CurrentRow.Cells("DocumentNo").Value.ToString
            Me.cmbStockAuditName.Value = Val(Me.GridEX2.CurrentRow.Cells("StockAuditId").Value.ToString)
            'Me.txtSessionName.Text = Val(Me.GridEX2.CurrentRow.Cells("SessionName").Value.ToString)
            DisplayDetail(ID)
            Me.btnSave.Text = "&Update"
            UltraTabControl2.SelectedTab = UltraTabControl2.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcode.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If ValidateInsertion() Then
                    AddToGrid()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                HasDeleteRights = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    HasDeleteRights = False
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                    Me.CtrlGrdBar1.mGridExport.Enabled = False
                    Me.CtrlGrdBar1.mGridPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmStoreIssuence)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If IsEditMode = False Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        'Me.Visible = dt.Rows(0).Item("View_Rights").ToString

                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                HasDeleteRights = False
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
                Me.CtrlGrdBar1.mGridExport.Enabled = False
                Me.CtrlGrdBar1.mGridPrint.Enabled = False
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        HasDeleteRights = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        Me.CtrlGrdBar1.mGridExport.Enabled = True

                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.CtrlGrdBar1.mGridPrint.Enabled = True

                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True

                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub GridEX2_RowDoubleClick(sender As Object, e As Janus.Windows.GridEX.RowActionEventArgs) Handles GridEX2.RowDoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl2_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl2.SelectedTabChanged
        Try
            If UltraTabControl2.SelectedTab.Index = 0 Then
                CtrlGrdBar1.MyGrid = grdStockDetail
                CtrlGrdBar1.FormName = Me
            ElseIf UltraTabControl2.SelectedTab.Index = 1 Then
                CtrlGrdBar1.MyGrid = GridEX2
                CtrlGrdBar1.FormName = Me
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX2.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.GridEX2.Name, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
                Me.GridEX2.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Customers
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & " Stock Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            'ObjDAL = New StockAuditReportMasterDAL()
            FillCombo()
            ''InitializeStockComparison()
            'dtStock = ObjDAL.GetStock()
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub GridEX2_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles GridEX2.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                If Me.GridEX2.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then

                    If Delete() Then
                        msg_Information("Record has been saved successfully.")
                        ReSetControls()
                    Else
                        ShowErrorMessage("Record has failed to delete")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class