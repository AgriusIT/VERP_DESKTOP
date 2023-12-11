Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports CrystalDecisions.CrystalReports
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.CrystalReports.Engine.ReportDocument
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class frmInvoiceAdjustment
    Implements IGeneral
    Dim DocId As Integer = 0I
    Dim objInvoiceAdjustment As InvoiceAdjustmentBE
    Dim IsOpenForm As Boolean = False
    Enum enmVoucherList
        voucher_detail_id
        voucher_no
        voucher_date
        AccountCode
        AccountDescription
        AccountHead
        Amount
        AdjAmount
        coa_detail_id
        voucher_id
    End Enum
    Enum enmInvoiceList
        DocId
        DocNo
        InvoiceAmount
        Adjustment
        AccountCode
        AccountTitle
        AccountHead
        AccountId
        InvoiceType
    End Enum
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            If Condition = "InvoiceList" Then
                FillUltraDropDown(Me.cmbInvoiceList, "SP_InvoiceList " & Me.cmbAccount.Value & "")
                Me.cmbInvoiceList.Rows(0).Activate()
                If Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns(enmInvoiceList.AccountId).Hidden = True
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns(enmInvoiceList.DocId).Hidden = True
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns(enmInvoiceList.InvoiceType).Hidden = True
                End If
            ElseIf Condition = "VoucherDetailList" Then
                FillUltraDropDown(Me.cmbInvoiceList, "SP_VoucherDetailList  '" & Me.cmbAccount.ActiveRow.Cells("Account_Type").Value.ToString & "',  " & Me.cmbAccount.Value & "")
                Me.cmbInvoiceList.Rows(0).Activate()
                If Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns(enmInvoiceList.AccountId).Hidden = True
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns(enmInvoiceList.DocId).Hidden = True
                    Me.cmbInvoiceList.DisplayLayout.Bands(0).Columns(enmInvoiceList.InvoiceType).Hidden = True
                End If
            ElseIf Condition = "VoucherList" Then
                If cmbInvoiceList.ActiveRow Is Nothing Then Exit Sub
                FillUltraDropDown(Me.cmbVoucherList, "SP_VoucherBalancesList  " & Val(Me.cmbInvoiceList.ActiveRow.Cells(enmInvoiceList.AccountId).Value.ToString) & "")
                Me.cmbVoucherList.Rows(0).Activate()
                If Me.cmbVoucherList.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVoucherList.DisplayLayout.Bands(0).Columns(enmVoucherList.coa_detail_id).Hidden = True
                    Me.cmbVoucherList.DisplayLayout.Bands(0).Columns(enmVoucherList.voucher_detail_id).Hidden = True
                    Me.cmbVoucherList.DisplayLayout.Bands(0).Columns(enmVoucherList.voucher_id).Hidden = True
                End If
            ElseIf Condition = "VoucherAdjList" Then
                If cmbInvoiceList.ActiveRow Is Nothing Then Exit Sub
                FillUltraDropDown(Me.cmbVoucherList, "SP_VoucherAdjustmnetList  '" & Me.cmbAccount.ActiveRow.Cells("Account_Type").Value.ToString & "',  " & Me.cmbAccount.Value & "")
                Me.cmbVoucherList.Rows(0).Activate()
                If Me.cmbVoucherList.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVoucherList.DisplayLayout.Bands(0).Columns(enmVoucherList.coa_detail_id).Hidden = True
                    Me.cmbVoucherList.DisplayLayout.Bands(0).Columns(enmVoucherList.voucher_detail_id).Hidden = True
                    Me.cmbVoucherList.DisplayLayout.Bands(0).Columns(enmVoucherList.voucher_id).Hidden = True
                End If
            ElseIf Condition = "Accounts" Then
                FillUltraDropDown(Me.cmbAccount, "Select coa_detail_id, detail_title as [Account Title], detail_code as [Account Code], sub_sub_title as [Account Head], Account_Type From vwCOADetail WHERE detail_title <> '' ORDER BY detail_title ASC ")
                Me.cmbAccount.Rows(0).Activate()
                Me.cmbAccount.DisplayLayout.Bands(0).Columns("coa_detail_id").Hidden = True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmInvoiceAdjustment_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try
           
            ReSetControls()

            'TFS3360_Aashir_Added select/contain filters on account feilds in all transaction screens
            UltraDropDownSearching(cmbAccount, frmModProperty.blnListSeachStartWith, frmModProperty.blnListSeachContains)

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbInvoiceList.ActiveRow.Cells(0).Value
            If Me.rbtInvoice.Checked = True Then
                FillCombo("InvoiceList")
                FillCombo("VoucherList")
            Else
                FillCombo("VoucherDetailList")
                FillCombo("VoucherAdjList")
            End If
            Me.cmbInvoiceList.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbInvoiceList_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInvoiceList.Leave
        Try
            If IsOpenForm = True Then
                If Me.cmbInvoiceList.ActiveRow Is Nothing Then Exit Sub
                If Me.rbtInvoice.Checked = True Then
                    FillCombo("VoucherList")
                Else
                    FillCombo("VoucherAdjList")
                End If
                Me.txtInvoiceAmount.Text = Val(Me.cmbInvoiceList.ActiveRow.Cells(enmInvoiceList.InvoiceAmount).Value.ToString)
                Me.txtAdjustedAmount.Text = Val(Me.cmbInvoiceList.ActiveRow.Cells(enmInvoiceList.Adjustment).Value.ToString)
                Me.txtBalance.Text = (Val(Me.txtInvoiceAmount.Text) - Val(Me.txtAdjustedAmount.Text))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbVoucherList_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVoucherList.Leave
        Try
            If IsOpenForm = True Then
                Me.txtVoucherAmount.Text = Val(Me.cmbVoucherList.ActiveRow.Cells(enmVoucherList.Amount).Value.ToString)
                Me.txtSettledAmount.Text = Val(Me.cmbVoucherList.ActiveRow.Cells(enmVoucherList.AdjAmount).Value.ToString)
                Me.txtAdjustmentAmount.Text = Val(Me.txtVoucherAmount.Text) - Val(Me.txtSettledAmount.Text)
                Me.txtAdjustmentAmount.Focus()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtAdjustmentAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdjustmentAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            Me.grd.RootTable.Columns("DocId").Visible = False
            Me.grd.RootTable.Columns("InvoiceId").Visible = False
            Me.grd.RootTable.Columns("VoucherDetailId").Visible = False
            Me.grd.RootTable.Columns("Voucher_Id").Visible = False

            Me.grd.RootTable.Columns("Voucher_No").Caption = "V No"
            Me.grd.RootTable.Columns("Voucher_Date").Caption = "V Date"
            Me.grd.RootTable.Columns("detail_code").Caption = "Account Code"
            Me.grd.RootTable.Columns("detail_title").Caption = "Account Description"
            Me.grd.RootTable.Columns("sub_sub_title").Caption = "Account Head"
            Me.grd.RootTable.Columns("SalesNo").Caption = "Tag Invoice"
            Me.grd.RootTable.Columns("AdjustmentAmount").Caption = "Adj Amount"
            Me.grd.RootTable.Columns("Voucher_Date").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
            Me.grd.RootTable.Columns("AdjustmentAmount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("AdjustmentAmount").TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("AdjustmentAmount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("AdjustmentAmount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("AdjustmentAmount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmExpense)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False

                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        ' Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New InvoiceAdjustmentDAL().Remove(objInvoiceAdjustment) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos

    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try
            objInvoiceAdjustment = New InvoiceAdjustmentBE
            objInvoiceAdjustment.DocId = DocId
            objInvoiceAdjustment.DocDate = Me.dtpAdjDate.Value
            objInvoiceAdjustment.DocNo = Me.txtAdjNo.Text
            objInvoiceAdjustment.InvoiceId = Val(Me.cmbInvoiceList.ActiveRow.Cells(enmInvoiceList.DocId).Value.ToString)
            objInvoiceAdjustment.InvoiceType = IIf(Me.rbtInvoice.Checked = True, Me.cmbInvoiceList.ActiveRow.Cells(enmInvoiceList.InvoiceType).Value.ToString, "Voucher")
            objInvoiceAdjustment.Remarks = String.Empty
            objInvoiceAdjustment.UserName = LoginUserName
            objInvoiceAdjustment.VoucherDetailId = Val(Me.cmbVoucherList.ActiveRow.Cells(enmVoucherList.voucher_detail_id).Value.ToString)
            objInvoiceAdjustment.EntryDate = Now
            objInvoiceAdjustment.coa_detail_id = Val(Me.cmbInvoiceList.ActiveRow.Cells(enmInvoiceList.AccountId).Value.ToString)
            objInvoiceAdjustment.AdjustmentAmount = Val(Me.txtAdjustmentAmount.Text)
            objInvoiceAdjustment.ActivityLog = New ActivityLog
            With objInvoiceAdjustment.ActivityLog
                .UserID = LoginUserId
                .Source = Me.Name
                .LogDateTime = Now
                .LogComments = String.Empty
                .FormCaption = Me.Text
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try


            Dim objDt As New DataTable
            objDt = New InvoiceAdjustmentDAL().GetDallRecords(IIf(Condition = "All", "All", ""))
            objDt.AcceptChanges()
            Me.grd.DataSource = objDt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try

            If (Me.cmbInvoiceList Is Nothing Or Me.cmbInvoiceList.ActiveRow.Cells(0).Value <= 0) Then
                ShowErrorMessage("Please select invoice.")
                Me.cmbInvoiceList.Focus()
                Return False
            End If

            If (Me.cmbVoucherList.ActiveRow Is Nothing Or Me.cmbVoucherList.ActiveRow.Cells(0).Value <= 0) Then
                ShowErrorMessage("Please select voucher.")
                Me.cmbVoucherList.Focus()
                Return False
            End If

            If Val(Me.txtAdjustmentAmount.Text) = 0 Then
                ShowErrorMessage("Please enter adjustment amount.")
                Me.txtAdjustmentAmount.Focus()
                Return False
            End If

            If (Val(Me.txtSettledAmount.Text) + Val(Me.txtAdjustmentAmount.Text)) > Val(Me.txtVoucherAmount.Text) Then
                ShowErrorMessage("Adjustment amount [" & Val(Me.txtVoucherAmount.Text) & "] grater than voucher amount")
                Me.txtAdjustmentAmount.Focus()
                Return False
            End If

            If (Val(Me.txtAdjustmentAmount.Text)) > Val(Me.txtBalance.Text) Then
                ShowErrorMessage("Adjustment amount [" & Val(Me.txtVoucherAmount.Text) & "] grater than invoice amount")
                Me.txtInvoiceAmount.Focus()
                Return False
            End If
            FillModel()
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls

        Try
            Me.btnSave.Text = "&Save"
            DocId = 0I
            Me.dtpAdjDate.Value = Now
            Me.txtAdjNo.Text = UtilityDAL.GetNextDocNo("Adj-" & dtpAdjDate.Value.ToString("yy"), 6, "InvoiceAdjustmentTable", "DocNo", Nothing)
            Me.txtInvoiceAmount.Text = String.Empty
            Me.txtVoucherAmount.Text = String.Empty
            Me.txtSettledAmount.Text = String.Empty
            Me.txtAdjustmentAmount.Text = String.Empty
            Me.txtAdjustedAmount.Text = String.Empty
            Me.txtBalance.Text = String.Empty
            FillCombo("Accounts")
            FillCombo("InvoiceList")
            IsOpenForm = True
            FillCombo("VoucherList")
            Me.cmbInvoiceList.Rows(0).Activate()
            Me.cmbVoucherList.Rows(0).Activate()
            GetAllRecords(String.Empty)
            ApplySecurity(EnumDataMode.[New])
            Me.dtpAdjDate.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try
            If New InvoiceAdjustmentDAL().Add(objInvoiceAdjustment) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
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
            If New InvoiceAdjustmentDAL().Modify(objInvoiceAdjustment) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try

            If Me.grd.RowCount = 0 Then Exit Sub
            Dim objDt As New DataTable
            objDt = CType(Me.cmbInvoiceList.DataSource, DataTable)
            Dim drFound() As DataRow
            drFound = objDt.Select("DocId=" & Val(Me.grd.GetRow.Cells("InvoiceId").Value.ToString) & " AND InvoiceType='" & Me.grd.GetRow.Cells("InvoiceType").Value.ToString & "'")
            If drFound.Length <= 0 Then
                ShowErrorMessage("You can not edit, because adjustment has been completed.")
                Exit Sub
            End If

            Me.btnSave.Text = "&Update"
            DocId = Me.grd.GetRow.Cells("DocId").Value.ToString
            Me.dtpAdjDate.Value = Me.grd.GetRow.Cells("DocDate").Value.ToString
            Me.txtAdjNo.Text = Me.grd.GetRow.Cells("DocNo").Value.ToString
            Me.cmbInvoiceList.Value = Val(Me.grd.GetRow.Cells("InvoiceId").Value.ToString)
            Me.cmbInvoiceList_Leave(Nothing, Nothing)

            Dim objDt1 As New DataTable
            objDt1 = CType(Me.cmbVoucherList.DataSource, DataTable)
            Dim drFound1() As DataRow
            drFound1 = objDt1.Select("voucher_detail_id=" & Val(Me.grd.GetRow.Cells("VoucherDetailId").Value.ToString) & "")
            If drFound1.Length <= 0 Then
                ShowErrorMessage("You can not edit, because adjustment has been completed.")
                Exit Sub
            End If

            Me.cmbVoucherList.Value = Val(Me.grd.GetRow.Cells("VoucherDetailId").Value.ToString)
            Me.cmbVoucherList_Leave(Nothing, Nothing)
            Me.txtAdjustmentAmount.Text = Val(Me.grd.GetRow.Cells("AdjustmentAmount").Value.ToString)
            Me.txtSettledAmount.Text = (Val(Me.txtSettledAmount.Text) - Val(Me.txtAdjustmentAmount.Text))
            ApplySecurity(EnumDataMode.Edit)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grd.RowCount = 0 Then Exit Sub
            objInvoiceAdjustment = New InvoiceAdjustmentBE
            objInvoiceAdjustment.DocId = Me.grd.GetRow.Cells("DocId").Value
            objInvoiceAdjustment.DocNo = Me.grd.GetRow.Cells("DocNo").Value.ToString
            objInvoiceAdjustment.DocDate = Me.grd.GetRow.Cells("DocDate").Value.ToString
            objInvoiceAdjustment.UserName = LoginUserName
            objInvoiceAdjustment.EntryDate = Date.Now
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.DoubleClick
        Try
            Editrecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Try
            GetAllRecords("All")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbAccount_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbAccount.KeyDown
        ''TFS1781 : Ayesha Rehman :Added for Selection of Vendor
        Try
            If e.KeyCode = Keys.F1 Then
                frmAccountSearch.BringToFront()
                frmAccountSearch.ShowDialog()
                If frmAccountSearch.DialogResult = Windows.Forms.DialogResult.OK Then
                    cmbAccount.Value = frmAccountSearch.SelectedAccountId
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try


    End Sub
    Private Sub cmbAccount_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccount.Leave
        Try
            If Me.cmbAccount.ActiveRow Is Nothing Then Exit Sub
            If Me.cmbAccount.IsItemInList = False Then Exit Sub
            If Me.rbtInvoice.Checked = True Then
                FillCombo("InvoiceList")
                FillCombo("VoucherList")
            Else
                FillCombo("VoucherDetailList")
                FillCombo("VoucherAdjList")
            End If
            FillCombo("VoucherList")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub rbtInvoice_CheckedChanged(sender As Object, e As EventArgs) Handles rbtInvoice.CheckedChanged
        Try
            If IsOpenForm = False Then Exit Sub
            Dim id As Integer = 0I
            If Me.cmbInvoiceList.ActiveRow Is Nothing Then
                Me.cmbInvoiceList.Rows(0).Activate()
            End If
            id = Me.cmbInvoiceList.ActiveRow.Cells(0).Value
            If rbtInvoice.Checked = True Then
                FillCombo("InvoiceList")
                FillCombo("VoucherList")
            Else
                FillCombo("VoucherDetailList")
                FillCombo("VoucherAdjList")
            End If
            Me.cmbInvoiceList.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbtVoucher_CheckedChanged(sender As Object, e As EventArgs) Handles rbtVoucher.CheckedChanged
        Try

            If IsOpenForm = False Then Exit Sub
            Dim id As Integer = 0I
            If Me.cmbInvoiceList.ActiveRow Is Nothing Then
                Me.cmbInvoiceList.Rows(0).Activate()
            End If
            id = Me.cmbInvoiceList.ActiveRow.Cells(0).Value
            If rbtInvoice.Checked = False Then
                FillCombo("VoucherDetailList")
                FillCombo("VoucherAdjList")
            Else
                FillCombo("InvoiceList")
                FillCombo("VoucherList")
            End If
            Me.cmbInvoiceList.Value = id

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
            Me.CtrlGrdBar3.txtGridTitle.Text = CompanyTitle & Chr(10) & "Invoice Adjustment"
            'CtrlGrdBar.strForm_Name = CtrlGrdBar.EnmAccountType.Vendors

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class