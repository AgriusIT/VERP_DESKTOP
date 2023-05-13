'18-July-2017 TFS# 1091 : Ali Faisal : Add new form to save, update and delete the records from this form.
'Ali Faisal : TFS1537 : Values with come seperation
'Ali Faisal : TFS1537 : Current, previous and total progress
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient
Public Class frmProjectProgressApproval
    Implements IGeneral
    Dim objDAL As ProjectProgressApprovalDAL
    Dim objModel As ProjectProgressApprovalBE
    Dim BillAmount As Double
    Dim AccountId As Integer = 0
    Dim flgPurchaseAccountFrontEnd As Boolean = False
    Dim GLAccountArticleDepartment As Boolean = False
    ''' <summary>
    ''' Ali Faisal : Set indexes for grid
    ''' </summary>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Enum grd
        ApprovalId
        ProgressId
        ProgressNo
        ProgressDate
        VendorId
        VendorName
        POId
        PONo
        ItemDepartmentId
        ItemId
        ItemName
        Approved
        ContractId
        RetentionPercentage
        RetentionValue
        MobilizationPercentage
        MobilizationValue
        CurrentProgress
        BillAmount
        NetAmount
        ContractValue
        ContractMobilization
        TotalDeductions
        Remarks
        PreviousValue
        CurrentValue
    End Enum
    ''' <summary>
    ''' Ali Faisal : Apply grid settings to hide some columns
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdSaved.RootTable.Columns(grd.ApprovalId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ProgressId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.VendorId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.POId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ItemDepartmentId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ItemId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ContractId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CurrentProgress).Visible = False
            Me.grdSaved.RootTable.Columns(grd.RetentionValue).Visible = False
            Me.grdSaved.RootTable.Columns(grd.MobilizationValue).Visible = False
            Me.grdSaved.RootTable.Columns(grd.RetentionValue).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.RetentionValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.RetentionValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.RetentionValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.MobilizationValue).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.MobilizationValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.MobilizationValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.MobilizationValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.BillAmount).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.BillAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.BillAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.BillAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.ContractValue).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.ContractValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.ContractValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.ContractValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.PreviousValue).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.PreviousValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.PreviousValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.PreviousValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.RootTable.Columns(grd.CurrentValue).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.CurrentValue).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CurrentValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.CurrentValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            'Ali Faisal : TFS1469 : apply grid settings
            If Me.cmbPendingApprovals.Text = "Pending Approvals" Then
                Me.grdSaved.RootTable.Columns(grd.ContractMobilization).Visible = False
                Me.grdSaved.RootTable.Columns(grd.NetAmount).Visible = False
            Else
                Me.grdSaved.RootTable.Columns(grd.ContractMobilization).Visible = False
                Me.grdSaved.RootTable.Columns(grd.ContractValue).Visible = False

            End If
            'Ali Faisal : TFS1469 : End
            Me.grdSaved.RootTable.Columns("ContractNo").Visible = False
            Me.grdSaved.RootTable.Columns("CostCenterId").Visible = False
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    ''' <summary>
    ''' Ali Faisal : Apply Security for Standard user
    ''' </summary>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnNew.Enabled = True
                Me.btnEdit.Enabled = True
                Me.btnApprove.Enabled = True
                Me.btnReject.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.BtnSave.Enabled = False
            Me.BtnDelete.Enabled = False
            Me.btnPrint.Enabled = False
            Me.btnApprove.Enabled = False
            Me.btnReject.Enabled = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
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
                ElseIf Rights.Item(i).FormControlName = "Approval" Then
                    Me.btnApprove.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Reject" Then
                    Me.btnReject.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Print" Then
                    Me.CtrlGrdBar1.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Grid Export" Then
                    Me.CtrlGrdBar1.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    Me.CtrlGrdBar1.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Call Delete function from DAL to remove the records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New ProjectProgressApprovalDAL
            If objDAL.Delete(objModel) = True Then
                objDAL.DeleteVoucher(GetVoucherId("frmProjectProgressApproval", objModel.ProgressNo), objModel)
                'Insert Activity Log by Ali Faisal on 08-Aug-2017
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, objModel.ProgressNo, True)
                ReSetControls()
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Fill combos of Vendor, PO, Contract and Progress
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Vendor" Then
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
            ElseIf Condition = "PO" Then
                str = "Select PurchaseOrderID, PurchaseOrderNo, PurchaseOrderDate, UserName, Remarks, IsNull(CostCenterId, 0) As CostCenterId from PurchaseOrderMasterTable where  IsNull(Post,0)=1 AND Status='Open' Order By PurchaseOrderID DESC"
                FillUltraDropDown(Me.cmbPO, str)
                Me.cmbPO.Rows(0).Activate()
                Me.cmbPO.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "Contract" Then
                str = "Select * from tblVendorContractMaster"
                FillUltraDropDown(Me.cmbVendorContract, str)
                Me.cmbVendorContract.Rows(0).Activate()
                Me.cmbVendorContract.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "Progress" Then
                str = "Select * from tblTaskProgressMaster"
                FillUltraDropDown(Me.cmbProgressNo, str)
                Me.cmbProgressNo.Rows(0).Activate()
                Me.cmbProgressNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "CostCenter" Then
                str = "SELECT  PurchaseOrderMasterTable.CostCenterId AS Id, tblDefCostCenter.Name FROM PurchaseOrderMasterTable INNER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID Where PurchaseOrderMasterTable.PurchaseOrderId = " & Me.cmbPO.Value & ""
                FillDropDown(Me.cmbCostCenter, str, False)
            ElseIf Condition = "PurchaseAccount" Then
                FillUltraDropDown(Me.cmbPurchaseAccount, "Select coa_detail_id, detail_title, detail_code From vwCOADetail WHERE detail_title <> ''")
                Me.cmbPurchaseAccount.Rows(0).Activate()
                If Me.cmbPurchaseAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbPurchaseAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbPurchaseAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Account Description"
                    Me.cmbPurchaseAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Account Code"
                    Me.cmbPurchaseAccount.DisplayLayout.Bands(0).Columns(1).Width = 250
                    Me.cmbPurchaseAccount.DisplayLayout.Bands(0).Columns(0).Width = 150
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : FillModel to get all values from controls to model properties
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New ProjectProgressApprovalBE
            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
                GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Else
                GLAccountArticleDepartment = False
            End If
            If flgPurchaseAccountFrontEnd = True AndAlso cmbPurchaseAccount.Value > 0 Then
                AccountId = Me.cmbPurchaseAccount.Value
            ElseIf GLAccountArticleDepartment = True Then
                AccountId = Val(Me.grdSaved.CurrentRow.Cells(grd.ItemDepartmentId).Value)
            Else
                AccountId = Val(getConfigValueByType("PurchaseAccountVendorPM").ToString)
            End If
            Dim RetentionAccountId As Integer = Convert.ToInt32((getConfigValueByType("RetentionAccount").ToString))
            Dim MobilizationAccountId As Integer = Convert.ToInt32(Val(getConfigValueByType("MobilizationAccount").ToString))
            objModel.RetentionAccountId = RetentionAccountId
            objModel.MobilizationAccountId = MobilizationAccountId
            objModel.AccountId = AccountId
            objModel.ApprovalId = 0
            objModel.VendorId = Me.cmbVendor.Value
            objModel.VendorName = Me.cmbVendor.Text
            objModel.POId = Me.cmbPO.Value
            objModel.CostCenterId = Me.cmbCostCenter.SelectedValue
            objModel.CostCenter = Me.cmbCostCenter.Text
            objModel.ContractId = Me.cmbVendorContract.Value
            objModel.ProgressId = Me.cmbProgressNo.Value
            objModel.RetentionPercentage = Val(Me.txtRetentionPercentage.Text)
            'Ali Faisal : TFS1537 : Values with come seperation
            'objModel.RetentionValue = String.Format("{0:###0.00}", CInt(Me.txtRetentionValue.Text))
            'objModel.MobilizationPercentage = Val(Me.txtMobilizationPercentage.Text)
            'objModel.MobilizationValue = String.Format("{0:###0.00}", CInt(Me.txtMobilizationValue.Text))
            'objModel.BillAmount = String.Format("{0:###0.00}", CInt(Me.txtBillAmount.Text))
            'objModel.TotalDeduction = String.Format("{0:###0.00}", CInt(Me.txtDeduction.Text))
            'objModel.NetValue = String.Format("{0:###0.00}", CInt(Me.txtNetValue.Text))
            objModel.RetentionValue = Val(Me.txtRetentionValue.Text)
            objModel.MobilizationPercentage = Val(Me.txtMobilizationPercentage.Text)
            objModel.MobilizationValue = Val(Me.txtMobilizationValue.Text)
            objModel.BillAmount = Val(Me.txtBillAmount.Text)
            objModel.TotalDeduction = Val(Me.txtDeduction.Text)
            objModel.NetValue = Val(Me.txtNetValue.Text)
            'Ali Faisal : TFS1537 : Values with come seperation
            objModel.Remarks = Me.txtApprovalRemarks.Text
            objModel.TotalProgress = Val(Me.txtTotalProgress.Text)
            If Me.grdSaved.RowCount > 0 Then
                objModel.ApprovalId = Val(Me.grdSaved.CurrentRow.Cells(grd.ApprovalId).Value.ToString)
                objModel.ProgressId = Val(Me.grdSaved.CurrentRow.Cells(grd.ProgressId).Value.ToString)
                objModel.ProgressNo = Me.grdSaved.CurrentRow.Cells(grd.ProgressNo).Value.ToString
                objModel.ProgressDate = Me.grdSaved.CurrentRow.Cells(grd.ProgressDate).Value
                objModel.Approved = Me.grdSaved.CurrentRow.Cells(grd.Approved).Value
                objModel.ItemName = Me.grdSaved.CurrentRow.Cells(grd.ItemName).Value.ToString
                objModel.ContractValue = Val(Me.grdSaved.CurrentRow.Cells(grd.ContractValue).Value)
                If Me.btnSave.Enabled = True Then
                    Dim str As String = "Select IsNull(Sum(MobilizationValue),0) MobilizationValue,IsNull(Sum(BillAmount),0) BillAmount From tblProjectProgressApproval Where VendorId = " & Val(Me.grdSaved.CurrentRow.Cells(grd.VendorId).Value) & " And ContractId = " & Me.grdSaved.CurrentRow.Cells(grd.ContractId).Value & ""
                    Dim dt As DataTable = GetDataTable(str)
                    Dim ContractMobilization As Double = 0
                    Dim TotalBillsValue As Double = 0
                    If dt.Rows.Count > 0 Then
                        ContractMobilization = dt.Rows(0).Item(0)
                        TotalBillsValue = dt.Rows(0).Item(1)
                    End If
                    'Ali Faisal : TFS1537 : Values with come seperation
                    'objModel.PreviousAmount = String.Format("{0:###0.00}", CInt(TotalBillsValue))
                    objModel.PreviousAmount = Val(Me.txtPreviousValue.Text)
                End If
                objModel.ContractNo = Me.grdSaved.CurrentRow.Cells("ContractNo").Value
            End If
            objModel.CurrentProgress = Me.txtPreviousProgress.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Get all records Pending and Approved condition based
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>19-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Condition = "PendingApprovals" Then
                'str = "SELECT 0 As ApprovalId, Task.Id AS ProgressId, Task.DocNo AS ProgressNo, Task.DocDate AS ProgressDate, Task.VendorId, COA.detail_title AS VendorName, Task.POId, PO.PurchaseOrderNo AS PONo, Article.SubSubID As ItemDepartmentId, Task.ItemId, Article.ArticleDescription AS ItemName, ISNULL(Task.Approved, 0) AS Approved, tblVendorContractMaster.ContractId, tblVendorContractMaster.RetentionPercentage, tblVendorContractMaster.RetentionValue, tblVendorContractMaster.MobilizationPerBill As MobilizationPercentage, (tblVendorContractMaster.MobilizationPerBill * tblVendorContractMaster.MobilizationValue)/100 As MobilizationValue, SUM(tblTaskProgressDetail.CurrentProgress) AS CurrentProgress, SUM(tblTaskProgressDetail.NetValue) AS BillAmount, 0 AS NetAmount,ContractValue.NetValue As ContractValue,tblVendorContractMaster.MobilizationValue ContractMobilization, tblVendorContractMaster.DocNo AS ContractNo, IsNull(PO.CostCenterId,0) CostCenterId  FROM tblTaskProgressMaster AS Task INNER JOIN vwCOADetail AS COA ON Task.VendorId = COA.coa_detail_id INNER JOIN PurchaseOrderMasterTable AS PO ON Task.POId = PO.PurchaseOrderId INNER JOIN ArticleDefView AS Article ON Task.ItemId = Article.ArticleId INNER JOIN tblVendorContractMaster ON Task.VendorId = tblVendorContractMaster.VendorId AND Task.POId = tblVendorContractMaster.POId AND Task.ItemId = tblVendorContractMaster.ItemId INNER JOIN (Select Sum(NetValue) AS NetValue,ContractId from tblVendorContractDetail Group BY ContractId) AS ContractValue ON ContractValue.ContractId = tblVendorContractMaster.ContractId INNER JOIN tblTaskProgressDetail ON Task.Id = tblTaskProgressDetail.ProgressId WHERE Task.Approved is null or Task.Approved = '0' And Task.SendForApproval = '1' GROUP BY Task.Id, Task.DocNo, Task.DocDate, Task.VendorId, COA.detail_title, Task.POId, PO.PurchaseOrderNo, Article.SubSubID, Task.ItemId, Article.ArticleDescription, ISNULL(Task.Approved, 0), tblVendorContractMaster.ContractId, tblVendorContractMaster.RetentionPercentage, tblVendorContractMaster.RetentionValue, tblVendorContractMaster.MobilizationPerBill, tblVendorContractMaster.MobilizationPercentage, tblVendorContractMaster.MobilizationValue,ContractValue.NetValue, tblVendorContractMaster.DocNo, PO.CostCenterId"
                str = "SELECT 0 As ApprovalId, Task.Id AS ProgressId, Task.DocNo AS ProgressNo, Task.DocDate AS ProgressDate, Task.VendorId, COA.detail_title AS VendorName, Task.POId, PO.PurchaseOrderNo AS PONo, Article.SubSubID As ItemDepartmentId, Task.ItemId, Article.ArticleDescription AS ItemName, ISNULL(Task.Approved, 0) AS Approved, tblVendorContractMaster.ContractId, tblVendorContractMaster.RetentionPercentage, tblVendorContractMaster.RetentionValue, tblVendorContractMaster.MobilizationPerBill As MobilizationPercentage, (tblVendorContractMaster.MobilizationPerBill * tblVendorContractMaster.MobilizationValue)/100 As MobilizationValue, SUM(tblTaskProgressDetail.CurrentProgress) AS CurrentProgress, SUM(tblTaskProgressDetail.NetValue) AS BillAmount, 0 AS NetAmount,ContractValue.NetValue As ContractValue,tblVendorContractMaster.MobilizationValue ContractMobilization, tblVendorContractMaster.DocNo AS ContractNo, IsNull(PO.CostCenterId,0) CostCenterId, SUM(tblTaskProgressDetail.ApprovedProgress) AS PreviousValue, SUM(tblTaskProgressDetail.NetValue) AS CurrentValue  FROM tblTaskProgressMaster AS Task INNER JOIN vwCOADetail AS COA ON Task.VendorId = COA.coa_detail_id INNER JOIN PurchaseOrderMasterTable AS PO ON Task.POId = PO.PurchaseOrderId INNER JOIN ArticleDefView AS Article ON Task.ItemId = Article.ArticleId INNER JOIN tblVendorContractMaster ON Task.VendorId = tblVendorContractMaster.VendorId AND Task.POId = tblVendorContractMaster.POId AND Task.ItemId = tblVendorContractMaster.ItemId INNER JOIN (Select Sum(NetValue) AS NetValue,ContractId from tblVendorContractDetail Group BY ContractId) AS ContractValue ON ContractValue.ContractId = tblVendorContractMaster.ContractId INNER JOIN tblTaskProgressDetail ON Task.Id = tblTaskProgressDetail.ProgressId WHERE Task.Approved is null or Task.Approved = '0' And Task.SendForApproval = '1' GROUP BY Task.Id, Task.DocNo, Task.DocDate, Task.VendorId, COA.detail_title, Task.POId, PO.PurchaseOrderNo, Article.SubSubID, Task.ItemId, Article.ArticleDescription, ISNULL(Task.Approved, 0), tblVendorContractMaster.ContractId, tblVendorContractMaster.RetentionPercentage, tblVendorContractMaster.RetentionValue, tblVendorContractMaster.MobilizationPerBill, tblVendorContractMaster.MobilizationPercentage, tblVendorContractMaster.MobilizationValue,ContractValue.NetValue, tblVendorContractMaster.DocNo, PO.CostCenterId"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings()
            ElseIf Condition = "ApprovedProgress" Then
                'str = "SELECT Approval.ApprovalId, TPM.Id AS ProgressId, TPM.DocNo AS ProgressNo, TPM.DocDate AS ProgressDate, Approval.VendorId, vwCOADetail.detail_title AS VendorName, Approval.POId, PO.PurchaseOrderNo AS PONo, ArticleDefView.SubSubID As ItemDepartmentId, TPM.ItemId, ArticleDefView.ArticleDescription AS ItemName, TPM.Approved, Approval.ContractId, Approval.RetentionPercentage, Approval.RetentionValue, Approval.MobilizationPercentage, Approval.MobilizationValue, Approval.CurrentProgress, Approval.BillAmount, Approval.NetValue, VCM.ContractValue, VCM.MobilizationValue AS ContractMobilization, Approval.TotalDeduction, Approval.Remarks, VCM.DocNo AS ContractNo, IsNull(PO.CostCenterId,0) CostCenterId FROM tblProjectProgressApproval AS Approval INNER JOIN vwCOADetail ON Approval.VendorId = vwCOADetail.coa_detail_id INNER JOIN PurchaseOrderMasterTable AS PO ON Approval.POId = PO.PurchaseOrderId INNER JOIN tblTaskProgressMaster AS TPM ON Approval.ProgressId = TPM.Id INNER JOIN ArticleDefView ON TPM.ItemId = ArticleDefView.ArticleId INNER JOIN tblVendorContractMaster AS VCM ON Approval.ContractId = VCM.ContractId Where TPM.Rejected = '0'"
                str = "SELECT Approval.ApprovalId, TPM.Id AS ProgressId, TPM.DocNo AS ProgressNo, TPM.DocDate AS ProgressDate, Approval.VendorId, vwCOADetail.detail_title AS VendorName, Approval.POId, PO.PurchaseOrderNo AS PONo, ArticleDefView.SubSubID As ItemDepartmentId, TPM.ItemId, ArticleDefView.ArticleDescription AS ItemName, TPM.Approved, Approval.ContractId, Approval.RetentionPercentage, Approval.RetentionValue, Approval.MobilizationPercentage, Approval.MobilizationValue, Approval.CurrentProgress, Approval.BillAmount, Approval.NetValue, VCM.ContractValue, VCM.MobilizationValue AS ContractMobilization, Approval.TotalDeduction, Approval.Remarks, VCM.DocNo AS ContractNo, IsNull(PO.CostCenterId,0) CostCenterId, SUM(TPD.ApprovedProgress) AS PreviousValue, SUM(TPD.NetValue) AS CurrentValue FROM tblProjectProgressApproval AS Approval INNER JOIN vwCOADetail ON Approval.VendorId = vwCOADetail.coa_detail_id INNER JOIN PurchaseOrderMasterTable AS PO ON Approval.POId = PO.PurchaseOrderId INNER JOIN tblTaskProgressMaster AS TPM ON Approval.ProgressId = TPM.Id INNER JOIN ArticleDefView ON TPM.ItemId = ArticleDefView.ArticleId INNER JOIN tblVendorContractMaster AS VCM ON Approval.ContractId = VCM.ContractId INNER JOIN tblTaskProgressDetail AS TPD ON TPM.Id = TPD.ProgressId Where TPM.Rejected = '0' GROUP BY Approval.ApprovalId, TPM.Id, TPM.DocNo, TPM.DocDate, Approval.VendorId, vwCOADetail.detail_title, Approval.POId, PO.PurchaseOrderNo, ArticleDefView.SubSubID, TPM.ItemId, ArticleDefView.ArticleDescription, TPM.Approved, Approval.ContractId, Approval.RetentionPercentage, Approval.RetentionValue, Approval.MobilizationPercentage, Approval.MobilizationValue, Approval.CurrentProgress, Approval.BillAmount, Approval.NetValue, VCM.ContractValue, VCM.MobilizationValue, Approval.TotalDeduction, Approval.Remarks, VCM.DocNo, PO.CostCenterId"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit records and fill all controls from history grid data
    ''' </summary>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub EditRecords()
        Try
            FillModel()
            Me.cmbVendor.Value = Val(Me.grdSaved.CurrentRow.Cells(grd.VendorId).Value)
            Me.cmbPO.Value = Me.grdSaved.CurrentRow.Cells(grd.POId).Value
            Me.cmbVendorContract.Value = Me.grdSaved.CurrentRow.Cells(grd.ContractId).Value
            Me.cmbProgressNo.Value = Me.grdSaved.CurrentRow.Cells(grd.ProgressId).Value
            Me.txtPreviousProgress.Text = Me.grdSaved.CurrentRow.Cells(grd.CurrentProgress).Value.ToString
            Me.txtPreviousValue.Text = Me.grdSaved.CurrentRow.Cells("PreviousValue").Value.ToString
            Me.txtBillAmount.Text = Me.grdSaved.CurrentRow.Cells(grd.BillAmount).Value.ToString
            Me.txtRetentionPercentage.Text = Me.grdSaved.CurrentRow.Cells(grd.RetentionPercentage).Value.ToString
            Me.txtRetentionValue.Text = (Me.grdSaved.CurrentRow.Cells(grd.RetentionPercentage).Value.ToString * Me.grdSaved.CurrentRow.Cells(grd.BillAmount).Value.ToString) / 100
            'Ali Faisal : TFS1469 : Net progress upto date calculations
            Me.TxtContractValue.Text = Val(Me.grdSaved.CurrentRow.Cells(grd.ContractValue).Value)
            Me.txtTotalMobilization.Text = Val(Me.grdSaved.CurrentRow.Cells(grd.ContractMobilization).Value)
            Dim str As String = "Select IsNull(Sum(MobilizationValue),0) MobilizationValue,IsNull(Sum(BillAmount),0) BillAmount From tblProjectProgressApproval Where VendorId = " & Val(Me.grdSaved.CurrentRow.Cells(grd.VendorId).Value) & " And ContractId = " & Me.grdSaved.CurrentRow.Cells(grd.ContractId).Value & ""
            Dim dt As DataTable = GetDataTable(str)
            Dim ContractMobilization As Double = 0
            Dim TotalBillsValue As Double = 0
            If dt.Rows.Count > 0 Then
                ContractMobilization = dt.Rows(0).Item(0)
                TotalBillsValue = dt.Rows(0).Item(1)
            End If
            If ContractMobilization = Me.grdSaved.CurrentRow.Cells(grd.ContractMobilization).Value AndAlso Me.cmbPendingApprovals.Text = "Pending Approvals" Then
                Me.txtMobilizationPercentage.Text = 0
                Me.txtMobilizationValue.Text = 0
            Else
                'Ali Faisal : TFS1469 : End
                Me.txtMobilizationPercentage.Text = Me.grdSaved.CurrentRow.Cells(grd.MobilizationPercentage).Value.ToString
                Me.txtMobilizationValue.Text = Me.grdSaved.CurrentRow.Cells(grd.MobilizationValue).Value.ToString
            End If
            If objModel.Approved = "True" Then
                Me.btnSave.Enabled = False
                Me.txtApprovalRemarks.Text = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString
                If GetVoucherId("frmProjectProgressApproval", cmbProgressNo.Text) > 0 Then
                    Me.btnApprove.Text = "Approved"
                    Me.btnApprove.Enabled = False
                End If
            Else
                Me.btnSave.Text = "&Save"
            End If
            'Me.txtDeduction.Text = Val(Me.txtRetentionValue.Text) + Val(Me.txtMobilizationValue.Text)
            'Me.txtDeduction.Text = Val(String.Format("{0:###0.00}", CInt(Me.txtRetentionValue.Text))) + Val(String.Format("{0:###0.00}", CInt(Me.txtMobilizationValue.Text)))
            'Me.txtDeduction.Text = String.Format("{0:#,##0.00}", CInt(Me.txtDeduction.Text))
            Me.txtDeduction.Text = Val(Me.txtRetentionValue.Text) + Val(Me.txtMobilizationValue.Text)
            Me.txtDeduction.Text = Val(Me.txtDeduction.Text)
            Me.txtNetDeduction.Text = Me.txtDeduction.Text
            Me.txtNetValue.Text = Val(Me.txtBillAmount.Text) - Val(Me.txtDeduction.Text)
            Me.txtNetValue.Text = Val(Me.txtNetValue.Text)
            BillAmount = Me.txtBillAmount.Text
            Me.txtCurrentProgress.Text = Math.Round((BillAmount / Val(Me.grdSaved.CurrentRow.Cells(grd.ContractValue).Value)) * 100, DecimalPointInValue)
            If Me.cmbPendingApprovals.Text = "Pending Approvals" Then
                Me.txtPreviousProgress.Text = Math.Round((TotalBillsValue / Val(Me.grdSaved.CurrentRow.Cells(grd.ContractValue).Value)) * 100, DecimalPointInValue)
                Me.txtPreviousValue.Text = Math.Round(TotalBillsValue, DecimalPointInValue)
            End If
            Dim TotalProgress As Double = 0
            TotalProgress = Val(Me.txtCurrentProgress.Text) + Val(Me.txtPreviousProgress.Text)
            Me.txtTotalProgress.Text = Math.Round(TotalProgress, DecimalPointInValue)
            Me.txtCurrentValue.Text = Val(Me.grdSaved.CurrentRow.Cells("CurrentValue").Value)
            'Me.txtTotalValue.Text = Val(Me.txtPreviousValue.Text) + Val(Me.txtCurrentValue.Text)
            Me.txtTotalValue.Text = Val(Me.txtPreviousValue.Text) + Val(Me.txtCurrentValue.Text)
            Me.txtTotalValue.Text = Val(Me.txtTotalValue.Text)
            Me.UltraTabControl1.Tabs(0).Selected = True
            Me.btnDelete.Visible = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Validate that all controls are selected before save and update etc
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbVendor.Value = 0 Then
                ShowInformationMessage("Please select any Vendor")
                Return False
            ElseIf Me.cmbPO.Value = 0 Then
                ShowInformationMessage("Please select any PO")
                Return False
            ElseIf Me.cmbVendorContract.Value = 0 Then
                ShowInformationMessage("Please select any Contract")
                Return False
            ElseIf Me.cmbProgressNo.Value = 0 Then
                ShowInformationMessage("Please select any Progress")
                Return False
            ElseIf Me.txtNetValue.Text = "" Or Me.txtNetValue.Text = 0 Then
                ShowInformationMessage("Net Value should be greater than Zero")
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AccountsValidation() As Boolean
        Try
            If flgPurchaseAccountFrontEnd = True AndAlso cmbPurchaseAccount.Value = 0 Then
                msg_Information("Please select the Account for Item/Purchase")
                Return False
            ElseIf GLAccountArticleDepartment = True AndAlso AccountId = 0 Then
                msg_Information("Please select the Account for Item/Purchase")
                Return False
            ElseIf flgPurchaseAccountFrontEnd = False AndAlso GLAccountArticleDepartment = False Then
                msg_Information("Please select the Account for Item/Purchase")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.cmbVendor.Value = 0
            Me.cmbPO.Value = 0
            Me.cmbProgressNo.Value = 0
            Me.cmbVendorContract.Value = 0
            Me.cmbPendingApprovals.SelectedIndex = 0
            Me.txtPreviousProgress.Text = 0
            Me.txtRetentionPercentage.Text = 0
            Me.txtRetentionValue.Text = 0
            Me.txtMobilizationPercentage.Text = 0
            Me.txtMobilizationValue.Text = 0
            Me.txtBillAmount.Text = 0
            Me.txtDeduction.Text = 0
            Me.txtNetDeduction.Text = 0
            Me.txtNetValue.Text = 0
            Me.TxtContractValue.Text = 0
            Me.txtTotalMobilization.Text = 0
            Me.txtCurrentProgress.Text = 0
            Me.txtTotalProgress.Text = 0
            Me.txtApprovalRemarks.Text = ""
            Me.btnSave.Text = "&Save"
            Me.btnSave.Enabled = True
            Me.btnApprove.Text = "Approve"
            Me.btnApprove.Enabled = True
            Me.txtBillAmount.Enabled = False
            Me.txtNetDeduction.Enabled = False
            Me.txtNetValue.Enabled = False
            Me.cmbCostCenter.Enabled = False
            Me.txtPreviousValue.Text = 0
            Me.txtCurrentValue.Text = 0
            Me.txtTotalValue.Text = 0
            GetAllRecords("PendingApprovals")
            FillCombos("Vendor")
            FillCombos("PO")
            FillCombos("Contract")
            FillCombos("Progress")
            FillCombos("PurchaseAccount")
            flgPurchaseAccountFrontEnd = False
            If Not getConfigValueByType("PurchaseAccountFrontEndVendorPM").ToString = "Error" Then
                flgPurchaseAccountFrontEnd = getConfigValueByType("PurchaseAccountFrontEndVendorPM")
            End If
            If flgPurchaseAccountFrontEnd = False Then
                Me.lblPurchaseAccount.Visible = False
                Me.cmbPurchaseAccount.Visible = False
            Else
                Me.lblPurchaseAccount.Visible = True
                Me.cmbPurchaseAccount.Visible = True
            End If
            UltraDropDownSearching(cmbPurchaseAccount, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            ApplySecurityRights()
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Call save function from DAL to insert data in Database
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New ProjectProgressApprovalDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    Me.btnSave.Enabled = False
                    'Insert Activity Log by Ali Faisal on 08-Aug-2017
                    SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, objModel.ProgressNo, True)
                    Return True
                End If
            End If
            Return False
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
    ''' Ali Faisal : Call Update function from DAL to midify the records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New ProjectProgressApprovalDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    'Insert Activity Log by Ali Faisal on 08-Aug-2017
                    SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, objModel.ProgressNo, True)
                    ReSetControls()
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : Load forms
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub frmProjectProgressApproval_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ReSetControls()
            If Me.grdSaved.RowCount > 0 Then
                EditRecords()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Reset all controls on New button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit records on button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
            ApplySecurityRights()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Save button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSave.Text = "&Save" Then
                Save()
            Else
                Update1()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Delete records
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            Delete()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Edit records on double click of history grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Retention % changes and effects on net value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub txtRetentionPercentage_Leave(sender As Object, e As EventArgs) Handles txtRetentionPercentage.Leave
        Try
            Me.txtRetentionValue.Text = Math.Round(BillAmount * Me.txtRetentionPercentage.Text / 100, DecimalPointInValue)
            Me.txtDeduction.Text = Math.Round(Val(String.Format("{0:###0.00}", CInt(Me.txtRetentionValue.Text))) + Val(String.Format("{0:###0.00}", CInt(Me.txtMobilizationValue.Text))), DecimalPointInValue)
            Me.txtNetValue.Text = Math.Round(Val(String.Format("{0:###0.00}", CInt(Me.txtBillAmount.Text))) - Val(String.Format("{0:###0.00}", CInt(Me.txtDeduction.Text))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Retention value chages and effect on net value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub txtRetentionValue_Leave(sender As Object, e As EventArgs) Handles txtRetentionValue.Leave
        Try
            Me.txtRetentionPercentage.Text = Math.Round(Me.txtRetentionValue.Text * 100 / BillAmount, DecimalPointInValue)
            Me.txtDeduction.Text = Math.Round(Val(String.Format("{0:###0.00}", CInt(Me.txtRetentionValue.Text))) + Val(String.Format("{0:###0.00}", CInt(Me.txtMobilizationValue.Text))), DecimalPointInValue)
            Me.txtNetValue.Text = Math.Round(Val(String.Format("{0:###0.00}", CInt(Me.txtBillAmount.Text))) - Val(String.Format("{0:###0.00}", CInt(Me.txtDeduction.Text))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Mobilization % changes and efffect on Net value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub txtMobilizationPercentage_Leave(sender As Object, e As EventArgs) Handles txtMobilizationPercentage.Leave
        Try
            Me.txtMobilizationValue.Text = Math.Round(BillAmount * Me.txtMobilizationPercentage.Text / 100, DecimalPointInValue)
            Me.txtDeduction.Text = Math.Round(Val(String.Format("{0:###0.00}", CInt(Me.txtRetentionValue.Text))) + Val(String.Format("{0:###0.00}", CInt(Me.txtMobilizationValue.Text))), DecimalPointInValue)
            Me.txtNetValue.Text = Math.Round(Val(String.Format("{0:###0.00}", CInt(Me.txtBillAmount.Text))) - Val(String.Format("{0:###0.00}", CInt(Me.txtDeduction.Text))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Mobilization Value changes and effects on Net value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>18-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub txtMobilizationValue_Leave(sender As Object, e As EventArgs) Handles txtMobilizationValue.Leave
        Try
            Me.txtMobilizationPercentage.Text = Math.Round(Me.txtMobilizationValue.Text * 100 / BillAmount, DecimalPointInValue)
            Me.txtDeduction.Text = Math.Round(Val(String.Format("{0:###0.00}", CInt(Me.txtRetentionValue.Text))) + Val(String.Format("{0:###0.00}", CInt(Me.txtMobilizationValue.Text))), DecimalPointInValue)
            Me.txtNetValue.Text = Math.Round(Val(String.Format("{0:###0.00}", CInt(Me.txtBillAmount.Text))) - Val(String.Format("{0:###0.00}", CInt(Me.txtDeduction.Text))), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : Combo box value changes and load the pending and approved records 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>20-July-2017 TFS# 1091 : Ali Faisal</remarks>
    Private Sub cmbPendingApprovals_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPendingApprovals.SelectedIndexChanged
        Try
            If Me.cmbPendingApprovals.Text = "Pending Approvals" Then
                btnNew_Click(Nothing, Nothing)
                GetAllRecords("PendingApprovals")
                If Me.grdSaved.RowCount > 0 Then
                    EditRecords()
                End If
            Else
                GetAllRecords("ApprovedProgress")
                If Me.grdSaved.RowCount > 0 Then
                    EditRecords()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            If Me.btnSave.Text = "&Save" AndAlso Me.btnSave.Enabled = True Then
                msg_Information("Please save the document first.")
                Exit Sub
            Else
                Me.lblProgress.Text = "Processing Please Wait ..."
                Me.lblProgress.Visible = True
                Application.DoEvents()
                Me.Cursor = Cursors.WaitCursor
                objDAL = New ProjectProgressApprovalDAL
                If IsValidate() = True Then
                    If AccountsValidation() = True Then
                        objDAL.SaveVoucher(objModel)
                        ReSetControls()
                        msg_Information("Progress is approved!")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Try
            Dim str As String = ""
            Dim conn As New SqlConnection(SQLHelper.CON_STR)
            Dim trans As SqlTransaction
            Try
                FillModel()
                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If
                trans = conn.BeginTransaction
                If GetVoucherId("frmProjectProgressApproval", cmbProgressNo.Text) > 0 Then
                    objDAL = New ProjectProgressApprovalDAL
                    objModel = New ProjectProgressApprovalBE
                    objModel.RejectedId = 1
                    objDAL.DeleteVoucher(GetVoucherId("frmProjectProgressApproval", cmbProgressNo.Text), objModel)
                End If
                If GetVoucherId("frmProjectProgressApproval", cmbProgressNo.Text) = 0 Then
                    'Update tblTaskProgressMaster Approved and Rejected column
                    str = "Update tblTaskProgressMaster Set Approved = 'False',Rejected = 'True',SendForApproval = 'False',Voucher = 'False' Where Id = " & objModel.ProgressId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
                trans.Commit()
                Me.btnApprove.Enabled = True
                Me.btnApprove.Text = "Approve"
                msg_Information("Progress is rejected")
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPO_ValueChanged(sender As Object, e As EventArgs) Handles cmbPO.ValueChanged
        Try
            FillCombos("CostCenter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub UltraTabControl1_SelectedTabChanged(sender As Object, e As Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try
            If Me.UltraTabControl1.Tabs(0).Selected = True Then
                Me.btnEdit.Visible = False
                Me.btnDelete.Visible = False
                Me.btnSave.Visible = True
                Me.CtrlGrdBar1.Visible = False
            End If
            If Me.UltraTabControl1.Tabs(1).Selected = True Then
                Me.btnEdit.Visible = True
                Me.btnDelete.Visible = True
                Me.btnSave.Visible = False
                Me.CtrlGrdBar1.Visible = True
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
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Project Progress Approval History"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            AddRptParam("@ApprovedId", Val(Me.grdSaved.CurrentRow.Cells(grd.ApprovalId).Value.ToString))
            AddRptParam("@CostCenterId", Val(Me.grdSaved.CurrentRow.Cells("CostCenterId").Value.ToString))
            ShowReport("rptProjectProgressBill")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Ali Faisal : TFS1537 : Values with come seperation
    Private Sub TxtContractValue_TextChanged(sender As Object, e As EventArgs) Handles TxtContractValue.TextChanged
        Try
            If IsNumeric(Me.TxtContractValue.Text) Then
                'Me.TxtContractValue.Text = String.Format("{0:#,##0.00}", CInt(Me.TxtContractValue.Text))
                Me.TxtContractValue.Text = Val(Me.TxtContractValue.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotalMobilization_TextChanged(sender As Object, e As EventArgs) Handles txtTotalMobilization.TextChanged
        Try
            If IsNumeric(Me.txtTotalMobilization.Text) Then
                'Me.txtTotalMobilization.Text = String.Format("{0:#,##0.00}", CInt(Me.txtTotalMobilization.Text))
                Me.txtTotalMobilization.Text = Val(Me.txtTotalMobilization.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPreviousValue_TextChanged(sender As Object, e As EventArgs) Handles txtPreviousValue.TextChanged
        Try
            If IsNumeric(Me.txtPreviousValue.Text) Then
                'Me.txtPreviousValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtPreviousValue.Text))
                Me.txtPreviousValue.Text = Val(Me.txtPreviousValue.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCurrentValue_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentValue.TextChanged
        Try
            If IsNumeric(Me.txtCurrentValue.Text) Then
                'Me.txtCurrentValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtCurrentValue.Text))
                Me.txtCurrentValue.Text = Val(Me.txtCurrentValue.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBillAmount_TextChanged(sender As Object, e As EventArgs) Handles txtBillAmount.TextChanged
        Try
            If IsNumeric(Me.txtBillAmount.Text) Then
                'Me.txtBillAmount.Text = String.Format("{0:#,##0.00}", CInt(Me.txtBillAmount.Text))
                Me.txtBillAmount.Text = Val(Me.txtBillAmount.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNetDeduction_TextChanged(sender As Object, e As EventArgs) Handles txtNetDeduction.TextChanged
        Try
            If IsNumeric(Me.txtNetDeduction.Text) Then
                'Me.txtNetDeduction.Text = String.Format("{0:#,##0.00}", CInt(Me.txtNetDeduction.Text))
                Me.txtNetDeduction.Text = Val(Me.txtNetDeduction.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRetentionValue_TextChanged(sender As Object, e As EventArgs) Handles txtRetentionValue.TextChanged
        Try
            'If IsNumeric(Me.txtRetentionValue.Text) Then
            '    Me.txtRetentionValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtRetentionValue.Text))
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtMobilizationValue_TextChanged(sender As Object, e As EventArgs) Handles txtMobilizationValue.TextChanged
        Try
            'If IsNumeric(Me.txtMobilizationValue.Text) Then
            '    Me.txtMobilizationValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtMobilizationValue.Text))
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDeduction_TextChanged(sender As Object, e As EventArgs) Handles txtDeduction.TextChanged
        Try
            If IsNumeric(Me.txtDeduction.Text) Then
                'Me.txtDeduction.Text = String.Format("{0:#,##0.00}", CInt(Me.txtDeduction.Text))
                Me.txtDeduction.Text = Val(Me.txtDeduction.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtNetValue_TextChanged(sender As Object, e As EventArgs) Handles txtNetValue.TextChanged
        Try
            If IsNumeric(Me.txtNetValue.Text) Then
                'Me.txtNetValue.Text = String.Format("{0:#,##0.00}", CInt(Me.txtNetValue.Text))
                Me.txtNetValue.Text = Val(Me.txtNetValue.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Ali Faisal : TFS1537 : Values with come seperation : End
End Class