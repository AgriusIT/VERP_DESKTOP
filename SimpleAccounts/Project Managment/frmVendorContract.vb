'05-July-2017 TFS1040 : Ali Faisal : Add new form to save update and delete records through this form.
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class frmVendorContract
    Implements IGeneral
    Dim objDAL As VendorContractDAL
    Dim objModel As VendorContractBE
    Dim Approved As Boolean
    Dim ContractValue As Double
    ''' <summary>
    ''' Ali Faisal : set indexes of detail grid to use name of columns from enum instead of from query.
    ''' </summary>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Enum grdenm
        Id
        ItemId
        TaskId
        TaskTitle
        TaskDetail
        TaskUnit
        TaskRate
        Qty
        TotalMeasurment
        NetValue
    End Enum
    ''' <summary>
    ''' Ali Faisal : Apply grid setings to hide some columns and also apply filters on specific columns.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
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
            Me.grd.RootTable.Columns(grdenm.ItemId).Visible = False
            Me.grd.RootTable.Columns(grdenm.TaskId).Visible = False
            'Me.grd.RootTable.Columns(grdenm.TaskTitle).EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grd.RootTable.Columns(grdenm.TaskTitle).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Me.grd.RootTable.Columns(grdenm.TaskDetail).EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grd.RootTable.Columns(grdenm.TaskDetail).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Me.grd.RootTable.Columns(grdenm.TaskUnit).EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grd.RootTable.Columns(grdenm.TaskUnit).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            'Me.grd.RootTable.Columns(grdenm.Qty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            'Me.grd.RootTable.Columns(grdenm.Qty).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdenm.TaskRate).CellStyle.BackColor = Color.LightYellow
            Me.grd.RootTable.Columns(grdenm.TotalMeasurment).CellStyle.BackColor = Color.LightYellow
            Me.grd.RootTable.Columns(grdenm.Qty).CellStyle.BackColor = Color.LightYellow
            Me.grd.RootTable.Columns(grdenm.Qty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(grdenm.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.Qty).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.TotalMeasurment).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.TotalMeasurment).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.TotalMeasurment).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.TaskRate).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.TaskRate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.TaskRate).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.NetValue).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.NetValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdenm.NetValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Delete").TextAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns("Delete").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Center
            Me.grd.RootTable.Columns(grdenm.TotalMeasurment).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdenm.NetValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            'Ali Faisal : TFS1460 : Add column totals decimal config
            Me.grd.RootTable.Columns(grdenm.TotalMeasurment).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.NetValue).TotalFormatString = "N" & DecimalPointInValue
            'Ali Faisal : TFS1460 : End
            Me.grd.RootTable.Columns(grdenm.TaskRate).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdenm.TaskRate).TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    ''' <summary>
    ''' Ali Faisal : Add security rights for standard user to enable/disable buttons on right based. 
    ''' </summary>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnEdit.Enabled = True
                'Ali Faisal : TFS1535 : Voucher Rights
                Me.chkApprove.Visible = True
                Me.chkApprove.Checked = True
                Exit Sub
            End If
            Me.Visible = False
            Me.BtnSave.Enabled = False
            Me.BtnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            'Ali Faisal : TFS1535 : Voucher Rights
            Me.chkApprove.Visible = False
            Me.chkApprove.Checked = False
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
                    'Ali Faisal : TFS1535 : Voucher Rights
                ElseIf Rights.Item(i).FormControlName = "Approve" Then
                    Me.chkApprove.Visible = True
                    Me.chkApprove.Checked = True
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
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New VendorContractDAL
            FillModel()
            If objDAL.Delete(Val(Me.grdSaved.CurrentRow.Cells("ContractId").Value.ToString)) = True Then
                objDAL.DeleteVoucher(objModel.VoucherId)
                DeleteTerms()
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
    ''' Ali Faisal : FillCombos of items, Vendor list, open PO, Terms and Bank ACs.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Item" Then
                str = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination,ArticleDefView.PackQty, ISNULL(ArticleDefView.SalePrice, 0) AS Price, ISNULL(ArticleDefView.PurchasePrice, 0) AS PurchasePrice, ArticleDefView.SortOrder, ArticleDefView.ArticleGroupName AS Dept, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleLpoName AS Brand, ISNULL(ArticleDefView.Cost_Price, 0) AS [Cost Price], ISNULL(ArticleDefView.TradePrice, 0) AS [Trade Price] FROM ArticleDefView INNER JOIN PurchaseOrderDetailTable ON ArticleDefView.ArticleId = PurchaseOrderDetailTable.ArticleDefId LEFT OUTER JOIN tblVendorContractMaster ON ArticleDefView.ArticleId = tblVendorContractMaster.TermId"
                If Me.btnSave.Text = "&Save" Then
                    str += " where Active=1 And PurchaseOrderId = " & Me.cmbPO.Value & " And POId Not In (Select POId From tblVendorContractMaster Where POId = " & Me.cmbPO.Value & ")"
                Else
                    str += " where Active=1 And PurchaseOrderId = " & Me.cmbPO.Value & ""
                End If
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
                'Ali Faisal : TFS1460 : Alter Drop down to get cost center name
                'str = "SELECT PurchaseOrderID, PurchaseOrderNo, PurchaseOrderDate, UserName, Remarks, IsNull(CostCenterId, 0) As CostCenterId, PurchaseOrderAmount FROM PurchaseOrderMasterTable  WHERE VendorId= " & Me.cmbVendor.Value & " And IsNull(Post,0)=1 AND Status='Open' Order By PurchaseOrderID DESC"
                'Ali Faisal : TFS1519 : Cost center in dropdown
                str = "SELECT PurchaseOrderID, PurchaseOrderNo, PurchaseOrderDate, UserName, Remarks, IsNull(tblDefCostCenter.CostCenterId, 0) As CostCenterId, tblDefCostCenter.Name CostCenter, PurchaseOrderAmount, PurchaseOrderQty FROM PurchaseOrderMasterTable LEFT OUTER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID WHERE VendorId= " & Me.cmbVendor.Value & " And IsNull(Post,0)=1 AND Status='Open' Order By PurchaseOrderID DESC"
                FillUltraDropDown(Me.cmbPO, str)
                Me.cmbPO.Rows(0).Activate()
                Me.cmbPO.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbPO.DisplayLayout.Bands(0).Columns(5).Hidden = True
                Me.cmbPO.DisplayLayout.Bands(0).Columns(7).Hidden = True
                'Ali Faisal : TFS1460 : End
                UltraDropDownSearching(cmbPO, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

            ElseIf Condition = "Terms" Then
                FillDropDown(Me.cmbTermCondition, "select * From tblTermsAndConditionType", True)
            ElseIf Condition = "CostCenter" Then
                str = "SELECT  PurchaseOrderMasterTable.CostCenterId AS Id, tblDefCostCenter.Name FROM PurchaseOrderMasterTable INNER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID Where PurchaseOrderMasterTable.PurchaseOrderId = " & Me.cmbPO.Value & ""
                FillDropDown(Me.cmbCostCenter, str, False)
            ElseIf Condition = "Bank" Then
                str = "Select coa_detail_id,detail_title from vwCOADetail Where account_type='Bank'"
                FillDropDown(Me.cmbBank, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fill valus in controls in edit mode from history grid.
    ''' </summary>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Sub EditRecords()
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Dim ContractId As Integer = 0I
            ContractId = Me.grdSaved.CurrentRow.Cells("ContractId").Value.ToString
            Me.txtDocNo.Text = Me.grdSaved.CurrentRow.Cells("DocNo").Value.ToString
            Me.dtpDocDate.Value = CType(Me.grdSaved.CurrentRow.Cells("DocDate").Value, Date)
            Me.cmbVendor.Value = Me.grdSaved.CurrentRow.Cells("VendorId").Value.ToString
            Me.cmbPO.Value = Me.grdSaved.CurrentRow.Cells("POId").Value.ToString
            Me.cmbItem.Value = Me.grdSaved.CurrentRow.Cells("ItemId").Value.ToString
            Me.cmbBank.SelectedValue = Val(Me.grdSaved.CurrentRow.Cells("BankId").Value.ToString)
            Me.txtChequeNo.Text = Me.grdSaved.CurrentRow.Cells("ChequeNo").Value.ToString
            Me.txtChequeAmount.Text = Me.grdSaved.CurrentRow.Cells("ChequeAmount").Value.ToString
            If IsDBNull(Me.grdSaved.CurrentRow.Cells("ChequeDate").Value) = True Then
                Me.dtpChequeDate.Checked = False
            Else
                Me.dtpChequeDate.Value = Me.grdSaved.CurrentRow.Cells("ChequeDate").Value
            End If
            Me.txtChequeDetails.Text = Me.grdSaved.CurrentRow.Cells("ChequeDetails").Value.ToString
            DisplayDetail(Val(Me.grdSaved.CurrentRow.Cells("ContractId").Value.ToString))
            Me.txtRetentionPercentage.Text = Val(Me.grdSaved.CurrentRow.Cells("RetentionPercentage").Value.ToString)
            Me.txtRetentionValue.Text = Val(Me.grdSaved.CurrentRow.Cells("RetentionValue").Value.ToString)
            Me.txtMobilizationPerBill.Text = Val(Me.grdSaved.CurrentRow.Cells("MobilizationPerBill").Value.ToString)
            Me.txtMobilizationPercentage.Text = Val(Me.grdSaved.CurrentRow.Cells("MobilizationPercentage").Value.ToString)
            Me.txtMobilizationValue.Text = Val(Me.grdSaved.CurrentRow.Cells("MobilizationValue").Value.ToString)
            Dim TermsAndConditionsId As Integer = Val(Me.grdSaved.CurrentRow.Cells("TermId").Value.ToString)
            Me.cmbTermCondition.SelectedValue = TermsAndConditionsId
            Me.btnDelete.Visible = True
            '05-July-2017 TFS1040 : Ali Faisal : Fill the Terms in Grid
            Dim str As String = ""
            Dim dt As DataTable
            str = "Select Heading,Details from tblVendorContractTerms Where TermId=" & Me.grdSaved.CurrentRow.Cells("TermId").Value.ToString & " And ContractId = " & Me.grdSaved.CurrentRow.Cells("ContractId").Value.ToString & ""
            dt = GetDataTable(str)
            Me.grdTerms.DataSource = dt
            Me.grdTerms.RetrieveStructure()
            If Me.grdTerms.RootTable.Columns.Contains("Delete") = False Then
                Me.grdTerms.RootTable.Columns.Add("Delete")
                Me.grdTerms.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grdTerms.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grdTerms.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grdTerms.RootTable.Columns("Delete").Key = "Delete"
                Me.grdTerms.RootTable.Columns("Delete").Caption = "Action"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fillmodel to get data of Master and Detail records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New VendorContractBE
            objModel.Detail = New List(Of VendorContractDetailBE)
            Dim RetentionAccountId As Integer = Convert.ToInt32((getConfigValueByType("RetentionAccount").ToString))
            Dim MobilizationAccountId As Integer = Convert.ToInt32(Val(getConfigValueByType("MobilizationAccount").ToString))
            objModel.RetentionAccountId = RetentionAccountId
            objModel.MobilizationAccountId = MobilizationAccountId
            If Me.btnSave.Text = "&Save" Then
                objModel.ContractNo = GetDocumentNo()
            Else
                objModel.ContractNo = Me.txtDocNo.Text
                objModel.ContractId = Me.grdSaved.CurrentRow.Cells("ContractId").Value.ToString
            End If
            objModel.VoucherId = GetVoucherId("frmVendorContract", objModel.ContractNo)
            objModel.ContractDate = Me.dtpDocDate.Value
            objModel.VendorId = Me.cmbVendor.Value
            objModel.VendorName = Me.cmbVendor.Text
            objModel.POId = Me.cmbPO.Value
            objModel.POQty = Val(Me.cmbPO.ActiveRow().Cells(8).Value)
            objModel.CostCenterId = Me.cmbCostCenter.SelectedValue
            objModel.CostCenter = Me.cmbCostCenter.Text
            objModel.ItemId = Me.cmbItem.Value
            objModel.ItemName = Me.cmbItem.ActiveRow().Cells(2).Value.ToString
            objModel.TermId = Me.cmbTermCondition.SelectedValue
            ContractValue = Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum)
            objModel.ContractValue = ContractValue
            objModel.RetentionPercentage = Me.txtRetentionPercentage.Text
            objModel.RetentionValue = Me.txtRetentionValue.Text
            objModel.MobilizationPerBill = Me.txtMobilizationPerBill.Text
            objModel.MobilizationPercentage = Me.txtMobilizationPercentage.Text
            objModel.MobilizationValue = Me.txtMobilizationValue.Text
            objModel.BankId = Me.cmbBank.SelectedValue
            If Me.txtChequeNo.Text = "" Then
                objModel.ChequeNo = 0
            Else
                objModel.ChequeNo = Me.txtChequeNo.Text
            End If
            If Me.txtChequeAmount.Text = "" Then
                objModel.ChequeAmount = 0
            Else
                objModel.ChequeAmount = Me.txtChequeAmount.Text
            End If
            If Me.dtpChequeDate.Checked = True Then
                objModel.ChequeDate = Me.dtpChequeDate.Value
            Else
                objModel.ChequeDate = DateTime.MinValue
            End If
            objModel.ChequeDetails = Me.txtChequeDetails.Text
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grd.GetDataRows
                Dim Detail As New VendorContractDetailBE
                Detail.DetailId = Val(Row.Cells(grdenm.Id).Value.ToString)
                Detail.TaskId = Val(Row.Cells(grdenm.TaskId).Value.ToString)
                Detail.TaskTitle = Row.Cells(grdenm.TaskTitle).Value.ToString
                Detail.TaskDetail = Row.Cells(grdenm.TaskDetail).Value.ToString
                Detail.TaskRate = Val(Row.Cells(grdenm.TaskRate).Value.ToString)
                Detail.TaskUnit = Row.Cells(grdenm.TaskUnit).Value.ToString
                Detail.Qty = Val(Row.Cells(grdenm.Qty).Value.ToString)
                Detail.TotalMeasurment = Val(Row.Cells(grdenm.TotalMeasurment).Value.ToString)
                Detail.NetValue = Val(Row.Cells(grdenm.NetValue).Value.ToString)
                objModel.Detail.Add(Detail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : To show all saved records in history grid.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Contract.ContractId, Contract.DocNo, Contract.DocDate, Contract.ItemId, Article.ArticleDescription AS ItemName, Contract.VendorId, COA.detail_title AS VendorName, Contract.POId, PO.PurchaseOrderNo AS PONo, Contract.TermId, Contract.RetentionPercentage, Contract.RetentionValue, Contract.MobilizationPerBill, Contract.MobilizationPercentage, Contract.MobilizationValue, Contract.BankId, Contract.ChequeNo,Contract.ChequeAmount, Contract.ChequeDate, Contract.ChequeDetails FROM vwCOADetail AS COA INNER JOIN tblVendorContractMaster AS Contract INNER JOIN ArticleDefView AS Article ON Contract.ItemId = Article.ArticleId INNER JOIN PurchaseOrderMasterTable AS PO ON Contract.POId = PO.PurchaseOrderId ON COA.coa_detail_id = Contract.VendorId Order By Contract.ContractId DESC"
            dt = GetDataTable(str)
            Me.grdSaved.DataSource = dt
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.RootTable.Columns("ContractId").Visible = False
            Me.grdSaved.RootTable.Columns("VendorId").Visible = False
            Me.grdSaved.RootTable.Columns("POId").Visible = False
            Me.grdSaved.RootTable.Columns("ItemId").Visible = False
            Me.grdSaved.RootTable.Columns("TermId").Visible = False
            Me.grdSaved.RootTable.Columns("BankId").Visible = False
            Me.grdSaved.RootTable.Columns("ChequeNo").FormatString = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : To fill the detail grid.
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Sub DisplayDetail(ByVal Id As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Me.btnSave.Text = "&Save" Then
                'Ali Faisal : TFS1460 : change measurment in floats
                str = "SELECT 0 AS Id, ATS.ItemId, ATS.Id AS TaskId, ATS.TaskTitle, ATS.TaskDetail, ATS.TaskUnit, CONVERT(Decimal(18," & DecimalPointInValue & "), ATS.TaskRate) TaskRate, CONVERT(Decimal(18," & DecimalPointInQty & "), PO.Qty) Qty, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) TotalMeasurment, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) NetValue FROM ArticleDefTaskDetails AS ATS LEFT OUTER JOIN(SELECT ArticleDefId, Sz1 AS Qty FROM PurchaseOrderDetailTable WHERE (PurchaseOrderId = " & cmbPO.Value & ")) AS PO ON ATS.ItemId = PO.ArticleDefId  WHERE (ATS.ItemId = " & Id & ")"
                'Ali Faisal : TFS1460 : End
            Else
                str = "SELECT VCD.Id, VCM.ItemId, VCD.Id AS TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskUnit, CONVERT(Decimal(18," & DecimalPointInValue & "), VCD.TaskRate) TaskRate, CONVERT(Decimal(18," & DecimalPointInQty & "), IsNull(VCD.Qty,0)) AS Qty, CONVERT(Decimal(18," & DecimalPointInValue & "), VCD.TotalMeasurment) TotalMeasurment, CONVERT(Decimal(18," & DecimalPointInValue & "), VCD.NetValue) NetValue FROM tblVendorContractMaster AS VCM INNER JOIN tblVendorContractDetail AS VCD ON VCM.ContractId = VCD.ContractId INNER JOIN PurchaseOrderDetailTable ON VCM.POId = PurchaseOrderDetailTable.PurchaseOrderId WHERE VCM.ItemId =" & Me.cmbItem.Value & " And VCM.POId = " & Me.cmbPO.Value & " And VCD.ContractId = " & Id & ""
            End If
            dt = GetDataTable(str)
            dt.Columns(grdenm.NetValue).Expression = "TaskRate * Qty * TotalMeasurment"
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Verify the controls are selected before save or update etc.
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
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
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
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
            Me.btnEdit.Visible = False
            Me.btnDelete.Visible = False
            Me.cmbCostCenter.Enabled = False
            Me.cmbCostCenter.SelectedValue = 0
            Me.txtRetentionPercentage.Text = Val(0)
            Me.txtRetentionValue.Text = Val(0)
            Me.txtMobilizationPercentage.Text = Val(0)
            Me.txtMobilizationPerBill.Text = Val(0)
            Me.txtMobilizationValue.Text = Val(0)
            Me.cmbBank.SelectedValue = Val(0)
            Me.txtChequeNo.Text = ""
            Me.dtpChequeDate.Value = Now
            Me.dtpChequeDate.Checked = False
            Me.txtChequeAmount.Text = 0
            Me.txtChequeDetails.Text = ""
            Me.txtPOAmount.Text = 0
            CtrlGrdBar2_Load(Nothing, Nothing)
            CtrlGrdBar3_Load(Nothing, Nothing)
            ApplySecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Get doc no for next document.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("VC-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblVendorContractMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("VC-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblVendorContractMaster", "DocNo")
            Else
                Return GetNextDocNo("VC-", 6, "tblVendorContractMaster", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Call the save function from DAL to save the records of master and details.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New VendorContractDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    If Me.chkApprove.Checked = True Then
                        objDAL.SaveVoucher(objModel)
                    End If
                    SaveTerms()
                    'Insert Activity Log by Ali Faisal on 05-July-2017
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
    ''' Ali Faisal : Call the update function from DAL to modify the records.
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New VendorContractDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    objDAL.UpdateVoucher(objModel)
                    SaveTerms()
                    'Insert Activity Log by Ali Faisal on 05-July-2017
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
    ''' Ali Faisal : Set ShortKeys to perform actions on save and refresh
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub frmItemTaskProgress_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                If Me.btnSave.Enabled = True Then
                    Me.btnSave_Click(Nothing, Nothing)
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
    ''' Ali Faisal : Load form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub frmItemTaskProgress_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
            FillCombos("Item")
            FillCombos("Vendor")
            FillCombos("PO")
            FillCombos("Terms")
            FillCombos("Bank")
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
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
            FillCombos("Item")
            FillCombos("Vendor")
            FillCombos("PO")
            FillCombos("Terms")
            FillCombos("Bank")
            DisplayDetail(-1)
            GetAllRecords()
            Me.UltraTabControl1.Tabs(0).Selected = True
            Me.UltraTabControl2.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fill PO combo to filter PO for Selected Vendor.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub cmbVendor_ValueChanged(sender As Object, e As EventArgs) Handles cmbVendor.ValueChanged
        Try
            FillCombos("PO")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Fill Item combo to filter Items used in Selected PO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub cmbPO_ValueChanged(sender As Object, e As EventArgs) Handles cmbPO.ValueChanged
        Try
            FillCombos("Item")
            FillCombos("CostCenter")
            'Ali Faisal : TFS1460 : Set PO Net Amount to text box from drop down column
            If Me.cmbPO.Value > 0 Then
                Me.txtPOAmount.Text = Math.Round(Me.cmbPO.ActiveRow().Cells(7).Value, DecimalPointInValue)
                If IsNumeric(Me.txtPOAmount.Text) Then
                    Me.txtPOAmount.Text = String.Format("{0:#,##0.00}", CInt(Me.txtPOAmount.Text))
                End If
            End If
            'Ali Faisal : TFS1460 : End
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Refresh controls.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Dim VendorId As Integer = 0I
            Dim POId As Integer = 0I
            Dim ItemId As Integer = 0I
            Dim TermId As Integer = 0I
            Dim BankId As Integer = 0I
            VendorId = Me.cmbVendor.Value
            POId = Me.cmbPO.Value
            ItemId = Me.cmbItem.Value
            TermId = Me.cmbTermCondition.SelectedValue
            BankId = Me.cmbBank.SelectedValue

            FillCombos("Vendor")
            Me.cmbVendor.Value = VendorId

            FillCombos("PO")
            Me.cmbPO.Value = POId

            FillCombos("Item")
            Me.cmbItem.Value = ItemId

            FillCombos("Terms")
            Me.cmbTermCondition.SelectedValue = TermId

            FillCombos("Bank")
            Me.cmbBank.SelectedValue = BankId
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbItem_ValueChanged(sender As Object, e As EventArgs) Handles cmbItem.ValueChanged
        Try
            DisplayDetail(Me.cmbItem.Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Save and Update the records.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
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
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
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
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New VendorContractDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grd.CurrentRow.Cells(grdenm.Id).Value.ToString))
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
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            Me.btnSave.Text = "&Update"
            EditRecords()
            ApplySecurityRights()
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit the selected row from history on double click of grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            btnEdit_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Select terms and conditions and also fill grid for selected term
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub cmbTermCondition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTermCondition.SelectedIndexChanged
        Try
            If Not Me.cmbTermCondition.SelectedItem Is Nothing Then
                Me.txtTerms_And_Condition.Text = ""
                Me.txtTerms_And_Condition.Text = CType(Me.cmbTermCondition.SelectedItem, DataRowView).Row.Item("Term_Condition").ToString()
                Dim str As String = ""
                Dim dt As DataTable
                str = "Select Heading,Details from tblTermsAndConditionDetail Where TermId=" & Me.cmbTermCondition.SelectedValue & ""
                dt = GetDataTable(str)
                Me.grdTerms.DataSource = dt
                Me.grdTerms.RetrieveStructure()
                If Me.grdTerms.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdTerms.RootTable.Columns.Add("Delete")
                    Me.grdTerms.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdTerms.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdTerms.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdTerms.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdTerms.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Save Terms and Conditions in tblVendorContractTerms
    ''' </summary>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Sub SaveTerms()
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            If Me.grdTerms.RowCount > 0 Then
                str = "Delete from tblVendorContractTerms Where TermId=" & objModel.TermId & " and ContractId=" & objModel.ContractId & " "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdTerms.GetDataRows
                    If Me.btnSave.Text = "&Save" Then
                        str = "Insert into tblVendorContractTerms (TermId,ContractId,Heading,Details) Values (" & Me.cmbTermCondition.SelectedValue & ",ident_current('tblVendorContractMaster'),N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
                    Else
                        str = "Insert into tblVendorContractTerms (TermId,ContractId,Heading,Details) Values (" & Me.cmbTermCondition.SelectedValue & "," & objModel.ContractId & ",N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
                    End If
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                Next
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            conn.Close()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete Terms and Conditions
    ''' </summary>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Public Sub DeleteTerms()
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            If Me.grdTerms.RowCount > 0 Then
                str = "Delete from tblVendorContractTerms Where TermId=" & objModel.TermId & " and ContractId=" & objModel.ContractId & " "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                trans.Commit()
            End If
        Catch ex As Exception
            trans.Rollback()
            conn.Close()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete the terms record from Terms Grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub grdTerms_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTerms.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Change RetentionValue on leave of Retention%
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub txtRetentionPercentage_Leave(sender As Object, e As EventArgs) Handles txtRetentionPercentage.Leave
        Try
            If Me.txtRetentionPercentage.Text = 0 Then
                Me.txtRetentionValue.Text = 0
                Exit Sub
            End If
            ContractValue = 0
            ContractValue = Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.txtRetentionValue.Text = Math.Round(ContractValue * Me.txtRetentionPercentage.Text / 100, DecimalPointInValue)
            'If IsNumeric(Me.txtRetentionValue.Text) Then
            '    Me.txtRetentionValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtRetentionValue.Text))
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Retention% change on leave of RetentionValue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub txtRetentionValue_Leave(sender As Object, e As EventArgs) Handles txtRetentionValue.Leave
        Try
            If Me.txtRetentionValue.Text = 0 Then
                Me.txtRetentionPercentage.Text = 0
                Exit Sub
            End If
            ContractValue = Math.Round(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum), DecimalPointInValue)
            Me.txtRetentionPercentage.Text = Math.Round(Me.txtRetentionValue.Text * 100 / ContractValue, DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : MobilizationValue change on leave of Mobilization%
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub txtMobilizationPercentage_Leave(sender As Object, e As EventArgs) Handles txtMobilizationPercentage.Leave
        Try
            If Me.txtMobilizationPercentage.Text = 0 Then
                Me.txtMobilizationValue.Text = 0
                Me.txtMobilizationPerBill.Text = 0
                Exit Sub
            End If
            ContractValue = Math.Round(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum), DecimalPointInValue)
            Me.txtMobilizationValue.Text = Math.Round(ContractValue * Me.txtMobilizationPercentage.Text / 100, DecimalPointInValue)
            Me.txtMobilizationPerBill.Text = Me.txtMobilizationPercentage.Text
            'If IsNumeric(Me.txtMobilizationValue.Text) Then
            '    Me.txtMobilizationValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtMobilizationValue.Text))
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Mobilization% change on leave of MobilizationValue
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>05-July-2017 TFS1040 : Ali Faisal</remarks>
    Private Sub txtMobilizationValue_Leave(sender As Object, e As EventArgs) Handles txtMobilizationValue.Leave
        Try
            If Me.txtMobilizationValue.Text = 0 Then
                Me.txtMobilizationPercentage.Text = 0
                Exit Sub
            End If
            ContractValue = Math.Round(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum), DecimalPointInValue)
            Me.txtMobilizationPercentage.Text = Math.Round(Me.txtMobilizationValue.Text * 100 / ContractValue, DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle & Chr(10) & "Vendor Contract List"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnEdit.Visible = False
                Me.btnSave.Visible = True
                Me.CtrlGrdBar2.Visible = False
                Me.CtrlGrdBar3.Visible = True
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnEdit.Visible = True
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
                Me.CtrlGrdBar2.Visible = True
                Me.CtrlGrdBar3.Visible = False
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub txtChequeAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtChequeAmount.KeyPress
    '    Try
    '        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
    '            e.Handled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtMobilizationPercentage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMobilizationPercentage.KeyPress
    '    Try
    '        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
    '            e.Handled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtMobilizationValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMobilizationValue.KeyPress
    '    Try
    '        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
    '            e.Handled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtRetentionPercentage_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetentionPercentage.KeyPress
    '    Try
    '        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
    '            e.Handled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtRetentionValue_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetentionValue.KeyPress
    '    Try
    '        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
    '            e.Handled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    'Private Sub txtMobilizationPerBill_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMobilizationPerBill.KeyPress
    '    Try
    '        If Char.IsNumber(e.KeyChar) = False AndAlso e.KeyChar <> ControlChars.Back Then
    '            e.Handled = True
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub rbtnByName_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnByName.CheckedChanged
        Try
            If Me.rbtnByCode.Checked = True Then
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(1).Column.Key.ToString
            Else
                Me.cmbItem.DisplayMember = Me.cmbItem.Rows(0).Cells(2).Column.Key.ToString
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Vendor Contract"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@ContractId", Me.grdSaved.CurrentRow.Cells("ContractId").Value.ToString)
            ShowReport("rptVendorContract")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtMobilizationValue_TextChanged(sender As Object, e As EventArgs) Handles txtMobilizationValue.TextChanged
        Try
            If IsNumeric(Me.txtMobilizationValue.Text) Then
                'Me.txtMobilizationValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtMobilizationValue.Text))
                Me.txtMobilizationValue.Text = Me.txtMobilizationValue.Text
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRetentionValue_TextChanged(sender As Object, e As EventArgs) Handles txtRetentionValue.TextChanged
        Try
            If IsNumeric(Me.txtRetentionValue.Text) Then
                'Me.txtRetentionValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtRetentionValue.Text))
                Me.txtRetentionValue.Text = Me.txtRetentionValue.Text
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
        Try
            If Me.txtRetentionPercentage.Text = 0 Then
                Me.txtRetentionValue.Text = 0
                Exit Sub
            End If
            grd.UpdateData()
            ContractValue = 0
            ContractValue = Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.txtRetentionValue.Text = Math.Round(ContractValue * Me.txtRetentionPercentage.Text / 100, DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class