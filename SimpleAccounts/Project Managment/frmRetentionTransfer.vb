'TFS3435: Waqar Added this screen to transfer the Retention of 3G customer

Imports SBDal
Imports SBModel
Imports System.Data.OleDb
Public Class frmRetentionTransfer
    Implements IGeneral
    Dim VoucherNo As String
    Dim objModel As RetentionTransferBE
    Dim Detail As List(Of RetentionTransferDetail)
    Dim objDal As New RetentionTransferDAL
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
            Me.grdRetentionTransfer.RootTable.Columns("AmountPaid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("AmountPaid").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("AmountPaid").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("AmountPaid").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("BalanceAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("BalanceAmount").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("BalanceAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("BalanceAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("RententionValue").Caption = "Retention Value"
            Me.grdRetentionTransfer.RootTable.Columns("RetentionPaid").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("RetentionPaid").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RetentionPaid").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RetentionPaid").CellStyle.BackColor = Color.LightYellow
            Me.grdRetentionTransfer.RootTable.Columns("TransferPer").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("TransferPer").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("TransferPer").TotalFormatString = "N" & DecimalPointInValue
            'Me.grdRetentionTransfer.RootTable.Columns("TransferPer").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("TransferPer").CellStyle.BackColor = Color.LightYellow
            Me.grdRetentionTransfer.RootTable.Columns("RealizedAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("RealizedAmount").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RealizedAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("RealizedAmount").EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grdRetentionTransfer.RootTable.Columns("CurrentPayables").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdRetentionTransfer.RootTable.Columns("CurrentPayables").FormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("CurrentPayables").TotalFormatString = "N" & DecimalPointInValue
            Me.grdRetentionTransfer.RootTable.Columns("CurrentPayables").EditType = Janus.Windows.GridEX.EditType.NoEdit
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
            If Condition = "Vendor" Then
                str = "SELECT     dbo.vwCOADetail.coa_detail_id, dbo.vwCOADetail.detail_title as Name " & _
                            "FROM dbo.tblCustomer INNER JOIN " & _
                            "dbo.tblListTerritory ON dbo.tblCustomer.Territory = dbo.tblListTerritory.TerritoryId INNER JOIN " & _
                            "dbo.tblListCity ON dbo.tblListTerritory.CityId = dbo.tblListCity.CityId INNER JOIN " & _
                            "dbo.tblListState ON dbo.tblListCity.StateId = dbo.tblListState.StateId RIGHT OUTER JOIN " & _
                            "dbo.vwCOADetail ON dbo.tblCustomer.AccountId = dbo.vwCOADetail.coa_detail_id WHERE dbo.vwCOADetail.detail_title <> '' "
                str += " AND   (dbo.vwCOADetail.account_type = 'Vendor') "
                str += " AND vwCOADetail.Active=1"
                FillUltraDropDown(cmbVendor, str, True)
                Me.cmbVendor.Rows(0).Activate()
                If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
                End If
                UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

            ElseIf Condition = "PO" Then
                str = "SELECT PurchaseOrderID, PurchaseOrderNo, PurchaseOrderDate, UserName, Remarks, IsNull(tblDefCostCenter.CostCenterId, 0) As CostCenterId, tblDefCostCenter.Name CostCenter, PurchaseOrderAmount, PurchaseOrderQty FROM PurchaseOrderMasterTable LEFT OUTER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID WHERE VendorId= " & Me.cmbVendor.Value & " And IsNull(Post,0)=1 AND Status='Open' Order By PurchaseOrderID DESC"
                FillDropDown(Me.cmbPONo, str, True)
            ElseIf Condition = "CostCenter" Then
                str = "SELECT  PurchaseOrderMasterTable.CostCenterId AS Id, tblDefCostCenter.Name FROM PurchaseOrderMasterTable INNER JOIN tblDefCostCenter ON PurchaseOrderMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " & IIf(cmbPONo.SelectedValue > 0, " Where PurchaseOrderMasterTable.PurchaseOrderId = " & Me.cmbPONo.SelectedValue & "", "")
                FillDropDown(Me.cmbCostCenter, str, False)
            ElseIf Condition = "VendorContract" Then
                str = "select ContractId, DocNo from tblVendorContractMaster Where POId = " & Me.cmbPONo.SelectedValue & ""
                FillUltraDropDown(Me.cmbVendorContract, str, True)
                Me.cmbVendorContract.Rows(0).Activate()
                If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendorContract.DisplayLayout.Bands(0).Columns("ContractId").Hidden = True
                End If
                UltraDropDownSearching(cmbVendorContract, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

                ElseIf Condition = "Item" Then
                    str = "SELECT tblVendorContractMaster.ItemId, ArticleDefView.ArticleDescription FROM tblVendorContractMaster INNER JOIN ArticleDefView ON tblVendorContractMaster.ItemId = ArticleDefView.ArticleId Where ContractId = " & Me.cmbVendorContract.Value & ""
                    FillDropDown(Me.cmbItem, str, True)
                End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional Condition As String = "") Implements IGeneral.FillModel
        Try
            objModel = New RetentionTransferBE
            objModel.RetentionMasterId = RetentionMasterId
            objModel.VoucherNo = txtVoucherNo.Text
            objModel.VoucherDate = dtpVoucherDate.Value
            objModel.VendorId = cmbVendor.Value
            objModel.POId = cmbPONo.SelectedValue
            objModel.VendorConractId = cmbVendorContract.Value
            objModel.CostCenterId = cmbCostCenter.SelectedValue
            objModel.ArticleId = cmbItem.SelectedValue
            objModel.Remarks = txtRemarks.Text
            objModel.RetentionAccountId = Convert.ToInt32(Val(getConfigValueByType("RetentionAccount").ToString))
            Detail = New List(Of RetentionTransferDetail)
            For Each Row As Janus.Windows.GridEX.GridEXRow In Me.grdRetentionTransfer.GetDataRows
                Dim WDetail As New RetentionTransferDetail
                WDetail.RetentionDetailId = Val(Row.Cells("RetentionDetailId").Value.ToString)
                WDetail.ContractId = cmbVendorContract.Value
                WDetail.ContractValue = Row.Cells("ContractValue").Value.ToString
                WDetail.AmountPaid = Row.Cells("AmountPaid").Value.ToString
                WDetail.BalanceAmount = Row.Cells("BalanceAmount").Value.ToString
                WDetail.RententionValue = Row.Cells("RententionValue").Value.ToString
                WDetail.RetentionPaid = Row.Cells("RetentionPaid").Value.ToString
                WDetail.TransferPer = Row.Cells("TransferPer").Value.ToString
                WDetail.RealizedAmount = Row.Cells("RealizedAmount").Value.ToString
                WDetail.CurrentPayables = Row.Cells("CurrentPayables").Value.ToString
                WDetail.Remarks = txtRemarks.Text
                objModel.TransferPer = Row.Cells("TransferPer").Value.ToString
                objModel.CurrentPayables = Row.Cells("CurrentPayables").Value.ToString
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
            If Me.cmbVendor.Value = 0 Then
                msg_Error("Select a Vendor")
                Return False
            End If
            If Me.cmbVendorContract.Value = 0 Then
                msg_Error("Select a Vendor Contract")
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

    Private Sub cmbVCNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVendorContract.ValueChanged
        Try
            Dim str As String
            Dim dt As DataTable
            If Not cmbVendorContract.Value Is Nothing Then
                FillCombos("Item")
                str = "select ContractId from RetentionDetailTable WHERE ContractId = " & cmbVendorContract.Value & ""
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
                Return GetSerialNo("RT-" + Microsoft.VisualBasic.Right(Me.dtpVoucherDate.Value.Year, 2) + "-", "RetentionMasterTable", "VoucherNo")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo("RT-" & Format(Me.dtpVoucherDate.Value, "yy") & Me.dtpVoucherDate.Value.Month.ToString("00"), 4, "RetentionMasterTable", "VoucherNo")
                    Else
                        VoucherNo = GetNextDocNo("RT", 6, "RetentionMasterTable", "VoucherNo")
                        Return VoucherNo
                    End If
                Else
                    VoucherNo = GetNextDocNo("RT", 6, "RetentionMasterTable", "VoucherNo")
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
                    'strDisplay = "Select Top 1 RetentionDetailId, RetentionMasterId, ContractId, ContractValue, AmountPaid, BalanceAmount, RententionValue, RealizedAmount as RetentionPaid, TransferPer, RealizedAmount, CurrentPayables  from RetentionDetailTable where ContractId = " & cmbVendorContract.Value & " ORDER BY RetentionDetailId Desc"
                    strDisplay = "SELECT TOP 1 RetentionDetailTable.RetentionDetailId, RetentionDetailTable.RetentionMasterId, RetentionDetailTable.ContractId, RetentionDetailTable.ContractValue, RetentionDetailTable.AmountPaid, RetentionDetailTable.BalanceAmount, tblVendorContractMaster.RetentionValue AS RententionValue, RetentionDetailTable.RealizedAmount AS RetentionPaid, RetentionDetailTable.TransferPer, RetentionDetailTable.RealizedAmount, RetentionDetailTable.CurrentPayables FROM RetentionDetailTable INNER JOIN tblVendorContractMaster ON RetentionDetailTable.ContractId = tblVendorContractMaster.ContractId WHERE RetentionDetailTable.ContractId = " & cmbVendorContract.Value & " ORDER BY RetentionDetailTable.RetentionDetailId DESC"
                Else
                    strDisplay = "Select * from RetentionDetailTable where RetentionMasterId = " & RetentionMasterId
                    btnSave.Enabled = DoHaveUpdateRights
                End If

            Else
                strDisplay = "SELECT 0 as RetentionDetailId, 0 as RetentionMasterId, 0 as ContractId, tblVendorContractMaster.ContractValue, (select sum(Debit_Amount) from tblVoucherDetail where coa_detail_id = " & cmbVendor.Value & " and CostCenterID = " & cmbCostCenter.SelectedValue & ") as AmountPaid, 0 as BalanceAmount,  ISNULL(tblVendorContractMaster.RetentionValue, 0) as RententionValue, 0.000 AS RetentionPaid, 0.000 as TransferPer, 0.000 as RealizedAmount, 0.000 as CurrentPayables FROM tblVendorContractMaster Left Outer JOIN tblProjectProgressApproval ON tblVendorContractMaster.ContractId = tblProjectProgressApproval.ContractId where tblVendorContractMaster.ContractId = " & cmbVendorContract.Value & " group by tblVendorContractMaster.RetentionValue, tblVendorContractMaster.ContractValue"
            End If
                dtDisplay = GetDataTable(strDisplay)
                If dtDisplay.Rows.Count > 0 Then
                    If EditMode = False Then
                        If dtDisplay.Rows(0).Item("TransferPer") = 100 Then
                            msg_Information("Retention Transfer on this Vendor Contract is Completed")
                            Exit Function
                        Else
                        grdRetentionTransfer.DataSource = dtDisplay
                        grdRetentionTransfer.RetrieveStructure()
                        dtDisplay.Columns("BalanceAmount").Expression = "ContractValue-AmountPaid"
                        dtDisplay.Columns("RealizedAmount").Expression = "(RententionValue*TransferPer)/100"
                        dtDisplay.Columns("CurrentPayables").Expression = "RealizedAmount-RetentionPaid"
                        ApplyGridSettings()
                        End If
                    Else
                        grdRetentionTransfer.DataSource = dtDisplay
                    grdRetentionTransfer.RetrieveStructure()
                        dtDisplay.Columns("BalanceAmount").Expression = "ContractValue-AmountPaid"
                        dtDisplay.Columns("RealizedAmount").Expression = "(RententionValue*TransferPer)/100"
                    dtDisplay.Columns("CurrentPayables").Expression = "RealizedAmount-ISNULL(RetentionPaid, 0)"
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
                        msg_Information("Current Payables should be greater than Zero.")
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

    'Private Sub frmRetentionTransfer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
    '    Try
    '        cmbVendor.Value = 0
    '        cmbVendorContract.Value = 0
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub

    Private Sub frmRetentionTransfer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            
            btnSave.FlatAppearance.BorderSize = 0
            btnCancel.FlatAppearance.BorderSize = 0
            FillCombos("Vendor")
            FillCombos("PO")
            FillCombos("CostCenter")
            FillCombos("VendorContract")
            FillCombos("Item")
            UltraDropDownSearching(cmbVendorContract, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            UltraDropDownSearching(cmbVendor, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)
            Dim dt As DataTable
            Dim dttrans As DataTable
            Dim str As String = "select TransferPer from RetentionDetailTable where RetentionMasterId = " & RetentionMasterId & ""
            dttrans = GetDataTable(str)
            If dttrans.Rows.Count > 0 Then
                TansferPercentage = dttrans.Rows(0).Item("TransferPer")
            End If
            dt = New RetentionTransferDAL().GetById(RetentionMasterId)
            Dim i As Integer
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    Me.txtVoucherNo.Text = dt.Rows(0).Item("VoucherNo")
                    Me.dtpVoucherDate.Value = dt.Rows(0).Item("VoucherDate")
                    Me.cmbVendor.Value = dt.Rows(0).Item("VendorId")
                    Me.cmbPONo.SelectedValue = dt.Rows(0).Item("POId")
                    Me.cmbVendorContract.Value = dt.Rows(0).Item("VendorConractId")
                    cmbCostCenter.SelectedValue = dt.Rows(0).Item("CostCenterId")
                    cmbItem.SelectedValue = dt.Rows(0).Item("ArticleId")
                    txtRemarks.Text = dt.Rows(0).Item("Remarks")
                Next
            Else
                ReSetControls()
            End If
            Me.cmbVendor.Focus()
            'DisplayRecords(RetentionMasterId)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVendor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbVendor.ValueChanged
        Try
            FillCombos("PO")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPONo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPONo.SelectedIndexChanged
        Try
            FillCombos("CostCenter")
            FillCombos("VendorContract")
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
            ShowReport("rptRetentionTransfer")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class