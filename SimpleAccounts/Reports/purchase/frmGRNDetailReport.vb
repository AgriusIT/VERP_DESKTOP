'Ali Faisal : TFS1770 : Add report to show the GRN detail report
Imports System
Imports SBDal
Imports SBModel
Imports SBUtility
Public Class frmGRNDetailReport
    Implements IGeneral
    Dim _SearchDt As New DataTable
    ''' <summary>
    ''' Ali Faisal : TFS1770 : Set indexing of Records grid
    ''' </summary>
    ''' <remarks></remarks>
    Enum grd
        ReceivingNoteId
        LocationId
        LocationName
        CostCenterId
        CostCenter
        ReceivingNo
        ReceivingDate
        VendorId
        Vendor
        Remarks
        IGPNo
        DriverName
        VehicleNo
        ReceivingDetailId
        DepartmentId
        Department
        TypeId
        Type
        CatagoryId
        Catagory
        SubCatagoryId
        SubCatagory
        ItemId
        ItemCode
        ItemName
        SizeId
        Size
        ColorId
        Color
        Unit
        Sz1
        Sz7
        Qty
        Price
        BatchNo
        ReceivedQty
        RejectedQty
        ExpiryDate
        X_Gross_Weights
        X_Tray_Weights
        X_Net_Weights
        Y_Gross_Weights
        Y_Tray_Weights
        Y_Net_Weights
        Gross_Quantity
    End Enum
    ''' <summary>
    ''' Ali Faisal : TFS1770 : Apply grid settings to hide some columns and formating too
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.ReceivingNoteId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.LocationId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CostCenterId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.VendorId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ReceivingDetailId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.DepartmentId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.TypeId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CatagoryId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SubCatagoryId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ItemId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SizeId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ColorId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.Sz1).Visible = False
            Me.grdSaved.RootTable.Columns(grd.Sz7).Visible = False
            Me.grdSaved.RootTable.Columns(grd.BatchNo).Visible = False
            Me.grdSaved.RootTable.Columns(grd.Price).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ExpiryDate).Visible = False
            Me.grdSaved.RootTable.Columns(grd.Qty).Visible = False

            If getConfigValueByType("WeighbridgeGRN").ToString.ToUpper = "TRUE" Then
                Me.grdSaved.RootTable.Columns(grd.X_Tray_Weights).Visible = True
                Me.grdSaved.RootTable.Columns(grd.X_Gross_Weights).Visible = True
                Me.grdSaved.RootTable.Columns(grd.X_Net_Weights).Visible = True
                Me.grdSaved.RootTable.Columns(grd.Y_Gross_Weights).Visible = True
                Me.grdSaved.RootTable.Columns(grd.Y_Tray_Weights).Visible = True
                Me.grdSaved.RootTable.Columns(grd.Y_Net_Weights).Visible = True
            Else
                Me.grdSaved.RootTable.Columns(grd.X_Tray_Weights).Visible = False
                Me.grdSaved.RootTable.Columns(grd.X_Gross_Weights).Visible = False
                Me.grdSaved.RootTable.Columns(grd.X_Net_Weights).Visible = False
                Me.grdSaved.RootTable.Columns(grd.Y_Gross_Weights).Visible = False
                Me.grdSaved.RootTable.Columns(grd.Y_Tray_Weights).Visible = False
                Me.grdSaved.RootTable.Columns(grd.Y_Net_Weights).Visible = False
            End If
            Me.grdSaved.RootTable.Columns(grd.X_Tray_Weights).Caption = "V Tray Wts"
            Me.grdSaved.RootTable.Columns(grd.X_Gross_Weights).Caption = "V Gross Wts"
            Me.grdSaved.RootTable.Columns(grd.X_Net_Weights).Caption = "V Net Wts"
            Me.grdSaved.RootTable.Columns(grd.Y_Gross_Weights).Caption = "F Gross Wts"
            Me.grdSaved.RootTable.Columns(grd.Y_Tray_Weights).Caption = "F Tray Wts"
            Me.grdSaved.RootTable.Columns(grd.Y_Net_Weights).Caption = "F Net Wts"
            Me.grdSaved.RootTable.Columns(grd.Gross_Quantity).Caption = "Total Qty"
            Me.grdSaved.RootTable.Columns(grd.ExpiryDate).FormatString = str_DisplayDateFormat

            Me.grdSaved.RootTable.Columns(grd.Qty).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.ReceivedQty).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.ReceivedQty).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.ReceivedQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.ReceivedQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.ReceivedQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.RejectedQty).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.RejectedQty).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.RejectedQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.RejectedQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.RejectedQty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.X_Gross_Weights).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.X_Gross_Weights).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.X_Gross_Weights).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.X_Tray_Weights).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.X_Tray_Weights).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.X_Tray_Weights).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.X_Net_Weights).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.X_Net_Weights).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.X_Net_Weights).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Y_Gross_Weights).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Y_Gross_Weights).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Y_Gross_Weights).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Y_Tray_Weights).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Y_Tray_Weights).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Y_Tray_Weights).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Y_Net_Weights).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Y_Net_Weights).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Y_Net_Weights).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Gross_Quantity).FormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Gross_Quantity).TotalFormatString = "N" & DecimalPointInQty
            Me.grdSaved.RootTable.Columns(grd.Gross_Quantity).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.Gross_Quantity).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.Gross_Quantity).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1770 : Apply security to show specific controls to standard users
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ApplySecurity(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnPrint.Enabled = True
                Me.btnPrintPreview.Enabled = True
                Me.btnShow.Enabled = True
                Me.CtrlGrdBar1.mGridPrint.Enabled = True
                Me.CtrlGrdBar1.mGridExport.Enabled = True
                Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnPrint.Enabled = False
            Me.btnPrintPreview.Enabled = False
            Me.btnShow.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print Preview" Then
                    Me.btnPrintPreview.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    If Me.btnPrint.Text = "&Print" Then btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1770 : Fill list bosex
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "HeadCostCenter" Then
                FillListBox(Me.lstHeadCostCenter.ListItem, "SELECT DISTINCT CostCenterGroup, CostCenterGroup FROM tblDefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1")
            ElseIf Condition = "CostCenter" Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1")
            ElseIf Condition = "Department" Then
                FillListBox(Me.lstDepartment.ListItem, "SELECT ArticleGroupId, ArticleGroupName FROM ArticleGroupDefTable WHERE Active = 1 ORDER BY SortOrder")
            ElseIf Condition = "Type" Then
                FillListBox(Me.lstType.ListItem, "SELECT ArticleTypeId, ArticleTypeName FROM ArticleTypeDefTable WHERE Active = 1 ORDER BY SortOrder")
            ElseIf Condition = "VendorType" Then
                FillListBox(Me.lstVendorType.ListItem, "SELECT VendorTypeId, VendorType FROM tblVendorType WHERE Active = 1 ORDER BY SortOrder")
            ElseIf Condition = "Vendor" Then
                FillListBox(Me.lstVendor.ListItem, "SELECT coa_detail_id, detail_title FROM vwCOADetail WHERE Active = 1 AND account_type = 'Vendor' ORDER BY coa_detail_id")
            ElseIf Condition = "Item" Then
                FillListBox(Me.lstItem.ListItem, "SELECT ArticleId, ArticleCode + ' ~ ' + ArticleDescription AS ArticleName FROM ArticleDefTable WHERE Active = 1 ORDER BY SortOrder")
            ElseIf Condition = "Catagory" Then
                FillListBox(Me.lstCatagory.ListItem, "SELECT ArticleCompanyId, ArticleCompanyName FROM ArticleCompanyDefTable WHERE Active = 1 ORDER BY SortOrder")
            ElseIf Condition = "SubCatagory" Then
                FillListBox(Me.lstSubCatagory.ListItem, "SELECT ArticleLpoId, ArticleLpoName FROM ArticleLpoDefTable WHERE Active = 1 ORDER BY SortOrder")
            ElseIf Condition = "Size" Then
                FillListBox(Me.lstSize.ListItem, "SELECT ArticleSizeId, ArticleSizeName FROM ArticleSizeDefTable WHERE Active = 1 ORDER BY SortOrder")
            ElseIf Condition = "Color" Then
                FillListBox(Me.lstColor.ListItem, "SELECT ArticleColorId, ArticleColorName FROM ArticleColorDefTable WHERE Active = 1 ORDER BY SortOrder")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1770 : Get all records to get data in given duration
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            If Me.rbtnLoose.Checked = True Then
                str = "SELECT ReceivingNoteMasterTable.ReceivingNoteId, ReceivingNoteDetailTable.LocationId, tblDefLocation.location_name AS LocationName, ReceivingNoteMasterTable.CostCenterId, tblDefCostCenter.Name AS CostCenter, ReceivingNoteMasterTable.ReceivingNo, ReceivingNoteMasterTable.ReceivingDate, ReceivingNoteMasterTable.VendorId, vwCOADetail.detail_title AS Vendor, ReceivingNoteMasterTable.Remarks, ReceivingNoteMasterTable.IGPNo, ReceivingNoteMasterTable.Driver_Name AS DriverName, ReceivingNoteMasterTable.Vehicle_No AS VehicleNo, ReceivingNoteDetailTable.ReceivingDetailId, ArticleDefView.ArticleGroupId AS DepartmentId, ArticleDefView.ArticleGroupName AS Department, ArticleDefView.ArticleTypeId AS TypeId, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleCompanyId AS CatagoryId, ArticleDefView.ArticleCompanyName AS Catagory, ArticleDefView.ArticleLPOId AS SubCatagoryId, ArticleDefView.ArticleLpoName AS SubCatagory, ReceivingNoteDetailTable.ArticleDefId AS ItemId, ArticleDefView.ArticleCode AS ItemCode, ArticleDefView.ArticleDescription AS ItemName, ArticleDefView.SizeRangeId AS SizeId, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorId AS ColorId, ArticleDefView.ArticleColorName AS Color, ArticleDefView.ArticleUnitName AS Unit, ReceivingNoteDetailTable.Sz1, ReceivingNoteDetailTable.Sz7, ReceivingNoteDetailTable.Qty, ReceivingNoteDetailTable.Price, ReceivingNoteDetailTable.BatchNo, CASE WHEN ReceivingNoteDetailTable.ArticleSize = 'Loose' THEN ReceivingNoteDetailTable.ReceivedQty ELSE ReceivingNoteDetailTable.ReceivedQty * ArticleDefView.PackQty END ReceivedQty, CASE WHEN ReceivingNoteDetailTable.ArticleSize = 'Loose' THEN ReceivingNoteDetailTable.RejectedQty ELSE ReceivingNoteDetailTable.RejectedQty * ArticleDefView.PackQty END RejectedQty , ReceivingNoteDetailTable.ExpiryDate, ReceivingNoteDetailTable.X_Gross_Weights, ReceivingNoteDetailTable.X_Tray_Weights, ReceivingNoteDetailTable.X_Net_Weights, ReceivingNoteDetailTable.Y_Gross_Weights, ReceivingNoteDetailTable.Y_Tray_Weights, ReceivingNoteDetailTable.Y_Net_Weights, ReceivingNoteDetailTable.Qty Gross_Quantity "
            Else
                str = "SELECT ReceivingNoteMasterTable.ReceivingNoteId, ReceivingNoteDetailTable.LocationId, tblDefLocation.location_name AS LocationName, ReceivingNoteMasterTable.CostCenterId, tblDefCostCenter.Name AS CostCenter, ReceivingNoteMasterTable.ReceivingNo, ReceivingNoteMasterTable.ReceivingDate, ReceivingNoteMasterTable.VendorId, vwCOADetail.detail_title AS Vendor, ReceivingNoteMasterTable.Remarks, ReceivingNoteMasterTable.IGPNo, ReceivingNoteMasterTable.Driver_Name AS DriverName, ReceivingNoteMasterTable.Vehicle_No AS VehicleNo, ReceivingNoteDetailTable.ReceivingDetailId, ArticleDefView.ArticleGroupId AS DepartmentId, ArticleDefView.ArticleGroupName AS Department, ArticleDefView.ArticleTypeId AS TypeId, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleCompanyId AS CatagoryId, ArticleDefView.ArticleCompanyName AS Catagory, ArticleDefView.ArticleLPOId AS SubCatagoryId, ArticleDefView.ArticleLpoName AS SubCatagory, ReceivingNoteDetailTable.ArticleDefId AS ItemId, ArticleDefView.ArticleCode AS ItemCode, ArticleDefView.ArticleDescription AS ItemName, ArticleDefView.SizeRangeId AS SizeId, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorId AS ColorId, ArticleDefView.ArticleColorName AS Color, ArticleDefView.ArticleUnitName AS Unit, ReceivingNoteDetailTable.Sz1, ReceivingNoteDetailTable.Sz7, ReceivingNoteDetailTable.Qty / ArticleDefView.PackQty AS Qty, ReceivingNoteDetailTable.Price, ReceivingNoteDetailTable.BatchNo, CASE WHEN ReceivingNoteDetailTable.ArticleSize = 'Pack' THEN ReceivingNoteDetailTable.ReceivedQty ELSE ReceivingNoteDetailTable.ReceivedQty / ArticleDefView.PackQty END ReceivedQty, CASE WHEN ReceivingNoteDetailTable.ArticleSize = 'Pack' THEN ReceivingNoteDetailTable.RejectedQty ELSE ReceivingNoteDetailTable.RejectedQty / ArticleDefView.PackQty END RejectedQty , ReceivingNoteDetailTable.ExpiryDate, ReceivingNoteDetailTable.X_Gross_Weights, ReceivingNoteDetailTable.X_Tray_Weights, ReceivingNoteDetailTable.X_Net_Weights, ReceivingNoteDetailTable.Y_Gross_Weights, ReceivingNoteDetailTable.Y_Tray_Weights, ReceivingNoteDetailTable.Y_Net_Weights, ReceivingNoteDetailTable.Qty / ArticleDefView.PackQty AS Gross_Quantity "
            End If
            str += "FROM ReceivingNoteMasterTable INNER JOIN ReceivingNoteDetailTable ON ReceivingNoteMasterTable.ReceivingNoteId = ReceivingNoteDetailTable.ReceivingNoteId INNER JOIN tblDefLocation ON ReceivingNoteDetailTable.LocationId = tblDefLocation.location_id INNER JOIN vwCOADetail ON ReceivingNoteMasterTable.VendorId = vwCOADetail.coa_detail_id INNER JOIN ArticleDefView ON ReceivingNoteDetailTable.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN tblDefCostCenter ON ReceivingNoteMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
                & "WHERE ReceivingNoteMasterTable.ReceivingDate BETWEEN '" & Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00") & "' AND '" & Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59") & "'"

            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                str += " AND ReceivingNoteMasterTable.CostCenterId IN (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstVendor.SelectedIDs.Length > 0 Then
                str += " AND ReceivingNoteMasterTable.VendorId IN (" & Me.lstVendor.SelectedIDs & ")"
            End If
            If Me.lstDepartment.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.ArticleGroupId IN (" & Me.lstDepartment.SelectedIDs & ")"
            End If
            If Me.lstType.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.ArticleTypeId IN (" & Me.lstType.SelectedIDs & ")"
            End If
            If Me.lstCatagory.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.ArticleCompanyId IN (" & Me.lstCatagory.SelectedIDs & ")"
            End If
            If Me.lstSubCatagory.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.ArticleLPOId IN (" & Me.lstSubCatagory.SelectedIDs & ")"
            End If
            If Me.lstItem.SelectedIDs.Length > 0 Then
                str += " AND ReceivingNoteDetailTable.ArticleDefId IN (" & Me.lstItem.SelectedIDs & ")"
            End If
            If Me.lstSize.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.SizeRangeId IN (" & Me.lstSize.SelectedIDs & ")"
            End If
            If Me.lstColor.SelectedIDs.Length > 0 Then
                str += " AND ArticleDefView.ArticleColorId IN (" & Me.lstColor.SelectedIDs & ")"
            End If

            Dim dt As DataTable = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function
    ''' <summary>
    ''' Ali Faisal : TFS1770 : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks></remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbPeriod.Text = "Current Month"
            Me.txtSearch.Text = ""
            FillCombos("HeadCostCenter")
            FillCombos("CostCenter")
            FillCombos("VendorType")
            FillCombos("Vendor")
            FillCombos("Department")
            FillCombos("Type")
            FillCombos("Catagory")
            FillCombos("SubCatagory")
            FillCombos("Item")
            FillCombos("Size")
            FillCombos("Color")
            _SearchDt = CType(Me.lstItem.ListItem.DataSource, DataTable)
            _SearchDt.AcceptChanges()
            Me.UltraTabControl1.Tabs(0).Selected = True
            CtrlGrdBar1_Load(Nothing, Nothing)
            ApplySecurity(Utility.EnumDataMode.New)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFromDate.Value = Date.Today
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-1)
                Me.dtpToDate.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpToDate.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpToDate.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Goods Receiving Note Details Report"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmGRNDetailReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            btnNew_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS1770 : Show crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click, btnPrintPreview.Click
        Try
            GetCrystalReportRights()
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            AddRptParam("@FromDate", Me.dtpFromDate.Value.ToString("yyyy-MM-dd 00:00:00"))
            AddRptParam("@ToDate", Me.dtpToDate.Value.ToString("yyyy-MM-dd 23:59:59"))
            AddRptParam("@CostCenterIds", Me.lstCostCenter.SelectedIDs)
            If Me.rbtnLoose.Checked = True Then
                AddRptParam("@Unit", "Loose")
            Else
                AddRptParam("@Unit", "Pack")
            End If
            AddRptParam("@VendorIds", Me.lstVendor.SelectedIDs)
            AddRptParam("@DepartmentIds", Me.lstDepartment.SelectedIDs)
            AddRptParam("@TypeIds", Me.lstType.SelectedIDs)
            AddRptParam("@CatagoryIds", Me.lstCatagory.SelectedIDs)
            AddRptParam("@SubCatagoryIds", Me.lstSubCatagory.SelectedIDs)
            AddRptParam("@ItemIds", Me.lstItem.SelectedIDs)
            AddRptParam("@SizeIds", Me.lstSize.SelectedIDs)
            AddRptParam("@ColorIds", Me.lstColor.SelectedIDs)
            ShowReport("rptGRNDetails")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            GetAllRecords()
            Me.UltraTabControl1.Tabs(1).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Search item list by name or code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyUp
        Try
            Dim dv As New DataView
            _SearchDt.TableName = "Default"
            _SearchDt.CaseSensitive = False
            dv.Table = _SearchDt
            dv.RowFilter = "ArticleName Like '%" & Me.txtSearch.Text & "%'"
            Me.lstItem.ListItem.DataSource = dv.ToTable
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnPrint.Visible = False
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnPrint.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstHeadCostCenter_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstHeadCostCenter.SelectedIndexChaned
        Try
            If Me.lstHeadCostCenter.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstCostCenter.ListItem, "SELECT CostCenterID, Name FROM tblDefCostCenter WHERE Active = 1  AND CostCenterGroup IN ('" & Me.lstHeadCostCenter.SelectedIDs & "')")
            Else
                FillCombos("CostCenter")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstVendorType_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstVendorType.SelectedIndexChaned
        Try
            If Me.lstVendorType.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstVendor.ListItem, "SELECT coa_detail_id, detail_title FROM vwCOADetail WHERE Active = 1 AND account_type = 'Vendor' AND CustomerType IN (" & Me.lstVendorType.SelectedItems & ")")
            Else
                FillCombos("Vendor")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class