''TASK : TFS1373 added new field of Bank Credit Limit might allocate credit limit for a bank. Done by Ameen on 23-08-2017
Imports SBModel
Imports SBDal

Public Class frmLetterCredit
    Implements IGeneral
    Dim letter As lettercreditBE
    Dim str As String = String.Empty
    Dim int As Integer
    Dim LCdoc_Id As Integer = 0
    Dim flg As Boolean = False
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If
            If Me.btnSave.Enabled = False Then Exit Sub
            If IsDateLock(Me.DateTimePicker1.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    'If Not msg_Confirm(str_ConfirmSave) = True Then
                    'Exit Sub
                    'End If
                    If Save() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        'msg_Information(str_informSave)
                        ReSetControls()
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then
                        Exit Sub
                    End If
                    If Update1() = True Then
                        DialogResult = Windows.Forms.DialogResult.Yes
                        'msg_Information(str_informUpdate)
                        ReSetControls()
                    End If
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            grdLetterOfCreadit_DoubleClick(Nothing, Nothing)
            'EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdLetterOfCreadit_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLetterOfCreadit.DoubleClick
        Try


            'EditRecords()
            If Me.grdLetterOfCreadit.RowCount = 0 Then Exit Sub
            Me.txtdocnumber.ReadOnly = True
            LCdoc_Id = Me.grdLetterOfCreadit.GetRow.Cells(0).Value
            Me.txtdocnumber.Text = Me.grdLetterOfCreadit.GetRow.Cells("lcdoc_No").Value.ToString
            Me.DateTimePicker1.Value = Me.grdLetterOfCreadit.GetRow.Cells("lcdoc_Date").Value.ToString
            Me.cmbdoctype.Text = Me.grdLetterOfCreadit.GetRow.Cells("lcdoc_type").Value.ToString
            Me.cmbbank.SelectedValue = Me.grdLetterOfCreadit.GetRow.Cells("Bank").Value.ToString()
            Me.cmbVendor.Value = Val(Me.grdLetterOfCreadit.GetRow.Cells("VendorId").Value.ToString)
            Me.txtLCamount.Text = Val(Me.grdLetterOfCreadit.GetRow.Cells("lcAmount").Value.ToString)
            Me.txtpaidamount.Text = Val(Me.grdLetterOfCreadit.GetRow.Cells("paidamount").Value.ToString)
            Me.cmbtypeCFD.Text = Me.grdLetterOfCreadit.GetRow.Cells("lcType").Value.ToString
            Me.txtremarks.Text = Me.grdLetterOfCreadit.GetRow.Cells("LCDescription").Value.ToString
            Me.cmbmethod.SelectedValue = Me.grdLetterOfCreadit.GetRow.Cells("VoucherTypeId").Value.ToString
            Me.cmbpaymentfrom.SelectedValue = Me.grdLetterOfCreadit.GetRow.Cells("coa_detail_id").Value.ToString()
            If Not Me.grdLetterOfCreadit.GetRow.Cells("Cheque_Date").Value.ToString = "" Then
                Me.dateCheque.Value = Me.grdLetterOfCreadit.GetRow.Cells("Cheque_Date").Value
            Else
                Me.dateCheque.Value = Date.Now
            End If
            If Not Me.grdLetterOfCreadit.GetRow.Cells("Cheque_No").Value.ToString = "" Then
                Me.txtChequeNo.Text = Me.grdLetterOfCreadit.GetRow.Cells("Cheque_No").Value
            Else
                Me.txtChequeNo.Text = String.Empty
            End If
            Me.cmbCostCenter.SelectedValue = Me.grdLetterOfCreadit.GetRow.Cells("CostCenter").Value

            Me.btnSave.Text = "&Update"
    
            txtLCamount_LostFocus(Me.txtLCamount, Nothing)

            If flgDateLock = True Then
                If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") Then
                    'ShowErrorMessage("Previous date work not allowed") : Exit Sub
                    Me.DateTimePicker1.Enabled = False
                Else
                    Me.DateTimePicker1.Enabled = True
                End If
            Else
                Me.DateTimePicker1.Enabled = True
            End If
            Me.txtAdvisingBank.Text = Me.grdLetterOfCreadit.GetRow.Cells("Advising_Bank").Value.ToString
            Me.txtSpecialInstruction.Text = Me.grdLetterOfCreadit.GetRow.Cells("Special_Instruction").Value.ToString
            Me.txtRefNo.Text = Me.grdLetterOfCreadit.GetRow.Cells("Reference_No").Value.ToString
            Me.txtPerformaNo.Text = Me.grdLetterOfCreadit.GetRow.Cells("Performa_No").Value.ToString
            If IsDBNull(Me.grdLetterOfCreadit.GetRow.Cells("Performa_Date").Value) Then
                Me.dtpPerformaDate.Value = Date.Now
            Else
                Me.dtpPerformaDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("Performa_Date").Value
            End If
            Me.txtVessel.Text = Me.grdLetterOfCreadit.GetRow.Cells("Vessel").Value.ToString
            Me.txtBLNo.Text = Me.grdLetterOfCreadit.GetRow.Cells("BL_No").Value.ToString
            If IsDBNull(Me.grdLetterOfCreadit.GetRow.Cells("BL_Date").Value) Then
                Me.dtpBLDate.Value = Date.Now
            Else
                Me.dtpBLDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("BL_Date").Value
            End If
            If IsDBNull(Me.grdLetterOfCreadit.GetRow.Cells("ETD_Date").Value) Then
                Me.dtpETDDate.Value = Date.Now
                Me.dtpETDDate.Checked = False
            Else
                Me.dtpETDDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("ETD_Date").Value
                Me.dtpETDDate.Checked = True
            End If
            If IsDBNull(Me.grdLetterOfCreadit.GetRow.Cells("ETA_Date").Value) Then
                Me.dtpETADate.Value = Date.Now
                Me.dtpETADate.Checked = False
            Else
                Me.dtpETADate.Value = Me.grdLetterOfCreadit.GetRow.Cells("ETA_Date").Value
                Me.dtpETADate.Checked = True
            End If
            If Not Me.grdLetterOfCreadit.GetRow.Cells("NN_Date").Value.ToString = "" Then
                Me.dtpNNDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("NN_Date").Value
            Else
                Me.dtpNNDate.Value = Date.Now
                Me.dtpNNDate.Checked = False
            End If
            If Not Me.grdLetterOfCreadit.GetRow.Cells("BDR_Date").Value.ToString = "" Then
                Me.dtpDBRDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("BDR_Date").Value
            Else
                Me.dtpDBRDate.Value = Date.Now
                Me.dtpDBRDate.Checked = False
            End If
            If Not Me.grdLetterOfCreadit.GetRow.Cells("DD_Date").Value.ToString = "" Then
                Me.dtpDDDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("DD_Date").Value
            Else
                Me.dtpDDDate.Value = Date.Now
                Me.dtpDDDate.Checked = False
            End If
            If Not Me.grdLetterOfCreadit.GetRow.Cells("DTB_Date").Value.ToString = "" Then
                Me.DTBDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("DTB_Date").Value
            Else
                Me.DTBDate.Value = Date.Now
                Me.DTBDate.Checked = False
            End If
            Me.txtFreight.Text = Val(Me.grdLetterOfCreadit.GetRow.Cells("Freight").Value.ToString)
            Me.txtInsurrance.Text = Val(Me.grdLetterOfCreadit.GetRow.Cells("InsurranceValue").Value.ToString)
            Me.txtremarks.Text = Me.grdLetterOfCreadit.GetRow.Cells("Remarks").Value.ToString
            Me.cmbClearingAgent.Text = Me.grdLetterOfCreadit.GetRow.Cells("Clearing_Agent").Value.ToString
            Me.cmbTransporter.SelectedValue = Val(Me.grdLetterOfCreadit.GetRow.Cells("TransporterID").Value.ToString)
            Me.cmbOpenedBy.SelectedValue = Val(Me.grdLetterOfCreadit.GetRow.Cells("OpenedBy").Value.ToString)
            If IsDBNull(Me.grdLetterOfCreadit.GetRow.Cells("Expiry_Date").Value) Then
                Me.dtpExpiryDate.Value = Date.Now
                Me.dtpExpiryDate.Checked = False
            Else
                Me.dtpExpiryDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("Expiry_Date").Value
                Me.dtpExpiryDate.Checked = True
            End If
            Me.cmbCurrency.SelectedValue = Val(Me.grdLetterOfCreadit.GetRow.Cells("CurrencyType").Value.ToString)
            Me.txtCurrencyRate.Text = Val(Me.grdLetterOfCreadit.GetRow.Cells("CurrencyRate").Value.ToString)
            Me.cmbPortOfDischarge.Text = Me.grdLetterOfCreadit.GetRow.Cells("PortOfDischarge").Value.ToString
            Me.cmbPortOfLoading.Text = Me.grdLetterOfCreadit.GetRow.Cells("PortOfLoading").Value.ToString
            Me.cmbOrigin.Text = Me.grdLetterOfCreadit.GetRow.Cells("Origin").Value.ToString
            If IsDBNull(Me.grdLetterOfCreadit.GetRow.Cells("LDS").Value) Then
                Me.dtpLatestDateofShipment.Value = Date.Now
                Me.dtpLatestDateofShipment.Checked = False
            Else
                Me.dtpLatestDateofShipment.Value = Me.grdLetterOfCreadit.GetRow.Cells("LDS").Value
                Me.dtpLatestDateofShipment.Checked = True
            End If
            If IsDBNull(Me.grdLetterOfCreadit.GetRow.Cells("LSDB").Value) Then
                Me.dtpLSBDate.Value = Date.Now
                Me.dtpLSBDate.Checked = False
            Else
                Me.dtpLSBDate.Value = Me.grdLetterOfCreadit.GetRow.Cells("LSDB").Value
                Me.dtpLSBDate.Checked = True
            End If
            Me.cmbStatus.Text = Me.grdLetterOfCreadit.GetRow.Cells("Status").Value.ToString
            Me.txtCostOfMaterial.Text = Val(Me.grdLetterOfCreadit.GetRow.Cells("CostOfMaterial").Value.ToString)
            Me.chkClose.Checked = Me.grdLetterOfCreadit.GetRow.Cells("Closed").Value.ToString
            ApplySecurity(SBUtility.Utility.EnumDataMode.Edit)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings

    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    'Me.PrintListToolStripMenuItem.Enabled = False
                    'PrintToolStripMenuItem.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmCustomerCollection)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                'UserPostingRights = GetUserPostingRights(LoginUserId)
                'If UserPostingRights = True Then
                '    Me.chkPost.Visible = True
                'Else
                '    Me.chkPost.Visible = False
                '    Me.chkPost.Checked = False
                'End If
                GetSecurityByPostingUser(UserPostingRights, btnSave, btnDelete)
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                'Me.chkPost.Visible = False
                'CtrlGrdBar1.mGridPrint.Enabled = False
                'CtrlGrdBar1.mGridExport.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    ElseIf RightsDt.FormControlName = "Print" Then
                        Me.btnPrint.Enabled = True
                        ' CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf RightsDt.FormControlName = "Export" Then
                        'CtrlGrdBar1.mGridExport.Enabled = True
                    ElseIf RightsDt.FormControlName = "Post" Then
                        'Me.chkPost.Visible = True
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            letter = New lettercreditBE
            letter.LCdoc_Id = Me.grdLetterOfCreadit.GetRow.Cells(0).Value
            letter.CostCenter = Val(Me.grdLetterOfCreadit.GetRow.Cells("CostCenter").Value.ToString)
            letter.LCdoc_No = Me.grdLetterOfCreadit.GetRow.Cells("LCDoc_No").Value.ToString
            If New letterCreditDAL().delete(letter) = True Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            If Condition = "bank" Then
                FillDropDown(cmbbank, "select  distinct bank,bank from tblLetterOfCredit  ", False)
            ElseIf Condition = "vendor" Then
                FillUltraDropDown(cmbVendor, " select coa_detail_id,detail_title as Account, detail_code as Code, sub_sub_title as [Account Head], Account_Type as [Type]  from vwcoadetail where account_type IN('Vendor','LC')")
                Me.cmbVendor.Rows(0).Activate()
                If Me.cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
            ElseIf Condition = "paymentfrom" Then
                FillDropDown(cmbpaymentfrom, "select coa_detail_id,detail_title from vwcoadetail where account_type=" & IIf(cmbmethod.SelectedIndex > 0, "'Bank'", "'Cash'"))
            ElseIf Condition = "CostCenter" Then
                FillDropDown(Me.cmbCostCenter, "If  exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") " _
                    & "Select CostCenterID, Name from tblDefCostCenter where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & ") order by SortOrder " _
                    & "Else " _
                    & "Select CostCenterID, Name from tblDefCostCenter where Active = 1 order by SortOrder")
            ElseIf Condition = "Currency" Then
                str = "Select currency_id, Currency_Code From tblCurrency"
                FillDropDown(Me.cmbCurrency, str)
            ElseIf Condition = "Transporter" Then
                FillDropDown(Me.cmbTransporter, "Select TransporterId, TransporterName,AccountId From tblDefTransporter")
            ElseIf Condition = "OpenedBy" Then
                FillDropDown(Me.cmbOpenedBy, "Select Employee_ID, Employee_Name,Dept_Id, Desig_Id From tblDefEmployee")
            ElseIf Condition = "ClearingAgent" Then
                FillDropDown(Me.cmbClearingAgent, "Select DISTINCT Clearing_Agent,Clearing_Agent From tblLetterOfCredit ORDER BY 2 ASC", False)
            ElseIf Condition = "PortDischarge" Then
                FillDropDown(Me.cmbPortOfDischarge, "Select DISTINCT PortOfDischarge, PortOfDischarge From tblLetterOfCredit", False)
            ElseIf Condition = "PortLoading" Then
                FillDropDown(Me.cmbPortOfLoading, "Select DISTINCT PortOfLoading, PortOfLoading From tblLetterOfCredit", False)
            ElseIf Condition = "Origin" Then
                FillDropDown(Me.cmbOrigin, "Select DISTINCT Origin, Origin From tblLetterOfCredit Where Origin <> '' ", False)
            ElseIf Condition = "Status" Then
                FillDropDown(Me.cmbStatus, "Select DISTINCT Status, Status From tblLetterOfCredit Where Status <> '' ", False)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            letter = New lettercreditBE
            letter.LCdoc_Id = LCdoc_Id
            letter.LCdoc_No = Me.txtdocnumber.Text.Replace("'", "''")
            letter.LCdoc_Type = Me.cmbdoctype.Text
            letter.LCdoc_Date = Me.DateTimePicker1.Value
            letter.Bank = Me.cmbbank.Text.ToString.Replace("'", "''")
            letter.vendorId = Me.cmbVendor.Value
            letter.LCAmount = Val(Me.txtLCamount.Text)
            letter.PaidAmount = Val(Me.txtpaidamount.Text)
            letter.LCtype = Me.cmbtypeCFD.Text.ToString.Replace("'", "''")
            letter.LCdescription = Me.txtremarks.Text.Replace("'", "''")
            letter.VoucherTypeId = Me.cmbmethod.SelectedValue
            letter.coa_detail_id = Me.cmbpaymentfrom.SelectedValue
            If Me.Groupbox.Visible = True Then
                letter.Cheque_No = Me.txtChequeNo.Text.Replace("'", "''")
                letter.Cheque_Date = Me.dateCheque.Value
            Else
                letter.Cheque_No = Nothing
                letter.Cheque_Date = Nothing
            End If
            letter.CostCenter = Me.cmbCostCenter.SelectedValue
            letter.Active = Me.chkActive.Checked
            letter.Advising_Bank = Me.txtAdvisingBank.Text
            letter.Special_Instruction = Me.txtSpecialInstruction.Text
            letter.Reference_No = Me.txtRefNo.Text
            letter.Performa_No = Me.txtPerformaNo.Text
            letter.Performa_Date = Me.dtpPerformaDate.Value
            letter.Vessel = Me.txtVessel.Text
            letter.BL_No = Me.txtBLNo.Text
            letter.BL_Date = Me.dtpBLDate.Value
            If dtpETDDate.Checked = True Then
                letter.ETD_Date = Me.dtpETDDate.Value
            Else
                letter.ETD_Date = Date.MinValue
            End If
            If dtpETADate.Checked = True Then
                letter.ETA_Date = Me.dtpETADate.Value
            Else
                letter.ETA_Date = Date.MinValue
            End If
            If dtpNNDate.Checked = True Then
                letter.NN_Date = Me.dtpNNDate.Value
            Else
                letter.NN_Date = Date.MinValue
            End If
            If dtpDDDate.Checked = True Then
                letter.DD_Date = Me.dtpDDDate.Value
            Else
                letter.DD_Date = Date.MinValue
            End If
            If dtpDDDate.Checked = True Then
                letter.DD_Date = Me.dtpDDDate.Value
            Else
                letter.DD_Date = Date.MinValue
            End If
            If dtpDBRDate.Checked = True Then
                letter.BDR_Date = Me.dtpDBRDate.Value
            Else
                letter.BDR_Date = Date.MinValue
            End If
            If DTBDate.Checked = True Then
                letter.DTB_Date = Me.DTBDate.Value
            Else
                letter.DTB_Date = Date.MinValue
            End If
            letter.InsurranceValue = Val(Me.txtInsurrance.Text)
            letter.Freight = Val(Me.txtFreight.Text)
            letter.Remarks = Me.txtremarks.Text
            letter.Clearing_Agent = Me.cmbClearingAgent.Text
            letter.TransporterID = Me.cmbTransporter.SelectedValue
            letter.OpendBy = Me.cmbOpenedBy.SelectedValue
            If Me.dtpExpiryDate.Checked = True Then
                letter.Expiry_Date = Me.dtpExpiryDate.Value
            Else
                letter.Expiry_Date = Date.MinValue
            End If
            letter.CurrencyType = Me.cmbCurrency.SelectedValue
            letter.CurrencyRate = Val(Me.txtCurrencyRate.Text)
            letter.PortDischarge = Me.cmbPortOfDischarge.Text
            letter.PortLoading = Me.cmbPortOfLoading.Text
            If Me.dtpLatestDateofShipment.Checked = True Then
                letter.LatestDateShipment = Me.dtpLatestDateofShipment.Value
            Else
                letter.LatestDateShipment = Date.MinValue
            End If
            If Me.dtpLSBDate.Checked = True Then
                letter.LastDateShipmentBefore = Me.dtpLSBDate.Value
            Else
                letter.LastDateShipmentBefore = Date.MinValue
            End If
            If Not Me.cmbOrigin.Text = "" Then
                letter.Origin = Me.cmbOrigin.Text
            Else
                letter.Origin = ""
            End If
            letter.Status = Me.cmbStatus.Text.ToString
            letter.CostOfMaterial = Val(Me.txtCostOfMaterial.Text)
            letter.Closed = Me.chkClose.CheckState
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Me.grdLetterOfCreadit.DataSource = New letterCreditDAL().getall()
            grdLetterOfCreadit.RetrieveStructure()
            Me.grdLetterOfCreadit.RootTable.Columns("TransporterID").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("CurrencyType").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("CurrencyRate").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("Expiry_Date").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("OpenedBy").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("LCdoc_Date").FormatString = "dd/MMM/yyyy"
            Me.grdLetterOfCreadit.RootTable.Columns("BL_Date").FormatString = "dd/MMM/yyyy"
            Me.grdLetterOfCreadit.RootTable.Columns("ETA_Date").FormatString = "dd/MMM/yyyy"
            Me.grdLetterOfCreadit.RootTable.Columns("ETD_Date").FormatString = "dd/MMM/yyyy"
            Me.grdLetterOfCreadit.RootTable.Columns("Performa_date").FormatString = "dd/MMM/yyyy"

            Me.grdLetterOfCreadit.RootTable.Columns("NN_Date").FormatString = "dd/MMM/yyyy"
            Me.grdLetterOfCreadit.RootTable.Columns("BDR_Date").FormatString = "dd/MMM/yyyy"
            Me.grdLetterOfCreadit.RootTable.Columns("DD_Date").FormatString = "dd/MMM/yyyy"
            Me.grdLetterOfCreadit.RootTable.Columns("DTB_Date").FormatString = "dd/MMM/yyyy"

            Me.grdLetterOfCreadit.RootTable.Columns("InsurranceValue").FormatString = "N" & DecimalPointInValue
            Me.grdLetterOfCreadit.RootTable.Columns("Freight").FormatString = "N" & DecimalPointInValue

            Me.grdLetterOfCreadit.RootTable.Columns("InsurranceValue").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdLetterOfCreadit.RootTable.Columns("Freight").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdLetterOfCreadit.RootTable.Columns("InsurranceValue").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdLetterOfCreadit.RootTable.Columns("Freight").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdLetterOfCreadit.RootTable.Columns("LCdoc_Id").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("LCdoc_No").Caption = " Document No"
            Me.grdLetterOfCreadit.RootTable.Columns("LCdoc_Type").Caption = "Document Type"
            Me.grdLetterOfCreadit.RootTable.Columns("LCdoc_Date").Caption = "Document Date"
            Me.grdLetterOfCreadit.RootTable.Columns("Bank").Caption = "Bank"
            Me.grdLetterOfCreadit.RootTable.Columns("LCAmount").Caption = "LC Amount"
            Me.grdLetterOfCreadit.RootTable.Columns("PaidAmount").Caption = "Paid Amount"
            Me.grdLetterOfCreadit.RootTable.Columns("LCtype").Caption = "LC type"
            Me.grdLetterOfCreadit.RootTable.Columns("LCdescription").Caption = "Remarks"
            Me.grdLetterOfCreadit.RootTable.Columns("VoucherTypeId").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("coa_detail_id").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("Cheque_No").Caption = "Cheque No"
            Me.grdLetterOfCreadit.RootTable.Columns("Cheque_Date").Caption = "Cheque Date"
            Me.grdLetterOfCreadit.RootTable.Columns("Active").Caption = "Active"
            Me.grdLetterOfCreadit.RootTable.Columns("vendorId").Visible = False
            Me.grdLetterOfCreadit.RootTable.Columns("CostCenter").Visible = False
            grdLetterOfCreadit.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtdocnumber.Text = String.Empty Then
                ShowErrorMessage("Please enter document number")
                txtdocnumber.Focus()
                Return False
            End If
            'If Me.cmbbank.Text = String.Empty Then
            '    ShowErrorMessage("Please select a bank")
            '    cmbbank.Focus()
            '    Return False
            'End If
            If Me.cmbVendor.ActiveRow Is Nothing Then
                ShowErrorMessage("LC Account Is Invalid.")
                Me.cmbVendor.Focus()
                Return False
            End If
            If Me.cmbVendor.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select a vendor")
                cmbVendor.Focus()
                Return False
            End If
            'If Me.txtLCamount.Text = String.Empty Then
            '    ShowErrorMessage("Please enter LC amount")
            '    txtLCamount.Focus()
            '    Return False
            'End If
            'If Me.txtpaidamount.Text = String.Empty Then
            '    ShowErrorMessage("Please enter paid amount")
            '    txtpaidamount.Focus()
            '    Return False
            'End If
            'If Me.cmbpaymentfrom.SelectedIndex = 0 Then
            '    ShowErrorMessage("Please select payment from")
            '    cmbpaymentfrom.Focus()
            '    Return False
            'End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            LCdoc_Id = 0
            FillCombos("bank")
            Me.DateTimePicker1.Value = Date.Now
            Me.txtdocnumber.Text = String.Empty
            Me.cmbdoctype.SelectedIndex = 0
            If Not Me.cmbbank.SelectedIndex = -1 Then Me.cmbbank.SelectedIndex = 0
            Me.cmbVendor.Rows(0).Activate()
            Me.txtLCamount.Text = String.Empty
            Me.txtpaidamount.Text = String.Empty
            Me.cmbtypeCFD.SelectedIndex = 0
            Me.txtremarks.Text = String.Empty
            Me.cmbmethod.SelectedIndex = 0
            Me.cmbpaymentfrom.SelectedIndex = 0
            Me.txtChequeNo.Text = String.Empty
            Me.dateCheque.Value = Date.Now
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedIndex = 0
            FillCombos("ClearingAgent")
            FillCombos("PortDischarge")
            FillCombos("PortLoading")
            Me.cmbPortOfLoading.Text = String.Empty
            Me.cmbPortOfDischarge.Text = String.Empty
            Me.cmbClearingAgent.Text = String.Empty
            Me.cmbOrigin.Text = String.Empty
            Me.txtAdvisingBank.Text = String.Empty
            Me.txtSpecialInstruction.Text = String.Empty
            Me.txtRefNo.Text = String.Empty
            Me.txtPerformaNo.Text = String.Empty
            Me.dtpPerformaDate.Value = Date.Now
            Me.txtVessel.Text = String.Empty
            Me.txtBLNo.Text = String.Empty
            Me.dtpBLDate.Value = Date.Now

            Me.dtpETDDate.Value = Date.Now
            Me.dtpETADate.Value = Date.Now
            Me.dtpLatestDateofShipment.Value = Date.Now
            Me.dtpLSBDate.Value = Date.Now
            Me.dtpExpiryDate.Value = Date.Now

            Me.dtpETADate.Checked = False
            Me.dtpETDDate.Checked = False
            Me.dtpExpiryDate.Checked = False
            Me.dtpLatestDateofShipment.Checked = False
            Me.dtpLSBDate.Checked = False



            Me.txtInsurrance.Text = String.Empty
            Me.txtFreight.Text = String.Empty
            Me.txtremarks.Text = String.Empty
            Me.txtCreditLimit.Text = String.Empty
            Me.DTBDate.Value = Date.Now
            Me.dtpNNDate.Value = Date.Now
            Me.dtpDDDate.Value = Date.Now
            Me.dtpDBRDate.Value = Date.Now

            Me.DTBDate.Checked = False
            Me.dtpNNDate.Checked = False
            Me.dtpDDDate.Checked = False
            Me.dtpDBRDate.Checked = False

            Me.cmbStatus.Text = ""
            Me.txtCostOfMaterial.Text = 0
            Me.chkClose.Checked = False


            If Not Me.cmbTransporter.SelectedIndex = -1 Then Me.cmbTransporter.SelectedIndex = 0
            If Not Me.cmbOpenedBy.SelectedIndex = -1 Then Me.cmbOpenedBy.SelectedIndex = 0

            If Me.cmbCurrency.SelectedIndex = -1 Then Me.cmbCurrency.SelectedIndex = 0
            Me.txtCurrencyRate.Text = 0
            GetAllRecords()
            If Me.cmbmethod.SelectedIndex > 0 Then
                Me.Groupbox.Visible = True
                'Me.Label5.Location = New Point(359, 185)
                'Me.cmbCostCenter.Location = New Point(495, 181)
            Else
                Me.Groupbox.Visible = False
                'Me.Label5.Location = New Point(359, 107)
                'Me.cmbCostCenter.Location = New Point(495, 104)
            End If
            ApplySecurity(SBUtility.Utility.EnumDataMode.[New])
            Me.txtdocnumber.ReadOnly = False
            Me.txtdocnumber.Focus()
            Me.DateTimePicker1.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            If New letterCreditDAL().save(letter) = True Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try
            If New letterCreditDAL().update(letter) = True Then
                Return True
            Else : Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmLetterCredit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 29-12-13
        If e.KeyCode = Keys.F4 Then
            If btnSave.Enabled = True Then
                btnSave_Click(Nothing, Nothing)
            End If
        End If
        If e.KeyCode = Keys.Escape Then

            btnNew_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If
        If e.KeyCode = Keys.U AndAlso e.Alt Then
            If Me.btnSave.Text = "&Update" Then
                If Me.btnSave.Enabled = False Then
                    RemoveHandler Me.btnSave.Click, AddressOf Me.btnSave_Click
                End If
            End If
        End If
        If e.KeyCode = Keys.D AndAlso e.Alt Then
            If Me.btnSave.Text = "&Delete" Then
                If Me.btnDelete.Enabled = False Then
                    RemoveHandler Me.btnDelete.Click, AddressOf Me.btnDelete_Click
                End If
            End If
        End If
        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If

    End Sub

    Private Sub frmLetterCredit_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            'R-974 Ehtisham ul Haq user friendly system modification on 31-12-13
            Me.lblProgress.Text = "Loading Please Wait ..."
            Me.lblProgress.BackColor = Color.LightYellow
            Me.lblProgress.Visible = True
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents()
            Dim dt As New DataTable
            Dim dr As DataRow
            dt.Columns.Add("ID", GetType(System.Int16))
            dt.Columns.Add("Name", GetType(System.String))

            dr = dt.NewRow
            dr(0) = 2 'Cash Payment Voucher Type
            dr(1) = "Cash"
            dt.Rows.Add(dr)

            Dim dr1 As DataRow
            dr1 = dt.NewRow
            dr1(0) = 4 ' Payment Voucher Type
            dr1(1) = "Bank"
            dt.Rows.Add(dr1)

            Me.cmbmethod.ValueMember = "ID"
            Me.cmbmethod.DisplayMember = "Name"
            Me.cmbmethod.DataSource = dt

            FillCombos("bank")
            FillCombos("vendor")
            FillCombos("paymentfrom")
            FillCombos("CostCenter")
            FillCombos("Currency")
            FillCombos("Transporter")
            FillCombos("OpenedBy")
            FillCombos("Origin")
            FillCombos("Status")
            'FillCombos("ClearingAgent")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)

        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub frmLetterCredit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmbmethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbmethod.SelectedIndexChanged
        Try
            FillCombos("paymentfrom")
            If Me.cmbmethod.SelectedIndex > 0 Then
                Me.Groupbox.Visible = True
                'Me.cmbCostCenter.Location = New Point(495, 186)
                'Me.Label5.Location = New Point(359, 190)
            Else
                Me.Groupbox.Visible = False
                'Me.cmbCostCenter.Location = New Point(493, 215)
                'Me.Label5.Location = New Point(357, 215)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            'ValidateDateLock()
            'If flgDateLock = True Then ShowErrorMessage("Previous date work not allowed") : Exit Sub
            'If flgDateLock = True Then
            '    If MyDateLock.ToString("yyyy-M-d 00:00:00") >= Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") Then
            '        ShowErrorMessage("Previous date work not allowed") : Exit Sub
            '    End If
            'End If

            If IsDateLock(Me.DateTimePicker1.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If IsValidToDelete("PurchaseOrderMasterTable", "LCId", Me.grdLetterOfCreadit.GetRow.Cells(0).Value) = True Then

                If Delete() = True Then
                    'msg_Information(str_informDelete)
                    ReSetControls()
                End If
            Else
                ShowErrorMessage(str_ErrorDependentRecordFound)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtpaidamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpaidamount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtLCamount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLCamount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtLCamount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLCamount.LostFocus, txtpaidamount.LostFocus
        Try
            Dim txtName As TextBox = CType(sender, TextBox)
            Select Case txtName.Name
                Case Me.txtLCamount.Name
                    Me.txtretiringamount.Text = Val(Me.txtLCamount.Text) - Val(Me.txtpaidamount.Text)
                Case Me.txtpaidamount.Name
                    Me.txtretiringamount.Text = Val(Me.txtLCamount.Text) - Val(Me.txtpaidamount.Text)
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos("vendor")
            Me.cmbVendor.Value = id
            id = Me.cmbpaymentfrom.SelectedValue
            FillCombos("paymentfrom")
            Me.cmbpaymentfrom.SelectedValue = id
            id = Me.cmbCostCenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedValue = id
            id = Me.cmbTransporter.SelectedIndex
            FillCombos("Transporter")
            Me.cmbTransporter.SelectedIndex = id
            id = Me.cmbOpenedBy.SelectedIndex
            FillCombos("OpenedBy")
            Me.cmbOpenedBy.SelectedIndex = id
            id = Me.cmbOrigin.SelectedIndex
            FillCombos("Origin")
            Me.cmbOrigin.SelectedIndex = id
            id = Me.cmbStatus.SelectedIndex
            FillCombos("Status")
            Me.cmbStatus.SelectedIndex = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Vendor"
            FrmAddCustomers.ShowDialog()
            CustId = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos("vendor")
            Me.cmbVendor.Value = CustId
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CashAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CashAccountToolStripMenuItem.Click
        Try
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Cash"
            FrmAddCustomers.ShowDialog()
            CustId = Me.cmbpaymentfrom.SelectedValue
            FillCombos("paymentfrom")
            Me.cmbpaymentfrom.SelectedValue = CustId
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BankAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankAccountToolStripMenuItem.Click
        Try
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Bank"
            FrmAddCustomers.ShowDialog()
            CustId = Me.cmbpaymentfrom.SelectedValue
            FillCombos("paymentfrom")
            Me.cmbpaymentfrom.SelectedValue = CustId
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CostCenterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostCenterToolStripMenuItem.Click
        Try
            Dim id As Integer = 0
            frmAddCostCenter.ShowDialog()
            id = Me.cmbCostCenter.SelectedValue
            FillCombos("CostCenter")
            Me.cmbCostCenter.SelectedValue = id
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    'Private Sub txtdocnumber_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdocnumber.LostFocus
    '    Try
    '        Dim str As String = String.Empty
    '        str = "Select lcDoc_No From tblLetterOfCredit WHERE lcDoc_No='" & Me.txtdocnumber.Text & "'"
    '        Dim dt As New DataTable
    '        dt = GetDataTable(str)
    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                ShowErrorMessage("Docment No already exist")
    '                flg = True
    '                Exit Sub
    '            Else
    '                flg = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Public Function chkDateLock(ByVal DateLock As SBModel.DateLockBE) As Boolean
        Try
            If DateLock.DateLock.ToString("yyyy-M-d 00:00:00") = Me.DateTimePicker1.Value.ToString("yyyy-M-d 00:00:00") Then
                If DateLock.Lock = True Then
                    Return True
                End If
            End If
        Catch ex As Exception

        End Try
    End Function
    Public Sub ValidateDateLock()
        Try
            Dim dateLock As New SBModel.DateLockBE
            dateLock = DateLockList.Find(AddressOf chkDateLock)
            If dateLock IsNot Nothing Then
                If dateLock.DateLock.ToString.Length > 0 Then
                    flgDateLock = True
                Else
                    flgDateLock = False
                End If
            Else
                flgDateLock = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    
    Private Sub grdLetterOfCreadit_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdLetterOfCreadit.KeyDown
        'R-974 Ehtisham ul Haq user friendly system modification on 24-1-14
        If e.KeyCode = Keys.F2 Then
            btnEdit_Click(Nothing, Nothing)
            Exit Sub
        End If

        If e.KeyCode = Keys.Delete Then
            btnDelete_Click(Nothing, Nothing)
            Exit Sub
        End If
    End Sub
    Private Sub txtCurrencyRate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCurrencyRate.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub VendorLCAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VendorLCAccountToolStripMenuItem.Click
        Try
            Dim CustId As Integer = 0
            FrmAddCustomers.FormType = "Vendor"
            FrmAddCustomers.ShowDialog()
            CustId = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos("vendor")
            Me.cmbVendor.Value = CustId
            Exit Sub
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdLetterOfCreadit.GetRow Is Nothing AndAlso grdLetterOfCreadit.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmLetterCredit"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdLetterOfCreadit.CurrentRow.Cells(2).Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "Letter of Credit (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Accounts
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    '' This event is created against TASK : TFS1373 means to save and update credit limit against selected bank. on 23-08-2017
    Private Sub btnSaveCreditLimit_Click(sender As Object, e As EventArgs) Handles btnSaveCreditLimit.Click
        Try
            If Me.cmbbank.Text.Length > 0 AndAlso Val(Me.txtCreditLimit.Text) > 0 Then
                If msg_Confirm("Do you want to set credit limit against selected bank?") = False Then Exit Sub
                If New letterCreditDAL().SaveBankCredit(Me.cmbbank.Text, CDec(Me.txtCreditLimit.Text)) Then
                    msg_Information("Credit limit has been saved successfully.")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbbank_Leave(sender As Object, e As EventArgs) Handles cmbbank.Leave
        Try
            If Me.cmbbank.Text.Length > 0 Then
                Me.txtCreditLimit.Text = New letterCreditDAL().GetBankCredit(Me.cmbbank.Text).ToString
            Else
                Me.txtCreditLimit.Text = String.Empty
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbpaymentfrom_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbpaymentfrom.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Customer or Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = IIf(cmbmethod.SelectedIndex > 0, "'Bank'", "'Cash'")
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbpaymentfrom.SelectedValue = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub cmbVendor_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbVendor.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.AccountType = "'Vendor','LC'"
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbVendor.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Lsayouts\" & Me.Name & "_" & Me.grdLetterOfCreadit.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdLetterOfCreadit.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdLetterOfCreadit.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Letter Of Credit"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class