'03-Oct-2018 TFS4629 : Saad Afzaal : Add new form to save update and delete records through this form.
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient

Public Class frmInterimPaymentCertificate
    Implements IGeneral
    Dim objDAL As IntermPaymentCertificateDAL
    Dim objModel As IntermPaymentCertificateBE
    Dim ProgressId As Integer
    Public ContractId As Integer
    Public SendForApproval As Boolean
    Dim NetValue As Double
    Dim IsEditMode As Boolean = False
    Public IntermPaymentCertificateId As Integer
    Public editModel As IntermPaymentCertificateBE

    ''' <summary>
    ''' Saad Afzaal : set indexes of detail grid to use name of columns from enum instead of from query.
    ''' </summary>
    ''' <remarks>'03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Enum grdenm
        Id
        ProgressId
        ContractId
        TaskId
        TaskTitle
        TaskDetail
        TaskRate
        TaskUnit
        NetValue
        NetProgress
        CurrentValue
        CurrentProgress
        PreValue
        PrevProgress
        ApprovedProgress
        PendingProgress
        Qty
        Measurment
        ContractValue
    End Enum

    ''' <summary>
    ''' Saad Afzaal : Apply grid seeting to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>'03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Me.grdIPC.RootTable.Columns.Contains("Delete") = False Then
                Me.grdIPC.RootTable.Columns.Add("Delete")
                Me.grdIPC.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdIPC.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdIPC.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdIPC.RootTable.Columns("Delete").Key = "Delete"
                Me.grdIPC.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grdIPC.RootTable.Columns(grdenm.Id).Visible = False
            Me.grdIPC.RootTable.Columns(grdenm.ProgressId).Visible = False
            Me.grdIPC.RootTable.Columns(grdenm.TaskId).Visible = False
            Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress).Visible = False
            Me.grdIPC.RootTable.Columns(grdenm.ContractId).Visible = False
            Me.grdIPC.RootTable.Columns(grdenm.TaskTitle).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.TaskTitle).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.TaskDetail).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.TaskDetail).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.TaskUnit).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.TaskUnit).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.PrevProgress).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.PrevProgress).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.PreValue).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.PreValue).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.CurrentProgress).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.CurrentProgress).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.CurrentValue).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.CurrentValue).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Me.grdIPC.RootTable.Columns(grdenm.NetProgress).CellStyle.BackColor = Color.LightYellow
            Me.grdIPC.RootTable.Columns(grdenm.NetProgress).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.NetProgress).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.PendingProgress).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.PendingProgress).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.ContractValue).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.ContractValue).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.NetValue).CellStyle.BackColor = Color.LightYellow
            Me.grdIPC.RootTable.Columns(grdenm.NetValue).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.NetValue).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Saad Afzaal : TFS4629 : Apply grid settings
            Me.grdIPC.RootTable.Columns(grdenm.Measurment).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.Measurment).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grdIPC.RootTable.Columns(grdenm.Qty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdIPC.RootTable.Columns(grdenm.Qty).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Saad Afzaal : TFS4629

            Me.grdIPC.RootTable.Columns(grdenm.NetValue).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.NetValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.NetValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.PrevProgress).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.PrevProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.PrevProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.PreValue).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.PreValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.PreValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.CurrentProgress).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.CurrentProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.CurrentProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.CurrentValue).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.CurrentValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.CurrentValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.PendingProgress).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.PendingProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.PendingProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.NetProgress).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.NetProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.NetProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            'Saad Afzaal : TFS4629 : Apply grid settings

            Me.grdIPC.RootTable.Columns(grdenm.ContractValue).FormatString = "N" & DecimalPointInQty
            Me.grdIPC.RootTable.Columns(grdenm.ContractValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.ContractValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.Measurment).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.Measurment).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.Measurment).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.ContractValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdIPC.RootTable.Columns(grdenm.NetValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdIPC.RootTable.Columns(grdenm.Measurment).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress).FormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdIPC.RootTable.Columns(grdenm.NetValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress).TotalFormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.ContractValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.Measurment).TotalFormatString = "N" & DecimalPointInValue
            Me.grdIPC.RootTable.Columns(grdenm.TaskRate).TotalFormatString = "N" & DecimalPointInValue

            Me.grdIPC.RootTable.Columns(grdenm.Qty).FormatString = "N" & DecimalPointInQty
            Me.grdIPC.RootTable.Columns(grdenm.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdIPC.RootTable.Columns(grdenm.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdIPC.RootTable.Columns(grdenm.PrevProgress).Caption = "Prev %"
            Me.grdIPC.RootTable.Columns(grdenm.CurrentProgress).Caption = "Current %"
            Me.grdIPC.RootTable.Columns(grdenm.NetProgress).Caption = "Net %"
            Me.grdIPC.RootTable.Columns(grdenm.PendingProgress).Caption = "Pending %"


            Me.grdIPC.UpdateData()
            'SaaD Afzaal : TFS4629 : End
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>'03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.SaveAndSendForApprovalToolStripMenuItem.Enabled = True
                Me.DeleteToolStripMenuItem.Enabled = True
                Me.btnPrint.Enabled = True
                Me.NewToolStripMenuItem.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.SaveAndSendForApprovalToolStripMenuItem.Enabled = False
            Me.DeleteToolStripMenuItem.Enabled = False
            Me.btnPrint.Enabled = False
            Me.CtrlGrdBar3.mGridPrint.Enabled = False
            Me.CtrlGrdBar3.mGridExport.Enabled = False
            Me.CtrlGrdBar3.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If Me.btnSave.Tag = "Save" Then
                        btnSave.Enabled = True
                    End If
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If Me.btnSave.Tag = "Update" Then
                        btnSave.Enabled = True
                    End If
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.DeleteToolStripMenuItem.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    Me.btnPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar3.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar3.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar3.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Calls the Delete function from DAL to remove the data of selected row.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>'03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New IntermPaymentCertificateDAL
            If objDAL.Delete(Me.IntermPaymentCertificateId) = True Then      'Val(Me.grdSaved.CurrentRow.Cells("Id").Value.ToString)
                'Insert Activity Log by Saad Afzaal on 03-Oct-2018
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
    ''' Saad Afzaal : FillCombos of items, Customer list and open SO here.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Item" Then
                str = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination, ArticleDefView.PackQty, ISNULL(ArticleDefView.SalePrice, 0) AS Price, ISNULL(ArticleDefView.PurchasePrice, 0) AS PurchasePrice, ArticleDefView.SortOrder, ArticleDefView.ArticleGroupName AS Dept, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleLpoName AS Brand, ISNULL(ArticleDefView.Cost_Price, 0) AS [Cost Price], ISNULL(ArticleDefView.TradePrice, 0) AS [Trade Price] FROM ArticleDefView INNER JOIN SalesOrderDetailTable ON ArticleDefView.ArticleId = SalesOrderDetailTable.ArticleDefId"
                str += " where Active=1 And SalesOrderId = " & Me.cmbSO.Value & ""
                FillUltraDropDown(Me.cmbItem, str)
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    '03-Oct-2018 TFS4629 : Saad Afzaal : To change the dispaly member when check is changed
                    If Me.rbtnByCode.Checked = True Then
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
                    Else
                        Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
                    End If
                End If
            ElseIf Condition = "Customer" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id AS Id, dbo.vwCOADetail.detail_title as Name,dbo.vwCOADetail.detail_Code as [Code], dbo.tblListState.StateName as State, dbo.tblListCity.CityName as City,  " & _
                            "dbo.tblListTerritory.TerritoryName as Territory, IsNull(tblCustomer.CustomerTypes,0) as [Type Id], dbo.vwCOADetail.Contact_Email as Email,dbo.vwCOADetail.Contact_Phone as Phone, dbo.vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
                str += " AND   (dbo.vwCOADetail.account_type = 'Customer') "
                str += " AND vwCOADetail.Active=1"
                FillUltraDropDown(cmbCustomer, str)
                Me.cmbCustomer.Rows(0).Activate()
                If Me.cmbCustomer.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Type Id").Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
                End If
            ElseIf Condition = "SO" Then
                'Saad Afzaal : TFS4629 : Add Cost Center Name in drop down
                str = "Select SalesOrderID, SalesOrderNo, SalesOrderDate, UserName, Remarks, IsNull(SalesOrderMasterTable.CostCenterId, 0) As CostCenterId,tblDefCostCenter.Name As CostCenter from SalesOrderMasterTable INNER JOIN tblDefCostCenter ON SalesOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID where VendorId= " & Me.cmbCustomer.Value & " And IsNull(Posted,0)=1 AND Status='Open' And IsNull(SalesOrderMasterTable.CostCenterId, 0) > 0 Order By SalesOrderID DESC"
                FillUltraDropDown(Me.cmbSO, str)
                Me.cmbSO.Rows(0).Activate()
                Me.cmbSO.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbSO.DisplayLayout.Bands(0).Columns(5).Hidden = True
                'Saad Afzaal : TFS4629 : End
            ElseIf Condition = "CostCenter" Then
                str = "SELECT  SalesOrderMasterTable.CostCenterId AS Id, tblDefCostCenter.Name FROM SalesOrderMasterTable INNER JOIN tblDefCostCenter ON SalesOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID Where SalesOrderMasterTable.SalesOrderId = " & Me.cmbSO.Value & ""
                FillDropDown(Me.cmbCostCenter, str, False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Fill valus in controls in edit mode from history grid.
    ''' </summary>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub EditRecords()
        Try
            If Me.grdIPC.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            IsEditMode = True
            Dim ProgressId As Integer = 0I
            ProgressId = Me.IntermPaymentCertificateId
            ContractId = Me.ContractId
            Me.txtDocNo.Text = Me.editModel.ProgressNo
            Me.dtpDocDate.Value = Me.editModel.ProgressDate
            Me.cmbCustomer.Value = Me.editModel.CustomerId
            Me.cmbSO.Value = Me.editModel.SOId
            Me.cmbItem.Value = Me.editModel.ItemId
            SendForApproval = Me.SendForApproval
            DisplayDetail(Me.IntermPaymentCertificateId)
            ProgressCalculation()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Fillmodel to get data of Master and Detail records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New IntermPaymentCertificateBE
            objModel.Detail = New List(Of IntermPaymentCertificateDetailBE)
            Dim AccountId As Integer = Val(getConfigValueByType("SalesAccountCustomerPM").ToString)
            If Me.btnSave.Tag = "Save" Then
                objModel.ProgressNo = GetDocumentNo()
            Else
                objModel.ProgressNo = Me.txtDocNo.Text
                Dim ProgressId As Integer = 0I
                ProgressId = Me.IntermPaymentCertificateId   'Me.grdSaved.CurrentRow.Cells("Id").Value.ToString
                objModel.ProgressId = ProgressId
                objModel.SendForApproval = SendForApproval
            End If
            objModel.ProgressDate = Me.dtpDocDate.Value
            objModel.CustomerId = Me.cmbCustomer.Value
            objModel.SOId = Me.cmbSO.Value
            objModel.ItemId = Me.cmbItem.Value
            objModel.AccountId = AccountId
            If Me.cmbCostCenter.SelectedValue > 0 Then
                objModel.CostCenterId = Me.cmbCostCenter.SelectedValue
            Else
                objModel.CostCenterId = 0
            End If
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdIPC.GetDataRows
                Dim Detail As New IntermPaymentCertificateDetailBE
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
    ''' Saad Afzaal : Get doc no for next document.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("IPC-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "IntermPaymentCertificateMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("IPC-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "IntermPaymentCertificateMaster", "DocNo")
            Else
                Return GetNextDocNo("IPC-", 6, "IntermPaymentCertificateMaster", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Saad Afzaal : To fill the detail grid.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub DisplayDetail(ByVal Id As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Me.btnSave.Tag = "Save" Then
                'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
                str = "SELECT 0 AS Id, 0 AS ProgressId, IsNull(VCD.Id,0) ContractDetailId, VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, CASE WHEN SUM(TPD.NetProgress) > 0 THEN ((VCD.NetValue * SUM(Isnull(TPD.CurrentProgress, 0))) / 100) ELSE 0 END AS NetValue, CASE WHEN SUM(TPD.NetProgress) > 0 THEN CONVERT(Decimal(18," & DecimalPointInValue & "),SUM(Isnull(TPD.CurrentProgress, 0))) ELSE 0 END AS NetProgress, 0 AS CurrentValue, CONVERT(decimal(18," & DecimalPointInValue & "),0) AS CurrentProgress, 0 AS PreValue, CASE WHEN SUM(TPD.NetProgress) > 0 THEN SUM(Isnull(TPD.CurrentProgress, 0)) ELSE 0 END AS PrevProgress, 0 AS ApprovedProgress, 0 AS PendingProgress, CONVERT(Decimal(18," & DecimalPointInQty & "),ISNULL(VCD.Qty,0)) Qty, VCD.TotalMeasurment Measurment, VCD.NetValue AS ContractValue, CASE WHEN SUM(TPD.PrevProgress) = 100 THEN 'Completed' ELSE 'InProgress' END AS Status FROM SalesOrderMasterTable INNER JOIN tblCustomerContractDetail AS VCD INNER JOIN tblCustomerContractMaster AS VCM ON VCD.ContractId = VCM.ContractId ON SalesOrderMasterTable.SalesOrderId = VCM.SOId LEFT OUTER JOIN IntermPaymentCertificateMaster AS TPM INNER JOIN IntermPaymentCertificateDetail AS TPD ON TPM.Id = TPD.ProgressId ON VCM.SOId = TPM.SOId AND VCM.ItemId = TPM.ItemId AND VCD.Id = TPD.ContractDetailId Where VCM.ItemId= " & Id & " And VCM.SOId = " & Me.cmbSO.Value & " GROUP BY VCD.Id,VCD.TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskRate, VCD.TaskUnit, VCD.NetValue, VCD.Qty, VCD.TotalMeasurment ORDER BY ContractDetailId ASC"
            Else
                'Saad Afzaal : TFS4629 : Add new Columns
                str = "SELECT Id, ProgressId, ContractDetailId, TaskId, TaskTitle, TaskDetail, TaskRate, TaskUnit, NetValue, NetProgress, 0 AS CurrentValue, CurrentProgress, 0 AS PreValue, PrevProgress, ApprovedProgress, 0 AS PendingProgress, IsNull(SOQty,0) Qty, IsNull(TotalMeasurment,0) Measurment, IsNull(ContractValue,0) ContractValue, CASE WHEN SUM(PrevProgress) = 100 THEN 'Completed' ELSE 'InProgress' END AS Status FROM IntermPaymentCertificateDetail WHERE ProgressId = " & Id & " GROUP BY Id, ProgressId, ContractDetailId, TaskId, TaskTitle, TaskDetail, TaskRate, TaskUnit, PrevProgress, CurrentProgress, ApprovedProgress, NetProgress, NetValue, SOQty, TotalMeasurment, ContractValue ORDER BY ContractDetailId ASC"
                'Saad Afzaal : TFS4629 : End
            End If
            dt = GetDataTable(str)
            Me.grdIPC.DataSource = dt
            Me.grdIPC.RetrieveStructure()
            dt.Columns("CurrentProgress").Expression = "NetProgress-PrevProgress"
            dt.Columns("PendingProgress").Expression = "100-PrevProgress-CurrentProgress"
            dt.Columns("NetProgress").Expression = "NetValue*100/ContractValue"
            dt.Columns("ApprovedProgress").Expression = "(PrevProgress * ContractValue)/100"
            dt.Columns("PreValue").Expression = "(PrevProgress * ContractValue)/100"
            dt.Columns("CurrentValue").Expression = "NetValue-ApprovedProgress"
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Validate that all controls are selected before save and update functions etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbCustomer.Value = 0 Then
                msg_Error("Please select any Customer")
                Return False
            ElseIf Me.cmbSO.Value = 0 Then
                msg_Error("Please select any SO")
                Return False
            ElseIf Me.cmbItem.Value = 0 Then
                msg_Error("Please select any Item")
                Return False
            ElseIf Not Me.grdIPC.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Saad Afzaal : Reset controls to default values.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = GetDocumentNo()
            Me.dtpDocDate.Value = Now
            Me.cmbCustomer.Value = 0
            Me.cmbSO.Value = 0
            Me.cmbItem.Value = 0
            DisplayDetail(-1)
            GetAllRecords()
            Me.btnSave.Tag = "Save"
            Me.btnSave.Enabled = True
            IsEditMode = False
            ApplySecurityRights()
            Me.cmbCostCenter.Enabled = False
            Me.cmbCostCenter.SelectedValue = 0
            RecordCounter("New")
            RecordCounter("Sent")
            RecordCounter("Rejected")
            RecordCounter("Approved")
            Me.txtPreviousPercentage.Text = ""
            Me.txtCurrentPercentage.Text = ""
            Me.txtTotalPercentage.Text = ""
            Me.txtPreviousProgress.Text = ""
            Me.txtCurrentProgress.Text = ""
            Me.txtTotalProgress.Text = ""
            ProgressCalculation()
            CtrlGrdBar3_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Calls the save function from DAL to save the records of master and details.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New IntermPaymentCertificateDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel, ProgressId) = True Then
                    'Insert Activity Log by Saad on 03-Oct-2018
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
    ''' Saad Afzaal : Calls the update function from DAL to modify the records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New IntermPaymentCertificateDAL
            If IsValidate() = True Then
                'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
                If GetApproved(objModel.ProgressId) = False Then
                    If objDAL.Update(objModel) = True Then
                        'Insert Activity Log by Saad Afzaal on 03-Oct-2018
                        SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, Me.txtDocNo.Text, True)
                        Return True
                    Else
                        Return False
                    End If
                Else
                    msg_Error("Sorry record can't be update becuase IPC has been Approved.")
                    Return False
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdIPC.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdIPC.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdIPC.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Project Task Progress"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Set short keys for save and refresh buttons
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub frmIntermPayementCertificate_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If Me.btnSave.Enabled = True Then
                    Me.btnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.Escape Then
                Me.NewToolStripMenuItem_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F5 Then
                Me.RefreshToolStripMenuItem_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Load from
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub frmIntermPayementCertificate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ReSetControls()
            FillCombos("Item")
            FillCombos("Customer")
            FillCombos("SO")
            ApplySecurityRights()
            Me.cmbCustomer.Focus()
            If Me.IntermPaymentCertificateId > 0 Then
                Me.btnSave.Tag = "Update"
                Me.SaveAndSendForApprovalToolStripMenuItem.Visible = False
                EditRecords()
                Me.cmbCustomer.Enabled = False
                Me.cmbSO.Enabled = False
                Me.cmbItem.Enabled = False
                Me.btnPrint.Visible = True
                Me.btnCancel.Location = New Point(725, 7)
                Me.DeleteToolStripMenuItem.Visible = True
                Me.SendForApprovalToolStripMenuItem.Visible = True
            Else
                Me.SendForApproval = False
                Me.ContractId = 0
                Me.btnSave.Tag = "Save"
                Me.SaveAndSendForApprovalToolStripMenuItem.Visible = True
                Me.DeleteToolStripMenuItem.Visible = False
                IsEditMode = False
                Me.cmbCustomer.Enabled = True
                Me.cmbSO.Enabled = True
                Me.cmbItem.Enabled = True
                Me.btnPrint.Visible = False
                Me.btnCancel.Location = New Point(795, 7)
                Me.DeleteToolStripMenuItem.Visible = False
                Me.SendForApprovalToolStripMenuItem.Visible = False
            End If
            Me.dtpDocDate.TabIndex = 0
            UltraDropDownSearching(cmbCustomer, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbSO, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Reset all controls on New button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Try
            ReSetControls()
            FillCombos("Item")
            FillCombos("Customer")
            FillCombos("SO")
            DisplayDetail(-1)
            Me.DeleteToolStripMenuItem.Visible = False
            Me.btnPrint.Visible = False
            Me.btnCancel.Location = New Point(795, 7)
            Me.cmbCustomer.Enabled = True
            Me.cmbSO.Enabled = True
            Me.cmbItem.Enabled = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Filter Open SO for selected Customer
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            FillCombos("SO")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Filter Items and cost center for selected SO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub cmbSO_ValueChanged(sender As Object, e As EventArgs) Handles cmbSO.ValueChanged
        Try
            FillCombos("Item")
            FillCombos("CostCenter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Refresh controls.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        Try
            Dim CustomerId As Integer = 0I
            Dim SOId As Integer = 0I
            Dim ItemId As Integer = 0I
            CustomerId = Me.cmbCustomer.Value
            SOId = Me.cmbSO.Value
            ItemId = Me.cmbItem.Value

            FillCombos("Customer")
            Me.cmbCustomer.Value = CustomerId

            FillCombos("PO")
            Me.cmbSO.Value = SOId

            FillCombos("Item")
            Me.cmbItem.Value = ItemId

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
    ''' Saad Afzaal : Delete the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then
                msg_Information(str_informDelete)
                NewToolStripMenuItem_Click(Nothing, Nothing)
                Me.Close()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Delete the record from grid and also from database.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub grdIPC_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdIPC.ColumnButtonClick
        Try
            objDAL = New IntermPaymentCertificateDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grdIPC.CurrentRow.Cells("Id").Value.ToString))
                Me.grdIPC.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Verify that entered value does not exceeds from 100%
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub grdIPC_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdIPC.CellEdited
        Try
            Me.grdIPC.UpdateData()
            If Me.grdIPC.CurrentRow.Cells(grdenm.NetProgress).Value > 100 Then
                msg_Error("Entered value exceeds the limit of 100%")
                Me.grdIPC.CurrentRow.Cells(grdenm.NetProgress).Value = 100
            End If

            If Me.grdIPC.CurrentRow.Cells(grdenm.NetProgress).Value < Me.grdIPC.CurrentRow.Cells(grdenm.PrevProgress).Value Then
                msg_Error("Entered value must be greater then previous progress")
                Me.grdIPC.CurrentRow.Cells(grdenm.NetProgress).Value = Me.grdIPC.CurrentRow.Cells(grdenm.PrevProgress).Value
            End If

            If Me.grdIPC.CurrentRow.Cells(grdenm.NetValue).Value > Me.grdIPC.CurrentRow.Cells(grdenm.ContractValue).Value Then
                msg_Error("You exceeded the Contract Value")
                Me.grdIPC.CurrentRow.Cells(grdenm.NetValue).Value = Me.grdIPC.CurrentRow.Cells(grdenm.ContractValue).Value
            End If

            If Me.grdIPC.CurrentRow.Cells(grdenm.CurrentProgress).Value < 0 Then
                Me.grdIPC.CurrentRow.Cells(grdenm.NetValue).Value = Me.grdIPC.CurrentRow.Cells(grdenm.ContractValue).Value
            End If

            ProgressCalculation()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Save and Update the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>03-Oct-2018 TFS4629 : Saad Afzaal</remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSave.Tag = "Save" Then
                If Save() = True Then
                    msg_Information(str_informSave)
                    NewToolStripMenuItem_Click(Nothing, Nothing)
                    Me.Close()
                End If
            Else
                If msg_Confirm(str_ConfirmUpdate) = False Then Exit Sub
                If Update1() = True Then
                    msg_Information(str_informUpdate)
                    NewToolStripMenuItem_Click(Nothing, Nothing)
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub SaveAndSendForApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAndSendForApprovalToolStripMenuItem.Click
        Try
            If GetProgressId(Me.txtDocNo.Text) = 0 Then
                btnSave_Click(Nothing, Nothing)
            End If
            SendForApprovalToolStripMenuItem_Click(Nothing, Nothing)
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub SendForApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendForApprovalToolStripMenuItem.Click
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
                    str = "Update IntermPaymentCertificateMaster Set SendForApproval = '1' Where Id = " & objModel.ProgressId & ""
                Else
                    str = "Update IntermPaymentCertificateMaster Set SendForApproval = '1' Where Id = " & ProgressId & ""
                End If
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                trans.Commit()
                ReSetControls()
                Me.Close()
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

    Public Function GetProgressId(ByVal Code As String) As Long
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "Select Id From IntermPaymentCertificateMaster Where DocNo = '" & Me.txtDocNo.Text & "'"
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
                str = "Select Count(Id) Id from IntermPaymentCertificateMaster Where Approved = '0' And SendForApproval = '0' And Rejected = '0' And Voucher = '0'"
                dt = GetDataTable(str)
                dt.AcceptChanges()
                lblNewEntry.Text = dt.Rows(0).Item(0).ToString
            ElseIf Condition = "Sent" Then
                str = "Select Count(Id) Id from IntermPaymentCertificateMaster Where SendForApproval = '1'"
                dt = GetDataTable(str)
                dt.AcceptChanges()
                lblSent.Text = dt.Rows(0).Item(0).ToString
            ElseIf Condition = "Rejected" Then
                str = "Select Count(Id) Id from IntermPaymentCertificateMaster Where Rejected = '1'"
                dt = GetDataTable(str)
                dt.AcceptChanges()
                lblRejected.Text = dt.Rows(0).Item(0).ToString
                lblRejected.ForeColor = Color.Red
            ElseIf Condition = "Approved" Then
                str = "Select Count(Id) Id from IntermPaymentCertificateMaster Where Approved = '1'"
                dt = GetDataTable(str)
                dt.AcceptChanges()
                lblApproved.Text = dt.Rows(0).Item(0).ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdIPC_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdIPC.CellValueChanged
        Try
            NetValue = Me.grdIPC.CurrentRow.Cells(grdenm.NetProgress).Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub rbtnByCode_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnByCode.CheckedChanged, rbtnByName.CheckedChanged
        Try
            If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '03-Oct-2018 TFS4629 : Saad Afzaal : To change the dispaly member when check is changed
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


    ''' <summary>
    ''' 'Saad Afzaal : TFS4629 : Row colors change on status based
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdIPC_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdIPC.FormattingRow
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

    Public Sub ProgressCalculation()
        Try
            Me.txtPreviousProgress.Text = 0
            Me.txtCurrentProgress.Text = 0
            Me.txtTotalProgress.Text = 0
            If Me.grdIPC.RowCount > 0 Then

                Me.txtPreviousPercentage.Text = Math.Round(Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress), Janus.Windows.GridEX.AggregateFunction.Sum)) / Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.ContractValue), Janus.Windows.GridEX.AggregateFunction.Sum)) * 100, DecimalPointInValue)
                Dim _NetValue As Decimal = Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum))
                Dim _ApprovedValue As Decimal = Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress), Janus.Windows.GridEX.AggregateFunction.Sum))
                Dim _CurrentValue As Decimal = _NetValue - _ApprovedValue
                Me.txtCurrentPercentage.Text = Math.Round(Val(_CurrentValue) / Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.ContractValue), Janus.Windows.GridEX.AggregateFunction.Sum)) * 100, DecimalPointInValue)
                Me.txtTotalPercentage.Text = Math.Round(Val(Me.txtPreviousPercentage.Text) + Val(Me.txtCurrentPercentage.Text), DecimalPointInValue)

                Me.txtPreviousProgress.Text = Math.Round(Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.ContractValue), Janus.Windows.GridEX.AggregateFunction.Sum)) * (Val(Me.txtPreviousPercentage.Text) / 100), DecimalPointInValue)
                Me.txtCurrentProgress.Text = Math.Round(Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.ContractValue), Janus.Windows.GridEX.AggregateFunction.Sum)) * (Val(Me.txtCurrentPercentage.Text) / 100), DecimalPointInValue)

                Me.txtPreviousProgress.Text = Math.Round(Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress), DecimalPointInValue)))

                Dim _Net As Decimal = Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.NetValue), DecimalPointInValue)) - Val(Me.grdIPC.GetTotal(Me.grdIPC.RootTable.Columns(grdenm.ApprovedProgress), DecimalPointInValue))
                Me.txtCurrentProgress.Text = Math.Round(_Net, DecimalPointInValue)

                Me.txtTotalProgress.Text = Math.Round(Val(Me.txtPreviousProgress.Text) + Val(Me.txtCurrentProgress.Text), DecimalPointInValue)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            ReSetControls()
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnMore_Click(sender As Object, e As EventArgs) Handles btnMore.Click
        ContextMenuStrip1.Show(btnMore, 0, btnMore.Height)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@IPCId", 1)
            ShowReport("rptInterimPaymentCertificate")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetApproved(ByVal Id As Integer) As Boolean
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Id FROM IntermPaymentCertificateMaster WHERE Approved = '1' AND Id = " & Id & ""
            dt = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return True
                End If
                Return False
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function

End Class