Imports SBDal
Imports SBModel
Imports System
Imports System.Data.SqlClient

Public Class frmCustomerContract
    Implements IGeneral
    Dim objDAL As CustomerContractDAL
    Dim objModel As CustomerContractBE
    Dim Approved As Boolean
    Dim ContractValue As Double
    Public Contract_Id As Integer
    Public editModel As CustomerContractBE

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

    Private Sub frmCustomerContract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ReSetControls()
            FillCombos("Item")
            FillCombos("Customer")
            FillCombos("SO")
            FillCombos("Terms")
            FillCombos("Bank")
            ApplySecurityRights()
            Me.cmbCustomer.Focus()
            If Me.Contract_Id > 0 Then
                Me.btnSave.Tag = "Update"
                EditRecords()
                Me.cmbCustomer.Enabled = False
                Me.cmbSO.Enabled = False
                Me.cmbItem.Enabled = False
                Me.btnPrint.Visible = True
                Me.btnCancel.Location = New Point(755, 12)
                Me.DeleteToolStripMenuItem.Visible = True
            Else
                Me.btnSave.Tag = "Save"
                Me.DeleteToolStripMenuItem.Visible = False
                Me.cmbCustomer.Enabled = True
                Me.cmbSO.Enabled = True
                Me.cmbItem.Enabled = True
                Me.btnPrint.Visible = False
                Me.btnCancel.Location = New Point(825, 12)
                Me.DeleteToolStripMenuItem.Visible = False
            End If
            Me.dtpDocDate.TabIndex = 0
            UltraDropDownSearching(cmbCustomer, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbSO, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbItem, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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
            Me.grd.RootTable.Columns(grdenm.TotalMeasurment).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.NetValue).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdenm.TaskRate).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdenm.TaskRate).TotalFormatString = "N" & DecimalPointInValue
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.DeleteToolStripMenuItem.Enabled = True
                Me.btnPrint.Enabled = True
                Me.NewToolStripMenuItem.Enabled = True
                Me.chkApprove.Visible = True
                Me.chkApprove.Checked = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnSave.Enabled = False
            Me.DeleteToolStripMenuItem.Enabled = False
            Me.btnPrint.Enabled = False
            Me.chkApprove.Visible = False
            Me.chkApprove.Checked = False
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
                ElseIf Rights.Item(i).FormControlName = "Approve" Then
                    Me.chkApprove.Visible = True
                    Me.chkApprove.Checked = True
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

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            objDAL = New CustomerContractDAL
            FillModel()
            If objDAL.Delete(Me.editModel.ContractId) = True Then
                Dim VoucherId As Integer = GetVoucherId("frmCustomerContract", Me.editModel.ContractNo)
                objDAL.DeleteVoucher(VoucherId)
                DeleteTerms()
                'Insert Activity Log
                SaveActivityLog("Config", Me.Text, EnumActions.Delete, LoginUserId, EnumRecordType.Configuration, Me.editModel.ContractNo, True)
                Me.Close()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

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
                str = "Delete from tblCustomerContractTerms Where TermId=" & objModel.TermId & " and ContractId=" & objModel.ContractId & " "
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

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = ""
            If Condition = "Item" Then
                str = "SELECT ArticleDefView.ArticleId AS Id, ArticleDefView.ArticleCode AS Code, ArticleDefView.ArticleDescription AS Item, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Combination,ArticleDefView.PackQty, ISNULL(ArticleDefView.SalePrice, 0) AS Price, ISNULL(ArticleDefView.PurchasePrice, 0) AS PurchasePrice, ArticleDefView.SortOrder, ArticleDefView.ArticleGroupName AS Dept, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleLpoName AS Brand, ISNULL(ArticleDefView.Cost_Price, 0) AS [Cost Price], ISNULL(ArticleDefView.TradePrice, 0) AS [Trade Price] FROM ArticleDefView INNER JOIN SalesOrderDetailTable ON ArticleDefView.ArticleId = SalesOrderDetailTable.ArticleDefId LEFT OUTER JOIN tblCustomerContractMaster ON ArticleDefView.ArticleId = tblCustomerContractMaster.TermId"
                If Me.btnSave.Tag = "Save" Then
                    str += " where Active=1 And SalesOrderId = " & Me.cmbSO.Value & " And SOId Not In (Select SOId From tblCustomerContractMaster Where SOId = " & Me.cmbSO.Value & ")"
                Else
                    str += " where Active=1 And SalesOrderId = " & Me.cmbSO.Value & ""
                End If
                FillUltraDropDown(Me.cmbItem, str)
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Cost Price").Hidden = True
                Me.cmbItem.Rows(0).Activate()
                If Me.cmbItem.DisplayLayout.Bands(0).Columns.Count > 0 Then
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
                str = "SELECT SalesOrderID, SalesOrderNo, SalesOrderDate, UserName, Remarks, IsNull(tblDefCostCenter.CostCenterId, 0) As CostCenterId, tblDefCostCenter.Name CostCenter, SalesOrderAmount, SalesOrderQty FROM SalesOrderMasterTable LEFT OUTER JOIN tblDefCostCenter ON SalesOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID WHERE VendorId= " & Me.cmbCustomer.Value & " And IsNull(Posted,0)=1 AND Status='Open' And IsNull(tblDefCostCenter.CostCenterId, 0) > 0 Order By SalesOrderID DESC"
                FillUltraDropDown(Me.cmbSO, str)
                Me.cmbSO.Rows(0).Activate()
                Me.cmbSO.DisplayLayout.Bands(0).Columns(0).Hidden = True
                Me.cmbSO.DisplayLayout.Bands(0).Columns(5).Hidden = True
                Me.cmbSO.DisplayLayout.Bands(0).Columns(7).Hidden = True
            ElseIf Condition = "Terms" Then
                FillDropDown(Me.cmbTermCondition, "select * From tblTermsAndConditionType", True)
            ElseIf Condition = "CostCenter" Then
                str = "SELECT  SalesOrderMasterTable.CostCenterId AS Id, tblDefCostCenter.Name FROM SalesOrderMasterTable INNER JOIN tblDefCostCenter ON SalesOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID Where SalesOrderMasterTable.SalesOrderId = " & Me.cmbSO.Value & ""
                FillDropDown(Me.cmbCostCenter, str, False)
            ElseIf Condition = "Bank" Then
                str = "Select coa_detail_id,detail_title from vwCOADetail Where account_type='Bank'"
                FillDropDown(Me.cmbBank, str, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub EditRecords()
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            Dim ContractId As Integer = 0I
            ContractId = Me.editModel.ContractId
            Me.txtDocNo.Text = Me.editModel.ContractNo
            Me.dtpDocDate.Value = Me.editModel.ContractDate
            Me.cmbCustomer.Value = Me.editModel.CustomerId
            Me.cmbSO.Value = Me.editModel.SOId
            Me.cmbItem.Value = Me.editModel.ItemId
            Me.cmbBank.SelectedValue = Me.editModel.BankId
            Me.txtChequeNo.Text = Me.editModel.ChequeNo
            Me.txtChequeAmount.Text = Me.editModel.ChequeAmount
            If Me.editModel.ChequeDate = Date.MinValue Then
                Me.dtpChequeDate.Checked = False
            Else
                Me.dtpChequeDate.Value = Me.editModel.ChequeDate
            End If
            Me.txtChequeDetails.Text = Me.editModel.ChequeDetails
            DisplayDetail(ContractId)
            Me.txtRetentionPercentage.Text = Me.editModel.RetentionPercentage
            Me.txtRetentionValue.Text = Me.editModel.RetentionValue
            Me.txtMobilizationPerBill.Text = Me.editModel.MobilizationPerBill
            Me.txtMobilizationPercentage.Text = Me.editModel.MobilizationPercentage
            Me.txtMobilizationValue.Text = Me.editModel.MobilizationValue
            Dim TermsAndConditionsId As Integer = Me.editModel.TermId
            Me.cmbTermCondition.SelectedValue = TermsAndConditionsId
            Me.DeleteToolStripMenuItem.Visible = True
            Dim str As String = ""
            Dim dt As DataTable
            str = "Select Heading,Details from tblCustomerContractTerms Where TermId=" & Me.editModel.TermId & " And ContractId = " & ContractId & ""
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

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New CustomerContractBE
            If Me.Contract_Id > 0 Then
                objModel.ContractId = Me.Contract_Id
            End If
            objModel.Detail = New List(Of CustomerContractDetailBE)
            Dim RetentionAccountId As Integer = Convert.ToInt32((getConfigValueByType("RetentionAccountCustomerPM").ToString))
            Dim MobilizationAccountId As Integer = Convert.ToInt32(Val(getConfigValueByType("MobilizationAccountCustomerPM").ToString))
            objModel.RetentionAccountId = RetentionAccountId
            objModel.MobilizationAccountId = MobilizationAccountId
            If Not MobilizationAccountId > 0 Then
                Throw New Exception("Please select the Mobilization account.")
            End If
            If Me.btnSave.Tag = "Save" Then
                objModel.ContractNo = GetDocumentNo()
            Else
                objModel.ContractNo = Me.txtDocNo.Text
            End If
            objModel.VoucherId = GetVoucherId("frmCustomerContract", objModel.ContractNo)
            objModel.ContractDate = Me.dtpDocDate.Value
            objModel.CustomerId = Me.cmbCustomer.Value
            objModel.CustomerName = Me.cmbCustomer.Text
            objModel.SOId = Me.cmbSO.Value
            objModel.SOQty = Val(Me.cmbSO.ActiveRow().Cells(8).Value)
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
                Dim Detail As New CustomerContractDetailBE
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

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT Contract.ContractId, Contract.DocNo, Contract.DocDate, Contract.ItemId, Article.ArticleDescription AS ItemName, Contract.VendorId, COA.detail_title AS CustomerName, Contract.SOId, SO.SalesOrderNo AS SONo, Contract.TermId, Contract.RetentionPercentage, Contract.RetentionValue, Contract.MobilizationPerBill, Contract.MobilizationPercentage, Contract.MobilizationValue, Contract.BankId, Contract.ChequeNo,Contract.ChequeAmount, Contract.ChequeDate, Contract.ChequeDetails FROM vwCOADetail AS COA INNER JOIN tblCustomerContractMaster AS Contract INNER JOIN ArticleDefView AS Article ON Contract.ItemId = Article.ArticleId INNER JOIN SalesOrderMasterTable AS SO ON Contract.SOId = SO.PurchaseOrderId ON COA.coa_detail_id = Contract.VendorId Order By Contract.ContractId DESC"
            dt = GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

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
            ElseIf Not Me.grd.RowCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.txtDocNo.Text = GetDocumentNo()
            Me.dtpDocDate.Value = Now
            Me.cmbCustomer.Value = 0
            Me.cmbSO.Value = 0
            Me.cmbItem.Value = 0
            DisplayDetail(-1)
            Me.btnSave.Tag = "Save"
            Me.btnSave.Enabled = True
            Me.DeleteToolStripMenuItem.Visible = False
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
            Me.txtSOAmount.Text = 0
            CtrlGrdBar3_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("CC-" + Microsoft.VisualBasic.Right(Me.dtpDocDate.Value.Year, 2) + "-", "tblCustomerContractMaster", "DocNo")
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo("CC-" & Format(Me.dtpDocDate.Value, "yy") & Me.dtpDocDate.Value.Month.ToString("00"), 4, "tblCustomerContractMaster", "DocNo")
            Else
                Return GetNextDocNo("CC-", 6, "tblCustomerContractMaster", "DocNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub DisplayDetail(ByVal Id As Integer)
        Try
            Dim str As String = ""
            Dim dt As DataTable
            If Me.btnSave.Tag = "Save" Then
                str = "SELECT 0 AS Id, ATS.ItemId, ATS.Id AS TaskId, ATS.TaskTitle, ATS.TaskDetail, ATS.TaskUnit, CONVERT(Decimal(18," & DecimalPointInValue & "), ATS.TaskRate) TaskRate, CONVERT(Decimal(18," & DecimalPointInQty & "), SO.Qty) Qty, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) TotalMeasurment, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) NetValue FROM ArticleDefTaskDetails AS ATS LEFT OUTER JOIN(SELECT ArticleDefId, Sz1 AS Qty FROM SalesOrderDetailTable WHERE (SalesOrderId = " & cmbSO.Value & ")) AS SO ON ATS.ItemId = SO.ArticleDefId  WHERE (ATS.ItemId = " & Id & ")"
            Else
                str = "SELECT VCD.Id, VCM.ItemId, VCD.Id AS TaskId, VCD.TaskTitle, VCD.TaskDetail, VCD.TaskUnit, CONVERT(Decimal(18," & DecimalPointInValue & "), VCD.TaskRate) TaskRate, CONVERT(Decimal(18," & DecimalPointInQty & "), IsNull(VCD.Qty,0)) AS Qty, CONVERT(Decimal(18," & DecimalPointInValue & "), VCD.TotalMeasurment) TotalMeasurment, CONVERT(Decimal(18," & DecimalPointInValue & "), VCD.NetValue) NetValue FROM tblCustomerContractMaster AS VCM INNER JOIN tblCustomerContractDetail AS VCD ON VCM.ContractId = VCD.ContractId INNER JOIN SalesOrderDetailTable ON VCM.SOId = SalesOrderDetailTable.SalesOrderId WHERE VCM.ItemId =" & Me.cmbItem.Value & " And VCM.SOId = " & Me.cmbSO.Value & " And VCD.ContractId = " & Id & ""
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

    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            objDAL = New CustomerContractDAL
            If IsValidate() = True Then
                If objDAL.Save(objModel) = True Then
                    If Me.chkApprove.Checked = True Then
                        objDAL.SaveVoucher(objModel)
                    End If
                    SaveTerms()
                    'Insert Activity Log 
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
                str = "Delete from tblCustomerContractTerms Where TermId=" & objModel.TermId & " and ContractId=" & objModel.ContractId & " "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                For Each row As Janus.Windows.GridEX.GridEXRow In Me.grdTerms.GetDataRows
                    If Me.btnSave.Tag = "Save" Then
                        str = "Insert into tblCustomerContractTerms (TermId,ContractId,Heading,Details) Values (" & Me.cmbTermCondition.SelectedValue & ",ident_current('tblCustomerContractMaster'),N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
                    Else
                        str = "Insert into tblCustomerContractTerms (TermId,ContractId,Heading,Details) Values (" & Me.cmbTermCondition.SelectedValue & "," & objModel.ContractId & ",N'" & row.Cells("Heading").Value.ToString & "',N'" & row.Cells("Details").Value.ToString & "')"
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

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            objDAL = New CustomerContractDAL
            If IsValidate() = True Then
                If objDAL.Update(objModel) = True Then
                    objDAL.UpdateVoucher(objModel)
                    SaveTerms()
                    'Insert Activity Log
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

    Private Sub CtrlGrdBar3_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar3.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Customer Contract"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomerContract_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            ReSetControls()
            Me.btnSave.Tag = "Save"
            Me.DeleteToolStripMenuItem.Visible = False
            Me.cmbCustomer.Enabled = True
            Me.cmbSO.Enabled = True
            Me.cmbItem.Enabled = True
            Me.btnPrint.Visible = False
            Me.btnCancel.Location = New Point(825, 12)
            Me.DeleteToolStripMenuItem.Visible = False
            FillCombos("Item")
            FillCombos("Customer")
            FillCombos("SO")
            FillCombos("Terms")
            FillCombos("Bank")
            DisplayDetail(-1)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            Dim CustomerId As Integer = 0I
            Dim SOId As Integer = 0I
            Dim ItemId As Integer = 0I
            Dim TermId As Integer = 0I
            Dim BankId As Integer = 0I
            CustomerId = Me.cmbCustomer.Value
            SOId = Me.cmbSO.Value
            ItemId = Me.cmbItem.Value
            TermId = Me.cmbTermCondition.SelectedValue
            BankId = Me.cmbBank.SelectedValue

            FillCombos("Customer")
            Me.cmbCustomer.Value = CustomerId

            FillCombos("SO")
            Me.cmbSO.Value = SOId

            FillCombos("Item")
            Me.cmbItem.Value = ItemId

            FillCombos("Terms")
            Me.cmbTermCondition.SelectedValue = TermId

            FillCombos("Bank")
            Me.cmbBank.SelectedValue = BankId
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

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

    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            FillCombos("SO")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub cmbSO_ValueChanged(sender As Object, e As EventArgs) Handles cmbSO.ValueChanged
        Try
            FillCombos("Item")
            FillCombos("CostCenter")
            'Set PO Net Amount to text box from drop down column
            If Me.cmbSO.Value > 0 Then
                Me.txtSOAmount.Text = Math.Round(Me.cmbSO.ActiveRow().Cells(7).Value, DecimalPointInValue)
                If IsNumeric(Me.txtSOAmount.Text) Then
                    Me.txtSOAmount.Text = String.Format("{0:#,##0.00}", CInt(Me.txtSOAmount.Text))
                End If
            End If
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

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Me.Cursor = Cursors.WaitCursor
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then
                msg_Information(str_informDelete)
                NewToolStripMenuItem_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            Me.lblProgress.Visible = False
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            objDAL = New CustomerContractDAL
            If e.Column.Key = "Delete" Then
                If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
                objDAL.DeleteDetail(Val(Me.grd.CurrentRow.Cells(grdenm.Id).Value.ToString))
                Me.grd.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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

    Private Sub grdTerms_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdTerms.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.grdTerms.GetRow.Delete()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRetentionPercentage_Leave(sender As Object, e As EventArgs) Handles txtRetentionPercentage.Leave
        Try
            If Me.txtRetentionPercentage.Text = 0 Then
                Me.txtRetentionValue.Text = 0
                Exit Sub
            End If
            ContractValue = Me.grd.GetTotal(Me.grd.RootTable.Columns(grdenm.NetValue), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.txtRetentionValue.Text = Math.Round(ContractValue * Me.txtRetentionPercentage.Text / 100, DecimalPointInValue)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

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

    Private Sub txtMobilizationValue_TextChanged(sender As Object, e As EventArgs) Handles txtMobilizationValue.TextChanged
        Try
            If IsNumeric(Me.txtMobilizationValue.Text) Then
                Me.txtMobilizationValue.Text = Me.txtMobilizationValue.Text
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtRetentionValue_TextChanged(sender As Object, e As EventArgs) Handles txtRetentionValue.TextChanged
        Try
            If IsNumeric(Me.txtRetentionValue.Text) Then
                Me.txtRetentionValue.Text = Me.txtRetentionValue.Text
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
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
            AddRptParam("@ContractId", 1)
            ShowReport("rptCustomerContract")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class