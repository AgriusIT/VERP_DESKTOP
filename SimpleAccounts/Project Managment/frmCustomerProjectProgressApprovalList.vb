Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient

Public Class frmCustomerProjectProgressApprovalList
    Implements IGeneral
    Dim DeleteRights As Boolean
    Dim objDAL As CustomerProjectProgressApprovalDAL
    Dim objModel As CustomerProjectProgressApprovalBE
    ''' <summary>
    ''' Saad Afzaal : Set indexes for grid
    ''' </summary>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Enum grd
        ApprovalId
        ProgressId
        ProgressNo
        ProgressDate
        CustomerId
        CustomerName
        SOId
        SONo
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
    End Enum

    ''' <summary>
    ''' Saad Afzaal : Apply grid settings to hide some columns
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If rdoApproved.Checked = True Then
                If Me.grdSaved.RootTable.Columns.Contains("Delete") = False Then
                    Me.grdSaved.RootTable.Columns.Add("Delete")
                    Me.grdSaved.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                    Me.grdSaved.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                    Me.grdSaved.RootTable.Columns("Delete").ButtonText = "Delete"
                    Me.grdSaved.RootTable.Columns("Delete").Key = "Delete"
                    Me.grdSaved.RootTable.Columns("Delete").Caption = "Action"
                End If
            End If

            Me.grdSaved.RootTable.Columns(grd.CustomerName).Width = 250

            Me.grdSaved.RootTable.Columns(grd.ApprovalId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ProgressId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CustomerId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.SOId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ItemDepartmentId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ItemId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.ContractId).Visible = False
            Me.grdSaved.RootTable.Columns(grd.CurrentProgress).Visible = False
            Me.grdSaved.RootTable.Columns(grd.RetentionValue).Visible = False
            Me.grdSaved.RootTable.Columns(grd.MobilizationValue).Visible = False

            Me.grdSaved.RootTable.Columns(grd.RetentionPercentage).Caption = "Ret %"
            Me.grdSaved.RootTable.Columns(grd.MobilizationPercentage).Caption = "Mob %"
            Me.grdSaved.RootTable.Columns(grd.SONo).Caption = "SO No"

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
            Me.grdSaved.RootTable.Columns(grd.NetAmount).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.NetAmount).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.NetAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.NetAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.TotalDeductions).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.TotalDeductions).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.TotalDeductions).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.TotalDeductions).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns(grd.RetentionPercentage).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.RetentionPercentage).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.RetentionPercentage).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.MobilizationPercentage).FormatString = "N" & DecimalPointInValue
            Me.grdSaved.RootTable.Columns(grd.MobilizationPercentage).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns(grd.MobilizationPercentage).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            'Saad Afzaal : TFS4630 : apply grid settings
            If Me.rdoPending.Checked = True Then
                Me.grdSaved.RootTable.Columns(grd.ContractMobilization).Visible = False
                Me.grdSaved.RootTable.Columns(grd.NetAmount).Visible = False
            ElseIf Me.rdoApproved.Checked = True Then
                Me.grdSaved.RootTable.Columns(grd.ContractMobilization).Visible = False
                Me.grdSaved.RootTable.Columns(grd.ContractValue).Visible = False
            End If
            'Saad Afzaal : TFS4629 : End
            Me.grdSaved.RootTable.Columns("ContractNo").Visible = False
            Me.grdSaved.RootTable.Columns("CostCenterId").Visible = False

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Saad Afzaal : Apply Security for Standard user
    ''' </summary>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.DeleteRights = True
                Exit Sub
            End If
            Me.Visible = False
            Me.DeleteRights = False
            Me.CtrlGrdBar1.mGridPrint.Enabled = False
            Me.CtrlGrdBar1.mGridExport.Enabled = False
            Me.CtrlGrdBar1.mGridChooseFielder.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    Me.DeleteRights = True
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

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New CustomerProjectProgressApprovalDAL

            objModel = New CustomerProjectProgressApprovalBE

            objModel.ApprovalId = Val(Me.grdSaved.CurrentRow.Cells(grd.ApprovalId).Value.ToString)
            objModel.ProgressNo = Me.grdSaved.CurrentRow.Cells(grd.ProgressNo).Value.ToString
            objModel.ProgressId = Val(Me.grdSaved.CurrentRow.Cells(grd.ProgressId).Value.ToString)

            If objDAL.Delete(objModel) = True Then
                objDAL.DeleteVoucher(GetVoucherId("frmCustomerProjectProgressApproval", objModel.ProgressNo), objModel)
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

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel

    End Sub


    ''' <summary>
    ''' Saad Afzaal : Get all records Pending and Approved condition based
    ''' </summary>
    ''' <param name="Condition"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If rdoPending.Checked = True Then
                str = "SELECT isNull(CustomerProjectProgressApproval.ApprovalId,0) As ApprovalId, Task.Id AS ProgressId, Task.DocNo AS ProgressNo, Task.DocDate AS ProgressDate, Task.CustomerId, COA.detail_title AS CustomerName, Task.SOId, PO.SalesOrderNo AS SONo, Article.SubSubID As ItemDepartmentId, Task.ItemId, Article.ArticleDescription AS ItemName, ISNULL(Task.Approved, 0) AS Approved, tblCustomerContractMaster.ContractId, tblCustomerContractMaster.RetentionPercentage, tblCustomerContractMaster.RetentionValue, tblCustomerContractMaster.MobilizationPerBill As MobilizationPercentage, (tblCustomerContractMaster.MobilizationPerBill * tblCustomerContractMaster.MobilizationValue)/100 As MobilizationValue, SUM(IntermPaymentCertificateDetail.CurrentProgress) AS CurrentProgress, SUM(IntermPaymentCertificateDetail.NetValue) - SUM(IntermPaymentCertificateDetail.ApprovedProgress) AS BillAmount, 0 AS NetAmount,ContractValue.NetValue As ContractValue,tblCustomerContractMaster.MobilizationValue ContractMobilization, tblCustomerContractMaster.DocNo AS ContractNo, IsNull(PO.CostCenterId,0) CostCenterId  FROM IntermPaymentCertificateMaster AS Task INNER JOIN vwCOADetail AS COA ON Task.CustomerId = COA.coa_detail_id INNER JOIN SalesOrderMasterTable AS PO ON Task.SOId = PO.SalesOrderId INNER JOIN ArticleDefView AS Article ON Task.ItemId = Article.ArticleId INNER JOIN tblCustomerContractMaster ON Task.CustomerId = tblCustomerContractMaster.CustomerId AND Task.SOId = tblCustomerContractMaster.SOId AND Task.ItemId = tblCustomerContractMaster.ItemId INNER JOIN (Select Sum(isNull(IntermPaymentCertificateDetail.ContractValue,0)) As NetValue , ProgressId from IntermPaymentCertificateDetail Group By ProgressId) As ContractValue On Task.Id = ContractValue.ProgressId  INNER JOIN IntermPaymentCertificateDetail ON Task.Id = IntermPaymentCertificateDetail.ProgressId Left Outer Join CustomerProjectProgressApproval On Task.Id = CustomerProjectProgressApproval.ProgressId WHERE Task.Approved is null or Task.Approved = '0' And Task.SendForApproval = '1' GROUP BY Task.Id, Task.DocNo, Task.DocDate, Task.CustomerId, COA.detail_title, Task.SOId, PO.SalesOrderNo, Article.SubSubID, Task.ItemId, Article.ArticleDescription, ISNULL(Task.Approved, 0), tblCustomerContractMaster.ContractId, tblCustomerContractMaster.RetentionPercentage, tblCustomerContractMaster.RetentionValue, tblCustomerContractMaster.MobilizationPerBill, tblCustomerContractMaster.MobilizationPercentage, tblCustomerContractMaster.MobilizationValue,ContractValue.NetValue, tblCustomerContractMaster.DocNo, PO.CostCenterId , CustomerProjectProgressApproval.ApprovalId"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings()
            ElseIf rdoApproved.Checked = True Then
                str = "SELECT Approval.ApprovalId, TPM.Id AS ProgressId, TPM.DocNo AS ProgressNo, TPM.DocDate AS ProgressDate, Approval.CustomerId, vwCOADetail.detail_title AS CustomerName, Approval.SOId, SO.SalesOrderNo AS SONo, ArticleDefView.SubSubID As ItemDepartmentId, TPM.ItemId, ArticleDefView.ArticleDescription AS ItemName, TPM.Approved, Approval.ContractId, Approval.RetentionPercentage, Approval.RetentionValue, Approval.MobilizationPercentage, Approval.MobilizationValue, Approval.CurrentProgress, Approval.BillAmount, Approval.NetValue, ContractValue.NetValue As ContractValue, VCM.MobilizationValue AS ContractMobilization, Approval.TotalDeduction, Approval.Remarks, VCM.DocNo AS ContractNo, IsNull(SO.CostCenterId,0) CostCenterId FROM CustomerProjectProgressApproval AS Approval INNER JOIN vwCOADetail ON Approval.CustomerId = vwCOADetail.coa_detail_id INNER JOIN SalesOrderMasterTable AS SO ON Approval.SOId = SO.SalesOrderId INNER JOIN IntermPaymentCertificateMaster AS TPM ON Approval.ProgressId = TPM.Id INNER JOIN ArticleDefView ON TPM.ItemId = ArticleDefView.ArticleId INNER JOIN tblCustomerContractMaster AS VCM ON Approval.ContractId = VCM.ContractId INNER JOIN (Select Sum(isNull(IntermPaymentCertificateDetail.ContractValue,0)) As NetValue , ProgressId from IntermPaymentCertificateDetail Group By ProgressId) As ContractValue On TPM.Id = ContractValue.ProgressId Where TPM.Rejected = '0' And TPM.Approved = '1' ORDER BY Approval.ApprovalId DESC"
                dt = GetDataTable(str)
                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                ApplyGridSettings()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate

    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls

    End Sub

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save

    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Customer Project Progress Approval History"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Saad Afzaal : Edit records on double click of history grid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>04-Oct-2018 TFS# 4630 : Saad Afzaal</remarks>
    Private Sub grdSaved_DoubleClick(sender As Object, e As EventArgs) Handles grdSaved.DoubleClick
        Try
            If Me.rdoPending.Checked = True Then
                frmCustomerProjectProgressApproval.PendingApprovals = "Pending Approvals"
            ElseIf Me.rdoApproved.Checked = True Then
                frmCustomerProjectProgressApproval.PendingApprovals = "Approved"
            End If

            CustomerProjectProgressApprovalClass.ItemDepartmentId = Val(Me.grdSaved.CurrentRow.Cells(grd.ItemDepartmentId).Value)
            CustomerProjectProgressApprovalClass.ApprovalId = Val(Me.grdSaved.CurrentRow.Cells(grd.ApprovalId).Value.ToString)
            CustomerProjectProgressApprovalClass.ProgressId = Val(Me.grdSaved.CurrentRow.Cells(grd.ProgressId).Value.ToString)
            CustomerProjectProgressApprovalClass.ProgressNo = Me.grdSaved.CurrentRow.Cells(grd.ProgressNo).Value.ToString
            CustomerProjectProgressApprovalClass.ProgressDate = Me.grdSaved.CurrentRow.Cells(grd.ProgressDate).Value
            CustomerProjectProgressApprovalClass.Approved = Me.grdSaved.CurrentRow.Cells(grd.Approved).Value
            CustomerProjectProgressApprovalClass.ItemName = Me.grdSaved.CurrentRow.Cells(grd.ItemName).Value.ToString
            CustomerProjectProgressApprovalClass.ContractValue = Val(Me.grdSaved.CurrentRow.Cells(grd.ContractValue).Value)
            CustomerProjectProgressApprovalClass.CustomerId = Val(Me.grdSaved.CurrentRow.Cells(grd.CustomerId).Value)
            CustomerProjectProgressApprovalClass.ContractId = Me.grdSaved.CurrentRow.Cells(grd.ContractId).Value
            CustomerProjectProgressApprovalClass.ContractNo = Me.grdSaved.CurrentRow.Cells("ContractNo").Value

            CustomerProjectProgressApprovalClass.SOId = Me.grdSaved.CurrentRow.Cells(grd.SOId).Value
            CustomerProjectProgressApprovalClass.CurrentProgress = Me.grdSaved.CurrentRow.Cells(grd.CurrentProgress).Value.ToString
            CustomerProjectProgressApprovalClass.BillAmount = Me.grdSaved.CurrentRow.Cells(grd.BillAmount).Value.ToString
            CustomerProjectProgressApprovalClass.RetentionPercentage = Me.grdSaved.CurrentRow.Cells(grd.RetentionPercentage).Value.ToString
            CustomerProjectProgressApprovalClass.ContractMobilization = Val(Me.grdSaved.CurrentRow.Cells(grd.ContractMobilization).Value)
            CustomerProjectProgressApprovalClass.MobilizationPercentage = Me.grdSaved.CurrentRow.Cells(grd.MobilizationPercentage).Value.ToString()
            CustomerProjectProgressApprovalClass.MobilizationValue = Me.grdSaved.CurrentRow.Cells(grd.MobilizationValue).Value.ToString()

            If Me.rdoApproved.Checked = True Then
                CustomerProjectProgressApprovalClass.Remarks = Me.grdSaved.CurrentRow.Cells("Remarks").Value.ToString()
            End If
            frmCustomerProjectProgressApproval.ShowDialog()
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rdoApproved_CheckedChanged(sender As Object, e As EventArgs) Handles rdoApproved.CheckedChanged, rdoPending.CheckedChanged
        If rdoApproved.Checked = True Then
            GetAllRecords()
        ElseIf rdoPending.Checked = True Then
            GetAllRecords()
        End If
    End Sub

    Private Sub grdSaved_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdSaved.ColumnButtonClick
        Try
            objDAL = New CustomerProjectProgressApprovalDAL
            If e.Column.Key = "Delete" Then
                If Me.DeleteRights = True Then
                    If msg_Confirm(str_ConfirmDelete) = True Then
                        If Delete() = True Then
                            msg_Information(str_informDelete)
                            GetAllRecords()
                        End If
                    End If
                Else
                    ShowErrorMessage("You do not have rights to delete this document")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub frmCustomerProjectProgressApprovalList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ApplySecurityRights()
            Me.rdoPending.Checked = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            GetAllRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class