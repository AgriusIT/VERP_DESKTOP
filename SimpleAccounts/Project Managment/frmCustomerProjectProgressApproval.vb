'04-Oct-2018 TFS# 4630 : Saad Afzaal : Add new form to save, update and delete the records from this form.
'Saad Afzaal : TFS4630 : Values with come seperation
'Saad Afzaal : TFS4630 : Current, previous and total progress
Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient

Public Class frmCustomerProjectProgressApproval
    Implements IGeneral
    Dim objDAL As CustomerProjectProgressApprovalDAL
    Dim objModel As CustomerProjectProgressApprovalBE
    Dim BillAmount As Double
    Dim AccountId As Integer = 0
    Dim flgPurchaseAccountFrontEnd As Boolean = False
    Dim GLAccountArticleDepartment As Boolean = False
    Public PendingApprovals As String
    Dim Approval_id As Integer

    Private Sub lblHeader_Click(sender As Object, e As EventArgs) Handles lblHeader.Click

    End Sub

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Apply Security for Standard user
    ''' </summary>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.DeleteToolStripMenuItem.Enabled = True
                Me.btnPrint.Enabled = True
                Me.NewToolStripMenuItem.Enabled = True
                Me.ApprovalToolStripMenuItem.Enabled = True
                Me.RejectToolStripMenuItem.Enabled = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.DeleteToolStripMenuItem.Enabled = False
            Me.btnPrint.Enabled = False
            Me.ApprovalToolStripMenuItem.Enabled = False
            Me.RejectToolStripMenuItem.Enabled = False
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
                ElseIf Rights.Item(i).FormControlName = "Approval" Then
                    Me.ApprovalToolStripMenuItem.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Reject" Then
                    Me.RejectToolStripMenuItem.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Call Delete function from DAL to remove the records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New CustomerProjectProgressApprovalDAL
            If objDAL.Delete(objModel) = True Then
                objDAL.DeleteVoucher(GetVoucherId("frmCustomerProjectProgressApproval", objModel.ProgressNo), objModel)
                'Insert Activity Log by Saad Afzaal on 04-Oct-2018
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
    ''' Saad Afzaal : Fill combos of Customer, SO, Contract and Progress
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Customer" Then
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
                str = "Select SalesOrderID, SalesOrderNo, SalesOrderDate, UserName, Remarks, IsNull(CostCenterId, 0) As CostCenterId from SalesOrderMasterTable where  IsNull(Posted,0)=1 AND Status='Open' and IsNull(CostCenterId, 0) > 0 Order By SalesOrderID DESC"
                FillUltraDropDown(Me.cmbSO, str)
                Me.cmbSO.Rows(0).Activate()
                Me.cmbSO.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "Contract" Then
                str = "Select * from tblCustomerContractMaster"
                FillUltraDropDown(Me.cmbCustomerContract, str)
                Me.cmbCustomerContract.Rows(0).Activate()
                Me.cmbCustomerContract.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "Progress" Then
                str = "Select * from IntermPaymentCertificateMaster"
                FillUltraDropDown(Me.cmbProgressNo, str)
                Me.cmbProgressNo.Rows(0).Activate()
                Me.cmbProgressNo.DisplayLayout.Bands(0).Columns(0).Hidden = True
            ElseIf Condition = "CostCenter" Then
                str = "SELECT  SalesOrderMasterTable.CostCenterId AS Id, tblDefCostCenter.Name FROM SalesOrderMasterTable INNER JOIN tblDefCostCenter ON SalesOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID Where SalesOrderMasterTable.SalesOrderId = " & Me.cmbSO.Value & ""
                FillDropDown(Me.cmbCostCenter, str, False)
            ElseIf Condition = "SalesAccount" Then
                FillUltraDropDown(Me.cmbSalesAccount, "Select coa_detail_id, detail_title, detail_code From vwCOADetail WHERE detail_title <> '' and main_type = 'Income'")
                Me.cmbSalesAccount.Rows(0).Activate()
                If Me.cmbSalesAccount.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbSalesAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbSalesAccount.DisplayLayout.Bands(0).Columns(1).Header.Caption = "Account Description"
                    Me.cmbSalesAccount.DisplayLayout.Bands(0).Columns(0).Header.Caption = "Account Code"
                    Me.cmbSalesAccount.DisplayLayout.Bands(0).Columns(1).Width = 250
                    Me.cmbSalesAccount.DisplayLayout.Bands(0).Columns(0).Width = 150
                End If
            ElseIf Condition = "Expense" Then
                FillUltraDropDown(Me.cmbExpense, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], sub_sub_title as [Account Head], Account_Type as [Account Type] From vwCOADetail where detail_title <> '' And Account_Type Not In ('Cash' , 'Bank' , 'Customer' , 'Vendor')")
                Me.cmbExpense.Rows(0).Activate()
                If Me.cmbExpense.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbExpense.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : FillModel to get all values from controls to model properties
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New CustomerProjectProgressApprovalBE
            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
                GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Else
                GLAccountArticleDepartment = False
            End If
            If flgPurchaseAccountFrontEnd = True AndAlso cmbSalesAccount.Value > 0 Then
                AccountId = Me.cmbSalesAccount.Value
            ElseIf GLAccountArticleDepartment = True Then
                AccountId = CustomerProjectProgressApprovalClass.ItemDepartmentId
            Else
                AccountId = Val(getConfigValueByType("SalesAccountCustomerPM").ToString)
            End If
            Dim RetentionAccountId As Integer = Convert.ToInt32((getConfigValueByType("RetentionAccountCustomerPM").ToString))
            Dim MobilizationAccountId As Integer = Convert.ToInt32(Val(getConfigValueByType("MobilizationAccountCustomerPM").ToString))
            objModel.RetentionAccountId = RetentionAccountId
            objModel.MobilizationAccountId = MobilizationAccountId
            objModel.AccountId = AccountId
            objModel.ApprovalId = 0
            objModel.CustomerId = Me.cmbCustomer.Value
            objModel.CustomerName = Me.cmbCustomer.Text
            objModel.SOId = Me.cmbSO.Value
            objModel.CostCenterId = Me.cmbCostCenter.SelectedValue
            objModel.CostCenter = Me.cmbCostCenter.Text
            objModel.ContractId = Me.cmbCustomerContract.Value
            objModel.ProgressId = Me.cmbProgressNo.Value
            objModel.RetentionPercentage = Val(Me.txtRetentionPercentage.Text)
            'Saad Afzaal : TFS4630 : Values with come seperation
            'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
            objModel.RetentionValue = Val(Me.txtRetentionValue.Text)
            objModel.MobilizationPercentage = Val(Me.txtMobilizationPercentage.Text)
            objModel.MobilizationValue = Val(txtMobilizationValue.Text)
            objModel.BillAmount = Val(Me.txtBillAmount.Text)
            objModel.TotalDeduction = Val(Me.txtDeduction.Text)
            objModel.NetValue = Val(Me.txtNetValue.Text)
            'Saad Afzaal : TFS4630 : Values with come seperation
            objModel.Remarks = Me.txtApprovalRemarks.Text
            objModel.TotalProgress = Val(Me.txtTotalProgress.Text)
            objModel.ApprovalId = CustomerProjectProgressApprovalClass.ApprovalId
            objModel.ProgressId = CustomerProjectProgressApprovalClass.ProgressId
            objModel.ProgressNo = CustomerProjectProgressApprovalClass.ProgressNo
            objModel.ProgressDate = CustomerProjectProgressApprovalClass.ProgressDate
            objModel.Approved = CustomerProjectProgressApprovalClass.Approved
            objModel.ItemName = CustomerProjectProgressApprovalClass.ItemName
            objModel.ContractValue = CustomerProjectProgressApprovalClass.ContractValue
            If Me.btnSave.Enabled = True Then
                Dim str As String = "Select IsNull(Sum(MobilizationValue),0) MobilizationValue,IsNull(Sum(BillAmount),0) BillAmount From CustomerProjectProgressApproval Where CustomerId = " & CustomerProjectProgressApprovalClass.CustomerId & " And ContractId = " & CustomerProjectProgressApprovalClass.ContractId & ""
                Dim dt As DataTable = GetDataTable(str)
                Dim ContractMobilization As Double = 0
                Dim TotalBillsValue As Double = 0
                If dt.Rows.Count > 0 Then
                    ContractMobilization = dt.Rows(0).Item(0)
                    TotalBillsValue = dt.Rows(0).Item(1)
                End If
                'Saad Afzaal : TFS1537 : Values with come seperation
                'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
                objModel.PreviousAmount = Val(TotalBillsValue)
            End If
            objModel.ContractNo = CustomerProjectProgressApprovalClass.ContractNo
            'End If
            objModel.CurrentProgress = Me.txtPreviousProgress.Text
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Edit records and fill all controls from history grid data
    ''' </summary>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub EditRecords()
        Try
            FillModel()
            Me.cmbCustomer.Value = CustomerProjectProgressApprovalClass.CustomerId
            Me.cmbSO.Value = CustomerProjectProgressApprovalClass.SOId
            Me.cmbCustomerContract.Value = CustomerProjectProgressApprovalClass.ContractId
            Me.cmbProgressNo.Value = CustomerProjectProgressApprovalClass.ProgressId
            Me.txtPreviousProgress.Text = CustomerProjectProgressApprovalClass.CurrentProgress
            Me.txtPreviousValue.Text = CustomerProjectProgressApprovalClass.CurrentProgress * CustomerProjectProgressApprovalClass.ContractValue / 100
            Me.txtBillAmount.Text = CustomerProjectProgressApprovalClass.BillAmount
            Me.txtRetentionPercentage.Text = CustomerProjectProgressApprovalClass.RetentionPercentage
            Me.txtRetentionValue.Text = CustomerProjectProgressApprovalClass.RetentionPercentage * CustomerProjectProgressApprovalClass.BillAmount / 100
            'Saad Afzaal : TFS4630 : Net progress upto date calculations
            Me.TxtContractValue.Text = CustomerProjectProgressApprovalClass.ContractValue
            Me.txtTotalMobilization.Text = CustomerProjectProgressApprovalClass.ContractMobilization
            Dim str As String = "Select IsNull(Sum(MobilizationValue),0) MobilizationValue,IsNull(Sum(BillAmount),0) BillAmount From CustomerProjectProgressApproval left outer join IntermPaymentCertificateMaster on CustomerProjectProgressApproval.ProgressId = IntermPaymentCertificateMaster.Id Where CustomerProjectProgressApproval.CustomerId = " & CustomerProjectProgressApprovalClass.CustomerId & " And CustomerProjectProgressApproval.ContractId = " & CustomerProjectProgressApprovalClass.ContractId & " And IntermPaymentCertificateMaster.Approved = 1"
            Dim dt As DataTable = GetDataTable(str)
            Dim ContractMobilization As Double = 0
            Dim TotalBillsValue As Double = 0
            If dt.Rows.Count > 0 Then
                ContractMobilization = dt.Rows(0).Item(0)
                TotalBillsValue = dt.Rows(0).Item(1)
            End If
            If ContractMobilization = CustomerProjectProgressApprovalClass.ContractMobilization AndAlso Me.PendingApprovals = "Pending Approvals" Then
                Me.txtMobilizationPercentage.Text = 0
                Me.txtMobilizationValue.Text = 0
            Else
                'Saad Afzaal : TFS4630() : End
                Me.txtMobilizationPercentage.Text = CustomerProjectProgressApprovalClass.MobilizationPercentage
                'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
                Me.txtMobilizationValue.Text = CustomerProjectProgressApprovalClass.MobilizationPercentage * Val(txtBillAmount.Text) / 100
            End If
            If objModel.Approved = "True" Then
                Me.btnSave.Enabled = False
                Me.txtApprovalRemarks.Text = CustomerProjectProgressApprovalClass.Remarks
                If GetVoucherId("frmCustomerProjectProgressApproval", cmbProgressNo.Text) > 0 Then
                    Me.ApprovalToolStripMenuItem.Tag = "Approved"
                    Me.ApprovalToolStripMenuItem.Enabled = False
                End If
            Else
                Me.btnSave.Tag = "Save"
            End If
            'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
            Me.txtDeduction.Text = Val(Me.txtRetentionValue.Text) + Val(Me.txtMobilizationValue.Text)
            Me.txtDeduction.Text = Val(Me.txtDeduction.Text)
            Me.txtNetDeduction.Text = Me.txtDeduction.Text
            Me.txtNetValue.Text = Val(Me.txtBillAmount.Text) - Val(Me.txtDeduction.Text)
            Me.txtNetValue.Text = Val(Me.txtNetValue.Text)
            BillAmount = Me.txtBillAmount.Text
            Me.txtCurrentProgress.Text = Math.Round((BillAmount / CustomerProjectProgressApprovalClass.ContractValue) * 100, DecimalPointInValue)
            If Me.PendingApprovals = "Pending Approvals" Then
                Me.txtPreviousProgress.Text = Math.Round((TotalBillsValue / CustomerProjectProgressApprovalClass.ContractValue) * 100, DecimalPointInValue)
                Me.txtPreviousValue.Text = Math.Round(TotalBillsValue, DecimalPointInValue)
            End If
            Dim TotalProgress As Double = 0
            TotalProgress = Val(Me.txtCurrentProgress.Text) + Val(Me.txtPreviousProgress.Text)
            Me.txtTotalProgress.Text = Math.Round(TotalProgress, DecimalPointInValue)
            Me.txtCurrentValue.Text = Math.Round(BillAmount, DecimalPointInValue)
            Me.txtTotalValue.Text = Val(Me.txtPreviousValue.Text) + Val(Me.txtCurrentValue.Text)
            Me.txtTotalValue.Text = Val(Me.txtTotalValue.Text)
            Me.UltraTabControl1.Tabs(0).Selected = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Saad Afzaal : Validate that all controls are selected before save and update etc
    ''' </summary>
    ''' <param name="Mode"></param>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbCustomer.Value = 0 Then
                msg_Error("Please select any Customer")
                Return False
            ElseIf Me.cmbSO.Value = 0 Then
                msg_Error("Please select any SO")
                Return False
            ElseIf Me.cmbCustomerContract.Value = 0 Then
                msg_Error("Please select any Contract")
                Return False
            ElseIf Me.cmbProgressNo.Value = 0 Then
                msg_Error("Please select any Progress")
                Return False
            ElseIf Me.txtNetValue.Text = "" Or Me.txtNetValue.Text = 0 Then
                msg_Error("Net Value should be greater than Zero")
                Return False
            End If
            FillModel()
            If fillExpenseListModel() = True Then
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AccountsValidation() As Boolean
        Try
            If flgPurchaseAccountFrontEnd = True AndAlso cmbSalesAccount.Value = 0 Then
                msg_Error("Please select the Account for Item/Sales")
                Return False
            ElseIf GLAccountArticleDepartment = True AndAlso AccountId = 0 Then
                msg_Error("Please select the Account for Item/Sales")
                Return False
            ElseIf flgPurchaseAccountFrontEnd = False AndAlso GLAccountArticleDepartment = False Then
                msg_Error("Please select the Account for Item/Sales")
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        End Try
    End Function


    ''' <summary>
    ''' Saad Afzaal : Reset all controls to their default values
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.Approval_id = 0
            Me.cmbCustomer.Value = 0
            Me.cmbSO.Value = 0
            Me.cmbProgressNo.Value = 0
            Me.cmbCustomerContract.Value = 0
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
            Me.btnSave.Tag = "Save"
            Me.btnSave.Enabled = True
            Me.ApprovalToolStripMenuItem.Tag = "Approve"
            Me.ApprovalToolStripMenuItem.Enabled = True
            Me.txtBillAmount.Enabled = False
            Me.txtNetDeduction.Enabled = False
            Me.txtNetValue.Enabled = False
            Me.cmbCostCenter.Enabled = False
            Me.txtPreviousValue.Text = 0
            Me.txtCurrentValue.Text = 0
            Me.txtTotalValue.Text = 0
            GetAllRecords("PendingApprovals")
            FillCombos("Customer")
            FillCombos("SO")
            FillCombos("Contract")
            FillCombos("Progress")
            FillCombos("SalesAccount")
            FillCombos("Expense")
            FillExpenseGrid(Me.Approval_id)
            flgPurchaseAccountFrontEnd = False
            If Not getConfigValueByType("RevenueAccountFrontEnd").ToString = "Error" Then
                flgPurchaseAccountFrontEnd = getConfigValueByType("RevenueAccountFrontEnd")
            End If
            If flgPurchaseAccountFrontEnd = False Then
                Me.lblReceivedAccount.Visible = False
                Me.cmbSalesAccount.Visible = False
            Else
                Me.lblReceivedAccount.Visible = True
                Me.cmbSalesAccount.Visible = True
            End If
            ApplySecurityRights()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Call save function from DAL to insert data in Database
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New CustomerProjectProgressApprovalDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    Me.btnSave.Tag = "Approve"
                    If AccountsValidation() = True Then
                        objDAL.SaveVoucher(objModel)
                        ReSetControls()
                        msg_Information("Progress is approved!")
                        Me.Close()
                    End If
                    'Insert Activity Log by Saad Afzaal on 04-Oct-2018
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
    ''' Saad Afzaal : Call Update function from DAL to midify the records
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <returns></returns>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New CustomerProjectProgressApprovalDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    'Insert Activity Log by Saad Afzaal on 03-Oct-2018
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
    ''' Saad Afzaal : Load forms
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Private Sub frmCustomerProjectProgressApproval_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ReSetControls()
            Me.ApprovalToolStripMenuItem.Visible = True
            Me.RejectToolStripMenuItem.Visible = True
            If CustomerProjectProgressApprovalClass.ApprovalId > 0 Then
                Me.btnSave.Enabled = False
                Me.ApprovalToolStripMenuItem.Visible = True
                Me.RejectToolStripMenuItem.Visible = True
                Me.Approval_id = CustomerProjectProgressApprovalClass.ApprovalId
                Me.DeleteToolStripMenuItem.Visible = True
                FillExpenseGrid(Me.Approval_id)
                Me.btnPrint.Visible = True
                Me.btnCancel.Location = New Point(705, 3)
            Else
                Me.btnSave.Enabled = True
                Me.ApprovalToolStripMenuItem.Visible = False
                Me.RejectToolStripMenuItem.Visible = False
                Me.DeleteToolStripMenuItem.Visible = False
                Me.Approval_id = 0
                FillExpenseGrid(Me.Approval_id)
                Me.btnPrint.Visible = False
                Me.btnCancel.Location = New Point(775, 3)
            End If
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Reset all controls on New button click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Save button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Me.btnSave.Tag = "Save" Then
                Save()
            Else
                'Update1()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Delete records
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
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
    ''' Saad Afzaal : Retention % changes and effects on net value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Private Sub txtRetentionPercentage_Leave(sender As Object, e As EventArgs) Handles txtRetentionPercentage.Leave
        Try
            Me.txtRetentionValue.Text = Math.Round(BillAmount * Me.txtRetentionPercentage.Text / 100, DecimalPointInValue)
            Me.txtDeduction.Text = Math.Round(Val(Me.txtRetentionValue.Text) + Val(Me.txtMobilizationValue.Text), DecimalPointInValue)
            'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
            Me.txtNetDeduction.Text = Val(Me.txtDeduction.Text)
            Me.txtNetValue.Text = Math.Round(Val(Me.txtBillAmount.Text) - Val(Me.txtDeduction.Text), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Retention value chages and effect on net value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Private Sub txtRetentionValue_Leave(sender As Object, e As EventArgs) Handles txtRetentionValue.Leave
        Try
            Me.txtRetentionPercentage.Text = Math.Round(Me.txtRetentionValue.Text * 100 / BillAmount, DecimalPointInValue)
            Me.txtDeduction.Text = Math.Round(Val(Me.txtRetentionValue.Text) + Val(Me.txtMobilizationValue.Text), DecimalPointInValue)
            'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
            Me.txtNetDeduction.Text = Val(Me.txtDeduction.Text)
            Me.txtNetValue.Text = Math.Round(Val(Me.txtBillAmount.Text) - Val(Me.txtDeduction.Text), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Mobilization % changes and efffect on Net value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 1091 : Saad Afzaal</remarks>
    Private Sub txtMobilizationPercentage_Leave(sender As Object, e As EventArgs) Handles txtMobilizationPercentage.Leave
        Try
            Me.txtMobilizationValue.Text = Math.Round(BillAmount * Me.txtMobilizationPercentage.Text / 100, DecimalPointInValue)
            Me.txtDeduction.Text = Math.Round(Val(Me.txtRetentionValue.Text) + Val(Me.txtMobilizationValue.Text), DecimalPointInValue)
            'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
            Me.txtNetDeduction.Text = Val(Me.txtDeduction.Text)
            Me.txtNetValue.Text = Math.Round(Val(Me.txtBillAmount.Text) - Val(Me.txtDeduction.Text), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Saad Afzaal : Mobilization Value changes and effects on Net value
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Private Sub txtMobilizationValue_Leave(sender As Object, e As EventArgs) Handles txtMobilizationValue.Leave
        Try
            Me.txtMobilizationPercentage.Text = Math.Round(Me.txtMobilizationValue.Text * 100 / BillAmount, DecimalPointInValue)
            Me.txtDeduction.Text = Math.Round(Val(Me.txtRetentionValue.Text) + Val(Me.txtMobilizationValue.Text), DecimalPointInValue)
            'Ali Faisal : 'Modoifcations for 3G constructions on 19-Nov-2018
            Me.txtNetDeduction.Text = Val(Me.txtDeduction.Text)
            Me.txtNetValue.Text = Math.Round(Val(Me.txtBillAmount.Text) - Val(Me.txtDeduction.Text), DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub ApprovalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApprovalToolStripMenuItem.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            objDAL = New CustomerProjectProgressApprovalDAL
            If IsValidate() = True Then
                If AccountsValidation() = True Then
                    objDAL.SaveVoucher(objModel)
                    ReSetControls()
                    msg_Information("Progress is approved!")
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


    Private Sub RejectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RejectToolStripMenuItem.Click
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
                If GetVoucherId("frmCustomerProjectProgressApproval", cmbProgressNo.Text) > 0 Then
                    objDAL = New CustomerProjectProgressApprovalDAL
                    objModel = New CustomerProjectProgressApprovalBE
                    objModel.RejectedId = 1
                    objModel.ProgressId = CustomerProjectProgressApprovalClass.ProgressId
                    objDAL.DeleteVoucher(GetVoucherId("frmCustomerProjectProgressApproval", cmbProgressNo.Text), objModel)
                End If
                If GetVoucherId("frmCustomerProjectProgressApproval", cmbProgressNo.Text) = 0 Then
                    'Update tblTaskProgressMaster Approved and Rejected column
                    str = "Update IntermPaymentCertificateMaster Set Approved = '0',Rejected = '1',SendForApproval = '0',Voucher = '0' Where Id = " & objModel.ProgressId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
                trans.Commit()
                Me.ApprovalToolStripMenuItem.Enabled = True
                Me.ApprovalToolStripMenuItem.Tag = "Approve"
                msg_Information("Progress is rejected")
                Me.Close()
            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub cmbSO_ValueChanged(sender As Object, e As EventArgs) Handles cmbSO.ValueChanged
        Try
            FillCombos("CostCenter")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            NewToolStripMenuItem_Click(Nothing, Nothing)
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub buttonSave_Click(sender As Object, e As EventArgs)
        Try
            btnSave_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAddExp_Click(sender As Object, e As EventArgs) Handles btnAddExp.Click
        Try

            If Me.cmbExpense.IsItemInList = False Then Exit Sub
            If Me.cmbExpense.ActiveRow Is Nothing Then Exit Sub
            If Con Is Nothing Then Exit Sub
            If Me.grdExpense.RowCount > 0 Then
                Dim bln As Boolean = Me.grdExpense.Find(Me.grdExpense.RootTable.Columns("AccountId"), Janus.Windows.GridEX.ConditionOperator.Equal, Me.cmbExpense.Value, 0, 1)
                If bln = True Then
                    ShowErrorMessage("Account is already added")
                    Exit Sub
                End If
            End If
            Dim dt As DataTable = CType(Me.grdExpense.DataSource, DataTable)
            dt.AcceptChanges()
            Dim dr As DataRow
            dr = dt.NewRow
            dr(0) = Me.cmbExpense.Value
            If Me.Approval_id > 0 Then
                dr(1) = Me.Approval_id
            Else
                dr(1) = 0
            End If
            dr(2) = Me.cmbExpense.ActiveRow.Cells("Account Code").Value.ToString
            dr(3) = Me.cmbExpense.ActiveRow.Cells("Account Title").Value.ToString
            dr(4) = 0
            dr(5) = ""
            dt.Rows.Add(dr)
            dt.AcceptChanges()
            cmbExpense.Rows(0).Activate()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub FillExpenseGrid(ApprovalId As Integer)
        Try
            Dim dtExpDetail As DataTable

            If ApprovalId > 0 Then
                dtExpDetail = GetDataTable("select AccountId , ApprovalId , detail_code , detail_title , Amount, Comments from CustomerProjectProgressApprovalExpense where ApprovalId = " & ApprovalId)
            Else
                dtExpDetail = GetDataTable("select Distinct AccountId , 0 As ApprovalId , detail_code , detail_title , 0 As Amount, Comments from CustomerProjectProgressApprovalExpense")
            End If
            dtExpDetail.AcceptChanges()
            Me.grdExpense.DataSource = Nothing
            Me.grdExpense.DataSource = dtExpDetail
            Me.grdExpense.RootTable.Columns("Amount").FormatString = "N" & DecimalPointInValue
            Me.grdExpense.RootTable.Columns("Comments").FormatString = "N"
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function fillExpenseListModel() As Boolean
        Try
            objModel.ExpenseList = New List(Of CustomerProjectProgressApprovalExpenseBE)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdExpense.GetDataRows
                Dim expDetail As New CustomerProjectProgressApprovalExpenseBE
                expDetail.ApprovalId = Val(Row.Cells("ApprovalId").Value.ToString)
                expDetail.AccountId = Val(Row.Cells("AccountId").Value.ToString)
                expDetail.detail_code = Row.Cells("detail_code").Value.ToString
                expDetail.detail_title = Row.Cells("detail_title").Value.ToString
                expDetail.Amount = Val(Row.Cells("Amount").Value.ToString)
                expDetail.Comments = Row.Cells("Comments").Value.ToString
                objModel.ExpenseList.Add(expDetail)
            Next
            Return True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
            Return False
        End Try
    End Function

    Private Sub grdExpense_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdExpense.ColumnButtonClick
        Try
            objDAL = New CustomerProjectProgressApprovalDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub

                If Val(Me.grdExpense.CurrentRow.Cells("ApprovalId").Value.ToString) > 0 Then
                    objDAL.DeleteExpense(Val(Me.grdExpense.CurrentRow.Cells("ApprovalId").Value.ToString), Val(Me.grdExpense.CurrentRow.Cells("AccountId").Value.ToString))
                    msg_Information(str_informDelete)
                    Me.grdExpense.CurrentRow.Delete()
                Else
                    Me.grdExpense.CurrentRow.Delete()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnMore_Click(sender As Object, e As EventArgs) Handles btnMore.Click
        ContextMenuStrip1.Show(btnMore, 0, btnMore.Height)
    End Sub
End Class



' CustomerProjectProgressApproval Class To fill model that use in EditRecords
Public Class CustomerProjectProgressApprovalClass

    Public Shared ApprovalId As Integer
    Public Shared ItemDepartmentId As Integer
    Public Shared ProgressId As Integer

    Public Shared ProgressNo As String
    Public Shared ProgressDate As DateTime
    Public Shared Approved As Boolean

    Public Shared ItemName As String
    Public Shared ContractValue As Decimal
    Public Shared ContractNo As String
    Public Shared CustomerId As Integer
    Public Shared SOId As Integer
    Public Shared ContractId As Integer

    Public Shared CurrentProgress As Decimal
    Public Shared BillAmount As Decimal
    Public Shared RetentionPercentage As Decimal
    Public Shared ContractMobilization As Decimal

    Public Shared MobilizationPercentage As Decimal
    Public Shared MobilizationValue As Decimal

    Public Shared Remarks As String
End Class