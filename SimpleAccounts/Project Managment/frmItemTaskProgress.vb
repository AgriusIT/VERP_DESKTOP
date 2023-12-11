'29-June-2017 TFS1014 : Ali Faisal : Add new form to save update and delete records through this form.
'03-March-2018 TFS2712 : Ayesha Rehman : Save & Send for approval didn't work properly
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class frmItemTaskProgress
    Implements IGeneral
    Dim objDAL As ItemTaskProgressDAL
    Dim objModel As ItemTaskProgressBE
    Dim ProgressId As Integer
    Dim ContractId As Integer
    Dim SendForApproval As Boolean
    Dim NetValue As Double
    Dim IsEditMode As Boolean = False
    ''' <summary>
    ''' Ali Faisal : set indexes of detail grid to use name of columns from enum instead of from query.
    ''' </summary>
    ''' <remarks>'29-June-2017 TFS1014 : Ali Faisal</remarks>
    Enum grdenm
        Id
        ProgressId
        ContractId
        TaskId
        TaskTitle
        TaskDetail
        TaskRate
        TaskUnit
        PrevProgress
        CurrentProgress
        ApprovedProgress
        NetProgress
        PendingProgress
        NetValue
        Qty
        Measurment
        ContractValue
    End Enum
    ''' <summary>
    ''' Ali Faisal : Apply grid seeting to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>'29-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grd.RootTable.Columns(grdenm.Id).Visible = False
            Me.grd.RootTable.Columns(grdenm.ProgressId).Visible = False
            Me.grd.RootTable.Columns(grdenm.TaskId).Visible = False
            Me.grd.RootTable.Columns(grdenm.ApprovedProgress).Visible = False
            Me.grd.RootTable.Columns(grdenm.ContractId).Visible = False
            Me.grd.RootTable.Columns(grdenm.TaskTitle).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.TaskTitle).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.TaskDetail).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.TaskDetail).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.TaskRate).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.TaskRate).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.TaskUnit).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.TaskUnit).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.PrevProgress).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.PrevProgress).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.CurrentProgress).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.CurrentProgress).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.NetProgress).CellStyle.BackColor = Color.LightYellow
            Me.grd.RootTable.Columns(grdenm.NetProgress).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns(grdenm.NetProgress).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.PendingProgress).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.PendingProgress).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.ContractValue).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.ContractValue).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.NetValue).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.NetValue).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Ali Faisal : TFS1463 : Apply grid settings
            Me.grd.RootTable.Columns(grdenm.Measurment).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.Measurment).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.Qty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdenm.Qty).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Ali Faisal : TFS1463

            Me.grd.RootTable.Columns(grdenm.NetValue).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.NetValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.NetValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.TaskRate).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.TaskRate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.TaskRate).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.PrevProgress).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.PrevProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.PrevProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.CurrentProgress).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.CurrentProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.CurrentProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.PendingProgress).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.PendingProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.PendingProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.NetProgress).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.NetProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.NetProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'Ali Faisal : TFS1463 : Apply grid settings
            
            Me.grd.RootTable.Columns(grdenm.ContractValue).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(grdenm.ContractValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.ContractValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.Measurment).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.Measurment).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.Measurment).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.TaskRate).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.TaskRate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.TaskRate).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns(grdenm.ContractValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdenm.TaskRate).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdenm.NetValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdenm.Measurment).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns(grdenm.ApprovedProgress).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.ApprovedProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.ApprovedProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.ApprovedProgress).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns(grdenm.NetValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.ApprovedProgress).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.ContractValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.Measurment).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.TaskRate).TotalFormatString = "N" & DecimalPointInValue

            Me.grd.RootTable.Columns(grdenm.Qty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(grdenm.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.UpdateData()
            'Ali Faisal : TFS1463 : End
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    ''' <summary>
    ''' Ali Faisal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>'03-July-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnEdit.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.BtnSave.Enabled = False
            Me.BtnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            Me.CtrlGrdBar3.mGridPrint.Enabled = False
            Me.CtrlGrdBar3.mGridExport.Enabled = False
            Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
            Me.CtrlGrdBar2.mGridPrint.Enabled = False
            Me.CtrlGrdBar2.mGridExport.Enabled = False
            Me.CtrlGrdBar2.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.BtnDelete.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar3.mGridPrint.Enabled = True
                    Me.CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar3.mGridExport.Enabled = True
                    Me.CtrlGrdBar2.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                    Me.CtrlGrdBar2.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Calls the Delete function from DAL to remove the data of selected row.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>'29-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New ItemTaskProgressDAL
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells("Id").Value.ToString)) = True Then
                'Insert Activity Log by Ali Faisal on 30-June-2017
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : FillCombos of items, Vendor list and open PO here.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Item" Then
                str = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.PackQty, ISNULL(ArticleDefView.SalePrice, 0) AS Price, ISNULL(ArticleDefView.PurchasePrice, 0) AS PurchasePrice, ArticleDefView.SortOrder, ArticleDefView.ArticleGroupName AS Dept, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleLpoName AS Brand, ISNULL(ArticleDefView.Cost_Price, 0) AS [Cost Price], ISNULL(ArticleDefView.TradePrice, 0) AS [Trade Price] FROM ArticleDefView INNER JOIN PurchaseOrderDetailTable ON ArticleDefView.ArticleId = PurchaseOrderDetailTable.ArticleDefId"
                str += " where Active=1 And PurchaseOrderId = " & Me.cmbPO.Value & ""
                FillUltraDropDown(Me.cmbItem, str)
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    '30-June-2017 TFS1014 : Ali Faisal : To change the dispaly member when check is changed
                    If Me.rbtnByCode.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
                UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "Vendor" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as [Type Id], dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
                str += " AND   (dbo.vwCOADetail.account_type = 'Vendor') "
                str += " AND vwCOADetail.Active=1"
                FillUltraDropDown(cmbVendor, str)
                Me.cmbVendor.Rows(0).Activate()
                If Me.cmbVendor.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Type Id").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
                End If
                UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "PO" Then
                'Ali Faisal : TFS1463 : Add Cost Center Name in drop down
                str = "Select PurchaseOrderID, PurchaseOrderNo, PurchaseOrderDate, UserName, Remarks, IsNull(PurchaseOrderMasterTable.CostCenterId, 0) As CostCenterId,tblDefCostCenter.Name As CostCenter from PurchaseOrderMasterTable INNER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID where VendorId= " & Me.cmbVendor.Value & " And IsNull(Post,0)=1 AND Status='Open' Order By PurchaseOrderID DESC"
                FillUltraDropDown(Me.cmbPO, str)
                Me.cmbPO.Rows(0).Activate()
                Me.cmbPO.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbPO.DisplayLayout.Bands(0).Columns(5).Hidden = True
                'Ali Faisal : TFS1463 : End
                UltraDropDownSearching(cmbPO, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ElseIf Condition = "CostCenter" Then
                str = "SELECT  PurchaseOrderMasterTable.CostCenterId AS Id, tblDefCostCenter.Name FROM PurchaseOrderMasterTable INNER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID Where PurchaseOrderMasterTable.PurchaseOrderId = " & Me.cmbPO.Value & ""
                FillDropDown(Me.cmbCostCenter, str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fill valus in controls in edit mode from history grid.
    ''' </summary>
    ''' <remarks>03-July-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub EditRecords()
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            IsEditMode = True
            Dim ProgressId As Integer = 0I
            ProgressId = Me.grdSaved.CurrentRow.Cells("Id").Value.ToString
            ContractId = Me.grdSaved.CurrentRow.Cells("ContractId").Value.ToString
            Me.txtDocNo.Text = Me.grdSaved.CurrentRow.Cells("DocNo").Value.ToString
            Me.dtpDocDate.Value = CType(Me.grdSaved.CurrentRow.Cells("DocDate").Value, Date)
            Me.cmbVendor.Value = Me.grdSaved.CurrentRow.Cells("VendorId").Value.ToString
            Me.cmbPO.Value = Me.grdSaved.CurrentRow.Cells("POId").Value.ToString
            Me.cmbItem.Value = Me.grdSaved.CurrentRow.Cells("ItemId").Value.ToString
            SendForApproval = Me.grdSaved.CurrentRow.Cells("SendForApproval").Value.ToString
            DisplayDetail(Me.grdSaved.CurrentRow.Cells("Id").Value.ToString)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fillmodel to get data of Master and Detail records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New ItemTaskProgressBE
            objModel.Detail = New List(Of ItemTaskProgressDetailBE)
            'Dim AccountId As Integer = Val(getConfigValueByType("PurchaseDebitAccount").ToString)
            Dim AccountId As Integer = Val(getConfigValueByType("PurchaseAccountVendorPM").ToString)
            If Me.btnSave.Text = "&Save" Then
                objModel.ProgressNo = GetDocumentNo()
            Else
                objModel.ProgressNo = Me.txtDocNo.Text
                Dim ProgressId As Integer = 0I
                ProgressId = Me.grdSaved.CurrentRow.Cells("Id").Value.ToString
                objModel.ProgressId = ProgressId
                objModel.SendForApproval = SendForApproval
            End If
            objModel.ProgressDate = Me.dtpDocDate.Value
            objModel.VendorId = Me.cmbVendor.Value
            objModel.POId = Me.cmbPO.Value
            objModel.ItemId = Me.cmbItem.Value
            objModel.AccountId = AccountId
            If Me.cmbCostCenter.SelectedValue > 0 Then
                objModel.CostCenterId = Me.cmbCostCenter.SelectedValue
            Else
                objModel.CostCenterId = 0
            End If
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New ItemTaskProgressDetailBE
                Detail.DetailId = Val(Row.Cells(grdenm.Id).Value.ToString)
                Detail.ContractId = Val(Row.Cells(grdenm.ContractId).Value.ToString)
                Detail.TaskId = Val(Row.Cells(grdenm.TaskId).Value.ToString)
                Detail.TaskTitle = Row.Cells(grdenm.TaskTitle).Value.ToString
                Detail.TaskDetail = Row.Cells(grdenm.TaskDetail).Value.ToString
                Detail.TaskRate = Val(Row.Cells(grdenm.TaskRate).Value.ToString)
                Detail.TaskUnit = Row.Cells(grdenm.TaskUnit).Value.ToString
                Detail.PreviousProgress = Val(Row.Cells(grdenm.PrevProgress).Value.ToString)
                Detail.CurrentProgress = Val(Row.Cells(grdenm.CurrentProgress).Value.ToString)
                Detail.ApprovedProgress = Val(Row.Cells(grdenm.ApprovedProgress).Value.ToString)
                Detail.NetProgress = Val(Row.Cells(grdenm.NetProgress).Value.ToString)
                Detail.PendingProgress = Val(Row.Cells(grdenm.PendingProgress).Value.ToString)
                Detail.NetValue = Val(Row.Cells(grdenm.NetValue).Value.ToString)
                Detail.Qty = Val(Row.Cells(grdenm.Qty).Value.ToString)
                Detail.Measurment = Val(Row.Cells(grdenm.Measurment).Value.ToString)
                Detail.ContractValue = Val(Row.Cells(grdenm.ContractValue).Value.ToString)
                objModel.Detail.Add(Detail)
            Next
            ApplySecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : To show all saved records in history gidr.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Task.Id, Task.DocNo, Task.DocDate, Task.VendorId, COA.detail_title AS VendorName, Task.POId, PO.PurchaseOrderNo AS PONo, Task.ItemId, Article.ArticleDescription AS ItemName, ISNULL(Task.Approved, 0) AS Approved, tblVendorContractMaster.ContractId, ISNULL(Task.SendForApproval,0) As SendForApproval, ISNULL(Task.Rejected,0) As Rejected, ISNULL(Task.Voucher,0) AS Voucher FROM tblTaskProgressMaster AS Task INNER JOIN vwCOADetail AS COA ON Task.VendorId = COA.coa_detail_id INNER JOIN PurchaseOrderMasterTable AS PO ON Task.POId = PO.PurchaseOrderId INNER JOIN ArticleDefView AS Article ON Task.ItemId = Article.ArticleId INNER JOIN tblVendorContractMaster ON Task.VendorId = tblVendorContractMaster.VendorId AND Task.POId = tblVendorContractMaster.POId AND Task.ItemId = tblVendorContractMaster.ItemId"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("Id").Visible = False
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("POId").Visible = False
            Me.grdSaved.RootTable.Columns("ItemId").Visible = False
            Me.grdSaved.RootTable.Columns("ContractId").Visible = False
            Me.grdSaved.RootTable.Columns("Approved").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : To fill the detail grid.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub DisplayDetail(ByVal Id As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Me.btnSave.Text = "&Save" Then
                'str = "SELECT     0 AS Id, 0 AS ProgressId, IsNull(VCD.Id,0) ContractDetailId, VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, CASE WHEN SUM(TPD.NetProgress) > 0 THEN SUM(Isnull(TPD.CurrentProgress, 0)) ELSE 0 END AS PrevProgress, 0 AS CurrentProgress, 0 AS ApprovedProgress, CASE WHEN SUM(TPD.NetProgress) > 0 THEN SUM(Isnull(TPD.CurrentProgress, 0)) ELSE 0 END AS NetProgress, 0 AS PendingProgress, 0 AS NetValue, VCD.NetValue AS ContractValue FROM tblTaskProgressMaster AS TPM INNER JOIN tblTaskProgressDetail AS TPD ON TPM.Id = TPD.ProgressId RIGHT OUTER JOIN tblVendorContractDetail AS VCD INNER JOIN tblVendorContractMaster AS VCM ON VCD.ContractId = VCM.ContractId ON TPM.POId = VCM.POId AND TPM.ItemId = VCM.ItemId AND TPD.ContractDetailId = VCD.Id Where VCM.ItemId= " & Id & " And VCM.POId = " & Me.cmbPO.Value & " GROUP BY VCD.Id,VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, VCD.NetValue HAVING (SUM(ISNULL(TPD.CurrentProgress, 0)) < 100)"
                'Ali Faisal : TFS1463 : Show all tasks instead of pending or less than 100% progress
                'str = "SELECT 0 AS Id, 0 AS ProgressId, IsNull(VCD.Id,0) ContractDetailId, VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, CASE WHEN SUM(TPD.NetProgress) > 0 THEN SUM(Isnull(TPD.CurrentProgress, 0)) ELSE 0 END AS PrevProgress, 0 AS CurrentProgress, 0 AS ApprovedProgress, CASE WHEN SUM(TPD.NetProgress) > 0 THEN SUM(Isnull(TPD.CurrentProgress, 0)) ELSE 0 END AS NetProgress, 0 AS PendingProgress, 0 AS NetValue, PurchaseOrderMasterTable.PurchaseOrderQty Qty, VCD.TotalMeasurment Measurment, VCD.NetValue AS ContractValue, CASE WHEN SUM(TPD.PrevProgress) = 100 THEN 'Completed' ELSE 'InProgress' END AS Status FROM PurchaseOrderMasterTable INNER JOIN tblVendorContractDetail AS VCD INNER JOIN tblVendorContractMaster AS VCM ON VCD.ContractId = VCM.ContractId ON PurchaseOrderMasterTable.PurchaseOrderId = VCM.POId LEFT OUTER JOIN tblTaskProgressMaster AS TPM INNER JOIN tblTaskProgressDetail AS TPD ON TPM.Id = TPD.ProgressId ON VCM.POId = TPM.POId AND VCM.ItemId = TPM.ItemId AND VCD.Id = TPD.ContractDetailId Where VCM.ItemId= " & Id & " And VCM.POId = " & Me.cmbPO.Value & " GROUP BY VCD.Id,VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, VCD.NetValue, PurchaseOrderMasterTable.PurchaseOrderQty, VCD.TotalMeasurment ORDER BY PrevProgress ASC"
                'str = "SELECT 0 AS Id, 0 AS ProgressId, IsNull(VCD.Id,0) ContractDetailId, VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, CASE WHEN SUM(TPD.NetProgress) > 0 THEN CONVERT(decimal(18," & DecimalPointInValue & "),SUM(Isnull(TPD.CurrentProgress, 0))) ELSE 0 END AS PrevProgress, CONVERT(decimal(18," & DecimalPointInValue & "),0) AS CurrentProgress, CONVERT(decimal(18," & DecimalPointInValue & "),0) AS ApprovedProgress, CASE WHEN SUM(TPD.NetProgress) > 0 THEN CONVERT(Decimal(18," & DecimalPointInValue & "),SUM(Isnull(TPD.CurrentProgress, 0))) ELSE 0 END AS NetProgress, 0 AS PendingProgress, 0 AS NetValue, CONVERT(Decimal(18," & DecimalPointInQty & "),ISNULL(VCD.Qty,0)) Qty, VCD.TotalMeasurment Measurment, VCD.NetValue AS ContractValue, CASE WHEN SUM(TPD.PrevProgress) = 100 THEN 'Completed' ELSE 'InProgress' END AS Status FROM PurchaseOrderMasterTable INNER JOIN tblVendorContractDetail AS VCD INNER JOIN tblVendorContractMaster AS VCM ON VCD.ContractId = VCM.ContractId ON PurchaseOrderMasterTable.PurchaseOrderId = VCM.POId LEFT OUTER JOIN tblTaskProgressMaster AS TPM INNER JOIN tblTaskProgressDetail AS TPD ON TPM.Id = TPD.ProgressId ON VCM.POId = TPM.POId AND VCM.ItemId = TPM.ItemId AND VCD.Id = TPD.ContractDetailId Where VCM.ItemId= " & Id & " And VCM.POId = " & Me.cmbPO.Value & " GROUP BY VCD.Id,VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, VCD.NetValue, VCD.Qty, VCD.TotalMeasurment ORDER BY ContractDetailId ASC"
                str = "SELECT 0 AS Id, 0 AS ProgressId, IsNull(VCD.Id,0) ContractDetailId, VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, CASE WHEN SUM(TPD.NetProgress) > 0 THEN (CONVERT(decimal(18," & DecimalPointInValue & "),SUM(Isnull(TPD.NetValue, 0)))/VCD.NetValue)*100 ELSE 0 END AS PrevProgress, CONVERT(decimal(18," & DecimalPointInValue & "),0) AS CurrentProgress, CONVERT(decimal(18," & DecimalPointInValue & "),0) AS ApprovedProgress, CASE WHEN SUM(TPD.NetProgress) > 0 THEN (CONVERT(decimal(18," & DecimalPointInValue & "),SUM(Isnull(TPD.NetValue, 0)))/VCD.NetValue)*100 ELSE 0 END AS NetProgress, 0 AS PendingProgress, 0 AS NetValue, CONVERT(Decimal(18," & DecimalPointInQty & "),ISNULL(VCD.Qty,0)) Qty, VCD.TotalMeasurment Measurment, VCD.NetValue AS ContractValue, CASE WHEN SUM(TPD.PrevProgress) = 100 THEN 'Completed' ELSE 'InProgress' END AS Status FROM PurchaseOrderMasterTable INNER JOIN tblVendorContractDetail AS VCD INNER JOIN tblVendorContractMaster AS VCM ON VCD.ContractId = VCM.ContractId ON PurchaseOrderMasterTable.PurchaseOrderId = VCM.POId LEFT OUTER JOIN tblTaskProgressMaster AS TPM INNER JOIN tblTaskProgressDetail AS TPD ON TPM.Id = TPD.ProgressId ON VCM.POId = TPM.POId AND VCM.ItemId = TPM.ItemId AND VCD.Id = TPD.ContractDetailId Where VCM.ItemId= " & Id & " And VCM.POId = " & Me.cmbPO.Value & " GROUP BY VCD.Id,VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, VCD.NetValue, VCD.Qty, VCD.TotalMeasurment ORDER BY ContractDetailId ASC"
            Else
                'str = "SELECT TPD.Id, TPD.ProgressId, TPD.ContractDetailId, TPD.TaskId, TPD.TaskTitle, VCD.TaskDetail, TPD.TaskRate, VCD.TaskUnit, TPD.PrevProgress, TPD.CurrentProgress, 0 AS ApprovedProgress, TPD.NetProgress, 0 AS PendingProgress, 0 AS NetValue, VCD.NetValue AS ContractValue FROM tblVendorContractDetail AS VCD INNER JOIN tblTaskProgressDetail AS TPD ON VCD.TaskId = TPD.TaskId RIGHT OUTER JOIN tblTaskProgressMaster AS TPM ON TPD.ProgressId = TPM.Id Where TPM.ItemId= " & Me.cmbItem.Value & " And TPM.POId = " & Me.cmbPO.Value & " And TPD.ProgressId = " & Id & " And VCD.ContractId= " & ContractId & ""
                'Ali Faisal : TFS1463 : Add new Columns
                str = "SELECT Id, ProgressId, ContractDetailId, TaskId, TaskTitle, TaskDetail, TaskRate, TaskUnit, PrevProgress, CurrentProgress, ApprovedProgress, NetProgress, 0 AS PendingProgress, NetValue, IsNull(POQty,0) Qty, IsNull(TotalMeasurment,0) Measurment, IsNull(ContractValue,0) ContractValue, CASE WHEN SUM(PrevProgress) = 100 THEN 'Completed' ELSE 'InProgress' END AS Status FROM tblTaskProgressDetail WHERE ProgressId = " & Id & " GROUP BY Id, ProgressId, ContractDetailId, TaskId, TaskTitle, TaskDetail, TaskRate, TaskUnit, PrevProgress, CurrentProgress, ApprovedProgress, NetProgress, NetValue, POQty, TotalMeasurment, ContractValue ORDER BY ContractDetailId ASC"
                'Ali Faisal : TFS1463 : End
            End If
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            dt.Columns("CurrentProgress").Expression = "NetProgress-PrevProgress"
            dt.Columns("PendingProgress").Expression = "100-PrevProgress-CurrentProgress"
            dt.Columns("NetValue").Expression = "(ContractValue * CurrentProgress)/100"
            dt.Columns("ApprovedProgress").Expression = "(PrevProgress * ContractValue)/100"
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Validate that all controls are selected before save and update functions etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbVendor.Value = 0 Then
                msg_Information("Please select any Vendor")
                Return False
            ElseIf Me.cmbPO.Value = 0 Then
                msg_Information("Please select any PO")
                Return False
            ElseIf Me.cmbItem.Value = 0 Then
                msg_Information("Please select any Item")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = GetDocumentNo()
            Me.dtpDocDate.Value = Now
            Me.cmbVendor.Value = 0
            Me.cmbPO.Value = 0
            Me.cmbItem.Value = 0
            DisplayDetail(-1)
            GetAllRecords()
            Me.btnSave.Text = "&Save"
            Me.btnSave.Enabled = True
            ApplySecurityRights()
            Me.cmbCostCenter.Enabled = False
            Me.cmbCostCenter.SelectedValue = 0
            RecordCounter("New")
            RecordCounter("Sent")
            RecordCounter("Rejected")
            RecordCounter("Approved")
            ProgressCalculation()
            CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Get doc no for next document.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("PP-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblTaskProgressMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("PP-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblTaskProgressMaster", "DocNo")
            Else
                Return GetNextDocNo("PP-", 6, "tblTaskProgressMaster", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Calls the save function from DAL to save the records of master and details.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New ItemTaskProgressDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel, ProgressId) = True Then
                    'Insert Activity Log by Ali Faisal on 30-June-2017
                    SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                    Return True
                Else
                    Return False
                End If
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
    ''' <summary>
    ''' Ali Faisal : Calls the update function from DAL to modify the records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New ItemTaskProgressDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    'Insert Activity Log by Ali Faisal on 30-June-2017
                    SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                    Return True
                Else
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Set short keys for save and refresh buttons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub frmItemTaskProgress_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If Me.btnSave.Enabled = True Then
                    Me.btnSave_ButtonClick(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                Me.btnNew_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F5 Then
                Me.btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Load from
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub frmItemTaskProgress_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
            FillCombos("Item")
            FillCombos("Vendor")
            FillCombos("PO")
            ApplySecurityRights()
            Me.cmbVendor.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Reset all controls on New button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            FillCombos("Item")
            FillCombos("Vendor")
            FillCombos("PO")
            DisplayDetail(-1)
            GetAllRecords()
            Me.btnEdit.Visible = False
            Me.btnDelete.Visible = False
            Me.btnPrint.Visible = False
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Filter Open PO for selected Vendor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub cmbVendor_ValueChanged(sender As Object, e As EventArgs) Handles cmbVendor.ValueChanged
        Try
            FillCombos("PO")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Filter Items and cost center for selected PO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub cmbPO_ValueChanged(sender As Object, e As EventArgs) Handles cmbPO.ValueChanged
        Try
            FillCombos("Item")
            FillCombos("CostCenter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Refresh controls.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-July-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim VendorId As Integer = 0I
            Dim POId As Integer = 0I
            Dim ItemId As Integer = 0I
            VendorId = Me.cmbVendor.Value
            POId = Me.cmbPO.Value
            ItemId = Me.cmbItem.Value

            FillCombos("Vendor")
            Me.cmbVendor.Value = VendorId

            FillCombos("PO")
            Me.cmbPO.Value = POId

            FillCombos("Item")
            Me.cmbItem.Value = ItemId
            If Me.grdSaved.RowCount > 0 Then
                DisplayDetail(Me.grdSaved.CurrentRow.Cells("Id").Value.ToString)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbItem.ValueChanged
        Try
            DisplayDetail(Me.cmbItem.Value)
            ProgressCalculation()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Save and Update the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub btnSave_ButtonClick(sender As Object, e As EventArgs) Handles btnSave.ButtonClick
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSave.Text = "&Save" Then
                If Save() = True Then
                    msg_Information(str_informSave)
                    btnNew_Click(Nothing, Nothing)
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() = True Then
                    msg_Information(str_informUpdate)
                    btnNew_Click(Nothing, Nothing)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>30-June-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then
                msg_Information(str_informDelete)
                btnNew_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete the record from grid and also from database.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-July-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New ItemTaskProgressDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grd.CurrentRow.Cells("Id").Value.ToString))
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-July-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Me.btnSave.Text = "&Update"
            EditRecords()
            ProgressCalculation()
            Me.UltraTabControl1.Tabs(0).Selected = True
            ApplySecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit records on double click of History grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>29-June-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Verify that entered value does not exceeds from 100%
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-July-2017 TFS1014 : Ali Faisal</remarks>
    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        Try
            Me.grd.UpdateData()
            If Me.grd.CurrentRow.Cells(grdenm.NetProgress).Value > 100 Then
                msg_Information("Entered value exceeds the limit of 100%")
                Me.grd.CurrentRow.Cells(grdenm.NetProgress).Value = 100
            End If
            'If Me.grd.CurrentRow.Cells(grdenm.NetProgress).Value < 0 Or Me.grd.CurrentRow.Cells(grdenm.NetProgress).Value < NetValue Then
            '    msg_Information("Entered value is less than the Net Progress")
            '    Me.grd.CurrentRow.Cells(grdenm.NetProgress).Value = NetValue
            'End If
            ProgressCalculation()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Project Task Progress"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSaveAndSendForApproval_Click(sender As Object, e As EventArgs) Handles btnSaveAndSendForApproval.Click
        Try
            If GetProgressId(Me.txtDocNo.Text) = 0 Then
                btnSave_ButtonClick(Nothing, Nothing)
            End If
            btnSendForApproval_Click(Nothing, Nothing)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSendForApproval_Click(sender As Object, e As EventArgs) Handles btnSendForApproval.Click
        Try
            Dim str As String = ""
            Dim conn As New SqlConnection(SQLHelper.CON_STR)
            Dim trans As SqlTransaction
            Try
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Me.Cursor = Cursors.WaitCursor
                FillModel()
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                trans = conn.BeginTransaction
                'Update Master records
                If IsEditMode = True Then
                    str = "Update tblTaskProgressMaster Set SendForApproval = '1' Where Id = " & objModel.ProgressId & ""
                Else
                    str = "Update tblTaskProgressMaster Set SendForApproval = '1' Where Id = " & ProgressId & ""
                End If
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                trans.Commit()
                ReSetControls()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            Finally
                Me.Cursor = Cursors.Default
                Me.lblProgress.Visible = False
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.FormattingRow
        Try
            If e.Row.Cells("Approved").Value = False AndAlso e.Row.Cells("SendForApproval").Value = False AndAlso e.Row.Cells("Voucher").Value = False AndAlso e.Row.Cells("Rejected").Value = False Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LightYellow
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("Approved").Value = True Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LawnGreen
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("SendForApproval").Value = True AndAlso e.Row.Cells("Approved").Value = False AndAlso e.Row.Cells("Rejected").Value = False AndAlso e.Row.Cells("Voucher").Value = False Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LightPink
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("Rejected").Value = True AndAlso e.Row.Cells("SendForApproval").Value = False AndAlso e.Row.Cells("Approved").Value = False AndAlso e.Row.Cells("Voucher").Value = False Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.LightGray
                e.Row.RowStyle = rowstyle
            End If
            If e.Row.Cells("Voucher").Value = True AndAlso e.Row.Cells("Approved").Value = True AndAlso e.Row.Cells("Rejected").Value = False AndAlso e.Row.Cells("SendForApproval").Value = False Then
                Dim rowstyle As New Janus.Windows.GridEX.GridEXFormatStyle
                rowstyle.BackColor = Color.Wheat
                e.Row.RowStyle = rowstyle
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Function GetProgressId(ByVal Code As String) As Long
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "Select Id From tblTaskProgressMaster Where DocNo = '" & Me.txtDocNo.Text & "'"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub RecordCounter(ByVal Condition As String)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Condition = "New" Then
                str = "Select Count(Id) Id from tblTaskProgressMaster Where Approved = '0' And SendForApproval = '0' And Rejected = '0' And Voucher = '0'"
                dt = GetDataTable(str)
                dt.AcceptChanges()
                lblNewEntry.Text = dt.Rows(0).Item(0).ToString
            ElseIf Condition = "Sent" Then
                str = "Select Count(Id) Id from tblTaskProgressMaster Where SendForApproval = '1'"
                dt = GetDataTable(str)
                dt.AcceptChanges()
                lblSent.Text = dt.Rows(0).Item(0).ToString
            ElseIf Condition = "Rejected" Then
                str = "Select Count(Id) Id from tblTaskProgressMaster Where Rejected = '1'"
                dt = GetDataTable(str)
                dt.AcceptChanges()
                lblRejected.Text = dt.Rows(0).Item(0).ToString
                lblRejected.ForeColor = Color.Red
            ElseIf Condition = "Approved" Then
                str = "Select Count(Id) Id from tblTaskProgressMaster Where Approved = '1'"
                dt = GetDataTable(str)
                dt.AcceptChanges()
                lblApproved.Text = dt.Rows(0).Item(0).ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If e.Tab.Index = 0 And btnSave.Text = "&Save" Then
                Me.btnDelete.Visible = False
                Me.btnPrint.Visible = False
            Else
                Me.btnDelete.Visible = True
                Me.btnPrint.Visible = True
            End If
            If e.Tab.Index = 0 Then
                Me.btnEdit.Visible = False
                Me.btnSave.Visible = True
                Me.btnNew.Visible = True
                Me.CtrlGrdBar2.Visible = True
                Me.CtrlGrdBar3.Visible = False
            Else
                Me.btnEdit.Visible = True
                Me.btnSave.Visible = False
                Me.btnNew.Visible = False
                Me.CtrlGrdBar2.Visible = False
                Me.CtrlGrdBar3.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellValueChanged
        Try
            NetValue = Me.grd.CurrentRow.Cells(grdenm.NetProgress).Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtnByCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnByCode.CheckedChanged, rbtnByName.CheckedChanged
        Try
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '30-June-2017 TFS1014 : Ali Faisal : To change the dispaly member when check is changed
                If Me.rbtnByCode.Checked = True Then
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                Else
                    Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Project Task Progress"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1463 : Row colors change on status based
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grd_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grd.FormattingRow
        Try
            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells(grdenm.PrevProgress).Value = 100 AndAlso e.Row.Cells(grdenm.PrevProgress).Text <> "" Then
                rowStyle.BackColor = Color.LightGray
                e.Row.RowStyle = rowStyle
                e.Row.BeginEdit()
                e.Row.Cells("Status").Text = "Completed"
            ElseIf e.Row.Cells(grdenm.PrevProgress).Text <> "" Then
                e.Row.BeginEdit()
                e.Row.Cells("Status").Text = "InProgress"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : TFS1463 : Print crystal report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@ProgressId", Me.grdSaved.CurrentRow.Cells("Id").Value.ToString)
            ShowReport("rptItemTaskProgress")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub ProgressCalculation()
        Try
            Me.txtPreviousProgress.Text = 0
            Me.txtCurrentProgress.Text = 0
            Me.txtTotalProgress.Text = 0
            If Me.grd.RowCount > 0 Then
                Me.txtPreviousProgress.Text = Math.Round(Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.ApprovedProgress), Janus.Windows.GridEX.AggregateFunction.Sum)) / Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.ContractValue), Janus.Windows.GridEX.AggregateFunction.Sum)) * 100, DecimalPointInValue)
                Me.txtCurrentProgress.Text = Math.Round(Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum)) / Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.ContractValue), Janus.Windows.GridEX.AggregateFunction.Sum)) * 100, DecimalPointInValue)
                Me.txtTotalProgress.Text = Math.Round(Val(Me.txtPreviousProgress.Text) + Val(Me.txtCurrentProgress.Text), DecimalPointInValue)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class