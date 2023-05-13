Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility
Imports SBUtility.Utility
Imports System.Net
Imports CRUFLIDAutomation
Public Class frmMRPlan
    Implements IGeneral
    Dim _DocId As Integer = 0I
    Dim MRPlan As MRPBE
    Dim strLoadAll As String = String.Empty
    Dim IsOpenedForm As Boolean = False
    Dim _MyGrd As New Janus.Windows.GridEX.GridEX
    Enum enmGrd
        LocationId
        ArticleDefId
        Item
        Code
        Color
        Size
        Unit
        CurrentStock
        SuggestedQty
        ActualQty
        Qty
        Price
        Total
        PackQty
        Comments
    End Enum
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = "Master" Then
                Me.grdSaved.RootTable.Columns("DocId").Visible = False
                Me.grdSaved.RootTable.Columns("ProductionArticleDefId").Visible = False
                Me.grdSaved.RootTable.Columns("PlanId").Visible = False
                Me.grdSaved.RootTable.Columns("PlanDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("ArticleDescription").Caption = "Item"
                Me.grdSaved.RootTable.Columns("ArticleCode").Caption = "Code"
                Me.grdSaved.RootTable.Columns("Total Amount").FormatString = "N"
                Me.grdSaved.AutoSizeColumns()
            ElseIf Condition = "Detail" Then
                For Each col As Janus.Windows.GridEX.GridEXColumn In Me.grd.RootTable.Columns
                    If col.Index <> enmGrd.LocationId AndAlso col.Index <> enmGrd.SuggestedQty AndAlso col.Index <> enmGrd.ActualQty AndAlso col.Index <> enmGrd.Price AndAlso col.Index <> enmGrd.Comments Then
                        col.EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
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
                Me.btnPrint.Enabled = True
            Else
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                CtrlGrdBar1.mGridChooseFielder.Enabled = False 'Task:2406 Added Field Chooser Security
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Field Chooser" Then
                        CtrlGrdBar1.mGridChooseFielder.Enabled = True
                    End If
                Next
            End If

            If Mode = EnumDataMode.[New] Then
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.btnEdit.Enabled = True
                Me.btnNew.Enabled = True
            ElseIf Mode = EnumDataMode.Edit Then
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnEdit.Enabled = False
                Me.btnNew.Enabled = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If Me.grdSaved.RowCount = 0 Then Return False
            MRPlan = New MRPBE
            MRPlan.DocId = Me.grdSaved.GetRow.Cells("DocId").Value.ToString
            MRPlan.DocNo = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            MRPlan.DocDate = Me.grdSaved.GetRow.Cells("DocDate").Value
            MRPlan.PlanId = Val(Me.grdSaved.GetRow.Cells("PlanId").Value.ToString)
            MRPlan.ActivityLog = New ActivityLog
            MRPlan.ActivityLog.UserID = LoginUserId
            MRPlan.ActivityLog.Source = Me.Name
            MRPlan.ActivityLog.LogDateTime = Date.Now
            MRPlan.ActivityLog.LogComments = String.Empty
            MRPlan.ActivityLog.FormCaption = Me.Text
            MRPlan.ActivityLog.ApplicationName = "Inventory"
            If New MRPDAL().Remove(MRPlan) = True Then
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
            Dim strSQL As String = String.Empty
            If Condition = "Plan" Then
                FillUltraDropDown(Me.cmbPlan, "Select PlanId, PlanNo + ' ~ ' + Convert(varchar,PlanDate,102) as PlanNo,CustomerId From PlanMasterTable ORDER BY PlanNo DESC")
                Me.cmbPlan.Rows(0).Activate()
                If Me.cmbPlan.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbPlan.DisplayLayout.Bands(0).Columns("PlanId").Hidden = True
                    Me.cmbPlan.DisplayLayout.Bands(0).Columns("CustomerId").Hidden = True
                    Me.cmbPlan.DisplayLayout.Bands(0).Columns(1).Width = 300
                End If
            ElseIf Condition = "ProductionItems" Then
                FillUltraDropDown(Me.cmbProductionItem, "SELECT  dbo.PlanDetailTable.ArticleDefId, dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleCode, IsNull(MasterID,0) as MasterID, IsNull(Qty,0) as Qty FROM dbo.PlanDetailTable INNER JOIN dbo.ArticleDefView ON dbo.PlanDetailTable.ArticleDefId = dbo.ArticleDefView.ArticleId WHERE dbo.PlanDetailTable.PlanId=" & Me.cmbPlan.Value & " And dbo.PlanDetailTable.ArticleDefId Not In(Select ArticleDefId From tblMaterialRequiredPlanMaster WHERE DocId <> " & _DocId & " And PlanId=" & Me.cmbPlan.Value & ") ")
                Me.cmbProductionItem.Rows(0).Activate()
                If Me.cmbProductionItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbProductionItem.DisplayLayout.Bands(0).Columns("ArticleDefId").Hidden = True
                    Me.cmbProductionItem.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                    Me.cmbProductionItem.DisplayLayout.Bands(0).Columns("ArticleDescription").Width = 300
                    Me.cmbProductionItem.DisplayLayout.Bands(0).Columns("ArticleCode").Width = 150
                    Me.cmbProductionItem.DisplayLayout.Bands(0).Columns("Qty").Width = 125
                End If
            ElseIf Condition = "Grid_Location" Then
                strSQL = String.Empty
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    strSQL = "Select Location_Id,Location_Code From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
                'Else
                '    strSQL = "Select Location_Id,Location_Code From tblDefLocation"
                'End If

                strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                         & " Else " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"

                Dim objDt As New DataTable
                objDt = GetDataTable(strSQL)
                objDt.AcceptChanges()
                Me.grd.RootTable.Columns(enmGrd.LocationId).ValueList.PopulateValueList(objDt.DefaultView, "Location_Id", "Location_Code")
            ElseIf Condition = "Location" Then
                strSQL = String.Empty
                'If getConfigValueByType("UserwiseLocation").ToString = "True" Then
                '    strSQL = "Select Location_Id, Location_Code From tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ")"
                'Else
                '    strSQL = "Select Location_Id, Location_Code From tblDefLocation"
                'End If


                strSQL = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                         & " Else " _
                         & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"


                FillDropDown(cmbLocation, strSQL, False)
                ''  FillDropDown(Me.cmbLocation, strSQL, False)

            ElseIf Condition = "Item" Then
                FillUltraDropDown(Me.cmbItem, "Select ArticleId, ArticleCode as Code, ArticleDescription as Item, ArticleColorName as Color, ArticleSizeName as Size, MasterID From ArticleDefView WHERE Active=1 ORDER BY ArticleDescription ASC")
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItem.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterID").Hidden = True
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Item").Width = 300
                    Me.cmbItem.DisplayLayout.Bands(0).Columns("Code").Width = 150
                    Me.rbCode.Checked = True
                End If
            ElseIf Condition = "Unit" Then
                Me.cmbUnit.ValueMember = "ArticlePackId"
                Me.cmbUnit.DisplayMember = "PackName"
                Me.cmbUnit.DataSource = GetPackData(Me.cmbItem.Value)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            Me.grd.UpdateData()
            MRPlan = New MRPBE
            With MRPlan
                .DocId = _DocId
                .DocNo = Me.txtDocumentNo.Text
                .DocDate = Me.dtpDocDate.Value
                .PlanId = Me.cmbPlan.Value
                .ProudctionArticleId = Me.cmbProductionItem.Value
                .Remarks = Me.txtRemarks.Text
                .TotalQty = Me.grd.GetTotal(Me.grd.RootTable.Columns("Actual Qty"), Janus.Windows.GridEX.AggregateFunction.Sum)
                .TotalAmount = Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
                .Issued = False
                .UserName = LoginUserName
                .EntryDate = Date.Now
                .ActivityLog = New ActivityLog
                .ActivityLog.UserID = LoginUserId
                .ActivityLog.Source = Me.Name
                .ActivityLog.LogDateTime = Date.Now
                .ActivityLog.LogComments = String.Empty
                .ActivityLog.FormCaption = Me.Text
                .ActivityLog.ApplicationName = "Inventory"
                .MRPlanDetail = New List(Of MRPDetailBE)
                Dim MRPDetail As MRPDetailBE
                For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    MRPDetail = New MRPDetailBE
                    MRPDetail.LocationId = Val(objRow.Cells(enmGrd.LocationId).Value.ToString)
                    MRPDetail.ArticleDefId = Val(objRow.Cells(enmGrd.ArticleDefId).Value.ToString)
                    MRPDetail.ArticleSize = objRow.Cells(enmGrd.Unit).Value.ToString
                    MRPDetail.CurrentStock = Val(objRow.Cells(enmGrd.CurrentStock).Value.ToString)
                    MRPDetail.SuggestedQty = Val(objRow.Cells(enmGrd.SuggestedQty).Value.ToString)
                    MRPDetail.Sz1 = Val(objRow.Cells(enmGrd.Qty).Value.ToString)
                    MRPDetail.Sz7 = Val(objRow.Cells(enmGrd.PackQty).Value.ToString)
                    MRPDetail.Qty = IIf(objRow.Cells(enmGrd.Unit).Value.ToString = "Loose", Val(objRow.Cells(enmGrd.Qty).Value.ToString), Val(MRPDetail.Sz1) * Val(MRPDetail.Sz7))
                    MRPDetail.Price = Val(objRow.Cells(enmGrd.Price).Value.ToString)
                    MRPDetail.Comments = objRow.Cells(enmGrd.Comments).Value.ToString
                    .MRPlanDetail.Add(MRPDetail)
                Next
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim dt As New DataTable
            If Condition = "Master" Then
                dt = New MRPDAL().GetAllRecords(strLoadAll)
                dt.AcceptChanges()
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings("Master")
            ElseIf Condition = "Detail" Then
                dt = New MRPDAL().DisplyDetail(_DocId)
                dt.AcceptChanges()
                dt.Columns("Actual Qty").Expression = "IIF( [Qty] > [Current Stock], [Suggested Qty],[Qty])"
                dt.Columns("Total").Expression = "IIF([Unit]='Loose', ([Price]*[Qty]),(([Pack Qty]*[Qty]) * [Price]))"
                Me.grd.DataSource = dt
                FillCombos("Grid_Location")
                ApplyGridSettings("Detail")
            ElseIf Condition = "PO" Then
                dt = New MRPDAL().PODetail(Val(Me.cmbProductionItem.ActiveRow.Cells("MasterID").Value.ToString), Val(Me.cmbProductionItem.ActiveRow.Cells("Qty").Value.ToString))
                dt.AcceptChanges()
                dt.Columns("Actual Qty").Expression = "IIF( [Qty] > [Current Stock], [Suggested Qty],[Qty])"
                dt.Columns("Total").Expression = "IIF([Unit]='Loose', ([Price]*[Qty]),(([Pack Qty]*[Qty]) * [Price]))"
                Me.grd.DataSource = dt
                FillCombos("Grid_Location")
                ApplyGridSettings("Detail")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.grd.RowCount = 0 Then
                ShowErrorMessage("Record not in grid")
                Me.grd.Focus()
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
            _DocId = 0I
            Me.btnSave.Text = "&Save"
            Me.txtDocumentNo.Text = New MRPDAL().GetSerialNo(Me.dtpDocDate.Value)
            Me.dtpDocDate.Value = Date.Now
            Me.cmbPlan.Rows(0).Activate()
            Me.cmbProductionItem.Rows(0).Activate()
            Me.txtRemarks.Text = String.Empty
            strLoadAll = String.Empty
            GetAllRecords("Master")
            GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(EnumDataMode.[New])
            Me.dtpDocDate.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New MRPDAL().Add(MRPlan) = True Then
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
            If New MRPDAL().Modify(MRPlan) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub cmbPlan_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPlan.ValueChanged
        Try
            If Me.cmbPlan.ActiveRow Is Nothing Then Exit Sub
            FillCombos("ProductionItems")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmMRPlan_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
            FillCombos("Plan")
            FillCombos("ProductionItems")
            FillCombos("Location")
            FillCombos("Item")
            FillCombos("Unit")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbPlan.Value
            FillCombos("Plan")
            Me.cmbPlan.Value = id

            id = Me.cmbProductionItem.Value
            FillCombos("ProductionItems")
            Me.cmbProductionItem.Value = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            Me.btnSave.Text = "&Update"
            Me._DocId = Val(Me.grdSaved.GetRow.Cells("DocId").Value.ToString)
            Me.txtDocumentNo.Text = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells("DocDate").Value
            Me.cmbPlan.Value = Val(Me.grdSaved.GetRow.Cells("PlanId").Value.ToString)
            FillCombos("ProductionItems")
            Me.cmbProductionItem.Value = Val(Me.grdSaved.GetRow.Cells("ProductionArticleDefId").Value.ToString)
            Me.txtRemarks.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            GetAllRecords("Detail")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            ApplySecurity(EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
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
            EditRecords()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnLoadAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadAll.Click
        Try
            strLoadAll = "All"
            GetAllRecords("Master")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            strLoadAll = String.Empty
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 1 Then
                Me.btnRefresh.Visible = False
                Me.btnLoadAll.Visible = True
                _MyGrd = grdSaved
            Else
                Me.btnRefresh.Visible = True
                Me.btnLoadAll.Visible = False
                _MyGrd = grd
            End If
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If Me.cmbLocation.SelectedIndex < 0 Then
                ShowErrorMessage("Please define location.")
                Me.cmbLocation.Focus()
                Exit Sub
            End If
            If Me.cmbItem.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select Item.")
                Me.cmbItem.Focus()
                Exit Sub
            End If
            If Val(txtQty.Text) = 0 Then
                ShowErrorMessage("Please enter qty.")
                Me.txtQty.Focus()
                Exit Sub
            End If
            Me.grd.UpdateData()
            Dim dtData As DataTable
            dtData = CType(Me.grd.DataSource, DataTable)
            dtData.AcceptChanges()
            Dim dt As New DataTable
            dt = GetDataTable("Select ArticleDefId, Sum(IsNull(InQty,0)-IsNull(OutQty,0)) as CurrentStock From StockDetailTable WHERE ArticleDefId=" & Me.cmbItem.Value & " Group By ArticleDefId")
            Dim dblCurrentStock As Double = 0D
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    dblCurrentStock = Val(dt.Rows(0).Item(1).ToString)
                End If
            End If
            Dim dr As DataRow
            dr = dtData.NewRow
            dr(enmGrd.LocationId) = Me.cmbLocation.SelectedValue
            dr(enmGrd.ArticleDefId) = Me.cmbItem.Value
            dr(enmGrd.Item) = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            dr(enmGrd.Code) = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
            dr(enmGrd.Color) = Me.cmbItem.ActiveRow.Cells("Color").Value.ToString
            dr(enmGrd.Size) = Me.cmbItem.ActiveRow.Cells("Size").Value.ToString
            dr(enmGrd.Unit) = "Loose"
            dr(enmGrd.CurrentStock) = dblCurrentStock
            dr(enmGrd.SuggestedQty) = 0
            dr(enmGrd.Qty) = Val(Me.txtQty.Text)
            dr(enmGrd.Price) = Val(Me.txtPrice.Text)
            dr(enmGrd.Comments) = String.Empty
            dr(enmGrd.PackQty) = Val(Me.txtPackQty.Text)
            dtData.Rows.Add(dr)
            dtData.AcceptChanges()
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Me.cmbItem.Focus()
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            If Not Me.cmbUnit.SelectedIndex = -1 Then Me.cmbUnit.SelectedIndex = 0
            Me.txtPackQty.Text = String.Empty
            Me.txtQty.Text = String.Empty
            Me.txtPrice.Text = String.Empty
            Me.txtTotal.Text = String.Empty

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbItem.ValueChanged
        Try
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            FillCombos("Unit")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If Me.cmbItem.ActiveRow Is Nothing Then Exit Sub
            If Not Me.cmbUnit.SelectedIndex = -1 Then
                If Me.cmbUnit.Text = "Loose" Then
                    Me.txtPackQty.Text = 1
                    Me.txtPackQty.Enabled = False
                Else
                    Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Row.Item("PackQty").ToString)
                    Me.txtPackQty.Enabled = True
                End If
            Else
                Me.txtPackQty.Text = String.Empty
                Me.txtPackQty.Enabled = False
            End If
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetTotal(Optional ByVal Condition As String = "")
        Try
            txtTotal.Text = ((Val(Me.txtPackQty.Text) * Val(Me.txtQty.Text)) * Val(Me.txtPrice.Text))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtPackQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPackQty.KeyPress, txtQty.KeyPress, txtPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grdSaved_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "btnDelete" Then
                Me.grd.GetRow.Delete()
                Me.grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbProductionItem_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProductionItem.Leave
        Try
            If Me.cmbProductionItem.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbProductionItem.Value > 0 Then
                GetAllRecords("PO")
                For Each jsRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                    jsRow.BeginEdit()
                    jsRow.Cells("LocationID").Value = IIf(Me.cmbLocation.SelectedIndex = -1, 0, Me.cmbLocation.SelectedValue)
                    jsRow.EndEdit()
                Next
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me._MyGrd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me._MyGrd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me._MyGrd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Material Plan"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            ShowReport("rptMaterialPlan", "{Sp_MaterialPlan;1.DocId} = " & Me.grdSaved.CurrentRow.Cells("DocId").Value & "")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmMRPlan"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells(1).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Material Plan (" & frmtask.Ref_No & ") "
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
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Inventory
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbCode.CheckedChanged
        Try
            If Me.rbCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbName_CheckedChanged(sender As Object, e As EventArgs) Handles rbName.CheckedChanged
        If Me.rbName.Checked = True Then
            Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
        End If
    End Sub
End Class