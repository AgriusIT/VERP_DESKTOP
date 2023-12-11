Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmCustomerRetentionTransfer
    Implements IGeneral
    Dim VoucherNo As String
    Dim objModel As CustomerRetentionTransferBE
    Dim Detail As List(Of CustomerRetentionTransferDetail)
    Dim objDal As New CustomerRetentionTransferDAL
    Public RetentionMasterId As Integer
    Public DoHaveSaveRights As Boolean = False
    Public DoHaveUpdateRights As Boolean = False
    Public EditMode As Boolean = False
    Dim TansferPercentage As Double
    Dim NetValue As Double
    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grdRetentionTransfer.RootTable.Columns("RetentionDetailId").Visible = False
            Me.grdRetentionTransfer.RootTable.Columns("RetentionMasterId").Visible = False
            Me.grdRetentionTransfer.RootTable.Columns("ContractId").Visible = False
            Me.grdRetentionTransfer.RootTable.Columns("ContractValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("ContractValue").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("ContractValue").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("ContractValue").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("AmountReceived").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("AmountReceived").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("AmountReceived").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("AmountReceived").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("BalanceAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("BalanceAmount").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("BalanceAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("BalanceAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("RetentionReceived").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("RetentionReceived").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RetentionReceived").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RetentionReceived").CellStyle.BackColor = Color.LightYellow
            Me.grdRetentionTransfer.RootTable.Columns("TransferPer").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("TransferPer").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("TransferPer").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("TransferPer").CellStyle.BackColor = Color.LightYellow
            Me.grdRetentionTransfer.RootTable.Columns("RealizedAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("RealizedAmount").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RealizedAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RealizedAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("CurrentReceivables").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("CurrentReceivables").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("CurrentReceivables").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("CurrentReceivables").EditType = Janus.Windows.GridEX.EditType.NoEdit
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub

    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function

    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String
            If Condition = "Customer" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title as Name " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
                str += " AND   (dbo.vwCOADetail.account_type = 'Customer') "
                str += " AND vwCOADetail.Active=1"
                FillUltraDropDown(cmbCustomer, str, True)
                Me.cmbCustomer.Rows(0).Activate()
                If cmbCustomer.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If

            ElseIf Condition = "SO" Then
                str = "SELECT SalesOrderId, SalesOrderNo, SalesOrderDate, UserName, Remarks, IsNull(tblDefCostCenter.CostCenterId, 0) As CostCenterId, tblDefCostCenter.Name CostCenter, SalesOrderAmount, SalesOrderQty FROM SalesOrderMasterTable LEFT OUTER JOIN tblDefCostCenter ON SalesOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID WHERE VendorId= " & Me.cmbCustomer.Value & " AND Status='Open' Order By SalesOrderId DESC"
                FillDropDown(Me.cmbSONo, str, True)
            ElseIf Condition = "CostCenter" Then
                str = "SELECT  SalesOrderMasterTable.CostCenterId AS Id, tblDefCostCenter.Name FROM SalesOrderMasterTable INNER JOIN tblDefCostCenter ON SalesOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " & IIf(cmbSONo.SelectedValue > 0, " Where SalesOrderMasterTable.SalesOrderId = " & Me.cmbSONo.SelectedValue & "", "")
                FillDropDown(Me.cmbCostCenter, str, False)
            ElseIf Condition = "Contract" Then
                str = "select ContractId, DocNo from tblCustomerContractMaster Where SOId = " & Me.cmbSONo.SelectedValue & ""
                FillUltraDropDown(Me.cmbContract, str, True)
                Me.cmbContract.Rows(0).Activate()
                If cmbCustomer.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbContract.DisplayLayout.Bands(0).Columns("ContractId").Hidden = True
                End If
            ElseIf Condition = "Item" Then
                str = "SELECT tblCustomerContractMaster.ItemId, ArticleDefView.ArticleDescription FROM tblCustomerContractMaster INNER JOIN ArticleDefView ON tblCustomerContractMaster.ItemId = ArticleDefView.ArticleId Where ContractId = " & Me.cmbContract.Value & ""
                FillDropDown(Me.cmbItem, str, True)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New CustomerRetentionTransferBE
            objModel.RetentionMasterId = RetentionMasterId
            objModel.VoucherNo = txtVoucherNo.Text
            objModel.VoucherDate = dtpVoucherDate.Value
            objModel.CustomerId = cmbCustomer.Value
            objModel.SOId = cmbSONo.SelectedValue
            objModel.ContractId = cmbContract.Value
            objModel.CostCenterId = cmbCostCenter.SelectedValue
            objModel.ArticleId = cmbItem.SelectedValue
            objModel.Remarks = txtRemarks.Text
            objModel.RetentionAccountId = Convert.ToInt32(Val(getConfigValueByType("RetentionAccountCustomerPM").ToString))
            Detail = New List(Of CustomerRetentionTransferDetail)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdRetentionTransfer.GetDataRows
                Dim WDetail As New CustomerRetentionTransferDetail
                WDetail.RetentionDetailId = Val(Row.Cells("RetentionDetailId").Value.ToString)
                WDetail.ContractId = cmbContract.Value
                WDetail.ContractValue = Row.Cells("ContractValue").Value.ToString
                WDetail.AmountReceived = Row.Cells("AmountReceived").Value.ToString
                WDetail.BalanceAmount = Row.Cells("BalanceAmount").Value.ToString
                WDetail.RententionValue = Row.Cells("RententionValue").Value.ToString
                WDetail.RetentionReceived = Row.Cells("RetentionReceived").Value.ToString
                WDetail.TransferPer = Row.Cells("TransferPer").Value.ToString
                WDetail.RealizedAmount = Row.Cells("RealizedAmount").Value.ToString
                WDetail.CurrentReceivables = Row.Cells("CurrentReceivables").Value.ToString
                WDetail.Remarks = txtRemarks.Text
                objModel.TransferPer = Row.Cells("TransferPer").Value.ToString
                objModel.CurrentReceivables = Row.Cells("CurrentReceivables").Value.ToString
                Detail.Add(WDetail)
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords

    End Sub

    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.cmbCustomer.Value = 0 Then
                msg_Error("Select a Customer")
                Return False
            End If
            If Me.cmbContract.Value = 0 Then
                msg_Error("Select a Customer Contract")
                Return False
            End If
            If Me.grdRetentionTransfer.RowCount = 0 Then
                msg_Error("No record found in grid")
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
            dtpVoucherDate.Value = Date.Now
            txtVoucherNo.Text = GetVoucherNo()
            txtRemarks.Text = ""
            btnSave.Enabled = DoHaveSaveRights
            btnPrint.Visible = False
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

    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update

    End Function

    Private Sub cmbItem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItem.SelectedIndexChanged

    End Sub

    Private Sub cmbContract_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbContract.ValueChanged
        Try
            Dim str As String
            Dim dt As DataTable
            If Not cmbContract.Value Is Nothing Then
                FillCombos("Item")
                str = "select ContractId from CustomerRetentionDetailTable WHERE ContractId = " & cmbContract.Value & ""
                dt = GetDataTable(str)
                Displaydetail(dt)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetVoucherNo()
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo("CRT-" + Microsoft.VisualBasic.Right(Me.dtpVoucherDate.Value.Year, 2) + "-", "CustomerRetentionMasterTable", "VoucherNo")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo("CRT-" & Format(Me.dtpVoucherDate.Value, "yy") & Me.dtpVoucherDate.Value.Month.ToString("00"), 4, "CustomerRetentionMasterTable", "VoucherNo")
                    Else
                        VoucherNo = GetNextDocNo("CRT", 6, "CustomerRetentionMasterTable", "VoucherNo")
                        Return VoucherNo
                    End If
                Else
                    VoucherNo = GetNextDocNo("CRT", 6, "CustomerRetentionMasterTable", "VoucherNo")
                    Return VoucherNo
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function Displaydetail(ByVal dt As DataTable)
        Try
            Dim strDisplay As String
            Dim dtDisplay As DataTable
            If dt.Rows.Count > 0 Then
                If EditMode = False Then
                    strDisplay = "Select Top 1 RetentionDetailId, RetentionMasterId, ContractId, ContractValue, AmountReceived, BalanceAmount, RententionValue, RetentionReceived as RetentionReceived, TransferPer, RealizedAmount, CurrentReceivables  from CustomerRetentionDetailTable where ContractId = " & cmbContract.Value & " ORDER BY RetentionDetailId Desc"
                Else
                    strDisplay = "Select * from CustomerRetentionDetailTable where RetentionMasterId = " & RetentionMasterId
                    btnSave.Enabled = DoHaveUpdateRights
                End If

            Else
                strDisplay = "SELECT 0 as RetentionDetailId, 0 as RetentionMasterId, 0 as ContractId, tblCustomerContractMaster.ContractValue, (select sum(Debit_Amount) from tblVoucherDetail where coa_detail_id = " & cmbCustomer.Value & " and CostCenterID = " & cmbCostCenter.SelectedValue & ") as AmountReceived, 0 as BalanceAmount,  ISNULL(tblCustomerContractMaster.RetentionValue, 0) as RententionValue, 0.000 AS RetentionReceived, 0.000 as TransferPer, 0.000 as RealizedAmount, 0.000 as CurrentReceivables FROM tblCustomerContractMaster Left Outer JOIN tblProjectProgressApproval ON tblCustomerContractMaster.ContractId = tblProjectProgressApproval.ContractId where tblCustomerContractMaster.ContractId = " & cmbContract.Value & " group by tblCustomerContractMaster.RetentionValue, tblCustomerContractMaster.ContractValue"
            End If
            dtDisplay = GetDataTable(strDisplay)
            If dtDisplay.Rows.Count > 0 Then
                If EditMode = False Then
                    If dtDisplay.Rows(0).Item("TransferPer") = 100 Then
                        msg_Information("Retention Transfer on this Customer Contract is Completed")
                        Exit Function
                    Else
                        grdRetentionTransfer.DataSource = dtDisplay
                        grdRetentionTransfer.RetrieveStructure()
                        dtDisplay.Columns("BalanceAmount").Expression = "ContractValue-AmountReceived"
                        dtDisplay.Columns("RealizedAmount").Expression = "(RententionValue*TransferPer)/100"
                        dtDisplay.Columns("CurrentReceivables").Expression = "RealizedAmount-RetentionReceived"
                        ApplyGridSettings()
                    End If
                Else
                    grdRetentionTransfer.DataSource = dtDisplay
                    grdRetentionTransfer.RetrieveStructure()
                    dtDisplay.Columns("BalanceAmount").Expression = "ContractValue-AmountReceived"
                    dtDisplay.Columns("RealizedAmount").Expression = "(RententionValue*TransferPer)/100"
                    dtDisplay.Columns("CurrentReceivables").Expression = "RealizedAmount-ISNULL(RetentionReceived, 0)"
                    ApplyGridSettings()
                End If
            Else
                grdRetentionTransfer.DataSource = Nothing
                grdRetentionTransfer.RetrieveStructure()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If IsValidate() = True Then
                If RetentionMasterId = 0 Then
                    If objDal.Add(objModel) Then
                        If objDal.AddRetentionDetail(Detail) Then
                            msg_Information("Record has been saved successfully.")
                            SaveActivityLog("Config", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Configuration, txtVoucherNo.Text, True)
                            ReSetControls()
                            Me.Close()
                        End If
                    Else
                        msg_Information("Current Receivables should be greater than Zero.")
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If objDal.Update(objModel) Then
                        If objDal.UpdateRetentionDetail(Detail) Then
                            msg_Information("Record has been Updated successfully.")
                            SaveActivityLog("Config", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Configuration, txtVoucherNo.Text, True)
                            ReSetControls()
                            Me.Close()
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmRetentionTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            btnSave.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.BorderSize = 0
            FillCombos("Customer")
            FillCombos("SO")
            FillCombos("CostCenter")
            FillCombos("Contract")
            FillCombos("Item")
            UltraDropDownSearching(cmbContract, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbCustomer, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            Dim dt As DataTable
            Dim dttrans As DataTable
            Dim str As String = "select TransferPer from CustomerRetentionDetailTable where RetentionMasterId = " & RetentionMasterId & ""
            dttrans = GetDataTable(str)
            If dttrans.Rows.Count > 0 Then
                TansferPercentage = dttrans.Rows(0).Item("TransferPer")
            End If
            dt = New CustomerRetentionTransferDAL().GetById(RetentionMasterId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Me.txtVoucherNo.Text = dt.Rows(0).Item("VoucherNo")
                    Me.dtpVoucherDate.Value = dt.Rows(0).Item("VoucherDate")
                    Me.cmbCustomer.Value = dt.Rows(0).Item("CustomerId")
                    Me.cmbSONo.SelectedValue = dt.Rows(0).Item("SOId")
                    Me.cmbContract.Value = dt.Rows(0).Item("ContractId")
                    cmbCostCenter.SelectedValue = dt.Rows(0).Item("CostCenterId")
                    cmbItem.SelectedValue = dt.Rows(0).Item("ArticleId")
                    txtRemarks.Text = dt.Rows(0).Item("Remarks")
                Next
                btnPrint.Visible = True
            Else
                ReSetControls()
            End If
            Me.cmbCustomer.Focus()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            FillCombos("SO")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPONo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSONo.SelectedIndexChanged
        Try
            FillCombos("CostCenter")
            FillCombos("Contract")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdRetentionTransfer_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdRetentionTransfer.CellEdited
        Try
            Me.grdRetentionTransfer.UpdateData()
            If Me.grdRetentionTransfer.CurrentRow.Cells("TransferPer").Value > 100 Then
                msg_Information("Entered value exceeds the limit of 100%")
                Me.grdRetentionTransfer.CurrentRow.Cells("TransferPer").Value = 100
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdRetentionTransfer_CellValueChanged(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdRetentionTransfer.CellValueChanged
        Try
            NetValue = Me.grdRetentionTransfer.CurrentRow.Cells("TransferPer").Value
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdRetentionTransfer_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdRetentionTransfer.CellUpdated
        Try
            If EditMode = True Then
                If Me.grdRetentionTransfer.CurrentRow.Cells("TransferPer").Value < 0 Or Me.grdRetentionTransfer.CurrentRow.Cells("TransferPer").Value < TansferPercentage Then
                    msg_Information("Entered value is less than the Net Progress")
                    Me.grdRetentionTransfer.CurrentRow.Cells("TransferPer").Value = TansferPercentage
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdRetentionTransfer_FormattingRow(sender As Object, e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdRetentionTransfer.FormattingRow
        Try
            Dim rowStyle As New Janus.Windows.GridEX.GridEXFormatStyle
            If e.Row.Cells("TransferPer").Value = 100 Then
                rowStyle.BackColor = Color.LightGray
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbVendor_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            GetCrystalReportRights()
            AddRptParam("@id", Me.RetentionMasterId)
            ShowReport("rptCustomerRetentionTransfer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class