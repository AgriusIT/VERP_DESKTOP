''19-May-2014 TASK:2642 Imran Ali Adjustment Avg Rate In ERP
''01-Jul-2014 TASK:2707 Imran Ali AvgRate Adjustment Voucher In Adjustment Average Rate
'' 26-NOV-2026 TASTTFS35 Muhammad Ameen: Implementation of two more fields FromDate and ToDate for AdjustmentAvgRateDetail. 
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net

Public Class frmAdjeustmentAveragerate
    Implements IGeneral
    Dim DocId As Integer = 0I
    Dim IsOpenedForm As Boolean = False
    Dim AdjustmentAvg As AdjustmentAveragerateBE
    Dim strAllRecord As String = String.Empty
    Enum enmAvgRate
        Doc_Id
        LocationId
        ArticleDefId
        ArticleDescription
        ArticleCode
        ArticleSizeName
        ArticleColorName
        ArticleSize
        CurrentStock
        Current_Avg_Rate
        Adj_New_Cost_Price
        Adj_Amount
        'TAsk:2707 Added Index
        PurchaseAccountId
        CGSAccountId
        'End Task:2702
        FromDate
        ToDate
        Delete
    End Enum
    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmCustomerCollection)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkPost.Visible = True
                Else
                    Me.chkPost.Visible = False
                    Me.chkPost.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, btnSave, btnDelete)
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.chkPost.Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                        Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Me.grdSaved.RowCount = 0 Then
                Return False
            End If
            AdjustmentAvg = New AdjustmentAveragerateBE
            AdjustmentAvg.Doc_Id = Val(Me.grdSaved.GetRow.Cells("Doc_Id").Value.ToString)
            AdjustmentAvg.Doc_No = Me.grdSaved.GetRow.Cells("Doc_No").Value.ToString
            AdjustmentAvg.Doc_Date = Me.grdSaved.GetRow.Cells("Doc_Date").Value
            If New AdjustmentAveerageRate_Dal().DeleteRecord(AdjustmentAvg) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "Item" Then
                str = String.Empty
                str = "Select ArticleId as Id, ArticleDescription as Item, ArticleCode as Code, ArticleSizeName as [Size], ArticleColorName as [Color], SubSubID as AccountID From ArticleDefView"
                FillUltraDropDown(Me.cmbItem, str)
            ElseIf Condition = "Location" Then
                str = String.Empty
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    str = "Select Location_Id, Location_Code From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") "
                'Else
                '    str = "Select Location_Id, Location_Code From tblDefLocation"
                'End If
                str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                 & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") And Active = 1 order by sort_order " _
                 & " Else " _
                 & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation Where Active = 1 order by sort_order"

                FillDropDown(Me.cmbLocation, str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel

        Try



            AdjustmentAvg = New AdjustmentAveragerateBE
            AdjustmentAvg.Doc_Id = DocId
            AdjustmentAvg.Doc_Date = Me.dtpDocumentDate.Value
            AdjustmentAvg.Doc_No = Me.txtDocumentNo.Text
            AdjustmentAvg.Post = Me.chkPost.Checked
            AdjustmentAvg.EntryDate = Now
            AdjustmentAvg.UserId = LoginUserId
            AdjustmentAvg.Comments = Me.txtComments.Text
            AdjustmentAvg.StockTransId = 0
            AdjustmentAvg.VoucherId = 0
            AdjustmentAvg.AdjustmentAvgRateDetail = New List(Of AdjustmentAvgRateDetailBE)
            Dim AdjDt As AdjustmentAvgRateDetailBE
            Me.grd.UpdateData()
            For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                AdjDt = New AdjustmentAvgRateDetailBE
                AdjDt.DocDetail_Id = 0
                AdjDt.Doc_Id = Val(objRow.Cells("Doc_Id").Value.ToString)
                AdjDt.LocationId = Val(objRow.Cells("LocationId").Value.ToString)
                AdjDt.ArticleDefId = Val(objRow.Cells("ArticleDefId").Value.ToString)
                AdjDt.ArticleSize = objRow.Cells("ArticleSize").Value.ToString
                AdjDt.CurrentStock = Val(objRow.Cells("Current_Stock").Value.ToString)
                AdjDt.Current_Avg_Rate = Val(objRow.Cells("Current_Avg_Rate").Value.ToString)
                AdjDt.Adj_New_Cost_Price = Val(objRow.Cells("Adj_New_Cost_Price").Value.ToString)
                AdjDt.Adj_Amount = Val(objRow.Cells("Adj_Amount").Value.ToString)
                ''01-Jul-2014 TASK:2707 Imran Ali AvgRate Adjustment Voucher In Adjustment Average Rate
                AdjDt.PurchaseAccountId = Val(objRow.Cells("PurchaseAccountId").Value.ToString)
                AdjDt.CGSAccountId = Val(objRow.Cells("CGSAccountId").Value.ToString)

                ''TASTTFS35
                AdjDt.FromDate = CDate(objRow.Cells("FromDate").Value.ToString)
                AdjDt.ToDate = CDate(objRow.Cells("ToDate").Value.ToString)
                'End Task:2707
                AdjustmentAvg.AdjustmentAvgRateDetail.Add(AdjDt)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try

            Dim objdt As New DataTable
            If Condition = "Master" Then
                objdt.Clear()
                objdt.Columns.Clear()
                objdt = New AdjustmentAveerageRate_Dal().GetAllRecords(IIf(strAllRecord = "All", "All", String.Empty))
                Me.grdSaved.DataSource = objdt
                Me.grdSaved.RetrieveStructure()
                Me.grdSaved.RootTable.Columns("Doc_Id").Visible = False
                Me.grdSaved.RootTable.Columns("UserId").Visible = False
                Me.grdSaved.RootTable.Columns("EntryDate").Visible = False
                Me.grdSaved.AutoSizeColumns()
            ElseIf Condition = "Detail" Then
                objdt.Clear()
                objdt.Columns.Clear()
                objdt = New AdjustmentAveerageRate_Dal().GetAllRecordDetail(DocId)
                objdt.Columns("Adj_Amount").Expression = "((Adj_New_Cost_Price-Current_Avg_Rate)*Current_Stock)"
                objdt.AcceptChanges()
                Me.grd.DataSource = objdt
                Dim Str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
              & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
              & " Else " _
              & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

                Dim dt As New DataTable
                dt = GetDataTable(Str)
                Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(dt.DefaultView, "Location_Id", "Location_Code")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("Record not in grid.")
                Me.grd.Focus()
                Return False
            End If

            If Me.txtDocumentNo.Text = String.Empty Then
                ShowErrorMessage("Define document no.")
                Me.txtDocumentNo.Focus()
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
            DocId = 0
            Dim strFirstChar As String = "AR-" & Me.dtpDocumentDate.Value.ToString("yy") & "-"
            Me.txtDocumentNo.Text = New AdjustmentAveerageRate_Dal().GetDocNo(strFirstChar)
            Me.dtpDocumentDate.Value = Now
            Me.chkPost.Checked = True
            Me.txtComments.Text = String.Empty
            Me.cmbItem.Rows(0).Activate()
            Me.txtCurrStock.Text = String.Empty
            Me.txtCurrAvgRate.Text = String.Empty
            Me.txtAdjNewCostPrice.Text = String.Empty
            If Not Me.cmbLocation.SelectedIndex - 1 Then Me.cmbLocation.SelectedIndex = 0
            Me.btnDelete.Visible = False
            Me.btnRefresh.Visible = True
            Me.dtpDocumentDate.Focus()
            GetAllRecords("Master")
            GetAllRecords("Detail")
            ApplySecurity(EnumDataMode.[New])
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try

            If New AdjustmentAveerageRate_Dal().SaveRecord(AdjustmentAvg) = True Then
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

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            If New AdjustmentAveerageRate_Dal().UpdateRecord(AdjustmentAvg) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub frmAdjeustmentAveragerate_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If Me.btnSave.Enabled = True Then
                    Me.btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                If Me.grd.RowCount > 0 Then
                    If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
                    ReSetControls()
                End If
            End If
            If e.KeyCode = Keys.F2 Then
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    Me.btnEdit_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Delete Then
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    Me.btnDelete_Click(Nothing, Nothing)
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecord(Optional ByVal Condition As String = "")
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            DocId = Val(Me.grdSaved.GetRow.Cells("Doc_Id").Value.ToString)
            Me.dtpDocumentDate.Value = Me.grdSaved.GetRow.Cells("Doc_Date").Value
            Me.txtDocumentNo.Text = Me.grdSaved.GetRow.Cells("Doc_No").Value.ToString
            Me.chkPost.Checked = Me.grdSaved.GetRow.Cells("Post").Value
            Me.txtComments.Text = Me.grdSaved.GetRow.Cells("Comments").Value.ToString
            Me.btnSave.Text = "&Update"
            Me.btnDelete.Visible = True
            GetAllRecords("Detail")
            ApplySecurity(EnumDataMode.Edit)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            Me.lblProgress.Text = "Loading please wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            If Delete() = True Then
                ReSetControls()
                Me.lblProgress.Visible = False
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    Me.lblProgress.Text = "Loading please wait ..."
                    Me.lblProgress.BackColor = Color.LightYellow
                    Me.lblProgress.Visible = True
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    If Save() = True Then
                        ReSetControls()
                        Me.lblProgress.Visible = False
                        Me.Cursor = Cursors.Default
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    Me.lblProgress.Text = "Loading please wait ..."
                    Me.lblProgress.BackColor = Color.LightYellow
                    Me.lblProgress.Visible = True
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents()
                    If Update1() = True Then
                        ReSetControls()
                        Me.lblProgress.Visible = False
                        Me.Cursor = Cursors.Default
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If cmbItem.IsItemInList = False Then Exit Sub
            If Me.cmbItem.ActiveRow.Cells(0).Value = 0 Then
                ShowErrorMessage("Please Select Item.")
                Me.cmbItem.Focus()
                Exit Sub
            End If
            If Val(Me.txtCurrStock.Text) < 0 Then
                ShowErrorMessage("Minus stock not allowed.")
                Me.txtCurrStock.Focus()
                Exit Sub
            End If
            If Val(Me.txtCurrStock.Text) = 0 Then
                ShowErrorMessage("Current stock not exist.")
                Me.txtCurrStock.Focus()
                Exit Sub
            End If
            If Val(Me.txtCurrAvgRate.Text) = 0 Then
                ShowErrorMessage("Current average rate not exist.")
                Me.txtCurrAvgRate.Focus()
                Exit Sub
            End If
            If Val(Me.txtAdjNewCostPrice.Text) = 0 Then
                ShowErrorMessage("Please enter adjustment new cost price.")
                Me.txtAdjNewCostPrice.Focus()
                Exit Sub
            End If

            Dim CgsAccountId As Integer = Val(getConfigValueByType("CGSAccountId").ToString)
            Dim GLAccountArticleDepartment As Boolean = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Dim AccountId As Integer = getConfigValueByType("PurchaseDebitAccount")

            Dim objDt As New DataTable
            Me.grd.UpdateData()
            objDt = CType(Me.grd.DataSource, DataTable)
            objDt.AcceptChanges()
            Dim objDR As DataRow
            objDR = objDt.NewRow
            objDR(enmAvgRate.Doc_Id) = DocId
            objDR(enmAvgRate.LocationId) = Me.cmbLocation.SelectedValue
            objDR(enmAvgRate.ArticleDefId) = Me.cmbItem.Value
            objDR(enmAvgRate.ArticleDescription) = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            objDR(enmAvgRate.ArticleCode) = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
            objDR(enmAvgRate.ArticleSizeName) = Me.cmbItem.ActiveRow.Cells("Size").Value.ToString
            objDR(enmAvgRate.ArticleColorName) = Me.cmbItem.ActiveRow.Cells("Color").Value.ToString
            If dtpFrom.Checked = True Then
                objDR(enmAvgRate.FromDate) = CDate(Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00"))
            Else
                objDR(enmAvgRate.FromDate) = Date.MinValue
            End If
            If dtpTo.Checked = True Then
                objDR(enmAvgRate.ToDate) = CDate(Me.dtpTo.Value.ToString("yyyy-M-d 00:00:00"))
            Else
                objDR(enmAvgRate.ToDate) = Date.MinValue
            End If

            objDR(enmAvgRate.ArticleSize) = "Loose"
            objDR(enmAvgRate.CurrentStock) = Val(Me.txtCurrStock.Text)
            objDR(enmAvgRate.Current_Avg_Rate) = Val(Me.txtCurrAvgRate.Text)
            objDR(enmAvgRate.Adj_New_Cost_Price) = Val(Me.txtAdjNewCostPrice.Text)
            If GLAccountArticleDepartment = False Then
                objDR(enmAvgRate.PurchaseAccountId) = AccountId
            Else
                objDR(enmAvgRate.PurchaseAccountId) = Me.cmbItem.ActiveRow.Cells("AccountId").Value.ToString
            End If
            objDR(enmAvgRate.CGSAccountId) = CgsAccountId
            objDt.Rows.InsertAt(objDR, 0)
            objDt.AcceptChanges()
            objDt.Columns("Adj_Amount").Expression = "((Adj_New_Cost_Price-Current_Avg_Rate)*Current_Stock)"
            objDt.AcceptChanges()

            Me.txtCurrStock.Text = String.Empty
            Me.txtCurrAvgRate.Text = String.Empty
            Me.txtAdjNewCostPrice.Text = String.Empty
            Me.txtAdjAmount.Text = String.Empty
            Me.cmbItem.Focus()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.SelectedTab.Index = 0 Then
                Me.btnSave.Visible = True
                Me.btnRefresh.Visible = True
                If Not Me.btnSave.Text = "&Update" Then
                    Me.btnDelete.Visible = False
                Else
                    Me.btnDelete.Visible = True
                End If
            Else
                Me.btnDelete.Visible = True
                Me.btnRefresh.Visible = False
                Me.btnSave.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmAdjeustmentAveragerate_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.lblProgress.Text = "Loading please wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos("Location")
            FillCombos("Item")
            IsOpenedForm = True
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub cmbItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.Leave
        Try
            If Me.cmbItem.IsItemInList = False Then Exit Sub
            Dim objDt As New DataTable
            objDt = New AdjustmentAveerageRate_Dal().GetCurrentStock(Me.cmbItem.Value)
            If objDt IsNot Nothing Then
                If objDt.Rows.Count > 0 Then
                    Me.txtCurrStock.Text = Val(objDt.Rows(0).Item(1).ToString())
                    Me.txtCurrAvgRate.Text = Val(objDt.Rows(0).Item(2).ToString()) / Val(objDt.Rows(0).Item(1).ToString())
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtAdjNewCostPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdjNewCostPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtAdjNewCostPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdjNewCostPrice.TextChanged
        Try
            Me.txtAdjAmount.Text = ((Val(Me.txtAdjNewCostPrice.Text) - Val(Me.txtCurrAvgRate.Text)) * Val(Me.txtCurrStock.Text))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            Me.grd.GetRow.Delete()
            Me.grd.UpdateData()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbLocation.SelectedIndex
            FillCombos("Location")
            Me.cmbLocation.SelectedIndex = id
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load

    End Sub

    Private Sub frmAdjeustmentAveragerate_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class